namespace leetcode_1897;

internal static class Program
{
    /// <summary>
    /// LeetCode 1897. Redistribute Characters to Make All Strings Equal.
    /// LeetCode 1897. 重新分配字元使所有字串相等。
    /// English: You may move any character from one string to another. Return whether the
    /// characters can be redistributed so every string becomes identical.
    /// 中文：可將任一字串中的任一字元移至另一字串；判斷能否重新分配所有字元，使每個字串
    /// 最後完全相同。
    /// English: https://leetcode.com/problems/redistribute-characters-to-make-all-strings-equal/
    /// 中文：https://leetcode.cn/problems/redistribute-characters-to-make-all-strings-equal/
    /// </summary>
    private static void Main()
    {
        string[] oneHundredCopiesOfA = Enumerable.Repeat("a", 100).ToArray();
        string[] twoLengthOneHundredStrings = [new string('a', 100), new string('b', 100)];

        TestCase[] testCases =
        [
            new("Official example", "[\"abc\", \"aabc\", \"bc\"]", ["abc", "aabc", "bc"], true),
            new("Character count is not divisible", "[\"ab\", \"a\"]", ["ab", "a"], false),
            new("Single string", "[\"a\"]", ["a"], true),
            new("Already equal strings", "[\"abc\", \"abc\"]", ["abc", "abc"], true),
            new("Multiple remainders", "[\"ab\", \"ab\", \"aa\"]", ["ab", "ab", "aa"], false),
            new("Redistribution across all characters", "[\"aa\", \"bb\", \"ab\"]", ["aa", "bb", "ab"], true),
            new("One hundred copies", "[\"a\" x 100]", oneHundredCopiesOfA, true),
            new(
                "Two strings of maximum length",
                "[\"a\" x 100 chars, \"b\" x 100 chars]",
                twoLengthOneHundredStrings,
                true)
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck("MakeEqual result", result.Expected, result.MakeEqualActual));
            Console.WriteLine(PrintCheck("MakeEqual input preserved", true, result.MakeEqualInputPreserved));
            Console.WriteLine(PrintCheck("MakeEqual2 result", result.Expected, result.MakeEqual2Actual));
            Console.WriteLine(PrintCheck("MakeEqual2 input preserved", true, result.MakeEqual2InputPreserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 32;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string PrintCheck(string checkName, bool expected, bool actual)
    {
        string status = expected == actual ? "PASS" : "FAIL";
        return $"{status} {checkName} | Expected: {expected} | Actual: {actual}";
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        string[] makeEqualWords = [.. testCase.Words];
        string[] originalMakeEqualWords = [.. makeEqualWords];
        bool makeEqualActual = MakeEqual(makeEqualWords);
        bool makeEqualInputPreserved = makeEqualWords.SequenceEqual(originalMakeEqualWords);

        string[] makeEqual2Words = [.. testCase.Words];
        string[] originalMakeEqual2Words = [.. makeEqual2Words];
        bool makeEqual2Actual = MakeEqual2(makeEqual2Words);
        bool makeEqual2InputPreserved = makeEqual2Words.SequenceEqual(originalMakeEqual2Words);

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.Expected,
            makeEqualActual,
            makeEqualInputPreserved,
            makeEqual2Actual,
            makeEqual2InputPreserved);
    }

    /// <summary>
    /// 統計所有字元的總出現次數，判斷是否能重新分配後使每個字串相等。對題目保證的有效
    /// 輸入（words 長度與每個字串長度皆介於 1 至 100，且只含小寫英文字母），每種字元
    /// 的總次數可被字串數量整除，是存在平均分配方案的充要不變量。方法不修改 words、
    /// 其中的字串或主控台狀態；符合不變量時回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="words">題目限制內、由小寫英文字母組成的字串陣列。</param>
    /// <returns>每種字元總次數都可被 words.Length 整除時為 true；否則為 false。</returns>
    public static bool MakeEqual(string[] words)
    {
        Dictionary<char, int> characterCounts = new();

        foreach (string word in words)
        {
            foreach (char character in word)
            {
                characterCounts.TryGetValue(character, out int count);
                characterCounts[character] = count + 1;
            }
        }

        foreach (int count in characterCounts.Values)
        {
            // 每種字元均分給每個字串，當且僅當其總次數可整除字串數量。
            if (count % words.Length != 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 以固定小寫英文字母表的 26 格計數陣列，判斷字元能否重新分配為相同字串。對題目
    /// 保證的有效輸入（words 與其中字串長度皆介於 1 至 100，且只含小寫英文字母），每個
    /// 字元的總數可被 words.Length 整除，是可平均分配的充要不變量。方法只讀取輸入，
    /// 不修改 words、其中的字串或主控台狀態；符合不變量時回傳 true，否則回傳 false。
    /// 時間複雜度為 O(C)，其中 C 為所有字元總數；固定 26 格計數陣列的輔助空間為 O(1)。
    /// </summary>
    /// <param name="words">題目限制內、由小寫英文字母組成的字串陣列。</param>
    /// <returns>每種小寫字元總次數都可被 words.Length 整除時為 true；否則為 false。</returns>
    public static bool MakeEqual2(string[] words)
    {
        int[] characterCounts = new int[26];

        foreach (string word in words)
        {
            foreach (char character in word)
            {
                characterCounts[character - 'a']++;
            }
        }

        foreach (int count in characterCounts)
        {
            if (count % words.Length != 0)
            {
                return false;
            }
        }

        return true;
    }

    private sealed record TestCase(string Name, string Input, string[] Words, bool Expected);

    private sealed record CaseResult(
        string Name,
        string Input,
        bool Expected,
        bool MakeEqualActual,
        bool MakeEqualInputPreserved,
        bool MakeEqual2Actual,
        bool MakeEqual2InputPreserved)
    {
        public int PassedCheckCount =>
            (MakeEqualActual == Expected ? 1 : 0) +
            (MakeEqualInputPreserved ? 1 : 0) +
            (MakeEqual2Actual == Expected ? 1 : 0) +
            (MakeEqual2InputPreserved ? 1 : 0);
    }
}