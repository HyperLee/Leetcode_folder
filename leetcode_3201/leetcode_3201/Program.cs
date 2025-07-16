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
    /// 求最長有效子序列長度，解題思路如下：
    /// 
    /// 題目要求 (sub[i] + sub[i+1]) % 2 在所有相鄰元素都相等。
    /// 透過模運算移項可得 (sub[i] - sub[i+2]) % 2 == 0，代表偶數項彼此同餘、奇數項彼此同餘。
    /// 因此問題等價於：求一個子序列，其偶數位都同餘、奇數位都同餘。
    /// 
    /// 本方法採用動態規劃，維護一個二維陣列 f[y, x]，表示最後兩項模 2 分別為 y 和 x 的子序列長度。
    /// 遍歷 nums，每次根據餘數組合更新最長長度：
    ///   f[y, x] = f[x, y] + 1
    /// 最終答案為所有 f[y, x] 的最大值。
    /// 
    /// 簡單說我們考慮的就是 (a−c)modk=0, 也就是 a 和 c 的餘數相同。
    /// 考慮最後兩項時候, 就分別加上奇數或是偶數 idx 的值。
    /// 也就是因為有加上奇數或偶數 idx 的值，所以 f[y, x] = f[x, y] + 1 => 最終要 + 1 (長度 + 1)
    /// <example>
    /// <code>
    /// int[] nums = { 1, 2, 1, 2, 1, 2 };
    /// int result = new Program().MaximumLength(nums); // result = 6
    /// </code>
    /// </example>
    /// 參考：
    /// https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/solutions/2826593/deng-jie-zhuan-huan-dong-tai-gui-hua-pyt-7l4b/?envType=daily-question&envId=2025-07-16
    /// https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-ii/solutions/2826591/deng-jie-zhuan-huan-dong-tai-gui-hua-pyt-z2fs/
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>最長有效子序列長度</returns>
    public int MaximumLength(int[] nums)
    {
        int k = 2; // 只考慮餘數 0 和 1
        if (nums is null || nums.Length == 0)
        {
            // 邊界檢查，空陣列直接回傳 0
            return 0;
        }
        int ans = 0; // 最終答案
        int[,] f = new int[k, k]; // f[y, x]: 最後兩項模 k 分別為 y 和 x 的子序列長度
        foreach (var num in nums)
        {
            int x = num % k; // 取得目前元素的模 k
            for (int y = 0; y < k; y++)
            {
                // 動態規劃轉移：在「最後兩項模 k 分別為 x 和 y」的子序列末尾加上 num
                // 得到「最後兩項模 k 分別為 y 和 x」的子序列長度
                f[y, x] = f[x, y] + 1;
                ans = Math.Max(ans, f[y, x]); // 更新最長長度
            }
        }
        // 最長有效子序列長度
        return ans;
    }
}
