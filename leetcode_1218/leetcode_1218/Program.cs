namespace leetcode_1218;

internal static class Program
{
    /// <summary>
    /// 1218. Longest Arithmetic Subsequence of Given Difference
    /// https://leetcode.com/problems/longest-arithmetic-subsequence-of-given-difference/
    /// 1218. 最長定差子序列
    /// https://leetcode.cn/problems/longest-arithmetic-subsequence-of-given-difference/
    /// English: Given an integer array and a fixed difference, return the length of the longest subsequence whose adjacent elements differ by exactly that value; deleting elements must not change the order of the remaining elements.
    /// 中文：給定整數陣列與固定公差，回傳相鄰元素差恰為該公差的最長子序列長度；可以刪除元素，但不可改變其餘元素的順序。
    /// </summary>
    /// <remarks>
    /// 找出 arr 中 等差為 difference  的 最常子序列
    /// 可以刪除 element 但是不能 變更順序
    /// </remarks>
    /// <param name="args">命令列參數；驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, int[] Values, int Difference, int Expected)[] testCases =
        {
            ("Case 1: Official increasing example", new[] { 1, 2, 3, 4 }, 1, 4),
            ("Case 2: Official no-chain example", new[] { 1, 3, 5, 7 }, 1, 1),
            ("Case 3: Official negative-difference example", new[] { 1, 5, 7, 8, 5, 3, 4, 2, 1 }, -2, 4),
            ("Case 4: Minimum valid input", new[] { 5 }, 7, 1),
            ("Case 5: Zero difference with duplicates", new[] { 7, 7, 7, 7 }, 0, 4),
            ("Case 6: Subsequence order regression", new[] { 4, 1, 2, 3 }, 1, 3),
            ("Case 7: Value-range boundaries", new[] { -10_000, 0, 10_000 }, 10_000, 3)
        };

        List<CaseResult> checks = new();

        foreach ((string caseName, int[] values, int difference, int expected) in testCases)
        {
            string input = $"arr = {FormatArray(values)}, difference = {difference}";
            int dictionaryActual = LongestSubsequence(values, difference);
            int arrayActual = LongestSubsequence2(values, difference);
            checks.Add(new CaseResult(caseName, input, "Dictionary DP", expected, dictionaryActual, expected == dictionaryActual));
            checks.Add(new CaseResult(caseName, input, "Value-range array DP", expected, arrayActual, expected == arrayActual));
        }

        int[] maximumInput = Enumerable.Repeat(7, 100_000).ToArray();
        const int maximumExpected = 100_000;
        int maximumDictionaryActual = LongestSubsequence(maximumInput, 0);
        int maximumArrayActual = LongestSubsequence2(maximumInput, 0);
        const string maximumInputSummary = "arr = 100,000 × 7, difference = 0";
        checks.Add(new CaseResult("Case 8: Maximum-length spot check", maximumInputSummary, "Dictionary DP", maximumExpected, maximumDictionaryActual, maximumExpected == maximumDictionaryActual));
        checks.Add(new CaseResult("Case 8: Maximum-length spot check", maximumInputSummary, "Value-range array DP", maximumExpected, maximumArrayActual, maximumExpected == maximumArrayActual));

        int passedCount = 0;
        Console.WriteLine("LeetCode 1218 acceptance harness");

        foreach (IGrouping<string, CaseResult> caseGroup in checks.GroupBy(check => check.CaseName))
        {
            CaseResult firstCheck = caseGroup.First();
            Console.WriteLine();
            Console.WriteLine(firstCheck.CaseName);
            Console.WriteLine($"Input: {firstCheck.Input}");

            foreach (CaseResult check in caseGroup)
            {
                Console.WriteLine($"{(check.Passed ? "PASS" : "FAIL")} | {check.CheckName} | Expected: {check.Expected} | Actual: {check.Actual}");

                if (check.Passed)
                {
                    passedCount++;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{checks.Count} checks passed.");

        if (passedCount != checks.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用雜湊動態規劃計算指定公差的最長子序列。對每個目前值保存以該值結尾的最佳長度，並由「目前值減去公差」的既有狀態延伸。有效輸入須符合題目定義的非空整數陣列與公差；方法不修改輸入，也不寫入主控台。
    /// </summary>
    /// <param name="arr">題目定義的非空整數陣列，元素順序即子序列可使用的順序。</param>
    /// <param name="difference">相鄰子序列元素必須符合的固定差值。</param>
    /// <returns>相鄰差皆為 <paramref name="difference"/> 的最長子序列長度。</returns>
    public static int LongestSubsequence(int[] arr, int difference)
    {
        Dictionary<int, int> maxLengthByValue = new();
        int longest = 0;

        foreach (int value in arr)
        {
            int previousLength = maxLengthByValue.GetValueOrDefault(value - difference);
            int currentLength = previousLength + 1;

            // 依掃描順序延伸前驅狀態，確保只形成合法子序列而不是重排後的數列。
            maxLengthByValue[value] = currentLength;
            longest = Math.Max(longest, currentLength);
        }

        return longest;
    }

    /// <summary>
    /// 利用題目固定值域，以陣列動態規劃計算指定公差的最長子序列。陣列索引代表結尾值，前驅值超出合法值域時視為沒有可延伸狀態。有效輸入須符合題目定義；方法不修改輸入，也不寫入主控台。
    /// </summary>
    /// <param name="arr">元素介於 -10,000 到 10,000 的題目有效非空陣列。</param>
    /// <param name="difference">介於 -10,000 到 10,000 的固定差值。</param>
    /// <returns>相鄰差皆為 <paramref name="difference"/> 的最長子序列長度。</returns>
    public static int LongestSubsequence2(int[] arr, int difference)
    {
        const int minimumValue = -10_000;
        const int maximumValue = 10_000;
        const int offset = 10_000;
        int[] maxLengthByValue = new int[maximumValue - minimumValue + 1];
        int longest = 0;

        foreach (int value in arr)
        {
            int previousValue = value - difference;
            int previousLength = previousValue >= minimumValue && previousValue <= maximumValue
                ? maxLengthByValue[previousValue + offset]
                : 0;
            int currentLength = previousLength + 1;

            // 題目值域經 offset 後對應至 0..20,000，避免雜湊並維持固定空間。
            maxLengthByValue[value + offset] = currentLength;
            longest = Math.Max(longest, currentLength);
        }

        return longest;
    }

    private static string FormatArray(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    private readonly record struct CaseResult(string CaseName, string Input, string CheckName, int Expected, int Actual, bool Passed);
}
