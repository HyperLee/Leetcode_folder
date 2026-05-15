namespace leetcode_026;

internal class Program
{
    /// <summary>
    /// 26. Remove Duplicates from Sorted Array
    /// https://leetcode.com/problems/remove-duplicates-from-sorted-array/description/
    ///
    /// English:
    /// Given an integer array nums sorted in non-decreasing order, remove the duplicates in-place such that each unique element appears only once. The relative order of the elements should be kept the same.
    ///
    /// Consider the number of unique elements in nums to be k. After removing duplicates, return the number of unique elements k.
    ///
    /// The first k elements of nums should contain the unique numbers in sorted order. The remaining elements beyond index k - 1 can be ignored.
    ///
    /// 繁體中文:
    /// 給定一個依非遞減順序排序的整數陣列 nums，請原地移除重複元素，使每個唯一元素只出現一次，並且保留原本的相對順序。
    ///
    /// 將 nums 中唯一元素的數量視為 k。移除重複元素後，請回傳唯一元素的數量 k。
    ///
    /// nums 的前 k 個元素必須依排序後的順序保留所有不重複的數字。超過索引 k - 1 的其餘元素可以忽略。
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        RunDemoSuite();
    }

    /// <summary>
    /// 執行多組排序陣列測資，對照兩種原地去重解法的輸出結果。
    /// 解題核心都是利用已排序陣列中重複值會相鄰出現的特性。
    /// 輸入測資需符合非遞減排序；每個案例都會列出回傳的唯一元素數量與前 k 個有效結果。
    /// </summary>
    private static void RunDemoSuite()
    {
        Program solver = new Program();

        Console.WriteLine("LeetCode 026 - Remove Duplicates from Sorted Array");
        Console.WriteLine();

        RunDemoCase(solver, "Example 1", new int[] { 1, 1, 2 });
        RunDemoCase(solver, "Example 2", new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 });
        RunDemoCase(solver, "Single Element", new int[] { 1 });
        RunDemoCase(solver, "All Duplicates", new int[] { 1, 1, 1 });
        RunDemoCase(solver, "Defensive Empty Array", Array.Empty<int>());
    }

    /// <summary>
    /// 對單一排序陣列依序執行兩種解法，驗證它們都能在原陣列前段留下唯一值。
    /// 解題概念是比較不同指標寫法下的同一個雙指標策略。
    /// 輸入條件為非遞減排序陣列；輸出會顯示兩個解法回傳的 k 與原地修改後的內容。
    /// </summary>
    /// <param name="solver">用來呼叫解法的程式實例。</param>
    /// <param name="caseName">目前展示的測資名稱。</param>
    /// <param name="source">尚未被修改的原始排序陣列。</param>
    private static void RunDemoCase(Program solver, string caseName, int[] source)
    {
        int[] methodOneInput = (int[])source.Clone();
        int methodOneUniqueCount = solver.RemoveDuplicates(methodOneInput);

        int[] methodTwoInput = (int[])source.Clone();
        int methodTwoUniqueCount = solver.RemoveDuplicates2(methodTwoInput);

        Console.WriteLine($"=== {caseName} ===");
        PrintMethodResult("解法一 - 雙指標 while", source, methodOneInput, methodOneUniqueCount);
        PrintMethodResult("解法二 - 雙指標 for", source, methodTwoInput, methodTwoUniqueCount);
        Console.WriteLine();
    }

    /// <summary>
    /// 將單一解法的執行結果整理成一致的主控台輸出。
    /// 此方法不參與解題，只負責對照原始輸入、唯一值前綴與原地更新後的陣列。
    /// 輸入包含原始測資、被修改後的陣列與唯一元素數量；輸出為主控台文字。
    /// </summary>
    /// <param name="methodName">要展示的解法名稱。</param>
    /// <param name="originalInput">未被修改的原始測資。</param>
    /// <param name="mutatedArray">執行解法後被原地更新的陣列。</param>
    /// <param name="uniqueCount">解法回傳的唯一元素數量 k。</param>
    private static void PrintMethodResult(string methodName, int[] originalInput, int[] mutatedArray, int uniqueCount)
    {
        Console.WriteLine(methodName);
        Console.WriteLine($"Input: {FormatArray(originalInput)}");
        Console.WriteLine($"k = {uniqueCount}");
        Console.WriteLine($"Unique prefix: {FormatUniquePrefix(mutatedArray, uniqueCount)}");
        Console.WriteLine($"Array after in-place update: {FormatArray(mutatedArray)}");
    }

    /// <summary>
    /// 將整個陣列格式化為展示字串，方便閱讀 demo 輸出。
    /// 輸入可以是任意整數陣列；輸出為中括號包裹、逗號分隔的字串。
    /// </summary>
    /// <param name="nums">要格式化的整數陣列。</param>
    /// <returns>適合顯示在主控台的陣列字串。</returns>
    private static string FormatArray(int[] nums)
    {
        return $"[{string.Join(", ", nums)}]";
    }

    /// <summary>
    /// 只擷取前 k 個有效答案區段，方便展示去重後的結果。
    /// 輸入條件是 k 介於 0 到陣列長度之間；輸出只包含題目要求保留的前綴。
    /// </summary>
    /// <param name="nums">已經原地整理過的陣列。</param>
    /// <param name="uniqueCount">唯一元素數量 k。</param>
    /// <returns>只包含前 k 個有效元素的字串。</returns>
    private static string FormatUniquePrefix(int[] nums, int uniqueCount)
    {
        if (uniqueCount == 0)
        {
            return "[]";
        }

        return $"[{string.Join(", ", nums[..uniqueCount])}]";
    }

    /// <summary>
    /// 使用快慢指標搭配 while 迴圈，原地移除排序陣列中的重複值。
    /// 解題概念是讓 fast 掃描整個陣列，slow 指向下一個可寫入唯一值的位置。
    /// 輸入必須是非遞減排序陣列；回傳唯一元素數量 k，且 nums 前 k 個位置會被整理成不重複結果。
    /// </summary>
    /// <param name="nums">依非遞減順序排序的整數陣列。</param>
    /// <returns>去重後唯一元素的數量 k。</returns>
    public int RemoveDuplicates(int[] nums)
    {
        int n = nums.Length;
        if (n == 0)
        {
            // LeetCode 原題保證至少一個元素，這裡額外保留防呆以支援本地邊界測試。
            return 0;
        }

        int fast = 1;
        int slow = 1;

        while (fast < n)
        {
            // 陣列已排序，相同值一定連續出現；遇到新值時即可覆寫到下一個唯一區段位置。
            if (nums[fast] != nums[fast - 1])
            {
                nums[slow] = nums[fast];
                slow++;
            }

            fast++;
        }

        return slow;
    }

    /// <summary>
    /// 使用左右指標搭配 for 迴圈，原地移除排序陣列中的重複值。
    /// 解題概念與解法一相同，只是把掃描流程改寫成 for 迴圈，讓 right 負責遍歷、left 負責寫回唯一值。
    /// 輸入必須是非遞減排序陣列；回傳唯一元素數量 k，且 nums 前 k 個位置會被整理成不重複結果。
    /// </summary>
    /// <param name="nums">依非遞減順序排序的整數陣列。</param>
    /// <returns>去重後唯一元素的數量 k。</returns>
    public int RemoveDuplicates2(int[] nums)
    {
        int n = nums.Length;
        if (n == 0)
        {
            // 額外補上空陣列防呆，讓本地 demo 能驗證題目限制之外的邊界輸入。
            return 0;
        }

        int left = 1;

        for (int right = 1; right < n; right++)
        {
            // 只要目前值和前一個值不同，就代表找到新的唯一元素，應寫回 left 指向的位置。
            if (nums[right] != nums[right - 1])
            {
                nums[left] = nums[right];
                left++;
            }
        }

        return left;
    }
}
