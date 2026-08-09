# LeetCode 2917：找出陣列的 K-or

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個以 .NET 10 Console App 撰寫的教學專案，示範如何從整數陣列中找出 K-or。專案保留一個直接逐位統計的基準解法，並加入「數字優先」與「只走訪已設定 bit」兩種比較實作；執行程式即可用 7 組案例同時驗證三種解法。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：bit-first 逐位統計](#解法一bit-first-逐位統計)
- [解法二：number-first 次數陣列](#解法二number-first-次數陣列)
- [解法三：只列舉已設定的-bit](#解法三只列舉已設定的-bit)
- [三種解法比較](#三種解法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定一個整數陣列 `nums` 與整數 `k`。對每一個二進位 bit 位置分別統計：如果至少有 `k` 個陣列元素在該位置為 `1`，答案的相同位置就設為 `1`；否則維持 `0`。所有符合門檻的 bit 合起來就是陣列的 K-or。

例如 `nums = [7, 12, 9, 8, 9, 15]`、`k = 4`：

- bit 0 在 `7、9、9、15` 中為 `1`，共 4 次，因此答案包含 bit 0。
- bit 3 在 `12、9、8、9、15` 中為 `1`，共 5 次，因此答案包含 bit 3。
- 其他 bit 未達 4 次，不放入答案。
- 結果為二進位 `1001`，也就是十進位 `9`。

原題連結：[2917. Find the K-or of an Array](https://leetcode.com/problems/find-the-k-or-of-an-array/description/)

## 限制條件

- `1 <= nums.Length <= 50`
- `0 <= nums[i] < 2^31`
- `1 <= k <= nums.Length`

三個公開解法都假設輸入符合上述題目限制，不另外拋出參數驗證例外，也不會修改 `nums` 的內容。

## 解題概念與出發點

### 1. 每一個 bit 可以獨立判斷

K-or 不需要考慮進位，也不需要比較整個數字的大小。某個 bit 是否出現在答案中，只取決於該 bit 在多少個輸入數字中為 `1`。因此可以把問題拆成最多 31 個互不影響的計數問題。

### 2. 為什麼只檢查 31 個 bit

題目保證 `nums[i] < 2^31`，所以所有輸入都是非負 `int`，可能使用的 bit 只有第 0 到第 30 位。第 31 位是 `int` 的符號位，不會出現在合法輸入中。

### 3. 如何把合格 bit 寫回答案

若第 `bit` 位的次數至少為 `k`，使用：

```csharp
result |= 1 << bit;
```

`1 << bit` 只保留目標位置，再透過 bitwise OR 把該位置加入答案，不會影響先前已設定的 bit。

### 共用示範案例

以下三種解法都以 `nums = [3, 5, 6]`、`k = 2` 示範：

| 數字 | 二進位 | bit 2 | bit 1 | bit 0 |
| ---: | :---: | ---: | ---: | ---: |
| 3 | `011` | 0 | 1 | 1 |
| 5 | `101` | 1 | 0 | 1 |
| 6 | `110` | 1 | 1 | 0 |
| 出現次數 |  | 2 | 2 | 2 |

三個 bit 都出現 2 次，全部達到 `k = 2`，因此答案是 `111₂ = 7`。

## 解法一：bit-first 逐位統計

對應 API：`FindKOr(int[] nums, int k)`

### 設計說明

這是最直接對應題意的實作。外層固定選擇一個 bit，內層掃描所有數字並累計該 bit 是否為 `1`。完成一個 bit 的統計後，立即判斷是否達到 `k`，若達標就把它寫入答案。

判斷某個數字的第 `bit` 位時使用：

```csharp
(number >> bit) & 1
```

先把目標 bit 右移到最低位，再用 `& 1` 取得 `0` 或 `1`。

### 範例演示流程

以 `[3, 5, 6]`、`k = 2` 為例：

1. 檢查 bit 0：`3、5` 為 `1`，次數是 2，將 bit 0 寫入結果，`result = 001₂`。
2. 檢查 bit 1：`3、6` 為 `1`，次數是 2，將 bit 1 寫入結果，`result = 011₂`。
3. 檢查 bit 2：`5、6` 為 `1`，次數是 2，將 bit 2 寫入結果，`result = 111₂`。
4. bit 3 到 bit 30 的次數都是 0，不改變結果。
5. 回傳 `111₂ = 7`。

### 複雜度

- 時間複雜度：`O(B × n)`，其中 `B = 31`、`n = nums.Length`。
- 額外空間複雜度：`O(1)`。

## 解法二：number-first 次數陣列

對應 API：`FindKOr2(int[] nums, int k)`

### 設計說明

第二種解法交換兩層迴圈的觀察方向。外層先取得一個數字，內層檢查它的 31 個 bit，並把每個為 `1` 的位置累加到 `bitCounts[bit]`。所有數字都處理完後，再統一掃描次數陣列並重建答案。

相較於解法一，這種寫法把「蒐集統計資料」和「依門檻產生答案」分成兩個階段。`bitCounts` 也能直接保留每個位置的完整出現次數，方便除錯或延伸分析。

### 範例演示流程

只列出有使用到的 bit 0～2，計數順序為 `[bit 0, bit 1, bit 2]`：

1. 初始計數：`[0, 0, 0]`。
2. 處理 `3 = 011₂`：bit 0、bit 1 加一，得到 `[1, 1, 0]`。
3. 處理 `5 = 101₂`：bit 0、bit 2 加一，得到 `[2, 1, 1]`。
4. 處理 `6 = 110₂`：bit 1、bit 2 加一，得到 `[2, 2, 2]`。
5. 三個位置的次數都至少為 2，重建出 `111₂ = 7`。

### 複雜度

- 時間複雜度：`O(n × B)`，其中 `B = 31`。
- 額外空間複雜度：`O(B)`；在本題中固定為 31 個整數。

## 解法三：只列舉已設定的 bit

對應 API：`FindKOr3(int[] nums, int k)`

### 設計說明

前兩種解法不論數字有多少個 `1`，都會檢查全部 31 個位置。第三種解法改為只走訪實際為 `1` 的 bit：

1. `BitOperations.TrailingZeroCount(value)` 找出最低位 `1` 的索引。
2. 將該位置的次數加一。
3. `value &= value - 1` 清除目前最低位的 `1`。
4. 重複到 `value` 變成 0。

這個技巧常用於列舉 bitmask 中所有已設定位置。程式先把合法的非負 `int` 轉成 `uint`，使位元操作的意圖更清楚；原始陣列元素並未被修改。

### 範例演示流程

以 `[3, 5, 6]`、`k = 2` 為例：

1. `3 = 011₂`：先找到 bit 0，清除後為 `010₂`；再找到 bit 1，清除後為 0。
2. `5 = 101₂`：先找到 bit 0，清除後為 `100₂`；再找到 bit 2，清除後為 0。
3. `6 = 110₂`：先找到 bit 1，清除後為 `100₂`；再找到 bit 2，清除後為 0。
4. 得到 bit 0、bit 1、bit 2 的次數皆為 2。
5. 共用的答案重建流程將三個 bit 全部設為 `1`，回傳 `111₂ = 7`。

### 複雜度

- 時間複雜度：`O(S + B)`，`S` 是所有輸入數字中 bit 1 的總數；最壞情況仍為 `O(n × B)`。
- 額外空間複雜度：`O(B)`；在本題中固定為 31 個整數。

## 三種解法比較

| 解法 | 走訪方式 | 時間複雜度 | 額外空間 | 教學重點 |
| --- | --- | --- | --- | --- |
| `FindKOr` | bit-first，逐位掃描全部數字 | `O(B × n)` | `O(1)` | 最直接對應題意，容易驗證 |
| `FindKOr2` | number-first，建立次數陣列 | `O(n × B)` | `O(B)` | 分離統計與答案重建階段 |
| `FindKOr3` | 只列舉每個數字已設定的 bit | `O(S + B)` | `O(B)` | 最低位元技巧與稀疏 bit 最佳化 |

`B` 在本題固定為 31，因此三種解法都能輕鬆通過限制。若重視可讀性與額外空間，優先選擇 `FindKOr`；若要保留完整計數或學習位元列舉技巧，可分別參考 `FindKOr2`、`FindKOr3`。

## 測試案例設計

`Main` 會讓三個 API 分別執行下列 7 組案例，因此總共有 21 項獨立檢查：

| 案例 | `nums` | `k` | 預期值 | 驗證重點 |
| --- | --- | ---: | ---: | --- |
| 官方範例一 | `[7,12,9,8,9,15]` | 4 | 9 | 多個 bit 以不同次數達標 |
| 官方範例二 | `[2,12,1,11,4,5]` | 6 | 0 | 沒有 bit 達到全員門檻 |
| 官方範例三 | `[10,8,5,9,11,6,8]` | 1 | 15 | `k = 1` 等同一般 OR |
| 單一零值 | `[0]` | 1 | 0 | 最小長度與零值 |
| 重複值 | `[5,5,2]` | 2 | 5 | 重複數字剛好達標 |
| 交錯支援 | `[3,5,6]` | 2 | 7 | 每個 bit 由不同數字組合達標 |
| 第 30 位 | `[1073741824,1073741824,0]` | 2 | 1073741824 | 合法 `int` 最高可用 bit |

每項檢查會列出 `Expected`、`Actual` 與 `PASS/FAIL`。只要有任何結果不符，程式就把結束碼設為非零，方便在終端機或 CI 環境辨識失敗。

## 專案結構

```text
leetcode_2917/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_2917.sln
└── leetcode_2917/
    ├── leetcode_2917.csproj
    └── Program.cs
```

## 建置與執行

需要安裝支援 .NET 10 的 SDK。以下命令皆從本 repository 根目錄執行：

```bash
dotnet restore leetcode_2917/leetcode_2917.csproj
dotnet build leetcode_2917/leetcode_2917.csproj --nologo
dotnet run --no-build --project leetcode_2917/leetcode_2917.csproj
dotnet format leetcode_2917/leetcode_2917.csproj --verify-no-changes --no-restore
```

專案目前沒有獨立的自動化測試專案；`Main` 內的確定性測試資料、建置結果與非零失敗結束碼共同作為驗收依據。

## 實際執行結果

以下內容來自 `dotnet run --no-build --project leetcode_2917/leetcode_2917.csproj` 的實際輸出：

```text
Case: 官方範例一：四個數字支援 bit 0 與 bit 3
Input: nums = [7, 12, 9, 8, 9, 15], k = 4
  FindKOr: Expected = 9, Actual = 9, Result = PASS
  FindKOr2: Expected = 9, Actual = 9, Result = PASS
  FindKOr3: Expected = 9, Actual = 9, Result = PASS

Case: 官方範例二：沒有 bit 出現在全部數字中
Input: nums = [2, 12, 1, 11, 4, 5], k = 6
  FindKOr: Expected = 0, Actual = 0, Result = PASS
  FindKOr2: Expected = 0, Actual = 0, Result = PASS
  FindKOr3: Expected = 0, Actual = 0, Result = PASS

Case: 官方範例三：k 為 1 等同一般 OR
Input: nums = [10, 8, 5, 9, 11, 6, 8], k = 1
  FindKOr: Expected = 15, Actual = 15, Result = PASS
  FindKOr2: Expected = 15, Actual = 15, Result = PASS
  FindKOr3: Expected = 15, Actual = 15, Result = PASS

Case: 單一零值
Input: nums = [0], k = 1
  FindKOr: Expected = 0, Actual = 0, Result = PASS
  FindKOr2: Expected = 0, Actual = 0, Result = PASS
  FindKOr3: Expected = 0, Actual = 0, Result = PASS

Case: 重複值剛好達到門檻
Input: nums = [5, 5, 2], k = 2
  FindKOr: Expected = 5, Actual = 5, Result = PASS
  FindKOr2: Expected = 5, Actual = 5, Result = PASS
  FindKOr3: Expected = 5, Actual = 5, Result = PASS

Case: 不同數字共同支援三個 bit
Input: nums = [3, 5, 6], k = 2
  FindKOr: Expected = 7, Actual = 7, Result = PASS
  FindKOr2: Expected = 7, Actual = 7, Result = PASS
  FindKOr3: Expected = 7, Actual = 7, Result = PASS

Case: 第 30 位邊界
Input: nums = [1073741824, 1073741824, 0], k = 2
  FindKOr: Expected = 1073741824, Actual = 1073741824, Result = PASS
  FindKOr2: Expected = 1073741824, Actual = 1073741824, Result = PASS
  FindKOr3: Expected = 1073741824, Actual = 1073741824, Result = PASS

Summary: 21/21 checks passed.
```
