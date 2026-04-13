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

        Console.WriteLine();
        Console.WriteLine("=== 解法二驗證 ===");
        Console.WriteLine($"測試 1 (預期 4)  : {program.MinimumDistance2(nums1)}");
        Console.WriteLine($"測試 2 (預期 8)  : {program.MinimumDistance2(nums2)}");
        Console.WriteLine($"測試 3 (預期 -1) : {program.MinimumDistance2(nums3)}");
        Console.WriteLine($"測試 4 (預期 4)  : {program.MinimumDistance2(nums4)}");
        Console.WriteLine($"測試 5 (預期 8)  : {program.MinimumDistance2(nums5)}");
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
    /// 1. 使用三重迴圈，嚴格按照 i < j < k 枚舉所有不同的三元組。
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

    /// <summary>
    /// 方法二：維護最近三次出現位置（O(n) 時間、O(n) 空間）
    ///
    /// 解題思路：
    /// 關鍵性質：若同一個數在索引 a <= b <= c 各出現一次，
    /// 則距離 = |a-b| + |b-c| + |c-a| = 2 * (c - a)，結果只與最左和最右位置有關。
    ///
    /// 因此只需在遍歷過程中，為每個數維護「最近 3 次出現的索引」：
    /// - 出現次數未滿 3 次：直接記錄索引。
    /// - 已有 3 次：視窗右移——丟棄 slot 0 最舊索引，slot 內資料依序往前遞補
    ///   （cache[v][0]←cache[v][1]、cache[v][1]←cache[v][2]），再將當前索引存入 slot 2；
    ///   視窗在陣列上向右滑動，始終保留最近三個位置（越靠近的三點，距離公式中的 c-a 越小）。
    /// - 一旦某數已累積 3 個位置，立即用 2*(cache[v][2]-cache[v][0]) 更新全局最小值。
    ///
    /// 題目保證 1 <= nums[i] <= n，故直接以 n+1 大小的陣列代替 Dictionary，避免雜湊開銷。
    /// 
    /// 主要概念就類似於「滑動視窗」，不過這裡的視窗是針對每個數值獨立維護的，確保在遍歷過程中能即時更新最小距離。
    /// 當視窗右移時，丟棄最舊位置（slot 0），因為越靠近的三點距離必定越小，這樣能確保在任何時刻，cache[v] 中的三個索引都是最近的三個出現位置。
    ///
    /// 時間複雜度：O(n)，空間複雜度：O(n)。
    /// </summary>
    /// <param name="nums">整數陣列，元素值限於 [1, n]。</param>
    /// <returns>最小距離；若不存在三個相等元素的 good tuple 則回傳 -1。</returns>
    public int MinimumDistance2(int[] nums)
    {
        int n = nums.Length;

        // cache[v, slot]：數值 v 最近三次出現的索引（slot = 0/1/2）
        // 題目保證 1 <= nums[i] <= n，故開 n+1 避免越界
        int[,] cache = new int[n + 1, 3];

        // count[v]：數值 v 目前已記錄的索引數量，最大追蹤至 3
        int[] count = new int[n + 1];

        int res = int.MaxValue;

        for (int i = 0; i < n; i++)
        {
            int v = nums[i];

            if (count[v] < 3)
            {
                // 尚未滿三個位置，直接填入
                cache[v, count[v]] = i;
                count[v]++;
            }
            else
            {
                // 視窗右移：slot 資料依序往前遞補，slot 2 存入最新索引
                // 丟棄最舊（最左）位置，因為越靠近的三點距離必定越小
                cache[v, 0] = cache[v, 1];
                cache[v, 1] = cache[v, 2];
                cache[v, 2] = i;
            }

            // 當恰好累積到三個位置（或更新後仍有三個）的情況下更新答案
            // 距離 = 2 * (最右索引 - 最左索引)
            if (count[v] == 3)
            {
                res = Math.Min(res, 2 * (cache[v, 2] - cache[v, 0]));
            }
        }

        return res == int.MaxValue ? -1 : res;
    }
}
