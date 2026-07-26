using System.Globalization;

namespace leetcode_2785;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 2785. Sort Vowels in a String
    /// https://leetcode.com/problems/sort-vowels-in-a-string/
    /// 2785. 將字串中的母音字母排序
    /// https://leetcode.cn/problems/sort-vowels-in-a-string/
    /// Given a string, sort only its vowels by ASCII order while preserving every non-vowel position.
    /// 給定一個字串，僅依 ASCII 遞增排序其中的母音，其他非母音字元的位置必須維持不變。
    /// </summary>
    private static void Main()
    {
        HarnessCase[] cases =
        [
            RunStringCase(1, "lEetcOde", "lEOtcede", "Mixed-case vowel sorting"),
            RunStringCase(2, "lYmpH", "lYmpH", "No vowels"),
            RunStringCase(3, "a", "a", "Single vowel"),
            RunStringCase(4, "AaEe", "AEae", "ASCII order"),
            RunTurkishCultureCase(),
            RunLargeInputCase()
        ];

        Console.WriteLine("LeetCode 2785 acceptance harness");
        Console.WriteLine();

        foreach (HarnessCase testCase in cases)
        {
            foreach (string headerLine in testCase.HeaderLines)
            {
                Console.WriteLine(headerLine);
            }

            foreach (CheckResult check in testCase.Checks)
            {
                RecordCheck(check.Passed);
                Console.WriteLine(FormatCheck(check));
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 收集輸入中的母音、以 ASCII 遞增排序後依序放回原本的母音位置；非母音字元不會被移動或替換。
    /// </summary>
    /// <param name="s">符合 LeetCode 題目契約的輸入字串。</param>
    /// <returns>僅母音字元經 ASCII 排序後得到的新字串。</returns>
    public static string SortVowels(string s)
    {
        char[] chars = s.ToCharArray();
        List<char> vowels = [];

        foreach (char c in chars)
        {
            if (IsVowel(c))
            {
                vowels.Add(c);
            }
        }

        vowels.Sort();

        int vowelIndex = 0;
        for (int i = 0; i < chars.Length; i++)
        {
            if (IsVowel(chars[i]))
            {
                chars[i] = vowels[vowelIndex++];
            }
        }

        return new string(chars);
    }

    /// <summary>
    /// 以題目定義的十個 ASCII 字元直接判斷是否為母音，避免目前文化特性影響 I/i 的轉換結果。
    /// </summary>
    /// <param name="c">要判斷的字元。</param>
    /// <returns>當字元是 A、E、I、O、U 或其小寫形式時為 true，否則為 false。</returns>
    private static bool IsVowel(char c)
    {
        return c is 'A' or 'E' or 'I' or 'O' or 'U' or 'a' or 'e' or 'i' or 'o' or 'u';
    }

    /// <summary>
    /// 執行一般字串案例，並回傳由 Main 輸出的標頭與驗證結果。
    /// </summary>
    private static HarnessCase RunStringCase(int caseNumber, string input, string expected, string label)
    {
        string actual = SortVowels(input);
        CheckResult check = CreateCheck(string.Equals(expected, actual, StringComparison.Ordinal), "Sorted result", expected, actual);

        return new HarnessCase(
            [$"Case {caseNumber}: {label}", $"Input: {input}"],
            [check]);
    }

    /// <summary>
    /// 暫時切換至土耳其文化特性，回傳不受文化特性大小寫規則影響的驗證結果，最後一定還原原有文化特性。
    /// </summary>
    private static HarnessCase RunTurkishCultureCase()
    {
        CultureInfo originalCulture = CultureInfo.CurrentCulture;

        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("tr-TR");
            string actual = SortVowels("IbA");
            CheckResult check = CreateCheck(string.Equals("AbI", actual, StringComparison.Ordinal), "Sorted result", "AbI", actual);

            return new HarnessCase(
                ["Case 5: Turkish culture regression", "Culture: tr-TR", "Input: IbA"],
                [check]);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    /// <summary>
    /// 建立十萬字元的大型輸入，並回傳長度、兩個區段及完整結果的驗證資料。
    /// </summary>
    private static HarnessCase RunLargeInputCase()
    {
        const int halfLength = 50_000;
        const int totalLength = halfLength * 2;
        string input = new string('u', halfLength) + new string('A', halfLength);
        string expected = new string('A', halfLength) + new string('u', halfLength);
        string actual = SortVowels(input);

        return new HarnessCase(
            ["Case 6: Large input", "Input: 50000 'u' characters followed by 50000 'A' characters"],
            [
                CreateCheck(actual.Length == totalLength, "Result length", totalLength.ToString(), actual.Length.ToString()),
                CreateCheck(AllCharactersEqual(actual.AsSpan(0, halfLength), 'A'), "First half", "all A", DescribeCharacterRange(actual.AsSpan(0, halfLength), 'A')),
                CreateCheck(AllCharactersEqual(actual.AsSpan(halfLength, halfLength), 'u'), "Second half", "all u", DescribeCharacterRange(actual.AsSpan(halfLength, halfLength), 'u')),
                CreateCheck(string.Equals(expected, actual, StringComparison.Ordinal), "Exact result", "50000 A followed by 50000 u", actual == expected ? "50000 A followed by 50000 u" : "different")
            ]);
    }

    /// <summary>
    /// 檢查指定字元範圍中的每一個字元是否都等於預期字元。
    /// </summary>
    private static bool AllCharactersEqual(ReadOnlySpan<char> values, char expected)
    {
        foreach (char value in values)
        {
            if (value != expected)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 產生大型字元區段的簡短驗證描述，避免將十萬字元測試資料完整輸出至主控台。
    /// </summary>
    private static string DescribeCharacterRange(ReadOnlySpan<char> values, char expected)
    {
        return AllCharactersEqual(values, expected) ? $"all {expected}" : "contains another character";
    }

    /// <summary>
    /// 建立一項驗證結果，讓 Main 集中負責計數與主控台輸出。
    /// </summary>
    private static CheckResult CreateCheck(bool passed, string label, string expected, string actual)
    {
        return new CheckResult(passed, label, expected, actual);
    }

    /// <summary>
    /// 將驗證結果格式化為 acceptance harness 的單行輸出。
    /// </summary>
    private static string FormatCheck(CheckResult check)
    {
        return $"{(check.Passed ? "PASS" : "FAIL")} | {check.Label} | Expected: {check.Expected} | Actual: {check.Actual}";
    }

    /// <summary>
    /// 記錄 Main 即將輸出的驗證結果，遞增總檢查計數，通過時也遞增通過計數。
    /// </summary>
    private static void RecordCheck(bool passed)
    {
        s_checks++;

        if (passed)
        {
            s_passed++;
        }
    }

    private sealed record HarnessCase(string[] HeaderLines, CheckResult[] Checks);

    private sealed record CheckResult(bool Passed, string Label, string Expected, string Actual);
}
