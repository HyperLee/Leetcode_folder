using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1913
{
    internal class Program
    {
        /// <summary>
        /// 1913. Maximum Product Difference Between Two Pairs
        /// https://leetcode.com/problems/maximum-product-difference-between-two-pairs/description/?envType=daily-question&envId=2023-12-18
        /// 1913. 两个数对之间的最大乘积差
        /// https://leetcode.cn/problems/maximum-product-difference-between-two-pairs/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 5, 6, 2, 7, 4 };

            Console.WriteLine(MaxProductDifference(input));
            Console.ReadKey();
        }



        /// <summary>
        /// a, b 為輸入陣列裡面最大兩者
        /// c, d 為輸入陣列裡面最小兩者
        /// 
        /// 相乘之後在相減 即可求出題目要求
        /// 最大乘積差值
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxProductDifference(int[] nums)
        {
            Array.Sort(nums);

            int a = nums[nums.Length - 1];
            int b = nums[nums.Length - 2];

            int c = nums[0];
            int d = nums[1];

            int res = (a * b) - (c * d);

            return res;
        }
    }
}
