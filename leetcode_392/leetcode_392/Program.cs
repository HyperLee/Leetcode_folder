using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_392
{
    internal class Program
    {
        /// <summary>
        /// 392. Is Subsequence
        /// https://leetcode.com/problems/is-subsequence/
        /// 392. 判断子序列
        /// https://leetcode.cn/problems/is-subsequence/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "abc";
            string t = "ahbgdc";

            Console.WriteLine(IsSubsequence(s, t));
            Console.ReadKey();

        }

        /// <summary>
        /// 给定字符串 s 和 t ，判断 s 是否为 t 的子序列。
        /// s小
        /// t大
        /// https://leetcode.cn/problems/is-subsequence/solutions/346539/pan-duan-zi-xu-lie-by-leetcode-solution/
        /// 
        ///  (i.e., "ace" is a subsequence of "abcde" while "aec" is not).
        ///  需要注意 子序列 順序不能變
        ///  所以不能單純用 contains 比對文字
        ///  
        ///  採用類雙指針, 比對
        ///  相符就 count++, 
        ///  當最後的 count 與 輸入的s 長度相符合
        ///  就是成立
        ///  
        /// 子序列
        /// https://zh.wikipedia.org/zh-tw/%E5%AD%90%E5%BA%8F%E5%88%97
        /// 在數學中，某個序列的子序列是從最初序列通過去除某些元素但不破壞餘下元素的相對位置（在前或在後）而形成的新序列。 
        /// 子序列可以包含連續或不連續的字元。
        /// 子序列不能改變原始字串中字元的順序
        /// 
        /// 要注意這是子序列, 不是子字串
        /// 只要出現文字相同且順序一致即可
        /// 不用連續相鄰
        /// 
        /// </summary>
        /// <param name="s">子序列(小)</param>
        /// <param name="t">原始字串(大)</param>
        /// <returns></returns>
        public static bool IsSubsequence(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;

            int i = 0, j = 0;

            while(i < n && j < m)
            {
                // 文字相符就 ++
                if (s[i] == t[j])
                {
                    i++;
                }

                // 字串 t 繼續往右走
                j++;
            }

            // 相符的數量需要與輸入的子序列 s 長度相符合
            return i == n;

        }

    }
}
