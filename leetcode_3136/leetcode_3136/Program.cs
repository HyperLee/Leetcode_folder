namespace leetcode_3136;

class Program
{
    /// <summary>
    /// 3136. Valid Word
    /// https://leetcode.com/problems/valid-word/description/
    /// <para>
    /// A word is considered valid if:
    /// - It contains at least 3 characters.
    /// - It contains only digits (0-9) and English letters (uppercase and lowercase).
    /// - It includes at least one vowel.
    /// - It includes at least one consonant.
    ///
    /// You are given a string word. Return true if word is valid; otherwise, return false.
    ///
    /// Notes:
    /// - 'a', 'e', 'i', 'o', 'u', and their uppercase forms are vowels.
    /// - A consonant is an English letter that is not a vowel.
    ///
    /// Example 1:
    /// Input: word = "234Adas"
    /// Output: true
    /// Explanation: This word satisfies all conditions.
    ///
    /// Example 2:
    /// Input: word = "b3"
    /// Output: false
    /// Explanation: The word has fewer than 3 characters and has no vowel.
    ///
    /// Example 3:
    /// Input: word = "a3$e"
    /// Output: false
    /// Explanation: The word contains '$' and has no consonant.
    ///
    /// Constraints:
    /// - 1 &lt;= word.length &lt;= 20
    /// - word consists of uppercase and lowercase English letters, digits, '@', '#', and '$'.
    /// </para>
    /// <para>
    /// 3136. 有效單字
    /// https://leetcode.cn/problems/valid-word/description/
    ///
    /// 若單字符合下列條件，就視為有效：
    /// - 至少包含 3 個字元。
    /// - 只包含數字（0-9）與英文字母（大寫及小寫）。
    /// - 至少包含一個母音。
    /// - 至少包含一個子音。
    ///
    /// 給定字串 word。若 word 有效則回傳 true，否則回傳 false。
    ///
    /// 注意事項：
    /// - 'a'、'e'、'i'、'o'、'u' 及其大寫形式都是母音。
    /// - 子音是非母音的英文字母。
    ///
    /// 範例 1：
    /// 輸入：word = "234Adas"
    /// 輸出：true
    /// 解釋：此單字符合所有條件。
    ///
    /// 範例 2：
    /// 輸入：word = "b3"
    /// 輸出：false
    /// 解釋：此單字少於 3 個字元，且不含母音。
    ///
    /// 範例 3：
    /// 輸入：word = "a3$e"
    /// 輸出：false
    /// 解釋：此單字含有 '$' 字元，且不含子音。
    ///
    /// 限制條件：
    /// - 1 &lt;= word.length &lt;= 20
    /// - word 由大小寫英文字母、數字、'@'、'#' 和 '$' 組成。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例測試
        var words = new[] { "abc", "a1b2c3", "aei", "bcd", "Aei1", "12aB", "a2b", "a2", "UuE6" };
        foreach (var word in words)
        {
            Console.WriteLine($"{word}: {IsValidWord(word)}");
        }
    }

    /// <summary>
    /// 判斷單字是否有效。
    /// 
    /// 用個迴圈一次性遍歷就好, 不需要把條件分開判斷跑迴圈。
    /// </summary>
    /// <param name="word">待檢查的字串</param>
    /// <returns>若有效則回傳 true，否則回傳 false。</returns>
    public static bool IsValidWord(string word)
    {
        // 檢查輸入是否為 null 或長度小於 3，若是則直接回傳 false
        if (word is null || word.Length < 3)
        {
            return false;
        }

        bool hasVowel = false;      // 是否包含母音字母
        bool hasConsonant = false;  // 是否包含子音字母
        foreach (var ch in word)
        {
            // 若字元不是英文字母或數字，則不合法，直接回傳 false
            if (!char.IsLetterOrDigit(ch))
            {
                return false;
            }
            // 判斷是否為母音字母
            if (IsVowel(ch))
            {
                hasVowel = true;
            }
            // 若是英文字母且不是母音，則為子音
            else if (char.IsLetter(ch))
            {
                hasConsonant = true;
            }
        }
        // 最終需同時包含母音與子音才算有效
        return hasVowel && hasConsonant;
    }

    /// <summary>
    /// 判斷字元是否為母音字母。
    /// 母音部分大小寫皆可以，此處統一轉大寫就不用把母音全列出來大小寫了簡化判斷。
    /// </summary>
    /// <param name="ch">待檢查的字元</param>
    /// <returns>若為母音則回傳 true。</returns>
    private static bool IsVowel(char ch)
    {
        // 統一轉成大寫，簡化判斷
        char upper = char.ToUpperInvariant(ch);
        return upper is 'A' or 'E' or 'I' or 'O' or 'U';
    }
}
