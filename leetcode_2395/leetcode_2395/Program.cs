namespace leetcode_2395;

internal static class Program
{
    /// <summary>
    /// LeetCode 2395. Find Subarrays With Equal Sum.
    /// LeetCode 2395. 和相等的子陣列。
    /// English: Given a 0-indexed integer array, determine whether two length-2 subarrays
    /// that start at different indices have the same sum.
    /// 中文：給定一個索引從 0 開始的整數陣列，判斷是否存在兩個起始索引不同、
    /// 長度皆為 2 且元素總和相同的子陣列。
    /// English: https://leetcode.com/problems/find-subarrays-with-equal-sum/
    /// 中文：https://leetcode.cn/problems/find-subarrays-with-equal-sum/
    /// </summary>
    private static void Main()
    {
        int[] maximumInput = Enumerable.Range(0, 1_000).ToArray();

        TestCase[] testCases =
        [
            new("Official example 1", "nums=[4,2,4]", [4, 2, 4], true),
            new("Official example 2", "nums=[1,2,3,4,5]", [1, 2, 3, 4, 5], false),
            new("Official example 3", "nums=[0,0,0]", [0, 0, 0], true),
            new("Minimum length", "nums=[5,-5]", [5, -5], false),
            new("Separated equal sums", "nums=[1,2,2,1]", [1, 2, 2, 1], true),
            new(
                "Value limits",
                "nums=[1000000000,1000000000,-1000000000,-1000000000]",
                [1_000_000_000, 1_000_000_000, -1_000_000_000, -1_000_000_000],
                false),
            new("Maximum length with unique sums", "nums=[0..999] (length 1000)", maximumInput, false)
        ];

        (string Name, Func<int[], bool> Solve)[] solutions =
        [
            (nameof(FindSubarrays), FindSubarrays),
            (nameof(FindSubarrays2), FindSubarrays2)
        ];

        CaseResult[] results =
        [
            .. testCases.SelectMany(testCase =>
                solutions.Select(solution => RunCase(testCase, solution.Name, solution.Solve)))
        ];

        foreach (CaseResult result in results)
        {
            Console.WriteLine($"Case: {result.CaseName} [{result.SolutionName}]");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck("result", result.Expected, result.Actual));
            Console.WriteLine(PrintCheck("input preserved", true, result.InputPreserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 28;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 由左至右計算題目限制內每個長度為 2 的子陣列總和，使用雜湊集合保存已看過的總和；
    /// 若目前總和無法加入集合，代表已有不同起始索引產生相同總和。方法不修改
    /// <paramref name="nums"/> 或主控台狀態，存在重複總和時回傳 <see langword="true"/>，
    /// 否則回傳 <see langword="false"/>。時間複雜度為 O(n)，輔助空間為 O(n)。
    /// </summary>
    /// <param name="nums">長度 2 至 1000、元素介於 -10^9 至 10^9 的整數陣列。</param>
    /// <returns>是否存在兩個起始索引不同且總和相同的長度 2 子陣列。</returns>
    public static bool FindSubarrays(int[] nums)
    {
        HashSet<int> sums = [];

        for (int index = 0; index < nums.Length - 1; index++)
        {
            int sum = nums[index] + nums[index + 1];
            // HashSet.Add 回傳 false 時，這個總和必定由較早的不同起始索引產生過。
            if (!sums.Add(sum))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 逐一選擇題目限制內每個長度為 2 的子陣列，再與所有較晚起始的長度 2 子陣列比較總和。
    /// 第二個索引永遠大於第一個索引，因此相等時必定符合不同起始位置的要求。方法不修改
    /// <paramref name="nums"/> 或主控台狀態，存在重複總和時回傳 <see langword="true"/>，
    /// 否則回傳 <see langword="false"/>。時間複雜度為 O(n²)，輔助空間為 O(1)。
    /// </summary>
    /// <param name="nums">長度 2 至 1000、元素介於 -10^9 至 10^9 的整數陣列。</param>
    /// <returns>是否存在兩個起始索引不同且總和相同的長度 2 子陣列。</returns>
    public static bool FindSubarrays2(int[] nums)
    {
        for (int firstIndex = 0; firstIndex < nums.Length - 1; firstIndex++)
        {
            int firstSum = nums[firstIndex] + nums[firstIndex + 1];

            for (int secondIndex = firstIndex + 1; secondIndex < nums.Length - 1; secondIndex++)
            {
                // 從下一個起始索引開始比較，允許兩個長度 2 子陣列重疊但不會與自己比較。
                int secondSum = nums[secondIndex] + nums[secondIndex + 1];
                if (firstSum == secondSum)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static CaseResult RunCase(
        TestCase testCase,
        string solutionName,
        Func<int[], bool> solve)
    {
        int[] input = [.. testCase.Nums];
        int[] original = [.. input];
        bool actual = solve(input);

        return new CaseResult(
            testCase.Name,
            solutionName,
            testCase.Input,
            testCase.Expected,
            actual,
            input.SequenceEqual(original));
    }

    private static string PrintCheck<T>(string name, T expected, T actual)
    {
        string status = EqualityComparer<T>.Default.Equals(expected, actual) ? "PASS" : "FAIL";
        return $"{status} {name} | Expected: {expected} | Actual: {actual}";
    }

    private sealed record TestCase(string Name, string Input, int[] Nums, bool Expected);

    private sealed record CaseResult(
        string CaseName,
        string SolutionName,
        string Input,
        bool Expected,
        bool Actual,
        bool InputPreserved)
    {
        public int PassedCheckCount =>
            (Expected == Actual ? 1 : 0) +
            (InputPreserved ? 1 : 0);
    }
}