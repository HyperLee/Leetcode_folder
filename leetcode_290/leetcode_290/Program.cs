namespace leetcode_290;

class Program
{
    /// <summary>
    /// 290. Word Pattern
    /// https://leetcode.com/problems/word-pattern/description/
    /// 290. 單詞規律
    /// https://leetcode.cn/problems/word-pattern/description/
    ///
    /// English:
    /// Given a pattern and a string s, find if s follows the same pattern.
    /// Here follow means a full match, such that there is a bijection between a letter in pattern
    /// and a non-empty word in s. Specifically:
    /// Each letter in pattern maps to exactly one unique word in s.
    /// Each unique word in s maps to exactly one letter in pattern.
    /// No two letters map to the same word, and no two words map to the same letter.
    ///
    /// 繁體中文:
    /// 給定一個模式 pattern 和一個字串 s，判斷 s 是否遵循相同的模式。
    /// 這裡的遵循表示完整匹配，也就是 pattern 中的字母與 s 中的非空單字之間存在雙射。具體來說：
    /// pattern 中的每個字母都只對應到 s 中唯一的一個單字。
    /// s 中的每個唯一單字都只對應到 pattern 中唯一的一個字母。
    /// 不能有兩個字母對應到同一個單字，也不能有兩個單字對應到同一個字母。
    /// </summary>
    /// <remarks>
    /// 主程式會呼叫 <see cref="RunSamples"/>，以固定測資展示雙向 Dictionary 解法是否能正確判斷 pattern 與單字之間的雙射關係。
    /// </remarks>
    /// <param name="args">命令列參數；本範例程式不需要使用。</param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行固定範例測資，展示 <see cref="WordPattern"/> 在符合、長度不一致、正向對應衝突與反向對應衝突時的輸出結果。
    /// </summary>
    /// <remarks>
    /// 每筆測資都會列出 pattern、輸入字串、預期值與實際值，最後輸出通過筆數，方便直接用 <c>dotnet run</c> 驗證。
    /// </remarks>
    private static void RunSamples()
    {
        List<SampleCase> samples =
        [
            new SampleCase("官方範例 1 - 符合 abba 對應", "abba", "dog cat cat dog", true),
            new SampleCase("官方範例 2 - a 第二次對到不同單字", "abba", "dog cat cat fish", false),
            new SampleCase("官方範例 3 - 同一字母不能對到不同單字", "aaaa", "dog cat cat dog", false),
            new SampleCase("常見範例 - 不同字母不能共用同一單字", "abba", "dog dog dog dog", false),
            new SampleCase("長度不一致 - pattern 與單字數不同", "abba", "dog cat cat", false),
            new SampleCase("全部唯一 - 每個字母對應不同單字", "abc", "dog cat fish", true),
            new SampleCase("反向衝突 - dog 已經對應到 a", "abc", "dog cat dog", false)
        ];

        Program solution = new Program();
        int passedCount = 0;

        Console.WriteLine("LeetCode 290 - Word Pattern");
        Console.WriteLine("解法：雙向 Dictionary 檢查 pattern 字元與單字是否形成雙射");
        Console.WriteLine();

        for (int i = 0; i < samples.Count; i++)
        {
            SampleCase sample = samples[i];
            bool actual = solution.WordPattern(sample.Pattern, sample.S);
            bool passed = actual == sample.Expected;

            if (passed)
            {
                passedCount++;
            }

            Console.WriteLine($"案例 {i + 1}：{sample.Description}");
            Console.WriteLine($"pattern: \"{sample.Pattern}\"");
            Console.WriteLine($"s: \"{sample.S}\"");
            Console.WriteLine($"預期：{FormatBoolean(sample.Expected)}，實際：{FormatBoolean(actual)} => {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedCount}/{samples.Count} 筆測試通過");
    }

    /// <summary>
    /// 判斷字串 <paramref name="s"/> 是否完全遵循 <paramref name="pattern"/> 指定的字母模式。
    /// </summary>
    /// <remarks>
    /// 解題概念是建立兩個方向的對應表：pattern 字元到單字，以及單字到 pattern 字元。只要任一方向出現既有對應不一致，就代表雙射關係被破壞。
    /// 輸入需符合題目條件：<paramref name="pattern"/> 由小寫英文字母組成，<paramref name="s"/> 由以單一空白分隔的非空單字組成。
    /// </remarks>
    /// <param name="pattern">要比對的字母模式；每個字元都必須對應到一個唯一單字。</param>
    /// <param name="s">以空白分隔的單字字串；每個唯一單字也只能對應到一個 pattern 字元。</param>
    /// <returns>若 pattern 字元與單字之間存在完整的一對一雙射則回傳 <c>true</c>；否則回傳 <c>false</c>。</returns>
    public bool WordPattern(string pattern, string s)
    {
        Dictionary<char, string> patternToWord = new Dictionary<char, string>();
        Dictionary<string, char> wordToPattern = new Dictionary<string, char>();
        string[] words = s.Split(' ');

        // 必須完整匹配：pattern 字元數與單字數不同時，不可能形成一對一關係。
        if (words.Length != pattern.Length)
        {
            return false;
        }

        for (int i = 0; i < pattern.Length; i++)
        {
            char patternCharacter = pattern[i];
            string word = words[i];

            // 正向與反向都要一致，才能避免「一個字母對多個單字」或「多個字母共用同一單字」。
            if (patternToWord.TryGetValue(patternCharacter, out string? mappedWord))
            {
                if (mappedWord != word)
                {
                    return false;
                }
            }
            else
            {
                patternToWord[patternCharacter] = word;
            }

            if (wordToPattern.TryGetValue(word, out char mappedPatternCharacter))
            {
                if (mappedPatternCharacter != patternCharacter)
                {
                    return false;
                }
            }
            else
            {
                wordToPattern[word] = patternCharacter;
            }
        }

        return true;
    }

    /// <summary>
    /// 將布林值轉為小寫文字，讓主控台輸出與 LeetCode 範例慣用的 true/false 表示一致。
    /// </summary>
    /// <param name="value">要格式化的布林值。</param>
    /// <returns>若 <paramref name="value"/> 為 true 則回傳 <c>true</c>；否則回傳 <c>false</c>。</returns>
    private static string FormatBoolean(bool value)
    {
        return value ? "true" : "false";
    }

    /// <summary>
    /// 表示一筆可執行範例，包含案例說明、pattern、輸入字串與預期布林結果。
    /// </summary>
    /// <param name="Description">案例目的或要覆蓋的判斷情境。</param>
    /// <param name="Pattern">要傳入 <see cref="WordPattern"/> 的 pattern。</param>
    /// <param name="S">要傳入 <see cref="WordPattern"/> 的空白分隔單字字串。</param>
    /// <param name="Expected">此案例預期得到的判斷結果。</param>
    private sealed record SampleCase(string Description, string Pattern, string S, bool Expected);
}
