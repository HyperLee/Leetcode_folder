namespace leetcode_2609;

class Program
{
    /// <summary>
    /// 2609. Find the Longest Balanced Substring of a Binary String
    /// https://leetcode.com/problems/find-the-longest-balanced-substring-of-a-binary-string/description/
    /// 2609. 最長平衡子字串
    /// https://leetcode.cn/problems/find-the-longest-balanced-substring-of-a-binary-string/description/
    ///
    /// English:
    /// You are given a binary string s consisting only of zeroes and ones.
    ///
    /// A substring of s is considered balanced if all zeroes are before ones and the number of zeroes
    /// is equal to the number of ones inside the substring. Notice that the empty substring is considered
    /// a balanced substring.
    ///
    /// Return the length of the longest balanced substring of s.
    ///
    /// A substring is a contiguous sequence of characters within a string.
    ///
    /// 繁體中文：
    /// 給定一個只由 0 和 1 組成的二進位字串 s。
    ///
    /// 如果 s 的一個子字串中所有的 0 都位於 1 之前，且子字串中 0 和 1 的數量相等，
    /// 則稱此子字串為平衡子字串。請注意，空字串也視為平衡子字串。
    ///
    /// 回傳 s 中最長平衡子字串的長度。
    ///
    /// 子字串是字串中一段連續的字元序列。
    /// </summary>
    /// <remarks>
    /// 使用固定案例依序執行三個解法，並輸出各解法的實際值、預期值與 PASS/FAIL 結果。
    /// </remarks>
    /// <param name="args">命令列參數；本範例不使用此參數。</param>
    static void Main(string[] args)
    {
        Program solution = new();
        (string Name, Func<string, int> Solve)[] solutions =
        [
            ("FindTheLongestBalancedSubstring", solution.FindTheLongestBalancedSubstring),
            ("FindTheLongestBalancedSubstring2", solution.FindTheLongestBalancedSubstring2),
            ("FindTheLongestBalancedSubstring3", solution.FindTheLongestBalancedSubstring3)
        ];
        (string Input, int Expected)[] testCases =
        [
            ("01000111", 6),
            ("00111", 4),
            ("111", 0),
            ("", 0),
            ("0", 0),
            ("0000", 0),
            ("1111", 0),
            ("01", 2),
            ("000111", 6),
            ("00110011", 4)
        ];

        int passedTests = 0;
        int totalTests = testCases.Length * solutions.Length;

        Console.WriteLine("LeetCode 2609 - Find the Longest Balanced Substring of a Binary String");
        Console.WriteLine();

        for (int caseIndex = 0; caseIndex < testCases.Length; caseIndex++)
        {
            (string input, int expected) = testCases[caseIndex];
            passedTests += RunTestCase(caseIndex + 1, input, expected, solutions);
        }

        Console.WriteLine($"{passedTests}/{totalTests} tests passed.");
    }

    /// <summary>
    /// 執行一組最長平衡子字串測試，讓所有指定解法使用相同輸入，並逐一比對實際值與預期值。
    /// 輸入必須提供案例編號、二進位字串、預期長度及待驗證解法；輸出為本案例通過的解法數量。
    /// </summary>
    /// <param name="caseNumber">從 1 開始顯示的測試案例編號。</param>
    /// <param name="input">要交給各解法處理的二進位字串。</param>
    /// <param name="expected">此案例預期得到的最長平衡子字串長度。</param>
    /// <param name="solutions">解法名稱及對應函式的集合。</param>
    /// <returns>實際結果等於預期結果的解法數量。</returns>
    private static int RunTestCase(
        int caseNumber,
        string input,
        int expected,
        (string Name, Func<string, int> Solve)[] solutions)
    {
        int passedTests = 0;
        string displayedInput = $"\"{input}\"";

        Console.WriteLine($"Case {caseNumber}: s = {displayedInput}, expected = {expected}");

        foreach ((string name, Func<string, int> solve) in solutions)
        {
            int actual = solve(input);
            bool passed = actual == expected;

            if (passed)
            {
                passedTests++;
            }

            Console.WriteLine(
                $"  {name,-36} actual = {actual}, expected = {expected} => {(passed ? "PASS" : "FAIL")}");
        }

        Console.WriteLine();
        return passedTests;
    }

    /// <summary>
    /// 以單次逐字元掃描找出最長平衡子字串。
    /// 掃描時分別記錄目前候選區段中的連續 0 與連續 1 數量；遇到新的 0 區段便重設計數，
    /// 遇到 1 時以兩段較短者乘以 2 更新答案。
    /// 輸入應為長度 1 到 50、且僅含 0 與 1 的字串；輸出為最長平衡子字串長度。
    /// </summary>
    /// <param name="s">題目限制內的非 null 二進位字串。</param>
    /// <returns>最長平衡子字串的長度；不存在非空平衡子字串時回傳 0。</returns>
    /// <remarks>時間複雜度為 O(n)，額外空間複雜度為 O(1)。</remarks>
    public int FindTheLongestBalancedSubstring(string s)
    {
        int maxLength = 0;
        int zeroCount = 0;
        int oneCount = 0;

        for (int index = 0; index < s.Length; index++)
        {
            if (s[index] == '0')
            {
                // 前一個字元是 1 時，舊候選區段不能跨越 10 邊界，必須從新的 0 區段重新計數。
                if (index == 0 || s[index - 1] == '1')
                {
                    zeroCount = 1;
                    oneCount = 0;
                }
                else
                {
                    zeroCount++;
                }

                continue;
            }

            oneCount++;

            // 連續 0 與其後連續 1 能配對的數量，由兩段中較短的一段決定。
            int balancedLength = Math.Min(zeroCount, oneCount) * 2;
            maxLength = Math.Max(maxLength, balancedLength);
        }

        return maxLength;
    }

    /// <summary>
    /// 以成對區段掃描找出最長平衡子字串。
    /// 每輪先計算一段連續 0，再計算緊接其後的一段連續 1，使用兩段較短長度的兩倍更新答案；
    /// 掃描索引會直接前進到下一組候選區段。
    /// 輸入應為長度 1 到 50、且僅含 0 與 1 的字串；輸出為最長平衡子字串長度。
    /// </summary>
    /// <param name="s">題目限制內的非 null 二進位字串。</param>
    /// <returns>最長平衡子字串的長度；不存在非空平衡子字串時回傳 0。</returns>
    /// <remarks>時間複雜度為 O(n)，額外空間複雜度為 O(1)。</remarks>
    public int FindTheLongestBalancedSubstring2(string s)
    {
        int index = 0;
        int maxLength = 0;

        while (index < s.Length)
        {
            int zeroCount = 0;
            int oneCount = 0;

            while (index < s.Length && s[index] == '0')
            {
                zeroCount++;
                index++;
            }

            // 只計算緊接在這段 0 後方的 1，確保候選子字串符合 0...01...1。
            while (index < s.Length && s[index] == '1')
            {
                oneCount++;
                index++;
            }

            int balancedLength = Math.Min(zeroCount, oneCount) * 2;
            maxLength = Math.Max(maxLength, balancedLength);
        }

        return maxLength;
    }

    /// <summary>
    /// 以相鄰同字元分組找出最長平衡子字串。
    /// 掃描時保存上一組與目前組的長度；只有目前組為 1 時，上一組 0 與目前組 1
    /// 才能依兩組較短長度的兩倍形成平衡子字串。
    /// 輸入應為長度 1 到 50、且僅含 0 與 1 的字串；輸出為最長平衡子字串長度。
    /// </summary>
    /// <param name="s">題目限制內的非 null 二進位字串。</param>
    /// <returns>最長平衡子字串的長度；不存在非空平衡子字串時回傳 0。</returns>
    /// <remarks>時間複雜度為 O(n)，額外空間複雜度為 O(1)。</remarks>
    public int FindTheLongestBalancedSubstring3(string s)
    {
        int maxLength = 0;
        int previousRunLength = 0;
        int currentRunLength = 0;

        for (int index = 0; index < s.Length; index++)
        {
            currentRunLength++;

            bool isEndOfRun = index == s.Length - 1 || s[index] != s[index + 1];
            if (!isEndOfRun)
            {
                continue;
            }

            // 二進位分組交替出現；目前組為 1 時，上一組若存在就必然是一段連續 0。
            if (s[index] == '1')
            {
                int balancedLength = Math.Min(previousRunLength, currentRunLength) * 2;
                maxLength = Math.Max(maxLength, balancedLength);
            }

            previousRunLength = currentRunLength;
            currentRunLength = 0;
        }

        return maxLength;
    }
}