namespace leetcode_2962;

class Program
{
    /// <summary>
    /// 2962. Count Subarrays Where Max Element Appears at Least K Times
    /// https://leetcode.com/problems/count-subarrays-where-max-element-appears-at-least-k-times/description/?envType=daily-question&envId=2024-03-29
    /// 2962. 统计最大元素出现至少 K 次的子数组
    /// https://leetcode.cn/problems/count-subarrays-where-max-element-appears-at-least-k-times/description/
    /// 
    /// 題目描述：
    /// 給定一個整數數組 nums 和一個整數 k，請找出子數組的數量，這些子數組的最大元素至少出現 k 次。
    /// 解題出發點建議：
    /// 1. 使用滑動視窗技術來處理子數組的範圍。
    /// 2. 需要統計最大元素的出現次數，並根據條件調整視窗的起點。
    /// 3. 注意結果的數據類型為 long，避免溢出。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        int passedChecks = 0;
        int totalChecks = 0;

        RunCase("官方範例：最大值出現至少兩次", [1, 3, 2, 3, 3], 2, 6, ref passedChecks, ref totalChecks);
        RunCase("官方範例：最大值出現次數不足", [1, 4, 2, 1], 3, 0, ref passedChecks, ref totalChecks);
        RunCase("全部元素相同", [1, 1, 1, 1], 3, 3, ref passedChecks, ref totalChecks);
        RunCase("k 等於一", [1, 2, 3, 4, 5], 1, 5, ref passedChecks, ref totalChecks);
        RunCase("最大值恰好出現 k 次", [10, 10, 10, 10, 10, 10], 6, 1, ref passedChecks, ref totalChecks);
        RunCase("混合陣列但最大值不足 k 次", [1, 2, 3, 2, 1], 2, 0, ref passedChecks, ref totalChecks);

        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項測試通過");
        Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
    }

    /// <summary>
    /// 執行一組測試資料，分別呼叫兩個滑動視窗解法，並比對手動推導的期望結果。
    /// 輸入必須符合題目限制：陣列非空、元素皆為正整數，且 <paramref name="k"/> 為正整數。
    /// 執行結果會輸出至主控台，並透過參考參數累計通過與總檢查數。
    /// </summary>
    /// <param name="name">用來辨識測試目的的案例名稱。</param>
    /// <param name="nums">本次測試使用的非空正整數陣列。</param>
    /// <param name="k">全域最大元素至少需要出現的次數。</param>
    /// <param name="expected">手動推導的預期子陣列數量。</param>
    /// <param name="passedChecks">目前累計通過的檢查數，方法會直接更新此值。</param>
    /// <param name="totalChecks">目前累計執行的檢查數，方法會直接更新此值。</param>
    private static void RunCase(
        string name,
        int[] nums,
        int k,
        long expected,
        ref int passedChecks,
        ref int totalChecks)
    {
        long actual1 = CountSubarrays(nums, k);
        long actual2 = CountSubarrays2(nums, k);
        bool passed1 = actual1 == expected;
        bool passed2 = actual2 == expected;

        totalChecks += 2;
        passedChecks += passed1 ? 1 : 0;
        passedChecks += passed2 ? 1 : 0;

        Console.WriteLine($"案例：{name}");
        Console.WriteLine($"nums = [{string.Join(", ", nums)}], k = {k}");
        Console.WriteLine($"CountSubarrays  | Expected: {expected} | Actual: {actual1} | {(passed1 ? "PASS" : "FAIL")}");
        Console.WriteLine($"CountSubarrays2 | Expected: {expected} | Actual: {actual2} | {(passed2 ? "PASS" : "FAIL")}");
        Console.WriteLine();
    }


    /// <summary>
    /// 計算全域最大元素至少出現 <paramref name="k"/> 次的連續子陣列數量。
    /// 先以一次走訪找出全域最大值，再用 <c>[start, end]</c> 滑動視窗追蹤其出現次數；
    /// 每當視窗已含有 <paramref name="k"/> 個最大值，就持續右移左邊界，藉由最後的
    /// <c>start</c> 一次計入所有以目前右邊界結尾的有效子陣列。
    /// 輸入必須符合題目限制：<paramref name="nums"/> 非空且元素皆為正整數，
    /// <paramref name="k"/> 為正整數；回傳值為符合條件的子陣列總數。
    /// </summary>
    /// <param name="nums">長度介於 1 到 100,000 的正整數陣列。</param>
    /// <param name="k">全域最大元素至少需要出現的次數，介於 1 到 100,000。</param>
    /// <returns>全域最大元素至少出現 <paramref name="k"/> 次的連續子陣列數量。</returns>
    /// <remarks>時間複雜度為 O(n)，額外空間複雜度為 O(1)。</remarks>
    public static long CountSubarrays(int[] nums, int k)
    {
        int maxNum = 0;
        foreach (int number in nums)
        {
            maxNum = Math.Max(maxNum, number);
        }

        long result = 0;
        int start = 0;
        int end = 0;
        int maxCount = 0;

        while (end < nums.Length)
        {
            if (nums[end] == maxNum)
            {
                maxCount++;
            }

            // 將左邊界移到第一個無效起點；被越過的每個起點都能形成有效子陣列。
            while (maxCount == k)
            {
                if (nums[start] == maxNum)
                {
                    maxCount--;
                }

                start++;
            }

            // 起點 0 到 start - 1 皆含至少 k 個最大值，因此本輪新增 start 個答案。
            result += start;
            end++;
        }

        return result;
    }

    /// <summary>
    /// 計算全域最大元素至少出現 <paramref name="k"/> 次的連續子陣列數量。
    /// 此版本以 LINQ <see cref="Enumerable.Max(IEnumerable{int})"/> 取得全域最大值，
    /// 並用 <c>for</c> 迴圈推進右邊界；當視窗剛好含有 <paramref name="k"/> 個最大值時，
    /// 持續收縮左側直到視窗失效，接著以左邊界位置累計所有有效起點。
    /// 輸入必須符合題目限制：<paramref name="nums"/> 非空且元素皆為正整數，
    /// <paramref name="k"/> 為正整數；回傳值為符合條件的子陣列總數。
    /// </summary>
    /// <param name="nums">長度介於 1 到 100,000 的正整數陣列。</param>
    /// <param name="k">全域最大元素至少需要出現的次數，介於 1 到 100,000。</param>
    /// <returns>全域最大元素至少出現 <paramref name="k"/> 次的連續子陣列數量。</returns>
    /// <remarks>時間複雜度為 O(n)，額外空間複雜度為 O(1)。</remarks>
    public static long CountSubarrays2(int[] nums, int k)
    {
        int maxNum = nums.Max();
        long result = 0;
        int start = 0;
        int maxCount = 0;

        for (int end = 0; end < nums.Length; end++)
        {
            if (nums[end] == maxNum)
            {
                maxCount++;
            }

            // 收縮至只剩 k - 1 個最大值，讓 start 左側的所有起點都代表有效子陣列。
            while (maxCount == k)
            {
                if (nums[start] == maxNum)
                {
                    maxCount--;
                }

                start++;
            }

            result += start;
        }

        return result;
    }
}