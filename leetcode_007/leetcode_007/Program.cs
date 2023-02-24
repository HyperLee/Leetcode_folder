using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_007
{
    internal class Program
    {

        /// <summary>
        /// leetcode 007
        /// Reverse Integer
        /// https://leetcode.com/problems/reverse-integer/description/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int x = -123;
            Console.WriteLine(Reverse(x));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/reverse-integer/solution/zheng-shu-fan-zhuan-by-leetcode-solution-bccn/
        /// 
        /// 要在没有辅助栈或数组的帮助下「弹出」和「推入」数字，我们可以使用如下数学方法：
        /// 弹出 x 的末尾数字 digit
        /// int digit = x % 10;
        /// x /= 10;
        /// 
        /// 将数字 digit 推入 rev 末尾
        /// rev = rev * 10 + digit;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Reverse(int x)
        {
            int rev = 0;
            while (x != 0)
            {
                if (rev < int.MinValue / 10 || rev > int.MaxValue / 10)
                {
                    return 0;
                }

                //弹出 x 的末尾数字 digit
                int digit = x % 10;
                x /= 10;

                //将数字 digit 推入 rev 末尾
                rev = rev * 10 + digit;

            }
            return rev;

        }


    }
}
