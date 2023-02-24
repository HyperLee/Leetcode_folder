using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_326
{
    internal class Program
    {
        /// <summary>
        /// leetcode 326 Power of Three 3的次方 冪次
        /// https://leetcode.com/problems/power-of-three/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 9;
            Console.WriteLine(IsPowerOfThree(n));

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/power-of-three/solution/3de-mi-by-leetcode-solution-hnap/
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPowerOfThree(int n)
        {
            while (n != 0 && n % 3 == 0)
            {
                n /= 3;
            }
            return n == 1;
        }


    }
}
