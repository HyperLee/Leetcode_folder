namespace leetcode_1232;

internal static class Program
{
    /// <summary>
    /// 1232. Check If It Is a Straight Line
    /// https://leetcode.com/problems/check-if-it-is-a-straight-line/
    /// 1232. 綴點成線
    /// https://leetcode.cn/problems/check-if-it-is-a-straight-line/
    /// English: Given distinct points on a two-dimensional plane, determine whether all points lie on the same straight line.
    /// 中文：給定二維平面上的不同座標點，判斷所有點是否位於同一直線上。
    /// </summary>
    /// <param name="args">命令列參數；驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, int[][] Coordinates, bool Expected)[] testCases =
        {
            ("Case 1: Official true", new[] { new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 }, new[] { 4, 5 }, new[] { 5, 6 }, new[] { 6, 7 } }, true),
            ("Case 2: Official false", new[] { new[] { 1, 1 }, new[] { 2, 2 }, new[] { 3, 4 }, new[] { 4, 5 }, new[] { 5, 6 }, new[] { 7, 7 } }, false),
            ("Case 3: Minimum input", new[] { new[] { 0, 0 }, new[] { 3, -7 } }, true),
            ("Case 4: Vertical line", new[] { new[] { 2, -5 }, new[] { 2, 0 }, new[] { 2, 8 }, new[] { 2, 10_000 } }, true),
            ("Case 5: Horizontal line", new[] { new[] { -10_000, 7 }, new[] { 0, 7 }, new[] { 10_000, 7 } }, true),
            ("Case 6: Negative slope", new[] { new[] { -3, 3 }, new[] { -1, 1 }, new[] { 1, -1 }, new[] { 3, -3 } }, true),
            ("Case 7: Late deviation", new[] { new[] { 0, 0 }, new[] { 1, 1 }, new[] { 2, 2 }, new[] { 3, 3 }, new[] { 4, 5 } }, false)
        };

        List<CaseResult> checks = new();

        foreach ((string caseName, int[][] coordinates, bool expected) in testCases)
        {
            bool actual = CheckStraightLine(coordinates);
            checks.Add(new CaseResult(caseName, FormatCoordinates(coordinates), expected, actual, expected == actual));
        }

        int[][] maximumCoordinates = new int[1_000][];

        for (int index = 0; index < maximumCoordinates.Length; index++)
        {
            int x = index - 500;
            maximumCoordinates[index] = new[] { x, (2 * x) + 1 };
        }

        bool maximumActual = CheckStraightLine(maximumCoordinates);
        checks.Add(new CaseResult(
            "Case 8: Maximum length",
            "1000 distinct points: x = -500..499, y = 2*x + 1",
            true,
            maximumActual,
            maximumActual));

        int passedCount = 0;
        Console.WriteLine("LeetCode 1232 acceptance harness");

        foreach (CaseResult check in checks)
        {
            Console.WriteLine();
            Console.WriteLine(check.CaseName);
            Console.WriteLine($"Input: {check.Input}");
            Console.WriteLine($"Expected: {check.Expected.ToString().ToLowerInvariant()}");
            Console.WriteLine($"Actual: {check.Actual.ToString().ToLowerInvariant()}");
            Console.WriteLine(check.Passed ? "PASS" : "FAIL");

            if (check.Passed)
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
    /// 以首兩點建立基準向量，透過長整數叉積判斷所有後續座標是否共線。方法不修改輸入，也不寫入主控台。
    /// </summary>
    /// <param name="coordinates">題目保證有效且至少包含兩個不同座標的二維整數陣列。</param>
    /// <returns>所有座標位於同一直線上時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    public static bool CheckStraightLine(int[][] coordinates)
    {
        long baselineX = coordinates[1][0] - coordinates[0][0];
        long baselineY = coordinates[1][1] - coordinates[0][1];

        for (int index = 2; index < coordinates.Length; index++)
        {
            long currentX = coordinates[index][0] - coordinates[0][0];
            long currentY = coordinates[index][1] - coordinates[0][1];

            // 叉積為零代表目前向量與基準向量平行，因此三點保持共線。
            if ((baselineX * currentY) != (baselineY * currentX))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 將座標陣列格式化為簡潔且可重現的驗收輸入文字；不修改原始座標。
    /// </summary>
    /// <param name="coordinates">要顯示的二維座標。</param>
    /// <returns>以方括號表示的座標清單。</returns>
    private static string FormatCoordinates(IEnumerable<int[]> coordinates)
    {
        return $"[{string.Join(", ", coordinates.Select(point => $"[{point[0]}, {point[1]}]"))}]";
    }

    private readonly record struct CaseResult(string CaseName, string Input, bool Expected, bool Actual, bool Passed);
}
