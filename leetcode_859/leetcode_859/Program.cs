namespace leetcode_859;

internal static class Program
{
    /// <summary>
    /// 859. Buddy Strings
    /// https://leetcode.com/problems/buddy-strings/
    /// 859. 親密字串
    /// https://leetcode.cn/problems/buddy-strings/
    /// Given two lowercase strings, determine whether swapping the characters at exactly two distinct indices in the first string can make it equal to the goal string.
    /// 給定兩個小寫字串，判斷是否能交換第一個字串中兩個不同索引的字元，使結果等於目標字串。
    /// </summary>
    private static void Main()
    {
        string maximumSource = new string('a', 19_998) + "bc";
        string maximumGoal = new string('a', 19_998) + "cb";

        (string Name, string Source, string Goal, bool Expected, string InputDescription)[] cases =
        [
            ("Official example: one valid swap", "ab", "ba", true, "s = \"ab\", goal = \"ba\""),
            ("Official example: equal without duplicate", "ab", "ab", false, "s = \"ab\", goal = \"ab\""),
            ("Official example: equal with duplicate", "aa", "aa", true, "s = \"aa\", goal = \"aa\""),
            ("Minimum length", "a", "a", false, "s = \"a\", goal = \"a\""),
            ("Different lengths", "abc", "ab", false, "s = \"abc\", goal = \"ab\""),
            ("Exactly one difference", "ab", "ac", false, "s = \"ab\", goal = \"ac\""),
            ("Exactly two cross-matching differences", "abcd", "abdc", true, "s = \"abcd\", goal = \"abdc\""),
            ("Two differences without cross-match", "ab", "ca", false, "s = \"ab\", goal = \"ca\""),
            ("More than two differences", "abcd", "badc", false, "s = \"abcd\", goal = \"badc\""),
            ("Maximum length spot check", maximumSource, maximumGoal, true, "length = 20,000; shared prefix = 19,998 x 'a'; suffix = \"bc\" -> \"cb\"")
        ];

        int passedCount = 0;
        Console.WriteLine("LeetCode 859 acceptance harness");
        Console.WriteLine();

        for (int i = 0; i < cases.Length; i++)
        {
            (string name, string source, string goal, bool expected, string inputDescription) = cases[i];
            bool actual = BuddyStrings(source, goal);
            bool passed = expected == actual;

            if (passed)
            {
                passedCount++;
            }

            Console.WriteLine($"Case {i + 1}: {name}");
            Console.WriteLine($"Input: {inputDescription}");
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | Expected: {expected.ToString().ToLowerInvariant()} | Actual: {actual.ToString().ToLowerInvariant()}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passedCount}/{cases.Length} checks passed.");

        if (passedCount != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 以單次掃描找出 <paramref name="s"/> 與 <paramref name="goal"/> 的不同位置，並判斷交換 <paramref name="s"/> 中兩個不同索引後能否得到目標字串。有效輸入須符合題目定義的非空、僅含小寫英文字母字串；方法不會修改輸入，也不會寫入主控台。
    /// </summary>
    /// <param name="s">要執行恰好一次雙索引字元交換的題目有效字串。</param>
    /// <param name="goal">交換後必須相等的題目有效目標字串。</param>
    /// <returns>若存在一次合法交換可使兩字串相等則為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    public static bool BuddyStrings(string s, string goal)
    {
        if (s.Length != goal.Length)
        {
            return false;
        }

        bool[] seenLetters = new bool[26];
        bool hasDuplicate = false;
        int firstDifference = -1;
        int secondDifference = -1;

        for (int i = 0; i < s.Length; i++)
        {
            // 字串完全相同時，只有重複字母能讓兩個不同索引交換後仍維持原字串。
            int letterIndex = s[i] - 'a';
            hasDuplicate |= seenLetters[letterIndex];
            seenLetters[letterIndex] = true;

            if (s[i] == goal[i])
            {
                continue;
            }

            // 一次交換最多只能修正兩個不同位置；第三個 mismatch 可立即判定失敗。
            if (firstDifference == -1)
            {
                firstDifference = i;
            }
            else if (secondDifference == -1)
            {
                secondDifference = i;
            }
            else
            {
                return false;
            }
        }

        if (firstDifference == -1)
        {
            return hasDuplicate;
        }

        // 兩個 mismatch 必須能交叉配對，交換後才會同時等於 goal。
        return secondDifference != -1
            && s[firstDifference] == goal[secondDifference]
            && s[secondDifference] == goal[firstDifference];
    }
}
