namespace leetcode_443;

internal static class Program
{
    /// <summary>
    /// <para>443. String Compression / 壓縮字串</para>
    /// <para>English: Compress consecutive groups of characters in-place and return the length of the compressed prefix.</para>
    /// <para>繁體中文：將連續相同字元原地壓縮，並回傳壓縮後前綴的長度。</para>
    /// <para>https://leetcode.com/problems/string-compression/</para>
    /// <para>https://leetcode.cn/problems/string-compression/</para>
    /// </summary>
    private static void Main()
    {
        Console.WriteLine("LeetCode 443 acceptance harness");
        Console.WriteLine();

        CaseResult[] results =
        [
            RunCase("Case 1: Repeated runs", "aabbccc".ToCharArray(), "a2b2c3"),
            RunCase("Case 2: Single character", "a".ToCharArray(), "a"),
            RunCase("Case 3: Mixed run lengths", "aabcccccaaa".ToCharArray(), "a2bc5a3"),
            RunCase("Case 4: Two-digit letter count", new string('a', 12).ToCharArray(), "a12"),
            RunCase("Case 5: Digit character with two-digit count", new string('1', 12).ToCharArray(), "112"),
            RunCase("Case 6: Four-digit count", new string('a', 2000).ToCharArray(), "a2000")
        ];

        int passedChecks = 0;

        foreach (CaseResult result in results)
        {
            if (result.Passed)
            {
                passedChecks++;
            }

            Console.WriteLine(result.Name);
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine($"Expected: {result.Expected} (length {result.Expected.Length})");
            Console.WriteLine($"Actual: {result.Actual} (length {result.ActualLength})");
            Console.WriteLine(result.Passed ? "PASS" : "FAIL");
            Console.WriteLine();
        }

        int totalChecks = results.Length;

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用讀寫雙指標原地壓縮連續相同的字元，回傳壓縮前綴的長度。
    /// </summary>
    public static int Compress(char[] chars)
    {
        int read = 0;
        int write = 0;

        while (read < chars.Length)
        {
            char character = chars[read];
            int runStart = read;

            while (read < chars.Length && chars[read] == character)
            {
                read++;
            }

            int runLength = read - runStart;

            // 每輪結束時 [0, write) 恰為所有已處理 runs 的正確壓縮前綴。
            chars[write++] = character;

            if (runLength > 1)
            {
                int digitStart = write;

                while (runLength > 0)
                {
                    chars[write++] = (char)('0' + (runLength % 10));
                    runLength /= 10;
                }

                // 數字先由個位數寫入；反轉後 [digitStart, write) 保持正常十進位順序。
                Reverse(chars, digitStart, write - 1);
            }
        }

        return write;
    }

    /// <summary>
    /// 原地反轉指定範圍，將倒序寫入的十進位數字改回正常順序。
    /// </summary>
    private static void Reverse(char[] chars, int left, int right)
    {
        while (left < right)
        {
            (chars[left], chars[right]) = (chars[right], chars[left]);
            left++;
            right--;
        }
    }

    /// <summary>
    /// 執行一個壓縮案例，核對回傳長度與實際壓縮前綴，並回傳驗證結果。
    /// </summary>
    private static CaseResult RunCase(string name, char[] input, string expected)
    {
        char[] chars = (char[])input.Clone();
        int actualLength = Compress(chars);
        string actual = new string(chars, 0, actualLength);
        bool passed = actualLength == expected.Length && actual == expected;

        return new CaseResult(name, FormatInput(input), expected, actual, actualLength, passed);
    }

    /// <summary>
    /// 將案例輸入轉為可閱讀文字；大型案例僅顯示字元與長度，避免輸出完整內容。
    /// </summary>
    private static string FormatInput(char[] chars)
    {
        return chars.Length > 50
            ? $"{chars.Length} '{chars[0]}' characters"
            : new string(chars);
    }

    private readonly record struct CaseResult(
        string Name,
        string Input,
        string Expected,
        string Actual,
        int ActualLength,
        bool Passed);
}
