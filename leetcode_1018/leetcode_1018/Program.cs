namespace leetcode_1018;

class Program
{
    /// <summary>
    /// 1018. Binary Prefix Divisible By 5
    /// https://leetcode.com/problems/binary-prefix-divisible-by-5/description/?envType=daily-question&envId=2025-11-24
    /// 1018. 可被 5 整除的二进制前缀
    /// https://leetcode.cn/problems/binary-prefix-divisible-by-5/description/?envType=daily-question&envId=2025-11-24
    ///
    /// Problem (EN):
    /// You are given a binary array nums (0-indexed).
    /// We define x_i as the number whose binary representation is the subarray nums[0..i]
    /// (from most-significant-bit to least-significant-bit).
    /// Return an array of booleans answer where answer[i] is true if x_i is divisible by 5.
    ///
    /// 題目（中文翻譯）:
    /// 給定一個二進位陣列 nums（0-indexed）。
    /// 我們定義 x_i 為一個數字，它的二進位表示為子陣列 nums[0..i]（從最高位到最低位）。
    /// 回傳布林陣列 answer，使得 answer[i] 為 true 當且僅當 x_i 能被 5 整除。
    ///
    /// 範例：
    /// - nums = [1,0,1] => x0=1, x1=2, x2=5 => answer = [false, false, true]
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
