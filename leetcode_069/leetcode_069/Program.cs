using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_069
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 4;
            Console.WriteLine(MySqrt(x));
            //double a = Math.Sqrt(8);
            //Console.WriteLine(a);
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/sqrtx/solution/x-de-ping-fang-gen-by-leetcode-solution/
        /// 方法二：二分查找
        /// 由于 x 平方根的整数部分 ans 是满足 k^2≤x 的最大 k 值，因此我们可以对 k 进行二分查找，从
        /// 而得到答案。
        /// 
        /// 
        /// 實作 內建 function =>  Math.Sqrt(x)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int MySqrt(int x)
        {
            //double a = 0;
            //a = Math.Sqrt(x);
            //return (int)a;
            int l = 0, r = x, ans = -1;
            while (l <= r)
            {
                int mid = l + (r - l) / 2;
                if ((long)mid * mid <= x)
                {
                    ans = mid;
                    l = mid + 1;
                }
                else
                {
                    r = mid - 1;
                }
            }
            return ans;
        }

    }
}
