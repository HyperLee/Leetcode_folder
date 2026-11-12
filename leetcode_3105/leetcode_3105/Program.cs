namespace leetcode_3105;

class Program
{
    /// <summary>
    /// 3105. Longest Strictly Increasing or Strictly Decreasing Subarray
    /// https://leetcode.com/problems/longest-strictly-increasing-or-strictly-decreasing-subarray/description/
    /// 3105. 最长的严格递增或递减子数组
    /// https://leetcode.cn/problems/longest-strictly-increasing-or-strictly-decreasing-subarray/
    /// <para>
    /// You are given an array of integers <c>nums</c>. Return the length of the longest
    /// subarray of <c>nums</c> which is either strictly increasing or strictly decreasing.
    /// </para>
    /// <para>
    /// 給定一個整數陣列 <c>nums</c>，請回傳其中最長的嚴格遞增或嚴格遞減連續子陣列的長度。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Environment.ExitCode = RunExamples();
    }

    /// <summary>
    /// 執行內建的合法輸入案例，將兩種解法的結果各自與預期長度比較，
    /// 並輸出每筆案例的結果及通過次數。
    /// </summary>
    /// <returns>所有結果皆符合預期時回傳 0，否則回傳 1。</returns>
    private static int RunExamples()
    {
        (string Name, int[] Nums, int Expected)[] cases =
        [
            ("官方範例：轉折與相等", [1, 4, 3, 3, 2], 2),
            ("官方範例：全部相等", [3, 3, 3, 3], 1),
            ("官方範例：整段遞減", [3, 2, 1], 3),
            ("單一元素", [50], 1),
            ("整段遞增", [1, 2, 3, 4], 4),
            ("相等中斷遞增", [1, 2, 2, 3], 2),
            ("轉折、相等與末尾最長段", [1, 4, 3, 3, 2, 1], 3)
        ];

        Program solution = new();
        int passedChecks = 0;

        foreach ((string name, int[] nums, int expected) in cases)
        {
            int firstResult = solution.LongestMonotonicSubarray(nums);
            int secondResult = solution.LongestMonotonicSubarray2(nums);
            bool firstPassed = firstResult == expected;
            bool secondPassed = secondResult == expected;
            passedChecks += (firstPassed ? 1 : 0) + (secondPassed ? 1 : 0);

            string status = firstPassed && secondPassed ? "通過" : "失敗";
            Console.WriteLine(
                $"[{status}] {name} | 輸入 [{string.Join(", ", nums)}] | 預期 {expected} | 方法一 {firstResult} | 方法二 {secondResult}");
        }

        int totalChecks = cases.Length * 2;
        Console.WriteLine($"共 {cases.Length} 筆案例，{passedChecks}/{totalChecks} 次比對通過。");
        return passedChecks == totalChecks ? 0 : 1;
    }

    /// <summary>
    /// 逐一比較相鄰元素，用兩個計數器分別追蹤以目前元素結尾的嚴格遞增與遞減長度。
    /// 在各段中斷或掃描結束時結算長度，取其中的最大值。
    /// </summary>
    /// <param name="nums">含有 1 到 50 個元素的整數陣列，每個元素介於 1 到 50。</param>
    /// <returns>最長的嚴格遞增或嚴格遞減連續子陣列的元素個數。</returns>
    public int LongestMonotonicSubarray(int[] nums)
    {
        int cnt1 = 1, cnt2 = 1;
        int ans = 1;
        int n = nums.Length;

        for (int i = 1; i < n; i++)
        {
            // 任一方向中斷時，先結算前一段，再從目前元素重新計為 1。
            // 兩個中斷條件都包含相等，因此相等會同時重設兩個計數器。
            if (nums[i] <= nums[i - 1])
            {
                ans = Math.Max(ans, cnt1);
                cnt1 = 1;
            }
            else
            {
                cnt1++;
            }

            if (nums[i] >= nums[i - 1])
            {
                ans = Math.Max(ans, cnt2);
                cnt2 = 1;
            }
            else
            {
                cnt2++;
            }
        }

        // 延伸到陣列末尾的區段可能還沒有結算。
        ans = Math.Max(ans, Math.Max(cnt1, cnt2));
        return ans;
    }

    /// <summary>
    /// 用段首的相鄰配對決定遞增或遞減方向，沿同一方向延伸到無法繼續為止。
    /// 每段結束時記錄長度，並從該段最後一個元素繼續尋找下一段。
    /// </summary>
    /// <param name="nums">含有 1 到 50 個元素的整數陣列，每個元素介於 1 到 50。</param>
    /// <returns>最長的嚴格遞增或嚴格遞減連續子陣列的元素個數。</returns>
    public int LongestMonotonicSubarray2(int[] nums)
    {
        int ans = 1;
        int i = 0;
        int n = nums.Length;
        while (i < n - 1)
        {
            // 相等的配對無法建立嚴格方向，改從右側元素嘗試起段。
            if (nums[i + 1] == nums[i])
            {
                i++;
                continue;
            }

            int j = i;
            bool inc = nums[i + 1] > nums[i];
            i += 2;
            // 先排除相等，才可用「大於是否成立」來區分遞增與遞減。
            while (i < n && nums[i] != nums[i - 1] && (nums[i] > nums[i - 1] == inc))
            {
                i++;
            }
            // i 指向區段右端之外，因此 [j, i) 有 i - j 個元素。
            ans = Math.Max(ans, i - j);
            // 回到區段最後一個元素，讓轉折點也能作為下一段的起點。
            i--;
        }
        return ans;
    }
}
