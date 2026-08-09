namespace leetcode_3330;

class Program
{
    /// <summary>
    /// 3330. Find the Original Typed String I
    /// https://leetcode.com/problems/find-the-original-typed-string-i/description/
    /// <para>
    /// Alice is trying to type a specific string on her computer. However, she may press a key too long, causing a character to be typed multiple times.
    ///
    /// Alice knows this may have happened at most once.
    ///
    /// You are given word, the final output shown on Alice's screen.
    ///
    /// Return the total number of possible original strings Alice might have intended to type.
    ///
    /// Example 1:
    /// Input: word = "abbcccc"
    /// Output: 5
    /// Explanation: The possible strings are "abbcccc", "abbccc", "abbcc", "abbc", and "abcccc".
    ///
    /// Example 2:
    /// Input: word = "abcd"
    /// Output: 1
    /// Explanation: The only possible string is "abcd".
    ///
    /// Example 3:
    /// Input: word = "aaaa"
    /// Output: 4
    ///
    /// Constraints:
    /// - 1 &lt;= word.length &lt;= 100
    /// - word consists only of lowercase English letters.
    /// </para>
    /// <para>
    /// 3330. 找出原始輸入字串 I
    /// https://leetcode.cn/problems/find-the-original-typed-string-i/description/
    ///
    /// Alice 正嘗試在電腦上輸入某個特定字串。然而，她可能因為按鍵時間過長，使某個字元被輸入多次。
    ///
    /// Alice 知道這種情況最多可能發生一次。
    ///
    /// 給定 word，表示 Alice 螢幕上顯示的最終輸出。
    ///
    /// 回傳 Alice 可能原本想輸入的原始字串總數。
    ///
    /// 範例 1：
    /// 輸入：word = "abbcccc"
    /// 輸出：5
    /// 解釋：可能的字串為 "abbcccc"、"abbccc"、"abbcc"、"abbc" 與 "abcccc"。
    ///
    /// 範例 2：
    /// 輸入：word = "abcd"
    /// 輸出：1
    /// 解釋：唯一可能的字串是 "abcd"。
    ///
    /// 範例 3：
    /// 輸入：word = "aaaa"
    /// 輸出：4
    ///
    /// 限制條件：
    /// - 1 &lt;= word.length &lt;= 100
    /// - word 只由小寫英文字母組成。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();
        string[] testCases = { "aabb", "abc", "aabbaa", "a", "aa", "abcc" };
        foreach (var word in testCases)
        {
            int result = program.PossibleStringCount(word);
            Console.WriteLine($"word: {word}, 可能原始字串總數: {result}");
        }
    }

    /// <summary>
    /// 計算 Alice 可能原本想輸入的原始字串總數
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public int PossibleStringCount(string word)
    {
        int n = word.Length;
        int res = 1;
        for (int i = 1; i < n; i++)
        {
            if (word[i] == word[i - 1])
            {
                res++;
            }
        }

        return res;
    }
}
