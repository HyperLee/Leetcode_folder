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
    /// 1. 0 <= i < j < nums.length，且
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
        Program solution = new Program();

        // 測試案例 1: [1, 3, 2, 3, 1]
        // 鏡像對 (1,3): reverse(3)=3，距離 2；鏡像對 (0,4): reverse(1)=1，距離 4 → 最小 2
        int[] nums1 = [1, 3, 2, 3, 1];
        Console.WriteLine($"Test 1: {solution.MinMirrorPairDistance(nums1)}"); // 預期: 2

        // 測試案例 2: [4, 5, 6]
        // 無任何鏡像對 → 回傳 -1
        int[] nums2 = [4, 5, 6];
        Console.WriteLine($"Test 2: {solution.MinMirrorPairDistance(nums2)}"); // 預期: -1

        // 測試案例 3: [12, 21, 10, 1]
        // 鏡像對 (0,1): reverse(12)=21，距離 1；鏡像對 (2,3): reverse(10)=1，距離 1 → 最小 1
        int[] nums3 = [12, 21, 10, 1];
        Console.WriteLine($"Test 3: {solution.MinMirrorPairDistance(nums3)}"); // 預期: 1
    }

    /// <summary>
    /// 方法一：一次遍歷（哈希表）
    /// <para>
    /// 解題概念：從左到右遍歷陣列，維護哈希表 <c>reversedToLastIndex</c>，其中 <c>reversedToLastIndex[v]</c>
    /// 儲存最近的索引 j，滿足 <c>reverse(nums[j]) == v</c>。
    /// </para>
    /// <para>
    /// 演算法步驟：
    /// <list type="number">
    ///   <item>對於當前位置 i，令 current = nums[i]。</item>
    ///   <item>若 current 存在於 reversedToLastIndex，表示存在 j 使得 reverse(nums[j]) == current，
    ///         (j, i) 為鏡像對，以 i - reversedToLastIndex[current] 更新 minDistance。</item>
    ///   <item>計算 reverse(current) 並令 reversedToLastIndex[reverse(current)] = i，
    ///         讓目前索引與後續值為 reverse(current) 的元素形成鏡像對。</item>
    ///   <item>同一個鍵只保留最近索引：右端點固定時，左端點越靠右距離越小。</item>
    /// </list>
    /// </para>
    /// <para>時間複雜度：O(length × D)，D 為數字最大位數（int 最多 10 位）。</para>
    /// <para>空間複雜度：O(length)，哈希表最多儲存 length 組鍵值對。</para>
    /// </summary>
    /// <param name="nums">輸入整數陣列。</param>
    /// <returns>所有鏡像對中索引絕對距離的最小值；若無鏡像對則回傳 -1。</returns>
    public int MinMirrorPairDistance(int[] nums)
    {
        // reversedToLastIndex[v] = 最近一個滿足 reverse(nums[j]) == v 的索引 j
        Dictionary<int, int> reversedToLastIndex = new Dictionary<int, int>();
        int length = nums.Length;
        int minDistance = int.MaxValue;

        for (int i = 0; i < length; i++)
        {
            int current = nums[i];

            // 若 current 已在 reversedToLastIndex 中，代表存在 j 使 reverse(nums[j]) == current → 形成鏡像對
            if (reversedToLastIndex.ContainsKey(current))
            {
                minDistance = Math.Min(minDistance, i - reversedToLastIndex[current]);
            }

            // 記錄 reverse(current) 對應的最近索引，供後續元素配對使用
            reversedToLastIndex[ReverseNum(current)] = i;
        }

        return minDistance == int.MaxValue ? -1 : minDistance;
    }

    /// <summary>
    /// 將非負整數 num 的十進位數字反轉，自動省略前導零。
    /// <para>
    /// 演算法：每次取 num 末位數字（num % 10）附加到 reversed 末位，
    /// 再將 num 右移一位（num /= 10），重複直到 num 歸零。
    /// </para>
    /// <example>
    /// <code>
    /// ReverseNum(120) → 21
    /// ReverseNum(123) → 321
    /// ReverseNum(1)   → 1
    /// </code>
    /// </example>
    /// <para>時間複雜度：O(D)，D 為 num 的位數。</para>
    /// <para>空間複雜度：O(1)。</para>
    /// </summary>
    /// <param name="num">欲反轉的非負整數。</param>
    /// <returns>反轉後的整數（省略前導零）。</returns>
    private int ReverseNum(int num)
    {
        int reversed = 0;
        while (num > 0)
        {
            reversed = reversed * 10 + num % 10; // 將末位數字附加到 reversed 末位
            num /= 10;                            // 移除 num 的末位數字
        }

        return reversed;
    }
}
