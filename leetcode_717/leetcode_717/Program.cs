using System;
using System.Collections.Generic;

namespace leetcode_717;

class Program
{
    /// <summary>
    /// 717. 1-bit and 2-bit Characters
    /// https://leetcode.com/problems/1-bit-and-2-bit-characters/description/
    /// <para>
    /// We have two special characters:
    /// - The first character can be represented by one bit 0.
    /// - The second character can be represented by two bits (10 or 11).
    ///
    /// Given a binary array bits that ends with 0, return true if the last character must be a one-bit character.
    ///
    /// Example 1:
    /// Input: bits = [1,0,0]
    /// Output: true
    /// Explanation: The only way to decode it is a two-bit character and a one-bit character. Therefore, the last character is a one-bit character.
    ///
    /// Example 2:
    /// Input: bits = [1,1,1,0]
    /// Output: false
    /// Explanation: The only way to decode it is a two-bit character and a two-bit character. Therefore, the last character is not a one-bit character.
    ///
    /// Constraints:
    /// - 1 &lt;= bits.length &lt;= 1000
    /// - bits[i] is either 0 or 1.
    /// </para>
    /// <para>
    /// 717. 1 位元與 2 位元字元
    /// https://leetcode.cn/problems/1-bit-and-2-bit-characters/description/
    ///
    /// 有兩種特殊字元：
    /// - 第一種字元可由一個位元 0 表示。
    /// - 第二種字元可由兩個位元表示（10 或 11）。
    ///
    /// 給定以 0 結尾的二進位陣列 bits，若最後一個字元必定是一位元字元，回傳 true。
    ///
    /// 範例 1：
    /// 輸入：bits = [1,0,0]
    /// 輸出：true
    /// 解釋：唯一的解碼方式是一個二位元字元加上一個一位元字元。因此，最後一個字元是一位元字元。
    ///
    /// 範例 2：
    /// 輸入：bits = [1,1,1,0]
    /// 輸出：false
    /// 解釋：唯一的解碼方式是兩個二位元字元。因此，最後一個字元不是一位元字元。
    ///
    /// 限制條件：
    /// - 1 &lt;= bits.length &lt;= 1000
    /// - bits[i] 是 0 或 1。
    /// </para>
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
