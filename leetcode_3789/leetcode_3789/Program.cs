namespace leetcode_3789;

class Program
{
    /// <summary>
    /// 3789. Minimum Cost to Acquire Required Items
    /// https://leetcode.com/problems/minimum-cost-to-acquire-required-items/description/
    /// 3789. 采购的最小花费
    /// https://leetcode.cn/problems/minimum-cost-to-acquire-required-items/description/
    /// <para>English</para>
    /// <para>You are given five integers <c>cost1</c>, <c>cost2</c>, <c>costBoth</c>, <c>need1</c>, and <c>need2</c>.</para>
    /// <para>There are three types of items available:</para>
    /// <list type="bullet">
    /// <item><description>An item of <b>type 1</b> costs <c>cost1</c> and contributes 1 unit to the type 1 requirement only.</description></item>
    /// <item><description>An item of <b>type 2</b> costs <c>cost2</c> and contributes 1 unit to the type 2 requirement only.</description></item>
    /// <item><description>An item of <b>type 3</b> costs <c>costBoth</c> and contributes 1 unit to <b>both</b> type 1 and type 2 requirements.</description></item>
    /// </list>
    /// <para>You must collect enough items so that the total contribution toward type 1 is <b>at least</b> <c>need1</c> and the total contribution toward type 2 is <b>at least</b> <c>need2</c>.</para>
    /// <para>Return an integer representing the <b>minimum</b> possible total cost to achieve these requirements.</para>
    /// <para>繁體中文</para>
    /// <para>給定五個整數 <c>cost1</c>、<c>cost2</c>、<c>costBoth</c>、<c>need1</c> 和 <c>need2</c>。</para>
    /// <para>有三種物品可供購買：</para>
    /// <list type="bullet">
    /// <item><description>每件<b>第 1 類</b>物品的成本為 <c>cost1</c>，僅為第 1 類需求貢獻 1 個單位。</description></item>
    /// <item><description>每件<b>第 2 類</b>物品的成本為 <c>cost2</c>，僅為第 2 類需求貢獻 1 個單位。</description></item>
    /// <item><description>每件<b>第 3 類</b>物品的成本為 <c>costBoth</c>，<b>同時</b>為第 1 類和第 2 類需求各貢獻 1 個單位。</description></item>
    /// </list>
    /// <para>你必須購買足夠的物品，使第 1 類需求的總貢獻量<b>至少</b>為 <c>need1</c>，且第 2 類需求的總貢獻量<b>至少</b>為 <c>need2</c>。</para>
    /// <para>回傳一個整數，代表滿足這兩項需求所需的<b>最低</b>總成本。</para>
    /// </summary>
    /// <remarks>執行固定測資，逐筆比較最低成本與預期值。全部通過時結束碼為 0，任一案例失敗時為 1。</remarks>
    /// <param name="args">命令列參數，此測試入口不使用。</param>
    static void Main(string[] args)
    {
        var solution = new Program();
        (string Name, int Cost1, int Cost2, int CostBoth, int Need1, int Need2, long Expected)[] testCases =
        [
            ("官方範例 1：第 3 類超額供應", 3, 2, 1, 3, 2, 3),
            ("官方範例 2：分開購買", 5, 4, 15, 2, 3, 22),
            ("官方範例 3：兩邊需求皆為零", 5, 4, 15, 0, 0, 0),
            ("第 2 類剩餘需求用單類物品補足", 6, 3, 5, 1, 4, 14),
            ("第 1 類需求為零，第 3 類仍較便宜", 5, 4, 2, 0, 3, 6),
            ("共同需求的兩種買法同價", 3, 2, 5, 2, 3, 12),
            ("兩邊需求相等，配對後無剩餘", 5, 4, 6, 2, 2, 12),
            ("成本與需求達上限", 1_000_000, 1_000_000, 1_000_000, 1_000_000_000, 1_000_000_000, 1_000_000_000_000_000L),
        ];

        int passedCount = 0;
        foreach (var testCase in testCases)
        {
            long actual = solution.MinimumCost(
                testCase.Cost1, testCase.Cost2, testCase.CostBoth, testCase.Need1, testCase.Need2);
            bool passed = actual == testCase.Expected;
            if (passed)
            {
                passedCount++;
            }

            Console.WriteLine(
                $"[{(passed ? "PASS" : "FAIL")}] {testCase.Name} | 預期 {testCase.Expected} | 實際 {actual}");
        }

        Console.WriteLine($"通過 {passedCount}/{testCases.Length} 筆測試");
        Environment.ExitCode = passedCount == testCases.Length ? 0 : 1;
    }

    /// <summary>
    /// 以貪心法計算至少滿足兩類需求的最低成本。先比較共同需求的配對成本，
    /// 再以較便宜的單類或第 3 類物品補足剩餘需求，允許其中一類超額供應。
    /// </summary>
    /// <param name="cost1">第 1 類物品的單價，介於 1 到 1,000,000。</param>
    /// <param name="cost2">第 2 類物品的單價，介於 1 到 1,000,000。</param>
    /// <param name="costBoth">第 3 類物品的單價，介於 1 到 1,000,000。</param>
    /// <param name="need1">第 1 類需求的最低數量，介於 0 到 1,000,000,000。</param>
    /// <param name="need2">第 2 類需求的最低數量，介於 0 到 1,000,000,000。</param>
    /// <returns>以 <see cref="long"/> 表示的最低總成本。</returns>
    public long MinimumCost(int cost1, int cost2, int costBoth, int need1, int need2)
    {
        long totalCost = 0;

        // 若一對單類物品已比第 3 類便宜或同價，分開買也能以最低成本補足單邊需求。
        if (cost1 + cost2 <= costBoth)
        {
            totalCost = 1L * cost1 * need1 + 1L * cost2 * need2;
        }
        else
        {
            // 第 3 類每件同時抵銷兩類各一個需求，最多可配對較小需求量那麼多次。
            int pairedCount = Math.Min(need1, need2);
            totalCost = 1L * pairedCount * costBoth;

            need1 -= pairedCount;
            need2 -= pairedCount;

            // 配對後最多只剩一類有需求。若第 3 類更便宜，可以接受另一類超額供應。
            if (need1 > 0)
            {
                totalCost += 1L * need1 * Math.Min(cost1, costBoth);
            }
            else if (need2 > 0)
            {
                totalCost += 1L * need2 * Math.Min(cost2, costBoth);
            }
        }

        return totalCost;
    }
}
