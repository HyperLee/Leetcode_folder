namespace leetcode_1512;

internal class Program
{
    /// <summary>
    /// 1512. Number of Good Pairs／好數對的數目。
    /// LeetCode English: https://leetcode.com/problems/number-of-good-pairs/
    /// LeetCode 中文：https://leetcode.cn/problems/number-of-good-pairs/
    /// Given an integer array, count pairs of indices i and j where i &lt; j and nums[i] equals nums[j].
    /// 給定整數陣列，計算所有滿足 i &lt; j 且 nums[i] 等於 nums[j] 的索引配對數量。
    /// </summary>
    private static void Main()
    {
        TestCase[] cases =
        [
            new("Official example 1", [1, 2, 3, 1, 1, 3], 4),
            new("Official example 2", [1, 1, 1, 1], 6),
            new("Official example 3", [1, 2, 3], 0),
            new("Minimum input", [1], 0),
            new("Two-element pair", [5, 5], 1),
            new("Non-adjacent duplicates", [1, 2, 1, 2, 1], 4),
            new("Value boundaries", [1, 100, 1, 100], 2),
            new("Mixed frequencies", [1, 1, 2, 2, 2, 3, 3, 3, 3], 10),
            new("Maximum length", Enumerable.Repeat(7, 100).ToArray(), 4950)
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
            Console.WriteLine($"Expected: {result.Expected}");
            Console.WriteLine($"NumIdenticalPairs: {result.BruteForceActual}");
            Console.WriteLine($"NumIdenticalPairs2: {result.DictionaryActual}");
            Console.WriteLine($"Input preserved: {result.InputsPreserved}");
            Console.WriteLine($"Result: {(result.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passedCount}/{cases.Length} checks passed.");
        if (passedCount != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 計算好數對的數目。以雙迴圈枚舉每個 i 小於 j 的索引配對，僅適用於題目定義的有效
    /// 整數陣列輸入；不修改 <paramref name="nums"/>，並回傳元素值相同的配對總數。
    /// 時間複雜度為 O(n^2)，結果與輔助空間皆為 O(1)。
    /// </summary>
    /// <param name="nums">長度介於 1 到 100、元素值介於 1 到 100 的有效整數陣列。</param>
    /// <returns>所有滿足 i 小於 j 且 nums[i] 等於 nums[j] 的配對數目。</returns>
    public static int NumIdenticalPairs(int[] nums)
    {
        int pairCount = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            // 從 i 的下一個位置開始，確保每一組索引只計數一次。
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] == nums[j])
                {
                    pairCount++;
                }
            }
        }

        return pairCount;
    }

    /// <summary>
    /// 計算好數對的數目。單次掃描時把目前值先前已出現的次數加入答案，再更新字典計數；
    /// 僅適用於題目定義的有效整數陣列輸入；不修改 <paramref name="nums"/>，並回傳元素值
    /// 相同的配對總數。時間複雜度為 O(n)，輔助空間為 O(k)，結果空間為 O(1)。
    /// </summary>
    /// <param name="nums">長度介於 1 到 100、元素值介於 1 到 100 的有效整數陣列。</param>
    /// <returns>所有滿足 i 小於 j 且 nums[i] 等於 nums[j] 的配對數目。</returns>
    public static int NumIdenticalPairs2(int[] nums)
    {
        int pairCount = 0;
        Dictionary<int, int> occurrences = new();

        foreach (int value in nums)
        {
            if (occurrences.TryGetValue(value, out int previousCount))
            {
                // 每個先前相同值都能與目前索引形成一個新的好數對。
                pairCount += previousCount;
                occurrences[value] = previousCount + 1;
            }
            else
            {
                occurrences.Add(value, 1);
            }
        }

        return pairCount;
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int[] bruteForceInput = (int[])testCase.Input.Clone();
        int[] dictionaryInput = (int[])testCase.Input.Clone();
        int[] bruteForceSnapshot = (int[])bruteForceInput.Clone();
        int[] dictionarySnapshot = (int[])dictionaryInput.Clone();

        int bruteForceActual = NumIdenticalPairs(bruteForceInput);
        int dictionaryActual = NumIdenticalPairs2(dictionaryInput);
        bool inputsPreserved = bruteForceInput.SequenceEqual(bruteForceSnapshot)
            && dictionaryInput.SequenceEqual(dictionarySnapshot);
        bool passed = bruteForceActual == testCase.Expected
            && dictionaryActual == testCase.Expected
            && inputsPreserved;

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.Expected,
            bruteForceActual,
            dictionaryActual,
            inputsPreserved,
            passed);
    }

    private static string FormatInput(int[] nums)
    {
        if (nums.Length == 100 && nums.All(value => value == nums[0]))
        {
            return $"[{nums[0]} × 100]";
        }

        return $"[{string.Join(", ", nums)}]";
    }

    private sealed record TestCase(string Name, int[] Input, int Expected);

    private sealed record CaseResult(
        string Name,
        int[] Input,
        int Expected,
        int BruteForceActual,
        int DictionaryActual,
        bool InputsPreserved,
        bool Passed);
}
