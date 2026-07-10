namespace leetcode_412;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 412. Fizz Buzz
    /// https://leetcode.com/problems/fizz-buzz/
    /// 412. Fizz Buzz
    /// https://leetcode.cn/problems/fizz-buzz/
    /// Given an integer n, return the strings for 1 through n, replacing multiples of 3, 5, or both with Fizz, Buzz, or FizzBuzz.
    /// 給定整數 n，回傳 1 到 n 的字串，並將 3、5 或兩者的倍數分別替換為 Fizz、Buzz 或 FizzBuzz。
    /// </summary>
    private static void Main()
    {
        (int N, string[] Expected)[] sequenceCases =
        [
            (1, ["1"]),
            (3, ["1", "2", "Fizz"]),
            (5, ["1", "2", "Fizz", "4", "Buzz"]),
            (15, ["1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz"]),
            (16, ["1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz", "16"])
        ];

        Console.WriteLine("LeetCode 412 acceptance harness");
        Console.WriteLine();

        for (int i = 0; i < sequenceCases.Length; i++)
        {
            (int n, string[] expected) = sequenceCases[i];
            IList<string> actual = FizzBuzz(n);
            string expectedText = FormatSequence(expected);
            string actualText = FormatSequence(actual);
            bool passed = expected.SequenceEqual(actual);

            Console.WriteLine($"Case {i + 1}: Full sequence");
            Console.WriteLine($"Input: n = {n}");
            RecordCheck(passed);
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | Full sequence | Expected: {expectedText} | Actual: {actualText}");
            Console.WriteLine();
        }

        const int upperBound = 10000;
        IList<string> upperBoundResult = FizzBuzz(upperBound);

        Console.WriteLine("Case 6: Upper-bound spot checks");
        Console.WriteLine($"Input: n = {upperBound}");

        (string Label, string Expected, string Actual)[] upperBoundChecks =
        [
            ("Result count", "10000", upperBoundResult.Count.ToString()),
            ("Value for 1", "1", upperBoundResult[0]),
            ("Value for 3", "Fizz", upperBoundResult[2]),
            ("Value for 5", "Buzz", upperBoundResult[4]),
            ("Value for 15", "FizzBuzz", upperBoundResult[14]),
            ("Value for 10000", "Buzz", upperBoundResult[9999])
        ];

        foreach ((string label, string expected, string actual) in upperBoundChecks)
        {
            bool passed = string.Equals(expected, actual, StringComparison.Ordinal);
            RecordCheck(passed);
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | {label} | Expected: {expected} | Actual: {actual}");
        }

        Console.WriteLine();

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// Returns the Fizz Buzz representation of every integer from 1 through n.
    /// </summary>
    public static IList<string> FizzBuzz(int n)
    {
        string[] result = new string[n];

        for (int i = 1; i <= n; i++)
        {
            if (i % 15 == 0)
            {
                result[i - 1] = "FizzBuzz";
            }
            else if (i % 3 == 0)
            {
                result[i - 1] = "Fizz";
            }
            else if (i % 5 == 0)
            {
                result[i - 1] = "Buzz";
            }
            else
            {
                result[i - 1] = i.ToString();
            }
        }

        return result;
    }

    private static void RecordCheck(bool passed)
    {
        s_checks++;

        if (passed)
        {
            s_passed++;
        }
    }

    private static string FormatSequence(IEnumerable<string> values)
    {
        return $"[{string.Join(", ", values.Select(static value => $"\"{value}\""))}]";
    }
}
