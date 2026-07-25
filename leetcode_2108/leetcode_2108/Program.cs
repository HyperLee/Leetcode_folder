namespace leetcode_2108;

internal static class Program
{
    /// <summary>
    /// LeetCode 2108. Find First Palindromic String in the Array.
    /// LeetCode 2108. 找出陣列中的第一個回文字串。
    /// English: Given an array of lowercase English strings, return the first string that reads
    /// identically from left to right and right to left. Return an empty string when no word is
    /// palindromic.
    /// 中文：給定一個只含小寫英文字串的陣列，回傳依原順序遇到的第一個正讀與反讀相同的
    /// 字串；若沒有任何回文字串，則回傳空字串。
    /// English: https://leetcode.com/problems/find-first-palindromic-string-in-the-array/
    /// 中文：https://leetcode.cn/problems/find-first-palindromic-string-in-the-array/
    /// </summary>
    private static void Main()
    {
        string maximumPalindrome = new('a', 100);
        string[] maximumWords = [.. Enumerable.Repeat("ab", 99), "z"];
        TestCase[] testCases =
        [
            new(
                "Official example 1",
                "words=[abc, car, ada, racecar, cool]",
                ["abc", "car", "ada", "racecar", "cool"],
                "ada"),
            new(
                "Official example 2",
                "words=[notapalindrome, racecar]",
                ["notapalindrome", "racecar"],
                "racecar"),
            new(
                "Official example 3",
                "words=[def, ghi]",
                ["def", "ghi"],
                ""),
            new("Minimum input", "words=[a]", ["a"], "a"),
            new(
                "First even-length palindrome",
                "words=[abba, level]",
                ["abba", "level"],
                "abba"),
            new(
                "Reject inner mismatch and continue",
                "words=[abca, cdc]",
                ["abca", "cdc"],
                "cdc"),
            new(
                "Maximum word length",
                "words=[a x 100]",
                [maximumPalindrome],
                maximumPalindrome),
            new(
                "Maximum array length with final match",
                "words=[ab x 99, z]",
                maximumWords,
                "z")
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck(
                "FirstPalindrome result",
                result.Expected,
                result.Actual));
            Console.WriteLine(PrintCheck(
                "FirstPalindrome input preserved",
                true,
                result.InputPreserved));
            Console.WriteLine();
        }

        PalindromeTestCase[] palindromeTestCases =
        [
            new("Single character", "a", true),
            new("Even-length palindrome", "aa", true),
            new("Odd-length palindrome", "aba", true),
            new("Outer mismatch", "ab", false),
            new("Inner mismatch", "abca", false),
            new("Maximum word length", maximumPalindrome, true)
        ];

        int palindromePassedCount = 0;
        for (int index = 0; index < palindromeTestCases.Length; index++)
        {
            PalindromeTestCase testCase = palindromeTestCases[index];
            bool actual = IsPalindrome(testCase.Word);
            bool passed = actual == testCase.Expected;
            palindromePassedCount += passed ? 1 : 0;

            Console.WriteLine($"Palindrome check: {index + 1} - {testCase.Name}");
            Console.WriteLine($"Input: word={FormatWord(testCase.Word)}");
            Console.WriteLine(PrintCheck("IsPalindrome result", testCase.Expected, actual));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount) + palindromePassedCount;
        const int totalCheckCount = 22;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        string[] words = [.. testCase.Words];
        string[] originalWords = [.. words];
        string actual = FirstPalindrome(words);

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.Expected,
            actual,
            words.SequenceEqual(originalWords));
    }

    /// <summary>
    /// 依序掃描題目限制內的有效字串陣列，對每個候選字串執行雙指標回文檢查，並回傳第一個
    /// 回文字串；若全部候選都不是回文則回傳空字串。方法只讀取
    /// <paramref name="words"/>，不修改輸入或主控台狀態。令 n 為陣列長度、k 為最長字串
    /// 長度，時間複雜度為 O(n × k)，輔助空間與結果空間皆為 O(1)。
    /// </summary>
    /// <param name="words">長度 1 至 100，且元素為長度 1 至 100 小寫英文字串的陣列。</param>
    /// <returns>依輸入順序找到的第一個回文字串；找不到時回傳空字串。</returns>
    public static string FirstPalindrome(string[] words)
    {
        foreach (string word in words)
        {
            if (IsPalindrome(word))
            {
                // 掃描順序與輸入一致，因此第一次成功即可確立題目要求的「第一個」答案。
                return word;
            }
        }

        return "";
    }

    /// <summary>
    /// 以左右雙指標檢查題目限制內的有效字串：每輪比較對稱位置，若不相同便立即判定不是
    /// 回文；所有對稱字元都相同時回傳 true。方法只讀取 <paramref name="word"/>，不修改
    /// 字串或主控台狀態。令 k 為字串長度，時間複雜度為 O(k)，輔助空間與結果空間皆為
    /// O(1)。
    /// </summary>
    /// <param name="word">長度 1 至 100，且只含小寫英文字母的字串。</param>
    /// <returns>字串正讀與反讀相同時回傳 true，否則回傳 false。</returns>
    public static bool IsPalindrome(string word)
    {
        int left = 0;
        int right = word.Length - 1;

        while (left < right)
        {
            // 任何一組對稱字元不相同都足以否定回文，不必檢查剩餘內層字元。
            if (word[left] != word[right])
            {
                return false;
            }

            left++;
            right--;
        }

        return true;
    }

    private static string FormatWord(string word)
    {
        return word.Length <= 20 ? word : $"{word[0]} x {word.Length}";
    }

    private static string PrintCheck<T>(string checkName, T expected, T actual)
    {
        string status = EqualityComparer<T>.Default.Equals(expected, actual) ? "PASS" : "FAIL";
        return
            $"{status} {checkName} | Expected: {FormatValue(expected)} | " +
            $"Actual: {FormatValue(actual)}";
    }

    private static string FormatValue<T>(T value)
    {
        return value is string { Length: 0 } ? "\"\"" : value?.ToString() ?? "null";
    }

    private sealed record TestCase(
        string Name,
        string Input,
        string[] Words,
        string Expected);

    private sealed record CaseResult(
        string Name,
        string Input,
        string Expected,
        string Actual,
        bool InputPreserved)
    {
        public int PassedCheckCount =>
            (Actual == Expected ? 1 : 0) +
            (InputPreserved ? 1 : 0);
    }

    private sealed record PalindromeTestCase(
        string Name,
        string Word,
        bool Expected);
}