namespace leetcode_054
{
    internal class Program
    {
        /// <summary>
        /// 54. Spiral Matrix
        /// https://leetcode.com/problems/spiral-matrix/description/
        /// <para>
        /// Given an m x n matrix, return all elements of the matrix in spiral order.
        ///
        /// Example 1:
        /// Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
        /// Output: [1,2,3,6,9,8,7,4,5]
        ///
        /// Example 2:
        /// Input: matrix = [[1,2,3,4],[5,6,7,8],[9,10,11,12]]
        /// Output: [1,2,3,4,8,12,11,10,9,5,6,7]
        ///
        /// Constraints:
        /// - m == matrix.length
        /// - n == matrix[i].length
        /// - 1 &lt;= m, n &lt;= 10
        /// - -100 &lt;= matrix[i][j] &lt;= 100
        /// </para>
        /// <para>
        /// 54. 螺旋矩陣
        /// https://leetcode.cn/problems/spiral-matrix/description/
        ///
        /// 給定一個 m x n 矩陣 matrix，請以螺旋順序回傳矩陣中的所有元素。
        ///
        /// 範例 1：
        /// 輸入：matrix = [[1,2,3],[4,5,6],[7,8,9]]
        /// 輸出：[1,2,3,6,9,8,7,4,5]
        ///
        /// 範例 2：
        /// 輸入：matrix = [[1,2,3,4],[5,6,7,8],[9,10,11,12]]
        /// 輸出：[1,2,3,4,8,12,11,10,9,5,6,7]
        ///
        /// 限制條件：
        /// - m == matrix.length
        /// - n == matrix[i].length
        /// - 1 &lt;= m, n &lt;= 10
        /// - -100 &lt;= matrix[i][j] &lt;= 100
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Name, int[][] Matrix, int[] Expected)[] testCases =
            {
                (
                    "Official 3x3",
                    new int[][]
                    {
                        new int[] { 1, 2, 3 },
                        new int[] { 4, 5, 6 },
                        new int[] { 7, 8, 9 }
                    },
                    new int[] { 1, 2, 3, 6, 9, 8, 7, 4, 5 }
                ),
                (
                    "Official 3x4",
                    new int[][]
                    {
                        new int[] { 1, 2, 3, 4 },
                        new int[] { 5, 6, 7, 8 },
                        new int[] { 9, 10, 11, 12 }
                    },
                    new int[] { 1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7 }
                ),
                (
                    "Single row",
                    new int[][]
                    {
                        new int[] { 1, 2, 3, 4 }
                    },
                    new int[] { 1, 2, 3, 4 }
                ),
                (
                    "Single column",
                    new int[][]
                    {
                        new int[] { 1 },
                        new int[] { 2 },
                        new int[] { 3 }
                    },
                    new int[] { 1, 2, 3 }
                ),
                (
                    "Empty matrix",
                    Array.Empty<int[]>(),
                    Array.Empty<int>()
                )
            };

            int passed = 0;
            foreach ((string name, int[][] matrix, int[] expected) in testCases)
            {
                if (RunTestCase(name, matrix, expected))
                {
                    passed++;
                }
            }

            Console.WriteLine($"Overall: {passed}/{testCases.Length} passed.");
        }

        /// <summary>
        /// 執行單一固定案例。輸入為案例名稱、整數矩陣與預期的螺旋順序；
        /// 呼叫解法前會深層複製矩陣，避免 <see cref="SpiralOrder(int[][])"/> 的原地標記改寫展示資料。
        /// 輸出案例的預期值、實際值與 PASS/FAIL，並回傳比對是否成功。
        /// </summary>
        /// <param name="name">顯示在主控台上的案例名稱。</param>
        /// <param name="matrix">要驗證的 jagged array 矩陣，可為空矩陣。</param>
        /// <param name="expected">預期取得的順時針螺旋序列。</param>
        /// <returns>實際結果與預期結果依序相同時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunTestCase(string name, int[][] matrix, int[] expected)
        {
            int[][] workingMatrix = matrix.Select(row => row.ToArray()).ToArray();
            IList<int> actual = SpiralOrder(workingMatrix);
            bool passed = actual.SequenceEqual(expected);

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: [{string.Join(", ", expected)}]");
            Console.WriteLine($"Actual:   [{string.Join(", ", actual)}]");
            Console.WriteLine($"Result:   {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed;
        }

        /// <summary>
        /// {0,1}: 向右移動（行不變，列+1）
        /// {1,0}: 向下移動（行+1，列不變）
        /// {0,-1}: 向左移動（行不變，列-1）
        /// {-1,0}: 向上移動（行-1，列不變）
        /// 想像成 [row, column]，第一個值調整列，第二個值調整欄。
        /// </summary>
        /// <value></value>
        private static readonly int[,] DIRS = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; // 右下左上

        /// <summary>
        /// 以順時針螺旋順序讀取整數矩陣。從左上角開始，利用方向陣列依序模擬右、下、左、上；
        /// 當下一步超出邊界或碰到已訪問位置時，方向索引前進一格完成右轉。
        /// 輸入須為每列長度相同的矩陣；空矩陣會回傳空集合。走訪過的位置會被改寫為
        /// <see cref="int.MaxValue"/>，因此呼叫完成後輸入矩陣不會保留原值。
        /// </summary>
        /// <param name="matrix">要遍歷的整數 jagged array 矩陣；各列長度必須一致，也可傳入空矩陣。</param>
        /// <returns>依順時針螺旋順序排列的所有矩陣元素；輸入為空矩陣時回傳空集合。</returns>
        public static IList<int> SpiralOrder(int[][] matrix)
        {
            int m = matrix.Length;
            if (m == 0)
            {
                return new List<int>();
            }

            int n = matrix[0].Length;
            List<int> res = new List<int>(m * n);

            int i = 0;
            int j = 0;
            int di = 0;

            for (int k = 0; k < m * n; k++)
            {
                res.Add(matrix[i][j]);
                matrix[i][j] = int.MaxValue;

                int nextRow = i + DIRS[di, 0];
                int nextCol = j + DIRS[di, 1];

                // 下一步若出界或已走過，就把方向由右、下、左、上循環右轉。
                if (nextRow < 0 || nextRow >= m || nextCol < 0 || nextCol >= n || matrix[nextRow][nextCol] == int.MaxValue)
                {
                    di = (di + 1) % 4;
                }

                i += DIRS[di, 0];
                j += DIRS[di, 1];
            }

            return res;
        }

    }
}