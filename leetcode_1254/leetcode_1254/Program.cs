using System.ComponentModel.DataAnnotations.Schema;

namespace leetcode_1254;

    /// <summary>
    /// 1254. Number of Closed Islands
    /// https://leetcode.com/problems/number-of-closed-islands/description/
    /// 1254. 统计封闭岛屿的数目
    /// https://leetcode.cn/problems/number-of-closed-islands/description/
    /// <para>English Original:</para>
    /// <para>Given a 2D <c>grid</c> consists of <c>0s</c> (land) and <c>1s</c> (water). An island is a maximal 4-directionally connected group of <c>0s</c> and a closed island is an island totally (all left, top, right, bottom) surrounded by <c>1s</c>.</para>
    /// <para>Return the number of closed islands.</para>
    /// <para>繁體中文：</para>
    /// <para>給定一個由 <c>0</c>（陸地）和 <c>1</c>（水域）組成的二維網格。島嶼是由 <c>0</c> 構成且在上下左右四個方向連通的極大區域；封閉島嶼是四周（左、上、右、下）都被 <c>1</c> 完全包圍的島嶼。</para>
    /// <para>回傳封閉島嶼的數量。</para>
    /// </summary>
class Program
{
    /// <summary>
    /// 執行固定網格案例，分別驗證 DFS 與 BFS，並輸出每項結果及總通過數。
    /// </summary>
    /// <param name="args">命令列參數；本範例程式不使用此參數。</param>
    /// <returns>無回傳值；若有案例未通過，會將程序結束碼設為 1。</returns>
    static void Main(string[] args)
    {
        var testCases = new (string Name, int[][] Grid, int Expected)[]
        {
            ("Example 1", new int[][]
            {
                new[] { 1, 1, 1, 1, 1, 1, 1, 0 },
                new[] { 1, 0, 0, 0, 0, 1, 1, 0 },
                new[] { 1, 0, 1, 0, 1, 1, 1, 0 },
                new[] { 1, 0, 0, 0, 0, 1, 0, 1 },
                new[] { 1, 1, 1, 1, 1, 1, 1, 0 }
            }, 2),
            ("Example 2", new int[][]
            {
                new[] { 0, 0, 1, 0, 0 },
                new[] { 0, 1, 0, 1, 0 },
                new[] { 0, 1, 1, 1, 0 }
            }, 1),
            ("Example 3", new int[][]
            {
                new[] { 1, 1, 1, 1, 1, 1, 1 },
                new[] { 1, 0, 0, 0, 0, 0, 1 },
                new[] { 1, 0, 1, 1, 1, 0, 1 },
                new[] { 1, 0, 1, 0, 1, 0, 1 },
                new[] { 1, 0, 1, 1, 1, 0, 1 },
                new[] { 1, 0, 0, 0, 0, 0, 1 },
                new[] { 1, 1, 1, 1, 1, 1, 1 }
            }, 2),
            ("Single land cell touches boundary", new int[][] { new[] { 0 } }, 0),
            ("Single water cell", new int[][] { new[] { 1 } }, 0)
        };

        var solution = new Program();
        var solvers = new (string Name, Func<int[][], int> Solve)[]
        {
            ("DFS", solution.ClosedIsland),
            ("BFS", solution.ClosedIslandBFS)
        };
        int passedChecks = 0;
        int totalChecks = testCases.Length * solvers.Length;

        foreach (var testCase in testCases)
        {
            foreach (var solver in solvers)
            {
                // 兩種解法都會把已走訪陸地改成 -1，因此各自使用原始案例的深層複本。
                int[][] grid = testCase.Grid.Select(row => (int[])row.Clone()).ToArray();
                int actual = solver.Solve(grid);
                bool passed = actual == testCase.Expected;
                if (passed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | {solver.Name} | {testCase.Name} | expected={testCase.Expected}, actual={actual}");
            }
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用深度優先搜尋（DFS）走訪每個陸地連通區域，回傳未接觸網格邊界的區域數。
    /// 輸入為非空矩形網格，0 表示陸地、1 表示水域；已走訪陸地會在原陣列中改為 -1。
    /// </summary>
    /// <param name="grid">由 0（陸地）和 1（水域）組成的非空矩形網格。</param>
    /// <returns>完全被水域包圍的封閉島嶼數量。</returns>
    public int ClosedIsland(int[][] grid)
    {
        int ans = 0;
        int rows = grid.Length;
        int cols = grid[0].Length;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // 只從尚未走訪的陸地開始 DFS。
                if (grid[i][j] == 0 && DFS(i, j, grid, rows, cols))
                {
                    ans++;
                }
            }
        }
        return ans;
    }

    /// <summary>
    /// 使用廣度優先搜尋（BFS）逐一走訪陸地連通區域，回傳未接觸網格邊界的區域數。
    /// 輸入為非空矩形網格，0 表示陸地、1 表示水域；已走訪陸地會在原陣列中改為 -1。
    /// </summary>
    /// <param name="grid">由 0（陸地）和 1（水域）組成的非空矩形網格。</param>
    /// <returns>完全被水域包圍的封閉島嶼數量。</returns>
    public int ClosedIslandBFS(int[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int closedIslands = 0;
        var queue = new Queue<(int Row, int Col)>();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (grid[row][col] != 0)
                {
                    continue;
                }

                bool isClosed = true;
                grid[row][col] = -1;
                queue.Enqueue((row, col));

                while (queue.Count > 0)
                {
                    var (currentRow, currentCol) = queue.Dequeue();
                    // 連通區域中只要有一格陸地位於最外圈，整座島就不是封閉島。
                    if (currentRow == 0 || currentRow == rows - 1 || currentCol == 0 || currentCol == cols - 1)
                    {
                        isClosed = false;
                    }

                    EnqueueLand(currentRow - 1, currentCol);
                    EnqueueLand(currentRow + 1, currentCol);
                    EnqueueLand(currentRow, currentCol - 1);
                    EnqueueLand(currentRow, currentCol + 1);
                }

                if (isClosed)
                {
                    closedIslands++;
                }
            }
        }

        return closedIslands;

        void EnqueueLand(int row, int col)
        {
            if (row < 0 || col < 0 || row >= rows || col >= cols || grid[row][col] != 0)
            {
                return;
            }

            // 入列時立即標記，確保其他相鄰格不會重複排入同一個陸地格。
            grid[row][col] = -1;
            queue.Enqueue((row, col));
        }
    }

    /// <summary>
    /// 從指定格以 DFS 探索其陸地連通區域；遞迴碰到網格外代表該島接觸邊界。
    /// 輸入為網格座標、網格與尺寸；回傳 true 表示此區域未接觸邊界，false 表示接觸邊界。
    /// </summary>
    /// <param name="x">目前列索引；允許遞迴呼叫時暫時超出網格。</param>
    /// <param name="y">目前行索引；允許遞迴呼叫時暫時超出網格。</param>
    /// <param name="grid">由 0（陸地）和 1（水域）組成的非空矩形網格，走訪陸地會改為 -1。</param>
    /// <param name="m">網格列數。</param>
    /// <param name="n">網格行數。</param>
    /// <returns>區域未接觸邊界時為 true；遞迴越界時為 false。</returns>
    public bool DFS(int x, int y, int[][] grid, int m, int n)
    {
        // 越界代表島嶼不是封閉的。
        if (x < 0 || y < 0 || x >= m || y >= n)
        {
            return false;
        }

        // 遇到水域或已走訪的位置，不再探索。
        if (grid[x][y] != 0)
        {
            return true;
        }

        // 將目前陸地標記為已走訪。
        grid[x][y] = -1;

        // 分別保存四向結果，讓任何一向越界的結果往回傳，同時完成整個連通區域的走訪。
        bool up = DFS(x - 1, y, grid, m, n);
        bool down = DFS(x + 1, y, grid, m, n);
        bool left = DFS(x, y - 1, grid, m, n);
        bool right = DFS(x, y + 1, grid, m, n);

        // 四個方向都沒有越界，才是封閉島嶼。
        return up && down && left && right;
    }
}
