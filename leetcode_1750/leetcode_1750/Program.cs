namespace leetcode_1750;

internal static class Program
{
    /// <summary>
    /// LeetCode 1750. Minimum Length of String After Deleting Similar Ends.
    /// LeetCode 1750. 刪除字串兩端相同字元後的最短長度。
    /// English: Repeatedly choose non-overlapping, non-empty prefixes and suffixes
    /// made of the same character, delete them, and return the minimum remaining length.
    /// 中文：反覆選擇互不重疊、非空且各自由相同字元組成的前綴與後綴；兩者字元相同時
    /// 將其刪除，並回傳最終可得到的最短剩餘長度。
    /// English: https://leetcode.com/problems/minimum-length-of-string-after-deleting-similar-ends/
    /// 中文：https://leetcode.cn/problems/minimum-length-of-string-after-deleting-similar-ends/
    /// </summary>
    private static void Main()
    {
        CaseResult[] cases =
        [
            RunCase("Official example 1", "ca", 2),
            RunCase("Official example 2", "cabaabac", 0),
            RunCase("Official example 3", "aabccabba", 3),
            RunCase("Minimum input", "a", 1),
            RunCase("Two matching characters", "aa", 0),
            RunCase("Longer left boundary run", "aaabca", 2),
            RunCase("Chained complete deletion", "abbbbbba", 0),
            RunCase("Single character remains", "aaaabaaaa", 1),
            RunCase(
                "Maximum length all equal",
                new string('a', 100_000),
                0,
                "100000 x 'a'"),
            RunCase(
                "Maximum length different ends",
                $"a{new string('b', 99_998)}c",
                100_000,
                "'a' + 99998 x 'b' + 'c'")
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

    private static CaseResult RunCase(string name, string input, int expected, string? inputDescription = null)
    {
        int actual = MinimumLength(input);
        return new CaseResult(name, inputDescription ?? $"\"{input}\"", expected, actual, expected == actual);
    }

    /// <summary>
    /// 對題目保證由 <c>a</c>、<c>b</c>、<c>c</c> 組成且長度介於 1 到 100,000 的字串，
    /// 以左右指標表示尚未刪除的區間；當兩端字元相同時跳過兩側完整的同字元區段，
    /// 最後回傳無法再刪除的字串長度。此方法不修改輸入，也不產生主控台輸出。
    /// </summary>
    public static int MinimumLength(string s)
    {
        int left = 0;
        int right = s.Length - 1;

        while (left < right && s[left] == s[right])
        {
            char boundaryCharacter = s[left];

            // 每輪結束後，[left, right] 恰好是刪除兩側完整同字元區段後的剩餘範圍。
            while (left <= right && s[left] == boundaryCharacter)
            {
                left++;
            }

            while (left <= right && s[right] == boundaryCharacter)
            {
                right--;
            }
        }

        return right - left + 1;
    }

    private sealed record CaseResult(string Name, string Input, int Expected, int Actual, bool Passed);
}
