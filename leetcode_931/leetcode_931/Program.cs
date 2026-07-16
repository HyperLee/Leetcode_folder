namespace leetcode_931;

class Program
{
    /// <summary>
    /// 931. Minimum Falling Path Sum
    /// https://leetcode.com/problems/minimum-falling-path-sum/description/
    ///
    /// Given an n x n array of integers matrix, return the minimum sum of any falling path through matrix.
    ///
    /// A falling path starts at any element in the first row and chooses the element in the next row that is either directly below or diagonally left/right. Specifically, the next element from position (row, col) will be (row + 1, col - 1), (row + 1, col), or (row + 1, col + 1).
    ///
    /// 931. 下降路徑最小和
    /// https://leetcode.cn/problems/minimum-falling-path-sum/description/
    /// 
    /// 給定一個 n x n 的整數陣列 matrix，請回傳 matrix 中任意下降路徑的最小總和。
    ///
    /// 下降路徑可以從第一列的任意元素開始，並在下一列選擇正下方、左下方或右下方的元素。具體而言，位置 (row, col) 的下一個元素會是 (row + 1, col - 1)、(row + 1, col) 或 (row + 1, col + 1)。
    /// </summary>
    /// <remarks>
    /// 程式進入點；使用固定案例驗證二維動態規劃與一維空間優化解法，
    /// 並輸出每組案例的預期值、實際值與 PASS/FAIL 結果。
    /// </remarks>
    /// <param name="args">命令列參數；本範例不使用。</param>
    static void Main(string[] args)
    {
        (string Name, int[][] Matrix, int Expected)[] testCases =
        [
            (
                "官方正數範例",
                [[2, 1, 3], [6, 5, 4], [7, 8, 9]],
                13
            ),
            (
                "官方負數範例",
                [[-19, 57], [-40, -5]],
                -59
            ),
            (
                "單一元素邊界案例",
                [[-48]],
                -48
            )
        ];

        Console.WriteLine("LeetCode 931 - Minimum Falling Path Sum");
        Console.WriteLine(new string('=', 48));

        int passedChecks = 0;
        foreach ((string name, int[][] matrix, int expected) in testCases)
        {
            passedChecks += RunTestCase(name, matrix, expected);
        }

        int totalChecks = testCases.Length * 2;
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
    }

    /// <summary>
    /// 執行單一矩陣案例，分別驗證二維 DP 與一維空間優化解法。
    /// 輸入必須是非空的正方形整數矩陣；輸出案例內容、預期值、實際值與驗證狀態，
    /// 並回傳通過的解法數量。
    /// </summary>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="matrix">要求最小下降路徑和的正方形矩陣。</param>
    /// <param name="expected">案例的預期最小路徑和。</param>
    /// <returns>兩個解法中結果正確的數量，範圍為 0 至 2。</returns>
    private static int RunTestCase(string name, int[][] matrix, int expected)
    {
        int twoDimensionalResult = MinFallingPathSum(matrix);
        int optimizedResult = MinFallingPathSumOptimized(matrix);
        bool twoDimensionalPassed = twoDimensionalResult == expected;
        bool optimizedPassed = optimizedResult == expected;

        Console.WriteLine($"Case: {name}");
        Console.WriteLine($"Matrix: {FormatMatrix(matrix)}");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"2D DP: {twoDimensionalResult} [{(twoDimensionalPassed ? "PASS" : "FAIL")}]");
        Console.WriteLine($"1D DP: {optimizedResult} [{(optimizedPassed ? "PASS" : "FAIL")}]");
        Console.WriteLine();

        return (twoDimensionalPassed ? 1 : 0) + (optimizedPassed ? 1 : 0);
    }

    /// <summary>
    /// 將非空的正方形整數矩陣格式化為單行字串，方便將測試輸入與結果並列顯示。
    /// </summary>
    /// <param name="matrix">要格式化的非空正方形矩陣。</param>
    /// <returns>例如 <c>[[2, 1], [3, 4]]</c> 的可讀字串。</returns>
    private static string FormatMatrix(int[][] matrix)
    {
        return $"[{string.Join(", ", matrix.Select(row => $"[{string.Join(", ", row)}]"))}]";
    }

    /// <summary>
    /// 使用二維動態規劃計算最小下降路徑和。
    /// <c>dp[row][column]</c> 代表從第一列走到該位置的最小累積和，
    /// 每個狀態只從左上、正上與右上三個合法前驅中取最小值。
    /// 輸入必須是大小為 1 至 100 的非空正方形整數矩陣，每個元素介於 -100 與 100 之間。
    /// </summary>
    /// <param name="matrix">要計算下降路徑的正方形整數矩陣；方法不會修改其內容。</param>
    /// <returns>從第一列任意元素走到最後一列的最小路徑總和。</returns>
    public static int MinFallingPathSum(int[][] matrix)
    {
        int n = matrix.Length;
        int[][] dp = new int[n][];
        for (int row = 0; row < n; row++)
        {
            dp[row] = new int[n];
        }

        // 第一列可從任意位置起步，因此初始狀態就是矩陣本身的值。
        Array.Copy(matrix[0], 0, dp[0], 0, n);

        for (int row = 1; row < n; row++)
        {
            for (int column = 0; column < n; column++)
            {
                int minimumPreviousSum = dp[row - 1][column];

                // 最左與最右欄各少一個斜上方前驅，只比較存在的位置。
                if (column > 0)
                {
                    minimumPreviousSum = Math.Min(minimumPreviousSum, dp[row - 1][column - 1]);
                }

                if (column < n - 1)
                {
                    minimumPreviousSum = Math.Min(minimumPreviousSum, dp[row - 1][column + 1]);
                }

                dp[row][column] = minimumPreviousSum + matrix[row][column];
            }
        }

        return dp[n - 1].Min();
    }

    /// <summary>
    /// 使用一維滾動陣列計算最小下降路徑和。
    /// 由於當前列只依賴前一列，只保留 <c>previous</c> 與 <c>current</c> 兩個長度為 n 的陣列，
    /// 在每列計算完成後交換角色，將額外空間從 O(n²) 降低為 O(n)。
    /// 輸入必須是大小為 1 至 100 的非空正方形整數矩陣，每個元素介於 -100 與 100 之間。
    /// </summary>
    /// <param name="matrix">要計算下降路徑的正方形整數矩陣；方法不會修改其內容。</param>
    /// <returns>從第一列任意元素走到最後一列的最小路徑總和。</returns>
    public static int MinFallingPathSumOptimized(int[][] matrix)
    {
        int n = matrix.Length;
        int[] previous = new int[n];
        int[] current = new int[n];
        Array.Copy(matrix[0], previous, n);

        for (int row = 1; row < n; row++)
        {
            for (int column = 0; column < n; column++)
            {
                int minimumPreviousSum = previous[column];

                if (column > 0)
                {
                    minimumPreviousSum = Math.Min(minimumPreviousSum, previous[column - 1]);
                }

                if (column < n - 1)
                {
                    minimumPreviousSum = Math.Min(minimumPreviousSum, previous[column + 1]);
                }

                current[column] = minimumPreviousSum + matrix[row][column];
            }

            // current 成為下一輪的 previous，舊陣列則回收為寫入緩衝區。
            (previous, current) = (current, previous);
        }

        return previous.Min();
    }
}