# LeetCode 2799 — 統計完整子陣列的數目

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/C%23-console-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個以 .NET 10 Console App 實作的教學專案，示範兩種 O(n) 滑動視窗方法，統計陣列中的「完整子陣列」。程式進入點內建五組可重複執行的案例，會同時驗證兩種解法並輸出 PASS/FAIL。

## 快速連結

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：固定左界並尋找最短完整視窗](#解法一固定左界並尋找最短完整視窗)
- [解法二：至多-k-種減去至多-k---1-種](#解法二至多-k-種減去至多-k---1-種)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

[2799. Count Complete Subarrays in an Array](https://leetcode.com/problems/count-complete-subarrays-in-an-array/description/)

給定一個由正整數組成的陣列 `nums`。若某個連續、非空子陣列所包含的相異元素數量，與整個 `nums` 的相異元素數量相同，便稱它為「完整子陣列」。請回傳完整子陣列的總數。

例如 `nums = [1, 3, 1, 2, 2]` 一共有三種相異元素 `{1, 2, 3}`，因此子陣列必須同時包含 1、2、3 才算完整。符合條件的四個子陣列是：

- `[1, 3, 1, 2]`
- `[1, 3, 1, 2, 2]`
- `[3, 1, 2]`
- `[3, 1, 2, 2]`

### 限制條件

- `1 <= nums.length <= 1000`
- `1 <= nums[i] <= 2000`
- 答案可使用 32 位元整數表示；長度上限為 1000 時，子陣列總數最多為 `1000 * 1001 / 2 = 500500`。

## 解題概念與出發點

先令 `k` 為整個陣列的相異元素數量。因為子陣列取自原陣列，不可能出現原陣列以外的新元素，所以「相異元素數量等於 k」也就等同「包含原陣列的全部相異元素」。

直接枚舉所有左右邊界需要 O(n²) 個子陣列；若還要逐一計算相異元素，成本會更高。兩種實作都利用滑動視窗讓左右指標只向右移動，避免重複掃描：

| 解法 | 核心問題 | 計數方式 |
| --- | --- | --- |
| `CountCompleteSubarrays` | 固定左界後，第一個完整視窗在哪裡？ | 找到最短完整視窗後，一次加入所有可向右延伸的答案 |
| `CountCompleteSubarrays2` | 有多少子陣列至多包含指定種數？ | `atMost(k) - atMost(k - 1)` 得到恰好 k 種 |

兩個公開方法都只讀取 `nums`，不會改變陣列內容。

## 解法一：固定左界並尋找最短完整視窗

### 設計說明

`CountCompleteSubarrays` 使用半開區間 `[left, right)` 表示目前視窗，並以 `Dictionary<int, int>` 記錄各數值的出現次數。

1. 先用 `HashSet<int>` 求出整個陣列需要包含的相異元素數 `requiredDistinct`。
2. 固定 `left`，持續增加 `right`，直到字典內的相異元素數量等於 `requiredDistinct`。
3. 此時 `[left, right)` 是目前左界下第一個完整視窗。它的結尾是 `right - 1`。
4. 把結尾從 `right - 1` 延伸到陣列末端，只會增加元素，不會失去完整性，因此可直接加入：

   ```text
   length - (right - 1) = length - right + 1
   ```

5. 左界右移前，從頻率字典移除離開視窗的元素。`right` 不必退回，因為先前掃過的位置不可能成為新左界下更晚才需要重讀的資料。

### 範例演示

以 `[1, 3, 1, 2, 2]` 為例，`requiredDistinct = 3`：

| `left` | 找到的最短完整視窗 | `right` | 新增答案數 | 說明 |
| ---: | --- | ---: | ---: | --- |
| 0 | `[1, 3, 1, 2]` | 4 | `5 - 4 + 1 = 2` | 結尾可選索引 3 或 4 |
| 1 | `[3, 1, 2]` | 4 | `5 - 4 + 1 = 2` | 不必移動右界，視窗仍完整 |
| 2 | 無 | 5 | 0 | 剩餘範圍缺少 3 |
| 3 | 無 | 5 | 0 | 剩餘範圍缺少 1、3 |
| 4 | 無 | 5 | 0 | 只有元素 2 |

總數為 `2 + 2 = 4`。

### 正確性理由

- 對每個 `left`，迴圈停止時的 `[left, right)` 是第一個完整視窗；因此更短的結尾都不完整。
- 所有從 `right - 1` 到陣列末端的結尾都包含這個最短完整視窗，所以全部合法。
- 每個完整子陣列都有唯一的左界，演算法按左界計數，不會遺漏或重複。

### 複雜度

- 時間：O(n)。每個元素至多被右界加入一次、被左界移除一次。
- 空間：O(k)。頻率字典最多保存 k 種元素。

## 解法二：至多 k 種減去至多 k - 1 種

### 設計說明

`CountCompleteSubarrays2` 把「恰好包含 k 種元素」改寫為兩個容易以滑動視窗計算的集合差：

```text
exactly(k) = atMost(k) - atMost(k - 1)
```

私有 helper `CountSubarraysWithAtMostDistinct` 負責計算 `atMost(maxDistinct)`：

1. 右界每次加入一個元素並更新頻率。
2. 若視窗相異元素數超過上限，就持續移動左界，直到重新合法。
3. 固定目前右界時，從 `left` 到 `right` 的每個位置都能作為合法起點，所以新增 `right - left + 1` 個子陣列。

`atMost(k)` 包含所有可能子陣列，因為整個陣列本來就只有 k 種元素；扣除缺少至少一種元素、也就是至多 k - 1 種的子陣列後，剩下的正是完整子陣列。

### 範例演示

對 `[1, 3, 1, 2, 2]` 而言，`k = 3`，全部 15 個非空子陣列都屬於 `atMost(3)`。

計算 `atMost(2)` 時：

| `right` | 加入元素 | 收縮後的最長合法視窗 | 本輪新增 | 累計 |
| ---: | ---: | --- | ---: | ---: |
| 0 | 1 | `[1]` | 1 | 1 |
| 1 | 3 | `[1, 3]` | 2 | 3 |
| 2 | 1 | `[1, 3, 1]` | 3 | 6 |
| 3 | 2 | `[1, 2]` | 2 | 8 |
| 4 | 2 | `[1, 2, 2]` | 3 | 11 |

因此：

```text
完整子陣列數 = atMost(3) - atMost(2)
             = 15 - 11
             = 4
```

### 正確性理由

- helper 收縮完成後，`[left, right]` 是以 `right` 結尾的最長合法視窗。
- 刪除其左側元素不會增加相異元素數，因此同一右界共有 `right - left + 1` 個合法起點。
- 每個子陣列只在自己的右界被計數一次；兩個 `atMost` 結果的集合差恰好留下含 k 種元素的子陣列。

### 複雜度

- 時間：O(n)。helper 執行兩次仍是 O(2n)，化簡為 O(n)。
- 空間：O(k)。兩次 helper 依序執行，各自的頻率字典最多保存 k 種元素。

## 測試案例

專案未建立額外測試專案；`Main` 是可執行的自我驗證入口。每個案例都會分別呼叫兩種解法，共進行 10 項檢查。

| 案例 | 輸入 | 預期 | 驗證重點 |
| --- | --- | ---: | --- |
| 官方範例 | `[1,3,1,2,2]` | 4 | 一般重複元素與多個有效結尾 |
| 全部相同 | `[5,5,5,5]` | 10 | 每個非空子陣列都完整 |
| 全部相異 | `[1,2,3,4]` | 1 | 只有整個陣列完整 |
| 最小長度 | `[1]` | 1 | 題目允許的最小輸入 |
| 相異元素交錯出現 | `[1,2,1,3,2]` | 5 | 左右界多次保留或收縮 |

## 建置與執行

需求：安裝支援 `net10.0` 的 [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)。在本專案根目錄依序執行：

```bash
dotnet restore leetcode_2799/leetcode_2799.csproj
dotnet build leetcode_2799/leetcode_2799.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_2799/leetcode_2799.csproj
```

若任一案例失敗，輸出會顯示 `FAIL`，程序結束碼也會設為非零。

## 實際執行結果

以下內容來自上述 `dotnet run` 命令的實際輸出：

```text
案例：官方範例：包含重複元素
輸入：[1, 3, 1, 2, 2]
預期：4
CountCompleteSubarrays: Actual = 4, PASS
CountCompleteSubarrays2: Actual = 4, PASS

案例：全部相同
輸入：[5, 5, 5, 5]
預期：10
CountCompleteSubarrays: Actual = 10, PASS
CountCompleteSubarrays2: Actual = 10, PASS

案例：全部相異
輸入：[1, 2, 3, 4]
預期：1
CountCompleteSubarrays: Actual = 1, PASS
CountCompleteSubarrays2: Actual = 1, PASS

案例：最小長度
輸入：[1]
預期：1
CountCompleteSubarrays: Actual = 1, PASS
CountCompleteSubarrays2: Actual = 1, PASS

案例：相異元素交錯出現
輸入：[1, 2, 1, 3, 2]
預期：5
CountCompleteSubarrays: Actual = 5, PASS
CountCompleteSubarrays2: Actual = 5, PASS

總結：10/10 項測試通過
```

## 專案結構

```text
leetcode_2799/
├── docs/
│   └── readme-template.md
├── leetcode_2799/
│   ├── leetcode_2799.csproj
│   └── Program.cs
├── leetcode_2799.sln
└── README.md
```