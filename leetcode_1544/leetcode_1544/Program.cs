namespace leetcode_1544;

internal class Program
{
    /// <summary>
    /// LeetCode 1544. Make The String Great.
    /// LeetCode 1544. 整理字串。
    /// English: Remove adjacent pairs of the same English letter whose cases differ,
    /// repeating until no such pair remains, and return the resulting string.
    /// 中文：反覆移除相鄰且為同一英文字母、但大小寫不同的字元對，直到不能再移除，
    /// 並回傳最後的字串。
    /// English: https://leetcode.com/problems/make-the-string-great/
    /// 中文：https://leetcode.cn/problems/make-the-string-great/
    /// </summary>
    private static void Main()
    {
        (string Input, string Expected)[] cases =
        [
            ("leEeetcode", "leetcode"),
            ("abBAcC", string.Empty),
            ("s", "s"),
            ("aa", "aa"),
            ("abA", "abA"),
            ("aA", string.Empty),
            ("Aa", string.Empty),
            ("abBA", string.Empty),
            ("aAbBc", "c"),
            (string.Concat(Enumerable.Repeat("aA", 50)), string.Empty)
        ];

        int passedChecks = 0;

        for (int index = 0; index < cases.Length; index++)
        {
            (string input, string expected) = cases[index];
            string actual = MakeGood(input);
            bool passed = actual == expected;

            Console.WriteLine($"Case: {index + 1}");
            Console.WriteLine($"Input: {Display(input)}");
            Console.WriteLine($"Expected: {Display(expected)}");
            Console.WriteLine($"Actual: {Display(actual)}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            if (passed)
            {
                passedChecks++;
            }
        }

        Console.WriteLine($"Summary: {passedChecks}/{cases.Length} checks passed.");

        if (passedChecks != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string Display(string value)
    {
        return value.Length == 0 ? "(empty)" : value;
    }

    /// <summary>
    /// 依題目保證的英文字母輸入，以堆疊維護已整理完成的前綴；相鄰同字母且大小寫相反時
    /// 移除配對，否則保留字元，最後回傳整理後的字串。
    /// </summary>
    public static string MakeGood(string s)
    {
        Stack<char> stack = new();

        foreach (char character in s)
        {
            // 堆疊中的內容永遠是目前輸入前綴已無可消除相鄰字元對的結果。
            if (stack.Count > 0 && Math.Abs(stack.Peek() - character) == 32)
            {
                stack.Pop();
            }
            else
            {
                stack.Push(character);
            }
        }

        return string.Create(
            stack.Count,
            stack,
            static (characters, source) =>
            {
                int targetIndex = source.Count - 1;

                foreach (char character in source)
                {
                    characters[targetIndex] = character;
                    targetIndex--;
                }
            });
    }
}
