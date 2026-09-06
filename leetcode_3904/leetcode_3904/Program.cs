namespace leetcode_3904;

class Program
{
    /// <summary>
    /// 3904. Smallest Stable Index II
    /// https://leetcode.com/problems/smallest-stable-index-ii/description/
    ///
    /// You are given an integer array nums of length n and an integer k.
    /// For each index i, define its instability score as max(nums[0..i]) - min(nums[i..n - 1]).
    ///
    /// In other words:
    /// max(nums[0..i]) is the largest value among the elements from index 0 to i.
    /// min(nums[i..n - 1]) is the smallest value among the elements from index i to n - 1.
    /// An index i is called stable if its instability score is less than or equal to k.
    /// Return the smallest stable index. If no such index exists, return -1.
    ///
    /// 3904. 最小穩定下標 II
    /// https://leetcode.cn/problems/smallest-stable-index-ii/description/?envType=daily-question&amp;envId=2026-09-05
    ///
    /// 給定一個長度為 n 的整數陣列 nums，以及一個整數 k。
    /// 對於每個索引 i，將其不穩定分數定義為 max(nums[0..i]) - min(nums[i..n - 1])。
    ///
    /// 換句話說：
    /// max(nums[0..i]) 是索引 0 到 i 之間元素的最大值。
    /// min(nums[i..n - 1]) 是索引 i 到 n - 1 之間元素的最小值。
    /// 當索引 i 的不穩定分數小於或等於 k 時，稱索引 i 為穩定。
    /// 請回傳最小的穩定索引。如果不存在這樣的索引，請回傳 -1。
    /// </summary>
    /// <remarks>
    /// 程式進入點不要求使用者輸入，會執行六組固定案例，並以兩種解法交叉比對結果。
    /// 每組案例都會輸出 PASS 或 FAIL，最後列出通過與失敗的案例數量；若有案例失敗，程序會以非零狀態結束。
    /// </remarks>
    /// <param name="args">命令列參數；本程式不使用任何命令列輸入。</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (string name, int[] nums, int k, int expected)[] testCases =
        {
            ("官方範例 1", new[] { 5, 0, 1, 4 }, 3, 3),
            ("官方範例 2", new[] { 3, 2, 1 }, 1, -1),
            ("官方範例 3", new[] { 0 }, 0, 0),
            ("最小索引即可穩定", new[] { 2, 1, 3 }, 1, 0),
            ("不穩定分數等於 k", new[] { 5, 0, 1, 4 }, 1, 3),
            ("n = 100000 邊界", Enumerable.Range(1, 100_000).Reverse().ToArray(), 0, -1)
        };

        Console.WriteLine("=== 3904. Smallest Stable Index II ===");

        int passedCount = 0;
        foreach ((string name, int[] nums, int k, int expected) in testCases)
        {
            (int actual1, int actual2, bool passed) result = solver.RunTestCase(name, nums, k, expected);
            Console.WriteLine(
                $"{name}：預期：{expected}，方法一：{result.actual1}，方法二：{result.actual2}，結果：{(result.passed ? "PASS" : "FAIL")}");

            if (result.passed)
            {
                passedCount++;
            }
        }

        int totalCount = testCases.Length;
        Console.WriteLine($"總結：{passedCount}/{totalCount} 通過，{totalCount - passedCount} 個失敗。");
        Environment.ExitCode = passedCount == totalCount ? 0 : 1;
    }

    /// <summary>
    /// 執行一組固定測試案例，分別呼叫兩種線性解法並比較回傳結果。
    /// 輸入是案例名稱、符合題目限制的整數陣列、k 與預期的最小穩定索引；
    /// 回傳兩種方法的實際結果與案例是否通過。兩種解法都不會修改輸入陣列。
    /// </summary>
    /// <param name="name">測試案例名稱；僅供呼叫端識別案例。</param>
    /// <param name="nums">長度介於 1 到 100000、元素介於 0 到 10^9 的整數陣列。</param>
    /// <param name="k">允許的不穩定分數上限，介於 0 到 10^9。</param>
    /// <param name="expected">案例預期的最小穩定索引，若不存在則為 -1。</param>
    /// <returns>包含兩種方法實際結果，以及兩者是否都等於預期值的 tuple。</returns>
    private (int actual1, int actual2, bool passed) RunTestCase(string name, int[] nums, int k, int expected)
    {
        int actual1 = FirstStableIndex(nums, k);
        int actual2 = FirstStableIndex2(nums, k);
        bool passed = actual1 == expected && actual2 == expected;

        return (actual1, actual2, passed);
    }

    /// <summary>
    /// 先建立每個索引的前綴最大值與後綴最小值，再一次掃描尋找穩定索引。
    /// 前綴最大值 preMax[i] 代表 nums[0..i] 的最大值，後綴最小值 sufMin[i]
    /// 代表 nums[i..n-1] 的最小值。輸入必須是符合題目限制的非空陣列；
    /// 回傳第一個滿足 preMax[i] - sufMin[i] &lt;= k 的索引，若不存在則回傳 -1。
    /// 時間複雜度為 O(n)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums">符合題目限制的非空整數陣列。</param>
    /// <param name="k">允許的不穩定分數上限。</param>
    /// <returns>最小的穩定索引；若不存在則回傳 -1。</returns>
    public int FirstStableIndex(int[] nums, int k)
    {
        int n = nums.Length;
        int[] sufMin = new int[n];
        sufMin[n - 1] = nums[n - 1];

        // 從右到左建立後綴最小值，讓每個索引都能 O(1) 取得右側最小值。
        for (int i = n - 2; i >= 0; i--)
        {
            sufMin[i] = Math.Min(sufMin[i + 1], nums[i]);
        }

        int[] preMax = new int[n];
        preMax[0] = nums[0];

        // 從左到右建立前綴最大值，對應每個索引的左側區間。
        for (int i = 1; i < n; i++)
        {
            preMax[i] = Math.Max(preMax[i - 1], nums[i]);
        }

        // 由小索引開始檢查，第一個符合條件的索引就是答案。
        for (int i = 0; i < n; i++)
        {
            if (preMax[i] - sufMin[i] <= k)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 以後綴最小值陣列搭配單一前綴最大值變數尋找答案。
    /// 先從右到左計算 sufMin[i]，再從左到右逐步更新前綴最大值 preMax，
    /// 並在同一趟掃描中檢查不穩定分數。輸入必須是符合題目限制的非空陣列；
    /// 回傳第一個穩定索引，若不存在則回傳 -1。時間複雜度為 O(n)，
    /// 額外空間複雜度為 O(n)，且比 FirstStableIndex 少配置一個前綴陣列。
    /// </summary>
    /// <param name="nums">符合題目限制的非空整數陣列。</param>
    /// <param name="k">允許的不穩定分數上限。</param>
    /// <returns>最小的穩定索引；若不存在則回傳 -1。</returns>
    public int FirstStableIndex2(int[] nums, int k)
    {
        int n = nums.Length;
        int[] sufMin = new int[n];
        sufMin[n - 1] = nums[n - 1];

        // 先建立後綴最小值，讓之後的前綴掃描能直接取得 nums[i..n-1] 的最小值。
        for (int i = n - 2; i >= 0; i--)
        {
            sufMin[i] = Math.Min(sufMin[i + 1], nums[i]);
        }

        int preMax = 0;
        for (int i = 0; i < n; i++)
        {
            // 題目保證 nums[i] 非負，因此 0 可作為尚未讀取元素前的初始前綴最大值。
            // 不必建立 preMax 陣列；目前值就是 nums[0..i] 的前綴最大值。
            preMax = Math.Max(preMax, nums[i]);

            if (preMax - sufMin[i] <= k)
            {
                return i;
            }
        }

        return -1;
    }

}