using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_974
{
    internal class Program
    {
        /// <summary>
        /// leetcode 974
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] num = { 4, 5, 0, -2, -3, 1 };
            int k = 5;

            Console.WriteLine(SubarraysDivByK(num, k));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/subarray-sums-divisible-by-k/solution/by-stormsunshine-37mq/
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int SubarraysDivByK(int[] nums, int k)
        {
            int subarrays = 0;
            int[] counts = new int[k];
            counts[0] = 1;
            int sum = 0;
            int length = nums.Length;
            for (int i = 0; i < length; i++)
            {
                sum = (sum + nums[i]) % k;
                if (sum < 0)
                {
                    sum += k;
                }
                int count = counts[sum];
                subarrays += count;
                counts[sum]++;
            }
            return subarrays;
        }

    }
}
