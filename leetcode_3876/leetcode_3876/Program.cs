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
    /// <remarks>
    /// 程式進入點不需要使用者輸入，會以六組固定案例驗證三種解法，
    /// 並輸出每項檢查的 PASS/FAIL 結果與最終統計。
    /// </remarks>
    /// <param name="args">命令列參數；此範例不使用。</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (string Name, int[] Nums1, bool Expected)[] testCases =
        {
            ("官方範例 1：最小值為奇數", new[] { 1, 4, 7 }, true),
            ("官方範例 2：最小值為偶數且存在奇數", new[] { 2, 3 }, false),
            ("官方範例 3：全部為偶數", new[] { 4, 6 }, true),
            ("全部為奇數", new[] { 1, 3, 5 }, true),
            ("單一元素", new[] { 9 }, true),
            ("最大長度", Enumerable.Range(1, 100_000).ToArray(), true)
        };
        (string Name, Func<int[], bool> Solve)[] solutions =
        {
            (nameof(UniformArray), solver.UniformArray),
            (nameof(UniformArray2), solver.UniformArray2),
            (nameof(UniformArray3), solver.UniformArray3)
        };

        Console.WriteLine("=== 3876. Construct Uniform Parity Array II ===");

        int passedCount = 0;
        foreach ((string caseName, int[] nums1, bool expected) in testCases)
        {
            foreach ((string solutionName, Func<int[], bool> solve) in solutions)
            {
                passedCount += RunTestCase(caseName, nums1, expected, solutionName, solve);
            }
        }

        int totalCount = testCases.Length * solutions.Length;
        Console.WriteLine($"總結：{passedCount}/{totalCount} 通過，{totalCount - passedCount} 個失敗。");
    }

    /// <summary>
    /// 執行一組指定解法的固定測試，將符合題目限制的非空互異整數陣列傳入解法，
    /// 比較預期與實際布林值並輸出結果；測試通過時回傳 1，否則回傳 0。
    /// </summary>
    /// <param name="caseName">測試案例的顯示名稱。</param>
    /// <param name="nums1">長度 1 到 100,000，元素介於 1 到 1,000,000,000 且互異的陣列。</param>
    /// <param name="expected">案例預期的可行性結果。</param>
    /// <param name="solutionName">目前受測解法的名稱。</param>
    /// <param name="solve">接受輸入陣列並回傳可行性結果的解法。</param>
    /// <returns>預期與實際結果相同時回傳 1，否則回傳 0。</returns>
    private static int RunTestCase(
        string caseName,
        int[] nums1,
        bool expected,
        string solutionName,
        Func<int[], bool> solve)
    {
        bool actual = solve(nums1);
        bool passed = actual == expected;

        Console.WriteLine(
            $"{caseName} | {solutionName}：預期 {expected}，實際 {actual}，結果 {(passed ? "PASS" : "FAIL")}");

        return passed ? 1 : 0;
    }

    /// <summary>
    /// 使用 LINQ 分別取得最小值並判斷是否存在奇數。
    /// 輸入必須是符合題目限制的非空互異正整數陣列；若最小值是奇數，或陣列完全沒有奇數，
    /// 即可將所有元素統一為相同奇偶性並回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="nums1">長度 1 到 100,000，元素介於 1 到 1,000,000,000 且互異的陣列。</param>
    /// <returns>可以建構出奇偶性一致的 nums2 時回傳 true，否則回傳 false。</returns>
    public bool UniformArray(int[] nums1)
    {
        // 最小值無法減去更小的陣列元素，因此它的奇偶性無法被第二種操作改變。
        int min = nums1.Min();
        bool hasOdd = nums1.Any(x => x % 2 != 0);

        // 唯一失敗情況是最小值為偶數，但陣列內仍有無法統一成偶數的奇數。
        return min % 2 != 0 || !hasOdd;
    }

    /// <summary>
    /// 使用顯式條件分支呈現分類討論。
    /// 輸入必須是符合題目限制的非空互異正整數陣列；最小值為奇數或全體皆為偶數時回傳 true，
    /// 最小值為偶數且同時存在奇數時回傳 false。
    /// </summary>
    /// <param name="nums1">長度 1 到 100,000，元素介於 1 到 1,000,000,000 且互異的陣列。</param>
    /// <returns>可以建構出奇偶性一致的 nums2 時回傳 true，否則回傳 false。</returns>
    public bool UniformArray2(int[] nums1)
    {
        int min = nums1.Min();
        bool hasOdd = nums1.Any(x => x % 2 != 0);

        // 最小值為奇數時，可以用它把所有較大的偶數轉成奇數。
        if (min % 2 != 0)
        {
            return true;
        }

        // 最小值為偶數時，只有原陣列已經全為偶數才能直接保留所有元素。
        if (!hasOdd)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 使用單次迴圈同步尋找最小值與記錄奇數是否存在。
    /// 輸入必須是符合題目限制的非空互異正整數陣列；掃描完成後依最小值奇偶性與奇數存在性
    /// 判斷能否統一所有元素的奇偶性，並回傳對應布林結果。
    /// </summary>
    /// <param name="nums1">長度 1 到 100,000，元素介於 1 到 1,000,000,000 且互異的陣列。</param>
    /// <returns>可以建構出奇偶性一致的 nums2 時回傳 true，否則回傳 false。</returns>
    public bool UniformArray3(int[] nums1)
    {
        int min = int.MaxValue;
        bool hasOdd = false;

        // 一次走訪同時收集最終判斷所需的兩項資訊。
        foreach (int num in nums1)
        {
            min = Math.Min(min, num);

            if (num % 2 != 0)
            {
                hasOdd = true;
            }
        }

        return min % 2 != 0 || !hasOdd;
    }
}