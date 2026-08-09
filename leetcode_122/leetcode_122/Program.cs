namespace leetcode_122
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 122. Best Time to Buy and Sell Stock II
        /// https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/description/
        ///
        /// You are given an integer array prices where prices[i] is the price of a given stock on the i-th day.
        /// On each day, you may decide to buy and/or sell the stock. You can only hold at most one share of the
        /// stock at any time. However, you can sell and buy the stock multiple times on the same day, ensuring
        /// you never hold more than one share of the stock.
        /// Find and return the maximum profit you can achieve.
        ///
        /// Example 1:
        /// Input: prices = [7,1,5,3,6,4]
        /// Output: 7
        /// Explanation: Buy on day 2 (price = 1) and sell on day 3 (price = 5), profit = 5 - 1 = 4.
        /// Then buy on day 4 (price = 3) and sell on day 5 (price = 6), profit = 6 - 3 = 3.
        /// Total profit is 4 + 3 = 7.
        ///
        /// Example 2:
        /// Input: prices = [1,2,3,4,5]
        /// Output: 4
        /// Explanation: Buy on day 1 (price = 1) and sell on day 5 (price = 5), profit = 5 - 1 = 4.
        /// Total profit is 4.
        ///
        /// Example 3:
        /// Input: prices = [7,6,4,3,1]
        /// Output: 0
        /// Explanation: There is no way to make a positive profit, so we never buy the stock to achieve the
        /// maximum profit of 0.
        ///
        /// Constraints:
        /// 1 &lt;= prices.length &lt;= 3 * 10^4
        /// 0 &lt;= prices[i] &lt;= 10^4
        /// </para>
        /// <para>
        /// 122. 買賣股票的最佳時機 II
        /// https://leetcode.cn/problems/best-time-to-buy-and-sell-stock-ii/description/
        ///
        /// 給定整數陣列 prices，其中 prices[i] 是某支股票在第 i 天的價格。
        /// 每一天都可以決定買入及／或賣出股票。任何時候最多只能持有一股股票。不過，可以在同一天
        /// 多次賣出與買入股票，只要確保持有的股票不超過一股。
        /// 請找出並回傳可獲得的最大利潤。
        ///
        /// 範例 1：
        /// 輸入：prices = [7,1,5,3,6,4]
        /// 輸出：7
        /// 解釋：在第 2 天買入（價格 = 1），第 3 天賣出（價格 = 5），利潤 = 5 - 1 = 4。
        /// 接著在第 4 天買入（價格 = 3），第 5 天賣出（價格 = 6），利潤 = 6 - 3 = 3。
        /// 總利潤為 4 + 3 = 7。
        ///
        /// 範例 2：
        /// 輸入：prices = [1,2,3,4,5]
        /// 輸出：4
        /// 解釋：在第 1 天買入（價格 = 1），第 5 天賣出（價格 = 5），利潤 = 5 - 1 = 4。
        /// 總利潤為 4。
        ///
        /// 範例 3：
        /// 輸入：prices = [7,6,4,3,1]
        /// 輸出：0
        /// 解釋：沒有任何方法可以獲得正利潤，因此完全不買入股票，以取得最大利潤 0。
        ///
        /// 限制條件：
        /// 1 &lt;= prices.length &lt;= 3 * 10^4
        /// 0 &lt;= prices[i] &lt;= 10^4
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            const int totalChecks = 12;
            int passedChecks = RunSamples();

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// 執行六組固定股價案例，分別驗證貪心法與動態規劃法是否得到預期的最大利潤，
        /// 並回傳通過的檢查總數。
        /// </summary>
        /// <returns>兩種解法合計通過的檢查數，最大值為 12。</returns>
        private static int RunSamples()
        {
            SampleCase[] sampleCases =
            [
                new("官方範例一", [7, 1, 5, 3, 6, 4], 7),
                new("官方範例二", [1, 2, 3, 4, 5], 4),
                new("官方範例三", [7, 6, 4, 3, 1], 0),
                new("單日價格", [5], 0),
                new("重複價格", [2, 2, 3, 3, 1, 4], 4),
                new("零價多波段", [0, 4, 0, 4], 8)
            ];

            int passedChecks = 0;

            for (int i = 0; i < sampleCases.Length; i++)
            {
                passedChecks += RunSample(i + 1, sampleCases[i]);
            }

            return passedChecks;
        }

        /// <summary>
        /// 以單組非空股價資料呼叫兩種最大利潤解法，比對預期結果、輸出 PASS/FAIL，
        /// 並回傳本案例通過的解法數。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="sampleCase">案例名稱、符合題目限制的股價陣列與預期最大利潤。</param>
        /// <returns>本案例通過的檢查數，範圍為 0 到 2。</returns>
        private static int RunSample(int caseNumber, SampleCase sampleCase)
        {
            int greedyResult = MaxProfit(sampleCase.Prices);
            int dynamicProgrammingResult = MaxProfit2(sampleCase.Prices);
            bool greedyPassed = greedyResult == sampleCase.Expected;
            bool dynamicProgrammingPassed = dynamicProgrammingResult == sampleCase.Expected;

            Console.WriteLine($"案例 {caseNumber}：{sampleCase.Name}");
            Console.WriteLine($"prices = {FormatPrices(sampleCase.Prices)}");
            Console.WriteLine($"預期結果：{sampleCase.Expected}");
            Console.WriteLine($"MaxProfit（貪心）：{greedyResult} => {FormatStatus(greedyPassed)}");
            Console.WriteLine($"MaxProfit2（動態規劃）：{dynamicProgrammingResult} => {FormatStatus(dynamicProgrammingPassed)}");
            Console.WriteLine();

            return Convert.ToInt32(greedyPassed) + Convert.ToInt32(dynamicProgrammingPassed);
        }

        /// <summary>
        /// 將符合題目限制的股價陣列格式化為便於主控台與 README 閱讀的方括號字串。
        /// </summary>
        /// <param name="prices">至少包含一天價格的整數陣列。</param>
        /// <returns>以逗號與空格分隔的股價字串，例如 <c>[7, 1, 5]</c>。</returns>
        private static string FormatPrices(int[] prices)
        {
            return $"[{string.Join(", ", prices)}]";
        }

        /// <summary>
        /// 將單一解法的比對結果轉換為可讀的 PASS 或 FAIL 標記。
        /// </summary>
        /// <param name="passed">實際結果是否等於預期結果。</param>
        /// <returns>通過時回傳 <c>PASS</c>，否則回傳 <c>FAIL</c>。</returns>
        private static string FormatStatus(bool passed)
        {
            return passed ? "PASS" : "FAIL";
        }

        /// <summary>
        /// 保存一組可執行案例的名稱、非空股價陣列與預期最大利潤。
        /// </summary>
        /// <param name="Name">案例名稱。</param>
        /// <param name="Prices">符合題目限制的股價陣列。</param>
        /// <param name="Expected">預期可取得的最大利潤。</param>
        private readonly record struct SampleCase(string Name, int[] Prices, int Expected);

        /// <summary>
        /// 計算可無限次交易但同時最多持有一股時的最大利潤。
        /// 貪心概念是收集每一組相鄰日期的正價差；輸入須為長度 1 到 30,000、
        /// 每個價格介於 0 到 10,000 的整數陣列。
        /// </summary>
        /// <param name="prices">依日期排列且符合題目限制的非空股價陣列。</param>
        /// <returns>完成任意次合法交易後可取得的最大總利潤。</returns>
        public static int MaxProfit(int[] prices)
        {
            int totalProfit = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                // 無限次交易時，每一段上漲都可獨立收集，合計後等同整段低買高賣。
                totalProfit += Math.Max(prices[i] - prices[i - 1], 0);
            }

            return totalProfit;
        }

        /// <summary>
        /// 以動態規劃計算可無限次交易但同時最多持有一股時的最大利潤。
        /// 每天維護收盤後未持股的 <c>cash</c> 與持股的 <c>hold</c> 最佳值；
        /// 輸入須為長度 1 到 30,000、每個價格介於 0 到 10,000 的整數陣列。
        /// </summary>
        /// <param name="prices">依日期排列且符合題目限制的非空股價陣列。</param>
        /// <returns>最後一天結束且未持股時可取得的最大總利潤。</returns>
        public static int MaxProfit2(int[] prices)
        {
            int cash = 0;
            int hold = -prices[0];

            for (int i = 1; i < prices.Length; i++)
            {
                // 兩個新狀態必須同時由前一日快照轉移，避免更新順序改變狀態定義。
                int previousCash = cash;
                int previousHold = hold;

                cash = Math.Max(previousCash, previousHold + prices[i]);
                hold = Math.Max(previousHold, previousCash - prices[i]);
            }

            return cash;
        }
    }
}