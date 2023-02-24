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
        /// leetcode 953
        /// https://leetcode.com/problems/verifying-an-alien-dictionary/
        /// 
        /// 看不懂題目
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] word = { "hello", "leetcode" };
            string order = "hlabcdefgijkmnopqrstuvwxyz";
            Console.WriteLine(IsAlienSorted(word, order));

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/verifying-an-alien-dictionary/comments/
        /// </summary>
        /// <param name="words"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool IsAlienSorted(string[] words, string order)
        {
            int[] index = new int[26];
            for (int i = 0; i < order.Length; ++i)
            {
                index[order[i] - 'a'] = i;
            }
            for (int i = 1; i < words.Length; i++)
            {
                bool valid = false;
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
                    /* 比较两个字符串的长度 */
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
