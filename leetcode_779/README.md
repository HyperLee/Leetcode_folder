# LeetCode 779：K-th Symbol in Grammar／第 K 個語法符號

這是一個以 C# 撰寫的 .NET 10 主控台專案，利用每一列前半段沿用上一列、後半段
逐位翻轉的規律，以遞迴方式找出指定位置的符號，而不必建立長度呈指數成長的字串。

- [英文題目：779. K-th Symbol in Grammar](https://leetcode.com/problems/k-th-symbol-in-grammar/)
- [中文題目：779. 第 K 個語法符號](https://leetcode.cn/problems/k-th-symbol-in-grammar/)

## 題目說明

第一列只有 `0`。建立下一列時，將上一列每個 `0` 替換為 `01`，每個 `1` 替換為
`10`。給定列號 `n` 與從 1 起算的位置 `k`，回傳第 `n` 列的第 `k` 個符號。

## 限制條件

- `1 <= n <= 30`
- `1 <= k <= 2^(n - 1)`
- 實作只處理題目契約內的有效輸入，不另外定義無效輸入的例外行為。

## 核心不變量

第 `n` 列的長度為 `2^(n - 1)`，其中：

- 前半段與第 `n - 1` 列完全相同。
- 後半段與第 `n - 1` 列逐位相反。

因此只需比較 `k` 與半列長度 `2^(n - 2)`：位於前半段時保留原位置；位於後半段
時扣除半列長度，再將遞迴結果以 XOR `1` 翻轉。

## 遞迴解法與取捨

1. `k == 1` 時直接回傳 `0`；每一列的第一個符號都不變。
2. 計算目前列的半列長度 `1 << (n - 2)`。
3. `k` 位於前半段時，遞迴查詢 `KthGrammar(n - 1, k)`。
4. `k` 位於後半段時，映射為 `k - halfLength`，並以
   `1 ^ KthGrammar(...)` 翻轉上一列的符號。

此方法保留題目由目前列回推上一列的教學關係，不建立任何完整列。取捨是每次呼叫
都會占用一層遞迴堆疊；在本題 `n <= 30` 的限制內，深度有明確上限。

- 時間複雜度：最差 `O(n)`
- 輔助空間：最差 `O(n)` 遞迴堆疊
- 結果空間：`O(1)`

公開方法 `KthGrammar` 不修改輸入且不寫入主控台；所有輸出只由 `Main` 負責。

## `n = 4, k = 6` 逐步走查

第 4 列長度為 8，半列長度為 4。`k = 6` 位於後半段，因此先映射到第 3 列的
位置 2，最後再翻轉答案。

```plaintext
KthGrammar(4, 6)
= 1 XOR KthGrammar(3, 2)
= 1 XOR KthGrammar(2, 2)
= 1 XOR (1 XOR KthGrammar(1, 1))
= 1 XOR (1 XOR 0)
= 0
```

## 可執行驗證案例

`Main` 固定執行十項檢查；`(1, 1)` 同時是第一個官方範例與最小有效輸入。

| 案例 | `n` | `k` | 預期 | 驗證重點 |
| --- | ---: | ---: | ---: | --- |
| 1 | 1 | 1 | 0 | 官方範例、最小輸入 |
| 2 | 2 | 1 | 0 | 官方範例、第二列首位 |
| 3 | 2 | 2 | 1 | 官方範例、第一次翻轉 |
| 4 | 3 | 3 | 1 | 奇數位置回歸 |
| 5 | 4 | 4 | 0 | 前半段最後位置 |
| 6 | 4 | 5 | 1 | 後半段第一位置 |
| 7 | 4 | 6 | 0 | 後半段翻轉回歸 |
| 8 | 5 | 11 | 0 | 多層翻轉回歸 |
| 9 | 30 | 1 | 0 | 最大列號首位 |
| 10 | 30 | 536870912 | 1 | 最大列號最後位置 |

任一檢查失敗時，程式會將 `Environment.ExitCode` 設為 `1`。本題沒有獨立 test
project 或第三方測試框架；console acceptance harness 是目前的驗證機制。

## 建置與執行

請從此 README 所在的外層 `leetcode_779` 目錄執行：

```bash
dotnet build leetcode_779/leetcode_779.csproj --nologo
dotnet run --no-build --project leetcode_779/leetcode_779.csproj
```

以下是完成建置後執行第二個命令的完整輸出：

```text
LeetCode 779 acceptance harness

Case 1: Official example and minimum input
Input: n=1, k=1
PASS | K-th symbol | Expected: 0 | Actual: 0

Case 2: Official first symbol of row 2
Input: n=2, k=1
PASS | K-th symbol | Expected: 0 | Actual: 0

Case 3: Official second symbol of row 2
Input: n=2, k=2
PASS | K-th symbol | Expected: 1 | Actual: 1

Case 4: Odd position regression
Input: n=3, k=3
PASS | K-th symbol | Expected: 1 | Actual: 1

Case 5: End of first half
Input: n=4, k=4
PASS | K-th symbol | Expected: 0 | Actual: 0

Case 6: Start of complemented half
Input: n=4, k=5
PASS | K-th symbol | Expected: 1 | Actual: 1

Case 7: Complemented-half regression
Input: n=4, k=6
PASS | K-th symbol | Expected: 0 | Actual: 0

Case 8: Multi-level complement regression
Input: n=5, k=11
PASS | K-th symbol | Expected: 0 | Actual: 0

Case 9: Maximum row first symbol
Input: n=30, k=1
PASS | K-th symbol | Expected: 0 | Actual: 0

Case 10: Maximum row last symbol
Input: n=30, k=536870912
PASS | K-th symbol | Expected: 1 | Actual: 1

Summary: 10/10 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/
│       │   └── 2026-07-15-leetcode-779-net10.md
│       └── specs/
│           └── 2026-07-15-leetcode-779-net10-design.md
├── leetcode_779/
│   ├── Program.cs
│   └── leetcode_779.csproj
├── AGENTS.md
└── README.md
```
