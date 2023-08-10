using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_080
{
    internal class Program
    {
        /// <summary>
        /// 80. Remove Duplicates from Sorted Array II
        /// https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/
        /// 80. 删除有序数组中的重复项 II

        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 1, 1, 2, 2, 2, 3 };
            Console.WriteLine(RemoveDuplicates(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 參考
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/solutions/702970/gong-shui-san-xie-guan-yu-shan-chu-you-x-glnq/
        /// 通用解法 for 保留 k 位數
        /// 
        /// 由于是保留 k 个相同数字，对于前 k 个数字，我们可以直接保留
        /// 对于后面的任意数字，能够保留的前提是：
        /// 与当前写入的位置前面的第 k 个元素进行比较，不相同则保留
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int RemoveDuplicates(int[] nums)
        {
            return process(nums, 2);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int process (int[] nums, int k)
        {
            int u = 0;

            foreach (int x in nums) 
            {
                if(u < k || nums[u - k] != x)
                {
                    nums[u++] = x;
                }
            }

            return u;
        }

    }
}
