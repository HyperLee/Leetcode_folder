namespace leetcode_3541;

using System;

class Program
{
    /// <summary>
    /// 3541. Find Most Frequent Vowel and Consonant
    /// https://leetcode.com/problems/find-most-frequent-vowel-and-consonant/description/
    /// <para>
    /// You are given a lowercase English string s.
    ///
    /// Find the maximum frequency among vowels ('a', 'e', 'i', 'o', 'u') and the maximum frequency among consonants, then return their sum.
    ///
    /// If multiple vowels or consonants tie, choose any. If there are no vowels or no consonants, use frequency 0. A letter's frequency is its number of occurrences.
    ///
    /// Example 1:
    /// Input: s = "successes"
    /// Output: 6
    /// Explanation: Vowels 'u' and 'e' have frequencies 1 and 2, so the vowel maximum is 2. Consonants 's' and 'c' have frequencies 4 and 2, so the consonant maximum is 4. The result is 2 + 4 = 6.
    ///
    /// Example 2:
    /// Input: s = "aeiaeia"
    /// Output: 3
    /// Explanation: Vowels 'a', 'e', and 'i' have frequencies 3, 2, and 2, so the vowel maximum is 3. There are no consonants, so their maximum is 0. The result is 3 + 0 = 3.
    ///
    /// Constraints:
    /// - 1 &lt;= s.length &lt;= 100
    /// - s consists only of lowercase English letters.
    /// </para>
    /// <para>
    /// 3541. 找出最高頻率的母音與子音
    /// https://leetcode.cn/problems/find-most-frequent-vowel-and-consonant/description/
    ///
    /// 給定只含小寫英文字母的字串 s。
    ///
    /// 找出母音（'a'、'e'、'i'、'o'、'u'）中的最大頻率，以及子音中的最大頻率，並回傳兩者總和。
    ///
    /// 若多個母音或子音並列，可任選其一。若不存在母音或子音，將其頻率視為 0。字母的頻率是它的出現次數。
    ///
    /// 範例 1：
    /// 輸入：s = "successes"
    /// 輸出：6
    /// 解釋：母音 'u' 與 'e' 的頻率為 1、2，因此母音最大頻率為 2。子音 's' 與 'c' 的頻率為 4、2，因此子音最大頻率為 4。結果為 2 + 4 = 6。
    ///
    /// 範例 2：
    /// 輸入：s = "aeiaeia"
    /// 輸出：3
    /// 解釋：母音 'a'、'e'、'i' 的頻率為 3、2、2，因此母音最大頻率為 3。沒有子音，所以其最大頻率為 0。結果為 3 + 0 = 3。
    ///
    /// 限制條件：
    /// - 1 &lt;= s.length &lt;= 100
    /// - s 只由小寫英文字母組成。
    /// </para>
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
