using System.Text;

namespace leetcode_1768
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1768. Merge Strings Alternately
        /// https://leetcode.com/problems/merge-strings-alternately/description/
        ///
        /// You are given two strings word1 and word2. Merge them by adding letters in alternating order, starting with word1. If one string is longer, append its remaining letters to the end.
        ///
        /// Return the merged string.
        ///
        /// Example 1:
        /// Input: word1 = "abc", word2 = "pqr"
        /// Output: "apbqcr"
        /// Explanation:
        /// word1: a b c
        /// word2: p q r
        /// merged: a p b q c r
        ///
        /// Example 2:
        /// Input: word1 = "ab", word2 = "pqrs"
        /// Output: "apbqrs"
        /// Explanation: word2 is longer, so "rs" is appended at the end.
        /// word1: a b
        /// word2: p q r s
        /// merged: a p b q r s
        ///
        /// Example 3:
        /// Input: word1 = "abcd", word2 = "pq"
        /// Output: "apbqcd"
        /// Explanation: word1 is longer, so "cd" is appended at the end.
        /// word1: a b c d
        /// word2: p q
        /// merged: a p b q c d
        ///
        /// Constraints:
        /// - 1 &lt;= word1.length, word2.length &lt;= 100
        /// - word1 and word2 consist of lowercase English letters.
        /// </para>
        /// <para>
        /// 1768. 交替合併字串
        /// https://leetcode.cn/problems/merge-strings-alternately/description/
        ///
        /// 給定兩個字串 word1 和 word2。從 word1 開始，依序交替加入兩個字串的字元來合併它們。若其中一個字串較長，將剩餘字元附加到合併字串末尾。
        ///
        /// 回傳合併後的字串。
        ///
        /// 範例 1：
        /// 輸入：word1 = "abc", word2 = "pqr"
        /// 輸出："apbqcr"
        /// 說明：
        /// word1：a b c
        /// word2：p q r
        /// 合併：a p b q c r
        ///
        /// 範例 2：
        /// 輸入：word1 = "ab", word2 = "pqrs"
        /// 輸出："apbqrs"
        /// 說明：word2 較長，因此將 "rs" 附加到末尾。
        /// word1：a b
        /// word2：p q r s
        /// 合併：a p b q r s
        ///
        /// 範例 3：
        /// 輸入：word1 = "abcd", word2 = "pq"
        /// 輸出："apbqcd"
        /// 說明：word1 較長，因此將 "cd" 附加到末尾。
        /// word1：a b c d
        /// word2：p q
        /// 合併：a p b q c d
        ///
        /// 限制條件：
        /// - 1 &lt;= word1.length, word2.length &lt;= 100
        /// - word1 和 word2 僅由小寫英文字母組成。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Name, string Word1, string Word2, string Expected)[] testCases =
            [
                ("Equal lengths", "abc", "pqr", "apbqcr"),
                ("Second word is longer", "ab", "pqrs", "apbqrs"),
                ("First word is longer", "abcd", "pq", "apbqcd"),
                ("Single-character words", "a", "z", "az"),
                ("Empty first word", string.Empty, "xyz", "xyz"),
                ("Empty second word", "xyz", string.Empty, "xyz")
            ];

            (string Name, Func<string, string, string> Execute)[] solutions =
            [
                (nameof(MergeAlternately), MergeAlternately),
                (nameof(MergeAlternately2), MergeAlternately2)
            ];

            int passedChecks = 0;
            int totalChecks = testCases.Length * solutions.Length;

            foreach ((string caseName, string word1, string word2, string expected) in testCases)
            {
                foreach ((string solutionName, Func<string, string, string> execute) in solutions)
                {
                    if (RunCase(caseName, solutionName, execute, word1, word2, expected))
                    {
                        passedChecks++;
                    }
                }
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// 執行單一解法與測試案例，將實際結果和預期結果進行字串比對，並輸出可重複驗證的
        /// Expected、Actual 與 PASS/FAIL 資訊。輸入字串與預期結果皆須為非 <see langword="null"/>；
        /// 回傳值表示該解法在本案例是否得到正確結果。
        /// </summary>
        /// <param name="caseName">用於辨識輸入情境的測試案例名稱。</param>
        /// <param name="solutionName">目前受測解法的名稱。</param>
        /// <param name="solution">接收兩個非 <see langword="null"/> 字串並回傳合併結果的解法。</param>
        /// <param name="word1">第一個待交替合併的字串。</param>
        /// <param name="word2">第二個待交替合併的字串。</param>
        /// <param name="expected">此案例預期產生的合併結果。</param>
        /// <returns>實際結果與預期結果相同時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunCase(
            string caseName,
            string solutionName,
            Func<string, string, string> solution,
            string word1,
            string word2,
            string expected)
        {
            string actual = solution(word1, word2);
            bool passed = actual == expected;

            Console.WriteLine($"Case: {caseName} | Solution: {solutionName}");
            Console.WriteLine($"Input: word1=\"{word1}\", word2=\"{word2}\"");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed;
        }

        /// <summary>
        /// 將兩個非 <see langword="null"/> 字串由第一個字串開始交替合併。此解法先處理兩者的
        /// 共同長度，再將較長字串尚未使用的尾端一次附加至結果；輸入字串不會被修改。
        /// </summary>
        /// <param name="word1">第一個待合併的非 <see langword="null"/> 字串。</param>
        /// <param name="word2">第二個待合併的非 <see langword="null"/> 字串。</param>
        /// <returns>從 <paramref name="word1"/> 開始交替排列，並包含較長字串剩餘字元的新字串。</returns>
        public static string MergeAlternately(string word1, string word2)
        {
            int n1 = word1.Length;
            int n2 = word2.Length;
            int commonLength = Math.Min(n1, n2);
            StringBuilder result = new StringBuilder(n1 + n2);

            // 在共同長度內，每一輪固定先放 word1，再放 word2，維持題目要求的交替順序。
            for (int i = 0; i < commonLength; i++)
            {
                result.Append(word1[i]);
                result.Append(word2[i]);
            }

            // 共同區段結束後，只會有其中一個字串仍有字元，直接附加其完整尾端。
            if (n1 > commonLength)
            {
                result.Append(word1, commonLength, n1 - commonLength);
            }

            if (n2 > commonLength)
            {
                result.Append(word2, commonLength, n2 - commonLength);
            }

            return result.ToString();
        }

        /// <summary>
        /// 將兩個非 <see langword="null"/> 字串由第一個字串開始交替合併。此解法以單一迴圈
        /// 同步推進兩個索引，每輪只追加仍在各自字串範圍內的字元，因此自然涵蓋長度不同的輸入；
        /// 輸入字串不會被修改。
        /// </summary>
        /// <param name="word1">第一個待合併的非 <see langword="null"/> 字串。</param>
        /// <param name="word2">第二個待合併的非 <see langword="null"/> 字串。</param>
        /// <returns>從 <paramref name="word1"/> 開始交替排列，並包含較長字串剩餘字元的新字串。</returns>
        public static string MergeAlternately2(string word1, string word2)
        {
            StringBuilder result = new StringBuilder(word1.Length + word2.Length);
            int maxLength = Math.Max(word1.Length, word2.Length);

            // 以相同索引檢查兩個字串；較短字串用盡後，另一個字串會在後續輪次繼續追加。
            for (int i = 0; i < maxLength; i++)
            {
                if (i < word1.Length)
                {
                    result.Append(word1[i]);
                }

                if (i < word2.Length)
                {
                    result.Append(word2[i]);
                }
            }

            return result.ToString();
        }
    }
}