using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_053
{
    internal class Program
    {
        /// <summary>
        /// 53. Maximum Subarray
        /// https://leetcode.com/problems/maximum-subarray/
        /// 53. 最大子数组和
        /// https://leetcode.cn/problems/maximum-subarray/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            Console.WriteLine(MaxSubArray(input));
            Console.ReadKey();

        }


        /// <summary>
        /// /https://leetcode.cn/problems/maximum-subarray/solutions/228009/zui-da-zi-xu-he-by-leetcode-solution/?envType=daily-question&envId=Invalid+Date
        /// 題目要求是要找出連續的位置
        /// 
        /// 前面總和 + 現在 i位置 數值 與 現在 i 位置 取出最大者
        /// 即可當作是連續加總找出最大總和
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxSubArray(int[] nums)
        {
            int pre = 0;
            int maxans = nums[0];

            foreach(int x in nums)
            {
                // 連續加總
                pre = Math.Max(pre + x, x);
                // 記錄最大
                maxans = Math.Max(pre, maxans);
            }

            return maxans;
        }

    }
}
