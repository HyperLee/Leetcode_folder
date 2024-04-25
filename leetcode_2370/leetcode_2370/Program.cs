namespace leetcode_2370
{
    internal class Program
    {
        /// <summary>
        /// 2370. Longest Ideal Subsequence
        /// https://leetcode.com/problems/longest-ideal-subsequence/description/?envType=daily-question&envId=2024-04-25
        /// 2370. 最长理想子序列
        /// https://leetcode.cn/problems/longest-ideal-subsequence/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "acb";
            int k = 1;

            Console.WriteLine(LongestIdealString(s, k));
            Console.ReadKey();
        }



        /// <summary>
        /// ref:
        /// https://leetcode.com/problems/longest-ideal-subsequence/?envType=daily-question&envId=2024-04-25
        /// 
        /// https://leetcode.cn/problems/longest-ideal-subsequence/solutions/1728730/by-endlesscheng-t7zf/
        /// https://leetcode.cn/problems/longest-ideal-subsequence/solutions/2048672/c-by-hayasaka-ai-7h7e/
        /// https://leetcode.cn/problems/longest-ideal-subsequence/solutions/1745867/csuan-fa-by-qcwwg4sbek-d0e1/
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int LongestIdealString(string s, int k)
        {
            int n = s.Length;
            // 26個英文字母
            int[] dp = new int[26];
            int maxlen = 0;

            for(int i = 0; i < n; i++)
            {
                char curr = s[i];
                // 找出當下這個char 的英文index數值
                int idx = curr - 'a';
                int best = 0;

                // 每個s[i] 都要跟a ~ z 比對一輪, 找出該字母結尾長度
                // 計算 curr 字母結尾長度, 再往下更新至dp 裡面
                for(int prev = 0; prev < 26; prev++)
                {
                    // 每個字母index絕對值差異 要小於 k, 題目規定
                    if(Math.Abs(curr - 'a' - prev) <= k)
                    {
                        // 範圍內最大長度
                        best = Math.Max(best, dp[prev]);
                    }
                }

                // 更新資料, 長度至少為1
                dp[idx] = best + 1;
                maxlen = Math.Max(maxlen, dp[idx]);
            }

            return maxlen;
        }
    }
}
