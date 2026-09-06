namespace leetcode_3904;

class Program
{
    /// <summary>
    /// 3904. Smallest Stable Index II
    /// https://leetcode.com/problems/smallest-stable-index-ii/description/
    ///
    /// You are given an integer array nums of length n and an integer k.
    /// For each index i, define its instability score as max(nums[0..i]) - min(nums[i..n - 1]).
    ///
    /// In other words:
    /// max(nums[0..i]) is the largest value among the elements from index 0 to i.
    /// min(nums[i..n - 1]) is the smallest value among the elements from index i to n - 1.
    /// An index i is called stable if its instability score is less than or equal to k.
    /// Return the smallest stable index. If no such index exists, return -1.
    ///
    /// 3904. 最小穩定下標 II
    /// https://leetcode.cn/problems/smallest-stable-index-ii/description/?envType=daily-question&amp;envId=2026-09-05
    ///
    /// 給定一個長度為 n 的整數陣列 nums，以及一個整數 k。
    /// 對於每個索引 i，將其不穩定分數定義為 max(nums[0..i]) - min(nums[i..n - 1])。
    ///
    /// 換句話說：
    /// max(nums[0..i]) 是索引 0 到 i 之間元素的最大值。
    /// min(nums[i..n - 1]) 是索引 i 到 n - 1 之間元素的最小值。
    /// 當索引 i 的不穩定分數小於或等於 k 時，稱索引 i 為穩定。
    /// 請回傳最小的穩定索引。如果不存在這樣的索引，請回傳 -1。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}