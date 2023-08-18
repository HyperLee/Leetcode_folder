using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_290
{
    internal class Program
    {
        /// <summary>
        /// 290. Word Pattern
        /// https://leetcode.com/problems/word-pattern/
        /// 290. 单词规律
        /// https://leetcode.cn/problems/word-pattern/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string pattern = "abba";
            string s = "dog cat cat dog";

            Console.WriteLine(WordPattern(pattern, s));
            Console.ReadKey();

        }


        /// <summary>
        /// 參考
        /// https://leetcode.cn/problems/word-pattern/solutions/1458479/by-stormsunshine-lcw5/
        /// 
        /// 初步理解為 pattern 每一個單字
        /// 對應至
        /// s 裡面的每一個word (有用空白隔開)
        /// 只要對應相同就回傳 true
        /// 否則false
        /// 
        /// pattern = "abba", s = "dog cat cat dog"
        /// a => dog
        /// b => cat
        /// ===========> 相同
        /// 
        /// 須注意是雙向對應 不是單方面比對即可
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="s"></param>
        /// <returns></returns>

        public static bool WordPattern(string pattern, string s)
        {
            // 宣告需要注意 兩者是相反 char, string
            Dictionary<char, string> dic1 = new Dictionary<char, string>();
            Dictionary<string, char> dic2 = new Dictionary<string, char>();
            int length = pattern.Length;
            string[] arr = s.Split(new char[] { ' ' });

            // 兩邊長度需要相同
            if(arr.Length != length)
            {
                return false;
            }

            for(int i = 0; i < length; i++)
            {
                char c = pattern[i];
                string word = arr[i];
                
                if(!dic1.ContainsKey(c))
                {
                    // 不存在 就加入
                    dic1.Add(c, word);
                }
                else if (!dic1[c].Equals(word))
                {
                    // 已存在, 兩邊比對需要相同
                    return false;
                }

                if (!dic2.ContainsKey(word))
                {
                    // 不存在 就加入
                    dic2.Add(word, c);
                }
                else if (dic2[word] != c)
                {
                    // 已存在, 兩邊比對需要相同
                    return false;
                }

            }

            return true;

        }


    }
}
