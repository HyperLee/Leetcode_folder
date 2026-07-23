namespace leetcode_1802;

internal static class Program
{
    /// <summary>
    /// LeetCode 1802: Maximum Value at a Given Index in a Bounded Array /
    /// 有界陣列中指定索引處的最大值。
    /// https://leetcode.com/problems/maximum-value-at-a-given-index-in-a-bounded-array/
    /// https://leetcode.cn/problems/maximum-value-at-a-given-index-in-a-bounded-array/
    /// Given a positive integer array of length n whose adjacent values differ by at most
    /// one and whose sum is at most maxSum, maximize the value at index. 給定長度為 n 的
    /// 正整數陣列，相鄰元素差的絕對值不得超過一且總和不得超過 maxSum，求指定 index
    /// 可取得的最大值。
    /// </summary>
    private static void Main()
    {
        List<CaseResult> cases =
        [
            RunCase("Official example 1", 4, 2, 6, 2),
            RunCase("Official example 2", 6, 1, 10, 3),
            RunCase("Minimum valid input", 1, 0, 1, 1),
            RunCase("Single element maximum budget", 1, 0, 1_000_000_000, 1_000_000_000),
            RunCase("Peak at left boundary", 4, 0, 7, 3),
            RunCase("Peak at right boundary", 4, 3, 7, 3),
            RunCase("Both sides reach one", 5, 2, 10, 3),
            RunCase("Tight adjacent boundary", 2, 0, 3, 2),
            RunCase("Large arithmetic-series sum", 3, 1, 1_000_000_000, 333_333_334),
            RunCase("Maximum length minimum budget", 1_000_000_000, 500_000_000, 1_000_000_000, 1)
        ];

        foreach (CaseResult caseResult in cases)
        {
            Console.WriteLine($"Case: {caseResult.Name}");
            Console.WriteLine($"Input: {caseResult.Input}");
            Console.WriteLine($"Expected: {caseResult.Expected}");
            Console.WriteLine($"Actual: {caseResult.Actual}");
            Console.WriteLine($"Result: {(caseResult.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int passedCount = cases.Count(caseResult => caseResult.Passed);
        Console.WriteLine($"Summary: {passedCount}/{cases.Count} checks passed.");

        if (passedCount != cases.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 對題目保證有效的 n、index 與 maxSum，以二分搜尋找出指定索引可配置的最大正整數。
    /// 每次以最省總和的山形陣列判斷候選峰值是否可行，回傳不超過 maxSum 的最大峰值。
    /// </summary>
    public static int MaxValue(int n, int index, int maxSum)
    {
        int left = 1;
        int right = maxSum;

        while (left < right)
        {
            // 上取中點讓可行的 mid 能安全取代 left，避免只剩兩個候選時停滯。
            int mid = left + (right - left + 1) / 2;

            if (IsFeasible(mid, n, index, maxSum))
            {
                left = mid;
            }
            else
            {
                right = mid - 1;
            }
        }

        return left;
    }

    /// <summary>
    /// 判斷題目保證有效的尺寸與預算下，指定 peak 能否放在 index；計算峰值與左右兩側
    /// 逐步下降且最低為 1 的最小必要總和，若不超過 maxSum 則回傳 true。
    /// </summary>
    private static bool IsFeasible(int peak, int n, int index, int maxSum)
    {
        long minimumSum = peak
            + CalculateSideSum(peak, index)
            + CalculateSideSum(peak, n - index - 1);

        return minimumSum <= maxSum;
    }

    /// <summary>
    /// 計算峰值一側 length 個位置的最小總和；有效 peak 至少為 1、length 至少為 0。
    /// 數值從 peak - 1 每格下降 1，降到 1 後以 1 補足，並以 long 回傳避免大數乘法溢位。
    /// </summary>
    private static long CalculateSideSum(int peak, int length)
    {
        int descendingLength = peak - 1;

        if (length < descendingLength)
        {
            int smallestValue = peak - length;
            return (long)(peak - 1 + smallestValue) * length / 2;
        }

        long descendingSum = (long)peak * descendingLength / 2;
        int ones = length - descendingLength;
        return descendingSum + ones;
    }

    private static CaseResult RunCase(string name, int n, int index, int maxSum, int expected)
    {
        int actual = MaxValue(n, index, maxSum);
        return new CaseResult(name, $"n={n}, index={index}, maxSum={maxSum}", expected, actual, expected == actual);
    }

    private sealed record CaseResult(string Name, string Input, int Expected, int Actual, bool Passed);
}