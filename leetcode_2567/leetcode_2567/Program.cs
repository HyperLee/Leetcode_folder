namespace leetcode_2567;

class Program
{
    /// <summary>
    /// 2567. Minimum Score by Changing Two Elements
    /// https://leetcode.com/problems/minimum-score-by-changing-two-elements/description/
    /// 2567. 修改两个元素的最小分数
    /// https://leetcode.cn/problems/minimum-score-by-changing-two-elements/description/
    ///
    /// English:
    /// You are given an integer array nums.
    ///
    /// - The low score of nums is the minimum absolute difference between any two integers.
    /// - The high score of nums is the maximum absolute difference between any two integers.
    /// - The score of nums is the sum of the high and low scores.
    ///
    /// Return the minimum score after changing two elements of nums.
    ///
    /// 繁體中文：
    /// 給定一個整數陣列 nums。
    ///
    /// - nums 的低分是任意兩個整數之間絕對差的最小值。
    /// - nums 的高分是任意兩個整數之間絕對差的最大值。
    /// - nums 的分數是高分與低分的總和。
    ///
    /// 請回傳修改 nums 中兩個元素後所能得到的最低分數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 直接執行所有案例；每種解法使用獨立副本，避免排序改變另一種解法的輸入。
        (string Name, int[] Nums, int Expected)[] testCases =
        [
            ("OfficialExample1", [1, 4, 7, 8, 5], 3),
            ("ThreeElements_ReturnsZero", [1, 4, 3], 0),
            ("AllEqual_ReturnsZero", [5, 5, 5, 5], 0),
            ("Duplicates_ReturnsZero", [1, 1, 1, 9, 9], 0),
            ("SortedInput", [1, 2, 3, 4, 5], 2),
            ("ReversedInput", [5, 4, 3, 2, 1], 2),
            ("ChangeTwoSmallest", [1, 2, 100, 101, 102], 2),
            ("ChangeTwoLargest", [1, 2, 3, 100, 200], 2),
            ("ChangeBothEnds", [1, 50, 51, 52, 100], 2),
            ("BoundaryValues_ReturnsZero", [1, 1, 1_000_000_000, 1_000_000_000], 0),
            ("MaximumLength", Enumerable.Range(0, 100_000)
                .Select(index => index % 2 == 0 ? 1 : 1_000_000_000).ToArray(), 999_999_999)
        ];

        Program solution = new Program();
        int passed = 0;
        foreach ((string name, int[] nums, int expected) in testCases)
        {
            int sortedResult = solution.MinimizeSum((int[])nums.Clone());
            int linearResult = solution.MinimizeSum2((int[])nums.Clone());
            bool success = sortedResult == expected && linearResult == expected;
            string input = nums.Length <= 10 ? $"[{string.Join(",", nums)}]" : $"Length={nums.Length}";
            Console.WriteLine($"{name}: {input}, Expected={expected}, "
                + $"MinimizeSum={sortedResult}, MinimizeSum2={linearResult}, {(success ? "PASS" : "FAIL")}");
            if (success)
            {
                passed++;
            }
        }

        Console.WriteLine($"Passed {passed}/{testCases.Length} cases ({testCases.Length * 2} solution checks).");
        Environment.ExitCode = passed == testCases.Length ? 0 : 1;
    }

    /// <summary>
    /// 使用排序與貪心，求修改兩個元素後的最小總分。
    /// 將被修改的值移入保留區間並製造重複值，可使低分為零；
    /// 排序後比較修改最小兩個、兩端各一個、最大兩個的剩餘範圍。
    /// 輸入須符合題目限制：長度 3 至 100,000，元素值 1 至 1,000,000,000。
    /// 本方法會將輸入陣列原地排序。
    /// </summary>
    /// <param name="nums">符合題目限制的整數陣列，可包含重複值。</param>
    /// <returns>修改兩個元素後，低分與高分總和的最小值。</returns>
    public int MinimizeSum(int[] nums)
    {
        int n = nums.Length;
        Array.Sort(nums);
        // 修改值可放入保留區間，因此只需比較三種移除端點後的最大值減最小值。
        int changeTwoSmallest = nums[n - 1] - nums[2];
        int changeBothEnds = nums[n - 2] - nums[1];
        int changeTwoLargest = nums[n - 3] - nums[0];
        return Math.Min(changeTwoSmallest, Math.Min(changeBothEnds, changeTwoLargest));
    }

    /// <summary>
    /// 使用單次遍歷維護最小三個與最大三個值，求修改兩個元素後的最小總分。
    /// 這六個極值足以比較三種端點修改策略，不必排序或儲存其他元素。
    /// 輸入須符合題目限制：長度 3 至 100,000，元素值 1 至 1,000,000,000。
    /// 本方法不修改輸入，時間複雜度 O(n)，額外空間 O(1)。
    /// </summary>
    /// <param name="nums">符合題目限制的整數陣列，可包含重複值。</param>
    /// <returns>修改兩個元素後，低分與高分總和的最小值。</returns>
    public int MinimizeSum2(int[] nums)
    {
        int min1 = int.MaxValue, min2 = int.MaxValue, min3 = int.MaxValue;
        int max1 = int.MinValue, max2 = int.MinValue, max3 = int.MinValue;

        foreach (int x in nums)
        {
            // 由第三小往前搬移，避免覆蓋仍需使用的舊值。
            // 相同數值仍可進入下一個名次；此處記錄的是三個元素，而非三個不同值。
            if (x < min1)
            {
                min3 = min2;
                min2 = min1;
                min1 = x;
            }
            else if (x < min2)
            {
                min3 = min2;
                min2 = x;
            }
            else if (x < min3)
            {
                min3 = x;
            }

            // 最大值獨立更新；同一個元素可能同時屬於目前的最小三個與最大三個。
            if (x > max1)
            {
                max3 = max2;
                max2 = max1;
                max1 = x;
            }
            else if (x > max2)
            {
                max3 = max2;
                max2 = x;
            }
            else if (x > max3)
            {
                max3 = x;
            }
        }
        // 對應修改最小兩個、兩端各一個、最大兩個；低分皆可達零。
        return Math.Min(Math.Min(max1 - min3, max2 - min2), max3 - min1);
    }
}