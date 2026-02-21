namespace leetcode_762;

class Program
{
    /// <summary>
    /// 762. Prime Number of Set Bits in Binary Representation
    /// https://leetcode.com/problems/prime-number-of-set-bits-in-binary-representation/description/?envType=daily-question&envId=2026-02-21
    /// 762. 二進位表示中質數個計算置位
    /// https://leetcode.cn/problems/prime-number-of-set-bits-in-binary-representation/description/?envType=daily-question&envId=2026-02-21
    ///
    /// 英文描述：
    /// Given two integers <c>left</c> and <c>right</c>, return the count of numbers in the inclusive range
    /// [left, right] having a prime number of set bits in their binary representation.
    /// Recall that the number of set bits an integer has is the number of 1's present when written in binary.
    /// For example, 21 written in binary is 10101, which has 3 set bits.
    ///
    /// 繁體中文描述：
    /// 給定兩個整數 <c>left</c> 和 <c>right</c>，返回在包含範圍 [left, right] 中，
    /// 二進位表示中置位數量為質數的整數個數。
    /// 回想一下，一個整數的置位數是其二進位表示中「1」的個數。
    /// 例如，21 的二進位為 10101，具有 3 個置位。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program p = new Program();

        // 測試案例 1：left=6, right=10 => 預期輸出 4
        // 6  (110)  -> 2 bits  -> 2 是質數 ✓
        // 7  (111)  -> 3 bits  -> 3 是質數 ✓
        // 8  (1000) -> 1 bit   -> 1 不是質數 ✗
        // 9  (1001) -> 2 bits  -> 2 是質數 ✓
        // 10 (1010) -> 2 bits  -> 2 是質數 ✓
        Console.WriteLine(p.CountPrimeSetBits(6, 10));   // 預期：4

        // 測試案例 2：left=10, right=15 => 預期輸出 5
        // 10 (1010) -> 2 bits -> 質數 ✓
        // 11 (1011) -> 3 bits -> 質數 ✓
        // 12 (1100) -> 2 bits -> 質數 ✓
        // 13 (1101) -> 3 bits -> 質數 ✓
        // 14 (1110) -> 3 bits -> 質數 ✓
        // 15 (1111) -> 4 bits -> 4 不是質數 ✗
        Console.WriteLine(p.CountPrimeSetBits(10, 15));  // 預期：5

        // 方法二測試：位元遮罩快取
        // 測試案例 1：left=6, right=10 => 預期輸出 4
        Console.WriteLine(p.CountPrimeSetBits2(6, 10));   // 預期：4

        // 測試案例 2：left=10, right=15 => 預期輸出 5
        Console.WriteLine(p.CountPrimeSetBits2(10, 15));  // 預期：5
    }

    /// <summary>
    /// 方法一：列舉 + 位元計算 + 質數判斷
    ///
    /// 解題思路：
    /// 枚舉 [left, right] 範圍內的每個整數 x，依序執行：
    ///   1. 使用 <see cref="BitCount"/> 計算 x 的二進位中「1」的個數（置位數）。
    ///   2. 使用 <see cref="IsPrime"/> 判斷該置位數是否為質數。
    ///   3. 若是質數，累加計數器。
    ///
    /// 由於 left, right ≤ 10^6 < 2^20，置位數最多 20，
    /// 需要判斷的質數範圍極小，每次呼叫 IsPrime 成本極低。
    ///
    /// <example>
    /// <code>
    /// CountPrimeSetBits(6, 10) // returns 4
    /// CountPrimeSetBits(10, 15) // returns 5
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="left">範圍左端點（含）。</param>
    /// <param name="right">範圍右端點（含）。</param>
    /// <returns>滿足條件的整數個數。</returns>
    public int CountPrimeSetBits(int left, int right)
    {
        int res = 0;

        // 逐一枚舉範圍內每個整數
        for(int x = left; x <= right; x++)
        {
            // 若 x 的置位數（二進位中 1 的個數）為質數，即計入結果
            if(IsPrime(BitCount(x)))
            {
                res++;
            }
        }

        return res;
    }

    /// <summary>
    /// 試除法判斷質數（Trial Division）。
    ///
    /// 解題說明：
    /// 質數定義：大於 1 且只能被 1 和自身整除的整數。
    /// - 0、1 不是質數，直接返回 false。
    /// - 對 x 從 2 開始試除到 √x，若有因數則非質數。
    /// - 只需枚舉到 √x 是因為：若 x = a × b 且 a ≤ b，則 a ≤ √x。
    ///
    /// 由於置位數最大為 20，此函式最多執行 √20 ≈ 4 次迴圈，效能極佳。
    ///
    /// 參考：LeetCode 204. Count Primes
    /// </summary>
    /// <param name="x">待判斷的整數（通常為置位數）。</param>
    /// <returns>若 x 為質數則 true，否則 false。</returns>
    private bool IsPrime(int x)
    {
        // 0 和 1 不是質數
        if(x < 2)
        {
            return false;
        }

        // 試除到 √x，若找到因數則非質數
        for(int i = 2; i * i <= x; i++)
        {
            if(x % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Hamming Weight（位元計數）：計算 32 位元整數中「1」的位元數（置位數）。
    ///
    /// 演算法說明（SWAR — SIMD Within A Register）：
    /// 此為經典的 Population Count（popcount）位元平行加法演算法，步驟如下：
    ///
    ///   Step 1：每 2 位元為一組，計算該組中 1 的個數。
    ///           使用遮罩 0x55555555（...01010101）和位移運算實現。
    ///
    ///   Step 2：每 4 位元為一組，累加相鄰的 2-bit 計數值。
    ///           使用遮罩 0x33333333（...00110011）。
    ///
    ///   Step 3：每 8 位元為一組，累加相鄰的 4-bit 計數值。
    ///           使用遮罩 0x0f0f0f0f（...00001111）去除高位污染。
    ///
    ///   Step 4：累加 16 位元的兩半（byte 3 + byte 2，byte 1 + byte 0）。
    ///
    ///   Step 5：累加高低 16-bit 的計數，最終取低 6 位元（最大值 32）。
    ///
    /// 時間複雜度：O(1)（固定位元數操作）。
    /// 此實作等效於 C# 內建的 <c>int.PopCount(i)</c>（.NET 7+）。
    ///
    /// 參考：LeetCode 191. Number of 1 Bits
    /// </summary>
    /// <param name="i">待計算的 32 位元整數。</param>
    /// <returns>i 的二進位表示中「1」的位元個數。</returns>
    private static int BitCount(int i)
    {
        // Step 1：每 2 位元一組，計算各組的 1 的個數
        // 例：0b11 -> 0b10 (2個1), 0b10 -> 0b01 (1個1)
        i = i - ((i >> 1) & 0x55555555);

        // Step 2：每 4 位元一組，累加相鄰兩個 2-bit 計數
        i = (i & 0x33333333) + ((i >> 2) & 0x33333333);

        // Step 3：每 8 位元一組，累加相鄰兩個 4-bit 計數，遮罩去除高位
        i = (i + (i >> 4)) & 0x0f0f0f0f;

        // Step 4：累加 byte2+byte3（高 16 位）、byte0+byte1（低 16 位）
        i = i + (i >> 8);

        // Step 5：累加高低 16-bit 的計數
        i = i + (i >> 16);

        // 取低 6 位元（32 位元整數最多 32 個 1，6 位元足夠表示）
        return i & 0x3f;
    }


    /// <summary>
    /// 方法二：列舉 + 位元計算 + 位元遮罩質數快取（Bitmask Lookup）
    ///
    /// 解題思路：
    /// 由於 right ≤ 10^6 &lt; 2^20，任一數的置位數 c 最多為 20，
    /// 不超過 20 的質數只有 {2, 3, 5, 7, 11, 13, 17, 19} 共 8 個。
    ///
    /// 利用一個 32 位元整數 mask = 665772（二進位：10100010100010101100₂）
    /// 做為質數查表，其中第 i 個位元為 1 表示 i 是質數：
    ///   bit  2 = 1（2 是質數）
    ///   bit  3 = 1（3 是質數）
    ///   bit  5 = 1（5 是質數）
    ///   bit  7 = 1（7 是質數）
    ///   bit 11 = 1（11 是質數）
    ///   bit 13 = 1（13 是質數）
    ///   bit 17 = 1（17 是質數）
    ///   bit 19 = 1（19 是質數）
    ///
    /// 判斷方式：計算 x 的置位數 c，若 (1 << c) & mask ≠ 0，則 c 為質數。
    /// 此方法以單次位元運算取代 IsPrime 的迴圈，效能更佳。
    ///
    /// <example>
    /// <code>
    /// CountPrimeSetBits2(6, 10)  // returns 4
    /// CountPrimeSetBits2(10, 15) // returns 5
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="left">範圍左端點（含）。</param>
    /// <param name="right">範圍右端點（含）。</param>
    /// <returns>滿足條件的整數個數。</returns>
    public int CountPrimeSetBits2(int left, int right)
    {
        int res = 0;

        // 質數位元遮罩：665772 = 0b10100010100010101100
        // 第 i 位元為 1 代表 i 是質數（涵蓋 2, 3, 5, 7, 11, 13, 17, 19）
        const int mask = 665772;

        for(int x = left; x <= right; x++)
        {
            // 計算 x 的置位數 c，再以 (1 << c) & mask 判斷 c 是否為質數
            // 若結果非零，代表 mask 的第 c 個位元為 1，即 c 是質數
            if (((1 << BitCount(x)) & mask) != 0)
            {
                res++;
            }
        }

        return res;
    }
}
