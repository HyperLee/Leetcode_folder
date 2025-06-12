namespace leetcode_3423;

class Program
{
    /// <summary>
    /// 3423. Maximum Difference Between Adjacent Elements in a Circular Array
    /// https://leetcode.com/problems/maximum-difference-between-adjacent-elements-in-a-circular-array/description/?envType=daily-question&envId=2025-06-12
    /// 3423. 循環陣列中相鄰元素的最大差值
    /// https://leetcode.cn/problems/maximum-difference-between-adjacent-elements-in-a-circular-array/description/?envType=daily-question&envId=2025-06-12
    ///
    /// 題目描述：
    /// 給定一個循環陣列 nums，請找出所有相鄰元素之間的最大絕對差值。
    /// 注意：在循環陣列中，第一個和最後一個元素也視為相鄰。
    ///
    /// Given a circular array nums, find the maximum absolute difference between adjacent elements.
    /// Note: In a circular array, the first and last elements are adjacent.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }


    /// <summary>
    /// 計算循環陣列中相鄰元素的最大絕對差值。
    /// 解題說明：
    /// 1. 依序比較每一對相鄰元素（nums[i] 與 nums[i-1]），計算其差值的絕對值。
    /// 2. 由於是循環陣列，還需比較首尾元素（nums[0] 與 nums[n-1]）的差值絕對值。
    /// 3. 回傳所有相鄰元素差值絕對值中的最大值。
    /// 
    /// 注意:題目是要求出差異最大值，所以取絕對值後再比較沒有問題。
    /// 不是要求出整數最大值。這是不同做法!!!!
    /// </summary>
    /// <param name="nums">整數陣列，代表循環陣列</param>
    /// <returns>最大相鄰元素差值的絕對值</returns>
    public int MaxAdjacentDistance(int[] nums)
    {
        int n = nums.Length;
        if (n < 2)
        {
            // 若陣列長度小於2，無法計算差值，直接回傳0
            return 0;
        }

        int res = 0; // 用來記錄目前最大差值
        // 比較每一對相鄰元素的差值絕對值
        for (int i = 1; i < n; i++)
        {
            int diff = Math.Abs(nums[i] - nums[i - 1]); // 計算相鄰元素的差值絕對值
            res = Math.Max(res, diff); // 更新最大差值
        }

        // 處理循環陣列的首尾元素
        int circularDiff = Math.Abs(nums[0] - nums[n - 1]); // 首尾元素的差值絕對值
        return Math.Max(res, circularDiff); // 回傳最大差值
    }
}
