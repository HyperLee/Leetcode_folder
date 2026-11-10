namespace leetcode_3693;

internal static class SolutionChecks
{
    /// <summary>
    /// 無需輸入，依序執行固定範例、邊界案例及小型陣列的獨立窮舉交叉驗證。
    /// 三種解法共用同一實例，以涵蓋重複呼叫；成功輸出統計，失敗擲出例外。
    /// </summary>
    internal static void Run()
    {
        // 固定預期值可抓出漏算落點費用、跳躍平方或邊界索引等錯誤。
        (string Name, int[] Costs, int Expected)[] cases =
        [
            ("範例一", [1, 2, 3, 4], 13),
            ("範例二", [5, 1, 6, 2], 11),
            ("範例三／三階跳", [9, 8, 3], 12),
            ("單階／最小費用", [1], 2),
            ("單階／最大費用", [10000], 10001),
            ("兩次一階跳", [1, 1], 4),
            ("直接兩階跳", [10000, 1], 5),
            ("三次一階跳", [1, 1, 1], 6),
            ("重複呼叫使用新快取", [1, 1, 10000], 10005),
            ("最大階數／最小費用", Enumerable.Repeat(1, 100000).ToArray(), 200000),
            // 33332 次三階跳加兩次兩階跳：33332 * 10009 + 2 * 10004。
            ("最大階數／最大費用", Enumerable.Repeat(10000, 100000).ToArray(), 333639996)
        ];

        Program solution = new Program();
        int checks = 0;
        foreach ((string name, int[] costs, int expected) in cases)
        {
            CheckSolutions(solution, name, costs, expected);

            checks += 3;
            Console.WriteLine($"PASS {name}：{expected}");
        }

        int exhaustiveCases = 0;
        for (int length = 1; length <= 7; length++)
        {
            exhaustiveCases += CheckArrays(solution, new int[length], 0);
        }

        checks += exhaustiveCases * 3;
        Console.WriteLine($"窮舉驗證通過：{exhaustiveCases} 個案例，{exhaustiveCases * 3} 項檢查。");
        Console.WriteLine($"全部通過：{cases.Length + exhaustiveCases} 個案例，{checks} 項檢查。");
    }

    /// <summary>
    /// 對同一合法輸入呼叫三種解法，逐一比對獨立預期值；無回傳值。
    /// 不一致時擲出含輸入與三個實際結果的例外，讓測試程序明確失敗。
    /// </summary>
    /// <param name="solution">可重複使用的解題實例。</param>
    /// <param name="name">用於定位失敗案例的名稱。</param>
    /// <param name="costs">符合題目限制的落點費用。</param>
    /// <param name="expected">固定答案或獨立路徑窮舉所得的最小費用。</param>
    private static void CheckSolutions(Program solution, string name, int[] costs, int expected)
    {
        int first = solution.ClimbStairs(costs.Length, costs);
        int second = solution.ClimbStairs2(costs.Length, costs);
        int third = solution.ClimbStairs3(costs.Length, costs);
        if (first != expected || second != expected || third != expected)
        {
            throw new InvalidOperationException(
                $"{name}：n={costs.Length}，costs=[{string.Join(", ", costs)}]，" +
                $"預期 {expected}，解法一 {first}，解法二 {second}，解法三 {third}。");
        }
    }

    /// <summary>
    /// 將長度 1 至 7 的陣列未填部分逐格填入 1、2、10，列舉每個可能輸入。
    /// 填滿後以無快取的路徑窮舉取得答案，再核對三種解法。
    /// </summary>
    /// <param name="solution">用來交叉驗證的解題實例。</param>
    /// <param name="costs">目前正在填入的小型陣列。</param>
    /// <param name="index">下一個待填索引，介於 0 與 costs.Length。</param>
    /// <returns>本次分支完成驗證的陣列數量。</returns>
    private static int CheckArrays(Program solution, int[] costs, int index)
    {
        if (index == costs.Length)
        {
            int expected = EnumeratePaths(costs, 0, 0);
            CheckSolutions(solution, "小型陣列窮舉", costs, expected);
            return 1;
        }

        int cases = 0;
        foreach (int cost in new int[] { 1, 2, 10 })
        {
            costs[index] = cost;
            cases += CheckArrays(solution, costs, index + 1);
        }

        return cases;
    }

    /// <summary>
    /// 從目前階梯向前列舉所有合法路徑，累加每次跳躍的實際費用並選出最小值。
    /// 不使用 DP、快取或剪枝，僅供長度不超過 7 的測試輸入作為獨立答案來源。
    /// 時間上界 O(3^n)，遞迴空間 O(n)。
    /// </summary>
    /// <param name="costs">長度 1 至 7、費用符合題目限制的陣列。</param>
    /// <param name="position">目前所在階梯，介於 0 與 costs.Length。</param>
    /// <param name="total">從起點走到目前階梯已支付的費用。</param>
    /// <returns>經過目前路徑前綴抵達終點的最小完整費用。</returns>
    private static int EnumeratePaths(int[] costs, int position, int total)
    {
        if (position == costs.Length)
        {
            return total;
        }

        int best = int.MaxValue;
        for (int jump = 1; jump <= 3 && position + jump <= costs.Length; jump++)
        {
            int landing = position + jump;
            // 從起點向前實際走路徑，避免與受測解法共用「最小前驅」的轉移實作。
            int nextTotal = total + costs[landing - 1] + (jump * jump);
            best = Math.Min(best, EnumeratePaths(costs, landing, nextTotal));
        }

        return best;
    }
}