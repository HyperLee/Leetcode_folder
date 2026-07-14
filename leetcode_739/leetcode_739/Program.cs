namespace leetcode_739;

internal static class Program
{
    /// <summary>
    /// 739. Daily Temperatures
    /// https://leetcode.com/problems/daily-temperatures/
    /// 739. 每日溫度
    /// https://leetcode.cn/problems/daily-temperatures/
    /// English: Given daily temperatures, return for every day how many days must pass before a warmer temperature; return zero when none exists.
    /// 中文：給定每日溫度，回傳每一天還要等待幾天才會遇到更高溫；若之後不存在更高溫則回傳零。
    /// </summary>
    /// <remarks>
    /// 原始需求：
    /// 簡單說題目
    /// 輸入一個陣列, 找出 i 位置後面第幾個比i大
    /// 回傳 該位置 or 距離 (距離i 多少位置)
    /// 要是都沒比他大 救回傳 0
    /// </remarks>
    /// <param name="args">命令列參數；本驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, int[] Input, int[] Expected)[] testCases =
        {
            ("Case 1: Official example", new[] { 73, 74, 75, 71, 69, 72, 76, 73 }, new[] { 1, 1, 4, 2, 1, 1, 0, 0 }),
            ("Case 2: Minimum valid input", new[] { 30 }, new[] { 0 }),
            ("Case 3: Strictly increasing", new[] { 30, 40, 50, 60 }, new[] { 1, 1, 1, 0 }),
            ("Case 4: Strictly decreasing", new[] { 60, 50, 40, 30 }, new[] { 0, 0, 0, 0 }),
            ("Case 5: Equal temperatures", new[] { 70, 70, 71 }, new[] { 2, 1, 0 }),
            ("Case 6: Chained resolution", new[] { 70, 65, 60, 80 }, new[] { 3, 2, 1, 0 })
        };

        List<(string CaseName, string Input, string CheckName, string Expected, string Actual, bool Passed)> checks = new();

        foreach ((string caseName, int[] input, int[] expected) in testCases)
        {
            int[] actual = DailyTemperatures(input);
            checks.Add((caseName, FormatArray(input), "Waiting days", FormatArray(expected), FormatArray(actual), expected.SequenceEqual(actual)));
        }

        int[] maximumInput = Enumerable.Repeat(30, 99_999).Append(100).ToArray();
        int[] maximumActual = DailyTemperatures(maximumInput);
        checks.Add(("Case 7: Maximum-length spot checks", "99,999 × 30 followed by 100", "Input length", "100000", maximumInput.Length.ToString(), maximumInput.Length == 100_000));
        checks.Add(("Case 7: Maximum-length spot checks", "99,999 × 30 followed by 100", "First waiting days", "99999", maximumActual[0].ToString(), maximumActual[0] == 99_999));
        checks.Add(("Case 7: Maximum-length spot checks", "99,999 × 30 followed by 100", "Penultimate waiting days", "1", maximumActual[^2].ToString(), maximumActual[^2] == 1));
        checks.Add(("Case 7: Maximum-length spot checks", "99,999 × 30 followed by 100", "Last waiting days", "0", maximumActual[^1].ToString(), maximumActual[^1] == 0));

        int passedCount = 0;
        string? previousCaseName = null;

        Console.WriteLine("LeetCode 739 acceptance harness");

        foreach ((string caseName, string input, string checkName, string expected, string actual, bool passed) in checks)
        {
            if (!string.Equals(caseName, previousCaseName, StringComparison.Ordinal))
            {
                Console.WriteLine();
                Console.WriteLine(caseName);
                Console.WriteLine($"Input: {input}");
                previousCaseName = caseName;
            }

            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | {checkName} | Expected: {expected} | Actual: {actual}");

            if (passed)
            {
                passedCount++;
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
    /// 使用單調遞減索引堆疊計算每一天距離下一個更高溫日的等待天數。有效輸入必須符合題目限制；回傳與輸入等長的結果陣列，不會修改輸入陣列，也不會寫入主控台。
    /// </summary>
    /// <param name="temperatures">依日期排序的溫度陣列。</param>
    /// <returns>每一天等待到下一個更高溫日的天數；不存在時為零。</returns>
    public static int[] DailyTemperatures(int[] temperatures)
    {
        int[] waitingDays = new int[temperatures.Length];
        Stack<int> unresolvedDays = new();

        for (int day = 0; day < temperatures.Length; day++)
        {
            while (unresolvedDays.Count > 0 && temperatures[day] > temperatures[unresolvedDays.Peek()])
            {
                // 首次遇到更高溫時，索引差即是這一天的最短等待天數。
                int unresolvedDay = unresolvedDays.Pop();
                waitingDays[unresolvedDay] = day - unresolvedDay;
            }

            unresolvedDays.Push(day);
        }

        return waitingDays;
    }

    private static string FormatArray(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }
}
