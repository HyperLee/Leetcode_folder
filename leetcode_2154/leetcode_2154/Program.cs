namespace leetcode_2154;

public class Program
{
    /// <summary>
    /// 2154. Keep Multiplying Found Values by Two
    /// https://leetcode.com/problems/keep-multiplying-found-values-by-two/description/?envType=daily-question&envId=2025-11-19
    /// 2154. 將找到的值乘以 2
    /// https://leetcode.cn/problems/keep-multiplying-found-values-by-two/description/?envType=daily-question&envId=2025-11-19
    ///
    /// 題目說明（繁體中文翻譯）:
    /// 給定一個整數陣列 `nums`，以及一個整數 `original`，代表第一個要在 `nums` 中搜尋的數字。
    /// 如果 `original` 在 `nums` 中找到，則把 `original` 乘以 2（即 `original = original * 2`），
    /// 然後以新的 `original` 繼續搜尋並重複上述步驟；若未找到則停止。
    /// 最後回傳程序結束時的 `original` 值。
    /// 
    /// 
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
