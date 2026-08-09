namespace leetcode_409
{
    internal class Program
    {
        /// <summary>
        /// 409. Longest Palindrome
        /// https://leetcode.com/problems/longest-palindrome/description/
        /// <para>
        /// Given a string s which consists of lowercase or uppercase letters, return the length of the longest palindrome that can be built with those letters.
        ///
        /// Letters are case sensitive; for example, "Aa" is not considered a palindrome.
        ///
        /// Example 1:
        /// Input: s = "abccccdd"
        /// Output: 7
        /// Explanation: One longest palindrome that can be built is "dccaccd", whose length is 7.
        ///
        /// Example 2:
        /// Input: s = "a"
        /// Output: 1
        /// Explanation: The longest palindrome that can be built is "a", whose length is 1.
        ///
        /// Constraints:
        /// - 1 &lt;= s.length &lt;= 2000
        /// - s consists only of lowercase and/or uppercase English letters.
        /// </para>
        /// <para>
        /// 409. 最長迴文串
        /// https://leetcode.cn/problems/longest-palindrome/description/
        ///
        /// 給定由小寫或大寫字母組成的字串 s，回傳能以這些字母構成之最長迴文串的長度。
        ///
        /// 字母區分大小寫；例如，"Aa" 不被視為迴文串。
        ///
        /// 範例 1：
        /// 輸入：s = "abccccdd"
        /// 輸出：7
        /// 解釋：可以構成的其中一個最長迴文串是 "dccaccd"，其長度為 7。
        ///
        /// 範例 2：
        /// 輸入：s = "a"
        /// 輸出：1
        /// 解釋：可以構成的最長迴文串是 "a"，其長度為 1。
        ///
        /// 限制條件：
        /// - 1 &lt;= s.length &lt;= 2000
        /// - s 只由小寫和／或大寫英文字母組成。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行固定範例，逐一比較兩種最長迴文長度解法的預期值與實際值，
        /// 並在所有案例完成後輸出通過數；若任一驗證失敗，程式會以非零結束碼結束。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] cases =
            [
                new("空字串（防禦性）", string.Empty, 0),
                new("單一字元", "a", 1),
                new("單一字元配對", "aa", 2),
                new("兩個不同字元", "ab", 1),
                new("官方範例", "abccccdd", 7),
                new("大小寫敏感", "Aa", 1),
                new("多組奇數次字元", "cccaaa", 5)
            ];

            int passedChecks = 0;
            foreach (SampleCase sample in cases)
            {
                passedChecks += RunSample(sample);
            }

            int totalChecks = cases.Length * 2;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 以一組非 null 字串與預期長度呼叫兩種解法，輸出各自的實際結果與驗證狀態，
        /// 並回傳本案例通過的解法數量，範圍為 0 到 2。
        /// </summary>
        /// <param name="sample">包含案例名稱、輸入字串與預期最長迴文長度的測試資料。</param>
        /// <returns>此案例通過驗證的解法數量。</returns>
        private static int RunSample(SampleCase sample)
        {
            int result1 = LongestPalindrome(sample.Input);
            int result2 = LongestPalindrome2(sample.Input);
            bool passed1 = result1 == sample.Expected;
            bool passed2 = result2 == sample.Expected;

            Console.WriteLine($"案例：{sample.Description}");
            Console.WriteLine($"輸入：s = \"{sample.Input}\"");
            Console.WriteLine($"預期（Expected）：{sample.Expected}");
            Console.WriteLine(
                $"實際（LongestPalindrome）：{result1} => {(passed1 ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"實際（LongestPalindrome2）：{result2} => {(passed2 ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (passed1 ? 1 : 0) + (passed2 ? 1 : 0);
        }

        /// <summary>
        /// 表示一組可重複執行的範例，包含顯示名稱、非 null 輸入字串與預期回傳值。
        /// </summary>
        /// <param name="Description">案例用途或邊界條件的說明。</param>
        /// <param name="Input">要傳入兩種解法的字串。</param>
        /// <param name="Expected">可由輸入字元構成的最長迴文長度。</param>
        private sealed record SampleCase(string Description, string Input, int Expected);

        /// <summary>
        /// 計算由指定字串字元可構成的最長迴文長度。
        /// 此解法以 ASCII 計數陣列統計每個字元，先取出所有偶數配對，
        /// 若存在奇數次字元，再選其中一個放在迴文中心。
        /// </summary>
        /// <param name="s">僅包含大小寫英文字母的非 null 字串；空字串亦會回傳 0。</param>
        /// <returns>使用輸入字元可構成的最長迴文長度。</returns>
        public static int LongestPalindrome(string s)
        {
            int[] count = new int[128];

            foreach (char c in s)
            {
                count[c]++;
            }

            int length = 0;
            bool hasOddCount = false;
            foreach (int frequency in count)
            {
                // 每一對相同字元可分別放在迴文的左右兩側。
                length += (frequency / 2) * 2;
                if (frequency % 2 == 1)
                {
                    hasOddCount = true;
                }
            }

            // 不論有幾種奇數次字元，迴文中心最多只能再放一個字元。
            return hasOddCount ? length + 1 : length;
        }

        /// <summary>
        /// 計算由指定字串字元可構成的最長迴文長度。
        /// 此解法以 HashSet 保存尚未配對的字元；再次遇到相同字元時完成一組配對，
        /// 最後若集合仍有剩餘字元，取其中一個作為迴文中心。
        /// </summary>
        /// <param name="s">僅包含大小寫英文字母的非 null 字串；空字串亦會回傳 0。</param>
        /// <returns>使用輸入字元可構成的最長迴文長度。</returns>
        public static int LongestPalindrome2(string s)
        {
            HashSet<char> unmatchedCharacters = [];
            int length = 0;

            foreach (char c in s)
            {
                if (unmatchedCharacters.Remove(c))
                {
                    // 第二次遇到相同字元時完成配對，分別放到迴文左右兩側。
                    length += 2;
                }
                else
                {
                    unmatchedCharacters.Add(c);
                }
            }

            // 尚未配對的字元中，最多只能選一個放在迴文中心。
            return unmatchedCharacters.Count > 0 ? length + 1 : length;
        }
    }
}