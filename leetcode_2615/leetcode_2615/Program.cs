namespace leetcode_2615;

class Program
{
    /// <summary>
    /// 2615. Sum of Distances
    /// https://leetcode.com/problems/sum-of-distances/description/?envType=daily-question&envId=2026-04-23
    /// You are given a 0-indexed integer array nums. There exists an array arr of length nums.length, 
    /// where arr[i] is the sum of |i - j| over all j such that nums[j] == nums[i] and j != i. 
    /// If there is no such j, set arr[i] to be 0.
    /// Return the array arr.
    ///
    /// 2615. 等值距离和
    /// https://leetcode.cn/problems/sum-of-distances/description/?envType=daily-question&envId=2026-04-23
    /// 給定一個 0-indexed 整數陣列 nums。
    /// 存在一個長度為 nums.length 的陣列 arr，
    /// 其中 arr[i] 為所有滿足 nums[j] == nums[i] 且 j != i 的 j 之 |i - j| 總和。
    /// 若不存在此類 j，則 arr[i] = 0。
    /// 回傳陣列 arr。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1：官方範例一
        // 輸入：nums = [1,3,1,1,2]
        // 預期：[5,0,3,4,0]
        //   下標 0 的 1：|0-2|+|0-3| = 2+3 = 5
        //   下標 1 的 3：無其他 3，故為 0
        //   下標 2 的 1：|2-0|+|2-3| = 2+1 = 3
        //   下標 3 的 1：|3-0|+|3-2| = 3+1 = 4
        //   下標 4 的 2：無其他 2，故為 0
        int[] nums1 = { 1, 3, 1, 1, 2 };
        long[] res1 = program.Distance(nums1);
        Console.WriteLine($"測試 1：[{string.Join(",", res1)}]  (預期：[5,0,3,4,0])");

        // 測試案例 2：官方範例二
        // 輸入：nums = [0,5,3]
        // 預期：[0,0,0]（每個元素皆唯一）
        int[] nums2 = { 0, 5, 3 };
        long[] res2 = program.Distance(nums2);
        Console.WriteLine($"測試 2：[{string.Join(",", res2)}]  (預期：[0,0,0])");

        // 測試案例 3：全部相同
        // 輸入：nums = [7,7,7,7]
        // 下標 0：|0-1|+|0-2|+|0-3| = 1+2+3 = 6
        // 下標 1：|1-0|+|1-2|+|1-3| = 1+1+2 = 4
        // 下標 2：|2-0|+|2-1|+|2-3| = 2+1+1 = 4
        // 下標 3：|3-0|+|3-1|+|3-2| = 3+2+1 = 6
        int[] nums3 = { 7, 7, 7, 7 };
        long[] res3 = program.Distance(nums3);
        Console.WriteLine($"測試 3：[{string.Join(",", res3)}]  (預期：[6,4,4,6])");

        // 測試案例 4：單一元素
        int[] nums4 = { 42 };
        long[] res4 = program.Distance(nums4);
        Console.WriteLine($"測試 4：[{string.Join(",", res4)}]  (預期：[0])");
    }

    /// <summary>
    /// 方法一：分組 + 前綴和
    ///
    /// 解題思路：
    /// 對於每個下標 i，答案 res[i] = Σ|i - j|，其中 nums[j] == nums[i] 且 j ≠ i。
    /// 把相同值的下標收集到同一組後，組內下標天然遞增排序。
    /// 對排序後的組 a0 &lt; a1 &lt; ... &lt; a_{m-1}，設 S 為組內下標總和、
    /// P_i = a0 + a1 + ... + a_{i-1} 為前綴和，則可推得：
    ///   res[a_i] = (i * a_i - P_i) + ((S - P_i - a_i) - (m - i - 1) * a_i)
    ///            = S - 2 * P_i + a_i * (2i - m)
    /// 每組只需一次線性掃描維護前綴和，總複雜度 O(n)。
    ///
    /// 時間複雜度：O(n)
    /// 空間複雜度：O(n)（雜湊表與結果陣列）
    /// </summary>
    /// <param name="nums">輸入整數陣列</param>
    /// <returns>長度等於 nums 的結果陣列 arr</returns>
    public long[] Distance(int[] nums)
    {
        int n = nums.Length;

        // 依數值將下標分組，同組內的下標會自然遞增（因為由小到大掃描）
        Dictionary<int, List<int>> groups = new Dictionary<int, List<int>>();
        for(int i = 0; i < n; i++)
        {
            if(!groups.ContainsKey(nums[i]))
            {
                groups[nums[i]] = new List<int>();
            }
            groups[nums[i]].Add(i);
        }

        long[] res = new long[n];

        // 逐組處理，對每個組套用「全組下標和 S − 2 × 前綴和 P_i + a_i × (2i − m)」公式
        foreach(var group in groups.Values)
        {
            // total = S：組內所有下標之和
            long total = 0;
            foreach(int idx in group)
            {
                total += idx;
            }

            // prefixTotal = P_i：當下標 a_i 之前的下標前綴和（a_0 + ... + a_{i-1}）
            long prefixTotal = 0;
            int sz = group.Count; // m：組大小
            for(int i = 0; i < sz; i++)
            {
                int idx = group[i]; // a_i
                // res[a_i] = S - 2 * P_i + a_i * (2i - m)
                res[idx] = total - prefixTotal * 2 + (long)idx * (2 * i - sz);
                // 更新前綴和，供下一輪使用
                prefixTotal += idx;
            }
        }

        return res;
    }
}
