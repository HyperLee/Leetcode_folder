# LeetCode 1399：統計最大組的數目

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Language](https://img.shields.io/badge/C%23-Console-239120?logo=csharp)

這是一個可直接執行的 .NET 10 Console 教學專案。程式保留字典統計的基準解法，並加入固定大小計數陣列與各位數和遞推，方便比較相同題目在資料結構、時間與空間上的不同取捨。

## 快速導覽

- [題目說明](#題目說明)
- [核心概念](#核心概念)
- [解法一：Dictionary 分組統計](#解法一dictionary-分組統計)
- [解法二：固定大小計數陣列](#解法二固定大小計數陣列)
- [解法三：各位數和遞推與即時最大值](#解法三各位數和遞推與即時最大值)
- [解法比較](#解法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定整數 `n`，把 `1` 到 `n` 的每個整數依照「各位數字的總和」分組。最後回傳擁有最多成員的群組共有幾個。

例如 `n = 13`：

```text
[1, 10]、[2, 11]、[3, 12]、[4, 13]、[5]、[6]、[7]、[8]、[9]
```

前四組各有 2 個成員，其他群組只有 1 個成員。因此最大群組大小是 2，而達到最大大小的群組共有 4 個，答案為 `4`。

### 限制條件

- `1 <= n <= 10^4`
- 呼叫端遵循題目保證，不另外處理超出範圍的輸入。
- `1..10000` 中最大的各位數和是 `9 + 9 + 9 + 9 = 36`，因此固定陣列只需要索引 `0..36`。

## 核心概念

### 什麼是各位數和？

將十進位整數的每一位相加：

- `7 → 7`
- `45 → 4 + 5 = 9`
- `123 → 1 + 2 + 3 = 6`

若用數學運算拆位數，可以用 `% 10` 取得個位數，再用整數除法 `/ 10` 移除個位數，直到數字成為 0。

### 真正要統計的是什麼？

題目不要求回傳最大群組「有幾個成員」，而是要求「有幾個群組並列最大」。因此每種解法都必須完成兩層統計：

1. 算出每個數字所屬的各位數和群組。
2. 找出最大群組大小，再計算有幾個群組達到該大小。

## 解法一：Dictionary 分組統計

### 設計出發點

最直接的模型是讓 Dictionary 的鍵代表各位數和，值代表該群組目前有多少成員：

```text
groupSizes[各位數和] = 群組成員數
```

逐一處理 `1..n`，利用 `GetDigitSum` 算出鍵，再增加對應計數。完成所有分組後，先找出 Dictionary 中的最大值，再計算有幾個值等於它。

### 演算法流程

1. 建立空的 `Dictionary<int, int>`。
2. 對每個 `number` 計算 `digitSum`。
3. 若鍵尚未存在，視為原本計數為 0，再加 1。
4. 取得所有群組大小中的最大值 `maxSize`。
5. 回傳大小等於 `maxSize` 的群組數。

### 範例演示：`n = 13`

處理完成後，Dictionary 內容為：

| 各位數和 | 群組成員 | 大小 |
|---:|---|---:|
| 1 | `[1, 10]` | 2 |
| 2 | `[2, 11]` | 2 |
| 3 | `[3, 12]` | 2 |
| 4 | `[4, 13]` | 2 |
| 5–9 | 各自只有原本的一位數 | 1 |

最大值是 2；鍵 `1、2、3、4` 的值皆為 2，因此回傳 `4`。

### 複雜度

- 時間：`O(n log n)`；每個數字要逐位拆解，位數為 `O(log n)`。
- 空間：`O(log n)`；可能的各位數和數量隨十進位位數成長。依本題固定限制觀察時，最多只使用 37 個鍵。

## 解法二：固定大小計數陣列

### 設計出發點

題目已限制 `n <= 10000`，各位數和不可能超過 36。既然鍵的範圍很小且連續，就能用長度 37 的 `int[]` 直接把各位數和當成索引，省去 Dictionary 的雜湊與查找成本。

### 演算法流程

1. 建立索引 `0..36` 的計數陣列。
2. 逐一計算 `1..n` 的各位數和，直接增加對應索引。
3. 掃描陣列找出最大值。
4. 再計算有幾個陣列元素等於最大值。

索引 0 不會被有效輸入使用，但保留它能讓「各位數和」直接對應到陣列索引，不需要額外位移。

### 範例演示：`n = 24`

最後的非零計數如下：

| 各位數和 | 代表成員 | 大小 |
|---:|---|---:|
| 1 | `1, 10` | 2 |
| 2 | `2, 11, 20` | 3 |
| 3 | `3, 12, 21` | 3 |
| 4 | `4, 13, 22` | 3 |
| 5 | `5, 14, 23` | 3 |
| 6 | `6, 15, 24` | 3 |
| 7–9 | 各 2 個成員 | 2 |
| 10 | `19` | 1 |

陣列中的最大值為 3，索引 `2..6` 共 5 格等於 3，因此回傳 `5`。

### 複雜度

- 時間：`O(n log n)`；仍使用逐位拆解計算各位數和。
- 空間：`O(1)`；計數陣列大小固定為 37。

## 解法三：各位數和遞推與即時最大值

### 設計出發點

前兩種解法會為每個數字重新拆解所有位數。其實移除個位數後的前綴 `number / 10` 一定比 `number` 小，所以它的各位數和早已算過：

```text
digitSums[number] = digitSums[number / 10] + number % 10
```

例如 `123 / 10 = 12`，因此 `digitSums[123] = digitSums[12] + 3 = 3 + 3 = 6`。每個數字只需一次陣列查詢、一次餘數與一次加法。

這個版本也不等到最後才掃描所有群組。每次群組大小增加時，就同步維護：

- `maxSize`：目前看過的最大群組大小。
- `largestGroupCount`：目前有幾個群組達到 `maxSize`。

若某群組超越舊最大值，就把並列數重設為 1；若剛好追平最大值，才把並列數加 1。

### 範例演示：`n = 19`

1. 處理 `1..9` 時，每個群組大小都是 1，所以 `maxSize = 1`、`largestGroupCount = 9`。
2. `10` 的各位數和為 `digitSums[1] + 0 = 1`；群組 1 增加為 2，超越舊最大值，因此更新為 `maxSize = 2`、`largestGroupCount = 1`。
3. `11..18` 依序讓群組 `2..9` 也達到大小 2，每次追平都增加並列數；最後 `largestGroupCount = 9`。
4. `19` 的各位數和為 `digitSums[1] + 9 = 10`；群組 10 只有 1 個成員，不影響最大值。
5. 回傳 `9`。

### 複雜度

- 時間：`O(n)`；每個數字的各位數和都由已知結果在常數時間內取得。
- 空間：`O(n)`；`digitSums` 保存 `0..n` 的各位數和，計數陣列則是固定大小。

## 解法比較

| 解法 | 分組結構 | 各位數和算法 | 最大值追蹤 | 時間 | 額外空間 | 適合的教學重點 |
|---|---|---|---|---|---|---|
| `CountLargestGroup` | Dictionary | 逐位拆解 | 最後掃描 | `O(n log n)` | `O(log n)` | 直接依題意建模 |
| `CountLargestGroup2` | 固定陣列 | 逐位拆解 | 最後掃描 | `O(n log n)` | `O(1)` | 利用有限鍵範圍最佳化 |
| `CountLargestGroup3` | 固定陣列 | 前綴遞推 | 處理時同步更新 | `O(n)` | `O(n)` | 用空間換取線性時間 |

若只考慮本題的固定上限，三種方法都足以快速完成；第三種解法的價值主要在展示如何消除重複的位數計算，而不是宣稱它在所有輸入規模下都必然最省資源。

## 可執行驗證設計

`Main` 對三種解法執行完全相同的資料，並逐項比較 Expected 與 Actual：

| 案例 | `n` | 預期結果 | 驗證目的 |
|---|---:|---:|---|
| 最小邊界 | 1 | 1 | 最小合法輸入 |
| 官方範例二 | 2 | 2 | 所有群組同樣大 |
| 官方範例一 | 13 | 4 | 題目標準分組 |
| 進位前的一般案例 | 19 | 9 | 個位數跨入二位數後的分組 |
| 多個最大群組 | 24 | 5 | 多組並列最大 |
| 三位數邊界 | 999 | 2 | 較大輸入與多位數 |
| 最大邊界 | 10000 | 1 | 題目最大合法輸入 |

總計為 `7 個案例 × 3 種解法 = 21` 項檢查。若任一檢查失敗，程式會設定非零結束碼，方便命令列或 CI 偵測失敗。

## 建置與執行

請從本 README 所在的 repository root 執行：

```bash
dotnet restore leetcode_1399/leetcode_1399.csproj
dotnet build leetcode_1399/leetcode_1399.csproj --nologo
dotnet run --no-build --project leetcode_1399/leetcode_1399.csproj
```

專案沒有獨立的自動化測試專案；目前以成功建置及 `Main` 的 21 項自我檢查作為行為驗收。

## 實際執行結果

以下內容來自執行 `dotnet run --no-build --project leetcode_1399/leetcode_1399.csproj`：

```text
LeetCode 1399 - Count Largest Group

案例：最小邊界
Input: n = 1
CountLargestGroup | Expected: 1 | Actual: 1 | PASS
CountLargestGroup2 | Expected: 1 | Actual: 1 | PASS
CountLargestGroup3 | Expected: 1 | Actual: 1 | PASS

案例：官方範例二
Input: n = 2
CountLargestGroup | Expected: 2 | Actual: 2 | PASS
CountLargestGroup2 | Expected: 2 | Actual: 2 | PASS
CountLargestGroup3 | Expected: 2 | Actual: 2 | PASS

案例：官方範例一
Input: n = 13
CountLargestGroup | Expected: 4 | Actual: 4 | PASS
CountLargestGroup2 | Expected: 4 | Actual: 4 | PASS
CountLargestGroup3 | Expected: 4 | Actual: 4 | PASS

案例：進位前的一般案例
Input: n = 19
CountLargestGroup | Expected: 9 | Actual: 9 | PASS
CountLargestGroup2 | Expected: 9 | Actual: 9 | PASS
CountLargestGroup3 | Expected: 9 | Actual: 9 | PASS

案例：多個最大群組
Input: n = 24
CountLargestGroup | Expected: 5 | Actual: 5 | PASS
CountLargestGroup2 | Expected: 5 | Actual: 5 | PASS
CountLargestGroup3 | Expected: 5 | Actual: 5 | PASS

案例：三位數邊界
Input: n = 999
CountLargestGroup | Expected: 2 | Actual: 2 | PASS
CountLargestGroup2 | Expected: 2 | Actual: 2 | PASS
CountLargestGroup3 | Expected: 2 | Actual: 2 | PASS

案例：最大邊界
Input: n = 10000
CountLargestGroup | Expected: 1 | Actual: 1 | PASS
CountLargestGroup2 | Expected: 1 | Actual: 1 | PASS
CountLargestGroup3 | Expected: 1 | Actual: 1 | PASS

Summary: 21/21 checks passed.
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1399.sln
└── leetcode_1399/
    ├── Describe.md
    ├── Program.cs
    └── leetcode_1399.csproj
```

- `Program.cs`：三種演算法、各位數和輔助函式及可執行驗證案例。
- `Describe.md`：既有的題目概念筆記。
- `docs/readme-template.md`：本專案 README 的建立與驗證準則。