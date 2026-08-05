# LeetCode 1550：存在連續三個奇數的陣列

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)

這是一個以 C# / .NET 10 實作的主控台專案，解決 LeetCode 1550「Three Consecutive Odds」。專案包含四種解法：逐一檢查固定長度三的視窗、維護連續奇數區段邊界、記錄連續奇數數量，以及使用位元運算判斷奇偶。

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法比較](#解法比較)
- [解法一：固定三格視窗](#解法一固定三格視窗)
- [解法二：連續奇數滑動視窗](#解法二連續奇數滑動視窗)
- [解法三：連續奇數計數](#解法三連續奇數計數)
- [解法四：位元運算判斷奇偶](#解法四位元運算判斷奇偶)
- [固定案例與執行結果](#固定案例與執行結果)
- [建置與執行](#建置與執行)
- [專案結構](#專案結構)

## 題目說明

給定一個整數陣列 `arr`，如果陣列中存在三個相鄰且皆為奇數的元素，就回傳 `true`；如果整個陣列都找不到這樣的連續區段，就回傳 `false`。

- [LeetCode 英文題目](https://leetcode.com/problems/three-consecutive-odds/description/)
- [LeetCode 中文題目](https://leetcode.cn/problems/three-consecutive-odds/description/)

例如：

- `[2, 6, 4, 1]` 沒有三個連續奇數，因此結果是 `false`。
- `[1, 2, 34, 3, 4, 5, 7, 23, 12]` 包含 `[5, 7, 23]`，因此結果是 `true`。

## 限制條件

- `1 <= arr.Length <= 1000`
- `1 <= arr[i] <= 1000`
- 陣列元素是正整數，因此可用 `arr[i] % 2 == 1` 判斷奇數。

## 解題概念與出發點

題目只關心「是否有三個相鄰元素全部為奇數」，不需要排序，也不需要改變陣列內容。最直接的思考方式是：

1. 每次只觀察連續三個元素。
2. 判斷這三個元素是否全部為奇數。
3. 只要找到一組符合條件的區段，就可以立即回傳 `true`。
4. 所有區段都檢查完仍沒有找到，才回傳 `false`。

四個方法採用相同的核心條件，但保存的狀態不同：第一個方法直接檢查三個位置；第二個方法維護連續奇數區段的左右邊界；第三個方法只記錄目前連續奇數的數量；第四個方法以位元運算取代餘數判斷奇偶。

## 解法比較

| 方法 | 核心作法 | 時間複雜度 | 額外空間 | 是否修改輸入 |
| --- | --- | --- | --- | --- |
| `ThreeConsecutiveOdds` | 以 `i` 為右端點，檢查 `arr[i - 2]`、`arr[i - 1]`、`arr[i]` | `O(n)` | `O(1)` | 否 |
| `ThreeConsecutiveOdds2` | `right` 向右掃描，`left` 指向最近一個偶數之後的位置 | `O(n)` | `O(1)` | 否 |
| `ThreeConsecutiveOdds3` | 以 `consecutiveOddCount` 記錄目前連續奇數區段長度 | `O(n)` | `O(1)` | 否 |
| `ThreeConsecutiveOddsByBitwise` | 以 `(value & 1)` 讀取最低位元判斷奇偶，再累計連續數量 | `O(n)` | `O(1)` | 否 |

四種方法遇到符合條件的區段都會提早結束，所以最佳情況可能在前幾個元素就完成；最壞情況仍需掃描整個陣列。

## 解法一：固定三格視窗

對長度為 `n` 的陣列，第一個完整的三元素區段是索引 `[0, 2]`，所以可以讓 `i` 從 `2` 開始，將 `i` 視為目前視窗的右端點：

```text
[arr[i - 2], arr[i - 1], arr[i]]
```

每次迴圈檢查三個值的餘數是否都是 `1`。由於題目限制陣列值為正整數，奇數除以 2 的餘數就是 `1`。

### 設計步驟

1. 取得陣列長度 `n`。
2. 從 `i = 2` 開始掃描到 `n - 1`。
3. 檢查目前三格是否全部為奇數。
4. 若是，立即回傳 `true`。
5. 若所有三格視窗都不符合，回傳 `false`。

### 範例演示：官方 true 案例

輸入：

```text
[1, 2, 34, 3, 4, 5, 7, 23, 12]
```

逐一檢查的視窗如下：

| `i` | 檢查區段 | 結果 |
| ---: | --- | --- |
| 2 | `[1, 2, 34]` | 含偶數，繼續 |
| 3 | `[2, 34, 3]` | 含偶數，繼續 |
| 4 | `[34, 3, 4]` | 含偶數，繼續 |
| 5 | `[3, 4, 5]` | 含偶數，繼續 |
| 6 | `[4, 5, 7]` | 含偶數，繼續 |
| 7 | `[5, 7, 23]` | 三個都是奇數，回傳 `true` |

這個方法會檢查每一個可能的長度為 3 的連續區段，因此不會漏掉答案。

### 邊界案例

當陣列長度小於 3 時，根本不存在長度為 3 的連續區段。方法不需要額外建立陣列；`for` 迴圈沒有可執行的索引，最後直接回傳 `false`。

## 解法二：連續奇數滑動視窗

第二個方法不直接保存三個元素，而是維護目前連續奇數區段的左右邊界：

- `right` 從左到右逐一掃描每個元素。
- `left` 指向最近一個偶數的下一個位置。
- `[left, right]` 就是目前連續奇數區段。
- 當 `right - left + 1 == 3` 時，代表已找到三個連續奇數。

遇到偶數時，連續奇數區段被中斷，因此將 `left` 設為 `right + 1`。之後的奇數會從新位置重新累計，不需要把視窗內的元素逐一移除。

### 設計步驟

1. 若陣列長度小於 3，直接回傳 `false`。
2. 初始化 `left = 0`、`right = 0`。
3. 若 `arr[right]` 是偶數，將 `left` 移到 `right + 1`。
4. 計算目前區段長度 `right - left + 1`。
5. 長度達到 3 就回傳 `true`，否則將 `right` 向右移動一格。
6. 掃描結束仍未達到長度 3，回傳 `false`。

### 範例演示：追蹤滑動視窗

仍使用：

```text
[1, 2, 34, 3, 4, 5, 7, 23, 12]
```

| `right` | `arr[right]` | 更新後 `left` | 目前連續奇數區段 | 長度 | 結果 |
| ---: | ---: | ---: | --- | ---: | --- |
| 0 | 1 | 0 | `[1]` | 1 | 繼續 |
| 1 | 2 | 2 | 空區段 | 0 | 偶數重設左界 |
| 2 | 34 | 3 | 空區段 | 0 | 偶數重設左界 |
| 3 | 3 | 3 | `[3]` | 1 | 繼續 |
| 4 | 4 | 5 | 空區段 | 0 | 偶數重設左界 |
| 5 | 5 | 5 | `[5]` | 1 | 繼續 |
| 6 | 7 | 5 | `[5, 7]` | 2 | 繼續 |
| 7 | 23 | 5 | `[5, 7, 23]` | 3 | 回傳 `true` |

這個方法把「連續三個奇數」轉成「目前連續奇數區段長度是否達到 3」，每個元素只被 `right` 讀取一次，因此時間複雜度仍是 `O(n)`。

## 解法三：連續奇數計數

第三個方法不需要保存視窗的左右索引，只維護一個 `consecutiveOddCount`，代表「目前連續奇數區段的長度」：

- 讀到奇數時，將計數加一。
- 讀到偶數時，連續區段被中斷，將計數歸零。
- 計數達到 `3` 時，代表目前最後三個元素都是奇數，可以立即回傳 `true`。

### 設計步驟

1. 將 `consecutiveOddCount` 初始化為 `0`。
2. 由左到右逐一讀取陣列元素。
3. 若元素是奇數，增加連續奇數計數。
4. 若元素是偶數，將連續奇數計數重設為 `0`。
5. 計數達到 `3` 時回傳 `true`。
6. 掃描完整個陣列仍未達到 `3`，回傳 `false`。

### 範例演示：追蹤連續奇數計數

使用：

```text
[1, 2, 34, 3, 4, 5, 7, 23, 12]
```

| 讀取值 | 奇偶 | `consecutiveOddCount` | 結果 |
| ---: | --- | ---: | --- |
| 1 | 奇數 | 1 | 繼續 |
| 2 | 偶數 | 0 | 重設計數 |
| 34 | 偶數 | 0 | 重設計數 |
| 3 | 奇數 | 1 | 繼續 |
| 4 | 偶數 | 0 | 重設計數 |
| 5 | 奇數 | 1 | 繼續 |
| 7 | 奇數 | 2 | 繼續 |
| 23 | 奇數 | 3 | 回傳 `true` |

每個元素只被掃描一次，因此時間複雜度為 `O(n)`，只使用一個計數器，額外空間複雜度為 `O(1)`。

## 解法四：位元運算判斷奇偶

第四個方法保留解法三的連續奇數計數概念，但改用位元運算判斷奇偶。題目限制 `arr[i]` 為正整數，而正整數的二進位表示中：

- 奇數的最低位元一定是 `1`，例如 `5` 的二進位結尾是 `...001`。
- 偶數的最低位元一定是 `0`，例如 `6` 的二進位結尾是 `...110`。
- 因此 `(value & 1) == 1` 時代表 `value` 是奇數，否則就是偶數。

### 設計步驟

1. 將 `consecutiveOddCount` 初始化為 `0`。
2. 由左到右讀取每個元素，使用 `(value & 1)` 取得最低位元。
3. 最低位元為 `1` 時增加連續奇數計數。
4. 最低位元為 `0` 時將計數重設為 `0`。
5. 計數達到 `3` 時立即回傳 `true`。
6. 掃描結束仍未達到 `3`，回傳 `false`。

這個方法與解法三維護相同的狀態，因此時間複雜度為 `O(n)`、額外空間複雜度為 `O(1)`。位元運算讓「判斷最低位元」的意圖更直接，但不改變整體複雜度。

## 固定案例與執行結果

`Main` 透過 `RunTestCase` 對每個案例呼叫四個方法，並比較預期與實際結果。每次呼叫前都會建立陣列複本，確保四種解法收到相同的原始輸入。案例涵蓋官方範例、剛好三個元素、少於三個元素、全偶數與重複奇數。

| 案例 | 輸入 | 預期 | 驗證重點 |
| --- | --- | --- | --- |
| 1 | `[2, 6, 4, 1]` | `False` | 官方 false 案例 |
| 2 | `[1, 2, 34, 3, 4, 5, 7, 23, 12]` | `True` | 官方 true 案例 |
| 3 | `[1, 3, 5]` | `True` | 最小的成功視窗 |
| 4 | `[1, 3]` | `False` | 陣列長度小於 3 |
| 5 | `[2, 4, 6, 8]` | `False` | 所有元素都是偶數 |
| 6 | `[1, 1, 1, 2]` | `True` | 重複奇數仍可形成連續區段 |

### 實際輸出

以下內容來自 `dotnet run --project leetcode_1550/leetcode_1550.csproj` 的完整執行結果：

```text
LeetCode 1550 - Three Consecutive Odds

案例 1：官方案例：沒有三個連續奇數
輸入：[2, 6, 4, 1]
預期：False
ThreeConsecutiveOdds（固定三格視窗）：實際 False -> PASS
ThreeConsecutiveOdds2（連續奇數滑動視窗）：實際 False -> PASS
ThreeConsecutiveOdds3（連續奇數計數）：實際 False -> PASS
ThreeConsecutiveOddsByBitwise（位元運算判斷奇偶）：實際 False -> PASS

案例 2：官方案例：中段出現三個連續奇數
輸入：[1, 2, 34, 3, 4, 5, 7, 23, 12]
預期：True
ThreeConsecutiveOdds（固定三格視窗）：實際 True -> PASS
ThreeConsecutiveOdds2（連續奇數滑動視窗）：實際 True -> PASS
ThreeConsecutiveOdds3（連續奇數計數）：實際 True -> PASS
ThreeConsecutiveOddsByBitwise（位元運算判斷奇偶）：實際 True -> PASS

案例 3：恰好三個元素
輸入：[1, 3, 5]
預期：True
ThreeConsecutiveOdds（固定三格視窗）：實際 True -> PASS
ThreeConsecutiveOdds2（連續奇數滑動視窗）：實際 True -> PASS
ThreeConsecutiveOdds3（連續奇數計數）：實際 True -> PASS
ThreeConsecutiveOddsByBitwise（位元運算判斷奇偶）：實際 True -> PASS

案例 4：少於三個元素
輸入：[1, 3]
預期：False
ThreeConsecutiveOdds（固定三格視窗）：實際 False -> PASS
ThreeConsecutiveOdds2（連續奇數滑動視窗）：實際 False -> PASS
ThreeConsecutiveOdds3（連續奇數計數）：實際 False -> PASS
ThreeConsecutiveOddsByBitwise（位元運算判斷奇偶）：實際 False -> PASS

案例 5：全部為偶數
輸入：[2, 4, 6, 8]
預期：False
ThreeConsecutiveOdds（固定三格視窗）：實際 False -> PASS
ThreeConsecutiveOdds2（連續奇數滑動視窗）：實際 False -> PASS
ThreeConsecutiveOdds3（連續奇數計數）：實際 False -> PASS
ThreeConsecutiveOddsByBitwise（位元運算判斷奇偶）：實際 False -> PASS

案例 6：重複奇數
輸入：[1, 1, 1, 2]
預期：True
ThreeConsecutiveOdds（固定三格視窗）：實際 True -> PASS
ThreeConsecutiveOdds2（連續奇數滑動視窗）：實際 True -> PASS
ThreeConsecutiveOdds3（連續奇數計數）：實際 True -> PASS
ThreeConsecutiveOddsByBitwise（位元運算判斷奇偶）：實際 True -> PASS

總結：24/24 通過
```

## 建置與執行

需要安裝 [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)。請從 `leetcode_1550` repository 根目錄執行：

```powershell
dotnet restore leetcode_1550/leetcode_1550.csproj
dotnet build leetcode_1550/leetcode_1550.csproj --nologo
dotnet run --project leetcode_1550/leetcode_1550.csproj
```

本專案目前沒有獨立的自動化測試專案；`Main` 內的固定案例 runner 會執行 12 次方法檢查，並以 `PASS/FAIL` 與最終通過總數作為範例驗收。

若要檢查 Git 變更中的空白，可從父層 Git 根目錄執行：

```powershell
git -c safe.directory=C:/GitHubFolder/Leetcode_folder diff --check -- leetcode_1550
```

## 專案結構

```text
leetcode_1550/
├── docs/
│   └── readme-template.md
├── leetcode_1550/
│   ├── leetcode_1550.csproj
│   └── Program.cs
├── leetcode_1550.sln
└── README.md
```

四種解法都只讀取輸入陣列，不會修改呼叫者傳入的元素順序或內容。`RunTestCase` 會在每次呼叫前建立陣列複本，讓每一種解法都以相同的原始案例獨立執行。
