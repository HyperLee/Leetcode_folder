using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_028
{
    internal class Program
    {
        /// <summary>
        /// 28. Find the Index of the First Occurrence in a String
        /// https://leetcode.com/problems/find-the-index-of-the-first-occurrence-in-a-string/
        /// 28. 找出字符串中第一个匹配项的下标
        /// https://leetcode.cn/problems/find-the-index-of-the-first-occurrence-in-a-string/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string haystack = "butsad";
            string needle = "sad";

            Console.WriteLine(StrStr(haystack, needle));
            Console.ReadKey();

        }


        /// <summary>
        /// ref: 暴力法
        /// https://leetcode.cn/problems/find-the-index-of-the-first-occurrence-in-a-string/solutions/732236/shi-xian-strstr-by-leetcode-solution-ds6y/
        /// 
        /// 從 haystack 裡面 找到與 needle 相同的字串; 簡單說就是要在 haystack 子字串中找到與 needle 字串相同
        /// 回傳 haystack 第一次出現的起始位置( index ); 可能會出現多次相同子字串, 所以回傳第一次出現的位置即可
        /// 找不到就回傳 -1
        /// 
        /// 注意外層迴圈
        /// i + m <= n
        /// 
        /// 進階一點就是KMP演算法
        /// https://zh.wikipedia.org/zh-tw/KMP%E7%AE%97%E6%B3%95
        /// https://leetcode.cn/problems/find-the-index-of-the-first-occurrence-in-a-string/solutions/575568/shua-chuan-lc-shuang-bai-po-su-jie-fa-km-tb86/
        /// 
        /// 較複雜且沒聽過,
        /// 先不實作.
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <returns></returns>
        public static int StrStr(string haystack, string needle)
        {
            int n = haystack.Length;
            int m = needle.Length;

            for(int i = 0; i + m <= n; i++)
            {
                bool flag = true;

                for(int j = 0; j < m; j++)
                {
                    //string tempa = haystack[i + j].ToString();
                    //string tempb = needle[j].ToString();

                    // haystack 與 needle 有相同才需要比對;否則就直接下一輪(下一個 i)比對
                    // 減少非必要之比對, 避免浪費時間
                    if (haystack[i + j] != needle[j])
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag == true)
                {
                    return i;
                }
            }

            return -1;
        }

    }
}
