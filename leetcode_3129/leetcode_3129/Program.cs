namespace leetcode_3129;

class Program
{
    /// <summary>
    /// 3129. Find All Possible Stable Binary Arrays I
    /// https://leetcode.com/problems/find-all-possible-stable-binary-arrays-i/description/?envType=daily-question&envId=2026-03-09
    /// 3129. 找出所有稳定的二进制数组 I
    /// https://leetcode.cn/problems/find-all-possible-stable-binary-arrays-i/description/?envType=daily-question&envId=2026-03-09
    ///
    /// Problem description (English):
    /// You are given 3 positive integers zero, one, and limit.
    ///
    /// A binary array arr is called stable if:
    ///
    /// - The number of occurrences of 0 in arr is exactly zero.
    /// - The number of occurrences of 1 in arr is exactly one.
    /// - Each subarray of arr with a size greater than limit must contain both 0 and 1.
    ///
    /// Return the total number of stable binary arrays.
    ///
    /// Since the answer may be very large, return it modulo 10^9 + 7.
    ///
    /// 題目描述（繁體中文）：
    /// 給定三個正整數 zero、one 和 limit。
    ///
    /// 二元陣列 arr 被稱為穩定的，如果：
    ///
    /// - arr 中出現 0 的次數恰好為 zero。
    /// - arr 中出現 1 的次數恰好為 one。
    /// - 每個長度大於 limit 的子陣列必須同時包含 0 和 1。
    ///
    /// 回傳穩定二元陣列的總數。
    ///
    /// 由於答案可能非常大，請將結果對 10^9 + 7 取模。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：zero=1, one=1, limit=2 → 預期輸出 2
        // 所有穩定數組：[0,1]、[1,0]
        Console.WriteLine($"Test 1 (expected 2): {solution.NumberOfStableArrays(1, 1, 2)}");

        // 測試案例 2：zero=1, one=1, limit=1 → 預期輸出 2
        // 所有穩定數組：[0,1]、[1,0]
        Console.WriteLine($"Test 2 (expected 2): {solution.NumberOfStableArrays(1, 1, 1)}");

        // 測試案例 3：zero=2, one=1, limit=1 → 預期輸出 1
        // 只有 [0,1,0] 合法；[0,0,1] 與 [1,0,0] 皆含長度為 2 的連續 0，超過 limit=1
        Console.WriteLine($"Test 3 (expected 1): {solution.NumberOfStableArrays(2, 1, 1)}");
    }

    /// <summary>
    /// 使用動態規劃計算所有穩定二元陣列的總數。
    /// <para>
    /// 關鍵觀察：「每個長度大於 <paramref name="limit"/> 的子陣列同時含 0 和 1」
    /// 等價於「陣列中不存在長度超過 <paramref name="limit"/> 的連續相同元素」。
    /// </para>
    /// <para>
    /// 狀態定義：<br/>
    /// dp[i][j][0] = 已放置 i 個 0、j 個 1 且最後一個元素為 0 的合法方案數<br/>
    /// dp[i][j][1] = 已放置 i 個 0、j 個 1 且最後一個元素為 1 的合法方案數
    /// </para>
    /// <para>
    /// 轉移方程式（以 dp[i][j][0] 為例，i≥1, j≥1）：
    /// · i ≤ limit：dp[i][j][0] = dp[i-1][j][0] + dp[i-1][j][1]
    /// · i > limit：dp[i][j][0] = dp[i-1][j][0] + dp[i-1][j][1] - dp[i-limit-1][j][1]
    ///   （減去「前一個 1 之後恰好連填 limit 個 0」的非法方案 dp[i-limit-1][j][1]）
    /// </para>
    /// <para>最終答案：(dp[zero][one][0] + dp[zero][one][1]) mod (10^9+7)</para>
    /// </summary>
    /// <param name="zero">陣列中 0 的個數</param>
    /// <param name="one">陣列中 1 的個數</param>
    /// <param name="limit">單一連續相同元素的長度上限</param>
    /// <returns>穩定二元陣列的總數，對 10^9+7 取模</returns>
    public int NumberOfStableArrays(int zero, int one, int limit)
    {
        const long MOD = 1000000007;

        // dp[i][j][k]：放置 i 個 0、j 個 1，最後一個元素為 k (0 或 1) 的合法方案數
        long[][][] dp = new long[zero + 1][][];
        for(int i = 0; i <= zero; i++)
        {
            dp[i] = new long[one + 1][];
            for(int j = 0; j <= one; j++)
            {
                dp[i][j] = new long[2];
            }
        }

        // 基底：只填 0 且不超過 limit 個，每種長度恰好一種方案
        for(int i = 0; i <= Math.Min(zero, limit); i++)
        {
            dp[i][0][0] = 1;
        }

        // 基底：只填 1 且不超過 limit 個，每種長度恰好一種方案
        for(int j = 0; j <= Math.Min(one, limit); j++)
        {
            dp[0][j][1] = 1;
        }

        for (int i = 1; i <= zero; i++) 
        {
            for (int j = 1; j <= one; j++) 
            {
                // 在前一狀態末尾再補一個 0
                if (i > limit) 
                {
                    // 需扣除「末尾已有 limit 個連續 0，再補 1 個」的非法方案
                    // 非法方案等於 dp[i-limit-1][j][1]（在那 limit 個 0 之前最後是 1 的方案）
                    dp[i][j][0] = dp[i - 1][j][0] + dp[i - 1][j][1] - dp[i - limit - 1][j][1];
                } 
                else 
                {
                    dp[i][j][0] = dp[i - 1][j][0] + dp[i - 1][j][1];
                }
                // 保持結果在 [0, MOD) 範圍內（減法可能產生負數，故加 MOD 再取模）
                dp[i][j][0] = (dp[i][j][0] % MOD + MOD) % MOD;
                
                // 在前一狀態末尾再補一個 1（邏輯與上方對稱）
                if (j > limit) 
                {
                    // 扣除「末尾已有 limit 個連續 1，再補 1 個」的非法方案
                    dp[i][j][1] = dp[i][j - 1][1] + dp[i][j - 1][0] - dp[i][j - limit - 1][0];
                } 
                else 
                {
                    dp[i][j][1] = dp[i][j - 1][1] + dp[i][j - 1][0];
                }
                dp[i][j][1] = (dp[i][j][1] % MOD + MOD) % MOD;
            }
        }

        // 最後一個元素可以是 0 或 1，兩者相加即為答案
        return (int) ((dp[zero][one][0] + dp[zero][one][1]) % MOD);
    }
}
