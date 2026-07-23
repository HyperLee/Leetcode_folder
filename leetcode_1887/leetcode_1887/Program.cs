namespace leetcode_1887;

internal static class Program
{
    /// <summary>
    /// LeetCode 1887. Reduction Operations to Make the Array Elements Equal.
    /// LeetCode 1887. 使陣列元素相等的減少操作次數。
    /// English: Given an integer array, repeatedly find the largest value and the next smaller
    /// distinct value. If the largest value appears more than once, choose its smallest index,
    /// reduce that element to the next smaller value, and return the operations needed to make
    /// every element equal.
    /// 中文：給定一個整數陣列，反覆找出最大值與嚴格小於它的次大相異值；最大值有多個
    /// 時選擇索引最小者，將該元素降低為次大值，回傳使所有元素相等所需的操作次數。
    /// English: https://leetcode.com/problems/reduction-operations-to-make-the-array-elements-equal/
    /// 中文：https://leetcode.cn/problems/reduction-operations-to-make-the-array-elements-equal/
    /// </summary>
    /// <remarks>
    /// 舊版需求重點：找出最大數與下一個最大數，讓最大值降低至次大值；若有多個相同
    /// 最大值，從索引最小者開始，直到陣列中所有數值相同，並計算所需步驟。
    /// </remarks>
    private static void Main()
    {
        int[] maximumDistinctNumbers = Enumerable.Range(1, 50_000).Reverse().ToArray();

        TestCase[] testCases =
        [
            new("Official example 1", "[5, 1, 3]", [5, 1, 3], 3),
            new("Official example 2", "[1, 1, 1]", [1, 1, 1], 0),
            new("Official example 3", "[1, 1, 2, 2, 3]", [1, 1, 2, 2, 3], 4),
            new("Minimum input", "[1]", [1], 0),
            new("Two elements", "[2, 1]", [2, 1], 1),
            new("Value gap does not change level count", "[1, 100, 100]", [1, 100, 100], 2),
            new("Unsorted duplicate groups", "[4, 1, 2, 2, 4]", [4, 1, 2, 2, 4], 6),
            new(
                "Maximum length all equal",
                "[7 x 50000]",
                Enumerable.Repeat(7, 50_000).ToArray(),
                0),
            new(
                "Maximum length all distinct",
                "[50000..1]",
                maximumDistinctNumbers,
                1_249_975_000)
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine($"Expected: {result.Expected}");
            Console.WriteLine($"Actual: {result.Actual}");
            Console.WriteLine($"Input preserved: {result.InputPreserved}");
            Console.WriteLine($"Result: {(result.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int passedCount = results.Count(result => result.Passed);
        Console.WriteLine($"Summary: {passedCount}/{results.Length} checks passed.");

        if (passedCount != results.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 在 nums 符合題目限制的有效輸入下，先複製並排序陣列，再由小到大統計已跨越的
    /// 相異值層數；每個元素必須降低的次數正是它上方跨越的層數，累加後回傳總操作數。
    /// 此方法不修改 nums，也不產生主控台輸出。
    /// </summary>
    /// <param name="nums">長度介於 1 至 50000，元素值介於 1 至 50000 的整數陣列。</param>
    /// <returns>將所有元素降低至相同值所需的操作次數。</returns>
    public static int ReductionOperations(int[] nums)
    {
        int[] sortedNumbers = [.. nums];
        Array.Sort(sortedNumbers);

        int distinctLevelCount = 0;
        int operationCount = 0;

        for (int index = 1; index < sortedNumbers.Length; index++)
        {
            if (sortedNumbers[index] != sortedNumbers[index - 1])
            {
                // 跨入新的相異值層後，這個值與其後元素都會多一次降低操作。
                distinctLevelCount++;
            }

            operationCount += distinctLevelCount;
        }

        return operationCount;
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int[] originalNumbers = [.. testCase.Numbers];
        int actual = ReductionOperations(testCase.Numbers);
        bool inputPreserved = testCase.Numbers.SequenceEqual(originalNumbers);

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.Expected,
            actual,
            inputPreserved,
            actual == testCase.Expected && inputPreserved);
    }

    private sealed record TestCase(string Name, string Input, int[] Numbers, int Expected);

    private sealed record CaseResult(
        string Name,
        string Input,
        int Expected,
        int Actual,
        bool InputPreserved,
        bool Passed);
}