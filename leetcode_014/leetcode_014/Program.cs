using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_014
{
    internal class Program
    {
        /// <summary>
        /// leetcode 014
        /// https://leetcode.com/problems/longest-common-prefix/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "flow", "flower" };
            Console.WriteLine(LongestCommonPrefix(input));
            Console.ReadKey();
        }

        /// <summary>
        /// https://leetcode.cn/problems/longest-common-prefix/solution/by-lockzipeel-q64w/
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string LongestCommonPrefix(string[] strs)
        {
            int a = int.MaxValue;//求最短字符串长度
            int b = 0;//最长相同的字符串长度
            string d = "";//最短字符段的显示
            string c = "";
            //通过下面的for循环，可以求出最短字符串及其长度
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].Length < a)
                {
                    a = strs[i].Length;
                    d = strs[i];
                }
            }
            for (int i = 0; i < a; i++)//遍历最短的字符串和其他字符串相比较，比较次数上限为最短字符串的长度
            {
                for (int j = 0; j < strs.Length; j++)
                {
                    if (d[i] != strs[j][i])
                    {
                        return c;
                    }
                }
                c += d[i];
            }
            return c;
        }


    }
}
