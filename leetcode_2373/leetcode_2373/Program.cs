namespace leetcode_2373
{
    internal class Program
    {
        /// <summary>
        /// 2373. Largest Local Values in a Matrix
        /// https://leetcode.com/problems/largest-local-values-in-a-matrix/description/?envType=daily-question&envId=2024-05-12
        /// 2373. 矩阵中的局部最大值
        /// https://leetcode.cn/problems/largest-local-values-in-a-matrix/description/
        /// 
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
        /// 不規則陣列
        /// </summary>
        /// <remarks>
        /// 執行五組固定案例，比較兩種解法的結果，並確認兩者都不會修改輸入矩陣。
        /// 每組案例包含四項檢查；若任一檢查失敗，程式會以非零結束碼結束。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            (string Name, int[][] Input, int[][] Expected)[] cases =
            [
                (
                    "官方 4x4 範例",
                    [
                        [9, 9, 8, 1],
                        [5, 6, 2, 6],
                        [8, 6, 2, 4],
                        [6, 2, 2, 2]
                    ],
                    [
                        [9, 9],
                        [8, 6]
                    ]
                ),
                (
                    "3x3 最小尺寸",
                    [
                        [1, 2, 3],
                        [4, 9, 6],
                        [7, 8, 5]
                    ],
                    [
                        [9]
                    ]
                ),
                (
                    "全重複值",
                    [
                        [5, 5, 5, 5],
                        [5, 5, 5, 5],
                        [5, 5, 5, 5],
                        [5, 5, 5, 5]
                    ],
                    [
                        [5, 5],
                        [5, 5]
                    ]
                ),
                (
                    "最大值位於視窗邊界",
                    [
                        [9, 1, 1, 8],
                        [1, 1, 1, 1],
                        [1, 1, 1, 1],
                        [7, 1, 1, 6]
                    ],
                    [
                        [9, 8],
                        [7, 6]
                    ]
                ),
                (
                    "遞增 5x5 多視窗",
                    [
                        [1, 2, 3, 4, 5],
                        [6, 7, 8, 9, 10],
                        [11, 12, 13, 14, 15],
                        [16, 17, 18, 19, 20],
                        [21, 22, 23, 24, 25]
                    ],
                    [
                        [13, 14, 15],
                        [18, 19, 20],
                        [23, 24, 25]
                    ]
                )
            ];

            int passedChecks = 0;
            int totalChecks = 0;

            foreach ((string name, int[][] input, int[][] expected) in cases)
            {
                (int passed, int total) = RunCase(name, input, expected);
                passedChecks += passed;
                totalChecks += total;
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// 執行單一測試案例，分別驗證兩種解法的輸出結果與輸入不變契約。
        /// 每種解法都取得獨立的矩陣副本，輸出四項 Expected、Actual 與 PASS/FAIL 結果。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="input">符合題目限制的正方形輸入矩陣。</param>
        /// <param name="expected">手動推導的預期局部最大值矩陣。</param>
        /// <returns>此案例通過的檢查數，以及固定的總檢查數四項。</returns>
        private static (int Passed, int Total) RunCase(string name, int[][] input, int[][] expected)
        {
            int[][] baselineInput = CloneMatrix(input);
            int[][] optimizedInput = CloneMatrix(input);

            int[][] baselineActual = LargestLocal(baselineInput);
            int[][] optimizedActual = LargestLocal2(optimizedInput);

            bool baselineResultPassed = MatricesEqual(expected, baselineActual);
            bool optimizedResultPassed = MatricesEqual(expected, optimizedActual);
            bool baselineInputPassed = MatricesEqual(input, baselineInput);
            bool optimizedInputPassed = MatricesEqual(input, optimizedInput);

            Console.WriteLine($"Case: {name}");
            PrintResult("LargestLocal", expected, baselineActual, baselineResultPassed);
            PrintResult("LargestLocal2", expected, optimizedActual, optimizedResultPassed);
            Console.WriteLine($"  LargestLocal input unchanged: {(baselineInputPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"  LargestLocal2 input unchanged: {(optimizedInputPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            int passed = Convert.ToInt32(baselineResultPassed)
                + Convert.ToInt32(optimizedResultPassed)
                + Convert.ToInt32(baselineInputPassed)
                + Convert.ToInt32(optimizedInputPassed);

            return (passed, 4);
        }

        /// <summary>
        /// 以固定格式輸出單一解法的預期矩陣、實際矩陣與檢查結果。
        /// </summary>
        /// <param name="methodName">接受檢查的解法名稱。</param>
        /// <param name="expected">手動推導的預期矩陣。</param>
        /// <param name="actual">解法實際回傳的矩陣。</param>
        /// <param name="passed">預期與實際矩陣是否完全相等。</param>
        private static void PrintResult(string methodName, int[][] expected, int[][] actual, bool passed)
        {
            Console.WriteLine($"  {methodName}");
            Console.WriteLine($"    Expected: {FormatMatrix(expected)}");
            Console.WriteLine($"    Actual:   {FormatMatrix(actual)}");
            Console.WriteLine($"    Result:   {(passed ? "PASS" : "FAIL")}");
        }

        /// <summary>
        /// 建立不規則矩陣的深層副本，讓每種解法使用彼此獨立的測試資料。
        /// </summary>
        /// <param name="matrix">要複製的矩陣。</param>
        /// <returns>每一列皆為新陣列的矩陣副本。</returns>
        private static int[][] CloneMatrix(int[][] matrix)
        {
            int[][] clone = new int[matrix.Length][];

            for (int row = 0; row < matrix.Length; row++)
            {
                clone[row] = (int[])matrix[row].Clone();
            }

            return clone;
        }

        /// <summary>
        /// 逐列比較兩個不規則矩陣的尺寸與元素，用於驗證輸出及輸入不變契約。
        /// </summary>
        /// <param name="left">比較左側矩陣。</param>
        /// <param name="right">比較右側矩陣。</param>
        /// <returns>尺寸與所有元素皆相同時回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        private static bool MatricesEqual(int[][] left, int[][] right)
        {
            if (left.Length != right.Length)
            {
                return false;
            }

            for (int row = 0; row < left.Length; row++)
            {
                if (!left[row].SequenceEqual(right[row]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 將不規則矩陣格式化成單行字串，提供 Expected 與 Actual 的穩定主控台輸出。
        /// </summary>
        /// <param name="matrix">要格式化的矩陣。</param>
        /// <returns>格式如 <c>[[9, 9], [8, 6]]</c> 的字串。</returns>
        private static string FormatMatrix(int[][] matrix)
        {
            return $"[{string.Join(", ", matrix.Select(static row => $"[{string.Join(", ", row)}]"))}]";
        }

        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/largest-local-values-in-a-matrix/solutions/2138032/ju-zhen-zhong-de-ju-bu-zui-da-zhi-by-lee-o703/
        /// https://leetcode.cn/problems/largest-local-values-in-a-matrix/solutions/1746845/yuan-di-xiu-gai-by-endlesscheng-m1k3/
        /// https://leetcode.cn/problems/largest-local-values-in-a-matrix/solutions/2576863/2373-ju-zhen-zhong-de-ju-bu-zui-da-zhi-b-kkc6/
        /// 
        /// 輸入的矩陣grid大小為 n * n 
        /// 要輸出 n - 2 * n - 2 範圍大小的矩陣res.
        /// 然後根據題目意思要在原先輸入的矩陣內
        /// 用輸入的 grid 範圍為 3 * 3 的大小去找出這範圍內最大數值
        /// 塞入要輸出的 n - 2 * n - 2 的矩陣內
        /// 
        /// </summary>
        /// <remarks>
        /// 依序枚舉每個輸出位置對應的 3x3 視窗並掃描九個元素。
        /// 時間複雜度為 O(n²)，輸出矩陣以外的額外空間為 O(1)，且不修改輸入矩陣。
        /// </remarks>
        /// <param name="grid">大小為 n x n 的正方形矩陣；題目保證 3 &lt;= n &lt;= 100，且元素介於 1 到 100。</param>
        /// <returns>大小為 (n - 2) x (n - 2) 的矩陣，每格為對應 3x3 視窗的最大值。</returns>
        public static int[][] LargestLocal(int[][] grid)
        {
            int n = grid.Length;
            int[][] result = new int[n - 2][];

            for (int row = 0; row < n - 2; row++)
            {
                result[row] = new int[n - 2];

                for (int column = 0; column < n - 2; column++)
                {
                    // 輸出座標 (row, column) 對應輸入中以同座標為左上角的 3x3 視窗。
                    for (int windowRow = row; windowRow < row + 3; windowRow++)
                    {
                        for (int windowColumn = column; windowColumn < column + 3; windowColumn++)
                        {
                            result[row][column] = Math.Max(result[row][column], grid[windowRow][windowColumn]);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 以兩階段滑動視窗求出每個 3x3 區域的最大值：先計算每列寬度為 3 的最大值，
        /// 再對橫向結果的每一欄計算高度為 3 的最大值。
        /// </summary>
        /// <remarks>
        /// 每次滑動視窗都使用單調遞減佇列，讓每個元素最多進出佇列一次。
        /// 時間複雜度為 O(n²)，包含中間矩陣的額外空間為 O(n²)，且不修改輸入矩陣。
        /// </remarks>
        /// <param name="grid">大小為 n x n 的正方形矩陣；題目保證 3 &lt;= n &lt;= 100，且元素介於 1 到 100。</param>
        /// <returns>大小為 (n - 2) x (n - 2) 的矩陣，每格為對應 3x3 視窗的最大值。</returns>
        public static int[][] LargestLocal2(int[][] grid)
        {
            const int windowSize = 3;
            int n = grid.Length;
            int resultSize = n - windowSize + 1;
            int[][] horizontalMaximums = new int[n][];

            for (int row = 0; row < n; row++)
            {
                horizontalMaximums[row] = GetSlidingWindowMaximums(grid[row], windowSize);
            }

            int[][] result = new int[resultSize][];
            for (int row = 0; row < resultSize; row++)
            {
                result[row] = new int[resultSize];
            }

            for (int column = 0; column < resultSize; column++)
            {
                int[] verticalValues = new int[n];
                for (int row = 0; row < n; row++)
                {
                    verticalValues[row] = horizontalMaximums[row][column];
                }

                int[] verticalMaximums = GetSlidingWindowMaximums(verticalValues, windowSize);
                for (int row = 0; row < resultSize; row++)
                {
                    // 橫向與縱向兩次寬度 3 的最大值，合併後就是原矩陣對應 3x3 視窗的最大值。
                    result[row][column] = verticalMaximums[row];
                }
            }

            return result;
        }

        /// <summary>
        /// 使用單調遞減索引佇列，計算一維陣列中所有固定寬度滑動視窗的最大值。
        /// 輸入長度必須大於或等於視窗寬度，輸出長度為輸入長度減去視窗寬度再加一。
        /// </summary>
        /// <param name="values">要掃描的一維數值陣列。</param>
        /// <param name="windowSize">固定滑動視窗寬度。</param>
        /// <returns>每個滑動視窗的最大值陣列。</returns>
        private static int[] GetSlidingWindowMaximums(int[] values, int windowSize)
        {
            int[] maximums = new int[values.Length - windowSize + 1];
            int[] deque = new int[values.Length];
            int head = 0;
            int tail = 0;

            for (int index = 0; index < values.Length; index++)
            {
                // 先移除已經離開目前視窗的索引，佇列首端才會永遠代表有效最大值。
                if (head < tail && deque[head] <= index - windowSize)
                {
                    head++;
                }

                // 移除尾端不大於目前值的索引；它們不可能成為後續視窗的最大值。
                while (head < tail && values[deque[tail - 1]] <= values[index])
                {
                    tail--;
                }

                deque[tail] = index;
                tail++;

                if (index >= windowSize - 1)
                {
                    maximums[index - windowSize + 1] = values[deque[head]];
                }
            }

            return maximums;
        }
    }
}