namespace leetcode_228;

class Program
{
    /// <summary>
    /// 228. Summary Ranges
    /// https://leetcode.com/problems/summary-ranges/description/
    /// 228. 彙總區間
    /// https://leetcode.cn/problems/summary-ranges/description/
    ///
    /// English original:
    /// You are given a sorted unique integer array nums.
    ///
    /// A range [a,b] is the set of all integers from a to b (inclusive).
    ///
    /// Return the smallest sorted list of ranges that cover all the numbers in the array exactly.
    /// That is, each element of nums is covered by exactly one of the ranges, and there is no integer x
    /// such that x is in one of the ranges but not in nums.
    ///
    /// Each range [a,b] in the list should be output as:
    ///
    /// "a->b" if a != b
    /// "a" if a == b
    ///
    /// 繁體中文：
    /// 給定一個已排序且所有元素皆唯一的整數陣列 nums。
    ///
    /// 範圍 [a,b] 是從 a 到 b 的所有整數集合（包含 a 與 b）。
    ///
    /// 請回傳最小的已排序範圍列表，使其能精確涵蓋陣列中的所有數字。
    /// 也就是說，nums 中每個元素都只會被一個範圍涵蓋一次，且不存在任何整數 x
    /// 屬於某個範圍但不屬於 nums。
    ///
    /// 列表中的每個範圍 [a,b] 應輸出為：
    ///
    /// 若 a != b，輸出 "a->b"
    /// 若 a == b，輸出 "a"
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
