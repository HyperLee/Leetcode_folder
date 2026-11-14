namespace leetcode_3364;

class Program
{
    /// <summary>
    /// 3364. Minimum Positive Sum Subarray
    /// https://leetcode.com/problems/minimum-positive-sum-subarray/description/
    /// 3364. 最小正和子数组
    /// https://leetcode.cn/problems/minimum-positive-sum-subarray/description/
    ///
    /// You are given an integer array nums and two integers l and r.
    /// Your task is to find the minimum sum of a subarray whose size is between l and r (inclusive) and whose sum is greater than 0.
    /// Return the minimum sum of such a subarray. If no such subarray exists, return -1.
    /// A subarray is a contiguous non-empty sequence of elements within an array.
    ///
    /// 給定一個整數陣列 nums 和兩個整數 l、r。請找出長度介於 l 和 r 之間（含兩端），且元素總和大於 0 的子陣列，並求出其中最小的總和。
    /// 回傳符合條件的子陣列中的最小總和。如果沒有符合條件的子陣列，回傳 -1。
    /// 子陣列是陣列中一段連續且非空的元素序列。
    ///
    /// </summary>
    /// <remarks>
    /// 使用固定測試資料驗證三種解法，輸出個別結果與通過總數。任一比較失敗時，程序結束碼為 1。
    /// </remarks>
    /// <param name="args">命令列引數，測試流程無須使用。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (string Label, int[] Nums, int L, int R, int Expected)[] testCases =
        [
            ("範例一：最小正和在長度下界", [3, -2, 1, 4], 2, 3, 1),
            ("範例二：沒有正和子陣列", [-2, 2, -3, 1], 2, 3, -1),
            ("範例三：全為正數", [1, 2, 3, 4], 2, 4, 3),
            ("固定長度 l == r", [2, 2, -1], 2, 2, 1),
            ("只有長度上界的子陣列有正和", [2, -2, 1], 2, 3, 1),
            ("單元素為零", [0], 1, 1, -1),
            ("單元素達題目上限", [1000], 1, 1, 1000),
            ("最小值多次更新", [4, -1, -2, 4], 2, 3, 1),
        ];

        (string Name, Func<IList<int>, int, int, int> Solve)[] methods =
        [
            (nameof(MinimumSumSubarray), solution.MinimumSumSubarray),
            (nameof(MinimumSumSubarray2), solution.MinimumSumSubarray2),
            (nameof(MinimumSumSubarray3), solution.MinimumSumSubarray3),
        ];

        int passedCount = 0;
        foreach ((string label, int[] nums, int l, int r, int expected) in testCases)
        {
            foreach ((string name, Func<IList<int>, int, int, int> solve) in methods)
            {
                int actual = solve(nums, l, r);
                bool passed = actual == expected;
                Console.WriteLine($"{label}，{name}，預期 {expected}，實際 {actual}，{(passed ? "PASS" : "FAIL")}");
                if (passed)
                {
                    passedCount++;
                }
            }
        }

        int totalCount = testCases.Length * methods.Length;
        Console.WriteLine($"通過 {passedCount}/{totalCount} 次比較。");
        Environment.ExitCode = passedCount == totalCount ? 0 : 1;
    }

    /// <summary>
    /// 逐一固定視窗長度，滑動視窗並增減兩端元素，找出合法子陣列的最小正和。
    /// </summary>
    /// <param name="nums">含 1 到 100 個整數，每個元素介於 -1000 與 1000。</param>
    /// <param name="l">子陣列長度下界，至少為 1。</param>
    /// <param name="r">子陣列長度上界，至少為 <paramref name="l"/>，且不超過元素總數。</param>
    /// <returns>長度介於 <paramref name="l"/> 與 <paramref name="r"/> 的子陣列中最小的正和，無解時回傳 -1。</returns>
    public int MinimumSumSubarray(IList<int> nums, int l, int r)
    {
        int n = nums.Count;
        int minSum = int.MaxValue;

        // 先累計長度 l - 1 的前綴，每輪再補一個元素形成新的首個視窗。
        int initial = 0;
        for (int i = 0; i < l - 1; i++)
        {
            initial += nums[i];
        }

        for (int length = l; length <= r; length++)
        {
            initial += nums[length - 1];
            int sum = initial;

            if (sum > 0 && sum < minSum)
            {
                minSum = sum;
            }

            for (int hi = length; hi < n; hi++)
            {
                // 加上右側移入的元素並減去左側移出的元素，維持視窗長度。
                sum += nums[hi] - nums[hi - length];
                if (sum > 0 && sum < minSum)
                {
                    minSum = sum;
                }
            }
        }
        return minSum == int.MaxValue ? -1 : minSum;
    }

    /// <summary>
    /// 預先建立前綴和，以兩個端點的差求出每個合法子陣列的總和，並找出最小正和。
    /// </summary>
    /// <param name="nums">含 1 到 100 個整數，每個元素介於 -1000 與 1000。</param>
    /// <param name="l">子陣列長度下界，至少為 1。</param>
    /// <param name="r">子陣列長度上界，至少為 <paramref name="l"/>，且不超過元素總數。</param>
    /// <returns>長度介於 <paramref name="l"/> 與 <paramref name="r"/> 的子陣列中最小的正和，無解時回傳 -1。</returns>
    public int MinimumSumSubarray2(IList<int> nums, int l, int r)
    {
        int n = nums.Count;
        int[] prefixSum = new int[n + 1];

        for (int i = 0; i < n; i++)
        {
            prefixSum[i + 1] = prefixSum[i] + nums[i];
        }

        int minSum = int.MaxValue;
        for (int length = l; length <= r; length++)
        {
            for (int start = 0; start + length <= n; start++)
            {
                // 扣掉起點前的前綴和，留下半開區間 [start, start + length) 的總和。
                int sum = prefixSum[start + length] - prefixSum[start];
                if (sum > 0 && sum < minSum)
                {
                    minSum = sum;
                }
            }
        }
        return minSum == int.MaxValue ? -1 : minSum;
    }

    /// <summary>
    /// 枚舉可容納合法子陣列的起點，逐步累加右端元素，找出長度符合限制的最小正和。
    /// </summary>
    /// <remarks>
    /// 時間複雜度為 O(n × r)，額外空間複雜度為 O(1)。
    /// </remarks>
    /// <param name="nums">含 1 到 100 個整數，每個元素介於 -1000 與 1000。</param>
    /// <param name="l">子陣列長度下界，至少為 1。</param>
    /// <param name="r">子陣列長度上界，至少為 <paramref name="l"/>，且不超過元素總數。</param>
    /// <returns>長度介於 <paramref name="l"/> 與 <paramref name="r"/> 的子陣列中最小的正和，無解時回傳 -1。</returns>
    public int MinimumSumSubarray3(IList<int> nums, int l, int r)
    {
        int n = nums.Count;
        int minSum = int.MaxValue;

        for (int start = 0; start <= n - l; start++)
        {
            int sum = 0;
            int maxLength = Math.Min(r, n - start);
            for (int length = 1; length <= maxLength; length++)
            {
                sum += nums[start + length - 1];
                // 短於 l 的總和會留給下一輪延伸，達到 l 後才參與比較。
                if (length >= l && sum > 0 && sum < minSum)
                {
                    minSum = sum;
                }
            }
        }

        return minSum == int.MaxValue ? -1 : minSum;
    }
}