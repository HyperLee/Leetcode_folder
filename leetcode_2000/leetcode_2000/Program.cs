using System.Text;

namespace leetcode_2000;

internal static class Program
{
    /// <summary>
    /// LeetCode 2000. Reverse Prefix of Word.
    /// LeetCode 2000. 反轉單字前綴。
    /// English: https://leetcode.com/problems/reverse-prefix-of-word/
    /// 中文：https://leetcode.cn/problems/reverse-prefix-of-word/
    /// English: Given a 0-indexed string word and a character ch, reverse the segment from
    /// index 0 through the first occurrence of ch, inclusive. If ch is absent, return word
    /// unchanged.
    /// 中文：給定從索引 0 開始的字串 word 與字元 ch，反轉從索引 0 到 ch 第一次出現
    /// 位置（含）的區段；若 word 不含 ch，則原樣回傳 word。
    /// </summary>
    private static void Main()
    {
        string maximumInput = new string('a', 249) + 'b';
        string maximumExpected = 'b' + new string('a', 249);
        TestCase[] cases =
        [
            new("Official example 1", "abcdefd", 'd', "dcbaefd"),
            new("Official example 2", "xyxzxe", 'z', "zxyxxe"),
            new("Official example 3", "abcd", 'z', "abcd"),
            new("Minimum input", "a", 'a', "a"),
            new("Character at first position", "leetcode", 'l', "leetcode"),
            new("Character at last position", "abcd", 'd', "dcba"),
            new("Odd-length prefix", "abcdef", 'c', "cbadef"),
            new("First of repeated characters", "azbyzcz", 'z', "zabyzcz"),
            new("Maximum-length input", maximumInput, 'b', maximumExpected)
        ];

        Console.WriteLine("LeetCode 2000 Acceptance Harness");

        int passedChecks = 0;
        foreach (TestCase testCase in cases)
        {
            string reconstructionActual = ReversePrefix(testCase.Word, testCase.Character);
            string twoPointerActual = ReversePrefix2(testCase.Word, testCase.Character);

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: word = {FormatWord(testCase.Word)}, ch = '{testCase.Character}'");

            CheckResult reconstructionResult = EvaluateCheck(
                "ReversePrefix result",
                testCase.Expected,
                reconstructionActual);
            Console.WriteLine(reconstructionResult.Output);
            passedChecks += reconstructionResult.Passed ? 1 : 0;

            CheckResult twoPointerResult = EvaluateCheck(
                "ReversePrefix2 result",
                testCase.Expected,
                twoPointerActual);
            Console.WriteLine(twoPointerResult.Output);
            passedChecks += twoPointerResult.Passed ? 1 : 0;
            Console.WriteLine();
        }

        const int totalChecks = 18;
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CheckResult EvaluateCheck<T>(string checkName, T expected, T actual)
    {
        bool passed = EqualityComparer<T>.Default.Equals(expected, actual);
        string output =
            $"{(passed ? "PASS" : "FAIL")} {checkName} | " +
            $"Expected: {FormatValue(expected)} | Actual: {FormatValue(actual)}";
        return new CheckResult(passed, output);
    }

    private static string FormatValue<T>(T value)
    {
        return value is string text ? FormatWord(text) : value?.ToString() ?? "null";
    }

    private static string FormatWord(string word)
    {
        const int visibleCharactersPerSide = 16;
        if (word.Length <= visibleCharactersPerSide * 2)
        {
            return $"\"{word}\"";
        }

        return
            $"\"{word[..visibleCharactersPerSide]}...{word[^visibleCharactersPerSide..]}\" " +
            $"(length: {word.Length})";
    }

    /// <summary>
    /// 對長度介於 1 至 250、只含小寫英文字母的 <paramref name="word" />，尋找
    /// <paramref name="ch" /> 第一次出現的位置，反向走訪該位置以前（含）的字元，再接回
    /// 未反轉的後綴。若找不到指定字元則回傳原字串。此純函式不輸出主控台；時間複雜度為
    /// O(n)，結果空間與輔助空間皆為 O(n)。
    /// </summary>
    public static string ReversePrefix(string word, char ch)
    {
        int firstIndex = word.IndexOf(ch);
        if (firstIndex < 0)
        {
            return word;
        }

        StringBuilder result = new(word.Length);
        for (int index = firstIndex; index >= 0; index--)
        {
            result.Append(word[index]);
        }

        result.Append(word.AsSpan(firstIndex + 1));
        return result.ToString();
    }

    /// <summary>
    /// 對長度介於 1 至 250、只含小寫英文字母的 <paramref name="word" />，尋找
    /// <paramref name="ch" /> 第一次出現的位置，將字串複製成字元陣列後，以左右指標交換
    /// 索引 0 到該位置（含）的字元。若找不到指定字元則回傳原字串。此純函式不輸出主控台；
    /// 時間複雜度為 O(n)，結果空間與輔助空間皆為 O(n)。
    /// </summary>
    public static string ReversePrefix2(string word, char ch)
    {
        int firstIndex = word.IndexOf(ch);
        if (firstIndex < 0)
        {
            return word;
        }

        char[] characters = word.ToCharArray();
        int left = 0;
        int right = firstIndex;

        // 交換範圍止於第一次出現的位置，後續相同字元與其後綴必須保持原順序。
        while (left < right)
        {
            (characters[left], characters[right]) = (characters[right], characters[left]);
            left++;
            right--;
        }

        return new string(characters);
    }

    private sealed record TestCase(string Name, string Word, char Character, string Expected);

    private sealed record CheckResult(bool Passed, string Output);
}