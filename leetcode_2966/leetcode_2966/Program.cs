namespace leetcode_2966;

class Program
{
    /// <summary>
    /// 2966. Divide Array Into Arrays With Max Difference
    /// https://leetcode.com/problems/divide-array-into-arrays-with-max-difference/description/
    /// <para>
    /// You are given an integer array nums of size n, where n is a multiple of 3, and a positive integer k.
    ///
    /// Divide nums into n / 3 arrays of size 3 so that the difference between any two elements in one array is at most k.
    ///
    /// Return a 2D array containing the arrays. If the conditions cannot be satisfied, return an empty array. If there are multiple answers, return any of them.
    ///
    /// Example 1:
    /// Input: nums = [1,3,4,8,7,9,3,5,1], k = 2
    /// Output: [[1,1,3],[3,4,5],[7,8,9]]
    /// Explanation: The difference between any two elements in each array is at most 2.
    ///
    /// Example 2:
    /// Input: nums = [2,4,2,2,5,2], k = 2
    /// Output: []
    /// Explanation: Possible divisions include [[2,2,2],[2,4,5]] and [[2,2,4],[2,2,5]], with their permutations. Because there are four 2s, some group contains 2 and 5 regardless of the division. Since 5 - 2 = 3 &gt; k, no valid division exists.
    ///
    /// Example 3:
    /// Input: nums = [4,2,9,8,2,12,7,12,10,5,8,5,5,7,9,2,5,11], k = 14
    /// Output: [[2,2,2],[4,5,5],[5,5,7],[7,8,8],[9,9,10],[11,12,12]]
    /// Explanation: The difference between any two elements in each array is at most 14.
    ///
    /// Constraints:
    /// - n == nums.length
    /// - 1 &lt;= n &lt;= 10^5
    /// - n is a multiple of 3.
    /// - 1 &lt;= nums[i] &lt;= 10^5
    /// - 1 &lt;= k &lt;= 10^5
    /// </para>
    /// <para>
    /// 2966. 將陣列分組並限制最大差值
    /// https://leetcode.cn/problems/divide-array-into-arrays-with-max-difference/description/
    ///
    /// 給定一個長度為 n 的整數陣列 nums，其中 n 是 3 的倍數，另給定正整數 k。
    ///
    /// 將 nums 分成 n / 3 個長度為 3 的陣列，使每個陣列中任意兩元素之差不超過 k。
    ///
    /// 回傳包含這些陣列的 2D 陣列。若無法滿足條件，回傳空陣列；若有多個答案，可回傳任一答案。
    ///
    /// 範例 1：
    /// 輸入：nums = [1,3,4,8,7,9,3,5,1], k = 2
    /// 輸出：[[1,1,3],[3,4,5],[7,8,9]]
    /// 解釋：每個陣列中任意兩元素之差都不超過 2。
    ///
    /// 範例 2：
    /// 輸入：nums = [2,4,2,2,5,2], k = 2
    /// 輸出：[]
    /// 解釋：可能的分法包括 [[2,2,2],[2,4,5]] 與 [[2,2,4],[2,2,5]]，以及它們的排列。因為共有四個 2，無論如何分組，都會有一組同時包含 2 和 5。由於 5 - 2 = 3 &gt; k，因此不存在有效分法。
    ///
    /// 範例 3：
    /// 輸入：nums = [4,2,9,8,2,12,7,12,10,5,8,5,5,7,9,2,5,11], k = 14
    /// 輸出：[[2,2,2],[4,5,5],[5,5,7],[7,8,8],[9,9,10],[11,12,12]]
    /// 解釋：每個陣列中任意兩元素之差都不超過 14。
    ///
    /// 限制條件：
    /// - n == nums.length
    /// - 1 &lt;= n &lt;= 10^5
    /// - n 是 3 的倍數。
    /// - 1 &lt;= nums[i] &lt;= 10^5
    /// - 1 &lt;= k &lt;= 10^5
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main (string [] args)
    {
        var program = new Program ();
        // 測試 1：可正常分組
        int [] nums1 = { 1,3,4,8,7,9,3,5,1 };
        int k1 = 2;
        PrintResult (nums1, k1, program, "可正常分組");

        // 測試 2：無法分組（有一組差值超過 k）
        int [] nums2 = { 1, 10, 20, 2, 11, 21 };
        int k2 = 3;
        PrintResult (nums2, k2, program, "無法分組");

        // 測試 3：全部元素相同
        int [] nums3 = { 5, 5, 5, 5, 5, 5 };
        int k3 = 0;
        PrintResult (nums3, k3, program, "全部元素相同");

        // 測試 4：k 很大，必定可分組
        int [] nums4 = { 1, 100, 200, 2, 101, 201 };
        int k4 = 500;
        PrintResult (nums4, k4, program, "k 很大");
    }

    static void PrintResult (int [] nums, int k, Program program, string caseName)
    {
        var result = program.DivideArray (nums, k);
        Console.WriteLine ($"【{caseName}】nums=[{string.Join (",", nums)}], k={k}");
        if (result.Length == 0)
        {
            Console.WriteLine ("無法分組，回傳空陣列");
        }
        else
        {
            foreach (var arr in result)
            {
                Console.WriteLine (string.Join (",", arr));
            }
        }
        Console.WriteLine ();
    }


    /// <summary>
    /// 解題說明：
    /// 1. 先將 nums 排序，確保每 3 個連續元素的差值最小。
    /// 2. 每次取 3 個元素，檢查最大與最小的差是否小於等於 k。
    /// 3. 若有任一組不符合條件，直接回傳空陣列。
    /// 4. 全部分組都符合則回傳分組結果。
    /// 
    /// 時間複雜度：O (n log n)，主要來自排序。
    /// 空間複雜度：O (n)，用於儲存分組結果。
    /// </summary>
    /// <param name="nums"> 待分組的整數陣列，長度為 3 的倍數 </param>
    /// <param name="k"> 每組最大差值限制 </param>
    /// <returns > 分組後的二維陣列，或空陣列 </returns>
    public int [][] DivideArray (int [] nums, int k)
    {
        // 將陣列排序，方便分組
        Array.Sort (nums);
        int n = nums.Length;
        int [][] res = new int [n / 3][];

        // 以每 3 個元素為一組進行分組
        for (int i = 0; i < n; i += 3)
        {
            // 檢查這組的最大與最小差值是否超過 k
            if (nums [i + 2] - nums [i] > k)
            { 
                // 若超過，無法分組，回傳空陣列
                return new int [0][]; 
            }
            // 將這 3 個元素組成一組
            res [i / 3] = new int [] { nums [i], nums [i + 1], nums [i + 2] };
        }
        // 回傳所有分組結果
        return res;
    }
}
