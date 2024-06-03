namespace leetcode_2486
{
    internal class Program
    {
        /// <summary>
        /// 2486. Append Characters to String to Make Subsequence
        /// https://leetcode.com/problems/append-characters-to-string-to-make-subsequence/?envType=daily-question&envId=2024-06-03
        /// 2486. 追加字符以获得子序列
        /// https://leetcode.cn/problems/append-characters-to-string-to-make-subsequence/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "coaching";
            string t = "coding";

            Console.WriteLine(AppendCharacters(s, t));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/append-characters-to-string-to-make-subsequence/solutions/1993448/tan-xin-pi-pei-by-endlesscheng-d6eq/
        /// https://leetcode.cn/problems/append-characters-to-string-to-make-subsequence/solutions/2602537/2486-zhui-jia-zi-fu-yi-huo-de-zi-xu-lie-ghzhg/
        /// 
        /// sIndex 與 tIndex
        /// 分別為s與t的下標位置
        /// 初始時候都為0
        /// 
        /// 當s[sIndex] == t[tIndex], 此時 sIndex 與 tIndex 都向右移動
        /// 因為兩個字母相同
        /// 
        /// 反之當 s[sIndex] != t[tIndex] 此時 sIndex向右移動
        /// 
        /// 遍歷整個輸入字串s長度, 
        /// 
        /// 最後結果計算為 輸入字串t 
        /// 總長度扣除兩者相同長度 
        /// n - tIndex
        /// 不相同者字母即為需要加入s中的長度
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int AppendCharacters(string s, string t)
        {
            int m = s.Length, n = t.Length;
            int sIndex = 0, tIndex = 0;

            while(sIndex < m && tIndex < n) 
            {
                // 兩個輸入字串, 字母相同時候. s與t 下標往右移動
                if (s[sIndex] == t[tIndex])
                {
                    tIndex++;
                }

                // 兩個輸入字串 字母不相同時候, s下標單獨移動
                sIndex++;
            }

            // 總長度扣除兩者相同長度即為答案
            return n - tIndex;
        }
    }
}
