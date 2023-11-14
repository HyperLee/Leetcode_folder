using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1930
{
    internal class Program
    {
        /// <summary>
        /// 1930. Unique Length-3 Palindromic Subsequences
        /// https://leetcode.com/problems/unique-length-3-palindromic-subsequences/?envType=daily-question&envId=2023-11-14
        /// 1930. 长度为 3 的不同回文子序列
        /// https://leetcode.cn/problems/unique-length-3-palindromic-subsequences/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "aabca";
            Console.WriteLine(CountPalindromicSubsequence(s));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.com/problems/unique-length-3-palindromic-subsequences/solutions/4285483/beats-96-16-easiest-approach-beginner-friendly-explanation/?envType=daily-question&envId=2023-11-14
        /// https://leetcode.cn/problems/unique-length-3-palindromic-subsequences/solutions/870024/chang-du-wei-3-de-bu-tong-hui-wen-zi-xu-21trj/
        /// 說明類似 官方 方法一：枚举两侧的字符
        /// 
        /// 雙指針作法
        /// 第一次出現位置:左邊界, 最後一次出現位置: 右邊界
        /// 所以回文會出現在 邊界內
        /// 所以從邊界內去找出子字符串
        /// 計算在範圍內字符串 字符的種類有多少
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int CountPalindromicSubsequence(string s)
        {
            int count = 0;

            for(int i = 0; i < 26; i++)
            {
                // 轉成 ascii 小寫 a開始
                char currentchar = (char)(i + 97);
                // currentchar 第一次出現位置
                int left = s.IndexOf(currentchar);
                // currentchar 最後一次出現位置
                int right = s.LastIndexOf(currentchar);

                // 左右不為空且 左小於右
                if(left != -1 && right != -1 && left < right)
                {
                    // 利用哈希集合统计 s[left + 1..right - left - 1] 子串的字符总数，并更新答案
                    //string tempa = s.Substring(left + 1, right - left - 1);

                    count += new HashSet<char>(s.Substring(left + 1, right - left - 1)).Count;
                }
            }

            return count;
        }

    }
}
