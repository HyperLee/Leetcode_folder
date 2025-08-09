namespace leetcode_231;

class Program
{
    /// <summary>
    /// 231. Power of Two
    /// https://leetcode.com/problems/power-of-two/description/?envType=daily-question&envId=2025-08-09
    /// 231. 2 的幂
    /// https://leetcode.cn/problems/power-of-two/description/?envType=daily-question&envId=2025-08-09
    /// 
    /// 給定一個整數 n，如果它是 2 的冪，則回傳 true；否則回傳 false。
    /// 
    /// 如果存在一個整數 x 使得 n == 2^x，則整數 n 是 2 的冪。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] testCases = { 1, 2, 3, 4, 8, 16, 31, 64, 1024, 0, -2, -8 };
        var program = new Program();
        foreach (var n in testCases)
        {
            bool result = program.IsPowerOfTwo(n);
            Console.WriteLine($"n = {n}, IsPowerOfTwo = {result}");
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public bool IsPowerOfTwo(int n)
    {
        return n > 0 && (n & (n - 1)) == 0;
    }
}
