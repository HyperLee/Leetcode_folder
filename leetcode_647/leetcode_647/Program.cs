using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_647
{
    internal class Program
    {
        /// <summary>
        /// leetcode 647 
        /// https://leetcode.com/problems/palindromic-substrings/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "aaa";
            Console.WriteLine(CountSubstrings(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.com/problems/palindromic-substrings/discuss/105717/Beats-95-C
        /// 
        /// https://leetcode.cn/problems/palindromic-substrings/solution/hui-wen-zi-chuan-by-leetcode-solution/
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int CountSubstrings(String s)
        {
            int count = 0;
            if (s == null || s.Length == 0) return count;

            for (int i = 0; i < s.Length; i++)
            {
                count += CheckForPalindrome(i, i, s) + CheckForPalindrome(i, i + 1, s);
            }
            return count;
        }


        private static int CheckForPalindrome(int start, int end, string s)
        {
            int count = 0;
            while (start >= 0 && end < s.Length && s[start] == s[end])
            {
                count++; start--; end++;
            }

            return count;
        }

    }
}
