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
        for (int i = 0; i < testCases.Length; i++)
        {
            int result = program.MaxSum(testCases[i]);
            Console.WriteLine($"TestCase {i+1}: [{string.Join(", ", testCases[i])}] => MaxSum = {result}");
        }
    }


    /// <summary>
    /// 解題說明：
    /// 本方法針對「刪除後的最大子陣列元素和」問題，採用貪心策略。
    /// 1. 只要將所有正數（>0）去重後相加，即為最大和，因為正數越多和越大。
    /// 2. 若陣列中沒有正數，則返回陣列中的最大值（可能為 0 或負數）。
    /// 
    /// 時間複雜度：O(n)，空間複雜度：O(n)。
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>最大不重複子序列和</returns>
    public int MaxSum(int[] nums)
    {
        int n = nums.Length;
        // 用於存放所有出現過的正數，確保唯一性
        HashSet<int> set = new HashSet<int>();
        for (int i = 0; i < n; i++)
        {
            // 只考慮正數，並去重
            if (nums[i] > 0)
            {
                set.Add(nums[i]);
            }
        }

        // 若沒有任何正數，返回陣列最大值（可能為 0 或負數）
        // <= 0 都從這邊取出最大值
        if (set.Count == 0)
        {
            return nums.Max();
        }

        // 將所有唯一正數加總，得到最大和
        return set.Sum();
    }
}
