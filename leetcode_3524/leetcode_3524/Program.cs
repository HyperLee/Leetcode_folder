namespace leetcode_3524;

class Program
{
    /// <summary>
    /// 3524. Find X Value of Array I
    /// https://leetcode.com/problems/find-x-value-of-array-i/description/
    /// 3524. 求出陣列的 X 值 I
    /// https://leetcode.cn/problems/find-x-value-of-array-i/description/
    ///
    /// English:
    /// Given an array of positive integers nums, and a positive integer k.
    ///
    /// You are allowed to perform an operation once on nums, where in each operation you can remove any non-overlapping prefix and suffix from nums such that nums remains non-empty.
    ///
    /// You need to find the x-value of nums, which is the number of ways to perform this operation so that the product of the remaining elements leaves a remainder of x when divided by k.
    ///
    /// Return an array result of size k where result[x] is the x-value of nums for 0 <= x <= k - 1.
    ///
    /// A prefix of an array is a subarray that starts from the beginning of the array and extends to any point within it.
    ///
    /// A suffix of an array is a subarray that starts at any point within the array and extends to the end of the array.
    ///
    /// Note that the prefix and suffix to be chosen for the operation can be empty.
    ///
    /// Traditional Chinese（繁體中文）:
    /// 給定一個由正整數組成的陣列 nums，以及一個正整數 k。
    ///
    /// 你可以對 nums 執行一次操作。在每次操作中，你可以從 nums 移除任意不重疊的前綴和後綴，使 nums 維持非空。
    ///
    /// 你需要找出 nums 的 x 值，也就是執行此操作後，剩餘元素的乘積除以 k 的餘數為 x 的操作方式數量。
    ///
    /// 回傳一個大小為 k 的陣列 result，其中 result[x] 是 nums 的 x 值，且 0 <= x <= k - 1。
    ///
    /// 陣列的前綴是從陣列開頭開始，延伸到其中任一位置的子陣列。
    ///
    /// 陣列的後綴是從其中任一位置開始，延伸到陣列結尾的子陣列。
    ///
    /// 請注意，操作中選擇移除的前綴和後綴可以為空。
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>
    /// Main 不要求互動式輸入，會使用固定案例同時驗證正式 DP 解法與小型測資參考解法。
    /// </remarks>
    static void Main(string[] args)
    {
        Program solution = new();
        (int[] Nums, int K, long[] Expected)[] testCases = new[]
        {
            (new[] { 1, 2, 3, 4, 5 }, 3, new long[] { 9, 2, 4 }),
            (new[] { 1, 2, 4, 8, 16, 32 }, 4, new long[] { 18, 1, 2, 0 }),
            (new[] { 1, 1, 2, 1, 1 }, 2, new long[] { 9, 6 }),
            (new[] { 7 }, 5, new long[] { 0, 0, 1, 0, 0 }),
            (new[] { 1, 1, 1 }, 1, new long[] { 6 })
        };

        int passedChecks = 0;
        int totalChecks = testCases.Length * 2;

        for (int i = 0; i < testCases.Length; i++)
        {
            (int[] nums, int k, long[] expected) = testCases[i];
            long[] actual = solution.ResultArray(nums, k);
            long[] reference = solution.BruteForceResultArray(nums, k);
            bool dpPassed = actual.SequenceEqual(expected);
            bool referencePassed = reference.SequenceEqual(expected);

            Console.WriteLine($"Case {i + 1}: nums = [{string.Join(", ", nums)}], k = {k}, Expected = [{string.Join(", ", expected)}]");
            Console.WriteLine($"  ResultArray (DP): Actual = [{string.Join(", ", actual)}], Result = {(dpPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"  BruteForceResultArray: Actual = [{string.Join(", ", reference)}], Result = {(referencePassed ? "PASS" : "FAIL")}");

            if (dpPassed)
            {
                passedChecks++;
            }

            if (referencePassed)
            {
                passedChecks++;
            }
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
        Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
    }

    /// <summary>
    /// 使用以乘積餘數為狀態的滾動動態規劃，計算每個非空連續子陣列的乘積除以 k 的餘數出現次數。
    /// 移除任意不重疊前綴與後綴後的剩餘陣列，恰好對應一個非空連續子陣列；方法會回傳每個餘數的操作方式數量。
    /// </summary>
    /// <param name="nums">由正整數組成的陣列，長度介於 1 到 100000，且每個元素不超過 1000000000。</param>
    /// <param name="k">正整數除數，介於 1 到 5。</param>
    /// <returns>長度為 k 的 long 陣列；索引 r 儲存乘積餘數為 r 的非空連續子陣列數量。</returns>
    public long[] ResultArray(int[] nums, int k)
    {
        int n = nums.Length;
        long[] res = new long[k];

        // dp[r] 表示目前掃描位置的前一格為止，所有「以該位置結尾」且乘積餘數為 r 的子陣列數量。
        // 尚未掃描任何元素時，還沒有非空子陣列，所以所有狀態都是 0。
        long[] dp = new long[k];

        for (int i = 0; i < n; i++)
        {
            long[] ndp = new long[k];

            // 以 nums[i] 自己作為新的起點，形成長度為 1 的非空子陣列。
            ndp[nums[i] % k]++;

            // 將每個以前一格結尾的子陣列接上 nums[i]；只保留餘數即可避免直接計算巨大乘積。
            for (int r = 0; r < k; r++)
            {
                int nextRemainder = (int)(((long)r * nums[i]) % k);
                ndp[nextRemainder] += dp[r];
            }

            dp = ndp;

            // 每個子陣列會在其右端點被完整建立一次，因此把本輪所有狀態加入最終答案。
            for (int r = 0; r < k; r++)
            {
                res[r] += dp[r];
            }
        }

        return res;
    }

    /// <summary>
    /// 以列舉所有非空連續子陣列的方式計算乘積餘數分布，作為小型測資的直觀參考解法。
    /// 此方法保留每次延伸後的乘積餘數，不直接保存完整乘積；它適合驗證 DP，但不適合官方最大輸入範圍。
    /// </summary>
    /// <param name="nums">由正整數組成的陣列。</param>
    /// <param name="k">正整數除數。</param>
    /// <returns>長度為 k 的 long 陣列；索引 r 儲存乘積餘數為 r 的非空連續子陣列數量。</returns>
    public long[] BruteForceResultArray(int[] nums, int k)
    {
        long[] res = new long[k];

        for (int start = 0; start < nums.Length; start++)
        {
            long productRemainder = 1 % k;

            for (int end = start; end < nums.Length; end++)
            {
                productRemainder = (productRemainder * (nums[end] % k)) % k;
                res[(int)productRemainder]++;
            }
        }

        return res;
    }
}