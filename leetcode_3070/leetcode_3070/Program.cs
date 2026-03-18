namespace leetcode_3070;

class Program
{
    /// <summary>
    /// 3070. Count Submatrices with Top-Left Element and Sum Less Than k
    /// https://leetcode.com/problems/count-submatrices-with-top-left-element-and-sum-less-than-k/description/?envType=daily-question&envId=2026-03-18
    /// 3070. 元素和小於等於 k 的子矩陣的數目
    /// https://leetcode.cn/problems/count-submatrices-with-top-left-element-and-sum-less-than-k/description/?envType=daily-question&envId=2026-03-18
    ///
    /// You are given a 0-indexed integer matrix grid and an integer k.
    /// Return the number of submatrices that contain the top-left element of the grid,
    /// and have a sum less than or equal to k.
    ///
    /// 給定一個以 0 為索引的整數矩陣 grid 以及一個整數 k。
    /// 回傳包含矩陣左上角元素，且元素總和小於等於 k 的子矩陣數目。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試範例 1: grid = [[1,1,1],[1,1,1],[1,1,1]], k = 4 => 預期輸出: 6
        int[][] grid1 = [[1, 1, 1], [1, 1, 1], [1, 1, 1]];
        Console.WriteLine($"Test 1: {solution.CountSubmatrices(grid1, 4)}"); // 6

        // 測試範例 2: grid = [[7,6,3],[6,6,1]], k = 18 => 預期輸出: 4
        int[][] grid2 = [[7, 6, 3], [6, 6, 1]];
        Console.WriteLine($"Test 2: {solution.CountSubmatrices(grid2, 18)}"); // 4

        // 測試範例 3: 單一元素矩陣，元素 ≤ k => 預期輸出: 1
        int[][] grid3 = [[5]];
        Console.WriteLine($"Test 3: {solution.CountSubmatrices(grid3, 5)}"); // 1

        // 測試範例 4: 單一元素矩陣，元素 > k => 預期輸出: 0
        int[][] grid4 = [[10]];
        Console.WriteLine($"Test 4: {solution.CountSubmatrices(grid4, 3)}"); // 0

        // 測試範例 5: 單一列矩陣 => 預期輸出: 3
        int[][] grid5 = [[1, 2, 3]];
        Console.WriteLine($"Test 5: {solution.CountSubmatrices(grid5, 6)}"); // 3

        // 測試範例 6: 單一行矩陣 => 預期輸出: 2
        int[][] grid6 = [[2], [3], [5]];
        Console.WriteLine($"Test 6: {solution.CountSubmatrices(grid6, 5)}"); // 2

        // 測試範例 7: 全部為 0 的矩陣，所有子矩陣和皆為 0 ≤ k => 預期輸出: 6
        int[][] grid7 = [[0, 0, 0], [0, 0, 0]];
        Console.WriteLine($"Test 7: {solution.CountSubmatrices(grid7, 0)}"); // 6

        // 測試範例 8: 左上角元素已超過 k => 預期輸出: 0
        int[][] grid8 = [[100, 1], [1, 1]];
        Console.WriteLine($"Test 8: {solution.CountSubmatrices(grid8, 50)}"); // 0

        // 測試範例 9: k 非常大，所有子矩陣都符合 => 預期輸出: 9
        int[][] grid9 = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
        Console.WriteLine($"Test 9: {solution.CountSubmatrices(grid9, 1000000000)}"); // 9
    }

    /// <summary>
    /// 方法一：二維前綴和
    ///
    /// 思路與演算法：
    /// 題目要求統計包含矩陣 grid 左上角元素的所有子矩陣中，元素和不超過 k 的子矩陣個數。
    ///
    /// 從左上角出發，按行優先順序遍歷矩陣，將當前位置 (i, j) 視為子矩陣的右下角。
    /// 維護陣列 cols[j] 記錄第 j 列從第 0 行到目前第 i 行的元素總和。
    /// 遍歷第 i 行時，由左至右逐列累加 cols[j]，即可得到以 (0,0) 為左上角、(i,j) 為右下角的子矩陣元素和。
    /// 若累加和 ≤ k，則答案加 1。
    ///
    /// 時間複雜度：O(n * m)
    /// 空間複雜度：O(m)，其中 m 為列數
    /// </summary>
    /// <param name="grid">整數矩陣</param>
    /// <param name="k">元素和上限</param>
    /// <returns>符合條件的子矩陣數目</returns>
    public int CountSubmatrices(int[][] grid, int k) 
    {
        int n = grid.Length;
        int m = grid[0].Length;
        // cols[j] 記錄第 j 列從第 0 行累加到當前行的總和
        int[] cols = new int[m];
        int res = 0;

        for (int i = 0; i < n; i++)
        {
            // rows 為當前行中，由左向右累加 cols[j] 的值，即子矩陣 (0,0)~(i,j) 的元素和
            int rows = 0;
            for (int j = 0; j < m; j++)
            {
                // 將當前元素累加至該列的前綴和
                cols[j] += grid[i][j];
                // 累加至行方向的前綴和，得到子矩陣總和
                rows += cols[j];
                // 若子矩陣總和 ≤ k，計入答案
                if (rows <= k)
                {
                    res++;
                }
            }
        }

        return res;
    }
}
