namespace leetcode_3903;

class Program
{
    /// <summary>
    /// 3903. Smallest Stable Index I
    /// https://leetcode.com/problems/smallest-stable-index-i/description/
    ///
    /// English:
    /// You are given an integer array nums of length n and an integer k.
    ///
    /// For each index i, define its instability score as max(nums[0..i]) - min(nums[i..n - 1]).
    ///
    /// In other words:
    /// max(nums[0..i]) is the largest value among the elements from index 0 to i.
    /// min(nums[i..n - 1]) is the smallest value among the elements from index i to n - 1.
    ///
    /// An index i is called stable if its instability score is less than or equal to k.
    ///
    /// Return the smallest stable index. If no such index exists, return -1.
    ///
    /// 繁體中文：
    /// 給定一個長度為 n 的整數陣列 nums，以及一個整數 k。
    ///
    /// 對於每個索引 i，定義其不穩定分數為 max(nums[0..i]) - min(nums[i..n - 1])。
    ///
    /// 換句話說：
    /// max(nums[0..i]) 是索引 0 到 i 之間元素的最大值。
    /// min(nums[i..n - 1]) 是索引 i 到 n - 1 之間元素的最小值。
    ///
    /// 如果索引 i 的不穩定分數小於或等於 k，則稱 i 為穩定索引。
    ///
    /// 請回傳最小的穩定索引。如果不存在任何穩定索引，請回傳 -1。
    ///
    /// https://leetcode.cn/problems/smallest-stable-index-i/description/
    /// </summary>
    /// <param name="args">命令列參數；本程式不使用任何命令列輸入。</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
