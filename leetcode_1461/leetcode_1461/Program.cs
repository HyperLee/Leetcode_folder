namespace leetcode_1461;

class Program
{
    /// <summary>
    /// 1461. Check If a String Contains All Binary Codes of Size K
    /// https://leetcode.com/problems/check-if-a-string-contains-all-binary-codes-of-size-k/
    /// 1461. 检查一个字符串是否包含所有长度为 K 的二进制子串
    /// https://leetcode.cn/problems/check-if-a-string-contains-all-binary-codes-of-size-k/description/
    /// 
    /// Given a binary string s and an integer k, return true if every binary code of length k is a substring of s. 
    /// Otherwise, return false.
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試案例 1: s = "00110110", k = 2
        // 長度為 2 的二進位字串有: "00", "01", "10", "11"
        // s 中包含: "00"(索引0), "01"(索引1), "11"(索引2), "10"(索引3), "01"(索引4), "11"(索引5), "10"(索引6)
        // 所有長度為 2 的二進位字串都存在，預期結果: true
        string s1 = "00110110";
        int k1 = 2;
        Console.WriteLine($"測試案例 1: s = \"{s1}\", k = {k1}");
        Console.WriteLine($"結果 (方法一): {program.HasAllCodes(s1, k1)}"); // 預期: true
        Console.WriteLine($"結果 (方法二): {program.HasAllCodes2(s1, k1)}"); // 預期: true
        Console.WriteLine();

        // 測試案例 2: s = "0110", k = 1
        // 長度為 1 的二進位字串有: "0", "1"
        // s 中包含: "0"(索引0), "1"(索引1), "1"(索引2), "0"(索引3)
        // 所有長度為 1 的二進位字串都存在，預期結果: true
        string s2 = "0110";
        int k2 = 1;
        Console.WriteLine($"測試案例 2: s = \"{s2}\", k = {k2}");
        Console.WriteLine($"結果 (方法一): {program.HasAllCodes(s2, k2)}"); // 預期: true
        Console.WriteLine($"結果 (方法二): {program.HasAllCodes2(s2, k2)}"); // 預期: true
        Console.WriteLine();

        // 測試案例 3: s = "0110", k = 2
        // 長度為 2 的二進位字串有: "00", "01", "10", "11"
        // s 中包含: "01"(索引0), "11"(索引1), "10"(索引2)
        // 缺少 "00"，預期結果: false
        string s3 = "0110";
        int k3 = 2;
        Console.WriteLine($"測試案例 3: s = \"{s3}\", k = {k3}");
        Console.WriteLine($"結果 (方法一): {program.HasAllCodes(s3, k3)}"); // 預期: false
        Console.WriteLine($"結果 (方法二): {program.HasAllCodes2(s3, k3)}"); // 預期: false
        Console.WriteLine();

        // 測試案例 4: s = "0000000001011100", k = 4
        // 長度為 4 的二進位字串有 2^4 = 16 種
        // 字串長度為 16，最少需要 2^4 + 4 - 1 = 19 個字元
        // 字串長度不足，預期結果: false
        string s4 = "0000000001011100";
        int k4 = 4;
        Console.WriteLine($"測試案例 4: s = \"{s4}\", k = {k4}");
        Console.WriteLine($"結果 (方法一): {program.HasAllCodes(s4, k4)}"); // 預期: false
        Console.WriteLine($"結果 (方法二): {program.HasAllCodes2(s4, k4)}"); // 預期: false
    }

    /// <summary>
    /// 方法一：雜湊表 (Hash Set)
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 我們需要判斷字串 s 是否包含所有長度為 k 的二進位字串。
    /// 長度為 k 的二進位字串總共有 2^k 種（從 00...0 到 11...1）。
    /// </para>
    /// 
    /// <para><b>演算法步驟：</b></para>
    /// <list type="number">
    ///   <item>
    ///     <description>
    ///     首先進行長度檢查優化：如果 s 的長度小於 2^k + k - 1，則不可能包含所有的二進位字串。
    ///     這是因為要容納 2^k 個不同的長度為 k 的子串，至少需要 2^k + k - 1 個字元。
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///     使用滑動視窗遍歷字串 s，取出所有長度為 k 的子串。
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///     將每個子串加入雜湊集合（HashSet），自動去除重複。
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///     最後判斷雜湊集合的大小是否等於 2^k。
    ///     </description>
    ///   </item>
    /// </list>
    /// 
    /// <para><b>時間複雜度：</b>O(n × k)，其中 n 是字串 s 的長度，每次取子串需要 O(k) 時間。</para>
    /// <para><b>空間複雜度：</b>O(2^k × k)，雜湊集合最多儲存 2^k 個長度為 k 的字串。</para>
    /// </summary>
    /// <param name="s">二進位字串，只包含字元 '0' 和 '1'</param>
    /// <param name="k">子串長度，1 &lt;= k &lt;= 20</param>
    /// <returns>如果 s 包含所有長度為 k 的二進位字串則返回 true，否則返回 false</returns>
    /// <example>
    /// <code>
    ///  範例 1: 返回 true
    /// HasAllCodes("00110110", 2); // 包含 "00", "01", "10", "11"
    /// 
    ///  範例 2: 返回 false
    /// HasAllCodes("0110", 2); // 缺少 "00"
    /// </code>
    /// </example>
    public bool HasAllCodes(string s, int k)
    {
        // 提前檢查：字串長度是否足夠
        // 要包含 2^k 個長度為 k 的不同子串，字串長度至少需要 2^k + k - 1
        // 例如：k=2 時，需要至少 2^2 + 2 - 1 = 5 個字元
        if (s.Length < (1 << k) + k - 1)
        {
            return false;
        }

        // 使用雜湊集合儲存所有長度為 k 的子串
        // HashSet 會自動去除重複的子串
        HashSet<string> exists = new HashSet<string>();

        // 使用滑動視窗遍歷字串
        // 從索引 0 開始，到索引 s.Length - k 結束
        for (int i = 0; i <= s.Length - k; i++)
        {
            // 取出從索引 i 開始、長度為 k 的子串
            exists.Add(s.Substring(i, k));
        }

        // 判斷是否收集到了所有 2^k 種不同的子串
        // 1 << k 等同於 2^k (位元左移運算)
        return exists.Count == (1 << k);
    }

    /// <summary>
    /// 方法二：位元運算滑動視窗 (Bit Manipulation Sliding Window)
    ///
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 與方法一不同，本方法使用位元運算將長度為 k 的子串直接映射為整數，
    /// 避免字串操作的開銷，並用布林陣列取代雜湊集合，提升整體效能。
    /// </para>
    ///
    /// <para><b>核心觀念：滑動視窗轉整數</b></para>
    /// <para>
    /// 每次視窗向右滑動一格，更新公式如下：
    /// x = (x << 1 & Mask) | (c & 1)
    /// 1. 將 x 左移 1 位（捨棄最左邊的舊位元）
    /// 2. 與 Mask 做 AND，保留最低 k 個位元
    /// 3. 將新字元的位元值 OR 入最低位
    /// </para>
    ///
    /// <para><b>演算法步驟：</b></para>
    /// <list type="number">
    ///   <item><description>建立位元遮罩 Mask = 2^k - 1，二進位為 k 個連續的 1。</description></item>
    ///   <item><description>建立大小為 2^k 的布林陣列 has，以整數值為索引記錄出現狀態。</description></item>
    ///   <item><description>遍歷字串，對每個字元用位元運算更新滑動視窗整數 x。</description></item>
    ///   <item><description>當 i &gt;= k-1 時視窗才湊滿 k 個字元，開始記錄。</description></item>
    ///   <item><description>小優化：count 達到 2^k 時提前結束迴圈。</description></item>
    /// </list>
    ///
    /// <para><b>時間複雜度：</b>O(n)，每個字元只處理一次，位元運算為 O(1)。</para>
    /// <para><b>空間複雜度：</b>O(2^k)，只需布林陣列，不需儲存字串。</para>
    /// </summary>
    /// <param name="s">二進位字串，只包含字元 '0' 和 '1'</param>
    /// <param name="k">子串長度，1 &lt;= k &lt;= 20</param>
    /// <returns>如果 s 包含所有長度為 k 的二進位字串則返回 true，否則返回 false</returns>
    /// <example>
    /// <code>
    ///  範例 1: 返回 true
    /// HasAllCodes2("00110110", 2); // 視窗依序產生整數 0,1,3,2,1,3,2，涵蓋 0~3 全部 ✓
    ///
    ///  範例 2: 返回 false
    /// HasAllCodes2("0110", 2); // 缺少整數 0（即 "00"）
    /// </code>
    /// </example>
    public bool HasAllCodes2(string s, int k)
    {
        // Mask = 2^k - 1，二進位為 k 個連續的 1
        // 例如 k=2 時：Mask = 0b11 = 3，用於保留整數 x 最低 k 個位元
        int Mask = (1 << k) - 1;

        // 用布林陣列取代 HashSet，索引即為整數值，存取速度更快
        // 陣列大小為 2^k，對應所有可能的 k 位元二進位值
        bool[] has = new bool[1 << k];

        // count：已收集到的不同子串數量；達 2^k 時可提前結束
        int count = 0;

        // x：目前滑動視窗對應的整數值（始終只保留最低 k 個位元）
        int x = 0;

        // 迴圈結束條件加入 count < (1 << k) 的提前終止優化
        // 一旦所有 2^k 種子串都找到，立即跳出，無需繼續掃描
        for (int i = 0; i < s.Length && count < (1 << k); i++)
        {
            char c = s[i];

            // 位元滑動更新：左移捨棄舊位元，AND 遮罩保留低 k 位，OR 加入新位元
            // c & 1：利用 ASCII 特性，'0'(48) & 1 = 0，'1'(49) & 1 = 1
            x = (x << 1 & Mask) | (c & 1);

            // 前 k-1 個字元尚未湊滿一個完整視窗
            // 從第 k 個字元（i >= k-1）起，x 才代表有效的 k 位元子串
            if (i >= k - 1 && !has[x])
            {
                has[x] = true;
                count++;
            }
        }

        // 若收集到所有 2^k 種不同子串，返回 true
        return count == (1 << k);
    }
}
