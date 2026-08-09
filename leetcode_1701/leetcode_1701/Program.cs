using System.Globalization;

namespace leetcode_1701
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1701. Average Waiting Time
        /// https://leetcode.com/problems/average-waiting-time/description/
        ///
        /// There is a restaurant with a single chef. You are given an array customers, where customers[i] = [arrival_i, time_i]:
        /// - arrival_i is the arrival time of the i-th customer. The arrival times are sorted in non-decreasing order.
        /// - time_i is the time needed to prepare the order of the i-th customer.
        ///
        /// When a customer arrives, they give the chef their order, and the chef starts preparing it once idle. The customer waits until the chef finishes the order. The chef prepares only one order at a time and serves customers in input order.
        ///
        /// Return the average waiting time of all customers. Answers within 10^-5 of the actual answer are accepted.
        ///
        /// Example 1:
        /// Input: customers = [[1,2],[2,5],[4,3]]
        /// Output: 5.00000
        /// Explanation:
        /// 1) The first customer arrives at time 1. The chef starts immediately at time 1 and finishes at time 3, so the waiting time is 3 - 1 = 2.
        /// 2) The second customer arrives at time 2. The chef starts at time 3 and finishes at time 8, so the waiting time is 8 - 2 = 6.
        /// 3) The third customer arrives at time 4. The chef starts at time 8 and finishes at time 11, so the waiting time is 11 - 4 = 7.
        /// The average waiting time is (2 + 6 + 7) / 3 = 5.
        ///
        /// Example 2:
        /// Input: customers = [[5,2],[5,4],[10,3],[20,1]]
        /// Output: 3.25000
        /// Explanation:
        /// 1) The first customer arrives at time 5. The chef starts immediately at time 5 and finishes at time 7, so the waiting time is 7 - 5 = 2.
        /// 2) The second customer arrives at time 5. The chef starts at time 7 and finishes at time 11, so the waiting time is 11 - 5 = 6.
        /// 3) The third customer arrives at time 10. The chef starts at time 11 and finishes at time 14, so the waiting time is 14 - 10 = 4.
        /// 4) The fourth customer arrives at time 20. The chef starts immediately at time 20 and finishes at time 21, so the waiting time is 21 - 20 = 1.
        /// The average waiting time is (2 + 6 + 4 + 1) / 4 = 3.25.
        ///
        /// Constraints:
        /// - 1 &lt;= customers.length &lt;= 10^5
        /// - 1 &lt;= arrival_i, time_i &lt;= 10^4
        /// - arrival_i &lt;= arrival_(i+1)
        /// </para>
        /// <para>
        /// 1701. 平均等待時間
        /// https://leetcode.cn/problems/average-waiting-time/description/
        ///
        /// 有一家餐廳只有一位廚師。給定陣列 customers，其中 customers[i] = [arrival_i, time_i]：
        /// - arrival_i 是第 i 位顧客的抵達時間，且抵達時間依非遞減順序排列。
        /// - time_i 是準備第 i 位顧客餐點所需的時間。
        ///
        /// 顧客抵達時會向廚師點餐；廚師空閒後便開始準備，顧客會等待到餐點完成。廚師一次只能準備一份餐點，並依輸入順序服務顧客。
        ///
        /// 回傳所有顧客的平均等待時間。與正確答案相差不超過 10^-5 的答案都會被接受。
        ///
        /// 範例 1：
        /// 輸入：customers = [[1,2],[2,5],[4,3]]
        /// 輸出：5.00000
        /// 說明：
        /// 1) 第一位顧客在時間 1 抵達。廚師於時間 1 立即開始並在時間 3 完成，因此等待時間為 3 - 1 = 2。
        /// 2) 第二位顧客在時間 2 抵達。廚師於時間 3 開始並在時間 8 完成，因此等待時間為 8 - 2 = 6。
        /// 3) 第三位顧客在時間 4 抵達。廚師於時間 8 開始並在時間 11 完成，因此等待時間為 11 - 4 = 7。
        /// 平均等待時間為 (2 + 6 + 7) / 3 = 5。
        ///
        /// 範例 2：
        /// 輸入：customers = [[5,2],[5,4],[10,3],[20,1]]
        /// 輸出：3.25000
        /// 說明：
        /// 1) 第一位顧客在時間 5 抵達。廚師於時間 5 立即開始並在時間 7 完成，因此等待時間為 7 - 5 = 2。
        /// 2) 第二位顧客在時間 5 抵達。廚師於時間 7 開始並在時間 11 完成，因此等待時間為 11 - 5 = 6。
        /// 3) 第三位顧客在時間 10 抵達。廚師於時間 11 開始並在時間 14 完成，因此等待時間為 14 - 10 = 4。
        /// 4) 第四位顧客在時間 20 抵達。廚師於時間 20 立即開始並在時間 21 完成，因此等待時間為 21 - 20 = 1。
        /// 平均等待時間為 (2 + 6 + 4 + 1) / 4 = 3.25。
        ///
        /// 限制條件：
        /// - 1 &lt;= customers.length &lt;= 10^5
        /// - 1 &lt;= arrival_i, time_i &lt;= 10^4
        /// - arrival_i &lt;= arrival_(i+1)
        /// </para>
        /// </summary>
        /// <remarks>
        /// 主要進入點會執行六組固定案例，比較完成時間、等待積壓與前綴公式三種線性解法，
        /// 並以 Expected、Actual、誤差及 PASS/FAIL 顯示驗證結果。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            bool allPassed = RunSamples();
            Environment.ExitCode = allPassed ? 0 : 1;
        }

        /// <summary>
        /// 執行六組固定案例，對三種平均等待時間解法分別驗證數值結果與輸入不變性。
        /// </summary>
        /// <returns>三十六項檢查全部通過時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            const int sampleCount = 6;
            const int solutionCount = 3;
            const int checksPerSolution = 2;
            int passedCount = 0;

            passedCount += RunSample(
                "1. 官方範例一（廚師持續忙碌）",
                new[] { new[] { 1, 2 }, new[] { 2, 5 }, new[] { 4, 3 } },
                5.0);
            passedCount += RunSample(
                "2. 官方範例二（同時到達與空閒）",
                new[] { new[] { 5, 2 }, new[] { 5, 4 }, new[] { 10, 3 }, new[] { 20, 1 } },
                3.25);
            passedCount += RunSample(
                "3. 最小輸入",
                new[] { new[] { 1, 1 } },
                1.0);
            passedCount += RunSample(
                "4. 每位客人到達前廚師皆已空閒",
                new[] { new[] { 1, 2 }, new[] { 10, 3 }, new[] { 20, 1 } },
                2.0);
            passedCount += RunSample(
                "5. 多位客人同時到達",
                new[] { new[] { 5, 2 }, new[] { 5, 1 }, new[] { 5, 3 } },
                11.0 / 3.0);

            int[][] maximumCustomers = Enumerable.Range(0, 100_000)
                .Select(_ => new[] { 1, 10_000 })
                .ToArray();
            passedCount += RunSample(
                "6. 官方最大客人數與製作時間",
                maximumCustomers,
                500_005_000.0);

            int totalCount = sampleCount * solutionCount * checksPerSolution;
            Console.WriteLine();
            Console.WriteLine($"總結：{passedCount}/{totalCount} 項測試通過");
            return passedCount == totalCount;
        }

        /// <summary>
        /// 顯示單一案例的輸入，並依序執行三種解法及累計通過的契約檢查數量。
        /// </summary>
        /// <param name="name">案例名稱與涵蓋情境。</param>
        /// <param name="customers">依到達時間非遞減排列的客人資料。</param>
        /// <param name="expected">此案例的預期平均等待時間。</param>
        /// <returns>本案例通過的檢查數量，範圍為零到六。</returns>
        private static int RunSample(string name, int[][] customers, double expected)
        {
            Console.WriteLine();
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"Input：customers = {FormatCustomers(customers)}");

            int passedCount = 0;
            passedCount += RunSolution(
                "解法一：AverageWaitingTime（完成時間模擬）",
                AverageWaitingTime,
                customers,
                expected);
            passedCount += RunSolution(
                "解法二：AverageWaitingTime2（等待積壓遞推）",
                AverageWaitingTime2,
                customers,
                expected);
            passedCount += RunSolution(
                "解法三：AverageWaitingTime3（前綴公式）",
                AverageWaitingTime3,
                customers,
                expected);

            return passedCount;
        }

        /// <summary>
        /// 使用獨立的巢狀陣列複本執行指定解法，並分別檢查浮點結果及輸入不變性。
        /// </summary>
        /// <param name="solutionName">解法的顯示名稱。</param>
        /// <param name="solution">接受客人資料並回傳平均等待時間的函式。</param>
        /// <param name="customers">案例的原始客人資料。</param>
        /// <param name="expected">案例的預期平均等待時間。</param>
        /// <returns>本次通過的檢查數量，範圍為零到二。</returns>
        private static int RunSolution(
            string solutionName,
            Func<int[][], double> solution,
            int[][] customers,
            double expected)
        {
            const double tolerance = 1e-9;
            int[][] workingCustomers = CloneCustomers(customers);
            double actual = solution(workingCustomers);
            double error = Math.Abs(actual - expected);
            bool outputMatches = error <= tolerance;
            bool inputUnchanged = CustomersEqual(workingCustomers, customers);

            Console.WriteLine(solutionName);
            Console.WriteLine($"Expected：{FormatDouble(expected)}");
            Console.WriteLine($"Actual：{FormatDouble(actual)}");
            Console.WriteLine($"Error：{FormatDouble(error)}");
            Console.WriteLine($"Output：{(outputMatches ? "PASS" : "FAIL")}");
            Console.WriteLine($"Input unchanged：{(inputUnchanged ? "PASS" : "FAIL")}");

            return (outputMatches ? 1 : 0) + (inputUnchanged ? 1 : 0);
        }

        /// <summary>
        /// 深層複製二維交錯陣列，確保每種解法取得互不共用列資料的測試輸入。
        /// </summary>
        /// <param name="customers">要複製的客人資料。</param>
        /// <returns>內容相同但不共用內層陣列的新資料。</returns>
        private static int[][] CloneCustomers(int[][] customers)
        {
            return customers.Select(customer => (int[])customer.Clone()).ToArray();
        }

        /// <summary>
        /// 比較兩份客人資料的列數、每列長度及所有元素是否完全相同。
        /// </summary>
        /// <param name="left">第一份客人資料。</param>
        /// <param name="right">第二份客人資料。</param>
        /// <returns>兩份巢狀陣列內容完全相同時回傳 <see langword="true"/>。</returns>
        private static bool CustomersEqual(int[][] left, int[][] right)
        {
            return left.Length == right.Length
                && left.Zip(right).All(pair => pair.First.SequenceEqual(pair.Second));
        }

        /// <summary>
        /// 將客人資料格式化為穩定、易於閱讀的字串；大型案例只顯示筆數與首尾資料。
        /// </summary>
        /// <param name="customers">要顯示的客人資料。</param>
        /// <returns>完整小型資料或大型資料摘要。</returns>
        private static string FormatCustomers(int[][] customers)
        {
            if (customers.Length > 10)
            {
                return $"{customers.Length} 筆；first = {FormatCustomer(customers[0])}；last = {FormatCustomer(customers[^1])}";
            }

            return $"[{string.Join(", ", customers.Select(FormatCustomer))}]";
        }

        /// <summary>
        /// 將單筆客人的到達時間與製作時間格式化為方括號字串。
        /// </summary>
        /// <param name="customer">包含到達時間與製作時間的兩元素陣列。</param>
        /// <returns>格式為 <c>[arrival, time]</c> 的字串。</returns>
        private static string FormatCustomer(int[] customer)
        {
            return $"[{customer[0]}, {customer[1]}]";
        }

        /// <summary>
        /// 使用固定文化特性格式化浮點數，讓終端輸出與 README 在不同系統上保持一致。
        /// </summary>
        /// <param name="value">要格式化的浮點值。</param>
        /// <returns>最多保留十位小數且不補多餘零的字串。</returns>
        private static string FormatDouble(double value)
        {
            return value.ToString("0.##########", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 以廚師的完成時間模擬依序服務；每位客人的開始時間是到達時間與前一份餐點完成時間的較大值，
        /// 再以完成時間減到達時間累加等待時間。
        /// 輸入需符合題目限制：一到十萬筆 <c>[arrival, time]</c>，兩值皆為 1 到 10000，且到達時間非遞減。
        /// 方法不修改 <paramref name="customers"/>，回傳所有客人從到達至取得餐點的平均時間；時間複雜度為 O(n)，額外空間為 O(1)。
        /// </summary>
        /// <remarks>
        /// 參考：
        /// https://leetcode.cn/problems/average-waiting-time/solutions/2579810/1701-ping-jun-deng-dai-shi-jian-by-storm-bl74/
        /// https://leetcode.cn/problems/average-waiting-time/solutions/536627/average-waiting-time-by-ikaruga-3iej/
        /// </remarks>
        /// <param name="customers">依到達時間非遞減排列的非空客人資料。</param>
        /// <returns>所有客人的平均等待時間。</returns>
        public static double AverageWaitingTime(int[][] customers)
        {
            long totalWaitingTime = 0;
            long finishTime = 0;

            foreach (int[] customer in customers)
            {
                int arrival = customer[0];
                int preparationTime = customer[1];

                // 廚師若仍忙碌就接續前一份訂單，否則等到目前客人抵達再開始。
                finishTime = Math.Max(finishTime, arrival) + preparationTime;
                totalWaitingTime += finishTime - arrival;
            }

            return (double)totalWaitingTime / customers.Length;
        }

        /// <summary>
        /// 以等待積壓量遞推平均等待時間；相鄰客人的到達間隔會先消耗尚未完成的工作，
        /// 再把目前餐點的製作時間加入積壓，而更新後的積壓就是目前客人的完整等待時間。
        /// 輸入需符合題目限制：一到十萬筆 <c>[arrival, time]</c>，兩值皆為 1 到 10000，且到達時間非遞減。
        /// 方法不修改 <paramref name="customers"/>，回傳所有客人的平均等待時間；時間複雜度為 O(n)，額外空間為 O(1)。
        /// </summary>
        /// <param name="customers">依到達時間非遞減排列的非空客人資料。</param>
        /// <returns>所有客人的平均等待時間。</returns>
        public static double AverageWaitingTime2(int[][] customers)
        {
            long totalWaitingTime = 0;
            long pendingTime = 0;
            int previousArrival = customers[0][0];

            foreach (int[] customer in customers)
            {
                int arrival = customer[0];
                int preparationTime = customer[1];
                int elapsedSincePreviousArrival = arrival - previousArrival;

                // 兩次到達之間廚師會消化積壓；積壓不能低於零，之後再排入目前訂單。
                pendingTime = Math.Max(0, pendingTime - elapsedSincePreviousArrival) + preparationTime;
                totalWaitingTime += pendingTime;
                previousArrival = arrival;
            }

            return (double)totalWaitingTime / customers.Length;
        }

        /// <summary>
        /// 以製作時間前綴和與起始偏移量的前綴最大值計算平均等待時間。
        /// 對目前客人而言，完成時間可寫成「目前製作前綴和 + 先前所有可開工時間偏移的最大值」，
        /// 因此只需維護兩個前綴狀態，不必逐次比較廚師是否空閒。
        /// 輸入需符合題目限制：一到十萬筆 <c>[arrival, time]</c>，兩值皆為 1 到 10000，且到達時間非遞減。
        /// 方法不修改 <paramref name="customers"/>，回傳所有客人的平均等待時間；時間複雜度為 O(n)，額外空間為 O(1)。
        /// </summary>
        /// <param name="customers">依到達時間非遞減排列的非空客人資料。</param>
        /// <returns>所有客人的平均等待時間。</returns>
        public static double AverageWaitingTime3(int[][] customers)
        {
            long totalWaitingTime = 0;
            long preparationPrefix = 0;
            long maximumStartOffset = long.MinValue;

            foreach (int[] customer in customers)
            {
                int arrival = customer[0];
                int preparationTime = customer[1];

                // arrival - 舊前綴和代表從這位客人重新開工時，完成時間公式需要的基準偏移。
                maximumStartOffset = Math.Max(maximumStartOffset, arrival - preparationPrefix);
                preparationPrefix += preparationTime;
                long finishTime = preparationPrefix + maximumStartOffset;
                totalWaitingTime += finishTime - arrival;
            }

            return (double)totalWaitingTime / customers.Length;
        }
    }
}