namespace leetcode_121
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 121. Best Time to Buy and Sell Stock
        /// https://leetcode.com/problems/best-time-to-buy-and-sell-stock/description/
        ///
        /// You are given an array prices where prices[i] is the price of a given stock on the i-th day.
        /// You want to maximize your profit by choosing a single day to buy one stock and choosing a different
        /// day in the future to sell that stock.
        /// Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit,
        /// return 0.
        ///
        /// Example 1:
        /// Input: prices = [7,1,5,3,6,4]
        /// Output: 5
        /// Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6 - 1 = 5.
        /// Note that buying on day 2 and selling on day 1 is not allowed because you must buy before you sell.
        ///
        /// Example 2:
        /// Input: prices = [7,6,4,3,1]
        /// Output: 0
        /// Explanation: In this case, no transactions are done and the max profit = 0.
        ///
        /// Constraints:
        /// 1 &lt;= prices.length &lt;= 10^5
        /// 0 &lt;= prices[i] &lt;= 10^4
        /// </para>
        /// <para>
        /// 121. 買賣股票的最佳時機
        /// https://leetcode.cn/problems/best-time-to-buy-and-sell-stock/description/
        ///
        /// 給定陣列 prices，其中 prices[i] 是某支股票在第 i 天的價格。
        /// 你想藉由選擇某一天買入一股股票，並選擇未來不同的一天賣出該股票，讓利潤最大化。
        /// 請回傳這筆交易可獲得的最大利潤。若無法獲得任何利潤，則回傳 0。
        ///
        /// 範例 1：
        /// 輸入：prices = [7,1,5,3,6,4]
        /// 輸出：5
        /// 解釋：在第 2 天買入（價格 = 1），並在第 5 天賣出（價格 = 6），
        /// 利潤 = 6 - 1 = 5。
        /// 請注意，不允許在第 2 天買入並在第 1 天賣出，因為必須先買入才能賣出。
        ///
        /// 範例 2：
        /// 輸入：prices = [7,6,4,3,1]
        /// 輸出：0
        /// 解釋：在此情況下不進行任何交易，最大利潤 = 0。
        ///
        /// 限制條件：
        /// 1 &lt;= prices.length &lt;= 10^5
        /// 0 &lt;= prices[i] &lt;= 10^4
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行固定的股票價格範例，依序呼叫三種最大獲利解法，
        /// 並比較每個方法的實際結果與預期結果，最後輸出通過項目總數。
        /// 輸入案例包含官方範例、邊界情境、重複價格，以及空陣列防禦性案例。
        /// </summary>
        private static void RunSamples()
        {
            (string Description, int[] Prices, int Expected)[] samples =
            [
                ("一般價格波動", [7, 1, 5, 3, 6, 4], 5),
                ("價格持續下跌", [7, 6, 4, 3, 1], 0),
                ("只有一天價格", [5], 0),
                ("兩天價格上漲", [1, 2], 1),
                ("先上漲後出現更低價格", [2, 4, 1], 2),
                ("包含重複價格與多個低點", [3, 3, 5, 0, 0, 3, 1, 4], 4),
                ("空陣列（防禦性案例，非 LeetCode 官方輸入）", [], 0)
            ];

            (string Name, Func<int[], int> Calculate)[] solutions =
            [
                (nameof(MaxProfit), MaxProfit),
                (nameof(MaxProfit2), MaxProfit2),
                (nameof(MaxProfit3), MaxProfit3)
            ];

            int passedChecks = 0;
            int totalChecks = samples.Length * solutions.Length;

            Console.WriteLine("LeetCode 121：買賣股票的最佳時機");
            Console.WriteLine();

            for (int sampleIndex = 0; sampleIndex < samples.Length; sampleIndex++)
            {
                (string description, int[] prices, int expected) = samples[sampleIndex];

                Console.WriteLine($"案例 {sampleIndex + 1}：{description}");
                Console.WriteLine($"輸入：prices = {FormatPrices(prices)}");
                Console.WriteLine($"預期：{expected}");

                foreach ((string name, Func<int[], int> calculate) in solutions)
                {
                    int actual = calculate(prices);
                    bool passed = actual == expected;

                    if (passed)
                    {
                        passedChecks++;
                    }

                    Console.WriteLine($"  {name}：{actual} => {(passed ? "PASS" : "FAIL")}");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        }

        /// <summary>
        /// 將股票價格陣列格式化為 README 與主控台使用的方括號表示法。
        /// 輸入必須是非 <see langword="null"/> 的整數陣列；空陣列會格式化為 <c>[]</c>。
        /// </summary>
        /// <param name="prices">依日期排列的股票價格陣列。</param>
        /// <returns>以逗號分隔並包在方括號內的價格字串。</returns>
        private static string FormatPrices(int[] prices)
        {
            return $"[{string.Join(", ", prices)}]";
        }

        /// <summary>
        /// 使用兩層迴圈枚舉所有合法的買入日與未來賣出日，求出單次交易的最大獲利。
        /// 輸入必須是非 <see langword="null"/> 且依日期排序的價格陣列；
        /// 若沒有可獲利的交易、只有一天或陣列為空，則回傳 <c>0</c>。
        /// 此解法的時間複雜度為 O(n²)，空間複雜度為 O(1)。
        /// </summary>
        /// <param name="prices">第 <c>i</c> 個元素代表第 <c>i</c> 天股票價格的陣列。</param>
        /// <returns>先買後賣且最多交易一次所能取得的最大獲利。</returns>
        public static int MaxProfit(int[] prices)
        {
            int maxProfit = 0;

            for (int buyDay = 0; buyDay < prices.Length - 1; buyDay++)
            {
                // 賣出日必須晚於買入日，確保枚舉的交易順序符合題意。
                for (int sellDay = buyDay + 1; sellDay < prices.Length; sellDay++)
                {
                    int profit = prices[sellDay] - prices[buyDay];
                    maxProfit = Math.Max(maxProfit, profit);
                }
            }

            return maxProfit;
        }

        /// <summary>
        /// 使用索引式單次掃描，在走訪每一天時維護先前出現過的最低買入價格，
        /// 並以當天價格減去最低價格更新最大獲利。
        /// 輸入必須是非 <see langword="null"/> 且依日期排序的價格陣列；
        /// 若沒有可獲利的交易、只有一天或陣列為空，則回傳 <c>0</c>。
        /// 此解法的時間複雜度為 O(n)，空間複雜度為 O(1)。
        /// </summary>
        /// <param name="prices">第 <c>i</c> 個元素代表第 <c>i</c> 天股票價格的陣列。</param>
        /// <returns>先買後賣且最多交易一次所能取得的最大獲利。</returns>
        public static int MaxProfit2(int[] prices)
        {
            int minPrice = int.MaxValue;
            int maxProfit = 0;

            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < minPrice)
                {
                    // 最低價格只涵蓋當天以前，代表目前可採用的最佳買入成本。
                    minPrice = prices[i];
                }
                else if (prices[i] - minPrice > maxProfit)
                {
                    maxProfit = prices[i] - minPrice;
                }
            }

            return maxProfit;
        }

        /// <summary>
        /// 使用 <see langword="foreach"/> 單次掃描價格，在每一天同時以
        /// <see cref="Math.Max(int, int)"/> 與 <see cref="Math.Min(int, int)"/>
        /// 維護最大獲利及歷史最低價格。
        /// 輸入必須是非 <see langword="null"/> 且依日期排序的價格陣列；
        /// 若沒有可獲利的交易、只有一天或陣列為空，則回傳 <c>0</c>。
        /// 此解法的時間複雜度為 O(n)，空間複雜度為 O(1)。
        /// </summary>
        /// <param name="prices">第 <c>i</c> 個元素代表第 <c>i</c> 天股票價格的陣列。</param>
        /// <returns>先買後賣且最多交易一次所能取得的最大獲利。</returns>
        public static int MaxProfit3(int[] prices)
        {
            int minPrice = int.MaxValue;
            int maxProfit = 0;

            foreach (int price in prices)
            {
                // 先納入當天價格，再計算候選獲利；同一天買賣只會產生 0，不影響答案。
                minPrice = Math.Min(minPrice, price);
                maxProfit = Math.Max(maxProfit, price - minPrice);
            }

            return maxProfit;
        }
    }
}