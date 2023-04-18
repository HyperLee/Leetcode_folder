using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1768
{
    internal class Program
    {
        /// <summary>
        /// 1768. Merge Strings Alternately
        /// https://leetcode.com/problems/merge-strings-alternately/
        /// 1768. 交替合并字符串
        /// https://leetcode.cn/problems/merge-strings-alternately/
        /// 
        /// 兩個字串依序交叉組合成新字串
        /// 如果有某字串特別長 那就把多餘的放在 新字串後面
        /// 
        /// ex:  
        /// w1 = abc, w2 = pqr
        ///  new =>  apbqr
        ///  
        /// w1 = abc4, w2 = pqr
        /// new => apbqcr4
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string w1 = "abc";
            string w2 = "pqr77";

            Console.WriteLine(MergeAlternately(w1, w2));

            Console.ReadKey();
        }


        /// <summary>
        /// 利用 StringBuilder來整合成新字串
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        public static string MergeAlternately(string word1, string word2)
        {
            int n1 = word1.Length;
            int n2 = word2.Length;

            // 取兩者共同都有的長度 出來跑迴圈
            int n = Math.Min(n1, n2);

            // 交叉寫入 sb
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < n; i++)
            {
                sb.Append(word1[i]);
                sb.Append(word2[i]);
            }


            // 超長部分 擷取出來之後 再放到字尾
            // 基於共同都有的長度(n)之下 再組合 多出的 diff 長度
            if(n1 > n)
            {
                int diff = n1 - n;

                sb.Append(word1.Substring(n, diff));
            }

            if(n2 > n)
            {
                int diff = n2 - n;
                sb.Append(word2.Substring(n, diff));
            }

            return sb.ToString();

        }

    }
}
