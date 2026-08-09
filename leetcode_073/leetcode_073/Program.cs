namespace leetcode_073;

class Program
{
    /// <summary>
    /// <para>
    /// 73. Set Matrix Zeroes
    /// https://leetcode.com/problems/set-matrix-zeroes/
    ///
    /// Given an m x n integer matrix matrix, if an element is 0, set its entire row and column to 0's.
    /// You must do it in place.
    ///
    /// Example 1:
    /// Input: matrix = [[1,1,1],[1,0,1],[1,1,1]]
    /// Output: [[1,0,1],[0,0,0],[1,0,1]]
    ///
    /// Example 2:
    /// Input: matrix = [[0,1,2,0],[3,4,5,2],[1,3,1,5]]
    /// Output: [[0,0,0,0],[0,4,5,0],[0,3,1,0]]
    ///
    /// Constraints:
    /// m == matrix.length
    /// n == matrix[0].length
    /// 1 &lt;= m, n &lt;= 200
    /// -2^31 &lt;= matrix[i][j] &lt;= 2^31 - 1
    ///
    /// Follow up:
    /// A straightforward solution using O(mn) space is probably a bad idea.
    /// A simple improvement uses O(m + n) space, but still not the best solution.
    /// Could you devise a constant space solution?
    /// </para>
    /// <para>
    /// 73. 矩陣設零
    /// https://leetcode.cn/problems/set-matrix-zeroes/
    ///
    /// 給定一個 m x n 的整數矩陣 matrix，如果某個元素為 0，請將它所在的整列與整欄都設為 0。
    /// 你必須原地完成這項操作。
    ///
    /// 範例 1：
    /// 輸入：matrix = [[1,1,1],[1,0,1],[1,1,1]]
    /// 輸出：[[1,0,1],[0,0,0],[1,0,1]]
    ///
    /// 範例 2：
    /// 輸入：matrix = [[0,1,2,0],[3,4,5,2],[1,3,1,5]]
    /// 輸出：[[0,0,0,0],[0,4,5,0],[0,3,1,0]]
    ///
    /// 限制條件：
    /// m == matrix.length
    /// n == matrix[0].length
    /// 1 &lt;= m, n &lt;= 200
    /// -2^31 &lt;= matrix[i][j] &lt;= 2^31 - 1
    ///
    /// 進階：
    /// 直接使用 O(mn) 空間的解法可能不是好主意。
    /// 一個簡單的改進會使用 O(m + n) 空間，但仍不是最佳解法。
    /// 你能設計出只使用常數空間的解法嗎？
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        int passed = 0;

        passed += RunTestCase(
            solution,
            "案例 1：內部零值",
            [
                [1, 1, 1],
                [1, 0, 1],
                [1, 1, 1]
            ],
            [
                [1, 0, 1],
                [0, 0, 0],
                [1, 0, 1]
            ]);

        passed += RunTestCase(
            solution,
            "案例 2：第一行、第一列零值",
            [
                [0, 1, 2, 0],
                [3, 4, 5, 2],
                [1, 3, 1, 5]
            ],
            [
                [0, 0, 0, 0],
                [0, 4, 5, 0],
                [0, 3, 1, 0]
            ]);

        passed += RunTestCase(
            solution,
            "案例 3：無零值",
            [
                [1, 2],
                [3, 4]
            ],
            [
                [1, 2],
                [3, 4]
            ]);

        passed += RunTestCase(
            solution,
            "案例 4：單一零值",
            [[0]],
            [[0]]);

        passed += RunTestCase(
            solution,
            "案例 5：單列含零",
            [[1, 0, 3]],
            [[0, 0, 0]]);

        passed += RunTestCase(
            solution,
            "案例 6：單欄含零",
            [
                [1],
                [0],
                [3]
            ],
            [
                [0],
                [0],
                [0]
            ]);

        Console.WriteLine($"{passed}/12 passed.");
    }

    /// <summary>
    /// 執行一組矩陣置零驗收案例。
    /// 兩種解法各自使用輸入矩陣的深拷貝，避免原地修改互相影響，
    /// 並將實際結果與預期矩陣逐列比較。
    /// 輸入與預期值必須是至少 1 x 1、每列長度相同的非空矩陣；
    /// 回傳本案例通過的解法數量，結果範圍為 0 到 2。
    /// </summary>
    /// <param name="solution">提供兩種矩陣置零解法的物件。</param>
    /// <param name="caseName">顯示於主控台的案例名稱。</param>
    /// <param name="input">符合題目限制、執行前不會被修改的輸入矩陣。</param>
    /// <param name="expected">兩種解法執行後都應得到的預期矩陣。</param>
    /// <returns>本案例通過的解法數量。</returns>
    private static int RunTestCase(
        Program solution,
        string caseName,
        int[][] input,
        int[][] expected)
    {
        Console.WriteLine(caseName);
        Console.WriteLine("Input:");
        PrintMatrix(input);
        Console.WriteLine("Expected:");
        PrintMatrix(expected);

        int passed = 0;

        int[][] setZeroesActual = CloneMatrix(input);
        solution.SetZeroes(setZeroesActual);
        Console.WriteLine("SetZeroes Actual:");
        PrintMatrix(setZeroesActual);
        bool setZeroesPassed = MatricesEqual(setZeroesActual, expected);
        Console.WriteLine($"SetZeroes: {(setZeroesPassed ? "PASS" : "FAIL")}");
        if (setZeroesPassed)
        {
            passed++;
        }

        int[][] setZeroes2Actual = CloneMatrix(input);
        solution.SetZeroes2(setZeroes2Actual);
        Console.WriteLine("SetZeroes2 Actual:");
        PrintMatrix(setZeroes2Actual);
        bool setZeroes2Passed = MatricesEqual(setZeroes2Actual, expected);
        Console.WriteLine($"SetZeroes2: {(setZeroes2Passed ? "PASS" : "FAIL")}");
        if (setZeroes2Passed)
        {
            passed++;
        }

        Console.WriteLine();
        return passed;
    }

    /// <summary>
    /// 建立矩陣的逐列深拷貝，讓不同解法能在相同的原始資料上獨立執行。
    /// 輸入必須是至少 1 x 1、每列皆非空的矩陣；
    /// 回傳內容相同但各列陣列皆為新實例的矩陣。
    /// </summary>
    /// <param name="matrix">要複製的非空矩陣。</param>
    /// <returns>與輸入內容相同且可獨立修改的矩陣。</returns>
    private static int[][] CloneMatrix(int[][] matrix)
    {
        return matrix.Select(row => (int[])row.Clone()).ToArray();
    }

    /// <summary>
    /// 比較兩個矩陣的列數、各列長度及所有元素是否完全相同。
    /// 輸入必須是每列皆非空的矩陣；
    /// 若維度與內容全部一致則回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。
    /// </summary>
    /// <param name="actual">演算法原地修改後的實際矩陣。</param>
    /// <param name="expected">案例定義的預期矩陣。</param>
    /// <returns>兩個矩陣是否具有相同維度與內容。</returns>
    private static bool MatricesEqual(int[][] actual, int[][] expected)
    {
        return actual.Length == expected.Length
            && actual
                .Zip(expected, (actualRow, expectedRow) => actualRow.SequenceEqual(expectedRow))
                .All(rowsEqual => rowsEqual);
    }

    /// <summary>
    /// 將矩陣逐列輸出到主控台，欄位之間以單一空白分隔。
    /// 輸入必須是至少 1 x 1、每列長度相同的非空矩陣；
    /// 方法不修改矩陣，輸出結果為便於人工核對的多行文字。
    /// </summary>
    /// <param name="matrix">要輸出的非空矩陣。</param>
    private static void PrintMatrix(int[][] matrix)
    {
        foreach (int[] row in matrix)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }

    /// <summary>
    /// 使用第一行與第一列作為標記區，以 O(1) 額外空間完成矩陣置零。
    /// 先保存第一行、第一列原本是否含零，再用外框元素記錄內部零值所影響的行列，
    /// 依標記清除內部後，最後處理第一行與第一列。
    /// 輸入必須是至少 1 x 1、每列長度相同的非空整數矩陣；
    /// 方法會直接修改輸入，使原始零值所在的整行與整列皆為零，且不回傳新矩陣。
    /// 時間複雜度為 O(mn)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="matrix">要原地置零的非空矩形整數矩陣。</param>
    public void SetZeroes(int[][] matrix)
    {
        int rows = matrix.Length;
        int cols = matrix[0].Length;

        // 標記第一行和第一列是否原本包含 0
        bool firstRowZero = false;
        bool firstColZero = false;

        // 檢查第一行是否有 0
        for (int j = 0; j < cols; j++)
        {
            if (matrix[0][j] == 0)
            {
                firstRowZero = true;
                break;
            }
        }

        // 檢查第一列是否有 0
        for (int i = 0; i < rows; i++)
        {
            if (matrix[i][0] == 0)
            {
                firstColZero = true;
                break;
            }
        }

        // 第一行與第一列同時作為標記區，因此內部掃描從索引 1 開始。
        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < cols; j++)
            {
                if (matrix[i][j] == 0)
                {
                    matrix[i][0] = 0; // 標記該行需要設為 0
                    matrix[0][j] = 0; // 標記該列需要設為 0
                }
            }
        }

        // 延後到標記完成後才清零，避免新寫入的 0 被誤判為原始零值。
        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < cols; j++)
            {
                if (matrix[i][0] == 0 || matrix[0][j] == 0)
                {
                    matrix[i][j] = 0;
                }
            }
        }

        // 外框本身兼作標記，必須依先前保存的原始狀態最後處理。
        if (firstRowZero)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[0][j] = 0;
            }
        }

        if (firstColZero)
        {
            for (int i = 0; i < rows; i++)
            {
                matrix[i][0] = 0;
            }
        }
    }

    /// <summary>
    /// 使用兩個布林陣列記錄需要置零的行與列，以直觀的兩次掃描完成矩陣置零。
    /// 第一次掃描只收集原始零值的位置資訊，第二次掃描再依標記修改元素，
    /// 因此不會把演算法新寫入的零誤認為原始條件。
    /// 輸入必須是至少 1 x 1、每列長度相同的非空整數矩陣；
    /// 方法會直接修改輸入，使原始零值所在的整行與整列皆為零，且不回傳新矩陣。
    /// 時間複雜度為 O(mn)，額外空間複雜度為 O(m+n)。
    /// </summary>
    /// <param name="matrix">要原地置零的非空矩形整數矩陣。</param>
    public void SetZeroes2(int[][] matrix)
    {
        int m = matrix.Length;
        int n = matrix[0].Length;
        bool[] row = new bool[m]; // 記錄需要設為 0 的行
        bool[] col = new bool[n]; // 記錄需要設為 0 的列

        // 第一次遍歷：標記包含 0 的行和列
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] == 0)
                {
                    row[i] = true; // 標記該行需要設為 0
                    col[j] = true; // 標記該列需要設為 0
                }
            }
        }

        // 第二次遍歷：根據標記設置 0
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (row[i] || col[j])
                {
                    matrix[i][j] = 0;
                }
            }
        }
    }
}