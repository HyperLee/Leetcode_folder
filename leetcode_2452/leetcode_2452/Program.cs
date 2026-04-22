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
        Console.WriteLine($"Test 1: [{string.Join(", ", result1)}]");
        // Expected: [word, note, wood]

        // 測試資料 2：預期輸出 []
        string[] queries2 = ["yes"];
        string[] dictionary2 = ["not"];
        IList<string> result2 = program.TwoEditWords(queries2, dictionary2);
        Console.WriteLine($"Test 2: [{string.Join(", ", result2)}]");
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
}
