namespace leetcode_953;

class Program
{
    /// <summary>
    /// 953. Verifying an Alien Dictionary
    /// https://leetcode.com/problems/verifying-an-alien-dictionary/description/
    /// 953. 驗證外星語詞典
    /// https://leetcode.cn/problems/verifying-an-alien-dictionary/description/
    ///
    /// In an alien language, surprisingly, they also use English lowercase letters,
    /// but possibly in a different order. The order of the alphabet is some permutation of lowercase letters.
    /// Given a sequence of words written in the alien language, and the order of the alphabet,
    /// return true if and only if the given words are sorted lexicographically in this alien language.
    ///
    /// 在外星語言中，令人驚訝的是，他們也使用英文小寫字母，但可能以不同的順序排列。
    /// 字母順序是小寫字母的某種排列。
    /// 給定一個用外星語言撰寫的單詞序列，以及字母的順序，
    /// 當且僅當給定的單詞在此外星語言中按字典序排序時，返回 true。
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1：依照 order 字典序，"hello" < "leetcode"，回傳 true
        string[] words1 = { "hello", "leetcode" };
        string order1 = "hlabcdefgijkmnopqrstuvwxyz";
        Console.WriteLine($"Test 1 expected True, got: {program.IsAlienSorted(words1, order1)}");

        // 測試案例 2：在 order 中 'd' 的排序大於 'l'，因此 "word" > "world"，回傳 false
        string[] words2 = { "word", "world", "row" };
        string order2 = "worldabcefghijkmnpqstuvxyz";
        Console.WriteLine($"Test 2 expected False, got: {program.IsAlienSorted(words2, order2)}");

        // 測試案例 3：前綴相同但前一字串較長 ("apple" > "app")，回傳 false
        string[] words3 = { "apple", "app" };
        string order3 = "abcdefghijklmnopqrstuvwxyz";
        Console.WriteLine($"Test 3 expected False, got: {program.IsAlienSorted(words3, order3)}");

        // 測試案例 4：單一單字必為已排序，回傳 true
        string[] words4 = { "leetcode" };
        string order4 = "abcdefghijklmnopqrstuvwxyz";
        Console.WriteLine($"Test 4 expected True, got: {program.IsAlienSorted(words4, order4)}");

        // 測試案例 5：前綴相同且前者較短 ("app" < "apple")，回傳 true
        string[] words5 = { "app", "apple" };
        string order5 = "abcdefghijklmnopqrstuvwxyz";
        Console.WriteLine($"Test 5 expected True, got: {program.IsAlienSorted(words5, order5)}");
    }

    /// <summary>
    /// 方法一：直接遍歷
    ///
    /// 解題思路：
    /// 題目要求按照給定的字母表 <paramref name="order"/> 的順序，檢測 <paramref name="words"/>
    /// 是否按照外星字母表的字典升序排列。我們只需要依序比較相鄰兩個字串的字典序即可。
    ///
    /// 具體步驟：
    /// 1. 建立一個長度為 26 的索引陣列 index，將 order 轉換為「字元 → 排序位置」的映射，
    ///    index[c - 'a'] 即為字元 c 在外星字母表中的排序位置。
    ///    比較兩字元字典序時，只需比較它們對應的 index 值大小。
    /// 2. 依序比對 words[i] 與 words[i - 1]：
    ///    從左到右逐字元比較，第一次遇到兩字元不同時即可決定兩字串字典序大小：
    ///      - prev &lt; curr：前者較小，已正確排序，停止比較此組。
    ///      - prev &gt; curr：前者較大，違反字典序，直接回傳 false。
    /// 3. 特殊情況：若兩字串前綴完全相同（在共同長度內字元皆相等），
    ///    當 words[i - 1] 長度大於 words[i] 時（如 "apple" 與 "app"），
    ///    依字典序定義 words[i - 1] 較大，回傳 false。
    ///
    /// 時間複雜度：O(N * M)，其中 N 為單字數量，M 為單字平均長度。
    /// 空間複雜度：O(1)，僅使用固定大小 26 的索引陣列。
    /// </summary>
    /// <param name="words">待驗證的外星語單字陣列。</param>
    /// <param name="order">外星字母表，為 26 個小寫字母的排列。</param>
    /// <returns>若 <paramref name="words"/> 在外星字母表下為字典升序則回傳 true，否則回傳 false。</returns>
    public bool IsAlienSorted(string[] words, string order)
    {
        // index[c - 'a']：字元 c 在外星字母表中的排序位置（0 ~ 25）
        int[] index = new int[26];

        // 將 order 轉成索引陣列：order 中第 i 個字元在外星字典中的排名為 i
        // 例如 order = "hlabcdefg..."，則 index['h' - 'a'] = 0, index['l' - 'a'] = 1, ...
        for (int i = 0; i < order.Length; i++)
        {
            index[order[i] - 'a'] = i;
        }

        // 依序檢查相鄰兩個單字 words[i - 1] 與 words[i] 的字典序
        for (int i = 1; i < words.Length; i++)
        {
            // valid 表示是否在共同長度內已經透過某個不同字元判定 words[i - 1] < words[i]
            bool valid = false;

            // 在兩字串共同長度內，逐字元比較其在外星字母表中的字典序
            for (int j = 0; j < words[i - 1].Length && j < words[i].Length; j++)
            {
                int prev = index[words[i - 1][j] - 'a']; // 前一個單字第 j 個字元的排名
                int curr = index[words[i][j] - 'a'];     // 後一個單字第 j 個字元的排名

                if (prev < curr)
                {
                    // 前者字典序較小，符合升序，無需再比較此組後續字元
                    valid = true;
                    break;
                }
                else if (prev > curr)
                {
                    // 前者字典序較大，違反升序，直接回傳 false
                    return false;
                }
                // prev == curr 時繼續比較下一個字元
            }

            // 走完共同長度仍未分出大小（即前綴相同）
            if (!valid)
            {
                // 此情境若前一個單字較長（如 "apple" vs "app"），
                // 依字典序定義前者較大，違反升序
                if (words[i - 1].Length > words[i].Length)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
