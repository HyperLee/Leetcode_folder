namespace leetcode_3381;

class Program
{
    /// <summary>
    /// 3381. Maximum Subarray Sum With Length Divisible by K
    /// https://leetcode.com/problems/maximum-subarray-sum-with-length-divisible-by-k/description/
    /// <para>
    /// You are given an integer array nums and an integer k.
    ///
    /// Return the maximum sum of a nums subarray whose length is divisible by k.
    ///
    /// Example 1:
    /// Input: nums = [1,2], k = 1
    /// Output: 3
    /// Explanation: Subarray [1,2] has sum 3 and length 2, which is divisible by 1.
    ///
    /// Example 2:
    /// Input: nums = [-1,-2,-3,-4,-5], k = 4
    /// Output: -10
    /// Explanation: The maximum-sum subarray is [-1,-2,-3,-4]. Its length 4 is divisible by 4.
    ///
    /// Example 3:
    /// Input: nums = [-5,1,2,-3,4], k = 2
    /// Output: 4
    /// Explanation: The maximum-sum subarray is [1,2,-3,4]. Its length 4 is divisible by 2.
    ///
    /// Constraints:
    /// - 1 &lt;= k &lt;= nums.length &lt;= 2 * 10^5
    /// - -10^9 &lt;= nums[i] &lt;= 10^9
    /// </para>
    /// <para>
    /// 3381. 長度可被 K 整除的最大子陣列總和
    /// https://leetcode.cn/problems/maximum-subarray-sum-with-length-divisible-by-k/description/
    ///
    /// 給定整數陣列 nums 與整數 k。
    ///
    /// 回傳 nums 中長度可被 k 整除之子陣列的最大總和。
    ///
    /// 範例 1：
    /// 輸入：nums = [1,2], k = 1
    /// 輸出：3
    /// 解釋：子陣列 [1,2] 的總和為 3、長度為 2，可被 1 整除。
    ///
    /// 範例 2：
    /// 輸入：nums = [-1,-2,-3,-4,-5], k = 4
    /// 輸出：-10
    /// 解釋：最大總和子陣列為 [-1,-2,-3,-4]，其長度 4 可被 4 整除。
    ///
    /// 範例 3：
    /// 輸入：nums = [-5,1,2,-3,4], k = 2
    /// 輸出：4
    /// 解釋：最大總和子陣列為 [1,2,-3,4]，其長度 4 可被 2 整除。
    ///
    /// 限制條件：
    /// - 1 &lt;= k &lt;= nums.length &lt;= 2 * 10^5
    /// - -10^9 &lt;= nums[i] &lt;= 10^9
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試範例 1: nums = [1, 2], k = 1
        // 預期輸出: 3 (子陣列 [1, 2] 長度為 2，可被 1 整除，總和為 3)
        int[] nums1 = [1, 2];
        int k1 = 1;
        Console.WriteLine($"範例 1: nums = [{string.Join(", ", nums1)}], k = {k1}");
        Console.WriteLine($"結果: {solution.MaxSubarraySum(nums1, k1)}");
        Console.WriteLine();

        // 測試範例 2: nums = [-1, -2, -3, -4, -5], k = 4
        // 預期輸出: -10 (子陣列 [-1, -2, -3, -4] 長度為 4，可被 4 整除，總和為 -10)
        int[] nums2 = [-1, -2, -3, -4, -5];
        int k2 = 4;
        Console.WriteLine($"範例 2: nums = [{string.Join(", ", nums2)}], k = {k2}");
        Console.WriteLine($"結果: {solution.MaxSubarraySum(nums2, k2)}");
        Console.WriteLine();

        // 測試範例 3: nums = [-5, 1, 2, -3, 4], k = 2
        // 預期輸出: 4 (子陣列 [1, 2, -3, 4] 長度為 4，可被 2 整除，總和為 4)
        int[] nums3 = [-5, 1, 2, -3, 4];
        int k3 = 2;
        Console.WriteLine($"範例 3: nums = [{string.Join(", ", nums3)}], k = {k3}");
        Console.WriteLine($"結果: {solution.MaxSubarraySum(nums3, k3)}");
    }

    /// <summary>
    /// 使用前綴和（Prefix Sum）方法求解長度可被 K 整除的子陣列最大總和。
    /// 
    /// <para>
    /// <b>解題思路：</b>
    /// </para>
    /// <para>
    /// 1. 令前綴和 prefixSum[i] = nums[0] + nums[1] + ... + nums[i]。
    /// </para>
    /// <para>
    /// 2. 區間 [j, i] 的子陣列和為：sum(j, i) = prefixSum[i] - prefixSum[j-1]。
    /// </para>
    /// <para>
    /// 3. 題目要求子陣列長度 (i - j + 1) 能被 k 整除，即 (i - j + 1) % k = 0。
    /// </para>
    /// <para>
    /// 4. 推導：i % k = (j - 1) % k，表示索引 i 和 j-1 對 k 同餘。
    /// </para>
    /// <para>
    /// 5. 使用 kSum[l] 記錄所有與 l 同餘的前綴和最小值。
    /// </para>
    /// <para>
    /// 6. 對於每個 i，找到同餘的最小前綴和 kSum[i % k]，計算 prefixSum[i] - kSum[i % k] 即為最大子陣列和。
    /// </para>
    /// 
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int[] nums = [1, 2];
    /// int k = 1;
    /// long result = solution.MaxSubarraySum(nums, k); // 回傳 3
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <param name="k">子陣列長度必須能被此值整除</param>
    /// <returns>長度可被 k 整除的子陣列的最大總和</returns>
    public long MaxSubarraySum(int[] nums, int k)
    {
        int n = nums.Length;

        // 累計前綴和：prefixSum[i] = nums[0] + nums[1] + ... + nums[i]
        long prefixSum = 0;

        // 儲存最大子陣列和的結果，初始化為最小值
        long maxSum = long.MinValue;

        // kSum[l] 記錄所有索引 % k = l 的前綴和中的最小值
        // 用於快速找到同餘位置的最小前綴和
        long[] kSum = new long[k];

        // 初始化 kSum 為極大值，避免在計算差值時產生溢位
        for (int i = 0; i < k; i++)
        {
            kSum[i] = long.MaxValue / 2;
        }

        // 重要：kSum[k-1] = 0 代表「空前綴」的情況
        // 當子陣列從索引 0 開始且長度為 k 的倍數時，需要減去的前綴和為 0
        // 因為索引 k-1 對應到「前 k 個元素」的情況，其前綴和減去 0 即為原始前綴和
        kSum[k - 1] = 0;

        // 遍歷每個元素，計算前綴和並更新最大值
        for (int i = 0; i < n; i++)
        {
            // 累加當前元素到前綴和
            prefixSum += nums[i];

            // 計算以 i 為結尾、長度為 k 的倍數的子陣列最大和
            // prefixSum[i] - kSum[i % k] 表示從某個同餘位置到 i 的子陣列和
            maxSum = Math.Max(maxSum, prefixSum - kSum[i % k]);

            // 更新 kSum[i % k]，保留同餘位置中的最小前綴和
            // 這樣後續計算差值時可以得到最大的子陣列和
            kSum[i % k] = Math.Min(kSum[i % k], prefixSum);
        }

        return maxSum;
    }
}
