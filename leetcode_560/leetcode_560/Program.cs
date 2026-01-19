namespace leetcode_560;

class Program
{
    /// <summary>
    /// 560. Subarray Sum Equals K
    /// https://leetcode.com/problems/subarray-sum-equals-k/description/
    /// 560. 和為K 的子數組
    /// https://leetcode.cn/problems/subarray-sum-equals-k/description/
    /// 
    /// Given an array of integers nums and an integer k, return the total number of subarrays whose sum equals to k.
    /// A subarray is a contiguous non-empty sequence of elements within an array.
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1: nums = [1,1,1], k = 2
        int[] nums1 = [1, 1, 1];
        int k1 = 2;
        int result1 = solution.SubarraySum(nums1, k1);
        Console.WriteLine($"測試案例 1: nums = [1,1,1], k = 2, 結果 = {result1} (預期: 2)");
        
        // 測試案例 2: nums = [1,2,3], k = 3
        int[] nums2 = [1, 2, 3];
        int k2 = 3;
        int result2 = solution.SubarraySum(nums2, k2);
        Console.WriteLine($"測試案例 2: nums = [1,2,3], k = 3, 結果 = {result2} (預期: 2)");
        
        // 測試案例 3: nums = [1,-1,0], k = 0
        int[] nums3 = [1, -1, 0];
        int k3 = 0;
        int result3 = solution.SubarraySum(nums3, k3);
        Console.WriteLine($"測試案例 3: nums = [1,-1,0], k = 0, 結果 = {result3} (預期: 3)");
    }

    /// <summary>
    /// 方法一: 枚舉
    /// 
    /// 解題思路:
    /// 考慮以 i 結尾和為 k 的連續子數組個數，我們需要統計符合條件的下標 j 的個數，
    /// 其中 0 ≤ j ≤ i 且 [j..i] 這個子數組的和恰好為 k。
    /// 
    /// 核心概念:
    /// - 枚舉每個結尾位置 i
    /// - 對於每個結尾位置，反向枚舉起始位置 j (從 i 到 0)
    /// - 如果已知 [j, i] 的和，可以 O(1) 推出 [j-1, i] 的和
    /// - 只需在枚舉過程中累加元素，無需額外遍歷求和
    /// 
    /// 時間複雜度: O(n^2) - 兩層迴圈遍歷所有可能的子數組
    /// 空間複雜度: O(1) - 只使用常數額外空間
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <param name="k">目標和</param>
    /// <returns>和為 k 的連續子數組個數</returns>
    public int SubarraySum(int[] nums, int k)
    {
        int count = 0;
        
        // 枚舉每個結尾位置 i
        for(int start = 0; start < nums.Length; start++)
        {
            int sum = 0;
            
            // 從當前位置 start 反向枚舉起始位置 end
            // 這樣可以逐步累加元素，避免重複計算
            for(int end = start; end >= 0; end--)
            {
                // 將當前元素加入總和
                // 此時 sum 代表 [end, start] 區間的和
                sum += nums[end];
                
                // 如果區間和等於目標值 k，計數器加 1
                if(sum == k)
                {
                    count++;
                }
            }
        }
        
        return count;
    }
}
