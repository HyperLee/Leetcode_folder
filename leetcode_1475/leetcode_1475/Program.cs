namespace leetcode_1475
{
    internal class Program
    {
        /// <summary>
        /// 1475. Final Prices With a Special Discount in a Shop
        /// https://leetcode.com/problems/final-prices-with-a-special-discount-in-a-shop/description/?envType=daily-question&envId=2024-12-18
        /// 
        /// 1475. 商品折扣后的最终价格
        /// https://leetcode.cn/problems/final-prices-with-a-special-discount-in-a-shop/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] prices = { 8, 4, 6, 2, 3 };
            //Console.WriteLine("res: " + FinalPrices(prices));

            var res = FinalPrices(prices);
            Console.Write("res: [");
            foreach(int value in res)
            {
                Console.Write(value + ", ");
            }
            Console.WriteLine("]");
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/final-prices-with-a-special-discount-in-a-shop/solutions/1788169/shang-pin-zhe-kou-hou-de-zui-zhong-jie-g-ind3/
        /// https://leetcode.cn/problems/final-prices-with-a-special-discount-in-a-shop/solutions/1790738/by-ac_oier-hw5b/
        /// https://leetcode.cn/problems/final-prices-with-a-special-discount-in-a-shop/solutions/2642423/1475-shang-pin-zhe-kou-hou-de-zui-zhong-ln4to/
        /// 
        /// 方法一:直接模擬題目敘述 使用兩個迴圈來跑整個輸入資料 遍歷
        /// 
        /// 折扣條件:
        /// 如果你要买第 i 件商品，那么你可以得到与 prices[j] 相等的折扣，
        /// 1. j 是满足 j > i 
        /// 2. prices[j] <= prices[i] 
        /// 3. 如果没有满足条件的 j ，你将没有任何折扣。
        /// 
        /// 时间复杂度：O(n^2)，其中 n 为数组的长度。对于每个商品，我们需要遍历一遍数组查找符合题目要求的折扣。
        /// 空间复杂度：O(1)。返回值不计入空间复杂度。
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int[] FinalPrices(int[] prices)
        {
            int n = prices.Length;
            int[] res = new int[n];

            for(int i = 0; i < n; i++)
            {
                // 折扣
                int discount = 0;
                // 從 i + 1 開始向後遍歷, 找出 prices[j] <= prices[i] 的 index 
                for(int j = i + 1; j < n; j++)
                {
                    // 符合折扣條件
                    if (prices[j] <= prices[i])
                    {
                        discount = prices[j];
                        break;
                    }
                }

                res[i] = prices[i] - discount;
            }

            return res;
        }
    }
}
