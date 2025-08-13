namespace leetcode_326;

class Program
{
    /// <summary>
    /// 326. Power of Three
    /// https://leetcode.com/problems/power-of-three/description/?envType=daily-question&envId=2025-08-13
    /// 326. 3 的冪
    /// https://leetcode.cn/problems/power-of-three/description/?envType=daily-question&envId=2025-08-13
    ///
    /// 給定一個整數 n，若 n 為 3 的冪則回傳 true，否則回傳 false。
    ///
    /// 一個整數 n 是 3 的冪，代表存在一個整數 x 使得 n == 3^x。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例測試
        int[] testCases = { 27, 0, 9, 45, 1, 3, 81, 10, -9 };
        foreach (var n in testCases)
        {
            Console.WriteLine($"n = {n}, IsPowerOfThree = {IsPowerOfThree(n)}");
        }
    }

    /// <summary>
    /// 判斷一個整數是否為 3 的冪。
    /// 方法一：試除法
    /// 思路：不斷將 n 除以 3，直到 n = 1。
    /// 如果 n 不能被 3 整除，則 n 不是 3 的冪。
    /// n 可以為負數或 0，這些情況直接回傳 false。
    /// </summary>
    /// <param name="n">待判斷的整數</param>
    /// <returns>若 n 為 3 的冪則回傳 true，否則回傳 false。</returns>
    public static bool IsPowerOfThree(int n)
    {
        // 只要 n 能被 3 整除就持續除以 3
        while (n != 0 && n % 3 == 0)
        {
            // 每次將 n 除以 3
            n /= 3;
        }
        // 最後 n 是否為 1，若是則代表 n 為 3 的冪
        return n == 1;
    }

}
