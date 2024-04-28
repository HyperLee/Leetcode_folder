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
        /// 326. Power of Three
        /// https://leetcode.com/problems/power-of-three/
        /// 326. 3 的幂
        /// https://leetcode.cn/problems/power-of-three/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 3;
            Console.WriteLine(IsPowerOfThree(n));

            Console.ReadKey();
        }


        /// <summary>
        /// ref:
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

            // 回傳 true or false
            return n == 1;
        }


    }
}
