using System.Runtime.CompilerServices;
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
            string output1 = solution.FrequencySort(input);
            string output2 = solution.FrequencySort2(input);
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Method 1 Output: {output1}");
            Console.WriteLine($"Method 2 Output: {output2}");
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

    /// <summary>
    /// 解法二：桶排序
    /// 先統計每個字元的出現次數與最高頻率，再建立頻率桶，將相同頻率的字元放在同一個桶中。
    /// 最後由最高頻率往低頻率遍歷桶，依照頻率重複加入字元，即可組合出依頻率遞減排序的字串。
    /// </summary>
    /// <param name="s">需要依字元頻率重新排序的輸入字串。</param>
    /// <returns>使用桶排序依字元出現頻率由高到低排列後的字串。</returns>
    public string FrequencySort2(string s)
    {
        Dictionary<char, int> frequencies = new Dictionary<char, int>();
        int maxFreq = 0;

        // 統計每個字元出現次數，並同步記錄最高頻率，方便後續建立桶的大小。
        foreach (char ch in s)
        {
            if (frequencies.ContainsKey(ch))
            {
                frequencies[ch]++;
            }
            else
            {
                frequencies[ch] = 1;
            }

            maxFreq = Math.Max(maxFreq, frequencies[ch]);
        }

        // buckets[i] 存放出現 i 次的所有字元，因此索引範圍需要到 maxFreq。
        StringBuilder[] buckets = new StringBuilder[maxFreq + 1];
        for (int i = 0; i <= maxFreq; i++)
        {
            buckets[i] = new StringBuilder();
        }

        // 將每個字元依照出現頻率放入對應的桶。
        foreach (KeyValuePair<char, int> item in frequencies)
        {
            char ch = item.Key;
            int frequency = item.Value;
            buckets[frequency].Append(ch);
        }

        StringBuilder result = new StringBuilder(s.Length);

        // 從高頻率桶往低頻率桶取出字元，並依照頻率重複加入結果。
        for (int i = maxFreq; i > 0; i--)
        {
            StringBuilder bucket = buckets[i];
            for (int j = 0; j < bucket.Length; j++)
            {
                for (int k = 0; k < i; k++)
                {
                    result.Append(bucket[j]);
                }
            }
        }

        return result.ToString();
    }
}
