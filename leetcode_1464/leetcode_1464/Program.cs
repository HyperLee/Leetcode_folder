using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1464
{
    internal class Program
    {
        /// <summary>
        /// 1464. Maximum Product of Two Elements in an Array
        /// https://leetcode.com/problems/maximum-product-of-two-elements-in-an-array/?envType=daily-question&envId=2023-12-12
        /// 1464. 数组中两元素的最大乘积
        /// https://leetcode.cn/problems/maximum-product-of-two-elements-in-an-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 3, 4, 5, 2 };
            Console.WriteLine(MaxProduct(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 1. 先排序 小至大 
        /// 2. 反轉成大至小
        /// 3.取出題目要求最大兩數字
        /// 4.依題目公式計算 回傳
        /// 
        /// 可能會有更好方法
        /// 應該是在於 取出 最大兩數方法這邊
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static int MaxProduct(int[] nums)
        {
            Array.Sort(nums);
            Array.Reverse(nums);

            int res = 0;
            int a = 0;
            int b = 0;

            a = nums[0];
            b = nums[1];

            res = (a - 1) * (b - 1);

            return res;
        }
    }
}
