namespace leetcode_1802;

internal static class Program
{
    /// <summary>
    /// <para>
    /// 1802. Maximum Value at a Given Index in a Bounded Array
    /// https://leetcode.com/problems/maximum-value-at-a-given-index-in-a-bounded-array/description/
    ///
    /// You are given three positive integers n, index, and maxSum. Construct a 0-indexed array nums satisfying:
    /// - nums.length == n
    /// - nums[i] is positive for 0 &lt;= i &lt; n
    /// - abs(nums[i] - nums[i+1]) &lt;= 1 for 0 &lt;= i &lt; n - 1
    /// - The sum of nums does not exceed maxSum.
    /// - nums[index] is maximized.
    ///
    /// Return nums[index] of the constructed array. Note: abs(x) equals x when x &gt;= 0 and -x otherwise.
    ///
    /// Example 1:
    /// Input: n = 4, index = 2, maxSum = 6
    /// Output: 2
    /// Explanation: nums = [1,2,[2],1] satisfies every condition. No valid array has nums[2] == 3.
    ///
    /// Example 2:
    /// Input: n = 6, index = 1, maxSum = 10
    /// Output: 3
    ///
    /// Constraints:
    /// - 1 &lt;= n &lt;= maxSum &lt;= 10^9
    /// - 0 &lt;= index &lt; n
    /// </para>
    /// <para>
    /// 1802. 有界陣列中指定索引處的最大值
    /// https://leetcode.cn/problems/maximum-value-at-a-given-index-in-a-bounded-array/description/
    ///
    /// 給定三個正整數 n、index、maxSum。請建立一個從 0 開始索引的陣列 nums，並滿足：
    /// - nums.length == n
    /// - 對 0 &lt;= i &lt; n，nums[i] 為正整數
    /// - 對 0 &lt;= i &lt; n - 1，abs(nums[i] - nums[i+1]) &lt;= 1
    /// - nums 的元素總和不超過 maxSum。
    /// - nums[index] 儘可能大。
    ///
    /// 回傳所建立陣列的 nums[index]。注意：當 x &gt;= 0 時 abs(x) 等於 x，否則等於 -x。
    ///
    /// 範例 1：
    /// 輸入：n = 4, index = 2, maxSum = 6
    /// 輸出：2
    /// 說明：nums = [1,2,[2],1] 滿足所有條件，且不存在 nums[2] == 3 的有效陣列。
    ///
    /// 範例 2：
    /// 輸入：n = 6, index = 1, maxSum = 10
    /// 輸出：3
    ///
    /// 限制條件：
    /// - 1 &lt;= n &lt;= maxSum &lt;= 10^9
    /// - 0 &lt;= index &lt; n
    /// </para>
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