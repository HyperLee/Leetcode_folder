namespace leetcode_875;

internal static class Program
{
    /// <summary>
    /// 875. Koko Eating Bananas
    /// https://leetcode.com/problems/koko-eating-bananas/
    /// 875. 愛吃香蕉的珂珂
    /// https://leetcode.cn/problems/koko-eating-bananas/
    /// Find the minimum integer eating speed that lets Koko finish every banana pile within h hours.
    /// 找出能讓珂珂在 h 小時內吃完所有香蕉堆的最小整數速度。
    /// </summary>
    private static void Main()
    {
        int[] maximumPileCount = Enumerable.Repeat(1_000_000_000, 10_000).ToArray();

        (string Name, int[] Piles, int Hours, int Expected, string InputDescription)[] cases =
        [
            ("Official example 1", [3, 6, 7, 11], 8, 4, "piles = [3,6,7,11], h = 8"),
            ("Official example 2", [30, 11, 23, 4, 20], 5, 30, "piles = [30,11,23,4,20], h = 5"),
            ("Official example 3", [30, 11, 23, 4, 20], 6, 23, "piles = [30,11,23,4,20], h = 6"),
            ("Single pile", [1], 1, 1, "piles = [1], h = 1"),
            ("Many available hours", [1, 1, 1, 1], 1_000_000_000, 1, "piles = [1,1,1,1], h = 1000000000"),
            ("One hour per pile", [2, 8, 4], 3, 8, "piles = [2,8,4], h = 3"),
            ("Ceiling division boundary", [5, 5], 3, 5, "piles = [5,5], h = 3"),
            ("Overflow regression", [1, 1, 1, 1_000_000_000], 1_000_000_000, 2, "piles = [1,1,1,1000000000], h = 1000000000"),
            ("Maximum constraints", maximumPileCount, 1_000_000_000, 10_000, "10,000 piles, each 1000000000; h = 1000000000")
        ];

        int passedCount = 0;
        Console.WriteLine("LeetCode 875 acceptance harness");
        Console.WriteLine();

        for (int i = 0; i < cases.Length; i++)
        {
            (string name, int[] piles, int hours, int expected, string inputDescription) = cases[i];
            int actual = MinEatingSpeed(piles, hours);
            bool passed = expected == actual;

            if (passed)
            {
                passedCount++;
            }

            Console.WriteLine($"Case {i + 1}: {name}");
            Console.WriteLine($"Input: {inputDescription}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine(passed ? "PASS" : "FAIL");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passedCount}/{cases.Length} checks passed.");

        if (passedCount != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 在非空且每堆香蕉數量皆為正整數、h 不小於香蕉堆數的有效輸入下，
    /// 以答案範圍的下界二分搜尋找出能在 h 小時內吃完所有香蕉的最小整數速度，
    /// 並回傳該速度。
    /// </summary>
    public static int MinEatingSpeed(int[] piles, int h)
    {
        int low = 1;
        int high = piles.Max();

        while (low < high)
        {
            int speed = low + ((high - low) / 2);
            long requiredHours = CalculateRequiredHours(piles, speed);

            if (requiredHours <= h)
            {
                // 目前速度可行，保留它並繼續在左半段尋找更小的可行速度。
                high = speed;
            }
            else
            {
                // 目前速度不可行，答案必定在更快的右半段。
                low = speed + 1;
            }
        }

        return low;
    }

    /// <summary>
    /// 在香蕉堆與速度皆為正整數的有效輸入下，以整數上取整逐堆計算指定速度所需時數，
    /// 使用 long 累加避免總時數溢位，並回傳吃完全部香蕉所需的總時數。
    /// </summary>
    private static long CalculateRequiredHours(int[] piles, int speed)
    {
        long requiredHours = 0;

        foreach (int pile in piles)
        {
            // 先提升為 long，再以整數公式完成上取整，避免 pile + speed - 1 溢位。
            requiredHours += ((long)pile + speed - 1) / speed;
        }

        return requiredHours;
    }
}
