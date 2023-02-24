using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_268
{
    internal class Program
    {
        /// <summary>
        /// leetcode 268  Missing Number
        /// https://leetcode.com/problems/missing-number/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 3, 0, 1 };

            Console.WriteLine(MissingNumber(nums));
            Console.ReadKey();

        }

        /// <summary>
        /// 利用排序下去找出 缺少的
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MissingNumber(int[] nums)
        {
            Array.Sort(nums);
            int n = nums.Length;
            for(int i = 0; i < n; i++)
            {
                if (nums[i] != i)
                {
                    return i;
                }
            }
            return n;


        }


    }
}
