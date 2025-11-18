using System;
using System.Collections.Generic;

namespace leetcode_717;

class Program
{
    /// <summary>
    /// 717. 1-bit and 2-bit Characters
    /// https://leetcode.com/problems/1-bit-and-2-bit-characters/description/?envType=daily-question&envId=2025-11-18
    /// 717. 1 比特与 2 比特字符
    /// 717. 1 比特與 2 比特字元 (繁體中文翻譯)
    /// 我們有兩種特殊字元：
    /// 第一種字元可以由單一位元 0 表示。
    /// 第二種字元可以由兩個位元表示 (10 或 11)。
    /// 給定一個以 0 結尾的二進位陣列 bits，若最後一個字元一定是 1 位元字元，請回傳 true。
    /// https://leetcode.cn/problems/1-bit-and-2-bit-characters/description/?envType=daily-question&envId=2025-11-18
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 範例測資
        var tests = new List<int[]>
        {
            new int[] { 1, 0, 0 }, // -> true
            new int[] { 1, 1, 1, 0 }, // -> false
            new int[] { 0 }, // -> true
            new int[] { 1, 0 } // -> false
        };

        Console.WriteLine("LeetCode 717 - 1-bit and 2-bit Characters 範例測試:");
        foreach (var bits in tests)
        {
            var result = program.IsOneBitCharacter(bits);
            Console.WriteLine($"bits: [{string.Join(", ", bits)}] -> {result}");
        }
    }

    /// <summary>
    /// 判斷陣列 bits 是否一定以 1-bit 字元 (a -> 0) 結尾。
    /// 解題說明：
    ///  我們知道 a -> 0 (1 位元)，b -> 10 或 11 (2 位元)，因此：
    ///  - 當 bits[i] == 0 時，代表該字元為 a，跳過一位 (i += 1)。
    ///  - 當 bits[i] == 1 時，代表該字元為 b，跳過兩位 (i += 2)。
    ///  重複直到 i >= n - 1。若 i == n - 1，表示最後一個字元為 1-bit 字元（a），回傳 true；否則回傳 false。
    /// 時間複雜度: O(n)，空間複雜度: O(1)。
    /// </summary>
    /// <param name="bits">二進位陣列（題目保證最後一個元素為 0）</param>
    /// <returns>若最終字元一定為 1-bit 字元（a）則回傳 true，否則回傳 false</returns>
    public bool IsOneBitCharacter(int[] bits)
    {
        int n = bits.Length;
        int i = 0;
        // 迴圈停止條件: 當剩餘元素 <= 1 時結束
        // 若 bits[i] == 0，表示當前字元為 a：只移動一格 (i += 1)
        // 若 bits[i] == 1，表示當前字元為 b：移動兩格 (i += 2)
        while (i < n - 1)
        {
            i += bits[i] + 1; // bits[i] == 0 -> +1; bits[i] == 1 -> +2
        }
        return i == n - 1;
    }


    /// <summary>
    /// 從尾端計算最後一個 0 前連續 1 的數目，判斷最後一個 0 是否為 1-bit 字元的解法。
    /// 解題說明：
    /// - 題目保證陣列以 0 結尾。若最後一個 0 是獨立的 1-bit 字元，則在該 0 之前的 連續 1 的個數必須為偶數。
    /// - 原因：每個 2-bit 字元 (10 或 11) 都以 1 作為它的第一個位元，並佔用兩位。若在最後的 0 前有奇數個 1，
    ///   那麼最後一個 1 將與最後的 0 組成一個 2-bit 字元 (10)，表示最後一個字元並非 1-bit 字元。
    /// - 若連續 1 的個數為偶數，最後的 0 為獨立的 1-bit 字元 (a -> 0)。
    /// 時間複雜度: O(n) (最壞情形為掃描到陣列開頭)，空間複雜度: O(1)。
    /// </summary>
    /// <param name="bits">二進位陣列，題目保證 bits 長度至少為 1 並以 0 結尾。</param>
    /// <returns>若最後一個字元一定為 1-bit 字元，回傳 true，否則回傳 false。</returns>
    public bool IsOneBitCharacter_CountTrailingOnes(int[] bits)
    {
        if (bits is null)
        {
            throw new ArgumentNullException(nameof(bits));
        }
    
        int n = bits.Length;
        int countOnes = 0;

        // 從倒數第二個位元開始向前計數，直到遇到第一個非 1（或陣列開頭）為止。
        // 例如 bits = [1, 1, 1, 0] 時，我們從索引 n-2 = 2 開始：bits[2] == 1 -> countOnes++，
        // bits[1] == 1 -> countOnes++，bits[0] == 1 -> countOnes++ => countOnes = 3 (奇數) -> 回傳 false。
        for (int i = n - 2; i >= 0 && bits[i] == 1; i--)
        {
            countOnes++;
        }

        // 如果 countOnes 為偶數，代表最後的 0 是獨立的 1-bit 字元，否則最後一個字元為 2-bit。
        return countOnes % 2 == 0;
    }
}
