namespace leetcode_091;

class Program
{
    /// <summary>
    /// 91. Decode Ways
    /// https://leetcode.com/problems/decode-ways/description/?envType=problem-list-v2&envId=oizxjoit
    /// 91. 解码方法
    /// https://leetcode.cn/problems/decode-ways/description/
    /// 
    /// 題目描述：
    /// 一條包含字母 A-Z 的訊息可以按照以下規則進行編碼：
    /// 'A' -> "1"
    /// 'B' -> "2"
    /// ...
    /// 'Z' -> "26"
    /// 
    /// 要解碼一個已編碼的訊息，所有數字必須被映射回字母（可能有多種方法）。
    /// 例如，"11106" 可以被映射為：
    /// "AAJF"，對應 (1 1 10 6)
    /// "KJF"，對應 (11 10 6)
    /// 
    /// 給定一個數字字串 s，請計算有多少種解碼方法。
    /// 
    /// 解題出發點：
    /// 1. 我們使用動態規劃的方法解決此問題，因為需要記錄並利用先前計算的結果
    /// 2. 針對每個位置的數字，考慮兩種可能性：單獨解碼或與前一個數字組合解碼
    /// 3. 需要處理特殊情況如 "0" 不能單獨解碼，而 "10" 和 "20" 只能作為整體解碼
    /// 4. 使用 1-based 索引來簡化邊界條件和狀態轉移的處理(index 從 1 開始)
    /// 
    /// 注意:雙位數開頭不能是 0
    /// 例如: 06 -> 錯誤，因為 0 不能單獨解碼
    /// 所以區分兩種案例
    /// 1.個位數 (1-9) 可以單獨解碼
    /// 2.雙位數 (10-26) 可以組合解碼
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
