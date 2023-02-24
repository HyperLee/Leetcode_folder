using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_029
{
    internal class Program
    {
        /// <summary>
        /// leetcode 029 Divide Two Integers
        /// https://leetcode.com/problems/divide-two-integers/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int dividend = -2147483648;
            int divisor = -1;


            Console.WriteLine(Divide(dividend, divisor));
            //Console.WriteLine(Divide2(dividend, divisor));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.com/problems/divide-two-integers/discuss/529561/C-bit-shift
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        public static int Divide(int dividend, int divisor)
        {

            long absDividend = Math.Abs((long)dividend);
            long absDivisor = Math.Abs((long)divisor);

            long tmpDividend = absDividend;
            long tmpDivisor = absDivisor;

            long res = 0;
            while (tmpDivisor <= tmpDividend)
            {
                long multipliedDivisor = tmpDivisor;
                long iMultiPlier = 1;
                // Find the maximum number of multipling
                while (multipliedDivisor << 1 < tmpDividend)
                {
                    multipliedDivisor <<= 1;
                    iMultiPlier <<= 1;
                }
                // calc: tmpDividend - (iMultiPlier * abs(divisor)) 
                tmpDividend -= multipliedDivisor;
                res += iMultiPlier;
            }

            if ((dividend < 0) ^ (divisor < 0))
            {
                res = -res;
            }

            if (res > 2147483647) { res = 2147483647; }
            if (res < -2147483648) { res = -2147483648; }

            return (int)res;

        }

        public static int Divide2(int dividend, int divisor)
        {
            int c = 0;
            try
            {
                c = dividend / divisor;
            }
            catch(Exception ee)
            {
                //Console.WriteLine(ee);
                if (dividend > 2147483647) { c = 2147483647; }
                if (dividend < -2147483648) { c = -2147483648; }

                if(dividend < 0 || divisor < 0)
                {
                    c = c * -1;
                }
                else
                {
                    c = c * 1;
                }
            }


            return c;
        }

    }
}
