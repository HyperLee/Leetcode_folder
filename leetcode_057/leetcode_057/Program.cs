namespace leetcode_057
{
    internal class Program
    {
        /// <summary>
        /// 57. Insert Interval
        /// https://leetcode.com/problems/insert-interval/description/
        /// <para>
        /// You are given an array of non-overlapping intervals, intervals, where intervals[i] = [start_i, end_i] represents the start and end of the i-th interval, and intervals is sorted in ascending order by start_i. You are also given an interval newInterval = [start, end] that represents the start and end of another interval.
        ///
        /// Two intervals are considered overlapping if they share at least one point.
        ///
        /// Insert newInterval into intervals so that intervals remains sorted in ascending order by start_i and still contains no overlapping intervals. Merge overlapping intervals if necessary.
        ///
        /// Return intervals after the insertion.
        ///
        /// Note that you do not need to modify intervals in-place. You may create and return a new array.
        ///
        /// Example 1:
        /// Input: intervals = [[1,3],[6,9]], newInterval = [2,5]
        /// Output: [[1,5],[6,9]]
        ///
        /// Example 2:
        /// Input: intervals = [[1,2],[3,5],[6,7],[8,10],[12,16]], newInterval = [4,8]
        /// Output: [[1,2],[3,10],[12,16]]
        /// Explanation: The new interval [4,8] overlaps with [3,5], [6,7], and [8,10].
        ///
        /// Constraints:
        /// - 0 &lt;= intervals.length &lt;= 10^4
        /// - intervals[i].length == 2
        /// - 0 &lt;= start_i &lt;= end_i &lt;= 10^5
        /// - intervals is sorted by start_i in ascending order.
        /// - newInterval.length == 2
        /// - 0 &lt;= start &lt;= end &lt;= 10^5
        /// </para>
        /// <para>
        /// 57. 插入區間
        /// https://leetcode.cn/problems/insert-interval/description/
        ///
        /// 給定一個互不重疊的區間陣列 intervals，其中 intervals[i] = [start_i, end_i] 表示第 i 個區間的起點與終點，且 intervals 已依 start_i 遞增排序。另給定一個區間 newInterval = [start, end]，表示另一個區間的起點與終點。
        ///
        /// 若兩個區間共享至少一個點，則視為重疊。
        ///
        /// 將 newInterval 插入 intervals，使 intervals 仍依 start_i 遞增排序，且仍不包含任何重疊區間；必要時請合併重疊區間。
        ///
        /// 回傳插入後的 intervals。
        ///
        /// 注意，你不需要原地修改 intervals，可以建立並回傳新的陣列。
        ///
        /// 範例 1：
        /// 輸入：intervals = [[1,3],[6,9]], newInterval = [2,5]
        /// 輸出：[[1,5],[6,9]]
        ///
        /// 範例 2：
        /// 輸入：intervals = [[1,2],[3,5],[6,7],[8,10],[12,16]], newInterval = [4,8]
        /// 輸出：[[1,2],[3,10],[12,16]]
        /// 解釋：新區間 [4,8] 與 [3,5]、[6,7]、[8,10] 重疊。
        ///
        /// 限制條件：
        /// - 0 &lt;= intervals.length &lt;= 10^4
        /// - intervals[i].length == 2
        /// - 0 &lt;= start_i &lt;= end_i &lt;= 10^5
        /// - intervals 依 start_i 遞增排序。
        /// - newInterval.length == 2
        /// - 0 &lt;= start &lt;= end &lt;= 10^5
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        /// <summary>
        /// 執行 LeetCode 57「插入區間」的固定驗收案例。
        /// 依序涵蓋空陣列、插在最前、插在最後、單段重疊與多段重疊，
        /// 並比較每個輸出端點後顯示各案例及整體通過結果。
        /// </summary>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            int passed = 0;

            passed += RunTestCase(
                "Empty intervals",
                [],
                [5, 7],
                [[5, 7]]) ? 1 : 0;

            passed += RunTestCase(
                "Insert before all intervals",
                [[3, 5], [7, 9]],
                [0, 1],
                [[0, 1], [3, 5], [7, 9]]) ? 1 : 0;

            passed += RunTestCase(
                "Insert after all intervals",
                [[1, 2], [3, 5]],
                [6, 8],
                [[1, 2], [3, 5], [6, 8]]) ? 1 : 0;

            passed += RunTestCase(
                "Merge one interval",
                [[1, 3], [6, 9]],
                [2, 5],
                [[1, 5], [6, 9]]) ? 1 : 0;

            passed += RunTestCase(
                "Merge multiple intervals",
                [[1, 2], [3, 5], [6, 7], [8, 10], [12, 16]],
                [4, 8],
                [[1, 2], [3, 10], [12, 16]]) ? 1 : 0;

            Console.WriteLine($"{passed}/5 tests passed.");
        }

        /// <summary>
        /// 執行一組插入區間案例並輸出測試資料、預期結果、實際結果與 PASS/FAIL。
        /// 實際結果由 <see cref="Insert(int[][], int[])"/> 產生，再逐一比較區間數量與每個端點。
        /// </summary>
        /// <param name="name">顯示在主控台上的案例名稱。</param>
        /// <param name="intervals">依左端點遞增排序且彼此不重疊的區間；可為空陣列。</param>
        /// <param name="newInterval">包含起點與終點的待插入區間。</param>
        /// <param name="expected">插入並合併後的預期區間。</param>
        /// <returns>實際結果與預期結果的區間數量及所有端點都相同時回傳 <see langword="true"/>。</returns>
        private static bool RunTestCase(
            string name,
            int[][] intervals,
            int[] newInterval,
            int[][] expected)
        {
            int[][] actual = Insert(intervals, newInterval);
            bool passed = AreIntervalsEqual(expected, actual);

            Console.WriteLine($"Test: {name}");
            Console.WriteLine(
                $"Input: intervals={FormatIntervals(intervals)}, newInterval=[{newInterval[0]},{newInterval[1]}]");
            Console.WriteLine($"Expected: {FormatIntervals(expected)}");
            Console.WriteLine($"Actual: {FormatIntervals(actual)}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed;
        }

        /// <summary>
        /// 比較兩個二維區間陣列是否完全相同。
        /// 先檢查區間數量，再逐一比較每個區間的端點數量與值，避免以顯示字串代替資料驗證。
        /// </summary>
        /// <param name="expected">預期區間陣列；每個區間應包含兩個端點。</param>
        /// <param name="actual">實際區間陣列；每個區間應包含兩個端點。</param>
        /// <returns>兩個陣列的結構及所有端點皆相同時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool AreIntervalsEqual(int[][] expected, int[][] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i].Length != actual[i].Length)
                {
                    return false;
                }

                for (int j = 0; j < expected[i].Length; j++)
                {
                    if (expected[i][j] != actual[i][j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 將二維區間陣列轉為容易閱讀且可直接放入測試輸出的文字。
        /// 空陣列輸出為 <c>[]</c>，其他輸入輸出為不含多餘空白的巢狀括號格式。
        /// </summary>
        /// <param name="intervals">要格式化的區間陣列；每個區間應包含起點與終點。</param>
        /// <returns>例如 <c>[[1,3],[6,9]]</c> 的格式化結果。</returns>
        private static string FormatIntervals(int[][] intervals)
        {
            return $"[{string.Join(",", intervals.Select(interval => $"[{interval[0]},{interval[1]}]"))}]";
        }

        /// <summary>
        /// 將新區間插入已排序且彼此不重疊的區間陣列。
        /// 演算法以新區間作為待合併範圍，單次掃描原陣列並區分左側、右側與重疊三種關係；
        /// 重疊時持續擴張邊界，直到確認後續區間位於右側才加入合併結果。
        /// </summary>
        /// <param name="intervals">依左端點遞增排序且彼此不重疊的區間；可為空陣列。</param>
        /// <param name="newInterval">包含起點與終點的待插入區間，且起點不大於終點。</param>
        /// <returns>插入並合併後，仍依左端點遞增排序且彼此不重疊的新區間陣列。</returns>
        public static int[][] Insert(int[][] intervals, int[] newInterval)
        {
            int left = newInterval[0];
            int right = newInterval[1];
            bool merged = false;
            List<int[]> ansList = [];

            foreach (int[] interval in intervals)
            {
                if (interval[0] > right)
                {
                    // 輸入已排序；第一次進入右側時，先把完整的合併區間加入一次。
                    if (!merged)
                    {
                        ansList.Add([left, right]);
                        merged = true;
                    }

                    ansList.Add(interval);
                }
                else if (interval[1] < left)
                {
                    // 完全位於待合併區間左側，不需要改變邊界。
                    ansList.Add(interval);
                }
                else
                {
                    // 有交集時只擴張邊界，延後加入，因為後續區間仍可能繼續重疊。
                    left = Math.Min(left, interval[0]);
                    right = Math.Max(right, interval[1]);
                }
            }

            if (!merged)
            {
                ansList.Add([left, right]);
            }

            int[][] ans = new int[ansList.Count][];
            for (int i = 0; i < ansList.Count; i++)
            {
                ans[i] = ansList[i];
            }

            return ans;
        }
    }
}