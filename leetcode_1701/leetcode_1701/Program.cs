using System.Globalization;

namespace leetcode_1701
{
    internal class Program
    {
        /// <summary>
        /// 1701. Average Waiting Time
        /// https://leetcode.com/problems/average-waiting-time/?envType=daily-question&envId=2024-07-09
        /// 1701. 平均等待时间
        /// https://leetcode.cn/problems/average-waiting-time/description/
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