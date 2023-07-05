using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1493
{
    internal class Program
    {
        /// <summary>
        /// 1493. Longest Subarray of 1's After Deleting One Element
        /// https://leetcode.com/problems/longest-subarray-of-1s-after-deleting-one-element/description/
        /// 1493. 删掉一个元素以后全为 1 的最长子数组
        /// https://leetcode.cn/problems/longest-subarray-of-1s-after-deleting-one-element/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 0, 1, 1, 1, 0, 1, 1, 0, 1 };
            Console.WriteLine(LongestSubarray(input));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/longest-subarray-of-1s-after-deleting-one-element/solution/chua-dong-chuang-kou-by-godsekyman-olps/
        /// 滑動窗口 解法 sliding window
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static int LongestSubarray(int[] nums)
        {
            var left = 0;
            var right = 0;
            var diff = 0;
            var ans = 0;

            while (right < nums.Length)
            {

                if (nums[right] != 1)
                {
                    // 窗口 遇到右指針不是1的就累加計數
                    diff += 1;
                }

                while (diff > 1)
                {
                    if (nums[left] != 1)
                    {
                        // 確保最多只能刪除一個 0
                        diff--;
                    }

                    left++;
                    
                }

                // 連續最大長度: 窗口總長度 right 扣除 需要刪除的left長度
                ans = Math.Max(right - left, ans);

                right++;
            }

            return ans;
        }
    }
}
