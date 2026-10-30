namespace leetcode_3882;

    /// <summary>
    /// 3882. Minimum XOR Path in a Grid
    /// https://leetcode.com/problems/minimum-xor-path-in-a-grid/description/
    /// 3882. 网格图中最小异或路径
    /// https://leetcode.cn/problems/minimum-xor-path-in-a-grid/description/
    /// English
    /// You are given a 2D integer array grid of size m * n.
    /// You start at the top-left cell (0, 0) and want to reach the bottom-right cell (m - 1, n - 1).
    /// At each step, you may move either right or down.
    /// The cost of a path is defined as the bitwise XOR of all the values in the cells along that path, including the start and end cells.
    /// Return the minimum possible XOR value among all valid paths from (0, 0) to (m - 1, n - 1).
    /// 繁體中文
    /// 給定一個大小為 m * n 的二維整數陣列 grid。
    /// 你從左上角儲存格 (0, 0) 出發，並希望抵達右下角儲存格 (m - 1, n - 1)。
    /// 每一步你可以向右或向下移動。
    /// 路徑成本定義為沿途所有儲存格數值的位元 XOR，包含起點與終點。
    /// 請回傳從 (0, 0) 到 (m - 1, n - 1) 的所有有效路徑中，可能取得的最小 XOR 值。
    /// </summary>
class Program
{
    /// <summary>
    /// 執行官方範例、單格與值域邊界案例，驗證兩種解法並將預期值與計算結果輸出至主控台。
    /// </summary>
    /// <param name="args">命令列參數；此範例程式不使用。</param>
    static void Main(string[] args)
    {
        Program solution = new();
        (string Name, int[][] Grid, int Expected)[] testCases =
        {
            ("Example 1", new[] { new[] { 1, 2 }, new[] { 3, 4 } }, 6),
            ("Example 2", new[] { new[] { 6, 7 }, new[] { 5, 8 } }, 9),
            ("Example 3", new[] { new[] { 2, 7, 5 } }, 0),
            ("Single cell", new[] { new[] { 7 } }, 7),
            ("Maximum value", new[] { new[] { 0, 1023 } }, 1023)
        };

        foreach ((string name, int[][] grid, int expected) in testCases)
        {
            int dpResult = solution.MinCost(grid);
            int dfsResult = solution.MinCostDfs(grid);
            bool passed = dpResult == expected && dfsResult == expected;
            string status = passed ? "PASS" : "FAIL";

            Console.WriteLine($"{name}: expected={expected}, DP={dpResult}, DFS={dfsResult}, {status}");

            if (!passed)
            {
                throw new InvalidOperationException($"{name} did not match the expected result.");
            }
        }

        Console.WriteLine("All 10 solution checks passed.");
    }

    /// <summary>
    /// 以動態規劃計算從左上角到右下角的最小路徑 XOR 值。
    /// </summary>
    /// <param name="grid">非空網格；列數與欄數皆至少為 1、總格數不超過 1000，且格值介於 0 到 1023。</param>
    /// <returns>所有合法路徑 XOR 值中的最小值。</returns>
    public int MinCost(int[][] grid)
    {
        int m = grid.Length;
        int n = grid[0].Length;

        bool[][] dp = new bool[n][];
        for (int j = 0; j < n; j++)
        {
            dp[j] = new bool[1024];
        }
        bool[] next = new bool[1024];

        dp[0][grid[0][0]] = true;

        // dp[j] 保留上方格子的狀態，dp[j - 1] 則是本列左方格子的狀態。
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                int value = grid[i][j];
                Array.Clear(next, 0, next.Length);

                // 從上方或左方可達的 XOR 狀態，加入目前格子的值後繼續傳遞。
                for (int k = 0; k < 1024; k++)
                {
                    if (dp[j][k] || (j > 0 && dp[j - 1][k]))
                    {
                        next[k ^ value] = true;
                    }
                }
                Array.Copy(next, dp[j], 1024);
            }

        }
        return Array.IndexOf(dp[n - 1], true);
    }

    /// <summary>
    /// 由終點反向以 DFS 搜尋路徑，使用格子與剩餘 XOR 去重，並在找到 XOR 0 時剪枝。
    /// </summary>
    /// <param name="grid">非空網格；列數與欄數皆至少為 1、總格數不超過 1000，且格值介於 0 到 1023。</param>
    /// <returns>從左上角到右下角的最小路徑 XOR 值。</returns>
    public int MinCostDfs(int[][] grid)
    {
        int m = grid.Length;
        int n = grid[0].Length;

        int orAll = 0;
        foreach (int[] row in grid)
        {
            foreach (int value in row)
            {
                orAll |= value;
            }
        }

        // 路徑 XOR 只會使用網格中出現的位元，因此 orAll 可作為 XOR 狀態的上界。
        bool[,,] visited = new bool[m, n, orAll + 1];
        int answer = int.MaxValue;

        void Dfs(int i, int j, int xor)
        {
            // xor 是尚未包含目前格子的後綴 XOR；相同格子與後綴狀態只需探索一次。
            if (answer == 0 || i < 0 || j < 0 || visited[i, j, xor])
            {
                return;
            }

            visited[i, j, xor] = true;
            xor ^= grid[i][j];

            if (i == 0 && j == 0)
            {
                answer = Math.Min(answer, xor);
                return;
            }

            // 0 已是最小可能值，找到後不必再探索其他分支。
            Dfs(i - 1, j, xor);
            Dfs(i, j - 1, xor);
        }

        Dfs(m - 1, n - 1, 0);
        return answer;
    }
}
