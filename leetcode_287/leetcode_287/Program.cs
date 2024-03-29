﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_287
{
    internal class Program
    {
        /// <summary>
        /// leetcode 287 Find the Duplicate Number
        /// https://leetcode.com/problems/find-the-duplicate-number/
        /// 287. 寻找重复数
        /// https://leetcode.cn/problems/find-the-duplicate-number/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 3, 1, 3, 4, 2 };
            Console.WriteLine(FindDuplicate(nums));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.com/problems/find-the-duplicate-number/discuss/745961/C-O(n-logn)-Sorting
        /// 一樣利用sort排序
        /// 找出 第n個和 第n -1 相同
        /// 就是 Duplicate 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int FindDuplicate(int[] nums)
        {
            Array.Sort(nums);

            for(int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1])
                {
                    // 回傳 array中該位置的數值, 不是index
                    return nums[i];
                }
            }

            return -1;

        }


    }
}
