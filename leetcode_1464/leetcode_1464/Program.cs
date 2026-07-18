namespace leetcode_1464;

internal class Program
{
    /// <summary>
    /// 1464. Maximum Product of Two Elements in an Array
    /// 1464. 陣列中兩個元素的最大乘積
    /// https://leetcode.com/problems/maximum-product-of-two-elements-in-an-array/
    /// https://leetcode.cn/problems/maximum-product-of-two-elements-in-an-array/
    /// Given an integer array, choose two different elements and return the maximum value of
    /// (nums[i] - 1) * (nums[j] - 1).
    /// 給定整數陣列，選取兩個不同元素，回傳 (nums[i] - 1) * (nums[j] - 1) 的最大值。
    /// </summary>
    /// <param name="args">主控台啟動參數；本驗證器不使用。</param>
    private static void Main(string[] args)
    {
        int[] maximumLengthNumbers = [.. Enumerable.Range(1, 498), 1000, 999];
        TestCase[] testCases =
        [
            new("Official example 1", "[3, 4, 5, 2]", [3, 4, 5, 2], 12),
            new("Official example 2 / duplicate maximum", "[1, 5, 4, 5]", [1, 5, 4, 5], 16),
            new("Official example 3 / minimum length", "[3, 7]", [3, 7], 12),
            new("Minimum values", "[1, 1]", [1, 1], 0),
            new("Maximum values", "[1000, 1000]", [1000, 1000], 998001),
            new("Largest arrives first / second-largest regression", "[10, 2, 5, 2]", [10, 2, 5, 2], 36),
            new("Unsorted general regression", "[4, 9, 2, 8, 3]", [4, 9, 2, 8, 3], 56),
            new("Maximum-length case", "[length 500; values 1..498, 1000, 999]", maximumLengthNumbers, 997002)
        ];

        int passed = 0;
        foreach (TestCase testCase in testCases)
        {
            int[] originalNumbers = [.. testCase.Numbers];
            int actual = MaxProduct(testCase.Numbers);
            bool isPassed = actual == testCase.Expected && testCase.Numbers.SequenceEqual(originalNumbers);
            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: {testCase.Input}");
            Console.WriteLine($"Expected: {testCase.Expected}");
            Console.WriteLine($"Actual: {actual}");
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
    /// 在 nums 長度至少為二且每個值皆符合題目限制的有效輸入下，單趟掃描維護最大與次大值，
    /// 以計算兩個元素依題意各減一後的最大乘積；回傳該最大乘積。
    /// </summary>
    /// <param name="nums">長度介於 2 至 500，且元素介於 1 至 1000 的有效整數陣列。</param>
    /// <returns>兩個不同元素各減一後可得到的最大乘積。</returns>
    public static int MaxProduct(int[] nums)
    {
        int largest = 0;
        int secondLargest = 0;

        foreach (int number in nums)
        {
            // largest 與 secondLargest 始終保存目前已掃描元素中的前兩大值。
            if (number > largest)
            {
                secondLargest = largest;
                largest = number;
            }
            else if (number > secondLargest)
            {
                secondLargest = number;
            }
        }

        return (largest - 1) * (secondLargest - 1);
    }

    private sealed record TestCase(string Name, string Input, int[] Numbers, int Expected);
}
