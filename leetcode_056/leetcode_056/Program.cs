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
        /// ref:
        /// https://leetcode.cn/problems/merge-intervals/solutions/203562/he-bing-qu-jian-by-leetcode-solution/
        /// https://leetcode.cn/problems/merge-intervals/solutions/2798138/jian-dan-zuo-fa-yi-ji-wei-shi-yao-yao-zh-f2b3/
        /// https://leetcode.cn/problems/merge-intervals/solutions/1530698/by-stormsunshine-kvvc/
        /// 
        /// Array.Sort(intervals, (p, q) => p[0].CompareTo(q[0]));
        /// 按照每個子陣列（區間）的第一個元素（起始值）進行升序排序。
        /// 
        /// (p, q) => p[0].CompareTo(q[0])：
        /// 這是一個 Lambda 表達式，用於指定排序的比較邏輯。
        /// p 和 q 是 intervals 陣列中的兩個元素（即兩個一維陣列）。
        /// p[0] 和 q[0] 分別是這兩個一維陣列的第一個元素。
        /// 
        /// p[0].CompareTo(q[0])：
        /// CompareTo 方法用於比較兩個值。
        /// 如果 p[0] 小於 q[0]，則返回負數，表示 p 應排在 q 之前。
        /// 如果 p[0] 等於 q[0]，則返回 0，表示 p 和 q 的順序不變。
        /// 如果 p[0] 大於 q[0]，則返回正數，表示 p 應排在 q 之後。
        /// 
        /// p[0] 代表起點，p[1] 代表終點。
        /// p[0] <= ans[m - 1][1]：如果當前區間的起點 p[0] 小於等於最後一個合併區間的終點 ans[m-1][1]，代表這兩個區間重疊，我們可以進行合併。
        /// 
        /// 時間複雜度：O(n log n)
        /// 空間複雜度：O(n)，需要額外空間存儲結果
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        public static int[][] Merge(int[][] intervals)
        {
            // 先按照區間起始位置排序, 左邊界開始由小至大
            // 這樣可以保證後面的區間的左邊界一定大於等於前面的區間的左邊界
            Array.Sort(intervals, (p, q) => p[0].CompareTo(q[0]));
            // 用來存放合併後的區間
            List<int[]> result = new List<int[]>();

            foreach (var p in intervals)
            {
                // ans.Count 代表合併後的區間數量
                int m = result.Count;
                // 如果當前區間與前一個區間相交; 可以合併
                // 檢查是否有重疊：
                // 如果結果清單不為空且當前區間的起始值小於等於前一個區間的結束值
                if (m > 0 && p[0] <= result[m - 1][1])
                {
                    // 更新前一個區間的右端點為兩者中的最大值; 更新右端點最大值
                    // 取這兩者的最大值，確保合併後的終點涵蓋更大的範圍。
                    result[m - 1][1] = Math.Max(result[m - 1][1], p[1]);
                }
                else // 不相交, 無法合併
                {
                    // 否則，將當前區間添加為新的不相交區間; 下面兩個寫法意思相同
                    // ans.Add(new int[] { p[0], p[1] });
                    // 這裡不用 new 一個新的陣列，直接將 p 加入即可; 因為 p 已經是一個新的陣列
                    result.Add(p);
                }
            }

            return result.ToArray();
        }
    }
}
