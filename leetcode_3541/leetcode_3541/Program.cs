namespace leetcode_3541;

using System;

class Program
{
    /// <summary>
    /// 3541. Find Most Frequent Vowel and Consonant
    /// https://leetcode.com/problems/find-most-frequent-vowel-and-consonant/description/?envType=daily-question&envId=2025-09-13
    /// 3541. 找到頻率最高的元音和輔音
    /// 題目描述：
    /// 給定一個只包含小寫英文字母（'a' 到 'z'）的字串 s。
    /// 任務：
    ///  - 找出出現頻率最高的元音（a, e, i, o, u）。
    ///  - 找出出現頻率最高的輔音（除元音外的其他字母）。
    /// 回傳兩者頻率的總和。
    /// 注意：若元音或輔音不存在，視其頻率為 0；若有多個頻率相同的字母，可任意選一個。
    /// 字母 x 的頻率是它在字串中出現的次數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例測試：會輸出每個測試字串的結果
        var tests = new[]
        {
            "abcde",
            "aaaaa",
            "bcdfgh",
            "abacaba",
            string.Empty
        };

        foreach (var t in tests)
        {
            int res = Solution.MaxFrequencySum(t);
            Console.WriteLine($"s = \"{t}\", result = {res}");
        }

        foreach (var t in tests)
        {
            int res2 = Solution.MaxFrequencySum2(t);
            Console.WriteLine($"s = \"{t}\", result2 = {res2}");
        }
    }
}

/// <summary>
/// 解題輔助：包含計算元音與輔音最高頻率總和的方法。
/// </summary>
public static class Solution
{
    /// <summary>
    /// 回傳字串中出現頻率最高的元音與頻率最高的輔音的頻率總和。
    /// 若輸入為 null 或空字串，回傳 0。
    /// </summary>
    /// <param name="s">只包含小寫英文字母的字串</param>
    /// <returns>最高元音頻率 + 最高輔音頻率</returns>
    public static int MaxFrequencySum(string s)
    {
        if (s is null || s.Length == 0)
        {
            return 0;
        }

        // 使用固定長度陣列計數 26 個字母
        Span<int> counts = stackalloc int[26];
        foreach (char ch in s)
        {
            if (ch < 'a' || ch > 'z')
            {
                // 跳過非小寫字母（題目保證為小寫，但此處保護性處理）
                continue;
            }
            counts[ch - 'a']++;
        }

        /*
        // 使用 counts（例如輸出或進一步處理）
        for (int i = 0; i < counts.Length; i++)
        {
            if (counts[i] != 0)
            {
                // 輸出每個字母統計的次數
                Console.WriteLine($"{(char)('a' + i)}: {counts[i]}");
            }
        }   
        */     

        int maxVowel = 0;
        int maxConsonant = 0;

        // 標記元音
        bool[] isVowel = new bool[26];
        isVowel['a' - 'a'] = true;
        isVowel['e' - 'a'] = true;
        isVowel['i' - 'a'] = true;
        isVowel['o' - 'a'] = true;
        isVowel['u' - 'a'] = true;

        for (int i = 0; i < 26; i++)
        {
            int c = counts[i];
            if (c == 0) continue;
            if (isVowel[i])
            {
                if (c > maxVowel) maxVowel = c;
            }
            else
            {
                if (c > maxConsonant) maxConsonant = c;
            }
        }

        return maxVowel + maxConsonant;
    }


    /// <summary>
    /// 方法二：使用 Dictionary 計數字母頻率，邏輯與方法一相似。
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int MaxFrequencySum2(string s)
    {
        Dictionary<char, int> counts = new Dictionary<char, int>();
        foreach (char ch in s)
        {
            // 跳過非小寫字母（題目保證為小寫，但此處保護性處理）
            if (ch < 'a' || ch > 'z')
            {
                continue;
            }
            // 計數字母出現次數
            if (counts.ContainsKey(ch))
            {
                counts[ch]++;
            }
            else
            {
                counts[ch] = 1;
            }
        }

        int maxVowel = 0;
        int maxConsonant = 0;

        foreach (var kvp in counts)
        {
            char letter = kvp.Key;
            int frequency = kvp.Value;

            // 判斷是否為元音並更新最大頻率
            if ("aeiou".Contains(letter))
            {
                // 元音
                if (frequency > maxVowel)
                {
                    maxVowel = frequency;
                }
            }
            else
            {
                // 輔音
                if (frequency > maxConsonant)
                {
                    maxConsonant = frequency;
                }
            }
        }

        // 回傳最高元音頻率與最高輔音頻率的總和
        return maxVowel + maxConsonant;
    }
}
