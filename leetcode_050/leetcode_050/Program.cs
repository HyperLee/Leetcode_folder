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
        /// 
        /// 只有方法3 不會 TLE
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            double x = 2.00000;
            int n = -1;

            //Console.WriteLine("方法1: " + MyPow(x, n));
            //Console.WriteLine("方法2: " + MyPow2(x, n));
            Console.WriteLine("方法3: " + MyPow3(x, n));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/powx-n/solution/by-stormsunshine-zmxx/
        /// https://leetcode.cn/problems/powx-n/solution/50-powx-n-kuai-su-mi-qing-xi-tu-jie-by-jyd/
        /// 實作 Math.Pow(x, n);
        /// 最直覺直觀的數學計算方法
        /// 
        /// 簡單說就是
        /// 數字 x * 次數 n 
        /// 用迴圈去跑 n 次
        /// 幾次方就跑幾次
        /// 
        /// 次方負數就是分數算法
        /// 2^-2 = 1 / 2^2 = 1 / 4 = 0.25
        /// 
        /// 當數字很大, 容易 TLE
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
        /// 
        /// 邊界值
        /// 任何數的 0 次方都為 1
        /// 1 的次方也是 1
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>

        public static double MyPow2(double x, int n)
        {
            // 邊界值
            if(n == 0 || x == 1)
            {
                return 1;
            }

            // n 為負數
            if(n < 0)
            {
                // 因為負數算法是分數, 所以 n 取正值
                return 1 / mypowhelper(x, Math.Abs(n));
            }
            else
            {
                return mypowhelper(x, n);
            }

        }


        /// <summary>
        /// MyPow2
        /// 遞迴
        /// 把前一次的結果再取平方
        /// 不需要乘以 n 的次數
        /// 
        /// 當
        /// n 為奇數要多乘以 x  一次
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double mypowhelper(double x, Int32 n)
        {
            if(n == 1)
            {
                return x;
            }

            // 根據 n 的奇偶性進行遞迴
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
        /// 
        /// 只有這方法才不會 TLE
        /// 主要概念跟上面方法2差不多
        /// 遞迴 + 快速幂
        /// 詳細說明要看連結
        /// 把 n 次方 拆成 a + b 來計算, 降低複雜度
        /// a = n / 2
        /// b = n - a
        /// 每次遞迴都會把 a 拆得更細/更小
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double MyPow3(double x, int n)
        {
            if (n == 0)
            {
                // 任何數的 0 次方 都為 1
                return 1;
            }

            if (x == 0)
            {
                // 0 的次方也是 0
                return 0;
            }

            // 持續遞迴 取 n / 2
            double power = MyPow3(x, n / 2);
            if (n % 2 == 0)
            {
                return power * power;
            }

            return n > 0 ? power * power * x : power * power / x;

        }

    }
}
