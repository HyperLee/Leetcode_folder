namespace leetcode_241;

class Program
{
    /// <summary>
    /// 241. Different Ways to Add Parentheses
    /// https://leetcode.com/problems/different-ways-to-add-parentheses/description/
    /// 241. 為運算表達式設計優先級
    /// https://leetcode.cn/problems/different-ways-to-add-parentheses/description/
    /// 
    /// 題目描述：
    /// 給你一個由數字和運算符組成的字串 expression，按不同優先級組合數字和運算符，
    /// 計算並返回所有可能組合的結果。你可以按任意順序返回答案。
    /// 
    /// 生成的測試用例滿足：
    /// - 運算符只有 '+'、'-' 和 '*'
    /// - 運算數只有整數
    /// - 運算數和運算符都能被空格字元分隔
    /// - 算式的結果在 32-bit 整數範圍內
    /// 
    /// 範例 1:
    /// 輸入: expression = "2-1-1"
    /// 輸出: [0, 2]
    /// 解釋：
    /// ((2-1)-1) = 0 
    /// (2-(1-1)) = 2
    /// 
    /// 範例 2:
    /// 輸入: expression = "2*3-4*5"
    /// 輸出: [-34, -14, -10, -10, 10]
    /// 解釋：
    /// (2*(3-(4*5))) = -34 
    /// ((2*3)-(4*5)) = -14 
    /// ((2*(3-4))*5) = -10 
    /// (2*((3-4)*5)) = -10 
    /// (((2*3)-4)*5) = 10
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Program program = new();
        Environment.ExitCode = program.RunSamples() ? 0 : 1;
    }

    /// <summary>
    /// 執行五組固定案例，分別驗證四種解法是否產生包含重複值的完整結果集合。
    /// 每種解法的輸入都是只含非負整數及 <c>+</c>、<c>-</c>、<c>*</c> 的合法表達式，
    /// 輸出會先排序再與預期值比較，最後回傳全部檢查是否通過。
    /// </summary>
    /// <returns>全部二十項檢查通過時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private bool RunSamples()
    {
        SampleCase[] sampleCases =
        [
            new("11", [11]),
            new("2-1-1", [0, 2]),
            new("2*3-4*5", [-34, -14, -10, -10, 10]),
            new("1+2*3", [7, 9]),
            new("11+22*33", [737, 1089])
        ];

        (string Name, Func<string, IList<int>> Solve)[] solutions =
        [
            ("分治法", DiffWaysToCompute),
            ("字元區間 DFS", DiffWaysToCompute_DFS),
            ("記憶化搜尋", DiffWaysToCompute_memo),
            ("動態規劃", DiffWaysToCompute_DP)
        ];

        int passedChecks = 0;
        int totalChecks = sampleCases.Length * solutions.Length;

        Console.WriteLine("LeetCode 241 - Different Ways to Add Parentheses");
        Console.WriteLine("================================================");

        for (int caseIndex = 0; caseIndex < sampleCases.Length; caseIndex++)
        {
            SampleCase sample = sampleCases[caseIndex];
            int[] expected = NormalizeResults(sample.Expected);

            Console.WriteLine();
            Console.WriteLine($"案例 {caseIndex + 1}: {sample.Expression}");
            Console.WriteLine($"預期: {FormatResults(expected)}");

            foreach ((string name, Func<string, IList<int>> solve) in solutions)
            {
                int[] actual = NormalizeResults(solve(sample.Expression));
                bool passed = expected.SequenceEqual(actual);
                passedChecks += passed ? 1 : 0;

                Console.WriteLine($"  {name}: {FormatResults(actual)} => {(passed ? "PASS" : "FAIL")}");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項檢查通過");

        return passedChecks == totalChecks;
    }

    /// <summary>
    /// 將任意順序的結果複製並升冪排序，讓允許任意回傳順序的解答可以穩定比較。
    /// 輸入可包含重複整數；輸出會保留所有重複值，不改動原集合。
    /// </summary>
    /// <param name="results">演算法產生的計算結果。</param>
    /// <returns>保留重複值的升冪排序陣列。</returns>
    private static int[] NormalizeResults(IEnumerable<int> results)
    {
        return results.OrderBy(value => value).ToArray();
    }

    /// <summary>
    /// 將已正規化的整數結果格式化為可重現的方括號文字。
    /// 輸入應為欲顯示的結果序列，輸出格式為 <c>[值1, 值2]</c>。
    /// </summary>
    /// <param name="results">欲顯示的整數結果。</param>
    /// <returns>適合主控台與 README 使用的結果文字。</returns>
    private static string FormatResults(IReadOnlyList<int> results)
    {
        return $"[{string.Join(", ", results)}]";
    }

    private const int Add = -1;
    private const int Sub = -2;
    private const int Mul = -3;

    /// <summary>
    /// 表示一組合法算術表達式，以及所有加括號方式應得到的完整預期結果。
    /// </summary>
    /// <param name="Expression">只含非負整數及 <c>+</c>、<c>-</c>、<c>*</c> 的表達式。</param>
    /// <param name="Expected">保留重複值的預期結果。</param>
    private sealed record SampleCase(string Expression, int[] Expected);

    /// <summary>
    /// 使用記憶化搜尋計算所有加括號結果。
    /// 此解法先把合法表達式編碼為數字與負值運算子序列，再快取每個索引區間的結果，
    /// 避免相同子表達式被重複求解。
    /// </summary>
    /// <param name="expression">只含非負整數及 <c>+</c>、<c>-</c>、<c>*</c>，且數字與運算子交錯的非空字串。</param>
    /// <returns>所有合法括號組合的計算結果；不同括號方式得到相同數值時會保留重複值。</returns>
    public IList<int> DiffWaysToCompute_memo(string expression)
    {
        IList<int> ops = new List<int>();

        for (int i = 0; i < expression.Length;)
        {
            if (!char.IsDigit(expression[i]))
            {
                if (expression[i] == '+')
                {
                    ops.Add(Add);
                }
                else if (expression[i] == '-')
                {
                    ops.Add(Sub);
                }
                else if (expression[i] == '*')
                {
                    ops.Add(Mul);
                }

                i++;
            }
            else
            {
                int t = 0;
                while (i < expression.Length && char.IsDigit(expression[i]))
                {
                    t = t * 10 + (expression[i] - '0');
                    i++;
                }

                ops.Add(t);
            }
        }

        IList<int>[][] dp = new IList<int>[ops.Count][];
        for (int i = 0; i < ops.Count; i++)
        {
            dp[i] = new IList<int>[ops.Count];

            for (int j = 0; j < ops.Count; j++)
            {
                dp[i][j] = new List<int>();
            }
        }

        return DFS(dp, 0, ops.Count - 1, ops);
    }

    /// <summary>
    /// 遞迴求出編碼序列指定區間的所有結果，並把已完成的區間保存於二維快取。
    /// 區間端點必須落在數字位置；輸出包含區間內每個合法切分點的左右結果組合。
    /// </summary>
    /// <param name="dp">依左右端點保存子問題結果的二維快取。</param>
    /// <param name="l">區間左端點，必須是數字索引。</param>
    /// <param name="r">區間右端點，必須是數字索引且不小於 <paramref name="l"/>。</param>
    /// <param name="ops">以非負值表示數字、負值常數表示運算子的交錯序列。</param>
    /// <returns>指定區間所有合法括號組合的計算結果。</returns>
    public IList<int> DFS(IList<int>[][] dp, int l, int r, IList<int> ops)
    {
        // 非空快取代表此區間已完成，可直接重用整份結果。
        if (dp[l][r].Count > 0)
        {
            return dp[l][r];
        }

        if (l == r)
        {
            dp[l][r].Add(ops[l]);
            return dp[l][r];
        }

        for (int i = l; i < r; i++)
        {
            if (ops[i] >= 0)
            {
                continue;
            }

            IList<int> left = DFS(dp, l, i - 1, ops);
            IList<int> right = DFS(dp, i + 1, r, ops);

            // 每個左結果都要搭配每個右結果，才能涵蓋此切分點的所有括號方式。
            foreach (int lval in left)
            {
                foreach (int rval in right)
                {
                    int result;
                    if (ops[i] == Add)
                    {
                        result = lval + rval;
                    }
                    else if (ops[i] == Sub)
                    {
                        result = lval - rval;
                    }
                    else
                    {
                        result = lval * rval;
                    }

                    dp[l][r].Add(result);
                }
            }
        }

        if (dp[l][r].Count == 0)
        {
            dp[l][r].Add(ops[l]);
        }

        return dp[l][r];
    }

    /// <summary>
    /// 使用自底向上的區間動態規劃計算所有加括號結果。
    /// 此解法把合法表達式編碼後，先建立單一數字區間，再依區間長度合併較短子問題，
    /// 因此不需要遞迴呼叫。
    /// </summary>
    /// <param name="expression">只含非負整數及 <c>+</c>、<c>-</c>、<c>*</c>，且數字與運算子交錯的非空字串。</param>
    /// <returns>所有合法括號組合的計算結果；不同括號方式得到相同數值時會保留重複值。</returns>
    public IList<int> DiffWaysToCompute_DP(string expression)
    {
        IList<int> ops = new List<int>();

        for (int i = 0; i < expression.Length;)
        {
            if (!char.IsDigit(expression[i]))
            {
                if (expression[i] == '+')
                {
                    ops.Add(Add);
                }
                else if (expression[i] == '-')
                {
                    ops.Add(Sub);
                }
                else if (expression[i] == '*')
                {
                    ops.Add(Mul);
                }

                i++;
            }
            else
            {
                int t = 0;
                while (i < expression.Length && char.IsDigit(expression[i]))
                {
                    t = t * 10 + (expression[i] - '0');
                    i++;
                }

                ops.Add(t);
            }
        }

        IList<int>[][] dp = new IList<int>[ops.Count][];
        for (int i = 0; i < ops.Count; i++)
        {
            dp[i] = new IList<int>[ops.Count];

            for (int j = 0; j < ops.Count; j++)
            {
                dp[i][j] = new List<int>();
            }
        }

        for (int i = 0; i < ops.Count; i++)
        {
            if (ops[i] >= 0)
            {
                dp[i][i].Add(ops[i]);
            }
        }

        // 只有奇數長度區間能以數字為首尾；短區間必須先於長區間完成。
        for (int len = 3; len <= ops.Count; len += 2)
        {
            for (int j = 0; j + len <= ops.Count; j += 2)
            {
                int left = j;
                int right = j + len - 1;

                for (int k = left + 1; k < right; k += 2)
                {
                    IList<int> leftResults = dp[left][k - 1];
                    IList<int> rightResults = dp[k + 1][right];

                    // 笛卡兒積保留每一種左右括號組合，也保留數值相同的不同組合。
                    foreach (int leftVal in leftResults)
                    {
                        foreach (int rightVal in rightResults)
                        {
                            if (ops[k] == Add)
                            {
                                dp[left][right].Add(leftVal + rightVal);
                            }
                            else if (ops[k] == Sub)
                            {
                                dp[left][right].Add(leftVal - rightVal);
                            }
                            else if (ops[k] == Mul)
                            {
                                dp[left][right].Add(leftVal * rightVal);
                            }
                        }
                    }
                }
            }
        }

        return dp[0][ops.Count - 1];
    }

    /// <summary>
    /// 使用字串切割的分治法計算所有加括號結果。
    /// 此解法把合法表達式中的每個運算子輪流視為最後執行的運算，
    /// 遞迴求左右子字串後組合結果，純數字子字串則直接解析。
    /// </summary>
    /// <param name="expression">只含非負整數及 <c>+</c>、<c>-</c>、<c>*</c>，且數字與運算子交錯的非空字串。</param>
    /// <returns>所有合法括號組合的計算結果；不同括號方式得到相同數值時會保留重複值。</returns>
    public IList<int> DiffWaysToCompute(string expression)
    {
        bool isDigitOnly = true;
        foreach (char c in expression)
        {
            if (!char.IsDigit(c))
            {
                isDigitOnly = false;
                break;
            }
        }

        if (isDigitOnly)
        {
            return new List<int> { int.Parse(expression) };
        }

        IList<int> res = new List<int>();
        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];
            if (c == '+' || c == '-' || c == '*')
            {
                // 將此運算子視為最後一步，左右子字串可各自採用任意括號方式。
                IList<int> left = DiffWaysToCompute(expression.Substring(0, i));
                IList<int> right = DiffWaysToCompute(expression.Substring(i + 1));

                foreach (int l in left)
                {
                    foreach (int r in right)
                    {
                        if (c == '+')
                        {
                            res.Add(l + r);
                        }
                        else if (c == '-')
                        {
                            res.Add(l - r);
                        }
                        else
                        {
                            res.Add(l * r);
                        }
                    }
                }
            }
        }

        return res;
    }

    /// <summary>
    /// 使用字元陣列與區間 DFS 計算所有加括號結果。
    /// 此解法不建立子字串，而是讓遞迴輔助函式在合法表達式的閉區間內尋找分割運算子，
    /// 再組合左右區間的所有結果。
    /// </summary>
    /// <param name="expression">只含非負整數及 <c>+</c>、<c>-</c>、<c>*</c>，且數字與運算子交錯的非空字串。</param>
    /// <returns>所有合法括號組合的計算結果；不同括號方式得到相同數值時會保留重複值。</returns>
    public IList<int> DiffWaysToCompute_DFS(string expression)
    {
        char[] cs = expression.ToCharArray();
        return DFS(cs, 0, cs.Length - 1);
    }

    /// <summary>
    /// 遞迴計算字元陣列指定閉區間的所有結果。
    /// 區間必須構成合法的數字或子表達式；找不到運算子時會把整段解析為多位數，
    /// 否則以每個運算子分割並組合左右區間。
    /// </summary>
    /// <param name="cs">合法表達式的字元陣列。</param>
    /// <param name="l">閉區間左端點，必須指向數字。</param>
    /// <param name="r">閉區間右端點，必須指向數字且不小於 <paramref name="l"/>。</param>
    /// <returns>指定區間所有合法括號組合的計算結果。</returns>
    private IList<int> DFS(char[] cs, int l, int r)
    {
        IList<int> ans = new List<int>();

        for (int i = l; i <= r; i++)
        {
            if (cs[i] >= '0' && cs[i] <= '9')
            {
                continue;
            }

            IList<int> left = DFS(cs, l, i - 1);
            IList<int> right = DFS(cs, i + 1, r);

            // 區間索引取代字串切割，但每個運算子仍代表一個可能的最後運算。
            foreach (int a in left)
            {
                foreach (int b in right)
                {
                    int current;
                    if (cs[i] == '+')
                    {
                        current = a + b;
                    }
                    else if (cs[i] == '-')
                    {
                        current = a - b;
                    }
                    else
                    {
                        current = a * b;
                    }

                    ans.Add(current);
                }
            }
        }

        if (ans.Count == 0)
        {
            int current = 0;
            for (int i = l; i <= r; i++)
            {
                current = current * 10 + (cs[i] - '0');
            }

            ans.Add(current);
        }

        return ans;
    }
}
