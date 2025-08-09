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
    /// 判斷一個整數 n 是否為 2 的冪。
    /// 解題思路：
    /// 一個數 n 是 2 的冪，當且僅當 n 是正整數，且其二進位表示中僅有一個 1。
    /// 利用位運算 n & (n - 1) 可以將 n 的二進位最低位的 1 移除，
    /// 若結果為 0，代表 n 僅有一個 1，故為 2 的冪。
    /// 
    /// (n & (n - 1))
    /// n 二進位最低位為 1，其餘所有位為 0，
    /// n - 1 二進位最低位為 0，其餘所有位為 1。
    /// 兩者取 & 結果為 0
    /// </summary>
    /// <param name="n">待判斷的整數</param>
    /// <returns>若 n 為 2 的冪則回傳 true，否則回傳 false</returns>
    public bool IsPowerOfTwo(int n)
    {
        // 必須是正整數，否則不可能是 2 的冪
        // n & (n - 1) 會將 n 的二進位最低位 1 移除
        // 若結果為 0，代表 n 僅有一個 1，為 2 的冪
        return n > 0 && (n & (n - 1)) == 0;
    }


    /// <summary>
    /// 判斷一個整數 n 是否為 2 的冪（解法二：利用 n & -n 位運算技巧）。
    /// 解題思路：
    /// - n & -n 可以取得 n 二進位表示的最低位的 1。
    /// - 若 n 是 2 的冪，則其二進位僅有一個 1，且 n & -n == n。
    /// - 例如：n = 8 (1000)，-n = -8 (1000)，n & -n = 1000 == n。
    /// - 若 n 不是 2 的冪，則 n & -n 會取得最低位的 1，但不等於 n。
    /// - 利用此性質可判斷 n 是否為 2 的冪。
    /// <para>
    /// 原理說明：
    /// - 負數在電腦中以補數表示，-n = ~n + 1。
    /// - n 的二進位若為 (a10...0)，-n 則為 (a'10...0)，a' 為 a 取反。
    /// - n & -n 只保留最低位的 1，其餘為 0。
    /// </para>
    /// <param name="n">待判斷的整數</param>
    /// <returns>若 n 為 2 的冪則回傳 true，否則回傳 false</returns>
    public bool IsPowerOfTwo2(int n)
    {
        // 必須是正整數
        // n & -n 會取得 n 的最低位 1
        // 若 n 僅有一個 1，則 n & -n == n，為 2 的冪
        return n > 0 && (n & -n) == n;
    }
}
