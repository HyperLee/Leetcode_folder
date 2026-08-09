namespace leetcode_1380
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1380. Lucky Numbers in a Matrix
        /// https://leetcode.com/problems/lucky-numbers-in-a-matrix/description/
        ///
        /// Given an m x n matrix of distinct numbers, return all lucky numbers in the matrix in any order.
        ///
        /// A lucky number is an element of the matrix such that it is the minimum element in its row and maximum in its column.
        ///
        /// Example 1:
        /// Input: matrix = [[3,7,8],[9,11,13],[15,16,17]]
        /// Output: [15]
        /// Explanation: 15 is the only lucky number since it is the minimum in its row and the maximum in its column.
        ///
        /// Example 2:
        /// Input: matrix = [[1,10,4,2],[9,3,8,7],[15,16,17,12]]
        /// Output: [12]
        /// Explanation: 12 is the only lucky number since it is the minimum in its row and the maximum in its column.
        ///
        /// Example 3:
        /// Input: matrix = [[7,8],[1,2]]
        /// Output: [7]
        /// Explanation: 7 is the only lucky number since it is the minimum in its row and the maximum in its column.
        ///
        /// Constraints:
        /// - m == mat.length
        /// - n == mat[i].length
        /// - 1 &lt;= n, m &lt;= 50
        /// - 1 &lt;= matrix[i][j] &lt;= 10^5
        /// - All elements in the matrix are distinct.
        /// </para>
        /// <para>
        /// 1380. 矩陣中的幸運數
        /// https://leetcode.cn/problems/lucky-numbers-in-a-matrix/description/
        ///
        /// 給定一個由相異數字組成的 m x n 矩陣，以任意順序回傳矩陣中的所有幸運數。
        ///
        /// 幸運數是矩陣中的一個元素，它是所在列的最小元素，同時也是所在欄的最大元素。
        ///
        /// 範例 1：
        /// 輸入：matrix = [[3,7,8],[9,11,13],[15,16,17]]
        /// 輸出：[15]
        /// 解釋：15 是唯一的幸運數，因為它是所在列的最小值，也是所在欄的最大值。
        ///
        /// 範例 2：
        /// 輸入：matrix = [[1,10,4,2],[9,3,8,7],[15,16,17,12]]
        /// 輸出：[12]
        /// 解釋：12 是唯一的幸運數，因為它是所在列的最小值，也是所在欄的最大值。
        ///
        /// 範例 3：
        /// 輸入：matrix = [[7,8],[1,2]]
        /// 輸出：[7]
        /// 解釋：7 是唯一的幸運數，因為它是所在列的最小值，也是所在欄的最大值。
        ///
        /// 限制條件：
        /// - m == mat.length
        /// - n == mat[i].length
        /// - 1 &lt;= n, m &lt;= 50
        /// - 1 &lt;= matrix[i][j] &lt;= 10^5
        /// - 矩陣中的所有元素均不相同。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            (string Description, int[][] Matrix, int[] Expected)[] cases =
            {
                (
                    "官方範例一",
                    [
                        [3, 7, 8],
                        [9, 11, 13],
                        [15, 16, 17]
                    ],
                    [15]),
                (
                    "官方範例二",
                    [
                        [1, 10, 4, 2],
                        [9, 3, 8, 7],
                        [15, 16, 17, 12]
                    ],
                    [12]),
                (
                    "官方範例三",
                    [
                        [7, 8],
                        [1, 2]
                    ],
                    [7]),
                ("單一元素", [[42]], [42]),
                ("單列矩陣", [[9, 1, 5]], [1]),
                ("單欄矩陣", [[3], [9], [1]], [9]),
                (
                    "沒有幸運數的矩陣",
                    [
                        [10, 20],
                        [30, 5]
                    ],
                    []),
                ("50 x 50 上界矩陣（數值 1 到 2500）", CreateSequentialMatrix(50, 50), [2451])
            };

            int passedChecks = 0;
            const int checksPerCase = 3;

            for (int index = 0; index < cases.Length; index++)
            {
                (string description, int[][] matrix, int[] expected) = cases[index];
                passedChecks += RunCase(index + 1, description, matrix, expected);
            }

            int totalChecks = cases.Length * checksPerCase;
            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 執行同一筆合法矩陣測資的三種解法，比對各自的輸出與預期幸運數，並印出 PASS 或 FAIL。
        /// 輸入矩陣必須符合題目非空、矩形且元素互異的限制；回傳本案例通過的檢查數，範圍為 0 到 3。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="description">案例驗證目的的簡短說明。</param>
        /// <param name="matrix">符合題目限制的整數矩陣。</param>
        /// <param name="expected">預期回傳的幸運數集合。</param>
        /// <returns>三種解法中輸出符合預期的數量。</returns>
        private static int RunCase(
            int caseNumber,
            string description,
            int[][] matrix,
            int[] expected)
        {
            Console.WriteLine($"Case {caseNumber}: {description}");
            Console.WriteLine(matrix.Length * matrix[0].Length <= 20
                ? $"Matrix: {FormatMatrix(matrix)}"
                : $"Matrix: {matrix.Length} x {matrix[0].Length} generated matrix");
            Console.WriteLine($"Expected: {FormatValues(expected)}");

            int passedChecks = 0;
            passedChecks += RecordCheck("LuckyNumbers", LuckyNumbers(CloneMatrix(matrix)), expected);
            passedChecks += RecordCheck("LuckyNumbers2", LuckyNumbers2(CloneMatrix(matrix)), expected);
            passedChecks += RecordCheck("LuckyNumbers3", LuckyNumbers3(CloneMatrix(matrix)), expected);
            Console.WriteLine();

            return passedChecks;
        }

        /// <summary>
        /// 比對單一解法的實際輸出與預期結果，並輸出統一格式的驗證列。
        /// 輸入集合可為空；回傳 1 代表順序與內容皆相同，否則回傳 0。
        /// </summary>
        /// <param name="methodName">顯示於輸出中的解法名稱。</param>
        /// <param name="actual">解法實際回傳的整數集合。</param>
        /// <param name="expected">預期的整數集合。</param>
        /// <returns>驗證通過時回傳 1，否則回傳 0。</returns>
        private static int RecordCheck(string methodName, IList<int> actual, int[] expected)
        {
            bool passed = actual.SequenceEqual(expected);
            Console.WriteLine(
                $"{methodName} Actual: {FormatValues(actual)} => {(passed ? "PASS" : "FAIL")}");
            return passed ? 1 : 0;
        }

        /// <summary>
        /// 深層複製鋸齒矩陣的每一列，讓各解法取得獨立測試資料，避免未來的輸入異動互相污染。
        /// 輸入需為非 null 矩陣；回傳內容相同但列陣列彼此獨立的新矩陣。
        /// </summary>
        /// <param name="matrix">要複製的矩陣。</param>
        /// <returns>逐列複製完成的新矩陣。</returns>
        private static int[][] CloneMatrix(int[][] matrix)
        {
            return matrix.Select(row => row.ToArray()).ToArray();
        }

        /// <summary>
        /// 建立由 1 開始、依列遞增且元素互異的矩陣，用於穩定產生尺寸上界測資。
        /// 列數與欄數皆須為正整數；回傳指定尺寸的新矩陣。
        /// </summary>
        /// <param name="rowCount">矩陣列數。</param>
        /// <param name="columnCount">矩陣欄數。</param>
        /// <returns>依列填入連續正整數的矩陣。</returns>
        private static int[][] CreateSequentialMatrix(int rowCount, int columnCount)
        {
            int[][] matrix = new int[rowCount][];
            int value = 1;

            for (int row = 0; row < rowCount; row++)
            {
                matrix[row] = new int[columnCount];
                for (int column = 0; column < columnCount; column++)
                {
                    matrix[row][column] = value++;
                }
            }

            return matrix;
        }

        /// <summary>
        /// 將矩陣轉換為單行方括號格式，供小型案例輸出與 README 執行紀錄使用。
        /// 輸入需為非 null 矩陣；回傳例如 [[1, 2], [3, 4]] 的文字。
        /// </summary>
        /// <param name="matrix">要格式化的矩陣。</param>
        /// <returns>可閱讀的矩陣文字。</returns>
        private static string FormatMatrix(int[][] matrix)
        {
            return $"[{string.Join(", ", matrix.Select(FormatValues))}]";
        }

        /// <summary>
        /// 將整數序列轉換為方括號格式，空集合會表示為 []。
        /// 輸入需為非 null 序列；回傳適合 Expected 與 Actual 欄位的文字。
        /// </summary>
        /// <param name="values">要格式化的整數序列。</param>
        /// <returns>以逗號分隔的方括號文字。</returns>
        private static string FormatValues(IEnumerable<int> values)
        {
            return $"[{string.Join(", ", values)}]";
        }



        /// <summary>
        /// 逐一將每個元素視為候選，分別掃描同列與同欄，確認它同時是列最小值與欄最大值。
        /// 輸入必須是題目定義的非空矩形矩陣且元素互異；回傳所有幸運數，不修改輸入矩陣。
        /// </summary>
        /// <param name="matrix">列數與欄數皆介於 1 到 50、元素互異的整數矩陣。</param>
        /// <returns>同時為所在列最小值與所在欄最大值的元素集合；不存在時回傳空集合。</returns>
        public static IList<int> LuckyNumbers(int[][] matrix)
        {
            int rowCount = matrix.Length;
            int columnCount = matrix[0].Length;
            IList<int> result = new List<int>();

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    bool isRowMinimum = true;

                    for (int comparedColumn = 0; comparedColumn < columnCount; comparedColumn++)
                    {
                        if (matrix[row][comparedColumn] < matrix[row][column])
                        {
                            isRowMinimum = false;
                            break;
                        }
                    }

                    // 不是列最小值就不可能成為幸運數，直接略過較昂貴的欄掃描。
                    if (!isRowMinimum)
                    {
                        continue;
                    }

                    bool isColumnMaximum = true;
                    for (int comparedRow = 0; comparedRow < rowCount; comparedRow++)
                    {
                        if (matrix[comparedRow][column] > matrix[row][column])
                        {
                            isColumnMaximum = false;
                            break;
                        }
                    }

                    if (isColumnMaximum)
                    {
                        result.Add(matrix[row][column]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 先以一次掃描記錄每列最小值與每欄最大值，再找出同時出現在兩種極值位置的元素。
        /// 輸入必須是題目定義的非空矩形矩陣且元素互異；回傳所有幸運數，不修改輸入矩陣。
        /// </summary>
        /// <param name="matrix">列數與欄數皆介於 1 到 50、元素互異的整數矩陣。</param>
        /// <returns>同時為所在列最小值與所在欄最大值的元素集合；不存在時回傳空集合。</returns>
        public static IList<int> LuckyNumbers2(int[][] matrix)
        {
            int rowCount = matrix.Length;
            int columnCount = matrix[0].Length;
            int[] rowMinimums = new int[rowCount];
            int[] columnMaximums = new int[columnCount];
            Array.Fill(rowMinimums, int.MaxValue);
            Array.Fill(columnMaximums, int.MinValue);

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    int value = matrix[row][column];
                    rowMinimums[row] = Math.Min(rowMinimums[row], value);
                    columnMaximums[column] = Math.Max(columnMaximums[column], value);
                }
            }

            IList<int> result = new List<int>();
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    int value = matrix[row][column];
                    if (value == rowMinimums[row] && value == columnMaximums[column])
                    {
                        result.Add(value);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 利用鞍點的 maximin/minimax 性質，比較「所有列最小值中的最大值」與「所有欄最大值中的最小值」。
        /// 輸入必須是題目定義的非空矩形矩陣且元素互異；兩個極值相等時回傳該幸運數，否則回傳空集合，且不修改輸入。
        /// </summary>
        /// <param name="matrix">列數與欄數皆介於 1 到 50、元素互異的整數矩陣。</param>
        /// <returns>唯一的幸運數；不存在時回傳空集合。</returns>
        public static IList<int> LuckyNumbers3(int[][] matrix)
        {
            int rowCount = matrix.Length;
            int columnCount = matrix[0].Length;
            int maximumOfRowMinimums = int.MinValue;

            for (int row = 0; row < rowCount; row++)
            {
                int rowMinimum = int.MaxValue;
                for (int column = 0; column < columnCount; column++)
                {
                    rowMinimum = Math.Min(rowMinimum, matrix[row][column]);
                }

                maximumOfRowMinimums = Math.Max(maximumOfRowMinimums, rowMinimum);
            }

            int minimumOfColumnMaximums = int.MaxValue;
            for (int column = 0; column < columnCount; column++)
            {
                int columnMaximum = int.MinValue;
                for (int row = 0; row < rowCount; row++)
                {
                    columnMaximum = Math.Max(columnMaximum, matrix[row][column]);
                }

                minimumOfColumnMaximums = Math.Min(minimumOfColumnMaximums, columnMaximum);
            }

            IList<int> result = new List<int>();

            // 一般矩陣必有 max(row minima) <= min(column maxima)；相等時才存在鞍點。
            if (maximumOfRowMinimums == minimumOfColumnMaximums)
            {
                result.Add(maximumOfRowMinimums);
            }

            return result;
        }
    }
}