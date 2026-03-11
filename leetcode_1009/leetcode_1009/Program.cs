namespace leetcode_1009;

class Program
{
    /// <summary>
    /// 1009. Complement of Base 10 Integer
    /// https://leetcode.com/problems/complement-of-base-10-integer/description/?envType=daily-question&envId=2026-03-11
    /// 1009. 十进制整数的反码
    /// https://leetcode.cn/problems/complement-of-base-10-integer/description/?envType=daily-question&envId=2026-03-11
    ///
    /// The complement of an integer is the integer you get when you flip all the 0's to 1's 
    /// and all the 1's to 0's in its binary representation.
    ///
    /// For example, the integer 5 is "101" in binary and its complement is "010" which is 
    /// the integer 2. Given an integer n, return its complement.
    ///
    /// 整數的反碼是指將其二進位表示中的所有 0 變成 1，所有 1 變成 0 所得到的整數。
    ///
    /// 例如，整數 5 在二進位是 "101"，其反碼為 "010"，即整數 2。給定一個整數 n，
    /// 返回它的反碼。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：n=5 (101₂)，反碼 010₂ = 2
        Console.WriteLine($"BitwiseComplement(5)  = {solution.BitwiseComplement(5)}");   // 預期：2

        // 測試案例 2：n=7 (111₂)，反碼 000₂ = 0
        Console.WriteLine($"BitwiseComplement(7)  = {solution.BitwiseComplement(7)}");   // 預期：0

        // 測試案例 3：n=10 (1010₂)，反碼 0101₂ = 5
        Console.WriteLine($"BitwiseComplement(10) = {solution.BitwiseComplement(10)}");  // 預期：5

        // 邊界案例：n=0，二進位視為 "0"，反碼為 "1" = 1
        Console.WriteLine($"BitwiseComplement(0)  = {solution.BitwiseComplement(0)}");   // 預期：1

        Console.WriteLine();
        Console.WriteLine("--- 解法二：LeadingZeroCount ---");

        // 測試案例 1：n=5 (101₂)，反碼 010₂ = 2
        Console.WriteLine($"BitwiseComplement2(5)  = {solution.BitwiseComplement2(5)}");   // 預期：2

        // 測試案例 2：n=7 (111₂)，反碼 000₂ = 0
        Console.WriteLine($"BitwiseComplement2(7)  = {solution.BitwiseComplement2(7)}");   // 預期：0

        // 測試案例 3：n=10 (1010₂)，反碼 0101₂ = 5
        Console.WriteLine($"BitwiseComplement2(10) = {solution.BitwiseComplement2(10)}");  // 預期：5

        // 邊界案例：n=0，二進位視為 "0"，反碼為 "1" = 1
        Console.WriteLine($"BitwiseComplement2(0)  = {solution.BitwiseComplement2(0)}");   // 預期：1
    }

    /// <summary>
    /// 計算整數 n 的位元反碼（Bitwise Complement）
    ///
    /// 解題出發點：
    /// 電腦以 32 位儲存整數，若直接對所有位元取反會翻轉前導零，
    /// 因此只能翻轉 n 二進位「最高位 1 及以下」的有效位元。
    ///
    /// 位元運算解法步驟：
    /// 1. 找出最高位 1 的位置 i，使得 2^i ≤ n < 2^(i+1)（0 ≤ i ≤ 30）
    /// 2. 建立遮罩 mask = 2^(i+1) - 1，即低 i+1 個位元皆為 1 的數
    /// 3. 對 n 與 mask 做 XOR 運算：
    ///    - 有效位（0～i）與 1 做 XOR → 位元翻轉
    ///    - 高位（i+1 以上）皆為 0，與 0 做 XOR → 保持 0 不變
    ///
    /// 邊界情況：
    /// 當 i=30 時，1 << 31 會造成有號整數溢位，
    /// 故直接使用常數 0x7FFFFFFF（= 2^31 - 1）。
    ///
    /// 範例：n=5 (101₂)，i=2，mask=7 (111₂)，5 XOR 7 = 2 (010₂) ✓
    ///
    /// 時間複雜度：O(log n)　空間複雜度：O(1)
    /// </summary>
    /// <param name="n">輸入的非負整數（0 ≤ n &lt; 10^9）</param>
    /// <returns>n 的位元反碼對應的十進制整數</returns>
    public int BitwiseComplement(int n)
    {
        // 記錄最高位 1 所在的位置，初始為 0（對應 n=0 或 n=1 的情況）
        int highbit = 0;

        // 從第 1 位開始向上找，直到 2^i > n 為止
        for (int i = 1; i <= 30; i++)
        {
            if (n >= 1 << i)
            {
                // n 的最高位至少在第 i 位
                highbit = i;
            }
            else
            {
                // 2^i 已超過 n，最高位就是第 highbit 位，提前結束
                break;
            }
        }

        // 建立遮罩：低 (highbit+1) 個位元全為 1
        // 例如 highbit=2 → mask = (1<<3)-1 = 7 = 111₂
        // 特例 highbit=30 → 直接使用 0x7FFFFFFF 避免 1<<31 溢位
        int mask = highbit == 30 ? 0x7fffffff : (1 << (highbit + 1)) - 1;

        // XOR 翻轉有效位元，高位原本為 0 與 0 做 XOR 仍為 0，不受影響
        return n ^ mask;
    }

    /// <summary>
    /// 計算整數 n 的位元反碼（解法二：LeadingZeroCount）
    ///
    /// 核心想法：
    /// 直接以 <see cref="System.Numerics.BitOperations.LeadingZeroCount"/> 取得
    /// n 的二進位長度 w（即最高位 1 所在位置 + 1），再以
    ///   mask = (1 << w) - 1
    /// 建立低 w 個位元全為 1 的遮罩，最後計算 mask XOR n 即為補數。
    ///
    /// 特殊情況：
    /// n=0 時 LeadingZeroCount 回傳 32，導致 w=0，mask=0，XOR 結果為 0，
    /// 與題意不符（應回傳 1），故以 if 提前返回。
    ///
    /// 範例：n=25 (11001₂)，w=5，mask=31 (11111₂)，25 XOR 31 = 6 (00110₂) ✓
    ///
    /// 時間複雜度：O(1)　空間複雜度：O(1)
    /// </summary>
    /// <param name="n">輸入的非負整數（0 ≤ n &lt; 10^9）</param>
    /// <returns>n 的位元反碼對應的十進制整數</returns>
    public int BitwiseComplement2(int n)
    {
        // 特判 n=0：題意規定 0 的補數為 1
        if (n == 0)
        {
            return 1;
        }

        // LeadingZeroCount 回傳從最高位起連續 0 的個數，
        // 以 uint 轉型確保符號位不干擾計算
        int w = 32 - (int)System.Numerics.BitOperations.LeadingZeroCount((uint)n);

        // 遮罩：低 w 個位元全為 1，例如 w=5 → (1<<5)-1 = 31 = 11111₂
        int mask = (1 << w) - 1;

        // XOR 翻轉有效位元
        return mask ^ n;
    }
}
