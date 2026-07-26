using System.Text;

namespace leetcode_2864;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 2864. Maximum Odd Binary Number
    /// https://leetcode.com/problems/maximum-odd-binary-number/
    /// 2864. 最大二進位奇數
    /// https://leetcode.cn/problems/maximum-odd-binary-number/
    /// Given a binary string containing at least one 1, rearrange its bits to form the maximum possible odd binary number.
    /// 給定至少包含一個 1 的二進位字串，重新排列所有位元，組成可取得的最大二進位奇數。
    /// </summary>
    private static void Main()
    {
        (string Input, string Expected)[] exactCases =
        [
            ("1", "1"),
            ("010", "001"),
            ("0101", "1001"),
            ("111", "111"),
            ("1000", "0001"),
            ("1100", "1001"),
            ("101010", "110001")
        ];

        Console.WriteLine("LeetCode 2864 acceptance harness");
        Console.WriteLine();

        for (int i = 0; i < exactCases.Length; i++)
        {
            (string input, string expected) = exactCases[i];
            string? actual = MaximumOddBinaryNumber(input);
            string actualText = actual ?? "<null>";

            Console.WriteLine($"Case {i + 1}: Exact result");
            Console.WriteLine($"Input: s = \"{input}\"");
            RecordCheck(
                "Maximum odd binary number",
                expected,
                actualText,
                string.Equals(expected, actual, StringComparison.Ordinal));
            Console.WriteLine();
        }

        string upperBoundInput = $"{new string('1', 50)}{new string('0', 50)}";
        string? upperBoundResult = MaximumOddBinaryNumber(upperBoundInput);

        Console.WriteLine("Case 8: Upper-bound spot checks");
        Console.WriteLine("Input: 50 ones followed by 50 zeros");

        int? resultLength = upperBoundResult?.Length;
        RecordCheck(
            "Result length",
            "100",
            resultLength?.ToString() ?? "<null>",
            resultLength == 100);

        string expectedCounts = "ones=50, zeros=50";
        string actualCounts = upperBoundResult is null
            ? "<unavailable>"
            : $"ones={upperBoundResult.Count(static bit => bit == '1')}, " +
              $"zeros={upperBoundResult.Count(static bit => bit == '0')}";
        RecordCheck(
            "Bit counts preserved",
            expectedCounts,
            actualCounts,
            string.Equals(expectedCounts, actualCounts, StringComparison.Ordinal));

        int? leadingOnes = upperBoundResult is null
            ? null
            : CountLeading(upperBoundResult, '1');
        RecordCheck(
            "Leading ones",
            "49",
            leadingOnes?.ToString() ?? "<unavailable>",
            leadingOnes == 49);

        string expectedMiddleBits = new('0', 50);
        string actualMiddleBits = upperBoundResult?.Length >= 99
            ? upperBoundResult.Substring(49, 50)
            : "<unavailable>";
        RecordCheck(
            "Middle zeros",
            expectedMiddleBits,
            actualMiddleBits,
            string.Equals(expectedMiddleBits, actualMiddleBits, StringComparison.Ordinal));

        string actualLastBit = string.IsNullOrEmpty(upperBoundResult)
            ? "<unavailable>"
            : upperBoundResult[^1].ToString();
        RecordCheck(
            "Least-significant bit",
            "1",
            actualLastBit,
            string.Equals("1", actualLastBit, StringComparison.Ordinal));

        Console.WriteLine();
        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 接收長度 1 到 100、只含 0 與 1 且至少包含一個 1 的字串 s；統計其中的 1，保留一個 1 在最低位，
    /// 其餘 1 置前、0 置中，並回傳使用相同位元可排列出的最大二進位奇數。
    /// </summary>
    public static string MaximumOddBinaryNumber(string s)
    {
        int ones = s.Count(static bit => bit == '1');
        int zeros = s.Length - ones;
        StringBuilder result = new(s.Length);

        // 最低位必須保留一個 1 才能維持奇數，其餘 1 越靠左，數值就越大。
        result.Append('1', ones - 1);
        result.Append('0', zeros);
        result.Append('1');

        return result.ToString();
    }

    private static void RecordCheck(
        string label,
        string expected,
        string actual,
        bool passed)
    {
        s_checks++;

        if (passed)
        {
            s_passed++;
        }

        Console.WriteLine(
            $"{(passed ? "PASS" : "FAIL")} | {label} | Expected: {expected} | Actual: {actual}");
    }

    private static int CountLeading(string value, char target)
    {
        int count = 0;

        while (count < value.Length && value[count] == target)
        {
            count++;
        }

        return count;
    }
}
