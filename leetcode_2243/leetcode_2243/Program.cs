using System.Text;

namespace leetcode_2243;

internal class Program
{
    /// <summary>
    /// LeetCode 2243 - Calculate Digit Sum of a String.
    /// LeetCode 2243 - 計算字串的數位和。
    /// English: https://leetcode.com/problems/calculate-digit-sum-of-a-string/
    /// 中文：https://leetcode.cn/problems/calculate-digit-sum-of-a-string/
    /// English: Repeatedly split a digit string into consecutive groups of at most k characters,
    /// replace each group with the decimal representation of its digit sum, and stop when the
    /// resulting string has at most k characters.
    /// 中文：反覆將數字字串由左至右切成每組至多 k 個字元，並以各組數字和的十進位表示取代；
    /// 當新字串長度不超過 k 時停止。
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