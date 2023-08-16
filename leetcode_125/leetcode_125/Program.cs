using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_125
{
    internal class Program
    {
        /// <summary>
        /// 125. Valid Palindrome
        /// https://leetcode.com/problems/valid-palindrome/
        /// 125. 验证回文串
        /// https://leetcode.cn/problems/valid-palindrome/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "A man, a plan, a canal: Panama";

            Console.WriteLine(IsPalindrome(s));

            Console.ReadKey();
        }


        /// <summary>
        /// 雙指針 前後縮攏 比對
        /// 只要有不同 就是false
        /// 
        /// https://leetcode.cn/problems/valid-palindrome/solutions/1768293/by-stormsunshine-2y6d/
        /// https://leetcode.cn/problems/valid-palindrome/solutions/292148/yan-zheng-hui-wen-chuan-by-leetcode-solution/
        /// 
        /// 1.轉小寫
        /// 2.指比對數字 or 字母
        /// 
        /// 直接在原始字串上比對
        /// 不另開新的暫存
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPalindrome(string s)
        {
            int left = 0, right = s.Length - 1;

            while(left < right)
            {
                // 非 數字 or 字母 直接跳過
                while(left < right && !char.IsLetterOrDigit(s[left]))
                {
                    left++;
                }

                // 非 數字 or 字母 直接跳過
                while (left < right && !char.IsLetterOrDigit(s[right]))
                {
                    right--;
                }

                // 前後不同 直接false
                if (char.ToLower(s[left]) != char.ToLower(s[right]))
                {
                    return false;
                }

                // 下一輪
                left++;
                right--;
            }

            return true;
        }

    }
}
