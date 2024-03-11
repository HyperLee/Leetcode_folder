using System.Text;

namespace leetcode_2129
{
    internal class Program
    {
        /// <summary>
        /// 2129. Capitalize the Title
        /// https://leetcode.com/problems/capitalize-the-title/description/
        /// 2129. 将标题首字母大写
        /// https://leetcode.cn/problems/capitalize-the-title/description/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "capiTalIze tHe titLe";
            Console.WriteLine(CapitalizeTitle(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 1.先分割輸入的字串, 空白隔開
        /// 2.判斷每個單字開頭要大寫還是小寫 (題目說明 長度 > 2 第一個字才要大寫其餘小寫)
        /// 3.把第一個字之後的通通轉小寫
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string CapitalizeTitle(string title)
        {
            StringBuilder sb = new StringBuilder();
            // 題目說明, 輸入的字串每個單字用空白隔開
            string[] words = title.Split(" ");
            int n = words.Length;

            for(int i = 0; i < n; i++)
            {
                string word = words[i];

                if(sb.Length > 0)
                {
                    // 字與字之間空白隔開
                    sb.Append(" ");
                }

                if(word.Length > 2)
                {
                    // 單字長度 > 2 開頭大寫
                    sb.Append(char.ToUpper(word[0]));
                }
                else
                {
                    // 否則小寫
                    sb.Append(char.ToLower(word[0]));
                }

                // 上方判斷是開頭第一個字
                // 這邊是每個單字第二個字開始
                for(int j = 1; j < word.Length; j++)
                {
                    // 加入至sb裡面, 第一個字開始轉小寫
                    sb.Append(char.ToLower(word[j]));
                }

            }

            return sb.ToString();
        }
    }
}
