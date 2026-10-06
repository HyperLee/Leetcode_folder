namespace leetcode_921;

class Program
{
    /// <summary>
    /// 921. Minimum Add to Make Parentheses Valid
    /// https://leetcode.com/problems/minimum-add-to-make-parentheses-valid/description/
    /// 921. 使括号有效的最少添加
    /// https://leetcode.cn/problems/minimum-add-to-make-parentheses-valid/description/
    ///
    /// English:
    /// A parentheses string is valid if and only if:
    /// - It is the empty string,
    /// - It can be written as AB (A concatenated with B), where A and B are valid strings, or
    /// - It can be written as (A), where A is a valid string.
    ///
    /// You are given a parentheses string s. In one move, you can insert a parenthesis at any
    /// position of the string.
    ///
    /// - For example, if s = "()))", you can insert an opening parenthesis to be "(()))" or a
    ///   closing parenthesis to be "()))))".
    ///
    /// Return the minimum number of moves required to make s valid.
    ///
    /// 繁體中文：
    /// 括號字串當且僅當符合以下任一條件時才是有效的：
    /// - 它是空字串；
    /// - 它可以寫成 AB（A 與 B 串接），其中 A 與 B 都是有效字串；或
    /// - 它可以寫成 (A)，其中 A 是有效字串。
    ///
    /// 給定一個括號字串 s。一次操作中，你可以在字串的任意位置插入一個括號。
    ///
    /// - 例如，若 s = "()))"，你可以插入一個左括號，使其成為 "(()))"；或插入一個右括號，
    ///   使其成為 "()))))"。
    ///
    /// 請回傳使 s 變成有效括號字串所需的最少操作次數。
    /// </summary>
    /// <remarks>
    /// Main 使用固定測試案例呼叫解法並輸出每筆案例的預期值、實際值與 PASS/FAIL 結果，
    /// 不讀取標準輸入。
    /// </remarks>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var testCases = new (string Input, int Expected)[]
        {
            ("", 0),
            ("()", 0),
            ("())", 1),
            ("(((", 3),
            ("))", 2),
            ("())(()", 2),
            ("()))", 2)
        };

        var solution = new Program();
        int passed = 0;

        foreach (var testCase in testCases)
        {
            int actual = solution.MinAddToMakeValid(testCase.Input);
            bool isPassed = actual == testCase.Expected;
            if (isPassed)
            {
                passed++;
            }

            string displayInput = testCase.Input.Length == 0
                ? "<empty>"
                : $"\"{testCase.Input}\"";
            string result = isPassed ? "PASS" : "FAIL";
            Console.WriteLine(
                $"{displayInput} -> expected={testCase.Expected}, actual={actual}, result={result}");
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用貪心法從左至右掃描括號字串，計算使輸入變成有效括號字串所需的最少插入次數。
    /// 掃描過程維護尚未配對的左括號數量；遇到無法配對的右括號時立即計入一個必要的左括號，
    /// 掃描結束後再為剩餘的每個左括號計入一個必要的右括號。
    /// 輸入條件是 s 僅包含 '(' 與 ')'；輸出結果是使 s 有效所需的最少插入數量。
    /// </summary>
    /// <param name="s">只包含 '(' 與 ')' 的括號字串。</param>
    /// <returns>使 s 成為有效括號字串所需的最少插入數量。</returns>
    public int MinAddToMakeValid(string s)
    {
        int res = 0;
        // 未配對的左括號數量。
        int leftCount = 0;
        int length = s.Length;
        for(int i = 0; i < length; i++)
        {
            char c = s[i];
            if(c == '(')
            {
                leftCount++;
            }
            else
            {
                if(leftCount > 0)
                {
                    leftCount--;
                }
                else
                {
                    // 當前右括號沒有未配對的左括號可用，補一個左括號是必要且最少的修正。
                    res++;
                }
            }
        }

        // 掃描結束後，每個未配對的左括號都必須補一個右括號。
        res += leftCount;
        return res;
    }
}
