namespace leetcode_1318;

/// <summary>
/// 1318. Minimum Flips to Make a OR b Equal to c
/// https://leetcode.com/problems/minimum-flips-to-make-a-or-b-equal-to-c/description/
/// 1318. 或運算的最小翻轉次數
/// https://leetcode.cn/problems/minimum-flips-to-make-a-or-b-equal-to-c/description/
///
/// [EN] Given three positive integers a, b and c, return the minimum number of bit flips
/// needed in a and b so that (a OR b) == c.
///
/// [繁中] 給定三個正整數 a、b 和 c，請回傳需要翻轉 a 與 b 的最少位元次數，
/// 使得 (a OR b) == c。
/// </summary>
class Program
{
    /// <summary>
    /// 執行題目範例與自訂測資，快速驗證 MinFlips 的結果。
    /// </summary>
    /// <param name="args">命令列參數。</param>
    static void Main(string[] args)
    {
        var solver = new Program();
        var testCases = new (int A, int B, int C, int Expected)[]
        {
            (2, 6, 5, 3),
            (4, 2, 7, 1),
            (1, 2, 3, 0),
            (8, 3, 5, 3)
        };

        Console.WriteLine("1318. Minimum Flips to Make a OR b Equal to c");
        Console.WriteLine();

        foreach (var (a, b, c, expected) in testCases)
        {
            var actual = solver.MinFlips(a, b, c);
            var result = actual == expected ? "PASS" : "FAIL";

            Console.WriteLine($"{result,-4} a = {a}, b = {b}, c = {c} -> flips = {actual}, expected = {expected}");
        }
    }

    /// <summary>
    /// 計算讓 a OR b 等於 c 所需的最少翻轉次數。
    /// 解法會同步掃描 a、b、c 的二進位表示，逐位判斷目前這一位是否符合 OR 規則。
    /// 若目前位元不符合條件，則依 c 的該位數值決定翻轉次數：
    /// cBit 為 1 時只需補出一個 1；cBit 為 0 時必須將該位上的所有 1 都翻成 0。
    /// </summary>
    /// <param name="a">第一個正整數。</param>
    /// <param name="b">第二個正整數。</param>
    /// <param name="c">目標正整數。</param>
    /// <returns>使 a OR b 等於 c 的最少翻轉次數。</returns>
    /// <remarks>
    /// 每輪處理最低位後，將三個整數都右移一位，直到全部變成 0 為止。
    /// 由於每個位元彼此獨立，因此總答案等於每一位最少翻轉次數的總和。
    /// 時間複雜度為 O(log M)，空間複雜度為 O(1)，其中 M 為 a、b、c 的最大值。
    /// </remarks>
    /// <example>
    /// <code>
    /// var solver = new Program();
    /// var flips = solver.MinFlips(2, 6, 5); // 3
    /// </code>
    /// </example>
    public int MinFlips(int a, int b, int c)
    {
        var flips = 0;

        // 逐位元檢查，直到三個數的所有有效位都處理完成。
        while (a > 0 || b > 0 || c > 0)
        {
            var aBit = a & 1;
            var bBit = b & 1;
            var cBit = c & 1;

            // 只有當前位的 OR 結果與目標位不同時，才需要翻轉。
            if ((aBit | bBit) != cBit)
            {
                if (cBit == 1)
                {
                    // 目標位是 1 時，只要把 a 或 b 其中一個 0 翻成 1 即可。
                    flips++;
                }
                else
                {
                    // 目標位是 0 時，該位上的所有 1 都必須翻成 0。
                    flips += aBit + bBit;
                }
            }

            a >>= 1;
            b >>= 1;
            c >>= 1;
        }

        return flips;
    }
}
