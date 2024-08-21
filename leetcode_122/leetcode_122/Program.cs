namespace leetcode_122
{
    internal class Program
    {
        /// <summary>
        /// 122. Best Time to Buy and Sell Stock II
        /// https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/description/?envType=study-plan-v2&envId=top-interview-150
        /// 
        /// 122. 买卖股票的最佳时机 II
        /// https://leetcode.cn/problems/best-time-to-buy-and-sell-stock-ii/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 7, 1, 5, 3, 6, 4 };

            Console.WriteLine(MaxProfit(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 不限制交易次數, 也就是可以高頻交易
        /// 簡單說就是 只要能夠達成
        /// 買低賣高 賺取價差即可
        /// 隨時可以買以及隨時可以賣
        /// 
        /// 只能持有一支股票, 所以買的時候只能買一次
        /// 不能同時買多隻
        /// 
        /// 每天價格依據輸入陣列 prices 順序顯示
        /// 這樣的話就直接
        /// 計算每天的獲利差 
        /// 也就是
        /// 後面 減 前面 即可
        /// 然後獲利至少要大於 0
        /// 不然就是沒獲利
        /// 最後把全部獲利累積加總即可
        /// 
        /// ref:
        /// https://leetcode.cn/problems/best-time-to-buy-and-sell-stock-ii/solutions/476791/mai-mai-gu-piao-de-zui-jia-shi-ji-ii-by-leetcode-s/
        /// https://leetcode.cn/problems/best-time-to-buy-and-sell-stock-ii/solutions/38498/tan-xin-suan-fa-by-liweiwei1419-2/
        /// https://leetcode.cn/problems/best-time-to-buy-and-sell-stock-ii/solutions/2367130/122-mai-mai-gu-piao-de-zui-jia-shi-ji-ii-fwxq/
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int MaxProfit(int[] prices)
        {
            int totalprofits = 0;

            // i 從 1 開始
            for(int i = 1; i < prices.Length; i++)
            {
                // 後面減前面, 獲利 > 0. 累積加總
                totalprofits += Math.Max(prices[i] - prices[i - 1], 0);
            }

            return totalprofits;
        }


    }
}
