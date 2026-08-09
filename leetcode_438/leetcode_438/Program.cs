namespace leetcode_438;

class Program
{
    /// <summary>
    /// 438. Find All Anagrams in a String
    /// https://leetcode.com/problems/find-all-anagrams-in-a-string/description/
    /// <para>
    /// Given two strings s and p, return an array of all the start indices of p's anagrams in s. You may return the answer in any order.
    ///
    /// Example 1:
    /// Input: s = "cbaebabacd", p = "abc"
    /// Output: [0,6]
    /// Explanation: The substring starting at index 0 is "cba", an anagram of "abc". The substring starting at index 6 is "bac", also an anagram of "abc".
    ///
    /// Example 2:
    /// Input: s = "abab", p = "ab"
    /// Output: [0,1,2]
    /// Explanation: The substrings starting at indices 0, 1, and 2 are "ab", "ba", and "ab"; each is an anagram of "ab".
    ///
    /// Constraints:
    /// - 1 &lt;= s.length, p.length &lt;= 3 * 10^4
    /// - s and p consist of lowercase English letters.
    /// </para>
    /// <para>
    /// 438. 找出字串中所有字母異位詞
    /// https://leetcode.cn/problems/find-all-anagrams-in-a-string/description/
    ///
    /// 給定兩個字串 s 與 p，回傳 s 中所有 p 的字母異位詞之起始索引所組成的陣列。答案可以任意順序回傳。
    ///
    /// 範例 1：
    /// 輸入：s = "cbaebabacd", p = "abc"
    /// 輸出：[0,6]
    /// 解釋：起始索引為 0 的子字串是 "cba"，它是 "abc" 的字母異位詞。起始索引為 6 的子字串是 "bac"，它也是 "abc" 的字母異位詞。
    ///
    /// 範例 2：
    /// 輸入：s = "abab", p = "ab"
    /// 輸出：[0,1,2]
    /// 解釋：起始索引為 0、1 與 2 的子字串分別是 "ab"、"ba" 與 "ab"；每個都是 "ab" 的字母異位詞。
    ///
    /// 限制條件：
    /// - 1 &lt;= s.length, p.length &lt;= 3 * 10^4
    /// - s 與 p 只由小寫英文字母組成。
    /// </para>
    /// </summary>
    /// <param name="args"></param> 
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行固定的字母異位詞範例，逐案比較預期索引與
    /// <see cref="FindAnagrams(string, string)"/> 的實際結果。
    /// 此方法不接收輸入或回傳資料，會將 Expected、Actual 與 PASS/FAIL 輸出至主控台；
    /// 任一案例失敗時，將程序結束代碼設為 1。
    /// </summary>
    private static void RunSamples()
    {
        TestCase[] testCases =
        [
            new("官方範例一", "cbaebabacd", "abc", [0, 6]),
            new("官方範例二", "abab", "ab", [0, 1, 2]),
            new("空來源字串（防禦性案例）", "", "abc", []),
            new("目標字串較長", "ab", "abc", []),
            new("所有字母相同", "aaaaaaa", "aa", [0, 1, 2, 3, 4, 5]),
            new("等長完全匹配", "abc", "abc", [0]),
            new("沒有異位詞", "abc", "xyz", [])
        ];

        int passed = 0;
        foreach (TestCase testCase in testCases)
        {
            IList<int> actual = FindAnagrams(testCase.S, testCase.P);
            bool isPassed = testCase.Expected.SequenceEqual(actual);
            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine($"案例: {testCase.Name}");
            Console.WriteLine($"s: \"{testCase.S}\"");
            Console.WriteLine($"p: \"{testCase.P}\"");
            Console.WriteLine($"Expected: {FormatIndices(testCase.Expected)}");
            Console.WriteLine($"Actual: {FormatIndices(actual)}");
            Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 將一組非 null 整數索引依原列舉順序格式化，不修改來源資料。
    /// 輸入是演算法結果或測試預期索引；輸出為中括號包住的逗號分隔文字，
    /// 空集合則輸出 <c>[]</c>。時間與額外空間複雜度皆為 O(k)。
    /// </summary>
    /// <param name="indices">要格式化的非 null 整數索引序列。</param>
    /// <returns>例如 <c>[0, 6]</c> 的穩定顯示文字。</returns>
    private static string FormatIndices(IEnumerable<int> indices)
    {
        return $"[{string.Join(", ", indices)}]";
    }

    /// <summary>
    /// 尋找 <paramref name="s"/> 中所有與 <paramref name="p"/> 互為字母異位詞的子字串起始索引。
    /// 解法以 26 格陣列記錄目標字母的剩餘配額，並使用左右指針維護不含超額字母的滑動視窗；
    /// 當視窗長度等於 <paramref name="p"/> 長度時，即得到一組有效答案。
    /// 輸入預期符合題目限制：兩個字串皆只含小寫英文字母，且 <paramref name="p"/> 非空；
    /// 若 <paramref name="s"/> 為空或比 <paramref name="p"/> 短，回傳空集合。
    /// 時間複雜度為 O(|s| + |p|)，輔助空間為 O(1)，結果空間為 O(k)。
    /// </summary>
    /// <param name="s">要搜尋的來源字串，題目限制長度為 1 至 30000。</param>
    /// <param name="p">非空的目標字串，題目限制長度為 1 至 30000。</param>
    /// <returns>所有異位詞子字串的起始索引，依在 <paramref name="s"/> 中出現的順序排列。</returns>
    public static IList<int> FindAnagrams(string s, string p)
    {
        List<int> result = new List<int>();
        if (string.IsNullOrEmpty(s) || s.Length < p.Length)
        {
            return result;
        }

        // 每個位置代表目標字母尚未被目前視窗消耗的配額。
        int[] count = new int[26];
        foreach (char c in p)
        {
            count[c - 'a']++;
        }

        int left = 0;
        for (int right = 0; right < s.Length; right++)
        {
            int currentChar = s[right] - 'a';
            count[currentChar]--;

            // 新加入的字母超出目標需求時，從左側歸還配額，直到視窗重新有效。
            while (count[currentChar] < 0)
            {
                count[s[left] - 'a']++;
                left++;
            }

            // 有效視窗若長度也等於 p，字母數量與 p 必然完全相同。
            if (right - left + 1 == p.Length)
            {
                result.Add(left);
            }
        }

        return result;
    }

    private sealed record TestCase(string Name, string S, string P, int[] Expected);
}
