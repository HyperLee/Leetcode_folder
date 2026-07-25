namespace leetcode_1930;

internal static class Program
{
    /// <summary>
    /// LeetCode 1930. Unique Length-3 Palindromic Subsequences.
    /// LeetCode 1930. 長度為 3 的不同回文子序列。
    /// English: Given a lowercase English string, return the number of distinct length-three
    /// palindromes that can be formed as subsequences. Multiple index choices producing the same
    /// three characters count only once.
    /// 中文：給定一個小寫英文字串，回傳其中可作為子序列形成的不同長度三回文數量；
    /// 即使多組索引產生相同的三個字元，也只計算一次。
    /// English: https://leetcode.com/problems/unique-length-3-palindromic-subsequences/
    /// 中文：https://leetcode.cn/problems/unique-length-3-palindromic-subsequences/
    /// </summary>
    private static void Main()
    {
        string maximumAlphabetString =
            string.Concat(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 3846)) + "abcd";

        TestCase[] testCases =
        [
            new("Official example 1", "\"aabca\"", "aabca", 3),
            new("Official example 2", "\"adc\"", "adc", 0),
            new("Official example 3", "\"bbcbaba\"", "bbcbaba", 4),
            new("Minimum all equal", "\"aaa\"", "aaa", 1),
            new("Minimum distinct center", "\"aba\"", "aba", 1),
            new("Duplicate construction paths", "\"aaaa\"", "aaaa", 1),
            new("Two distinct centers", "\"abca\"", "abca", 2),
            new("Multiple boundary characters", "\"abccba\"", "abccba", 3),
            new("Maximum length all equal", "100000 x 'a'", new string('a', 100000), 1),
            new(
                "Maximum length repeating alphabet",
                "\"abcdefghijklmnopqrstuvwxyz\" repeated to 100000 characters",
                maximumAlphabetString,
                676)
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck(
                "CountPalindromicSubsequence result",
                result.Expected,
                result.HashSetActual));
            Console.WriteLine(PrintCheck(
                "CountPalindromicSubsequence2 result",
                result.Expected,
                result.FixedArrayActual));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 20;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string PrintCheck(string checkName, int expected, int actual)
    {
        string status = expected == actual ? "PASS" : "FAIL";
        return $"{status} {checkName} | Expected: {expected} | Actual: {actual}";
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int hashSetActual = CountPalindromicSubsequence(testCase.Value);
        int fixedArrayActual = CountPalindromicSubsequence2(testCase.Value);

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.Expected,
            hashSetActual,
            fixedArrayActual);
    }

    /// <summary>
    /// 對題目保證長度介於 3 至 100000、且只含小寫英文字母的字串，逐一固定回文的
    /// 首尾字元，找出它在輸入中的第一次與最後一次位置，再以 HashSet 統計兩者之間的
    /// 不同中心字元。每個中心字元恰對應一個不同的長度三回文；回傳所有首尾字元的合計。
    /// 方法不建立中間子字串、不修改輸入，也不產生主控台輸出。時間複雜度為 O(26n)，
    /// 固定字母表下的輔助空間為 O(1)。
    /// </summary>
    /// <param name="s">題目限制內、只含小寫英文字母的字串。</param>
    /// <returns>字串中不同長度三回文子序列的數量。</returns>
    public static int CountPalindromicSubsequence(string s)
    {
        int count = 0;

        for (char boundaryCharacter = 'a'; boundaryCharacter <= 'z'; boundaryCharacter++)
        {
            int left = s.IndexOf(boundaryCharacter);
            int right = s.LastIndexOf(boundaryCharacter);
            if (left < 0 || right - left < 2)
            {
                continue;
            }

            // 最寬的首末邊界已涵蓋此首尾字元可搭配的所有中心字元。
            HashSet<char> middleCharacters = [];
            for (int index = left + 1; index < right; index++)
            {
                middleCharacters.Add(s[index]);
            }

            count += middleCharacters.Count;
        }

        return count;
    }

    /// <summary>
    /// 對題目保證長度介於 3 至 100000、且只含小寫英文字母的字串，逐一固定回文首尾，
    /// 並以重用的 26 格布林陣列標記第一次出現的中心字元。每個首尾範圍內首次標記的字元
    /// 增加一種不同回文；回傳全部首尾字元的合計。方法不修改輸入或主控台狀態。時間
    /// 複雜度為 O(26n)，固定 26 格陣列的輔助空間為 O(1)。
    /// </summary>
    /// <param name="s">題目限制內、只含小寫英文字母的字串。</param>
    /// <returns>字串中不同長度三回文子序列的數量。</returns>
    public static int CountPalindromicSubsequence2(string s)
    {
        int count = 0;
        bool[] middleCharactersSeen = new bool[26];

        for (char boundaryCharacter = 'a'; boundaryCharacter <= 'z'; boundaryCharacter++)
        {
            int left = s.IndexOf(boundaryCharacter);
            int right = s.LastIndexOf(boundaryCharacter);
            if (left < 0 || right - left < 2)
            {
                continue;
            }

            Array.Clear(middleCharactersSeen);
            for (int index = left + 1; index < right; index++)
            {
                int middleCharacterIndex = s[index] - 'a';
                if (middleCharactersSeen[middleCharacterIndex])
                {
                    continue;
                }

                // 同一首尾下，每種中心字元只在首次出現時形成一個新答案。
                middleCharactersSeen[middleCharacterIndex] = true;
                count++;
            }
        }

        return count;
    }

    private sealed record TestCase(string Name, string Input, string Value, int Expected);

    private sealed record CaseResult(
        string Name,
        string Input,
        int Expected,
        int HashSetActual,
        int FixedArrayActual)
    {
        public int PassedCheckCount =>
            (HashSetActual == Expected ? 1 : 0) +
            (FixedArrayActual == Expected ? 1 : 0);
    }
}