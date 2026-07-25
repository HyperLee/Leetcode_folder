namespace leetcode_2085;

internal static class Program
{
    /// <summary>
    /// LeetCode 2085. Count Common Words With One Occurrence.
    /// LeetCode 2085. 統計出現過一次的公共字串。
    /// English: Given two string arrays, return the number of strings that appear exactly once
    /// in each array.
    /// 中文：給定兩個字串陣列，回傳在兩個陣列中都恰好出現一次的字串數量。
    /// English: https://leetcode.com/problems/count-common-words-with-one-occurrence/
    /// 中文：https://leetcode.cn/problems/count-common-words-with-one-occurrence/
    /// </summary>
    private static void Main()
    {
        string[] maximumWords = [.. Enumerable.Repeat("a", 999), "b"];
        TestCase[] testCases =
        [
            new(
                "Official example 1",
                "words1=[leetcode, is, amazing, as, is], words2=[amazing, leetcode, is]",
                ["leetcode", "is", "amazing", "as", "is"],
                ["amazing", "leetcode", "is"],
                2),
            new(
                "Official example 2",
                "words1=[b, bb, bbb], words2=[a, aa, aaa]",
                ["b", "bb", "bbb"],
                ["a", "aa", "aaa"],
                0),
            new(
                "Official example 3",
                "words1=[a, ab], words2=[a, a, a, ab]",
                ["a", "ab"],
                ["a", "a", "a", "ab"],
                1),
            new("Minimum matching input", "words1=[a], words2=[a]", ["a"], ["a"], 1),
            new(
                "Duplicate only in words1",
                "words1=[a, a, b], words2=[a, b]",
                ["a", "a", "b"],
                ["a", "b"],
                1),
            new(
                "Duplicate only in words2",
                "words1=[a, b], words2=[a, a, b]",
                ["a", "b"],
                ["a", "a", "b"],
                1),
            new(
                "Different duplicates in both arrays",
                "words1=[a, b, b, c], words2=[a, b, c, c]",
                ["a", "b", "b", "c"],
                ["a", "b", "c", "c"],
                1),
            new(
                "Dictionary key alignment regression",
                "words1=[a, b], words2=[a, a, c]",
                ["a", "b"],
                ["a", "a", "c"],
                0),
            new(
                "Maximum array lengths",
                "words1=[a x 999, b], words2=[a x 999, b]",
                maximumWords,
                maximumWords,
                1)
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck("CountWords result", result.Expected, result.Actual));
            Console.WriteLine(PrintCheck("Input preserved", true, result.InputPreserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 18;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string PrintCheck<T>(string checkName, T expected, T actual)
    {
        string status = EqualityComparer<T>.Default.Equals(expected, actual) ? "PASS" : "FAIL";
        return $"{status} {checkName} | Expected: {expected} | Actual: {actual}";
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        string[] words1 = [.. testCase.Words1];
        string[] words2 = [.. testCase.Words2];
        string[] originalWords1 = [.. words1];
        string[] originalWords2 = [.. words2];
        int actual = CountWords(words1, words2);
        bool inputPreserved =
            words1.SequenceEqual(originalWords1) &&
            words2.SequenceEqual(originalWords2);

        return new CaseResult(testCase.Name, testCase.Input, testCase.Expected, actual, inputPreserved);
    }

    /// <summary>
    /// 分別統計題目限制內兩個有效字串陣列的單字頻率，再逐鍵確認同一個字串是否在兩邊都
    /// 恰好出現一次。方法只讀取 <paramref name="words1"/> 與 <paramref name="words2"/>，
    /// 不修改輸入或主控台狀態；回傳符合條件的公共字串數量。時間複雜度為 O(n + m)，
    /// 輔助空間為 O(u + v)，結果空間為 O(1)。
    /// </summary>
    /// <param name="words1">長度 1 至 1000，且元素為長度 1 至 30 小寫英文字串的第一個陣列。</param>
    /// <param name="words2">長度 1 至 1000，且元素為長度 1 至 30 小寫英文字串的第二個陣列。</param>
    /// <returns>在兩個陣列中都恰好出現一次的字串數量。</returns>
    public static int CountWords(string[] words1, string[] words2)
    {
        Dictionary<string, int> firstFrequencies = [];
        Dictionary<string, int> secondFrequencies = [];

        foreach (string word in words1)
        {
            firstFrequencies.TryGetValue(word, out int frequency);
            firstFrequencies[word] = frequency + 1;
        }

        foreach (string word in words2)
        {
            secondFrequencies.TryGetValue(word, out int frequency);
            secondFrequencies[word] = frequency + 1;
        }

        int commonWordCount = 0;
        foreach ((string word, int frequency) in firstFrequencies)
        {
            // 次數為一必須與同一個鍵一起查詢，不能用任意 ContainsValue(1) 代替鍵值對齊。
            if (frequency == 1 &&
                secondFrequencies.TryGetValue(word, out int secondFrequency) &&
                secondFrequency == 1)
            {
                commonWordCount++;
            }
        }

        return commonWordCount;
    }

    private sealed record TestCase(
        string Name,
        string Input,
        string[] Words1,
        string[] Words2,
        int Expected);

    private sealed record CaseResult(
        string Name,
        string Input,
        int Expected,
        int Actual,
        bool InputPreserved)
    {
        public int PassedCheckCount =>
            (Actual == Expected ? 1 : 0) +
            (InputPreserved ? 1 : 0);
    }
}