namespace leetcode_3379;

class Program
{
    /// <summary>
    /// 3379. Transformed Array
    /// https://leetcode.com/problems/transformed-array/description/?envType=daily-question&envId=2026-02-05
    /// https://leetcode.cn/problems/transformed-array/description/?envType=daily-question&envId=2026-02-05
    ///
    /// 題目說明（繁體中文）：
    /// 給定整數陣列 nums，視為一個環狀陣列。請建立相同大小的陣列 result，對於每個索引 i（0 <= i < nums.Length）獨立地：
    /// - 若 nums[i] > 0：從索引 i 向右移動 nums[i] 步（環狀），並將 result[i] 設為落腳索引處的值。
    /// - 若 nums[i] < 0：從索引 i 向左移動 abs(nums[i]) 步（環狀），並將 result[i] 設為落腳索引處的值。
    /// - 若 nums[i] == 0：將 result[i] 設為 nums[i]（即 0）。
    /// 回傳新的陣列 result。
    ///
    /// Problem statement (English):
    /// You are given an integer array nums representing a circular array, Your task is to create a new array result of the same size. 
    /// For each index i (0 <= i < nums.Length):
    /// - If nums[i] > 0: start at i and move nums[i] steps to the right (circular); set result[i] to the value at the landed index.
    /// - If nums[i] < 0: start at i and move abs(nums[i]) steps to the left (circular); set result[i] to the value at the landed index.
    /// - If nums[i] == 0: set result[i] to nums[i].
    /// Return the new array result.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1: nums = [3, -2, 1, 1]
        // 預期輸出: [1, 1, 1, 3]
        int[] nums1 = { 3, -2, 1, 1 };
        int[] result1 = solution.ConstructTransformedArray(nums1);
        Console.WriteLine($"測試案例 1: [{string.Join(", ", nums1)}] => [{string.Join(", ", result1)}]");
        
        // 測試案例 2: nums = [-1, 4, -1]
        // 預期輸出: [-1, -1, 4]
        int[] nums2 = { -1, 4, -1 };
        int[] result2 = solution.ConstructTransformedArray(nums2);
        Console.WriteLine($"測試案例 2: [{string.Join(", ", nums2)}] => [{string.Join(", ", result2)}]");
        
        // 測試案例 3: nums = [0, 1, -1]
        // 預期輸出: [0, 0, 0]
        int[] nums3 = { 0, 1, -1 };
        int[] result3 = solution.ConstructTransformedArray(nums3);
        Console.WriteLine($"測試案例 3: [{string.Join(", ", nums3)}] => [{string.Join(", ", result3)}]");
    }

    /// <summary>
    /// 建立轉換後的陣列
    /// 
    /// 解題思路：
    /// 1. 題目要求在環狀陣列中，根據每個位置的值進行移動，並記錄移動後位置的值
    /// 2. 核心概念是使用模運算來處理環狀陣列的索引計算
    /// 3. 關鍵在於處理負數索引的情況：(i + nums[i]) % n 在 C# 中若為負數，結果也會是負數
    ///    因此需要 +n 再取模，確保最終索引在 [0, n-1] 範圍內
    /// 
    /// 時間複雜度：O(n)，只需遍歷一次陣列
    /// 空間複雜度：O(n)，需要建立結果陣列
    /// </summary>
    /// <param name="nums">輸入的整數陣列（環狀陣列）</param>
    /// <returns>轉換後的陣列</returns>
    public int[] ConstructTransformedArray(int[] nums)
    {
        int n = nums.Length;
        int[] res = new int[n];
        
        for(int i = 0; i < n; i++)
        {
            // 計算目標索引的步驟說明：
            // 1. (i + nums[i]): 從當前索引 i 移動 nums[i] 步
            //    - nums[i] > 0: 向右移動
            //    - nums[i] < 0: 向左移動
            //    - nums[i] = 0: 保持不動
            // 
            // 2. % n: 處理環狀陣列，將索引映射到 [0, n-1] 或可能的負數範圍
            // 
            // 3. + n: 處理負數餘數
            //    例如：在 C# 中，-1 % 4 = -1（而非 3）
            //    加上 n 後：-1 + 4 = 3
            // 
            // 4. % n: 再次取模，確保最終索引在 [0, n-1] 範圍內
            //    （對於已經是正數的情況，這一步確保不超出範圍）
            int targetIndex = ((i + nums[i]) % n + n) % n;
            res[i] = nums[targetIndex];
        }
        
        return res;
    }
}
