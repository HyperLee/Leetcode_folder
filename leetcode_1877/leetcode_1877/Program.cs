using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1877
{
    internal class Program
    {
        /// <summary>
        /// 1877. Minimize Maximum Pair Sum in Array
        /// https://leetcode.com/problems/minimize-maximum-pair-sum-in-array/?envType=daily-question&envId=2023-11-17
        /// 1877. 数组中最大数对和的最小值
        /// https://leetcode.cn/problems/minimize-maximum-pair-sum-in-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 3, 5, 2, 3 };
            Console.WriteLine(MinPairSum(nums));
            Console.ReadKey();
        }


        /// <summary>
        /// 取陣列中 數字相加之後取出 加總和 為最小
        /// 範例說明:
        /// nums = [3,5,2,3]
        /// 数组中的元素可以分为数对 (3,3) 和 (5,2) 。
        /// 最大数对和为 max(3+3, 5+2) = max(6, 7) = 7 。
        /// 
        /// 如果單純按順序會造成 
        /// max(3 + 5, 2 + 3) => max(8, 5) = 8
        /// 加總和 比上述方法還要大
        /// 
        /// 所以方法就是 拿到的陣列 先做排序
        /// 用意就是 較小數字 + 較大數字 配合成一對
        /// 這樣會讓 總和數字 比較小
        /// (大, 小) => 一對
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinPairSum(int[] nums)
        {
            Array.Sort(nums);

            int res = 0;
            int n = nums.Length;

            // 題目要求 取 n / 2 對; 前後各取一個出來加總
            for(int i = 0; i < n / 2; i++)
            {
                res = Math.Max(res, nums[i] + nums[n - 1 - i]);
            }

            return res;
        }

    }
}
