namespace leetcode_594;

class Program
{
    /// <summary>
    /// 594. Longest Harmonious Subsequence
    /// https://leetcode.com/problems/longest-harmonious-subsequence/description/?envType=daily-question&envId=2025-06-30
    /// 594. 最長和諧子序列
    /// https://leetcode.cn/problems/longest-harmonious-subsequence/description/?envType=daily-question&envId=2025-06-30
    /// 
    /// 題目描述：
    /// 我們定義一個和諧陣列為其最大值與最小值的差正好為 1 的陣列。
    /// 給定一個整數陣列 nums，請回傳所有可能子序列中，最長和諧子序列的長度。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1: [1,3,2,2,5,2,3,7]
        int[] nums1 = { 1, 3, 2, 2, 5, 2, 3, 7 };
        int result1 = solution.FindLHS(nums1);
        Console.WriteLine($"測試案例 1: [{string.Join(",", nums1)}]");
        Console.WriteLine($"結果: {result1}");
        Console.WriteLine($"說明: 最長和諧子序列是 [3,2,2,2,3]，長度為 5\n");

        // 測試案例 2: [1,2,3,4]
        int[] nums2 = { 1, 2, 3, 4 };
        int result2 = solution.FindLHS(nums2);
        Console.WriteLine($"測試案例 2: [{string.Join(",", nums2)}]");
        Console.WriteLine($"結果: {result2}");
        Console.WriteLine($"說明: 有多個長度為 2 的和諧子序列，如 [1,2]、[2,3]、[3,4]\n");

        // 測試案例 3: [1,1,1,1]
        int[] nums3 = { 1, 1, 1, 1 };
        int result3 = solution.FindLHS(nums3);
        Console.WriteLine($"測試案例 3: [{string.Join(",", nums3)}]");
        Console.WriteLine($"結果: {result3}");
        Console.WriteLine($"說明: 所有元素相同，無法形成和諧子序列\n");

        // 測試案例 4: [1,3,2,2,5,2,3,7,1]
        int[] nums4 = { 1, 3, 2, 2, 5, 2, 3, 7, 1 };
        int result4 = solution.FindLHS(nums4);
        Console.WriteLine($"測試案例 4: [{string.Join(",", nums4)}]");
        Console.WriteLine($"結果: {result4}");
        Console.WriteLine($"說明: 包含重複元素的更複雜案例");
    }

    /// <summary>
    /// 解題說明：
    /// 使用哈希表方法，先遍歷陣列統計每個數字的出現次數，然後遍歷哈希表，
    /// 對於每個數字 x，檢查 x+1 是否存在，如果存在，則 x 和 x+1 的出現次數之和
    /// 就是一個和諧子序列的長度。時間複雜度 O(n)，空間複雜度 O(n)。
    /// 
    /// 時間複雜度：O(n) - 需要遍歷陣列兩次，一次建立哈希表，一次查找
    /// 空間複雜度：O(n) - 哈希表存儲所有不同數字的計數
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>最長和諧子序列的長度</returns>
    public int FindLHS(int[] nums)
    {
        // 建立哈希表，統計每個數字的出現次數
        var count = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            if (count.ContainsKey(num))
            {
                count[num]++;
            }
            else
            {
                count[num] = 1;
            }
        }

        /* // 使用 TryGetValue 和 null-coalescing 運算子簡化程式碼
        var count = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            count[num] = count.GetValueOrDefault(num, 0) + 1;
        }
        */

        int maxLength = 0;
        // 遍歷哈希表中的每個鍵值對 (x, value)
        foreach (var key in count.Keys)
        {
            // 查詢 x+1 是否存在於哈希表中
            if (count.ContainsKey(key + 1))
            {
                // 如果 x+1 存在，則 x 和 x+1 的出現次數之和就是一個和諧子序列的長度
                maxLength = Math.Max(maxLength, count[key] + count[key + 1]);
            }
        }

        return maxLength;
    }
    
    
    /// <summary>
    /// 暴力解法 - 時間複雜度 O(n²)
    /// 問題：每個數字都要重新掃描整個陣列，效率很低！
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int FindLHS_BruteForce(int[] nums)
    {
        int maxLength = 0;

        // 對每個數字 x
        for (int i = 0; i < nums.Length; i++)
        {
            int x = nums[i];
            int countX = 0, countXPlus1 = 0;

            // 遍歷整個陣列統計 x 和 x+1 的出現次數
            for (int j = 0; j < nums.Length; j++)
            {
                if (nums[j] == x) countX++;
                else if (nums[j] == x + 1) countXPlus1++;
            }

            // 如果 x+1 存在，計算和諧子序列長度
            if (countXPlus1 > 0)
            {
                maxLength = Math.Max(maxLength, countX + countXPlus1);
            }
        }

        return maxLength;
    }    
}
