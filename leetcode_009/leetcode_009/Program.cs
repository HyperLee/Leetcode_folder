using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_009
{
    class Program
    {
        /// <summary>
        /// https://leetcode.com/problems/palindrome-number/
        /// leetcode_009
        /// 
        /// 判斷回文
        /// 
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int s = 11;

            Console.WriteLine(IsPalindrome(s));
            Console.ReadKey();
        }


        /// <summary>
        /// 定义一个新的变量存储反转后的字符，再与原字符比较
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsPalindrome(int x)
        {
            string s_input = x.ToString();
            string res = "";

            for (int i = s_input.Length - 1; i >= 0; i--)
            {
                res += s_input[i];
            }

            if (res == s_input)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 官方解法
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public bool IsPalindrome2(int x)
        {
            // Special cases:
            // As discussed above, when x < 0, x is not a palindrome.
            // Also if the last digit of the number is 0, in order to be a palindrome,
            // the first digit of the number also needs to be 0.
            // Only 0 satisfy this property.
            if (x < 0 || (x % 10 == 0 && x != 0))
            {
                return false;
            }

            int revertedNumber = 0;
            while (x > revertedNumber)
            {
                revertedNumber = revertedNumber * 10 + x % 10;
                x /= 10;
            }

            // When the length is an odd number, we can get rid of the middle digit by revertedNumber/10
            // For example when the input is 12321, at the end of the while loop we get x = 12, revertedNumber = 123,
            // since the middle digit doesn't matter in palidrome(it will always equal to itself), we can simply get rid of it.
            return x == revertedNumber || x == revertedNumber / 10;
        }

    }
}
