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
        private static void Main(string[] args)
        {
            Environment.ExitCode = RunSamples();
        }

        /// <summary>
        /// 執行固定且可重現的範例驗證，涵蓋官方案例、<c>k</c> 的上下界、
        /// 字母距離不循環、重複字母與最短合法字串。
        /// 每筆案例都會比較預期值與 <see cref="LongestIdealString(string, int)"/> 的實際結果。
        /// </summary>
        /// <returns>全部案例通過時回傳 <c>0</c>；任一案例失敗時回傳 <c>1</c>。</returns>
        private static int RunSamples()
        {
            (string Name, string S, int K, int Expected)[] testCases =
            [
                ("官方範例一", "acfgbd", 2, 4),
                ("官方範例二", "abcd", 3, 4),
                ("既有範例", "acb", 1, 2),
                ("k 的下界與重複字母", "aaaa", 0, 4),
                ("字母距離不循環", "az", 1, 1),
                ("k 的上界", "az", 25, 2),
                ("最短合法字串", "z", 0, 1)
            ];

            int passedCount = 0;

            for (int i = 0; i < testCases.Length; i++)
            {
                (string name, string s, int k, int expected) = testCases[i];
                int actual = LongestIdealString(s, k);
                bool passed = actual == expected;

                if (passed)
                {
                    passedCount++;
                }

                Console.WriteLine($"案例 {i + 1}：{name}");
                Console.WriteLine($"輸入：s = \"{s}\", k = {k}");
                Console.WriteLine($"預期：{expected}");
                Console.WriteLine($"實際：{actual}");
                Console.WriteLine($"結果：{(passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCount}/{testCases.Length} 筆測試通過");

            return passedCount == testCases.Length ? 0 : 1;
        }

        /// <summary>
        /// 計算字串中最長理想子序列的長度。
        /// 以長度為 26 的動態規劃陣列記錄「以各字母結尾的最長合法子序列」，
        /// 並依原字串順序，從與目前字母距離不超過 <paramref name="k"/> 的狀態轉移。
        /// </summary>
        /// <param name="s">長度介於 1 到 100,000，且只包含小寫英文字母的字串。</param>
        /// <param name="k">相鄰字母允許的最大字母序距離，範圍為 0 到 25。</param>
        /// <returns><paramref name="s"/> 中最長理想子序列的長度。</returns>
        /// <remarks>
        /// 參考資料：
        /// https://leetcode.com/problems/longest-ideal-subsequence/?envType=daily-question&amp;envId=2024-04-25
        /// https://leetcode.cn/problems/longest-ideal-subsequence/solutions/1728730/by-endlesscheng-t7zf/
        /// https://leetcode.cn/problems/longest-ideal-subsequence/solutions/2048672/c-by-hayasaka-ai-7h7e/
        /// https://leetcode.cn/problems/longest-ideal-subsequence/solutions/1745867/csuan-fa-by-qcwwg4sbek-d0e1/
        /// </remarks>
        public static int LongestIdealString(string s, int k)
        {
            // dp[c] 表示目前已處理字元中，以字母 c 結尾的最長理想子序列長度。
            int[] dp = new int[26];
            int maxLength = 0;

            for (int i = 0; i < s.Length; i++)
            {
                char current = s[i];
                int currentIndex = current - 'a';
                int bestPreviousLength = 0;

                // 只有字母序距離不超過 k 的結尾狀態，才能接上目前字母。
                for (int previousIndex = 0; previousIndex < 26; previousIndex++)
                {
                    if (Math.Abs(currentIndex - previousIndex) <= k)
                    {
                        bestPreviousLength = Math.Max(bestPreviousLength, dp[previousIndex]);
                    }
                }

                // 將目前字母接到最佳合法狀態之後；若沒有前一字母，長度自然為 1。
                dp[currentIndex] = bestPreviousLength + 1;
                maxLength = Math.Max(maxLength, dp[currentIndex]);
            }

            return maxLength;
        }
    }
}