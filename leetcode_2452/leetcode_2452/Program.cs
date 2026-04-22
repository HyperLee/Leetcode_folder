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
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queries"></param>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public IList<string> TwoEditWords(string[] queries, string[] dictionary)
    {
        IList<string> res = new List<string>();
        foreach(string query in queries)
        {
            foreach(string dict in dictionary)
            {
                int dis = 0;
                for(int i = 0; i < query.Length; i++)
                {
                    if(query[i] != dict[i])
                    {
                        dis++;
                    }
                }
                if(dis <= 2)
                {
                    res.Add(query);
                    break;
                }
            }
        }
        return res;
    }
}
