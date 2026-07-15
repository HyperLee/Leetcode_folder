namespace leetcode_767;

internal static class Program
{
    /// <summary>
    /// 767. Reorganize String
    /// https://leetcode.com/problems/reorganize-string/
    /// 767. 重構字串
    /// https://leetcode.cn/problems/reorganize-string/
    /// English: Given a lowercase English string, rearrange its characters so that no two
    /// adjacent characters are equal; return any valid arrangement, or an empty string when
    /// no such arrangement exists.
    /// 中文：給定一個只含小寫英文字母的字串，重新排列其中字元，使任兩個相鄰字元
    /// 都不相同；若可行則回傳任一合法排列，否則回傳空字串。
    /// </summary>
    /// <param name="args">命令列參數；本驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        string maximumLengthPossible = new string('a', 250) + new string('b', 250);
        string maximumLengthImpossible = new string('a', 251) + new string('b', 249);

        (string CaseName, string Input, bool Possible)[] testCases =
        {
            ("Case 1: Official possible example", "aab", true),
            ("Case 2: Official impossible example", "aaab", false),
            ("Case 3: Single character", "z", true),
            ("Case 4: Minimum impossible repetition", "aa", false),
            ("Case 5: Odd-length feasibility boundary", "aaabc", true),
            ("Case 6: Even-length feasibility boundary", "aaabbc", true),
            ("Case 7: General regression", "vvvlo", true),
            ("Case 8: Maximum-length possible", maximumLengthPossible, true),
            ("Case 9: Maximum-length threshold failure", maximumLengthImpossible, false)
        };

        int passedCount = 0;
        int checkCount = 0;

        Console.WriteLine("LeetCode 767 acceptance harness");

        foreach ((string caseName, string input, bool possible) in testCases)
        {
            string actual = ReorganizeString(input);

            Console.WriteLine();
            Console.WriteLine(caseName);
            Console.WriteLine($"Input: {FormatValue(input)}");
            Console.WriteLine($"Output: {FormatValue(actual)}");

            (string CheckName, bool Expected, bool Actual)[] checks = possible
                ? new[]
                {
                    ("Output is non-empty", true, actual.Length > 0),
                    ("Output length equals input length", true, actual.Length == input.Length),
                    (
                        "Output has same character multiset",
                        true,
                        HasSameCharacterMultiset(input, actual)),
                    (
                        "Output has no equal adjacent characters",
                        true,
                        HasNoEqualAdjacentCharacters(actual))
                }
                : new[]
                {
                    ("Output is empty", true, actual.Length == 0)
                };

            foreach ((string checkName, bool expected, bool actualCheck) in checks)
            {
                bool passed = expected == actualCheck;
                string result = passed ? "PASS" : "FAIL";
                Console.WriteLine(
                    $"{result} | {checkName} | Expected: {expected} | Actual: {actualCheck}");

                checkCount++;

                if (passed)
                {
                    passedCount++;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{checkCount} checks passed.");

        if (passedCount != checkCount)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 重新排列只含小寫英文字母的有效輸入，使相鄰字元皆不相同。演算法先統計
    /// 26 個字母，再將最高頻字元優先放入偶數索引，其餘字元接續填滿剩餘偶數索引
    /// 後轉至奇數索引。輸入長度須為 1 到 500；可行時回傳任一合法排列，不可行時
    /// 回傳空字串。此方法不修改輸入，且不會寫入主控台。
    /// </summary>
    /// <param name="s">長度為 1 到 500，且只含小寫英文字母的字串。</param>
    /// <returns>相鄰字元皆不同的排列；若不存在則回傳空字串。</returns>
    public static string ReorganizeString(string s)
    {
        int[] counts = new int[26];

        foreach (char character in s)
        {
            counts[character - 'a']++;
        }

        int mostFrequentIndex = 0;

        for (int index = 1; index < counts.Length; index++)
        {
            if (counts[index] > counts[mostFrequentIndex])
            {
                mostFrequentIndex = index;
            }
        }

        // 最高頻字元超過可用的交錯位置數時，無法避免相鄰重複。
        if (counts[mostFrequentIndex] > (s.Length + 1) / 2)
        {
            return string.Empty;
        }

        char[] reorganized = new char[s.Length];
        int resultIndex = 0;

        while (counts[mostFrequentIndex] > 0)
        {
            reorganized[resultIndex] = (char)('a' + mostFrequentIndex);
            resultIndex += 2;
            counts[mostFrequentIndex]--;
        }

        for (int characterIndex = 0; characterIndex < counts.Length; characterIndex++)
        {
            while (counts[characterIndex] > 0)
            {
                // 先填完剩餘偶數索引，再由索引 1 開始填奇數索引，維持交錯間隔。
                if (resultIndex >= reorganized.Length)
                {
                    resultIndex = 1;
                }

                reorganized[resultIndex] = (char)('a' + characterIndex);
                resultIndex += 2;
                counts[characterIndex]--;
            }
        }

        return new string(reorganized);
    }

    private static bool HasSameCharacterMultiset(string expected, string actual)
    {
        if (expected.Length != actual.Length)
        {
            return false;
        }

        int[] counts = new int[26];

        foreach (char character in expected)
        {
            counts[character - 'a']++;
        }

        foreach (char character in actual)
        {
            counts[character - 'a']--;
        }

        return counts.All(static count => count == 0);
    }

    private static bool HasNoEqualAdjacentCharacters(string value)
    {
        for (int index = 1; index < value.Length; index++)
        {
            if (value[index] == value[index - 1])
            {
                return false;
            }
        }

        return true;
    }

    private static string FormatValue(string value)
    {
        if (value.Length <= 80)
        {
            return $"\"{value}\"";
        }

        int aCount = value.Count(static character => character == 'a');
        int bCount = value.Count(static character => character == 'b');
        return $"length {value.Length}; counts: a={aCount}, b={bCount}";
    }

}
