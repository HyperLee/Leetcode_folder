namespace leetcode_091;

class Program
{
    /// <summary>
    /// <para>
    /// 91. Decode Ways
    /// https://leetcode.com/problems/decode-ways/description/
    ///
    /// You have intercepted a secret message encoded as a string of numbers. The message is decoded via
    /// the following mapping:
    /// "1" -&gt; 'A'
    /// "2" -&gt; 'B'
    /// ...
    /// "25" -&gt; 'Y'
    /// "26" -&gt; 'Z'
    /// However, while decoding the message, you realize that there are many different ways you can decode
    /// the message because some codes are contained in other codes ("2" and "5" vs "25").
    /// For example, "11106" can be decoded into:
    /// - "AAJF" with the grouping (1, 1, 10, 6)
    /// - "KJF" with the grouping (11, 10, 6)
    /// - The grouping (1, 11, 06) is invalid because "06" is not a valid code (only "6" is valid).
    /// Note: there may be strings that are impossible to decode.
    /// Given a string s containing only digits, return the number of ways to decode it. If the entire string
    /// cannot be decoded in any valid way, return 0.
    /// The test cases are generated so that the answer fits in a 32-bit integer.
    ///
    /// Example 1:
    /// Input: s = "12"
    /// Output: 2
    /// Explanation: "12" could be decoded as "AB" (1 2) or "L" (12).
    ///
    /// Example 2:
    /// Input: s = "226"
    /// Output: 3
    /// Explanation: "226" could be decoded as "BZ" (2 26), "VF" (22 6), or "BBF" (2 2 6).
    ///
    /// Example 3:
    /// Input: s = "06"
    /// Output: 0
    /// Explanation: "06" cannot be mapped to "F" because of the leading zero ("6" is different from "06").
    /// In this case, the string is not a valid encoding, so return 0.
    ///
    /// Constraints:
    /// 1 &lt;= s.length &lt;= 100
    /// s contains only digits and may contain leading zero(s).
    /// </para>
    /// <para>
    /// 91. 解碼方法
    /// https://leetcode.cn/problems/decode-ways/description/
    ///
    /// 你截獲了一則以數字字串編碼的祕密訊息。訊息依照下列對應關係解碼：
    /// "1" -&gt; 'A'
    /// "2" -&gt; 'B'
    /// ...
    /// "25" -&gt; 'Y'
    /// "26" -&gt; 'Z'
    /// 然而在解碼訊息時，你發現有許多不同的解碼方式，因為某些編碼包含在其他編碼中
    ///（例如 "2" 與 "5"，相對於 "25"）。
    /// 例如，"11106" 可以解碼為：
    /// - "AAJF"，分組方式為 (1, 1, 10, 6)
    /// - "KJF"，分組方式為 (11, 10, 6)
    /// - 分組 (1, 11, 06) 無效，因為 "06" 不是有效編碼（只有 "6" 有效）。
    /// 注意：可能存在完全無法解碼的字串。
    /// 給定只包含數字的字串 s，請回傳其解碼方式數量。若整個字串無法以任何有效方式解碼，
    /// 則回傳 0。
    /// 測試案例保證答案可容納於 32 位元整數中。
    ///
    /// 範例 1：
    /// 輸入：s = "12"
    /// 輸出：2
    /// 解釋："12" 可以解碼為 "AB"（1 2）或 "L"（12）。
    ///
    /// 範例 2：
    /// 輸入：s = "226"
    /// 輸出：3
    /// 解釋："226" 可以解碼為 "BZ"（2 26）、"VF"（22 6）或 "BBF"（2 2 6）。
    ///
    /// 範例 3：
    /// 輸入：s = "06"
    /// 輸出：0
    /// 解釋："06" 因為前導零而無法對應到 "F"（"6" 與 "06" 不同）。
    /// 在此情況下，該字串不是有效編碼，因此回傳 0。
    ///
    /// 限制條件：
    /// 1 &lt;= s.length &lt;= 100
    /// s 只包含數字，且可能包含前導零。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        (string Input, int Expected)[] testCases =
        {
            ("12", 2),
            ("226", 3),
            ("06", 0),
            ("0", 0),
            ("10", 1),
            ("27", 1),
            ("11106", 2),
            ("2101", 1),
            ("123123", 9)
        };

        (string Name, Func<string, int> Solver)[] solutions =
        {
            ("NumDecodings", program.NumDecodings),
            ("NumDecodings2", program.NumDecodings2)
        };

        int passed = 0;
        int total = testCases.Length * solutions.Length;

        Console.WriteLine("LeetCode 91. Decode Ways");

        foreach ((string name, Func<string, int> solver) in solutions)
        {
            foreach ((string input, int expected) in testCases)
            {
                if (RunTestCase(name, solver, input, expected))
                {
                    passed++;
                }
            }
        }

        Console.WriteLine($"Summary: {passed}/{total} passed.");
    }

    /// <summary>
    /// 執行一筆固定案例，呼叫指定解法並比較預期值與實際值。
    /// 輸入必須符合題目的純數字非空字串限制；輸出指出該次檢查是否通過。
    /// </summary>
    /// <param name="solutionName">顯示於測試結果中的解法名稱。</param>
    /// <param name="solution">接受數字字串並回傳解碼方法數的解法。</param>
    /// <param name="input">本次要解碼的純數字非空字串。</param>
    /// <param name="expected">預期的解碼方法數。</param>
    /// <returns>實際結果與預期結果相同時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    private static bool RunTestCase(
        string solutionName,
        Func<string, int> solution,
        string input,
        int expected)
    {
        int actual = solution(input);
        bool passed = actual == expected;

        Console.WriteLine(
            $"[{(passed ? "PASS" : "FAIL")}] {solutionName} | s=\"{input}\" | expected={expected} | actual={actual}");

        return passed;
    }

    /// <summary>
    /// 使用動態規劃陣列計算數字字串的解碼方法數。
    /// 令 <c>dp[i]</c> 表示前 i 個字元的解碼方法數，分別累加有效個位數 1–9 的
    /// <c>dp[i - 1]</c> 與有效雙位數 10–26 的 <c>dp[i - 2]</c>。
    /// 輸入必須是長度 1–100 的純數字非空字串；輸出為完整字串可被解碼的方法總數。
    /// </summary>
    /// <param name="s">符合題目限制、可能含前導零的純數字非空字串。</param>
    /// <returns>完整字串的有效解碼方法總數；無法解碼時回傳 0。</returns>
    public int NumDecodings(string s)
    {
        int n = s.Length;
        s = " " + s;
        int[] dp = new int[n + 1];

        // 空前綴只有一種組合方式，讓第一個合法數字能從 dp[0] 延伸。
        dp[0] = 1;
        char[] sChar = s.ToCharArray();

        for (int i = 1; i <= n; i++)
        {
            int singleDigit = sChar[i] - '0';
            int doubleDigits = (sChar[i - 1] - '0') * 10 + singleDigit;

            // 目前字元為 1–9 時，可以接在所有前 i-1 個字元的解法後面。
            if (1 <= singleDigit && singleDigit <= 9)
            {
                dp[i] = dp[i - 1];
            }

            // 最近兩個字元為 10–26 時，可以接在所有前 i-2 個字元的解法後面。
            if (10 <= doubleDigits && doubleDigits <= 26)
            {
                dp[i] += dp[i - 2];
            }
        }

        return dp[n];
    }

    /// <summary>
    /// 使用滾動變數計算數字字串的解碼方法數。
    /// 此解法沿用動態規劃轉移，但只保留 <c>dp[i - 2]</c> 與 <c>dp[i - 1]</c>，
    /// 將額外空間由 O(n) 降為 O(1)。
    /// 輸入必須是長度 1–100 的純數字非空字串；輸出為完整字串可被解碼的方法總數。
    /// </summary>
    /// <param name="s">符合題目限制、可能含前導零的純數字非空字串。</param>
    /// <returns>完整字串的有效解碼方法總數；無法解碼時回傳 0。</returns>
    public int NumDecodings2(string s)
    {
        int previousTwo = 1;
        int previousOne = s[0] == '0' ? 0 : 1;

        for (int i = 1; i < s.Length; i++)
        {
            int current = 0;

            // 非零字元可單獨解碼，因此延續前一個位置的所有解法。
            if (s[i] != '0')
            {
                current += previousOne;
            }

            int doubleDigits = (s[i - 1] - '0') * 10 + (s[i] - '0');

            // 10–26 可視為一個字母，因此加入前兩個位置的解法數。
            if (10 <= doubleDigits && doubleDigits <= 26)
            {
                current += previousTwo;
            }

            // 下一輪只需要目前位置與前一個位置的狀態。
            previousTwo = previousOne;
            previousOne = current;
        }

        return previousOne;
    }
}
