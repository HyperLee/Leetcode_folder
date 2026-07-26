using System.Text;

namespace leetcode_2390;

internal class Program
{
    /// <summary>
    /// LeetCode 2390 - Removing Stars From a String.
    /// LeetCode 2390 - 從字串中移除星號。
    /// English: https://leetcode.com/problems/removing-stars-from-a-string/
    /// 中文：https://leetcode.cn/problems/removing-stars-from-a-string/
    /// English: Repeatedly remove each star together with the closest non-star character to its left,
    /// then return the unique string that remains after all valid operations.
    /// 中文：依序移除每個星號及其左側最近的非星號字元，並回傳所有合法操作完成後唯一剩餘的字串。
    /// </summary>
    private static void Main()
    {
        string maximumInput = string.Concat(Enumerable.Repeat("ab*", 33333)) + "z";
        string maximumExpected = new string('a', 33333) + "z";

        (string Name, string InputDisplay, string Input, string Expected, bool SummarizeResult)[] cases =
        [
            ("Official example 1", "\"leet**cod*e\"", "leet**cod*e", "lecoe", false),
            ("Official example 2", "\"erase*****\"", "erase*****", string.Empty, false),
            ("Minimum retained character", "\"a\"", "a", "a", false),
            ("Minimum complete removal", "\"a*\"", "a*", string.Empty, false),
            ("No stars", "\"abcdefghijklmnopqrstuvwxyz\"", "abcdefghijklmnopqrstuvwxyz", "abcdefghijklmnopqrstuvwxyz", false),
            ("Interleaved removals", "\"ab*c*d\"", "ab*c*d", "ad", false),
            ("Consecutive and interleaved removals", "\"abc**d*e\"", "abc**d*e", "ae", false),
            ("100,000-character mixed input", "\"ab*\" x 33333 + \"z\"", maximumInput, maximumExpected, true)
        ];

        (string Name, Func<string, string> Solve)[] solutions =
        [
            (nameof(RemoveStars), RemoveStars),
            (nameof(RemoveStars2), RemoveStars2),
            (nameof(RemoveStars3), RemoveStars3)
        ];

        int passed = 0;

        foreach ((string caseName, string inputDisplay, string input, string expected, bool summarizeResult) in cases)
        {
            foreach ((string solutionName, Func<string, string> solve) in solutions)
            {
                string actual = solve(input);
                bool isPass = actual == expected;

                Console.WriteLine($"Case: {caseName} [{solutionName}]; Input: {inputDisplay}");
                Console.WriteLine($"Expected: {FormatResult(expected, summarizeResult)}");
                Console.WriteLine($"Actual: {FormatResult(actual, summarizeResult)}");
                Console.WriteLine(isPass ? "PASS" : "FAIL");

                if (isPass)
                {
                    passed++;
                }
            }
        }

        int totalChecks = cases.Length * solutions.Length;
        Console.WriteLine($"Summary: {passed}/{totalChecks} checks passed.");

        if (passed != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 以 <see cref="List{T}"/> 模擬可從尾端刪除的字元序列。輸入符合題目保證，只包含小寫英文字母與星號，
    /// 且每個星號左側都有可刪除字元；方法不修改輸入也不輸出，回傳完成所有刪除後的唯一字串。
    /// </summary>
    public static string RemoveStars(string s)
    {
        List<char> keptCharacters = new(s.Length);

        foreach (char character in s)
        {
            if (character == '*')
            {
                keptCharacters.RemoveAt(keptCharacters.Count - 1);
            }
            else
            {
                keptCharacters.Add(character);
            }
        }

        return new string(keptCharacters.ToArray());
    }

    /// <summary>
    /// 以 <see cref="StringBuilder"/> 保存目前仍存在的字元，遇到星號便刪除尾端字元。輸入符合題目的合法操作保證；
    /// 方法不修改輸入也不輸出，回傳完成所有刪除後的唯一字串。
    /// </summary>
    public static string RemoveStars2(string s)
    {
        StringBuilder keptCharacters = new(s.Length);

        foreach (char character in s)
        {
            if (character == '*')
            {
                keptCharacters.Remove(keptCharacters.Length - 1, 1);
            }
            else
            {
                keptCharacters.Append(character);
            }
        }

        return keptCharacters.ToString();
    }

    /// <summary>
    /// 以 <see cref="Stack{T}"/> 的 LIFO 特性保存尚未被刪除的字元，星號會彈出左側最近的字元。輸入符合題目的
    /// 合法操作保證；方法不修改輸入也不輸出，回傳將堆疊反轉為原始順序後的唯一結果字串。
    /// </summary>
    public static string RemoveStars3(string s)
    {
        Stack<char> keptCharacters = new();

        foreach (char character in s)
        {
            if (character == '*')
            {
                keptCharacters.Pop();
            }
            else
            {
                keptCharacters.Push(character);
            }
        }

        // Stack 列舉順序是由頂端往底端，必須反轉才能恢復剩餘字元的原始相對順序。
        return new string(keptCharacters.Reverse().ToArray());
    }

    private static string FormatResult(string value, bool summarize)
    {
        if (!summarize)
        {
            return $"\"{value}\"";
        }

        int prefixLength = Math.Min(5, value.Length);
        int suffixStart = Math.Max(0, value.Length - 5);
        return $"length={value.Length}, prefix=\"{value[..prefixLength]}\", suffix=\"{value[suffixStart..]}\"";
    }
}