namespace leetcode_3016
{
    internal class Program
    {
        /// <summary>
        /// 3016. Minimum Number of Pushes to Type Word II
        /// https://leetcode.com/problems/minimum-number-of-pushes-to-type-word-ii/description/?envType=daily-question&envId=2024-08-06
        /// 
        /// 3016. 输入单词需要的最少按键次数 II
        /// https://leetcode.cn/problems/minimum-number-of-pushes-to-type-word-ii/description/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string word = "abcde";
            Console.WriteLine(MinimumPushes(word));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/minimum-number-of-pushes-to-type-word-ii/solutions/2613399/tan-xin-jian-ji-xie-fa-pythonjavacgo-by-5l4je/
        /// https://leetcode.cn/problems/minimum-number-of-pushes-to-type-word-ii/solutions/2613661/3016-shu-ru-dan-ci-xu-yao-de-zui-shao-an-n2z2/
        /// 排序不等式
        /// https://zh.wikipedia.org/zh-tw/%E6%8E%92%E5%BA%8F%E4%B8%8D%E7%AD%89%E5%BC%8F
        /// 
        /// 注意 題目有說:
        /// 现在允许你将编号为 2 到 9 的按键重新映射到 不同 字母集合。每个按键可以映射到 任意数量 的字母，但每个字母 必须 恰好 映射到 一个 按键上。你需要找到输入字符串 word 所需的 最少 按键次数。
        /// => 也就是 
        /// 有 8 個字母只需要按下一次按鍵, 第二組 8 個字母需要按下兩次按鍵, 第三組 8 個字母需要按下三次按鍵, 第四組字母要按下四次按鍵
        /// 電話鍵盤共 12 個按鍵, 但是 注意 1，*，# 和 0 不能放入字母
        /// 所以 12 - 4 = 8 個按鍵可以放入字母而已
        /// 
        /// 為了達到題目要求, 最少按鍵次數
        /// 所以 出現頻率(次數)高的 數字 要分配在第一組, 只需要按下一次按鍵的即可
        /// 不然給他分配到第二, 三組會造成按鍵次數暴增
        /// 
        /// 頻率(次數)高 -> 按鍵次數少
        /// 反之
        /// 頻率(次數)低 -> 按鍵次數高
        /// 
        /// 出現的頻率(次數) * 按鍵次數 => 題目所求
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static int MinimumPushes(string word)
        {
            // 26 = 小寫英文字母數量
            int[] cnt = new int[26];
            foreach (char c in word)
            {
                // 統計每個字母出現次數
                cnt[c - 'a']++;
            }

            // 遞增 排序
            Array.Sort(cnt);

            int res = 0;
            // 從 26 個字母計算最少按鍵次數
            for(int i = 0; i < 26; i++)
            {
                // 出現頻率要從高的開始取, 所以是後面開始取
                // 出現頻率 * 按鍵次數
                res += cnt[25 - i] * (i / 8 + 1);
            }

            return res;

        }

    }
}
