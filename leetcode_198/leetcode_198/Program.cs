using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_198
{
    internal class Program
    {
        /// <summary>
        /// 198. House Robber
        /// https://leetcode.com/problems/house-robber/description/?envType=daily-question&envId=2024-01-21
        /// 198. 打家劫舍
        /// https://leetcode.cn/problems/house-robber/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 1, 2, 3, 1};
            Console.WriteLine("method1: " + Rob(input));
            Console.WriteLine("method2: " + Rob2(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 來源解法:
        /// https://leetcode.cn/problems/house-robber/solutions/2094726/198-da-jia-jie-she-by-stormsunshine-entl/
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int Rob(int[] nums)
        {
            int n = nums.Length;
            
            // 總長度只有1, 直接回傳那一間的數值
            if(n == 1)
            {
                return nums[0];
            }

            int[] dp = new int[n];
            dp[0] = nums[0];
            // 長度2, 取出這兩間最大者
            dp[1] = Math.Max(nums[0], nums[1]);

            for(int i = 2; i < n; i++)
            {
                // 前者是偷竊第 i 間, 前一輪總和 + 第 i 間的價值
                // 後者是不偷竊最後的第 i 間
                dp[i] = Math.Max(dp[i - 2] + nums[i], dp[i - 1]);
            }

            return dp[n - 1];
        }


        /// <summary>
        /// 上方解法 
        /// 最大值加總方式 有沒有覺得很類似 fibonacci 
        /// 既然是這樣 那就修改寫法 優化
        /// 
        /// 參考
        /// https://leetcode.cn/problems/house-robber/solutions/263856/da-jia-jie-she-by-leetcode-solution/
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int Rob2(int[] nums)
        {
            int n = nums.Length;
            if(n == 1)
            {
                return nums[0];
            }

            int prev = nums[0], next = Math.Max(nums[0], nums[1]);
            for(int i = 2; i < n; i++)
            {
                int temp = next;

                // 第 i 輪 
                // 前者是偷竊第 i 間, 前一輪總和 + 第 i 間的價值
                // 後者是不偷竊最後的第 i 間
                next = Math.Max(prev + nums[i], next);

                // 上一輪總和, 未加上 下一輪 第 i 輪
                prev = temp;
            }

            return next;
        }
    }
}
