using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_139
{
    internal class Program
    {
        /// <summary>
        /// Word Break
        /// https://leetcode.com/problems/word-break/
        /// 139. 单词拆分
        /// https://leetcode.cn/problems/word-break/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "leetcode";
            IList<string> wordDict = new List<string>();
            wordDict.Add("leet");
            wordDict.Add("code");

            Console.WriteLine(WordBreak(s, wordDict));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/word-break/solution/dan-ci-chai-fen-by-leetcode-solution/
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="wordDict"></param>
        /// <returns></returns>
        public static bool WordBreak(string s, IList<string> wordDict)
        {
            var worsDictSet = new HashSet<string>(wordDict);

            var dp = new bool[s.Length + 1];
            dp[0] = true;

            for(int i = 1; i <= s.Length; i++)
            {
                for(int j = 0; j < i; j++)
                {
                    string t_a = dp[j].ToString();
                    string t_b = s.Substring(j, i - j);
                    if (dp[j] && worsDictSet.Contains(s.Substring(j, i - j)))
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }

            return dp[s.Length];
        }

    }
}
