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
    /// 方法一：分类讨论
    /// 判斷是否能將 <paramref name="nums1"/> 轉換成所有元素奇偶性一致的陣列。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 每個位置可以選擇保留原值，或執行 nums1[i] - nums1[j]，
    /// 但第二種操作必須滿足結果至少為 1，也就是 nums1[i] 必須嚴格大於 nums1[j]。
    /// </para>
    /// 
    /// <para>
    /// 由於只有減去奇數才會改變原本的奇偶性，因此關鍵在於陣列中的最小值。
    /// 最小值無法再減去一個更小的元素，所以它本身的奇偶性會限制最終陣列可以形成的奇偶性。
    /// </para>
    /// 
    /// <para>
    /// 分類如下：
    /// </para>
    /// 
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// 全部為偶數：不需要進行減法，直接保留所有元素即可，因此一定可以形成全偶數陣列。
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// 全部為奇數：同樣直接保留所有元素即可，因此一定可以形成全奇數陣列。
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// 同時存在奇數與偶數，且最小值為奇數：
    /// 可以將所有元素轉成奇數。原本的奇數直接保留；
    /// 偶數則減去最小的奇數，因為偶數減奇數為奇數，
    /// 且該奇數是陣列最小值，所以減法結果一定大於等於 1。
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// 同時存在奇數與偶數，且最小值為偶數：
    /// 無法完成轉換。最小的偶數本身無法透過減去更小的奇數變成奇數；
    /// 而若想全部轉成偶數，最小的奇數也無法找到比它更小的奇數來改變自身奇偶性。
    /// </description>
    /// </item>
    /// </list>
    /// 
    /// <para>
    /// 因此最終判斷可以簡化為：
    /// 如果最小值是奇數，則一定可行；
    /// 如果最小值是偶數，則只有陣列中完全不存在奇數時才可行。
    /// </para>
    /// 
    /// <para>
    /// 時間複雜度：O(n)，只需要遍歷陣列一次。<br/>
    /// 空間複雜度：O(1)。
    /// </para>
    /// </remarks>
    /// <param name="nums1">由互不相同整數組成的輸入陣列。</param>
    /// <returns>
    /// 如果可以構造出所有元素皆為奇數或所有元素皆為偶數的陣列，回傳 <see langword="true"/>；
    /// 否則回傳 <see langword="false"/>。
    /// </returns>
    public bool UniformArray(int[] nums1)
    {
        
    }
}
