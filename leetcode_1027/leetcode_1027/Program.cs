using System.Net;

namespace leetcode_1027;

class Program
{
    /// <summary>
    /// 1027. Longest Arithmetic Subsequence
    /// https://leetcode.com/problems/longest-arithmetic-subsequence/description/
    ///
    /// Given an array nums of integers, return the length of the longest arithmetic subsequence in nums.
    ///
    /// Note that:
    /// - A subsequence is an array that can be derived from another array by deleting some or no elements
    ///   without changing the order of the remaining elements.
    /// - A sequence seq is arithmetic if seq[i + 1] - seq[i] are all the same value (for 0 &lt;= i &lt; seq.length - 1).
    ///
    /// ---
    ///
    /// 1027. 最長等差數列
    /// https://leetcode.cn/problems/longest-arithmetic-subsequence/description/
    ///
    /// 給定一個整數陣列 nums，回傳 nums 中最長等差子序列的長度。
    ///
    /// 注意：
    /// - 子序列是指從另一個陣列中刪除部分或不刪除元素，且不改變剩餘元素順序所得到的陣列。
    /// - 若序列 seq 中所有相鄰元素之差 seq[i + 1] - seq[i] 均相等（0 &lt;= i &lt; seq.length - 1），
    ///   則稱該序列為等差序列。
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例集合：每組為 (輸入陣列, 預期答案)
        (int[] nums, int expected)[] testCases =
        {
            // 範例 1：[3,6,9,12]，整個陣列即為公差 3 的等差數列，長度為 4
            (new[] { 3, 6, 9, 12 }, 4),
            // 範例 2：[9,4,7,2,10]，最長等差子序列為 [4,7,10]，公差 3，長度為 3
            (new[] { 9, 4, 7, 2, 10 }, 3),
            // 範例 3：[20,1,15,3,10,5,8]，最長等差子序列為 [20,15,10,5]，公差 -5，長度為 4
            (new[] { 20, 1, 15, 3, 10, 5, 8 }, 4),
            // 邊界：僅單一元素，等差子序列長度為 1
            (new[] { 7 }, 1),
            // 邊界：兩個元素，必為等差子序列，長度為 2
            (new[] { 1, 100 }, 2),
            // 含相同元素：公差為 0 的子序列 [5,5,5,5]，長度為 4
            (new[] { 5, 5, 5, 5, 1, 2 }, 4),
        };

        for (int i = 0; i < testCases.Length; i++)
        {
            (int[] nums, int expected) = testCases[i];
            int actual = program.LongestArithSeqLength(nums);
            string status = actual == expected ? "PASS" : "FAIL";
            Console.WriteLine($"Case {i + 1}: nums = [{string.Join(",", nums)}], expected = {expected}, actual = {actual} -> {status}");
        }
    }

    /// <summary>
    /// 計算陣列 <paramref name="nums"/> 中最長等差子序列的長度。
    /// </summary>
    /// <remarks>
    /// 解題思路（動態規劃）：
    /// <list type="number">
    ///   <item>
    ///     <description>
    ///     設 n 為陣列長度。任何等差子序列的公差 d 必落在 [-maxDiff, maxDiff]，
    ///     其中 maxDiff = max(nums) - min(nums)。為使索引非負，將公差平移為 d + maxDiff，
    ///     範圍變為 [0, 2*maxDiff]，共 2*maxDiff+1 個值。
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///     定義 dp[i][d] 為「以 nums[i] 結尾、平移後公差為 d 的最長等差子序列長度」。
    ///     單一元素本身即為長度 1 的等差子序列，故所有狀態初始化為 1。
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///     狀態轉移：對於 0 ≤ j < i，令 d = nums[i] - nums[j] + maxDiff，
    ///     則 dp[i][d] = max(dp[i][d], dp[j][d] + 1)，
    ///     代表將 nums[i] 接到「以 nums[j] 結尾、相同公差」的子序列尾端。
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///     最終答案為所有 dp[i][d] 的最大值。
    ///     </description>
    ///   </item>
    /// </list>
    /// 時間複雜度 O(n^2)，空間複雜度 O(n * maxDiff)。
    /// </remarks>
    /// <param name="nums">輸入整數陣列</param>
    /// <returns>最長等差子序列的長度</returns>
    /// <example>
    /// <code>
    /// var p = new Program();
    /// int len = p.LongestArithSeqLength(new[] { 9, 4, 7, 2, 10 }); // 回傳 3，對應子序列 [4,7,10]
    /// </code>
    /// </example>
    public int LongestArithSeqLength(int[] nums)
    {
        // 取得陣列中的最大、最小值，用以決定公差的合法範圍
        int maxNum = nums.Max();
        int minNum = nums.Min();

        // maxDiff 為公差絕對值上限；公差落在 [-maxDiff, maxDiff]
        int maxDiff = maxNum - minNum;

        // 任意單一元素即為長度 1 的等差子序列
        int maxLength = 1;
        int n = nums.Length;

        // dp[i][d]：以 nums[i] 結尾、平移後公差為 d 的最長等差子序列長度
        // 第二維長度為 2*maxDiff + 1，對應公差 -maxDiff..maxDiff 平移後的索引
        int[][] dp = new int[n][];

        for (int i = 0; i < n; i++)
        {
            dp[i] = new int[maxDiff * 2 + 1];
            // 初始化所有狀態為 1（單一元素）
            Array.Fill(dp[i], 1);
        }

        // 由小到大列舉結尾索引 i，再列舉前一個元素 j
        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                // 將真實公差 (nums[i] - nums[j]) 平移 maxDiff，避免負索引
                int d = nums[i] - nums[j] + maxDiff;

                // 狀態轉移：把 nums[i] 接在以 nums[j] 結尾、相同公差的子序列之後
                dp[i][d] = Math.Max(dp[i][d], dp[j][d] + 1);

                // 更新全域最大長度
                maxLength = Math.Max(maxLength, dp[i][d]);
            }
        }

        return maxLength;
    }
}
