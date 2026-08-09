namespace leetcode_3148
{
    internal class Program
    {
        /// <summary>
        /// 3148. Maximum Difference Score in a Grid
        /// https://leetcode.com/problems/maximum-difference-score-in-a-grid/description/
        /// 
        /// 3148. 矩阵中的最大得分
        /// https://leetcode.cn/problems/maximum-difference-score-in-a-grid/description/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <remarks>
        /// 以固定案例同時驗證二維與一維動態規劃解法，並輸出預期值、實際值與通過狀態。
        /// 任一檢查失敗時，程序會設定非零結束碼，方便在終端機或 CI 環境中判斷結果。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用此參數。</param>
        static void Main(string[] args)
        {
            (string Name, int[][] Grid, int Expected)[] testCases =
            {
                (
                    "Official example 1",
                    [
                        [9, 5, 7, 3],
                        [8, 9, 6, 1],
                        [6, 7, 14, 3],
                        [2, 5, 3, 1]
                    ],
                    9
                ),
                (
                    "Official example 2 - decreasing grid",
                    [
                        [4, 3, 2],
                        [3, 2, 1]
                    ],
                    -1
                ),
                (
                    "Minimum 2 x 2 increasing grid",
                    [
                        [1, 2],
                        [3, 4]
                    ],
                    3
                ),
                (
                    "Duplicate values",
                    [
                        [5, 5],
                        [5, 5]
                    ],
                    0
                ),
                (
                    "Best endpoint is not bottom-right",
                    [
                        [1, 10, 2],
                        [3, 4, 5]
                    ],
                    9
                )
            };

            int passedChecks = 0;
            int totalChecks = testCases.Length * 2;

            foreach ((string name, int[][] grid, int expected) in testCases)
            {
                Console.WriteLine($"Case: {name}");
                Console.WriteLine($"Input: {FormatGrid(grid)}");
                Console.WriteLine($"Expected: {expected}");
                passedChecks += RunSolution(nameof(MaxScore), MaxScore, grid, expected);
                passedChecks += RunSolution(nameof(MaxScore2), MaxScore2, grid, expected);
                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed");
            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }


        /// <summary>
        /// 執行指定解法並比對預期答案；每次都建立獨立矩陣，避免解法之間共享可變資料。
        /// 輸入為解法名稱、待執行方法、符合題目限制的矩陣與手動推導的預期值，輸出為通過檢查數（0 或 1）。
        /// </summary>
        /// <param name="solutionName">顯示於測試結果的解法名稱。</param>
        /// <param name="solution">接受矩陣並回傳最大分數的解法。</param>
        /// <param name="sourceGrid">測試用原始矩陣。</param>
        /// <param name="expected">手動推導的預期最大分數。</param>
        /// <returns>答案相符時為 1，否則為 0。</returns>
        private static int RunSolution(
            string solutionName,
            Func<IList<IList<int>>, int> solution,
            int[][] sourceGrid,
            int expected)
        {
            IList<IList<int>> grid = sourceGrid
                .Select(row => (IList<int>)row.ToArray())
                .ToList();

            try
            {
                int actual = solution(grid);
                bool passed = actual == expected;
                Console.WriteLine($"  {solutionName}: Actual = {actual}, Result = {(passed ? "PASS" : "FAIL")}");
                return passed ? 1 : 0;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"  {solutionName}: Actual = ERROR ({exception.GetType().Name}), Result = FAIL");
                return 0;
            }
        }


        /// <summary>
        /// 將測試矩陣格式化為單行巢狀陣列文字，方便同時閱讀終端輸出與 README 範例。
        /// 輸入為非空的鋸齒陣列，輸出為不修改原資料的可讀字串。
        /// </summary>
        /// <param name="grid">要格式化的測試矩陣。</param>
        /// <returns>形如 <c>[[1, 2], [3, 4]]</c> 的文字。</returns>
        private static string FormatGrid(int[][] grid)
        {
            return $"[{string.Join(", ", grid.Select(row => $"[{string.Join(", ", row)}]"))}]";
        }


        /// <summary>
        /// 使用二維動態規劃計算矩陣中的最大差異分數。
        /// 多步移動的分數會消去中間項，因此只需為每個終點找出其上方或左方可達區域中的最小起點值。
        /// 輸入必須是符合題目限制的非空矩陣，輸出為至少移動一次可取得的最大總分，且不會修改輸入矩陣。
        /// </summary>
        /// <remarks>
        /// <c>prefixMinimum[i][j]</c> 保存走到對應前綴範圍時可使用的最小值。
        /// 時間複雜度為 O(mn)，空間複雜度為 O(mn)。
        /// </remarks>
        /// <param name="grid">由正整數組成、至少為 2 x 2 的矩陣。</param>
        /// <returns>從任意格開始並至少向右或向下移動一次所能取得的最大總分。</returns>
        public static int MaxScore(IList<IList<int>> grid)
        {
            int rowCount = grid.Count;
            int columnCount = grid[0].Count;
            int[][] prefixMinimum = new int[rowCount + 1][];

            for (int row = 0; row <= rowCount; row++)
            {
                prefixMinimum[row] = new int[columnCount + 1];
                Array.Fill(prefixMinimum[row], int.MaxValue);
            }

            int maximumScore = int.MinValue;
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    // 合法起點必須位於目前格子的上方或左方；兩個前綴狀態已涵蓋所有可達位置。
                    int predecessorMinimum = Math.Min(
                        prefixMinimum[row][column + 1],
                        prefixMinimum[row + 1][column]);
                    int currentValue = grid[row][column];

                    if (row + column > 0)
                    {
                        // (c2-c1)+(c3-c2)+... 會消去中間項，只剩終點值減起點值。
                        maximumScore = Math.Max(maximumScore, currentValue - predecessorMinimum);
                    }

                    prefixMinimum[row + 1][column + 1] = Math.Min(
                        currentValue,
                        predecessorMinimum);
                }
            }

            return maximumScore;
        }


        /// <summary>
        /// 使用一維滾動動態規劃計算矩陣中的最大差異分數。
        /// 逐列掃描時，以陣列保存各欄上方的前綴最小值，並以單一變數保存目前格子左方的前綴最小值。
        /// 輸入必須是符合題目限制的非空矩陣，輸出為至少移動一次可取得的最大總分，且不會修改輸入矩陣。
        /// </summary>
        /// <remarks>
        /// 此方法與 <see cref="MaxScore"/> 使用相同轉移，只壓縮不再需要的列狀態。
        /// 時間複雜度為 O(mn)，空間複雜度為 O(n)。
        /// </remarks>
        /// <param name="grid">由正整數組成、至少為 2 x 2 的矩陣。</param>
        /// <returns>從任意格開始並至少向右或向下移動一次所能取得的最大總分。</returns>
        public static int MaxScore2(IList<IList<int>> grid)
        {
            int rowCount = grid.Count;
            int columnCount = grid[0].Count;
            int[] columnMinimum = new int[columnCount];
            Array.Fill(columnMinimum, int.MaxValue);

            int maximumScore = int.MinValue;
            for (int row = 0; row < rowCount; row++)
            {
                int leftMinimum = int.MaxValue;
                for (int column = 0; column < columnCount; column++)
                {
                    int predecessorMinimum = Math.Min(columnMinimum[column], leftMinimum);
                    int currentValue = grid[row][column];

                    if (row + column > 0)
                    {
                        maximumScore = Math.Max(maximumScore, currentValue - predecessorMinimum);
                    }

                    // 先讀取上方舊值，再同步更新欄狀態與左側狀態，避免覆蓋尚未使用的資料。
                    int currentMinimum = Math.Min(currentValue, predecessorMinimum);
                    columnMinimum[column] = currentMinimum;
                    leftMinimum = currentMinimum;
                }
            }

            return maximumScore;
        }
    }
}