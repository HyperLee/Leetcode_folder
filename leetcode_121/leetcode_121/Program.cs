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
        /// 121. Best Time to Buy and Sell Stock
        /// https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
        /// 121. 买卖股票的最佳时机
        /// https://leetcode.cn/problems/best-time-to-buy-and-sell-stock/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 7, 1, 5, 3, 6, 4 };
            Console.WriteLine(MaxProfit2(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 兩層迴圈
        /// 找出最大獲利者
        /// 缺點: 超時
        /// 
        /// 最小值 預設給 最大
        /// 最大值 預設給 最小
        /// 這樣之後 才有辦法更新狀態數值
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
        /// 
        /// 最大獲利 = 當前價格 - 最小價格
        /// 
        /// 最小值 預設給 最大
        /// 最大值 預設給 最小
        /// 這樣之後 才有辦法更新狀態數值
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int MaxProfit2(int[] prices)
        {
            int minprice = int.MaxValue; // int.MaxValue = 2147483647
            int maxprofit = 0;

            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < minprice)
                {
                    // 更新最小價格
                    minprice = prices[i];
                }
                else if (prices[i] - minprice > maxprofit)
                {
                    // 最大獲利 = 當前價格 - 最小價格
                    maxprofit = prices[i] - minprice;
                }
            }
            return maxprofit;

        }


        /// <summary>
        /// 概念類似 方法二
        /// 
        /// 最大獲利 = 當前價格 - 最小價格
        /// 
        /// 最小值 預設給 最大
        /// 最大值 預設給 最小
        /// 這樣之後 才有辦法更新狀態數值
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int MaxProfit3(int[] prices)
        {
            int minprice = int.MaxValue;  // int.MaxValue = 2147483647
            int maxprofit = 0;
            
            foreach(int price in prices)
            {
                // 最大獲利; 
                maxprofit = Math.Max(maxprofit, price - minprice);

                // 最小價格
                minprice = Math.Min(minprice, price);
            }

            return maxprofit;
        }

    }
}
