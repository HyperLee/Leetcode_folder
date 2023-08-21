using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_190
{
    internal class Program
    {
        /// <summary>
        /// 190. Reverse Bits
        /// https://leetcode.com/problems/reverse-bits/
        /// 190. 颠倒二进制位
        /// https://leetcode.cn/problems/reverse-bits/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            uint n = 0011;
            Console.WriteLine(reverseBits(n));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/reverse-bits/solutions/1623850/by-stormsunshine-wgw3/
        /// 
        /// https://www.programiz.com/csharp-programming/bitwise-operators
        /// Bitwise Left Shift
        /// 42 <<1 = 84
        /// 42 <<2 = 168
        /// 42 <<4 = 672
        /// 左移是乘法
        /// 
        /// Bitwise Right Shift
        /// 42 >> 1 = 21 (In binary 010101)
        /// 42 >> 2 = 10 (In binary 001010)
        /// 42 >> 4 = 2 (In binary 000010)
        /// 
        /// 2的次方 shift
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>

        public static uint reverseBits(uint n)
        {
            const int BITS = 32;
            uint reversed = 0;

            for (int i = 0, j = BITS - 1; i < BITS; i++, j--)
            {

                uint bit = (n >> i) & 1;

                reversed |= bit << j;

            }

            return reversed;

        }


    }
}
