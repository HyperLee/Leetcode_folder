namespace leetcode_2971;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 2971. Find Polygon With the Largest Perimeter
    /// https://leetcode.com/problems/find-polygon-with-the-largest-perimeter/
    /// 2971. 找到最大周長的多邊形
    /// https://leetcode.cn/problems/find-polygon-with-the-largest-perimeter/
    /// Given an array of positive integers, return the largest perimeter of a polygon that can be formed from its values, or -1 if no polygon is possible.
    /// 給定一個正整數陣列，回傳可由其中邊長組成之多邊形的最大周長；若無法組成多邊形則回傳 -1。
    /// </summary>
    private static void Main()
    {
        Console.WriteLine("LeetCode 2971 acceptance harness");
        Console.WriteLine();

        RunCase("Official example 1", [5, 5, 5], 15);
        RunCase("Official example 2", [1, 12, 1, 2, 5, 50, 3], 12);
        RunCase("Official example 3", [5, 5, 50], -1);
        RunCase("Minimum valid input", [1, 1, 1], 3);
        RunCase("Strict inequality", [1, 1, 2], -1);
        RunCase("Complete valid prefix", [1, 2, 3, 4, 5], 15);
        RunCase("Three-side regression", [2, 3, 3], 8);
        RunCase("64-bit perimeter", [1_000_000_000, 1_000_000_000, 1_000_000_000], 3_000_000_000);
        RunCase(
            "Upper-bound spot check",
            Enumerable.Repeat(1_000_000_000, 100_000).ToArray(),
            100_000_000_000_000,
            "[1_000_000_000 repeated 100000 times]");

        Console.WriteLine();
        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 將符合 LeetCode 有效輸入契約的正整數邊長陣列就地排序，使用前綴和判斷可形成多邊形的最大前綴，並回傳最大周長；若不存在有效多邊形則回傳 -1。
    /// </summary>
    public static long LargestPerimeter(int[] nums)
    {
        long largestPerimeter = -1;
        long prefixSum = 0;

        Array.Sort(nums);

        foreach (int edgeLength in nums)
        {
            prefixSum += edgeLength;

            // 排序後目前邊長是前綴中的最長邊；只有其他邊總和嚴格大於它時才能形成多邊形。
            if (prefixSum > 2L * edgeLength)
            {
                largestPerimeter = prefixSum;
            }
        }

        return largestPerimeter;
    }

    private static void RunCase(string name, int[] nums, long expected, string? inputDescription = null)
    {
        string input = inputDescription ?? $"[{string.Join(", ", nums)}]";
        long actual = LargestPerimeter(nums);
        bool passed = expected == actual;

        s_checks++;

        if (passed)
        {
            s_passed++;
        }

        Console.WriteLine(
            $"{(passed ? "PASS" : "FAIL")} | {name} | Input: {input} | Expected: {expected} | Actual: {actual}");
    }
}