namespace leetcode_1576;

class Program
{
    /// <summary>
    /// <para>
    /// 1576. Replace All ?'s to Avoid Consecutive Repeating Characters
    /// https://leetcode.com/problems/replace-all-s-to-avoid-consecutive-repeating-characters/description/
    ///
    /// Given a string s containing only lowercase English letters and '?', convert all '?' characters into lowercase letters
    /// so the final string has no consecutive repeating characters. You cannot modify non-'?' characters.
    /// It is guaranteed that the given string has no consecutive repeating characters except for '?'.
    /// Return the final string after all conversions (possibly zero). If multiple solutions exist, return any of them. An
    /// answer is always possible under the given constraints.
    ///
    /// Example 1:
    /// Input: s = "?zs"
    /// Output: "azs"
    /// Explanation: There are 25 solutions, from "azs" through "yzs". Only "z" is invalid because it would produce "zzs".
    ///
    /// Example 2:
    /// Input: s = "ubv?w"
    /// Output: "ubvaw"
    /// Explanation: There are 24 solutions. Only "v" and "w" are invalid because they produce "ubvvw" and "ubvww".
    ///
    /// Constraints:
    /// - 1 &lt;= s.length &lt;= 100
    /// - s consists of lowercase English letters and '?'.
    /// </para>
    /// <para>
    /// 1576. 替換所有問號以避免連續重複字元
    /// https://leetcode.cn/problems/replace-all-s-to-avoid-consecutive-repeating-characters/description/
    ///
    /// 給定只包含小寫英文字母與 '?' 的字串 s，將所有 '?' 轉換成小寫字母，使最終字串不含連續重複字元。
    /// 不可修改非 '?' 字元。題目保證給定字串除了 '?' 以外，不存在連續重複字元。
    /// 回傳完成所有轉換（也可能不需轉換）後的字串。若有多個解答，回傳任一個；在給定限制下必定有解。
    ///
    /// 範例 1：
    /// 輸入：s = "?zs"
    /// 輸出："azs"
    /// 解釋：共有 25 種解答，從 "azs" 到 "yzs" 都有效；只有 "z" 無效，因為會形成 "zzs"。
    ///
    /// 範例 2：
    /// 輸入：s = "ubv?w"
    /// 輸出："ubvaw"
    /// 解釋：共有 24 種解答；只有 "v" 與 "w" 無效，因為會形成 "ubvvw" 與 "ubvww"。
    ///
    /// 限制條件：
    /// - 1 &lt;= s.length &lt;= 100
    /// - s 只包含小寫英文字母與 '?'。
    /// </para>
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1: 單一問號在字串開頭
        string test1 = "?zs";
        Console.WriteLine($"輸入: \"{test1}\" => 輸出: \"{solution.ModifyString(test1)}\"");

        // 測試案例 2: 多個問號在中間
        string test2 = "ubv?w";
        Console.WriteLine($"輸入: \"{test2}\" => 輸出: \"{solution.ModifyString(test2)}\"");

        // 測試案例 3: 連續問號
        string test3 = "j?qg??b";
        Console.WriteLine($"輸入: \"{test3}\" => 輸出: \"{solution.ModifyString(test3)}\"");

        // 測試案例 4: 全部都是問號
        string test4 = "??????????";
        Console.WriteLine($"輸入: \"{test4}\" => 輸出: \"{solution.ModifyString(test4)}\"");

        // 測試案例 5: 沒有問號
        string test5 = "abcdefg";
        Console.WriteLine($"輸入: \"{test5}\" => 輸出: \"{solution.ModifyString(test5)}\"");
    }

    /// <summary>
    /// 解題思路：貪婪演算法 (Greedy Algorithm)
    /// 
    /// <para>
    /// <b>核心概念：</b>
    /// 題目要求將所有 '?' 替換為小寫字母，且不能產生連續重複字元。
    /// 關鍵觀察：每個 '?' 只需要考慮其左鄰居和右鄰居，因此只需要 3 個不同的字母 ('a', 'b', 'c') 就足夠了。
    /// 無論左右鄰居是什麼字母，我們總能從 'a', 'b', 'c' 中找到一個既不等於左鄰居也不等於右鄰居的字母。
    /// </para>
    /// 
    /// <para>
    /// <b>演算法步驟：</b>
    /// <list type="number">
    ///   <item>將字串轉換為字元陣列以便修改</item>
    ///   <item>從左到右遍歷每個字元</item>
    ///   <item>遇到 '?' 時，取得左右鄰居（注意邊界處理）</item>
    ///   <item>依序嘗試 'a', 'b', 'c'，選擇第一個不與左右鄰居重複的字母</item>
    /// </list>
    /// </para>
    /// 
    /// <para>
    /// <b>時間複雜度：</b> O(n)，其中 n 為字串長度，只需遍歷一次。
    /// </para>
    /// <para>
    /// <b>空間複雜度：</b> O(n)，用於儲存字元陣列。
    /// </para>
    /// 
    /// <para>
    /// <b>邊界處理技巧：</b>
    /// 使用 <c>(char?)null</c> 來表示不存在的鄰居（字串開頭或結尾），
    /// 這樣在比較時 <c>null != 'a'</c> 永遠為 true，簡化了邊界條件的判斷。
    /// </para>
    /// </summary>
    /// <param name="s">包含小寫英文字母和 '?' 的輸入字串</param>
    /// <returns>替換所有 '?' 後，不含連續重複字元的字串</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// solution.ModifyString("?zs");      // 回傳 "azs"
    /// solution.ModifyString("ubv?w");    // 回傳 "ubvaw"
    /// solution.ModifyString("j?qg??b");  // 回傳 "jaqgacb" 或其他有效答案
    /// </code>
    /// </example>
    public string ModifyString(string s)
    {
        int n = s.Length;

        // 將字串轉為字元陣列，因為 C# 字串是不可變的 (immutable)
        char[] arr = s.ToCharArray();

        // 從左到右遍歷每個字元
        for (int i = 0; i < n; i++)
        {
            // 取得左鄰居：若 i == 0（字串開頭），則沒有左鄰居，使用 null 表示
            // (char?)null 是可空字元型別，用於處理邊界情況
            var left = i > 0 ? arr[i - 1] : (char?)null;

            // 取得右鄰居：若 i == n-1（字串結尾），則沒有右鄰居，使用 null 表示
            var right = i < n - 1 ? arr[i + 1] : (char?)null;

            // 只處理 '?' 字元
            if (arr[i] == '?')
            {
                // 貪婪策略：依序嘗試 'a', 'b', 'c'
                // 由於只有最多兩個鄰居，三個候選字母中必有一個可用
                if (left != 'a' && right != 'a')
                {
                    // 'a' 不與左右鄰居重複，選擇 'a'
                    arr[i] = 'a';
                }
                else if (left != 'b' && right != 'b')
                {
                    // 'b' 不與左右鄰居重複，選擇 'b'
                    arr[i] = 'b';
                }
                else if (left != 'c' && right != 'c')
                {
                    // 'c' 不與左右鄰居重複，選擇 'c'
                    arr[i] = 'c';
                }
                // 注意：邏輯上一定會進入上述三個分支之一，因為最多只有兩個鄰居
            }
        }

        // 將字元陣列轉回字串並回傳
        return new string(arr);
    }
}
