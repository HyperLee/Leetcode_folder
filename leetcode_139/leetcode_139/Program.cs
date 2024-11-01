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
        /// 
        /// 給定一個字串 s 和一個字典 wordDict，如果 s 能夠被分割成由一個或多個字典中的單字所組成、以空格分隔的序列，則返回 true。
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
        /// ref: 動態規劃
        /// https://leetcode.cn/problems/word-break/solution/dan-ci-chai-fen-by-leetcode-solution/
        /// 
        /// 注意：不要求字典中出现的单词全部都使用，并且字典中的单词可以重复使用。
        /// 
        /// 我们定义 dp[i] 表示字符串 s 前 i 个字符组成的字符串 s[0..i − 1] 是否能被空格拆分成若干个字典中出现的单词。
        /// 默认 j = 0 时 s1​ 为空串
        /// 对于边界条件，我们定义 dp[0] = true 表示空串且合法。
        /// 
        /// 初始化: 
        /// 創建一個布爾數組 dp，長度為 n + 1（n 是字符串 s 的長度），用來記錄每個子字符串是否可以被劃分。
        /// 初始化 dp = true，表示空字符串可以被劃分。
        /// 
        /// 動態規劃:
        /// * 循環遍歷字符串的每個位置 i（從 1 到 n）。
        /// * 對於每個位置 i，再循環遍歷之前的所有位置 j（從 0 到 i - 1 ）。
        /// * 如果 dp[j] 為 true 且子字符串 s[j:i] 在字典中，則將 dp[i] 設置為 true。
        /// 
        /// 返回結果: 最終返回 dp[n]，這表示整個字符串是否可以被劃分。
        /// </summary>
        /// <param name="s">要比對字串</param>
        /// <param name="wordDict">字典</param>
        /// <returns></returns>
        public static bool WordBreak(string s, IList<string> wordDict)
        {
            // hash 排除重複
            var wordsDictSet = new HashSet<string>(wordDict);
            // 用來記錄每個子字符串是否可以被劃分
            var dp = new bool[s.Length + 1];
            dp[0] = true;

            for(int i = 1; i <= s.Length; i++)
            {
                // 列舉長度 i 的各種字串長度 (j) 組合, 出來比對
                for(int j = 0; j < i; j++)
                {
                    //string t_a = dp[j].ToString();
                    //string t_b = s.Substring(j, i - j);
                    
                    // 判斷字串 s 是否存在於 wordDict 中
                    // j 都從 0 開始且 dp[0] = true
                    if (dp[j] && wordsDictSet.Contains(s.Substring(j, i - j)))
                    {
                        // 更新 dp 狀態, 存在為 true
                        dp[i] = true;
                        break;
                    }
                }
            }

            return dp[s.Length];
        }

    }
}
