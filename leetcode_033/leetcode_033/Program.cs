using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_033
{
    internal class Program
    {
        /// <summary>
        /// 33. Search in Rotated Sorted Array
        /// https://leetcode.com/problems/search-in-rotated-sorted-array/
        /// 33. 搜索旋转排序数组
        /// https://leetcode.cn/problems/search-in-rotated-sorted-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 4, 5, 6, 7, 0, 1, 2 };

            Console.WriteLine(Search(input, 0));
            Console.ReadKey();

        }


        /// <summary>
        /// 單純用迴圈跑過一輪 比對
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int Search(int[] nums, int target)
        {

            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == target)
                {
                    return i;
                }
            }

            return -1;
        }


    }
}
