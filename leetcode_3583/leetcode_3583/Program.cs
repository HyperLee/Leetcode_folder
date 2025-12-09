namespace leetcode_3583;

class Program
{
    /// <summary>
    /// 3583. Count Special Triplets
    /// https://leetcode.com/problems/count-special-triplets/description/?envType=daily-question&envId=2025-12-09
    /// 3583. 统计特殊三元组
    /// https://leetcode.cn/problems/count-special-triplets/description/?envType=daily-question&envId=2025-12-09
    ///
    /// 繁體中文翻譯:
    /// 給你一個整數陣列 nums。
    /// 一個「特殊三元組」被定義為索引三元組 (i, j, k)，使得：
    ///  - 0 <= i < j < k < n，n = nums.Length
    ///  - nums[i] == nums[j] * 2
    ///  - nums[k] == nums[j] * 2
    /// 請回傳陣列中所有特殊三元組的數量，答案可能很大，請對 10^9 + 7 取模。
    /// 
    /// 
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

        // 執行所有測試並輸出結果
        for (int i = 0; i < tests.Length; i++)
        {
            var arr = tests[i];
            int expected = expects[i];
            int result = solution.SpecialTriplets(arr);
            Console.WriteLine($"Test #{i + 1}: nums=[{string.Join(", ", arr)}] => got={result}, expected={expected} => {(result == expected ? "PASS" : "FAIL")}");
        }
    }

    /// <summary>
    /// 計算「特殊三元組」的數量。
    /// 方法：枚舉中間位置 j，統計 nums[j]*2 在 j 左側與右側的出現次數，左側計數 (`leftSideCount`) * 右側計數 (`rightSideCount`) 即為以 j 為中間的特殊三元組數量。
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

        // 先找出陣列中最大值，用來建立出現次數陣列（等同於 Java 中的 mx）
        int mx = 0;
        foreach (var v in nums)
        {
            if (v > mx)
            {
                mx = v;
            }
        }

        // 右側出現次數（rightSideCount）先統計整個陣列
        // 初始化 rightSideCount 為整個陣列的出現次數，代表在 j 之後（包含當前）元素的出現次數
        var rightSideCount = new int[mx + 1];
        foreach (var v in nums)
        {
            rightSideCount[v]++;
        }

        long ans = 0;

        // 左側出現次數（leftSideCount），初始全為 0
        // leftSideCount 代表在 j 之前元素的出現次數
        var leftSideCount = new int[mx + 1];

        // 枚舉中間位置 j；對於每個 nums[j]，計算左右兩側 nums[j]*2 的次數相乘
        foreach (var v in nums)
        {
            // 將當前元素從右側出現次數中移除（因為 j 已經在中間）
            rightSideCount[v]--;

            // 目標值為 nums[j]*2
            long target = (long)v * 2L;
            if (target <= mx)
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
  
}
