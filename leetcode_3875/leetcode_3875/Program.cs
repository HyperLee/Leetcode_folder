namespace leetcode_3875;

class Program
{
    /// <summary>
    /// 3875. Construct Uniform Parity Array I
    /// https://leetcode.com/problems/construct-uniform-parity-array-i/description/
    ///
    /// English:
    /// You are given an array nums1 of n distinct integers.
    ///
    /// You want to construct another array nums2 of length n such that the elements in nums2 are either all odd or all even.
    ///
    /// For each index i, you must choose exactly one of the following (in any order):
    ///
    /// nums2[i] = nums1[i]
    /// nums2[i] = nums1[i] - nums1[j], for an index j != i
    ///
    /// Return true if it is possible to construct such an array, otherwise, return false.
    ///
    /// 繁體中文：
    /// 給定一個包含 n 個互不相同整數的陣列 nums1。
    ///
    /// 你想要建立另一個長度為 n 的陣列 nums2，使 nums2 中的元素要嘛全部為奇數，要嘛全部為偶數。
    ///
    /// 對於每個索引 i，你必須從下列選項中恰好選擇一個（選擇順序不限）：
    ///
    /// nums2[i] = nums1[i]
    /// nums2[i] = nums1[i] - nums1[j]，其中索引 j != i
    ///
    /// 如果可以建立出符合條件的陣列，請回傳 true；否則回傳 false。
    ///
    /// https://leetcode.cn/problems/construct-uniform-parity-array-i/description/
    ///
    /// </summary>
    /// <param name="args">Command-line arguments supplied to the program.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
