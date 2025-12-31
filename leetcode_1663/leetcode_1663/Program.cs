namespace leetcode_1663;

class Program
{
    /// <summary>
    /// 1663. Smallest String With A Given Numeric Value
    /// https://leetcode.com/problems/smallest-string-with-a-given-numeric-value/description/
    /// 1663. 具有給定數值的最小字串
    /// https://leetcode.cn/problems/smallest-string-with-a-given-numeric-value/description/
    /// 1663. 具有給定數值的最小字串
    /// 給定兩個整數 n 和 k，回傳長度等於 n 且字母數值總和等於 k 的字串；在所有滿足條件的字串中，回傳字典序（lexicographically）最小的那一個。
    /// 小寫字母的數值定義為其在字母表中的位置（a = 1, b = 2, c = 3, …），字串的數值為其所有字母數值的總和。例如字串 "abe" 的數值為 1 + 2 + 5 = 8。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1: n = 3, k = 27
        // 預期輸出: "aay" (1 + 1 + 25 = 27)
        string result1 = solution.GetSmallestString(3, 27);
        Console.WriteLine($"n = 3, k = 27 => \"{result1}\"");

        // 測試案例 2: n = 5, k = 73
        // 預期輸出: "aaszz" (1 + 1 + 19 + 26 + 26 = 73)
        string result2 = solution.GetSmallestString(5, 73);
        Console.WriteLine($"n = 5, k = 73 => \"{result2}\"");

        // 測試案例 3: n = 3, k = 3
        // 預期輸出: "aaa" (1 + 1 + 1 = 3)
        string result3 = solution.GetSmallestString(3, 3);
        Console.WriteLine($"n = 3, k = 3 => \"{result3}\"");

        // 測試案例 4: n = 1, k = 26
        // 預期輸出: "z" (26)
        string result4 = solution.GetSmallestString(1, 26);
        Console.WriteLine($"n = 1, k = 26 => \"{result4}\"");

        // 測試案例 5: n = 2, k = 52
        // 預期輸出: "zz" (26 + 26 = 52)
        string result5 = solution.GetSmallestString(2, 52);
        Console.WriteLine($"n = 2, k = 52 => \"{result5}\"");
    }

    /// <summary>
    /// 取得具有給定數值的字典序最小字串。
    /// </summary>
    /// <remarks>
    /// <para><b>解題思路：貪心演算法（Greedy Algorithm）</b></para>
    /// <para>
    /// 核心概念：要讓字串的字典序最小，應該盡可能讓前面的字元為 'a'，
    /// 而將較大的字元放在後面。因此，我們從字串的末尾往前填充字元。
    /// </para>
    /// <para><b>演算法步驟：</b></para>
    /// <list type="number">
    ///   <item>從最後一個位置開始往前填充</item>
    ///   <item>如果剩餘數值足夠放 'z'（即 k - 26 >= 剩餘位置數 - 1），則放 'z'</item>
    ///   <item>否則，計算當前位置應放的字元，使前面的位置都能放 'a'</item>
    ///   <item>剩餘位置全部填 'a'</item>
    /// </list>
    /// <para><b>範例演示 (n=5, k=73)：</b></para>
    /// <code>
    /// 步驟 1: 位置 4，k=73，73-26=47 >= 4，放 'z'，k=47
    /// 步驟 2: 位置 3，k=47，47-26=21 >= 3，放 'z'，k=21
    /// 步驟 3: 位置 2，k=21，21-26=-5 &lt; 2，放 'a'+(21-3)='s'，k=2
    /// 步驟 4: 位置 1，k=2，放 'a'，k=1
    /// 步驟 5: 位置 0，k=1，放 'a'，k=0
    /// 結果: "aaszz" (1+1+19+26+26=73)
    /// </code>
    /// </remarks>
    /// <param name="n">字串的長度，範圍為 1 到 10^5</param>
    /// <param name="k">目標數值總和，範圍為 n 到 26*n</param>
    /// <returns>長度為 n 且數值總和為 k 的字典序最小字串</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// string result = solution.GetSmallestString(3, 27);
    ///  result = "aay" (1 + 1 + 25 = 27)
    /// </code>
    /// </example>
    public string GetSmallestString(int n, int k)
    {
        // 建立字元陣列來儲存結果
        char[] chs = new char[n];
        
        // 從最後一個索引開始往前填充
        int index = n - 1;
        
        // 當還有位置需要填充時繼續迴圈
        while (n > 0)
        {
            // 情況 1: 剩餘數值足夠放 'z'
            // 條件：k - 26 >= n - 1 表示即使放了 'z'，前面的位置仍能用 'a' 填滿
            // 例如：k=73, n=5，73-26=47 >= 4，所以可以放 'z'
            if (k - 26 >= n - 1)
            {
                chs[index--] = 'z';  // 放置 'z'
                k -= 26;              // 減去 'z' 的數值
            }
            // 情況 2: 無法放 'z'，需要計算適當的字元
            // 公式：當前字元 = 'a' + (k - n)
            // 推導：假設前面 n-1 個位置都放 'a'，則需要 n-1 的數值
            //       當前位置需要 k - (n-1) = k - n + 1 的數值
            //       字元為 'a' + (k - n + 1 - 1) = 'a' + (k - n)
            else if (k > 0)
            {
                chs[index--] = (char)('a' + (k - n));  // 計算並放置適當字元
                k = n - 1;  // 剩餘數值恰好等於剩餘位置數（每個放 'a'）
            }
            // 情況 3: 剩餘數值剛好，放 'a'
            else
            {
                k--;
                chs[index--] = 'a';
            }
            
            n--;  // 剩餘位置數減 1
        }
        
        // 將字元陣列轉換為字串並回傳
        string s = new string(chs);
        return s;
    }
}
