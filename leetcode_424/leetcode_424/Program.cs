namespace leetcode_424;

class Program
{
    /// <summary>
    /// 424. Longest Repeating Character Replacement
    /// https://leetcode.com/problems/longest-repeating-character-replacement/description/
    /// <para>
    /// You are given a string s and an integer k. You can choose any character of the string and change it to any other uppercase English character. You can perform this operation at most k times.
    ///
    /// Return the length of the longest substring containing the same letter you can get after performing the operations.
    ///
    /// Example 1:
    /// Input: s = "ABAB", k = 2
    /// Output: 4
    /// Explanation: Replace the two 'A's with two 'B's, or vice versa.
    ///
    /// Example 2:
    /// Input: s = "AABABBA", k = 1
    /// Output: 4
    /// Explanation: Replace the one 'A' in the middle with 'B' to form "AABBBBA". The substring "BBBB" has the longest repeating letters, with length 4. Other ways may also achieve this answer.
    ///
    /// Constraints:
    /// - 1 &lt;= s.length &lt;= 10^5
    /// - s consists only of uppercase English letters.
    /// - 0 &lt;= k &lt;= s.length
    /// </para>
    /// <para>
    /// 424. 替換後的最長重複字元
    /// https://leetcode.cn/problems/longest-repeating-character-replacement/description/
    ///
    /// 給定字串 s 與整數 k。可以選擇字串中的任意字元，將它改成任何其他大寫英文字母。此操作最多可執行 k 次。
    ///
    /// 回傳執行上述操作後，能得到之全部由相同字母組成的最長子字串長度。
    ///
    /// 範例 1：
    /// 輸入：s = "ABAB", k = 2
    /// 輸出：4
    /// 解釋：將兩個 'A' 替換成兩個 'B'，或反向替換。
    ///
    /// 範例 2：
    /// 輸入：s = "AABABBA", k = 1
    /// 輸出：4
    /// 解釋：將中間的一個 'A' 替換成 'B'，形成 "AABBBBA"。子字串 "BBBB" 的重複字母最長，長度為 4。也可能有其他方式得到此答案。
    ///
    /// 限制條件：
    /// - 1 &lt;= s.length &lt;= 10^5
    /// - s 只由大寫英文字母組成。
    /// - 0 &lt;= k &lt;= s.length
    /// </para>
    /// </summary>
    /// <param name="args"></param> 
    static void Main(string[] args)
    {
        SampleCase[] sampleCases =
        [
            new("Official example 2", "AABABBA", 1, 4),
            new("Official example 1", "ABAB", 2, 4),
            new("All characters identical", "AAAA", 2, 4),
            new("Larger replacement budget", "AABABBA", 2, 5),
            new("Minimum input", "A", 0, 1),
            new("No replacements allowed", "ABCDE", 0, 1),
            new("Replace the whole window", "ABCDE", 4, 5)
        ];

        Program solution = new Program();
        int passed = 0;

        foreach (SampleCase sampleCase in sampleCases)
        {
            int actual = solution.CharacterReplacement(sampleCase.S, sampleCase.K);
            bool isPassed = actual == sampleCase.Expected;
            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine($"Case: {sampleCase.Name}");
            Console.WriteLine($"Input: s = \"{sampleCase.S}\", k = {sampleCase.K}");
            Console.WriteLine($"Expected: {sampleCase.Expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passed}/{sampleCases.Length} checks passed.");
        if (passed != sampleCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 計算最多替換 <paramref name="k"/> 個字元後，可形成的最長單一重複字元子字串長度。
    /// 解法以滑動窗口記錄 26 個大寫英文字母的出現次數，並以
    /// 「窗口長度減去窗口內最高字元頻率」判斷所需替換次數。當替換成本超過
    /// <paramref name="k"/> 時移動左邊界，使窗口長度維持為目前可達的最佳候選。
    /// 適用於題目限制內的非 null 大寫英文字串；時間複雜度為 O(n)，
    /// 輔助空間複雜度為 O(1)。
    /// </summary>
    /// <param name="s">長度介於 1 至 100000，且僅包含大寫英文字母的字串。</param>
    /// <param name="k">最多允許替換的次數，介於 0 至 <paramref name="s"/> 的長度。</param>
    /// <returns>最多替換 <paramref name="k"/> 個字元後，內容可全部相同的最長子字串長度。</returns>
    public int CharacterReplacement(string s, int k)
    {
        int len = s.Length;
        if (len < 2)
        {
            return len;
        }

        int left = 0;
        int right = 0;
        int res = 0;
        int maxCount = 0;
        int[] count = new int[26];

        while (right < len)
        {
            count[s[right] - 'A']++;

            // maxCount 保留掃描至今的最高頻率，不隨左邊界移動而下降；
            // 本題只求最長長度，較寬鬆的歷史上限不會錯過更長的候選窗口。
            if (count[s[right] - 'A'] > maxCount)
            {
                maxCount = count[s[right] - 'A'];
            }

            right++;

            // 窗口長度減去最高字元頻率，就是把窗口統一成單一字元所需的替換次數。
            if (right - left > maxCount + k)
            {
                count[s[left] - 'A']--;
                left++;

                // 即使把剩餘字元全接到目前窗口，也無法超越 res 時可提前結束。
                if (res >= right - left + (len - right))
                {
                    return res;
                }
            }

            res = Math.Max(res, right - left);
        }

        return res;
    }

    private sealed record SampleCase(string Name, string S, int K, int Expected);
}
