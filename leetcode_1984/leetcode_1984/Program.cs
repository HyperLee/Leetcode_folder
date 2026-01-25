namespace leetcode_1984;

class Program
{
    /// <summary>
    /// EN: Given a 0-indexed integer array `nums`, where `nums[i]` represents the score of the i-th student, and an integer `k`.
    /// Pick any `k` scores so that the difference between the highest and the lowest of the `k` scores is minimized.
    /// Return the minimum possible difference.
    /// 
    /// 繁體中文: 給定一個以 0 為起點的整數陣列 `nums`，`nums[i]` 表示第 i 位學生的分數，並給定整數 `k`。
    /// 從陣列中選出任意 `k` 位學生的分數，使這 k 個分數的最大值與最小值之差最小化。
    /// 回傳可能的最小差值。
    /// 
    /// LeetCode: 1984. Minimum Difference Between Highest and Lowest of K Scores
    /// https://leetcode.com/problems/minimum-difference-between-highest-and-lowest-of-k-scores/
    /// </summary>
    /// <param name="args">Command-line arguments</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1: nums = [90], k = 1
        // 只有一個學生，最大與最小分數相同，差值為 0
        int[] nums1 = [90];
        int k1 = 1;
        Console.WriteLine($"測試案例 1: nums = [{string.Join(", ", nums1)}], k = {k1}");
        Console.WriteLine($"結果: {solution.MinimumDifference(nums1, k1)}"); // 預期輸出: 0
        Console.WriteLine();

        // 測試案例 2: nums = [9,4,1,7], k = 2
        // 排序後: [1,4,7,9]，選擇連續的 2 個元素
        // 可能的差值: (4-1)=3, (7-4)=3, (9-7)=2 → 最小為 2
        int[] nums2 = [9, 4, 1, 7];
        int k2 = 2;
        Console.WriteLine($"測試案例 2: nums = [{string.Join(", ", nums2)}], k = {k2}");
        Console.WriteLine($"結果: {solution.MinimumDifference(nums2, k2)}"); // 預期輸出: 2
        Console.WriteLine();

        // 測試案例 3: nums = [87063,61094,44530,21297,95857,93551,9918], k = 6
        // 排序後選擇連續 6 個元素中差值最小的組合
        int[] nums3 = [87063, 61094, 44530, 21297, 95857, 93551, 9918];
        int k3 = 6;
        Console.WriteLine($"測試案例 3: nums = [{string.Join(", ", nums3)}], k = {k3}");
        Console.WriteLine($"結果: {solution.MinimumDifference(nums3, k3)}"); // 預期輸出: 74560
    }

    /// <summary>
    /// 計算選擇 k 個分數時，最高分與最低分的最小可能差值。
    /// 
    /// <para><b>解題思路：排序 + 滑動視窗</b></para>
    /// <para>
    /// 核心觀察：要最小化 k 個分數中最高分與最低分的差值，
    /// 我們一定是在排序後的陣列中選擇「連續」的 k 個元素。
    /// </para>
    /// 
    /// <para><b>證明：</b></para>
    /// <para>
    /// 假設我們跳過某個下標 i 而選擇了不連續的元素，
    /// 那麼可以將選中的最高分替換成 nums[i]，
    /// 此時最高分不會變大，差值也不會增加。
    /// 因此，最優解必定是連續選擇排序後陣列中的 k 個元素。
    /// </para>
    /// 
    /// <para><b>演算法步驟：</b></para>
    /// <list type="number">
    ///   <item>將陣列 nums 升序排序</item>
    ///   <item>使用大小為 k 的滑動視窗遍歷排序後的陣列</item>
    ///   <item>對於每個視窗，計算差值 = nums[i+k-1] - nums[i]</item>
    ///   <item>回傳所有差值中的最小值</item>
    /// </list>
    /// 
    /// <para><b>時間複雜度：</b>O(n log n)，其中 n 為陣列長度（排序的時間複雜度）</para>
    /// <para><b>空間複雜度：</b>O(1)（原地排序，僅使用常數額外空間）</para>
    /// </summary>
    /// <param name="nums">學生分數陣列</param>
    /// <param name="k">需要選擇的學生數量</param>
    /// <returns>k 個分數中最高分與最低分的最小可能差值</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int[] nums = [9, 4, 1, 7];
    /// int result = solution.MinimumDifference(nums, 2);
    ///  排序後: [1, 4, 7, 9]
    ///  視窗大小 k=2 的所有差值: (4-1)=3, (7-4)=3, (9-7)=2
    ///  result = 2
    /// </code>
    /// </example>
    public int MinimumDifference(int[] nums, int k)
    {
        // 步驟 1: 將陣列升序排序
        // 排序後，連續的 k 個元素會有最小的極差（最大值 - 最小值）
        Array.Sort(nums);

        int n = nums.Length;

        // 初始化最小差值為最大整數值
        int minDiff = int.MaxValue;

        // 步驟 2: 使用滑動視窗遍歷所有可能的 k 個連續元素
        // 視窗左邊界 i 從 0 開始，右邊界為 i + k - 1
        for (int i = 0; i + k - 1 < n; i++)
        {
            // 步驟 3: 計算當前視窗的差值（最大值 - 最小值）
            // 由於陣列已排序，nums[i] 是最小值，nums[i+k-1] 是最大值
            int diff = nums[i + k - 1] - nums[i];

            // 步驟 4: 更新最小差值
            if (diff < minDiff)
            {
                minDiff = diff;
            }
        }

        return minDiff;
    }
}
