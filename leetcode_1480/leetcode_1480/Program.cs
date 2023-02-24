using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1480
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1480 Running Sum of 1d Array
        /// https://leetcode.com/problems/running-sum-of-1d-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 1, 2, 3, 4 };
            RunningSum(nums);
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/running-sum-of-1d-array/solution/yi-wei-shu-zu-de-dong-tai-he-by-leetcode-flkm/
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] RunningSum(int[] nums)
        {
            int n = nums.Length;
            for (int i = 1; i < n; i++)
            {
                nums[i] += nums[i - 1];
            }

            for(int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine(nums[i]);
            }
            
            return nums;
        }


    }
}
