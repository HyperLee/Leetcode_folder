namespace leetcode_3041;

class Program
{
    /// <summary>
    /// 3041. Maximize Consecutive Elements in an Array After Modification
    /// https://leetcode.com/problems/maximize-consecutive-elements-in-an-array-after-modification/description/
    /// 3041. 修改陣列後最大化陣列中的連續元素數目
    /// https://leetcode.cn/problems/maximize-consecutive-elements-in-an-array-after-modification/description/
    /// English:
    /// You are given a 0-indexed array nums consisting of positive integers.
    /// Initially, you can increase the value of any element in the array by at most 1.
    /// After that, you need to select one or more elements from the final array such that those elements are
    /// consecutive when sorted in increasing order. For example, the elements [3, 4, 5] are consecutive while
    /// [3, 4, 6] and [1, 1, 2, 3] are not.
    /// Return the maximum number of elements that you can select.
    /// 繁體中文：
    /// 給定一個索引從 0 開始、由正整數組成的陣列 nums。
    /// 一開始，你可以將陣列中任意元素的值最多增加 1，也就是每個元素都可以選擇維持原值或增加 1。
    /// 接著，你需要從最終陣列中選取一個或多個元素，使這些元素由小到大排序後形成連續整數。
    /// 例如，[3, 4, 5] 是連續的，而 [3, 4, 6] 和 [1, 1, 2, 3] 則不是；選取的元素不可有重複值。
    /// 回傳你可以選取的最大元素數量。
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>執行固定案例、窮舉比對與上限案例；失敗時拋出例外並以非零狀態結束。args 不使用。</remarks>
    static void Main(string[] args)
    {
        RunTests();
    }

    /// <summary>
    /// 執行兩種解法的確定性驗證：固定輸入與預期值、小範圍暴力比對及題目長度上限。
    /// 無外部輸入；全部通過時輸出摘要，任一結果不符時拋出例外。
    /// </summary>
    private static void RunTests()
    {
        Program solver = new Program();
        (int[] Nums, int Expected)[] cases =
        [
            ([2, 1, 5, 1, 1], 3),
            ([1, 4, 7, 10], 1),
            ([1], 1),
            ([1, 1], 2),
            ([1, 1, 1], 2),
            ([1, 2, 3, 4], 4),
            ([1, 3], 2),
            ([1, 4], 1),
            ([1, 2, 2, 3], 4),
            ([1000000, 1000000], 2),
            ([4, 3, 2, 1], 4)
        ];

        foreach ((int[] nums, int expected) in cases)
        {
            VerifyCase(solver, nums, expected, true);
        }
        Console.WriteLine($"Fixed cases passed: {cases.Length}");

        int exhaustiveCount = 0;
        for (int length = 1; length <= 7; length++)
        {
            foreach (int[] nums in GenerateSortedInputs(new int[length], 0, 1))
            {
                VerifyCase(solver, nums, MaxSelectedElementsBruteForce(nums), false);
                exhaustiveCount++;
            }
        }
        Console.WriteLine($"Exhaustive cases passed: {exhaustiveCount} (length 1..7, values 1..5)");

        VerifyCase(solver, Enumerable.Repeat(1, 100000).ToArray(), 2, false);
        VerifyCase(solver, Enumerable.Range(1, 100000).ToArray(), 100000, false);
        Console.WriteLine("Large cases passed: 2 (length 100000)");
        Console.WriteLine($"All {cases.Length + exhaustiveCount + 2} cases passed for both solutions.");
    }

    /// <summary>
    /// 將同一合法非空輸入的副本交給兩種解法，比對指定的預期最大長度。
    /// 可選擇輸出案例；不相符時拋出含解法名稱、輸入及預期與實際結果的例外。
    /// </summary>
    /// <param name="solver">提供兩種解法的實例。</param>
    /// <param name="nums">符合題目限制的輸入，驗證過程不會修改此陣列。</param>
    /// <param name="expected">獨立給定或由暴力法算出的正確結果。</param>
    /// <param name="printCase">是否輸出案例與兩種解法的結果。</param>
    private static void VerifyCase(Program solver, int[] nums, int expected, bool printCase)
    {
        int dictionary = solver.MaxSelectedElements((int[])nums.Clone());
        int constant = solver.MaxSelectedElementsConstantSpace((int[])nums.Clone());
        if (dictionary != expected || constant != expected)
        {
            throw new InvalidOperationException(
                $"Input=[{string.Join(",", nums)}], expected={expected}, DictionaryDP={dictionary}, ConstantSpaceDP={constant}");
        }
        if (printCase)
        {
            Console.WriteLine($"PASS [{string.Join(",", nums)}] expected={expected}, DictionaryDP={dictionary}, ConstantSpaceDP={constant}");
        }
    }

    /// <summary>
    /// 以回溯產生指定長度、值域 1 到 5 的所有非遞減陣列，供小範圍窮舉驗證。
    /// buffer 為正長度工作陣列，index 為目前位置，minimum 為此位置的最小值；回傳獨立副本。
    /// </summary>
    /// <param name="buffer">回溯共用的工作陣列。</param>
    /// <param name="index">從 0 開始的填入位置。</param>
    /// <param name="minimum">維持非遞減順序的下界，介於 1 到 5。</param>
    /// <returns>所有符合條件的陣列副本。</returns>
    private static IEnumerable<int[]> GenerateSortedInputs(int[] buffer, int index, int minimum)
    {
        if (index == buffer.Length)
        {
            yield return (int[])buffer.Clone();
            yield break;
        }
        for (int value = minimum; value <= 5; value++)
        {
            buffer[index] = value;
            foreach (int[] nums in GenerateSortedInputs(buffer, index + 1, value))
            {
                yield return nums;
            }
        }
    }

    /// <summary>
    /// 對長度 1 到 7 的合法小陣列枚舉每個元素是否加一，作為獨立正確性參考。
    /// 對每個修改結果去重並排序，找出最長連續段；回傳所有選擇中的最大長度，不修改輸入。
    /// </summary>
    /// <param name="nums">長度 1 到 7 的正整數陣列，僅供測試。</param>
    /// <returns>最多可選取的連續且不重複元素數量。</returns>
    private static int MaxSelectedElementsBruteForce(int[] nums)
    {
        int best = 0;
        for (int mask = 0; mask < (1 << nums.Length); mask++)
        {
            SortedSet<int> values = new SortedSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                values.Add(nums[i] + ((mask >> i) & 1));
            }

            // 去重不會損失可選長度：相同最終值最多只能選一個。
            int previous = -1;
            int run = 0;
            foreach (int value in values)
            {
                run = value == previous + 1 ? run + 1 : 1;
                best = Math.Max(best, run);
                previous = value;
            }
        }
        return best;
    }

    /// <summary>
    /// 方法一：排序後以 Dictionary 動態規劃，求修改後最多可選取的連續元素數量。
    /// dp[v] 表示已處理元素中，以 v 結尾的最長連續長度；每個元素可維持原值或加一。
    /// 輸入須為符合題目限制的非空正整數陣列，會原地排序；時間 O(n log n)，額外空間 O(n)。
    /// </summary>
    /// <param name="nums">符合題目限制、會被原地排序的非空正整數陣列。</param>
    /// <returns>能選取的最長連續且不重複元素數量。</returns>
    public int MaxSelectedElements(int[] nums)
    {
        Array.Sort(nums);
        Dictionary<int, int> dp = new Dictionary<int, int>();
        int res = 0;
        foreach (int x in nums)
        {
            // 保存更新前的狀態，讓兩種選擇只使用先前元素，避免同一個 x 被接上兩次。
            dp.TryGetValue(x - 1, out int prev);
            dp.TryGetValue(x, out int curr);
            dp.TryGetValue(x + 1, out int next);

            dp[x + 1] = Math.Max(next, curr + 1); // 將 x 加一，接到原本以 x 結尾的序列。
            dp[x] = Math.Max(curr, prev + 1); // 保持 x，接到原本以 x - 1 結尾的序列。
            res = Math.Max(res, Math.Max(dp[x], dp[x + 1]));
        }
        return res;
    }

    /// <summary>
    /// 排序後以兩個狀態求最大連續元素數量；輸入為符合題目限制的非空正整數陣列。
    /// 會原地排序輸入，回傳最大可選長度；排序後 DP 額外空間為 O(1)。
    /// </summary>
    /// <param name="nums">會被原地排序的非空正整數陣列。</param>
    /// <returns>每個元素可獨立加一後，能選取的最長連續且不重複元素數量。</returns>
    public int MaxSelectedElementsConstantSpace(int[] nums)
    {
        Array.Sort(nums);
        int previous = nums[0];
        int unchanged = 1;
        int incremented = 1;
        int best = 1;

        for (int i = 1; i < nums.Length; i++)
        {
            int current = nums[i];
            int gap = current - previous;
            int oldUnchanged = unchanged;
            int oldIncremented = incremented;

            // 舊狀態分別以 previous、previous + 1 結尾；新狀態改以 current、current + 1 結尾。
            if (gap == 0)
            {
                // 相同值不能接在自己後面；另一個副本加一，才可延伸原值結尾的序列。
                incremented = Math.Max(oldIncremented, oldUnchanged + 1);
            }
            else if (gap == 1)
            {
                // 保持原值可延伸舊 unchanged，也可保留已到達 current 的舊 incremented。
                unchanged = Math.Max(oldUnchanged + 1, oldIncremented);
                incremented = oldIncremented + 1;
            }
            else if (gap == 2)
            {
                // 前一個值加一後恰好是 current - 1；目前值若也加一則仍有缺口。
                unchanged = oldIncremented + 1;
                incremented = 1;
            }
            else
            {
                // 每個元素只能加一，差距大於二時兩種結尾都必須重新開始。
                unchanged = 1;
                incremented = 1;
            }

            best = Math.Max(best, Math.Max(unchanged, incremented));
            previous = current;
        }
        return best;
    }
}