namespace leetcode_389;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 389. Find the Difference
    /// https://leetcode.com/problems/find-the-difference/
    /// 389. 找不同
    /// https://leetcode.cn/problems/find-the-difference/
    /// Given two lowercase strings where t is a shuffled copy of s with one extra letter, return the added letter.
    /// 給定兩個小寫英文字串，t 是將 s 重新排列後再加入一個字母的結果，請回傳新增的字母。
    /// </summary>
    private static void Main()
    {
        (string S, string T, char Expected)[] cases =
        [
            ("abcd", "abcde", 'e'),
            ("", "y", 'y'),
            ("abcd", "eabcd", 'e'),
            ("a", "aa", 'a'),
            ("ae", "aea", 'a'),
            ("aabbcc", "cbacaba", 'a')
        ];

        Console.WriteLine("LeetCode 389 acceptance harness");
        Console.WriteLine();

        for (int i = 0; i < cases.Length; i++)
        {
            (string s, string t, char expected) = cases[i];
            Console.WriteLine($"Case {i + 1}");
            Console.WriteLine($"Input: s = \"{s}\", t = \"{t}\"");
            Console.WriteLine($"Expected added character: '{expected}'");

            Check("Sorting", expected, FindTheDifference(s, t));
            Check("List removal", expected, FindTheDifference2(s, t));
            Check("XOR", expected, FindTheDifference3(s, t));
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 將兩個輸入字串複製後排序，讓相同字母對齊，藉此找出第一個被新增字母打破配對的位置。
    /// 輸入需符合題目契約：<paramref name="t"/> 是 <paramref name="s"/> 重新排列後，
    /// 再額外加入一個小寫字母所形成的字串。
    /// 若前面所有位置都仍可配對，則回傳排序後 <paramref name="t"/> 最後保留下來的新增字母。
    /// </summary>
    /// <param name="s">加入額外字母前的原始小寫字串。</param>
    /// <param name="t">包含 <paramref name="s"/> 全部字元且多出一個字母的重排結果字串。</param>
    /// <returns>題目要求找出的那個新增字母。</returns>
    public static char FindTheDifference(string s, string t)
    {
        char[] sourceCharacters = s.ToCharArray();
        char[] targetCharacters = t.ToCharArray();
        Array.Sort(sourceCharacters);
        Array.Sort(targetCharacters);

        // 排序後相同字母會落在相同索引，因此新增字母只會出現在第一個不相等的位置，
        // 或成為較長目標陣列最後尚未被配對的字元。
        for (int i = 0; i < sourceCharacters.Length; i++)
        {
            if (sourceCharacters[i] != targetCharacters[i])
            {
                return targetCharacters[i];
            }
        }

        return targetCharacters[^1];
    }

    /// <summary>
    /// 先將 <paramref name="t"/> 複製到可變動串列，再針對 <paramref name="s"/>
    /// 的每個字元移除一個相符項目。
    /// 輸入需符合題目契約：<paramref name="t"/> 在重排後仍包含
    /// <paramref name="s"/> 的全部字元，並另外多出一個小寫字母。
    /// 當所有可配對字元都被消耗後，回傳串列中唯一剩下的新增字母。
    /// </summary>
    /// <param name="s">需要逐一配對並移除的原始小寫字串。</param>
    /// <param name="t">包含 <paramref name="s"/> 所有字元且額外多一個字母的重排結果字串。</param>
    /// <returns>在可變動串列中最後未被移除的字元。</returns>
    public static char FindTheDifference2(string s, string t)
    {
        List<char> remainingCharacters = [.. t];

        // Remove 每次只會消耗一個相符字元，因此重複字母也能逐一正確配對，
        // 不會因為同值字元過多而一次移除過頭。
        foreach (char character in s)
        {
            remainingCharacters.Remove(character);
        }

        return remainingCharacters[0];
    }

    /// <summary>
    /// 將 <paramref name="s"/> 與 <paramref name="t"/> 的所有字元碼做 XOR，
    /// 讓成對出現的相同字元在任何順序下都能互相抵消。
    /// 輸入需符合題目契約：<paramref name="t"/> 是將 <paramref name="s"/> 重排後，
    /// 再額外加入一個小寫字母所形成的字串。
    /// 最後回傳 XOR 累加結果所留下的那個新增字元。
    /// </summary>
    /// <param name="s">其字元碼會與目標字串中對應字元互相抵消的原始小寫字串。</param>
    /// <param name="t">提供額外字元碼的重排結果字串。</param>
    /// <returns>在 XOR 抵消後唯一保留下來的新增字母。</returns>
    public static char FindTheDifference3(string s, string t)
    {
        int difference = 0;

        // 相同字元在 XOR 下會互相抵消，因此兩個迴圈都跑完後，
        // 累加器中只會剩下新增字母的字元碼。
        foreach (char character in s)
        {
            difference ^= character;
        }

        foreach (char character in t)
        {
            difference ^= character;
        }

        return (char)difference;
    }

    /// <summary>
    /// 記錄指定解法的一次驗證結果，比對預期值與實際值，更新統計計數，
    /// 並輸出 PASS 或 FAIL。
    /// </summary>
    /// <param name="solution">目前正在驗證的解法名稱。</param>
    /// <param name="expected">這組測試案例預期新增的字元。</param>
    /// <param name="actual">解法實際計算出的字元。</param>
    private static void Check(string solution, char expected, char actual)
    {
        s_checks++;
        bool passed = expected == actual;

        if (passed)
        {
            s_passed++;
        }

        Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | {solution} | Expected: '{expected}' | Actual: '{actual}'");
    }
}
