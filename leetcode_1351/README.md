# LeetCode 1351：Count Negative Numbers in a Sorted Matrix／統計有序矩陣中的負數

這個 .NET 10 主控台專案使用右上角階梯式掃描，在每列與每欄皆按非遞增順序排列的矩陣中統計負數。公開方法 `CountNegatives(int[][] grid)` 不修改輸入也不輸出文字；`Main` 統一負責八個確定性驗收案例、PASS/FAIL 統計與失敗結束碼。

## 題目連結

- [LeetCode（英文）](https://leetcode.com/problems/count-negative-numbers-in-a-sorted-matrix/)
- [LeetCode（中文）](https://leetcode.cn/problems/count-negative-numbers-in-a-sorted-matrix/)

## 題目敘述與限制

給定一個 `m x n` 矩陣 `grid`，每一列與每一欄都按非遞增順序排列。回傳矩陣中負數的總數。

- `m == grid.length`
- `n == grid[i].length`
- `1 <= m, n <= 100`
- `-100 <= grid[i][j] <= 100`
- 每列與每欄皆按非遞增順序排列

## 核心不變量與容易出錯的地方

掃描從右上角 `(row = 0, column = n - 1)` 開始。每一步都維持「尚未判定的區域位於目前位置左下方」的不變量：

- 若 `grid[row][column] < 0`，因同一欄由上往下不會變大，從目前列到最後一列皆為負數，可一次加入 `m - row`，再往左處理下一欄。
- 若目前值為非負數，因同一列由左往右不會變大，該位置左側也都非負，不可能貢獻答案，因此往下處理下一列。

最容易出錯的是把負數批次數量寫成欄數、在遇到非負數時往左移，或只利用列排序而忽略欄排序。實作不排序、不改寫元素，也不對題目保證以外的 null 或不規則矩陣加入額外行為。

## 解法設計與取捨

專案只保留 `CountNegatives` 的右上角階梯式掃描。逐格掃描雖然直觀，但需要 `O(mn)` 時間；對每列做二分搜尋需要 `O(m log n)` 時間。階梯式掃描同時利用列與欄的排序，每次只會往左或往下移動，最多走過 `m + n` 個位置，而且不需要額外集合。

- 時間複雜度：`O(m+n)`。
- 結果空間：`O(1)`。
- 輔助空間：`O(1)`。

## 範例推演

以 `[[5,2,0,-1],[3,1,-1,-2],[1,0,-2,-3]]` 為例：

1. 從右上角 `-1` 開始，該欄從目前列以下三個值皆為負數，累計 `3`，往左。
2. 遇到第一列的 `0`，其左側都非負，往下。
3. 第二列目前值 `-1`，該欄從第二列以下兩個值皆為負數，累計變成 `5`，往左。
4. 第二列的 `1` 與第三列的 `0` 都非負，依序往下後離開矩陣，答案為 `5`。

```plaintext
路徑：-1 → 0 → -1 → 1 → 0
批次計數：3 + 2 = 5
```

## 驗收案例

專案沒有正式測試專案或獨立測試框架；可執行檔內的確定性 harness 是驗證機制。

| 案例 | 輸入摘要 | 預期 | 驗證重點 |
| --- | --- | ---: | --- |
| Official example 1 | `4x4` 混合矩陣 | `8` | 主要官方範例與多次左右／向下移動 |
| Official example 2 | `[[3,2],[1,0]]` | `0` | 全部非負 |
| Official example 3 | `[[1,-1],[-1,-1]]` | `3` | 右上角直接批次計數 |
| Official example 4 | `[[-1]]` | `1` | 最小負數矩陣 |
| Minimum non-negative | `[[0]]` | `0` | 最小非負矩陣與零的邊界 |
| Rectangular regression | `3x4` 混合矩陣 | `5` | 非方形矩陣與列數批次計數 |
| 100x100 all -100 | 全部為 `-100` | `10000` | 尺寸上限與全負數 |
| 100x100 all 100 | 全部為 `100` | `0` | 尺寸上限與全正數 |

## 建置與執行

從題目根目錄 `leetcode_1351/` 執行：

```bash
dotnet build leetcode_1351/leetcode_1351.csproj --nologo
dotnet run --no-build --project leetcode_1351/leetcode_1351.csproj
```

使用 `--no-build` 前請先完成建置。不要執行裸的 `dotnet build` 或 `dotnet test`，因為題目根目錄沒有根專案、solution 或正式測試專案。

## 實際驗證輸出

以下內容直接取自成功執行結果；兩個 `100x100` 案例使用簡潔且確定的輸入標籤，不傾印完整矩陣：

```text
Case: Official example 1 | Input: [[4,3,2,-1],[3,2,1,-1],[1,1,-1,-2],[-1,-1,-2,-3]] | Expected: 8 | Actual: 8 | PASS
Case: Official example 2 | Input: [[3,2],[1,0]] | Expected: 0 | Actual: 0 | PASS
Case: Official example 3 | Input: [[1,-1],[-1,-1]] | Expected: 3 | Actual: 3 | PASS
Case: Official example 4 | Input: [[-1]] | Expected: 1 | Actual: 1 | PASS
Case: Minimum non-negative | Input: [[0]] | Expected: 0 | Actual: 0 | PASS
Case: Rectangular regression | Input: [[5,2,0,-1],[3,1,-1,-2],[1,0,-2,-3]] | Expected: 5 | Actual: 5 | PASS
Case: 100x100 all -100 | Input: 100x100 matrix filled with -100 | Expected: 10000 | Actual: 10000 | PASS
Case: 100x100 all 100 | Input: 100x100 matrix filled with 100 | Expected: 0 | Actual: 0 | PASS
Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1351/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/
│       │   └── 2026-07-16-leetcode-1351-net10-migration.md
│       └── specs/
│           └── 2026-07-16-leetcode-1351-net10-migration-design.md
└── leetcode_1351/
    ├── Program.cs
    └── leetcode_1351.csproj
```
