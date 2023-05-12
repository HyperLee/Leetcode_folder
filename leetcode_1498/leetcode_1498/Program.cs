using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1498
{
    internal class Program
    {
        /// <summary>
        /// 1498. Number of Subsequences That Satisfy the Given Sum Condition
        /// https://leetcode.com/problems/number-of-subsequences-that-satisfy-the-given-sum-condition/
        /// 1498. 满足条件的子序列数目
        /// https://leetcode.cn/problems/number-of-subsequences-that-satisfy-the-given-sum-condition/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }


        /// <summary>
        /// 本題尚未理解 解法
        /// 
        /// https://leetcode.cn/problems/number-of-subsequences-that-satisfy-the-given-sum-condition/solution/by-stormsunshine-c8m7/
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int NumSubseq(int[] nums, int target)
        {
            const int MODULO = 1000000007;
            int subsequences = 0;
            int length = nums.Length;
            int[] power2 = new int[length];
            power2[0] = 1;
            for (int i = 1; i < length; i++)
            {
                power2[i] = power2[i - 1] * 2 % MODULO;
            }
            Array.Sort(nums);
            for (int i = 0; i < length && nums[i] * 2 <= target; i++)
            {
                int j = SearchEnd(nums, target - nums[i], i);
                int count = power2[j - i];
                subsequences = (subsequences + count) % MODULO;
            }
            return subsequences;

        }


        public static int SearchEnd(int[] nums, int target, int low)
        {
            int high = nums.Length - 1;
            while (low < high)
            {
                int mid = low + (high - low + 1) / 2;
                if (nums[mid] <= target)
                {
                    low = mid;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return low;
        }
    }
}
