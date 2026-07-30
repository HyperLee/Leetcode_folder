# LeetCode 278 - First Bad Version

這個專案是 LeetCode 278「First Bad Version（第一個錯誤的版本）」的 C# Console 教學範例。程式保留 LeetCode 題解介面 `FirstBadVersion(int n)`，並在 `Main` 中執行六筆固定案例，驗證二分搜尋能處理官方範例、單一版本、兩側邊界與 32 位元整數上限。

- 題目連結：[LeetCode 278 - First Bad Version](https://leetcode.com/problems/first-bad-version/)
- 執行環境：.NET 10
- 主要語言：C#

## 題目說明

目前共有 `n` 個依序發布的版本，編號為 `1, 2, ..., n`。某一個版本開始沒有通過品質檢查，而且因為後續版本都建立在前一版之上，所以第一個錯誤版本之後的所有版本也都會是錯誤版本。

題目提供 `isBadVersion(version)` API，用來判斷指定版本是否錯誤。目標是在盡量減少 API 呼叫次數的前提下，找出第一個錯誤版本。

例如 `n = 5`、`bad = 4` 時，版本狀態如下：

| 版本 | 1 | 2 | 3 | 4 | 5 |
| --- | --- | --- | --- | --- | --- |
| 是否錯誤 | false | false | false | true | true |

第一個由 `false` 轉為 `true` 的位置是版本 4，因此答案為 4。

## 限制條件

- `1 <= bad <= n <= 2^31 - 1`
- `bad` 一定存在於版本範圍 `[1, n]`。
- `bad` 之前的版本全部正確。
- 從 `bad` 開始到版本 `n` 全部錯誤。
- 應盡量減少 `isBadVersion` API 的呼叫次數。

## 解題概念與出發點

### 單調真假邊界

這題最重要的觀察是 `isBadVersion(version)` 的結果具有單調性。版本狀態只會從 `false` 變成 `true` 一次，不會在後續又變回 `false`：

`false, false, ..., false, true, true, ..., true`

因此問題可以重新描述成：

> 在有序的布林邊界中，找出第一個回傳 `true` 的版本號。

若從版本 1 開始逐一查詢，最壞情況下需要呼叫 API `n` 次，時間複雜度為 `O(n)`。利用單調性，每次檢查中間版本即可排除一半搜尋範圍，把呼叫次數降低為 `O(log n)`。

## 解法一：閉區間二分搜尋

### 設計說明

搜尋範圍使用閉區間 `[left, right]`：

1. 初始設定 `left = 1`、`right = n`。
2. 當 `left < right` 時，計算中間版本：
   - `mid = left + (right - left) / 2`
3. 呼叫 `IsBadVersion(mid)`：
   - 若 `mid` 是錯誤版本，第一個錯誤版本可能就是 `mid`，也可能在左側，因此令 `right = mid`。
   - 若 `mid` 是正確版本，`mid` 與左側版本都不可能是答案，因此令 `left = mid + 1`。
4. 當 `left == right` 時，搜尋區間只剩一個版本；該版本就是第一個錯誤版本。

### 為什麼錯誤版本要保留 `mid`

當 `IsBadVersion(mid)` 回傳 `true`，只能確定答案不會出現在 `mid` 右側，卻不能排除 `mid` 本身。若直接令 `right = mid - 1`，可能把真正的第一個錯誤版本排除，所以必須使用 `right = mid`。

相反地，當 `mid` 是正確版本時，依照題目的單調條件，`mid` 以前也全部正確，因此可以安全地使用 `left = mid + 1` 排除整段 `[left, mid]`。

### 為什麼中點公式不會溢位

直接使用 `(left + right) / 2` 時，`left + right` 可能超過 `int.MaxValue`。本專案改用：

`left + (right - left) / 2`

因為 `right - left` 不會超過目前搜尋區間寬度，所以即使 `n = 2^31 - 1`，中點計算仍可維持在 `int` 的有效範圍內。

### 正確性說明

演算法在每次迴圈開始時都維持以下不變量：

> 第一個錯誤版本一定存在於閉區間 `[left, right]`。

- 初始時，題目保證 `bad` 位於 `[1, n]`，所以不變量成立。
- 若 `mid` 錯誤，答案位於 `[left, mid]`，設定 `right = mid` 後仍保留答案。
- 若 `mid` 正確，答案只能位於 `[mid + 1, right]`，設定 `left = mid + 1` 後仍保留答案。
- 每輪都會縮小區間。當 `left == right` 時，區間內唯一的版本必然是第一個錯誤版本。

因此回傳 `left` 可以得到正確答案。

### 官方範例演示：`n = 5, bad = 4`

初始版本狀態為 `false, false, false, true, true`。

| 輪次 | `left` | `right` | `mid` | `IsBadVersion(mid)` | 下一個範圍 | 說明 |
| --- | ---: | ---: | ---: | --- | --- | --- |
| 1 | 1 | 5 | 3 | false | `[4, 5]` | 版本 3 正確，排除版本 1 到 3 |
| 2 | 4 | 5 | 4 | true | `[4, 4]` | 版本 4 可能是第一個錯誤版本，因此保留 |

此時 `left == right == 4`，回傳版本 4。

### 邊界案例演示

| 案例 | 輸入 | 驗證重點 | 結果 |
| --- | --- | --- | ---: |
| 單一版本 | `n = 1, bad = 1` | 迴圈不需執行，唯一版本直接為答案 | 1 |
| 第一版即錯 | `n = 10, bad = 1` | 搜尋必須持續保留左側邊界 | 1 |
| 最後一版才錯 | `n = 10, bad = 10` | 每次遇到正確版本都能安全右移左邊界 | 10 |
| 一般中段 | `n = 8, bad = 5` | 驗證多輪左右邊界交替收斂 | 5 |
| 整數上限 | `n = 2147483647, bad = 2147483647` | 驗證中點公式不發生整數溢位 | 2147483647 |

### 複雜度分析

- 時間複雜度：`O(log n)`。每次 API 呼叫都會把搜尋範圍縮小約一半。
- 空間複雜度：`O(1)`。除了左右邊界與中點之外，不需要隨輸入規模增加的額外空間。
- API 呼叫次數：`O(log n)`，符合題目要求的最小化方向。

## 本機 `IsBadVersion` 模擬

LeetCode 提交環境會提供真正的 `isBadVersion(version)` API，本機 Console 專案則沒有該服務。為了讓範例可以直接執行，`RunSamples()` 會在每筆案例開始前設定 `simulatedFirstBadVersion`，而 `IsBadVersion(version)` 以 `version >= simulatedFirstBadVersion` 模擬連續錯誤版本。

這個替身只負責建立可重現的教學案例，不會改變 `FirstBadVersion(int n)` 的公開介面或二分搜尋邏輯。

## 專案結構

```bash
leetcode_278/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_278/
    ├── Program.cs
    └── leetcode_278.csproj
```

## 還原、建置與執行

請從 `leetcode_278` 專案根目錄執行下列命令。

還原相依套件：

```bash
dotnet restore leetcode_278/leetcode_278.csproj
```

建置專案：

```bash
dotnet build leetcode_278/leetcode_278.csproj --nologo
```

執行固定案例：

```bash
dotnet run --project leetcode_278/leetcode_278.csproj --no-build
```

目前沒有獨立的自動化測試專案。驗收方式是確認專案可以成功建置，並由 `Main` 中的六筆固定案例比對預期與實際結果。

## 實際輸出

以下內容來自 `dotnet run --project leetcode_278/leetcode_278.csproj --no-build` 的實際輸出：

```text
LeetCode 278 - First Bad Version
解法：在單調的版本區間中使用二分搜尋定位第一個錯誤版本

案例 1：官方範例 - 第 4 版開始錯誤
n：5
bad：4
預期：4
實際：4 => PASS

案例 2：單一版本 - 唯一版本即為錯誤版本
n：1
bad：1
預期：1
實際：1 => PASS

案例 3：第一版即錯 - 所有版本皆錯誤
n：10
bad：1
預期：1
實際：1 => PASS

案例 4：最後一版才錯 - 前面版本皆正確
n：10
bad：10
預期：10
實際：10 => PASS

案例 5：一般中段 - 第 5 版開始錯誤
n：8
bad：5
預期：5
實際：5 => PASS

案例 6：整數上限 - 驗證中點計算不溢位
n：2147483647
bad：2147483647
預期：2147483647
實際：2147483647 => PASS

總結：6/6 筆測試通過
```

## 驗證重點

完成程式或文件調整後，應確認：

- `dotnet restore` 成功。
- `dotnet build` 為 0 個錯誤。
- 六筆案例全部輸出 `PASS`。
- README 的實際輸出區塊與最新執行結果完全一致。
- `git diff --check` 沒有回報多餘空白或換行問題。
