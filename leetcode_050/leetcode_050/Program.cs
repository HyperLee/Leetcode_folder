using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_050
{
    internal class Program
    {
        /// <summary>
        /// 50. Pow(x, n)
        /// https://leetcode.com/problems/powx-n/
        /// 50. Pow(x, n)
        /// https://leetcode.cn/problems/powx-n/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            double x = 2.00000;
            int n = -2;

            Console.WriteLine(MyPow2(x, n));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/powx-n/solution/by-stormsunshine-zmxx/
        /// https://leetcode.cn/problems/powx-n/solution/50-powx-n-kuai-su-mi-qing-xi-tu-jie-by-jyd/
        /// 實作 Math.Pow(x, n);
        /// 
        /// 先偷懶用內建
        /// 再找時間實作
        /// 
        /// 官方解法
        /// 方法1: 暴力法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double MyPow(double x, int n)
        {
            long N = n;
            
            // N < 0 為 1/x
            if(N < 0)
            {
                x = 1 / x;
                N = -N;
            }

            double ans = 1;
            for(long i = 0; i < N; i++)
            {
                ans = ans * x;
            }

            return ans;

        }


        /// <summary>
        /// 方法2
        /// https://leetcode.cn/problems/powx-n/solution/powx-n-by-leetcode-solution/
        /// 官方解法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>

        public static double MyPow2(double x, int n)
        {
            // 邊界直
            if(n == 0 || x == 1)
            {
                return 1;
            }

            // n 為負數
            if(n < 0)
            {
                return 1 / mypowhelper(x, Math.Abs(n));
            }

            return mypowhelper(x, n);
        }

        public static double mypowhelper(double x, Int32 n)
        {
            if(n == 1)
            {
                return x;
            }

            // 根據n 的奇偶性進行遞迴
            if(n % 2 != 0)
            {
                double half = mypowhelper(x, n / 2);
                return half * half * x;
            }
            else
            {
                double half = mypowhelper(x, n / 2);
                return half * half;
            }
        }


        /// <summary>
        /// https://leetcode.cn/problems/powx-n/solution/by-stormsunshine-zmxx/
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double MyPow3(double x, int n)
        {
            if (n == 0)
            {
                return 1;
            }

            if (x == 0)
            {
                return 0;
            }

            double power = MyPow(x, n / 2);
            if (n % 2 == 0)
            {
                return power * power;
            }

            return n > 0 ? power * power * x : power * power / x;

        }

    }
}
