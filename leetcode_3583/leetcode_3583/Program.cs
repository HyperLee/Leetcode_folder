namespace leetcode_3583;

class Program
{
    /// <summary>
    /// 3583. Count Special Triplets
    /// https://leetcode.com/problems/count-special-triplets/description/
    /// <para>
    /// You are given an integer array nums.
    ///
    /// A special triplet is defined as a triplet of indices (i, j, k) such that:
    /// - 0 &lt;= i &lt; j &lt; k &lt; n, where n = nums.length
    /// - nums[i] == nums[j] * 2
    /// - nums[k] == nums[j] * 2
    ///
    /// Return the total number of special triplets in the array.
    ///
    /// Since the answer may be large, return it modulo 10^9 + 7.
    ///
    /// Example 1:
    /// Input: nums = [6,3,6]
    /// Output: 1
    /// Explanation: The only special triplet is (i, j, k) = (0, 1, 2), where nums[0] = 6, nums[1] = 3, nums[2] = 6, nums[0] = nums[1] * 2 = 3 * 2 = 6, and nums[2] = nums[1] * 2 = 3 * 2 = 6.
    ///
    /// Example 2:
    /// Input: nums = [0,1,0,0]
    /// Output: 1
    /// Explanation: The only special triplet is (i, j, k) = (0, 2, 3), where nums[0] = 0, nums[2] = 0, nums[3] = 0, nums[0] = nums[2] * 2 = 0 * 2 = 0, and nums[3] = nums[2] * 2 = 0 * 2 = 0.
    ///
    /// Example 3:
    /// Input: nums = [8,4,2,8,4]
    /// Output: 2
    /// Explanation: There are exactly two special triplets. For (0, 1, 3), nums[0] = 8, nums[1] = 4, nums[3] = 8, nums[0] = nums[1] * 2 = 4 * 2 = 8, and nums[3] = nums[1] * 2 = 4 * 2 = 8. For (1, 2, 4), nums[1] = 4, nums[2] = 2, nums[4] = 4, nums[1] = nums[2] * 2 = 2 * 2 = 4, and nums[4] = nums[2] * 2 = 2 * 2 = 4.
    ///
    /// Constraints:
    /// - 3 &lt;= n == nums.length &lt;= 10^5
    /// - 0 &lt;= nums[i] &lt;= 10^5
    /// </para>
    /// <para>
    /// 3583. 統計特殊三元組
    /// https://leetcode.cn/problems/count-special-triplets/description/
    ///
    /// 給定一個整數陣列 nums。
    ///
    /// 特殊三元組定義為符合下列條件的索引三元組 (i, j, k)：
    /// - 0 &lt;= i &lt; j &lt; k &lt; n，其中 n = nums.length
    /// - nums[i] == nums[j] * 2
    /// - nums[k] == nums[j] * 2
    ///
    /// 回傳陣列中特殊三元組的總數。
    ///
    /// 由於答案可能很大，請將結果對 10^9 + 7 取模後回傳。
    ///
    /// 範例 1：
    /// 輸入：nums = [6,3,6]
    /// 輸出：1
    /// 解釋：唯一的特殊三元組為 (i, j, k) = (0, 1, 2)，其中 nums[0] = 6、nums[1] = 3、nums[2] = 6、nums[0] = nums[1] * 2 = 3 * 2 = 6，且 nums[2] = nums[1] * 2 = 3 * 2 = 6。
    ///
    /// 範例 2：
    /// 輸入：nums = [0,1,0,0]
    /// 輸出：1
    /// 解釋：唯一的特殊三元組為 (i, j, k) = (0, 2, 3)，其中 nums[0] = 0、nums[2] = 0、nums[3] = 0、nums[0] = nums[2] * 2 = 0 * 2 = 0，且 nums[3] = nums[2] * 2 = 0 * 2 = 0。
    ///
    /// 範例 3：
    /// 輸入：nums = [8,4,2,8,4]
    /// 輸出：2
    /// 解釋：恰有兩個特殊三元組。對於 (0, 1, 3)，nums[0] = 8、nums[1] = 4、nums[3] = 8、nums[0] = nums[1] * 2 = 4 * 2 = 8，且 nums[3] = nums[1] * 2 = 4 * 2 = 8。對於 (1, 2, 4)，nums[1] = 4、nums[2] = 2、nums[4] = 4、nums[1] = nums[2] * 2 = 2 * 2 = 4，且 nums[4] = nums[2] * 2 = 2 * 2 = 4。
    ///
    /// 限制條件：
    /// - 3 &lt;= n == nums.length &lt;= 10^5
    /// - 0 &lt;= nums[i] &lt;= 10^5
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    /// <summary>
    /// 測試 SpecialTriplets 方法的簡單執行與驗證
    /// 在這裡加入多組測試資料並輸出結果與預期值
    /// </summary>
    static void Main(string[] args)
    {
        // 建立 Program 實例來呼叫方法
        var solution = new Program();

        // 測試資料：陣列與對應的預期結果
        int[][] tests = new int[][]
        {
            new int[] { 2, 1, 2 },              // j=1 => 左:2(1) 右:2(1) => 1
            new int[] { 1, 2, 1 },              // 無特殊三元組
            new int[] { 0, 0, 0, 0 },           // 全為 0，總數為 4
            new int[] { 2, 2, 1, 2, 2 },        // j=2 => 左：2 個 2；右：2 個 2；2*2=4
            new int[] { 2, 1, 2, 1, 2 },        // 多個 j，期望 4
            new int[] { 1, 1, 2, 2, 4 },        // 沒有符合條件的三元組
        };

        int[] expects = new int[] { 1, 0, 4, 4, 4, 0 };

        // 執行所有測試並輸出結果（兩種方法）
        for (int i = 0; i < tests.Length; i++)
        {
            var arr = tests[i];
            int expected = expects[i];
            int result1 = solution.SpecialTriplets(arr);
            int result2 = solution.SpecialTripletsWithDictionary(arr);
            Console.WriteLine($"Test #{i + 1}: nums=[{string.Join(", ", arr)}] => method1={result1}, method2={result2}, expected={expected} => {(result1 == expected && result2 == expected ? "PASS" : "FAIL")}");
        }
    }

    /// <summary>
    /// 計算「特殊三元組」的數量。
    /// 方法：枚舉中間位置 j，統計 nums[j] * 2 在 j 左側與右側的出現次數，左側計數 (`leftSideCount`) * 右側計數 (`rightSideCount`) 即為以 j 為中間的特殊三元組數量。
    /// 使用陣列作為計數器（leftSideCount / rightSideCount），時間複雜度 O(n + m)，m = max(nums)；空間複雜度 O(m)。
    /// 回傳值會在最後對 1e9+7 取模。
    /// </summary>
    /// <param name="nums">輸入整數陣列</param>
    /// <returns>特殊三元組的數量（對 1e9+7 取模）</returns>
    public int SpecialTriplets(int[] nums)
    {
        // 模組值，LeetCode 要求答案對 1e9+7 取模
        const long MOD = 1_000_000_007;

        // Null-or-short-circuit：陣列為 null 或長度 < 3 時無法形成三元組
        if (nums is null || nums.Length < 3)
        {
            return 0;
        }

        // 先找出陣列中最大值，用來建立出現次數陣列
        int maxValue = 0;
        foreach (var v in nums)
        {
            if (v > maxValue)
            {
                maxValue = v;
            }
        }

        // 右側出現次數（rightSideCount）先統計整個陣列
        // 初始化 rightSideCount 為整個陣列的出現次數，代表在 j 之後（包含當前）元素的出現次數
        var rightSideCount = new int[maxValue + 1];
        foreach (var v in nums)
        {
            rightSideCount[v]++;
        }

        long ans = 0;

        // 左側出現次數（leftSideCount），初始全為 0
        // leftSideCount 代表在 j 之前元素的出現次數
        var leftSideCount = new int[maxValue + 1];

        // 枚舉中間位置 j；對於每個 nums[j]，計算左右兩側 nums[j]*2 的次數相乘
        foreach (var v in nums)
        {
            // 將當前元素從右側出現次數中移除（因為 j 已經在中間）
            rightSideCount[v]--;

            // 目標值為 nums[j]*2
            long target = (long)v * 2L;
            if (target <= maxValue)
            {
                // 乘法計算左側出現次數 * 右側出現次數並累加
                // 左側出現次數 * 右側出現次數並累加
                ans += (long)leftSideCount[(int)target] * rightSideCount[(int)target];
                // 盡量避免 long 值過大，定期取模
                if (ans >= MOD)
                {
                    ans %= MOD;
                }
            }

            // 將當前元素加入左側出現次數
            leftSideCount[v]++;
        }

        // 最終回傳對 MOD 取模後的 int 值
        return (int)(ans % MOD);
    }
    
    /// <summary>
    /// 方法二：使用 Dictionary 作為計數器來解題
    /// time: O(n) average, space: O(unique(nums))
    /// 這個方法更通用，可處理負數或散佈在大的範圍中的值
    /// </summary>
    /// <param name="nums">輸入整數陣列</param>
    /// <returns>特殊三元組的數量（對 1e9+7 取模）</returns>
    public int SpecialTripletsWithDictionary(int[] nums)
    {
        const long MOD = 1_000_000_007;

        if (nums is null || nums.Length < 3)
        {
            return 0;
        }

        var right = new Dictionary<int, long>();
        var left = new Dictionary<int, long>();

        // 計算右側（初始為整個陣列）出現次數
        foreach (var v in nums)
        {
            if (right.ContainsKey(v))
            {
                right[v]++;
            }
            else
            {
                right[v] = 1;
            }
        }

        long ans = 0;

        foreach (var v in nums)
        {
            // 移除當前 j 從 right
            right[v]--;
            if (right[v] == 0)
            {
                right.Remove(v);
            }

            long targetLong = (long)v * 2L;
            if (targetLong <= int.MaxValue && targetLong >= int.MinValue)
            {
                int target = (int)targetLong;
                if (left.TryGetValue(target, out var leftCnt) && right.TryGetValue(target, out var rightCnt))
                {
                    ans += leftCnt * rightCnt;
                    if (ans >= MOD) ans %= MOD;
                }
            }

            // 將當前 v 加入 left
            if (left.ContainsKey(v))
            {
                left[v]++;
            }
            else
            {
                left[v] = 1;
            }
        }

        return (int)(ans % MOD);
    }
  
}
