using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2108
{
    internal class Program
    {
        /// <summary>
        /// 2108. Find First Palindromic String in the Array
        /// https://leetcode.com/problems/find-first-palindromic-string-in-the-array/description/?envType=daily-question&envId=2024-02-13
        /// 2108. 找出数组中的第一个回文字符串
        /// https://leetcode.cn/problems/find-first-palindromic-string-in-the-array/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "abc", "car", "ada", "racecar", "cool" };
            
            Console.WriteLine(FirstPalindrome(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 找出第一個回文的字串
        /// 
        /// 回文慣例解法 就是雙指標
        /// 一左一右 只要遇到不同 就不是回文
        /// 
        /// 之前有寫過 忘記題目編號
        /// 
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static string FirstPalindrome(string[] words)
        {
            foreach (var word in words) 
            {
                if(IsPalindrome(word) == true)
                {
                    return word;
                }
            }

            return "";

        }


        /// <summary>
        /// 雙指針 檢查 每一個word
        /// 
        /// 回文慣例解法 就是雙指標
        /// 一左一右 只要遇到不同 就不是回文
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool IsPalindrome(string word)
        {
            int left = 0, right = word.Length - 1;
            while (left < right)
            {
                if (word[left] != word[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }


    }
}
