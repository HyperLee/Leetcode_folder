namespace leetode_1605
{
    internal class Program
    {
        /// <summary>
        /// 1605. Find Valid Matrix Given Row and Column Sums
        /// <para>
        /// You are given two arrays rowSum and colSum of non-negative integers where rowSum[i] is the sum of the elements in the ith row and colSum[j] is the sum of the elements of the jth column of a 2D matrix. In other words, you do not know the elements of the matrix, but you do know the sums of each row and column.
        ///
        /// Find any matrix of non-negative integers of size rowSum.length x colSum.length that satisfies the rowSum and colSum requirements.
        ///
        /// Return a 2D array representing any matrix that fulfills the requirements. It's guaranteed that at least one matrix that fulfills the requirements exists.
        /// </para>
        /// <para>
        /// 給定兩個由非負整數組成的陣列 rowSum 與 colSum，其中 rowSum[i] 是二維矩陣第 i 列元素的總和，而 colSum[j] 是第 j 欄元素的總和。換句話說，你不知道矩陣中的元素，但知道每一列與每一欄的總和。
        ///
        /// 請找出任意一個尺寸為 rowSum.length x colSum.length，且符合 rowSum 與 colSum 要求的非負整數矩陣。
        ///
        /// 回傳一個代表任意符合要求矩陣的二維陣列。題目保證至少存在一個符合要求的矩陣。
        /// </para>
        /// https://leetcode.com/problems/find-valid-matrix-given-row-and-column-sums/description/
        /// 1605. 给定行和列的和求可行矩阵
        /// https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool allPassed = RunSamples();
            Environment.ExitCode = allPassed ? 0 : 1;
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/solutions/2166773/mei-you-si-lu-yi-ge-dong-hua-miao-dong-f-eezj/
        /// https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/solutions/2165784/gei-ding-xing-he-lie-de-he-qiu-ke-xing-j-u8dj/
        /// https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/solutions/2167065/python3javacgotypescript-yi-ti-yi-jie-ta-qtx7/
        /// https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/solutions/2019569/by-stormsunshine-ktsx/
        ///
        /// 詳細說明請參考第一連結裡面
        /// 圖片說明
        /// 比較好理解
        ///
        /// 輸入是給列, 行總和
        /// 所以要生成陣列填入出各i, j 位置數值
        /// 個位置最大數值取 min(rowsum, columnsum)
        /// 比較合適
        /// 取出來之後要扣除 取出來的數值
        /// 持續更新
        ///
        /// 題目說明有: sum(rowSum) == sum(colSum)
        /// <para>方法會就地扣減 rowSum 與 colSum，回傳尺寸為 rowSum.Length x colSum.Length 的非負整數矩陣，並使矩陣的每列與每欄總和符合輸入。</para>
        /// </summary>
        /// <param name="rowSum">是二维矩阵中第 i 行元素的和</param>
        /// <param name="colSum">是第 j 列元素的和</param>
        /// <returns>符合指定列和與欄和的非負整數矩陣。</returns>
        public static int[][] RestoreMatrix(int[] rowSum, int[] colSum)
        {
            int m = rowSum.Length;
            int n = colSum.Length;
            int[][] mat = new int[m][];

            for (int i = 0; i < m; i++)
            {
                mat[i] = new int[n];
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // 取兩個剩餘總和的最小值，確保這格不會超過任一邊的需求。
                    int value = Math.Min(rowSum[i], colSum[j]);
                    mat[i][j] = value;

                    // 分配後至少有一個剩餘總和歸零，後續格子只需處理未完成的需求。
                    rowSum[i] -= value;
                    colSum[j] -= value;
                }
            }

            return mat;
        }

        /// <summary>
        /// 使用雙指標貪婪法建立符合列和與欄和的非負整數矩陣。
        /// 每次只處理目前尚未耗盡的列與欄，將兩者剩餘總和的最小值填入交叉格，並在需求歸零後移動對應指標。
        /// 此方法會就地扣減輸入陣列；題目保證兩邊總和相等時，回傳矩陣的每列與每欄總和會符合輸入。
        /// </summary>
        /// <param name="rowSum">矩陣每一列期望的總和；方法執行時會就地扣減此陣列。</param>
        /// <param name="colSum">矩陣每一欄期望的總和；方法執行時會就地扣減此陣列。</param>
        /// <returns>尺寸為 rowSum.Length x colSum.Length 的合法非負整數矩陣。</returns>
        public static int[][] RestoreMatrix2(int[] rowSum, int[] colSum)
        {
            int m = rowSum.Length;
            int n = colSum.Length;
            int[][] mat = new int[m][];

            for (int i = 0; i < m; i++)
            {
                mat[i] = new int[n];
            }

            int rowIndex = 0;
            int columnIndex = 0;

            while (rowIndex < m && columnIndex < n)
            {
                if (rowSum[rowIndex] == 0)
                {
                    rowIndex++;
                    continue;
                }

                if (colSum[columnIndex] == 0)
                {
                    columnIndex++;
                    continue;
                }

                // 指標只停在仍有需求的列欄，填入最小值即可讓至少一方完成。
                int value = Math.Min(rowSum[rowIndex], colSum[columnIndex]);
                mat[rowIndex][columnIndex] = value;
                rowSum[rowIndex] -= value;
                colSum[columnIndex] -= value;

                if (rowSum[rowIndex] == 0)
                {
                    rowIndex++;
                }

                if (colSum[columnIndex] == 0)
                {
                    columnIndex++;
                }
            }

            return mat;
        }

        /// <summary>
        /// 執行固定的矩陣案例，逐一比較兩種解法，並以 exit code 表示是否全部通過。
        /// </summary>
        /// <returns>所有案例與解法都通過時回傳 true，否則回傳 false。</returns>
        private static bool RunSamples()
        {
            const int sampleCount = 6;
            const int solutionCount = 2;
            int passedCount = 0;

            passedCount += RunSample("1. 官方範例一", new[] { 3, 8 }, new[] { 4, 7 });
            passedCount += RunSample("2. 官方範例二", new[] { 5, 7, 10 }, new[] { 8, 6, 8 });
            passedCount += RunSample("3. 單一儲存格", new[] { 7 }, new[] { 7 });
            passedCount += RunSample("4. 零總和邊界", new[] { 0, 5 }, new[] { 2, 3 });
            passedCount += RunSample("5. 多個零列與零欄", new[] { 4, 0, 3 }, new[] { 0, 2, 5 });
            passedCount += RunSample("6. 不同形狀與重複總和", new[] { 2, 2, 2 }, new[] { 3, 3 });

            int totalCount = sampleCount * solutionCount;
            Console.WriteLine();
            Console.WriteLine($"總結：{passedCount}/{totalCount} 項測試通過");
            return passedCount == totalCount;
        }

        /// <summary>
        /// 顯示一組列和與欄和資料，並分別執行兩種矩陣重建解法。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="rowSum">案例的列總和。</param>
        /// <param name="columnSum">案例的欄總和。</param>
        /// <returns>本案例通過的解法數量，範圍為 0 到 2。</returns>
        private static int RunSample(string name, int[] rowSum, int[] columnSum)
        {
            Console.WriteLine();
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"輸入：rowSum = {FormatArray(rowSum)}, colSum = {FormatArray(columnSum)}");

            int passedCount = 0;

            if (RunSolution("解法一：RestoreMatrix（逐格貪婪）", RestoreMatrix, rowSum, columnSum))
            {
                passedCount++;
            }

            if (RunSolution("解法二：RestoreMatrix2（雙指標貪婪）", RestoreMatrix2, rowSum, columnSum))
            {
                passedCount++;
            }

            return passedCount;
        }

        /// <summary>
        /// 使用獨立輸入複本執行指定解法，檢查矩陣合法性與剩餘總和是否耗盡。
        /// </summary>
        /// <param name="solutionName">解法顯示名稱。</param>
        /// <param name="solution">接受列和、欄和並回傳矩陣的解法。</param>
        /// <param name="expectedRowSum">未被修改的原始列總和。</param>
        /// <param name="expectedColumnSum">未被修改的原始欄總和。</param>
        /// <returns>矩陣與輸入扣減結果都符合預期時回傳 true。</returns>
        private static bool RunSolution(
            string solutionName,
            Func<int[], int[], int[][]> solution,
            int[] expectedRowSum,
            int[] expectedColumnSum)
        {
            int[] workingRowSum = (int[])expectedRowSum.Clone();
            int[] workingColumnSum = (int[])expectedColumnSum.Clone();
            int[][] matrix = solution(workingRowSum, workingColumnSum);

            bool inputsConsumed = true;

            foreach (int remainingSum in workingRowSum)
            {
                if (remainingSum != 0)
                {
                    inputsConsumed = false;
                    break;
                }
            }

            if (inputsConsumed)
            {
                foreach (int remainingSum in workingColumnSum)
                {
                    if (remainingSum != 0)
                    {
                        inputsConsumed = false;
                        break;
                    }
                }
            }

            bool matrixIsValid = IsValidMatrix(matrix, expectedRowSum, expectedColumnSum);
            bool passed = matrixIsValid && inputsConsumed;

            Console.WriteLine($"{solutionName}");
            Console.WriteLine($"Expected：非負 {expectedRowSum.Length} x {expectedColumnSum.Length} 矩陣，列和 = {FormatArray(expectedRowSum)}，欄和 = {FormatArray(expectedColumnSum)}");
            Console.WriteLine("Actual：");
            Console.WriteLine(matrix is null ? "  null" : FormatMatrix(matrix));
            Console.WriteLine($"Result：{(passed ? "PASS" : "FAIL")}");

            return passed;
        }

        /// <summary>
        /// 檢查矩陣尺寸、元素非負，以及每列和每欄是否符合指定總和。
        /// </summary>
        /// <param name="matrix">待驗證的矩陣。</param>
        /// <param name="expectedRowSum">期望的各列總和。</param>
        /// <param name="expectedColumnSum">期望的各欄總和。</param>
        /// <returns>矩陣符合所有條件時回傳 true。</returns>
        private static bool IsValidMatrix(int[][] matrix, int[] expectedRowSum, int[] expectedColumnSum)
        {
            if (matrix is null || matrix.Length != expectedRowSum.Length)
            {
                return false;
            }

            long[] actualColumnSum = new long[expectedColumnSum.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i] is null || matrix[i].Length != expectedColumnSum.Length)
                {
                    return false;
                }

                long actualRowSum = 0;

                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] < 0)
                    {
                        return false;
                    }

                    actualRowSum += matrix[i][j];
                    actualColumnSum[j] += matrix[i][j];
                }

                if (actualRowSum != expectedRowSum[i])
                {
                    return false;
                }
            }

            for (int j = 0; j < expectedColumnSum.Length; j++)
            {
                if (actualColumnSum[j] != expectedColumnSum[j])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 將整數陣列格式化為 README 與 console harness 使用的固定表示法。
        /// </summary>
        /// <param name="values">待格式化的整數陣列。</param>
        /// <returns>以方括號包住、逗號分隔的陣列文字。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 將鋸齒狀矩陣格式化為逐列顯示的固定文字。
        /// </summary>
        /// <param name="matrix">待格式化的矩陣。</param>
        /// <returns>每列一行、具縮排的矩陣文字。</returns>
        private static string FormatMatrix(int[][] matrix)
        {
            string[] lines = new string[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                lines[i] = matrix[i] is null ? "  null" : $"  {FormatArray(matrix[i])}";
            }

            return string.Join(Environment.NewLine, lines);
        }
    }
}
