namespace leetcode_3190;

class Program
{
    /// <summary>
    /// 3190. Find Minimum Operations to Make All Elements Divisible by Three
    /// https://leetcode.com/problems/find-minimum-operations-to-make-all-elements-divisible-by-three/description/?envType=daily-question&envId=2025-11-22
    /// 3190. 使所有元素都可以被 3 整除的最少操作数
    /// https://leetcode.cn/problems/find-minimum-operations-to-make-all-elements-divisible-by-three/description/?envType=daily-question&envId=2025-11-22
    ///  
    /// Given an integer array nums. In one operation, you can add or subtract 1 from any element of nums.
    /// Return the minimum number of operations to make all elements of nums divisible by 3.
    ///
    /// 給定一個整數陣列 nums。一次操作可以將任一元素加 1 或減 1。
    /// 返回使所有元素都可以被 3 整除所需的最少操作次數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試案例 1: [1,2,3,4]
        int[] nums1 = new int[] { 1, 2, 3, 4 };
        Console.WriteLine($"測試案例 1: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"結果: {program.MinimumOperations(nums1)}");
        Console.WriteLine();
        
        // 測試案例 2: [3,6,9]
        int[] nums2 = new int[] { 3, 6, 9 };
        Console.WriteLine($"測試案例 2: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"結果: {program.MinimumOperations(nums2)}");
        Console.WriteLine();
        
        // 測試案例 3: [1,2,4,5,7,8]
        int[] nums3 = new int[] { 1, 2, 4, 5, 7, 8 };
        Console.WriteLine($"測試案例 3: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"結果: {program.MinimumOperations(nums3)}");
    }

    /// <summary>
    /// 解題思路:
    /// 遍歷 nums，按照元素模 3 的餘數分類:
    /// - 如果 nums[i] = 3k，無需操作
    /// - 如果 nums[i] = 3k+1，減一得到 3 的倍數
    /// - 如果 nums[i] = 3k+2，加一得到 3 的倍數
    /// 
    /// 由此可見，對於不是 3 的倍數的元素，只需操作一次就可以變成 3 的倍數。
    /// 所以答案為不是 3 的倍數的元素個數。
    /// 
    /// 時間複雜度: O(n)，其中 n 是陣列長度
    /// 空間複雜度: O(1)
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>使所有元素都可以被 3 整除所需的最少操作次數</returns>
    public int MinimumOperations(int[] nums)
    {
        int res = 0;
        
        // 遍歷陣列中的每個元素
        foreach(int num in nums)
        {
            // 如果元素不能被 3 整除，則需要一次操作
            // num % 3 == 1 時，減 1 變成 3 的倍數
            // num % 3 == 2 時，加 1 變成 3 的倍數
            res += num % 3 != 0 ? 1 : 0;
        }
        
        return res;
    }
}
