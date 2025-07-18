namespace leetcode_3202;

class Program
{
    /// <summary>
    /// 3202. Find the Maximum Length of Valid Subsequence II
    /// https://leetcode.com/problems/find-the-maximum-length-of-valid-subsequence-ii/description/?envType=daily-question&envId=2025-07-17
    /// 3202. 找出有效子序列的最大长度 II
    /// https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-ii/description/?envType=daily-question&envId=2025-07-17
    /// 
    /// 題目描述：
    /// 給定一個整數陣列 nums 和一個正整數 k。
    /// nums 的一個長度為 x 的子序列 sub 被稱為有效，若滿足：
    /// (sub[0] + sub[1]) % k == (sub[1] + sub[2]) % k == ... == (sub[x - 2] + sub[x - 1]) % k。
    /// 請回傳 nums 最長有效子序列的長度。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料 1
        int[] nums1 = {1, 2, 4, 5};
        int k1 = 3;
        int result1 = new Program().MaximumLength(nums1, k1);
        Console.WriteLine($"Test1: nums=[{string.Join(",", nums1)}], k={k1} => 最長有效子序列長度: {result1}");

        // 測試資料 2
        int[] nums2 = {7, 8, 9, 10, 11};
        int k2 = 4;
        int result2 = new Program().MaximumLength(nums2, k2);
        Console.WriteLine($"Test2: nums=[{string.Join(",", nums2)}], k={k2} => 最長有效子序列長度: {result2}");

        // 測試資料 3
        int[] nums3 = {5, 5, 5, 5};
        int k3 = 5;
        int result3 = new Program().MaximumLength(nums3, k3);
        Console.WriteLine($"Test3: nums=[{string.Join(",", nums3)}], k={k3} => 最長有效子序列長度: {result3}");
    }


    /// <summary>
    /// 使用動態規劃解決有效子序列的最大長度問題
    /// 
    /// 解題思路：
    /// 1. 根據有效子序列的定義，所有奇數下標的元素模 k 同餘，偶數下標的元素模 k 同餘
    /// 2. 考慮子序列最後兩個元素的模 k 的餘數，一共有 k² 種可能性
    /// 3. 使用 dp[i][j] 表示最後兩個元素模 k 的餘數分別是 i 和 j 的有效子序列的最大長度
    /// 4. 遍歷 nums，對每個元素嘗試將其加入子序列，更新對應的 dp 值
    /// 
    /// 時間複雜度：O(n * k)，其中 n 是陣列長度
    /// 空間複雜度：O(k²)
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <param name="k">正整數，用於模運算</param>
    /// <returns>最長有效子序列的長度</returns>
    public int MaximumLength(int[] nums, int k)
    {
        // dp[i][j] 表示最後兩個元素模 k 的餘數分別是 i 和 j 的有效子序列的最大長度
        int[,] dp = new int[k, k];
        int res = 0;
        
        // 遍歷陣列中的每個元素
        foreach (int num in nums)
        {
            // 計算當前元素模 k 的餘數
            int mod = num % k;
            
            // 嘗試將當前元素作為子序列的最後一個元素
            // 遍歷前一個元素模 k 的所有可能餘數
            for (int prev = 0; prev < k; prev++)
            {
                // 更新 dp[prev][mod]：以 prev 為倒數第二個元素，mod 為最後一個元素的子序列長度
                // dp[mod][prev] + 1 表示在原有子序列基礎上加入當前元素
                dp[prev, mod] = dp[mod, prev] + 1;
                
                // 更新全域最大值
                res = Math.Max(res, dp[prev, mod]);
            }
        }
        
        return res;
    }
}
