using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_209
{
    internal class Program
    {
        /// <summary>
        /// leetcode_209 Minimum Size Subarray Sum
        /// https://leetcode.com/problems/minimum-size-subarray-sum/
        /// 
        /// 209. 长度最小的子数组
        /// https://leetcode.cn/problems/minimum-size-subarray-sum/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 2, 3, 1, 2, 4, 3 };
            int target = 7;

            Console.WriteLine(MinSubArrayLen(target, input));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/minimum-size-subarray-sum/solution/chang-du-zui-xiao-de-zi-shu-zu-by-leetcode-solutio/
        /// 方法一: 暴力法
        /// 
        /// Time Limit Exceeded
        /// 看來需要優化
        /// </summary>
        /// <param name="target"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinSubArrayLen(int target, int[] nums)
        {
            int n = nums.Length;

            if(n == 0)
            {
                return 0;
            }

            int ans = int.MaxValue;

            for(int i = 0; i < n; i++) 
            {
                int sum = 0;

                for(int j = i; j < n; j++) 
                {
                    sum += nums[j];

                    if(sum >= target)
                    {
                        // 更新子数组的最小长度（此时子数组的长度是 j-i+1）
                        ans = Math.Min(ans, j - i + 1);
                        break;
                    }
                }
            }

            return ans == int.MaxValue ? 0 : ans;
        }


        /// <summary>
        /// 方法二:滑動窗口, 左右針
        /// 
        /// right 往右跑,跑一個就加總.
        /// 直到符合 target停止.
        /// 這樣就是第一個解
        /// 
        /// 下一輪就把 left往右一格
        /// 既然left往右那麼sum就要扣除原先left位置的數值
        /// 再來繼續上一步驟 right 繼續往右走
        /// 
        /// 重覆此行為 直到 sum < target 就停止
        /// 
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinSubArrayLen2(int target, int[] nums)
        {
            int n = nums.Length;

            if (n == 0)
            {
                return 0;
            }

            int ans = int.MaxValue;
            int left = 0, right = 0;
            int sum = 0;

            while (right < n)
            {
                sum += nums[right];

                while (sum >= target)
                {
                    // 找到符合target之後 更新長度
                    ans = Math.Min(ans, right - left + 1);

                    // left往右 繼續跑下一輪 直到 sum < target 停止
                    // 用意是找出最佳解; 第一個符合的不一定是最佳解
                    sum -= nums[left];
                    left++;
                }
                right++;
            }

            return ans == int.MaxValue ? 0 : ans;

        }


    }
}
