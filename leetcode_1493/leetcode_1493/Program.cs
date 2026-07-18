namespace leetcode_1493;

internal class Program
{
    /// <summary>
    /// 1493. Longest Subarray of 1's After Deleting One Element
    /// 1493. 刪除一個元素後全為 1 的最長子陣列
    /// https://leetcode.com/problems/longest-subarray-of-1s-after-deleting-one-element/
    /// https://leetcode.cn/problems/longest-subarray-of-1s-after-deleting-one-element/
    /// Given a binary array, delete exactly one element and return the length of the longest
    /// non-empty subarray containing only 1's; return 0 when no such subarray exists.
    /// 給定二進位陣列，刪除恰好一個元素後，回傳只包含 1 的最長非空子陣列長度；
    /// 若不存在這樣的子陣列，則回傳 0。
    /// </summary>
    /// <param name="args">主控台啟動參數；本驗證器不使用。</param>
    private static void Main(string[] args)
    {
        int[] maximumLengthNumbers =
        [
            .. Enumerable.Repeat(1, 50_000),
            0,
            .. Enumerable.Repeat(1, 49_999)
        ];

        TestCase[] testCases =
        [
            new("Official example 1", "nums = [1, 1, 0, 1]", [1, 1, 0, 1], 3),
            new("Official example 2", "nums = [0, 1, 1, 1, 0, 1, 1, 0, 1]", [0, 1, 1, 1, 0, 1, 1, 0, 1], 5),
            new("Official example 3 / all ones", "nums = [1, 1, 1]", [1, 1, 1], 2),
            new("Minimum input / zero", "nums = [0]", [0], 0),
            new("Minimum input / one", "nums = [1]", [1], 0),
            new("All zeros", "nums = [0, 0, 0]", [0, 0, 0], 0),
            new("Zero at the left boundary", "nums = [0, 1, 1, 1]", [0, 1, 1, 1], 3),
            new("Bridge two runs of ones", "nums = [1, 1, 0, 1, 1, 1, 0, 1, 1]", [1, 1, 0, 1, 1, 1, 0, 1, 1], 5),
            new("Shrink across consecutive zeros", "nums = [1, 0, 0, 1, 1, 1]", [1, 0, 0, 1, 1, 1], 3),
            new("Maximum length / one middle zero", "nums = [50000 ones, 0, 49999 ones]", maximumLengthNumbers, 99_999)
        ];

        int passed = 0;
        foreach (TestCase testCase in testCases)
        {
            int[] originalNumbers = [.. testCase.Numbers];
            int actual = LongestSubarray(testCase.Numbers);
            bool isPassed = actual == testCase.Expected
                && testCase.Numbers.SequenceEqual(originalNumbers);

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: {testCase.Input}");
            Console.WriteLine($"Expected: {testCase.Expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            if (isPassed)
            {
                passed++;
            }
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 在 nums 為長度 1 至 100000 的有效二進位陣列時，以滑動視窗維持最多一個 0，
    /// 將視窗長度扣除必須刪除的一個元素後更新答案；回傳刪除恰好一個元素後，
    /// 只包含 1 的最長非空子陣列長度，且不修改 nums。
    /// </summary>
    /// <param name="nums">每個元素皆為 0 或 1 的有效整數陣列。</param>
    /// <returns>刪除恰好一個元素後，只包含 1 的最長非空子陣列長度。</returns>
    public static int LongestSubarray(int[] nums)
    {
        int left = 0;
        int zeroCount = 0;
        int longestLength = 0;

        for (int right = 0; right < nums.Length; right++)
        {
            if (nums[right] == 0)
            {
                zeroCount++;
            }

            while (zeroCount > 1)
            {
                if (nums[left] == 0)
                {
                    zeroCount--;
                }

                left++;
            }

            // 有效視窗長度為 right - left + 1；扣除必刪元素後即為 right - left。
            longestLength = Math.Max(longestLength, right - left);
        }

        return longestLength;
    }

    private sealed record TestCase(string Name, string Input, int[] Numbers, int Expected);
}