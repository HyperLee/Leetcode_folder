namespace leetcode_594;

class Program
{
    /// <summary>
    /// 594. Longest Harmonious Subsequence
    /// https://leetcode.com/problems/longest-harmonious-subsequence/description/
    /// <para>
    /// We define a harmonious array as an array where the difference between its maximum value and its minimum value is exactly 1.
    ///
    /// Given an integer array nums, return the length of its longest harmonious subsequence among all its possible subsequences.
    ///
    /// Example 1:
    /// Input: nums = [1,3,2,2,5,2,3,7]
    /// Output: 5
    /// Explanation: The longest harmonious subsequence is [3,2,2,2,3].
    ///
    /// Example 2:
    /// Input: nums = [1,2,3,4]
    /// Output: 2
    /// Explanation: The longest harmonious subsequences are [1,2], [2,3], and [3,4], all of which have a length of 2.
    ///
    /// Example 3:
    /// Input: nums = [1,1,1,1]
    /// Output: 0
    /// Explanation: No harmonic subsequence exists.
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 2 * 10^4
    /// - -10^9 &lt;= nums[i] &lt;= 10^9
    /// </para>
    /// <para>
    /// 594. 最長和諧子序列
    /// https://leetcode.cn/problems/longest-harmonious-subsequence/description/
    ///
    /// 若一個陣列的最大值與最小值之差恰好為 1，我們便將它定義為和諧陣列。
    ///
    /// 給定整數陣列 nums，回傳其所有可能子序列中最長和諧子序列的長度。
    ///
    /// 範例 1：
    /// 輸入：nums = [1,3,2,2,5,2,3,7]
    /// 輸出：5
    /// 解釋：最長和諧子序列為 [3,2,2,2,3]。
    ///
    /// 範例 2：
    /// 輸入：nums = [1,2,3,4]
    /// 輸出：2
    /// 解釋：最長和諧子序列為 [1,2]、[2,3] 與 [3,4]，長度皆為 2。
    ///
    /// 範例 3：
    /// 輸入：nums = [1,1,1,1]
    /// 輸出：0
    /// 解釋：不存在和諧子序列。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 2 * 10^4
    /// - -10^9 &lt;= nums[i] &lt;= 10^9
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1: [1,3,2,2,5,2,3,7]
        int[] nums1 = { 1, 3, 2, 2, 5, 2, 3, 7 };
        int result1 = solution.FindLHS(nums1);
        Console.WriteLine($"測試案例 1: [{string.Join(",", nums1)}]");
        Console.WriteLine($"結果: {result1}");
        Console.WriteLine($"說明: 最長和諧子序列是 [3,2,2,2,3]，長度為 5\n");

        // 測試案例 2: [1,2,3,4]
        int[] nums2 = { 1, 2, 3, 4 };
        int result2 = solution.FindLHS(nums2);
        Console.WriteLine($"測試案例 2: [{string.Join(",", nums2)}]");
        Console.WriteLine($"結果: {result2}");
        Console.WriteLine($"說明: 有多個長度為 2 的和諧子序列，如 [1,2]、[2,3]、[3,4]\n");

        // 測試案例 3: [1,1,1,1]
        int[] nums3 = { 1, 1, 1, 1 };
        int result3 = solution.FindLHS(nums3);
        Console.WriteLine($"測試案例 3: [{string.Join(",", nums3)}]");
        Console.WriteLine($"結果: {result3}");
        Console.WriteLine($"說明: 所有元素相同，無法形成和諧子序列\n");

        // 測試案例 4: [1,3,2,2,5,2,3,7,1]
        int[] nums4 = { 1, 3, 2, 2, 5, 2, 3, 7, 1 };
        int result4 = solution.FindLHS(nums4);
        Console.WriteLine($"測試案例 4: [{string.Join(",", nums4)}]");
        Console.WriteLine($"結果: {result4}");
        Console.WriteLine($"說明: 包含重複元素的更複雜案例");
    }

    /// <summary>
    /// 解題說明：
    /// 使用哈希表方法，先遍歷陣列統計每個數字的出現次數，然後遍歷哈希表，
    /// 對於每個數字 x，檢查 x+1 是否存在，如果存在，則 x 和 x+1 的出現次數之和
    /// 就是一個和諧子序列的長度。時間複雜度 O(n)，空間複雜度 O(n)。
    /// 
    /// 時間複雜度：O(n) - 需要遍歷陣列兩次，一次建立哈希表，一次查找
    /// 空間複雜度：O(n) - 哈希表存儲所有不同數字的計數
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>最長和諧子序列的長度</returns>
    public int FindLHS(int[] nums)
    {
        // 建立哈希表，統計每個數字的出現次數
        var count = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            if (count.ContainsKey(num))
            {
                count[num]++;
            }
            else
            {
                count[num] = 1;
            }
        }

        /* // 使用 TryGetValue 和 null-coalescing 運算子簡化程式碼
        var count = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            count[num] = count.GetValueOrDefault(num, 0) + 1;
        }
        */

        int maxLength = 0;
        // 遍歷哈希表中的每個鍵值對 (x, value)
        foreach (var key in count.Keys)
        {
            // 查詢 x+1 是否存在於哈希表中
            if (count.ContainsKey(key + 1))
            {
                // 如果 x+1 存在，則 x 和 x+1 的出現次數之和就是一個和諧子序列的長度
                maxLength = Math.Max(maxLength, count[key] + count[key + 1]);
            }
        }

        return maxLength;
    }
    
    
    /// <summary>
    /// 暴力解法 - 時間複雜度 O(n²)
    /// 問題：每個數字都要重新掃描整個陣列，效率很低！
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int FindLHS_BruteForce(int[] nums)
    {
        int maxLength = 0;

        // 對每個數字 x
        for (int i = 0; i < nums.Length; i++)
        {
            int x = nums[i];
            int countX = 0, countXPlus1 = 0;

            // 遍歷整個陣列統計 x 和 x+1 的出現次數
            for (int j = 0; j < nums.Length; j++)
            {
                if (nums[j] == x) countX++;
                else if (nums[j] == x + 1) countXPlus1++;
            }

            // 如果 x+1 存在，計算和諧子序列長度
            if (countXPlus1 > 0)
            {
                maxLength = Math.Max(maxLength, countX + countXPlus1);
            }
        }

        return maxLength;
    }    
}
