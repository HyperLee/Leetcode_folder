namespace leetcode_209;

class Program
{
    /// <summary>
    /// 209. Minimum Size Subarray Sum
    /// https://leetcode.com/problems/minimum-size-subarray-sum/description/
    ///
    /// Given an array of positive integers nums and a positive integer target, return the minimal length of a subarray whose sum is greater than or equal to target. If there is no such subarray, return 0 instead.
    ///
    /// 209. 最小大小子陣列總和
    /// https://leetcode.cn/problems/minimum-size-subarray-sum/description/
    ///
    /// 給定一個正整數陣列 nums 和一個正整數 target，請回傳其總和大於或等於 target 的子陣列中，長度最小者的長度。若不存在這樣的子陣列，則回傳 0。
    /// </summary>
    /// <remarks>
    /// 執行兩種解法的固定案例，將預期值與實際結果並列，讓主控台可作為輕量的回歸驗證。
    /// </remarks>
    static void Main(string[] args)
    {
        Program solution = new Program();
        RunSamples(solution);
    }

    /// <summary>
    /// 使用雙層迴圈枚舉每個連續子陣列的起點，找出總和至少為 <paramref name="target" /> 的最短長度。
    /// 對每個起點，內層迴圈在首次達標後立即停止，因此取得該起點下的最短候選視窗。
    /// </summary>
    /// <param name="target">正整數目標和。</param>
    /// <param name="nums">由正整數組成的陣列；正數條件使視窗向右延伸時總和不會減少。</param>
    /// <returns>總和大於或等於目標和的最短連續子陣列長度；沒有符合條件的子陣列時回傳 0。</returns>
    public int MinSubArrayLen(int target, int[] nums)
    {
        int n = nums.Length;
        if (n == 0)
        {
            return 0;
        }

        int result = int.MaxValue;
        for (int i = 0; i < n; i++)
        {
            int sum = 0;
            for (int j = i; j < n; j++)
            {
                sum += nums[j];
                if (sum >= target)
                {
                    // nums 全為正數；同一起點再向右延伸只會增加長度，所以首次達標即為此起點的最佳候選。
                    result = Math.Min(result, j - i + 1);
                    break;
                }
            }
        }

        return result == int.MaxValue ? 0 : result;
    }

    /// <summary>
    /// 以滑動視窗維護目前連續區間的總和，右界擴張以累積總和，達標後盡可能收縮左界以取得最短長度。
    /// 每個元素最多加入與移出視窗一次，因此可在線性時間內完成。
    /// </summary>
    /// <param name="target">正整數目標和。</param>
    /// <param name="nums">由正整數組成的陣列；移除左端元素後總和會嚴格下降，才能安全地收縮視窗。</param>
    /// <returns>總和大於或等於目標和的最短連續子陣列長度；沒有符合條件的子陣列時回傳 0。</returns>
    public int MinSubArrayLen2(int target, int[] nums)
    {
        int n = nums.Length;
        if (n == 0)
        {
            return 0;
        }

        int result = int.MaxValue;
        int left = 0;
        int sum = 0;

        for (int right = 0; right < n; right++)
        {
            sum += nums[right];

            while (sum >= target)
            {
                result = Math.Min(result, right - left + 1);

                // 視窗仍達標時持續移除左端正數，逐一檢查更短的有效視窗，直到不再達標為止。
                sum -= nums[left];
                left++;
            }
        }

        return result == int.MaxValue ? 0 : result;
    }

    /// <summary>
    /// 執行經典案例、單元素剛好達標案例與無解案例，並累計兩種解法的驗證結果。
    /// </summary>
    /// <param name="solution">提供兩個解法的執行個體。</param>
    private static void RunSamples(Program solution)
    {
        SampleCase[] samples =
        {
            new SampleCase("Canonical", 7, new[] { 2, 3, 1, 2, 4, 3 }, 2),
            new SampleCase("Exact match", 4, new[] { 1, 4, 4 }, 1),
            new SampleCase("No solution", 11, new[] { 1, 1, 1, 1, 1, 1, 1, 1 }, 0)
        };

        int passedChecks = 0;
        int totalChecks = samples.Length * 2;

        Console.WriteLine("LeetCode 209 - Minimum Size Subarray Sum");
        Console.WriteLine();

        foreach (SampleCase sample in samples)
        {
            passedChecks += RunSample(solution, sample);
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
    }

    /// <summary>
    /// 對單一測資執行暴力法與滑動視窗法，輸出兩者是否等於預期值，並回傳通過的檢查數量。
    /// </summary>
    /// <param name="solution">提供兩個解法的執行個體。</param>
    /// <param name="sample">包含案例名稱、輸入與預期輸出的測資。</param>
    /// <returns>此案例中通過的解法數量，範圍為 0 到 2。</returns>
    private static int RunSample(Program solution, SampleCase sample)
    {
        int bruteForceResult = solution.MinSubArrayLen(sample.Target, sample.Nums);
        int slidingWindowResult = solution.MinSubArrayLen2(sample.Target, sample.Nums);
        bool bruteForcePassed = bruteForceResult == sample.Expected;
        bool slidingWindowPassed = slidingWindowResult == sample.Expected;

        Console.WriteLine($"Case: {sample.Name}");
        Console.WriteLine($"Input: target = {sample.Target}, nums = [{string.Join(", ", sample.Nums)}]");
        Console.WriteLine($"Expected: {sample.Expected}");
        Console.WriteLine($"Brute Force: {bruteForceResult} ({FormatResult(bruteForcePassed)})");
        Console.WriteLine($"Sliding Window: {slidingWindowResult} ({FormatResult(slidingWindowPassed)})");
        Console.WriteLine();

        int passedChecks = 0;
        if (bruteForcePassed)
        {
            passedChecks++;
        }

        if (slidingWindowPassed)
        {
            passedChecks++;
        }

        return passedChecks;
    }

    /// <summary>
    /// 將布林驗證結果轉換為主控台使用的 PASS 或 FAIL 標記。
    /// </summary>
    /// <param name="passed">驗證結果；<see langword="true" /> 代表實際值等於預期值。</param>
    /// <returns>通過時回傳 PASS，否則回傳 FAIL。</returns>
    private static string FormatResult(bool passed)
    {
        return passed ? "PASS" : "FAIL";
    }

    /// <summary>
    /// 表示一組可由主控台範例 harness 執行的目標和、陣列與預期最短長度。
    /// </summary>
    /// <param name="Name">顯示於主控台的案例名稱。</param>
    /// <param name="Target">需要達到的正整數目標和。</param>
    /// <param name="Nums">由正整數組成的輸入陣列。</param>
    /// <param name="Expected">預期的最短連續子陣列長度。</param>
    private readonly record struct SampleCase(string Name, int Target, int[] Nums, int Expected);
}
