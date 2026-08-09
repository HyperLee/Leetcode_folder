using System.Text;

namespace leetcode_3163
{
    internal class Program
    {
        /// <summary>
        /// 3163. String Compression III
        /// https://leetcode.com/problems/string-compression-iii/description/
        /// <para>
        /// Given a string word, compress it using this algorithm:
        /// - Begin with an empty string comp. While word is not empty:
        ///   - Remove a maximum-length prefix of word consisting of one character c repeated at most 9 times.
        ///   - Append the prefix length followed by c to comp.
        ///
        /// Return comp.
        ///
        /// Example 1:
        /// Input: word = "abcde"
        /// Output: "1a1b1c1d1e"
        /// Explanation: Initially comp = "". Perform the operation 5 times, choosing "a", "b", "c", "d", and "e" as the prefixes. For each prefix, append "1" followed by the character to comp.
        ///
        /// Example 2:
        /// Input: word = "aaaaaaaaaaaaaabb"
        /// Output: "9a5a2b"
        /// Explanation: Initially comp = "". Perform the operation 3 times, choosing "aaaaaaaaa", "aaaaa", and "bb". Append "9a", then "5a", then "2b" to comp.
        ///
        /// Constraints:
        /// - 1 &lt;= word.length &lt;= 2 * 10^5
        /// - word consists only of lowercase English letters.
        /// </para>
        /// <para>
        /// 3163. 字串壓縮 III
        /// https://leetcode.cn/problems/string-compression-iii/description/
        ///
        /// 給定字串 word，使用下列演算法壓縮它：
        /// - 從空字串 comp 開始。當 word 不為空時：
        ///   - 從 word 移除一個最長前綴，此前綴由單一字元 c 重複至多 9 次構成。
        ///   - 將此前綴的長度以及 c 依序附加到 comp。
        ///
        /// 回傳 comp。
        ///
        /// 範例 1：
        /// 輸入：word = "abcde"
        /// 輸出："1a1b1c1d1e"
        /// 解釋：起初 comp = ""。執行操作 5 次，依序選擇 "a"、"b"、"c"、"d"、"e" 作為前綴。每次都把 "1" 和該字元附加到 comp。
        ///
        /// 範例 2：
        /// 輸入：word = "aaaaaaaaaaaaaabb"
        /// 輸出："9a5a2b"
        /// 解釋：起初 comp = ""。執行操作 3 次，依序選擇 "aaaaaaaaa"、"aaaaa"、"bb"，並將 "9a"、"5a"、"2b" 附加到 comp。
        ///
        /// 限制條件：
        /// - 1 &lt;= word.length &lt;= 2 * 10^5
        /// - word 只由小寫英文字母組成。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 執行固定案例，對照兩種字串壓縮解法的預期與實際結果。
        /// </remarks>
        /// <param name="args">命令列參數；此範例不使用。</param>
        static void Main(string[] args)
        {
            (string Name, string Word, string Expected)[] testCases =
            [
                ("官方範例：每個字元皆不同", "abcde", "1a1b1c1d1e"),
                ("官方範例：連續字元超過 9 個", "aaaaaaaaaaaaaabb", "9a5a2b"),
                ("最小合法輸入", "x", "1x"),
                ("連續字元剛好 9 個", "aaaaaaaaa", "9a"),
                ("連續字元超過上限一個", "aaaaaaaaaa", "9a1a"),
                ("相同字元分段重現", "aaabbaa", "3a2b2a")
            ];

            int solution1PassedCount = 0;
            int solution2PassedCount = 0;
            int total = testCases.Length * 2;

            Console.WriteLine("LeetCode 3163 - String Compression III");
            Console.WriteLine();

            for (int i = 0; i < testCases.Length; i++)
            {
                (string name, string word, string expected) = testCases[i];
                string actual1 = CompressedString(word);
                string actual2 = CompressedString2(word);
                bool solution1Passed = actual1 == expected;
                bool solution2Passed = actual2 == expected;

                solution1PassedCount += solution1Passed ? 1 : 0;
                solution2PassedCount += solution2Passed ? 1 : 0;

                Console.WriteLine($"案例 {i + 1}：{name}");
                Console.WriteLine($"輸入：\"{word}\"");
                Console.WriteLine($"Expected：\"{expected}\"");
                Console.WriteLine($"解法一 Actual：\"{actual1}\" => {(solution1Passed ? "PASS" : "FAIL")}");
                Console.WriteLine($"解法二 Actual：\"{actual2}\" => {(solution2Passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            int passed = solution1PassedCount + solution2PassedCount;

            Console.WriteLine($"解法一：{solution1PassedCount}/{testCases.Length} 案例通過");
            Console.WriteLine($"解法二：{solution2PassedCount}/{testCases.Length} 案例通過");
            Console.WriteLine($"總結：{passed}/{total} 項驗證通過");

            if (passed != total)
            {
                Environment.ExitCode = 1;
            }
        }


        /// <summary>
        /// 將字串壓縮為「連續數量＋字元」的格式。
        /// 逐字累計目前區段長度，當長度到達 9、抵達字串結尾或下一個字元不同時，
        /// 立即輸出目前區段。輸入須符合題目限制：長度介於 1 到 200000，且只包含小寫英文字母。
        /// </summary>
        /// <param name="word">要壓縮的非空小寫英文字串。</param>
        /// <returns>每個區段以一位數長度接續原字元組成的壓縮字串。</returns>
        public static string CompressedString(string word)
        {
            StringBuilder compressed = new StringBuilder();
            int count = 0;
            int length = word.Length;

            for (int i = 0; i < length; i++)
            {
                char c = word[i];
                count++;

                // 每段最多只能包含 9 個字元；字串結尾或下一字元不同時也必須結束目前區段。
                if (count == 9 || i == length - 1 || c != word[i + 1])
                {
                    compressed.Append(count);
                    compressed.Append(c);

                    // 區段輸出後歸零，讓下一個字元重新開始計數。
                    count = 0;
                }
            }

            return compressed.ToString();
        }

        /// <summary>
        /// 將字串壓縮為「連續數量＋字元」的格式。
        /// 使用雙指標找出每一段完整的連續相同字元，再將段長拆成每批最多 9 個後依序輸出。
        /// 輸入須符合題目限制：長度介於 1 到 200000，且只包含小寫英文字母。
        /// </summary>
        /// <param name="word">要壓縮的非空小寫英文字串。</param>
        /// <returns>每個區段以一位數長度接續原字元組成的壓縮字串。</returns>
        public static string CompressedString2(string word)
        {
            StringBuilder compressed = new StringBuilder();
            int left = 0;

            while (left < word.Length)
            {
                int right = left + 1;

                // right 前進到不同字元或字串結尾，取得完整連續區段 [left, right)。
                while (right < word.Length && word[right] == word[left])
                {
                    right++;
                }

                int remaining = right - left;

                // 一個完整區段可能超過 9 個字元，因此需拆成多個合法批次。
                while (remaining > 0)
                {
                    int chunkLength = Math.Min(9, remaining);
                    compressed.Append(chunkLength);
                    compressed.Append(word[left]);
                    remaining -= chunkLength;
                }

                left = right;
            }

            return compressed.ToString();
        }
    }
}
