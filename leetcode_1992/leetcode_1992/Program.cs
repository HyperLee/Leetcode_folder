namespace leetcode_1992
{
    internal class Program
    {
        /// <summary>
        /// 1992. Find All Groups of Farmland
        /// https://leetcode.com/problems/find-all-groups-of-farmland/description/?envType=daily-question&envId=2024-04-20
        /// 1992. 找到所有的农场组
        /// https://leetcode.cn/problems/find-all-groups-of-farmland/description/
        /// 
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
        /// 不規則陣列
        /// 宣告
        /// 取長度
        /// 以及輸出 方式
        /// </summary>
        /// <remarks>
        /// 建立六組可重複執行的測試資料，比較左上角掃描法與 DFS 解法的結果。
        /// 所有輸入皆符合題目對二元矩陣與矩形農地的限制；若任一檢查失敗，程式會設定非零結束碼。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        static void Main(string[] args)
        {
            int passedChecks = 0;
            int totalChecks = 0;

            passedChecks += RunCase(
                "官方範例：單格與 2x2 農地",
                new int[][]
                {
                    new int[] { 1, 0, 0 },
                    new int[] { 0, 1, 1 },
                    new int[] { 0, 1, 1 }
                },
                new int[][]
                {
                    new int[] { 0, 0, 0, 0 },
                    new int[] { 1, 1, 2, 2 }
                });
            totalChecks += 2;

            passedChecks += RunCase(
                "完整 2x2 農地",
                new int[][]
                {
                    new int[] { 1, 1 },
                    new int[] { 1, 1 }
                },
                new int[][]
                {
                    new int[] { 0, 0, 1, 1 }
                });
            totalChecks += 2;

            passedChecks += RunCase(
                "只有森林",
                new int[][]
                {
                    new int[] { 0 }
                },
                Array.Empty<int[]>());
            totalChecks += 2;

            passedChecks += RunCase(
                "單列邊界農地",
                new int[][]
                {
                    new int[] { 0, 1, 1, 1 }
                },
                new int[][]
                {
                    new int[] { 0, 1, 0, 3 }
                });
            totalChecks += 2;

            passedChecks += RunCase(
                "單欄兩組農地",
                new int[][]
                {
                    new int[] { 1 },
                    new int[] { 1 },
                    new int[] { 0 },
                    new int[] { 1 }
                },
                new int[][]
                {
                    new int[] { 0, 0, 1, 0 },
                    new int[] { 3, 0, 3, 0 }
                });
            totalChecks += 2;

            passedChecks += RunCase(
                "多個不同尺寸的邊界農地",
                new int[][]
                {
                    new int[] { 1, 1, 0, 0, 1 },
                    new int[] { 1, 1, 0, 0, 1 },
                    new int[] { 0, 0, 0, 0, 0 },
                    new int[] { 1, 0, 1, 1, 1 }
                },
                new int[][]
                {
                    new int[] { 0, 0, 1, 1 },
                    new int[] { 0, 4, 1, 4 },
                    new int[] { 3, 0, 3, 0 },
                    new int[] { 3, 2, 3, 4 }
                });
            totalChecks += 2;

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 執行一組農地案例，分別驗證左上角掃描法與 DFS 解法，並輸出可讀的比較結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="land">符合題目限制的二元農地矩陣。</param>
        /// <param name="expected">預期的農地群組座標；群組順序不影響判定。</param>
        /// <returns>本案例通過的解法數量，範圍為 0 到 2。</returns>
        private static int RunCase(string name, int[][] land, int[][] expected)
        {
            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Input: {FormatMatrix(land)}");
            Console.WriteLine($"Expected: {FormatMatrix(expected)}");

            int passedChecks = 0;
            int[][] scanInput = land.Select(row => row.ToArray()).ToArray();
            int[][] scanResult = FindFarmland(scanInput);
            bool scanResultMatches = AreEquivalent(expected, scanResult);
            bool scanInputPreserved = FormatMatrix(scanInput) == FormatMatrix(land);
            bool scanPassed = scanResultMatches && scanInputPreserved;
            Console.WriteLine($"FindFarmland: {FormatMatrix(scanResult)} => {(scanPassed ? "PASS" : "FAIL")}" +
                $" (Result: {(scanResultMatches ? "PASS" : "FAIL")}, Input preserved: {(scanInputPreserved ? "PASS" : "FAIL")})");
            passedChecks += scanPassed ? 1 : 0;

            int[][] dfsInput = land.Select(row => row.ToArray()).ToArray();
            int[][] dfsResult = FindFarmlandDfs(dfsInput);
            bool dfsResultMatches = AreEquivalent(expected, dfsResult);
            bool dfsInputPreserved = FormatMatrix(dfsInput) == FormatMatrix(land);
            bool dfsPassed = dfsResultMatches && dfsInputPreserved;
            Console.WriteLine($"FindFarmlandDfs: {FormatMatrix(dfsResult)} => {(dfsPassed ? "PASS" : "FAIL")}" +
                $" (Result: {(dfsResultMatches ? "PASS" : "FAIL")}, Input preserved: {(dfsInputPreserved ? "PASS" : "FAIL")})");
            passedChecks += dfsPassed ? 1 : 0;
            Console.WriteLine();

            return passedChecks;
        }

        /// <summary>
        /// 比較兩組農地座標是否相同；先依四個座標排序，以忽略題目允許的回傳順序差異。
        /// </summary>
        /// <param name="expected">預期的農地群組座標。</param>
        /// <param name="actual">演算法實際回傳的農地群組座標。</param>
        /// <returns>兩者包含完全相同的四座標群組時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool AreEquivalent(int[][] expected, int[][] actual)
        {
            if (expected.Any(group => group.Length != 4) || actual.Any(group => group.Length != 4))
            {
                return false;
            }

            int[][] orderedExpected = expected
                .OrderBy(group => group[0])
                .ThenBy(group => group[1])
                .ThenBy(group => group[2])
                .ThenBy(group => group[3])
                .ToArray();
            int[][] orderedActual = actual
                .OrderBy(group => group[0])
                .ThenBy(group => group[1])
                .ThenBy(group => group[2])
                .ThenBy(group => group[3])
                .ToArray();

            return orderedExpected.Length == orderedActual.Length
                && orderedExpected.Zip(orderedActual).All(pair => pair.First.SequenceEqual(pair.Second));
        }

        /// <summary>
        /// 將不規則整數陣列格式化為穩定的單行矩陣文字，供主程式與 README 範例共用。
        /// </summary>
        /// <param name="matrix">要格式化的矩陣；可為空陣列。</param>
        /// <returns>例如 <c>[[1,0],[0,1]]</c> 的字串；空陣列回傳 <c>[]</c>。</returns>
        private static string FormatMatrix(int[][] matrix)
        {
            return $"[{string.Join(",", matrix.Select(row => $"[{string.Join(",", row)}]"))}]";
        }


        /// <summary>
        /// 找出二元矩陣中的所有矩形農地群組，利用上方與左方皆無相鄰農地的特徵辨識每組左上角，
        /// 再由該位置向下、向右延伸取得右下角。
        /// 輸入必須是至少一列、一欄且只包含 0 與 1 的矩形不規則陣列；各農地群組必須為矩形且彼此不四向相鄰。
        /// </summary>
        /// <param name="land">0 代表森林、1 代表農地的二元矩陣；方法不會修改矩陣內容。</param>
        /// <returns>每一列皆為 <c>[左上列, 左上欄, 右下列, 右下欄]</c> 的農地群組座標；沒有農地時回傳空陣列。</returns>
        /// <remarks>
        /// 時間複雜度為 O(mn)，其中 m、n 分別為列數與欄數；回傳結果以外的額外空間複雜度為 O(1)。
        /// </remarks>
        public static int[][] FindFarmland(int[][] land)
        {
            IList<int[]> farmlandGroups = new List<int[]>();
            int rowCount = land.Length;
            int columnCount = land[0].Length;

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    // 只有農地群組左上角的上方與左方都不是農地，因此其餘位置可直接略過。
                    if (land[row][column] == 0
                        || (row > 0 && land[row - 1][column] == 1)
                        || (column > 0 && land[row][column - 1] == 1))
                    {
                        continue;
                    }

                    int bottomRow = row;
                    int rightColumn = column;

                    while (bottomRow + 1 < rowCount && land[bottomRow + 1][rightColumn] == 1)
                    {
                        bottomRow++;
                    }

                    // 題目保證群組為完整矩形，因此從左下角向右即可定位右下角。
                    while (rightColumn + 1 < columnCount && land[bottomRow][rightColumn + 1] == 1)
                    {
                        rightColumn++;
                    }

                    farmlandGroups.Add(new int[] { row, column, bottomRow, rightColumn });
                }
            }

            return farmlandGroups.ToArray();
        }

        /// <summary>
        /// 找出二元矩陣中的所有矩形農地群組，使用顯式堆疊進行四方向深度優先搜尋。
        /// 每次走訪一個尚未拜訪的農地群組，持續更新最小與最大列、欄座標，據此取得左上角與右下角。
        /// 輸入必須是至少一列、一欄且只包含 0 與 1 的矩形不規則陣列；各農地群組必須為矩形且彼此不四向相鄰。
        /// </summary>
        /// <param name="land">0 代表森林、1 代表農地的二元矩陣；方法不會修改矩陣內容。</param>
        /// <returns>每一列皆為 <c>[左上列, 左上欄, 右下列, 右下欄]</c> 的農地群組座標；沒有農地時回傳空陣列。</returns>
        /// <remarks>
        /// 時間複雜度為 O(mn)，其中 m、n 分別為列數與欄數；不計回傳結果，visited 與堆疊的額外空間複雜度為 O(mn)。
        /// </remarks>
        public static int[][] FindFarmlandDfs(int[][] land)
        {
            int rowCount = land.Length;
            int columnCount = land[0].Length;
            bool[][] visited = new bool[rowCount][];

            for (int row = 0; row < rowCount; row++)
            {
                visited[row] = new bool[columnCount];
            }

            int[][] directions = new int[][]
            {
                new int[] { -1, 0 },
                new int[] { 1, 0 },
                new int[] { 0, -1 },
                new int[] { 0, 1 }
            };
            IList<int[]> farmlandGroups = new List<int[]>();

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    if (land[row][column] == 0 || visited[row][column])
                    {
                        continue;
                    }

                    int topRow = row;
                    int leftColumn = column;
                    int bottomRow = row;
                    int rightColumn = column;
                    Stack<(int Row, int Column)> cells = new Stack<(int Row, int Column)>();
                    cells.Push((row, column));
                    // 入棧時立即標記，避免相鄰農地重複加入堆疊。
                    visited[row][column] = true;

                    while (cells.Count > 0)
                    {
                        (int currentRow, int currentColumn) = cells.Pop();
                        topRow = Math.Min(topRow, currentRow);
                        leftColumn = Math.Min(leftColumn, currentColumn);
                        bottomRow = Math.Max(bottomRow, currentRow);
                        rightColumn = Math.Max(rightColumn, currentColumn);

                        foreach (int[] direction in directions)
                        {
                            int nextRow = currentRow + direction[0];
                            int nextColumn = currentColumn + direction[1];

                            if (nextRow < 0
                                || nextRow >= rowCount
                                || nextColumn < 0
                                || nextColumn >= columnCount
                                || land[nextRow][nextColumn] == 0
                                || visited[nextRow][nextColumn])
                            {
                                continue;
                            }

                            visited[nextRow][nextColumn] = true;
                            cells.Push((nextRow, nextColumn));
                        }
                    }

                    farmlandGroups.Add(new int[] { topRow, leftColumn, bottomRow, rightColumn });
                }
            }

            return farmlandGroups.ToArray();
        }

    }
}