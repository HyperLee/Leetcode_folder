namespace leetcode_976;

class Program
{
    /// <summary>
    /// 976. Largest Perimeter Triangle
    /// https://leetcode.com/problems/largest-perimeter-triangle/description/?envType=daily-question&envId=2025-09-28
    /// 976. 三角形的最大周长
    /// https://leetcode.cn/problems/largest-perimeter-triangle/description/?envType=daily-question&envId=2025-09-28
    /// 
    /// Given an integer array nums, return the largest perimeter of a triangle with a non-zero area, 
    /// formed from three of these lengths. If it is impossible to form any triangle of a non-zero area, return 0.
    /// 給定一個整數數組 nums，返回由這三個長度組成的非零面積三角形的最長周長。如果不可能形成任何非零面積的三角形，則返回 0。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1: 正常情況，能形成三角形
        int[] test1 = { 2, 1, 2 };
        Console.WriteLine($"測試案例 1: [{string.Join(", ", test1)}]");
        Console.WriteLine($"結果: {solution.LargestPerimeter(test1)}");
        Console.WriteLine($"預期: 5\n");
        
        // 測試案例 2: 無法形成三角形的情況
        int[] test2 = { 1, 2, 1 };
        Console.WriteLine($"測試案例 2: [{string.Join(", ", test2)}]");
        Console.WriteLine($"結果: {solution.LargestPerimeter(test2)}");
        Console.WriteLine($"預期: 0\n");
        
        // 測試案例 3: 複雜情況，多個可能的三角形
        int[] test3 = { 3, 2, 3, 4 };
        Console.WriteLine($"測試案例 3: [{string.Join(", ", test3)}]");
        Console.WriteLine($"結果: {solution.LargestPerimeter(test3)}");
        Console.WriteLine($"預期: 10\n");
        
        // 測試案例 4: 邊界情況，剛好不能形成三角形
        int[] test4 = { 1, 1, 10 };
        Console.WriteLine($"測試案例 4: [{string.Join(", ", test4)}]");
        Console.WriteLine($"結果: {solution.LargestPerimeter(test4)}");
        Console.WriteLine($"預期: 0\n");
        
        // 測試案例 5: 較大的數組
        int[] test5 = { 3, 6, 2, 3 };
        Console.WriteLine($"測試案例 5: [{string.Join(", ", test5)}]");
        Console.WriteLine($"結果: {solution.LargestPerimeter(test5)}");
        Console.WriteLine($"預期: 8");
    }

    /// <summary>
    /// 計算最大三角形周長的解法
    /// 
    /// 解題思路：
    /// 1. 將數組按升序排序，這樣可以方便檢查三角不等式
    /// 2. 從最大的三個數開始檢查，因為我們要找最大周長
    /// 3. 對於排序後的數組，只需檢查 nums[i-2] + nums[i-1] > nums[i] 即可
    ///    （因為其他兩個不等式在排序後必然成立）
    /// 4. 一旦找到滿足條件的三個數，立即返回其周長（貪心策略）
    /// 
    /// 三角形周長計算公式：
    /// 周長 = 邊長1 + 邊長2 + 邊長3
    /// 
    /// 三角不等式條件：
    /// 要形成一個非零面積的三角形，三個邊長必須滿足：
    /// - 邊長1 + 邊長2 > 邊長3
    /// - 邊長1 + 邊長3 > 邊長2  
    /// - 邊長2 + 邊長3 > 邊長1
    /// 
    /// 時間複雜度：O(n log n) - 主要來自排序
    /// 空間複雜度：O(1) - 只使用常數額外空間
    /// </summary>
    /// <param name="nums">包含邊長的整數數組</param>
    /// <returns>最大三角形周長，若無法形成三角形則返回 0</returns>
    public int LargestPerimeter(int[] nums)
    {
        // 步驟1: 將數組按升序排序，便於後續檢查三角不等式
        Array.Sort(nums);
        
        // 步驟2: 從最大的三個數開始向前檢查（貪心策略）
        // 由於排序後 nums[i] >= nums[i-1] >= nums[i-2]
        // 只需檢查最小的兩邊之和是否大於最大邊即可
        for (int i = nums.Length - 1; i >= 2; i--)
        {
            // 檢查三角不等式：兩小邊之和 > 最大邊
            if (nums[i - 2] + nums[i - 1] > nums[i])
            {
                // 找到第一組滿足條件的三邊，返回其周長
                return nums[i - 2] + nums[i - 1] + nums[i];
            }
        }
        
        // 若沒有找到滿足條件的三邊組合，返回 0
        return 0;
    }
}
