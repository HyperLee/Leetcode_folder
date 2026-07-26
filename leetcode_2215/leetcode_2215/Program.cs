namespace leetcode_2215;

internal static class Program
{
    /// <summary>
    /// LeetCode 2215. Find the Difference of Two Arrays.
    /// LeetCode 2215. 找出兩陣列的不同。
    /// English: Given two 0-indexed integer arrays nums1 and nums2, return a list answer of size 2.
    /// answer[0] contains every distinct integer in nums1 that is absent from nums2, and answer[1]
    /// contains every distinct integer in nums2 that is absent from nums1. Values may be returned
    /// in any order.
    /// 中文：給定兩個索引從 0 開始的整數陣列 nums1 與 nums2，回傳長度為 2 的列表 answer。
    /// answer[0] 包含所有只出現在 nums1、未出現在 nums2 的相異整數；answer[1] 包含所有只出現在
    /// nums2、未出現在 nums1 的相異整數。列表中的整數可依任意順序回傳。
    /// English: https://leetcode.com/problems/find-the-difference-of-two-arrays/
    /// 中文：https://leetcode.cn/problems/find-the-difference-of-two-arrays/
    /// </summary>
    /// <remarks>
    /// 歷史題述（保留原文）：
    /// 2215. Find the Difference of Two Arrays
    /// https://leetcode.com/problems/find-the-difference-of-two-arrays/
    ///
    /// 2215. 找出两数组的不同
    /// https://leetcode.cn/problems/find-the-difference-of-two-arrays/
    ///
    /// 给你两个下标从 0 开始的整数数组 nums1 和 nums2 ，请你返回一个长度为 2 的
    /// 列表 answer ，其中：
    /// answer[0] 是 nums1 中所有 不 存在于 nums2 中的 不同 整数组成的列表。
    /// answer[1] 是 nums2 中所有 不 存在于 nums1 中的 不同 整数组成的列表。
    /// 注意：列表中的整数可以按 任意 顺序返回。
    ///
    /// 回傳不要同時存在於兩個array中的元素,且去除重覆
    /// </remarks>
    private static void Main()
    {
        int[] maximumLengthNums1 = Enumerable.Range(-1000, 1000).ToArray();
        int[] maximumLengthNums2 = Enumerable.Range(-500, 1000).ToArray();
        TestCase[] testCases =
        [
            new(
                "Official example 1",
                "nums1=[1, 2, 3], nums2=[2, 4, 6]",
                [1, 2, 3],
                [2, 4, 6],
                [1, 3],
                [4, 6]),
            new(
                "Official duplicate example",
                "nums1=[1, 2, 3, 3], nums2=[1, 1, 2, 2]",
                [1, 2, 3, 3],
                [1, 1, 2, 2],
                [3],
                []),
            new(
                "Minimum equal boundary values",
                "nums1=[-1000], nums2=[-1000]",
                [-1000],
                [-1000],
                [],
                []),
            new(
                "Minimum disjoint boundary values",
                "nums1=[-1000], nums2=[1000]",
                [-1000],
                [1000],
                [-1000],
                [1000]),
            new(
                "Negative values and zero",
                "nums1=[-2, -1, 0, 1], nums2=[-1, 0, 2]",
                [-2, -1, 0, 1],
                [-1, 0, 2],
                [-2, 1],
                [2]),
            new(
                "Duplicates on both sides",
                "nums1=[1, 1, 2, 2], nums2=[2, 2, 3, 3]",
                [1, 1, 2, 2],
                [2, 2, 3, 3],
                [1],
                [3]),
            new(
                "String-concatenation collision regression",
                "nums1=[1, 23, -4], nums2=[12, 3, -4]",
                [1, 23, -4],
                [12, 3, -4],
                [1, 23],
                [3, 12]),
            new(
                "Maximum input lengths",
                "nums1=[-1000..-1] (1000 values), nums2=[-500..499] (1000 values)",
                maximumLengthNums1,
                maximumLengthNums2,
                Enumerable.Range(-1000, 500).ToArray(),
                Enumerable.Range(0, 500).ToArray())
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck("Result list count", 2, result.ResultListCount));
            Console.WriteLine(PrintCheck("nums1-only values", true, result.FirstOnlyMatches));
            Console.WriteLine(PrintCheck("nums2-only values", true, result.SecondOnlyMatches));
            Console.WriteLine(PrintCheck("nums1 input preserved", true, result.Nums1Preserved));
            Console.WriteLine(PrintCheck("nums2 input preserved", true, result.Nums2Preserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 40;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 對題目保證的有效輸入 <paramref name="nums1"/> 與 <paramref name="nums2"/>，
    /// 以兩個 <see cref="HashSet{T}"/> 執行雙向集合差集，回傳只出現在第一個陣列與只出現在
    /// 第二個陣列中的相異整數。方法不修改輸入且不輸出主控台；結果內部順序不保證。
    /// 平均時間複雜度為 O(n + m)，輔助空間與結果空間皆為 O(n + m)。
    /// </summary>
    /// <param name="nums1">長度 1 至 1,000、元素介於 -1,000 至 1,000 的第一個整數陣列。</param>
    /// <param name="nums2">長度 1 至 1,000、元素介於 -1,000 至 1,000 的第二個整數陣列。</param>
    /// <returns>
    /// 長度為 2 的列表；第一個列表是只在 <paramref name="nums1"/> 出現的相異值，
    /// 第二個列表是只在 <paramref name="nums2"/> 出現的相異值。
    /// </returns>
    public static IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
    {
        HashSet<int> firstOnly = [.. nums1];
        HashSet<int> secondOnly = [.. nums2];

        // 各集合移除另一個輸入中的所有值後，分別只保留該側獨有的相異整數。
        firstOnly.ExceptWith(nums2);
        secondOnly.ExceptWith(nums1);

        return [firstOnly.ToList(), secondOnly.ToList()];
    }

    private static string PrintCheck<T>(string checkName, T expected, T actual)
    {
        string status = EqualityComparer<T>.Default.Equals(expected, actual) ? "PASS" : "FAIL";
        return $"{status} {checkName} | Expected: {expected} | Actual: {actual}";
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int[] nums1Input = [.. testCase.Nums1];
        int[] nums2Input = [.. testCase.Nums2];
        int[] nums1Original = [.. nums1Input];
        int[] nums2Original = [.. nums2Input];

        IList<IList<int>>? actual = FindDifference(nums1Input, nums2Input);
        int resultListCount = actual?.Count ?? 0;
        IList<int>? firstOnly = resultListCount > 0 ? actual![0] : null;
        IList<int>? secondOnly = resultListCount > 1 ? actual![1] : null;

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            resultListCount,
            ValuesMatch(firstOnly, testCase.ExpectedFirstOnly),
            ValuesMatch(secondOnly, testCase.ExpectedSecondOnly),
            nums1Input.SequenceEqual(nums1Original),
            nums2Input.SequenceEqual(nums2Original));
    }

    private static bool ValuesMatch(IList<int>? actual, int[] expected)
    {
        return actual is not null &&
            actual.OrderBy(value => value).SequenceEqual(expected.OrderBy(value => value));
    }

    private sealed record TestCase(
        string Name,
        string Input,
        int[] Nums1,
        int[] Nums2,
        int[] ExpectedFirstOnly,
        int[] ExpectedSecondOnly);

    private sealed record CaseResult(
        string Name,
        string Input,
        int ResultListCount,
        bool FirstOnlyMatches,
        bool SecondOnlyMatches,
        bool Nums1Preserved,
        bool Nums2Preserved)
    {
        public int PassedCheckCount =>
            (ResultListCount == 2 ? 1 : 0) +
            (FirstOnlyMatches ? 1 : 0) +
            (SecondOnlyMatches ? 1 : 0) +
            (Nums1Preserved ? 1 : 0) +
            (Nums2Preserved ? 1 : 0);
    }
}