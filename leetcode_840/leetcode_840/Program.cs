namespace leetcode_840
{
    internal class Program
    {
        private const string ClockwiseMagicRing = "4381672943816729";
        private const string CounterclockwiseMagicRing = "9276183492761834";

        /// <summary>
        /// 840. Magic Squares In Grid
        /// https://leetcode.com/problems/magic-squares-in-grid/description/
        /// <para>
        /// A 3 x 3 magic square is a 3 x 3 grid filled with distinct numbers from 1 to 9 such that every row, column, and both diagonals have the same sum.
        ///
        /// Given a row x col integer grid, return the number of 3 x 3 magic-square subgrids it contains.
        ///
        /// Note: Although a magic square can contain only numbers from 1 to 9, grid may contain numbers up to 15.
        ///
        /// Example 1:
        /// Image: https://assets.leetcode.com/uploads/2020/09/11/magic_main.jpg
        /// Input: grid = [[4,3,8,4],[9,5,1,9],[2,7,6,2]]
        /// Output: 1
        /// Explanation: The following subgrid is a 3 x 3 magic square: https://assets.leetcode.com/uploads/2020/09/11/magic_valid.jpg
        /// The other one is not: https://assets.leetcode.com/uploads/2020/09/11/magic_invalid.jpg
        /// In total, the given grid contains only one magic square.
        ///
        /// Example 2:
        /// Input: grid = [[8]]
        /// Output: 0
        ///
        /// Constraints:
        /// - row == grid.length
        /// - col == grid[i].length
        /// - 1 &lt;= row, col &lt;= 10
        /// - 0 &lt;= grid[i][j] &lt;= 15
        /// </para>
        /// <para>
        /// 840. 矩陣中的幻方
        /// https://leetcode.cn/problems/magic-squares-in-grid/description/
        ///
        /// 3 x 3 幻方是一個填入 1 到 9 各不相同數字的 3 x 3 網格，且每一列、每一欄與兩條對角線的總和都相同。
        ///
        /// 給定 row x col 的整數網格 grid，回傳其中 3 x 3 幻方子網格的數量。
        ///
        /// 注意：幻方只能包含 1 到 9，但 grid 中的數字最大可為 15。
        ///
        /// 範例 1：
        /// 圖片：https://assets.leetcode.com/uploads/2020/09/11/magic_main.jpg
        /// 輸入：grid = [[4,3,8,4],[9,5,1,9],[2,7,6,2]]
        /// 輸出：1
        /// 解釋：下列子網格是 3 x 3 幻方：https://assets.leetcode.com/uploads/2020/09/11/magic_valid.jpg
        /// 另一個則不是：https://assets.leetcode.com/uploads/2020/09/11/magic_invalid.jpg
        /// 因此給定網格中總共只有一個幻方。
        ///
        /// 範例 2：
        /// 輸入：grid = [[8]]
        /// 輸出：0
        ///
        /// 限制條件：
        /// - row == grid.length
        /// - col == grid[i].length
        /// - 1 &lt;= row, col &lt;= 10
        /// - 0 &lt;= grid[i][j] &lt;= 15
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Description, int[][] Grid, int Expected)[] samples =
            {
                (
                    "官方範例",
                    new int[][]
                    {
                        new int[] { 4, 3, 8, 4 },
                        new int[] { 9, 5, 1, 9 },
                        new int[] { 2, 7, 6, 2 }
                    },
                    1
                ),
                (
                    "尺寸不足 3 x 3",
                    new int[][]
                    {
                        new int[] { 8 }
                    },
                    0
                ),
                (
                    "標準洛書",
                    new int[][]
                    {
                        new int[] { 4, 3, 8 },
                        new int[] { 9, 5, 1 },
                        new int[] { 2, 7, 6 }
                    },
                    1
                ),
                (
                    "鏡射洛書",
                    new int[][]
                    {
                        new int[] { 8, 3, 4 },
                        new int[] { 1, 5, 9 },
                        new int[] { 6, 7, 2 }
                    },
                    1
                ),
                (
                    "重複數字",
                    new int[][]
                    {
                        new int[] { 5, 5, 5 },
                        new int[] { 5, 5, 5 },
                        new int[] { 5, 5, 5 }
                    },
                    0
                ),
                (
                    "包含超出 1 到 9 的數字",
                    new int[][]
                    {
                        new int[] { 4, 3, 8 },
                        new int[] { 9, 5, 1 },
                        new int[] { 2, 7, 15 }
                    },
                    0
                ),
                (
                    "同一網格包含兩個幻方",
                    new int[][]
                    {
                        new int[] { 4, 3, 8, 4, 3, 8 },
                        new int[] { 9, 5, 1, 9, 5, 1 },
                        new int[] { 2, 7, 6, 2, 7, 6 }
                    },
                    2
                )
            };

            int passedChecks = 0;

            for (int i = 0; i < samples.Length; i++)
            {
                (string description, int[][] grid, int expected) = samples[i];
                passedChecks += RunSample(i + 1, description, grid, expected);
            }

            int totalChecks = samples.Length * 2;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }


        /// <summary>
        /// 計算網格內所有 3 x 3 幻方子矩陣的數量。
        /// 逐一枚舉可能的中心點，並透過數字 1 到 9 的唯一性、
        /// 三列三行與兩條對角線總和皆為 15，完整驗證每個候選區域。
        /// </summary>
        /// <param name="grid">
        /// 符合題目限制的非空矩形整數網格；每列長度相同，元素介於 0 到 15。
        /// </param>
        /// <returns>網格內符合定義的 3 x 3 幻方子矩陣數量。</returns>
        public static int NumMagicSquaresInside(int[][] grid)
        {
            int result = 0;
            int rowCount = grid.Length;
            int columnCount = grid[0].Length;

            for (int row = 1; row < rowCount - 1; row++)
            {
                for (int column = 1; column < columnCount - 1; column++)
                {
                    // 3 x 3 候選區域可由中心點唯一決定，只需掃描不位於邊界的座標。
                    if (IsMagicSquare(grid, row, column))
                    {
                        result++;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 判斷指定中心點周圍的 3 x 3 區域是否為幻方。
        /// 先利用中心必為 5 快速排除，再確認 1 到 9 各出現一次，
        /// 最後驗證每一列、每一行與兩條對角線的總和皆為 15。
        /// </summary>
        /// <param name="grid">符合題目限制的非空矩形整數網格。</param>
        /// <param name="centerRow">候選 3 x 3 區域的中心列索引，必須避開上下邊界。</param>
        /// <param name="centerCol">候選 3 x 3 區域的中心欄索引，必須避開左右邊界。</param>
        /// <returns>指定區域符合 3 x 3 幻方定義時回傳 <see langword="true"/>。</returns>
        public static bool IsMagicSquare(int[][] grid, int centerRow, int centerCol)
        {
            if (grid[centerRow][centerCol] != 5)
            {
                // 四條穿過中心的線總和為 60；扣除外圈總和後可推得中心只能是 5。
                return false;
            }

            ISet<int> seenNumbers = new HashSet<int>();
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                {
                    int number = grid[centerRow + rowOffset][centerCol + columnOffset];
                    if (number < 1 || number > 9 || !seenNumbers.Add(number))
                    {
                        // 九格必須恰好使用 1 到 9；越界或重複都不可能是正常幻方。
                        return false;
                    }
                }
            }

            for (int offset = -1; offset <= 1; offset++)
            {
                int rowSum = 0;
                int columnSum = 0;

                for (int lineOffset = -1; lineOffset <= 1; lineOffset++)
                {
                    rowSum += grid[centerRow + offset][centerCol + lineOffset];
                    columnSum += grid[centerRow + lineOffset][centerCol + offset];
                }

                if (rowSum != 15 || columnSum != 15)
                {
                    // 1 到 9 總和為 45，平均分配到三列或三行後，每條線必須為 15。
                    return false;
                }
            }

            int mainDiagonalSum = 0;
            int antiDiagonalSum = 0;

            for (int offset = -1; offset <= 1; offset++)
            {
                mainDiagonalSum += grid[centerRow + offset][centerCol + offset];
                antiDiagonalSum += grid[centerRow + offset][centerCol - offset];
            }

            return mainDiagonalSum == 15 && antiDiagonalSum == 15;
        }

        /// <summary>
        /// 使用洛書外圈的循環排列，計算網格內所有 3 x 3 幻方子矩陣數量。
        /// 每個候選中心只需檢查中心值與八個外圈數字形成的正向或反向模式，
        /// 便可涵蓋同一個正常幻方的四種旋轉與四種鏡射。
        /// </summary>
        /// <param name="grid">
        /// 符合題目限制的非空矩形整數網格；每列長度相同，元素介於 0 到 15。
        /// </param>
        /// <returns>網格內符合外圈洛書模式的 3 x 3 幻方子矩陣數量。</returns>
        public static int NumMagicSquaresInside2(int[][] grid)
        {
            int result = 0;
            int rowCount = grid.Length;
            int columnCount = grid[0].Length;

            for (int row = 1; row < rowCount - 1; row++)
            {
                for (int column = 1; column < columnCount - 1; column++)
                {
                    if (IsMagicSquareByOuterRing(grid, row, column))
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 判斷指定中心點周圍的 3 x 3 區域是否符合洛書外圈模式。
        /// 正常 3 x 3 幻方中心必為 5；其餘八個數字依順時針排列時，
        /// 必為基準外圈的某個循環位移，或鏡射後反向外圈的循環位移。
        /// </summary>
        /// <param name="grid">符合題目限制的非空矩形整數網格。</param>
        /// <param name="centerRow">候選 3 x 3 區域的中心列索引，必須避開上下邊界。</param>
        /// <param name="centerCol">候選 3 x 3 區域的中心欄索引，必須避開左右邊界。</param>
        /// <returns>中心與外圈構成任一旋轉或鏡射洛書時回傳 <see langword="true"/>。</returns>
        public static bool IsMagicSquareByOuterRing(int[][] grid, int centerRow, int centerCol)
        {
            if (grid[centerRow][centerCol] != 5)
            {
                return false;
            }

            // 從左上角開始順時針繞行，刻意略過已獨立驗證的中心位置。
            string outerRing =
                $"{grid[centerRow - 1][centerCol - 1]}" +
                $"{grid[centerRow - 1][centerCol]}" +
                $"{grid[centerRow - 1][centerCol + 1]}" +
                $"{grid[centerRow][centerCol + 1]}" +
                $"{grid[centerRow + 1][centerCol + 1]}" +
                $"{grid[centerRow + 1][centerCol]}" +
                $"{grid[centerRow + 1][centerCol - 1]}" +
                $"{grid[centerRow][centerCol - 1]}";

            // 將八位模式重複一次，可讓任何旋轉都成為連續子字串；反向模式涵蓋鏡射。
            return ClockwiseMagicRing.Contains(outerRing, StringComparison.Ordinal)
                || CounterclockwiseMagicRing.Contains(outerRing, StringComparison.Ordinal);
        }

        /// <summary>
        /// 執行一組網格案例，分別呼叫完整驗證法與外圈序列法，
        /// 並輸出輸入內容、預期數量、實際結果及通過狀態。
        /// </summary>
        /// <param name="caseNumber">顯示在主控台上的案例編號。</param>
        /// <param name="description">說明案例所涵蓋情境的名稱。</param>
        /// <param name="grid">符合題目限制的矩形整數網格。</param>
        /// <param name="expected">人工推導的 3 x 3 幻方數量。</param>
        /// <returns>本案例兩種解法通過的驗證項數，範圍為 0 到 2。</returns>
        private static int RunSample(int caseNumber, string description, int[][] grid, int expected)
        {
            int result1 = NumMagicSquaresInside(grid);
            int result2 = NumMagicSquaresInside2(grid);
            bool passed1 = result1 == expected;
            bool passed2 = result2 == expected;

            Console.WriteLine($"案例 {caseNumber}：{description}");
            Console.WriteLine($"輸入：grid = {FormatGrid(grid)}");
            Console.WriteLine($"預期：{expected}");
            Console.WriteLine($"解法一（完整驗證）：{result1}（{(passed1 ? "PASS" : "FAIL")}）");
            Console.WriteLine($"解法二（外圈序列）：{result2}（{(passed2 ? "PASS" : "FAIL")}）");
            Console.WriteLine();

            return (passed1 ? 1 : 0) + (passed2 ? 1 : 0);
        }

        /// <summary>
        /// 將矩形整數網格格式化成穩定的巢狀方括號文字，方便主控台與 README 對照。
        /// </summary>
        /// <param name="grid">要格式化的矩形整數網格。</param>
        /// <returns>以逗號與空格分隔元素的單行網格文字。</returns>
        private static string FormatGrid(int[][] grid)
        {
            return $"[{string.Join(", ", grid.Select(row => $"[{string.Join(", ", row)}]"))}]";
        }
    }
}