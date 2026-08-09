namespace leetcode_1052
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1052. Grumpy Bookstore Owner
        /// https://leetcode.com/problems/grumpy-bookstore-owner/description/
        ///
        /// There is a bookstore owner that has a store open for n minutes. You are given an integer array
        /// customers of length n where customers[i] is the number of customers that enter the store at the
        /// start of the i-th minute and all those customers leave after the end of that minute.
        /// During certain minutes, the bookstore owner is grumpy. You are given a binary array grumpy where
        /// grumpy[i] is 1 if the bookstore owner is grumpy during the i-th minute, and is 0 otherwise.
        /// When the bookstore owner is grumpy, the customers entering during that minute are not satisfied.
        /// Otherwise, they are satisfied.
        /// The bookstore owner knows a secret technique to remain not grumpy for minutes consecutive minutes,
        /// but this technique can only be used once.
        /// Return the maximum number of customers that can be satisfied throughout the day.
        ///
        /// Example 1:
        /// Input: customers = [1,0,1,2,1,1,7,5], grumpy = [0,1,0,1,0,1,0,1], minutes = 3
        /// Output: 16
        /// Explanation: The bookstore owner keeps themselves not grumpy for the last 3 minutes.
        /// The maximum number of customers that can be satisfied = 1 + 1 + 1 + 1 + 7 + 5 = 16.
        ///
        /// Example 2:
        /// Input: customers = [1], grumpy = [0], minutes = 1
        /// Output: 1
        ///
        /// Constraints:
        /// n == customers.length == grumpy.length
        /// 1 &lt;= minutes &lt;= n &lt;= 2 * 10^4
        /// 0 &lt;= customers[i] &lt;= 1000
        /// grumpy[i] is either 0 or 1.
        /// </para>
        /// <para>
        /// 1052. 愛生氣的書店老闆
        /// https://leetcode.cn/problems/grumpy-bookstore-owner/description/
        ///
        /// 有一位書店老闆，他的商店營業 n 分鐘。給定長度為 n 的整數陣列 customers，
        /// 其中 customers[i] 是在第 i 分鐘開始時進入商店的顧客人數，且這些顧客都會在該分鐘
        /// 結束後離開。
        /// 在某些分鐘，書店老闆會生氣。給定二元陣列 grumpy，若書店老闆在第 i 分鐘生氣，
        /// grumpy[i] 為 1，否則為 0。
        /// 當書店老闆生氣時，該分鐘進入的顧客不會滿意；否則，他們會感到滿意。
        /// 書店老闆知道一項祕密技巧，可以連續 minutes 分鐘保持不生氣，但這項技巧只能使用一次。
        /// 請回傳一整天中能感到滿意的最大顧客人數。
        ///
        /// 範例 1：
        /// 輸入：customers = [1,0,1,2,1,1,7,5], grumpy = [0,1,0,1,0,1,0,1], minutes = 3
        /// 輸出：16
        /// 解釋：書店老闆在最後連續 3 分鐘保持不生氣。
        /// 能感到滿意的最大顧客人數 = 1 + 1 + 1 + 1 + 7 + 5 = 16。
        ///
        /// 範例 2：
        /// 輸入：customers = [1], grumpy = [0], minutes = 1
        /// 輸出：1
        ///
        /// 限制條件：
        /// n == customers.length == grumpy.length
        /// 1 &lt;= minutes &lt;= n &lt;= 2 * 10^4
        /// 0 &lt;= customers[i] &lt;= 1000
        /// grumpy[i] 只能是 0 或 1。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Environment.ExitCode = RunSamples() ? 0 : 1;
        }

        /// <summary>
        /// 執行固定測試案例，逐一用滑動視窗與前綴和解法驗證答案及輸入保留性。
        /// 此方法不接受輸入；輸出每個案例的預期值、實際值與通過狀態，
        /// 並回傳所有解法檢查是否全部通過。
        /// </summary>
        /// <returns>全部檢查通過時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, int[] Customers, int[] Grumpy, int Minutes, int Expected)[] cases =
            [
                ("官方範例一", [1, 0, 1, 2, 1, 1, 7, 5], [0, 1, 0, 1, 0, 1, 0, 1], 3, 16),
                ("官方範例二", [1], [0], 1, 1),
                ("密技覆蓋全時段", [4, 10, 10], [1, 1, 1], 3, 24),
                ("全程不生氣", [2, 3, 4], [0, 0, 0], 2, 9),
                ("最佳視窗位於中間", [3, 8, 2, 5, 4], [0, 1, 1, 1, 0], 2, 17),
                ("單分鐘密技", [5, 0, 6, 2], [1, 1, 0, 1], 1, 11),
                ("顧客數皆為零", [0, 0, 0], [1, 0, 1], 2, 0)
            ];

            int passedChecks = 0;
            foreach ((string name, int[] customers, int[] grumpy, int minutes, int expected) in cases)
            {
                passedChecks += RunTestCase(name, customers, grumpy, minutes, expected);
            }

            int totalChecks = cases.Length * 2;
            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 執行一組書店案例，讓兩種解法使用獨立副本，避免彼此影響。
        /// 輸入包含案例名稱、每分鐘顧客數、生氣狀態、密技分鐘數與預期值；
        /// 輸出比較資訊，並回傳答案正確且沒有改動輸入的解法數量。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="customers">每分鐘進入書店的顧客數。</param>
        /// <param name="grumpy">老闆每分鐘是否生氣的二元陣列。</param>
        /// <param name="minutes">密技可連續生效的分鐘數。</param>
        /// <param name="expected">最多可滿意的顧客人數。</param>
        /// <returns>通過的解法數量，範圍為 0 到 2。</returns>
        private static int RunTestCase(string name, int[] customers, int[] grumpy, int minutes, int expected)
        {
            int[] originalCustomers = [.. customers];
            int[] originalGrumpy = [.. grumpy];
            int[] slidingCustomers = [.. customers];
            int[] slidingGrumpy = [.. grumpy];
            int[] prefixCustomers = [.. customers];
            int[] prefixGrumpy = [.. grumpy];

            int slidingActual = MaxSatisfied(slidingCustomers, slidingGrumpy, minutes);
            int prefixActual = MaxSatisfied2(prefixCustomers, prefixGrumpy, minutes);
            bool slidingInputsPreserved = slidingCustomers.SequenceEqual(originalCustomers)
                && slidingGrumpy.SequenceEqual(originalGrumpy);
            bool prefixInputsPreserved = prefixCustomers.SequenceEqual(originalCustomers)
                && prefixGrumpy.SequenceEqual(originalGrumpy);
            bool slidingPassed = slidingActual == expected && slidingInputsPreserved;
            bool prefixPassed = prefixActual == expected && prefixInputsPreserved;

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Customers: {FormatArray(customers)}");
            Console.WriteLine($"Grumpy: {FormatArray(grumpy)}");
            Console.WriteLine($"Minutes: {minutes}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine(
                $"MaxSatisfied: {slidingActual} | Inputs preserved: {(slidingInputsPreserved ? "PASS" : "FAIL")} | Result: {(slidingPassed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"MaxSatisfied2: {prefixActual} | Inputs preserved: {(prefixInputsPreserved ? "PASS" : "FAIL")} | Result: {(prefixPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (slidingPassed ? 1 : 0) + (prefixPassed ? 1 : 0);
        }

        /// <summary>
        /// 將整數陣列轉為適合主控台與 README 顯示的固定格式。
        /// 輸入必須是非 <see langword="null"/> 陣列；輸出以方括號包住元素。
        /// </summary>
        /// <param name="values">要格式化的整數陣列。</param>
        /// <returns>以逗號和空格分隔元素的陣列字串。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }



        /// <summary>
        /// 使用固定長度滑動視窗，計算密技可額外挽回的最大顧客數。
        /// 解法先累計老闆原本不生氣時的滿意顧客，再滑動長度為 <paramref name="minutes"/> 的視窗；
        /// 輸入陣列須非 <see langword="null"/>、長度相同且符合官方限制，輸出為最多可滿意的顧客總數。
        /// </summary>
        /// <param name="customers">每分鐘進入書店的顧客數。</param>
        /// <param name="grumpy">老闆每分鐘是否生氣的二元陣列。</param>
        /// <param name="minutes">密技可連續生效的分鐘數，範圍為 1 到陣列長度。</param>
        /// <returns>整天最多可滿意的顧客人數。</returns>
        /// <remarks>
        /// 此方法不修改輸入，時間複雜度為 O(n)，額外空間複雜度為 O(1)。
        /// 參考：
        /// https://leetcode.cn/problems/grumpy-bookstore-owner/solutions/615133/ai-sheng-qi-de-shu-dian-lao-ban-by-leetc-dloq/
        /// https://leetcode.cn/problems/grumpy-bookstore-owner/solutions/2751888/ding-chang-hua-dong-chuang-kou-fu-ti-dan-rch7/
        /// </remarks>
        public static int MaxSatisfied(int[] customers, int[] grumpy, int minutes)
        {
            int alwaysSatisfied = 0;
            int length = customers.Length;

            for (int i = 0; i < length; i++)
            {
                if (grumpy[i] == 0)
                {
                    alwaysSatisfied += customers[i];
                }
            }

            // 視窗只累計原本因生氣而不滿意、使用密技後才能挽回的顧客。
            int windowGain = 0;
            for (int i = 0; i < minutes; i++)
            {
                if (grumpy[i] == 1)
                {
                    windowGain += customers[i];
                }
            }

            int maxWindowGain = windowGain;
            for (int i = minutes; i < length; i++)
            {
                // 移除離開視窗的分鐘，再加入新分鐘，避免重複計算整個區間。
                if (grumpy[i - minutes] == 1)
                {
                    windowGain -= customers[i - minutes];
                }

                if (grumpy[i] == 1)
                {
                    windowGain += customers[i];
                }

                maxWindowGain = Math.Max(maxWindowGain, windowGain);
            }

            return alwaysSatisfied + maxWindowGain;
        }

        /// <summary>
        /// 使用前綴和，查詢每個密技區間可額外挽回的顧客數並取最大值。
        /// 解法同時累計原本滿意的顧客，並建立原本不滿意顧客的前綴總和；
        /// 輸入陣列須非 <see langword="null"/>、長度相同且符合官方限制，輸出為最多可滿意的顧客總數。
        /// </summary>
        /// <param name="customers">每分鐘進入書店的顧客數。</param>
        /// <param name="grumpy">老闆每分鐘是否生氣的二元陣列。</param>
        /// <param name="minutes">密技可連續生效的分鐘數，範圍為 1 到陣列長度。</param>
        /// <returns>整天最多可滿意的顧客人數。</returns>
        /// <remarks>此方法不修改輸入，時間複雜度為 O(n)，額外空間複雜度為 O(n)。</remarks>
        public static int MaxSatisfied2(int[] customers, int[] grumpy, int minutes)
        {
            int alwaysSatisfied = 0;
            int[] dissatisfiedPrefix = new int[customers.Length + 1];

            for (int i = 0; i < customers.Length; i++)
            {
                if (grumpy[i] == 0)
                {
                    alwaysSatisfied += customers[i];
                }

                dissatisfiedPrefix[i + 1] = dissatisfiedPrefix[i]
                    + (grumpy[i] == 1 ? customers[i] : 0);
            }

            int maxWindowGain = 0;
            for (int end = minutes; end <= customers.Length; end++)
            {
                // prefix[end] - prefix[start] 可在 O(1) 取得固定區間的可挽回顧客數。
                int start = end - minutes;
                int windowGain = dissatisfiedPrefix[end] - dissatisfiedPrefix[start];
                maxWindowGain = Math.Max(maxWindowGain, windowGain);
            }

            return alwaysSatisfied + maxWindowGain;
        }
    }
}