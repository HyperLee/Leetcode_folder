namespace leetcode_1002
{
    internal class Program
    {
        /// <summary>
        /// 1002. Find Common Characters
        /// https://leetcode.com/problems/find-common-characters/description/?envType=daily-question&envId=2024-06-05
        /// 1002. 查找共用字符
        /// https://leetcode.cn/problems/find-common-characters/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "bella", "label", "roller" };
            var output = CommonChars(input);
            foreach (var c in output) 
            {
                Console.WriteLine(c);
            }

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/find-common-characters/solutions/445468/cha-zhao-chang-yong-zi-fu-by-leetcode-solution/
        /// https://leetcode.cn/problems/find-common-characters/solutions/445914/1002-cha-zhao-chang-yong-zi-fu-ha-xi-fa-jing-dian-/
        /// https://leetcode.cn/problems/find-common-characters/solutions/1458604/by-stormsunshine-83l2/
        /// 
        /// 共用字符 也就是從words裡面的每一個 str, 
        /// 找出str中每個char 出現次數最小者
        /// 持續更新 minfreq.
        /// 最後再從 minfreq中找出不為0者
        /// 即是共通字符
        /// 
        /// 1.統計每一個char in str
        /// 2.把words每一個str都統計過, 持續更新 minfreq
        /// 3.全部words都遍歷過後, 從 minfreq找出不為0者
        /// 
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static IList<string> CommonChars(string[] words)
        {
            // 統計共通char 出現次數
            int[] minfreq = new int[26];
            // 預設給最大, 目標是找出最小
            Array.Fill(minfreq, int.MaxValue);

            foreach(string str in words)
            {
                // 26個英文字母大小
                int[] counts = new int[26];
                foreach(char c in str)
                {
                    // 統計 str 中每個 char 次數
                    counts[c - 'a']++;
                }

                for(int i = 0; i < 26; i++)
                {
                    // 更新次數, 從每一個str中, 找出每個char出現次數最小
                    // minfreq是全部str共用更新的, 後面的str會覆蓋前面str次數
                    minfreq[i] = Math.Min(minfreq[i], counts[i]);
                }
            }

            IList<string> letters = new List<string>();
            for(int i = 0; i < 26; i++)
            {
                // i 轉成英文 char a ~ z, 要輸出答案用
                string letter = ((char)('a' + i)).ToString();
                // 取出統計次數
                int count = minfreq[i];

                // minfreq累計次數大於等於1次者才會加入答案
                for (int j = 0; j < count; j++)
                {
                    // 加入 letters
                    letters.Add(letter);
                }
            }

            return letters;
        }
    }
}
