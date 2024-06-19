namespace leetcode_1482
{
    internal class Program
    {
        /// <summary>
        /// 1482. Minimum Number of Days to Make m Bouquets
        /// https://leetcode.com/problems/minimum-number-of-days-to-make-m-bouquets/description/?envType=daily-question&envId=2024-06-19
        /// 1482. 制作 m 束花所需的最少天数
        /// https://leetcode.cn/problems/minimum-number-of-days-to-make-m-bouquets/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] bloomDay = { 1, 10, 3, 10, 2 };
            int m = 3;
            int k = 1;
            Console.WriteLine(MinDays(bloomDay, m, k));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/minimum-number-of-days-to-make-m-bouquets/solutions/764671/zhi-zuo-m-shu-hua-suo-xu-de-zui-shao-tia-mxci/
        /// https://leetcode.cn/problems/minimum-number-of-days-to-make-m-bouquets/solutions/1608288/by-stormsunshine-6ovj/
        /// 
        /// 二分法查找所需要之天數
        /// low = bloomDay最小值
        /// high = bloomDay最大值
        /// 當 low = high時候 結束條件, low為最小天數
        /// </summary>
        /// <param name="bloomDay">花園中有n朵花</param>
        /// <param name="m">幾束花</param>
        /// <param name="k">每束花 需要幾朵</param>
        /// <returns></returns>
        public static int MinDays(int[] bloomDay, int m, int k)
        {
            // 製作花束需要花朵總量 要小於 花園花朵總量, 不然會不夠用
            // 注意,這邊要取long. 位數太長 int會出錯 溢位問題
            if ((long)m * k > bloomDay.Length)
            {
                return -1;
            }

            int low = int.MaxValue, high = 0;
            int length = bloomDay.Length;
            for (int i = 0; i < length; i++)
            {
                // 遍歷 找出 low , hight 初始值
                low = Math.Min(low, bloomDay[i]);
                high = Math.Max(high, bloomDay[i]);
            }

            // 二分法找出最小天數
            while (low < high)
            {
                int days = (high - low) / 2 + low;
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
        /// 
        /// </summary>
        /// <param name="bloomDay">花園</param>
        /// <param name="days">需要天數</param>
        /// <param name="m">幾束花</param>
        /// <param name="k">每束花 需要幾朵</param>
        /// <returns></returns>
        public static bool CanMake(int[] bloomDay, int days, int m, int k)
        {
            int bouquets = 0;
            int flowers = 0;
            int length = bloomDay.Length;

            // 花束 小於 m
            for (int i = 0; i < length && bouquets < m; i++)
            {
                // 找出可以湊出花束的天數, 這邊先不用管大小,
                // 只要可以湊出即可
                if (bloomDay[i] <= days)
                {
                    // 花朵++
                    flowers++;

                    if (flowers == k)
                    {
                        // 花朵累積數量達到 k
                        // 就變成花束
                        bouquets++;
                        flowers = 0;
                    }
                }
                else
                {
                    flowers = 0;
                }
            }

            return bouquets >= m;
        }




    }
}
