namespace leetcode_3498;

class Program
{
    /// <summary>
    /// 3498. Reverse Degree of a String
    /// https://leetcode.com/problems/reverse-degree-of-a-string/description/
    /// 3498. 字串的反轉度
    /// https://leetcode.cn/problems/reverse-degree-of-a-string/description/
    ///
    /// English version:
    /// Given a string s, calculate its reverse degree.
    ///
    /// The reverse degree is calculated as follows:
    /// For each character, multiply its position in the reversed alphabet
    /// ('a' = 26, 'b' = 25, ..., 'z' = 1) with its position in the string
    /// (1-indexed).
    /// Sum these products for all characters in the string.
    /// Return the reverse degree of s.
    ///
    /// 繁體中文版本：
    /// 給定一個字串 s，計算它的反轉度。
    ///
    /// 反轉度計算方式如下：
    /// 對每個字元，將它在反轉字母表中的位置
    /// （'a' = 26、'b' = 25、...、'z' = 1）
    /// 乘以它在字串中的位置（從 1 開始編號）。
    /// 將所有字元的乘積加總。
    /// 回傳 s 的反轉度。
    /// </summary>
    /// <param name="args">Command-line arguments; this program does not require interactive input.</param>
    /// <remarks>
    /// 程式不要求互動式輸入，Main 會使用固定案例同時驗證兩種解法，並以程序結束碼回報驗證結果。
    /// </remarks>
    static void Main(string[] args)
    {
        Program solution = new();
        (string Input, int Expected)[] testCases = new[]
        {
            ("abc", 148),
            ("zaza", 160),
            ("a", 26),
            ("z", 1)
        };

        int passedChecks = 0;
        int totalChecks = testCases.Length * 2;

        for (int i = 0; i < testCases.Length; i++)
        {
            (string input, int expected) = testCases[i];
            int actual = solution.ReverseDegree(input);
            bool passed = actual == expected;
            int actual2 = solution.ReverseDegree2(input);
            bool passed2 = actual2 == expected;

            Console.WriteLine($"Case {i + 1}: s = \"{input}\", Expected = {expected}");
            Console.WriteLine($"  ReverseDegree: Actual = {actual}, Result = {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine($"  ReverseDegree2: Actual = {actual2}, Result = {(passed2 ? "PASS" : "FAIL")}");

            if (passed)
            {
                passedChecks++;
            }

            if (passed2)
            {
                passedChecks++;
            }
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
        Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
    }

    /// <summary>
    /// 方法一：使用直接模擬計算字串的反轉度。
    /// 逐一走訪每個字元，以 26 減去該字元距離 'a' 的偏移量，取得它在反轉字母表中的位置，
    /// 再乘以從 1 開始的字串位置並累加，最後得到所有乘積的總和。
    /// </summary>
    /// <param name="s">長度介於 1 到 1000，且只包含小寫英文字母的字串。</param>
    /// <returns>每個字元的反轉字母位置乘以其 1-based 字串位置後的總和。</returns>
    public int ReverseDegree(string s)
    {
        int res = 0;
        for (int i = 1; i <= s.Length; i++)
        {
            // 'a' 的反轉位置是 26，字元每往後一格，反轉位置就少 1。
            res += (26 - (s[i - 1] - 'a')) * i;
        }

        return res;
    }

    /// <summary>
    /// 方法二：利用字元碼差值計算字串的反轉度。
    /// '{' 的字元碼剛好比 'z' 大 1，因此用 '{' 減去目前字元即可直接得到反轉字母位置，
    /// 再乘以該字元的 1-based 字串位置並累加。
    /// </summary>
    /// <param name="s">長度介於 1 到 1000，且只包含小寫英文字母的字串。</param>
    /// <returns>每個字元的反轉字母位置乘以其 1-based 字串位置後的總和。</returns>
    public int ReverseDegree2(string s)
    {
        int res = 0;
        for (int i = 0; i < s.Length; i++)
        {
            // '{' 緊接在 'z' 之後，所以 '{' - s[i] 會得到 26 到 1 的反轉字母位置。
            // i + 1 對應題目從 1 開始計算的字串位置。
            res += ('{' - s[i]) * (i + 1);
        }

        return res;
    }
}