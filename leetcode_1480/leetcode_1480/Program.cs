namespace leetcode_1480;

class Program
{
    /// <summary>
    /// 1480. Running Sum of 1d Array
    /// https://leetcode.com/problems/running-sum-of-1d-array/
    /// 1480. 一維數組的動態和
    /// https://leetcode.cn/problems/running-sum-of-1d-array/description/
    /// 
    /// Given an array nums. We define a running sum of an array as runningSum[i] = sum(nums[0]…nums[i]).
    /// Return the running sum of nums.
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試範例 1: nums = [1,2,3,4]
        // 預期輸出: [1,3,6,10]
        // 說明: runningSum[0] = 1
        //       runningSum[1] = 1 + 2 = 3
        //       runningSum[2] = 1 + 2 + 3 = 6
        //       runningSum[3] = 1 + 2 + 3 + 4 = 10
        int[] nums1 = [1, 2, 3, 4];
        int[] result1 = solution.RunningSum(nums1);
        Console.WriteLine($"範例 1: [{string.Join(", ", result1)}]");

        // 測試範例 2: nums = [1,1,1,1,1]
        // 預期輸出: [1,2,3,4,5]
        // 說明: runningSum = [1, 1+1, 1+1+1, 1+1+1+1, 1+1+1+1+1]
        int[] nums2 = [1, 1, 1, 1, 1];
        int[] result2 = solution.RunningSum(nums2);
        Console.WriteLine($"範例 2: [{string.Join(", ", result2)}]");

        // 測試範例 3: nums = [3,1,2,10,1]
        // 預期輸出: [3,4,6,16,17]
        int[] nums3 = [3, 1, 2, 10, 1];
        int[] result3 = solution.RunningSum(nums3);
        Console.WriteLine($"範例 3: [{string.Join(", ", result3)}]");
    }

    /// <summary>
    /// 方法一：原地修改 (In-place Modification)
    /// 
    /// <para>
    /// <b>思路與算法：</b>
    /// </para>
    /// <para>
    /// 根據動態和的定義：runningSum[i] = ∑(nums[0]...nums[i])
    /// </para>
    /// <para>
    /// 可以推導出遞推公式：
    /// <list type="bullet">
    ///   <item>當 i = 0 時：runningSum[0] = nums[0]</item>
    ///   <item>當 i > 0 時：runningSum[i] = runningSum[i-1] + nums[i]</item>
    /// </list>
    /// </para>
    /// <para>
    /// 由於 runningSum[i-1] 就等於更新後的 nums[i-1]，
    /// 我們可以直接在原陣列上進行修改，從索引 1 開始遍歷，
    /// 讓 nums[i] = nums[i-1] + nums[i]，達到原地修改的效果。
    /// </para>
    /// <para>
    /// <b>時間複雜度：</b> O(n)，其中 n 是陣列長度，只需遍歷一次陣列。
    /// </para>
    /// <para>
    /// <b>空間複雜度：</b> O(1)，原地修改，不需要額外空間。
    /// </para>
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>動態和陣列，其中 result[i] = sum(nums[0]...nums[i])</returns>
    /// <example>
    /// <code>
    /// int[] nums = [1, 2, 3, 4];
    /// int[] result = RunningSum(nums);
    ///  result = [1, 3, 6, 10]
    /// </code>
    /// </example>
    public int[] RunningSum(int[] nums)
    {
        // 取得陣列長度
        int n = nums.Length;

        // 從索引 1 開始遍歷（索引 0 的動態和就是它本身）
        for (int i = 1; i < n; i++)
        {
            // 利用遞推公式：nums[i] = nums[i-1] + nums[i]
            // 此時 nums[i-1] 已經是 runningSum[i-1]
            nums[i] += nums[i - 1];
        }

        // 返回原地修改後的陣列
        return nums;
    }
}
