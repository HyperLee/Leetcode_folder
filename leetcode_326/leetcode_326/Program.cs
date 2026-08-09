namespace leetcode_326;

class Program
{
    /// <summary>
    /// 326. Power of Three
    /// https://leetcode.com/problems/power-of-three/description/
    /// <para>
    /// Given an integer n, return true if it is a power of three; otherwise, return false.
    ///
    /// An integer n is a power of three if there is an integer x such that n == 3^x.
    ///
    /// Example 1:
    /// Input: n = 27
    /// Output: true
    /// Explanation: 27 = 3^3.
    ///
    /// Example 2:
    /// Input: n = 0
    /// Output: false
    /// Explanation: There is no x such that 3^x = 0.
    ///
    /// Example 3:
    /// Input: n = -1
    /// Output: false
    /// Explanation: There is no x such that 3^x = -1.
    ///
    /// Constraints:
    /// - -2^31 &lt;= n &lt;= 2^31 - 1
    ///
    /// Follow-up: Could you solve it without loops or recursion?
    /// </para>
    /// <para>
    /// 326. 3 的冪
    /// https://leetcode.cn/problems/power-of-three/description/
    ///
    /// 給定整數 n，若它是 3 的冪則回傳 true，否則回傳 false。
    ///
    /// 若存在整數 x，使 n == 3^x，則整數 n 是 3 的冪。
    ///
    /// 範例 1：
    /// 輸入：n = 27
    /// 輸出：true
    /// 解釋：27 = 3^3。
    ///
    /// 範例 2：
    /// 輸入：n = 0
    /// 輸出：false
    /// 解釋：不存在 x 使 3^x = 0。
    ///
    /// 範例 3：
    /// 輸入：n = -1
    /// 輸出：false
    /// 解釋：不存在 x 使 3^x = -1。
    ///
    /// 限制條件：
    /// - -2^31 &lt;= n &lt;= 2^31 - 1
    ///
    /// 進階：你能不用迴圈或遞迴來解題嗎？
    /// </para>
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
