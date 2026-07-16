namespace leetcode_1291;

internal static class Program
{
    /// <summary>
    /// 1291. Sequential Digits
    /// https://leetcode.com/problems/sequential-digits/
    /// 1291. 順次數
    /// https://leetcode.cn/problems/sequential-digits/
    /// English: Return every integer in the inclusive range [low, high] whose digits are each one greater than the preceding digit.
    /// 中文：回傳閉區間 [low, high] 中，每一位數字都比前一位大 1 的所有整數，並依遞增順序排列。
    /// </summary>
    /// <param name="args">命令列參數；驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, int Low, int High, int[] Expected)[] testCases =
        {
            ("Case 1: Official example 1", 100, 300, new[] { 123, 234 }),
            ("Case 2: Official example 2", 1_000, 13_000, new[] { 1_234, 2_345, 3_456, 4_567, 5_678, 6_789, 12_345 }),
            ("Case 3: Minimum range without result", 10, 10, Array.Empty<int>()),
            ("Case 4: First sequential digit", 10, 12, new[] { 12 }),
            ("Case 5: Exact single match", 123, 123, new[] { 123 }),
            ("Case 6: Cross digit lengths", 58, 155, new[] { 67, 78, 89, 123 }),
            ("Case 7: Range without result", 90, 100, Array.Empty<int>()),
            ("Case 8: Full constraint range", 10, 1_000_000_000, new[]
            {
                12, 23, 34, 45, 56, 67, 78, 89,
                123, 234, 345, 456, 567, 678, 789,
                1_234, 2_345, 3_456, 4_567, 5_678, 6_789,
                12_345, 23_456, 34_567, 45_678, 56_789,
                123_456, 234_567, 345_678, 456_789,
                1_234_567, 2_345_678, 3_456_789,
                12_345_678, 23_456_789,
                123_456_789
            })
        };

        List<CaseResult> checks = new();

        foreach ((string caseName, int low, int high, int[] expected) in testCases)
        {
            IList<int> actual = SequentialDigits(low, high);
            checks.Add(new CaseResult(caseName, low, high, expected, actual, expected.SequenceEqual(actual)));
        }

        int passedCount = 0;
        Console.WriteLine("LeetCode 1291 acceptance harness");

        foreach (CaseResult check in checks)
        {
            Console.WriteLine();
            Console.WriteLine(check.CaseName);
            Console.WriteLine($"Input: low = {check.Low}, high = {check.High}");
            Console.WriteLine($"Expected: {FormatSequence(check.Expected)}");
            Console.WriteLine($"Actual: {FormatSequence(check.Actual)}");
            Console.WriteLine(check.Passed ? "PASS" : "FAIL");

            if (check.Passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{checks.Count} checks passed.");

        if (passedCount != checks.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 從每個可能的起始數字逐位加一建立順次數，回傳落在題目保證有效之閉區間內的所有結果，並依數值遞增排序。
    /// </summary>
    /// <param name="low">題目保證介於 10 與 <paramref name="high"/> 之間的區間下界。</param>
    /// <param name="high">題目保證介於 <paramref name="low"/> 與 10<sup>9</sup> 之間的區間上界。</param>
    /// <returns>位於閉區間內且相鄰數字依序增加 1 的整數清單。</returns>
    public static IList<int> SequentialDigits(int low, int high)
    {
        List<int> results = new();

        for (int firstDigit = 1; firstDigit <= 9; firstDigit++)
        {
            int number = firstDigit;

            // 每次附加比前一位大 1 的數字，使 number 始終維持順次數不變量。
            for (int nextDigit = firstDigit + 1; nextDigit <= 9; nextDigit++)
            {
                number = (number * 10) + nextDigit;

                if (number >= low && number <= high)
                {
                    results.Add(number);
                }
            }
        }

        results.Sort();
        return results;
    }

    private static string FormatSequence(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    private readonly record struct CaseResult(
        string CaseName,
        int Low,
        int High,
        IList<int> Expected,
        IList<int> Actual,
        bool Passed);
}
