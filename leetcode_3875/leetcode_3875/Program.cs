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
    /// <remarks>
    /// 程式進入點不要求使用者輸入，會執行五組固定案例並輸出每組 PASS/FAIL 與總結。
    /// 案例涵蓋官方範例、全部為奇數、n = 1 與 n = 100 邊界。
    /// </remarks>
    /// <param name="args">命令列參數；此範例不使用。</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (string Name, int[] Nums1, bool Expected)[] testCases =
        {
            ("官方範例 1：奇偶混合", new[] { 2, 3 }, true),
            ("官方範例 2：全部為偶數", new[] { 4, 6 }, true),
            ("全部為奇數", new[] { 1, 3, 5 }, true),
            ("n = 1 邊界", new[] { 1 }, true),
            ("n = 100 邊界", Enumerable.Range(1, 100).ToArray(), true)
        };

        Console.WriteLine("=== 3875. Construct Uniform Parity Array I ===");

        int passedCount = 0;
        foreach ((string name, int[] nums1, bool expected) in testCases)
        {
            passedCount += solver.RunTestCase(name, nums1, expected);
        }

        int totalCount = testCases.Length;
        Console.WriteLine($"總結：{passedCount}/{totalCount} 通過，{totalCount - passedCount} 個失敗。");
    }

    /// <summary>
    /// 執行一組固定測試案例，呼叫 UniformArray 並比較預期與實際結果。
    /// 解題驗證概念是確認題目限制下的陣列都能構造出奇偶性一致的 nums2。
    /// 輸入是案例名稱、符合限制的 nums1 與預期布林值；輸出 1 表示 PASS，0 表示 FAIL。
    /// </summary>
    /// <param name="name">測試案例名稱。</param>
    /// <param name="nums1">長度 1 到 100、元素介於 1 到 100 且互異的整數陣列。</param>
    /// <param name="expected">案例預期的可行性結果。</param>
    /// <returns>案例通過時回傳 1，否則回傳 0。</returns>
    private int RunTestCase(string name, int[] nums1, bool expected)
    {
        bool actual = UniformArray(nums1);
        bool passed = actual == expected;

        Console.WriteLine(
            $"{name}：預期：{expected}，實際：{actual}，結果：{(passed ? "PASS" : "FAIL")}");

        return passed ? 1 : 0;
    }

    /// <summary>
    /// 輸入需符合題目限制：長度 1 到 100、元素介於 1 到 100 且互異。
    /// 由於每個合法輸入都可行，方法不需要真的建立 nums2，輸出固定為 true。
    /// </summary>
    /// <param name="nums1">符合題目限制的互異整數陣列。</param>
    /// <returns>若能建立奇偶性一致的 nums2 則回傳 true；合法輸入下固定為 true。</returns>
    public bool UniformArray(int[] nums1)
    {
        // 同奇或同偶時直接保留元素；奇偶混合時任取奇數 x，
        // 偶數減去 x 會變成奇數，奇數則保留，因此一定存在合法 nums2。
        return true;
    }
}
