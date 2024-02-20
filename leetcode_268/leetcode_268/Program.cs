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
        /// 268. 丢失的数字
        /// https://leetcode.cn/problems/missing-number/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 2, 0, 1 };

            Console.WriteLine(MissingNumber(nums));
            Console.ReadKey();

        }

        /// <summary>
        /// 利用排序下去找出 缺少的
        /// 題目有說 範圍: [0, n]
        /// 從0開始
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MissingNumber(int[] nums)
        {
            Array.Sort(nums);
            int n = nums.Length;

            // 迴圈找出缺失的數字  0 <= i < n
            for(int i = 0; i < n; i++)
            {
                if (nums[i] != i)
                {
                    return i;
                }
            }

            // 上述迴圈都存在那就是缺失n; 上述迴圈不包含 n
            return n;

        }


    }
}
