namespace leetcode_1695;

class Program
{
    /// <summary>
    /// 1695. Maximum Erasure Value
    /// https://leetcode.com/problems/maximum-erasure-value/description/?envType=daily-question&envId=2025-07-22
    /// 1695. 删除子数组的最大得分
    /// https://leetcode.cn/problems/maximum-erasure-value/description/?envType=daily-question&envId=2025-07-22
    /// 
    /// 題目描述：
    /// 給定一個正整數陣列 nums，你可以刪除一個只包含唯一元素的子陣列。刪除該子陣列後，你獲得的分數等於其所有元素之和。
    /// 請返回你能夠通過刪除恰好一個子陣列所能獲得的最大分數。
    /// 一個陣列 b 被稱為 a 的子陣列，如果 b 形成 a 的連續子序列，即 b 等於 a[l], a[l+1], ..., a[r]，其中 (l, r) 為某個區間。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] nums = {4, 2, 4, 5, 6};
        var program = new Program();
        int result1 = program.MaximumUniqueSubarray(nums);
        int result2 = program.MaximumUniqueSubarrayV2(nums);
        Console.WriteLine($"MaximumUniqueSubarray: {result1}"); // 預期 17
        Console.WriteLine($"MaximumUniqueSubarrayV2: {result2}"); // 預期 17
    }


    /// <summary>
    /// 計算由不同元素組成的連續子陣列的最大和。
    /// 解題思路：
    /// 使用滑動窗口與 HashSet 來維護當前子陣列的唯一性。
    /// 當遇到重複元素時，將左指標右移並移除集合中的元素，
    /// 同時更新當前窗口的元素和 currSum，確保窗口內元素唯一。
    /// 每次更新最大分數 res。
    /// <example>
    /// <code>
    /// int[] nums = {4,2,4,5,6};
    /// int result = MaximumUniqueSubarray(nums); // result = 17
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="nums">正整數陣列</param>
    /// <returns>最大分數</returns>
    public int MaximumUniqueSubarray(int[] nums)
    {
        int n = nums.Length;
        // seen 用來保存目前窗口內已出現的元素
        HashSet<int> seen = new HashSet<int>();
        int res = 0; // 保存最大分數
        int currSum = 0; // 保存目前窗口內元素和
        // i 為右指標，j 為左指標
        for (int i = 0, j = 0; i < n; i++)
        {
            currSum += nums[i]; // 將新元素加入窗口和
            // 若 nums[i] 已在窗口內，則移動左指標直到移除重複元素
            while (seen.Contains(nums[i]))
            {
                seen.Remove(nums[j]); // 移除左端元素
                currSum -= nums[j]; // 更新窗口和
                j++; // 左指標右移
            }
            seen.Add(nums[i]); // 新元素加入集合
            res = Math.Max(res, currSum); // 更新最大分數
        }
        return res;
    }

    /// <summary>
    /// 解法二：使用布林陣列優化滑動窗口，提升查找速度。
    /// 解題思路：
    /// 先遍歷陣列取得最大值，建立 has 布林陣列以記錄每個元素是否在目前窗口內。
    /// 右指標遍歷陣列，若遇到重複元素，左指標右移並移除窗口內重複元素，確保窗口內元素唯一。
    /// 每次更新最大分數。
    /// 此方法適合元素範圍較小的情境，空間複雜度低於 HashSet 實作。
    /// 優點：查找速度快，空間利用率高。
    /// 缺點：若元素範圍極大，空間消耗可能增加。
    /// <example>
    /// <code>
    /// int[] nums = {4,2,4,5,6};
    /// int result = MaximumUniqueSubarrayV2(nums); // result = 17
    /// </code>
    /// </example>
    /// ref: https://leetcode.cn/problems/maximum-erasure-value/solutions/3720052/hua-dong-chuang-kou-bu-er-shu-zu-ha-xi-j-rsvy/?envType=daily-question&envId=2025-07-22
    /// 
    /// </summary>
    /// <param name="nums">正整數陣列</param>
    /// <returns>最大分數</returns>
    public int MaximumUniqueSubarrayV2(int[] nums)
    {
        // 檢查輸入是否為 null，避免例外
        if (nums is null)
        {
            throw new ArgumentNullException(nameof(nums));
        }
        // 找出陣列中的最大值，作為布林陣列 has 的大小依據
        int mx = 0;
        foreach (var x in nums)
        {
            mx = Math.Max(mx, x);
        }
        // has[x] 表示 x 是否在目前窗口內
        // 陣列大小為 mx + 1，確保能覆蓋所有可能的元素值（0~mx），與 nums 長度無關
        // 例如 nums = {9,9,9,...}，mx = 9，has[10] 足夠標記 0~9 是否出現
        // 若元素範圍極大，建議改用 HashSet<int> 以避免記憶體消耗過大
        var has = new bool[mx + 1];
        int ans = 0; // 保存最大分數
        int s = 0;   // 保存目前窗口內元素和
        int left = 0; // 左指標，指向窗口左端
        // 右指標遍歷陣列
        // 範例推導：nums = {4,2,4,5,6}
        // i=0, x=4: has[4]=false，加入窗口，s=4，ans=4
        // i=1, x=2: has[2]=false，加入窗口，s=6，ans=6
        // i=2, x=4: has[4]=true，移除 nums[left]=4，s=2，left=1，has[4]=false
        //           再加入 x=4，s=6，ans=6
        // i=3, x=5: has[5]=false，加入窗口，s=11，ans=11
        // i=4, x=6: has[6]=false，加入窗口，s=17，ans=17
        // 最終返回 ans=17
        foreach (var x in nums)
        {
            // 若 x 已在窗口內，移動左指標直到移除重複元素
            while (has[x])
            {
                has[nums[left]] = false; // 移除左端元素
                s -= nums[left];        // 更新窗口和
                left++;                 // 左指標右移
            }
            has[x] = true; // 新元素加入窗口
            s += x;        // 更新窗口和
            ans = Math.Max(ans, s); // 更新最大分數
        }
        return ans;
    }
}
