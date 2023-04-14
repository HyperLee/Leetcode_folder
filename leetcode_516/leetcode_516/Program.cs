using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_516
{
    internal class Program
    {
        /// <summary>
        /// leetcode 516 Longest Palindromic Subsequence
        /// https://leetcode.com/problems/longest-palindromic-subsequence/
        /// 516. 最长回文子序列
        /// https://leetcode.cn/problems/longest-palindromic-subsequence/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "aba";
            Console.WriteLine(LongestPalindromeSubseq(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 思維參考:
        /// https://leetcode.cn/problems/longest-palindromic-subsequence/solution/zi-xu-lie-wen-ti-tong-yong-si-lu-zui-chang-hui-wen/
        /// https://leetcode.cn/problems/longest-palindromic-subsequence/solution/dong-tai-gui-hua-si-yao-su-by-a380922457-3/
        /// https://leetcode.cn/problems/longest-palindromic-subsequence/solution/zui-chang-hui-wen-zi-xu-lie-by-leetcode-hcjqp/
        /// 
        /// 需要先閱讀過 解題思維 才比較好理解
        /// 用二維陣列去做解題
        /// 
        /// 由于状态转移方程都是从长度较短的子序列向长度较长的子序列转移，因此需要注意动态规划的循环顺序。
        /// 找尋方式 簡單說就是
        /// 二維陣列的中間斜線 dp[i, i] = 1 
        /// 以及 dp[i,i]的左下 都不用找
        /// 只需要找 dp[i, i] 右下 => 右上 即可
        /// 然後是 由下往上(i 由大至小)
        /// 以及 左往右(j 小至大)
        /// 找尋字串由短至長
        /// 反向找尋
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LongestPalindromeSubseq(string s)
        {
            int n = s.Length;
            int[,] dp = new int[n, n];

            // 反着遍历保证正确的状态转移
            for (int i = n - 1; i >= 0; i--)
            {
                dp[i, i] = 1;
                //char c1 = s[i];

                // 下至上
                for (int j = i + 1; j < n; j++)
                {
                    // 狀態轉移; 左至右
                    //char c2 = s[j];
                    //if (c1 == c2)
                    if (s[i] == s[j])
                    {
                        // 頭尾相等 就在 dp[i + 1, j - 1] 基礎上 加2(頭尾)
                        dp[i, j] = dp[i + 1, j - 1] + 2;
                    }
                    else
                    {
                        // 看哪個字串(s[i+1..j] 和 s[i..j-1])產生的回文字串最長
                        dp[i, j] = Math.Max(dp[i + 1, j], dp[i, j - 1]);
                    }
                }
            }

            // 整个 s 的最长回文子串长度
            return dp[0, n - 1];

        }


    }
}
