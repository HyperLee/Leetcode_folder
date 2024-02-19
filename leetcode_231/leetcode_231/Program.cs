using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_231
{
    internal class Program
    {
        /// <summary>
        /// leetcode 231 Power of Two  2 的幂
        /// https://leetcode.com/problems/power-of-two/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 4;
            Console.WriteLine("Method1: " + IsPowerOfTwo(n));
            Console.WriteLine("Method2: " + IsPowerOfTwo2(n));
            Console.ReadKey();
        }


        /// <summary>
        /// 官方解法:
        /// https://leetcode.cn/problems/power-of-two/solution/2de-mi-by-leetcode-solution-rny3/
        /// 方法一：二进制表示
        /// https://leetcode.cn/problems/power-of-two/solutions/796201/2de-mi-by-leetcode-solution-rny3/
        /// 
        /// n & (n - 1)
        /// 其中 & 表示按位与运算。该位运算技巧可以直接将 n 二进制表示的最低位 1 移除
        /// 因此，如果 n 是正整数并且 n & (n - 1) = 0，那么 n 就是 2 的幂。
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPowerOfTwo(int n)
        {
            return n > 0 && (n & (n - 1)) == 0;
        }


        /// <summary>
        /// 方法一：二进制表示
        /// n & (-n)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>

        public static bool IsPowerOfTwo2(int n)
        {
            return n > 0 && (n & -n) == n;
        }


    }
}
