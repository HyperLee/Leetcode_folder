namespace leetcode_2187;

internal static class Program
{
    /// <summary>
    /// LeetCode 2187. Minimum Time to Complete Trips.
    /// LeetCode 2187. 完成旅途的最少時間。
    /// English: Given an array where each value is the duration of one trip, return the minimum
    /// time needed for all buses to complete at least totalTrips trips.
    /// 中文：給定每趟旅途所需時間的陣列，回傳所有巴士至少完成 totalTrips 趟旅途所需的最少時間。
    /// English: https://leetcode.com/problems/minimum-time-to-complete-trips/
    /// 中文：https://leetcode.cn/problems/minimum-time-to-complete-trips/
    /// </summary>
    private static void Main()
    {
        int[] almostAllFastBuses = [.. Enumerable.Repeat(1, 99_999), 10_000_000];
        int[] allSlowBuses = Enumerable.Repeat(10_000_000, 100_000).ToArray();
        TestCase[] testCases =
        [
            new("Official example", "time=[1, 2, 3], totalTrips=5", [1, 2, 3], 5, 3),
            new("Single bus", "time=[2], totalTrips=1", [2], 1, 2),
            new("Minimum valid input", "time=[1], totalTrips=1", [1], 1, 1),
            new("Shared slowest upper bound", "time=[5, 10, 10], totalTrips=9", [5, 10, 10], 9, 25),
            new("Unsorted durations", "time=[5, 1, 3], totalTrips=5", [5, 1, 3], 5, 4),
            new("Exact combined capacity", "time=[2, 3], totalTrips=5", [2, 3], 5, 6),
            new("Maximum answer", "time=[10_000_000], totalTrips=10_000_000", [10_000_000], 10_000_000, 100_000_000_000_000),
            new("Large-accumulation early-stop guard", "time=[1 x 99,999, 10,000,000], totalTrips=10,000,000", almostAllFastBuses, 10_000_000, 101),
            new("Maximum bus count", "time=[10,000,000 x 100,000], totalTrips=10,000,000", allSlowBuses, 10_000_000, 1_000_000_000)
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck("MinimumTime result", result.Expected, result.MinimumTimeActual));
            Console.WriteLine(PrintCheck("MinimumTime input preserved", true, result.MinimumTimeInputPreserved));
            Console.WriteLine(PrintCheck("MinimumTime2 result", result.Expected, result.MinimumTime2Actual));
            Console.WriteLine(PrintCheck("MinimumTime2 input preserved", true, result.MinimumTime2InputPreserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 36;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string PrintCheck<T>(string checkName, T expected, T actual)
    {
        string status = EqualityComparer<T>.Default.Equals(expected, actual) ? "PASS" : "FAIL";
        return $"{status} {checkName} | Expected: {expected} | Actual: {actual}";
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int[] minimumTimeInput = [.. testCase.Time];
        int[] minimumTimeOriginal = [.. minimumTimeInput];
        long minimumTimeActual = MinimumTime(minimumTimeInput, testCase.TotalTrips);
        bool minimumTimeInputPreserved = minimumTimeInput.SequenceEqual(minimumTimeOriginal);

        int[] minimumTime2Input = [.. testCase.Time];
        int[] minimumTime2Original = [.. minimumTime2Input];
        long minimumTime2Actual = MinimumTime2(minimumTime2Input, testCase.TotalTrips);
        bool minimumTime2InputPreserved = minimumTime2Input.SequenceEqual(minimumTime2Original);

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.Expected,
            minimumTimeActual,
            minimumTimeInputPreserved,
            minimumTime2Actual,
            minimumTime2InputPreserved);
    }

    /// <summary>
    /// 對題目保證的有效輸入 <paramref name="time"/> 與 <paramref name="totalTrips"/>，
    /// 以左閉右閉的答案區間尋找至少完成指定旅途數的最少時間。方法只讀取輸入，不輸出主控台，
    /// 回傳介於 1 與最慢單趟時間乘上目標趟數之間的最小可行整數時間。時間複雜度為
    /// O(n log(max(time) * totalTrips))，輔助空間為 O(1)。
    /// </summary>
    /// <param name="time">長度 1 至 100,000、元素 1 至 10,000,000 的每趟旅途時間陣列。</param>
    /// <param name="totalTrips">介於 1 至 10,000,000 的至少完成旅途數。</param>
    /// <returns>完成至少 <paramref name="totalTrips"/> 趟旅途所需的最少時間。</returns>
    public static long MinimumTime(int[] time, int totalTrips)
    {
        long left = 1;
        long right = (long)time.Max() * totalTrips;

        while (left < right)
        {
            long middle = left + ((right - left) / 2);
            // 可行時 middle 仍可能是最小答案，故保留它；不可行才排除左半部。
            if (CanCompleteTrips(time, middle, totalTrips))
            {
                right = middle;
            }
            else
            {
                left = middle + 1;
            }
        }

        return left;
    }

    /// <summary>
    /// 對題目保證的有效輸入 <paramref name="time"/> 與 <paramref name="totalTrips"/>，
    /// 使用含候選答案的左閉右閉二分搜尋，回傳至少完成指定旅途數的最少時間。方法只讀取輸入，
    /// 不輸出主控台；時間複雜度為 O(n log(max(time) * totalTrips))，輔助空間為 O(1)。
    /// </summary>
    /// <param name="time">長度 1 至 100,000、元素 1 至 10,000,000 的每趟旅途時間陣列。</param>
    /// <param name="totalTrips">介於 1 至 10,000,000 的至少完成旅途數。</param>
    /// <returns>完成至少 <paramref name="totalTrips"/> 趟旅途所需的最少時間。</returns>
    public static long MinimumTime2(int[] time, int totalTrips)
    {
        long left = 1;
        long right = (long)time.Max() * totalTrips;
        long candidate = right;

        while (left <= right)
        {
            long middle = left + ((right - left) / 2);
            // 每次可行都記錄候選並繼續搜尋更小時間；不可行時間不可能是答案。
            if (CanCompleteTrips(time, middle, totalTrips))
            {
                candidate = middle;
                right = middle - 1;
            }
            else
            {
                left = middle + 1;
            }
        }

        return candidate;
    }

    /// <summary>
    /// 計算題目有效輸入 <paramref name="time"/> 在 <paramref name="totalTime"/> 內可完成的旅途數
    /// 是否至少為 <paramref name="totalTrips"/>。累計一達目標即回傳，避免在大量巴士與大時間上
    /// 繼續相加造成 long 溢位；方法不修改輸入或主控台狀態。
    /// </summary>
    /// <param name="time">每趟旅途時間的有效正整數陣列。</param>
    /// <param name="totalTime">目前二分搜尋檢查的正整數總時間。</param>
    /// <param name="totalTrips">需達到的正整數旅途目標。</param>
    /// <returns>若可完成至少目標趟數則為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private static bool CanCompleteTrips(int[] time, long totalTime, int totalTrips)
    {
        long completedTrips = 0;
        foreach (int tripTime in time)
        {
            completedTrips += totalTime / tripTime;
            // 達標後不再累加，讓 completedTrips 不會為無用的大量總和而溢位。
            if (completedTrips >= totalTrips)
            {
                return true;
            }
        }

        return false;
    }

    private sealed record TestCase(
        string Name,
        string Input,
        int[] Time,
        int TotalTrips,
        long Expected);

    private sealed record CaseResult(
        string Name,
        string Input,
        long Expected,
        long MinimumTimeActual,
        bool MinimumTimeInputPreserved,
        long MinimumTime2Actual,
        bool MinimumTime2InputPreserved)
    {
        public int PassedCheckCount =>
            (MinimumTimeActual == Expected ? 1 : 0) +
            (MinimumTimeInputPreserved ? 1 : 0) +
            (MinimumTime2Actual == Expected ? 1 : 0) +
            (MinimumTime2InputPreserved ? 1 : 0);
    }
}