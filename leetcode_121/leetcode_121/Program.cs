using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_121
{
    internal class Program
    {
        /// <summary>
        /// leetcode 121
        /// https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
        /// https://leetcode.cn/problems/best-time-to-buy-and-sell-stock/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 7, 1, 5, 3, 6, 4 };
            Console.WriteLine(MaxProfit3(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 兩層迴圈
        /// 找出最大獲利者
        /// 缺點: 超時
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int MaxProfit(int[] prices)
        {
            int maxprofits = 0;
            for(int i = 0; i < prices.Length - 1; i++)
            {
                for(int j = i + 1; j < prices.Length; j ++)
                {
                    int profits = prices[j] - prices[i];
                    maxprofits = Math.Max(maxprofits, profits);
                }
            }
            
            return maxprofits;
        }


        /// <summary>
        /// https://leetcode.cn/problems/best-time-to-buy-and-sell-stock/solution/121-mai-mai-gu-piao-de-zui-jia-shi-ji-by-leetcode-/
        /// 方法2:
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int MaxProfit2(int[] prices)
        {
            int minprice = int.MaxValue;
            int maxprofit = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < minprice)
                {
                    minprice = prices[i];
                }
                else if (prices[i] - minprice > maxprofit)
                {
                    maxprofit = prices[i] - minprice;
                }
            }
            return maxprofit;

        }


        /// <summary>
        /// 概念類似 方法二
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int MaxProfit3(int[] prices)
        {
            int minprice = int.MaxValue;
            int maxprofit = 0;
            
            foreach(int price in prices)
            {
                maxprofit = Math.Max(maxprofit, price - minprice);
                minprice = Math.Min(minprice, price);
            }

            return maxprofit;
        }

    }
}
