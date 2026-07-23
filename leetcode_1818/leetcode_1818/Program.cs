namespace leetcode_1818;

internal static class Program
{
    /// <summary>
    /// LeetCode 1818. Minimum Absolute Sum Difference.
    /// LeetCode 1818. 最小絕對差值和。
    /// English: Given equal-length positive integer arrays, replace at most one element in
    /// <c>nums1</c> with another value already in <c>nums1</c> to minimize the sum of absolute differences,
    /// then return the minimized sum modulo 1_000_000_007.
    /// 中文：給定兩個等長正整數陣列，最多可將 <c>nums1</c> 的一個元素替換為該陣列已有的值，
    /// 以最小化逐項絕對差值總和，並回傳最小總和對 1_000_000_007 取模後的結果。
    /// English: https://leetcode.com/problems/minimum-absolute-sum-difference/
    /// 中文：https://leetcode.cn/problems/minimum-absolute-sum-difference/
    /// </summary>
    private static void Main()
    {
        int[] hugeNums1 = Enumerable.Repeat(1, 100000).ToArray();
        int[] hugeNums2 = Enumerable.Repeat(99998, 99999).Append(100000).ToArray();

        CheckResult[] checks =
        [
            .. RunCase("Official example 1", new[] { 1, 7, 5 }, new[] { 2, 3, 5 }, 3),
            .. RunCase("Official example 2", new[] { 2, 4, 6, 8, 10 }, new[] { 2, 4, 6, 8, 10 }, 0),
            .. RunCase("Official example 3", new[] { 1, 10, 4, 4, 2, 7 }, new[] { 9, 3, 5, 1, 7, 4 }, 20),
            .. RunCase("Minimum valid input", new[] { 1 }, new[] { 100000 }, 99999),
            .. RunCase("Predecessor is better", new[] { 1, 4, 5 }, new[] { 1, 8, 5 }, 3),
            .. RunCase("Successor is better", new[] { 1, 4, 10 }, new[] { 8, 4, 8 }, 4),
            .. RunCase("Duplicate values", new[] { 1, 1, 5 }, new[] { 3, 1, 5 }, 2),
            .. RunCase("Value boundary", new[] { 1, 100000 }, new[] { 100000, 1 }, 99999),
            .. RunCase("n = 100000 modulo spot check", hugeNums1, hugeNums2, 999699939, "n=100000; nums1=100000 copies of 1; nums2=99999 copies of 99998 followed by 100000")
        ];

        foreach (CheckResult check in checks)
        {
            Console.WriteLine($"Case: {check.Name} - {check.CheckName}");
            Console.WriteLine($"Input: {check.Input}");
            Console.WriteLine($"Expected: {check.Expected}");
            Console.WriteLine($"Actual: {check.Actual}");
            Console.WriteLine($"Result: {(check.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int passedCount = checks.Count(check => check.Passed);
        Console.WriteLine($"Summary: {passedCount}/{checks.Length} checks passed.");

        if (passedCount != checks.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CheckResult[] RunCase(
        string name,
        int[] nums1,
        int[] nums2,
        int expected,
        string? inputDescription = null)
    {
        int[] nums1Before = (int[])nums1.Clone();
        int[] nums2Before = (int[])nums2.Clone();
        int actual = MinAbsoluteSumDiff(nums1, nums2);
        string input = inputDescription ?? $"nums1=[{string.Join(", ", nums1)}], nums2=[{string.Join(", ", nums2)}]";
        bool inputsPreserved = nums1.SequenceEqual(nums1Before) && nums2.SequenceEqual(nums2Before);

        return
        [
            new CheckResult(name, input, "Answer", expected.ToString(), actual.ToString(), expected == actual),
            new CheckResult(name, input, "Input arrays preserved", "nums1 and nums2 unchanged", inputsPreserved ? "nums1 and nums2 unchanged" : "input array changed", inputsPreserved)
        ];
    }

    /// <summary>
    /// 對題目保證等長且元素為正整數的兩個陣列，計算最多替換 <paramref name="nums1"/> 一個
    /// 元素後的最小絕對差值和。方法先建立並排序 <paramref name="nums1"/> 的副本，再以
    /// lower bound 同時檢查目標值的前驅與後繼，找出可減少的最大差值；不修改任一輸入陣列，
    /// 並回傳題目指定模數下的最小總和。
    /// </summary>
    public static int MinAbsoluteSumDiff(int[] nums1, int[] nums2)
    {
        const int Modulo = 1000000007;
        int[] sortedNums1 = (int[])nums1.Clone();
        Array.Sort(sortedNums1);

        long totalDifference = 0;
        int maximumReduction = 0;

        for (int index = 0; index < nums1.Length; index++)
        {
            int originalDifference = Math.Abs(nums1[index] - nums2[index]);
            totalDifference += originalDifference;
            int successorIndex = BinarySearch(sortedNums1, nums2[index]);

            // Lower bound 將候選值分成前驅與後繼；兩者皆可能是距離目標最近的值。
            if (successorIndex < sortedNums1.Length)
            {
                int successorDifference = sortedNums1[successorIndex] - nums2[index];
                maximumReduction = Math.Max(maximumReduction, originalDifference - successorDifference);
            }

            if (successorIndex > 0)
            {
                int predecessorDifference = nums2[index] - sortedNums1[successorIndex - 1];
                maximumReduction = Math.Max(maximumReduction, originalDifference - predecessorDifference);
            }
        }

        return (int)((totalDifference - maximumReduction) % Modulo);
    }

    /// <summary>
    /// 在已遞增排序且非空的 <paramref name="rec"/> 中執行 lower bound 搜尋，回傳第一個
    /// 大於或等於 <paramref name="target"/> 的索引；若所有元素都小於目標值則回傳陣列長度。
    /// 搜尋不修改陣列，並維持答案位於半開區間 <c>[left, right)</c> 的不變量。
    /// </summary>
    public static int BinarySearch(int[] rec, int target)
    {
        int left = 0;
        int right = rec.Length;

        while (left < right)
        {
            int middle = left + ((right - left) / 2);

            // 小於 target 的位置不可能是答案，保留 middle 以右的候選半開區間。
            if (rec[middle] < target)
            {
                left = middle + 1;
            }
            else
            {
                right = middle;
            }
        }

        return left;
    }

    private sealed record CheckResult(
        string Name,
        string Input,
        string CheckName,
        string Expected,
        string Actual,
        bool Passed);
}