namespace leetcode_1909;

internal static class Program
{
    /// <summary>
    /// LeetCode 1909. Remove One Element to Make the Array Strictly Increasing.
    /// LeetCode 1909. 刪除一個元素使陣列嚴格遞增。
    /// English: Given a 0-indexed integer array, return whether removing exactly one element
    /// can make the remaining elements strictly increasing. An already strictly increasing
    /// array also qualifies because any one element may be removed.
    /// 中文：給定零起始整數陣列，判斷刪除恰好一個元素後，剩餘元素能否嚴格遞增。
    /// 原本已嚴格遞增的陣列也符合條件，因為可刪除其中任一元素。
    /// English: https://leetcode.com/problems/remove-one-element-to-make-the-array-strictly-increasing/
    /// 中文：https://leetcode.cn/problems/remove-one-element-to-make-the-array-strictly-increasing/
    /// </summary>
    private static void Main()
    {
        TestCase[] testCases =
        [
            new("Official example 1", "[1, 2, 10, 5, 7]", [1, 2, 10, 5, 7], true),
            new("Official example 2", "[2, 3, 1, 2]", [2, 3, 1, 2], false),
            new("Official example 3", "[1, 1, 1]", [1, 1, 1], false),
            new("Minimum input", "[2, 1]", [2, 1], true),
            new("Already strictly increasing", "[1, 2, 3]", [1, 2, 3], true),
            new("Remove first element", "[10, 1, 2, 3]", [10, 1, 2, 3], true),
            new("Remove last element", "[1, 2, 3, 0]", [1, 2, 3, 0], true),
            new("Remove previous middle element", "[1, 2, 5, 3, 4]", [1, 2, 5, 3, 4], true),
            new("Duplicate at the beginning", "[1, 1, 2]", [1, 1, 2], true),
            new("Single violation cannot be repaired", "[1, 4, 5, 3, 4]", [1, 4, 5, 3, 4], false),
            new(
                "Maximum length strictly increasing",
                "[1..1000]",
                Enumerable.Range(1, 1000).ToArray(),
                true)
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck(
                "CanBeIncreasing result",
                result.Expected,
                result.CanBeIncreasingActual));
            Console.WriteLine(PrintCheck(
                "CanBeIncreasing input preserved",
                true,
                result.CanBeIncreasingInputPreserved));
            Console.WriteLine(PrintCheck(
                "CanBeIncreasingBruteForce result",
                result.Expected,
                result.BruteForceActual));
            Console.WriteLine(PrintCheck(
                "CanBeIncreasingBruteForce input preserved",
                true,
                result.BruteForceInputPreserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 44;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string PrintCheck(string checkName, bool expected, bool actual)
    {
        string status = expected == actual ? "PASS" : "FAIL";
        return $"{status} {checkName} | Expected: {expected} | Actual: {actual}";
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        int[] optimizedNumbers = [.. testCase.Numbers];
        int[] optimizedNumbersBefore = [.. optimizedNumbers];
        bool canBeIncreasingActual = CanBeIncreasing(optimizedNumbers);
        bool canBeIncreasingInputPreserved = optimizedNumbers.SequenceEqual(optimizedNumbersBefore);

        int[] bruteForceNumbers = [.. testCase.Numbers];
        int[] bruteForceNumbersBefore = [.. bruteForceNumbers];
        bool bruteForceActual = CanBeIncreasingBruteForce(bruteForceNumbers);
        bool bruteForceInputPreserved = bruteForceNumbers.SequenceEqual(bruteForceNumbersBefore);

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.Expected,
            canBeIncreasingActual,
            canBeIncreasingInputPreserved,
            bruteForceActual,
            bruteForceInputPreserved);
    }

    /// <summary>
    /// 對題目保證長度至少為 2 的有效整數陣列，以單次掃描找出相鄰元素未嚴格遞增的
    /// 位置。第一次違規時，同時判斷刪除前一項或目前項能否銜接兩側；若兩者皆不可行，
    /// 或之後再出現第二次違規，便回傳 false。方法只讀取 <paramref name="nums"/>，
    /// 不修改輸入或主控台狀態；存在合法刪除位置時回傳 true。
    /// </summary>
    /// <param name="nums">題目限制內、長度介於 2 至 1000 的整數陣列。</param>
    /// <returns>刪除恰好一個元素後可使剩餘元素嚴格遞增時為 true；否則為 false。</returns>
    public static bool CanBeIncreasing(int[] nums)
    {
        int violationCount = 0;

        for (int index = 1; index < nums.Length; index++)
        {
            if (nums[index] > nums[index - 1])
            {
                continue;
            }

            violationCount++;
            if (violationCount > 1)
            {
                return false;
            }

            // 刪除前一項須能接回 index - 2；刪除目前項須能接回 index + 1。
            bool canRemovePrevious = index == 1 || nums[index] > nums[index - 2];
            bool canRemoveCurrent = index == nums.Length - 1 || nums[index + 1] > nums[index - 1];

            if (!canRemovePrevious && !canRemoveCurrent)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 對題目保證長度至少為 2 的有效整數陣列，逐一假設刪除每個索引，再檢查其餘元素
    /// 是否嚴格遞增。方法只讀取 <paramref name="nums"/>，不修改輸入或主控台狀態；
    /// 任一刪除位置可行時回傳 true，全部不可行時回傳 false。
    /// </summary>
    /// <param name="nums">題目限制內、長度介於 2 至 1000 的整數陣列。</param>
    /// <returns>存在一個刪除位置可使剩餘元素嚴格遞增時為 true；否則為 false。</returns>
    public static bool CanBeIncreasingBruteForce(int[] nums)
    {
        for (int skippedIndex = 0; skippedIndex < nums.Length; skippedIndex++)
        {
            if (IsStrictlyIncreasingAfterSkipping(nums, skippedIndex))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 在有效陣列中略過指定索引，以原始相對順序檢查其餘元素是否皆嚴格遞增。
    /// 方法不配置輸入副本且不修改陣列；剩餘序列嚴格遞增時回傳 true。
    /// </summary>
    /// <param name="nums">題目限制內的整數陣列。</param>
    /// <param name="skippedIndex">本次檢查中唯一略過的有效索引。</param>
    /// <returns>略過指定元素後的序列嚴格遞增時為 true；否則為 false。</returns>
    private static bool IsStrictlyIncreasingAfterSkipping(int[] nums, int skippedIndex)
    {
        bool hasPrevious = false;
        int previous = 0;

        for (int index = 0; index < nums.Length; index++)
        {
            if (index == skippedIndex)
            {
                continue;
            }

            if (hasPrevious && nums[index] <= previous)
            {
                return false;
            }

            previous = nums[index];
            hasPrevious = true;
        }

        return true;
    }

    private sealed record TestCase(string Name, string Input, int[] Numbers, bool Expected);

    private sealed record CaseResult(
        string Name,
        string Input,
        bool Expected,
        bool CanBeIncreasingActual,
        bool CanBeIncreasingInputPreserved,
        bool BruteForceActual,
        bool BruteForceInputPreserved)
    {
        public int PassedCheckCount =>
            (CanBeIncreasingActual == Expected ? 1 : 0) +
            (CanBeIncreasingInputPreserved ? 1 : 0) +
            (BruteForceActual == Expected ? 1 : 0) +
            (BruteForceInputPreserved ? 1 : 0);
    }
}