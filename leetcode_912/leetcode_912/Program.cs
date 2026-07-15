namespace leetcode_912;

internal static class Program
{
    /// <summary>
    /// 912. Sort an Array
    /// https://leetcode.com/problems/sort-an-array/
    /// 912. 排序陣列
    /// https://leetcode.cn/problems/sort-an-array/
    /// English: Given an integer array nums, sort the array in ascending order and return it without using built-in sorting functions. The solution must run in O(n log n) time with the smallest possible auxiliary space.
    /// 中文：給定整數陣列 nums，請在不使用內建排序函式的前提下，將陣列依遞增順序排序後回傳；解法須達到 O(n log n) 時間複雜度，並使用盡可能少的輔助空間。
    /// </summary>
    /// <param name="args">命令列參數；驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, int[] Input, int[] Expected)[] testCases =
        {
            ("Case 1: Official example 1", new[] { 5, 2, 3, 1 }, new[] { 1, 2, 3, 5 }),
            ("Case 2: Official example 2", new[] { 5, 1, 1, 2, 0, 0 }, new[] { 0, 0, 1, 1, 2, 5 }),
            ("Case 3: Single element", new[] { 42 }, new[] { 42 }),
            ("Case 4: Already sorted", new[] { -3, -1, 0, 2, 8 }, new[] { -3, -1, 0, 2, 8 }),
            ("Case 5: Reverse sorted", new[] { 9, 7, 5, 3, 1 }, new[] { 1, 3, 5, 7, 9 }),
            ("Case 6: All duplicates", new[] { 6, 6, 6, 6, 6 }, new[] { 6, 6, 6, 6, 6 }),
            ("Case 7: Repeated boundary values", new[] { 50000, -50000, 0, 50000, -50000 }, new[] { -50000, -50000, 0, 50000, 50000 })
        };

        List<CaseResult> checks = new();

        foreach ((string caseName, int[] input, int[] expected) in testCases)
        {
            string inputDisplay = FormatArray(input);
            int[] actual = SortArray(input);
            checks.Add(new CaseResult(caseName, inputDisplay, FormatArray(expected), FormatArray(actual), actual.SequenceEqual(expected)));
        }

        int[] maximumInput = CreateDescendingSequence(50_000);
        int[] maximumExpected = CreateAscendingSequence(50_000);
        int[] originalReference = maximumInput;
        int[] maximumActual = SortArray(maximumInput);
        checks.Add(new CaseResult(
            "Case 8: Maximum length",
            "50,000 integers in descending order [50000,...,1]",
            "50,000 integers in ascending order [1,...,50000]",
            SummarizeLargeArray(maximumActual),
            maximumActual.SequenceEqual(maximumExpected)));
        checks.Add(new CaseResult(
            "Case 8: Maximum length reference identity",
            "the original 50,000-element array reference",
            "the returned reference is the original input",
            ReferenceEquals(maximumActual, originalReference) ? "same reference" : "different reference",
            ReferenceEquals(maximumActual, originalReference)));

        int passedCount = 0;
        Console.WriteLine("LeetCode 912 acceptance harness");

        foreach (CaseResult check in checks)
        {
            Console.WriteLine();
            Console.WriteLine(check.CaseName);
            Console.WriteLine($"Input: {check.Input}");
            Console.WriteLine($"Expected: {check.Expected}");
            Console.WriteLine($"Actual: {check.Actual}");
            Console.WriteLine(check.Passed ? "PASS" : "FAIL");

            if (check.Passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{checks.Count} checks passed.");

        if (passedCount != checks.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用原地最大堆積排序將題目保證有效且至少含一個元素的整數陣列改為遞增順序。方法先由最後一個非葉節點向前建立最大堆，再逐次把堆頂最大值交換到未排序區尾端，並以迭代式下沉恢復堆不變量；輸入陣列會被直接修改，最後回傳同一個陣列參考。
    /// </summary>
    /// <param name="nums">符合題目限制、至少含一個元素且允許原地修改的整數陣列。</param>
    /// <returns>已依遞增順序排列的原輸入陣列參考。</returns>
    public static int[] SortArray(int[] nums)
    {
        // 長度除以二之後的索引皆為葉節點，因此從最後一個非葉節點開始建堆。
        for (int parent = (nums.Length / 2) - 1; parent >= 0; parent--)
        {
            SiftDown(nums, parent, nums.Length);
        }

        for (int unsortedEnd = nums.Length - 1; unsortedEnd > 0; unsortedEnd--)
        {
            (nums[0], nums[unsortedEnd]) = (nums[unsortedEnd], nums[0]);

            // 尾端已放妥目前最大值；只在縮小後的堆範圍內恢復最大堆。
            SiftDown(nums, 0, unsortedEnd);
        }

        return nums;
    }

    /// <summary>
    /// 在題目有效陣列的前 <paramref name="heapSize"/> 個元素中，將指定根節點以迭代方式向下移動，直到該子樹重新符合父節點不小於兩個子節點的最大堆不變量。方法會原地交換陣列元素、不建立額外集合，完成後指定堆範圍內的子樹為有效最大堆。
    /// </summary>
    /// <param name="nums">正在原地建立或修復最大堆的題目有效整數陣列。</param>
    /// <param name="root">可能違反最大堆不變量的子樹根索引。</param>
    /// <param name="heapSize">目前堆所涵蓋的前綴長度；此範圍外的元素不會被修改。</param>
    private static void SiftDown(int[] nums, int root, int heapSize)
    {
        while (true)
        {
            int leftChild = (root * 2) + 1;

            if (leftChild >= heapSize)
            {
                return;
            }

            int largerChild = leftChild;
            int rightChild = leftChild + 1;

            if (rightChild < heapSize && nums[rightChild] > nums[leftChild])
            {
                largerChild = rightChild;
            }

            if (nums[root] >= nums[largerChild])
            {
                return;
            }

            (nums[root], nums[largerChild]) = (nums[largerChild], nums[root]);
            root = largerChild;
        }
    }

    private static int[] CreateDescendingSequence(int length)
    {
        int[] values = new int[length];

        for (int index = 0; index < length; index++)
        {
            values[index] = length - index;
        }

        return values;
    }

    private static int[] CreateAscendingSequence(int length)
    {
        int[] values = new int[length];

        for (int index = 0; index < length; index++)
        {
            values[index] = index + 1;
        }

        return values;
    }

    private static string FormatArray(IEnumerable<int> values)
    {
        return $"[{string.Join(",", values.Select(value => value.ToString(System.Globalization.CultureInfo.InvariantCulture)))}]";
    }

    private static string SummarizeLargeArray(int[] values)
    {
        string length = values.Length.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
        return $"{length} integers [{values[0]},{values[1]},{values[2]},...,{values[^3]},{values[^2]},{values[^1]}]";
    }

    private readonly record struct CaseResult(string CaseName, string Input, string Expected, string Actual, bool Passed);
}
