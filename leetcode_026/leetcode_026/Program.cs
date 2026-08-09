namespace leetcode_026;

internal class Program
{
    /// <summary>
    /// 26. Remove Duplicates from Sorted Array
    /// https://leetcode.com/problems/remove-duplicates-from-sorted-array/description/
    /// <para>
    /// Given an integer array nums sorted in non-decreasing order, remove the duplicates in-place so that each unique element appears only once. The relative order of the elements must remain the same.
    ///
    /// Let k be the number of unique elements in nums. After removing the duplicates, return k.
    ///
    /// The first k elements of nums must contain the unique numbers in sorted order. The remaining elements beyond index k - 1 can be ignored.
    ///
    /// Custom Judge:
    /// The judge tests the solution with the following code:
    /// int[] nums = [...]; // Input array
    /// int[] expectedNums = [...]; // Expected answer with the correct length
    /// int k = removeDuplicates(nums); // Calls your implementation
    /// assert k == expectedNums.length;
    /// for (int i = 0; i &lt; k; i++) {
    ///     assert nums[i] == expectedNums[i];
    /// }
    /// If all assertions pass, the solution is accepted.
    ///
    /// Example 1:
    /// Input: nums = [1,1,2]
    /// Output: 2, nums = [1,2,_]
    /// Explanation: The function should return k = 2, with the first two elements of nums being 1 and 2 respectively. It does not matter what remains beyond the returned k, represented by underscores.
    ///
    /// Example 2:
    /// Input: nums = [0,0,1,1,1,2,2,3,3,4]
    /// Output: 5, nums = [0,1,2,3,4,_,_,_,_,_]
    /// Explanation: The function should return k = 5, with the first five elements of nums being 0, 1, 2, 3, and 4 respectively. It does not matter what remains beyond the returned k, represented by underscores.
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 3 * 10^4
    /// - -100 &lt;= nums[i] &lt;= 100
    /// - nums is sorted in non-decreasing order.
    /// </para>
    /// <para>
    /// 26. 移除排序陣列中的重複項目
    /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array/description/
    ///
    /// 給定一個以非遞減順序排序的整數陣列 nums，請原地移除重複項目，使每個唯一元素只出現一次。元素的相對順序必須保持不變。
    ///
    /// 令 k 為 nums 中唯一元素的數量。移除重複項目後，請回傳 k。
    ///
    /// nums 的前 k 個元素必須依排序順序包含所有唯一數字。索引 k - 1 之後的其餘元素可以忽略。
    ///
    /// 自訂評測：
    /// 評測程式會使用下列程式碼測試解答：
    /// int[] nums = [...]; // 輸入陣列
    /// int[] expectedNums = [...]; // 長度正確的預期答案
    /// int k = removeDuplicates(nums); // 呼叫你的實作
    /// assert k == expectedNums.length;
    /// for (int i = 0; i &lt; k; i++) {
    ///     assert nums[i] == expectedNums[i];
    /// }
    /// 若所有斷言都通過，解答即被接受。
    ///
    /// 範例 1：
    /// 輸入：nums = [1,1,2]
    /// 輸出：2, nums = [1,2,_]
    /// 解釋：函式應回傳 k = 2，且 nums 的前兩個元素分別為 1 和 2。回傳的 k 之後留下什麼內容並不重要，因此以底線表示。
    ///
    /// 範例 2：
    /// 輸入：nums = [0,0,1,1,1,2,2,3,3,4]
    /// 輸出：5, nums = [0,1,2,3,4,_,_,_,_,_]
    /// 解釋：函式應回傳 k = 5，且 nums 的前五個元素分別為 0、1、2、3 和 4。回傳的 k 之後留下什麼內容並不重要，因此以底線表示。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 3 * 10^4
    /// - -100 &lt;= nums[i] &lt;= 100
    /// - nums 以非遞減順序排序。
    /// </para>
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
