namespace leetcode_1541;

class Program
{
    /// <summary>
    /// 1541. Minimum Insertions to Balance a Parentheses String
    /// https://leetcode.com/problems/minimum-insertions-to-balance-a-parentheses-string/description/
    /// 1541. 平衡括號字串的最少插入次數
    /// https://leetcode.cn/problems/minimum-insertions-to-balance-a-parentheses-string/description/
    ///
    /// English (original)
    /// Given a parentheses string s containing only the characters '(' and ')'. A parentheses string is <b>balanced</b> if:
    /// <list type="bullet">
    /// Any left parenthesis '(' must have a corresponding two consecutive right parenthesis '))'.</description></item>
    /// Left parenthesis '(' must go before the corresponding two consecutive right parenthesis '))'.</description></item>
    /// 
    /// In other words, we treat '(' as an opening parenthesis and '))' as a closing parenthesis.
    /// <list type="bullet">
    /// For example, "())", "())(())))" and "(())())))" are balanced, ")()", "()))" and "(()))" are not balanced.</description></item>
    /// 
    /// You can insert the characters '(' and ')' at any position of the string to balance it if needed.
    /// Return <i>the minimum number of insertions</i> needed to make s balanced.
    ///
    /// 繁體中文
    /// 給定一個只包含字元 '(' 與 ')' 的括號字串 s。若括號字串符合下列條件，則稱為<b>平衡</b>：
    /// <list type="bullet">
    /// 每個左括號 '(' 都必須有兩個連續的右括號 '))' 與之對應。</description></item>
    /// 左括號 '(' 必須出現在與之對應的兩個連續右括號 '))' 之前。</description></item>
    /// 
    /// 換句話說，我們將 '(' 視為開括號，將 '))' 視為閉括號。
    /// <list type="bullet">
    /// 例如，"())"、"())(())))" 與 "(())())))" 是平衡的；")()"、"()))" 與 "(()))" 則不是平衡的。</description></item>
    /// 
    /// 必要時，你可以在字串的任意位置插入字元 '(' 與 ')'，使字串平衡。
    /// 回傳使 s 平衡所需的<i>最少插入次數</i>。
    /// </summary>
    /// <remarks>
    /// 執行兩種貪婪解法的固定測試，逐項輸出預期值、實際值與 PASS／FAIL。
    /// 不需要互動輸入；任何檢查失敗時將程序退出碼設為 1，全部通過則為 0。
    /// </remarks>
    /// <param name="args">命令列參數；本測試入口不使用此參數。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (string Name, string Input, int Expected)[] cases =
        [
            ("Official example 1", "(()))", 1),
            ("Official example 2", "())", 0),
            ("Official example 3", "))())(", 3),
            ("Single opening", "(", 2),
            ("Single closing", ")", 2),
            ("Two openings", "((", 4),
            ("Closing pair without opening", "))", 1),
            ("Three closings", ")))", 3),
            ("Incomplete closing pair", "()", 1),
            ("Closing before opening", ")(", 4),
            ("Interrupted closing pairs", "()()", 2),
            ("Balanced nesting", "(())))", 0),
            ("Maximum length openings", new string('(', 100_000), 200_000),
            ("Maximum length closings", new string(')', 100_000), 50_000)
        ];

        int passed = 0;
        Console.WriteLine("LeetCode 1541 - Minimum Insertions");
        foreach ((string name, string input, int expected) in cases)
        {
            // 長輸入只顯示摘要，仍將完整字串交給兩種解法。
            string displayInput = input.Length <= 20
                ? $"\"{input}\""
                : $"{input.Length} copies of '{input[0]}'";
            Console.WriteLine($"[{name}] Input={displayInput}");
            passed += CheckResult(nameof(MinInsertions), expected, solution.MinInsertions(input)) ? 1 : 0;
            passed += CheckResult(nameof(MinInsertions2), expected, solution.MinInsertions2(input)) ? 1 : 0;
        }

        int total = cases.Length * 2;
        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        Environment.ExitCode = passed == total ? 0 : 1;
    }

    /// <summary>
    /// 計算使合法括號字串平衡所需的最少插入次數。
    /// 使用貪婪成對掃描：記錄尚未匹配的左括號，將右括號視為連續的雙字元單位；
    /// 只在缺少左括號或缺少第二個右括號時插入，最後補足剩餘左括號的配對。
    /// </summary>
    /// <param name="s">只包含 '(' 與 ')'、長度介於 1 與 100,000 的字串；不修改輸入。</param>
    /// <returns>使每個左括號對應連續兩個右括號所需的最少插入次數。</returns>
    /// <remarks>時間複雜度 O(n)，額外空間複雜度 O(1)。</remarks>
    public int MinInsertions(string s)
    {
        int insertions = 0;
        int leftCount = 0;
        int length = s.Length;
        int index = 0;

        while (index < length)
        {
            char c = s[index];
            if (c == '(')
            {
                leftCount++;
                index++;
            }
            else
            {
                // 這一組右括號必須匹配之前的左括號；沒有可用者就補一個。
                if (leftCount > 0)
                {
                    leftCount--;
                }
                else
                {
                    insertions++;
                }

                // 只將原字串中相鄰的兩個右括號一起消耗，不能跨越左括號配對。
                if (index < length - 1 && s[index + 1] == ')')
                {
                    index += 2;
                }
                else
                {
                    // 單獨的右括號必須立即補成一對，才能繼續處理下一個左括號。
                    insertions++;
                    index++;
                }
            }
        }

        // 每個尚未匹配的左括號各需要完整的一組右括號。
        insertions += leftCount * 2;
        return insertions;
    }

    /// <summary>
    /// 比較單次解法的實際插入數與固定預期值，輸出檢查結果。
    /// </summary>
    /// <param name="methodName">受測方法名稱。</param>
    /// <param name="expected">測資預期的最少插入次數。</param>
    /// <param name="actual">方法實際回傳的插入次數。</param>
    /// <returns>實際值與預期值相等時回傳 true，否則回傳 false。</returns>
    private static bool CheckResult(string methodName, int expected, int actual)
    {
        bool passed = actual == expected;
        Console.WriteLine($"  {methodName}: Expected={expected}, Actual={actual}, {(passed ? "PASS" : "FAIL")}");
        return passed;
    }

    /// <summary>
    /// 以尚需的右括號數計算使合法括號字串平衡的最少插入次數。
    /// 逐字掃描；新左括號出現前先補完未完成的右括號對，右括號無匹配者時補左括號。
    /// </summary>
    /// <param name="s">只包含左右括號、長度介於 1 與 100,000 的字串。</param>
    /// <returns>使每個左括號對應連續兩個右括號所需的最少插入次數。</returns>
    /// <remarks>不修改輸入；時間複雜度 O(n)，額外空間複雜度 O(1)。</remarks>
    public int MinInsertions2(string s)
    {
        int insertions = 0;
        int requiredRight = 0;

        foreach (char c in s)
        {
            if (c == '(')
            {
                // 奇數需求表示已有一個右括號正在等其搭檔；不能被新的左括號打斷。
                if (requiredRight % 2 == 1)
                {
                    insertions++;
                    requiredRight--;
                }

                requiredRight += 2;
            }
            else
            {
                requiredRight--;
                if (requiredRight < 0)
                {
                    // 沒有左括號可匹配：在此右括號前補 '('，消耗後仍需一個 ')'。
                    insertions++;
                    requiredRight = 1;
                }
            }
        }

        // 剩餘需求都能由在結尾追加右括號補足，不必再插入左括號。
        return insertions + requiredRight;
    }
}