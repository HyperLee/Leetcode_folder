# leetcode_3195

本專案實作 LeetCode 題目 3195：Find the Minimum Area to Cover All Ones I（包含所有 1 的最小矩形面積 I）。

此專案為簡單且自包含的 .NET 範例，示範如何用直觀且安全的方式掃描 2D 二元矩陣，找出包含所有值為 1 的最小軸平行矩形並計算面積。

## 連結

- 題目（LeetCode）：[Find the Minimum Area to Cover All Ones I](https://leetcode.com/problems/find-the-minimum-area-to-cover-all-ones-i/)
- 題目（中文）：[包含所有 1 的最小矩形面積 I](https://leetcode.cn/problems/find-the-minimum-area-to-cover-all-ones-i/)

## 目標

撰寫一個函式，輸入一個只含 0/1 的二維陣列 `grid`，回傳一個邊與座標軸平行、能包含所有 1 的最小矩形之面積。

輸入/輸出契約（簡要）：

- 輸入：`int[][] grid`（非 null，且每列長度相同）
- 輸出：返回 int，為最小矩形面積；若輸入無效或沒有任何 1，回傳 0。

## 如何執行

在含有 `leetcode_3195.sln` 的工作資料夾中，使用 .NET CLI：

```powershell
# 建構
dotnet build ./leetcode_3195/leetcode_3195.csproj

# 執行
dotnet run --project ./leetcode_3195/leetcode_3195.csproj
```

以上命令會執行 `Program.cs` 中的幾組測試案例，並在主控台輸出 PASS/FAIL 結果。

## 程式檔案

- `leetcode_3195/Program.cs`：主程式與解題實作（包含 `MinimumArea` 方法與多組測試）。

## 詳細解題說明

核心想法很直接：掃描整個 `grid`，記錄出現 1 的最小/最大列與欄索引（minRow, maxRow, minCol, maxCol）。
這四個邊界索引可以唯一決定包含所有 1 的最小矩形，面積為：

```csharp
area = (maxRow - minRow + 1) * (maxCol - minCol + 1)
```

實作步驟：

1. 輸入驗證：若 `grid` 為 null、`grid.Length == 0`、或 `grid[0]` 為 null/長度為 0，回傳 0。
2. 令 `n = grid.Length`（列數）、`m = grid[0].Length`（欄數）。
3. 初始化邊界：`minRow = n`、`maxRow = -1`、`minCol = m`、`maxCol = -1`。
   - 這樣若沒有任何 1，`maxRow` 保持 -1，可用來判斷無 1 的情況。
4. 雙層迴圈掃描每個格子：外層走列（row），內層走欄（col）：
   - 外層：`for (int i = 0; i < n; i++)`（i 為列索引）
   - 內層：`for (int j = 0; j < m; j++)`（j 為欄索引）
5. 若 `grid[i][j] == 1`，更新四個邊界：
   - `minRow = Math.Min(minRow, i)`
   - `maxRow = Math.Max(maxRow, i)`
   - `minCol = Math.Min(minCol, j)`
   - `maxCol = Math.Max(maxCol, j)`
6. 掃描結束後，若 `maxRow == -1`，代表沒有任何 1，回傳 0。
7. 否則計算面積並回傳。

### 為什麼外層迴圈要用列數 `n`？（row 與 col 的說明）

在 C# 的 2D 陣列表示（以 `int[][]` 為例）中，`grid[i]` 代表第 `i` 列（row），而 `grid[i][j]` 代表第 `i` 列的第 `j` 欄（column）。因此：

- `n = grid.Length`：列數（row count）。
- `m = grid[0].Length`：欄數（column count）。

外層迴圈應該遍歷所有列，所以用 `for (int i = 0; i < n; i++)`；若誤用 `i < m` 將在某些情況下導致漏掃或 IndexOutOfRangeException。

## row / col 圖示（ASCII 範例）

假設 `n = 3`（列）和 `m = 4`（欄）：

```text
   欄索引 ->   0   1   2   3
              ┌───┬───┬───┬───┐
row 0 (i=0)   │ 0 │ 0 │ 0 │ 0 │
              ├───┼───┼───┼───┤
row 1 (i=1)   │ 0 │ 1 │ 0 │ 0 │  <-- 範例：grid[1][1] == 1
              ├───┼───┼───┼───┤
row 2 (i=2)   │ 0 │ 0 │ 0 │ 1 │  <-- 範例：grid[2][3] == 1
              └───┴───┴───┴───┘

說明：
- row（列）為水平方向，使用第一個索引 `i`（範圍 0..n-1）。
- col（欄）為垂直方向，使用第二個索引 `j`（範圍 0..m-1）。
```

## 範例與邊界情況

- 若沒有任何 1，回傳 0。程式以 `maxRow == -1` 判別此情形。
- 若只有單一個 1，面積為 1。
- 程式能處理不規則分布的 1（只要每列長度相同）。

## 時間與空間複雜度

- 時間複雜度：O(n * m)，必須掃描整個矩陣（最差情況）。
- 空間複雜度：O(1)，只使用常數個額外變數保存邊界索引。

## 測試（程式內建）

`Program.cs` 含多組簡單測試，例如：

- `NoOnes`：全 0，預期 0。
- `SingleOne`：單一 1，預期 1。
- `Block2x2`：2x2 全 1，預期 4。
- `Scattered`：分散 1，檢驗面積計算正確性。

執行 `dotnet run` 後主控台會輸出每組測試的 PASS/FAIL。

## 延伸建議

- 若想加速：可以透過二分搜尋來在行/列上尋找最左/最右/最上/最下的 1，將時間複雜度改善到 O(n log m + m log n) 的型態（此題簡單解法已足夠且易於理解）。
- 若輸入為 `bool[][]` 或 `BitArray`，可微調以降低記憶體或提昇效能。

---

如需我幫你把 README 的語言改回英文、加入範例輸出、或產生更完整的測試案例，請告訴我下一步要做什麼。
