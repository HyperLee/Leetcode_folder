namespace leetcode_2154;

public class Program
{
    /// <summary>
    /// <para>
    /// 2154. Keep Multiplying Found Values by Two
    /// https://leetcode.com/problems/keep-multiplying-found-values-by-two/description/
    ///
    /// You are given integer array nums and integer original. While original is found in nums, set original = 2 * original and search again. Stop when it is not found, and return the final original.
    ///
    /// Example 1:
    /// Input: nums = [5,3,6,1,12], original = 3
    /// Output: 24
    /// Explanation: Find 3 and double it to 6; find 6 and double it to 12; find 12 and double it to 24. Since 24 is absent, return 24.
    ///
    /// Example 2:
    /// Input: nums = [2,7,9], original = 4
    /// Output: 4
    /// Explanation: 4 is absent, so return 4.
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 1000
    /// - 1 &lt;= nums[i], original &lt;= 1000
    /// </para>
    /// <para>
    /// 2154. 將找到的值乘以 2
    /// https://leetcode.cn/problems/keep-multiplying-found-values-by-two/description/
    ///
    /// 給定整數陣列 nums 與整數 original。只要能在 nums 中找到 original，就設定 original = 2 * original 並再次搜尋；找不到時停止，回傳最終的 original。
    ///
    /// 範例 1：
    /// 輸入：nums = [5,3,6,1,12], original = 3
    /// 輸出：24
    /// 說明：找到 3 後加倍為 6；找到 6 後加倍為 12；找到 12 後加倍為 24。因為找不到 24，所以回傳 24。
    ///
    /// 範例 2：
    /// 輸入：nums = [2,7,9], original = 4
    /// 輸出：4
    /// 說明：找不到 4，因此回傳 4。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 1000
    /// - 1 &lt;= nums[i], original &lt;= 1000
    /// </para>
    /// </summary>
    /// <param name="args">CLI 參數（目前未使用）。</param>
    static void Main(string[] args)
    {
        Program solver = new Program();

        int[] numsSampleA = { 5, 3, 6, 1, 12 };
        int originalSampleA = 3;
        Console.WriteLine($"HashSet 解法結果: {solver.FindFinalValue(numsSampleA, originalSampleA)}");

        int[] numsSampleB = { 2, 7, 9 };
        int originalSampleB = 4;
        Console.WriteLine($"排序解法結果: {solver.FindFinalValue_Array(numsSampleB, originalSampleB)}");
    }

    /// <summary>
    /// 透過 HashSet 於 O(1) 平均時間檢查 original 是否出現在 nums，持續倍增 original 直到值缺席。
    /// </summary>
    /// <param name="nums">輸入陣列，可能包含重複值。</param>
    /// <param name="original">要持續搜尋並倍增的起始值。</param>
    /// <returns>流程結束後的 original 值。</returns>
    /// <example>
    /// <code>
    /// Program solver = new Program();
    /// int result = solver.FindFinalValue(new[] { 5, 3, 6, 1, 12 }, 3);
    /// // result == 24
    /// </code>
    /// </example>
    public int FindFinalValue(int[] nums, int original) 
    {
        HashSet<int> numSet = new HashSet<int>(nums);
        // HashSet 的查找為平均 O(1)，可快速確認目前 original 是否存在。

        while (numSet.Contains(original))
        {
            original *= 2;
            // 只要找到就倍增後繼續檢查下一輪，直到不再命中。
        }

        return original;
    }

    /// <summary>
    /// 以排序搭配單次迭代的方式模擬題目的倍增流程，適合空間受限的情境。
    /// </summary>
    /// <param name="nums">輸入陣列，會被就地排序。</param>
    /// <param name="original">要持續搜尋並倍增的起始值。</param>
    /// <returns>流程結束後的 original 值。</returns>
    /// <example>
    /// <code>
    /// Program solver = new Program();
    /// int result = solver.FindFinalValue_Array(new[] { 5, 3, 6, 1, 12 }, 3);
    /// // result == 24
    /// </code>
    /// </example>
    public int FindFinalValue_Array(int[] nums, int original) 
    {
        Array.Sort(nums);
        // 排序後採一次線性掃描就能依序處理所有可能的 original。

        foreach (int num in nums)
        {
            if (num == original)
            {
                original *= 2;
                // 倍增後不 break，因為排序後後續仍可能再出現新的 original。
            }
        }

        return original;
    }
}
