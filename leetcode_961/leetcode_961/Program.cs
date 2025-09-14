namespace leetcode_961;

/// <summary>
/// 程式主類別：實作 LeetCode 966. Vowel Spellchecker 的解法。
///
/// 解題概念：使用三種哈希資料結構來對應題目要求的三類比對優先順序：
///  1) 完全匹配 (區分大小寫)：使用 HashSet 存放原始單字以 O(1) 查詢。
///  2) 忽略大小寫匹配：使用 Dictionary 將單字的小寫形式對應到字詞表中第一個出現的原始單字。
///  3) 忽略元音（且忽略大小寫）匹配：先將單字的小寫形式把元音置換成占位符（例如 '*'），再用 Dictionary 對應到原始單字。
///
/// 優先順序在查詢時會依序檢查以上 1->2->3，若都不符合則回傳空字串。
///
/// 注意：字詞表中若有多個在忽略大小寫或忽略元音後相同的單字，要回傳字詞表中第一個出現的原始單字，
/// 因此在建立 Dictionary 時只在鍵不存在時寫入對應值，保證先出現的單字被保留。
/// </summary>
class Program
{
    /// <summary>
    /// 966. Vowel Spellchecker
    /// https://leetcode.com/problems/vowel-spellchecker/description/?envType=daily-question&envId=2025-09-14
    /// 966. 元音拼寫檢查器
    /// https://leetcode.cn/problems/vowel-spellchecker/description/?envType=daily-question&envId=2025-09-14
    ///
    /// 題目說明（中文翻譯）:
    /// 給定一個字詞表（wordlist），我們要實作一個拼字檢查器，將查詢字串轉換回正確的單字。
    /// 對於每個查詢字，檢查器處理兩類拼寫錯誤：
    ///  1) 大小寫錯誤 (Capitalization)：
    ///     - 若查詢字在不區分大小寫的情況下與字詞表中的某個字相符，則回傳字詞表中對應的那個字（保留原始大小寫）。
    ///     - 範例：wordlist = ["yellow"], query = "YellOw" -> 回傳 "yellow"
    ///     - 範例：wordlist = ["Yellow"], query = "yellow" -> 回傳 "Yellow"
    ///  2) 元音錯誤 (Vowel Errors)：
    ///     - 若將查詢字中的元音字母（'a','e','i','o','u'）替換成任意元音後（逐字元獨立替換），在不區分大小寫的情況下能與字詞表某字相符，
    ///       則回傳字詞表中對應的那個字（保留原始大小寫）。
    ///     - 範例：wordlist = ["YellOw"], query = "yollow" -> 回傳 "YellOw"
    ///     - 範例：wordlist = ["YellOw"], query = "yeellow" -> 無相符，回傳空字串
    ///     - 範例：wordlist = ["YellOw"], query = "yllw" -> 無相符，回傳空字串
    ///
    /// 優先順序規則：
    ///  1) 若查詢字與字詞表中某字完全相符（區分大小寫），回傳該字。
    ///  2) 若查詢字在不區分大小寫下相符，回傳字詞表中第一個符合此條件的字。
    ///  3) 若查詢字在忽略元音差異（並不區分大小寫）下相符，回傳字詞表中第一個符合此條件的字。
    ///  4) 若皆無符合，回傳空字串。
    ///
    /// 輸入/輸出：給定一個字詞表和多個查詢字，回傳一個陣列 answer，answer[i] 是 queries[i] 的修正結果。
    ///
    /// 注意：本檔案只包含題目描述與範例說明；實際演算法實作可於其他方法中補上。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 建立 Program 實例以呼叫非靜態的 Spellchecker
        Program solver = new Program();

        // 範例測試 1：LeetCode 題目常見測資
        string[] wordlist1 = new string[] { "KiTe", "kite", "hare", "Hare" };
        string[] queries1 = new string[] { "kite", "Kite", "KiTe", "Hare", "HARE", "Hear", "hear", "keti", "keet", "keto" };
        string[] ans1 = solver.Spellchecker(wordlist1, queries1);

        Console.WriteLine("=== Test 1 ===");
        for (int i = 0; i < queries1.Length; i++)
        {
            Console.WriteLine($"query: {queries1[i]} -> result: {ans1[i]}");
        }

        // 範例測試 2：簡單的大小寫/元音檢查
        string[] wordlist2 = new string[] { "yellow" };
        string[] queries2 = new string[] { "YellOw", "yellow", "YEllow", "yollow", "yllw" };
        string[] ans2 = solver.Spellchecker(wordlist2, queries2);

        Console.WriteLine();
        Console.WriteLine("=== Test 2 ===");
        for (int i = 0; i < queries2.Length; i++)
        {
            Console.WriteLine($"query: {queries2[i]} -> result: {ans2[i]}");
        }
    }

    // 儲存原始單字，用以處理「完全匹配 (case-sensitive)」情況
    private readonly HashSet<string> wordsPerfect = new HashSet<string>();

    // key: 單字小寫形式 -> value: 字詞表中第一個以該小寫形式出現的原始單字
    // 用以處理「忽略大小寫但需回傳原始大小寫」的情況
    private readonly Dictionary<string, string> wordsCap = new Dictionary<string, string>();

    // key: 單字小寫且將元音置換為 '*' 的形式 -> value: 字詞表中第一個以該形式出現的原始單字
    // 用以處理「元音錯誤 (vowel errors) 並忽略大小寫」的情況
    private readonly Dictionary<string, string> wordsVow = new Dictionary<string, string>();

    /// <summary>
    /// 主解法：建立三種資料結構來支援三類查詢規則，並依序處理每個查詢。
    /// Inputs:
    ///  - wordlist: 原始字詞表（會用來建立索引）
    ///  - queries: 要查詢並修正的單字陣列
    /// Outputs:
    ///  - 回傳一個與 queries 等長的陣列，ans[i] 為 queries[i] 的修正結果（或空字串表示無匹配）
    ///
    /// 實作重點：
    ///  - words_perfect 用於快速檢查完全匹配（區分大小寫）
    ///  - words_cap 用於忽略大小寫的第一個匹配
    ///  - words_vow 用於忽略元音差異（先把元音轉為 '*'）的第一個匹配
    ///
    /// 時間複雜度：O(N * L + Q * L)，N 為 wordlist 長度，Q 為 queries 長度，L 為單字平均長度。
    /// 空間複雜度：O(N * L)
    /// </summary>
    /// <param name="wordlist">字詞表</param>
    /// <param name="queries">查詢陣列</param>
    /// <returns>修正後的查詢結果陣列</returns>
    public string[] Spellchecker(string[] wordlist, string[] queries)
    {
        // 先建立三個索引結構
        foreach (string word in wordlist)
        {
            // 1) 完全匹配集合（保留原始大小寫）
            wordsPerfect.Add(word);

            // 2) 忽略大小寫的對應：使用小寫字串作為 key，value 存放字詞表中的原始單字
            //    只有當 key 尚未存在時才加入，以保證保留字詞表中第一個出現的原始單字
            string wordlow = word.ToLower();
            if (!wordsCap.ContainsKey(wordlow))
            {
                wordsCap.Add(wordlow, word);
            }

            // 3) 忽略元音差異的對應：先將小寫字串把元音換成 '*'，再作為 key
            //    同樣只在 key 不存在時加入，以保留字詞表中第一個出現的原始單字
            string wordlowDV = Devowel(wordlow);
            if (!wordsVow.ContainsKey(wordlowDV))
            {
                wordsVow.Add(wordlowDV, word);
            }
        }

        // 針對每個查詢執行查找，依優先順序回傳結果
        string[] ans = new string[queries.Length];
        for (int i = 0; i < queries.Length; i++)
        {
            ans[i] = Solve(queries[i]);
        }
        return ans;
    }

    /// <summary>
    /// 對單一查詢字執行三階段匹配：完全匹配 -> 忽略大小寫匹配 -> 忽略元音匹配。
    /// </summary>
    /// <param name="query">要修正的查詢字</param>
    /// <returns>符合優先順序的字詞表原始單字，或空字串代表無匹配</returns>
    private string Solve(string query)
    {
        // 1) 完全匹配（區分大小寫）
        if (wordsPerfect.Contains(query))
        {
            return query;
        }

        // 2) 忽略大小寫匹配：將查詢轉為小寫並檢查 words_cap
        string queryL = query.ToLower();
        if (wordsCap.ContainsKey(queryL))
        {
            return wordsCap[queryL];
        }

        // 3) 忽略元音匹配：將小寫查詢把元音換為 '*' 再檢查 words_vow
        string queryLV = Devowel(queryL);
        if (wordsVow.ContainsKey(queryLV))
        {
            return wordsVow[queryLV];
        }

        // 若以上皆無匹配，回傳空字串
        return string.Empty;
    }

    /// <summary>
    /// 將字串中的元音字母替換為 '*'，以做為忽略元音差異的標準化形式。
    /// 例如："apple" -> "*ppl*"（在傳入前通常會先轉為小寫）
    /// </summary>
    /// <param name="word">輸入字串（建議已為小寫）</param>
    /// <returns>將元音替換為 '*' 後的新字串</returns>
    private string Devowel(string word)
    {
        System.Text.StringBuilder ans = new System.Text.StringBuilder();
        foreach (char c in word)
        {
            ans.Append(IsVowel(c) ? '*' : c);
        }
        return ans.ToString();
    }

    /// <summary>
    /// 判斷字元是否為英文字母元音（a, e, i, o, u）。
    /// 會先將字元小寫化再比較。
    /// </summary>
    /// <param name="c">要判斷的字元</param>
    /// <returns>若為元音回傳 true，否則回傳 false</returns>
    private bool IsVowel(char c)
    {
        c = char.ToLower(c);
        return (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u');
    }
}
