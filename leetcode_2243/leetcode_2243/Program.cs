using System.Text;

namespace leetcode_2243;

internal class Program
{
    /// <summary>
    /// <para>
    /// 2243. Calculate Digit Sum of a String
    /// https://leetcode.com/problems/calculate-digit-sum-of-a-string/description/
    ///
    /// Given digit string s and integer k, perform rounds while s.length &gt; k. In each round, split s into consecutive groups of size k, except the last may be shorter. Replace each group with the decimal string of its digit sum, then concatenate the replacements. Return s after all rounds.
    ///
    /// Example 1:
    /// Input: s = "11111222223", k = 3
    /// Output: "135"
    /// Explanation: Split into "111", "112", "222", "23", whose sums are 3, 4, 6, 5, giving "3465". Then split into "346", "5", whose sums are 13 and 5, giving "135". Its length is at most 3.
    ///
    /// Example 2:
    /// Input: s = "00000000", k = 3
    /// Output: "000"
    /// Explanation: Groups "000", "000", "00" have sums 0, 0, 0, producing "000", whose length equals 3.
    ///
    /// Constraints:
    /// - 1 &lt;= s.length &lt;= 100
    /// - 2 &lt;= k &lt;= 100
    /// - s consists only of digits.
    /// </para>
    /// <para>
    /// 2243. 計算字串的數字和
    /// https://leetcode.cn/problems/calculate-digit-sum-of-a-string/description/
    ///
    /// 給定數字字串 s 與整數 k，只要 s.length &gt; k 就執行一輪：將 s 分成大小為 k 的連續群組，最後一組可以較短；將每組替換為其各位數總和的十進位字串，再串接所有替換結果。完成所有輪次後回傳 s。
    ///
    /// 範例 1：
    /// 輸入：s = "11111222223", k = 3
    /// 輸出："135"
    /// 說明：分成 "111"、"112"、"222"、"23"，總和分別為 3、4、6、5，得到 "3465"；再分成 "346"、"5"，總和為 13、5，得到 "135"，長度不超過 3。
    ///
    /// 範例 2：
    /// 輸入：s = "00000000", k = 3
    /// 輸出："000"
    /// 說明：群組 "000"、"000"、"00" 的總和為 0、0、0，得到 "000"，長度等於 3。
    ///
    /// 限制條件：
    /// - 1 &lt;= s.length &lt;= 100
    /// - 2 &lt;= k &lt;= 100
    /// - s 僅由數字組成。
    /// </para>
    /// </summary>
    private static void Main()
    {
        (string Name, string Input, int K, string Expected)[] cases =
        [
            ("Official example", "11111222223", 3, "135"),
            ("All zeroes", "00000000", 3, "000"),
            ("Single character", "1", 2, "1"),
            ("Already k characters", "123", 3, "123"),
            ("Two complete groups", "123456", 3, "615"),
            ("Final short group", "1234567", 3, "127"),
            ("Multiple rounds", "987654321", 2, "36"),
            ("Near limit group", new string('9', 100), 99, "8919")
        ];

        int passed = 0;

        foreach ((string name, string input, int k, string expected) in cases)
        {
            string actual = DigitSum(input, k);
            bool isPass = actual == expected;

            Console.WriteLine($"Case: {name}; Input: \"{input}\", k = {k}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine(isPass ? "PASS" : "FAIL");

            if (isPass)
            {
                passed++;
            }
        }

        Console.WriteLine($"Summary: {passed}/{cases.Length} checks passed.");

        if (passed != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 將有效的純數字字串依 k 個字元分組後反覆縮約為各組數字和；每輪維持由左至右、不重疊且涵蓋
    /// 原字串的分組不變量。輸入為題目保證的長度 1 至 100 的純數字字串，以及範圍 2 至 100 的 k，
    /// 回傳長度不超過 k 的最終數字字串。
    /// </summary>
    public static string DigitSum(string s, int k)
    {
        while (s.Length > k)
        {
            StringBuilder next = new();

            // 每個群組從 groupStart 起，直到不超過字串尾端的 groupEnd 為止。
            for (int groupStart = 0; groupStart < s.Length; groupStart += k)
            {
                int groupEnd = Math.Min(groupStart + k, s.Length);
                int sum = 0;

                for (int i = groupStart; i < groupEnd; i++)
                {
                    sum += s[i] - '0';
                }

                next.Append(sum);
            }

            s = next.ToString();
        }

        return s;
    }
}