namespace leetcode_502;

internal static class Program
{
    /// <summary>
    /// 502. IPO／502. 首次公開募股。
    /// https://leetcode.com/problems/ipo/
    /// https://leetcode.cn/problems/ipo/
    /// Given up to k projects, choose feasible projects to maximize final capital.
    /// 給定最多 k 個專案，每次僅選目前可啟動的專案以最大化最終資金。
    /// </summary>
    private static void Main()
    {
        CaseDefinition[] cases =
        {
            new("Official example 1", 2, 0, new[] { 1, 2, 3 }, new[] { 0, 1, 1 }, 4),
            new("Official example 2", 3, 0, new[] { 1, 2, 3 }, new[] { 0, 1, 2 }, 6),
            new("Single affordable project", 1, 0, new[] { 1 }, new[] { 0 }, 1),
            new("No affordable project", 2, 1, new[] { 2, 3 }, new[] { 2, 3 }, 1),
            new("Choose highest affordable profit", 2, 0, new[] { 1, 7, 3 }, new[] { 0, 0, 1 }, 10),
            new("Unlock a later project", 3, 0, new[] { 1, 2, 10 }, new[] { 0, 1, 3 }, 13),
            new("More selections than projects", 5, 0, new[] { 2, 1 }, new[] { 0, 1 }, 3),
            new("Zero-profit projects", 3, 4, new[] { 0, 0 }, new[] { 0, 4 }, 4),
            new(
                "Upper bound n=100000",
                100000,
                0,
                Enumerable.Repeat(1, 100000).ToArray(),
                new int[100000],
                100000,
                "k = 100000, w = 0, profits = all 1 (n = 100000), capital = all 0 (n = 100000)")
        };

        CaseResult[] results = cases
            .Select(testCase => new CaseResult(
                testCase,
                FindMaximizedCapital(testCase.K, testCase.InitialCapital, testCase.Profits, testCase.Capital)))
            .ToArray();

        foreach (CaseResult result in results)
        {
            Console.WriteLine($"Case: {result.Definition.Name}");
            Console.WriteLine($"Input: {result.Definition.FormatInput()}");
            Console.WriteLine($"Expected: {result.Definition.Expected}");
            Console.WriteLine($"Actual: {result.Actual}");
            Console.WriteLine($"Result: {(result.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int passedCount = results.Count(result => result.Passed);
        Console.WriteLine($"Summary: {passedCount}/{results.Length} checks passed.");

        if (passedCount != results.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 依目前資金挑選至多 k 個可啟動專案；最大堆確保每輪取得目前可得的最高利潤。
    /// 有效輸入的 profits 與 capital 長度相同，回傳完成選擇後的最終資金。
    /// </summary>
    public static int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
    {
        Project[] projects = new Project[profits.Length];
        for (int index = 0; index < projects.Length; index++)
        {
            projects[index] = new Project(capital[index], profits[index]);
        }

        Array.Sort(projects, static (left, right) => left.RequiredCapital.CompareTo(right.RequiredCapital));
        PriorityQueue<int, int> affordableProfits = new();
        int nextProject = 0;

        for (int selection = 0; selection < k; selection++)
        {
            // 只把目前資金可啟動的專案放進候選集合，指標永遠只往前移動。
            while (nextProject < projects.Length && projects[nextProject].RequiredCapital <= w)
            {
                int profit = projects[nextProject].Profit;
                affordableProfits.Enqueue(profit, -profit);
                nextProject++;
            }

            // 負 priority 讓堆頂永遠是可啟動專案中的最高利潤。
            if (!affordableProfits.TryDequeue(out int bestProfit, out _))
            {
                break;
            }

            w += bestProfit;
        }

        return w;
    }

    private sealed record Project(int RequiredCapital, int Profit);

    private sealed record CaseDefinition(
        string Name,
        int K,
        int InitialCapital,
        int[] Profits,
        int[] Capital,
        int Expected,
        string? FormattedInput = null)
    {
        /// <summary>
        /// 將案例輸入轉為可讀且可重現的主控台文字。
        /// </summary>
        public string FormatInput() => FormattedInput ??
            $"k = {K}, w = {InitialCapital}, profits = [{string.Join(", ", Profits)}], capital = [{string.Join(", ", Capital)}]";
    }

    private sealed record CaseResult(CaseDefinition Definition, int Actual)
    {
        /// <summary>
        /// 判斷演算法實際結果是否符合案例預期值。
        /// </summary>
        public bool Passed => Actual == Definition.Expected;
    }
}
