namespace leetcode_033;

internal class Program
{
    /// <summary>
    /// 33. Search in Rotated Sorted Array
    /// https://leetcode.com/problems/search-in-rotated-sorted-array/description/
    /// 33. 搜索旋转排序数组
    /// https://leetcode.cn/problems/search-in-rotated-sorted-array/description/
    ///
    /// English:
    /// There is an integer array nums sorted in ascending order (with distinct values).
    ///
    /// Prior to being passed to your function, nums is possibly left rotated at an unknown index k
    /// (1 &lt;= k &lt; nums.length) such that the resulting array is
    /// [nums[k], nums[k+1], ..., nums[n-1], nums[0], nums[1], ..., nums[k-1]] (0-indexed).
    /// For example, [0,1,2,4,5,6,7] might be left rotated by 3 indices and become
    /// [4,5,6,7,0,1,2].
    ///
    /// Given the array nums after the possible rotation and an integer target, return the index of
    /// target if it is in nums, or -1 if it is not in nums.
    ///
    /// You must write an algorithm with O(log n) runtime complexity.
    ///
    /// 繁體中文:
    /// 有一個整數陣列 nums，原本依照遞增順序排序，且所有值都不重複。
    ///
    /// 在傳入你的函式之前，nums 可能會在未知索引 k 處被向左旋轉
    /// (1 &lt;= k &lt; nums.length)，使得產生的陣列為
    /// [nums[k], nums[k+1], ..., nums[n-1], nums[0], nums[1], ..., nums[k-1]] (以 0 為索引起點)。
    /// 例如，[0,1,2,4,5,6,7] 可能向左旋轉 3 個索引，變成 [4,5,6,7,0,1,2]。
    ///
    /// 給定可能已被旋轉後的陣列 nums，以及一個整數 target，如果 target 存在於 nums 中，
    /// 回傳它的索引；如果不存在，則回傳 -1。
    ///
    /// 你必須撰寫一個時間複雜度為 O(log n) 的演算法。
    /// </summary>
    private static void Main()
    {
        Program solution = new();
        (int[] Nums, int Target, int Expected)[] examples =
        [
            ([4, 5, 6, 7, 0, 1, 2], 0, 4),
            ([4, 5, 6, 7, 0, 1, 2], 3, -1),
            ([1], 0, -1),
        ];

        for (int index = 0; index < examples.Length; index++)
        {
            (int[] nums, int target, int expected) = examples[index];
            int linearResult = solution.Search(nums, target);
            int binaryResult = solution.Search2(nums, target);
            int orderedHalfResult = solution.Search3(nums, target);

            Console.WriteLine($"Example {index + 1}: nums = [{string.Join(", ", nums)}], target = {target}");
            Console.WriteLine($"  Search  => {linearResult} (expected: {expected})");
            Console.WriteLine($"  Search2 => {binaryResult} (expected: {expected})");
            Console.WriteLine($"  Search3 => {orderedHalfResult} (expected: {expected})");
        }
    }

    /// <summary>
    /// 使用線性搜尋逐一比對旋轉排序陣列中的元素。
    /// 解題概念是從索引 0 掃描到陣列尾端，遇到第一個等於 <paramref name="target" /> 的元素就回傳其索引。
    /// 輸入條件為 <paramref name="nums" /> 至少包含一個整數，且元素值不重複；輸出為目標值索引，不存在時回傳 -1。
    /// </summary>
    /// <param name="nums">可能已旋轉的遞增排序整數陣列，元素值不重複。</param>
    /// <param name="target">要查找的目標整數。</param>
    /// <returns>若找到 <paramref name="target" /> 則回傳其索引，否則回傳 -1。</returns>
    public int Search(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 使用二分搜尋在旋轉排序陣列中查找目標值。
    /// 解題概念是先找出最小值索引，也就是旋轉後第二段遞增區間的起點，再依 <paramref name="target" />
    /// 與陣列最後一個元素的大小關係判斷目標落在哪一段，最後只在該遞增區間內做 lower bound 搜尋。
    /// 輸入條件為 <paramref name="nums" /> 至少包含一個整數，且元素值不重複；輸出為目標值索引，不存在時回傳 -1。
    /// </summary>
    /// <param name="nums">可能已旋轉的遞增排序整數陣列，元素值不重複。</param>
    /// <param name="target">要查找的目標整數。</param>
    /// <returns>若找到 <paramref name="target" /> 則回傳其索引，否則回傳 -1。</returns>
    public int Search2(int[] nums, int target)
    {
        int n = nums.Length;
        int i = FindMin(nums);

        // 陣列最後一個值屬於右側遞增段；比它大的 target 只可能在左側遞增段。
        if (target > nums[n - 1])
        {
            // 在開區間 (-1, i) 內搜尋實際索引 [0, i - 1]。
            return LowerBound(nums, -1, i, target);
        }

        // 其餘 target 只可能在右側遞增段，實際索引範圍是 [i, n - 1]。
        return LowerBound(nums, i - 1, n, target);
    }

    /// <summary>
    /// 使用二分搜尋找出旋轉排序陣列中的最小值索引。
    /// 解題概念是利用 <paramref name="nums" /> 最後一個元素作為右側遞增段的判斷基準：
    /// 小於最後一個元素的值位於右側遞增段，否則位於左側遞增段。搜尋結束時右邊界即為最小值索引。
    /// 輸入條件為 <paramref name="nums" /> 至少包含一個整數，且元素值不重複；輸出為最小值所在索引。
    /// </summary>
    /// <param name="nums">可能已旋轉的遞增排序整數陣列，元素值不重複。</param>
    /// <returns><paramref name="nums" /> 中最小值所在的索引。</returns>
    public int FindMin(int[] nums)
    {
        int n = nums.Length;
        int left = -1;
        // 使用開區間 (-1, n - 1)；right 一開始指向最後一個元素，代表已知的右側遞增段位置。
        int right = n - 1;

        while (left + 1 < right)
        {
            int mid = left + (right - left) / 2;

            // nums[mid] 小於最後一個值，表示 mid 已落在包含最小值的右側遞增段。
            if (nums[mid] < nums[n - 1])
            {
                right = mid;
            }
            else
            {
                left = mid;
            }
        }
        return right;
    }

    /// <summary>
    /// 在指定的遞增區間內尋找第一個大於或等於目標值的位置，並確認該位置是否等於目標值。
    /// 解題概念是維護開區間 (<paramref name="left" />, <paramref name="right" />)，
    /// 其中左界代表小於 <paramref name="target" /> 的位置，右界代表可能大於或等於
    /// <paramref name="target" /> 的位置。搜尋結束後若右界元素等於目標值就回傳索引，否則回傳 -1。
    /// 輸入條件為指定搜尋區間中的元素已依遞增排序；輸出為目標值索引，不存在時回傳 -1。
    /// </summary>
    /// <param name="nums">整數陣列，其中指定搜尋區間必須為遞增排序。</param>
    /// <param name="left">開區間左邊界，不包含在搜尋範圍內。</param>
    /// <param name="right">開區間右邊界，不包含在搜尋範圍內，可作為尾端哨兵。</param>
    /// <param name="target">要查找的目標整數。</param>
    /// <returns>若找到 <paramref name="target" /> 則回傳其索引，否則回傳 -1。</returns>
    public int LowerBound(int[] nums, int left, int right, int target)
    {
        // 維持開區間不變量：left 側排除小於 target 的值，right 側保留第一個可能 >= target 的位置。
        while (left + 1 < right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] < target)
            {
                // 範圍縮小到 (mid, right)
                left = mid;
            }
            else
            {
                // 範圍縮小到 (left, mid)
                right = mid;
            }
        }
        return nums[right] == target ? right : -1;
    }

    /// <summary>
    /// 使用每次判斷有序半邊的二分搜尋，在旋轉排序陣列中查找目標值。
    /// 解題概念是將 <c>mid</c> 作為分割點：旋轉排序陣列被切成兩半時，至少會有一半仍然保持遞增。
    /// 若 <c>mid</c> 落在左側遞增段，就用 <c>[nums[0], nums[mid])</c> 判斷
    /// <paramref name="target" /> 是否應往左找；否則 <c>mid</c> 落在右側遞增段，
    /// 再用 <c>(nums[mid], nums[n - 1]]</c> 判斷是否應往右找。
    /// 每輪都能排除一半搜尋範圍，因此時間複雜度為 O(log n)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">可能已旋轉的遞增排序整數陣列，元素值不重複。</param>
    /// <param name="target">要查找的目標整數。</param>
    /// <returns>若找到 <paramref name="target" /> 則回傳其索引，否則回傳 -1。</returns>
    public int Search3(int[] nums, int target)
    {
        int n = nums.Length;
        if (n == 0)
        {
            return -1;
        }

        if (n == 1)
        {
            return nums[0] == target ? 0 : -1;
        }

        int l = 0;
        int r = n - 1;
        while (l <= r)
        {
            int mid = l + (r - l) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }

            // nums[0] <= nums[mid] 表示 mid 位於左側遞增段，左半邊可用有序區間判斷。
            if (nums[0] <= nums[mid])
            {
                // target 若落在 [nums[0], nums[mid])，代表答案只可能在 mid 左側。
                if (nums[0] <= target && target < nums[mid])
                {
                    r = mid - 1;
                }
                else
                {
                    l = mid + 1;
                }
            }
            else
            {
                // mid 位於右側遞增段；target 若落在 (nums[mid], nums[n - 1]]，就往右側找。
                if (nums[mid] < target && target <= nums[n - 1])
                {
                    l = mid + 1;
                }
                else
                {
                    r = mid - 1;
                }
            }
        }
        return -1;
    }
}
