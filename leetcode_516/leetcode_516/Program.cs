namespace leetcode_516;

internal static class Program
{
    private sealed record AcceptanceCase(
        string Name,
        string InputDescription,
        string Value,
        int Expected);

    private sealed record CheckResult(
        string Name,
        string InputDescription,
        int Expected,
        int Actual,
        bool Passed);

    /// <summary>
    /// 執行 LeetCode 516. Longest Palindromic Subsequence／516. 最長回文子序列的確定性驗證器。
    /// https://leetcode.com/problems/longest-palindromic-subsequence/
    /// https://leetcode.cn/problems/longest-palindromic-subsequence/
    /// Given a string s, find the longest palindromic subsequence's length in s.
    /// 給定字串 s，找出其中最長回文子序列，並回傳該子序列的長度。
    /// 有效輸入為長度介於 1 到 1000 且只包含小寫英文字母的字串。
    /// </summary>
    private static void Main()
    {
        AcceptanceCase[] cases =
        [
            new("Official example 1", "s = \"bbbab\"", "bbbab", 4),
            new("Official example 2", "s = \"cbbd\"", "cbbd", 2),
            new("Single character", "s = \"a\"", "a", 1),
            new("Odd-length palindrome", "s = \"aba\"", "aba", 3),
            new("No repeated characters", "s = \"abcd\"", "abcd", 1),
            new("All characters equal", "s = \"aaaaa\"", "aaaaa", 5),
            new("Non-contiguous palindrome", "s = \"agbdba\"", "agbdba", 5),
            new("Regression case", "s = \"abcda\"", "abcda", 3),
            new("Complete palindrome", "s = \"racecar\"", "racecar", 7),
            new("Maximum length", "s = \"a\" repeated 1000 times", new string('a', 1000), 1000)
        ];

        CheckResult[] checks = new CheckResult[cases.Length];

        for (int i = 0; i < cases.Length; i++)
        {
            checks[i] = EvaluateCase(cases[i]);
        }

        Console.WriteLine("LeetCode 516 acceptance harness");
        Console.WriteLine();

        int passedCount = 0;

        for (int i = 0; i < checks.Length; i++)
        {
            CheckResult check = checks[i];

            Console.WriteLine($"Case {i + 1}: {check.Name}");
            Console.WriteLine($"Input: {check.InputDescription}");
            Console.WriteLine($"Expected: {check.Expected}");
            Console.WriteLine($"Actual: {check.Actual}");
            Console.WriteLine($"Result: {(check.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            if (check.Passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine($"Summary: {passedCount}/{checks.Length} checks passed.");

        if (passedCount != checks.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用區間動態規劃回傳字串中最長回文子序列的長度；s 必須符合題目規定的非空小寫英文字串輸入。
    /// </summary>
    /// <remarks>
    /// 保留原始解法中的二維陣列與反向遍歷教學主軸；相關思維參考如下：
    /// https://leetcode.cn/problems/longest-palindromic-subsequence/solution/zi-xu-lie-wen-ti-tong-yong-si-lu-zui-chang-hui-wen/
    /// https://leetcode.cn/problems/longest-palindromic-subsequence/solution/dong-tai-gui-hua-si-yao-su-by-a380922457-3/
    /// https://leetcode.cn/problems/longest-palindromic-subsequence/solution/zui-chang-hui-wen-zi-xu-lie-by-leetcode-hcjqp/
    /// </remarks>
    public static int LongestPalindromeSubseq(string s)
    {
        int n = s.Length;
        int[,] dp = new int[n, n];

        for (int i = n - 1; i >= 0; i--)
        {
            dp[i, i] = 1;

            // 由短區間推導長區間，因此 i 必須由右往左、j 必須由左往右填入。
            for (int j = i + 1; j < n; j++)
            {
                if (s[i] == s[j])
                {
                    // 首尾相同時納入兩端；相鄰字元的空內區間預設為 0，結果自然為 2。
                    dp[i, j] = dp[i + 1, j - 1] + 2;
                }
                else
                {
                    // 首尾不同時，最優解必須捨棄左端或右端其中之一。
                    dp[i, j] = Math.Max(dp[i + 1, j], dp[i, j - 1]);
                }
            }
        }

        return dp[0, n - 1];
    }

    /// <summary>
    /// 執行一個 acceptance 案例，回傳輸入、預期值、實際值與通過狀態供 Main 統一輸出。
    /// </summary>
    private static CheckResult EvaluateCase(AcceptanceCase testCase)
    {
        int actual = LongestPalindromeSubseq(testCase.Value);

        return new CheckResult(
            testCase.Name,
            testCase.InputDescription,
            testCase.Expected,
            actual,
            actual == testCase.Expected);
    }
}
