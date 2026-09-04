namespace leetcode_3903;

class Program
{
    /// <summary>
    /// 3903. Smallest Stable Index I
    /// https://leetcode.com/problems/smallest-stable-index-i/description/
    ///
    /// English:
    /// You are given an integer array nums of length n and an integer k.
    ///
    /// For each index i, define its instability score as max(nums[0..i]) - min(nums[i..n - 1]).
    ///
    /// In other words:
    /// max(nums[0..i]) is the largest value among the elements from index 0 to i.
    /// min(nums[i..n - 1]) is the smallest value among the elements from index i to n - 1.
    ///
    /// An index i is called stable if its instability score is less than or equal to k.
    ///
    /// Return the smallest stable index. If no such index exists, return -1.
    ///
    /// 繁體中文：
    /// 給定一個長度為 n 的整數陣列 nums，以及一個整數 k。
    ///
    /// 對於每個索引 i，定義其不穩定分數為 max(nums[0..i]) - min(nums[i..n - 1])。
    ///
    /// 換句話說：
    /// max(nums[0..i]) 是索引 0 到 i 之間元素的最大值。
    /// min(nums[i..n - 1]) 是索引 i 到 n - 1 之間元素的最小值。
    ///
    /// 如果索引 i 的不穩定分數小於或等於 k，則稱 i 為穩定索引。
    ///
    /// 請回傳最小的穩定索引。如果不存在任何穩定索引，請回傳 -1。
    ///
    /// https://leetcode.cn/problems/smallest-stable-index-i/description/
    /// </summary>
    /// <remarks>
    /// 程式進入點不要求使用者輸入，會執行六組固定案例，並以三種解法交叉比對結果。
    /// 每組案例都會輸出 PASS 或 FAIL，最後列出通過與失敗的案例數量。
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
            ("n = 100 邊界", Enumerable.Range(1, 100).ToArray(), 0, 0)
        };

        Console.WriteLine("=== 3903. Smallest Stable Index I ===");

        int passedCount = 0;
        foreach ((string name, int[] nums, int k, int expected) in testCases)
        {
            passedCount += solver.RunTestCase(name, nums, k, expected);
        }

        int totalCount = testCases.Length;
        Console.WriteLine($"總結：{passedCount}/{totalCount} 通過，{totalCount - passedCount} 個失敗。");
    }

    /// <summary>
    /// 執行一組固定測試案例，分別呼叫三種解法並比較回傳結果。
    /// 輸入是案例名稱、符合題目限制的整數陣列、k 與預期的最小穩定索引；
    /// 若三種解法都得到預期結果，輸出 PASS 並回傳 1，否則輸出 FAIL 並回傳 0。
    /// </summary>
    /// <param name="name">測試案例名稱。</param>
    /// <param name="nums">長度介於 1 到 100、元素介於 0 到 10^9 的整數陣列。</param>
    /// <param name="k">允許的不穩定分數上限，介於 0 到 10^9。</param>
    /// <param name="expected">案例預期的最小穩定索引，若不存在則為 -1。</param>
    /// <returns>案例通過時回傳 1，否則回傳 0。</returns>
    private int RunTestCase(string name, int[] nums, int k, int expected)
    {
        // 三個方法都只讀取 nums，因此可以用同一組輸入交叉驗證結果是否一致。
        int actual1 = FirstStableIndex(nums, k);
        int actual2 = FirstStableIndex2(nums, k);
        int actual3 = FirstStableIndex3(nums, k);
        bool passed = actual1 == expected && actual2 == expected && actual3 == expected;

        Console.WriteLine(
            $"{name}：預期：{expected}，方法一：{actual1}，方法二：{actual2}，方法三：{actual3}，結果：{(passed ? "PASS" : "FAIL")}");

        return passed ? 1 : 0;
    }

    /// <summary>
    /// 以逐一枚舉的方式計算每個索引的不穩定分數。
    /// 從左到右維護前綴最大值，並在每個索引重新掃描右側區間求出後綴最小值。
    /// 輸入必須是非空陣列與不小於 0 的 k；找到第一個分數不超過 k 的索引就回傳，
    /// 若所有索引都不穩定則回傳 -1。時間複雜度為 O(n^2)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">符合題目限制的非空整數陣列。</param>
    /// <param name="k">允許的不穩定分數上限。</param>
    /// <returns>最小的穩定索引；若不存在則回傳 -1。</returns>
    public int FirstStableIndex(int[] nums, int k)
    {
        int maxValue = nums[0];
        int n = nums.Length;
        for (int i = 0; i < n; i++)
        {
            int minValue = nums[i];

            // maxValue 可延續前一個索引的結果；minValue 則從目前索引開始重新計算後綴。
            maxValue = Math.Max(maxValue, nums[i]);

            for (int j = i + 1; j < n; j++)
            {
                minValue = Math.Min(minValue, nums[j]);
            }

            // 依索引遞增順序檢查，因此第一個符合條件的索引就是最小答案。
            if (maxValue - minValue <= k)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 先建立每個索引的前綴最大值與後綴最小值，再一次掃描尋找穩定索引。
    /// 前綴最大值 preMax[i] 代表 nums[0..i] 的最大值，後綴最小值 sufMin[i]
    /// 代表 nums[i..n-1] 的最小值。輸入必須是符合題目限制的非空陣列；
    /// 回傳第一個滿足 preMax[i] - sufMin[i] <= k 的索引，若不存在則回傳 -1。
    /// 時間複雜度為 O(n)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums">符合題目限制的非空整數陣列。</param>
    /// <param name="k">允許的不穩定分數上限。</param>
    /// <returns>最小的穩定索引；若不存在則回傳 -1。</returns>
    public int FirstStableIndex2(int[] nums, int k)
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
    /// 額外空間複雜度為 O(n)，且比 FirstStableIndex2 少配置一個前綴陣列。
    /// </summary>
    /// <param name="nums">符合題目限制的非空整數陣列。</param>
    /// <param name="k">允許的不穩定分數上限。</param>
    /// <returns>最小的穩定索引；若不存在則回傳 -1。</returns>
    public int FirstStableIndex3(int[] nums, int k)
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
