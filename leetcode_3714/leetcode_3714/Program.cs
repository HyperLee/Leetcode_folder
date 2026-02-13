namespace leetcode_3714;

class Program
{
    /// <summary>
    /// 3714. Longest Balanced Substring II
    /// <para>
    /// English:
    /// You are given a string <c>s</c> consisting only of the characters 'a', 'b', and 'c'.
    /// A substring of <c>s</c> is called balanced if all distinct characters in the substring appear the same number of times.
    /// Return the length of the longest balanced substring of <c>s</c>.
    /// </para>
    /// <para>
    /// 繁體中文：
    /// 給定一個僅包含字元 'a'、'b' 和 'c' 的字串 <c>s</c>。
    /// 若子字串中所有出現的不同字元出現次數相同，則稱該子字串為「平衡」。
    /// 回傳 <c>s</c> 中最長的平衡子字串的長度。
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數</param>
    /// <see href="https://leetcode.com/problems/longest-balanced-substring-ii/description/?envType=daily-question&amp;envId=2026-02-13">LeetCode 3714 (EN)</see>
    /// <see href="https://leetcode.cn/problems/longest-balanced-substring-ii/description/?envType=daily-question&amp;envId=2026-02-13">LeetCode 3714 (CN)</see>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1：僅包含一種字元 → 連續相同字元最長為 3
        string s1 = "aaa";
        Console.WriteLine($"Input: \"{s1}\" => Output: {program.LongestBalanced(s1)}"); // 預期輸出: 3

        // 測試案例 2：包含兩種字元且平衡 → "abab" 長度 4
        string s2 = "abab";
        Console.WriteLine($"Input: \"{s2}\" => Output: {program.LongestBalanced(s2)}"); // 預期輸出: 4

        // 測試案例 3：包含三種字元 → "abcabc" 長度 6
        string s3 = "abcabc";
        Console.WriteLine($"Input: \"{s3}\" => Output: {program.LongestBalanced(s3)}"); // 預期輸出: 6

        // 測試案例 4：混合情況 → 最長平衡子字串需要比較三種情況
        string s4 = "aabbcc";
        Console.WriteLine($"Input: \"{s4}\" => Output: {program.LongestBalanced(s4)}"); // 預期輸出: 6

        // 測試案例 5：單一字元
        string s5 = "a";
        Console.WriteLine($"Input: \"{s5}\" => Output: {program.LongestBalanced(s5)}"); // 預期輸出: 1

        // 測試案例 6：兩種字元不平衡的情況
        string s6 = "aaabbc";
        Console.WriteLine($"Input: \"{s6}\" => Output: {program.LongestBalanced(s6)}"); // 預期輸出: 3

        // 測試案例 7：較長的混合字串
        string s7 = "abcabcabc";
        Console.WriteLine($"Input: \"{s7}\" => Output: {program.LongestBalanced(s7)}"); // 預期輸出: 9
    }

    /// <summary>
    /// 處理「僅包含兩種字元」的情況，求最長平衡子字串長度。
    /// <para>
    /// 解題思路：字串被第三種字元分割為若干子串，每段子串獨立處理。
    /// 利用前綴和差值（prefix[x] - prefix[y]）搭配雜湊表，
    /// 記錄每個差值最早出現的位置，當同一差值再次出現時，
    /// 代表這段區間內 x 與 y 的個數相等，即為平衡子字串。
    /// </para>
    /// <example>
    /// <code>
    /// // s = "aabb", x = 'a', y = 'b'
    /// // 前綴差值依序為: 1, 2, 1, 0 → 差值 0 在起始位為 -1，
    /// // 當 i=3 時差值再次為 0，長度 = 3 - (-1) = 4
    /// Case2Helper("aabb", 'a', 'b'); // 回傳 4
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="s">輸入字串，僅包含 'a'、'b'、'c'</param>
    /// <param name="x">要比較的第一種字元</param>
    /// <param name="y">要比較的第二種字元</param>
    /// <returns>僅由 <paramref name="x"/> 和 <paramref name="y"/> 組成的最長平衡子字串長度</returns>
    private int Case2Helper(string s, char x, char y) 
    {
        int n = s.Length;
        int res = 0;
        // 雜湊表：記錄前綴差值(countX - countY)最早出現的位置
        Dictionary<int, int> dict = new Dictionary<int, int>();
        
        for (int i = 0; i < n; i++) 
        {
            // 跳過不屬於 x 或 y 的字元（即第三種字元，作為分割點）
            if (s[i] != x && s[i] != y) 
            {
                continue;
            }
            
            // 遇到新的連續段，清空雜湊表
            dict.Clear();
            // 子串起始前，差值為 0，位置設為 i - 1（代表虛擬起點）
            dict[0] = i - 1;
            int diff = 0;
            // 在連續段內逐字元處理
            while (i < n && (s[i] == x || s[i] == y)) 
            {
                // 遇到 x 則差值 +1，遇到 y 則差值 -1
                diff += (s[i] == x) ? 1 : -1;
                if (dict.ContainsKey(diff)) 
                {
                    // 同一差值再次出現 → 中間區間 x 與 y 個數相等
                    res = Math.Max(res, i - dict[diff]);
                } 
                else 
                {
                    // 首次出現此差值，記錄位置
                    dict[diff] = i;
                }
                i++;
            }
        }

        return res;
    }
    
    /// <summary>
    /// 求字串 <paramref name="s"/> 中最長平衡子字串的長度。
    /// <para>
    /// 解題思路：將問題拆分為三種情況分別求解，最後取最大值。
    /// <list type="number">
    /// <item>僅包含一種字元：計算最長連續相同字元長度。</item>
    /// <item>包含兩種字元：枚舉三種組合 (a,b)、(b,c)、(a,c)，利用前綴和差值 + 雜湊表求解。</item>
    /// <item>包含三種字元：利用二元組 (Sa-Sb, Sb-Sc) 的前綴差值 + 雜湊表求解。</item>
    /// </list>
    /// </para>
    /// <example>
    /// <code>
    /// LongestBalanced("abcabc"); // 回傳 6，整個字串即為平衡子字串
    /// LongestBalanced("aaa");    // 回傳 3，僅包含一種字元的情況
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="s">輸入字串，僅包含 'a'、'b'、'c'</param>
    /// <returns>最長平衡子字串的長度</returns>
    public int LongestBalanced(string s) 
    {
        int n = s.Length;
        int res = 0;

        // ========== 情況一：僅包含一種字元 ==========
        // 計算最長連續相同字元的長度
        int last = 0;
        for (int i = 0; i < n; i++)
        {
            if (i > 0 && s[i] == s[i - 1]) 
            {
                // 與前一個字元相同，連續長度 +1
                last++;
            } 
            else 
            {
                // 不同字元，重置為 1
                last = 1;
            }
            res = Math.Max(res, last);
        }

        // ========== 情況二：包含兩種字元 ==========
        // 枚舉所有兩種字元的組合，分別求最長平衡子字串
        res = Math.Max(res, Case2Helper(s, 'a', 'b'));
        res = Math.Max(res, Case2Helper(s, 'b', 'c'));
        res = Math.Max(res, Case2Helper(s, 'a', 'c'));
        
        // ========== 情況三：包含三種字元 ==========
        // 利用二元組 (Sb - Sa, Sb - Sc) 作為雜湊表的鍵
        // 當兩個位置的二元組相同時，代表中間區間三種字元個數相等
        Dictionary<string, int> dict = new Dictionary<string, int>();
        // 虛擬起點：位置 -1 處，三種字元計數均為 0，差值均為 0
        dict[GetId(0, 0, n)] = -1;
        
        // pre[0]=count('a'), pre[1]=count('b'), pre[2]=count('c') 的前綴計數
        int[] pre = new int[3];
        for (int i = 0; i < n; i++) 
        {
            // 更新對應字元的前綴計數
            pre[s[i] - 'a']++;
            // 計算二元組鍵值：(count('b') - count('a'), count('b') - count('c'))
            string id = GetId(pre[1] - pre[0], pre[1] - pre[2], n);
            if (dict.ContainsKey(id)) 
            {
                // 二元組重複出現 → 中間區間為平衡子字串
                res = Math.Max(res, i - dict[id]);
            } 
            else 
            {
                // 首次出現，記錄位置
                dict[id] = i;
            }
        }

        return res;
    }
    
    /// <summary>
    /// 將二元組 (<paramref name="x"/>, <paramref name="y"/>) 轉換為唯一的字串鍵值，供雜湊表使用。
    /// <para>
    /// 因為差值可能為負數，加上 <paramref name="n"/>（字串長度）確保偏移後為非負數，
    /// 再以底線連接組成如 "3_5" 的字串作為雜湊表鍵。
    /// </para>
    /// <example>
    /// <code>
    /// GetId(1, -2, 5); // 回傳 "6_3"
    /// GetId(0, 0, 5);  // 回傳 "5_5"
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="x">第一個差值（Sb - Sa）</param>
    /// <param name="y">第二個差值（Sb - Sc）</param>
    /// <param name="n">字串長度，用於偏移確保非負</param>
    /// <returns>表示二元組的唯一字串鍵值</returns>
    private string GetId(int x, int y, int n) 
    {
        // 加上 n 偏移，避免負數造成鍵值衝突
        return (x + n) + "_" + (y + n);
    }
}
