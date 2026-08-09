namespace leetcode_081;

class Program
{
    /// <summary>
    /// <para>
    /// 81. Search in Rotated Sorted Array II
    /// https://leetcode.com/problems/search-in-rotated-sorted-array-ii/
    ///
    /// There is an integer array nums sorted in non-decreasing order (not necessarily with distinct values).
    /// Before being passed to your function, nums is rotated at an unknown pivot index k
    /// (0 &lt;= k &lt; nums.length) such that the resulting array is
    /// [nums[k], nums[k+1], ..., nums[n-1], nums[0], nums[1], ..., nums[k-1]] (0-indexed).
    /// For example, [0,1,2,4,4,4,5,6,6,7] might be rotated at pivot index 5 and become
    /// [4,5,6,6,7,0,1,2,4,4].
    /// Given the array nums after the rotation and an integer target, return true if target is in nums,
    /// or false if it is not in nums. You must decrease the overall operation steps as much as possible.
    ///
    /// Example 1:
    /// Input: nums = [2,5,6,0,0,1,2], target = 0
    /// Output: true
    ///
    /// Example 2:
    /// Input: nums = [2,5,6,0,0,1,2], target = 3
    /// Output: false
    ///
    /// Constraints:
    /// 1 &lt;= nums.length &lt;= 5000
    /// -10^4 &lt;= nums[i] &lt;= 10^4
    /// nums is guaranteed to be rotated at some pivot.
    /// -10^4 &lt;= target &lt;= 10^4
    ///
    /// Follow up: This problem is similar to Search in Rotated Sorted Array, but nums may contain duplicates.
    /// Would this affect the runtime complexity? How and why?
    /// </para>
    /// <para>
    /// 81. 搜尋旋轉排序陣列 II
    /// https://leetcode.cn/problems/search-in-rotated-sorted-array-ii/
    ///
    /// 有一個依非遞減順序排序的整數陣列 nums，其中的值不一定互不相同。
    /// 在傳入函式之前，nums 會在未知的樞紐索引 k（0 &lt;= k &lt; nums.length）處旋轉，
    /// 使結果陣列變成 [nums[k], nums[k+1], ..., nums[n-1], nums[0], nums[1], ..., nums[k-1]]
    ///（索引從 0 開始）。例如，[0,1,2,4,4,4,5,6,6,7] 可能在樞紐索引 5 處旋轉，
    /// 並變成 [4,5,6,6,7,0,1,2,4,4]。
    /// 給定旋轉後的陣列 nums 與整數 target，若 target 位於 nums 中則回傳 true，否則回傳 false。
    /// 你必須盡可能減少整體操作步驟。
    ///
    /// 範例 1：
    /// 輸入：nums = [2,5,6,0,0,1,2], target = 0
    /// 輸出：true
    ///
    /// 範例 2：
    /// 輸入：nums = [2,5,6,0,0,1,2], target = 3
    /// 輸出：false
    ///
    /// 限制條件：
    /// 1 &lt;= nums.length &lt;= 5000
    /// -10^4 &lt;= nums[i] &lt;= 10^4
    /// nums 保證已在某個樞紐處旋轉。
    /// -10^4 &lt;= target &lt;= 10^4
    ///
    /// 進階：本題與「搜尋旋轉排序陣列」相似，但 nums 可能包含重複值。
    /// 這會影響執行時間複雜度嗎？為什麼？如何影響？
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數。</param>
    /// <summary>
    /// 主程式進入點，執行多組測資並比較四種解法結果是否一致。
    /// 解題概念是用固定案例同時驗證正確性與不同方法輸出一致性。
    /// 輸入為命令列參數（本範例未使用），輸出為每組案例的判斷結果報告。
    /// </summary>
    static void Main(string[] args)
    {
        var solver = new Program();
        var testCases = new (int[] nums, int target, bool expected, string note)[]
        {
            (new[] { 2, 5, 6, 0, 0, 1, 2 }, 0, true, "LeetCode 範例：目標存在"),
            (new[] { 2, 5, 6, 0, 0, 1, 2 }, 3, false, "LeetCode 範例：目標不存在"),
            (new[] { 1, 0, 1, 1, 1 }, 0, true, "重複值干擾有序區判斷"),
            (new[] { 1, 1, 1, 1, 1 }, 2, false, "全重複但目標不存在"),
            (new[] { 1 }, 1, true, "單一元素命中"),
            (new[] { 1 }, 0, false, "單一元素未命中"),
        };

        Console.WriteLine("LeetCode 81 - Search in Rotated Sorted Array II");
        Console.WriteLine(new string('-', 70));

        for (int i = 0; i < testCases.Length; i++)
        {
            var testCase = testCases[i];

            bool r1 = solver.Search(testCase.nums, testCase.target);
            bool r2 = solver.Search2(testCase.nums, testCase.target);
            bool r3 = solver.Search3(testCase.nums, testCase.target);
            bool r4 = solver.Search4(testCase.nums, testCase.target);
            bool allEqual = r1 == r2 && r2 == r3 && r3 == r4;

            Console.WriteLine($"Case {i + 1}: {testCase.note}");
            Console.WriteLine($"  nums    = [{string.Join(", ", testCase.nums)}]");
            Console.WriteLine($"  target  = {testCase.target}");
            Console.WriteLine($"  expected= {testCase.expected}");
            Console.WriteLine($"  Search  = {r1}, Search2 = {r2}, Search3 = {r3}, Search4 = {r4}");
            Console.WriteLine($"  pass    = {r4 == testCase.expected}, consistent = {allEqual}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 以線性掃描方式搜尋目標值。
    /// 解題概念是逐一比對每個元素，遇到目標就提早回傳 true。
    /// 輸入可為任意整數陣列（含重複值與旋轉），輸出為目標是否存在。
    /// </summary>
    /// <param name="nums">旋轉後整數陣列。</param>
    /// <param name="target">欲搜尋的目標值。</param>
    /// <returns>若 target 出現在 nums 中回傳 true，否則回傳 false。</returns>
    public bool Search(int[] nums, int target)
    {
        int n = nums.Length;
        for (int i = 0; i < n; i++)
        {
            if (nums[i] == target)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 二分搜尋解法（處理重複值版本）。
    /// 核心概念是先判斷哪一半有序，再決定保留區間；
    /// 當 left/mid/right 三者值相同時無法判定有序區，只能同時收縮左右邊界。
    /// 輸入需為原先非遞減排序、旋轉後的陣列；輸出為目標是否存在。
    /// </summary>
    /// <param name="nums">旋轉後整數陣列。</param>
    /// <param name="target">欲搜尋的目標值。</param>
    /// <returns>若 target 出現在 nums 中回傳 true，否則回傳 false。</returns>
    public bool Search2(int[] nums, int target)
    {
        int n = nums.Length;
        if (n == 0)
        {
            return false;
        }

        if (n == 1)
        {
            return nums[0] == target;
        }

        int left = 0;
        int right = n - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == target)
            {
                return true;
            }

            // 無法判斷哪半邊有序，只能縮小搜尋邊界以去除重複值干擾。
            if (nums[left] == nums[mid] && nums[mid] == nums[right])
            {
                left++;
                right--;
            }
            else if (nums[left] <= nums[mid])
            {
                // 左半邊有序，判斷 target 是否落在左半邊範圍。
                if (nums[left] <= target && target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            {
                // 右半邊有序，判斷 target 是否落在右半邊範圍。
                if (nums[mid] < target && target <= nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 開區間二分解法，透過與右端點比較決定淘汰方向。
    /// 概念是把 right 視為參考端點，當 nums[mid] 等於 nums[right] 時先丟棄 right 以去重，
    /// 否則用 check 判斷 mid 是否位於可能包含 target 的區間邊界內。
    /// 輸入需為旋轉非遞減陣列；輸出為目標是否存在。
    /// </summary>
    /// <param name="nums">旋轉後整數陣列。</param>
    /// <param name="target">欲搜尋的目標值。</param>
    /// <returns>若 target 出現在 nums 中回傳 true，否則回傳 false。</returns>
    public bool Search3(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return false;
        }

        int left = -1;
        int right = nums.Length - 1; // 開區間 (-1, n - 1)

        while (left + 1 < right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == nums[right])
            {
                // 去除重複尾值，避免無法判斷旋轉分界。
                right--;
            }
            else if (check(nums, target, right, mid))
            {
                right = mid;
            }
            else
            {
                left = mid;
            }
        }

        return nums[right] == target;
    }

    /// <summary>
    /// 判斷 nums[i] 是否應保留在當前二分收斂的右界區間。
    /// 透過 nums[right] 區分旋轉前後兩段，再結合 target 相對位置決定淘汰方向。
    /// 輸入要求 right 與 i 為合法索引；輸出 true 代表可將右邊界收斂至 i。
    /// </summary>
    /// <param name="nums">旋轉後整數陣列。</param>
    /// <param name="target">欲搜尋的目標值。</param>
    /// <param name="right">目前右邊界索引。</param>
    /// <param name="i">待判斷索引。</param>
    /// <returns>若索引 i 應位於保留區間中回傳 true，否則回傳 false。</returns>
    private bool check(int[] nums, int target, int right, int i)
    {
        int x = nums[i];
        if (x > nums[right])
        {
            return target > nums[right] && x >= target;
        }

        return target > nums[right] || x >= target;
    }

    /// <summary>
    /// 先找旋轉點，再對兩段有序區做標準二分搜尋。
    /// 核心是先縮掉尾端重複值以恢復二段性，接著在左右兩段分別查找 target。
    /// 輸入需為旋轉非遞減陣列；輸出為目標是否存在。
    /// </summary>
    /// <param name="nums">旋轉後整數陣列。</param>
    /// <param name="target">欲搜尋的目標值。</param>
    /// <returns>若 target 出現在 nums 中回傳 true，否則回傳 false。</returns>
    public bool Search4(int[] nums, int target)
    {
        int n = nums.Length;
        if (n == 0)
        {
            return false;
        }

        int l = 0;
        int r = n - 1;

        // 先削掉尾端與首元素相同的重複值，讓旋轉分界更容易判定。
        while (l < r && nums[0] == nums[r])
        {
            r--;
        }

        // 第一次二分：找第一段有序區尾端。
        while (l < r)
        {
            int mid = (l + r + 1) >> 1;
            if (nums[mid] >= nums[0])
            {
                l = mid;
            }
            else
            {
                r = mid - 1;
            }
        }

        int idx = n;
        if (nums[r] >= nums[0] && r + 1 < n)
        {
            // idx 為第二段起點（旋轉後最小值位置）。
            idx = r + 1;
        }

        int ans = Find(nums, 0, idx - 1, target);
        if (ans != -1)
        {
            return true;
        }

        ans = Find(nums, idx, n - 1, target);
        return ans != -1;
    }

    /// <summary>
    /// 在閉區間 [l, r] 內執行 lower-bound 風格二分搜尋。
    /// 概念是收斂到第一個大於等於 target 的位置，再判斷是否等於 target。
    /// 輸入需滿足 [l, r] 為非遞減有序區間（可為空）；輸出為索引或 -1。
    /// </summary>
    /// <param name="nums">待查詢陣列。</param>
    /// <param name="l">搜尋左邊界（含）。</param>
    /// <param name="r">搜尋右邊界（含）。</param>
    /// <param name="target">欲搜尋目標值。</param>
    /// <returns>找到 target 回傳其索引，否則回傳 -1。</returns>
    private int Find(int[] nums, int l, int r, int target)
    {
        if (l > r)
        {
            return -1;
        }

        while (l < r)
        {
            int mid = (l + r) >> 1;
            if (nums[mid] >= target)
            {
                r = mid;
            }
            else
            {
                l = mid + 1;
            }
        }

        return nums[r] == target ? r : -1;
    }
}
