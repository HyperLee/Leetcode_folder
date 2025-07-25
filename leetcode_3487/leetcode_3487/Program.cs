namespace leetcode_3487;

class Program
{
    /// <summary>
    /// 3487. Maximum Unique Subarray Sum After Deletion
    /// https://leetcode.com/problems/maximum-unique-subarray-sum-after-deletion/description/?envType=daily-question&envId=2025-07-25
    /// 3487. 刪除後的最大子陣列元素和
    /// https://leetcode.cn/problems/maximum-unique-subarray-sum-after-deletion/description/?envType=daily-question&envId=2025-07-25
    /// 
    /// 題目描述：
    /// 給你一個整數陣列 nums。
    /// 你可以從 nums 中刪除任意數量的元素（不能刪成空陣列）。
    /// 刪除後，選擇 nums 的一個子陣列，要求：
    ///   1. 子陣列所有元素都唯一。
    ///   2. 子陣列元素和最大。
    /// 返回這樣的子陣列的最大元素和。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var program = new Program();
        int[][] testCases = new int[][]
        {
            new int[] {1, 2, 2, 3, 4},      // 正數有重複，期望 1+2+3+4=10
            new int[] {-1, -2, -3},         // 全負數，期望 -1
            new int[] {0, 0, 0},            // 全 0，期望 0
            new int[] {5, 5, 5, 5},         // 全相同正數，期望 5
            new int[] {1, -1, 2, -2, 3},    // 混合正負，期望 1+2+3=6
            new int[] {100, 200, 300},      // 全正數，期望 100+200+300=600
            new int[] {-5, 0, -10},         // 無正數，期望 0
        };

        Console.WriteLine("=== 優化版本測試結果 ===");
        for (int i = 0; i < testCases.Length; i++)
        {
            int result = program.MaxSum(testCases[i]);
            Console.WriteLine($"TestCase {i + 1}: [{string.Join(", ", testCases[i])}] => MaxSum = {result}");
        }

        Console.WriteLine("\n=== 手動版本測試結果 ===");
        for (int i = 0; i < testCases.Length; i++)
        {
            int result = program.MaxSumManual(testCases[i]);
            Console.WriteLine($"TestCase {i + 1}: [{string.Join(", ", testCases[i])}] => MaxSumManual = {result}");
        }

        Console.WriteLine("\n=== HashSet版本測試結果 ===");
        for (int i = 0; i < testCases.Length; i++)
        {
            int result = program.MaxSum2(testCases[i]);
            Console.WriteLine($"TestCase {i + 1}: [{string.Join(", ", testCases[i])}] => MaxSum2 = {result}");
        }

        // 效能測試
        Console.WriteLine("\n=== 效能測試 (大陣列) ===");
        var largeArray = GenerateLargeTestArray(100000);

        var sw = System.Diagnostics.Stopwatch.StartNew();
        int resultOptimized = program.MaxSum(largeArray);
        sw.Stop();
        Console.WriteLine($"優化版本 (LINQ): {sw.ElapsedMilliseconds} ms, 結果: {resultOptimized}");

        sw.Restart();
        int resultManual = program.MaxSumManual(largeArray);
        sw.Stop();
        Console.WriteLine($"手動版本: {sw.ElapsedMilliseconds} ms, 結果: {resultManual}");

        sw.Restart();
        int resultHashSet = program.MaxSum2(largeArray);
        sw.Stop();
        Console.WriteLine($"HashSet版本: {sw.ElapsedMilliseconds} ms, 結果: {resultHashSet}");
    }

    /// <summary>
    /// 產生大型測試陣列用於效能測試
    /// </summary>
    /// <param name="size">陣列大小</param>
    /// <returns>測試用的大陣列</returns>
    static int[] GenerateLargeTestArray(int size)
    {
        var random = new Random(42); // 固定種子確保可重現
        var array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(-1000, 1001); // -1000 到 1000 的隨機數
        }
        return array;
    }


    /// <summary>
    /// 解題說明：
    /// 本方法針對「刪除後的最大子陣列元素和」問題，採用貪心策略。
    /// 1. 只要將所有正數（>0）去重後相加，即為最大和，因為正數越多和越大。
    /// 2. 若陣列中沒有正數，則返回陣列中的最大值（可能為 0 或負數）。
    /// 
    /// 優化版本：使用 LINQ 一行解決，避免額外迴圈和 HashSet 建立。
    /// 時間複雜度：O(n)，空間複雜度：O(k) 其中 k 為唯一正數個數。
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>最大不重複子序列和</returns>
    public int MaxSum(int[] nums)
    {
        // 使用 LINQ 一行解決：篩選正數 -> 去重 -> 求和
        var uniquePositiveSum = nums.Where(x => x > 0).Distinct().Sum();

        // 若沒有正數（和為 0），返回陣列最大值
        return uniquePositiveSum > 0 ? uniquePositiveSum : nums.Max();
    }

    /// <summary>
    /// 備用解法：手動實作版本，在記憶體使用上可能更優化
    /// 適用於對記憶體敏感的場景
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>最大不重複子序列和</returns>
    public int MaxSumManual(int[] nums)
    {
        // 先篩選出所有正數並排序
        var positiveNums = nums.Where(x => x > 0).OrderBy(x => x).ToArray();

        if (positiveNums.Length == 0)
        {
            return nums.Max();
        }

        // 使用排序後的陣列去重並累加，避免 HashSet 的額外開銷
        int sum = positiveNums[0];
        for (int i = 1; i < positiveNums.Length; i++)
        {
            // 只加入與前一個不同的數字
            if (positiveNums[i] != positiveNums[i - 1])
            {
                sum += positiveNums[i];
            }
        }

        return sum;
    }


    /// <summary>
    /// 對正數去重的貪心解法
    /// 
    /// 解題思路：
    /// 題目要求找到一個非空子序列，且元素不能重複，求最大和。
    /// 採用貪心策略：不重複地將所有正數放入哈希集合並求和。
    /// 如果哈希集合內不存在任何正數，則返回陣列中元素的最大值。
    /// 
    /// 時間複雜度：O(n)，其中 n 為陣列長度
    /// 空間複雜度：O(k)，其中 k 為唯一正數的個數
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>最大不重複子序列和</returns>
    public int MaxSum2(int[] nums)
    {
        // 建立 HashSet 來儲存唯一的正數
        HashSet<int> set = new HashSet<int>();
        
        // 遍歷陣列，將所有正數加入 HashSet（自動去重）
        for (int i = 0; i < nums.Length; i++)
        {
            // 只考慮正數，因為正數對最大和有貢獻
            if (nums[i] > 0)
            {
                set.Add(nums[i]); // HashSet 會自動處理重複值
            }
        }

        // 如果沒有任何正數，返回陣列中的最大值
        // 這種情況下陣列可能全為負數或零
        if (set.Count == 0)
        {
            return nums.Max();
        }
        
        // 返回所有唯一正數的總和
        return set.Sum();
    }
}
