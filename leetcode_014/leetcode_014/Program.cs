using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_014
{
    internal class Program
    {
        /// <summary>
        /// 14. Longest Common Prefix
        /// https://leetcode.com/problems/longest-common-prefix/
        /// 
        /// 14. 最长公共前缀
        /// https://leetcode.cn/problems/longest-common-prefix/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "flower", "flow", "flight" };
            Console.WriteLine(LongestCommonPrefix(input));
            Console.ReadKey();
        }

        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/longest-common-prefix/solution/by-lockzipeel-q64w/
        /// 
        /// 先找出輸入 strs 陣列中, 最短的字串
        /// 利用 最短字串長度 與 最短字串
        /// 去跟 "其他組字串" 來做文字比對
        /// 找出 相同的 文字出來
        /// 找到相同就加入 共同前綴 
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string LongestCommonPrefix(string[] strs)
        {
            // 求出最短字串長度
            int shortlength = int.MaxValue;
            // 最短字串
            string shortstring = "";
            string res = "";

            // 找出最短字串以及長度
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].Length < shortlength)
                {
                    // 最短字串長度
                    shortlength = strs[i].Length;
                    // 最短字串
                    shortstring = strs[i];
                }
            }

            // 遍歷 "最短的字串" 和陣列中 "其他字串" 相比較, 比較次數上限為 "最短字串的長度"
            for (int i = 0; i < shortlength; i++)
            {
                // j: strs 陣列中, 第 j 個字串
                for (int j = 0; j < strs.Length; j++)
                {
                    if (shortstring[i] != strs[j][i])
                    {
                        // 不存在回傳空字串
                        return res;
                    }
                }

                // 找到共同前綴字, 加入
                res += shortstring[i];
            }

            return res;
        }


    }
}
