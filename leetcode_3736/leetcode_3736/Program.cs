namespace leetcode_3736;

class Program
{
    /// <summary>
    /// 3736. Minimum Moves to Equal Array Elements III
    /// https://leetcode.com/problems/minimum-moves-to-equal-array-elements-iii/description/
    /// 3736. 最小操作次数使数组元素相等 III
    /// https://leetcode.cn/problems/minimum-moves-to-equal-array-elements-iii/description/
    /// English
    /// You are given an integer array nums.
    /// 
    /// In one move, you may increase the value of any single element nums[i] by 1.
    /// 
    /// Return the minimum total number of moves required so that all elements in nums become equal.
    /// 
    /// 繁體中文
    /// 給定一個整數陣列 nums。
    /// 
    /// 每次操作中，你可以將任意一個元素 nums[i] 的值增加 1。
    /// 
    /// 請回傳使 nums 中所有元素都相等所需的最少總操作次數。
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>
    /// 無須輸入即可執行七筆合法範圍內的固定測試，逐案驗證兩個解法。
    /// 輸出輸入資料、預期值、實際值及通過狀態；任一驗證失敗時設定非零結束碼。
    /// </remarks>
    static void Main(string[] args)
    {
        (string Name, int[] Nums, int Expected)[] testCases =
        [
            ("官方範例一", [2, 1, 3], 3),
            ("官方範例二、重複值", [4, 4, 5], 2),
            ("最小輸入", [1], 0),
            ("全部相等", [7, 7, 7], 0),
            ("最大值位於開頭", [100, 1, 1], 198),
            ("最大值重複", [100, 1, 100], 99),
            ("長度與差距上界", Enumerable.Repeat(1, 99).Append(100).ToArray(), 9801)
        ];

        Program solution = new Program();
        int passedCases = 0;

        foreach (var testCase in testCases)
        {
            int result1 = solution.MinMoves(testCase.Nums);
            int result2 = solution.MinMoves2(testCase.Nums);
            bool passed = result1 == testCase.Expected && result2 == testCase.Expected;
            if (passed)
            {
                passedCases++;
            }

            Console.WriteLine($"案例：{testCase.Name}");
            Console.WriteLine($"輸入：[{string.Join(", ", testCase.Nums)}]");
            Console.WriteLine($"預期：{testCase.Expected}；MinMoves：{result1}；MinMoves2：{result2}；{(passed ? "通過" : "失敗")}");
            Console.WriteLine();
        }

        Console.WriteLine($"測試結果：{passedCases}/{testCases.Length} 筆通過（每筆驗證兩個解法）。");
        Environment.ExitCode = passedCases == testCases.Length ? 0 : 1;
    }

    /// <summary>
    /// 使用 LINQ 取得最大值，再以貪心法計算使所有元素相等的最少操作次數。
    /// 輸入為長度及元素值皆介於 1 到 100 的整數陣列；每次只能增加單一元素 1。
    /// 以原始最大值為共同目標，回傳所有元素與目標的差距總和，不修改輸入陣列。
    /// </summary>
    /// <param name="nums">非 null、長度為 1 到 100，且元素值為 1 到 100 的陣列。</param>
    /// <returns>使所有元素相等所需的最少操作次數，範圍為 0 到 9801。</returns>
    public int MinMoves(int[] nums)
    {
        // 只能增加，目標至少是原始最大值；更大的目標會讓每個元素多做操作。
        int max = nums.Max();
        int moves = 0;
        // 每增加 1 需要一次操作，因此直接累加差距，無須逐次模擬。
        foreach (var num in nums)
        {
            moves += max - num;
        }
        return moves;
    }

    /// <summary>
    /// 使用兩次手動掃描的貪心法，計算使所有元素相等的最少操作次數。
    /// 輸入為長度及元素值皆介於 1 到 100 的整數陣列；每次只能增加單一元素 1。
    /// 第一輪找出最大值，第二輪累加與最大值的差距，回傳最少操作次數且不修改輸入。
    /// </summary>
    /// <param name="nums">非 null、長度為 1 到 100，且元素值為 1 到 100 的陣列。</param>
    /// <returns>使所有元素相等所需的最少操作次數，範圍為 0 到 9801。</returns>
    public int MinMoves2(int[] nums)
    {
        int max = nums[0];
        int moves = 0;

        // 非空陣列可用首元素初始化；第一輪結束後才確定共同目標。
        foreach (int x in nums)
        {
            max = Math.Max(max, x);
        }

        // 目標確定後，第二輪累加每個元素需要增加的次數。
        foreach (int x in nums)
        {
            moves += max - x;
        }
        return moves;
    }
}