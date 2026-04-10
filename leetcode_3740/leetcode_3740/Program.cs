namespace leetcode_3740;

class Program
{
    /// <summary>
    /// 3740. Minimum Distance Between Three Equal Elements I
    /// https://leetcode.com/problems/minimum-distance-between-three-equal-elements-i/description/?envType=daily-question&envId=2026-04-10
    /// 3740. 三個相等元素之間的最小距離 I
    /// https://leetcode.cn/problems/minimum-distance-between-three-equal-elements-i/description/?envType=daily-question&envId=2026-04-10
    ///
    /// English:
    /// You are given an integer array nums.
    ///
    /// A tuple (i, j, k) of 3 distinct indices is good if nums[i] == nums[j] == nums[k].
    ///
    /// The distance of a good tuple is abs(i - j) + abs(j - k) + abs(k - i), where abs(x) denotes the absolute value of x.
    ///
    /// Return an integer denoting the minimum possible distance of a good tuple. If no good tuples exist, return -1.
    ///
    /// 繁體中文:
    /// 給定一個整數陣列 nums。
    ///
    /// 若三個彼此不同的索引 (i, j, k) 滿足 nums[i] == nums[j] == nums[k]，則此三元組為 good tuple。
    ///
    /// good tuple 的距離定義為 abs(i - j) + abs(j - k) + abs(k - i)，其中 abs(x) 表示 x 的絕對值。
    ///
    /// 回傳 good tuple 可能的最小距離；如果不存在任何 good tuple，則回傳 -1。
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試案例 1: 只有三個相同元素 → 距離 = 2*(2-0) = 4
        int[] nums1 = [1, 1, 1, 2];
        Console.WriteLine($"測試 1 (預期 4)  : {program.MinimumDistance(nums1)}");

        // 測試案例 2: 值 1 分散在索引 0,2,4 → 距離 = 2*(4-0) = 8
        int[] nums2 = [1, 2, 1, 3, 1];
        Console.WriteLine($"測試 2 (預期 8)  : {program.MinimumDistance(nums2)}");

        // 測試案例 3: 不存在三個相同元素 → 回傳 -1
        int[] nums3 = [1, 2, 3];
        Console.WriteLine($"測試 3 (預期 -1) : {program.MinimumDistance(nums3)}");

        // 測試案例 4: 四個相同元素，最短的連續三個 → 距離 = 2*(2-0) = 4
        int[] nums4 = [1, 1, 1, 1];
        Console.WriteLine($"測試 4 (預期 4)  : {program.MinimumDistance(nums4)}");

        // 測試案例 5: 值 4 出現在索引 0,2,4 → 距離 = 2*(4-0) = 8
        int[] nums5 = [4, 3, 4, 1, 4];
        Console.WriteLine($"測試 5 (預期 8)  : {program.MinimumDistance(nums5)}");
    }

    /// <summary>
    /// 方法一：暴力法（三重迴圈）
    ///
    /// 解題思路：
    /// 給定距離公式 abs(i-j) + abs(j-k) + abs(k-i)，觀察可以得知：
    /// 不管三個索引的排列順序如何，此公式恆等於「最左側索引到最右側索引距離的兩倍」。
    /// 設排序後三個索引為 i <= j <= k，則距離 = 2 * (k - i)。
    ///
    /// 因此：
    /// 1. 使用三重迴圈，嚴格按照 i <= j <= k 枚舉所有不同的三元組。
    /// 2. 若三個位置的值相等（nums[i] == nums[j] == nums[k]），則計算距離 2*(k-i)。
    /// 3. 對固定的 (i, j)，k 越小距離越小，因此找到第一個符合條件的 k 即可 break。
    /// 4. 記錄全局最小值，最終若未找到任何 good tuple 則回傳 -1。
    ///
    /// 時間複雜度：O(n³)，空間複雜度：O(1)。
    /// </summary>
    /// <param name="nums">整數陣列。</param>
    /// <returns>最小距離；若不存在三個相等元素的 good tuple 則回傳 -1。</returns>
    public int MinimumDistance(int[] nums)
    {
        int n = nums.Length;

        // 使用 int.MaxValue 作為哨兵，確保任何合法距離都能更新結果
        // （最大距離為 2*(n-1)，可能遠大於 n+1，故不能用 n+1 作哨兵）
        int res = int.MaxValue;

        // 嚴格枚舉 i < j < k，確保三個索引皆不同
        for (int i = 0; i < n - 2; i++)
        {
            for (int j = i + 1; j < n - 1; j++)
            {
                // nums[i] 與 nums[j] 不同時，無法組成 good tuple，跳過
                if (nums[i] != nums[j])
                {
                    continue;
                }

                for (int k = j + 1; k < n; k++)
                {
                    if (nums[j] == nums[k])
                    {
                        // 三個索引均相等：距離 = |i-j|+|j-k|+|k-i| = 2*(k-i)
                        // 對固定 (i, j)，k 由小到大掃描，第一個符合的 k 即為此對的最小距離
                        res = Math.Min(res, 2 * (k - i));
                        break;
                    }
                }
            }
        }

        return res == int.MaxValue ? -1 : res;
    }
}
