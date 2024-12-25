using System.Text;

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
        /// new => apbqr
        ///  
        /// w1 = abc4, w2 = pqr
        /// new => apbqcr4
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string w1 = "abc";
            string w2 = "pqr";

            Console.WriteLine("res: " + MergeAlternately(w1, w2));

        }


        /// <summary>
        /// 利用 StringBuilder sb 來整合成新字串
        /// 
        /// 1.先計算出兩個輸入字串都有交集的共通長度
        /// 2.取出 n 之後先交叉寫入 sb
        /// 3.計算各字串超出共通長度部分
        /// 4. 承上3, 寫入 sb
        /// 
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        public static string MergeAlternately(string word1, string word2)
        {
            int n1 = word1.Length;
            int n2 = word2.Length;

            // 取兩者最大共同長度, 出來跑迴圈
            int n = Math.Min(n1, n2);

            // 1.先交叉寫入兩者共同長度部分文字
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < n; i++)
            {
                sb.Append(word1[i]);
                sb.Append(word2[i]);
            }

            // 2. 再來處理超出共同長度部分
            // 計算出超長部分長度 diff, 在寫入 sb 裡面
            if(n1 > n)
            {
                int diff = n1 - n;
                // 擷取長度: n 到 diff
                sb.Append(word1.Substring(n, diff));
            }

            if(n2 > n)
            {
                int diff = n2 - n;
                // 擷取長度: n 到 diff
                sb.Append(word2.Substring(n, diff));
            }

            return sb.ToString();
        }
    }
}
