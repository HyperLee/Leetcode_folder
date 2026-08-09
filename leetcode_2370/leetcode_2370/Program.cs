namespace leetcode_2370
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 2370. Longest Ideal Subsequence
        /// https://leetcode.com/problems/longest-ideal-subsequence/description/
        ///
        /// You are given lowercase string s and integer k. A string t is ideal when it is a subsequence of s and the absolute difference between the alphabet positions of every pair of adjacent letters in t is at most k. Return the length of the longest ideal string. A subsequence deletes zero or more characters without changing the order of those retained. Alphabet order is not cyclic: the difference between 'a' and 'z' is 25, not 1.
        ///
        /// Example 1:
        /// Input: s = "acfgbd", k = 2
        /// Output: 4
        /// Explanation: "acbd" is a longest ideal string and has length 4. "acfgbd" is not ideal because 'c' and 'f' differ by 3 alphabet positions.
        ///
        /// Example 2:
        /// Input: s = "abcd", k = 3
        /// Output: 4
        /// Explanation: "abcd" is a longest ideal string and has length 4.
        ///
        /// Constraints:
        /// - 1 &lt;= s.length &lt;= 10^5
        /// - 0 &lt;= k &lt;= 25
        /// - s consists of lowercase English letters.
        /// </para>
        /// <para>
        /// 2370. 最長理想子序列
        /// https://leetcode.cn/problems/longest-ideal-subsequence/description/
        ///
        /// 給定小寫字串 s 與整數 k。若字串 t 是 s 的子序列，且 t 中每一對相鄰字母在字母表位置上的絕對差至多為 k，則 t 是理想字串。回傳最長理想字串的長度。子序列可刪除零個或多個字元，但不能改變保留字元的順序。字母順序不循環：'a' 與 'z' 的位置差為 25，而不是 1。
        ///
        /// 範例 1：
        /// 輸入：s = "acfgbd", k = 2
        /// 輸出：4
        /// 說明："acbd" 是一個最長理想字串，長度為 4。"acfgbd" 並不理想，因為 'c' 與 'f' 的字母位置相差 3。
        ///
        /// 範例 2：
        /// 輸入：s = "abcd", k = 3
        /// 輸出：4
        /// 說明："abcd" 是一個最長理想字串，長度為 4。
        ///
        /// 限制條件：
        /// - 1 &lt;= s.length &lt;= 10^5
        /// - 0 &lt;= k &lt;= 25
        /// - s 僅由小寫英文字母組成。
        /// </para>
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