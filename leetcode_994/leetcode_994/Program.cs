namespace leetcode_994
{
    internal class Program
    {
        /// <summary>
        /// 994. Rotting Oranges
        /// https://leetcode.com/problems/rotting-oranges/description/
        /// <para>
        /// You are given an m x n grid where each cell has one of three values:
        /// - 0 represents an empty cell.
        /// - 1 represents a fresh orange.
        /// - 2 represents a rotten orange.
        ///
        /// Every minute, any fresh orange that is adjacent to a rotten orange in one of four directions becomes rotten.
        ///
        /// Return the minimum number of minutes until no cell contains a fresh orange. If this is impossible, return -1.
        ///
        /// Example 1:
        /// Image: https://assets.leetcode.com/uploads/2019/02/16/oranges.png
        /// Input: grid = [[2,1,1],[1,1,0],[0,1,1]]
        /// Output: 4
        ///
        /// Example 2:
        /// Input: grid = [[2,1,1],[0,1,1],[1,0,1]]
        /// Output: -1
        /// Explanation: The orange in the bottom-left corner (row 2, column 0) never rots because rotting spreads only in four directions.
        ///
        /// Example 3:
        /// Input: grid = [[0,2]]
        /// Output: 0
        /// Explanation: There are already no fresh oranges at minute 0, so the answer is 0.
        ///
        /// Constraints:
        /// - m == grid.length
        /// - n == grid[i].length
        /// - 1 &lt;= m, n &lt;= 10
        /// - grid[i][j] is 0, 1, or 2.
        /// </para>
        /// <para>
        /// 994. 腐爛的橘子
        /// https://leetcode.cn/problems/rotting-oranges/description/
        ///
        /// 給定 m x n 的網格 grid，每個格子具有下列三種值之一：
        /// - 0 表示空格。
        /// - 1 表示新鮮橘子。
        /// - 2 表示腐爛橘子。
        ///
        /// 每過一分鐘，任何在四個方向之一與腐爛橘子相鄰的新鮮橘子都會腐爛。
        ///
        /// 回傳直到沒有格子含有新鮮橘子所需的最少分鐘數；若不可能，回傳 -1。
        ///
        /// 範例 1：
        /// 圖片：https://assets.leetcode.com/uploads/2019/02/16/oranges.png
        /// 輸入：grid = [[2,1,1],[1,1,0],[0,1,1]]
        /// 輸出：4
        ///
        /// 範例 2：
        /// 輸入：grid = [[2,1,1],[0,1,1],[1,0,1]]
        /// 輸出：-1
        /// 解釋：左下角的橘子（第 2 列、第 0 欄）永遠不會腐爛，因為腐爛只會沿四個方向傳播。
        ///
        /// 範例 3：
        /// 輸入：grid = [[0,2]]
        /// 輸出：0
        /// 解釋：第 0 分鐘時已經沒有新鮮橘子，因此答案就是 0。
        ///
        /// 限制條件：
        /// - m == grid.length
        /// - n == grid[i].length
        /// - 1 &lt;= m, n &lt;= 10
        /// - grid[i][j] 是 0、1 或 2。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 不需要命令列參數；主程式會以七組合法案例分別驗證兩種多源 BFS 解法，
        /// 並輸出每次檢查的預期值、實際值與 PASS/FAIL 結果。
        /// </remarks>
        /// <param name="args">未使用的命令列參數。</param>
        static void Main(string[] args)
        {
            SampleCase[] cases =
            [
                new("官方範例一：可完全腐爛", [[2, 1, 1], [1, 1, 0], [0, 1, 1]], 4),
                new("官方範例二：存在不可達橘子", [[2, 1, 1], [0, 1, 1], [1, 0, 1]], -1),
                new("官方範例三：起始時沒有新鮮橘子", [[0, 2]], 0),
                new("單一空格", [[0]], 0),
                new("單一新鮮橘子", [[1]], -1),
                new("全部已腐爛", [[2, 2], [2, 2]], 0),
                new("對角多腐爛源", [[2, 1, 1], [1, 1, 1], [1, 1, 2]], 2)
            ];

            int passedChecks = 0;

            Console.WriteLine("LeetCode 994：腐爛的橘子");
            Console.WriteLine();

            for (int i = 0; i < cases.Length; i++)
            {
                SampleCase sample = cases[i];
                int actual1 = OrangesRotting(CloneGrid(sample.Grid));
                int actual2 = OrangesRotting2(CloneGrid(sample.Grid));
                bool passed1 = actual1 == sample.Expected;
                bool passed2 = actual2 == sample.Expected;

                passedChecks += passed1 ? 1 : 0;
                passedChecks += passed2 ? 1 : 0;

                Console.WriteLine($"案例 {i + 1}：{sample.Name}");
                Console.WriteLine($"輸入：grid = {FormatGrid(sample.Grid)}");
                Console.WriteLine($"預期：{sample.Expected}");
                Console.WriteLine($"OrangesRotting：實際 = {actual1}，結果 = {(passed1 ? "PASS" : "FAIL")}");
                Console.WriteLine($"OrangesRotting2：實際 = {actual2}，結果 = {(passed2 ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{cases.Length * 2} 項驗證通過");
        }

        // 依序表示上、左、下、右，兩個陣列使用相同索引即可得到一組相鄰座標。
        private static readonly int[] RowOffsets = [-1, 0, 1, 0];
        private static readonly int[] ColumnOffsets = [0, -1, 0, 1];

        /// <summary>
        /// 使用座標壓縮與深度字典執行多源廣度優先搜尋。
        /// 所有初始腐爛橘子都從第 0 分鐘開始，首次抵達新鮮橘子的深度就是它腐爛的最短時間。
        /// 輸入須為符合題目限制的非空矩形網格，元素只能是 0、1 或 2；
        /// 回傳全部新鮮橘子腐爛所需的最少分鐘數，若有橘子無法抵達則回傳 -1。
        /// </summary>
        /// <remarks>
        /// 方法會原地將已腐爛的新鮮橘子由 1 改為 2。時間與額外空間複雜度皆為 O(m × n)。
        /// 二維座標以 <c>row * columns + column</c> 壓縮，並可用除法及餘數還原。
        /// </remarks>
        /// <param name="grid">符合題目限制的橘子網格。</param>
        /// <returns>全部腐爛的最少分鐘數；無法全部腐爛時回傳 -1。</returns>
        public static int OrangesRotting(int[][] grid)
        {
            int rows = grid.Length;
            int columns = grid[0].Length;
            Queue<int> queue = new Queue<int>();
            IDictionary<int, int> depth = new Dictionary<int, int>();

            // 多源 BFS 必須讓所有初始腐爛橘子同時位於第 0 層。
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (grid[row][column] == 2)
                    {
                        int code = row * columns + column;
                        queue.Enqueue(code);
                        depth.Add(code, 0);
                    }
                }
            }

            int minutes = 0;

            while (queue.Count > 0)
            {
                int code = queue.Dequeue();
                int row = code / columns;
                int column = code % columns;

                for (int direction = 0; direction < RowOffsets.Length; direction++)
                {
                    int nextRow = row + RowOffsets[direction];
                    int nextColumn = column + ColumnOffsets[direction];

                    if (nextRow >= 0
                        && nextRow < rows
                        && nextColumn >= 0
                        && nextColumn < columns
                        && grid[nextRow][nextColumn] == 1)
                    {
                        // 入列時立即標記，避免同一顆橘子被不同來源重複加入。
                        grid[nextRow][nextColumn] = 2;
                        int nextCode = nextRow * columns + nextColumn;
                        int nextDepth = depth[code] + 1;
                        queue.Enqueue(nextCode);
                        depth.Add(nextCode, nextDepth);
                        minutes = nextDepth;
                    }
                }
            }

            foreach (int[] row in grid)
            {
                foreach (int cell in row)
                {
                    if (cell == 1)
                    {
                        // BFS 結束仍有新鮮橘子，表示它與所有腐爛來源都不連通。
                        return -1;
                    }
                }
            }

            return minutes;
        }

        /// <summary>
        /// 使用逐層佇列與剩餘新鮮橘子計數執行多源廣度優先搜尋。
        /// 每次處理進入該分鐘前已在佇列中的所有腐爛橘子，完成一層後分鐘數加一。
        /// 輸入須為符合題目限制的非空矩形網格，元素只能是 0、1 或 2；
        /// 回傳全部新鮮橘子腐爛所需的最少分鐘數，若最後仍有新鮮橘子則回傳 -1。
        /// </summary>
        /// <remarks>
        /// 方法會原地將已腐爛的新鮮橘子由 1 改為 2。時間與額外空間複雜度皆為 O(m × n)。
        /// </remarks>
        /// <param name="grid">符合題目限制的橘子網格。</param>
        /// <returns>全部腐爛的最少分鐘數；無法全部腐爛時回傳 -1。</returns>
        public static int OrangesRotting2(int[][] grid)
        {
            int rows = grid.Length;
            int columns = grid[0].Length;
            Queue<(int Row, int Column)> queue = new Queue<(int Row, int Column)>();
            int freshCount = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (grid[row][column] == 2)
                    {
                        queue.Enqueue((row, column));
                    }
                    else if (grid[row][column] == 1)
                    {
                        freshCount++;
                    }
                }
            }

            int minutes = 0;

            // 固定本層數量，確保本分鐘新腐爛的橘子要到下一分鐘才繼續傳播。
            while (queue.Count > 0 && freshCount > 0)
            {
                int currentLevelCount = queue.Count;

                for (int i = 0; i < currentLevelCount; i++)
                {
                    (int row, int column) = queue.Dequeue();

                    for (int direction = 0; direction < RowOffsets.Length; direction++)
                    {
                        int nextRow = row + RowOffsets[direction];
                        int nextColumn = column + ColumnOffsets[direction];

                        if (nextRow >= 0
                            && nextRow < rows
                            && nextColumn >= 0
                            && nextColumn < columns
                            && grid[nextRow][nextColumn] == 1)
                        {
                            grid[nextRow][nextColumn] = 2;
                            freshCount--;
                            queue.Enqueue((nextRow, nextColumn));
                        }
                    }
                }

                minutes++;
            }

            return freshCount == 0 ? minutes : -1;
        }

        /// <summary>
        /// 深層複製橘子網格，避免會原地修改輸入的演算法污染固定測試資料。
        /// 輸入須為符合題目限制的非空矩形網格；回傳內容相同且各列皆獨立的新網格。
        /// </summary>
        /// <param name="grid">要複製的橘子網格。</param>
        /// <returns>不與輸入共用任何陣列實例的網格副本。</returns>
        private static int[][] CloneGrid(int[][] grid)
        {
            int[][] clone = new int[grid.Length][];

            for (int row = 0; row < grid.Length; row++)
            {
                clone[row] = [.. grid[row]];
            }

            return clone;
        }

        /// <summary>
        /// 將橘子網格轉換成適合 console 與 README 閱讀的巢狀方括號格式。
        /// 輸入須為符合題目限制的非空矩形網格；回傳例如
        /// <c>[[2, 1], [1, 0]]</c> 的單行字串，且不會修改輸入。
        /// </summary>
        /// <param name="grid">要格式化的橘子網格。</param>
        /// <returns>以逗號與空格分隔的巢狀方括號字串。</returns>
        private static string FormatGrid(int[][] grid)
        {
            return $"[{string.Join(", ", grid.Select(row => $"[{string.Join(", ", row)}]"))}]";
        }

        /// <summary>
        /// 表示一筆固定驗證案例，包含顯示名稱、合法網格及手動推導的預期分鐘數。
        /// </summary>
        /// <param name="Name">案例顯示名稱。</param>
        /// <param name="Grid">符合題目限制的輸入網格。</param>
        /// <param name="Expected">預期的最少分鐘數，或無法全部腐爛時的 -1。</param>
        private sealed record SampleCase(string Name, int[][] Grid, int Expected);
    }
}