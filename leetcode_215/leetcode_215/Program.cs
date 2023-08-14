using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_215
{
    internal class Program
    {
        /// <summary>
        /// 215. Kth Largest Element in an Array
        /// https://leetcode.com/problems/kth-largest-element-in-an-array/
        /// 215. 数组中的第K个最大元素
        /// https://leetcode.cn/problems/kth-largest-element-in-an-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 3, 2, 1, 5, 6, 4 };
            int k = 2;
            Console.WriteLine(FindKthLargest(input, k));
            Console.ReadKey();
        }


        public static int FindKthLargest(int[] nums, int k)
        {
            // 排序
            Array.Sort(nums);
            // 大至小
            Array.Reverse(nums);

            // 找出第k個,    陣列0開始故 k - 1 才是實際上要找的
            for(int i = 0; i < nums.Length; i++)
            {
                if(i == (k - 1))
                {
                    return nums[i];
                }
            }

            return 0;
        }


    }
}
