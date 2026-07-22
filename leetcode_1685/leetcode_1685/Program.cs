namespace leetcode_1685;

internal class Program
{
    /// <summary>
    /// 1685. Sum of Absolute Differences in a Sorted Array
    /// 1685. 有序陣列中絕對差值之和
    /// https://leetcode.com/problems/sum-of-absolute-differences-in-a-sorted-array/
    /// https://leetcode.cn/problems/sum-of-absolute-differences-in-a-sorted-array/
    /// Given a sorted integer array nums, return an array result where result[i] is the sum of
    /// absolute differences between nums[i] and every other element in nums.
    /// 給定一個遞增排序的整數陣列 nums，回傳陣列 result，其中 result[i] 是 nums[i]
    /// 與 nums 內所有其他元素之絕對差值總和。
    /// </summary>
    /// <param name="args">主控台啟動參數；本驗證器不使用。</param>
    private static void Main(string[] args)
    {
        int[] maximumLengthNumbers = new int[100_000];
        Array.Fill(maximumLengthNumbers, 1, 0, 50_000);
        Array.Fill(maximumLengthNumbers, 10_000, 50_000, 50_000);
        int[] maximumLengthExpected = Enumerable.Repeat(499_950_000, 100_000).ToArray();

        TestCase[] testCases =
        [
            new("Official example 1", [2, 3, 5], [4, 3, 5]),
            new("Official example 2", [1, 4, 6, 8, 10], [24, 15, 13, 15, 21]),
            new("Minimum length / equal values", [1, 1], [0, 0]),
            new("Minimum and maximum values", [1, 10_000], [9_999, 9_999]),
            new("Duplicate groups", [1, 1, 3, 3], [4, 4, 4, 4]),
            new("Skewed distances regression", [1, 2, 10], [10, 9, 17]),
            new("Balanced consecutive values", [1, 2, 3, 4, 5], [10, 7, 6, 7, 10]),
            new("Maximum length / boundary values", maximumLengthNumbers, maximumLengthExpected)
        ];

        int passed = 0;
        foreach (TestCase testCase in testCases)
        {
            int[] originalNumbers = [.. testCase.Numbers];
            int[] actual = GetSumAbsoluteDifferences(testCase.Numbers);
            bool answerMatches = actual.SequenceEqual(testCase.Expected);
            bool inputPreserved = testCase.Numbers.SequenceEqual(originalNumbers);
            bool isPassed = answerMatches && inputPreserved;
            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: {FormatArray(testCase.Numbers)}");
            Console.WriteLine($"Expected: {FormatArray(testCase.Expected)}");
            Console.WriteLine($"Actual: {FormatArray(actual)}");
            Console.WriteLine($"Input preserved: {(inputPreserved ? "YES" : "NO")}");
            Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 在 nums 已依非遞減順序排列且符合題目有效輸入限制時，以總和與前綴和分別計算
    /// 每個元素和左右兩側元素的差值總和；回傳各索引的絕對差值總和，且不修改 nums。
    /// </summary>
    /// <param name="nums">長度介於 2 至 100,000，元素介於 1 至 10,000 的排序整數陣列。</param>
    /// <returns>各索引元素與陣列內所有其他元素之絕對差值總和。</returns>
    public static int[] GetSumAbsoluteDifferences(int[] nums)
    {
        int totalSum = 0;
        foreach (int number in nums)
        {
            totalSum += number;
        }

        int[] result = new int[nums.Length];
        int leftSum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            // 排序性保證左側值不大於 nums[i]，因此每個差值可直接寫成 nums[i] - nums[j]。
            int leftDifference = (nums[i] * i) - leftSum;
            int rightSum = totalSum - leftSum - nums[i];
            int rightDifference = rightSum - (nums[i] * (nums.Length - i - 1));
            result[i] = leftDifference + rightDifference;
            leftSum += nums[i];
        }

        return result;
    }

    private static string FormatArray(int[] numbers)
    {
        const int previewLength = 3;
        if (numbers.Length <= previewLength * 2)
        {
            return $"[{string.Join(", ", numbers)}]";
        }

        return $"[{string.Join(", ", numbers.Take(previewLength))}, ..., " +
            $"{string.Join(", ", numbers.TakeLast(previewLength))}] (length {numbers.Length})";
    }

    private sealed record TestCase(string Name, int[] Numbers, int[] Expected);
}