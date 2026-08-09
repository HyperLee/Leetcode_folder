namespace leetcode_392;

class Program
{
    /// <summary>
    /// 392. Is Subsequence
    /// https://leetcode.com/problems/is-subsequence/description/
    /// <para>
    /// Given two strings s and t, return true if s is a subsequence of t, or false otherwise.
    ///
    /// A subsequence of a string is a new string formed from the original string by deleting some (possibly none) of the characters without disturbing the relative positions of the remaining characters. For example, "ace" is a subsequence of "abcde", while "aec" is not.
    ///
    /// Example 1:
    /// Input: s = "abc", t = "ahbgdc"
    /// Output: true
    ///
    /// Example 2:
    /// Input: s = "axc", t = "ahbgdc"
    /// Output: false
    ///
    /// Constraints:
    /// - 0 &lt;= s.length &lt;= 100
    /// - 0 &lt;= t.length &lt;= 10^4
    /// - s and t consist only of lowercase English letters.
    ///
    /// Follow up: Suppose there are lots of incoming s, say s_1, s_2, ..., s_k where k &gt;= 10^9, and you want to check one by one whether t has each as a subsequence. How would you change your code?
    /// </para>
    /// <para>
    /// 392. 判斷子序列
    /// https://leetcode.cn/problems/is-subsequence/description/
    ///
    /// 給定兩個字串 s 與 t，若 s 是 t 的子序列則回傳 true，否則回傳 false。
    ///
    /// 字串的子序列是從原字串刪除部分字元（也可以不刪除）後，在不改變剩餘字元相對位置的情況下形成的新字串。例如，"ace" 是 "abcde" 的子序列，而 "aec" 不是。
    ///
    /// 範例 1：
    /// 輸入：s = "abc", t = "ahbgdc"
    /// 輸出：true
    ///
    /// 範例 2：
    /// 輸入：s = "axc", t = "ahbgdc"
    /// 輸出：false
    ///
    /// 限制條件：
    /// - 0 &lt;= s.length &lt;= 100
    /// - 0 &lt;= t.length &lt;= 10^4
    /// - s 與 t 只由小寫英文字母組成。
    ///
    /// 進階：假設有大量傳入的 s，例如 s_1、s_2、...、s_k，其中 k &gt;= 10^9，而且你想逐一檢查它們是否為 t 的子序列。在此情境下，你會如何修改程式碼？
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數。</param>
    static void Main(string[] args)
    {
        var solver = new Program();

        // 範例測試資料
        void Test(string s, string t)
        {
            bool result = solver.IsSubsequence(s, t);
            Console.WriteLine($"s=\"{s}\", t=\"{t}\" => {result}");
        }

        Test("abc", "ahbgdc"); // true
        Test("axc", "ahbgdc"); // false
        Test("", "ahbgdc"); // true
        Test("aaa", "aa"); // false
    }

    /// <summary>
    /// 判斷字串 <c>s</c> 是否為 <c>t</c> 的子序列。
    ///
    /// 解題說明：使用「雙指針（Greedy）」方法從左到右匹配。維護兩個指標 i, j，
    /// 分別指向 <c>s</c> 與 <c>t</c> 的目前位置；當字元相符時同時右移 i 與 j，
    /// 否則只右移 j。若最終 i 移動到 <c>s</c> 的末端，代表所有字元均被匹配成功。
    /// 時間複雜度為 O(n + m)，空間複雜度為 O(1)。
    /// </summary>
    /// <param name="s">欲檢查是否為子序列的字串（若為空字串或僅含空白，視為子序列）。</param>
    /// <param name="t">目標字串，用來尋找子序列。</param>
    /// <returns>若 <c>s</c> 為 <c>t</c> 的子序列回傳 <c>true</c>，否則回傳 <c>false</c>。</returns>
    public bool IsSubsequence(string s, string t)
    {
        if (s is null) throw new ArgumentNullException(nameof(s));
        if (t is null) throw new ArgumentNullException(nameof(t));

        int n = s.Length;
        int m = t.Length;
        int i = 0; // 指向 s 的目前位置
        int j = 0; // 指向 t 的目前位置

        // 空字串（含僅有空白字元）也是子序列
        if (s.Trim().Length == 0)
        {
            return true;
        }

        while (i < n && j < m)
        {
            // 若當前字元相符，移動 s 的指標（嘗試匹配下一個字元）
            if (s[i] == t[j])
            {
                i++;
            }

            // 無論是否相符，t 的指標都要往右移，繼續尋找下一個可以匹配的位置
            j++;
        }

        // 當 i 已經移動到 s 的長度，代表所有字元均被找到並順序匹配成功
        return i == n;
    }
}
