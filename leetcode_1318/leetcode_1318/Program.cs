using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1318
{
    internal class Program
    {

        /// <summary>
        /// leetcode 1318. Minimum Flips to Make a OR b Equal to c
        /// https://leetcode.com/problems/minimum-flips-to-make-a-or-b-equal-to-c/description/
        /// 1318. 或运算的最小翻转次数
        /// https://leetcode.cn/problems/minimum-flips-to-make-a-or-b-equal-to-c/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int a = 2, b = 6, c = 5;
            Console.WriteLine(MinFlips(a, b, c));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/minimum-flips-to-make-a-or-b-equal-to-c/solution/by-stormsunshine-jtab/
        /// 
        /// https://leetcode.cn/problems/minimum-flips-to-make-a-or-b-equal-to-c/solution/huo-yun-suan-de-zui-xiao-fan-zhuan-ci-shu-by-lee-2/
        /// 
        /// 解法都差不多
        /// 需要多加理解
        /// 目前還不是很懂
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>

        public static int MinFlips(int a, int b, int c)
        {
            int flips = 0;

            while(a > 0 || b > 0 || c > 0)
            {
                int abit = a & 1;
                int bbit = b & 1;
                int cbit = c & 1;

                if ((abit | bbit) != cbit)
                {
                    if (cbit == 1)
                    {
                        flips++;
                    }
                    else
                    {
                        flips += abit + bbit;
                    }
                }

                a >>= 1;
                b >>= 1;
                c >>= 1;
            }

            return flips;

        }


    }
}
