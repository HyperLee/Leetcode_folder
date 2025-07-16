namespace leetcode_3201;

class Program
{
    /// <summary>
    /// 3201. Find the Maximum Length of Valid Subsequence I
    /// https://leetcode.com/problems/find-the-maximum-length-of-valid-subsequence-i/description/
    /// 3201. 找出有效子序列的最大长度 I
    /// https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/description/?envType=daily-question&envId=2025-07-16
    /// 
    /// 給定一個整數陣列 nums。
    /// 
    /// 有一個長度為 x 的 nums 子序列被稱為有效，若滿足：
    /// (sub[0] + sub[1]) % 2 == (sub[1] + sub[2]) % 2 == ... == (sub[x - 2] + sub[x - 1]) % 2。
    /// 
    /// 請回傳 nums 最長有效子序列的長度。
    /// 
    /// 子序列是可以從原陣列刪除部分元素（或不刪除）且不改變剩餘元素順序所得到的陣列。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        // 測試資料 1
        int[] nums1 = { 1, 2, 3, 4 };
        Console.WriteLine($"Input: [1,2,3,4]  Output: {new Program().MaximumLength(nums1)}");

        // 測試資料 2
        int[] nums2 = { 1, 2, 1, 1, 2, 1, 2 };
        Console.WriteLine($"Input: [1,2,1,1,2,1,2]  Output: {new Program().MaximumLength(nums2)}");

        // 測試資料 3
        int[] nums3 = { 1, 3 };
        Console.WriteLine($"Input: [1,3]  Output: {new Program().MaximumLength(nums3)}");
    }

    /// <summary>
    /// 回傳 nums 最長有效子序列的長度。
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>最長有效子序列長度</returns>
    public int MaximumLength(int[] nums)
    {
        // dp[i, p]: 以 nums[i] 結尾，且子序列 parity 為 p 的最長長度
        // p: 0 或 1，分別代表 (sub[x-2] + sub[x-1]) % 2 == 0 或 1
        if (nums is null || nums.Length == 0)
        {
            return 0;
        }
        int n = nums.Length;
        int[,] dp = new int[n, 2];
        int maxLen = 1;
        for (int i = 0; i < n; i++)
        {
            // 單一元素子序列，尚未有 parity，初始化為 1
            dp[i, 0] = 1;
            dp[i, 1] = 1;
            for (int j = 0; j < i; j++)
            {
                int parity = (nums[j] + nums[i]) % 2;
                // 只有當 j 之前已經有 parity，且 parity 一致時才能延長
                dp[i, parity] = Math.Max(dp[i, parity], dp[j, parity] + 1);
            }
            maxLen = Math.Max(maxLen, Math.Max(dp[i, 0], dp[i, 1]));
        }
        return maxLen;
    }
}
