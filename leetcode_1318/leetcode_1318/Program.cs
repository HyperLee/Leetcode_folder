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
    /// 執行題目範例與自訂測資，快速驗證兩種解法的結果。
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
            var actualMethod1 = solver.MinFlips(a, b, c);
            var actualMethod2 = solver.MinFlips2(a, b, c);
            var resultMethod1 = actualMethod1 == expected ? "PASS" : "FAIL";
            var resultMethod2 = actualMethod2 == expected ? "PASS" : "FAIL";

            Console.WriteLine($"Input    a = {a}, b = {b}, c = {c}, expected = {expected}");
            Console.WriteLine($"Method 1 {resultMethod1,-4} flips = {actualMethod1}");
            Console.WriteLine($"Method 2 {resultMethod2,-4} flips = {actualMethod2}");
            Console.WriteLine();
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

    /// <summary>
    /// 計算讓 a OR b 等於 c 所需的最少翻轉次數。
    /// 解法固定枚舉每一個可能的二進位位元，並以位移搭配遮罩直接讀取該位的值。
    /// 若目標位 cBit 為 0，則 aBit 與 bBit 都必須為 0；若目標位 cBit 為 1，則 aBit 與 bBit 至少要有一個為 1。
    /// </summary>
    /// <param name="a">第一個正整數。</param>
    /// <param name="b">第二個正整數。</param>
    /// <param name="c">目標正整數。</param>
    /// <returns>使 a OR b 等於 c 的最少翻轉次數。</returns>
    /// <remarks>
    /// 題目條件保證 a、b、c 都小於 10^9，因此只需要檢查第 0 位到第 30 位。
    /// 這個版本使用固定次數的 for 迴圈完成枚舉，時間複雜度為 O(31)，可視為 O(1)，空間複雜度為 O(1)。
    /// </remarks>
    /// <example>
    /// <code>
    /// var solver = new Program();
    /// var flips = solver.MinFlips2(2, 6, 5); // 3
    /// </code>
    /// </example>
    public int MinFlips2(int a, int b, int c)
    {
        var flips = 0;

        // 題目範圍下，檢查第 0 位到第 30 位就足以覆蓋所有有效位元。
        for (var bitIndex = 0; bitIndex < 31; bitIndex++)
        {
            var aBit = (a >> bitIndex) & 1;
            var bBit = (b >> bitIndex) & 1;
            var cBit = (c >> bitIndex) & 1;

            if (cBit == 0)
            {
                // 目標位是 0 時，當前位上有幾個 1，就必須翻幾次。
                flips += aBit + bBit;
            }
            else
            {
                // 目標位是 1 時，只有兩個輸入位都為 0 才需要補一次翻轉。
                flips += (aBit + bBit == 0) ? 1 : 0;
            }
        }

        return flips;
    }
}
