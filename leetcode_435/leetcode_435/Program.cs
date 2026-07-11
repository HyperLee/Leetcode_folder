namespace leetcode_435;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 435. Non-overlapping Intervals
    /// 435. 無重疊區間
    /// https://leetcode.com/problems/non-overlapping-intervals/
    /// https://leetcode.cn/problems/non-overlapping-intervals/
    /// Given an array of intervals, return the minimum number to remove so the remaining intervals do not overlap.
    /// 給定一組區間，回傳使剩餘區間彼此不重疊所需移除的最小區間數。
    /// </summary>
    private static void Main()
    {
        (string Name, int[][] Intervals, string InputDescription, int Expected)[] cases =
        [
            ("Official example 1", [[1, 2], [2, 3], [3, 4], [1, 3]], "[[1, 2], [2, 3], [3, 4], [1, 3]]", 1),
            ("Official example 2", [[1, 2], [1, 2], [1, 2]], "[[1, 2], [1, 2], [1, 2]]", 2),
            ("Official example 3", [[1, 2], [2, 3]], "[[1, 2], [2, 3]]", 0),
            ("Single interval at coordinate limits", [[-50000, 50000]], "[[-50000, 50000]]", 0),
            ("Greedy keeps the short chain", [[1, 100], [2, 3], [3, 4], [4, 5]], "[[1, 100], [2, 3], [3, 4], [4, 5]]", 1),
            ("Unsorted multiple overlaps", [[1, 100], [11, 22], [1, 11], [2, 12]], "[[1, 100], [11, 22], [1, 11], [2, 12]]", 2),
            ("Maximum adjacent intervals", CreateAdjacentIntervals(), "100000 adjacent intervals from [-50000, -49999] through [49999, 50000]", 0),
            ("Maximum identical intervals", CreateIdenticalIntervals(), "100000 identical [-50000, 50000] intervals", 99999)
        ];

        Console.WriteLine("LeetCode 435 acceptance harness");
        Console.WriteLine();

        foreach ((string name, int[][] intervals, string inputDescription, int expected) in cases)
        {
            int[][] actualInput = CloneIntervals(intervals);
            int actual = EraseOverlapIntervals(actualInput);
            bool passed = actual == expected;

            Console.WriteLine($"Case {s_checks + 1}: {name}");
            Console.WriteLine($"Input: {inputDescription}");
            RecordCheck(passed);
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | Minimum removals | Expected: {expected} | Actual: {actual}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 依區間結束點由早到晚進行貪心選擇；輸入必須符合 LeetCode 的非空二元素區間契約，方法會就地重新排序並回傳使其餘區間不重疊所需移除的最小數量。
    /// </summary>
    public static int EraseOverlapIntervals(int[][] intervals)
    {
        Array.Sort(intervals, static (left, right) => left[1].CompareTo(right[1]));

        int removals = 0;
        int selectedEnd = intervals[0][1];

        for (int i = 1; i < intervals.Length; i++)
        {
            if (intervals[i][0] >= selectedEnd)
            {
                // 接觸同一端點不算重疊；保留這個最早可用結束點以容納後續最多區間。
                selectedEnd = intervals[i][1];
            }
            else
            {
                // 已保留的區間結束得不晚於目前區間，捨棄重疊的目前區間才能維持最佳選擇。
                removals++;
            }
        }

        return removals;
    }

    /// <summary>
    /// 接收符合題目格式的區間陣列，逐一建立新的二元素陣列並回傳深複製結果，讓會就地排序的解法不會改變驗證案例原始資料。
    /// </summary>
    private static int[][] CloneIntervals(int[][] intervals)
    {
        return intervals.Select(static interval => new[] { interval[0], interval[1] }).ToArray();
    }

    /// <summary>
    /// 建立 100000 個首尾相接的有效區間並依序回傳，用於驗證題目長度上限以及端點接觸不構成重疊的不變量。
    /// </summary>
    private static int[][] CreateAdjacentIntervals()
    {
        int[][] intervals = new int[100000][];

        for (int i = 0; i < intervals.Length; i++)
        {
            int start = -50000 + i;
            intervals[i] = [start, start + 1];
        }

        return intervals;
    }

    /// <summary>
    /// 建立 100000 個完全相同的有效區間並回傳，用於驗證每個重疊區間都必須計入移除數的上限案例。
    /// </summary>
    private static int[][] CreateIdenticalIntervals()
    {
        int[][] intervals = new int[100000][];

        for (int i = 0; i < intervals.Length; i++)
        {
            intervals[i] = [-50000, 50000];
        }

        return intervals;
    }

    /// <summary>
    /// 接收單一檢查的通過結果；遞增總檢查數，並在結果為 true 時遞增通過數，讓 Main 能輸出最終驗證摘要。
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
