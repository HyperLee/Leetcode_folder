namespace leetcode_3432;

class Program
{
    /// <summary>
    /// 3432. Count Partitions with Even Sum Difference
    /// https://leetcode.com/problems/count-partitions-with-even-sum-difference/description/?envType=daily-question&envId=2025-12-05
    /// 3432. 统计元素和差值为偶数的分区方案
    /// https://leetcode.cn/problems/count-partitions-with-even-sum-difference/description/?envType=daily-question&envId=2025-12-05
    /// 繁體中文翻譯：
    /// 給定一個長度為 n 的整數陣列 nums。
    /// 分割（partition）定義為索引 i，其中 0 <= i < n - 1，將陣列拆成兩個非空子陣列：
    /// 左子陣列包含索引 [0, i]。
    /// 右子陣列包含索引 [i + 1, n - 1]。
    /// 請返回使左子陣列與右子陣列元素總和之差為偶數的分割數量。
    /// </summary>
    /// <param name="args">命令列引數（未使用）</param>
    static void Main(string[] args)
    {
        // Basic runner / test harness for CountPartitions
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
