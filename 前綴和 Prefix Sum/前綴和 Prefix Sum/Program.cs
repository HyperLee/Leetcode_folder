using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 前綴和_Prefix_Sum
{
    internal class Program
    {
        /// <summary>
        /// leetcode 643 Maximum Average Subarray I
        /// https://leetcode.com/problems/maximum-average-subarray-i/
        /// 643. 子数组最大平均数 I
        /// https://leetcode.cn/problems/maximum-average-subarray-i/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 1, 12, -5, -6, 50, 3 };
            int k = 4;
            Console.WriteLine(FindMaxAverage(input, k));
            Console.ReadKey();

        }


        /// <summary>
        /// 前綴和 Prefix Sum 解法
        /// https://leetcode.cn/problems/maximum-average-subarray-i/solution/qian-zhui-he-by-muransea-a2dh/
        /// 
        /// 介紹:
        /// https://hackmd.io/@Edmond0908/H1QUI82Gt
        /// 
        /// 當題目都會需要求出一段陣列的總和，
        /// 就可以使用該方法
        /// 暴力法用 for迴圈 然後用 加減乘除 很容易 超時
        /// 
        /// 歸納出遞迴關係式 : S(n)=S(n−1)+a[n]
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>

        public static double FindMaxAverage(int[] nums, int k)
        {
            if (nums.Length == 1)
            {
                return nums[0];
            }

            var len = k - 1;
            double max = double.MinValue;

            // 從1開始; 取出最大k數量的加總值
            for (int i = 1; i < nums.Length; i++)
            {
                nums[i] += nums[i - 1];

                // 前 k個 最大值
                if (i == len)
                {
                    max = nums[i];
                }
                else if (i > len)
                {
                    // 開始每次更新 max, 與 新一輪的k個值相比
                    // 新一輪 nums[i] - nums[i - k], 第i個加總和之值 扣除 前一輪的值 才是新的
                    max = Math.Max(max, nums[i] - nums[i - k]);
                }
            }

            // 最後再做 除法 取出解答
            return max / k;
        }


    }
}
