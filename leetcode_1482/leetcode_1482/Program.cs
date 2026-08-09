namespace leetcode_1482
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1482. Minimum Number of Days to Make m Bouquets
        /// https://leetcode.com/problems/minimum-number-of-days-to-make-m-bouquets/description/
        ///
        /// You are given an integer array bloomDay, an integer m and an integer k.
        /// You want to make m bouquets. To make a bouquet, you need to use k adjacent flowers from the garden.
        /// The garden consists of n flowers. The i-th flower blooms on bloomDay[i] and can then be used in exactly one bouquet.
        /// Return the minimum number of days you need to wait to make m bouquets. If it is impossible, return -1.
        ///
        /// Example 1:
        /// Input: bloomDay = [1,10,3,10,2], m = 3, k = 1
        /// Output: 3
        /// Explanation: x means bloomed and _ means not bloomed. We need 3 bouquets of 1 flower.
        /// After day 1: [x, _, _, _, _] // one bouquet.
        /// After day 2: [x, _, _, _, x] // two bouquets.
        /// After day 3: [x, _, x, _, x] // 3 bouquets, so the answer is 3.
        ///
        /// Example 2:
        /// Input: bloomDay = [1,10,3,10,2], m = 3, k = 2
        /// Output: -1
        /// Explanation: We need 3 bouquets of 2 flowers, or 6 flowers, but only have 5; therefore it is impossible.
        ///
        /// Example 3:
        /// Input: bloomDay = [7,7,7,7,12,7,7], m = 2, k = 3
        /// Output: 12
        /// Explanation: We need 2 bouquets of 3 flowers.
        /// After day 7: [x, x, x, x, _, x, x]. We can make one bouquet from the first three bloomed flowers, but the last
        /// three bloomed flowers are not adjacent.
        /// After day 12: [x, x, x, x, x, x, x]. We can make two bouquets in different ways.
        ///
        /// Constraints:
        /// - bloomDay.length == n
        /// - 1 &lt;= n &lt;= 10^5
        /// - 1 &lt;= bloomDay[i] &lt;= 10^9
        /// - 1 &lt;= m &lt;= 10^6
        /// - 1 &lt;= k &lt;= n
        /// </para>
        /// <para>
        /// 1482. 製作 m 束花所需的最少天數
        /// https://leetcode.cn/problems/minimum-number-of-days-to-make-m-bouquets/description/
        ///
        /// 給定整數陣列 bloomDay，以及整數 m 與 k。
        /// 你想製作 m 束花；每束花需要使用花園中相鄰的 k 朵花。
        /// 花園共有 n 朵花，第 i 朵花會在 bloomDay[i] 當天開花，之後恰好可用於一束花。
        /// 回傳能製作 m 束花所需等待的最少天數；若不可能完成，回傳 -1。
        ///
        /// 範例 1：
        /// 輸入：bloomDay = [1,10,3,10,2]，m = 3，k = 1
        /// 輸出：3
        /// 解釋：x 表示已開花，_ 表示尚未開花。我們需要 3 束各含 1 朵花的花束。
        /// 第 1 天後：[x, _, _, _, _] // 可製作一束。
        /// 第 2 天後：[x, _, _, _, x] // 可製作兩束。
        /// 第 3 天後：[x, _, x, _, x] // 可製作 3 束，因此答案為 3。
        ///
        /// 範例 2：
        /// 輸入：bloomDay = [1,10,3,10,2]，m = 3，k = 2
        /// 輸出：-1
        /// 解釋：需要 3 束各含 2 朵花的花束，也就是 6 朵花，但只有 5 朵，因此不可能完成。
        ///
        /// 範例 3：
        /// 輸入：bloomDay = [7,7,7,7,12,7,7]，m = 2，k = 3
        /// 輸出：12
        /// 解釋：需要 2 束各含 3 朵花的花束。
        /// 第 7 天後：[x, x, x, x, _, x, x]。可用前三朵已開的花製作一束，但最後三朵已開的花並不相鄰。
        /// 第 12 天後：[x, x, x, x, x, x, x]。可以用不同方式製作兩束花。
        ///
        /// 限制條件：
        /// - bloomDay.length == n
        /// - 1 &lt;= n &lt;= 10^5
        /// - 1 &lt;= bloomDay[i] &lt;= 10^9
        /// - 1 &lt;= m &lt;= 10^6
        /// - 1 &lt;= k &lt;= n
        /// </para>
        /// </summary>
        /// <remarks>
        /// 以固定案例比較答案範圍二分搜尋與排序候選日兩種解法，並驗證兩者都不會修改輸入陣列。
        /// </remarks>
        /// <param name="args">命令列參數；此範例程式不使用。</param>
        static void Main(string[] args)
        {
            (string Name, int[] BloomDay, int M, int K, int Expected)[] cases =
            [
                ("官方範例一：每束只需要一朵花", [1, 10, 3, 10, 2], 3, 1, 3),
                ("官方範例二：花朵總數不足", [1, 10, 3, 10, 2], 3, 2, -1),
                ("官方範例三：花朵必須相鄰", [7, 7, 7, 7, 12, 7, 7], 2, 3, 12),
                ("重複值：同一天可完成所有花束", [1, 1, 1, 1], 2, 2, 1),
                ("邊界值：開花日為十億", [1_000_000_000, 1_000_000_000], 1, 2, 1_000_000_000),
                ("防禦性案例：空陣列", [], 1, 1, -1)
            ];

            int passedChecks = 0;
            int totalChecks = cases.Length * 4;

            for (int i = 0; i < cases.Length; i++)
            {
                passedChecks += RunCase(
                    i + 1,
                    cases[i].Name,
                    cases[i].BloomDay,
                    cases[i].M,
                    cases[i].K,
                    cases[i].Expected);
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 執行單一測試案例，分別呼叫兩種解法並比較預期答案，同時確認各自收到的輸入副本保持不變。
        /// 輸入可為空陣列；回傳本案例通過的檢查數，範圍為 0 到 4。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="name">案例用途或情境說明。</param>
        /// <param name="bloomDay">每朵花的開花日期。</param>
        /// <param name="m">需要製作的花束數量。</param>
        /// <param name="k">每束花需要的相鄰花朵數量。</param>
        /// <param name="expected">預期的最少完成天數；無法完成時為 -1。</param>
        /// <returns>兩個答案檢查與兩個輸入不變檢查中，通過的項目數。</returns>
        private static int RunCase(int caseNumber, string name, int[] bloomDay, int m, int k, int expected)
        {
            int[] binarySearchInput = [.. bloomDay];
            int[] sortedCandidatesInput = [.. bloomDay];
            int binarySearchResult = MinDays(binarySearchInput, m, k);
            int sortedCandidatesResult = MinDays2(sortedCandidatesInput, m, k);

            bool binarySearchResultPassed = binarySearchResult == expected;
            bool binarySearchInputPassed = binarySearchInput.SequenceEqual(bloomDay);
            bool sortedCandidatesResultPassed = sortedCandidatesResult == expected;
            bool sortedCandidatesInputPassed = sortedCandidatesInput.SequenceEqual(bloomDay);

            Console.WriteLine($"Case {caseNumber}: {name}");
            Console.WriteLine($"Input: bloomDay = {FormatArray(bloomDay)}, m = {m}, k = {k}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"MinDays Actual: {binarySearchResult}");
            Console.WriteLine($"MinDays Result: {(binarySearchResultPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"MinDays Input unchanged: {(binarySearchInputPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"MinDays2 Actual: {sortedCandidatesResult}");
            Console.WriteLine($"MinDays2 Result: {(sortedCandidatesResultPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"MinDays2 Input unchanged: {(sortedCandidatesInputPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return Convert.ToInt32(binarySearchResultPassed)
                + Convert.ToInt32(binarySearchInputPassed)
                + Convert.ToInt32(sortedCandidatesResultPassed)
                + Convert.ToInt32(sortedCandidatesInputPassed);
        }

        /// <summary>
        /// 將整數陣列格式化為固定的方括號表示法，供測試輸出與 README 執行紀錄使用。
        /// 輸入必須是非 null 陣列；空陣列輸出為 <c>[]</c>。
        /// </summary>
        /// <param name="values">要格式化的整數陣列。</param>
        /// <returns>以逗號與空格分隔元素的字串。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 使用答案範圍二分搜尋，找出製作 <paramref name="m"/> 束花所需的最少天數。
        /// 可製作花束的狀態會隨天數增加而保持成立，因此可在最小與最大開花日之間搜尋第一個可行日。
        /// 輸入必須是非 null 陣列，且 <paramref name="m"/>、<paramref name="k"/> 為正整數；方法不會修改輸入。
        /// </summary>
        /// <param name="bloomDay">每朵花的開花日期，索引相鄰代表花朵相鄰。</param>
        /// <param name="m">需要製作的花束數量。</param>
        /// <param name="k">每束花需要的相鄰花朵數量。</param>
        /// <returns>可完成所有花束的最少天數；花朵總數不足時回傳 -1。</returns>
        public static int MinDays(int[] bloomDay, int m, int k)
        {
            // 先以 long 計算需求量，避免 m * k 在 int 範圍內溢位後誤判為可行。
            if ((long)m * k > bloomDay.Length)
            {
                return -1;
            }

            int low = int.MaxValue, high = 0;
            int length = bloomDay.Length;
            for (int i = 0; i < length; i++)
            {
                low = Math.Min(low, bloomDay[i]);
                high = Math.Max(high, bloomDay[i]);
            }

            while (low < high)
            {
                int days = (high - low) / 2 + low;

                // days 可行時答案仍可能更小；不可行時則排除 days 以前的所有日期。
                if (CanMake(bloomDay, days, m, k))
                {
                    high = days;
                }
                else
                {
                    low = days + 1;
                }
            }

            return low;
        }

        /// <summary>
        /// 複製並排序所有開花日，再由小到大測試不重複的候選日期，找出製作指定花束數量的最少天數。
        /// 答案只可能在某朵花開花時改變，因此不需逐一枚舉兩個候選值之間的日期。
        /// 輸入必須是非 null 陣列，且 <paramref name="m"/>、<paramref name="k"/> 為正整數；方法不會修改輸入。
        /// </summary>
        /// <param name="bloomDay">每朵花的開花日期，索引相鄰代表花朵相鄰。</param>
        /// <param name="m">需要製作的花束數量。</param>
        /// <param name="k">每束花需要的相鄰花朵數量。</param>
        /// <returns>第一個可完成所有花束的候選日期；花朵總數不足或沒有候選日期時回傳 -1。</returns>
        public static int MinDays2(int[] bloomDay, int m, int k)
        {
            if ((long)m * k > bloomDay.Length)
            {
                return -1;
            }

            int[] candidateDays = [.. bloomDay];
            Array.Sort(candidateDays);

            for (int i = 0; i < candidateDays.Length; i++)
            {
                // 相同開花日不會改變可用花朵集合，只需檢查一次。
                if (i > 0 && candidateDays[i] == candidateDays[i - 1])
                {
                    continue;
                }

                if (CanMake(bloomDay, candidateDays[i], m, k))
                {
                    return candidateDays[i];
                }
            }

            return -1;
        }

        /// <summary>
        /// 判斷在指定天數內，是否能以互不重複的相鄰花朵製作至少 <paramref name="m"/> 束花。
        /// 由左至右累積已開花的連續區段；遇到未開花元素便中斷，湊滿 <paramref name="k"/> 朵時立即形成一束。
        /// 輸入必須是非 null 陣列，且數量參數為正整數；回傳布林結果且不修改輸入。
        /// </summary>
        /// <param name="bloomDay">每朵花的開花日期，索引相鄰代表花朵相鄰。</param>
        /// <param name="days">目前允許等待的天數。</param>
        /// <param name="m">需要製作的花束數量。</param>
        /// <param name="k">每束花需要的相鄰花朵數量。</param>
        /// <returns>若指定天數內可以製作至少 <paramref name="m"/> 束花則為 true；否則為 false。</returns>
        public static bool CanMake(int[] bloomDay, int days, int m, int k)
        {
            int bouquets = 0;
            int flowers = 0;
            int length = bloomDay.Length;

            for (int i = 0; i < length && bouquets < m; i++)
            {
                if (bloomDay[i] <= days)
                {
                    flowers++;

                    if (flowers == k)
                    {
                        // 已使用的花不能重複放入下一束，完成一束後重新累積。
                        bouquets++;
                        flowers = 0;
                    }
                }
                else
                {
                    // 未開花的位置會切斷相鄰區段。
                    flowers = 0;
                }
            }

            return bouquets >= m;
        }




    }
}