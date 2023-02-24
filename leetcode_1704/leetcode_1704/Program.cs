using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1704
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1704
        /// https://leetcode.com/problems/determine-if-string-halves-are-alike/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "";
            input = "book";

            //string b = input.Substring(input.Length / 2);
            //Console.WriteLine(b);

            Console.WriteLine(HalvesAreAlike(input));
            Console.ReadKey();

        }

        /// <summary>
        /// https://leetcode.cn/problems/determine-if-string-halves-are-alike/solution/pan-duan-zi-fu-chuan-de-liang-ban-shi-fo-d21g/
        /// 
        /// 现在我们将给定字符串 ss 拆分成长度相同的两半，前一半
        /// 表示为字符串 aa，后一半为字符串 bb，我们需要判断字符串 aa 和 bb 是否「相似」，那么我们只需要
        /// 按照「相似」的定义统计字符串 aa 和 bb 中的元音字母的个数是否相等即可。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool HalvesAreAlike(string s)
        {
            string a = s.Substring(0, s.Length / 2); //取前半部
            string b = s.Substring(s.Length / 2); // 取後半部
            string h = "aeiouAEIOU";
            int sum1 = 0, sum2 = 0;
            foreach (char c in a)
            {
                if (h.IndexOf(c) >= 0)
                {
                    sum1++;
                }
            }
            foreach (char c in b)
            {
                if (h.IndexOf(c) >= 0)
                {
                    sum2++;
                }
            }
            return sum1 == sum2;

        }


    }
}
