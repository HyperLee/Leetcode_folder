using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1137
{
    internal class Program
    {
        /// <summary>
        /// 1137. N-th Tribonacci Number
        /// https://leetcode.com/problems/n-th-tribonacci-number/description/?envType=study-plan-v2&envId=leetcode-75
        /// 1137. 第 N 个泰波那契数
        /// https://leetcode.cn/problems/n-th-tribonacci-number/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 4;
            Console.WriteLine(Tribonacci(n));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/n-th-tribonacci-number/solutions/921898/di-n-ge-tai-bo-na-qi-shu-by-leetcode-sol-kn16/
        /// 
        /// 該算法比傳統課堂上教的 方式還要好
        /// 尤其是 輸入的n非常大時候
        /// 更明顯
        /// 效率
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Tribonacci(int n)
        {
            if (n == 0)
            {
                return 0;
            }

            if (n == 1 || n == 2)
            {
                return 1;
            }

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
