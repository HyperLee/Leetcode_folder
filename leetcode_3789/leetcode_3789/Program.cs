namespace leetcode_3789;

class Program
{
    /// <summary>
    /// 3789. Minimum Cost to Acquire Required Items
    /// https://leetcode.com/problems/minimum-cost-to-acquire-required-items/description/
    /// 3789. 采购的最小花费
    /// https://leetcode.cn/problems/minimum-cost-to-acquire-required-items/description/
    /// English
    /// You are given five integers cost1, cost2, costBoth, need1, and need2.
    /// There are three types of items available:
    /// 
    /// An item of type 1 costs cost1 and contributes 1 unit to the type 1 requirement only.
    /// An item of type 2 costs cost2 and contributes 1 unit to the type 2 requirement only.
    /// An item of type 3 costs costBoth and contributes 1 unit to both type 1 and type 2 requirements.
    /// 
    /// You must collect enough items so that the total contribution toward type 1 is at least need1 and the total contribution toward type 2 is at least need2.
    /// Return an integer representing the minimum possible total cost to achieve these requirements.
    /// 繁體中文
    /// 給定五個整數 cost1、cost2、costBoth、need1 和 need2。
    /// 有三種物品可供購買：
    /// 
    /// 每件第 1 類物品的成本為 cost1，僅為第 1 類需求貢獻 1 個單位。
    /// 每件第 2 類物品的成本為 cost2，僅為第 2 類需求貢獻 1 個單位。
    /// 每件第 3 類物品的成本為 costBoth，同時為第 1 類和第 2 類需求各貢獻 1 個單位。
    /// 
    /// 你必須購買足夠的物品，使第 1 類需求的總貢獻量至少為 need1，且第 2 類需求的總貢獻量至少為 need2。
    /// 回傳一個整數，代表滿足這兩項需求所需的最低總成本。
    /// </summary>
    /// <remarks>以固定測資分別驗證兩種解法。全部通過時結束碼為 0，任一解法的任一案例失敗時為 1。</remarks>
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
            ("第 1 類剩餘需求買單品", 3, 6, 5, 4, 1, 14),
            ("第 2 類需求為零，組合包較便宜", 4, 5, 2, 3, 0, 6),
            ("組合包與剩餘單品同價", 6, 3, 3, 1, 4, 12),
        ];

        int passedCount1 = 0;
        int passedCount2 = 0;
        foreach (var testCase in testCases)
        {
            long actual = solution.MinimumCost(
                testCase.Cost1, testCase.Cost2, testCase.CostBoth, testCase.Need1, testCase.Need2);
            long actual2 = solution.MinimumCost2(
                testCase.Cost1, testCase.Cost2, testCase.CostBoth, testCase.Need1, testCase.Need2);
            bool passed = actual == testCase.Expected;
            bool passed2 = actual2 == testCase.Expected;
            if (passed)
            {
                passedCount1++;
            }

            if (passed2)
            {
                passedCount2++;
            }

            Console.WriteLine(
                $"[{(passed ? "PASS" : "FAIL")}] 解法一 | {testCase.Name} | 預期 {testCase.Expected} | 實際 {actual}");
            Console.WriteLine(
                $"[{(passed2 ? "PASS" : "FAIL")}] 解法二 | {testCase.Name} | 預期 {testCase.Expected} | 實際 {actual2}");
        }

        Console.WriteLine($"解法一通過 {passedCount1}/{testCases.Length} 筆測試");
        Console.WriteLine($"解法二通過 {passedCount2}/{testCases.Length} 筆測試");
        Environment.ExitCode = passedCount1 == testCases.Length && passedCount2 == testCases.Length ? 0 : 1;
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

    /// <summary>
    /// 解法二：比較全部單品、共同需求買組合包及全部組合包三種方案，取最低成本。
    /// 時間複雜度與額外空間複雜度皆為 O(1)。
    /// </summary>
    /// <remarks>
    /// 固定組合包數量 x 後，成本為 x * costBoth + max(need1 - x, 0) * cost1
    /// + max(need2 - x, 0) * cost2。在 0 到最小需求、最小需求到最大需求兩個區間內，
    /// 成本皆為線性變化，因此只需檢查端點 0、最小需求及最大需求。
    /// 超過最大需求後，兩類皆已達標，繼續購買只會增加成本。
    /// </remarks>
    /// <param name="cost1">第 1 類物品的單價，介於 1 到 1,000,000。</param>
    /// <param name="cost2">第 2 類物品的單價，介於 1 到 1,000,000。</param>
    /// <param name="costBoth">第 3 類物品的單價，介於 1 到 1,000,000。</param>
    /// <param name="need1">第 1 類需求的最低數量，介於 0 到 1,000,000,000。</param>
    /// <param name="need2">第 2 類需求的最低數量，介於 0 到 1,000,000,000。</param>
    /// <returns>以 <see cref="long"/> 表示的最低總成本。</returns>
    public long MinimumCost2(int cost1, int cost2, int costBoth, int need1, int need2)
    {
        long paired = Math.Min(need1, need2);
        long maxNeed = Math.Max(need1, need2);

        // 方案一：不買組合包，兩類需求全部用單品滿足。
        long separate = 1L * need1 * cost1 + 1L * need2 * cost2;

        // 方案二：買 paired 件組合包滿足共同需求，剩餘需求用對應單品補足。
        long mixed = paired * costBoth
                   + (need1 - paired) * cost1
                   + (need2 - paired) * cost2;

        // 方案三：買 maxNeed 件組合包，允許較小需求的那一類超額供應。
        long allBoth = maxNeed * costBoth;

        return Math.Min(separate, Math.Min(mixed, allBoth));
    }
}
