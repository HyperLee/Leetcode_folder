namespace leetcode_3876;

class Program
{
    /// <summary>
    /// 3876. Construct Uniform Parity Array II
    /// https://leetcode.com/problems/construct-uniform-parity-array-ii/description/
    /// 3876. 构造奇偶一致的数组 II
    /// https://leetcode.cn/problems/construct-uniform-parity-array-ii/description/
    ///
    /// English:
    /// You are given an array nums1 of n distinct integers.
    ///
    /// You want to construct another array nums2 of length n such that the elements in nums2 are either all odd or all even.
    ///
    /// For each index i, you must choose exactly one of the following (in any order):
    /// <list type="bullet">
    /// <item><description><code>nums2[i] = nums1[i]</code></description></item>
    /// <item><description><code>nums2[i] = nums1[i] - nums1[j]</code>, for an index j != i, such that <code>nums1[i] - nums1[j] &gt;= 1</code></description></item>
    /// </list>
    ///
    /// Return true if it is possible to construct such an array, otherwise return false.
    ///
    /// 繁體中文：
    /// 給定一個由 n 個互不相同的整數組成的陣列 nums1。
    ///
    /// 你想要建構另一個長度為 n 的陣列 nums2，使 nums2 中的元素全部為奇數或全部為偶數。
    ///
    /// 對於每個索引 i，你必須從下列選項中恰好選擇一種（順序不限）：
    /// <list type="bullet">
    /// <item><description><code>nums2[i] = nums1[i]</code></description></item>
    /// <item><description><code>nums2[i] = nums1[i] - nums1[j]</code>，其中索引 j != i，且 <code>nums1[i] - nums1[j] &gt;= 1</code></description></item>
    /// </list>
    ///
    /// 如果可以建構出這樣的陣列，回傳 true；否則回傳 false。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="nums1"></param>
    /// <returns></returns>
    public bool UniformArray(int[] nums1)
    {
        int min = nums1.Min();
        bool hasOdd = nums1.Any(x => x % 2 != 0);

        return min % 2 != 0 || !hasOdd;
    }

    /// <summary>
    /// | 陣列情況 | 最小值 | `hasOdd` | 結果 |
    /// |---|---:|---:|---:|
    /// | 全偶數 | 偶數 | false | true |
    /// | 全奇數 | 奇數 | true | true |
    /// | 奇偶混合，最小值奇數 | 奇數 | true | true |
    /// | 奇偶混合，最小值偶數 | 偶數 | true | false |
    /// => 只有「最小值為偶數，而且陣列內還存在奇數」時失敗。 
    /// </summary>
    /// <param name="nums1"></param>
    /// <returns></returns>
    public bool UniformArray2(int[] nums1)
    {
        int min = nums1.Min();
        bool hasOdd = nums1.Any(x => x % 2 != 0);

        if(min % 2 != 0)
        {
            return true;
        }        

        if(!hasOdd)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 最小值無法透過第二種操作改變 ->
    /// 最小值決定目標奇偶性 ->
    /// 如果最小值是奇數，可以拿它去改變所有較大的偶數 ->
    /// 如果最小值是偶數，而陣列又有奇數，就無法統一奇偶性
    /// </summary>
    /// <param name="nums1"></param>
    /// <returns></returns>
    public bool UniformArray3(int[] nums1)
    {
        int min = int.MaxValue;
        bool hasOdd = false;

        foreach(int num in nums1)
        {
            min = Math.Min(min, num);

            if(num % 2 != 0)
            {
                hasOdd = true;
            }
        }
        return min % 2 != 0 || !hasOdd;
    }
}
