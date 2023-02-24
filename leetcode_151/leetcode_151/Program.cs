using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace leetcode_151
{
    class Program
    {
        /// <summary>
        /// https://leetcode.com/problems/reverse-words-in-a-string/
        /// leetcode 151
        /// 
        /// https://leetcode.cn/problems/reverse-words-in-a-string/
        /// 字串反轉
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string a = "a  b c";
            Console.WriteLine(ReverseWords(a));
            Console.ReadKey();
        }

        /// <summary>
        /// https://leetcode.com/problems/reverse-words-in-a-string/discuss/737908/C-solutions
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string reverseWords1(string s)
        {
            s = s.Trim();
            String[] split = s.Split();
            // 正規畫 去除 兩個以上空白
            Regex replaceSpace = new Regex(@"\s{2,}", RegexOptions.IgnoreCase);

            s = replaceSpace.Replace(s, "").Trim();

            StringBuilder sb = new StringBuilder();
            //從後面取
            for (int i = split.Length - 1; i >= 0; i--)
            {
                if (sb.Length > 0)
                {
                    //if(i)
                    //前面正則已經把空白去掉了,這邊依照題目要求補上
                    sb.Append(" ");
                }
                sb.Append(split[i]);
            }

            string aa = "";
            aa = sb.ToString();

            aa = replaceSpace.Replace(aa, " ").Trim();

            return aa;
        }

        /// <summary>
        /// 要去除連續空白
        /// 單字與單字間 要空白隔開
        /// 反向排序
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReverseWords(string s)
        {

            if (s == "")
                return s;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                    continue;

                int start = i;
                while (i < s.Length && s[i] != ' ')
                    i++;

                if (sb.Length == 0)
                    sb.Append(s.Substring(start, i - start));
                else
                    sb.Insert(0, s.Substring(start, i - start) + " ");
            }

            return sb.ToString();
        }

    }
}
