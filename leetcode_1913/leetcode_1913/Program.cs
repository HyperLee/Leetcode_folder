namespace leetcode_1913;

internal static class Program
{
    /// <summary>
    /// LeetCode 1913. Maximum Product Difference Between Two Pairs.
    /// LeetCode 1913. 兩個數對之間的最大乘積差。
    /// English: Given an integer array, choose four distinct indices such that the difference
    /// between the product of the first pair and the product of the second pair is maximized.
    /// 中文：給定整數陣列，選擇四個相異索引，使第一組數對的乘積減去第二組數對的乘積之差最大。
    /// English: https://leetcode.com/problems/maximum-product-difference-between-two-pairs/
    /// 中文：https://leetcode.cn/problems/maximum-product-difference-between-two-pairs/
    /// </summary>
    private static void Main()
    {
        TestCase[] testCases =
        [
            new("Official example 1", "[5, 6, 2, 7, 4]", [5, 6, 2, 7, 4], 34),
            new("Official example 2", "[4, 2, 5, 9, 7, 4, 8]", [4, 2, 5, 9, 7, 4, 8], 64),
            new("Minimum length", "[1, 2, 3, 4]", [1, 2, 3, 4], 10),
            new("Duplicate extrema", "[1, 1, 10, 10]", [1, 1, 10, 10], 99),
            new("All equal", "[5, 5, 5, 5]", [5, 5, 5, 5], 0),
            new("Late extrema", "[3, 4, 9, 10, 2, 1]", [3, 4, 9, 10, 2, 1], 88),
            new(
                "Maximum-length spot check",
                "[5000 × 9996, 1 × 2, 10000 × 2]",
                [.. Enumerable.Repeat(5000, 9996), 1, 1, 10000, 10000],
                99999999)
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck(
                "MaxProductDifference result",
                result.Expected,
                result.Actual));
            Console.WriteLine(PrintCheck(
                "MaxProductDifference input preserved",
                true,
                result.InputPreserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 14;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string PrintCheck(string checkName, int expected, int actual)
    {
        string status = expected == actual ? "PASS" : "FAIL";
        return $"{status} {checkName} | Expected: {expected} | Actual: {actual}";
    }

    private static string PrintCheck(string checkName, bool expected, bool actual)
    {
        string status = expected == actual ? "PASS" : "FAIL";
        return $"{status} {checkName} | Expected: {expected} | Actual: {actual}";
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int[] numbers = [.. testCase.Numbers];
        int[] numbersBefore = [.. numbers];
        int actual = MaxProductDifference(numbers);
        bool inputPreserved = numbers.SequenceEqual(numbersBefore);

        return new CaseResult(testCase.Name, testCase.Input, testCase.Expected, actual, inputPreserved);
    }

    /// <summary>
    /// 對題目保證長度至少為 4、元素值介於 1 至 10000 的有效整數陣列單次掃描，同時維護兩個
    /// 最大值與兩個最小值，計算最大乘積差。方法只讀取 <paramref name="nums"/>，不修改輸入或
    /// 主控台狀態；回傳最大兩值乘積減最小兩值乘積的結果，時間複雜度為 O(n)，結果與輔助空間皆為 O(1)。
    /// </summary>
    /// <param name="nums">題目限制內且至少包含四個元素的整數陣列。</param>
    /// <returns>兩個最大值乘積減兩個最小值乘積的最大乘積差。</returns>
    public static int MaxProductDifference(int[] nums)
    {
        int largest = int.MinValue;
        int secondLargest = int.MinValue;
        int smallest = int.MaxValue;
        int secondSmallest = int.MaxValue;

        foreach (int number in nums)
        {
            if (number >= largest)
            {
                // 先下移舊第一名，再覆寫第一名，讓相同極值可各占一個位置。
                secondLargest = largest;
                largest = number;
            }
            else if (number > secondLargest)
            {
                secondLargest = number;
            }

            if (number <= smallest)
            {
                // 最小值同樣必須先保留舊第一名，才能維持兩個最小值的不變量。
                secondSmallest = smallest;
                smallest = number;
            }
            else if (number < secondSmallest)
            {
                secondSmallest = number;
            }
        }

        return (largest * secondLargest) - (smallest * secondSmallest);
    }

    private sealed record TestCase(string Name, string Input, int[] Numbers, int Expected);

    private sealed record CaseResult(
        string Name,
        string Input,
        int Expected,
        int Actual,
        bool InputPreserved)
    {
        public int PassedCheckCount =>
            (Actual == Expected ? 1 : 0) +
            (InputPreserved ? 1 : 0);
    }
}