namespace leetcode_3110
{
    internal class Program
    {
        /// <summary>
        /// 3110. Score of a String
        /// https://leetcode.com/problems/score-of-a-string/description/?envType=daily-question&envId=2024-06-01
        /// 3110. 字符串的分数
        /// https://leetcode.cn/problems/score-of-a-string/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "hello";
            Console.WriteLine(ScoreOfString(input));
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/score-of-a-string/solutions/2738900/bian-li-pythonjavacgo-by-endlesscheng-x63p/
        /// https://leetcode.cn/problems/score-of-a-string/solutions/2739065/3110-zi-fu-chuan-de-fen-shu-by-stormsuns-4yuk/
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ScoreOfString(string s)
        {
            int res = 0;
            int n = s.Length;

            for(int i = 1; i < n; i++)
            {
                //int pre = s[i - 1];
                //int next = s[i];

                // 根據題目要求 前面減後面 取絕對值
                // 累加 計算差值
                res += Math.Abs(s[i - 1] - s[i]);
            }

            return res;
        }
    }
}
