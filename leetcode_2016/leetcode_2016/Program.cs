namespace leetcode_2016;

class Program
{
    /// <summary>
    /// 2016. Maximum Difference Between Increasing Elements
    /// https://leetcode.com/problems/maximum-difference-between-increasing-elements/description/?envType=daily-question&envId=2025-06-16
    /// 2016. 增量元素之間的最大差值
    /// https://leetcode.cn/problems/maximum-difference-between-increasing-elements/description/?envType=daily-question&envId=2025-06-16
    /// 給定一個 0 索引的整數陣列 nums，大小為 n，請找出 nums [i] 和 nums [j]（即 nums [j] - nums [i]）的最大差值，
    /// 其中 0 <= i < j < n 且 nums [i] < nums [j]。
    /// 回傳最大差值。如果不存在這樣的 i 和 j，則回傳 -1。
    /// </summary>
    /// <param name="args"></param> 
    static void Main (string [] args)
    {
        Program program = new Program ();
        // 多組測試資料
        int [][] testCases = new int [][]
        {
            new int [] {7, 1, 5, 4},      // 預期: 4
            new int [] {9, 4, 3, 2},      // 預期: -1
            new int [] {1, 5, 2, 10},     // 預期: 9
            new int [] {2, 3, 10, 2, 4, 8, 1}, // 預期: 8
            new int [] {1, 2},            // 預期: 1
            new int [] {5, 5, 5, 5},      // 預期: -1
            new int [] {1, 2, 3, 4, 5},   // 預期: 4
            new int [] {5, 4, 3, 2, 1},   // 預期: -1
        };
        for (int i = 0; i < testCases.Length; i++)
        {
            int [] nums = testCases [i];
            int res1 = program.MaximumDifference (nums);
            int res2 = program.MaximumDifference2 (nums);
            Console.WriteLine ($"測試資料 {i+1}: [{string.Join (", ", nums)}]");
            Console.WriteLine ($"解法 1 (暴力): {res1}");
            Console.WriteLine ($"解法 2 (優化): {res2}");
            Console.WriteLine ();
        }
    }


    /// <summary>
    /// 解題說明：
    /// 兩層 for 迴圈暴力搜尋所有 i < j 且 nums [i] < nums [j] 的組合，計算最大差值。
    /// 時間複雜度 O (n^2)。
    /// </summary>
    /// <param name="nums"> 整數陣列 </param>
    /// <returns > 最大差值，若不存在則回傳 -1</returns>
    public int MaximumDifference (int [] nums)
    {
        int n = nums.Length;
        int res = -1; // 初始化為 -1，若無解直接回傳
        // 外層 for 迴圈，枚舉 i
        for (int i = 0; i < n; i++)
        {
            // 內層 for 迴圈，枚舉 j > i
            for (int j = i + 1; j < n; j++)
            {
                // 只考慮 nums [j] > nums [i] 的情況
                if (nums [i] < nums [j])
                {
                    int diff = nums [j] - nums [i]; // 計算差值
                    res = Math.Max (res, diff);    // 更新最大差值
                }
            }
        }
        return res;
    }


    /// <summary>
    /// 使用前一個數字的方式來優化
    /// 解題說明：
    /// 只需維護目前為止遇到的最小值 preNum，對每個 nums [i]，若 nums [i] > preNum，計算差值並更新最大值。
    /// 若 nums [i] <= preNum，則更新 preNum。
    /// 時間複雜度 O (n)，空間複雜度 O (1)。
    /// </summary>
    /// <param name="nums"> 整數陣列 </param>
    /// <returns > 最大差值，若不存在則回傳 -1</returns>
    public int MaximumDifference2 (int [] nums)
    {
        int n = nums.Length;
        int res = -1;
        int preNum = nums [0]; // 初始化前一個數字為第一個元素
        for (int i = 1; i < n; i++)
        {
            // 如果當前數字大於前一個數字，計算差值
            if (nums [i] > preNum)
            {
                int diff = nums [i] - preNum; // 計算差值
                res = Math.Max (res, diff);   // 更新最大差值
            }
            else
            {
                // 如果當前數字小於等於前一個數字，更新前一個數字為當前數字
                preNum = nums [i];
            }
        }
        return res;
    }
}
