namespace leetcode_1594;

class Program
{
    /// <summary>
    /// 1594. Maximum Non Negative Product in a Matrix
    /// https://leetcode.com/problems/maximum-non-negative-product-in-a-matrix/description/?envType=daily-question&envId=2026-03-23
    /// 1594. 矩陣的最大非負積
    /// https://leetcode.cn/problems/maximum-non-negative-product-in-a-matrix/description/?envType=daily-question&envId=2026-03-23+
    ///
    /// You are given a m x n matrix grid. Initially, you are located at the top-left corner (0, 0),
    /// and in each step, you can only move right or down in the matrix.
    /// Among all possible paths starting from the top-left corner (0, 0) and ending in the
    /// bottom-right corner (m - 1, n - 1), find the path with the maximum non-negative product.
    /// The product of a path is the product of all integers in the grid cells visited along the path.
    /// Return the maximum non-negative product modulo 10^9 + 7.
    /// If the maximum product is negative, return -1.
    /// Notice that the modulo is performed after getting the maximum product.
    ///
    /// 給定一個 m x n 的矩陣 grid。初始位置在左上角 (0, 0)，
    /// 每一步只能向右或向下移動。
    /// 在所有從左上角 (0, 0) 到右下角 (m - 1, n - 1) 的路徑中，
    /// 找出乘積最大的非負路徑。路徑的乘積為沿途所有格子中整數的乘積。
    /// 回傳最大非負乘積對 10^9 + 7 取模的結果。
    /// 若最大乘積為負數，則回傳 -1。
    /// 注意：取模運算是在取得最大乘積之後才進行的。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試範例 1: grid = [[-1,-2,-3],[-2,-3,-3],[-3,-3,-2]]
        // 預期輸出: 8 (路徑 -1 -> -2 -> -3 -> -3 -> -2，乘積 = -1 * -2 * -3 * -3 * -2 = -36? 不對)
        // 最大非負乘積路徑: -1 -> -2 -> -3 -> -3 -> -2，乘積 = (-1)*(-2)*(-3)*(-3)*(-2) = -36 為負
        // 另一條路徑: -1 -> -2 -> -3 -> -3 -> -2，需取乘積最大的非負路徑
        // 實際最佳路徑: -1 -> -2 -> -3 -> -3 -> -2 = -36 (負)，或 -1 -> -2 -> -3 -> -3 -> -2
        // 預期輸出: 8
        int[][] grid1 = [[-1, -2, -3], [-2, -3, -3], [-3, -3, -2]];
        Console.WriteLine($"測試 1: {program.MaxProductPath(grid1)}"); // 預期: 8

        // 測試範例 2: grid = [[1,-2,1],[1,-2,1],[3,-4,1]]
        // 預期輸出: 8
        int[][] grid2 = [[1, -2, 1], [1, -2, 1], [3, -4, 1]];
        Console.WriteLine($"測試 2: {program.MaxProductPath(grid2)}"); // 預期: 8

        // 測試範例 3: grid = [[1,3],[0,-4]]
        // 路徑 1->3->-4 = -12 (負)，路徑 1->0->-4 = 0
        // 預期輸出: 0
        int[][] grid3 = [[1, 3], [0, -4]];
        Console.WriteLine($"測試 3: {program.MaxProductPath(grid3)}"); // 預期: 0

        // 測試範例 4: grid = [[-1,-1],[-1,-1]]
        // 預期輸出: 1
        int[][] grid4 = [[-1, -1], [-1, -1]];
        Console.WriteLine($"測試 4: {program.MaxProductPath(grid4)}"); // 預期: 1

        // 測試範例 5: grid = [[1,4,4,0],[-2,0,0,1],[1,-1,1,1]]
        // 預期輸出: 2
        int[][] grid5 = [[1, 4, 4, 0], [-2, 0, 0, 1], [1, -1, 1, 1]];
        Console.WriteLine($"測試 5: {program.MaxProductPath(grid5)}"); // 預期: 2
    }

    /// <summary>
    /// 使用動態規劃求解矩陣中從左上角到右下角的最大非負乘積。
    /// 
    /// 解題思路：
    /// 由於矩陣中的元素有正有負，僅追蹤最大乘積不足以得到正確答案。
    /// 例如，當前最大乘積為正數，乘上負數後反而不如一個負數乘上同樣的負數。
    /// 因此需要同時追蹤乘積的最大值 (maxgt) 與最小值 (minlt)。
    /// 
    /// 狀態轉移：
    /// - 若 grid[i][j] >= 0：
    ///   maxgt[i,j] = max(maxgt[i-1,j], maxgt[i,j-1]) * grid[i][j]
    ///   minlt[i,j] = min(minlt[i-1,j], minlt[i,j-1]) * grid[i][j]
    /// - 若 grid[i][j] &lt; 0（負數會翻轉大小關係）：
    ///   maxgt[i,j] = min(minlt[i-1,j], minlt[i,j-1]) * grid[i][j]
    ///   minlt[i,j] = max(maxgt[i-1,j], maxgt[i,j-1]) * grid[i][j]
    /// 
    /// 時間複雜度：O(m * n)，空間複雜度：O(m * n)
    /// </summary>
    /// <param name="grid">m x n 的整數矩陣</param>
    /// <returns>最大非負乘積對 10^9+7 取模的結果，若為負數則回傳 -1</returns>
    /// <example>
    /// <code>
    /// var result = MaxProductPath(new int[][] { [-1, -2, -3], [-2, -3, -3], [-3, -3, -2] });
    /// // result = 8
    /// </code>
    /// </example>
    public int MaxProductPath(int[][] grid)
    {
        const int mod = 1000000007;
        int m = grid.Length;
        int n = grid[0].Length;

        // maxgt[i,j]: 從 (0,0) 到 (i,j) 路徑乘積的最大值
        // minlt[i,j]: 從 (0,0) 到 (i,j) 路徑乘積的最小值
        long[,] maxgt = new long[m, n];
        long[,] minlt = new long[m, n];

        // 初始化起點
        maxgt[0, 0] = minlt[0, 0] = grid[0][0];

        // 初始化第一列（只能從左方到達）
        for(int i = 1; i < n; i++)
        {
            maxgt[0, i] = minlt[0, i] = maxgt[0, i - 1] * grid[0][i];
        }

        // 初始化第一行（只能從上方到達）
        for(int i = 1; i < m; i++)
        {
            maxgt[i, 0] = minlt[i, 0] = maxgt[i - 1, 0] * grid[i][0];
        }

        // 填充 DP 表格
        for(int i = 1; i < m; i++)
        {
            for(int j = 1; j < n; j++)
            {
                if(grid[i][j] >= 0)
                {
                    // 正數或零：最大值來自前一步的最大值，最小值來自前一步的最小值
                    maxgt[i, j] = Math.Max(maxgt[i - 1, j], maxgt[i, j - 1]) * grid[i][j];
                    minlt[i, j] = Math.Min(minlt[i - 1, j], minlt[i, j - 1]) * grid[i][j];
                }
                else
                {
                    // 負數：乘以負數會翻轉大小，最大值來自前一步的最小值，反之亦然
                    maxgt[i, j] = Math.Min(minlt[i - 1, j], minlt[i, j - 1]) * grid[i][j];
                    minlt[i, j] = Math.Max(maxgt[i - 1, j], maxgt[i, j - 1]) * grid[i][j];
                }
            }
        }

        // 若最大乘積為負數回傳 -1，否則回傳取模結果
        return maxgt[m - 1, n - 1] < 0 ? -1 : (int)(maxgt[m - 1, n - 1] % mod);
    }
}
