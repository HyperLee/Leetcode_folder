using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_389
{
    internal class Program
    {
        /// <summary>
        /// 389. Find the Difference
        /// https://leetcode.com/problems/find-the-difference/?envType=daily-question&envId=2023-09-25
        /// 389. 找不同
        /// https://leetcode.cn/problems/find-the-difference/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "abcd", t = "abcde";
            Console.WriteLine(FindTheDifference2(s, t));
            Console.ReadKey();
        }

        /// <summary>
        /// 根據題目意思
        /// 字串t 由 字串s 演變而來
        /// 打亂排序組合以及增加字母
        /// 故字串t長度 大於字串s
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static char FindTheDifference(string s, string t)
        {
            char[] arrs = s.ToCharArray();
            char[] arrt = t.ToCharArray();
            Array.Sort(arrs);
            Array.Sort(arrt);

            //取出第一個不同的位置
            for(int i = 0; i < arrs.Length; i++)
            {
                if (arrs[i] != arrt[i])
                {
                    return arrt[i];
                }
            }

            //如果上述沒找到,就直接回傳最後一個
            return arrt[arrs.Length];
        }


        /// <summary>
        /// list作法
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static char FindTheDifference2(string s, string t)
        {
            List<char> strings = new List<char>();
            foreach(char c in s) 
            {
                strings.Add(c);
            }

            List<char> stringt = new List<char>();
            foreach(char c in t)
            {
                stringt.Add(c);
            }

            char a = 'A';
            //比對出不同取出
            foreach (char aitem in strings)
            {
                if (!stringt.Contains(aitem))
                {
                   a = aitem;
                }
            }

            // 如果上述找不到,直接給最後一個
            if(a == 'A')
            {
                a = stringt.Last();
            }

            return a;

        }


    }
}
