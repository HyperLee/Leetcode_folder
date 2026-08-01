namespace leetcode_1052
{
    internal class Program
    {
        /// <summary>
        /// 1052. Grumpy Bookstore Owner
        /// https://leetcode.com/problems/grumpy-bookstore-owner/description/?envType=daily-question&envId=2024-06-21
        /// 1052. 爱生气的书店老板
        /// https://leetcode.cn/problems/grumpy-bookstore-owner/description/
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