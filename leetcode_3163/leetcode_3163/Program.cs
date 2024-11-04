using System.Text;

namespace leetcode_3163
{
    internal class Program
    {
        /// <summary>
        /// 3163. String Compression III
        /// https://leetcode.com/problems/string-compression-iii/description/?envType=daily-question&envId=2024-11-04
        /// 
        /// 3163. 压缩字符串 III
        /// https://leetcode.cn/problems/string-compression-iii/description/
        /// 
        /// 從一個空字串 comp 開始。當 word 不為空時，執行以下操作：
        ///  從 word 中移除一個最長的前綴，該前綴由單個字符 c 重複最多 9 次構成。
        ///  將該前綴的長度接在字符 c 之後，然後附加到 comp 中。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string word = "aaaaaaaaaaaaaabb";
            Console.WriteLine(CompressedString(word));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/string-compression-iii/solutions/2790666/mo-ni-pythonjavacgo-by-endlesscheng-3hk7/
        /// https://leetcode.cn/problems/string-compression-iii/solutions/2790748/3163-ya-suo-zi-fu-chuan-iii-by-stormsuns-rtpi/
        /// 
        /// 簡單說就是統計連續相同 char 長度最長為 9, 超過就歸零重新累計
        /// 
        /// 輸出樣式: 數字在前, 字母在後
        /// 
        /// 使用 char 計算長度與比對前後字母是否相同
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string CompressedString(string word)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            int length = word.Length;
            for(int i = 0; i < length; i++)
            {
                char c = word[i];
                count++;
                // char 長度 9, i 達到字串長度尾端, 下一個 char 不相同
                if(count == 9 || i == length - 1 || c != word[i + 1])
                {
                    sb.Append(count);
                    sb.Append(c);
                    // 重新計算
                    count = 0;
                }
            }

            return sb.ToString();
        }
    }
}
