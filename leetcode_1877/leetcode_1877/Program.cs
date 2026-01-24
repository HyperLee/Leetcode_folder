namespace leetcode_1877;

class Program
{
    /// <summary>
    /// 1877. Minimize Maximum Pair Sum in Array
    /// https://leetcode.com/problems/minimize-maximum-pair-sum-in-array/description/?envType=daily-question&envId=2026-01-24
    /// 
    /// Problem (English):
    /// The pair sum of a pair (a, b) is equal to a + b. The maximum pair sum is the largest pair sum in a list of pairs.
    /// For example, if we have pairs (1,5), (2,3), and (4,4), the maximum pair sum would be max(1+5, 2+3, 4+4) = max(6, 5, 8) = 8.
    /// Given an array nums of even length n, pair up the elements of nums into n / 2 pairs such that:
    /// - Each element of nums is in exactly one pair, and
    /// - The maximum pair sum is minimized.
    /// Return the minimized maximum pair sum after optimally pairing up the elements.
    /// 
    /// 題目（繁體中文）：
    /// 對於一個數對 (a, b)，該數對的和為 a + b。在一組數對中，最大的數對和稱為「最大數對和」。
    /// 例如：數對 (1,5)、(2,3) 與 (4,4) 的最大數對和為 max(1+5, 2+3, 4+4) = max(6, 5, 8) = 8。
    /// 給定一個長度為偶數 n 的陣列 nums，將 nums 中的元素兩兩配對成 n / 2 個數對，要求：
    /// - 每個元素恰好屬於一個數對；
    /// - 要使配對後的最大數對和最小。
    /// 回傳最佳配對下的最小最大數對和。
    /// 
    /// 1877. 数组中最大数对和的最小值
    /// https://leetcode.cn/problems/minimize-maximum-pair-sum-in-array/description/?envType=daily-question&envId=2026-01-24
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1: nums = [3,5,2,3] => 期望輸出 7
        // 排序後: [2,3,3,5] => 配對 (2,5)=7, (3,3)=6 => max=7
        int[] nums1 = [3, 5, 2, 3];
        Console.WriteLine($"Test 1: [{string.Join(", ", nums1)}] => {solution.MinPairSum(nums1)}"); // 預期: 7

        // 測試案例 2: nums = [3,5,4,2,4,6] => 期望輸出 8
        // 排序後: [2,3,4,4,5,6] => 配對 (2,6)=8, (3,5)=8, (4,4)=8 => max=8
        int[] nums2 = [3, 5, 4, 2, 4, 6];
        Console.WriteLine($"Test 2: [{string.Join(", ", nums2)}] => {solution.MinPairSum(nums2)}"); // 預期: 8

        // 測試案例 3: nums = [1,2] => 期望輸出 3
        // 只有一對 (1,2)=3
        int[] nums3 = [1, 2];
        Console.WriteLine($"Test 3: [{string.Join(", ", nums3)}] => {solution.MinPairSum(nums3)}"); // 預期: 3

        // 測試案例 4: nums = [1,1,1,1] => 期望輸出 2
        // 排序後: [1,1,1,1] => 配對 (1,1)=2, (1,1)=2 => max=2
        int[] nums4 = [1, 1, 1, 1];
        Console.WriteLine($"Test 4: [{string.Join(", ", nums4)}] => {solution.MinPairSum(nums4)}"); // 預期: 2
    }

    /// <summary>
    /// 解法：排序 + 貪心（Greedy）
    /// 
    /// 核心思路：
    /// 將最小的數與最大的數配對，第二小的數與第二大的數配對，依此類推。
    /// 這樣可以讓各數對的和盡可能「平均」，從而使最大數對和最小化。
    /// 
    /// 證明（反證法）：
    /// 設排序後的陣列為 a₁ ≤ a₂ ≤ ... ≤ aₙ。
    /// 考慮 a₁ 與 aₙ 是否應該配對：
    /// - 若 a₁ 與 aᵢ 配對，aⱼ 與 aₙ 配對（i, j 為其他數）
    ///   => 最大數對和 M₁ = max(a₁ + aᵢ, aⱼ + aₙ, 其他)
    /// - 若 a₁ 與 aₙ 配對，aᵢ 與 aⱼ 配對
    ///   => 最大數對和 M₂ = max(a₁ + aₙ, aᵢ + aⱼ, 其他)
    /// 
    /// 因為 a₁ ≤ aⱼ 且 aᵢ ≤ aₙ，所以 a₁ + aₙ ≤ aⱼ + aₙ 且 aᵢ + aⱼ ≤ aⱼ + aₙ
    /// => M₂ ≤ M₁，故 a₁ 與 aₙ 配對不會使結果變差。
    /// 
    /// 移除已配對的 a₁ 與 aₙ 後，子問題規模縮小為 n-2，遞迴應用同樣策略即可。
    /// 
    /// 時間複雜度：O(n log n)，排序主導
    /// 空間複雜度：O(1)，原地排序（忽略排序內部使用的空間）
    /// </summary>
    /// <param name="nums">輸入的整數陣列，長度為偶數</param>
    /// <returns>最佳配對下的最小最大數對和</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.MinPairSum([3, 5, 2, 3]); // 回傳 7
    /// </code>
    /// </example>
    public int MinPairSum(int[] nums)
    {
        // Step 1: 排序陣列，使得最小的元素在前，最大的元素在後
        Array.Sort(nums);

        int res = 0;           // 紀錄最大數對和
        int n = nums.Length;   // 陣列長度

        // Step 2: 使用雙指標概念，第 i 小與第 i 大配對
        // i 從 0 到 n/2 - 1，對應配對 (nums[i], nums[n-1-i])
        for (int i = 0; i < n / 2; i++)
        {
            // 計算當前配對的和，並更新最大值
            int pairSum = nums[i] + nums[n - 1 - i];
            res = Math.Max(res, pairSum);
        }

        return res;
    }
}
