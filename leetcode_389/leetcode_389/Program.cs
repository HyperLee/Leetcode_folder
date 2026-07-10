namespace leetcode_389;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 389. Find the Difference
    /// https://leetcode.com/problems/find-the-difference/
    /// 389. 找不同
    /// https://leetcode.cn/problems/find-the-difference/
    /// Given two lowercase strings where t is a shuffled copy of s with one extra letter, return the added letter.
    /// 給定兩個小寫英文字串，t 是將 s 重新排列後再加入一個字母的結果，請回傳新增的字母。
    /// </summary>
    private static void Main()
    {
        (string S, string T, char Expected)[] cases =
        [
            ("abcd", "abcde", 'e'),
            ("", "y", 'y'),
            ("abcd", "eabcd", 'e'),
            ("a", "aa", 'a'),
            ("ae", "aea", 'a'),
            ("aabbcc", "cbacaba", 'a')
        ];

        Console.WriteLine("LeetCode 389 acceptance harness");
        Console.WriteLine();

        for (int i = 0; i < cases.Length; i++)
        {
            (string s, string t, char expected) = cases[i];
            Console.WriteLine($"Case {i + 1}");
            Console.WriteLine($"Input: s = \"{s}\", t = \"{t}\"");
            Console.WriteLine($"Expected added character: '{expected}'");

            Check("Sorting", expected, FindTheDifference(s, t));
            Check("List removal", expected, FindTheDifference2(s, t));
            Check("XOR", expected, FindTheDifference3(s, t));
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// Sorts copies of both strings and returns the first mismatched character,
    /// or the final character of the longer sorted array.
    /// </summary>
    public static char FindTheDifference(string s, string t)
    {
        char[] sourceCharacters = s.ToCharArray();
        char[] targetCharacters = t.ToCharArray();
        Array.Sort(sourceCharacters);
        Array.Sort(targetCharacters);

        for (int i = 0; i < sourceCharacters.Length; i++)
        {
            if (sourceCharacters[i] != targetCharacters[i])
            {
                return targetCharacters[i];
            }
        }

        return targetCharacters[^1];
    }

    /// <summary>
    /// Removes one matching occurrence for every source character from a mutable
    /// list initialized with all target characters, leaving the added character.
    /// </summary>
    public static char FindTheDifference2(string s, string t)
    {
        List<char> remainingCharacters = [.. t];

        foreach (char character in s)
        {
            remainingCharacters.Remove(character);
        }

        return remainingCharacters[0];
    }

    /// <summary>
    /// XORs every character code from both strings so paired characters cancel
    /// and the added character remains.
    /// </summary>
    public static char FindTheDifference3(string s, string t)
    {
        int difference = 0;

        foreach (char character in s)
        {
            difference ^= character;
        }

        foreach (char character in t)
        {
            difference ^= character;
        }

        return (char)difference;
    }

    private static void Check(string solution, char expected, char actual)
    {
        s_checks++;
        bool passed = expected == actual;

        if (passed)
        {
            s_passed++;
        }

        Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | {solution} | Expected: '{expected}' | Actual: '{actual}'");
    }
}
