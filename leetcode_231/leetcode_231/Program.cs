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
        /// https://leetcode.cn/problems/power-of-two/solution/2de-mi-by-leetcode-solution-rny3/
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPowerOfTwo(int n)
        {
            return n > 0 && (n & (n - 1)) == 0;
        }


        public static bool IsPowerOfTwo2(int n)
        {
            return n > 0 && (n & -n) == n;
        }


    }
}
