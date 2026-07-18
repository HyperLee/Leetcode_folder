namespace leetcode_1502;

internal class Program
{
    /// <summary>
    /// 1502. Can Make Arithmetic Progression From Sequence
    /// 1502. 能否形成等差數列
    /// https://leetcode.com/problems/can-make-arithmetic-progression-from-sequence/
    /// https://leetcode.cn/problems/can-make-arithmetic-progression-from-sequence/
    /// Given an array of numbers, rearrange its elements and determine whether they can form
    /// an arithmetic progression, where every pair of adjacent values has the same difference.
    /// 給定一個整數陣列，重新排列其中元素後，判斷是否能形成等差數列；等差數列中每一對
    /// 相鄰值的差皆相同。
    /// </summary>
    /// <param name="args">主控台啟動參數；本驗證器不使用。</param>
    private static void Main(string[] args)
    {
        int[] maximumLengthNumbers = Enumerable.Range(0, 1_000).Reverse().ToArray();

        TestCase[] testCases =
        [
            new("Official example 1", "[3, 5, 1]", [3, 5, 1], true),
            new("Official example 2", "[1, 2, 4]", [1, 2, 4], false),
            new("Minimum length", "[7, 3]", [7, 3], true),
            new("Zero difference", "[5, 5, 5, 5]", [5, 5, 5, 5], true),
            new("Negative values", "[-3, -1, -2]", [-3, -1, -2], true),
            new("Across zero", "[-10, 10, 0]", [-10, 10, 0], true),
            new("Tail breaks progression", "[1, 3, 5, 8]", [1, 3, 5, 8], false),
            new("Duplicate breaks progression", "[1, 2, 2, 3]", [1, 2, 2, 3], false),
            new("Value boundaries", "[-1000000, 0, 1000000]", [-1_000_000, 0, 1_000_000], true),
            new("Maximum length", "[999..0] (1000 values)", maximumLengthNumbers, true)
        ];

        int passed = 0;
        foreach (TestCase testCase in testCases)
        {
            int[] originalNumbers = [.. testCase.Numbers];
            bool actual = CanMakeArithmeticProgression(testCase.Numbers);
            bool isPassed = actual == testCase.Expected
                && testCase.Numbers.SequenceEqual(originalNumbers);

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: {testCase.Input}");
            Console.WriteLine($"Expected: {testCase.Expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            if (isPassed)
            {
                passed++;
            }
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 在 arr 為長度至少為 2 的有效整數陣列時，先複製並排序資料，再比較每一對相鄰值
    /// 與第一個公差是否一致；若可重新排列為等差數列則回傳 true，否則回傳 false，且不修改 arr。
    /// </summary>
    /// <param name="arr">長度介於 2 與 1000、元素值介於 -1000000 與 1000000 的有效整數陣列。</param>
    /// <returns>重新排列後能否形成相鄰差相同的等差數列。</returns>
    public static bool CanMakeArithmeticProgression(int[] arr)
    {
        int[] sortedNumbers = [.. arr];
        Array.Sort(sortedNumbers);

        int difference = sortedNumbers[1] - sortedNumbers[0];
        for (int index = 2; index < sortedNumbers.Length; index++)
        {
            // 排序後只要任一相鄰差不同，就不可能透過其他排列形成固定公差。
            if (sortedNumbers[index] - sortedNumbers[index - 1] != difference)
            {
                return false;
            }
        }

        return true;
    }

    private sealed record TestCase(string Name, string Input, int[] Numbers, bool Expected);
}
