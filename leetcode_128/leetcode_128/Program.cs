namespace leetcode_128;

class Program
{
    /// <summary>
    /// <para>
    /// 128. Longest Consecutive Sequence
    /// https://leetcode.com/problems/longest-consecutive-sequence/description/
    ///
    /// Given an unsorted array of integers nums, return the length of the longest consecutive elements sequence.
    /// You must write an algorithm that runs in O(n) time.
    ///
    /// Example 1:
    /// Input: nums = [100,4,200,1,3,2]
    /// Output: 4
    /// Explanation: The longest consecutive elements sequence is [1, 2, 3, 4]. Therefore its length is 4.
    ///
    /// Example 2:
    /// Input: nums = [0,3,7,2,5,8,4,6,0,1]
    /// Output: 9
    ///
    /// Example 3:
    /// Input: nums = [1,0,1,2]
    /// Output: 3
    ///
    /// Constraints:
    /// 0 &lt;= nums.length &lt;= 10^5
    /// -10^9 &lt;= nums[i] &lt;= 10^9
    /// </para>
    /// <para>
    /// 128. 最長連續序列
    /// https://leetcode.cn/problems/longest-consecutive-sequence/description/
    ///
    /// 給定未排序的整數陣列 nums，請回傳最長連續元素序列的長度。
    /// 你必須撰寫一個以 O(n) 時間執行的演算法。
    ///
    /// 範例 1：
    /// 輸入：nums = [100,4,200,1,3,2]
    /// 輸出：4
    /// 解釋：最長連續元素序列為 [1, 2, 3, 4]，因此其長度為 4。
    ///
    /// 範例 2：
    /// 輸入：nums = [0,3,7,2,5,8,4,6,0,1]
    /// 輸出：9
    ///
    /// 範例 3：
    /// 輸入：nums = [1,0,1,2]
    /// 輸出：3
    ///
    /// 限制條件：
    /// 0 &lt;= nums.length &lt;= 10^5
    /// -10^9 &lt;= nums[i] &lt;= 10^9
    /// </para>
    /// </summary>
    static void Main(string[] args)
    {
        // 測試案例 1: 基本案例
        int[] test1 = { 100, 4, 200, 1, 3, 2 };
        Console.WriteLine("Test 1 - 基本案例:");
        Console.WriteLine($"Input: [{string.Join(", ", test1)}]");
        Console.WriteLine($"Output: {LongestConsecutive(test1)}\n");

        // 測試案例 2: 空陣列
        int[] test2 = { };
        Console.WriteLine("Test 2 - 空陣列:");
        Console.WriteLine($"Input: []");
        Console.WriteLine($"Output: {LongestConsecutive(test2)}\n");

        // 測試案例 3: 包含重複數字
        int[] test3 = { 1, 2, 0, 1, 3, 2, 5, 2, 4 };
        Console.WriteLine("Test 3 - 重複數字:");
        Console.WriteLine($"Input: [{string.Join(", ", test3)}]");
        Console.WriteLine($"Output: {LongestConsecutive(test3)}\n");

        // 測試案例 4: 全部數字都不連續
        int[] test4 = { 2, 4, 6, 8, 10 };
        Console.WriteLine("Test 4 - 不連續數字:");
        Console.WriteLine($"Input: [{string.Join(", ", test4)}]");
        Console.WriteLine($"Output: {LongestConsecutive(test4)}\n");

        // 測試案例 5: 負數案例
        int[] test5 = { -5, -3, -2, -1, -4, 0, 2, 4 };
        Console.WriteLine("Test 5 - 包含負數:");
        Console.WriteLine($"Input: [{string.Join(", ", test5)}]");
        Console.WriteLine($"Output: {LongestConsecutive(test5)}\n");
    }

    /// <summary>
    /// 找出數組中最長的連續序列長度
    /// 解題思路：
    /// 1. 使用 HashSet 優化查詢時間，將數組轉換為集合，去除重複值
    /// 2. 只從連續序列的起點開始計算（若 n-1 不存在，則 n 為起點）
    /// 3. 對每個起點，往後查找連續的數字，並記錄最長序列
    /// 時間複雜度：O (n)，雖有巢狀迴圈，但每個數字最多被訪問 2 次
    /// 空間複雜度：O (n)，需要 HashSet 存儲所有數字
    /// 
    /// 檢查 num-1 的原因：
    /// 1. 目的是找到每個連續序列的起點，避免重複計算
    /// 2. 舉例：對於序列 [1,2,3,4]
    ///    - 當檢查 4 時，因為 3 存在，所以 4 不是起點
    ///    - 當檢查 3 時，因為 2 存在，所以 3 不是起點
    ///    - 當檢查 2 時，因為 1 存在，所以 2 不是起點
    ///    - 當檢查 1 時，因為 0 不存在，所以 1 是起點
    /// 3. 這樣可以確保每個連續序列只被計算一次，從最小的數字開始
    /// 4. 如果不檢查 num-1，每個數字都會計算一次，會導致重複計算
    /// </summary>
    /// <param name="nums">輸入的整數數組</param>
    /// <returns>最長連續序列的長度</returns>
    public static int LongestConsecutive(int[] nums)
    {
        // 步驟1: 建立 HashSet 來儲存所有數字，移除重複值並提供 O(1) 的查詢時間
        HashSet<int> numSet = new HashSet<int>();
        foreach (int num in nums)
        {
            numSet.Add(num);
        }

        // 步驟2: 初始化最長序列長度為0
        int longestStreak = 0;

        // 步驟3: 遍歷 HashSet 中的每個數字，這邊是題目重點之一
        foreach (int num in numSet)
        {
            // 步驟 4: 檢查是否為序列的起始數字，這邊是題目重點之一
            // 如果當前數字減 1 不在集合中，表示這是一個序列的起點
            if (!numSet.Contains(num - 1))
            {
                // 步驟5: 初始化當前數字和當前序列長度
                int currentNum = num;
                int currentStreak = 1;

                // 步驟6: 向後尋找連續的數字，這邊是題目重點之一
                // 當集合中存在下一個連續的數字時，繼續計數
                while (numSet.Contains(currentNum + 1))
                {
                    currentNum++;    // 更新當前數字
                    currentStreak++; // 序列長度加1
                }

                // 步驟7: 更新最長序列長度
                // 比較並保存最大值
                longestStreak = Math.Max(longestStreak, currentStreak);
            }
        }

        // 步驟8: 返回找到的最長連續序列長度
        return longestStreak;
    }
}
