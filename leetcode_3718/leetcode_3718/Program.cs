namespace leetcode_3718;

class Program
{
    /// <summary>
    /// 3718. Smallest Missing Multiple of K
    ///
    /// English:
    /// Given an integer array nums and an integer k, return the smallest positive multiple of k that is missing from nums.
    ///
    /// A multiple of k is any positive integer divisible by k.
    ///
    /// 繁體中文：
    /// 給定一個整數陣列 nums 和一個整數 k，請回傳 nums 中缺少的最小正整數 k 的倍數。
    ///
    /// k 的倍數是任何可被 k 整除的正整數。
    /// English problem:
    /// https://leetcode.com/problems/smallest-missing-multiple-of-k/description
    /// 中文題目：
    /// https://leetcode.cn/problems/smallest-missing-multiple-of-k/description
    /// </summary>
    /// <remarks>
    /// 此進入點執行三組不需使用者輸入的固定案例，並以相同資料驗證三種解法。
    /// 輸入條件遵循題目限制；輸出包含每種解法的預期值、實際值、PASS/FAIL 與總通過數。
    /// </remarks>
    /// <param name="args">Command-line arguments; no input is required.</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (string Name, int[] Nums, int K, int Expected)[] testCases =
        {
            ("官方範例 1", new[] { 8, 2, 3, 4, 6 }, 2, 10),
            ("官方範例 2", new[] { 1, 4, 7, 10, 15 }, 5, 5),
            ("邊界案例", Enumerable.Range(1, 100).ToArray(), 1, 101)
        };

        Console.WriteLine("=== 3718. Smallest Missing Multiple of K ===");

        int passedCount = 0;

        foreach ((string name, int[] nums, int k, int expected) in testCases)
        {
            passedCount += RunTestCase(solver, name, nums, k, expected);
        }

        int totalCount = testCases.Length * 3;
        Console.WriteLine($"總結：{passedCount}/{totalCount} 通過，{totalCount - passedCount} 個失敗。");
    }

    /// <summary>
    /// 使用同一組固定資料執行三種解法，逐一比對預期值並輸出 PASS/FAIL。
    /// 解題驗證概念是讓所有解法面對相同輸入，且各自取得陣列複本，避免方法之間互相影響。
    /// 輸入需符合題目限制；回傳本案例中通過驗證的解法數量，範圍為 0 到 3。
    /// </summary>
    /// <param name="solver">提供三種解法的 <see cref="Program"/> 執行個體。</param>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="nums">長度為 1 到 100，且每個元素介於 1 到 100 的整數陣列。</param>
    /// <param name="k">介於 1 到 100 的正整數倍數基準。</param>
    /// <param name="expected">此案例預期得到的最小缺失正倍數。</param>
    /// <returns>三種解法中結果等於 <paramref name="expected"/> 的數量。</returns>
    private static int RunTestCase(Program solver, string name, int[] nums, int k, int expected)
    {
        (string MethodName, int Actual)[] results =
        {
            (nameof(MissingMultiple), solver.MissingMultiple((int[])nums.Clone(), k)),
            (nameof(MissingMultiple2), solver.MissingMultiple2((int[])nums.Clone(), k)),
            (nameof(MissingMultiple3), solver.MissingMultiple3((int[])nums.Clone(), k))
        };

        Console.WriteLine($"{name}：nums = [{string.Join(", ", nums)}], k = {k}, expected = {expected}");

        int passedCount = 0;

        foreach ((string methodName, int actual) in results)
        {
            bool isPassed = actual == expected;

            if (isPassed)
            {
                passedCount++;
            }

            Console.WriteLine($"  {methodName}: actual = {actual}, {(isPassed ? "PASS" : "FAIL")}");
        }

        Console.WriteLine();
        return passedCount;
    }

    /// <summary>
    /// 方法一：使用雜湊集合與加法枚舉，找出陣列中缺少的最小正整數 <paramref name="k"/> 倍數。
    /// 解題概念是先將所有元素存入 <see cref="HashSet{T}"/>，再從 <paramref name="k"/> 開始每次加上
    /// <paramref name="k"/>；第一個不在集合中的候選值就是答案。
    /// 輸入需符合題目限制；輸出為未出現在 <paramref name="nums"/> 中的最小正整數 <paramref name="k"/> 倍數。
    /// </summary>
    /// <param name="nums">長度為 1 到 100，且每個元素介於 1 到 100 的整數陣列。</param>
    /// <param name="k">介於 1 到 100 的正整數倍數基準。</param>
    /// <returns>未出現在 <paramref name="nums"/> 中的最小正整數 <paramref name="k"/> 倍數。</returns>
    public int MissingMultiple(int[] nums, int k)
    {
        // HashSet 讓每個候選倍數能以平均 O(1) 時間判斷是否出現在陣列中。
        HashSet<int> seen = new HashSet<int>(nums);
        int multiple = k;

        // 候選值依序為 k、2k、3k；第一個不存在的值就是最小缺失倍數。
        while (seen.Contains(multiple))
        {
            multiple += k;
        }

        return multiple;
    }

    /// <summary>
    /// 方法二：使用固定大小的布林陣列標記已出現數字，找出缺少的最小正整數 <paramref name="k"/> 倍數。
    /// 解題概念是利用元素最大值為 100 的限制，讓索引直接代表數值是否存在，再依序檢查
    /// <paramref name="k"/>、2<paramref name="k"/>、3<paramref name="k"/>。
    /// 輸入需符合題目限制；輸出為未出現在 <paramref name="nums"/> 中的最小正整數 <paramref name="k"/> 倍數。
    /// </summary>
    /// <param name="nums">長度為 1 到 100，且每個元素介於 1 到 100 的整數陣列。</param>
    /// <param name="k">介於 1 到 100 的正整數倍數基準。</param>
    /// <returns>未出現在 <paramref name="nums"/> 中的最小正整數 <paramref name="k"/> 倍數。</returns>
    public int MissingMultiple2(int[] nums, int k)
    {
        // 題目保證 nums[i] <= 100，因此索引 0 到 100 足以標記所有可能出現的值。
        bool[] exists = new bool[101];

        foreach (int num in nums)
        {
            exists[num] = true;
        }

        int multiple = k;

        // 候選值超過 100 時，依輸入限制可直接確定它不可能出現在 nums 中。
        while (multiple <= 100 && exists[multiple])
        {
            multiple += k;
        }

        return multiple;
    }

    /// <summary>
    /// 方法三：使用雜湊集合與乘數枚舉，找出陣列中缺少的最小正整數 <paramref name="k"/> 倍數。
    /// 解題概念是令乘數從 1 開始遞增，依序檢查 <paramref name="k"/> × 1、
    /// <paramref name="k"/> × 2、<paramref name="k"/> × 3；第一個不在集合中的乘積就是答案。
    /// 輸入需符合題目限制；輸出為未出現在 <paramref name="nums"/> 中的最小正整數 <paramref name="k"/> 倍數。
    /// </summary>
    /// <param name="nums">長度為 1 到 100，且每個元素介於 1 到 100 的整數陣列。</param>
    /// <param name="k">介於 1 到 100 的正整數倍數基準。</param>
    /// <returns>未出現在 <paramref name="nums"/> 中的最小正整數 <paramref name="k"/> 倍數。</returns>
    public int MissingMultiple3(int[] nums, int k)
    {
        HashSet<int> seen = new HashSet<int>(nums);

        // multiplier 代表目前檢查第幾個正倍數，因此必須從 1 倍開始。
        int multiplier = 1;

        while (seen.Contains(k * multiplier))
        {
            multiplier++;
        }

        return k * multiplier;
    }
}
