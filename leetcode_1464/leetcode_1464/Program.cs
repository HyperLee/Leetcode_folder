namespace leetcode_1464;

internal class Program
{
    /// <summary>
    /// 1464. Maximum Product of Two Elements in an Array
    /// 1464. 陣列中兩個元素的最大乘積
    /// https://leetcode.com/problems/maximum-product-of-two-elements-in-an-array/
    /// https://leetcode.cn/problems/maximum-product-of-two-elements-in-an-array/
    /// Given an integer array, choose two different elements and return the maximum value of
    /// (nums[i] - 1) * (nums[j] - 1).
    /// 給定整數陣列，選取兩個不同元素，回傳 (nums[i] - 1) * (nums[j] - 1) 的最大值。
    /// </summary>
    /// <param name="args">主控台啟動參數；本驗證器不使用。</param>
    private static void Main(string[] args)
    {
        int[] maximumLengthNumbers = [.. Enumerable.Range(1, 498), 1000, 999];
        TestCase[] testCases =
        [
            new("Official example 1", "[3, 4, 5, 2]", [3, 4, 5, 2], 12),
            new("Official example 2 / duplicate maximum", "[1, 5, 4, 5]", [1, 5, 4, 5], 16),
            new("Official example 3 / minimum length", "[3, 7]", [3, 7], 12),
            new("Minimum values", "[1, 1]", [1, 1], 0),
            new("Maximum values", "[1000, 1000]", [1000, 1000], 998001),
            new("Largest arrives first / second-largest regression", "[10, 2, 5, 2]", [10, 2, 5, 2], 36),
            new("Unsorted general regression", "[4, 9, 2, 8, 3]", [4, 9, 2, 8, 3], 56),
            new("Maximum-length case", "[length 500; values 1..498, 1000, 999]", maximumLengthNumbers, 997002)
        ];

        int passed = 0;
        foreach (TestCase testCase in testCases)
        {
            int[] maxProductInput = [.. testCase.Numbers];
            int[] maxProduct2Input = [.. testCase.Numbers];
            int[] maxProduct3Input = [.. testCase.Numbers];

            int maxProductActual = MaxProduct(maxProductInput);
            int maxProduct2Actual = MaxProduct2(maxProduct2Input);
            int maxProduct3Actual = MaxProduct3(maxProduct3Input);
            bool inputsPreserved = maxProductInput.SequenceEqual(testCase.Numbers)
                && maxProduct2Input.SequenceEqual(testCase.Numbers)
                && maxProduct3Input.SequenceEqual(testCase.Numbers);
            bool isPassed = maxProductActual == testCase.Expected
                && maxProduct2Actual == testCase.Expected
                && maxProduct3Actual == testCase.Expected
                && inputsPreserved;
            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: {testCase.Input}");
            Console.WriteLine($"Expected: {testCase.Expected}");
            Console.WriteLine($"MaxProduct: {maxProductActual}");
            Console.WriteLine($"MaxProduct2: {maxProduct2Actual}");
            Console.WriteLine($"MaxProduct3: {maxProduct3Actual}");
            Console.WriteLine($"Input preserved: {inputsPreserved}");
            Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 計算陣列中兩個不同元素各減一後的最大乘積。單趟掃描維護目前最大值與次大值，
    /// 適用於題目定義的有效輸入；不修改 <paramref name="nums"/>，並回傳最大乘積。
    /// 時間複雜度為 O(n)，結果與輔助空間皆為 O(1)。
    /// </summary>
    /// <param name="nums">長度介於 2 至 500，且元素介於 1 至 1000 的有效整數陣列。</param>
    /// <returns>兩個不同元素各減一後可得到的最大乘積。</returns>
    public static int MaxProduct(int[] nums)
    {
        int largest = 0;
        int secondLargest = 0;

        foreach (int number in nums)
        {
            // largest 與 secondLargest 始終保存目前已掃描元素中的前兩大值。
            if (number > largest)
            {
                secondLargest = largest;
                largest = number;
            }
            else if (number > secondLargest)
            {
                secondLargest = number;
            }
        }

        return (largest - 1) * (secondLargest - 1);
    }

    /// <summary>
    /// 計算陣列中兩個不同元素各減一後的最大乘積。先複製並排序陣列，再取最大的兩個值，
    /// 適用於題目定義的有效輸入；不修改 <paramref name="nums"/>，並回傳最大乘積。
    /// 時間複雜度為 O(n log n)，結果空間為 O(1)，輔助空間為 O(n)。
    /// </summary>
    /// <param name="nums">長度介於 2 至 500，且元素介於 1 至 1000 的有效整數陣列。</param>
    /// <returns>兩個不同元素各減一後可得到的最大乘積。</returns>
    public static int MaxProduct2(int[] nums)
    {
        // 排序副本即可取得最大的兩個值，同時保留呼叫端的原始陣列。
        int[] sortedNumbers = [.. nums];
        Array.Sort(sortedNumbers);

        return (sortedNumbers[^1] - 1) * (sortedNumbers[^2] - 1);
    }

    /// <summary>
    /// 計算陣列中兩個不同元素各減一後的最大乘積。依序枚舉右側元素，同時維護其左側
    /// 已出現的最大值與目前最佳乘積，適用於題目定義的有效輸入；不修改
    /// <paramref name="nums"/>，並回傳最大乘積。時間複雜度為 O(n)，結果與輔助空間皆為 O(1)。
    /// </summary>
    /// <param name="nums">長度介於 2 至 500，且元素介於 1 至 1000 的有效整數陣列。</param>
    /// <returns>兩個不同元素各減一後可得到的最大乘積。</returns>
    public static int MaxProduct3(int[] nums)
    {
        int answer = 0;
        int largestOnLeft = 0;

        foreach (int number in nums)
        {
            // 先用左側最大值計算候選答案，再納入目前值，確保選到的是兩個不同索引。
            answer = Math.Max(answer, (largestOnLeft - 1) * (number - 1));
            largestOnLeft = Math.Max(largestOnLeft, number);
        }

        return answer;
    }

    private sealed record TestCase(string Name, string Input, int[] Numbers, int Expected);
}