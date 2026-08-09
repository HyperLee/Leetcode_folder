namespace leetcode_3379;

class Program
{
    /// <summary>
    /// 3379. Transformed Array
    /// https://leetcode.com/problems/transformed-array/description/
    /// <para>
    /// You are given an integer array nums representing a circular array. Create result of the same size by performing these independent actions for every index i, where 0 &lt;= i &lt; nums.length:
    /// - If nums[i] &gt; 0, move nums[i] steps right from i and set result[i] to the landing value.
    /// - If nums[i] &lt; 0, move abs(nums[i]) steps left from i and set result[i] to the landing value.
    /// - If nums[i] == 0, set result[i] to nums[i].
    ///
    /// Return result. Movement wraps around either end because nums is circular.
    ///
    /// Example 1:
    /// Input: nums = [3,-2,1,1]
    /// Output: [1,1,1,3]
    /// Explanation: From index 0 move 3 right to index 3, giving 1. From index 1 move 2 left to index 3, giving 1. From index 2 move 1 right to index 3, giving 1. From index 3 move 1 right to index 0, giving 3.
    ///
    /// Example 2:
    /// Input: nums = [-1,4,-1]
    /// Output: [-1,-1,4]
    /// Explanation: From index 0 move 1 left to index 2, giving -1. From index 1 move 4 right to index 2, giving -1. From index 2 move 1 left to index 1, giving 4.
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 100
    /// - -100 &lt;= nums[i] &lt;= 100
    /// </para>
    /// <para>
    /// 3379. 轉換後的陣列
    /// https://leetcode.cn/problems/transformed-array/description/
    ///
    /// 給定表示環狀陣列的整數陣列 nums。對每個索引 i（0 &lt;= i &lt; nums.length）分別執行下列操作，建立相同大小的 result：
    /// - 若 nums[i] &gt; 0，從 i 向右移動 nums[i] 步，將 result[i] 設為落點的值。
    /// - 若 nums[i] &lt; 0，從 i 向左移動 abs(nums[i]) 步，將 result[i] 設為落點的值。
    /// - 若 nums[i] == 0，將 result[i] 設為 nums[i]。
    ///
    /// 回傳 result。因 nums 為環狀，越過任一端時會繞回另一端。
    ///
    /// 範例 1：
    /// 輸入：nums = [3,-2,1,1]
    /// 輸出：[1,1,1,3]
    /// 解釋：從索引 0 向右移動 3 步到索引 3，得到 1；從索引 1 向左移動 2 步到索引 3，得到 1；從索引 2 向右移動 1 步到索引 3，得到 1；從索引 3 向右移動 1 步到索引 0，得到 3。
    ///
    /// 範例 2：
    /// 輸入：nums = [-1,4,-1]
    /// 輸出：[-1,-1,4]
    /// 解釋：從索引 0 向左移動 1 步到索引 2，得到 -1；從索引 1 向右移動 4 步到索引 2，得到 -1；從索引 2 向左移動 1 步到索引 1，得到 4。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 100
    /// - -100 &lt;= nums[i] &lt;= 100
    /// </para>
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
