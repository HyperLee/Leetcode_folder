using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_187
{
    internal class Program
    {
        /// <summary>
        /// 187. Repeated DNA Sequences
        /// https://leetcode.com/problems/repeated-dna-sequences/
        /// 187. 重复的DNA序列
        /// https://leetcode.cn/problems/repeated-dna-sequences/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT";
            //Console.WriteLine(FindRepeatedDnaSequences(s));
            FindRepeatedDnaSequences(s);
            Console.ReadKey();

        }

        const int L = 10;
        /// <summary>
        /// https://leetcode.cn/problems/repeated-dna-sequences/solution/zhong-fu-de-dnaxu-lie-by-leetcode-soluti-z8zn/
        /// 
        /// 統計相同長度 出現次數 
        /// 當 為2 時候判斷為 重覆
        /// 加入至解答中
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IList<string> FindRepeatedDnaSequences(string s)
        {
            IList<string> result = new List<string>();
            Dictionary<string, int> cnt = new Dictionary<string, int>();
            int n = s.Length;

            for(int i = 0; i <= n - L; i++)
            {
                string sub = s.Substring(i, L);

                if(!cnt.ContainsKey(sub))
                {
                    cnt.Add(sub, 1);
                }
                else
                {
                    cnt[sub]++;
                }

                if (cnt[sub] == 2) 
                {
                    result.Add(sub);
                }

            }


            Console.WriteLine("[");
            foreach(string sub in result)
            {
                Console.WriteLine(sub + ", ");
            }
            Console.WriteLine("]");

            return result;
        }

    }
}
