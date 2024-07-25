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
            SortArray2(nums);
            Console.ReadKey();
        }


        /// <summary>
        /// 偷懶 直接用 現成的function
        /// 
        /// 排序寫法 可參考
        /// https://leetcode.cn/problems/sort-an-array/solution/by-stormsunshine-mjb5/
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


        /// <summary>
        /// bubble sort
        /// 
        /// https://leetcode.cn/problems/sort-an-array/solutions/178305/pai-xu-shu-zu-by-leetcode-solution/
        /// https://leetcode.cn/problems/sort-an-array/solutions/179489/fu-xi-ji-chu-pai-xu-suan-fa-java-by-liweiwei1419/
        /// https://leetcode.cn/problems/sort-an-array/solutions/1496053/by-stormsunshine-mjb5/
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] SortArray2(int[] nums)
        {
            int length = nums.Length;

            for(int i = 1; i < length; i++)
            {
                for(int j = 0; j < length - 1; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        int temp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = temp;
                    }
                }
            }

            foreach (int i in nums)
            {
                Console.Write(i + " ");
            }

            return nums;

        }

    }
}
