namespace leetcode_1470;

class Program
{
    /// <summary>
    /// 1470. Shuffle the Array
    /// https://leetcode.com/problems/shuffle-the-array/
    /// 1470. 重新排列數組
    /// https://leetcode.cn/problems/shuffle-the-array/description/
    /// 
    /// Given the array nums consisting of 2n elements in the form [x1,x2,...,xn,y1,y2,...,yn].
    /// Return the array in the form [x1,y1,x2,y2,...,xn,yn].
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== LeetCode 1470. Shuffle the Array ===\n");

        var solution = new Program();

        // 測試案例 1: nums = [2,5,1,3,4,7], n = 3
        // 預期輸出: [2,3,5,4,1,7]
        int[] nums1 = [2, 5, 1, 3, 4, 7];
        int n1 = 3;
        int[] result1 = solution.Shuffle(nums1, n1);
        Console.WriteLine($"測試案例 1:");
        Console.WriteLine($"  輸入: nums = [{string.Join(",", nums1)}], n = {n1}");
        Console.WriteLine($"  輸出: [{string.Join(",", result1)}]");
        Console.WriteLine($"  預期: [2,3,5,4,1,7]\n");

        // 測試案例 2: nums = [1,2,3,4,4,3,2,1], n = 4
        // 預期輸出: [1,4,2,3,3,2,4,1]
        int[] nums2 = [1, 2, 3, 4, 4, 3, 2, 1];
        int n2 = 4;
        int[] result2 = solution.Shuffle(nums2, n2);
        Console.WriteLine($"測試案例 2:");
        Console.WriteLine($"  輸入: nums = [{string.Join(",", nums2)}], n = {n2}");
        Console.WriteLine($"  輸出: [{string.Join(",", result2)}]");
        Console.WriteLine($"  預期: [1,4,2,3,3,2,4,1]\n");

        // 測試案例 3: nums = [1,1,2,2], n = 2
        // 預期輸出: [1,2,1,2]
        int[] nums3 = [1, 1, 2, 2];
        int n3 = 2;
        int[] result3 = solution.Shuffle(nums3, n3);
        Console.WriteLine($"測試案例 3:");
        Console.WriteLine($"  輸入: nums = [{string.Join(",", nums3)}], n = {n3}");
        Console.WriteLine($"  輸出: [{string.Join(",", result3)}]");
        Console.WriteLine($"  預期: [1,2,1,2]");
    }

    /// <summary>
    /// 重新排列陣列 - 一次遍歷法
    /// <para>
    /// 解題思路：
    /// 給定一個長度為 2n 的陣列 nums，其形式為 [x1,x2,...,xn,y1,y2,...,yn]，
    /// 需要將其重新排列為 [x1,y1,x2,y2,...,xn,yn] 的形式。
    /// </para>
    /// <para>
    /// 核心觀察：
    /// - 原陣列前半部 nums[0..n-1] 對應 x1,x2,...,xn
    /// - 原陣列後半部 nums[n..2n-1] 對應 y1,y2,...,yn
    /// - 結果陣列中，偶數索引放置 x，奇數索引放置 y
    /// </para>
    /// <para>
    /// 對應規則：
    /// - nums[i] → res[2*i] (將 xi 放到結果陣列的第 2i 個位置)
    /// - nums[i+n] → res[2*i+1] (將 yi 放到結果陣列的第 2i+1 個位置)
    /// </para>
    /// </summary>
    /// <param name="nums">包含 2n 個元素的原始陣列</param>
    /// <param name="n">陣列元素數量的一半</param>
    /// <returns>重新排列後的陣列</returns>
    /// <example>
    /// <code>
    ///  範例：nums = [2,5,1,3,4,7], n = 3
    ///  前半部 [2,5,1] 是 x1,x2,x3
    ///  後半部 [3,4,7] 是 y1,y2,y3
    ///  結果為 [2,3,5,4,1,7] 即 [x1,y1,x2,y2,x3,y3]
    /// var result = Shuffle(new int[] {2,5,1,3,4,7}, 3);
    /// result = [2,3,5,4,1,7]
    /// </code>
    /// </example>
    /// <remarks>
    /// 時間複雜度：O(n) - 只需遍歷一次陣列
    /// 空間複雜度：O(n) - 需要額外的結果陣列
    /// </remarks>
    public int[] Shuffle(int[] nums, int n)
    {
        // 建立長度為 2n 的結果陣列
        int[] res = new int[2 * n];

        // 遍歷索引 0 到 n-1，將元素交錯放置
        for (int i = 0; i < n; i++)
        {
            // 將原陣列前半部的元素 nums[i] (即 xi) 放到結果陣列的偶數索引位置
            res[2 * i] = nums[i];

            // 將原陣列後半部的元素 nums[i+n] (即 yi) 放到結果陣列的奇數索引位置
            res[2 * i + 1] = nums[i + n];
        }

        return res;
    }
}
