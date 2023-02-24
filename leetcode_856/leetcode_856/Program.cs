using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_856
{
    class Program
    {
        /// <summary>
        /// https://leetcode-cn.com/problems/score-of-parentheses/solution/gua-hao-de-fen-shu-by-leetcode/
        /// 方法3
        /// 方法三：统计核心的数目
        /// 事实上，我们可以发现，只有 () 会对字符串 S 贡献实质的分数，其它的括号只会将分数乘二或者将分数累加。
        /// 因此，我们可以找到每一个 () 对应的深度 x，那么答案就是 2^x 的累加和。
        ///
        /// leetcode_856
        /// https://leetcode.com/problems/score-of-parentheses/
        /// 
        /// 
        /// https://www.shuzhiduo.com/A/kPzOWrG3zx/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "";
            s = "(()(()))"; // 6
                            //s = "()()"; // 2
                            //s = "(())"; // 2
                            //s = "()"; // 1

            /*
            Console.WriteLine(1 << 0); // 2^0 = 1
            Console.WriteLine(1 << 1); // 2^1 = 2
            Console.WriteLine(1 << 2); // 2^2 = 4
            Console.WriteLine(1 << 3); // 2^3 = 8
            Console.WriteLine(1 << 4); // 2^4 = 16
            */

            Console.WriteLine(ScoreOfParentheses(s));
            Console.ReadKey();
        }


        /// <summary>
        /// 
        /// 我们可以对空间复杂度进行进一步的优化，并不需要使用栈去保留所有的中间情况，
        /// 可以只用一个变量 cnt 来记录当前在第几层括号之中，因为本题的括号累加值是有规律的，"()" 是1，
        /// 因为最中间的括号在0层括号内，2^0 = 1。"(())" 是2，因为最中间的括号在1层括号内，2^1 = 2。"((()))" 是4，
        /// 因为最中间的括号在2层括号内，2^2 = 4。因此类推，其实只需要统计出最中间那个括号外变有几个括号
        /// ，就可以直接算出整个多重包含的括号字符串的值，
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ScoreOfParentheses(string s)
        {
            int ans = 0, bal = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i] == '(')
                {
                    bal++;
                }
                else
                {
                    bal--;
                    if (s[i - 1] == '(')
                    {
                        ans += (1 << bal);
                    }
                }
            }

            return ans;
            

        }

    }
}
