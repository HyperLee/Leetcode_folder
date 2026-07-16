# LeetCode 1337：The K Weakest Rows in a Matrix／矩陣中戰鬥力最弱的 K 行

這個 .NET 10 主控台專案以二分搜尋計算每列軍人數，再依戰鬥力與原始列索引找出最弱的 `k` 列。公開方法 `KWeakestRows(int[][] mat, int k)` 不修改輸入也不輸出文字；`Main` 統一負責八個確定性驗收案例、PASS/FAIL 統計與失敗結束碼。

## 題目連結

- [LeetCode（英文）](https://leetcode.com/problems/the-k-weakest-rows-in-a-matrix/)
- [LeetCode（中文）](https://leetcode.cn/problems/the-k-weakest-rows-in-a-matrix/)

## 題目敘述與限制

矩陣每列由若干軍人 `1` 開始，之後才是平民 `0`。一列的戰鬥力由軍人數決定；若兩列軍人數相同，原始索引較小者較弱。給定矩陣 `mat` 與整數 `k`，依戰鬥力由弱至強回傳最弱的 `k` 個列索引。

- `m == mat.length`
- `n == mat[i].length`
- `2 <= n, m <= 100`
- `1 <= k <= m`
- `mat[i][j]` 只能是 `0` 或 `1`
- 每列所有 `1` 都位於所有 `0` 之前

## 二分搜尋不變量與同強度排序

每列具有「先 1 後 0」的單調結構，因此 `CountSoldiers` 搜尋第一個 `0` 時，候選答案始終保留在閉區間 `[left, right]`；`left` 之前的索引已知皆為 `1`，而當 `right < row.Length` 時，從 `right` 起的索引已知皆為 `0`。若中點是 `1`，候選答案必在其右側；否則中點本身仍可能是答案。`row.Length` 是整列皆為 `1` 時的答案哨兵，所以不需要特殊分支。

每列轉成 `(SoldierCount, RowIndex)`。排序時先明確比較軍人數，再明確比較原始索引：

```plaintext
(0, 3), (1, 0), (1, 2), (2, 1)
       同為 1 名軍人時，索引 0 排在索引 2 前面
```

這個次排序鍵正是題目對同強度列的規則，也避免依賴排序實作是否穩定。

## 實作設計與複雜度

對 `m` 列各做一次長度為 `n` 的二分搜尋，接著排序全部 `m` 個 `(軍人數, 索引)` 配對，最後取前 `k` 個索引。解法只讀取 `mat`，不會改寫任何列或元素。

- 時間複雜度：`O(m log n + m log m)`。
- 輔助空間：`O(m)`。
- 回傳結果空間：`O(k)`。

## 範例推演

以 `[[1,0,0],[1,1,0],[1,0,0],[0,0,0]]`、`k = 4` 為例：

1. 二分搜尋得到各列軍人數 `[1, 2, 1, 0]`。
2. 建立配對 `(1,0), (2,1), (1,2), (0,3)`。
3. 先依軍人數排序，同數量再依索引排序，得到 `(0,3), (1,0), (1,2), (2,1)`。
4. 回傳索引 `[3, 0, 2, 1]`。

## 驗收案例

專案沒有正式測試專案或獨立測試框架；可執行檔內的確定性 harness 是驗證機制。

| 案例 | 輸入摘要 | 預期 | 驗證重點 |
| --- | --- | --- | --- |
| 1. Official example 1 | `5x5`, `k = 3` | `[2,0,3]` | 第一個官方範例 |
| 2. Official example 2 | `4x4`, `k = 2` | `[0,2]` | 第二個官方範例與同強度索引 |
| 3. Minimum 2x2, all civilians | 全為 `0`，`k = 1` | `[0]` | 最小尺寸、零軍人與 tie-break |
| 4. Minimum 2x2, all soldiers | 全為 `1`，`k = 2` | `[0,1]` | 全軍人列回傳列長度 |
| 5. Zero, full, and middle strengths | 軍人數 `0,2,1` | `[0,2,1]` | 零、全滿與中間邊界 |
| 6. Non-adjacent equal strengths | 軍人數 `1,2,1,0`，`k = m` | `[3,0,2,1]` | 非相鄰同強度列的明確次排序 |
| 7. Rows ordered strongest to weakest | 軍人數 `4,3,2,1`，`k = 2` | `[3,2]` | 輸入順序與答案完全相反 |
| 8. 100x100 descending counts | 軍人數由 `100` 降至 `1` | `[99,98,97,96,95]` | 題目尺寸上限 spot check |

## 建置與執行

從題目根目錄 `leetcode_1337/` 執行：

```bash
dotnet build leetcode_1337/leetcode_1337.csproj --nologo
dotnet run --no-build --project leetcode_1337/leetcode_1337.csproj
```

使用 `--no-build` 前請先完成建置。不要執行裸的 `dotnet build` 或 `dotnet test`，因為題目根目錄沒有根專案、solution 或正式測試專案。

## 實際驗證輸出

以下內容直接取自成功執行結果；第八組只輸出矩陣尺寸與軍人數分布，不傾印完整 `100x100` 矩陣：

```text
LeetCode 1337 acceptance harness

Case 1: Official example 1
Input: mat = [[1,1,0,0,0],[1,1,1,1,0],[1,0,0,0,0],[1,1,0,0,0],[1,1,1,1,1]], k = 3
Expected: [2, 0, 3]
Actual: [2, 0, 3]
PASS

Case 2: Official example 2
Input: mat = [[1,0,0,0],[1,1,1,1],[1,0,0,0],[1,0,0,0]], k = 2
Expected: [0, 2]
Actual: [0, 2]
PASS

Case 3: Minimum 2x2, all civilians
Input: mat = [[0,0],[0,0]], k = 1
Expected: [0]
Actual: [0]
PASS

Case 4: Minimum 2x2, all soldiers
Input: mat = [[1,1],[1,1]], k = 2
Expected: [0, 1]
Actual: [0, 1]
PASS

Case 5: Zero, full, and middle strengths
Input: mat = [[0,0],[1,1],[1,0]], k = 3
Expected: [0, 2, 1]
Actual: [0, 2, 1]
PASS

Case 6: Non-adjacent equal strengths with k = m
Input: mat = [[1,0,0],[1,1,0],[1,0,0],[0,0,0]], k = 4
Expected: [3, 0, 2, 1]
Actual: [3, 0, 2, 1]
PASS

Case 7: Rows ordered strongest to weakest
Input: mat = [[1,1,1,1],[1,1,1,0],[1,1,0,0],[1,0,0,0]], k = 2
Expected: [3, 2]
Actual: [3, 2]
PASS

Case 8: 100x100 descending soldier counts
Input: mat = 100x100 with soldier counts descending from 100 to 1, k = 5
Expected: [99, 98, 97, 96, 95]
Actual: [99, 98, 97, 96, 95]
PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1337/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_1337/
    ├── Program.cs
    └── leetcode_1337.csproj
```
