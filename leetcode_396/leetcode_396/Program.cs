namespace leetcode_396;

class Program
{
    /// <summary>
    /// 396. Rotate Function
    /// https://leetcode.com/problems/rotate-function/description/?envType=daily-question&envId=2026-05-01
    /// 
    /// English:
    /// Given an integer array nums of length n.
    /// 
    /// Assume arrk to be an array obtained by rotating nums by k positions clockwise.
    /// We define the rotation function F on nums as follows:
    /// F(k) = 0 * arrk[0] + 1 * arrk[1] + ... + (n - 1) * arrk[n - 1].
    /// Return the maximum value of F(0), F(1), ..., F(n - 1).
    /// The test cases are generated so that the answer fits in a 32-bit integer.
    /// 
    /// 396. 旋转函数
    /// https://leetcode.cn/problems/rotate-function/description/?envType=daily-question&envId=2026-05-01
    /// 
    /// 繁體中文：
    /// 給你一個長度為 n 的整數陣列 nums。
    /// 
    /// 假設 arrk 是將 nums 順時針旋轉 k 個位置後得到的陣列。
    /// 我們定義 nums 的旋轉函數 F 如下：
    /// F(k) = 0 * arrk[0] + 1 * arrk[1] + ... + (n - 1) * arrk[n - 1]。
    /// 請回傳 F(0)、F(1)、...、F(n - 1) 的最大值。
    /// 測試資料保證答案可放入 32 位元整數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 迭代
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MaxRotateFunction(int[] nums)
    {
        int f = 0;
        int n = nums.Length;
        int numSum = nums.Sum();

        for(int i = 0; i < n; i++)
        {
            f += i * nums[i];
        }

        int res = f;
        for(int i = n - 1; i > 0; i--)
        {
            f += numSum - n * nums[i];
            res = Math.Max(res, f);
        }
        return res;
    }
}
