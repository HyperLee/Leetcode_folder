namespace leetcode_198;

class Program
{
    private static readonly (int[] Houses, int Expected)[] s_sampleCases =
    [
        (new[] { 1, 2, 3, 1 }, 4),
        (new[] { 2, 7, 9, 3, 1 }, 12),
        (new[] { 2, 1, 1, 2 }, 4),
        (new[] { 5 }, 5),
        (new[] { 2, 1 }, 2),
        (new[] { 0 }, 0)
    ];

    /// <summary>
    /// 198. House Robber
    /// https://leetcode.com/problems/house-robber/description/
    /// 198. 打家劫舍
    /// https://leetcode.cn/problems/house-robber/description/
    ///
    /// You are a professional robber planning to rob houses along a street. Each house has a certain amount of money
    /// stashed, the only constraint stopping you from robbing each of them is that adjacent houses have security systems
    /// connected and it will automatically contact the police if two adjacent houses were broken into on the same night.
    ///
    /// Given an integer array nums representing the amount of money of each house, return the maximum amount of money
    /// you can rob tonight without alerting the police.
    ///
    /// 你是一名專業的小偷，正計劃沿著一條街偷竊每一間房子。每間房子都藏有一定數量的現金，
    /// 唯一限制你不能每間都偷的是：相鄰的房屋裝有彼此連動的防盜系統，
    /// 如果同一晚有兩間相鄰的房屋被闖入，系統就會自動通知警方。
    ///
    /// 給定一個整數陣列 nums，表示每間房子存放的金額，請回傳你今晚在不驚動警方的情況下，
    /// 最多可以偷到的金額。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        bool allPassed = true;

        Console.WriteLine("LeetCode 198 - House Robber Sample Verification");

        for (int i = 0; i < s_sampleCases.Length; i++)
        {
            bool casePassed = RunSampleCase(solver, i + 1, s_sampleCases[i].Houses, s_sampleCases[i].Expected);
            allPassed &= casePassed;
        }

        Console.WriteLine($"Overall: {(allPassed ? "PASS" : "FAIL")}");
    }

    /// <summary>
    /// 使用動態規劃陣列計算每一間房屋結束時的最佳收益。
    /// 解題概念是比較「略過目前房屋，沿用前一間的最佳值」與「偷目前房屋，加上前兩間的最佳值」。
    /// 輸入 <paramref name="nums"/> 應為非 null 且至少包含一間房屋；回傳值是今晚在不觸發警報下可偷到的最大金額。
    /// </summary>
    /// <param name="nums">每間房屋可偷得的金額，且題目保證至少有一間房屋。</param>
    /// <returns>從左到右規劃後，可以取得的最大總金額。</returns>
    public int Rob(int[] nums)
    {
        int n = nums.Length;

        // 只有一間房屋時，唯一選擇就是偷這一間。
        if (n == 1)
        {
            return nums[0];
        }

        int[] dp = new int[n];
        dp[0] = nums[0];
        dp[1] = Math.Max(nums[0], nums[1]);

        for (int i = 2; i < n; i++)
        {
            // 每一步都在比較：跳過當前房屋，或偷當前房屋並接上 i - 2 的最佳結果。
            dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]);
        }

        return dp[n - 1];
    }

    /// <summary>
    /// 使用滾動變數保留前兩個狀態，將動態規劃的空間從 O(n) 降到 O(1)。
    /// 解題概念與 <see cref="Rob(int[])"/> 相同，仍然是在「偷當前房屋」與「略過當前房屋」之間取最大值。
    /// 輸入 <paramref name="nums"/> 應為非 null 且至少包含一間房屋；回傳值是今晚在不觸發警報下可偷到的最大金額。
    /// </summary>
    /// <param name="nums">每間房屋可偷得的金額，且題目保證至少有一間房屋。</param>
    /// <returns>以常數額外空間計算出的最大可偷金額。</returns>
    public int Rob2(int[] nums)
    {
        int n = nums.Length;

        // 長度為 1 或 2 時可以直接回傳，避免進入後續轉移。
        if (n == 1)
        {
            return nums[0];
        }

        if (n == 2)
        {
            return Math.Max(nums[0], nums[1]);
        }

        // prev 代表 i - 2 的最佳結果，curr 代表 i - 1 的最佳結果。
        int prev = nums[0];
        int curr = Math.Max(nums[0], nums[1]);

        for (int i = 2; i < n; i++)
        {
            // next 同樣是在比較「偷當前」與「略過當前」兩種選擇。
            int next = Math.Max(curr, prev + nums[i]);
            prev = curr;
            curr = next;
        }

        return curr;
    }

    /// <summary>
    /// 執行單筆範例，並同時驗證兩種解法是否都得到預期答案。
    /// </summary>
    /// <param name="solver">目前題目的解題物件。</param>
    /// <param name="caseNumber">顯示用的案例編號。</param>
    /// <param name="houses">本次案例的房屋金額陣列。</param>
    /// <param name="expected">預期的最大可偷金額。</param>
    /// <returns>兩種解法都符合預期時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    private static bool RunSampleCase(Program solver, int caseNumber, int[] houses, int expected)
    {
        int robResult = solver.Rob(houses);
        int rob2Result = solver.Rob2(houses);
        bool robPassed = robResult == expected;
        bool rob2Passed = rob2Result == expected;

        Console.WriteLine($"Case {caseNumber}: nums = {FormatArray(houses)}, expected = {expected}");
        Console.WriteLine($"  Rob  -> actual = {robResult}, {(robPassed ? "PASS" : "FAIL")}");
        Console.WriteLine($"  Rob2 -> actual = {rob2Result}, {(rob2Passed ? "PASS" : "FAIL")}");

        return robPassed && rob2Passed;
    }

    private static string FormatArray(int[] nums)
    {
        return $"[{string.Join(", ", nums)}]";
    }
}
