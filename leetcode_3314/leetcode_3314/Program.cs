namespace leetcode_3314;

class Program
{
    /// <summary>
    /// 3314. Construct the Minimum Bitwise Array I
    /// https://leetcode.com/problems/construct-the-minimum-bitwise-array-i/description/?envType=daily-question&envId=2026-01-20
    /// 3314. 构造最小位运算数组 I
    /// https://leetcode.cn/problems/construct-the-minimum-bitwise-array-i/description/?envType=daily-question&envId=2026-01-20
    /// 
    /// You are given an array nums consisting of n prime integers.
    /// You need to construct an array ans of length n, such that, for each index i, 
    /// the bitwise OR of ans[i] and ans[i] + 1 is equal to nums[i], 
    /// i.e. ans[i] OR (ans[i] + 1) == nums[i].
    /// Additionally, you must minimize each value of ans[i] in the resulting array.
    /// If it is not possible to find such a value for ans[i] that satisfies the condition, then set ans[i] = -1.
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1: nums = [2, 3, 5, 7]
        // 預期輸出: [-1, 1, 4, 3]
        List<int> test1 = [2, 3, 5, 7];
        Console.WriteLine($"測試 1: nums = [{string.Join(", ", test1)}]");
        var result1 = solution.MinBitwiseArray(test1);
        Console.WriteLine($"結果: [{string.Join(", ", result1)}]");
        Console.WriteLine();

        // 測試案例 2: nums = [11, 13, 31]
        // 預期輸出: [9, 12, 15]
        List<int> test2 = [11, 13, 31];
        Console.WriteLine($"測試 2: nums = [{string.Join(", ", test2)}]");
        var result2 = solution.MinBitwiseArray(test2);
        Console.WriteLine($"結果: [{string.Join(", ", result2)}]");
        Console.WriteLine();

        // 測試案例 3: 邊界情況 - 只包含 2 (質數，無解)
        List<int> test3 = [2];
        Console.WriteLine($"測試 3: nums = [{string.Join(", ", test3)}]");
        var result3 = solution.MinBitwiseArray(test3);
        Console.WriteLine($"結果: [{string.Join(", ", result3)}] (2 無解，返回 -1)");
    }

    /// <summary>
    /// LeetCode 3314: Construct the Minimum Bitwise Array I
    /// <para>
    /// 【問題描述】
    /// 給定一個整數陣列 nums，對於每個元素 x，找到最小的 ans 使得 ans | (ans + 1) = x。
    /// </para>
    /// <para>
    /// 【解題思路 - 位元運算】
    /// 1. 觀察 ans + 1 的位元特性：將 ans 從低位到高位第一個 0 變成 1，並將該 0 之前的所有低位 1 變為 0。
    /// 2. 因此 ans | (ans + 1) 的效果是：將 ans 從低位到高位第一個 0 變成 1。
    /// 3. 對於 x 二進制中從低位到高位第一個 0 之前的所有 1，任取一個變為 0 都可得到有效的 ans。
    /// 4. 為求最小 ans，只需找到 x 中第一個 0 的位置 pos，並將 pos-1 處的 1 變為 0。
    /// 5. 特例：當 x = 2 時無解（因為 2 的最低位 0 之前沒有更低位的 1），返回 -1。
    /// </para>
    /// </summary>
    /// <param name="nums">輸入的整數陣列，每個元素需滿足 ans | (ans + 1) = nums[i]</param>
    /// <returns>包含每個對應最小 ans 的陣列，無解時該位置為 -1</returns>
    /// <example>
    /// <code>
    ///  範例 1: nums = [2, 3, 5, 7]
    ///  輸出: [-1, 1, 4, 3]
    ///  解釋:
    ///   - 2: 無解 → -1
    ///   - 3 (0b11): 1 | 2 = 3 → ans = 1
    ///   - 5 (0b101): 4 | 5 = 5 → ans = 4
    ///   - 7 (0b111): 3 | 4 = 7 → ans = 3
    /// </code>
    /// </example>
    public int[] MinBitwiseArray(IList<int> nums)
    {
        for (int i = 0; i < nums.Count; i++)
        {
            int x = nums[i];
            int res = -1;  // 預設為 -1，表示無解
            int d = 1;     // d 用於檢測當前二進制位是否為 1

            // 從最低位開始，逐位檢查是否為 1
            // 當 (x & d) != 0 表示當前位為 1
            while ((x & d) != 0)
            {
                // 將當前位的 1 變為 0，得到候選答案
                // x - d 相當於把位置 d 的位元從 1 翻轉成 0
                res = x - d;

                // 左移 d，檢查下一個更高的位元
                d <<= 1;
            }

            // 迴圈結束時，res 為最後一個連續 1 位元變 0 的結果
            // 即 x 的第一個 0 位之前的最高位 1 被清除，這是最小的 ans
            nums[i] = res;
        }

        return nums.ToArray();
    }
}
