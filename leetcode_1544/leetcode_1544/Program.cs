using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1544
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1544
        /// https://leetcode.com/problems/make-the-string-great/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "";
            input = "aAb";

            Console.WriteLine(MakeGood(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/make-the-string-great/solution/zheng-li-zi-fu-chuan-zhan-shi-xian-by-han-song-yan/
        /// 
        /// stack方式處理
        /// 遇到相同單字相鄰(大小寫)者 剃除
        /// 其餘繼續加入
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MakeGood(string s)
        {
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (stack.Count == 0)
                {
                    stack.Push(s[i]);
                }
                else
                {
                    // a-A=32 以此类推, ascii code 小寫 大寫 差距32
                    if (Math.Abs(stack.Peek() - s[i]) == 32)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(s[i]);
                    }
                }

            }

            string ans = "";
            // 大小為 stack.Count
            char[] list = new char[stack.Count];
            // 把處理過後資料丟到list裡面
            list = stack.ToArray();
            for (int i = list.Length - 1; i >= 0; i--)
            {
                ans += list[i];
            }

            return ans;
        }

    }
}
