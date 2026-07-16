namespace leetcode_974;

class Program
{
    /// <summary>
    /// 974. Subarray Sums Divisible by K
    ///
    /// English:
    /// Given an integer array nums and an integer k, return the number of non-empty subarrays that have a sum divisible by k.
    /// A subarray is a contiguous part of an array.
    ///
    /// 繁體中文：
    /// 給定一個整數陣列 nums 和一個整數 k，請回傳總和可以被 k 整除的非空子陣列數量。
    /// 子陣列是陣列中一段連續的部分。
    ///
    /// https://leetcode.com/problems/subarray-sums-divisible-by-k/description/
    /// https://leetcode.cn/problems/subarray-sums-divisible-by-k/description/
    /// </summary>
    /// <remarks>
    /// 程式進入點會建立解題物件，並以兩筆固定官方範例分別執行「邊遍歷邊統計」與
    /// 「先統計餘數再套用組合公式」兩種前綴和解法。此程式不讀取互動輸入，
    /// <paramref name="args"/> 不會影響執行結果；主控台會列出每種解法的答案及 PASS/FAIL 驗證結果。
    /// </remarks>
    /// <param name="args">Command-line arguments. This program does not require interactive input.</param>
    static void Main(string[] args)
    {
        Program solver = new Program();

        Console.WriteLine("LeetCode 974 - Subarray Sums Divisible by K");
        Console.WriteLine("================================================");
        Console.WriteLine();

        RunSampleCase(solver, 1, new int[] { 4, 5, 0, -2, -3, 1 }, 5, 7);
        RunSampleCase(solver, 2, new int[] { 5 }, 9, 0);
    }

    /// <summary>
    /// 使用前綴和餘數與即時計數，計算總和可以被 <paramref name="k"/> 整除的非空連續子陣列數量。
    /// <para>
    /// 若兩個前綴和除以 <paramref name="k"/> 的餘數相同，兩者相減所得的子陣列總和便能被
    /// <paramref name="k"/> 整除。因此，走訪陣列時，每取得一個餘數，就把先前相同餘數的
    /// 出現次數加入答案，再記錄目前的前綴和。
    /// </para>
    /// <para>
    /// 餘數計數陣列會先以 <c>counts[0] = 1</c> 記錄空前綴，使從索引 0 開始且總和可整除的
    /// 子陣列也能被計入。由於 C# 對負數取餘數可能得到負值，餘數必須正規化至
    /// <c>0</c> 到 <c>k - 1</c>，才能安全地作為陣列索引。
    /// </para>
    /// <para>
    /// 時間複雜度為 <c>O(n)</c>，空間複雜度為 <c>O(k)</c>。
    /// </para>
    /// <para>
    /// 參考：
    /// <see href="https://leetcode.cn/problems/subarray-sums-divisible-by-k/solution/by-stormsunshine-37mq/">前綴和解法</see>、
    /// <see href="https://leetcode.cn/problems/subarray-sums-divisible-by-k/solutions/263141/you-jian-qian-zhui-he-na-jiu-zai-ci-dai-ni-da-tong/">前綴和教學</see>、
    /// <see href="https://leetcode.cn/problems/subarray-sums-divisible-by-k/solutions/187947/he-ke-bei-k-zheng-chu-de-zi-shu-zu-by-leetcode-sol/">LeetCode 官方題解</see>。
    /// </para>
    /// </summary>
    /// <param name="nums">長度介於 1 到 30000，元素可為正數、零或負數的整數陣列。</param>
    /// <param name="k">介於 2 到 10000，用來判斷子陣列總和是否能整除的正整數。</param>
    /// <returns>總和可以被 <paramref name="k"/> 整除的非空連續子陣列數量。</returns>
    public int SubarraysDivByK(int[] nums, int k)
    {
        int res = 0;
        int[] counts = new int[k];

        // 空前綴的餘數為 0，先記錄一次，才能計入從索引 0 開始且總和可整除的子陣列。
        counts[0] = 1;
        int sum = 0;
        int length = nums.Length;

        for (int i = 0; i < length; i++)
        {
            sum = (sum + nums[i]) % k;
            if (sum < 0)
            {
                // C# 的負數取餘數可能為負值，正規化後才能安全地作為 counts 索引。
                sum += k;
            }

            // 相同餘數的兩個前綴和相減後可被 k 整除；先累加既有次數，再記錄目前前綴。
            int count = counts[sum];
            res += count;
            counts[sum]++;
        }

        return res;
    }

    /// <summary>
    /// 官方解法一：先統計所有前綴和餘數，再使用組合公式計算答案。
    /// 若某個餘數共出現 count 次，任選兩個相同餘數的前綴和即可形成一個總和能被 k 整除的子陣列，
    /// 因此該餘數可提供 count * (count - 1) / 2 組答案。
    ///
    /// 注意：此方法依專案的多解法命名慣例使用 SubarraysDivByK2，
    /// 但它對應的是官方題解中的「解法一」。
    /// </summary>
    /// <param name="nums">長度介於 1 到 30000，元素可為正數、零或負數的整數陣列。</param>
    /// <param name="k">介於 2 到 10000，用來判斷子陣列總和是否能整除的正整數。</param>
    /// <returns>總和可以被 <paramref name="k"/> 整除的非空連續子陣列數量。</returns>
    public int SubarraysDivByK2(int[] nums, int k)
    {
        // 納入餘數為 0 的空前綴，讓從索引 0 開始的合格子陣列也能形成前綴和配對。
        Dictionary<int, int> remainderCounts = new Dictionary<int, int>
        {
            [0] = 1
        };

        int prefixSum = 0;

        foreach (int num in nums)
        {
            prefixSum += num;
            int remainder = prefixSum % k;

            if (remainder < 0)
            {
                // 將負餘數正規化到 0 到 k - 1，確保同餘的前綴和會歸入同一組。
                remainder += k;
            }

            remainderCounts.TryGetValue(remainder, out int count);
            remainderCounts[remainder] = count + 1;
        }

        int result = 0;

        foreach (int count in remainderCounts.Values)
        {
            // 同一餘數組任選兩個前綴和，即可形成一個總和能被 k 整除的子陣列。
            result += count * (count - 1) / 2;
        }

        return result;
    }

    /// <summary>
    /// 執行單筆固定官方範例，將相同輸入交給兩種前綴和解法，並分別與預期答案比對。
    /// 此方法不回傳資料，而是在主控台輸出案例、實際答案及 PASS/FAIL 驗證結果。
    /// </summary>
    /// <param name="solver">提供兩種解法的程式實例。</param>
    /// <param name="caseNumber">官方範例編號。</param>
    /// <param name="nums">要驗證的輸入陣列。</param>
    /// <param name="k">用來判斷整除的正整數。</param>
    /// <param name="expected">此案例的預期答案。</param>
    private static void RunSampleCase(Program solver, int caseNumber, int[] nums, int k, int expected)
    {
        int incrementalResult = solver.SubarraysDivByK(nums, k);
        int combinationResult = solver.SubarraysDivByK2(nums, k);

        Console.WriteLine($"Case {caseNumber}");
        Console.WriteLine($"Input: nums = [{string.Join(", ", nums)}], k = {k}");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"SubarraysDivByK  (Official Method 2 - Incremental): {incrementalResult} {(incrementalResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine($"SubarraysDivByK2 (Official Method 1 - Combination): {combinationResult} {(combinationResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine();
    }
}
