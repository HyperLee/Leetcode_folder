namespace leetcode_3136;

class Program
{
    /// <summary>
    /// 3136. Valid Word
    /// https://leetcode.com/problems/valid-word/description/?envType=daily-question&envId=2025-07-15
    /// 3136. 有效單詞
    /// https://leetcode.cn/problems/valid-word/description/?envType=daily-question&envId=2025-07-15
    /// 
    /// 題目描述：
    /// 一個單詞被認為是有效的，需滿足：
    /// 1. 至少包含 3 個字元。
    /// 2. 僅包含數字 (0-9) 與英文字母（大小寫）。
    /// 3. 至少包含一個母音字母（a, e, i, o, u 及其大寫）。
    /// 4. 至少包含一個子音字母（非母音的英文字母）。
    /// 給定一個字串 word，若有效則回傳 true，否則回傳 false。
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
