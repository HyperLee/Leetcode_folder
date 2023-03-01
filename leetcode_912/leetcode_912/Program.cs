using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_912
{
    internal class Program
    {
        /// <summary>
        /// leetcode 920 Sort an Array
        /// https://leetcode.com/problems/sort-an-array/description/
        /// 
        /// 912. 排序数组
        /// https://leetcode.cn/problems/sort-an-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = new int[] { 5, 2, 3, 1 , 99, 77};

            //Console.WriteLine(SortArray(nums));
            SortArray(nums);
            Console.ReadKey();
        }


        /// <summary>
        /// 偷懶 直接用 現成的function
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] SortArray(int[] nums)
        {
            Array.Sort(nums);

            foreach (int i in nums)
            {
                Console.Write(i + " ");
            }

            return nums;

        }

    }
}
