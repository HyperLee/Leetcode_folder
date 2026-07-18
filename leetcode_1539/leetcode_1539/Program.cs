using System.Globalization;

namespace leetcode_1539;

internal class Program
{
    /// <summary>
    /// 1539. Kth Missing Positive Number／第 k 個缺失的正整數。
    /// LeetCode English: https://leetcode.com/problems/kth-missing-positive-number/
    /// LeetCode 中文：https://leetcode.cn/problems/kth-missing-positive-number/
    /// Given a strictly increasing array of positive integers and a positive integer k,
    /// return the kth positive integer that is missing from the array.
    /// 給定嚴格遞增的正整數陣列與正整數 k，回傳陣列中缺失的第 k 個正整數。
    /// </summary>
    private static void Main()
    {
        TestCase[] cases =
        [
            new("Official example 1", [2, 3, 4, 7, 11], 5, 9),
            new("Official example 2", [1, 2, 3, 4], 2, 6),
            new("Minimum values", [1], 1, 2),
            new("Missing before first element", [2], 1, 1),
            new("Missing inside array", [2, 3, 4, 7, 11], 3, 6),
            new("Missing after last element", [1, 2, 3], 5, 8),
            new("Maximum k after dense prefix", [1, 2, 3, 4, 5], 1_000, 1_005),
            new("Maximum first value and k", [1_000], 1_000, 1_001),
            new("Maximum length and k", Enumerable.Range(1, 1_000).ToArray(), 1_000, 2_000)
        ];

        int passedChecks = 0;
        int totalChecks = cases.Length * 2;

        foreach (TestCase testCase in cases)
        {
            CaseResult result = RunCase(testCase);
            passedChecks += result.ValuePassed ? 1 : 0;
            passedChecks += result.InputPreserved ? 1 : 0;

            Console.WriteLine($"Case: {result.Name}");
            Console.WriteLine($"Input: {FormatInput(result.Input)}");
            Console.WriteLine($"k: {result.K.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine("Check: kth missing positive");
            Console.WriteLine($"Expected: {result.Expected.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Actual: {result.Actual.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Result: {(result.ValuePassed ? "PASS" : "FAIL")}");
            Console.WriteLine("Check: input preserved");
            Console.WriteLine("Expected: True");
            Console.WriteLine($"Actual: {result.InputPreserved}");
            Console.WriteLine($"Result: {(result.InputPreserved ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int[] input = (int[])testCase.Input.Clone();
        int[] snapshot = (int[])input.Clone();
        int actual = FindKthPositive(input, testCase.K);
        bool inputPreserved = input.SequenceEqual(snapshot);

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.K,
            testCase.Expected,
            actual,
            actual == testCase.Expected,
            inputPreserved);
    }

    /// <summary>
    /// 從正整數 1 開始逐一枚舉，找出嚴格遞增陣列中缺失的第 k 個正整數。
    /// 陣列索引只在目前正整數存在於陣列時前進；其餘數值依序累計為缺失項目，且不修改輸入。
    /// 僅處理題目定義的有效輸入：非空、嚴格遞增的正整數陣列與正整數 k。
    /// </summary>
    /// <param name="arr">依題目限制提供的嚴格遞增正整數陣列。</param>
    /// <param name="k">要尋找的缺失正整數序號。</param>
    /// <returns>陣列中缺失的第 k 個正整數。</returns>
    public static int FindKthPositive(int[] arr, int k)
    {
        int arrayIndex = 0;
        int current = 1;
        int missingCount = 0;

        while (missingCount < k)
        {
            if (arrayIndex < arr.Length && arr[arrayIndex] == current)
            {
                arrayIndex++;
            }
            else
            {
                missingCount++;
            }

            current++;
        }

        return current - 1;
    }

    private static string FormatInput(int[] input)
    {
        const int compactThreshold = 10;
        const int displayedValues = 3;

        if (input.Length <= compactThreshold)
        {
            return $"[{string.Join(", ", input.Select(value => value.ToString(CultureInfo.InvariantCulture)))}]";
        }

        string first = string.Join(", ", input.Take(displayedValues).Select(value => value.ToString(CultureInfo.InvariantCulture)));
        string last = string.Join(", ", input.TakeLast(displayedValues).Select(value => value.ToString(CultureInfo.InvariantCulture)));
        return $"[{first}, ..., {last}] (Length: {input.Length.ToString(CultureInfo.InvariantCulture)})";
    }

    private sealed record TestCase(string Name, int[] Input, int K, int Expected);

    private sealed record CaseResult(
        string Name,
        int[] Input,
        int K,
        int Expected,
        int Actual,
        bool ValuePassed,
        bool InputPreserved);
}
