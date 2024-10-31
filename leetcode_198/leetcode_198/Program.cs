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
            int[] input = new int[] { 1, 2, 3, 1 };
            Console.WriteLine("method1: " + Rob(input));
            Console.WriteLine("method2: " + Rob2(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 來源解法:
        /// https://leetcode.cn/problems/house-robber/solutions/2094726/198-da-jia-jie-she-by-stormsunshine-entl/
        /// 
        /// 題目有說, 不能偷相鄰(連續)房屋, 要隔間偷
        /// 如果只有一間房屋, 最大值就是 第一間 dp[0] = nums[0]
        /// 如果有兩間房屋, 就是找出兩者中最大的 dp[1] = max(nums[0],nums[1])。
        /// 當 i >= 2
        /// 1. 如果偷窃第 i 间房屋，则不能偷窃第 i − 1 间房屋，最高金额为下标范围 [0,i − 2] 中能够偷窃到的最高金额加 nums[i]
        /// ，此时的最高金额是 dp[i − 2] + nums[i]。
        ///  => 簡單說偷竊第 i 間就是前 i - 2 間的總和 dp[i - 2] 加上第 i 間的數值 nums[i]
        /// 2. 如果不偷窃第 i 间房屋，则最高金额为下标范围 [0,i − 1] 中能够偷窃到的最高金额，此时的最高金额是 dp[i − 1]。
        ///  => 不偷竊第 i 間也就是取前 i - 1 間的總和 dp[i - 1]
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
            // 長度 2, 取出這兩間最大者
            dp[1] = Math.Max(nums[0], nums[1]);

            // 由於一間與兩間已經列舉, 第三間開始要用計算的
            // 前 i 間之總和
            for(int i = 2; i < n; i++)
            {
                // 前者是偷竊第 i 間, 前一輪總和 + 第 i 間的價值
                // 後者是不偷竊最後的第 i 間
                dp[i] = Math.Max(dp[i - 2] + nums[i], dp[i - 1]);
            }

            // 陣列從 0 開始取 n - 1 才不會超出邊界
            return dp[n - 1];
        }


        /// <summary>
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

            if(n == 2)
            {
                return Math.Max(nums[0], nums[1]);
            }

            int pre = nums[0];
            int next = Math.Max(nums[0], nums[1]);
            int result = 0;
            // n > 2; i 從 2 開始
            for(int i = 2; i < n; i++)
            {
                result = Math.Max(pre + nums[i], next);
                pre = next;
                next = result;
            }

            return result;
        }
    }
}
