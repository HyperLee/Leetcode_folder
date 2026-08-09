internal class Program
{
    /// <summary>
    /// 416. Partition Equal Subset Sum
    /// https://leetcode.com/problems/partition-equal-subset-sum/description/
    /// <para>
    /// Given an integer array nums, return true if you can partition the array into two subsets such that the sum of the elements in both subsets is equal, or false otherwise.
    ///
    /// Example 1:
    /// Input: nums = [1,5,11,5]
    /// Output: true
    /// Explanation: The array can be partitioned as [1, 5, 5] and [11].
    ///
    /// Example 2:
    /// Input: nums = [1,2,3,5]
    /// Output: false
    /// Explanation: The array cannot be partitioned into equal-sum subsets.
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 200
    /// - 1 &lt;= nums[i] &lt;= 100
    /// </para>
    /// <para>
    /// 416. 分割等和子集
    /// https://leetcode.cn/problems/partition-equal-subset-sum/description/
    ///
    /// 給定整數陣列 nums，若能將陣列分成兩個子集，使兩個子集中元素總和相等則回傳 true，否則回傳 false。
    ///
    /// 範例 1：
    /// 輸入：nums = [1,5,11,5]
    /// 輸出：true
    /// 解釋：陣列可以分成 [1, 5, 5] 與 [11]。
    ///
    /// 範例 2：
    /// 輸入：nums = [1,2,3,5]
    /// 輸出：false
    /// 解釋：陣列無法分成總和相等的子集。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 200
    /// - 1 &lt;= nums[i] &lt;= 100
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        (string Name, int[] Input, bool Expected)[] testCases =
        {
            ("官方範例一", new[] { 1, 5, 11, 5 }, true),
            ("官方範例二", new[] { 1, 2, 3, 5 }, false),
            ("單一元素", new[] { 1 }, false),
            ("最大元素值", new[] { 100, 100 }, true),
            ("偶數總和但不可分割", new[] { 2, 2, 3, 5 }, false),
            ("多種組合可達目標", new[] { 3, 3, 3, 4, 5 }, true)
        };

        int passedChecks = 0;

        for (int index = 0; index < testCases.Length; index++)
        {
            (string name, int[] input, bool expected) = testCases[index];
            int[] inputForDynamicProgramming = [.. input];
            int[] inputForMemoizedSearch = [.. input];

            bool dynamicProgrammingResult = CanPartition(inputForDynamicProgramming);
            bool memoizedSearchResult = CanPartition2(inputForMemoizedSearch);
            bool dynamicProgrammingPassed =
                dynamicProgrammingResult == expected &&
                inputForDynamicProgramming.SequenceEqual(input);
            bool memoizedSearchPassed =
                memoizedSearchResult == expected &&
                inputForMemoizedSearch.SequenceEqual(input);

            passedChecks += dynamicProgrammingPassed ? 1 : 0;
            passedChecks += memoizedSearchPassed ? 1 : 0;

            Console.WriteLine($"案例 {index + 1}：{name}");
            Console.WriteLine($"  輸入：[{string.Join(", ", input)}]");
            Console.WriteLine($"  預期：{expected}");
            Console.WriteLine(
                $"  解法一（二維動態規劃）：{dynamicProgrammingResult} => " +
                $"{(dynamicProgrammingPassed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"  解法二（記憶化 DFS）：{memoizedSearchResult} => " +
                $"{(memoizedSearchPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int totalChecks = testCases.Length * 2;
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 判斷正整數陣列能否分割成兩個元素總和相等的子集。
    /// 解題概念是先將目標轉換為總和的一半，再以二維 0/1 背包動態規劃判斷
    /// 前 <c>i</c> 個元素能否組成指定總和；每個元素只能選取一次。
    /// 輸入需符合 <c>1 &lt;= nums.Length &lt;= 200</c> 且
    /// <c>1 &lt;= nums[i] &lt;= 100</c>。若可分割則回傳 <see langword="true"/>，
    /// 否則回傳 <see langword="false"/>。時間與額外空間複雜度皆為
    /// O(n × target)，其中 <c>target</c> 是陣列總和的一半。
    /// </summary>
    /// <param name="nums">由正整數組成、要判斷能否等和分割的陣列。</param>
    /// <returns>能分割成兩個等和子集時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    public static bool CanPartition(int[] nums)
    {
        int totalSum = 0;

        foreach (int num in nums)
        {
            totalSum += num;
        }

        // 兩個子集的總和相等時，原陣列總和必須是偶數。
        if (totalSum % 2 != 0)
        {
            return false;
        }

        int target = totalSum / 2;
        int n = nums.Length;
        bool[,] dp = new bool[n + 1, target + 1];

        // 不選取任何元素即可組成總和 0，因此每一列的基底狀態都是 true。
        for (int i = 0; i <= n; i++)
        {
            dp[i, 0] = true;
        }

        for (int i = 1; i <= n; i++)
        {
            int currentNumber = nums[i - 1];

            for (int currentSum = 1; currentSum <= target; currentSum++)
            {
                // 先沿用「不選目前元素」的結果；放得下時再合併「選取」分支。
                dp[i, currentSum] = dp[i - 1, currentSum];

                if (currentNumber <= currentSum)
                {
                    dp[i, currentSum] =
                        dp[i, currentSum] ||
                        dp[i - 1, currentSum - currentNumber];
                }
            }
        }

        return dp[n, target];
    }

    /// <summary>
    /// 判斷正整數陣列能否分割成兩個元素總和相等的子集。
    /// 解題概念是以記憶化深度優先搜尋處理每個元素「選取或略過」的決策，
    /// 並以索引及剩餘目標總和作為快取狀態，避免重複搜尋相同子問題。
    /// 輸入需符合 <c>1 &lt;= nums.Length &lt;= 200</c> 且
    /// <c>1 &lt;= nums[i] &lt;= 100</c>。若可分割則回傳 <see langword="true"/>，
    /// 否則回傳 <see langword="false"/>。時間與記憶化表空間複雜度皆為
    /// O(n × target)，遞迴堆疊空間為 O(n)。
    /// </summary>
    /// <param name="nums">由正整數組成、要判斷能否等和分割的陣列。</param>
    /// <returns>能分割成兩個等和子集時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    public static bool CanPartition2(int[] nums)
    {
        int totalSum = 0;

        foreach (int num in nums)
        {
            totalSum += num;
        }

        if (totalSum % 2 != 0)
        {
            return false;
        }

        int target = totalSum / 2;
        bool?[,] memo = new bool?[nums.Length, target + 1];

        return CanReachTarget(nums, 0, target, memo);
    }

    /// <summary>
    /// 從指定索引開始搜尋能否選出總和等於剩餘目標的子集。
    /// 每個狀態可選取或略過目前元素，結果以索引及剩餘目標快取；
    /// 剩餘目標為 0 時表示成功，用完元素仍未達標時表示失敗。
    /// </summary>
    /// <param name="nums">由正整數組成且不會在搜尋過程中被修改的輸入陣列。</param>
    /// <param name="index">目前要決定是否選取的元素索引。</param>
    /// <param name="remaining">仍需組成的非負目標總和。</param>
    /// <param name="memo">以元素索引及剩餘目標為座標的記憶化結果表。</param>
    /// <returns>從目前狀態可組成剩餘目標時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    private static bool CanReachTarget(int[] nums, int index, int remaining, bool?[,] memo)
    {
        if (remaining == 0)
        {
            return true;
        }

        if (index == nums.Length)
        {
            return false;
        }

        if (memo[index, remaining].HasValue)
        {
            return memo[index, remaining]!.Value;
        }

        // 只有目前元素不超過剩餘目標時才進入選取分支，避免產生負數狀態。
        bool canInclude =
            nums[index] <= remaining &&
            CanReachTarget(nums, index + 1, remaining - nums[index], memo);
        bool result =
            canInclude ||
            CanReachTarget(nums, index + 1, remaining, memo);

        memo[index, remaining] = result;
        return result;
    }
}