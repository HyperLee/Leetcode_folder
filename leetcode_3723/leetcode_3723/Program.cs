using System.Text;

namespace leetcode_3723;

class Program
{
    /// <summary>
    /// 3723. Maximize Sum of Squares of Digits
    /// https://leetcode.com/problems/maximize-sum-of-squares-of-digits/description/
    ///
    /// English:
    /// You are given two positive integers num and sum.
    /// A positive integer n is good if it satisfies both of the following:
    /// - The number of digits in n is exactly num.
    /// - The sum of digits in n is exactly sum.
    /// The score of a good integer n is the sum of the squares of digits in n.
    /// Return a string denoting the good integer n that achieves the maximum score.
    /// If there are multiple possible integers, return the maximum one.
    /// If no such integer exists, return an empty string.
    ///
    /// 3723. 数位平方和的最大值
    /// https://leetcode.cn/problems/maximize-sum-of-squares-of-digits/description/
    ///
    /// 繁體中文：
    /// 給定兩個正整數 num 和 sum。
    /// 若正整數 n 同時符合以下兩項條件，則稱 n 為良好整數：
    /// - n 的位數恰好為 num。
    /// - n 的各位數字總和恰好為 sum。
    /// 良好整數 n 的分數為其各位數字平方和。
    /// 請回傳字串，表示分數最高的良好整數 n。
    /// 若有多個整數都能達到最高分，請回傳數值最大的那個。
    /// 若不存在符合條件的整數，請回傳空字串。
    /// </summary>
    /// <param name="args">本範例不讀取命令列參數；測試資料固定寫在 Main 中。</param>
    /// <remarks>
    /// 程式進入點會以固定案例呼叫兩種解法，逐例輸出期望值與實際結果，並在任一案例失敗時設定非零結束碼。
    /// </remarks>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (string Name, int Num, int Sum, string Expected)[] testCases =
        {
            ("Example 1", 2, 3, "30"),
            ("Example 2", 2, 17, "98"),
            ("Example 3 (impossible)", 1, 10, ""),
            ("Minimum feasible", 1, 1, "1"),
            ("Maximum digit sum", 2, 18, "99"),
            ("Multiple 9s and remainder", 4, 20, "9920")
        };

        int passedCount = 0;
        int totalCheckCount = testCases.Length * 2;
        foreach ((string Name, int Num, int Sum, string Expected) testCase in testCases)
        {
            (string MethodName, string Actual)[] results =
            {
                ("StringBuilder", solution.MaxSumOfSquares(testCase.Num, testCase.Sum)),
                ("string.Create", solution.MaxSumOfSquaresWithStringCreate(testCase.Num, testCase.Sum))
            };

            foreach ((string MethodName, string Actual) result in results)
            {
                bool passed = result.Actual == testCase.Expected;
                if (passed)
                {
                    passedCount++;
                }

                string status = passed ? "PASS" : "FAIL";
                Console.WriteLine($"{status} {testCase.Name} ({result.MethodName}): num={testCase.Num}, sum={testCase.Sum}, expected=\"{testCase.Expected}\", actual=\"{result.Actual}\"");
            }
        }

        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");
        Environment.ExitCode = passedCount == totalCheckCount ? 0 : 1;
    }

    /// <summary>
    /// 使用貪心法建構恰有 <paramref name="num"/> 位且數位和為 <paramref name="sum"/> 的整數，使數位平方和最大。
    /// 輸入須符合 1 ≤ num ≤ 2 × 10^5、1 ≤ sum ≤ 2 × 10^6；將數位和盡量集中到 9 與一個餘數可最大化平方和。
    /// 若 <paramref name="sum"/> 大於 <c>9 * num</c>，則不存在符合條件的整數並回傳空字串；否則回傳分數最高且同分時數值最大的整數字串。
    /// </summary>
    /// <param name="num">目標位數，範圍為 1 至 2 × 10^5。</param>
    /// <param name="sum">目標數位和，範圍為 1 至 2 × 10^6。</param>
    /// <returns>數位平方和最大的整數字串；若無法組成則回傳空字串。</returns>
    public string MaxSumOfSquares(int num, int sum)
    {
        // 每位最多為 9；超過 num 位可容納的最大數位和時沒有解。
        if (sum > 9 * num)
        {
            return string.Empty;
        }

        // 平方函數為凸函數，將總和盡量集中在 9 可得到最大的平方和。
        int fullNines = sum / 9;
        int remainder = sum % 9;
        StringBuilder result = new StringBuilder();
        result.Append('9', fullNines);
        if (remainder > 0)
        {
            result.Append((char)('0' + remainder));
        }

        // 高位優先放較大的數字以取得同分中的最大整數，其餘位數補 0。
        result.Append('0', num - result.Length);
        return result.ToString();
    }

    /// <summary>
    /// 使用與 <see cref="MaxSumOfSquares(int, int)"/> 相同的貪心配置，並以 <see cref="string.Create"/> 直接填入結果字串。
    /// 輸入須符合 1 ≤ num ≤ 2 × 10^5、1 ≤ sum ≤ 2 × 10^6；若 sum 大於 9 * num，則回傳空字串。
    /// </summary>
    /// <param name="num">目標位數，範圍為 1 至 2 × 10^5。</param>
    /// <param name="sum">目標數位和，範圍為 1 至 2 × 10^6。</param>
    /// <returns>數位平方和最大的整數字串；若無法組成則回傳空字串。</returns>
    public string MaxSumOfSquaresWithStringCreate(int num, int sum)
    {
        // 每位最多為 9；超過 num 位可容納的最大數位和時沒有解。
        if (sum > 9 * num)
        {
            return string.Empty;
        }

        int fullNines = sum / 9;
        int remainder = sum % 9;

        return string.Create(
            num,
            (FullNines: fullNines, Remainder: remainder),
            static (digits, state) =>
            {
                // 先將全部位置設為 0，再覆寫最高位的 9 與可能存在的餘數。
                digits.Fill('0');
                digits[..state.FullNines].Fill('9');
                if (state.Remainder > 0)
                {
                    digits[state.FullNines] = (char)('0' + state.Remainder);
                }
            });
    }
}
