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


        /// <summary>
        /// 這邊直接呼叫 排序與反敘
        /// 
        /// 但是題目要求 其實是 快速排序 會比較合適
        /// 附上解法介紹
        /// https://leetcode.cn/problems/kth-largest-element-in-an-array/solutions/1981037/by-stormsunshine-lefe/
        /// https://leetcode.cn/problems/kth-largest-element-in-an-array/solutions/307351/shu-zu-zhong-de-di-kge-zui-da-yuan-su-by-leetcod-2/
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int FindKthLargest(int[] nums, int k)
        {
            // 排序
            Array.Sort(nums);
            // 大至小
            Array.Reverse(nums);

            // 找出第 k 個, 陣列 0 開始故 k - 1 才是實際上要找的
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
