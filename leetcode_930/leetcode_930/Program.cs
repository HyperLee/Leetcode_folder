namespace leetcode_930;

class Program
{
    /// <summary>
    /// 930. Binary Subarrays With Sum
    /// https://leetcode.com/problems/binary-subarrays-with-sum/description/
    /// 930. 和相同的二元子陣列
    /// https://leetcode.cn/problems/binary-subarrays-with-sum/description/
    /// Given a binary array nums and an integer goal, return the number of non-empty subarrays with a sum goal.
    ///
    /// A subarray is a contiguous part of the array.
    ///
    /// 給定一個二元陣列 nums 和一個整數 goal，請回傳總和等於 goal 的非空子陣列數量。
    ///
    /// 子陣列是陣列中一段連續的部分。
    /// </summary>
    /// <remarks>
    /// 執行四組固定案例，分別驗證前綴和搭配雜湊表與雙左界滑動視窗兩種解法。
    /// 若任一解法的結果與預期不同，程式會輸出 FAIL 並將結束碼設為 1。
    /// </remarks>
    /// <param name="args">命令列參數；此範例程式不使用。</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        bool allPassed = true;

        Console.WriteLine("LeetCode 930 - Binary Subarrays With Sum");
        Console.WriteLine();

        allPassed &= RunSampleCase(solver, 1, new[] { 1, 0, 1, 0, 1 }, 2, 4);
        allPassed &= RunSampleCase(solver, 2, new[] { 0, 0, 0, 0, 0 }, 0, 15);
        allPassed &= RunSampleCase(solver, 3, new[] { 1 }, 0, 0);
        allPassed &= RunSampleCase(solver, 4, new[] { 0, 1, 0, 1, 0 }, 1, 8);

        Console.WriteLine(allPassed ? "Overall: PASS" : "Overall: FAIL");
        Environment.ExitCode = allPassed ? 0 : 1;
    }

    /// <summary>
    /// 執行一組測試資料，將相同輸入交給兩種解法，並比較結果是否都符合預期值。
    /// 輸入必須符合題目限制：<paramref name="nums"/> 是非空二元陣列，
    /// <paramref name="goal"/> 介於 0 與陣列長度之間。
    /// </summary>
    /// <param name="solver">提供兩種演算法方法的解題物件。</param>
    /// <param name="caseNumber">顯示在主控台上的案例編號。</param>
    /// <param name="nums">要計算的非空二元陣列。</param>
    /// <param name="goal">子陣列必須達到的目標總和。</param>
    /// <param name="expected">兩種解法都應回傳的預期數量。</param>
    /// <returns>兩種解法都符合預期值時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    private static bool RunSampleCase(Program solver, int caseNumber, int[] nums, int goal, int expected)
    {
        int prefixSumResult = solver.NumSubarraysWithSum(nums, goal);
        int slidingWindowResult = solver.NumSubarraysWithSum2(nums, goal);
        bool prefixSumPassed = prefixSumResult == expected;
        bool slidingWindowPassed = slidingWindowResult == expected;

        Console.WriteLine($"Case {caseNumber}: nums = [{string.Join(", ", nums)}], goal = {goal}, expected = {expected}");
        Console.WriteLine($"  NumSubarraysWithSum  (Prefix Sum + Hash Map): {prefixSumResult} [{(prefixSumPassed ? "PASS" : "FAIL")}]");
        Console.WriteLine($"  NumSubarraysWithSum2 (Sliding Window)        : {slidingWindowResult} [{(slidingWindowPassed ? "PASS" : "FAIL")}]");
        Console.WriteLine();

        return prefixSumPassed && slidingWindowPassed;
    }

    /// <summary>
    /// 使用「前綴和＋雜湊表」計算總和等於目標值的非空連續子陣列數量。
    /// 若目前前綴和為 <c>sum</c>，先前出現過的前綴和為 <c>sum - goal</c>，
    /// 兩者之間的連續區間總和便等於 <paramref name="goal"/>。
    /// 輸入必須是非空二元陣列，且目標值介於 0 與陣列長度之間。
    /// </summary>
    /// <param name="nums">只包含 0 與 1 的非空陣列。</param>
    /// <param name="goal">子陣列必須達到的目標總和。</param>
    /// <returns>總和等於 <paramref name="goal"/> 的非空連續子陣列數量。</returns>
    public int NumSubarraysWithSum(int[] nums, int goal)
    {
        int sum = 0;
        int res = 0;
        Dictionary<int, int> cnt = new Dictionary<int, int>();

        foreach (int num in nums)
        {
            // 在加入目前元素前記錄前綴和，確保只配對到目前位置之前的切點。
            if (cnt.ContainsKey(sum))
            {
                cnt[sum]++;
            }
            else
            {
                cnt.Add(sum, 1);
            }

            sum += num;

            // P[j] - P[i] = goal，因此每個 P[i] = sum - goal 都形成一個有效子陣列。
            int val = 0;
            cnt.TryGetValue(sum - goal, out val);
            res += val;
        }
        return res;
    }

    /// <summary>
    /// 使用「雙左界滑動視窗」計算總和等於目標值的非空連續子陣列數量。
    /// 對每個右邊界，同時維護總和不大於 <paramref name="goal"/> 的左界 <c>left1</c>，
    /// 以及總和小於 <paramref name="goal"/> 的左界 <c>left2</c>；兩個左界之差就是
    /// 以目前位置結尾且總和恰好等於目標值的子陣列數量。
    /// 此方法利用元素只有 0 與 1 的非負特性，輸入目標值必須介於 0 與陣列長度之間。
    /// </summary>
    /// <param name="nums">只包含 0 與 1 的非空陣列。</param>
    /// <param name="goal">子陣列必須達到的目標總和。</param>
    /// <returns>總和等於 <paramref name="goal"/> 的非空連續子陣列數量。</returns>
    public int NumSubarraysWithSum2(int[] nums, int goal)
    {
        int n = nums.Length;
        int left1 = 0;
        int left2 = 0;
        int right = 0;
        int sum1 = 0;
        int sum2 = 0;
        int res = 0;

        while (right < n)
        {
            sum1 += nums[right];
            // left1 停在視窗總和 <= goal 的第一個位置。
            while (left1 <= right && sum1 > goal)
            {
                sum1 -= nums[left1];
                left1++;
            }
            sum2 += nums[right];
            // left2 再往右縮到視窗總和 < goal，因此 [left1, left2) 都是有效起點。
            while (left2 <= right && sum2 >= goal)
            {
                sum2 -= nums[left2];
                left2++;
            }

            // 固定 right 時，left2 - left1 即為新增的有效子陣列數量。
            res += left2 - left1;
            right++;
        }
        return res;
    }
}
