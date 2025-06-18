namespace leetcode_2966;

class Program
{
    /// <summary>
    /// 2966. Divide Array Into Arrays With Max Difference
    /// https://leetcode.com/problems/divide-array-into-arrays-with-max-difference/description/?envType=daily-question&envId=2025-06-18
    /// 2966. 划分数组并满足最大差限制
    /// https://leetcode.cn/problems/divide-array-into-arrays-with-max-difference/description/?envType=daily-question&envId=2025-06-18
    /// 
    /// 題目描述:
    /// 給定一個整數陣列 nums（長度為 n，且 n 為 3 的倍數）與正整數 k。
    /// 請將 nums 分成 n/3 個長度為 3 的子陣列，且每個子陣列內任兩元素的差值皆小於等於 k。
    /// 若無法分割則回傳空陣列，若有多種答案可回傳任一種。
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
