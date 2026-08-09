namespace leetcode_763;

class Program
{
    /// <summary>
    /// 763. Partition Labels
    /// https://leetcode.com/problems/partition-labels/description/
    /// <para>
    /// You are given a string s. Partition it into as many parts as possible so that each letter appears in at most one part. For example, "ababcc" can be partitioned into ["abab", "cc"], but partitions such as ["aba", "bcc"] or ["ab", "ab", "cc"] are invalid.
    ///
    /// The partition must preserve the string: concatenating all parts in order must produce s.
    ///
    /// Return a list of integers representing the sizes of these parts.
    ///
    /// Example 1:
    /// Input: s = "ababcbacadefegdehijhklij"
    /// Output: [9,7,8]
    /// Explanation: The partition is "ababcbaca", "defegde", "hijhklij". Each letter appears in at most one part. A partition such as "ababcbacadefegde", "hijhklij" is incorrect because it splits s into fewer parts.
    ///
    /// Example 2:
    /// Input: s = "eccbbbbdec"
    /// Output: [10]
    ///
    /// Constraints:
    /// - 1 &lt;= s.length &lt;= 500
    /// - s consists of lowercase English letters.
    /// </para>
    /// <para>
    /// 763. 劃分字母區間
    /// https://leetcode.cn/problems/partition-labels/description/
    ///
    /// 給定字串 s。請將它劃分成盡可能多的區段，使每個字母最多只出現在一個區段中。例如，"ababcc" 可劃分為 ["abab", "cc"]，但 ["aba", "bcc"] 或 ["ab", "ab", "cc"] 等劃分方式無效。
    ///
    /// 劃分必須保留原字串：依序串接所有區段後，結果必須仍為 s。
    ///
    /// 回傳表示各區段大小的整數清單。
    ///
    /// 範例 1：
    /// 輸入：s = "ababcbacadefegdehijhklij"
    /// 輸出：[9,7,8]
    /// 解釋：劃分結果為 "ababcbaca"、"defegde"、"hijhklij"，每個字母最多只出現在一個區段中。像 "ababcbacadefegde"、"hijhklij" 這樣的劃分不正確，因為它把 s 分成較少的區段。
    ///
    /// 範例 2：
    /// 輸入：s = "eccbbbbdec"
    /// 輸出：[10]
    ///
    /// 限制條件：
    /// - 1 &lt;= s.length &lt;= 500
    /// - s 由小寫英文字母組成。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試範例 1: "ababcbacadefegdehijhklij"
        // 預期輸出: [9, 7, 8]
        string test1 = "ababcbacadefegdehijhklij";
        var result1 = solution.PartitionLabels(test1);
        Console.WriteLine($"輸入: \"{test1}\"");
        Console.WriteLine($"輸出: [{string.Join(", ", result1)}]");
        Console.WriteLine();

        // 測試範例 2: "eccbbbbdec"
        // 預期輸出: [10]
        string test2 = "eccbbbbdec";
        var result2 = solution.PartitionLabels(test2);
        Console.WriteLine($"輸入: \"{test2}\"");
        Console.WriteLine($"輸出: [{string.Join(", ", result2)}]");
        Console.WriteLine();

        // 測試範例 3: "abcdef"
        // 預期輸出: [1, 1, 1, 1, 1, 1] (每個字母只出現一次)
        string test3 = "abcdef";
        var result3 = solution.PartitionLabels(test3);
        Console.WriteLine($"輸入: \"{test3}\"");
        Console.WriteLine($"輸出: [{string.Join(", ", result3)}]");
    }

    /// <summary>
    /// 使用貪心演算法將字串劃分為盡可能多的區段，使每個字母最多只出現在一個區段中。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>1. 同一個字母只能出現在同一個區段，因此該字母的第一次和最後一次出現位置必須在同一區段內。</para>
    /// <para>2. 先遍歷字串，記錄每個字母最後一次出現的位置。</para>
    /// <para>3. 再次遍歷字串，使用貪心策略：</para>
    /// <para>   - 維護當前區段的起始位置 start 和結束位置 end</para>
    /// <para>   - 對於每個字母，將 end 更新為「當前 end」與「該字母最後出現位置」的較大值</para>
    /// <para>   - 當遍歷位置等於 end 時，表示當前區段結束，記錄長度並開始下一個區段</para>
    /// 
    /// <para><b>時間複雜度：</b> O(n)，其中 n 為字串長度</para>
    /// <para><b>空間複雜度：</b> O(1)，只使用固定大小的陣列（26 個字母）</para>
    /// </summary>
    /// <param name="s">只包含小寫英文字母的字串</param>
    /// <returns>整數串列，表示每個區段的長度</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// var result = solution.PartitionLabels("ababcbacadefegdehijhklij");
    ///  回傳: [9, 7, 8]
    ///  區段劃分為: "ababcbaca", "defegde", "hijhklij"
    /// </code>
    /// </example>
    public IList<int> PartitionLabels(string s)
    {
        // 步驟一：建立陣列記錄每個字母最後一次出現的索引位置
        // lastIndex[0] 代表 'a' 最後出現的位置，lastIndex[1] 代表 'b'，以此類推
        int[] lastIndex = new int[26];
        for (int i = 0; i < s.Length; i++)
        {
            lastIndex[s[i] - 'a'] = i;
        }

        // 步驟二：使用貪心演算法劃分區段
        List<int> result = new List<int>();
        int start = 0;  // 當前區段的起始位置
        int end = 0;    // 當前區段的結束位置（必須涵蓋所有已遇到字母的最後出現位置）

        for (int j = 0; j < s.Length; j++)
        {
            // 更新當前區段的結束位置：取當前 end 與目前字母最後出現位置的較大值
            // 這確保區段能涵蓋目前遇到的所有字母
            end = Math.Max(end, lastIndex[s[j] - 'a']);

            // 當遍歷位置剛好等於區段結束位置時，表示：
            // 1. 當前區段內的所有字母都不會再出現在後面
            // 2. 可以安全地結束這個區段並開始下一個
            if (j == end)
            {
                result.Add(end - start + 1);  // 計算並儲存當前區段長度
                start = j + 1;                 // 設定下一個區段的起始位置
            }
        }

        return result;
    }
}
