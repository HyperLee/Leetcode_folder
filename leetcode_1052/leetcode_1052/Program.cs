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
            int[] customers = { 1, 0, 1, 2, 1, 1, 7, 5 };
            int[] grumpy = { 0, 1, 0, 1, 0, 1, 0, 1 };
            int minutes = 3;

            Console.WriteLine(MaxSatisfied(customers, grumpy, minutes));
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/grumpy-bookstore-owner/solutions/615133/ai-sheng-qi-de-shu-dian-lao-ban-by-leetc-dloq/
        /// https://leetcode.cn/problems/grumpy-bookstore-owner/solutions/616238/yong-mi-mi-ji-qiao-wan-liu-zhu-zui-duo-d-py41/
        /// https://leetcode.cn/problems/grumpy-bookstore-owner/solutions/2751888/ding-chang-hua-dong-chuang-kou-fu-ti-dan-rch7/
        /// https://leetcode.cn/problems/grumpy-bookstore-owner/solutions/1725686/by-stormsunshine-sz84/
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="grumpy"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int MaxSatisfied(int[] customers, int[] grumpy, int minutes)
        {
            int original = 0;
            int n = customers.Length;

            // 統計 不使用密技 原先就滿意狀態顧客
            for(int i = 0; i < n; i++)
            {
                // 為0代表老闆不生氣, 增加滿意度
                if (grumpy[i] == 0)
                {
                    original += customers[i];
                }
            }

            // 使用 時間密技
            int increase = 0;
            for(int i = 0; i < minutes; i++)
            {
                if (grumpy[i] == 1)
                {
                    // 原本為1的,但是用了密技 變成滿意狀態
                    increase += customers[i];
                }
            }

            int maxIncrease = increase;
            // 使用密技之後 真正能留下的顧客數量
            for (int i = minutes; i < n; i++)
            {
                if (grumpy[i - minutes] == 1)
                {
                    increase -= customers[i - minutes];
                }

                if (grumpy[i] == 1)
                {
                    increase += customers[i];
                }
                maxIncrease = Math.Max(maxIncrease, increase);
            }

            return original + maxIncrease;
        }
    }
}
