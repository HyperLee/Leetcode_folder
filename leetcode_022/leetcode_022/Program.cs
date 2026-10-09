using System.Text;

namespace leetcode_022;

class Program
{
    /// <summary>
    /// 22. Generate Parentheses
    /// https://leetcode.com/problems/generate-parentheses/description/
    /// 22. 括号生成
    /// https://leetcode.cn/problems/generate-parentheses/description/
    /// <para>
    /// English: Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.
    /// </para>
    /// <para>
    /// 繁體中文：給定 n 對括號，請撰寫一個函式，產生所有格式正確的括號組合。
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數；本程式不使用。</param>
    /// <remarks>
    /// 執行 n = 1 至 8 的雙解法驗證與重複呼叫檢查；輸出 Expected、Actual、PASS/FAIL。
    /// 所有驗證通過時結束碼為 0，否則為 1；不需要互動輸入。
    /// </remarks>
    static void Main(string[] args)
    {
        Program solver = new Program();
        int[] expectedCounts = { 1, 2, 5, 14, 42, 132, 429, 1430 };
        string[][] expectedSets =
        {
            new[] { "()" },
            new[] { "(())", "()()" },
            new[] { "((()))", "(()())", "(())()", "()(())", "()()()" }
        };
        string[] caseNames =
        {
            "OnePair_ReturnsOneCombination", "TwoPairs_ReturnsTwoCombinations",
            "ThreePairs_ReturnsFiveUniqueCombinations", "FourPairs_ReturnsFourteenCombinations",
            "FivePairs_ReturnsFortyTwoCombinations", "SixPairs_Returns132Combinations",
            "SevenPairs_Returns429Combinations", "EightPairs_Returns1430Combinations"
        };
        int passed = 0;
        int total = 0;
        IList<string> firstBruteForce = new List<string>();
        IList<string> firstBacktracking = new List<string>();

        for (int n = 1; n <= 8; n++)
        {
            IList<string> bruteForce = solver.GenerateParenthesis(n);
            IList<string> backtracking = solver.GenerateParenthesis2(n);
            string[]? expected = n <= 3 ? expectedSets[n - 1] : null;
            IList<string>[] results = { bruteForce, backtracking };
            string[] methodNames = { "GenerateParenthesis", "GenerateParenthesis2" };

            for (int method = 0; method < results.Length; method++)
            {
                bool valid = ValidateCombinations(results[method], n, expectedCounts[n - 1], expected);
                Console.WriteLine($"Case: {caseNames[n - 1]} / {methodNames[method]}");
                Console.WriteLine($"Expected: count={expectedCounts[n - 1]}, valid=True" +
                    (expected is null ? "" : $", combinations={FormatCombinations(expected)}"));
                Console.WriteLine($"Actual: count={results[method].Count}, valid={valid}" +
                    (expected is null ? "" : $", combinations={FormatCombinations(results[method])}"));
                Console.WriteLine($"Result: {(valid ? "PASS" : "FAIL")}");
                total++;
                if (valid)
                {
                    passed++;
                }
            }

            bool same = new HashSet<string>(bruteForce, StringComparer.Ordinal).SetEquals(backtracking);
            Console.WriteLine($"Case: n={n} / BothMethods_ReturnSameSet");
            Console.WriteLine("Expected: sameSet=True");
            Console.WriteLine($"Actual: sameSet={same}");
            Console.WriteLine($"Result: {(same ? "PASS" : "FAIL")}");
            total++;
            if (same)
            {
                passed++;
            }

            if (n == 3)
            {
                // 保留原結果，稍後確認後續呼叫沒有改動它。
                firstBruteForce = bruteForce;
                firstBacktracking = backtracking;
            }
        }

        IList<string>[] previousResults = { firstBruteForce, firstBacktracking };
        IList<string>[] repeatedResults = { solver.GenerateParenthesis(3), solver.GenerateParenthesis2(3) };
        string[] repeatNames = { "GenerateParenthesis", "GenerateParenthesis2" };
        for (int method = 0; method < repeatedResults.Length; method++)
        {
            bool valid = ValidateCombinations(previousResults[method], 3, 5, expectedSets[2]) &&
                ValidateCombinations(repeatedResults[method], 3, 5, expectedSets[2]) &&
                !ReferenceEquals(previousResults[method], repeatedResults[method]);
            Console.WriteLine($"Case: RepeatedCalls_ReturnFreshUnchangedResults / {repeatNames[method]}");
            Console.WriteLine("Expected: freshAndUnchanged=True");
            Console.WriteLine($"Actual: freshAndUnchanged={valid}");
            Console.WriteLine($"Result: {(valid ? "PASS" : "FAIL")}");
            total++;
            if (valid)
            {
                passed++;
            }
        }

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        Environment.ExitCode = passed == total ? 0 : 1;
    }

    /// <summary>
    /// 解法一：暴力法
    /// 
    /// 我们可以生成所有 2＾2n个 ‘(’ 和 ‘)’ 字符构成的序列，然后我们检查每一个是否有效即可。
    /// 为了生成所有序列，我们可以使用递归。长度为 n 的序列就是在长度为 n−1 的序列前加一个 ‘(’ 或 ‘)’。
    /// 为了检查序列是否有效，我们遍历这个序列，并使用一个变量 balance 表示左括号的数量减去右括号的数量。如果在遍历过程中
    /// balance 的值小于零，或者结束时 balance 的值不为零，那么该序列就是无效的，否则它是有效的。
    /// <para>輸入 n 對括號，列舉所有長度為 2n 的候選，僅保留有效字串。</para>
    /// </summary>
    /// <param name="n">括號對數，題目條件為 1 至 8。</param>
    /// <returns>所有有效括號字串；每次呼叫回傳新的集合。</returns>
    public IList<string> GenerateParenthesis(int n)
    {
        List<string> combinations = new List<string>();
        generateAll(new char[2 * n], 0, combinations);
        return combinations;
    }

    /// <summary>
    /// 依序填入左、右括號，窮舉固定長度陣列；只有完整候選才驗證並加入結果。
    /// 呼叫時 current 的前 pos 個字元已確定，遞迴返回後該前綴維持不變。
    /// </summary>
    /// <param name="current">共用候選陣列，長度為 2n，只會寫入括號。</param>
    /// <param name="pos">下一個寫入位置，介於 0 與陣列長度之間。</param>
    /// <param name="result">接收有效字串快照的集合。</param>
    public void generateAll(char[] current, int pos, List<string> result)
    {
        if (pos == current.Length)
        {
            if (isValid(current))
            {
                result.Add(new string(current));
            }
        }
        else
        {
            // 只把 [0, pos) 視為有效前綴；下一層會覆寫尚未確定的後綴。
            current[pos] = '(';
            generateAll(current, pos + 1, result);
            // 以覆寫同一格切換分支，不必清空陣列；new string 會保存獨立快照。
            current[pos] = ')';
            generateAll(current, pos + 1, result);
        }
    }

    /// <summary>
    /// 掃描完整括號候選，以 balance 計算尚未配對的左括號數。
    /// 每個前綴不可出現負值，且掃描結束必須歸零。
    /// </summary>
    /// <param name="current">僅包含左、右括號的完整候選陣列。</param>
    /// <returns>所有前綴合法且左右括號完全配對時為 true，否則為 false。</returns>
    public bool isValid(char[] current)
    {
        int balance = 0;
        foreach (char c in current)
        {
            if (c == '(')
            {
                balance++;
            }
            else
            {
                balance--;
            }

            // 此前綴的右括號已過多，後面的左括號無法回頭修復。
            if (balance < 0)
            {
                return false;
            }
        }
        return balance == 0;
    }

    /// <summary>
    /// 解法二: 回朔法
    /// 方法一还有改进的余地：我们可以只在序列仍然保持有效时才添加 ‘(’ 或 ‘)’，而不是像 方法一 那样每次添加。我们可以通过跟踪到
    /// 目前为止放置的左括号和右括号的数目来做到这一点，
    /// 如果左括号数量不大于 n，我们可以放一个左括号。如果右括号数量小于左括号的数量，我们可以放一个右括号。
    /// <para>以左右括號計數維持有效前綴，只探索可能完成的路徑；輸出全部有效組合。</para>
    /// </summary>
    /// <param name="n">括號對數，題目條件為 1 至 8。</param>
    /// <returns>所有有效括號字串；每次呼叫回傳新的集合。</returns>
    public IList<string> GenerateParenthesis2(int n)
    {
        List<string> ans = new List<string>();
        backtrack(ans, new StringBuilder(), 0, 0, n);
        return ans;
    }

    /// <summary>
    /// 在有效前綴上回溯：左括號未用完才能加入左括號，尚有未配對左括號才能加入右括號。
    /// 維持 0 ≤ close ≤ open ≤ max；到達長度 2max 時保存字串，返回時還原 cur。
    /// </summary>
    /// <param name="ans">接收完整有效字串的集合。</param>
    /// <param name="cur">共用有效前綴，長度等於 open + close。</param>
    /// <param name="open">已加入的左括號數。</param>
    /// <param name="close">已加入的右括號數，不超過 open。</param>
    /// <param name="max">目標括號對數，題目條件為 1 至 8。</param>
    public void backtrack(List<string> ans, StringBuilder cur, int open, int close, int max)
    {
        if (cur.Length == max * 2)
        {
            // 不變量與長度共同保證 open = close = max；保存快照避免受後續還原影響。
            ans.Add(cur.ToString());
            return;
        }

        if (open < max)
        {
            // 加入 → 遞迴 → 移除，確保其他分支從相同前綴出發。
            cur.Append('(');
            backtrack(ans, cur, open + 1, close, max);
            cur.Remove(cur.Length - 1, 1);
        }
        // 使用獨立 if：左分支返回並還原後，右分支仍可能合法。
        if (close < open)
        {
            cur.Append(')');
            backtrack(ans, cur, open, close + 1, max);
            cur.Remove(cur.Length - 1, 1);
        }
    }


    /// <summary>
    /// 獨立驗證解法輸出：檢查預期數量、唯一性、字元、長度與所有前綴的配對條件。
    /// 小案例另比對完整預期集合；不使用受測方法 isValid，也不要求結果順序。
    /// </summary>
    /// <param name="combinations">解法實際產生的字串集合。</param>
    /// <param name="n">括號對數，介於 1 至 8。</param>
    /// <param name="expectedCount">已知的有效組合數。</param>
    /// <param name="expected">小案例的完整預期集合；null 表示只驗證數量及結構。</param>
    /// <returns>數量、唯一性、結構與指定集合全部符合時為 true。</returns>
    private static bool ValidateCombinations(IList<string> combinations, int n, int expectedCount, string[]? expected)
    {
        HashSet<string> unique = new HashSet<string>(combinations, StringComparer.Ordinal);
        if (combinations.Count != expectedCount || unique.Count != combinations.Count ||
            (expected is not null && !unique.SetEquals(expected)))
        {
            return false;
        }

        foreach (string combination in combinations)
        {
            if (combination.Length != 2 * n)
            {
                return false;
            }

            int unmatched = 0;
            foreach (char symbol in combination)
            {
                if (symbol == '(')
                {
                    unmatched++;
                }
                else if (symbol == ')')
                {
                    unmatched--;
                }
                else
                {
                    return false;
                }

                if (unmatched < 0)
                {
                    return false;
                }
            }

            if (unmatched != 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 將小案例集合以序數排序後格式化，讓 Expected 與 Actual 的輸出穩定且容易比較。
    /// 排序只用於顯示，不改動解法回傳的集合。
    /// </summary>
    /// <param name="combinations">要顯示的括號字串。</param>
    /// <returns>以雙引號和中括號呈現的字串集合。</returns>
    private static string FormatCombinations(IEnumerable<string> combinations)
    {
        return "[" + string.Join(", ", combinations.OrderBy(value => value, StringComparer.Ordinal)
            .Select(value => $"\"{value}\"")) + "]";
    }
}