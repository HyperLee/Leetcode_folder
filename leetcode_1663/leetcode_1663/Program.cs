using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1663
{
    class Program
    {

        /// <summary>
        /// leetcode 1663
        /// https://leetcode.com/problems/smallest-string-with-a-given-numeric-value/
        /// 
        /// 
        /// https://leetcode-cn.com/problems/smallest-string-with-a-given-numeric-value/comments/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine(GetSmallestString(5, 73));
            Console.ReadKey();
        }


        /// <summary>
        /// 能塞最大 就塞最大
        /// 不能塞最大 就由後往前找 最大的
        /// 最後其餘的就放最小的a
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string GetSmallestString(int n, int k)
        {
            /*
                k = 73, n = 5
                倒着添加字符，尾巴能添加z就添加z，k减去26
                如果不能添加z，并且剩余的字符大于k个，说明不能用全a表示，
                就添加一个目前能获得的最大字符，然后前面全是a。
                举个例子,k = 73,添加两个z，剩下21，剩余的字符数是3；
                结果就是对于a + a +(字符) = 21,解方程可知字符为s=19。
            */

            char[] chs = new char[n];
            int index = n - 1;
            while (n > 0)
            {
                if (k - 26 >= n - 1)
                {
                    chs[index--] = 'z';
                    k -= 26;
                }
                else if (k > n)
                {
                    chs[index--] = (char)('a' + (k - n));
                    k = n - 1;
                }
                else
                {
                    k--;
                    chs[index--] = 'a';
                }
                n--;
            }
            string s = new string(chs);

            return s;
        }

    }
}
