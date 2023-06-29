using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_953
{
    internal class Program
    {
        /// <summary>
        /// leetcode 953. Verifying an Alien Dictionary
        /// https://leetcode.com/problems/verifying-an-alien-dictionary/
        /// 
        /// 953. 验证外星语词典
        /// https://leetcode.cn/problems/verifying-an-alien-dictionary/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // word[0] = hello, word[1] = leetcode; 這兩者比對
            // 不是 hello裡面自己順序對比
            string[] word = { "hello", "leetcode" };
            string order = "hlabcdefgijkmnopqrstuvwxyz";
            Console.WriteLine(IsAlienSorted(word, order));

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/verifying-an-alien-dictionary/comments/
        /// https://leetcode.cn/problems/verifying-an-alien-dictionary/solution/yan-zheng-wai-xing-yu-ci-dian-by-leetcod-jew7/
        /// https://leetcode.cn/problems/verifying-an-alien-dictionary/solution/ha-xi-biao-by-long-yu-8-99mu/
        /// 
        /// 比對輸入的string 要依據 題目給的 order 排序
        /// 前一個字母跟後一個字母 要依據 order 排序
        /// 
        /// 題目給的範例需要詳細閱讀與觀察
        /// case1, 2: 比较每个单词出现的第一个不同的字母，如果index小于后面的单词，就不再比较 return false
        /// case3: 如果没出现不同字母&&前一个单词的长度比后一个单词长度大，返回false
        /// </summary>
        /// <param name="words"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool IsAlienSorted(string[] words, string order)
        {
            int[] index = new int[26];

            // 把題目給的字典順序大小轉成 index序列
            for (int i = 0; i < order.Length; ++i)
            {
                index[order[i] - 'a'] = i;
            }

            // 依序檢查第 i 與i - 1 字典順序大小
            // i > i - 1  return true;
            // i < i - 1  return false; 
            for (int i = 1; i < words.Length; i++)
            {
                bool valid = false;

                // j 要在word長度範圍內, 不能超出
                for (int j = 0; j < words[i - 1].Length && j < words[i].Length; j++)
                {
                    int prev = index[words[i - 1][j] - 'a'];
                    int curr = index[words[i][j] - 'a'];

                    if (prev < curr)
                    {
                        valid = true;
                        break;
                    }
                    else if (prev > curr)
                    {
                        return false;
                    }
                }

                if (!valid)
                {
                    // 當 輸入的字母 前面的比後面的長度還要長
                    // 直接回傳 false; 
                    if (words[i - 1].Length > words[i].Length)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
