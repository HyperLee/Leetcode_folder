namespace leetcode_1481;

internal class Program
{
    /// <summary>
    /// 1481. Least Number of Unique Integers after K Removals
    /// 1481. 移除 K 個元素後相異整數的最少數目
    /// https://leetcode.com/problems/least-number-of-unique-integers-after-k-removals/
    /// https://leetcode.cn/problems/least-number-of-unique-integers-after-k-removals/
    /// Given an integer array arr and an integer k, return the least number of unique integers
    /// after removing exactly k elements.
    /// 給定整數陣列 arr 與整數 k，恰好移除 k 個元素後，回傳可能留下的最少相異整數數量。
    /// </summary>
    /// <param name="args">主控台啟動參數；本驗證器不使用。</param>
    private static void Main(string[] args)
    {
        int[] maximumLengthNumbers =
        [
            .. Enumerable.Repeat(1, 50_000),
            .. Enumerable.Repeat(2, 25_000),
            .. Enumerable.Range(3, 25_000)
        ];

        TestCase[] testCases =
        [
            new("Official example 1", "arr = [5, 5, 4], k = 1", [5, 5, 4], 1, 1),
            new("Official example 2", "arr = [4, 3, 1, 1, 3, 3, 2], k = 3", [4, 3, 1, 1, 3, 3, 2], 3, 2),
            new("Minimum input / remove nothing", "arr = [1], k = 0", [1], 0, 1),
            new("Remove every element", "arr = [1], k = 1", [1], 1, 0),
            new("Zero removals", "arr = [1, 2, 2, 3, 3, 3], k = 0", [1, 2, 2, 3, 3, 3], 0, 3),
            new("Cannot remove the next frequency group", "arr = [1, 1, 2, 2, 2, 3, 3, 3], k = 1", [1, 1, 2, 2, 2, 3, 3, 3], 1, 3),
            new("Equal frequencies", "arr = [1, 1, 2, 2, 3, 3, 4], k = 4", [1, 1, 2, 2, 3, 3, 4], 4, 2),
            new("Large values", "arr = [1000000000, 1000000000, 999999999, 123456789], k = 2", [1_000_000_000, 1_000_000_000, 999_999_999, 123_456_789], 2, 1),
            new("Maximum length", "arr = [length 100000; 1 x 50000, 2 x 25000, 3..25002], k = 25000", maximumLengthNumbers, 25_000, 2)
        ];

        int passed = 0;

        foreach (TestCase testCase in testCases)
        {
            int[] original = [.. testCase.Numbers];
            int actual = FindLeastNumOfUniqueInts(testCase.Numbers, testCase.K);
            bool isPassed = actual == testCase.Expected && testCase.Numbers.SequenceEqual(original);

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
    /// 在 arr 與 k 符合題目限制的有效輸入下，統計各整數頻率並由低至高完整移除頻率群組，
    /// 以最小化恰好移除 k 個元素後的相異整數數量；回傳仍存在的相異整數數量，且不修改 arr。
    /// </summary>
    /// <param name="arr">長度介於 1 至 100000，元素介於 1 至 1000000000 的有效整數陣列。</param>
    /// <param name="k">要移除的元素數量，介於 0 至 arr.Length。</param>
    /// <returns>恰好移除 k 個元素後可得到的最少相異整數數量。</returns>
    public static int FindLeastNumOfUniqueInts(int[] arr, int k)
    {
        Dictionary<int, int> frequencyByNumber = [];
        foreach (int number in arr)
        {
            frequencyByNumber[number] = frequencyByNumber.GetValueOrDefault(number) + 1;
        }

        List<int> sortedFrequencies = [.. frequencyByNumber.Values];
        sortedFrequencies.Sort();

        int remainingUniqueCount = sortedFrequencies.Count;
        foreach (int frequency in sortedFrequencies)
        {
            // 只有完整刪除一個最低頻率群組，才會讓相異整數數量減一。
            if (frequency > k)
            {
                break;
            }

            k -= frequency;
            remainingUniqueCount--;
        }

        return remainingUniqueCount;
    }

    private sealed record TestCase(string Name, string Input, int[] Numbers, int K, int Expected);
}
