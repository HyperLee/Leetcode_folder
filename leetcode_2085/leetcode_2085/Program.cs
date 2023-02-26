using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2085
{
    internal class Program
    {
        /// <summary>
        /// leetcode 2085
        /// https://leetcode.com/problems/count-common-words-with-one-occurrence/
        /// https://leetcode.cn/problems/count-common-words-with-one-occurrence/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input1 = new string[] { "leetcode", "is", "amazing", "as", "is" };
            string[] input2 = new string[] { "amazing", "leetcode", "is" };

            Console.WriteLine(CountWords(input1, input2));
            Console.ReadKey();
        }


        /// <summary>
        /// 利用Dictionary
        /// 
        /// Dictionary<TKey,TValue>.TryGetValue(TKey, TValue) 方法
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.dictionary-2.trygetvalue?view=net-7.0
        /// 
        /// Dictionary<TKey,TValue> 類別
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.dictionary-2?view=net-7.0
        /// </summary>
        /// <param name="words1"></param>
        /// <param name="words2"></param>
        /// <returns></returns>

        public static int CountWords(string[] words1, string[] words2)
        {
            Dictionary<string, int> dic1 = new Dictionary<string, int>();
            Dictionary<string, int> dic2 = new Dictionary<string, int>();

            int counter = 0;

            foreach (string word1 in words1) 
            {
                if(dic1.ContainsKey(word1))
                {
                    dic1[word1]++;
                }
                else
                {
                    dic1.Add(word1, 1);
                }
            }

            foreach (string word2 in words2)
            {
                if (dic2.ContainsKey(word2))
                {
                    dic2[word2]++;
                }
                else
                {
                    dic2.Add(word2, 1);
                }
            }

            foreach (var kv in dic1)
            {
                // dic1 找出 符合只有出現一次的
                if(kv.Value == 1)
                {
                    int value = 0;
                    // dic2 找出 符合只出現一次 且 文字與dic1相同者
                    if(dic2.ContainsKey(kv.Key) && dic2.TryGetValue(kv.Key, out value))
                    {
                        if(value == 1)
                        {
                            // 相同且一次 就累加
                            counter++;
                        }
                    }
                    

                    // 改寫上面判斷式
                    /* // 錯誤失敗 看起來沒法改寫; 之後再想其他方法
                    if(dic2.ContainsKey(kv.Key) && dic2.ContainsValue(1))
                    {
                        if (kv.Key == dic2.FirstOrDefault(x => x.Value == 1).Key)
                        {
                            counter++;
                        }
                    }
                    */

                }
            }

            return counter;

        }


    }
}
