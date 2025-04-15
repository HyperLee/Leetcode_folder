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
        Console.WriteLine("300. 最長遞增子序列測試");
        Console.WriteLine("========================");

        // 測試案例 1: [10, 9, 2, 5, 3, 7, 101, 18]
        // 預期結果: 4 (子序列為 [2, 3, 7, 18] 或 [2, 3, 7, 101])
        int[] test1 = { 10, 9, 2, 5, 3, 7, 101, 18 };
        TestLIS(test1, "案例 1");

        // 測試案例 2: [0, 1, 0, 3, 2, 3]
        // 預期結果: 4 (子序列為 [0, 1, 2, 3])
        int[] test2 = { 0, 1, 0, 3, 2, 3 };
        TestLIS(test2, "案例 2");

        // 測試案例 3: [7, 7, 7, 7, 7, 7, 7]
        // 預期結果: 1 (所有元素相同，最長遞增子序列長度為 1)
        int[] test3 = { 7, 7, 7, 7, 7, 7, 7 };
        TestLIS(test3, "案例 3");
        
        // 測試案例 4: [1, 3, 6, 7, 9, 4, 10, 5, 6]
        // 預期結果: 6
        int[] test4 = { 1, 3, 6, 7, 9, 4, 10, 5, 6 };
        TestLIS(test4, "案例 4");
    }

    /// <summary>
    /// 測試並比較兩種最長遞增子序列演算法
    /// </summary>
    /// <param name="nums">測試用的整數陣列</param>
    /// <param name="testName">測試案例名稱</param>
    static void TestLIS(int[] nums, string testName)
    {
        Program p = new Program();
        
        // 執行兩種演算法
        int result1 = p.LengthOfLIS(nums);
        int result2 = p.LengthOfLIS2(nums);
        
        // 顯示輸入資料
        Console.WriteLine($"\n{testName}: [{string.Join(", ", nums)}]");
        
        // 顯示結果
        Console.WriteLine($"動態規劃解法 (O(n²)) 結果: {result1}");
        Console.WriteLine($"二分搜尋優化解法 (O(n log n)) 結果: {result2}");
        
        // 驗證兩種演算法結果是否一致
        if (result1 == result2)
        {
            Console.WriteLine($"兩種演算法結果一致: {result1}");
        }
        else
        {
            Console.WriteLine($"警告: 兩種演算法結果不一致! ({result1} vs {result2})");
        }
    }

    /// <summary>
    /// 給定一個無序的整數陣列，找到其中最長上升子序列的長度。
    /// 
    /// 解題概念：動態規劃
    /// - 定義 dp[i] 為以 nums[i] 結尾的最長遞增子序列長度
    /// - 對於每個位置 i，檢查所有 j < i 的位置，如果 nums[i] > nums[j]，則可以將 nums[i] 接在以 nums[j] 結尾的子序列後面
    /// - 時間複雜度：O(n²)，空間複雜度：O(n)
    /// </summary>
    /// <param name="nums"></param>
    /// <returns>最長上升子序列的長度</returns>
    public int LengthOfLIS(int[] nums)
    {
        // 取得數組長度
        int n = nums.Length;
        
        // 處理邊界情況：如果數組為空，最長遞增子序列長度為0
        if (n == 0) return 0;
        
        // 初始化動態規劃數組，dp[i] 表示以 nums[i] 結尾的最長遞增子序列的長度
        int[] dp = new int[n];
        
        // 初始化所有位置的最長遞增子序列長度為1（至少包含自身元素）
        Array.Fill(dp, 1);
        
        // 從第二個元素開始遍歷數組
        for (int i = 1; i < n; i++)
        {
            // 對於每個位置i，檢查之前的所有位置j
            for (int j = 0; j < i; j++)
            {
                // 如果當前元素大於之前的某個元素，可以形成更長的遞增子序列
                if (nums[i] > nums[j])
                {
                    // 更新dp[i]：取原值與「dp[j] + 1」中的較大者
                    // dp[j] + 1 表示將nums[i]加到以nums[j]結尾的最長遞增子序列後面
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }
        }
        
        // 返回整個dp數組中的最大值，即為最長遞增子序列的長度
        return dp.Max();
    }


    /// <summary>
    /// 動態規劃 + 二分搜尋法解法
    /// 
    /// 解題概念：
    /// - 使用一個陣列 g 維護「目前為止找到的最長遞增子序列」
    /// - g 中的元素不一定是真正的最長遞增子序列，但 g 的長度就是最長遞增子序列的長度
    /// - 對於每個元素，如果它大於 g 的最後一個元素，表示可以直接添加到 g 尾部形成更長的遞增序列
    /// - 如果元素小於 g 的最後一個元素，則使用二分搜尋找到 g 中第一個大於等於它的位置，並替換該位置的值
    /// - 這樣可以保持 g 的單調遞增特性，同時「為未來可能的更長序列創造潛力」
    /// - 時間複雜度：O(n log n)，空間複雜度：O(n)
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>最長上升子序列的長度</returns>
    public int LengthOfLIS2(int[] nums)
    {
        // 創建一個動態陣列 g，用於存儲目前為止的「最長遞增子序列候選」
        List<int> g = new List<int>();
        
        // 遍歷輸入陣列中的每個元素
        foreach (var num in nums)
        {
            // 情況1: g 為空或當前數字大於 g 的最後一個元素
            // 可以直接將當前數字添加到 g 尾部，形成更長的遞增序列
            if (g.Count == 0 || num > g[g.Count - 1])
            {
                g.Add(num);
            }
            else
            {
                // 情況2: 當前數字不大於 g 的最後一個元素
                // 使用二分搜尋找到 g 中第一個大於等於 num 的位置
                int index = LowerBound(g, num);
                // 用 num 替換該位置的值，保持 g 的遞增特性
                // 這樣可以為未來可能出現的更長序列創造潛力
                g[index] = num;
            }
        }

        // g 的長度即為最長遞增子序列的長度
        return g.Count;
    }

    /// <summary>
    /// 二分搜尋法實作：找出陣列中第一個大於等於目標值的元素位置
    /// 
    /// 解題概念：
    /// - 使用二分搜尋來降低搜尋時間複雜度，從 O(n) 降到 O(log n)
    /// - 在遞增有序陣列中查找第一個大於等於目標值的元素的索引位置
    /// - 當找不到大於等於目標值的元素時，返回陣列長度
    /// </summary>
    /// <param name="g">遞增有序的整數陣列</param>
    /// <param name="target">要查找的目標值</param>
    /// <returns>第一個大於等於目標值的元素位置</returns>
    private int LowerBound(List<int> g, int target)
    {
        // 初始化左右指針
        int left = 0, right = g.Count - 1;
        
        // 使用二分搜尋循環，當左指針超過右指針時結束
        while (left <= right)
        {
            // 計算中間位置，避免整數溢出的寫法
            int mid = left + (right - left) / 2;
            
            // 如果中間元素小於目標值，目標位置在右半部
            if (g[mid] < target)
            {
                // 將搜尋範圍縮小到右半部
                left = mid + 1;
            }
            else
            {
                // 如果中間元素大於等於目標值，目標位置在左半部或就是中間位置
                // 繼續向左搜尋以找到第一個符合條件的位置
                right = mid - 1;
            }
        }
        
        // 循環結束後，left 指向第一個大於等於目標值的位置
        return left;
    }
}
