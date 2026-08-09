namespace leetcode_3432;

class Program
{
    /// <summary>
    /// 3432. Count Partitions with Even Sum Difference
    /// https://leetcode.com/problems/count-partitions-with-even-sum-difference/description/
    /// <para>
    /// You are given an integer array nums of length n.
    ///
    /// A partition is an index i where 0 &lt;= i &lt; n - 1, splitting the array into two non-empty subarrays: the left contains [0, i], and the right contains [i + 1, n - 1].
    ///
    /// Return the number of partitions where the difference between the left and right sums is even.
    ///
    /// Example 1:
    /// Input: nums = [10,10,3,7,6]
    /// Output: 4
    /// Explanation: The four partitions have sum differences 10 - 26 = -16, 20 - 16 = 4, 23 - 13 = 10, and 30 - 6 = 24; all are even.
    ///
    /// Example 2:
    /// Input: nums = [1,2,2]
    /// Output: 0
    /// Explanation: No partition has an even sum difference.
    ///
    /// Example 3:
    /// Input: nums = [2,4,6,8]
    /// Output: 3
    /// Explanation: Every partition has an even sum difference.
    ///
    /// Constraints:
    /// - 2 &lt;= n == nums.length &lt;= 100
    /// - 1 &lt;= nums[i] &lt;= 100
    /// </para>
    /// <para>
    /// 3432. 計算總和差為偶數的分割數量
    /// https://leetcode.cn/problems/count-partitions-with-even-sum-difference/description/
    ///
    /// 給定長度為 n 的整數陣列 nums。
    ///
    /// 分割是滿足 0 &lt;= i &lt; n - 1 的索引 i，將陣列分成兩個非空子陣列：左側包含 [0, i]，右側包含 [i + 1, n - 1]。
    ///
    /// 回傳左、右子陣列總和之差為偶數的分割數量。
    ///
    /// 範例 1：
    /// 輸入：nums = [10,10,3,7,6]
    /// 輸出：4
    /// 解釋：四個分割的總和差分別為 10 - 26 = -16、20 - 16 = 4、23 - 13 = 10、30 - 6 = 24，全部都是偶數。
    ///
    /// 範例 2：
    /// 輸入：nums = [1,2,2]
    /// 輸出：0
    /// 解釋：沒有分割會產生偶數的總和差。
    ///
    /// 範例 3：
    /// 輸入：nums = [2,4,6,8]
    /// 輸出：3
    /// 解釋：所有分割都會產生偶數的總和差。
    ///
    /// 限制條件：
    /// - 2 &lt;= n == nums.length &lt;= 100
    /// - 1 &lt;= nums[i] &lt;= 100
    /// </para>
    /// </summary>
    /// <param name="args">命令列引數（未使用）</param>
    static void Main(string[] args)
    {
        var program = new Program();

        var tests = new (int[] nums, int expected)[]
        {
            (new int[] { 2, 1, 6, 4 }, 0), // sum = 13 -> odd -> 0
            (new int[] { 1, 1, 1, 1 }, 3), // sum = 4 -> even -> n - 1 = 3
            (new int[] { 2 }, 0),         // n = 1 -> no valid split -> 0
            (new int[] { 1, 2, 3 }, 2),   // sum = 6 -> even -> n - 1 = 2
        };

        Console.WriteLine("CountPartitions - Tests:");
        foreach (var (nums, expected) in tests)
        {
            int result = program.CountPartitions(nums);
            Console.WriteLine($"nums=[{string.Join(',', nums)}], expected={expected}, result={result}");
        }
    }

    /// <summary>
    /// Explanation / 思路：
    /// 方法一（直觀）：
    /// 令整數陣列總和為 S，左子陣列和為 L，右子陣列和為 S - L。差值為 L − (S − L) = 2L − S。
    /// 由於 2L 始終為偶數，差值的奇偶性只由 S 決定：如果 S 為奇數，差值非偶；S 為偶數，差值為偶。
    /// 於是當 S 為偶數時，所有的分割（n - 1）都有效；當 S 為奇數時，沒有任何分割有效。
    /// 
    /// 當 S 為奇數時，左右子陣列和一奇一偶或一偶一奇，差值為奇數 —— 答案為 0；
    /// 當 S 為偶數時，左右兩邊同為奇或同為偶，差值必為偶數 —— 答案為 n - 1。
    /// Complexity: O(n) time to compute the sum, O(1) extra space.
    /// </summary>
    /// <param name="nums">Input array of integers (non-null, length >= 1).</param>
    /// <returns>Number of valid partitions where difference is even.</returns>
    public int CountPartitions(int[] nums)
    {
        int totalSum = 0;
        foreach (int x in nums)
        {
            totalSum += x;
        }

        return (totalSum % 2 == 0) ? nums.Length - 1 : 0;
    }
}
