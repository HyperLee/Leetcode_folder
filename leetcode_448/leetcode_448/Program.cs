namespace leetcode_448;

internal static class Program
{
    /// <summary>
    /// 448. Find All Numbers Disappeared in an Array
    /// https://leetcode.com/problems/find-all-numbers-disappeared-in-an-array/
    /// 448. 找到所有陣列中消失的數字
    /// https://leetcode.cn/problems/find-all-numbers-disappeared-in-an-array/
    /// Given an array of integers in the range [1, n], return the values in that range that do not appear in the array.
    /// 給定元素範圍為 [1, n] 的整數陣列，回傳該範圍中未出現在陣列內的數字。
    /// </summary>
    private static void Main()
    {
        int[] officialExample = [4, 3, 2, 7, 8, 2, 3, 1];
        AcceptanceResult[] results =
        [
            RunResultCase("Official example", officialExample, [5, 6]),
            RunResultCase("Minimum", [1], []),
            RunResultCase("Duplicate one", [1, 1], [2]),
            RunResultCase("Full coverage", [1, 2, 3, 4, 5], []),
            RunResultCase("Repeated values", [2, 2, 3, 3, 4, 4], [1, 5, 6]),
            RunResultCase("Missing tail", [1, 1, 2, 3], [4]),
            RunResultCase("Two missing values", [2, 1, 2, 1], [3, 4]),
            RunResultCase("Reverse duplicates", [4, 4, 3, 3, 2, 2, 1, 1], [5, 6, 7, 8]),
            RunResultCase(
                "Upper bound n=100000",
                CreateUpperBoundInput(),
                [100000],
                "generated n=100000: [1, 2, ..., 99999, 99999]"),
            RunInvariantCheck(officialExample)
        ];
        int passedChecks = 0;

        foreach (AcceptanceResult result in results)
        {
            Console.WriteLine($"Case: {result.CaseName}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine($"Expected: {result.Expected}");
            Console.WriteLine($"Actual: {result.Actual}");
            Console.WriteLine($"Result: {(result.Passed ? "PASS" : "FAIL")}");

            passedChecks += result.Passed ? 1 : 0;
        }

        if (passedChecks == 10)
        {
            Console.WriteLine("Summary: 10/10 checks passed.");
        }
        else
        {
            Console.WriteLine($"Summary: {passedChecks}/10 checks passed.");
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 以原地負號標記找出指定範圍中未出現在陣列內的數字。
    /// </summary>
    /// <param name="nums">元素值介於一到陣列長度之間的整數陣列。</param>
    /// <returns>依遞增順序排列的缺失數字。</returns>
    public static IList<int> FindDisappearedNumbers(int[] nums)
    {
        List<int> missingNumbers = [];

        for (int index = 0; index < nums.Length; index++)
        {
            // 先取絕對值，避免先前標記的負號改變要對應的一次索引。
            int markIndex = Math.Abs(nums[index]) - 1;
            if (nums[markIndex] > 0)
            {
                nums[markIndex] = -nums[markIndex];
            }
        }

        for (int index = 0; index < nums.Length; index++)
        {
            // 保持為正值的位置代表該一位基數值從未出現在輸入中。
            if (nums[index] > 0)
            {
                missingNumbers.Add(index + 1);
            }
        }

        return missingNumbers;
    }

    private static AcceptanceResult RunResultCase(string caseName, int[] source, int[] expected, string? inputDescription = null)
    {
        int[] workingCopy = (int[])source.Clone();
        IList<int> actual = FindDisappearedNumbers(workingCopy);
        bool passed = MatchesExpected(expected, actual);

        return new AcceptanceResult(
            caseName,
            inputDescription ?? FormatNumbers(source),
            FormatNumbers(expected),
            FormatNumbers(actual),
            passed);
    }

    private static AcceptanceResult RunInvariantCheck(int[] officialExample)
    {
        int[] markedNumbers = (int[])officialExample.Clone();
        IList<int> missingNumbers = FindDisappearedNumbers(markedNumbers);
        bool passed = SatisfiesSignMarkingInvariant(markedNumbers, missingNumbers);

        return new AcceptanceResult(
            "Sign-marking invariant",
            FormatNumbers(officialExample),
            "a position is positive iff its one-based value is returned",
            passed ? "a position is positive iff its one-based value is returned" : "invariant violated",
            passed);
    }

    /// <summary>
    /// 驗證負號標記後的正值位置，是否剛好對應到所有回傳的缺失數字。
    /// </summary>
    private static bool SatisfiesSignMarkingInvariant(int[] markedNumbers, IList<int> missingNumbers)
    {
        for (int index = 0; index < markedNumbers.Length; index++)
        {
            bool isPositiveSlot = markedNumbers[index] > 0;
            bool isMissingNumber = missingNumbers.Contains(index + 1);

            if (isPositiveSlot != isMissingNumber)
            {
                return false;
            }
        }

        return true;
    }

    private static int[] CreateUpperBoundInput()
    {
        int[] numbers = new int[100000];

        for (int index = 0; index < numbers.Length - 1; index++)
        {
            numbers[index] = index + 1;
        }

        numbers[^1] = numbers.Length - 1;
        return numbers;
    }

    private static bool MatchesExpected(int[] expected, IList<int> actual)
    {
        if (expected.Length != actual.Count)
        {
            return false;
        }

        for (int index = 0; index < expected.Length; index++)
        {
            if (expected[index] != actual[index])
            {
                return false;
            }
        }

        return true;
    }

    private static string FormatNumbers(IEnumerable<int> numbers)
    {
        return $"[{string.Join(", ", numbers)}]";
    }

    private sealed record AcceptanceResult(string CaseName, string Input, string Expected, string Actual, bool Passed);
}
