using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace leetcode_1680;

class Program
{
    /// <summary>
    /// <para>
    /// 1680. Concatenation of Consecutive Binary Numbers
    /// https://leetcode.com/problems/concatenation-of-consecutive-binary-numbers/description/
    ///
    /// Given an integer n, return the decimal value of the binary string formed by concatenating the binary representations
    /// of 1 through n in order, modulo 10^9 + 7.
    ///
    /// Example 1:
    /// Input: n = 1
    /// Output: 1
    /// Explanation: "1" in binary corresponds to decimal 1.
    ///
    /// Example 2:
    /// Input: n = 3
    /// Output: 27
    /// Explanation: 1, 2, and 3 are "1", "10", and "11" in binary. Concatenating gives "11011", decimal 27.
    ///
    /// Example 3:
    /// Input: n = 12
    /// Output: 505379714
    /// Explanation: Concatenation gives "1101110010111011110001001101010111100", whose decimal value is 118505380540.
    /// After modulo 10^9 + 7, the result is 505379714.
    ///
    /// Constraints:
    /// - 1 &lt;= n &lt;= 10^5
    /// </para>
    /// <para>
    /// 1680. 連接連續二進位數字
    /// https://leetcode.cn/problems/concatenation-of-consecutive-binary-numbers/description/
    ///
    /// 給定整數 n，依序連接 1 到 n 的二進位表示，形成一個二進位字串；回傳其十進位值對 10^9 + 7
    /// 取模後的結果。
    ///
    /// 範例 1：
    /// 輸入：n = 1
    /// 輸出：1
    /// 解釋：二進位的 "1" 對應十進位 1。
    ///
    /// 範例 2：
    /// 輸入：n = 3
    /// 輸出：27
    /// 解釋：1、2、3 的二進位表示分別為 "1"、"10"、"11"；連接後為 "11011"，十進位值為 27。
    ///
    /// 範例 3：
    /// 輸入：n = 12
    /// 輸出：505379714
    /// 解釋：連接結果為 "1101110010111011110001001101010111100"，十進位值是 118505380540。
    /// 對 10^9 + 7 取模後，結果為 505379714。
    ///
    /// 限制條件：
    /// - 1 &lt;= n &lt;= 10^5
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 建立解題物件
        var solution = new Program();

        // 測試資料
        // n = 1  -> 1   (二進位串: "1")
        // n = 3  -> 27  (二進位串: "1" + "10" + "11" = "11011")
        // n = 12 -> 505379714
        int[] testCases = { 1, 3, 12 };
        int[] expected   = { 1, 27, 505379714 };

        Console.WriteLine("=== 解法一：BitOperations 暴力法 ===");
        for (int i = 0; i < testCases.Length; i++)
        {
            int result = solution.ConcatenatedBinary(testCases[i]);
            string pass = result == expected[i] ? "PASS" : "FAIL";
            Console.WriteLine($"n={testCases[i]:D2}  result={result,12}  [{pass}]");
        }

        Console.WriteLine();
        Console.WriteLine("=== 解法二：位元計數最佳化法 ===");
        for (int i = 0; i < testCases.Length; i++)
        {
            int result = solution.ConcatenatedBinary2(testCases[i]);
            string pass = result == expected[i] ? "PASS" : "FAIL";
            Console.WriteLine($"n={testCases[i]:D2}  result={result,12}  [{pass}]");
        }
    }

    /// <summary>
    /// 解法一：暴力左移法（BitOperations）
    ///
    /// 解題思路：
    /// 將 1 到 n 的二進位字串依序串接，等同於每次把目前累積結果
    /// 往左移動「本次數字 i 的二進位位元長度 w」位，再加上 i。
    ///
    /// 位元長度計算：
    ///   利用 BitOperations.LeadingZeroCount 可在 O(1) 取得
    ///   整數 i 的有效位元長度 w = 32 - LeadingZeroCount(i)。
    ///
    /// 防溢位處理：
    ///   n 最大為 10^5，w 最大約 17，左移後數值可能超過 int 範圍，
    ///   因此 res 宣告為 long，每步對 MOD (10^9+7) 取模。
    ///
    /// 時間複雜度：O(n)    空間複雜度：O(1)
    /// </summary>
    /// <param name="n">正整數 n，範圍 1 <= n <= 10^5</param>
    /// <returns>串接後的二進位字串對應的十進位值，對 10^9+7 取模</returns>
    /// <example>
    /// <code>
    /// n = 3 -> 串接 "1"+"10"+"11" = "11011" = 27
    /// int result = ConcatenatedBinary(3); // 27
    /// </code>
    /// </example>
    public int ConcatenatedBinary(int n)
    {
        const int MOD = 1_000_000_007;
        long res = 0;
        for (int i = 1; i <= n; i++)
        {
            // 計算 i 的二進位有效位元長度 w
            // 例：i=5 (101)，LeadingZeroCount=29，w=3
            int w = 32 - BitOperations.LeadingZeroCount((uint)i);

            // 將目前結果左移 w 位（騰出空間），再接上 i，並取模防溢位
            res = ((res << w) + i) % MOD;
        }

        return (int)res;
    }

    /// <summary>
    /// 解法二：位元計數遞增最佳化法
    ///
    /// 解題思路：
    /// 核心觀察：當且僅當 i 恰好是 2 的冪次時（i & (i-1) == 0），
    /// i 的二進位位元長度才比 i-1 多 1。
    /// 因此，可以用計數器 bits 追蹤當前數字的位元長度，
    /// 只有在遇到 2 的冪次時才遞增，無需每步重新計算。
    ///
    /// 演算法步驟：
    ///   1. bits = 0，res = 0
    ///   2. 遍歷 i = 1..n：
    ///      a. 若 i & (i-1) == 0 -> bits++（位元長度進位）
    ///      b. res = (res << bits + i) % MOD
    ///
    /// 與解法一相比，改以 O(1) 條件判斷維護 bits，避免每次呼叫 LeadingZeroCount，常數更小。
    ///
    /// 時間複雜度：O(n)    空間複雜度：O(1)
    /// </summary>
    /// <param name="n">正整數 n，範圍 1 <= n <= 10^5</param>
    /// <returns>串接後的二進位字串對應的十進位值，對 10^9+7 取模</returns>
    /// <example>
    /// <code>
    /// n = 3 -> 串接 "1"+"10"+"11" = "11011" = 27
    /// i=1: 1=2^0 -> bits=1, res=(0 << 1)+1=1
    /// i=2: 2=2^1 -> bits=2, res=(1 << 2)+2=6
    /// i=3: 非2冪  -> bits=2, res=(6 << 2)+3=27
    /// int result = ConcatenatedBinary2(3); // 27
    /// </code>
    /// </example>
    public int ConcatenatedBinary2(int n)
    {
        const int MOD = 1_000_000_007;
        long res = 0;
        int bits = 0;  // 目前遍歷到的整數 i 的二進位位元長度
        for (int i = 1; i <= n; i++)
        {
            // 當 i 是 2 的冪次時（例如 1,2,4,8,16,...），位元長度恰好進 1
            // i & (i-1) == 0 是判斷 2 的冪次的經典位元技巧
            if ((i & (i - 1)) == 0)
            {
                bits++;
            }

            // 將累積結果左移 bits 位後加上 i，並取模
            res = ((res << bits) + i) % MOD;
        }

        return (int)res;
    }
}
