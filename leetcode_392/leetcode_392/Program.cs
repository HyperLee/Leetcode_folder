namespace leetcode_392;

class Program
{
    /// <summary>
    /// 392. Is Subsequence
    /// https://leetcode.com/problems/is-subsequence/
    ///
    /// 題目說明（繁體中文）：
    /// 給定兩個字串 s 和 t，如果 s 是 t 的子序列則回傳 true，否則回傳 false。
    /// 子序列是從原字串刪除部分（可以不刪除）字元後，不改變相對順序所組成的新字串
    /// （例如 "ace" 是 "abcde" 的子序列，而 "aec" 不是）。
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
