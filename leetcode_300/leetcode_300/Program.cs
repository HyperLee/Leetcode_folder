namespace leetcode_300;

class Program
{
    /// <summary>
    /// 300. Longest Increasing Subsequence
    /// https://leetcode.com/problems/longest-increasing-subsequence/description/?envType=problem-list-v2&envId=oizxjoit
    /// 300. 最长递增子序列
    /// https://leetcode.cn/problems/longest-increasing-subsequence/solutions/147667/zui-chang-shang-sheng-zi-xu-lie-by-leetcode-soluti/
    /// 
    /// 題目描述：
    /// 給定一個無序的整數陣列，找到其中最長上升子序列的長度。
    /// 
    /// 解題提示：
    /// 1. 可以使用動態規劃 (DP) 解法，時間複雜度 O(n²)
    /// 2. 最佳解法為結合動態規劃和二分搜尋，時間複雜度 O(n log n)
    /// 3. 子序列不必連續，但必須保持原始順序
    /// 
    /// 滑動視窗方法不適合解決這個問題，因為：
    /// - 滑動視窗方法通常用於處理連續子序列的問題，而這個問題要求的是「最長上升子序列」，不一定是連續的。
    /// - 滑動視窗方法無法有效地處理非連續的元素，因為它只能在固定的範圍內移動，無法跳過某些元素。
    /// 
    /// 純粹的二分法也不適合直接解決這個問題，因為：
    /// - 二分搜尋法通常用於查找已排序的數組中的元素，而這個問題需要在無序的整數陣列中找到最長上升子序列。
    /// - 雖然可以使用二分搜尋法來優化動態規劃的解法，但這需要結合其他技術（如維護一個「目前為止的最長遞增子序列」）來實現。
    ///   本題中的輸入陣列是無序的
    ///   我們需要「建構」遞增子序列，而不只是在固定陣列中搜尋
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        SampleCase[] samples =
        [
            new("案例 1：官方範例一", [10, 9, 2, 5, 3, 7, 101, 18], 4),
            new("案例 2：官方範例二", [0, 1, 0, 3, 2, 3], 4),
            new("案例 3：所有元素相同", [7, 7, 7, 7, 7, 7, 7], 1),
            new("案例 4：混合上升路徑", [1, 3, 6, 7, 9, 4, 10, 5, 6], 6),
            new("案例 5：空陣列防禦", [], 0),
            new("案例 6：單一元素", [42], 1),
            new("案例 7：嚴格遞減", [5, 4, 3, 2, 1], 1)
        ];

        Program program = new();
        int passedChecks = 0;
        int totalChecks = samples.Length * 2;

        Console.WriteLine("300. 最長遞增子序列測試");
        Console.WriteLine("========================");

        foreach (SampleCase sample in samples)
        {
            int dynamicProgrammingResult = program.LengthOfLIS(sample.Numbers);
            int binarySearchResult = program.LengthOfLIS2(sample.Numbers);
            bool dynamicProgrammingPassed = dynamicProgrammingResult == sample.Expected;
            bool binarySearchPassed = binarySearchResult == sample.Expected;

            passedChecks += dynamicProgrammingPassed ? 1 : 0;
            passedChecks += binarySearchPassed ? 1 : 0;

            Console.WriteLine();
            Console.WriteLine(sample.Name);
            Console.WriteLine($"輸入：[{string.Join(", ", sample.Numbers)}]");
            Console.WriteLine($"預期：{sample.Expected}");
            Console.WriteLine(
                $"動態規劃：Expected = {sample.Expected}, Actual = {dynamicProgrammingResult} => " +
                $"{(dynamicProgrammingPassed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"二分搜尋：Expected = {sample.Expected}, Actual = {binarySearchResult} => " +
                $"{(binarySearchPassed ? "PASS" : "FAIL")}");
        }

        Console.WriteLine();
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 使用動態規劃計算整數陣列的最長嚴格遞增子序列長度。
    /// 以 <c>dp[i]</c> 表示以 <c>nums[i]</c> 結尾的最佳長度，逐一檢查
    /// 所有較早且數值較小的元素；時間複雜度為 O(n²)，空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums">
    /// 不可為 <see langword="null"/> 的整數陣列；題目限制長度至少為 1，
    /// 此實作亦防禦性支援空陣列。
    /// </param>
    /// <returns>最長嚴格遞增子序列的長度；空陣列回傳 0。</returns>
    public int LengthOfLIS(int[] nums)
    {
        int n = nums.Length;

        if (n == 0)
        {
            return 0;
        }

        int[] dp = new int[n];
        Array.Fill(dp, 1);

        int maxLength = 1;

        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (nums[i] > nums[j])
                {
                    // nums[i] 可以接到以 nums[j] 結尾的序列後方，形成更長候選。
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }

            maxLength = Math.Max(maxLength, dp[i]);
        }

        return maxLength;
    }

    /// <summary>
    /// 使用 tails 陣列搭配二分搜尋，計算整數陣列的最長嚴格遞增子序列長度。
    /// tails 的第 i 個值代表長度為 i + 1 的遞增子序列目前可取得的最小尾值；
    /// 每個元素以 lower bound 決定追加或替換位置。時間複雜度為 O(n log n)，
    /// 空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums">
    /// 不可為 <see langword="null"/> 的整數陣列；題目限制長度至少為 1，
    /// 此實作亦防禦性支援空陣列。
    /// </param>
    /// <returns>最長嚴格遞增子序列的長度；空陣列回傳 0。</returns>
    public int LengthOfLIS2(int[] nums)
    {
        List<int> tails = [];

        foreach (int num in nums)
        {
            if (tails.Count == 0 || num > tails[^1])
            {
                tails.Add(num);
            }
            else
            {
                // 以更小或相同的尾值取代原位置，保留未來延伸成更長序列的空間。
                int index = LowerBound(tails, num);
                tails[index] = num;
            }
        }

        // tails 不保證是一條實際子序列，但其長度必定等於 LIS 長度。
        return tails.Count;
    }

    /// <summary>
    /// 在遞增排列的 tails 中，以二分搜尋尋找第一個大於或等於目標值的位置，
    /// 讓呼叫端能以較小尾值替換該位置並維持遞增順序。
    /// </summary>
    /// <param name="tails">不可為空且遞增排列的尾值串列。</param>
    /// <param name="target">要插入或替換的目標值。</param>
    /// <returns>第一個大於或等於 <paramref name="target"/> 的索引；若不存在則回傳串列長度。</returns>
    private int LowerBound(List<int> tails, int target)
    {
        int left = 0;
        int right = tails.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (tails[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                // mid 已符合條件，繼續向左收斂以確認是否有更早的位置。
                right = mid - 1;
            }
        }

        return left;
    }

    private sealed record SampleCase(string Name, int[] Numbers, int Expected);
}
