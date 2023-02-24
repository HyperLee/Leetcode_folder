using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1332
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1332
        /// https://leetcode.com/problems/remove-palindromic-subsequences/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "abb";

            Console.WriteLine(RemovePalindromeSub(s));
            Console.ReadKey();

        }


        /// <summary>
        /// 
        /// https://leetcode.cn/problems/remove-palindromic-subsequences/solution/shan-chu-hui-wen-zi-xu-lie-by-leetcode-s-tqtb/
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int RemovePalindromeSub(string s)
        {
            int n = s.Length;
            for (int i = 0; i < n / 2; ++i)
            {
                if (s[i] != s[n - 1 - i])
                {
                    return 2;
                }
            }
            return 1;
        }


    }
}
