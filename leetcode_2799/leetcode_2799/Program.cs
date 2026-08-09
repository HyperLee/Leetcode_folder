namespace leetcode_2799;

class Program
{
    /// <summary>
    /// 2799. Count Complete Subarrays in an Array
    /// https://leetcode.com/problems/count-complete-subarrays-in-an-array/description/?envType=daily-question&envId=2025-04-24
    /// 2799. 统计完全子数组的数目
    /// https://leetcode.cn/problems/count-complete-subarrays-in-an-array/description/?envType=daily-question&envId=2025-04-24
    /// 
    /// Array, Sliding Window, Hash Table
    /// </summary>
    /// <remarks>
    /// 以五組固定案例執行兩種滑動視窗解法，逐項比較預期值與實際值；若有任一項失敗，
    /// 程式會設定非零結束碼，方便在命令列或自動化環境中辨識驗證結果。
    /// </remarks>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        (string Name, int[] Nums, int Expected)[] testCases =
        {
            ("官方範例：包含重複元素", new[] { 1, 3, 1, 2, 2 }, 4),
            ("全部相同", new[] { 5, 5, 5, 5 }, 10),
            ("全部相異", new[] { 1, 2, 3, 4 }, 1),
            ("最小長度", new[] { 1 }, 1),
            ("相異元素交錯出現", new[] { 1, 2, 1, 3, 2 }, 5)
        };
        (string Name, Func<int[], int> Solve)[] solutions =
        {
            (nameof(CountCompleteSubarrays), program.CountCompleteSubarrays),
            (nameof(CountCompleteSubarrays2), program.CountCompleteSubarrays2)
        };

        int passed = 0;
        int total = testCases.Length * solutions.Length;

        foreach ((string name, int[] nums, int expected) in testCases)
        {
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"輸入：[{string.Join(", ", nums)}]");
            Console.WriteLine($"預期：{expected}");

            foreach ((string solutionName, Func<int[], int> solve) in solutions)
            {
                int actual = solve(nums);
                bool isPassed = actual == expected;
                passed += isPassed ? 1 : 0;
                Console.WriteLine($"{solutionName}: Actual = {actual}, {(isPassed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passed}/{total} 項測試通過");

        if (passed != total)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 2799. Count Complete Subarrays in an Array 統計完整子陣列的數目
    /// 
    /// 題目說明：
    /// 給定一個正整數陣列 nums，「完整子陣列」定義為具有和原始陣列相同數量不同元素的子陣列。
    /// 例如，如果原始陣列有 3 個不同的元素，那麼任何包含這 3 個不同元素的子陣列都是「完整子陣列」。
    /// 
    /// 簡單說假如原始輸入的 nums 陣列中有 k 個不同的元素，那麼任何包含這 k 個不同元素的子陣列都是完整子陣列。
    /// 注意只要包涵了這 k 個不同元素的子陣列都是完整子陣列，元素的數量不需要等於 k。
    /// 
    /// 解題思路：
    /// 使用滑動窗口（Sliding Window）技術求解。先計算原始陣列中不同元素的數量，
    /// 然後遍歷每個可能的起始位置，對每個起始位置，找出最短的窗口使其包含所有不同元素，
    /// 然後計算以該起始位置開始的有效子陣列數量。
    /// 
    /// 固定左邊界，然後右邊界不斷向右移動，直到窗口中包含所有不同元素。
    /// 當不同元素的數量等於原始陣列中的不同元素數量時（此時已經符合題目描述的完整子陣列）。
    /// 再者繼續計算右邊界到陣列結尾的所有子陣列數量，這些子陣列都是完整的子陣列。
    /// 
    /// 時間複雜度：O(n)，其中 n 為陣列長度
    /// 空間複雜度：O(k)，其中 k 為不同元素的數量
    /// </summary>
    /// <remarks>
    /// 輸入須符合題目限制：陣列不可為 null，長度介於 1 到 1000，每個元素介於 1 到 2000。
    /// 此方法只讀取陣列，不會修改輸入內容。
    /// </remarks>
    /// <param name="nums">符合題目限制的正整數陣列。</param>
    /// <returns>包含原陣列全部相異元素的連續非空子陣列數量。</returns>
    public int CountCompleteSubarrays(int[] nums)
    {
        int result = 0;
        Dictionary<int, int> frequencies = new Dictionary<int, int>();
        int length = nums.Length;
        int right = 0;
        int requiredDistinct = new HashSet<int>(nums).Count;

        // 固定左邊界；right 只向右移動，因此每個元素至多進出視窗一次。
        for (int left = 0; left < length; left++)
        {
            if (left > 0)
            {
                int removed = nums[left - 1];
                frequencies[removed]--;
                if (frequencies[removed] == 0)
                {
                    frequencies.Remove(removed);
                }
            }

            // 找到從 left 出發、第一個包含全部相異元素的最短視窗 [left, right)。
            while (right < length && frequencies.Count < requiredDistinct)
            {
                int added = nums[right];
                frequencies[added] = frequencies.GetValueOrDefault(added) + 1;
                right++;
            }

            if (frequencies.Count == requiredDistinct)
            {
                // 最短完整視窗的結尾是 right - 1；再向右延伸仍然完整，共 length - right + 1 種。
                result += length - right + 1;
            }
        }

        return result;
    }

    /// <summary>
    /// 統計完整子陣列的數量。先取得整個陣列的相異元素數 k，再以滑動視窗分別計算
    /// 「至多包含 k 種元素」與「至多包含 k - 1 種元素」的子陣列數量，兩者相減即為
    /// 恰好包含 k 種元素的完整子陣列數量。輸入須為長度 1 到 1000、元素值 1 到 2000
    /// 的非 null 正整數陣列；方法回傳完整連續非空子陣列的總數，且不修改輸入。
    /// </summary>
    /// <param name="nums">符合題目限制的正整數陣列。</param>
    /// <returns>包含原陣列全部相異元素的連續非空子陣列數量。</returns>
    public int CountCompleteSubarrays2(int[] nums)
    {
        int requiredDistinct = new HashSet<int>(nums).Count;

        return CountSubarraysWithAtMostDistinct(nums, requiredDistinct)
            - CountSubarraysWithAtMostDistinct(nums, requiredDistinct - 1);
    }

    /// <summary>
    /// 使用滑動視窗計算至多包含指定相異元素數量的連續非空子陣列。
    /// 輸入陣列須符合題目條件，maxDistinct 須為非負整數；回傳所有符合上限的子陣列數量。
    /// </summary>
    /// <param name="nums">符合題目限制的正整數陣列。</param>
    /// <param name="maxDistinct">視窗允許包含的相異元素數量上限。</param>
    /// <returns>相異元素數量不超過 maxDistinct 的連續非空子陣列總數。</returns>
    private static int CountSubarraysWithAtMostDistinct(int[] nums, int maxDistinct)
    {
        Dictionary<int, int> frequencies = new Dictionary<int, int>();
        int left = 0;
        int result = 0;

        for (int right = 0; right < nums.Length; right++)
        {
            int added = nums[right];
            frequencies[added] = frequencies.GetValueOrDefault(added) + 1;

            // 移動左界直到視窗重新符合「至多 maxDistinct 種元素」。
            while (frequencies.Count > maxDistinct)
            {
                int removed = nums[left];
                frequencies[removed]--;
                if (frequencies[removed] == 0)
                {
                    frequencies.Remove(removed);
                }

                left++;
            }

            // 固定 right 時，[left..right] 內的每個起點都能形成合法子陣列。
            result += right - left + 1;
        }

        return result;
    }
}