namespace leetcode_1351;

internal class Program
{
    /// <summary>
    /// 1351. Count Negative Numbers in a Sorted Matrix
    /// https://leetcode.com/problems/count-negative-numbers-in-a-sorted-matrix/
    /// 1351. 統計有序矩陣中的負數
    /// https://leetcode.cn/problems/count-negative-numbers-in-a-sorted-matrix/
    /// Given a matrix sorted in non-increasing order by rows and columns, return the number of negative values.
    /// 給定一個每列與每欄皆按非遞增順序排列的矩陣，回傳其中負數的數量。
    /// </summary>
    private static void Main()
    {
        (string Name, int[][] Grid, string Input, int Expected)[] cases =
        [
            (
                "Official example 1",
                [[4, 3, 2, -1], [3, 2, 1, -1], [1, 1, -1, -2], [-1, -1, -2, -3]],
                "[[4,3,2,-1],[3,2,1,-1],[1,1,-1,-2],[-1,-1,-2,-3]]",
                8),
            ("Official example 2", [[3, 2], [1, 0]], "[[3,2],[1,0]]", 0),
            ("Official example 3", [[1, -1], [-1, -1]], "[[1,-1],[-1,-1]]", 3),
            ("Official example 4", [[-1]], "[[-1]]", 1),
            ("Minimum non-negative", [[0]], "[[0]]", 0),
            (
                "Rectangular regression",
                [[5, 2, 0, -1], [3, 1, -1, -2], [1, 0, -2, -3]],
                "[[5,2,0,-1],[3,1,-1,-2],[1,0,-2,-3]]",
                5),
            ("100x100 all -100", CreateGrid(100, 100, -100), "100x100 matrix filled with -100", 10000),
            ("100x100 all 100", CreateGrid(100, 100, 100), "100x100 matrix filled with 100", 0)
        ];

        int passed = 0;

        foreach ((string name, int[][] grid, string input, int expected) in cases)
        {
            int actual = CountNegatives(grid);
            bool isPassed = actual == expected;
            passed += isPassed ? 1 : 0;

            Console.WriteLine(
                $"Case: {name} | Input: {input} | Expected: {expected} | Actual: {actual} | {(isPassed ? "PASS" : "FAIL")}");
        }

        Console.WriteLine($"Summary: {passed}/{cases.Length} checks passed.");

        if (passed != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 統計每列與每欄皆按非遞增順序排列之有效矩陣中的負數數量。
    /// 從右上角開始維持階梯式邊界：遇到負數時，同欄自目前列以下皆為負數；
    /// 遇到非負數時，則移至下一列繼續搜尋。回傳矩陣內所有負數的總數。
    /// </summary>
    /// <param name="grid">符合 LeetCode 題目限制、每列與每欄皆為非遞增順序的矩形矩陣。</param>
    /// <returns>矩陣中的負數總數。</returns>
    public static int CountNegatives(int[][] grid)
    {
        int row = 0;
        int column = grid[0].Length - 1;
        int count = 0;

        while (row < grid.Length && column >= 0)
        {
            if (grid[row][column] < 0)
            {
                // 同欄下方的值不會更大，因此可一次計入目前列到最後一列。
                count += grid.Length - row;
                column--;
            }
            else
            {
                // 目前值非負時，該列左側也皆非負，只需往下一列搜尋。
                row++;
            }
        }

        return count;
    }

    private static int[][] CreateGrid(int rows, int columns, int value)
    {
        int[][] grid = new int[rows][];

        for (int row = 0; row < rows; row++)
        {
            grid[row] = new int[columns];
            Array.Fill(grid[row], value);
        }

        return grid;
    }
}
