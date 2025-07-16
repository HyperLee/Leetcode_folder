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
        Console.WriteLine($"Input: [1,2,3,4]  MaximumLengthEnum Output: {new Program().MaximumLengthEnum(nums1)}"); // MaximumLengthEnum 測試

        // 測試資料 2
        int[] nums2 = { 1, 2, 1, 1, 2, 1, 2 };
        Console.WriteLine($"Input: [1,2,1,1,2,1,2]  Output: {new Program().MaximumLength(nums2)}");
        Console.WriteLine($"Input: [1,2,1,1,2,1,2]  MaximumLengthEnum Output: {new Program().MaximumLengthEnum(nums2)}"); // MaximumLengthEnum 測試

        // 測試資料 3
        int[] nums3 = { 1, 3 };
        Console.WriteLine($"Input: [1,3]  Output: {new Program().MaximumLength(nums3)}");
        Console.WriteLine($"Input: [1,3]  MaximumLengthEnum Output: {new Program().MaximumLengthEnum(nums3)}"); // MaximumLengthEnum 測試
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

    /// <summary>
    /// 方法二：枚舉元素的奇偶性
    /// 
    /// 根據有效子序列的定義，可以發現，子序列中所有奇數下標的元素奇偶性相同，所有偶數下標的元素奇偶性相同。
    /// 因此對於這個子序列，元素的奇偶性一共有 4 種可能：
    /// 1. 全為奇數
    /// 2. 全為偶數
    /// 3. 奇數下標為奇數且偶數下標為偶數
    /// 4. 奇數下標為偶數且偶數下標為奇數
    /// 
    /// 我們可以枚舉這四種可能性，對於每一種可能性，都遍歷整個 nums 陣列，並計算這種可能性下子序列的最大長度。
    /// 計算時，如果目前下標的數字滿足奇偶性要求，就貪心地將子序列長度增加 1。
    /// 最後回傳所有可能性下子序列長度的最大值。
    /// <example>
    /// <code>
    /// int[] nums = { 1, 2, 1, 2, 1, 2 };
    /// int result = new Program().MaximumLengthEnum(nums); // result = 6
    /// </code>
    /// </example>
    /// ref:
    /// https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/solutions/3717152/zhao-chu-you-xiao-zi-xu-lie-de-zui-da-ch-1n3j/?envType=daily-question&envId=2025-07-16
    /// 
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>最長有效子序列長度</returns>
    public int MaximumLengthEnum(int[] nums)
    {
        // res 用來記錄所有模式下的最大長度
        int res = 0;
        // patterns 定義四種奇偶性模式：
        // {0,0}：奇數下標為偶數，偶數下標為偶數
        // {0,1}：奇數下標為偶數，偶數下標為奇數
        // {1,0}：奇數下標為奇數，偶數下標為偶數
        // {1,1}：奇數下標為奇數，偶數下標為奇數
        int[,] patterns = new int[4, 2] { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } };
        // 枚舉四種奇偶性模式
        for (int i = 0; i < 4; i++)
        {
            int cnt = 0; // 記錄目前模式下的子序列長度
            // 遍歷 nums 陣列
            foreach (int num in nums)
            {
                // 判斷目前元素 num 是否符合當前模式下的奇偶性要求
                // cnt % 2 為目前子序列的下標（偶數或奇數），patterns[i, cnt % 2] 為該下標應有的奇偶性
                if (num % 2 == patterns[i, cnt % 2])
                {
                    // 若符合，則將子序列長度加一
                    cnt++;
                }
            }
            // 更新最大長度
            res = Math.Max(res, cnt);
        }
        // 回傳所有模式下的最大長度
        return res;
    }
}
