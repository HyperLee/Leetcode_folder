namespace leetcode_3201;

class Program
{
    /// <summary>
    /// 3201. Find the Maximum Length of Valid Subsequence I
    /// https://leetcode.com/problems/find-the-maximum-length-of-valid-subsequence-i/description/
    /// 3201. 找出有效子序列的最大长度 I
    /// https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/description/?envType=daily-question&envId=2025-07-16
    /// 
    /// 給定一個整數陣列 nums。
    /// 
    /// 有一個長度為 x 的 nums 子序列被稱為有效，若滿足：
    /// (sub[0] + sub[1]) % 2 == (sub[1] + sub[2]) % 2 == ... == (sub[x - 2] + sub[x - 1]) % 2。
    /// 
    /// 請回傳 nums 最長有效子序列的長度。
    /// 
    /// 子序列是可以從原陣列刪除部分元素（或不刪除）且不改變剩餘元素順序所得到的陣列。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 回傳 nums 最長有效子序列的長度。
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>最長有效子序列長度</returns>
    public int MaximumLength(int[] nums)
    {
        // 根據題意，枚舉起始奇偶性，遍歷 nums，找出最長有效子序列長度
        if (nums is null || nums.Length == 0)
        {
            return 0;
        }
        int maxLen = 0;
        for (int parity = 0; parity <= 1; parity++)
        {
            int len = 1;
            int last = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (((last + nums[i]) % 2) == parity)
                {
                    len++;
                    last = nums[i];
                }
            }
            maxLen = Math.Max(maxLen, len);
        }
        // 也要考慮從每個起點開始的情況
        for (int parity = 0; parity <= 1; parity++)
        {
            for (int start = 0; start < nums.Length; start++)
            {
                int len = 1;
                int last = nums[start];
                for (int i = start + 1; i < nums.Length; i++)
                {
                    if (((last + nums[i]) % 2) == parity)
                    {
                        len++;
                        last = nums[i];
                    }
                }
                maxLen = Math.Max(maxLen, len);
            }
        }
        return maxLen;
    }
}
