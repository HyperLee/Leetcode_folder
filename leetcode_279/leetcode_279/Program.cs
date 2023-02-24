using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_279
{
    internal class Program
    {
        /// <summary>
        /// leetcode 279
        /// https://leetcode.com/problems/perfect-squares/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int Input = 4;

            Console.WriteLine(NumSquares(Input));
            Console.ReadKey();

        }

        /// <summary>
        /// method1
        /// https://leetcode.cn/problems/perfect-squares/solution/wan-quan-ping-fang-shu-by-leetcode-solut-t99c/
        /// 动态规划
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int NumSquares(int n)
        {
            int[] f = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                int minn = int.MaxValue;
                for (int j = 1; j * j <= i; j++)
                {
                    minn = Math.Min(minn, f[i - j * j]);
                }
                f[i] = minn + 1;
            }
            return f[n];
        }

    }
}
