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
        /// https://leetcode.cn/problems/unique-length-3-palindromic-subsequences/solutions/869731/c-xun-zhao-hui-wen-guan-jian-huan-shi-yi-264r/
        /// https://leetcode.com/problems/unique-length-3-palindromic-subsequences/solutions/4285483/beats-96-16-easiest-approach-beginner-friendly-explanation/?envType=daily-question&envId=2023-11-14
        /// https://leetcode.cn/problems/unique-length-3-palindromic-subsequences/solutions/870024/chang-du-wei-3-de-bu-tong-hui-wen-zi-xu-21trj/
        /// 說明類似 官方 方法一：枚举两侧的字符
        /// 
        /// 雙指針作法
        /// 第一次出現位置:左邊界, 最後一次出現位置: 右邊界
        /// 所以回文會出現在 邊界內
        /// 所以從邊界內去找出子字符串
        /// 計算在範圍內字符串 字符的種類有多少
        /// 
        /// 假設前後固定之後, 中間找出來有3種不重複的字符char
        /// 也就是說 我們可以組合出 3種不同迴文字串
        /// 例如 題目敘述的 Example 1:
        /// aabca
        /// 取出邊界之後會是
        /// a[abc]a  =>中間三個獨立 不同
        /// 可以組合出
        /// 1. aaa
        /// 2. aba
        /// 3. aca
        /// 題目說長度三
        /// 前後固定
        /// 中間每次取一種出來即可
        /// 可以組合出三組不重複子字符串
        /// 
        /// 如果還是看不懂可以說看連結說明
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
