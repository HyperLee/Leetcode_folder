namespace leetcode_1822;

internal static class Program
{
    /// <summary>
    /// LeetCode 1822. Sign of the Product of an Array.
    /// LeetCode 1822. 陣列元素乘積的符號。
    /// English: https://leetcode.com/problems/sign-of-the-product-of-an-array/
    /// 中文：https://leetcode.cn/problems/sign-of-the-product-of-an-array/
    /// English: Given an integer array, return 1 if the product of all elements is positive,
    /// -1 if it is negative, and 0 if it is zero.
    /// 中文：給定整數陣列，若所有元素的乘積為正數則回傳 1，為負數則回傳 -1，為零則回傳 0。
    /// </summary>
    private static void Main()
    {
        TestCase[] cases =
        [
            new("Official positive example", [-1, -2, -3, -4, 3, 2, 1], 1),
            new("Official zero example", [1, 5, 0, 2, -3], 0),
            new("Official negative example", [-1, 1, -1, 1, -1], -1),
            new("Minimum positive", [100], 1),
            new("Minimum negative", [-100], -1),
            new("Legacy sample", [9, 72, 34, 29, -49, -22, -77, -17, -66, -75, -44, -30, -24], -1),
            new("Zero at beginning", [0, -1, -1], 0),
            new("Upper-bound odd negatives", CreateUpperBoundInput(), -1)
        ];

        Console.WriteLine("LeetCode 1822 Acceptance Harness");

        int passedChecks = 0;
        foreach (TestCase testCase in cases)
        {
            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: {FormatInput(testCase.Input)}");

            int[] firstInput = (int[])testCase.Input.Clone();
            int[] firstOriginal = (int[])firstInput.Clone();
            int firstActual = ArraySign(firstInput);
            CheckResult firstResult = EvaluateCheck("ArraySign result", testCase.Expected, firstActual);
            Console.WriteLine(firstResult.Output);
            passedChecks += firstResult.Passed ? 1 : 0;
            CheckResult firstPreservation = EvaluateCheck("ArraySign input preserved", true, firstInput.SequenceEqual(firstOriginal));
            Console.WriteLine(firstPreservation.Output);
            passedChecks += firstPreservation.Passed ? 1 : 0;

            int[] secondInput = (int[])testCase.Input.Clone();
            int[] secondOriginal = (int[])secondInput.Clone();
            int secondActual = ArraySign2(secondInput);
            CheckResult secondResult = EvaluateCheck("ArraySign2 result", testCase.Expected, secondActual);
            Console.WriteLine(secondResult.Output);
            passedChecks += secondResult.Passed ? 1 : 0;
            CheckResult secondPreservation = EvaluateCheck("ArraySign2 input preserved", true, secondInput.SequenceEqual(secondOriginal));
            Console.WriteLine(secondPreservation.Output);
            passedChecks += secondPreservation.Passed ? 1 : 0;
            Console.WriteLine();
        }

        const int totalChecks = 32;
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CheckResult EvaluateCheck<T>(string checkName, T expected, T actual)
    {
        bool passed = EqualityComparer<T>.Default.Equals(expected, actual);
        string output = $"{(passed ? "PASS" : "FAIL")} {checkName} | Expected: {expected} | Actual: {actual}";
        return new CheckResult(passed, output);
    }

    private static int[] CreateUpperBoundInput()
    {
        int[] values = new int[1_000];
        Array.Fill(values, -100);
        values[^1] = 100;
        return values;
    }

    private static string FormatInput(int[] values)
    {
        return values.Length == 1_000
            ? "[-100 x 999, 100]"
            : $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// 以一次掃描統計負數個數來判斷陣列乘積的符號；題目保證 <paramref name="nums" />
    /// 長度介於 1 至 1000，且元素介於 -100 至 100。遇到零立即回傳 0；否則負數為偶數
    /// 時回傳 1、奇數時回傳 -1。此純函式不修改輸入、不計算完整乘積，也不輸出主控台，
    /// 時間複雜度為 O(n)，結果空間與輔助空間皆為 O(1)。
    /// </summary>
    public static int ArraySign(int[] nums)
    {
        int negativeCount = 0;
        foreach (int number in nums)
        {
            if (number == 0)
            {
                // 乘積一旦含零便固定為零，後續元素無法改變其符號。
                return 0;
            }

            if (number < 0)
            {
                negativeCount++;
            }
        }

        // 非零乘積的符號只由負因子的奇偶數決定。
        return negativeCount % 2 == 0 ? 1 : -1;
    }

    /// <summary>
    /// 以初始符號 1 單次掃描陣列，對每個負數翻轉符號來判斷乘積符號；題目保證
    /// <paramref name="nums" /> 長度介於 1 至 1000，且元素介於 -100 至 100。遇到零立即
    /// 回傳 0，否則回傳累積的 1 或 -1。此純函式不修改輸入、不計算完整乘積，也不輸出
    /// 主控台，時間複雜度為 O(n)，結果空間與輔助空間皆為 O(1)。
    /// </summary>
    public static int ArraySign2(int[] nums)
    {
        int sign = 1;
        foreach (int number in nums)
        {
            if (number == 0)
            {
                // 零會使整個乘積歸零，因此不需要繼續掃描。
                return 0;
            }

            if (number < 0)
            {
                // 每多一個負因子，非零乘積的符號恰好翻轉一次。
                sign = -sign;
            }
        }

        return sign;
    }

    private sealed record TestCase(string Name, int[] Input, int Expected);

    private sealed record CheckResult(bool Passed, string Output);
}