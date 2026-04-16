namespace leetcode_3488;

class Program
{
    /// <summary>
    /// 3488. Closest Equal Element Queries
    /// https://leetcode.com/problems/closest-equal-element-queries/description/?envType=daily-question&envId=2026-04-16
    /// 3488. 距離最小相等元素查詢
    /// https://leetcode.cn/problems/closest-equal-element-queries/description/?envType=daily-question&envId=2026-04-16
    ///
    /// [EN]
    /// You are given a circular array nums and an array queries.
    /// For each query i, you have to find the following:
    /// The minimum distance between the element at index queries[i] and any other index j
    /// in the circular array, where nums[j] == nums[queries[i]].
    /// If no such index exists, the answer for that query should be -1.
    /// Return an array answer of the same size as queries, where answer[i] represents the result for query i.
    ///
    /// [繁體中文]
    /// 給你一個環形陣列 nums 以及一個查詢陣列 queries。
    /// 對於每個查詢 i，你需要找出以下內容：
    /// 在環形陣列中，索引 queries[i] 處的元素與任意其他索引 j（滿足 nums[j] == nums[queries[i]]）之間的最小距離。
    /// 若不存在這樣的索引，則該查詢的答案為 -1。
    /// 回傳一個與 queries 大小相同的陣列 answer，其中 answer[i] 代表第 i 個查詢的結果。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
