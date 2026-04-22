namespace leetcode_2452;

class Program
{
    /// <summary>
    /// 2452. Words Within Two Edits of Dictionary
    /// https://leetcode.com/problems/words-within-two-edits-of-dictionary/description/?envType=daily-question&envId=2026-04-22
    /// 
    /// Problem Description (English):
    /// You are given two string arrays, queries and dictionary. All words in each array comprise of lowercase English letters and have the same length.
    /// In one edit you can take a word from queries, and change any letter in it to any other letter. Find all words from queries that, after a maximum of two edits, equal some word from dictionary.
    /// Return a list of all words from queries, that match with some word from dictionary after a maximum of two edits. Return the words in the same order they appear in queries.
    /// 
    /// 2452. 距离字典两次编辑以内的单词
    /// https://leetcode.cn/problems/words-within-two-edits-of-dictionary/description/?envType=daily-question&envId=2026-04-22
    /// 
    /// 問題描述（繁體中文）：
    /// 給定兩個字串陣列 queries 和 dictionary。每個陣列中的所有單詞都由小寫英文字母組成，且長度相同。
    /// 在一次編輯中，您可以從 queries 中取一個單詞，並將其中的任何字母更改為任何其他字母。找到 queries 中的所有單詞，這些單詞最多進行兩次編輯後，等於 dictionary 中的某個單詞。
    /// 返回 queries 中的所有單詞的列表，這些單詞最多進行兩次編輯後，與 dictionary 中的某個單詞相匹配。以單詞在 queries 中出現的相同順序返回。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試資料 1：預期輸出 ["word", "note", "wood"]
        string[] queries1 = ["word", "note", "ants", "wood"];
        string[] dictionary1 = ["wood", "joke", "moat"];
        IList<string> result1 = program.TwoEditWords(queries1, dictionary1);
        Console.WriteLine($"Test 1 (暴力解): [{string.Join(", ", result1)}]");
        IList<string> result1Trie = program.TwoEditWordsTrie(queries1, dictionary1);
        Console.WriteLine($"Test 1 (Trie):   [{string.Join(", ", result1Trie)}]");
        IList<string> result1Hash = program.TwoEditWordsHash(queries1, dictionary1);
        Console.WriteLine($"Test 1 (雜湊):   [{string.Join(", ", result1Hash)}]");
        // Expected: [word, note, wood]

        // 測試資料 2：預期輸出 []
        string[] queries2 = ["yes"];
        string[] dictionary2 = ["not"];
        IList<string> result2 = program.TwoEditWords(queries2, dictionary2);
        Console.WriteLine($"Test 2 (暴力解): [{string.Join(", ", result2)}]");
        IList<string> result2Trie = program.TwoEditWordsTrie(queries2, dictionary2);
        Console.WriteLine($"Test 2 (Trie):   [{string.Join(", ", result2Trie)}]");
        IList<string> result2Hash = program.TwoEditWordsHash(queries2, dictionary2);
        Console.WriteLine($"Test 2 (雜湊):   [{string.Join(", ", result2Hash)}]");
        // Expected: []
    }

    /// <summary>
    /// 找出 queries 中，與 dictionary 裡任一單詞的漢明距離（字元差異數）小於等於 2 的所有單詞。
    ///
    /// 解題思路（暴力解）：
    /// 對 queries 中的每個字串，逐一與 dictionary 中的每個字串比較，
    /// 計算兩者在相同位置上不同字元的數量（即漢明距離）。
    /// 若漢明距離 ≤ 2，表示最多只需兩次編輯即可讓 query 等於該 dictionary 單詞，
    /// 則將此 query 加入結果並跳出內層迴圈。
    ///
    /// 時間複雜度：O(n × m × L)，n = queries 長度，m = dictionary 長度，L = 字串長度
    /// 空間複雜度：O(1)（不含輸出結果）
    /// </summary>
    /// <param name="queries">查詢字串陣列</param>
    /// <param name="dictionary">字典字串陣列</param>
    /// <returns>所有符合條件（漢明距離 ≤ 2）的查詢字串列表</returns>
    public IList<string> TwoEditWords(string[] queries, string[] dictionary)
    {
        IList<string> result = new List<string>();

        foreach (string query in queries)
        {
            foreach (string dictWord in dictionary)
            {
                // 計算 query 與 dictWord 在相同索引位置上不同字元的個數（漢明距離）
                int diffCount = 0;
                for (int i = 0; i < query.Length; i++)
                {
                    if (query[i] != dictWord[i])
                    {
                        diffCount++;
                    }
                }

                // 漢明距離 ≤ 2 表示最多兩次編輯即可匹配，符合條件則加入結果並提前結束內層迴圈
                if (diffCount <= 2)
                {
                    result.Add(query);
                    break;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 解法二：Trie + DFS 剪枝
    ///
    /// 解題思路：
    /// 1. 將 dictionary 中所有單詞插入 Trie。
    /// 2. 對每個 query，以 DFS 方式遍歷 Trie，同時記錄已使用的編輯次數（字元不匹配數）。
    /// 3. 若已用編輯次數超過 2，立即剪枝，不再深入該分支。
    /// 4. 若 DFS 成功抵達某個 dictionary 單詞的末端節點（isEnd = true）且編輯次數 ≤ 2，
    ///    則此 query 符合條件。
    ///
    /// 與暴力解相比，Trie 可共用前綴，一旦某個前綴的差異已超過 2，
    /// 就能一次剪掉所有共用此前綴的 dictionary 單詞，大幅減少比較次數。
    ///
    /// 時間複雜度：O(m×L) 建 Trie + O(n×L×26²) 查詢（剪枝後實際遠低於此上界）
    /// 空間複雜度：O(m×L×26)（Trie 節點數）
    /// </summary>
    /// <param name="queries">查詢字串陣列</param>
    /// <param name="dictionary">字典字串陣列</param>
    /// <returns>所有符合條件（編輯次數 ≤ 2）的查詢字串列表</returns>
    public IList<string> TwoEditWordsTrie(string[] queries, string[] dictionary)
    {
        // 建立 Trie 並插入所有 dictionary 單詞
        var trie = new TrieNode();
        foreach (string word in dictionary)
        {
            trie.Insert(word);
        }

        IList<string> result = new List<string>();
        foreach (string query in queries)
        {
            // DFS 搜尋：若能在 ≤ 2 次編輯內匹配到任一 dictionary 單詞，則加入結果
            if (trie.Search(query, 0, 0))
            {
                result.Add(query);
            }
        }

        return result;
    }

    /// <summary>
    /// 解法三：萬用字元雜湊（Wildcard Hashing）
    ///
    /// 解題思路：
    /// 1. 對 dictionary 中每個單詞，產生所有 0、1、2 個位置被替換為萬用字元 '*' 的模式。
    ///    每個長度為 L 的單詞共產生 1 + L + C(L,2) 個模式。
    /// 2. 將所有模式存入 HashSet<string>，使查表操作達到 O(1)。
    /// 3. 對每個 query，同樣產生所有萬用字元模式，若任一模式存在於 HashSet，
    ///    表示此 query 與某個 dictionary 單詞的漢明距離 ≤ 2，加入結果。
    ///
    /// 核心原理：若 query 與 dictWord 在位置 i、j 上不同，
    /// 則兩者在這兩個位置都換成 '*' 後的模式字串完全相同，因此可被 HashSet 匹配。
    ///
    /// 時間複雜度：O((m+n)×L²)，m = dictionary 長度，n = queries 長度，L = 字串長度
    /// 空間複雜度：O(m×L²)（HashSet 中儲存的模式數量）
    /// </summary>
    /// <param name="queries">查詢字串陣列</param>
    /// <param name="dictionary">字典字串陣列</param>
    /// <returns>所有符合條件（漢明距離 ≤ 2）的查詢字串列表</returns>
    public IList<string> TwoEditWordsHash(string[] queries, string[] dictionary)
    {
        // 預處理：將 dictionary 中所有單詞的萬用字元模式加入 HashSet
        var patternSet = new HashSet<string>();
        foreach (string word in dictionary)
        {
            foreach (string pattern in GeneratePatterns(word))
            {
                patternSet.Add(pattern);
            }
        }

        IList<string> result = new List<string>();
        foreach (string query in queries)
        {
            // 若 query 的任一模式存在於 HashSet，表示漢明距離 ≤ 2，符合條件
            if (GeneratePatterns(query).Any(p => patternSet.Contains(p)))
            {
                result.Add(query);
            }
        }

        return result;
    }

    /// <summary>
    /// 為單詞產生所有「0、1、2 個位置替換為萬用字元 '*'」的模式。
    /// 共產生 1 + L + C(L,2) = 1 + L + L×(L-1)/2 個模式。
    /// </summary>
    /// <param name="word">輸入單詞</param>
    /// <returns>所有萬用字元模式的序列</returns>
    private static IEnumerable<string> GeneratePatterns(string word)
    {
        char[] chars = word.ToCharArray();
        int len = word.Length;

        // 0 個萬用字元：原始單詞（涵蓋漢明距離 = 0 的情況）
        yield return word;

        for (int i = 0; i < len; i++)
        {
            char origI = chars[i];
            chars[i] = '*';

            // 1 個萬用字元：位置 i
            yield return new string(chars);

            // 2 個萬用字元：位置 i 和 j（j > i）
            for (int j = i + 1; j < len; j++)
            {
                char origJ = chars[j];
                chars[j] = '*';
                yield return new string(chars);
                chars[j] = origJ;
            }

            chars[i] = origI;
        }
    }
}

/// <summary>
/// Trie 節點，用於支援最多 2 次編輯的 DFS 搜尋。
/// 每個節點儲存 26 個子節點（對應 a-z），以及是否為單詞結尾的旗標。
/// </summary>
internal class TrieNode
{
    private readonly TrieNode?[] children = new TrieNode?[26];
    private bool isEnd;

    /// <summary>
    /// 將單詞插入 Trie。
    /// </summary>
    /// <param name="word">要插入的單詞</param>
    public void Insert(string word)
    {
        var node = this;
        foreach (char c in word)
        {
            int idx = c - 'a';
            node.children[idx] ??= new TrieNode();
            node = node.children[idx]!;
        }

        node.isEnd = true;
    }

    /// <summary>
    /// 以 DFS 方式在 Trie 中搜尋 query，允許最多 2 次字元不匹配（編輯）。
    /// 超過 2 次不匹配時立即剪枝，不繼續深入該分支。
    /// </summary>
    /// <param name="query">查詢字串</param>
    /// <param name="pos">目前比對到的字元位置</param>
    /// <param name="edits">目前已累積的編輯（不匹配）次數</param>
    /// <returns>若能在 ≤ 2 次編輯內匹配到某個 dictionary 單詞，回傳 true；否則回傳 false</returns>
    public bool Search(string query, int pos, int edits)
    {
        // 剪枝：編輯次數已超過上限，放棄此分支
        if (edits > 2)
        {
            return false;
        }

        // 抵達 query 末端：若此節點為 dictionary 單詞結尾，代表找到匹配
        if (pos == query.Length)
        {
            return isEnd;
        }

        int queryIdx = query[pos] - 'a';

        // 嘗試 Trie 中所有存在的子節點分支
        for (int i = 0; i < 26; i++)
        {
            if (children[i] is null)
            {
                continue;
            }

            // 字元匹配則不增加編輯次數；不匹配則視為一次編輯
            int nextEdits = edits + (i == queryIdx ? 0 : 1);
            if (children[i]!.Search(query, pos + 1, nextEdits))
            {
                return true;
            }
        }

        return false;
    }
}
