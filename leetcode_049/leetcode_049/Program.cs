using System.Text;

namespace leetcode_049;

class Program
{
    /// <summary>
    /// 49. Group Anagrams
    /// https://leetcode.com/problems/group-anagrams/description/
    ///
    /// English:
    /// Given an array of strings strs, group the anagrams together. You can return the answer in any order.
    ///
    /// 繁體中文:
    /// 給定一個字串陣列 strs，請將所有字母異位詞分組。你可以用任意順序回傳答案。
    ///
    /// 49. 字母異位詞分組
    /// https://leetcode.cn/problems/group-anagrams/description/?envType=study-plan-v2&envId=top-interview-150
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (string Title, string[] Input)[] testCases =
        {
            ("範例 1：一般分組", new string[] { "eat", "tea", "tan", "ate", "nat", "bat" }),
            ("範例 2：空字串", new string[] { "" }),
            ("範例 3：單一字串", new string[] { "a" }),
            ("範例 4：重複字與多組", new string[] { "", "b", "bb", "b", "abc", "cab", "bac" }),
        };

        Console.WriteLine("49. Group Anagrams - 可執行範例");
        Console.WriteLine();

        foreach ((string title, string[] input) in testCases)
        {
            Console.WriteLine(title);
            Console.WriteLine($"Input: [{FormatValues(input)}]");
            PrintResult("排序 Key 解法", solution.GroupAnagrams(input));
            PrintResult("字母計數解法", solution.GroupAnagrams2(input));
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 使用「排序後字串作為雜湊鍵」分組字母異位詞。
    /// 解題概念：字母異位詞擁有相同字元集合，因此將每個字串的字元排序後，會得到相同的 key。
    /// 輸入條件：<paramref name="strs"/> 需符合題目限制，元素由小寫英文字母組成，且可包含空字串。
    /// 輸出結果：回傳由原始字串組成的群組集合；群組順序與群組內順序不影響答案正確性。
    /// </summary>
    /// <param name="strs">要分組的字串陣列。</param>
    /// <returns>每個內層集合代表一組互為字母異位詞的原始字串。</returns>
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, IList<string>> groupsByKey = new Dictionary<string, IList<string>>();
        IList<IList<string>> result = new List<IList<string>>();

        for (int i = 0; i < strs.Length; i++)
        {
            char[] letters = strs[i].ToArray();
            Array.Sort(letters);
            string key = new string(letters);

            // 字母異位詞排序後會得到相同 key，因此可直接累積到同一個群組。
            if (!groupsByKey.TryGetValue(key, out IList<string>? group))
            {
                group = new List<string>();
                groupsByKey[key] = group;
            }

            group.Add(strs[i]);
        }

        foreach (KeyValuePair<string, IList<string>> group in groupsByKey)
        {
            result.Add(group.Value);
        }

        return result;
    }

    /// <summary>
    /// 使用「26 個小寫英文字母出現次數作為雜湊鍵」分組字母異位詞。
    /// 解題概念：字母異位詞中每個字母的出現次數完全相同，可用計數陣列組合成唯一 key。
    /// 輸入條件：<paramref name="strs"/> 需符合題目限制，元素只包含小寫英文字母，且可包含空字串。
    /// 輸出結果：回傳由原始字串組成的群組集合；群組順序與群組內順序不影響答案正確性。
    /// </summary>
    /// <param name="strs">要分組的字串陣列。</param>
    /// <returns>每個內層集合代表一組互為字母異位詞的原始字串。</returns>
    public IList<IList<string>> GroupAnagrams2(string[] strs)
    {
        Dictionary<string, IList<string>> groupsByKey = new Dictionary<string, IList<string>>();

        foreach (string str in strs)
        {
            int[] counts = new int[26];

            foreach (char c in str)
            {
                counts[c - 'a']++;
            }

            StringBuilder keyBuilder = new StringBuilder();
            for (int i = 0; i < 26; i++)
            {
                if (counts[i] != 0)
                {
                    // key 同時包含字母與數量，例如 "a1e1t1"，避免只比較字串長度或部分字母。
                    keyBuilder.Append((char)('a' + i));
                    keyBuilder.Append(counts[i]);
                }
            }

            string key = keyBuilder.ToString();

            if (!groupsByKey.TryGetValue(key, out IList<string>? group))
            {
                group = new List<string>();
                groupsByKey[key] = group;
            }

            group.Add(str);
        }

        return groupsByKey.Values.ToList();
    }

    /// <summary>
    /// 輸出單一解法的分組結果，並先正規化排序以便 README 與執行結果穩定對照。
    /// 解題概念：LeetCode 允許任意順序回傳，但範例輸出需要固定排序才方便人工檢查。
    /// 輸入條件：<paramref name="label"/> 為解法名稱，<paramref name="groups"/> 為解法回傳的群組。
    /// 輸出結果：將格式化後的群組寫入主控台。
    /// </summary>
    /// <param name="label">顯示用的解法名稱。</param>
    /// <param name="groups">要輸出的分組結果。</param>
    private static void PrintResult(string label, IList<IList<string>> groups)
    {
        Console.WriteLine($"{label}: {FormatGroups(groups)}");
    }

    /// <summary>
    /// 將多組字串轉成穩定排序後的巢狀陣列文字。
    /// 解題概念：先排序每個群組內的字串，再依群組文字排序，避免字典列舉順序影響展示。
    /// 輸入條件：<paramref name="groups"/> 為非 null 的群組集合。
    /// 輸出結果：回傳形如 [["ate", "eat", "tea"], ["bat"]] 的展示文字。
    /// </summary>
    /// <param name="groups">要格式化的分組結果。</param>
    /// <returns>穩定排序後的巢狀陣列文字。</returns>
    private static string FormatGroups(IList<IList<string>> groups)
    {
        List<List<string>> normalizedGroups = groups
            .Select(group => group.OrderBy(word => word, StringComparer.Ordinal).ToList())
            .OrderBy(group => string.Join('\u0000', group), StringComparer.Ordinal)
            .ToList();

        IEnumerable<string> formattedGroups = normalizedGroups.Select(group => $"[{FormatValues(group)}]");
        return $"[{string.Join(", ", formattedGroups)}]";
    }

    /// <summary>
    /// 將一維字串集合轉成加上雙引號的陣列片段文字。
    /// 解題概念：保留空字串與重複字串的可讀表示，讓範例輸入與輸出能直接比對。
    /// 輸入條件：<paramref name="values"/> 為非 null 的字串集合。
    /// 輸出結果：回傳形如 "eat", "tea" 的文字片段。
    /// </summary>
    /// <param name="values">要格式化的字串集合。</param>
    /// <returns>以逗號分隔且加上雙引號的字串片段。</returns>
    private static string FormatValues(IEnumerable<string> values)
    {
        return string.Join(", ", values.Select(value => $"\"{EscapeForDisplay(value)}\""));
    }

    /// <summary>
    /// 將字串中的反斜線與雙引號轉義成適合主控台展示的形式。
    /// 解題概念：雖然題目輸入只會有小寫字母與空字串，展示工具仍保持一般字串輸出的安全格式。
    /// 輸入條件：<paramref name="value"/> 為非 null 字串。
    /// 輸出結果：回傳可放在雙引號中的顯示文字。
    /// </summary>
    /// <param name="value">要轉義的原始字串。</param>
    /// <returns>轉義後的展示字串。</returns>
    private static string EscapeForDisplay(string value)
    {
        return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
}
