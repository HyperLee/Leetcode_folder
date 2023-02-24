using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_560
{
    internal class Program
    {
        /// <summary>
        /// leetcode560
        /// https://leetcode.com/problems/subarray-sum-equals-k/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] num = { 1, 2, 3 };
            int k = 3;
            Console.WriteLine(SubarraySum(num, k));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/subarray-sum-equals-k/solution/he-wei-kde-zi-shu-zu-by-leetcode-solution/
        /// 方法一 枚舉
        /// 
        /// 前後夾擊 start 到end 加總 計數
        /// 找到加總為k 就count++
        /// 沒有就拉大間距 找
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int SubarraySum(int[] nums, int k)
        {
            int count = 0;
            for (int start = 0; start < nums.Length; start++) // 原先寫法是 ++start
            {
                int sum = 0;
                for (int end = start; end >= 0; end--) // 原先寫法是 --end
                {
                    sum += nums[end];
                    if (sum == k)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

    }
}
