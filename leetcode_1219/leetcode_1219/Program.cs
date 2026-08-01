namespace leetcode_1219
{
    internal class Program
    {
        /// <summary>
        /// 1219. Path with Maximum Gold
        /// https://leetcode.com/problems/path-with-maximum-gold/description/?envType=daily-question&envId=2024-05-14
        /// 1219. 黄金矿工
        /// https://leetcode.cn/problems/path-with-maximum-gold/description/
        /// </summary>
        /// <remarks>
        /// 程式主要進入點；執行七組固定案例與三種解法，並以 process exit code 表示驗證結果。
        /// </remarks>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Environment.ExitCode = RunSamples() ? 0 : 1;
        }

        /// <summary>
        /// 建立並執行七組固定金礦案例，依序驗證三種最大黃金路徑解法。
        /// 此方法不接受外部輸入；輸出每個案例的預期值、實際值、輸入保留狀態與 PASS/FAIL，
        /// 並回傳全部解法是否通過所有案例。
        /// </summary>
        /// <returns>全部 21 項檢查通過時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, Func<int[][]> BuildGrid, int Expected)[] cases =
            [
                (
                    "官方範例一",
                    () =>
                    [
                        [0, 6, 0],
                        [5, 8, 7],
                        [0, 9, 0]
                    ],
                    24),
                (
                    "官方範例二",
                    () =>
                    [
                        [1, 0, 7],
                        [2, 0, 6],
                        [3, 4, 5],
                        [0, 3, 0],
                        [9, 0, 20]
                    ],
                    28),
                (
                    "全零矩陣與重複呼叫",
                    () =>
                    [
                        [0, 0],
                        [0, 0]
                    ],
                    0),
                (
                    "單一含金格上界",
                    () => [[100]],
                    100),
                (
                    "重複黃金數值",
                    () =>
                    [
                        [1, 1],
                        [1, 1]
                    ],
                    4),
                (
                    "互不連通區塊",
                    () =>
                    [
                        [1, 2, 0],
                        [0, 0, 0],
                        [3, 4, 5]
                    ],
                    12),
                (
                    "15 x 15 與 25 個含金格上界",
                    BuildUpperBoundGrid,
                    25)
            ];

            Program solver = new Program();
            int passedChecks = 0;
            int totalChecks = 0;

            foreach ((string name, Func<int[][]> buildGrid, int expected) in cases)
            {
                (int passed, int total) = RunTestCase(solver, name, buildGrid(), expected);
                passedChecks += passed;
                totalChecks += total;
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過。");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 對同一案例建立三份互不共用的矩陣，執行三種解法並驗證答案及輸入保留契約。
        /// 輸入須為符合題目限制的矩形矩陣；輸出各解法結果並回傳本案例的通過數與檢查總數。
        /// </summary>
        /// <param name="solver">跨案例重複使用的解題物件，用來偵測狀態是否殘留。</param>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="grid">符合題目限制且不會由此方法修改的原始矩陣。</param>
        /// <param name="expected">此案例預期可收集的最大黃金量。</param>
        /// <returns>本案例通過的解法數與固定檢查總數 3。</returns>
        private static (int Passed, int Total) RunTestCase(Program solver, string name, int[][] grid, int expected)
        {
            (string Name, Func<int[][], int> Solve)[] solutions =
            [
                ("解法一（原地標記遞迴回溯）", solver.GetMaximumGold),
                ("解法二（visited 遞迴 DFS）", solver.GetMaximumGold2),
                ("解法三（迭代 DFS 與位元遮罩）", solver.GetMaximumGold3)
            ];

            int passedChecks = 0;

            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"輸入：{FormatGrid(grid)}");
            Console.WriteLine($"預期：{expected}");

            foreach ((string solutionName, Func<int[][], int> solve) in solutions)
            {
                int[][] input = CloneGrid(grid);
                int actual = solve(input);
                bool inputPreserved = HaveSameValues(grid, input);
                bool passed = actual == expected && inputPreserved;

                Console.WriteLine(
                    $"{solutionName}實際：{actual}；輸入保留：{(inputPreserved ? "是" : "否")} => {(passed ? "PASS" : "FAIL")}");

                if (passed)
                {
                    passedChecks++;
                }
            }

            Console.WriteLine();
            return (passedChecks, solutions.Length);
        }

        /// <summary>
        /// 建立 15 x 15 且恰有 25 個互不相鄰含金格的合法上界案例。
        /// 含金格數值依序為 1 到 25；因任兩格皆無法相連，輸出答案應為 25。
        /// </summary>
        /// <returns>符合列數、欄數與含金格數量上界的矩陣。</returns>
        private static int[][] BuildUpperBoundGrid()
        {
            int[][] grid = Enumerable.Range(0, 15).Select(_ => new int[15]).ToArray();
            int gold = 1;

            for (int row = 0; row < grid.Length && gold <= 25; row += 2)
            {
                for (int column = 0; column < grid[row].Length && gold <= 25; column += 2)
                {
                    grid[row][column] = gold;
                    gold++;
                }
            }

            return grid;
        }

        /// <summary>
        /// 深層複製鋸齒整數矩陣，使每種解法都取得互不共用的列陣列。
        /// 輸入須為非空矩形矩陣；輸出內容相同但可獨立修改的新矩陣。
        /// </summary>
        /// <param name="grid">要複製的矩陣。</param>
        /// <returns>不與輸入共用列陣列的矩陣副本。</returns>
        private static int[][] CloneGrid(int[][] grid)
        {
            return grid.Select(row => (int[])row.Clone()).ToArray();
        }

        /// <summary>
        /// 逐列比較兩個鋸齒整數矩陣的尺寸與內容。
        /// 輸入可為不同尺寸；輸出表示兩者是否具有完全相同的列長度與元素順序。
        /// </summary>
        /// <param name="left">第一個待比較矩陣。</param>
        /// <param name="right">第二個待比較矩陣。</param>
        /// <returns>矩陣尺寸與所有元素皆相同時為 <see langword="true"/>。</returns>
        private static bool HaveSameValues(int[][] left, int[][] right)
        {
            return left.Length == right.Length
                && left.Zip(right).All(rows => rows.First.SequenceEqual(rows.Second));
        }

        /// <summary>
        /// 將矩陣轉成不受地區設定影響的 LeetCode 風格單行字串。
        /// 輸入須為整數矩陣；輸出格式例如 <c>[[0,6,0],[5,8,7],[0,9,0]]</c>。
        /// </summary>
        /// <param name="grid">要顯示的矩陣。</param>
        /// <returns>以中括號與逗號組成的單行矩陣字串。</returns>
        private static string FormatGrid(int[][] grid)
        {
            return $"[{string.Join(",", grid.Select(row => $"[{string.Join(",", row)}]"))}]";
        }

        private static readonly (int Row, int Column)[] Directions =
        [
            (-1, 0),
            (1, 0),
            (0, -1),
            (0, 1)
        ];

        private int[][] currentGrid = [];
        private int rowCount;
        private int columnCount;
        private int maximumGold;

        /// <summary>
        /// 使用原地標記與遞迴回溯，從每個含金格嘗試所有不重複的四方向路徑。
        /// 輸入須為符合題目限制的非空矩形矩陣；搜尋期間會暫時將目前格設為 0，
        /// 但每次回溯都會還原，因此輸出為最大黃金量且呼叫後輸入內容不變。
        /// </summary>
        /// <param name="grid">每格介於 0 到 100，且最多有 25 個含金格的矩陣。</param>
        /// <returns>任一合法簡單路徑可收集的最大黃金總量；沒有含金格時為 0。</returns>
        public int GetMaximumGold(int[][] grid)
        {
            currentGrid = grid;
            rowCount = grid.Length;
            columnCount = grid[0].Length;
            maximumGold = 0;

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    if (grid[row][column] > 0)
                    {
                        DFS(row, column, 0);
                    }
                }
            }

            return maximumGold;
        }

        /// <summary>
        /// 延伸原地標記解法目前的採礦路徑，更新跨所有起點共用的最大黃金量。
        /// 呼叫前必須先由 <see cref="GetMaximumGold(int[][])"/> 初始化矩陣與尺寸；
        /// 輸入座標須指向尚未造訪的含金格，方法完成後會還原該格內容。
        /// </summary>
        /// <param name="x">目前格的列索引。</param>
        /// <param name="y">目前格的欄索引。</param>
        /// <param name="gold">進入目前格之前已收集的黃金量。</param>
        public void DFS(int x, int y, int gold)
        {
            gold += currentGrid[x][y];
            maximumGold = Math.Max(maximumGold, gold);
            int originalGold = currentGrid[x][y];

            // 0 同時代表不可進入與已在目前路徑中，省去額外 visited 陣列。
            currentGrid[x][y] = 0;

            foreach ((int rowOffset, int columnOffset) in Directions)
            {
                int nextRow = x + rowOffset;
                int nextColumn = y + columnOffset;

                if (IsInside(currentGrid, nextRow, nextColumn) && currentGrid[nextRow][nextColumn] > 0)
                {
                    DFS(nextRow, nextColumn, gold);
                }
            }

            // 還原目前格，讓其他起點與其他分支能再次使用它。
            currentGrid[x][y] = originalGold;
        }

        /// <summary>
        /// 使用獨立 <c>visited</c> 陣列與遞迴 DFS，從每個含金格計算最佳路徑。
        /// 輸入須為符合題目限制的非空矩形矩陣；走訪狀態完全存放於布林矩陣，
        /// 因此不修改輸入，並輸出任一合法路徑可收集的最大黃金量。
        /// </summary>
        /// <param name="grid">每格介於 0 到 100，且最多有 25 個含金格的矩陣。</param>
        /// <returns>任一合法簡單路徑可收集的最大黃金總量；沒有含金格時為 0。</returns>
        public int GetMaximumGold2(int[][] grid)
        {
            bool[,] visited = new bool[grid.Length, grid[0].Length];
            int best = 0;

            for (int row = 0; row < grid.Length; row++)
            {
                for (int column = 0; column < grid[row].Length; column++)
                {
                    if (grid[row][column] > 0)
                    {
                        best = Math.Max(best, CollectWithVisited(grid, visited, row, column));
                    }
                }
            }

            return best;
        }

        /// <summary>
        /// 從指定含金格遞迴尋找最大後續收益，並以回溯方式維護目前路徑的造訪狀態。
        /// 輸入座標須位於矩陣內、包含黃金且尚未造訪；輸出包含目前格的最佳路徑總和，
        /// 並在回傳前清除目前格的造訪標記。
        /// </summary>
        /// <param name="grid">唯讀使用的金礦矩陣。</param>
        /// <param name="visited">目前遞迴路徑的造訪狀態。</param>
        /// <param name="row">目前格的列索引。</param>
        /// <param name="column">目前格的欄索引。</param>
        /// <returns>從目前格出發可收集的最大黃金量。</returns>
        private static int CollectWithVisited(int[][] grid, bool[,] visited, int row, int column)
        {
            visited[row, column] = true;
            int bestContinuation = 0;

            foreach ((int rowOffset, int columnOffset) in Directions)
            {
                int nextRow = row + rowOffset;
                int nextColumn = column + columnOffset;

                if (IsInside(grid, nextRow, nextColumn)
                    && grid[nextRow][nextColumn] > 0
                    && !visited[nextRow, nextColumn])
                {
                    bestContinuation = Math.Max(
                        bestContinuation,
                        CollectWithVisited(grid, visited, nextRow, nextColumn));
                }
            }

            // 清除標記後，其他分支仍可在自己的路徑中使用目前格。
            visited[row, column] = false;
            return grid[row][column] + bestContinuation;
        }

        /// <summary>
        /// 使用顯式堆疊執行迭代 DFS，並以 32 位元遮罩記錄目前路徑造訪過的含金格。
        /// 輸入須為符合題目限制的非空矩形矩陣，且含金格不超過 25 個；方法不修改輸入，
        /// 並輸出所有起點與所有合法簡單路徑中的最大黃金總量。
        /// </summary>
        /// <param name="grid">每格介於 0 到 100，且最多有 25 個含金格的矩陣。</param>
        /// <returns>任一合法簡單路徑可收集的最大黃金總量；沒有含金格時為 0。</returns>
        public int GetMaximumGold3(int[][] grid)
        {
            int[,] cellIndices = BuildGoldCellIndices(grid);
            Stack<(int Row, int Column, uint VisitedMask, int Gold)> paths = new();
            int best = 0;

            for (int row = 0; row < grid.Length; row++)
            {
                for (int column = 0; column < grid[row].Length; column++)
                {
                    if (cellIndices[row, column] >= 0)
                    {
                        uint startMask = 1u << cellIndices[row, column];
                        paths.Push((row, column, startMask, grid[row][column]));
                    }
                }
            }

            while (paths.Count > 0)
            {
                (int row, int column, uint visitedMask, int gold) = paths.Pop();
                best = Math.Max(best, gold);

                foreach ((int rowOffset, int columnOffset) in Directions)
                {
                    int nextRow = row + rowOffset;
                    int nextColumn = column + columnOffset;

                    if (!IsInside(grid, nextRow, nextColumn) || cellIndices[nextRow, nextColumn] < 0)
                    {
                        continue;
                    }

                    uint nextBit = 1u << cellIndices[nextRow, nextColumn];

                    // 遮罩中已有此位元時，代表目前路徑已使用該格，不能再次進入。
                    if ((visitedMask & nextBit) == 0)
                    {
                        paths.Push((
                            nextRow,
                            nextColumn,
                            visitedMask | nextBit,
                            gold + grid[nextRow][nextColumn]));
                    }
                }
            }

            return best;
        }

        /// <summary>
        /// 依掃描順序為每個含金格配置唯一位元索引，零值格則保留為 -1。
        /// 輸入須符合最多 25 個含金格的題目限制；輸出尺寸相同的索引矩陣，
        /// 供迭代解法以 <see cref="uint"/> 位元遮罩記錄目前路徑。
        /// </summary>
        /// <param name="grid">要建立含金格索引的矩陣。</param>
        /// <returns>含金格為 0 起算索引、零值格為 -1 的矩陣。</returns>
        private static int[,] BuildGoldCellIndices(int[][] grid)
        {
            int[,] cellIndices = new int[grid.Length, grid[0].Length];
            int nextIndex = 0;

            for (int row = 0; row < grid.Length; row++)
            {
                for (int column = 0; column < grid[row].Length; column++)
                {
                    cellIndices[row, column] = grid[row][column] > 0 ? nextIndex++ : -1;
                }
            }

            return cellIndices;
        }

        /// <summary>
        /// 判斷指定座標是否位於非空矩形矩陣範圍內。
        /// 輸入可包含負數或超出邊界的座標；輸出為座標是否可安全存取。
        /// </summary>
        /// <param name="grid">用來判斷邊界的非空矩形矩陣。</param>
        /// <param name="row">待檢查的列索引。</param>
        /// <param name="column">待檢查的欄索引。</param>
        /// <returns>座標位於矩陣內時為 <see langword="true"/>。</returns>
        private static bool IsInside(int[][] grid, int row, int column)
        {
            return row >= 0 && row < grid.Length && column >= 0 && column < grid[row].Length;
        }
    }
}