namespace leetcode_542
{
    internal class Program
    {
        /// <summary>
        /// 542. 01 Matrix
        /// https://leetcode.com/problems/01-matrix/description/
        /// 
        /// 542. 01 矩阵
        /// https://leetcode.cn/problems/01-matrix/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TestCase[] testCases =
            [
                new(
                    "Official example 1",
                    [[0, 0, 0], [0, 1, 0], [0, 0, 0]],
                    [[0, 0, 0], [0, 1, 0], [0, 0, 0]]),
                new(
                    "Official example 2",
                    [[0, 0, 0], [0, 1, 0], [1, 1, 1]],
                    [[0, 0, 0], [0, 1, 0], [1, 2, 1]]),
                new("Single cell", [[0]], [[0]]),
                new("Single row / distant zero", [[0, 1, 1, 1]], [[0, 1, 2, 3]]),
                new("Single column / middle zero", [[1], [1], [0], [1]], [[2], [1], [0], [1]]),
                new(
                    "Rectangular matrix / multiple sources",
                    [[0, 1, 1, 1], [1, 1, 1, 0]],
                    [[0, 1, 2, 1], [1, 2, 1, 0]]),
                new(
                    "Bottom-right zero / long distances",
                    [[1, 1, 1], [1, 1, 1], [1, 1, 0]],
                    [[4, 3, 2], [3, 2, 1], [2, 1, 0]])
            ];

            int passed = 0;
            foreach (TestCase testCase in testCases)
            {
                int[][] breadthFirstInput = CloneMatrix(testCase.Input);
                int[][] dynamicProgrammingInput = CloneMatrix(testCase.Input);

                int[][] breadthFirstResult = UpdateMatrix(breadthFirstInput);
                int[][] dynamicProgrammingResult = UpdateMatrix2(dynamicProgrammingInput);
                bool breadthFirstReturnedInput = ReferenceEquals(breadthFirstInput, breadthFirstResult);
                bool dynamicProgrammingReturnedInput =
                    ReferenceEquals(dynamicProgrammingInput, dynamicProgrammingResult);
                bool isPassed = MatricesEqual(breadthFirstResult, testCase.Expected)
                    && MatricesEqual(dynamicProgrammingResult, testCase.Expected)
                    && breadthFirstReturnedInput
                    && dynamicProgrammingReturnedInput;

                if (isPassed)
                {
                    passed++;
                }

                Console.WriteLine($"Case: {testCase.Name}");
                Console.WriteLine($"Input: {FormatMatrix(testCase.Input)}");
                Console.WriteLine($"Expected: {FormatMatrix(testCase.Expected)}");
                Console.WriteLine($"UpdateMatrix (BFS): {FormatMatrix(breadthFirstResult)}");
                Console.WriteLine($"UpdateMatrix2 (DP): {FormatMatrix(dynamicProgrammingResult)}");
                Console.WriteLine($"BFS returned input reference: {breadthFirstReturnedInput}");
                Console.WriteLine($"DP returned input reference: {dynamicProgrammingReturnedInput}");
                Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
            if (passed != testCases.Length)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 建立矩陣的深層副本，讓各解法能在互不影響的輸入上原地計算。
        /// </summary>
        /// <param name="matrix">要複製的非空矩形矩陣。</param>
        /// <returns>各列皆為新陣列的矩陣副本。</returns>
        private static int[][] CloneMatrix(int[][] matrix)
        {
            int[][] clone = new int[matrix.Length][];
            for (int row = 0; row < matrix.Length; row++)
            {
                clone[row] = [.. matrix[row]];
            }

            return clone;
        }

        /// <summary>
        /// 逐列比較兩個矩陣的尺寸與元素，判斷內容是否完全相同。
        /// </summary>
        /// <param name="left">比較左側的矩陣。</param>
        /// <param name="right">比較右側的矩陣。</param>
        /// <returns>兩個矩陣的列數、各列長度與所有元素皆相同時回傳 <see langword="true"/>。</returns>
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
        /// 將矩陣轉成單行巢狀陣列文字，供 acceptance harness 與 README transcript 使用。
        /// </summary>
        /// <param name="matrix">要格式化的矩陣。</param>
        /// <returns>格式如 <c>[[0,1],[1,0]]</c> 的文字。</returns>
        private static string FormatMatrix(int[][] matrix)
        {
            return $"[{string.Join(",", matrix.Select(row => $"[{string.Join(",", row)}]"))}]";
        }

        /// <summary>
        /// 計算每個格子到最近 0 的曼哈頓距離。先把所有 0 同時放入佇列作為多源 BFS 起點，
        /// 再逐層擴散到尚未造訪的 1；第一次抵達某格時即得到最短距離。
        /// 適用於題目定義的非空矩形二元矩陣，矩陣中至少有一個 0。
        /// 本方法會直接改寫 <paramref name="mat"/>，時間複雜度為 O(mn)，最壞輔助空間為 O(mn)。
        /// </summary>
        /// <param name="mat">要原地轉換的有效二元矩陣。</param>
        /// <returns>與 <paramref name="mat"/> 相同的參考；每格已更新為到最近 0 的距離。</returns>
        public static int[][] UpdateMatrix(int[][] mat)
        {
            Queue<(int Row, int Column)> queue = new();
            int rows = mat.Length;
            int columns = mat[0].Length;

            // 所有 0 都是距離為 0 的來源；-1 同時表示原值為 1 且尚未造訪。
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (mat[row][column] == 0)
                    {
                        queue.Enqueue((row, column));
                    }
                    else
                    {
                        mat[row][column] = -1;
                    }
                }
            }

            (int Row, int Column)[] directions = [(-1, 0), (1, 0), (0, -1), (0, 1)];

            while (queue.Count > 0)
            {
                (int row, int column) = queue.Dequeue();
                foreach ((int rowOffset, int columnOffset) in directions)
                {
                    int nextRow = row + rowOffset;
                    int nextColumn = column + columnOffset;
                    if (nextRow >= 0
                        && nextRow < rows
                        && nextColumn >= 0
                        && nextColumn < columns
                        && mat[nextRow][nextColumn] == -1)
                    {
                        // BFS 首次抵達即是最短路徑；入隊前寫值可避免同一格重複入隊。
                        mat[nextRow][nextColumn] = mat[row][column] + 1;
                        queue.Enqueue((nextRow, nextColumn));
                    }
                }
            }

            return mat;
        }

        /// <summary>
        /// 計算每個格子到最近 0 的曼哈頓距離。先由左上往右下參考上方與左方距離，
        /// 再由右下往左上參考下方與右方距離，合併四個方向的最短候選值。
        /// 適用於題目定義的非空矩形二元矩陣，矩陣中至少有一個 0。
        /// 本方法會直接改寫 <paramref name="mat"/>，時間複雜度為 O(mn)，輔助空間為 O(1)。
        /// </summary>
        /// <param name="mat">要原地轉換的有效二元矩陣。</param>
        /// <returns>與 <paramref name="mat"/> 相同的參考；每格已更新為到最近 0 的距離。</returns>
        public static int[][] UpdateMatrix2(int[][] mat)
        {
            int rows = mat.Length;
            int columns = mat[0].Length;
            int unreachableDistance = rows + columns;

            // 最大有效距離是 rows + columns - 2，因此此哨兵一定不會被誤認為答案。
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (mat[row][column] == 1)
                    {
                        mat[row][column] = unreachableDistance;
                    }
                }
            }

            // 第一趟只使用已處理的上方與左方，取得來自左上方向的最短候選距離。
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (row > 0)
                    {
                        mat[row][column] = Math.Min(
                            mat[row][column],
                            mat[row - 1][column] + 1);
                    }

                    if (column > 0)
                    {
                        mat[row][column] = Math.Min(
                            mat[row][column],
                            mat[row][column - 1] + 1);
                    }
                }
            }

            // 反向掃描補上來自右方與下方的候選值，完成四個方向的最短距離比較。
            for (int row = rows - 1; row >= 0; row--)
            {
                for (int column = columns - 1; column >= 0; column--)
                {
                    if (row < rows - 1)
                    {
                        mat[row][column] = Math.Min(
                            mat[row][column],
                            mat[row + 1][column] + 1);
                    }

                    if (column < columns - 1)
                    {
                        mat[row][column] = Math.Min(
                            mat[row][column],
                            mat[row][column + 1] + 1);
                    }
                }
            }

            return mat;
        }

        private sealed record TestCase(string Name, int[][] Input, int[][] Expected);
    }
}