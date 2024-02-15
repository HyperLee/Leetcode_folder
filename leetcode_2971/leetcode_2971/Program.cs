using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2971
{
    internal class Program
    {
        /// <summary>
        /// 2971. Find Polygon With the Largest Perimeter
        /// https://leetcode.com/problems/find-polygon-with-the-largest-perimeter/description/?envType=daily-question&envId=2024-02-15
        /// 2971. 找到最大周长的多边形
        /// https://leetcode.cn/problems/find-polygon-with-the-largest-perimeter/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 12, 1, 2, 5, 50, 3 };
            Console.WriteLine(LargestPerimeter(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/find-polygon-with-the-largest-perimeter/solutions/2578019/2971-zhao-dao-zui-da-zhou-chang-de-duo-b-fnpe/
        /// 
        /// 多边形 指的是一个至少有 3 条边的封闭二维图形。多边形的 最长边 一定 小于 所有其他边长度之和。
        /// 
        /// 如果遍历到的前缀元素個數小於 3，
        /// 则一定不满足前缀和大於该前缀的最大元素的两倍，
        /// 只有在遍历到的前缀元素個數大於等於 3 的情况下才可能满足遍历到的前缀的所有元素可以构造多边形。
        /// 
        /// 這解法重點在於理解 
        /// 要滿足
        /// 1. 三條邊
        /// 2. 前綴和 取兩倍
        /// 
        /// sum: 前綴和 <前n個數量加總>
        /// num: 當下最長邊
        /// maxperimeter: 最大週長(邊總和)
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static long LargestPerimeter(int[] nums)
        {
            // 題目說長度不會有負數, 故拿負數來當初使值
            long maxperimeter = -1;
            long sum = 0;

            Array.Sort(nums);
            
            foreach(int num in nums) 
            {
                sum += num;

                // 多邊形的 最長邊 一定 小於 所有其他邊長度之和
                if (sum > num * 2)
                {
                    maxperimeter = sum;
                }
            }

            return maxperimeter;

        }
    }
}
