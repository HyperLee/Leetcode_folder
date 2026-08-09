namespace leetcode_412;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 412. Fizz Buzz
    /// https://leetcode.com/problems/fizz-buzz/description/
    /// <para>
    /// Given an integer n, return a string array answer (1-indexed) where:
    /// - answer[i] == "FizzBuzz" if i is divisible by 3 and 5.
    /// - answer[i] == "Fizz" if i is divisible by 3.
    /// - answer[i] == "Buzz" if i is divisible by 5.
    /// - answer[i] == i (as a string) if none of the above conditions are true.
    ///
    /// Example 1:
    /// Input: n = 3
    /// Output: ["1","2","Fizz"]
    ///
    /// Example 2:
    /// Input: n = 5
    /// Output: ["1","2","Fizz","4","Buzz"]
    ///
    /// Example 3:
    /// Input: n = 15
    /// Output: ["1","2","Fizz","4","Buzz","Fizz","7","8","Fizz","Buzz","11","Fizz","13","14","FizzBuzz"]
    ///
    /// Constraints:
    /// - 1 &lt;= n &lt;= 10^4
    /// </para>
    /// <para>
    /// 412. Fizz Buzz
    /// https://leetcode.cn/problems/fizz-buzz/description/
    ///
    /// 給定整數 n，回傳以 1 為起始索引的字串陣列 answer，其中：
    /// - 若 i 同時可被 3 與 5 整除，answer[i] == "FizzBuzz"。
    /// - 若 i 可被 3 整除，answer[i] == "Fizz"。
    /// - 若 i 可被 5 整除，answer[i] == "Buzz"。
    /// - 若以上條件皆不成立，answer[i] == i（字串形式）。
    ///
    /// 範例 1：
    /// 輸入：n = 3
    /// 輸出：["1","2","Fizz"]
    ///
    /// 範例 2：
    /// 輸入：n = 5
    /// 輸出：["1","2","Fizz","4","Buzz"]
    ///
    /// 範例 3：
    /// 輸入：n = 15
    /// 輸出：["1","2","Fizz","4","Buzz","Fizz","7","8","Fizz","Buzz","11","Fizz","13","14","FizzBuzz"]
    ///
    /// 限制條件：
    /// - 1 &lt;= n &lt;= 10^4
    /// </para>
    /// </summary>
    private static void Main()
    {
        (int N, string[] Expected)[] sequenceCases =
        [
            (1, ["1"]),
            (3, ["1", "2", "Fizz"]),
            (5, ["1", "2", "Fizz", "4", "Buzz"]),
            (15, ["1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz"]),
            (16, ["1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz", "16"])
        ];

        Console.WriteLine("LeetCode 412 acceptance harness");
        Console.WriteLine();

        for (int i = 0; i < sequenceCases.Length; i++)
        {
            (int n, string[] expected) = sequenceCases[i];
            IList<string> actual = FizzBuzz(n);
            string expectedText = FormatSequence(expected);
            string actualText = FormatSequence(actual);
            bool passed = expected.SequenceEqual(actual);

            Console.WriteLine($"Case {i + 1}: Full sequence");
            Console.WriteLine($"Input: n = {n}");
            RecordCheck(passed);
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | Full sequence | Expected: {expectedText} | Actual: {actualText}");
            Console.WriteLine();
        }

        const int upperBound = 10000;
        IList<string> upperBoundResult = FizzBuzz(upperBound);

        Console.WriteLine("Case 6: Upper-bound spot checks");
        Console.WriteLine($"Input: n = {upperBound}");

        (string Label, string Expected, string Actual)[] upperBoundChecks =
        [
            ("Result count", "10000", upperBoundResult.Count.ToString()),
            ("Value for 1", "1", upperBoundResult[0]),
            ("Value for 3", "Fizz", upperBoundResult[2]),
            ("Value for 5", "Buzz", upperBoundResult[4]),
            ("Value for 15", "FizzBuzz", upperBoundResult[14]),
            ("Value for 10000", "Buzz", upperBoundResult[9999])
        ];

        foreach ((string label, string expected, string actual) in upperBoundChecks)
        {
            bool passed = string.Equals(expected, actual, StringComparison.Ordinal);
            RecordCheck(passed);
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | {label} | Expected: {expected} | Actual: {actual}");
        }

        Console.WriteLine();

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 依 3、5 與 15 的整除規則產生 1 到 n 的 Fizz Buzz 字串；n 遵循 LeetCode 的正整數輸入契約，並依數值遞增順序回傳 IList&lt;string&gt;。
    /// </summary>
    public static IList<string> FizzBuzz(int n)
    {
        string[] result = new string[n];

        for (int i = 1; i <= n; i++)
        {
            // 同時為 3 與 5 的倍數必須優先處理，才能產生 FizzBuzz 而非單一標記。
            if (i % 15 == 0)
            {
                // 數值 i 從 1 起算，寫入結果陣列時需對應至從 0 起算的索引 i - 1。
                result[i - 1] = "FizzBuzz";
            }
            else if (i % 3 == 0)
            {
                result[i - 1] = "Fizz";
            }
            else if (i % 5 == 0)
            {
                result[i - 1] = "Buzz";
            }
            else
            {
                result[i - 1] = i.ToString();
            }
        }

        return result;
    }

    /// <summary>
    /// 接收布林驗證結果；遞增總檢查計數，若為 true 也遞增通過計數，且不回傳值。
    /// </summary>
    private static void RecordCheck(bool passed)
    {
        s_checks++;

        if (passed)
        {
            s_passed++;
        }
    }

    /// <summary>
    /// 接收有序字串序列，並回傳以方括號包覆、各項以雙引號顯示的字串。
    /// </summary>
    private static string FormatSequence(IEnumerable<string> values)
    {
        return $"[{string.Join(", ", values.Select(static value => $"\"{value}\""))}]";
    }
}
