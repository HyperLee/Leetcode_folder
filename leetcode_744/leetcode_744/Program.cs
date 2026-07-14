namespace leetcode_744;

internal static class Program
{
    /// <summary>
    /// 744. Find Smallest Letter Greater Than Target
    /// https://leetcode.com/problems/find-smallest-letter-greater-than-target/
    /// 744. 尋找比目標字母大的最小字母
    /// https://leetcode.cn/problems/find-smallest-letter-greater-than-target/
    /// English: Given a sorted array of letters and a target letter, return the smallest letter that is strictly greater than target; if none exists, return the first letter.
    /// 中文：給定已排序的字母陣列與目標字母，回傳嚴格大於目標的最小字母；若不存在，則回傳第一個字母。
    /// </summary>
    /// <param name="args">命令列參數；驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, char[] Letters, char Target, char Expected)[] testCases =
        {
            ("Case 1: Official example (target = 'a')", new[] { 'c', 'f', 'j' }, 'a', 'c'),
            ("Case 2: Official example (target = 'c')", new[] { 'c', 'f', 'j' }, 'c', 'f'),
            ("Case 3: Official example (target = 'd')", new[] { 'c', 'f', 'j' }, 'd', 'f'),
            ("Case 4: Official example (target = 'g')", new[] { 'c', 'f', 'j' }, 'g', 'j'),
            ("Case 5: Official example (target = 'j')", new[] { 'c', 'f', 'j' }, 'j', 'c'),
            ("Case 6: Minimum valid length", new[] { 'a', 'z' }, 'a', 'z'),
            ("Case 7: Duplicate letters", new[] { 'a', 'a', 'b', 'c', 'c' }, 'a', 'b')
        };

        List<CaseResult> checks = new();

        foreach ((string caseName, char[] letters, char target, char expected) in testCases)
        {
            char actual = NextGreatestLetter(letters, target);
            checks.Add(new CaseResult(caseName, $"letters = {FormatLetters(letters)}, target = {FormatChar(target)}", "Next greatest letter", FormatChar(expected), FormatChar(actual), expected == actual));
        }

        char[] maximumLetters = Enumerable.Repeat('m', 10_000).ToArray();
        maximumLetters[^1] = 'z';
        char maximumGreaterActual = NextGreatestLetter(maximumLetters, 'm');
        char maximumWrapActual = NextGreatestLetter(maximumLetters, 'z');
        checks.Add(new CaseResult("Case 8: Maximum-length spot checks", "letters = 9,999 × 'm' followed by 'z'", "Target = 'm'", "'z'", FormatChar(maximumGreaterActual), maximumGreaterActual == 'z'));
        checks.Add(new CaseResult("Case 8: Maximum-length spot checks", "letters = 9,999 × 'm' followed by 'z'", "Target = 'z'", "'m'", FormatChar(maximumWrapActual), maximumWrapActual == 'm'));

        int passedCount = 0;
        Console.WriteLine("LeetCode 744 acceptance harness");

        foreach (CaseResult check in checks)
        {
            Console.WriteLine();
            Console.WriteLine(check.CaseName);
            Console.WriteLine($"Input: {check.Input}");
            Console.WriteLine($"{(check.Passed ? "PASS" : "FAIL")} | {check.CheckName} | Expected: {check.Expected} | Actual: {check.Actual}");

            if (check.Passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{checks.Count} checks passed.");

        if (passedCount != checks.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 以 lower-bound 二分搜尋找出排序字元陣列中第一個嚴格大於目標字元的位置。有效輸入必須符合題目定義的非空、非遞減小寫字元陣列；若搜尋結果超出尾端，依題意環繞並回傳第一個字元。方法不會修改輸入，也不會寫入主控台。
    /// </summary>
    /// <param name="letters">依非遞減順序排列的題目有效字元陣列。</param>
    /// <param name="target">要尋找其下一個嚴格較大字元的題目有效小寫字元。</param>
    /// <returns>第一個嚴格大於 <paramref name="target"/> 的字元；若不存在則為 <paramref name="letters"/> 的第一個字元。</returns>
    public static char NextGreatestLetter(char[] letters, char target)
    {
        int low = 0;
        int high = letters.Length;

        while (low < high)
        {
            int middle = low + ((high - low) / 2);

            // 區間 [low, high) 始終保留第一個可能嚴格大於 target 的位置。
            if (letters[middle] > target)
            {
                high = middle;
            }
            else
            {
                low = middle + 1;
            }
        }

        return letters[low % letters.Length];
    }

    private static string FormatChar(char value)
    {
        return $"'{value}'";
    }

    private static string FormatLetters(IEnumerable<char> letters)
    {
        return $"[{string.Join(", ", letters)}]";
    }

    private readonly record struct CaseResult(string CaseName, string Input, string CheckName, string Expected, string Actual, bool Passed);
}
