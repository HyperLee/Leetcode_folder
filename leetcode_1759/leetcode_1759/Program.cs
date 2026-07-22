namespace leetcode_1759;

internal static class Program
{
    /// <summary>
    /// LeetCode 1759. Count Number of Homogenous Substrings.
    /// LeetCode 1759. 統計同質子字串的數目。
    /// English: Given a string, count its contiguous substrings whose characters are all the same,
    /// and return the count modulo 10^9 + 7.
    /// 中文：給定一個字串，統計其中所有字元完全相同的連續子字串數量，並將答案對
    /// 10^9 + 7 取模後回傳。
    /// English: https://leetcode.com/problems/count-number-of-homogenous-substrings/
    /// 中文：https://leetcode.cn/problems/count-number-of-homogenous-substrings/
    /// </summary>
    private static void Main()
    {
        CaseResult[] cases =
        [
            RunCase("Official example 1", "abbcccaa", 13),
            RunCase("Official example 2", "xy", 2),
            RunCase("Official example 3", "zzzzz", 15),
            RunCase("Minimum input", "a", 1),
            RunCase("Legacy sample", "bb", 3),
            RunCase("Separated runs of the same character", "aaabaaa", 13),
            RunCase("Trailing run finalization", "abb", 4),
            RunCase(
                "Maximum length all equal with modulo",
                new string('a', 100_000),
                49_965,
                "100000 x 'a'"),
            RunCase(
                "Maximum length alternating characters",
                string.Concat(Enumerable.Repeat("ab", 50_000)),
                100_000,
                "'ab' x 50000")
        ];

        for (int index = 0; index < cases.Length; index++)
        {
            CaseResult caseResult = cases[index];
            Console.WriteLine($"Case: {index + 1} - {caseResult.Name}");
            Console.WriteLine($"Input: {caseResult.Input}");
            Console.WriteLine($"Expected: {caseResult.Expected}");
            Console.WriteLine($"Actual: {caseResult.Actual}");
            Console.WriteLine($"Result: {(caseResult.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int passedCount = cases.Count(caseResult => caseResult.Passed);
        Console.WriteLine($"Summary: {passedCount}/{cases.Length} checks passed.");

        if (passedCount != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 對題目保證長度介於 1 到 100,000 且只含小寫英文字母的字串，以單次掃描辨識
    /// 每個連續同字元區段，將長度為 <c>L</c> 的區段貢獻 <c>L × (L + 1) / 2</c>
    /// 個同質子字串累加後取模並回傳。此方法不修改輸入，也不產生主控台輸出。
    /// </summary>
    public static int CountHomogenous(string s)
    {
        const int Modulo = 1_000_000_007;

        long total = 0;
        char previousCharacter = s[0];
        int runLength = 1;

        for (int index = 1; index < s.Length; index++)
        {
            if (s[index] == previousCharacter)
            {
                runLength++;
                continue;
            }

            // 已封閉區段的所有同質子字串都落在該段內，可用三角數一次結算。
            total += (long)runLength * (runLength + 1) / 2;
            previousCharacter = s[index];
            runLength = 1;
        }

        total += (long)runLength * (runLength + 1) / 2;
        return (int)(total % Modulo);
    }

    private static CaseResult RunCase(string name, string input, int expected, string? inputDescription = null)
    {
        int actual = CountHomogenous(input);
        return new CaseResult(name, inputDescription ?? $"\"{input}\"", expected, actual, expected == actual);
    }

    private sealed record CaseResult(string Name, string Input, int Expected, int Actual, bool Passed);
}
