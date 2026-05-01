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
        Program solution = new();
        (int[] Nums, int Expected)[] testCases =
        [
            (new[] { 4, 3, 2, 6 }, 26),
            (new[] { 100 }, 0),
            (new[] { 1, 2, 3, 4 }, 20),
            (new[] { -1, -2, -3 }, -5)
        ];

        Console.WriteLine("396. Rotate Function");
        Console.WriteLine();

        for (int i = 0; i < testCases.Length; i++)
        {
            int actual = solution.MaxRotateFunction(testCases[i].Nums);
            string numsText = string.Join(", ", testCases[i].Nums);

            Console.WriteLine($"Test Case {i + 1}");
            Console.WriteLine($"nums     = [{numsText}]");
            Console.WriteLine($"expected = {testCases[i].Expected}");
            Console.WriteLine($"actual   = {actual}");
            Console.WriteLine($"result   = {(actual == testCases[i].Expected ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 使用旋轉函數的遞推公式，以 O(n) 時間找出所有 F(k) 中的最大值。
    /// 先計算原始狀態的 F(0) 與陣列總和 numSum，
    /// 再利用 F(k) = F(k - 1) + numSum - n * nums[n - k]
    /// 逐步推出下一個旋轉結果，避免每次旋轉後都重新計算整個加權總和。
    /// </summary>
    /// <param name="nums">輸入整數陣列。</param>
    /// <returns>所有旋轉函數值中的最大值。</returns>
    public int MaxRotateFunction(int[] nums)
    {
        int n = nums.Length;
        int numSum = nums.Sum();
        int f = 0;

        // 先計算 F(0) = 0 * nums[0] + 1 * nums[1] + ... + (n - 1) * nums[n - 1]
        for (int i = 0; i < n; i++)
        {
            f += i * nums[i];
        }

        int res = f;

        // 由後往前套用遞推公式：
        // F(k) = F(k - 1) + numSum - n * nums[n - k]
        // 這樣就能在 O(1) 時間得到下一個旋轉函數值。
        for (int i = n - 1; i > 0; i--)
        {
            f += numSum - n * nums[i];
            res = Math.Max(res, f);
        }

        return res;
    }
}
