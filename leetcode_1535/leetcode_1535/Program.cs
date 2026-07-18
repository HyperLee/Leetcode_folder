using System.Globalization;

namespace leetcode_1535;

internal class Program
{
    /// <summary>
    /// 1535. Find the Winner of an Array Game／找出陣列遊戲的贏家。
    /// LeetCode English: https://leetcode.com/problems/find-the-winner-of-an-array-game/
    /// LeetCode 中文：https://leetcode.cn/problems/find-the-winner-of-an-array-game/
    /// Given distinct integers and a required winning streak, return the first integer that wins k consecutive rounds.
    /// 給定相異整數與所需連勝場次，回傳第一個連續贏得 k 場比賽的整數。
    /// </summary>
    private static void Main()
    {
        TestCase[] cases =
        [
            new("Official example 1", [2, 1, 3, 5, 4, 6, 7], 2, 5),
            new("Official example 2", [3, 2, 1], 10, 3),
            new("Official example 3", [1, 9, 8, 2, 3, 7, 6, 4, 5], 7, 9),
            new("Official example 4", [1, 11, 22, 33, 44, 55, 66, 77, 88, 99], 1_000_000_000, 99),
            new("Two elements", [1, 2], 1, 2),
            new("Champion keeps streak", [6, 1, 5, 4, 3, 2], 3, 6),
            new("Value upper bound", [1_000_000, 1], 1, 1_000_000),
            new("Maximum length", Enumerable.Range(1, 100_000).ToArray(), 1_000_000_000, 100_000)
        ];

        int passedCount = 0;

        foreach (TestCase testCase in cases)
        {
            CaseResult result = RunCase(testCase);
            if (result.Passed)
            {
                passedCount++;
            }

            Console.WriteLine($"Case: {result.Name}");
            Console.WriteLine($"Input: {FormatInput(result.Input)}");
            Console.WriteLine($"k: {result.K.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Expected: {result.Expected.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Actual: {result.Actual.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Input preserved: {result.InputPreserved}");
            Console.WriteLine($"Result: {(result.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passedCount}/{cases.Length} checks passed.");

        if (passedCount != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int[] input = (int[])testCase.Input.Clone();
        int[] snapshot = (int[])input.Clone();
        int actual = GetWinner(input, testCase.K);
        bool inputPreserved = input.SequenceEqual(snapshot);
        bool passed = actual == testCase.Expected && inputPreserved;

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.K,
            testCase.Expected,
            actual,
            inputPreserved,
            passed);
    }

    /// <summary>
    /// 以單次掃描找出最早達成指定連勝場次的贏家。
    /// 目前 champion 只會與下一個尚未比較的值交手；因此不需要模擬佇列，也不會修改輸入陣列。
    /// 僅處理題目定義的有效輸入：相異整數陣列與正整數 k。
    /// </summary>
    /// <param name="arr">依題目限制提供的相異整數陣列。</param>
    /// <param name="k">贏家必須達成的連勝場次。</param>
    /// <returns>最早達成 k 場連勝的整數；若掃描結束仍未達成，回傳全域最大值。</returns>
    public static int GetWinner(int[] arr, int k)
    {
        int champion = arr[0];
        int consecutiveWins = 0;

        for (int index = 1; index < arr.Length; index++)
        {
            if (champion > arr[index])
            {
                consecutiveWins++;
            }
            else
            {
                // 新的較大值成為 champion，連勝需從擊敗前任的這一場重新計算。
                champion = arr[index];
                consecutiveWins = 1;
            }

            if (consecutiveWins == k)
            {
                return champion;
            }
        }

        // 相異值的全域最大值不會再落敗，因此 k 很大時它就是最終贏家。
        return champion;
    }

    private static string FormatInput(int[] input)
    {
        const int compactThreshold = 10;
        const int displayedValues = 3;

        if (input.Length <= compactThreshold)
        {
            return $"[{string.Join(", ", input.Select(value => value.ToString(CultureInfo.InvariantCulture)))}]";
        }

        string first = string.Join(", ", input.Take(displayedValues).Select(value => value.ToString(CultureInfo.InvariantCulture)));
        string last = string.Join(", ", input.TakeLast(displayedValues).Select(value => value.ToString(CultureInfo.InvariantCulture)));
        return $"[{first}, ..., {last}] (Length: {input.Length.ToString(CultureInfo.InvariantCulture)})";
    }

    private sealed record TestCase(string Name, int[] Input, int K, int Expected);

    private sealed record CaseResult(
        string Name,
        int[] Input,
        int K,
        int Expected,
        int Actual,
        bool InputPreserved,
        bool Passed);
}
