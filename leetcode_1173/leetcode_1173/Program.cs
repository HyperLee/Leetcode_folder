using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1173
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1173 N-th Tribonacci Number
        /// https://leetcode.com/problems/n-th-tribonacci-number/
        /// 
        /// 類似 fibonacci 但是 變成 前三個加總
        /// 小心超時
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 35;

            Console.WriteLine(Tribonacci(n));

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/n-th-tribonacci-number/solution/by-en-xiao-gt01/
        /// 
        /// https://leetcode.cn/problems/n-th-tribonacci-number/solution/di-n-ge-tai-bo-na-qi-shu-by-leetcode-sol-kn16/
        /// 当 n>2时，每一项的和都等于前三项的和
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Tribonacci(int n)
        {
            if(n == 0)
            {
                return 0;
            }

            if(n == 1 || n ==2)
            {
                return 1;
            }
            // 不能用傳統 fibonacci 方式跑, 因 超時
            //else
            //{
            //    return Tribonacci(n - 1) + Tribonacci(n - 2) + Tribonacci(n - 3);
            //}

            // n == 3 開始.
            int t0 = 0, t1 = 1, t2 = 1, t3 = 0;
            for (int i = 3; i <= n; i++)
            {
                t3 = t0 + t1 + t2;
                t0 = t1;
                t1 = t2;
                t2 = t3;
            }
            return t3;

        }


    }
}
