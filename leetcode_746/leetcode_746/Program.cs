namespace leetcode_746;

internal static class Program
{
    /// <summary>
    /// 746. Min Cost Climbing Stairs
    /// https://leetcode.com/problems/min-cost-climbing-stairs/
    /// 746. 使用最小花費爬樓梯
    /// https://leetcode.cn/problems/min-cost-climbing-stairs/
    /// English: Given the cost of stepping on each stair, return the minimum total cost
    /// required to move beyond the last stair when each move climbs one or two steps and
    /// the climb may start at index 0 or index 1.
    /// 中文：給定踏上每一階所需的費用，每次可向上爬一階或兩階，且可從索引 0 或
    /// 索引 1 開始；請回傳越過最後一階、抵達樓梯頂端所需的最低總花費。
    /// </summary>
    /// <param name="args">命令列參數；本驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        int[] upperBoundInput = Enumerable.Repeat(999, 1_000).ToArray();

        (string CaseName, int[] Input, string InputDisplay, int Expected)[] testCases =
        {
            ("Case 1: Official example", new[] { 10, 15, 20 }, "[10, 15, 20]", 15),
            (
                "Case 2: Official longer example",
                new[] { 1, 100, 1, 1, 1, 100, 1, 1, 100, 1 },
                "[1, 100, 1, 1, 1, 100, 1, 1, 100, 1]",
                6),
            ("Case 3: Minimum length with zero costs", new[] { 0, 0 }, "[0, 0]", 0),
            ("Case 4: Starting at index 1 is optimal", new[] { 999, 0 }, "[999, 0]", 0),
            ("Case 5: Paying the final stair is optimal", new[] { 1, 100, 1 }, "[1, 100, 1]", 2),
            ("Case 6: Multi-step regression", new[] { 10, 1, 1, 10, 1 }, "[10, 1, 1, 10, 1]", 3),
            ("Case 7: All zero costs", new[] { 0, 0, 0, 0 }, "[0, 0, 0, 0]", 0),
            (
                "Case 8: Upper-bound spot check",
                upperBoundInput,
                "1,000 steps (all 999)",
                499_500)
        };

        int passedCount = 0;
        int checkCount = 0;

        Console.WriteLine("LeetCode 746 acceptance harness");

        foreach ((string caseName, int[] input, string inputDisplay, int expected) in testCases)
        {
            int[] dynamicProgrammingInput = (int[])input.Clone();
            int[] rollingStateInput = (int[])input.Clone();

            int dynamicProgrammingActual = MinCostClimbingStairs(dynamicProgrammingInput);
            int rollingStateActual = MinCostClimbingStairs2(rollingStateInput);

            (string CheckName, string Expected, string Actual, bool Passed)[] checks =
            {
                (
                    "DP array result",
                    expected.ToString(),
                    dynamicProgrammingActual.ToString(),
                    dynamicProgrammingActual == expected),
                (
                    "DP array input preserved",
                    "True",
                    dynamicProgrammingInput.SequenceEqual(input).ToString(),
                    dynamicProgrammingInput.SequenceEqual(input)),
                (
                    "Rolling-state result",
                    expected.ToString(),
                    rollingStateActual.ToString(),
                    rollingStateActual == expected),
                (
                    "Rolling-state input preserved",
                    "True",
                    rollingStateInput.SequenceEqual(input).ToString(),
                    rollingStateInput.SequenceEqual(input))
            };

            Console.WriteLine();
            Console.WriteLine(caseName);
            Console.WriteLine($"Input: {inputDisplay}");

            foreach ((string checkName, string expectedText, string actualText, bool passed) in checks)
            {
                string result = passed ? "PASS" : "FAIL";
                Console.WriteLine($"{result} | {checkName} | Expected: {expectedText} | Actual: {actualText}");

                checkCount++;

                if (passed)
                {
                    passedCount++;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{checkCount} checks passed.");

        if (passedCount != checkCount)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用動態規劃陣列計算抵達樓梯頂端的最低花費。有效輸入須符合題目限制：
    /// 至少有兩階且每階費用介於 0 到 999；可從索引 0 或 1 開始，每次向上爬一階
    /// 或兩階。回傳越過最後一階所需的最低總花費，不會修改輸入或寫入主控台。
    /// </summary>
    /// <param name="cost">依階梯索引排列的非負費用。</param>
    /// <returns>抵達索引 <c>cost.Length</c> 所代表樓梯頂端的最低總花費。</returns>
    public static int MinCostClimbingStairs(int[] cost)
    {
        int[] minimumCosts = new int[cost.Length + 1];

        for (int step = 2; step <= cost.Length; step++)
        {
            // 抵達目前位置前，只可能從前一階或前兩階支付費用後走上來。
            minimumCosts[step] = Math.Min(
                minimumCosts[step - 1] + cost[step - 1],
                minimumCosts[step - 2] + cost[step - 2]);
        }

        return minimumCosts[cost.Length];
    }

    /// <summary>
    /// 使用兩個滾動狀態計算抵達樓梯頂端的最低花費。有效輸入須符合題目限制：
    /// 至少有兩階且每階費用介於 0 到 999；狀態轉移與動態規劃陣列解法相同，
    /// 但只保留前兩個結果。回傳最低總花費，不會修改輸入或寫入主控台。
    /// </summary>
    /// <param name="cost">依階梯索引排列的非負費用。</param>
    /// <returns>越過最後一階、抵達樓梯頂端的最低總花費。</returns>
    public static int MinCostClimbingStairs2(int[] cost)
    {
        int twoStepsBefore = 0;
        int oneStepBefore = 0;

        for (int step = 2; step <= cost.Length; step++)
        {
            int current = Math.Min(
                oneStepBefore + cost[step - 1],
                twoStepsBefore + cost[step - 2]);

            // 狀態往前滑動後，下一輪仍只需要目前位置與前一位置的最低花費。
            twoStepsBefore = oneStepBefore;
            oneStepBefore = current;
        }

        return oneStepBefore;
    }
}