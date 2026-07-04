namespace leetcode_287;

class Program
{
    /// <summary>
    /// 287. Find the Duplicate Number
    /// https://leetcode.com/problems/find-the-duplicate-number/description/
    /// 287. 寻找重复数
    /// https://leetcode.cn/problems/find-the-duplicate-number/description/
    ///
    /// Given an array of integers nums containing n + 1 integers where each integer is in the range [1, n] inclusive.
    /// There is only one repeated number in nums, return this repeated number.
    /// You must solve the problem without modifying the array nums and using only constant extra space.
    ///
    /// 給定一個整數陣列 nums，其中包含 n + 1 個整數，且每個整數都在 [1, n] 的範圍內（含端點）。
    /// 陣列中只有一個重複出現的數字，請回傳這個重複的數字。
    /// 你必須在不修改陣列 nums，且只使用常數額外空間的條件下解出此題。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();

        Console.WriteLine("LeetCode 287 - Find the Duplicate Number");
        Console.WriteLine("================================");
        Console.WriteLine();

        RunAllSampleCases(solver);
    }

    /// <summary>
    /// 使用排序法找出陣列中的重複數字。
    /// 這個方法會直接對輸入陣列進行原地排序，接著掃描排序後的相鄰元素，
    /// 第一個重複出現的值就是答案。輸入必須符合題目條件：長度為 n + 1、元素值落在 [1, n]，
    /// 且只有一個數字重複出現；輸出為該重複數字。時間複雜度為 O(n log n)，空間複雜度依排序實作而定。
    /// </summary>
    /// <param name="nums">包含 n + 1 個整數的輸入陣列，每個值都落在 [1, n]。</param>
    /// <returns>陣列中唯一重複出現的數字。</returns>
    public int FindDuplicate(int[] nums)
    {
        Array.Sort(nums);

        // 排序後，第一個相鄰重複值就是答案。
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] == nums[i - 1])
            {
                return nums[i];
            }
        }

        return -1;
    }

    /// <summary>
    /// 使用 Floyd 快慢指標找出陣列中的重複數字。
    /// 把索引視為鏈結串列節點、把 nums[i] 視為 next 指標，就能把重複值轉換成環的入口問題。
    /// 輸入必須符合題目條件：長度為 n + 1、元素值落在 [1, n]，且只有一個數字重複出現；
    /// 輸出為該重複數字。時間複雜度為 O(n)，空間複雜度為 O(1)，也不會修改輸入陣列。
    /// </summary>
    /// <param name="nums">包含 n + 1 個整數的輸入陣列，每個值都落在 [1, n]。</param>
    /// <returns>陣列中唯一重複出現的數字。</returns>
    public int FindDuplicate2(int[] nums)
    {
        int slow = 0;
        int fast = 0;

        // 第一階段先找到環上的相遇點。
        do
        {
            slow = nums[slow];
            fast = nums[nums[fast]];
        }
        while (slow != fast);

        slow = 0;

        // 第二階段從起點與相遇點同步前進，環入口就是重複值。
        while (slow != fast)
        {
            slow = nums[slow];
            fast = nums[fast];
        }

        return slow;
    }

    /// <summary>
    /// 使用值域二分搜尋找出陣列中的重複數字。
    /// 針對值域 [1, n] 做二分搜尋，計算小於等於 mid 的元素個數是否超過 mid；
    /// 若超過，代表抽屜原理指出重複值一定落在左半值域，否則在右半值域。
    /// 輸入必須符合題目條件：長度為 n + 1、元素值落在 [1, n]，且只有一個數字重複出現；
    /// 輸出為該重複數字。時間複雜度為 O(n log n)，空間複雜度為 O(1)，也不會修改輸入陣列。
    /// </summary>
    /// <param name="nums">包含 n + 1 個整數的輸入陣列，每個值都落在 [1, n]。</param>
    /// <returns>陣列中唯一重複出現的數字。</returns>
    public int FindDuplicate3(int[] nums)
    {
        int left = 1;
        int right = nums.Length - 1;
        int result = -1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int count = 0;

            foreach (int num in nums)
            {
                if (num <= mid)
                {
                    count++;
                }
            }

            // 若 <= mid 的數量大於 mid，重複值一定落在左半值域。
            if (count > mid)
            {
                result = mid;
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return result;
    }

    /// <summary>
    /// 執行此題固定的五組示例案例，讓主控台輸出可直接作為教學與驗證用途。
    /// 這個方法只負責呼叫示例 harness，不屬於 LeetCode 提交時需要保留的核心解法。
    /// </summary>
    /// <param name="solver">提供三種找重複數字解法的程式實例。</param>
    private static void RunAllSampleCases(Program solver)
    {
        RunSampleCase(solver, 1, new int[] { 1, 3, 4, 2, 2 }, 2);
        RunSampleCase(solver, 2, new int[] { 3, 1, 3, 4, 2 }, 3);
        RunSampleCase(solver, 3, new int[] { 1, 1 }, 1);
        RunSampleCase(solver, 4, new int[] { 1, 1, 2 }, 1);
        RunSampleCase(solver, 5, new int[] { 2, 5, 9, 6, 9, 3, 8, 9, 7, 1 }, 9);
    }

    /// <summary>
    /// 執行單筆示例資料，輸出三種解法的結果並檢查是否符合預期答案。
    /// 這個方法是本地示例驗證 harness，不是 LeetCode 題目要求的公開 API；
    /// 它會複製輸入陣列，避免排序解法改動資料後影響其他解法的結果。
    /// </summary>
    /// <param name="solver">提供三種找重複數字解法的程式實例。</param>
    /// <param name="caseNumber">示例案例編號。</param>
    /// <param name="nums">要驗證的輸入陣列。</param>
    /// <param name="expected">此案例預期的重複數字。</param>
    private static void RunSampleCase(Program solver, int caseNumber, int[] nums, int expected)
    {
        int sortingResult = solver.FindDuplicate((int[])nums.Clone());
        int floydResult = solver.FindDuplicate2((int[])nums.Clone());
        int binarySearchResult = solver.FindDuplicate3((int[])nums.Clone());

        Console.WriteLine($"Case {caseNumber}");
        Console.WriteLine($"Input: [{string.Join(", ", nums)}]");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"{ "FindDuplicate  (Sort)",-30}: {sortingResult} {(sortingResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine($"{ "FindDuplicate2 (Floyd Cycle)",-30}: {floydResult} {(floydResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine($"{ "FindDuplicate3 (Binary Search)",-30}: {binarySearchResult} {(binarySearchResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine();
    }
}
