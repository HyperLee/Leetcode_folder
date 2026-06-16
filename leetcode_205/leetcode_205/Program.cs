namespace leetcode_205;

class Program
{
    /// <summary>
    /// 205. Isomorphic Strings
    /// https://leetcode.com/problems/isomorphic-strings/description/
    /// 205. 同構字串
    /// https://leetcode.cn/problems/isomorphic-strings/description/
    ///
    /// Given two strings s and t, determine if they are isomorphic.
    /// Two strings s and t are isomorphic if the characters in s can be replaced to get t.
    /// All occurrences of a character must be replaced with another character while preserving the order of characters.
    /// No two characters may map to the same character, but a character may map to itself.
    ///
    /// 給定兩個字串 s 與 t，判斷它們是否為同構字串。
    /// 如果可以將 s 中的字元逐一替換後得到 t，則 s 和 t 為同構字串。
    /// 同一個字元在所有出現的位置都必須替換成同一個字元，並且要維持字元順序不變。
    /// 不同字元不能對應到同一個字元，但字元可以對應到自己。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        program.RunSamples();
    }

    /// <summary>
    /// 使用固定案例逐一驗證三種同構字串解法，讓主程式直接輸出可比對的測試結果。
    /// 核心概念是以同一份測資驅動所有解法，確認不同策略的答案是否一致。
    /// 輸入條件為內建的合法字串案例；輸出結果為主控台上的格式化測試報告。
    /// </summary>
    private void RunSamples()
    {
        SampleCase[] sampleCases =
        [
            new SampleCase("Case 1", "最基本的一對一映射", "egg", "add", true),
            new SampleCase("Case 2", "同一字元對到不同字元，應判定失敗", "foo", "bar", false),
            new SampleCase("Case 3", "經典重複模式完全一致的成功案例", "paper", "title", true),
            new SampleCase("Case 4", "不同來源字元映射到同一目標字元，違反雙射", "badc", "baba", false),
            new SampleCase("Case 5", "每個字元都映射到自己也算同構", "abc", "abc", true),
            new SampleCase("Case 6", "同字元若對到不同目標字元就不是同構", "aa", "ab", false)
        ];

        int passedCount = 0;

        Console.WriteLine("LeetCode 205 - Isomorphic Strings");
        Console.WriteLine("==================================================");

        foreach (SampleCase sampleCase in sampleCases)
        {
            if (RunSample(sampleCase))
            {
                passedCount++;
            }
        }

        Console.WriteLine($"Summary: {passedCount}/{sampleCases.Length} cases passed.");
    }

    /// <summary>
    /// 執行單一案例，分別呼叫三種解法並輸出預期值、實際值與 PASS 或 FAIL。
    /// 核心概念是將案例驗證與輸出格式集中處理，避免 Main 充滿重複程式碼。
    /// 輸入條件為題目合法字串與預期布林結果；輸出結果為單一案例的驗證摘要。
    /// </summary>
    /// <param name="sampleCase">包含案例名稱、測試目的、輸入字串與預期結果的固定測資。</param>
    /// <returns>若三種解法都符合預期結果則回傳 <c>true</c>，否則回傳 <c>false</c>。</returns>
    private bool RunSample(SampleCase sampleCase)
    {
        bool result1 = IsIsomorphic(sampleCase.Source, sampleCase.Target);
        bool result2 = IsIsomorphic2(sampleCase.Source, sampleCase.Target);
        bool result3 = IsIsomorphic3(sampleCase.Source, sampleCase.Target);
        bool allPassed = result1 == sampleCase.Expected
            && result2 == sampleCase.Expected
            && result3 == sampleCase.Expected;

        Console.WriteLine($"{sampleCase.Name} - {sampleCase.Purpose}");
        Console.WriteLine($"Input: s = \"{sampleCase.Source}\", t = \"{sampleCase.Target}\"");
        Console.WriteLine($"Expected: {FormatBoolean(sampleCase.Expected)}");
        Console.WriteLine($"IsIsomorphic: {FormatResult(result1, sampleCase.Expected)}");
        Console.WriteLine($"IsIsomorphic2: {FormatResult(result2, sampleCase.Expected)}");
        Console.WriteLine($"IsIsomorphic3: {FormatResult(result3, sampleCase.Expected)}");
        Console.WriteLine($"Overall: {(allPassed ? "PASS" : "FAIL")}");
        Console.WriteLine();

        return allPassed;
    }

    /// <summary>
    /// 將布林結果轉成 README 與主控台都容易閱讀的小寫字串表示。
    /// 核心概念是統一輸出格式，避免不同區段出現大小寫不一致的結果文字。
    /// 輸入條件為任一布林值；輸出結果為 <c>true</c> 或 <c>false</c> 的小寫字串。
    /// </summary>
    /// <param name="value">要格式化的布林結果。</param>
    /// <returns>對應的小寫布林字串。</returns>
    private static string FormatBoolean(bool value)
    {
        return value ? "true" : "false";
    }

    /// <summary>
    /// 將單一解法的實際結果包裝成「答案 + PASS 或 FAIL」的對照輸出。
    /// 核心概念是集中處理驗證文字，讓每種解法的輸出格式保持一致。
    /// 輸入條件為實際布林值與預期布林值；輸出結果為含驗證狀態的格式化字串。
    /// </summary>
    /// <param name="actual">某個解法的實際回傳值。</param>
    /// <param name="expected">案例預期的正確回傳值。</param>
    /// <returns>例如 <c>true | PASS</c> 或 <c>false | FAIL</c> 的對照字串。</returns>
    private static string FormatResult(bool actual, bool expected)
    {
        return $"{FormatBoolean(actual)} | {(actual == expected ? "PASS" : "FAIL")}";
    }

    /// <summary>
    /// 使用「每個字元第一次出現的位置」建立模式序列，比較兩字串的重複結構是否一致。
    /// 核心概念是把字元映射問題轉成索引模式比對；輸入需為非 null 字串，長度不同直接回傳 <c>false</c>。
    /// 若兩字串的首次出現索引序列完全相同則回傳 <c>true</c>，否則回傳 <c>false</c>。
    /// </summary>
    /// <param name="s">來源字串，預期為 LeetCode 題目提供的合法輸入。</param>
    /// <param name="t">目標字串，預期為 LeetCode 題目提供的合法輸入。</param>
    /// <returns>若兩字串具有相同的重複模式則回傳 <c>true</c>，否則回傳 <c>false</c>。</returns>
    public bool IsIsomorphic(string s, string t)
    {
        if (s.Length != t.Length)
        {
            return false;
        }

        List<int> indexS = new List<int>();
        List<int> indexT = new List<int>();

        for (int i = 0; i < s.Length; i++)
        {
            // 首次出現位置序列一致，代表兩字串的重複模式一致。
            indexS.Add(s.IndexOf(s[i]));
            indexT.Add(t.IndexOf(t[i]));
        }

        return indexS.SequenceEqual(indexT);
    }

    /// <summary>
    /// 使用兩張 Dictionary 同步維護 s 對 t 與 t 對 s 的映射，驗證是否形成一對一雙射。
    /// 核心概念是雙向檢查可同時擋下一對多與多對一；輸入需為非 null 字串，長度不同直接回傳 <c>false</c>。
    /// 若每個位置的雙向映射都沒有衝突則回傳 <c>true</c>，否則回傳 <c>false</c>。
    /// </summary>
    /// <param name="s">來源字串，預期為 LeetCode 題目提供的合法輸入。</param>
    /// <param name="t">目標字串，預期為 LeetCode 題目提供的合法輸入。</param>
    /// <returns>若雙向映射全程一致則回傳 <c>true</c>，否則回傳 <c>false</c>。</returns>
    public bool IsIsomorphic2(string s, string t)
    {
        if (s.Length != t.Length)
        {
            return false;
        }

        Dictionary<char, char> sTot = new Dictionary<char, char>();
        Dictionary<char, char> tTos = new Dictionary<char, char>();

        for (int i = 0; i < s.Length; i++)
        {
            char x = s[i];
            char y = t[i];

            // 正反兩張表都要一致，才能保證映射是雙射而不是多對一。
            if (sTot.TryGetValue(x, out char mappedY) && mappedY != y)
            {
                return false;
            }

            if (tTos.TryGetValue(y, out char mappedX) && mappedX != x)
            {
                return false;
            }

            sTot[x] = y;
            tTos[y] = x;
        }

        return true;
    }

    /// <summary>
    /// 使用單張 Dictionary 記錄 s 到 t 的映射，並在建立新映射前確認目標字元尚未被其他來源字元占用。
    /// 核心概念是以單向表搭配值唯一性檢查模擬雙射；輸入需為非 null 字串，長度不同直接回傳 <c>false</c>。
    /// 若所有已建立的映射都穩定且沒有重複占用目標字元則回傳 <c>true</c>，否則回傳 <c>false</c>。
    /// </summary>
    /// <param name="s">來源字串，預期為 LeetCode 題目提供的合法輸入。</param>
    /// <param name="t">目標字串，預期為 LeetCode 題目提供的合法輸入。</param>
    /// <returns>若單表映射與目標字元唯一性都成立則回傳 <c>true</c>，否則回傳 <c>false</c>。</returns>
    public bool IsIsomorphic3(string s, string t)
    {
        if (s.Length != t.Length)
        {
            return false;
        }

        Dictionary<char, char> pairs = new Dictionary<char, char>();

        for (int i = 0; i < s.Length; i++)
        {
            if (!pairs.ContainsKey(s[i]))
            {
                // 單表只能記錄 s -> t，因此新配對前要先確認 t[i] 尚未被其他來源字元使用。
                if (pairs.ContainsValue(t[i]))
                {
                    return false;
                }

                pairs.Add(s[i], t[i]);
            }
            else if (t[i] != pairs[s[i]])
            {
                return false;
            }
        }

        return true;
    }

    private readonly record struct SampleCase(
        string Name,
        string Purpose,
        string Source,
        string Target,
        bool Expected);
}
