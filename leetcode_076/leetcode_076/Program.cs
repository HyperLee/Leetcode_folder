namespace leetcode_076;

class Program
{
    /// <summary>
    /// 76. Minimum Window Substring
    /// https://leetcode.com/problems/minimum-window-substring/description/
    /// 76. 最小覆蓋子串
    /// https://leetcode.cn/problems/minimum-window-substring/description/
    /// 
    /// 要在字符串 S 中找出包含所有目標字符串 t 中字符的最小子串
    /// 子串必須包含 t 中所有字符，包括重複的字符
    /// 需要找出最小長度的符合條件子串 
    /// </summary>
    /// <remarks>
    /// 主要進入點會以固定案例執行兩種滑動視窗解法，比對實際值與期望值，
    /// 並將每筆結果及總通過數輸出至主控台。
    /// </remarks>
    /// <param name="args">命令列參數；此範例程式不使用命令列輸入。</param>
    static void Main(string[] args)
    {
        (string Name, string S, string T, string Expected)[] testCases =
        [
            ("官方一般案例", "ADOBECODEBANC", "ABC", "BANC"),
            ("單字元", "a", "a", "a"),
            ("目標字串較長", "a", "aa", ""),
            ("重複需求字元", "ADOBECODEBANCBA", "AABC", "ANCBA"),
            ("整段即答案", "ABC", "ABC", "ABC"),
            ("答案位於尾端", "bba", "ab", "ba")
        ];

        (string Name, Func<string, string, string> Solve)[] solutions =
        [
            (nameof(MinWindow), MinWindow),
            (nameof(MinWindowOptimized), MinWindowOptimized)
        ];

        int passed = 0;
        foreach ((string name, Func<string, string, string> solve) in solutions)
        {
            passed += RunTestCases(name, solve, testCases);
        }

        int total = solutions.Length * testCases.Length;
        Console.WriteLine($"總計: {passed}/{total} 通過");
        Environment.ExitCode = passed == total ? 0 : 1;
    }

    /// <summary>
    /// 依序執行指定解法的固定案例，使用序數字串比較檢查實際值與期望值，
    /// 將每筆 PASS/FAIL 與小計輸出至主控台。
    /// </summary>
    /// <param name="solutionName">顯示於測試區段標題的解法名稱。</param>
    /// <param name="solve">接受來源字串與目標字串，並回傳最小覆蓋子串的解法。</param>
    /// <param name="testCases">包含案例名稱、輸入字串及期望結果的固定案例集合。</param>
    /// <returns>實際結果符合期望結果的案例數量。</returns>
    private static int RunTestCases(
        string solutionName,
        Func<string, string, string> solve,
        (string Name, string S, string T, string Expected)[] testCases)
    {
        Console.WriteLine($"===== {solutionName} =====");

        int passed = 0;
        foreach ((string name, string s, string t, string expected) in testCases)
        {
            string actual = solve(s, t);
            bool isPassed = string.Equals(actual, expected, StringComparison.Ordinal);
            passed += isPassed ? 1 : 0;

            string status = isPassed ? "PASS" : "FAIL";
            Console.WriteLine(
                $"[{status}] {name}: s=\"{s}\", t=\"{t}\", " +
                $"expected=\"{FormatResult(expected)}\", actual=\"{FormatResult(actual)}\"");
        }

        Console.WriteLine($"小計: {passed}/{testCases.Length} 通過");
        Console.WriteLine();
        return passed;
    }

    /// <summary>
    /// 將演算法結果轉成易讀的主控台文字，避免空字串在輸出中無法辨識。
    /// </summary>
    /// <param name="value">要顯示的最小覆蓋子串結果。</param>
    /// <returns>非空結果的原值；空字串則回傳 <c>&lt;empty&gt;</c>。</returns>
    private static string FormatResult(string value)
    {
        return value.Length == 0 ? "<empty>" : value;
    }

    /// <summary>
    /// 使用滑動視窗與兩個 ASCII 次數陣列尋找最小覆蓋子串。
    /// 右邊界負責納入字元；視窗涵蓋目標後，左邊界持續收縮並更新最短答案。
    /// </summary>
    /// <param name="S">只含大小寫英文字母且長度至少為 1 的來源字串。</param>
    /// <param name="t">只含大小寫英文字母且長度至少為 1 的目標字串，重複字元必須全部被涵蓋。</param>
    /// <returns>涵蓋 <paramref name="t"/> 全部字元的最短子串；不存在時回傳空字串。</returns>
    public static string MinWindow(string S, string t)
    {
        char[] s = S.ToCharArray();
        int m = s.Length;
        int ansLeft = -1;
        int ansRight = m;
        int[] cntS = new int[128];
        int[] cntT = new int[128];

        foreach (char c in t)
        {
            cntT[c]++;
        }

        int left = 0;
        for (int right = 0; right < m; right++)
        {
            // 先擴張右界取得可行視窗，再收縮左界以逼近最短答案。
            cntS[s[right]]++;

            while (isCovered(cntS, cntT))
            {
                if (right - left < ansRight - ansLeft)
                {
                    ansLeft = left;
                    ansRight = right;
                }

                cntS[s[left]]--;
                left++;
            }
        }

        return ansLeft < 0 ? "" : S.Substring(ansLeft, ansRight - ansLeft + 1);
    }

    /// <summary>
    /// 比較兩個固定長度的 ASCII 次數陣列，判斷目前視窗是否已滿足目標字串
    /// 每一種必要字元的數量，供滑動視窗決定是否能繼續收縮。
    /// </summary>
    /// <param name="cntS">目前視窗的 ASCII 字元次數陣列，長度必須至少為 128。</param>
    /// <param name="cntT">目標字串的 ASCII 字元次數陣列，長度必須至少為 128。</param>
    /// <returns><see langword="true"/> 表示視窗涵蓋全部目標字元；否則為 <see langword="false"/>。</returns>
    private static bool isCovered(int[] cntS, int[] cntT)
    {
        for (int i = 0; i < 128; i++)
        {
            // 非目標字元不影響覆蓋條件；任一必要字元不足即不可收縮。
            if (cntT[i] > 0 && cntS[i] < cntT[i])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 使用單一 ASCII 差額陣列與剩餘需求計數執行滑動視窗。
    /// 右界遇到仍缺少的字元時遞減 <c>count</c>；當 <c>count</c> 為 0，
    /// 左界持續收縮，直到移除必要字元使視窗再次失效。
    /// </summary>
    /// <param name="s">只含大小寫英文字母且長度至少為 1 的來源字串。</param>
    /// <param name="t">只含大小寫英文字母且長度至少為 1 的目標字串，重複字元必須全部被涵蓋。</param>
    /// <returns>涵蓋 <paramref name="t"/> 全部字元的最短子串；不存在時回傳空字串。</returns>
    public static string MinWindowOptimized(string s, string t)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t))
        {
            return "";
        }

        int[] map = new int[128];
        int count = t.Length;
        int start = 0;
        int end = 0;
        int minStart = 0;
        int minLen = int.MaxValue;

        foreach (char c in t)
        {
            map[c]++;
        }

        while (end < s.Length)
        {
            // 正值代表仍缺少該字元；零或負值代表目前視窗已足夠或有多餘。
            if (map[s[end]]-- > 0)
            {
                count--;
            }

            end++;

            while (count == 0)
            {
                if (end - start < minLen)
                {
                    minStart = start;
                    minLen = end - start;
                }

                // 移除前的差額為零，表示即將拿走剛好足夠的必要字元。
                if (map[s[start]]++ == 0)
                {
                    count++;
                }

                start++;
            }
        }

        return minLen == int.MaxValue ? "" : s.Substring(minStart, minLen);
    }
}