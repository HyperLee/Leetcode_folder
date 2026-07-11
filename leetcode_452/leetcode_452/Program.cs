namespace leetcode_452;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 452. Minimum Number of Arrows to Burst Balloons
    /// 452. 用最少數量的箭引爆氣球
    /// https://leetcode.com/problems/minimum-number-of-arrows-to-burst-balloons/
    /// https://leetcode.cn/problems/minimum-number-of-arrows-to-burst-balloons/
    /// Given balloon horizontal intervals, return the minimum number of vertical arrows needed to burst every balloon.
    /// 給定每個氣球的水平區間，回傳引爆全部氣球所需的最少垂直箭數。
    /// </summary>
    private static void Main()
    {
        (string Name, int[][] Points, string InputDescription, int Expected)[] cases =
        [
            ("Official example 1", [[10, 16], [2, 8], [1, 6], [7, 12]], "[[10, 16], [2, 8], [1, 6], [7, 12]]", 2),
            ("Official example 2", [[1, 2], [3, 4], [5, 6], [7, 8]], "[[1, 2], [3, 4], [5, 6], [7, 8]]", 4),
            ("Official example 3: touching endpoints", [[1, 2], [2, 3], [3, 4], [4, 5]], "[[1, 2], [2, 3], [3, 4], [4, 5]]", 2),
            ("Single interval at coordinate limits", [[int.MinValue, int.MaxValue]], "[[int.MinValue, int.MaxValue]]", 1),
            ("Endpoint chain has no global overlap", [[int.MinValue, -1], [-1, 0], [0, int.MaxValue]], "[[int.MinValue, -1], [-1, 0], [0, int.MaxValue]]", 2),
            ("Nested interval cannot replace short chain", [[1, 10], [2, 3], [4, 5]], "[[1, 10], [2, 3], [4, 5]]", 2),
            ("Maximum identical intervals", CreateIdenticalPoints(), "100000 identical [int.MinValue, int.MaxValue] intervals", 1),
            ("Maximum disjoint intervals", CreateDisjointPoints(), "100000 disjoint intervals [2i, 2i + 1]", 100000)
        ];

        Console.WriteLine("LeetCode 452 acceptance harness");
        Console.WriteLine();

        foreach ((string name, int[][] points, string inputDescription, int expected) in cases)
        {
            int actual = FindMinArrowShots(ClonePoints(points));
            bool passed = actual == expected;

            Console.WriteLine($"Case {s_checks + 1}: {name}");
            Console.WriteLine($"Input: {inputDescription}");
            RecordCheck(passed);
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | Minimum arrows | Expected: {expected} | Actual: {actual}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 依每個氣球的右端點遞增排序，將箭固定射在尚未引爆群組的最早右端點；輸入必須符合 LeetCode 的非空二元素區間契約，方法會就地排序並回傳引爆全部氣球所需的最少箭數。
    /// </summary>
    public static int FindMinArrowShots(int[][] points)
    {
        Array.Sort(points, static (left, right) => left[1].CompareTo(right[1]));

        int arrows = 1;
        int arrowPosition = points[0][1];

        for (int i = 1; i < points.Length; i++)
        {
            // 起點等於箭的位置仍能命中同一端點；只有嚴格在右方才需要新箭。
            if (points[i][0] > arrowPosition)
            {
                arrows++;
                arrowPosition = points[i][1];
            }
        }

        return arrows;
    }

    /// <summary>
    /// 深複製每個二元素區間，讓會就地排序的公開解法不會改變驗證案例的原始資料。
    /// </summary>
    private static int[][] ClonePoints(int[][] points)
    {
        int[][] clone = new int[points.Length][];

        for (int i = 0; i < points.Length; i++)
        {
            clone[i] = [points[i][0], points[i][1]];
        }

        return clone;
    }

    /// <summary>
    /// 建立題目長度上限的完全重疊極值區間，用於驗證所有氣球能由同一支箭引爆。
    /// </summary>
    private static int[][] CreateIdenticalPoints()
    {
        int[][] points = new int[100000][];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = [int.MinValue, int.MaxValue];
        }

        return points;
    }

    /// <summary>
    /// 建立題目長度上限的兩兩互斥區間，用於驗證每個區間都必須新增一支箭的上界。
    /// </summary>
    private static int[][] CreateDisjointPoints()
    {
        int[][] points = new int[100000][];

        for (int i = 0; i < points.Length; i++)
        {
            int start = i * 2;
            points[i] = [start, start + 1];
        }

        return points;
    }

    /// <summary>
    /// 記錄單一驗證結果，更新總檢查數與通過數，供 Main 輸出最終摘要。
    /// </summary>
    private static void RecordCheck(bool passed)
    {
        s_checks++;

        if (passed)
        {
            s_passed++;
        }
    }
}
