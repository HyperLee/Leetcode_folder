using System.Globalization;

namespace leetcode_643;

internal static class Program
{
    /// <summary>
    /// 643. Maximum Average Subarray I；643. 子陣列最大平均數 I。
    /// English URL: https://leetcode.com/problems/maximum-average-subarray-i/
    /// 中文 URL: https://leetcode.cn/problems/maximum-average-subarray-i/
    /// Given an integer array and k, return the largest average among all contiguous subarrays of length k.
    /// 給定整數陣列與 k，回傳所有長度為 k 的連續子陣列中最大的平均值。
    /// </summary>
    private static void Main()
    {
        List<CaseResult> cases =
        [
            RunAverageCase("Official example", "nums = [1, 12, -5, -6, 50, 3], k = 4", 12.75, [1, 12, -5, -6, 50, 3], 4),
            RunAverageCase("Minimum valid input", "nums = [5], k = 1", 5, [5], 1),
            RunAverageCase("k = 1 regression", "nums = [-1, -2], k = 1", -1, [-1, -2], 1),
            RunAverageCase("All negative values", "nums = [-8, -6, -7], k = 2", -6.5, [-8, -6, -7], 2),
            RunAverageCase("k equals n", "nums = [7, -3, 10], k = 3", 14d / 3d, [7, -3, 10], 3),
            RunAverageCase("Last window wins", "nums = [0, 4, 0, 3, 2], k = 2", 2.5, [0, 4, 0, 3, 2], 2),
            RunUpperBoundCase(),
            RunInputUnchangedCase()
        ];

        foreach (CaseResult caseResult in cases)
        {
            Console.WriteLine($"Case: {caseResult.Name}");
            Console.WriteLine($"Input: {caseResult.Input}");
            Console.WriteLine($"Expected: {caseResult.Expected}");
            Console.WriteLine($"Actual: {caseResult.Actual}");
            Console.WriteLine(caseResult.Passed ? "PASS" : "FAIL");
        }

        int passedCount = cases.Count(caseResult => caseResult.Passed);
        Console.WriteLine($"Summary: {passedCount}/{cases.Count} checks passed.");
        Environment.ExitCode = passedCount == cases.Count ? 0 : 1;
    }

    /// <summary>
    /// 以固定長度滑動視窗維護每個候選區間的總和；nums 與 k 必須符合題目定義的有效輸入，回傳最大總和除以 k 的雙精度平均值，且不修改 nums 或輸出主控台訊息。
    /// </summary>
    public static double FindMaxAverage(int[] nums, int k)
    {
        int currentSum = 0;

        for (int index = 0; index < k; index++)
        {
            currentSum += nums[index];
        }

        int maxSum = currentSum;

        for (int right = k; right < nums.Length; right++)
        {
            // 固定視窗右移時只加入新右端並移除舊左端，總和始終對應長度 k 的連續區間。
            currentSum += nums[right] - nums[right - k];
            maxSum = Math.Max(maxSum, currentSum);
        }

        return (double)maxSum / k;
    }

    private static CaseResult RunAverageCase(string name, string input, double expected, int[] nums, int k)
    {
        double actual = FindMaxAverage(nums, k);
        return new CaseResult(name, input, FormatDouble(expected), FormatDouble(actual), AreClose(expected, actual));
    }

    private static CaseResult RunUpperBoundCase()
    {
        const int valueCount = 100000;
        const int value = 10000;
        int[] nums = Enumerable.Repeat(value, valueCount).ToArray();
        return RunAverageCase("Upper-bound spot check", "nums = [10000 x 100000], k = 100000", value, nums, valueCount);
    }

    private static CaseResult RunInputUnchangedCase()
    {
        int[] nums = [1, 12, -5, -6, 50, 3];
        int[] original = (int[])nums.Clone();
        double actual = FindMaxAverage(nums, 4);
        bool passed = AreClose(12.75, actual) && nums.SequenceEqual(original);
        string expected = "average = 12.75; nums = [1, 12, -5, -6, 50, 3]";
        string actualDescription = $"average = {FormatDouble(actual)}; nums = {FormatNumbers(nums)}";
        return new CaseResult("Input remains unchanged", "nums = [1, 12, -5, -6, 50, 3], k = 4", expected, actualDescription, passed);
    }

    private static bool AreClose(double expected, double actual)
    {
        return Math.Abs(expected - actual) <= 0.000001;
    }

    private static string FormatDouble(double value)
    {
        return value.ToString("0.######", CultureInfo.InvariantCulture);
    }

    private static string FormatNumbers(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    private sealed record CaseResult(string Name, string Input, string Expected, string Actual, bool Passed);
}
