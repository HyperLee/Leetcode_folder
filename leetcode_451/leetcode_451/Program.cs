using System.Text;

namespace leetcode_451;

class Program
{
    /// <summary>
    /// 451. Sort Characters By Frequency
    /// https://leetcode.com/problems/sort-characters-by-frequency/description/
    /// 451. 根據字元出現頻率排序
    /// https://leetcode.cn/problems/sort-characters-by-frequency/description/
    /// 
    /// English:
    /// Given a string s, sort it in decreasing order based on the frequency of the characters.
    /// The frequency of a character is the number of times it appears in the string.
    /// Return the sorted string. If there are multiple answers, return any of them.
    ///
    /// 繁體中文：
    /// 給定一個字串 s，請依照字元出現頻率由高到低排序。
    /// 字元的頻率是該字元在字串中出現的次數。
    /// 回傳排序後的字串。若有多個答案，回傳任一個即可。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        var testCases = new[]
        {
            "tree",
            "cccaaa",
            "Aabb"
        };

        foreach (var input in testCases)
        {
            string output = solution.FrequencySort(input);
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Output: {output}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Sorts the characters in <paramref name="s"/> by decreasing frequency.
    /// 解題概念是先使用 Dictionary 統計每個字元的出現次數，再依照次數由高到低排序，
    /// 最後使用 StringBuilder 依照頻率重建字串。輸入需符合題目條件，也就是由英文字母或數字組成的非 null 字串；
    /// 輸出會包含與輸入完全相同的字元集合，且高頻字元會排在低頻字元前方。若頻率相同，任一順序都符合題意。
    /// </summary>
    /// <param name="s">需要依字元頻率重新排序的輸入字串。</param>
    /// <returns>依字元出現頻率由高到低排列後的字串。</returns>
    public string FrequencySort(string s)
    {
        Dictionary<char, int> frequencies = new Dictionary<char, int>();

        // 先完成頻率表，後續排序只需要處理不同字元，不必重複掃描原字串。
        foreach (char c in s)
        {
            if (frequencies.ContainsKey(c))
            {
                frequencies[c]++;
            }
            else
            {
                frequencies[c] = 1;
            }
        }

        var sortedFrequencies = frequencies.OrderByDescending(x => x.Value);
        StringBuilder result = new StringBuilder(s.Length);

        // 排序後依照頻率重複加入字元，即可得到符合題意的輸出。
        foreach (var item in sortedFrequencies)
        {
            for (int i = 0; i < item.Value; i++)
            {
                result.Append(item.Key);
            }
        }

        return result.ToString();
    }
}
