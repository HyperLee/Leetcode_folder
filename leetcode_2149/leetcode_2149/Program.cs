namespace leetcode_2149;

internal static class Program
{
    /// <summary>
    /// LeetCode 2149. Rearrange Array Elements by Sign.
    /// LeetCode 2149. 按正負號重排陣列。
    /// English: Given an even-length integer array containing equal numbers of positive and
    /// negative integers, return a new array that starts with a positive integer, alternates
    /// signs, and preserves the relative order of values with the same sign.
    /// 中文：給定一個長度為偶數、正整數與負整數數量相等的陣列，回傳一個以正整數開頭、
    /// 正負號交錯，且相同符號元素維持原相對順序的新陣列。
    /// English: https://leetcode.com/problems/rearrange-array-elements-by-sign/
    /// 中文：https://leetcode.cn/problems/rearrange-array-elements-by-sign/
    /// </summary>
    private static void Main()
    {
        int[] maximumInput =
        [
            .. Enumerable.Range(1, 100_000),
            .. Enumerable.Range(1, 100_000).Select(value => -value)
        ];
        int[] maximumExpected = Enumerable.Range(1, 100_000)
            .SelectMany(value => new[] { value, -value })
            .ToArray();

        TestCase[] testCases =
        [
            new(
                "Official example 1",
                "nums=[3,1,-2,-5,2,-4]",
                [3, 1, -2, -5, 2, -4],
                [3, -2, 1, -5, 2, -4]),
            new("Official example 2 and minimum length", "nums=[-1,1]", [-1, 1], [1, -1]),
            new(
                "Positive group before negative group",
                "nums=[1,2,3,-1,-2,-3]",
                [1, 2, 3, -1, -2, -3],
                [1, -1, 2, -2, 3, -3]),
            new(
                "Negative group before positive group",
                "nums=[-3,-2,-1,3,2,1]",
                [-3, -2, -1, 3, 2, 1],
                [3, -3, 2, -2, 1, -1]),
            new(
                "Already alternating",
                "nums=[5,-1,4,-2]",
                [5, -1, 4, -2],
                [5, -1, 4, -2]),
            new(
                "Value limits with duplicates",
                "nums=[100000,-100000,100000,-100000]",
                [100_000, -100_000, 100_000, -100_000],
                [100_000, -100_000, 100_000, -100_000]),
            new(
                "Maximum length",
                "nums=[1..100000,-1..-100000] (length 200000)",
                maximumInput,
                maximumExpected)
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintArrayCheck(
                "RearrangeArray result",
                result.Expected,
                result.RearrangeArrayActual));
            Console.WriteLine(PrintCheck(
                "RearrangeArray input preserved",
                true,
                result.RearrangeArrayInputPreserved));
            Console.WriteLine(PrintArrayCheck(
                "RearrangeArray2 result",
                result.Expected,
                result.RearrangeArray2Actual));
            Console.WriteLine(PrintCheck(
                "RearrangeArray2 input preserved",
                true,
                result.RearrangeArray2InputPreserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 28;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 掃描題目限制內正負數量相等的偶數長度陣列，將正數依原順序寫入結果的偶數索引，
    /// 並將負數依原順序寫入奇數索引。方法不修改 <paramref name="nums"/> 或主控台狀態，
    /// 回傳以正數開頭、符號交錯且維持同號元素相對順序的新陣列。時間複雜度為 O(n)，
    /// 除回傳陣列外的輔助空間為 O(1)。
    /// </summary>
    /// <param name="nums">
    /// 長度 2 至 200000 的偶數長度整數陣列；元素絕對值介於 1 至 100000，
    /// 且正數與負數數量相等。
    /// </param>
    /// <returns>符合正負號交錯、正數開頭及同號相對順序不變的新陣列。</returns>
    public static int[] RearrangeArray(int[] nums)
    {
        int[] result = new int[nums.Length];
        int positiveIndex = 0;
        int negativeIndex = 1;

        foreach (int value in nums)
        {
            if (value > 0)
            {
                // 偶數索引專屬正數；每次跨兩格即可同時保證正數順序與符號交錯。
                result[positiveIndex] = value;
                positiveIndex += 2;
            }
            else
            {
                // 奇數索引專屬負數，並以相同方向掃描來保留負數的相對順序。
                result[negativeIndex] = value;
                negativeIndex += 2;
            }
        }

        return result;
    }

    /// <summary>
    /// 掃描題目限制內正負數量相等的偶數長度陣列，依原順序分別收集正數與負數，
    /// 再逐對交錯寫入結果。方法不修改 <paramref name="nums"/> 或主控台狀態，回傳以正數
    /// 開頭、符號交錯且維持同號元素相對順序的新陣列。時間複雜度與輔助空間皆為 O(n)。
    /// </summary>
    /// <param name="nums">
    /// 長度 2 至 200000 的偶數長度整數陣列；元素絕對值介於 1 至 100000，
    /// 且正數與負數數量相等。
    /// </param>
    /// <returns>符合正負號交錯、正數開頭及同號相對順序不變的新陣列。</returns>
    public static int[] RearrangeArray2(int[] nums)
    {
        int signCount = nums.Length / 2;
        List<int> positiveValues = new(signCount);
        List<int> negativeValues = new(signCount);

        foreach (int value in nums)
        {
            if (value > 0)
            {
                positiveValues.Add(value);
            }
            else
            {
                negativeValues.Add(value);
            }
        }

        int[] result = new int[nums.Length];
        for (int index = 0; index < signCount; index++)
        {
            // 同一個分組索引代表各符號的下一個元素，交錯寫入時不會改變組內順序。
            result[index * 2] = positiveValues[index];
            result[(index * 2) + 1] = negativeValues[index];
        }

        return result;
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int[] rearrangeArrayInput = [.. testCase.Nums];
        int[] rearrangeArrayOriginal = [.. rearrangeArrayInput];
        int[] rearrangeArrayActual = RearrangeArray(rearrangeArrayInput);
        bool rearrangeArrayInputPreserved =
            rearrangeArrayInput.SequenceEqual(rearrangeArrayOriginal);

        int[] rearrangeArray2Input = [.. testCase.Nums];
        int[] rearrangeArray2Original = [.. rearrangeArray2Input];
        int[] rearrangeArray2Actual = RearrangeArray2(rearrangeArray2Input);
        bool rearrangeArray2InputPreserved =
            rearrangeArray2Input.SequenceEqual(rearrangeArray2Original);

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.Expected,
            rearrangeArrayActual,
            rearrangeArrayInputPreserved,
            rearrangeArray2Actual,
            rearrangeArray2InputPreserved);
    }

    private static string PrintArrayCheck(string name, int[] expected, int[] actual)
    {
        string status = expected.SequenceEqual(actual) ? "PASS" : "FAIL";
        return $"{status} {name} | Expected: {FormatArray(expected)} | Actual: {FormatArray(actual)}";
    }

    private static string PrintCheck<T>(string name, T expected, T actual)
    {
        string status = EqualityComparer<T>.Default.Equals(expected, actual) ? "PASS" : "FAIL";
        return $"{status} {name} | Expected: {expected} | Actual: {actual}";
    }

    private static string FormatArray(int[] values)
    {
        if (values.Length <= 12)
        {
            return $"[{string.Join(',', values)}]";
        }

        return $"[{string.Join(',', values.Take(3))},...,{string.Join(',', values.TakeLast(3))}] " +
            $"(length {values.Length})";
    }

    private sealed record TestCase(string Name, string Input, int[] Nums, int[] Expected);

    private sealed record CaseResult(
        string Name,
        string Input,
        int[] Expected,
        int[] RearrangeArrayActual,
        bool RearrangeArrayInputPreserved,
        int[] RearrangeArray2Actual,
        bool RearrangeArray2InputPreserved)
    {
        public int PassedCheckCount =>
            (Expected.SequenceEqual(RearrangeArrayActual) ? 1 : 0) +
            (RearrangeArrayInputPreserved ? 1 : 0) +
            (Expected.SequenceEqual(RearrangeArray2Actual) ? 1 : 0) +
            (RearrangeArray2InputPreserved ? 1 : 0);
    }
}