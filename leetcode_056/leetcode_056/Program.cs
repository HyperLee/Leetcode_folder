namespace leetcode_056
{
    internal class Program
    {
        /// <summary>
        /// 56. Merge Intervals
        /// https://leetcode.com/problems/merge-intervals/description/
        /// 56. 合併區間
        /// https://leetcode.cn/problems/merge-intervals/description/
        /// 
        /// 題目描述：
        /// 給定一組區間，請將所有重疊的區間合併，並返回一組不重疊的區間。
        /// 解題概念與出發點：
        /// 1. 先將區間按照起始值進行排序，確保後續處理時的順序性。
        /// 2. 使用一個結果清單來存放合併後的區間。
        /// 3. 遍歷排序後的區間，檢查當前區間是否與結果清單中的最後一個區間重疊。
        ///    - 如果重疊，更新最後一個區間的結束值。
        ///    - 如果不重疊，將當前區間加入結果清單。
        /// 4. 最後返回結果清單。
        /// </summary>
        /// <remarks>
        /// 使用六組固定案例分別驗證排序合併法與座標事件掃描法，
        /// 並輸出每一種解法的預期結果、實際結果與 PASS/FAIL。
        /// </remarks>
        /// <param name="args">命令列參數；此範例不使用任何參數。</param>
        static void Main(string[] args)
        {
            (string Name, int[][] Input, int[][] Expected)[] testCases =
            {
                (
                    "Case 1 - Typical overlaps",
                    new int[][] { new[] { 1, 3 }, new[] { 2, 6 }, new[] { 8, 10 }, new[] { 15, 18 } },
                    new int[][] { new[] { 1, 6 }, new[] { 8, 10 }, new[] { 15, 18 } }
                ),
                (
                    "Case 2 - Touching intervals",
                    new int[][] { new[] { 1, 4 }, new[] { 4, 5 } },
                    new int[][] { new[] { 1, 5 } }
                ),
                (
                    "Case 3 - Unsorted touching intervals",
                    new int[][] { new[] { 4, 7 }, new[] { 1, 4 } },
                    new int[][] { new[] { 1, 7 } }
                ),
                (
                    "Case 4 - Chained overlaps",
                    new int[][] { new[] { 1, 4 }, new[] { 0, 2 }, new[] { 3, 5 } },
                    new int[][] { new[] { 0, 5 } }
                ),
                (
                    "Case 5 - Duplicate and contained intervals",
                    new int[][] { new[] { 1, 3 }, new[] { 1, 3 }, new[] { 2, 2 } },
                    new int[][] { new[] { 1, 3 } }
                ),
                (
                    "Case 6 - Coordinate boundaries",
                    new int[][] { new[] { 0, 10000 } },
                    new int[][] { new[] { 0, 10000 } }
                )
            };

            int passedChecks = 0;
            const int solutionCount = 2;

            foreach ((string name, int[][] input, int[][] expected) in testCases)
            {
                passedChecks += RunTestCase(name, input, expected);
            }

            int totalChecks = testCases.Length * solutionCount;
            Console.WriteLine($"Overall: {passedChecks}/{totalChecks} passed.");
        }

        /// <summary>
        /// 使用同一組輸入與預期結果執行所有合併區間解法。
        /// 每次呼叫解法前都會深層複製輸入，以隔離可能修改陣列的實作；
        /// 回傳通過驗證的解法數量，並輸出各解法的 Expected、Actual 與 PASS/FAIL。
        /// </summary>
        /// <param name="caseName">顯示於主控台的案例名稱。</param>
        /// <param name="input">符合題目限制的區間陣列。</param>
        /// <param name="expected">案例預期得到的已排序、不重疊區間。</param>
        /// <returns>此案例中通過結果比對的解法數量。</returns>
        private static int RunTestCase(string caseName, int[][] input, int[][] expected)
        {
            (string Name, Func<int[][], int[][]> Solve)[] solutions =
            {
                (nameof(Merge), Merge),
                (nameof(MergeBySweepLine), MergeBySweepLine)
            };

            int passedChecks = 0;
            Console.WriteLine($"{caseName}: Input = {FormatIntervals(input)}");

            foreach ((string name, Func<int[][], int[][]> solve) in solutions)
            {
                int[][] actual = solve(CloneIntervals(input));
                bool passed = AreIntervalsEqual(expected, actual);
                passedChecks += passed ? 1 : 0;

                Console.WriteLine(
                    $"  {name}: Expected = {FormatIntervals(expected)}, " +
                    $"Actual = {FormatIntervals(actual)}, {(passed ? "PASS" : "FAIL")}");
            }

            return passedChecks;
        }

        /// <summary>
        /// 深層複製二維區間陣列，讓會排序或修改端點的解法不影響原始測試資料。
        /// 輸入必須由非 null 的二元素區間組成，輸出為內容相同但彼此獨立的新陣列。
        /// </summary>
        /// <param name="intervals">要複製的區間陣列。</param>
        /// <returns>包含獨立內層陣列的深層副本。</returns>
        private static int[][] CloneIntervals(int[][] intervals)
        {
            return intervals.Select(interval => interval.ToArray()).ToArray();
        }

        /// <summary>
        /// 依序比較兩組合併結果的區間數量與每個端點。
        /// 輸入應為已排序的合併結果；完全相同時回傳 true，否則回傳 false。
        /// </summary>
        /// <param name="expected">預期的合併結果。</param>
        /// <param name="actual">解法實際產生的合併結果。</param>
        /// <returns>兩組二維陣列是否依序相等。</returns>
        private static bool AreIntervalsEqual(int[][] expected, int[][] actual)
        {
            return expected.Length == actual.Length
                && expected.Zip(actual).All(pair => pair.First.SequenceEqual(pair.Second));
        }

        /// <summary>
        /// 將二維區間陣列格式化成容易閱讀且可直接放入 README 的文字。
        /// 輸入為合法區間陣列，輸出格式例如 <c>[[1, 3], [6, 9]]</c>。
        /// </summary>
        /// <param name="intervals">要格式化的區間陣列。</param>
        /// <returns>以方括號表示的單行區間字串。</returns>
        private static string FormatIntervals(int[][] intervals)
        {
            return $"[{string.Join(", ", intervals.Select(interval => $"[{interval[0]}, {interval[1]}]"))}]";
        }

        /// <summary>
        /// 依區間起點排序，再以一次線性掃描合併所有重疊區間。
        /// 排序後只需比較目前區間與結果中的最後一段；若重疊便延伸右端點，
        /// 否則建立新的結果區間。輸入必須符合題目限制，方法會原地排序外層陣列，
        /// 並可能透過結果中共用的內層陣列更新輸入端點。
        /// 時間複雜度為 O(n log n)，回傳結果需要 O(n) 空間。
        /// </summary>
        /// <param name="intervals">
        /// 由二元素陣列組成的非空區間集合，每段皆滿足
        /// <c>0 &lt;= start &lt;= end &lt;= 10000</c>。
        /// </param>
        /// <returns>依起點排序、彼此不重疊且覆蓋所有輸入區間的陣列。</returns>
        public static int[][] Merge(int[][] intervals)
        {
            // 起點遞增後，任何重疊只可能發生在目前區間與最後一段合併結果之間。
            Array.Sort(intervals, (left, right) => left[0].CompareTo(right[0]));
            List<int[]> result = new List<int[]>();

            foreach (int[] interval in intervals)
            {
                int lastIndex = result.Count - 1;

                if (lastIndex >= 0 && interval[0] <= result[lastIndex][1])
                {
                    // 重疊時保留較遠的右端點，讓同一段結果涵蓋兩個區間。
                    result[lastIndex][1] = Math.Max(result[lastIndex][1], interval[1]);
                }
                else
                {
                    // 起點已超過最後一段的右端點，必須開始新的不重疊區間。
                    result.Add(interval);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 利用題目座標上限建立起點與終點事件表，再由左至右掃描所有座標。
        /// 活動區間數從 0 變成正數時開始一段合併結果，回到 0 時結束；
        /// 同一座標會先加入起點再扣除終點，因此端點相接的區間仍會合併。
        /// 輸入必須符合題目限制，方法不會排序或修改輸入。
        /// 時間複雜度為 O(n + U)，額外空間為 O(U)，其中 U = 10001。
        /// </summary>
        /// <param name="intervals">
        /// 由二元素陣列組成的非空區間集合，每段皆滿足
        /// <c>0 &lt;= start &lt;= end &lt;= 10000</c>。
        /// </param>
        /// <returns>依起點排序、彼此不重疊且覆蓋所有輸入區間的全新陣列。</returns>
        public static int[][] MergeBySweepLine(int[][] intervals)
        {
            const int coordinateCount = 10001;
            int[] startCounts = new int[coordinateCount];
            int[] endCounts = new int[coordinateCount];

            foreach (int[] interval in intervals)
            {
                startCounts[interval[0]]++;
                endCounts[interval[1]]++;
            }

            List<int[]> result = new List<int[]>();
            int activeIntervals = 0;
            int mergedStart = 0;

            for (int coordinate = 0; coordinate < coordinateCount; coordinate++)
            {
                if (activeIntervals == 0 && startCounts[coordinate] > 0)
                {
                    mergedStart = coordinate;
                }

                // 先加入同座標的新區間，再結束舊區間，端點相接時活動數不會提早歸零。
                activeIntervals += startCounts[coordinate];
                activeIntervals -= endCounts[coordinate];

                if (activeIntervals == 0 && endCounts[coordinate] > 0)
                {
                    result.Add(new[] { mergedStart, coordinate });
                }
            }

            return result.ToArray();
        }
    }
}