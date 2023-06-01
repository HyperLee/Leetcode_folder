using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1816
{
    internal class Program
    {
        /// <summary>
        /// 1816. Truncate Sentence
        /// https://leetcode.com/problems/truncate-sentence/
        /// 1816. 截断句子
        /// https://leetcode.cn/problems/truncate-sentence/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "Hello how are you Contestant";
            int k = 4;

            Console.WriteLine(TruncateSentence(s, k));
            Console.ReadKey();
        }


        /// <summary>
        /// StringBuilder 用來串接 解答回傳
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string TruncateSentence(string s, int k)
        {
            StringBuilder sb = new StringBuilder();

            int length = 0;

            // 取出長度
            if( s.Length > k)
            {
                length = k;
            }
            else
            {
                // k 比 s.Length大, 就直接取　s.Length
                length = s.Length;
            }

            // 空白隔開
            string[] subs = s.Split(' ');

            for (int i = 0; i < length; i++)
            {
                sb.Append(subs[i]);

                // 不要讓單字黏再一起
                if(i != length - 1)
                {
                    sb.Append(' ');
                }
            }

            return sb.ToString();
        }
    }
}
