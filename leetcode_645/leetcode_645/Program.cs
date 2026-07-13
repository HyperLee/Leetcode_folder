namespace leetcode_645;

internal static class Program
{
    /// <summary>
    /// 645. Set Mismatch；645. 錯誤的集合。
    /// English URL: https://leetcode.com/problems/set-mismatch/
    /// 中文 URL: https://leetcode.cn/problems/set-mismatch/
    /// Given an integer array containing numbers from 1 through n, find its duplicated and missing values.
    /// 給定含有 1 到 n 數字的整數陣列，找出重複值與遺失值。
    /// </summary>
    private static void Main()
    {
        List<CaseResult> cases =
        [
            RunCase("Official example and input remains unchanged", "nums = [1, 2, 2, 4]", [2, 3], [1, 2, 2, 4], true),
            RunCase("Minimum valid input", "nums = [1, 1]", [1, 2], [1, 1]),
            RunCase("Missing first value", "nums = [2, 2, 3, 4]", [2, 1], [2, 2, 3, 4]),
            RunCase("Missing last value", "nums = [1, 2, 3, 3]", [3, 4], [1, 2, 3, 3]),
            RunCase("Duplicate first value", "nums = [1, 1, 3, 4]", [1, 2], [1, 1, 3, 4]),
            RunCase("Duplicate last value", "nums = [1, 2, 4, 4]", [4, 3], [1, 2, 4, 4]),
            RunCase("Unordered regression", "nums = [4, 3, 2, 2]", [2, 1], [4, 3, 2, 2]),
            RunUpperBoundCase()
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

    private static CaseResult RunCase(string name, string input, int[] expected, int[] nums, bool verifyInputUnchanged = false)
    {
        int[] original = (int[])nums.Clone();
        int[] actual = FindErrorNums(nums);
        bool passed = actual.SequenceEqual(expected) && (!verifyInputUnchanged || nums.SequenceEqual(original));
        string expectedDescription = verifyInputUnchanged
            ? $"result = {FormatNumbers(expected)}; nums remains {FormatNumbers(original)}"
            : FormatNumbers(expected);
        string actualDescription = verifyInputUnchanged
            ? $"result = {FormatNumbers(actual)}; nums is {FormatNumbers(nums)}"
            : FormatNumbers(actual);
        return new CaseResult(name, input, expectedDescription, actualDescription, passed);
    }

    private static CaseResult RunUpperBoundCase()
    {
        const int valueCount = 10000;
        int[] nums = Enumerable.Range(1, valueCount).ToArray();
        nums[^1] = valueCount - 1;
        return RunCase(
            "Upper-bound spot check",
            "nums = [1, 2, ..., 9999, 9999] (n = 10000)",
            [9999, 10000],
            nums);
    }

    /// <summary>
    /// 以長度為 n + 1 的計數陣列統計每個值；輸入必須符合 LeetCode 的有效範圍 1..n，並依序回傳 [重複值, 遺失值]。此方法為純函式，不輸出主控台訊息，也不修改輸入陣列。
    /// </summary>
    public static int[] FindErrorNums(int[] nums)
    {
        int[] counts = new int[nums.Length + 1];

        foreach (int num in nums)
        {
            counts[num]++;
        }

        int duplicate = 0;
        int missing = 0;

        // 掃描 1..n 時，出現兩次與零次的計數分別代表重複值與遺失值。
        for (int value = 1; value <= nums.Length; value++)
        {
            if (counts[value] == 2)
            {
                duplicate = value;
            }
            else if (counts[value] == 0)
            {
                missing = value;
            }
        }

        return [duplicate, missing];
    }

    private static string FormatNumbers(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    private sealed record CaseResult(string Name, string Input, string Expected, string Actual, bool Passed);
}
