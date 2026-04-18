namespace leetcode_3761;

class Program
{
    /// <summary>
    /// 3761. Minimum Absolute Distance Between Mirror Pairs
    /// https://leetcode.com/problems/minimum-absolute-distance-between-mirror-pairs/description/?envType=daily-question&amp;envId=2026-04-17
    /// 
    /// English:
    /// You are given an integer array nums.
    /// A mirror pair is a pair of indices (i, j) such that:
    /// 1. 0 &lt;= i &lt; j &lt; nums.length, and
    /// 2. reverse(nums[i]) == nums[j], where reverse(x) denotes the integer formed by reversing the digits of x.
    ///    Leading zeros are omitted after reversing, for example reverse(120) = 21.
    /// Return the minimum absolute distance between the indices of any mirror pair.
    /// The absolute distance between indices i and j is abs(i - j).
    /// If no mirror pair exists, return -1.
    /// 
    /// 繁體中文：
    /// 給定一個整數陣列 nums。
    /// 鏡像對是指一組索引 (i, j)，滿足：
    /// 1. 0 &lt;= i &lt; j &lt; nums.length，且
    /// 2. reverse(nums[i]) == nums[j]，其中 reverse(x) 表示將 x 的十進位數字反轉後所形成的整數。
    ///    反轉後會省略前導零，例如 reverse(120) = 21。
    /// 回傳任意鏡像對索引之間的最小絕對距離。
    /// 索引 i 與 j 之間的絕對距離為 abs(i - j)。
    /// 如果不存在任何鏡像對，回傳 -1。
    /// 
    /// 3761. 鏡像對之間最小絕對距離
    /// https://leetcode.cn/problems/minimum-absolute-distance-between-mirror-pairs/description/?envType=daily-question&amp;envId=2026-04-17
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
