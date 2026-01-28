namespace leetcode_3651;

class Program
{
    /// <summary>
    /// 3651. Minimum Cost Path with Teleportations
    /// https://leetcode.com/problems/minimum-cost-path-with-teleportations/description/?envType=daily-question&envId=2026-01-28
    /// https://leetcode.cn/problems/minimum-cost-path-with-teleportations/description/?envType=daily-question&envId=2026-01-28
    ///
    /// English:
    /// You are given an m x n 2D integer array grid and an integer k. You start at the top-left cell (0, 0) and your goal is to reach the bottom-right cell (m - 1, n - 1).
    /// There are two types of moves available:
    /// - Normal move: You can move right or down from your current cell (i, j), i.e. to (i, j + 1) or (i + 1, j). The cost is the value of the destination cell.
    /// - Teleportation: You can teleport from any cell (i, j) to any cell (x, y) such that grid[x][y] <= grid[i][j]; the cost of this move is 0. You may teleport at most k times.
    /// Return the minimum total cost to reach cell (m - 1, n - 1) from (0, 0).
    ///
    /// 繁體中文:
    /// 給定一個 m x n 的整數陣列 grid 和一個整數 k。你從左上角格子 (0, 0) 出發，目標是抵達右下角格子 (m - 1, n - 1)。
    /// 可用的移動類型有兩種：
    /// - 一般移動：從格子 (i, j) 向右或向下移動到 (i, j + 1) 或 (i + 1, j)，花費為目的格子的值。
    /// - 傳送：可以從任意格子 (i, j) 傳送到任何滿足 grid[x][y] <= grid[i][j] 的格子 (x, y)，此移動花費為 0。最多可傳送 k 次。
    /// 回傳從 (0, 0) 到達 (m - 1, n - 1) 的最小總花費。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試範例 1: grid = [[0,1,2],[3,4,5]], k = 1
        // 預期輸出: 2
        // 說明: 從 (0,0) 向右移動到 (0,1) 花費 1，再向右移動到 (0,2) 花費 2，
        //       傳送到 (1,2) 花費 0，最後向下移動到 (1,2) 已在目的地
        int[][] grid1 = [[0, 1, 2], [3, 4, 5]];
        int k1 = 1;
        Console.WriteLine($"測試 1: MinCost(grid1, {k1}) = {solution.MinCost(grid1, k1)}");

        // 測試範例 2: grid = [[3,1],[5,2]], k = 2
        // 預期輸出: 0
        // 說明: 可使用傳送從較高值格子移動到較低值格子，最終達到目的地
        int[][] grid2 = [[3, 1], [5, 2]];
        int k2 = 2;
        Console.WriteLine($"測試 2: MinCost(grid2, {k2}) = {solution.MinCost(grid2, k2)}");

        // 測試範例 3: grid = [[1,2,3],[4,5,6],[7,8,9]], k = 0
        // 預期輸出: 21 (不能使用傳送，只能一般移動)
        int[][] grid3 = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
        int k3 = 0;
        Console.WriteLine($"測試 3: MinCost(grid3, {k3}) = {solution.MinCost(grid3, k3)}");

        // 測試範例 4: grid = [[5,3,1],[4,2,6]], k = 1
        int[][] grid4 = [[5, 3, 1], [4, 2, 6]];
        int k4 = 1;
        Console.WriteLine($"測試 4: MinCost(grid4, {k4}) = {solution.MinCost(grid4, k4)}");
    }

    /// <summary>
    /// 使用動態規劃求解從 (0,0) 到 (m-1,n-1) 的最小成本路徑（含傳送功能）。
    /// 
    /// <para><b>解題思路：動態規劃 + 排序優化</b></para>
    /// <para>
    /// 定義 costs[t][i][j] 表示恰好使用 t 次傳送，從 (i,j) 移動到 (m-1,n-1) 的最小移動總成本。
    /// 由於 costs[t] 只依賴 costs[t-1]，可以省略 t 這一維度，用二維陣列 costs[i,j] 降低空間複雜度。
    /// </para>
    /// 
    /// <para><b>轉移方程：</b></para>
    /// <list type="number">
    ///   <item>
    ///     <description>
    ///       <b>一般移動（不使用傳送）：</b>從 (i,j) 移動到 (i+1,j) 或 (i,j+1)
    ///       <code>costs[i][j] = min(costs[i+1][j] + grid[i+1][j], costs[i][j+1] + grid[i][j+1])</code>
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <b>傳送移動：</b>可傳送到所有 (x,y) 且 grid[x][y] ≤ grid[i][j]，花費為 0
    ///       <code>costs[i][j] = min(costs[x][y]) 其中 grid[x][y] ≤ grid[i][j]</code>
    ///     </description>
    ///   </item>
    /// </list>
    /// 
    /// <para><b>優化技巧：</b></para>
    /// <para>
    /// 將所有單元格座標按 grid 值升序排序，使用雙指標維護值相同的區間，
    /// 可高效計算所有 grid[x][y] ≤ grid[i][j] 的最小 costs 值。
    /// </para>
    /// </summary>
    /// <param name="grid">m x n 的二維整數陣列，表示每個格子的值</param>
    /// <param name="k">最多可使用的傳送次數</param>
    /// <returns>從 (0,0) 到達 (m-1,n-1) 的最小總花費</returns>
    /// <example>
    /// <code>
    ///  範例：grid = [[0,1,2],[3,4,5]], k = 1
    ///  預期輸出: 2
    /// var solution = new Program();
    /// int result = solution.MinCost(new int[][] { new[] {0,1,2}, new[] {3,4,5} }, 1);
    /// </code>
    /// </example>
    public int MinCost(int[][] grid, int k)
    {
        int m = grid.Length;
        int n = grid[0].Length;

        // Step 1: 建立所有單元格座標的列表
        // 用於後續按 grid 值排序，以便高效處理傳送邏輯
        var points = new List<(int, int)>();
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                points.Add((i, j));
            }
        }

        // Step 2: 按照 grid 值升序排序所有座標
        // 這樣可以利用雙指標找出所有 grid[x][y] <= grid[i][j] 的格子
        points.Sort((p1, p2) => grid[p1.Item1][p1.Item2] - grid[p2.Item1][p2.Item2]);

        // Step 3: 初始化 costs 陣列
        // costs[i,j] 表示從 (i,j) 到達終點 (m-1,n-1) 的最小成本
        int[,] costs = new int[m, n];
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                costs[i, j] = int.MaxValue;
            }
        }

        // Step 4: 對每次傳送次數 t 進行迭代（共 k+1 輪）
        // t=0 表示不使用傳送，t=1 表示使用 1 次傳送，依此類推
        for (int t = 0; t <= k; t++)
        {
            // Step 4.1: 處理傳送轉移
            // 遍歷按 grid 值排序的座標，維護當前最小成本 minCost
            // 對於 grid 值相同的格子區間 [j, i]，它們可以傳送到的目標是相同的
            int minCost = int.MaxValue;
            for (int i = 0, j = 0; i < points.Count; i++)
            {
                // 更新目前遇到的最小成本（來自上一輪的 costs 值）
                minCost = Math.Min(minCost, costs[points[i].Item1, points[i].Item2]);

                // 若下一個座標的 grid 值與當前相同，繼續累積
                if (i + 1 < points.Count && grid[points[i].Item1][points[i].Item2] == grid[points[i + 1].Item1][points[i + 1].Item2])
                {
                    continue;
                }

                // 對區間 [j, i] 內所有座標更新傳送後的最小成本
                // 這些座標都可以透過傳送到達 minCost 對應的位置
                for (int r = j; r <= i; r++)
                {
                    costs[points[r].Item1, points[r].Item2] = minCost;
                }

                j = i + 1;
            }

            // Step 4.2: 處理一般移動轉移（從右下角往左上角逆向遞推）
            // 對於每個格子，考慮向右或向下移動的成本
            for (int i = m - 1; i >= 0; i--)
            {
                for (int j = n - 1; j >= 0; j--)
                {
                    // 終點的成本為 0
                    if (i == m - 1 && j == n - 1)
                    {
                        costs[i, j] = 0;
                        continue;
                    }

                    // 向下移動：從 (i,j) 移動到 (i+1,j)，花費為 grid[i+1][j]
                    if (i != m - 1)
                    {
                        costs[i, j] = Math.Min(costs[i, j], costs[i + 1, j] + grid[i + 1][j]);
                    }

                    // 向右移動：從 (i,j) 移動到 (i,j+1)，花費為 grid[i][j+1]
                    if (j != n - 1)
                    {
                        costs[i, j] = Math.Min(costs[i, j], costs[i, j + 1] + grid[i][j + 1]);
                    }
                }
            }
        }

        // 傳回從起點 (0,0) 到終點的最小總成本
        return costs[0, 0];
    }
}
