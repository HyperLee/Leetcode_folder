namespace leetcode_1816;

internal static class Program
{
    /// <summary>
    /// LeetCode 1816. Truncate Sentence.
    /// LeetCode 1816. 截斷句子。
    /// English: Given a sentence whose words are separated by single spaces and an integer k,
    /// return the sentence containing only its first k words.
    /// 中文：給定一個以單一空格分隔單字的句子與整數 k，回傳只保留前 k 個單字的句子。
    /// English: https://leetcode.com/problems/truncate-sentence/
    /// 中文：https://leetcode.cn/problems/truncate-sentence/
    /// </summary>
    private static void Main()
    {
        string maximumSentence = $"aa {string.Join(" ", Enumerable.Repeat("a", 249))}";

        CaseResult[] cases =
        [
            RunCase(
                "Official example 1",
                "Hello how are you Contestant",
                4,
                "Hello how are you"),
            RunCase(
                "Official example 2",
                "What is the solution to this problem",
                4,
                "What is the solution"),
            RunCase(
                "Official example 3",
                "chopper is not a tanuki",
                5,
                "chopper is not a tanuki"),
            RunCase("Minimum valid input", "a", 1, "a"),
            RunCase("Keep only the first word", "Hello World", 1, "Hello"),
            RunCase("Mixed letter casing", "a B c D", 3, "a B c"),
            RunCase(
                "Cut at the kth word boundary",
                "one two three four",
                3,
                "one two three"),
            RunCase(
                "Maximum sentence length",
                maximumSentence,
                1,
                "aa",
                "500 characters, 250 words")
        ];

        for (int index = 0; index < cases.Length; index++)
        {
            CaseResult caseResult = cases[index];
            Console.WriteLine($"Case: {index + 1} - {caseResult.Name}");
            Console.WriteLine($"Input: {caseResult.Input}");
            Console.WriteLine($"Expected: \"{caseResult.Expected}\"");
            Console.WriteLine($"Actual: \"{caseResult.Actual}\"");
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
    /// 對題目保證由單一空格分隔、沒有前後空格的有效句子，從左至右計數單字邊界；
    /// 遇到第 <paramref name="k"/> 個空格時回傳其前綴。若句子恰有 k 個單字，則回傳
    /// 原字串。方法不修改輸入，也不產生主控台輸出。
    /// </summary>
    public static string TruncateSentence(string s, int k)
    {
        int spacesSeen = 0;

        for (int index = 0; index < s.Length; index++)
        {
            if (s[index] != ' ')
            {
                continue;
            }

            spacesSeen++;

            // 第 k 個空格正好位於第 k 個單字之後，切片不能包含這個分隔符。
            if (spacesSeen == k)
            {
                return s[..index];
            }
        }

        return s;
    }

    private static CaseResult RunCase(
        string name,
        string input,
        int k,
        string expected,
        string? inputDescription = null)
    {
        string actual = TruncateSentence(input, k);
        string displayedInput = inputDescription ?? $"s=\"{input}\", k={k}";
        return new CaseResult(name, displayedInput, expected, actual, expected == actual);
    }

    private sealed record CaseResult(
        string Name,
        string Input,
        string Expected,
        string Actual,
        bool Passed);
}