namespace leetcode_2870;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 2870. Minimum Number of Operations to Make Array Empty
    /// https://leetcode.com/problems/minimum-number-of-operations-to-make-array-empty/
    /// 2870. 使陣列為空的最少操作次數
    /// https://leetcode.cn/problems/minimum-number-of-operations-to-make-array-empty/
    /// Given a 0-indexed array of positive integers, repeatedly remove two or three equal values and return the minimum operations needed to empty the array, or -1 when it is impossible.
    /// 給定一個由正整數組成、索引從 0 開始的陣列，反覆刪除兩個或三個相等的值，回傳清空陣列所需的最少操作次數；若無法清空則回傳 -1。
    /// </summary>
    private static void Main()
    {
        (string Name, int[] Input, int Expected)[] cases =
        [
            ("Official example 1", [2, 3, 3, 2, 2, 4, 2, 3, 4], 4),
            ("Official example 2", [2, 1, 2, 2, 3, 3], -1),
            ("Minimum pair", [8, 8], 1),
            ("One triple", [7, 7, 7], 1),
            ("Frequency four", [5, 5, 5, 5], 2),
            ("Frequency five", [6, 6, 6, 6, 6], 2),
            ("Frequency six", [9, 9, 9, 9, 9, 9], 2),
            ("Frequency seven remainder regression", [4, 4, 4, 4, 4, 4, 4], 3),
            ("Mixed frequencies", [1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3], 5),
            ("Singleton among removable groups", [1, 1, 1, 2, 2, 3], -1),
            ("Upper-bound frequency", Enumerable.Repeat(42, 100_000).ToArray(), 33_334)
        ];

        Console.WriteLine("LeetCode 2870 acceptance harness");
        Console.WriteLine();

        for (int i = 0; i < cases.Length; i++)
        {
            (string name, int[] input, int expected) = cases[i];
            string inputText = input.Length > 20
                ? $"{input.Length} copies of {input[0]}"
                : FormatSequence(input);

            RunCase(i + 1, name, inputText, input, expected);
        }

        int[] unchangedInput = [2, 2, 2, 3, 3];
        int[] snapshot = [.. unchangedInput];
        _ = MinOperations(unchangedInput);
        bool unchanged = snapshot.SequenceEqual(unchangedInput);

        Console.WriteLine("Case 12: Input remains unchanged");
        Console.WriteLine($"Input: nums = {FormatSequence(snapshot)}");
        RecordCheck(unchanged);
        Console.WriteLine($"{(unchanged ? "PASS" : "FAIL")} | Input sequence | Expected: {FormatSequence(snapshot)} | Actual: {FormatSequence(unchangedInput)}");
        Console.WriteLine();

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 統計有效正整數陣列中各數值的出現次數，並以每次移除兩個或三個相同值的方式計算清空陣列所需的最少操作次數；若任一數值只出現一次則回傳 -1。
    /// </summary>
    public static int MinOperations(int[] nums)
    {
        Dictionary<int, int> frequencies = [];

        foreach (int value in nums)
        {
            frequencies.TryGetValue(value, out int frequency);
            frequencies[value] = frequency + 1;
        }

        int operations = 0;

        foreach (int frequency in frequencies.Values)
        {
            if (frequency == 1)
            {
                return -1;
            }

            // 優先使用三個一組可減少操作數；餘數為 1 或 2 時都需要再補一次兩個一組的操作。
            operations += frequency / 3;

            if (frequency % 3 != 0)
            {
                operations++;
            }
        }

        return operations;
    }

    private static void RunCase(int number, string name, string inputText, int[] input, int expected)
    {
        int actual = MinOperations(input);
        bool passed = expected == actual;

        Console.WriteLine($"Case {number}: {name}");
        Console.WriteLine($"Input: nums = {inputText}");
        RecordCheck(passed);
        Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | Minimum operations | Expected: {expected} | Actual: {actual}");
        Console.WriteLine();
    }

    private static void RecordCheck(bool passed)
    {
        s_checks++;

        if (passed)
        {
            s_passed++;
        }
    }

    private static string FormatSequence(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }
}