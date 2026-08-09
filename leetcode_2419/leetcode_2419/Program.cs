namespace leetcode_2419;

class Program
{
    /// <summary>
    /// <para>
    /// 2419. Longest Subarray With Maximum Bitwise AND
    /// https://leetcode.com/problems/longest-subarray-with-maximum-bitwise-and/description/
    ///
    /// You are given integer array nums of size n. Consider non-empty subarrays whose bitwise AND is the maximum possible among all subarrays of nums. In other words, if k is that maximum, consider only subarrays whose bitwise AND equals k. Return the length of the longest such subarray. An array's bitwise AND applies bitwise AND to all its values, and a subarray is a contiguous sequence within an array.
    ///
    /// Example 1:
    /// Input: nums = [1,2,3,3,2,2]
    /// Output: 2
    /// Explanation: The maximum possible bitwise AND is 3. The longest subarray with that value is [3,3], so return 2.
    ///
    /// Example 2:
    /// Input: nums = [1,2,3,4]
    /// Output: 1
    /// Explanation: The maximum possible bitwise AND is 4. The longest subarray with that value is [4], so return 1.
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 10^5
    /// - 1 &lt;= nums[i] &lt;= 10^6
    /// </para>
    /// <para>
    /// 2419. 按位元 AND 最大的最長子陣列
    /// https://leetcode.cn/problems/longest-subarray-with-maximum-bitwise-and/description/
    ///
    /// 給定大小為 n 的整數陣列 nums。考慮所有非空子陣列中，按位元 AND 達到最大可能值者。換言之，若 k 是所有子陣列按位元 AND 的最大值，就只考慮按位元 AND 等於 k 的子陣列。回傳其中最長子陣列的長度。陣列的按位元 AND 是對所有值套用按位元 AND；子陣列是陣列中的連續序列。
    ///
    /// 範例 1：
    /// 輸入：nums = [1,2,3,3,2,2]
    /// 輸出：2
    /// 說明：可能的最大按位元 AND 是 3；具有該值的最長子陣列為 [3,3]，因此回傳 2。
    ///
    /// 範例 2：
    /// 輸入：nums = [1,2,3,4]
    /// 輸出：1
    /// 說明：可能的最大按位元 AND 是 4；具有該值的最長子陣列為 [4]，因此回傳 1。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 10^5
    /// - 1 &lt;= nums[i] &lt;= 10^6
    /// </para>
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料設計
        int[][] testCases = new int[][]
        {
            new int[] {1, 2, 3, 3, 2}, // 一般情境，最大值 3 連續 2 次
            new int[] {1, 1, 1, 1},     // 全部相同，最大值 1 連續 4 次
            new int[] {5},              // 單一元素
            new int[] {2, 2, 2, 1, 2},  // 最大值 2 連續 3 次
            new int[] {1, 2, 3, 4, 5},  // 遞增，最大值 5 連續 1 次
            new int[] {5, 5, 5, 5, 5},  // 全部最大值
            new int[] {1, 3, 2, 3, 3, 1, 3}, // 多段最大值
            new int[] {0, 0, 0, 0},     // 全為 0
            new int[] {int.MinValue, int.MaxValue, int.MaxValue}, // 包含極值
        };
        var program = new Program();
        for (int i = 0; i < testCases.Length; i++)
        {
            int[] test = testCases[i];
            int result1 = program.LongestSubarray(test);
            int result2 = program.LongestSubarray2(test);
            Console.WriteLine($"TestCase {i + 1}: [{string.Join(", ", test)}]");
            Console.WriteLine($"  LongestSubarray  = {result1}");
            Console.WriteLine($"  LongestSubarray2 = {result2}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 2419. 按位與最大的最長子陣列
    /// 解題說明：
    /// 1. 觀察與簡化：子陣列的按位與最大值，必然等於陣列中某個元素的值（因為 AND 運算只會讓值變小或不變），所以最大 AND 值就是 nums 中的最大值 maxVal。
    /// 2. 目標轉化：問題轉化為找出所有連續的 maxVal 段落，並返回這些段落的最大長度。
    /// 3. 解法步驟：先遍歷一次陣列，找出最大值 maxVal；再遍歷一次，統計連續出現 maxVal 的最長長度。
    /// 4. 時間複雜度：只需兩次遍歷，O(n)。
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.LongestSubarray(new int[] {1,2,3,3,2});
    /// result 應為 2
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="nums">輸入整數陣列</param>
    /// <returns>最大 AND 值的最長子陣列長度</returns>
    public int LongestSubarray(int[] nums)
    {
        int maxVal = int.MinValue;
        int maxLength = 0;
        int currentLength = 0;

        // 第一次遍歷：找出陣列中的最大值 maxVal
        foreach (int num in nums)
        {
            maxVal = Math.Max(maxVal, num);
        }

        // 第二次遍歷：統計連續出現 maxVal 的最長長度
        foreach (int num in nums)
        {
            if (num == maxVal)
            {
                currentLength++; // 累加連續 maxVal 的長度
                maxLength = Math.Max(maxLength, currentLength); // 更新最大長度
            }
            else
            {
                currentLength = 0; // 遇到非 maxVal，重置計數
            }
        }

        return maxLength;
    }


    /// <summary>
    /// 單次遍歷做法，統計最大值連續段長度。
    /// </summary>
    /// <param name="nums">輸入整數陣列</param>
    /// <returns>最大 AND 值的最長子陣列長度</returns>
    /// <remarks>
    /// 1. 只需一次遍歷，動態追蹤最大值 maxVal 及其連續出現的最大長度 maxLength。
    /// 2. 每遇到比 maxVal 更大的值，重置計數；遇到等於 maxVal 的值，累加連續長度。
    /// 3. 時間複雜度 O(n)。
    /// </remarks>
    public int LongestSubarray2(int[] nums)
    {
        int maxLength = 0;
        int maxVal = 0;
        int currentLength = 0;
        foreach (int num in nums)
        {
            if (num > maxVal)
            {
                // 發現新的最大值，重置計數
                maxVal = num;
                currentLength = 1;
                maxLength = 1;
            }
            else if (num == maxVal)
            {
                currentLength++;
                maxLength = Math.Max(maxLength, currentLength);
            }
            else
            {
                currentLength = 0; // 連續最大值斷開，重新統計
            }
        }
        return maxLength;
    }
}
