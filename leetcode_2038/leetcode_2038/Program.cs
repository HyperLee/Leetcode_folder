namespace leetcode_2038;

internal static class Program
{
    /// <summary>
    /// LeetCode 2038. Remove Colored Pieces if Both Neighbors are the Same Color.
    /// LeetCode 2038. 如果相鄰兩個顏色均相同則刪除當前顏色。
    /// English: https://leetcode.com/problems/remove-colored-pieces-if-both-neighbors-are-the-same-color/
    /// 中文：https://leetcode.cn/problems/remove-colored-pieces-if-both-neighbors-are-the-same-color/
    /// English: Given a line of pieces colored 'A' or 'B', Alice and Bob alternately
    /// remove only their own color when both neighboring pieces have that same color.
    /// Alice moves first, edge pieces cannot be removed, and a player who cannot move
    /// loses. Return whether Alice wins when both players play optimally.
    /// 中文：給定一列只含 'A' 與 'B' 的色塊，Alice 與 Bob 輪流移除自己的顏色色塊，
    /// 且該色塊左右相鄰色塊都必須同色。Alice 先手，兩端色塊不可移除；無法行動者
    /// 落敗。請判斷雙方皆採最佳策略時 Alice 是否獲勝。
    /// </summary>
    /// <remarks>
    /// 刪除相鄰的顏色A, B
    /// 1. 頭尾不能刪除
    /// 2. a, b 輪流刪除
    /// 3. 不能刪除時候結束
    /// </remarks>
    private static void Main()
    {
        string maximumInput = new('A', 100_000);
        TestCase[] cases =
        [
            new("Official example 1", "AAABABB", true),
            new("Official example 2", "AA", false),
            new("Official example 3", "ABBBBBBBAAA", false),
            new("Minimum input", "A", false),
            new("Alice-only long run", "AAAAAA", true),
            new("Bob-only long run", "BBBBBB", false),
            new("Equal move counts", "AAABBB", false),
            new("Multiple runs aggregate", "AAAABBAAABBBB", true),
            new("Overlapping removals regression", "AAAABBB", true),
            new("Maximum-length input", maximumInput, true)
        ];

        Console.WriteLine("LeetCode 2038 Acceptance Harness");

        int passedChecks = 0;
        foreach (TestCase testCase in cases)
        {
            bool actual = WinnerOfGame(testCase.Colors);

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: colors = {FormatColors(testCase.Colors)}");

            CheckResult result = EvaluateCheck(
                "WinnerOfGame result",
                testCase.Expected,
                actual);
            Console.WriteLine(result.Output);
            passedChecks += result.Passed ? 1 : 0;
            Console.WriteLine();
        }

        const int totalChecks = 10;
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 對長度介於 1 至 100,000 且只含 'A'、'B' 的有效
    /// <paramref name="colors" />，以單趟連續段計數分別累積 Alice 與 Bob 可移除的
    /// 色塊數。每個長度為 L 的同色連續段提供 max(0, L - 2) 次操作；Alice 先手，
    /// 因此只有操作數嚴格多於 Bob 時才會獲勝。此純函式不修改輸入也不輸出主控台，
    /// 時間複雜度為 O(n)，輔助空間為 O(1)。
    /// </summary>
    public static bool WinnerOfGame(string colors)
    {
        int aliceMoves = 0;
        int bobMoves = 0;
        char currentColor = colors[0];
        int runLength = 0;

        foreach (char color in colors)
        {
            if (color != currentColor)
            {
                currentColor = color;
                runLength = 0;
            }

            runLength++;
            // 同色段長度每超過兩端保留色塊一格，就新增一次互不干擾的可移除機會。
            if (runLength < 3)
            {
                continue;
            }

            if (currentColor == 'A')
            {
                aliceMoves++;
            }
            else
            {
                bobMoves++;
            }
        }

        // Alice 先手；平手時 Alice 會先無法行動，因此必須嚴格大於 Bob。
        return aliceMoves > bobMoves;
    }

    private static CheckResult EvaluateCheck(string checkName, bool expected, bool actual)
    {
        bool passed = expected == actual;
        string output =
            $"{(passed ? "PASS" : "FAIL")} {checkName} | " +
            $"Expected: {expected} | Actual: {actual}";
        return new CheckResult(passed, output);
    }

    private static string FormatColors(string colors)
    {
        const int visibleCharactersPerSide = 16;
        if (colors.Length <= visibleCharactersPerSide * 2)
        {
            return $"\"{colors}\"";
        }

        return
            $"\"{colors[..visibleCharactersPerSide]}..." +
            $"{colors[^visibleCharactersPerSide..]}\" (length: {colors.Length})";
    }

    private sealed record TestCase(string Name, string Colors, bool Expected);

    private sealed record CheckResult(bool Passed, string Output);
}