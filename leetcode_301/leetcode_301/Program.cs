namespace leetcode_301;

class Program
{
    /// <summary>
    /// 301. Remove Invalid Parentheses
    /// https://leetcode.com/problems/remove-invalid-parentheses/description/
    /// 301. 删除无效的括号
    /// https://leetcode.cn/problems/remove-invalid-parentheses/description/
    ///
    /// English version:
    /// Given a string `s` that contains parentheses and letters, remove the minimum number of invalid parentheses to make the input string valid.
    ///
    /// Return a list of unique strings that are valid with the minimum number of removals. You may return the answer in any order.
    ///
    /// 繁體中文版本：
    /// 給定一個包含括號和字母的字串 `s`，移除最少數量的無效括號，使輸入字串成為有效字串。
    ///
    /// 請回傳一個由所有有效字串組成的列表，這些字串都透過最少次數的移除操作得到。列表中的字串不可重複，回傳順序不限。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var testCases = new (string Input, string[] Expected)[]
        {
            ("()())()", new[] { "(())()", "()()()" }),
            ("(a)())()", new[] { "(a())()", "(a)()()" }),
            (")(", new[] { "" }),
            ("(a(b)c)", new[] { "(a(b)c)" }),
            ("(((", new[] { "" }),
            ("())", new[] { "()" }),
            ("abc", new[] { "abc" }),
            ("", new[] { "" })
        };

        var solution = new Program();
        int passedCount = 0;

        foreach (var (input, expected) in testCases)
        {
            string[] actual = solution.RemoveInvalidParentheses(input)
                .OrderBy(value => value, StringComparer.Ordinal)
                .ToArray();
            string[] expectedSorted = expected
                .OrderBy(value => value, StringComparer.Ordinal)
                .ToArray();
            bool passed = actual.SequenceEqual(expectedSorted);

            string inputDisplay = input.Length == 0 ? "\"\"" : $"\"{input}\"";
            string expectedDisplay = "[" + string.Join(", ", expectedSorted.Select(value => "\"" + value + "\"")) + "]";
            string actualDisplay = "[" + string.Join(", ", actual.Select(value => "\"" + value + "\"")) + "]";

            Console.WriteLine($"{(passed ? "PASS" : "FAIL")}: input={inputDisplay}, expected={expectedDisplay}, actual={actualDisplay}");
            if (passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine($"{passedCount}/{testCases.Length} checks passed.");
        Environment.ExitCode = passedCount == testCases.Length ? 0 : 1;
    }

    private IList<string> res = new List<string>();

    /// <summary>
    /// 以回溯和剪枝移除無效括號，回傳所有經最少刪除次數即可成為有效字串的唯一結果。
    /// </summary>
    /// <param name="s">只包含小寫英文字母、左括號與右括號的字串；題目限制長度為 1 至 25，且括號至多 20 個。空字串也可供本專案的額外案例驗證。</param>
    /// <returns>所有刪除最少數量括號後得到的有效字串；回傳順序不限。</returns>
    public IList<string> RemoveInvalidParentheses(string s)
    {
        res = new List<string>();

        int lremove = 0, rremove = 0;

        // 先計算必須移除的未配對括號數；字母不會影響括號配對。
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                lremove++;
            }
            else if (s[i] == ')')
            {
                if (lremove == 0)
                {
                    rremove++;
                }
                else
                {
                    lremove--;
                }
            }
        }

        // 固定最少移除數後，只搜尋符合這兩個數量的候選字串。
        Helper(s, 0, lremove, rremove);
        return res;
    }

    /// <summary>
    /// 以深度優先回溯移除指定數量的左右括號，跳過等價分支並剪去不可能完成的搜尋；有效候選會加入結果清單。
    /// </summary>
    /// <param name="str">目前待檢查、只包含小寫英文字母與括號的候選字串。</param>
    /// <param name="start">本層開始搜尋刪除位置的索引。</param>
    /// <param name="lremove">仍需移除的左括號數量。</param>
    /// <param name="rremove">仍需移除的右括號數量。</param>
    private void Helper(string str, int start, int lremove, int rremove)
    {
        if (lremove == 0 && rremove == 0)
        {
            if (IsValid(str))
            {
                res.Add(str);
            }
            return;
        }

        for (int i = start; i < str.Length; i++)
        {
            // 連續相同括號的刪除結果相同，只探索其中一個位置以避免重複分支。
            if (i != start && str[i] == str[i - 1])
            {
                continue;
            }

            // 剩餘字元數不足以完成所有刪除時，提早結束本層搜尋。
            if (lremove + rremove > str.Length - i)
            {
                return;
            }

            if (lremove > 0 && str[i] == '(')
            {
                // 把字串 str 中索引 i 的那一個字元刪掉，產生一個新的字串。
                // 因為字元會左移，所以下一層遞迴仍從索引 i 開始搜尋。
                // 以及 lremove - 1，表示已刪除一個左括號。
                Helper(str.Substring(0, i) + str.Substring(i + 1), i, lremove - 1, rremove);
            }
            if (rremove > 0 && str[i] == ')')
            {
                // 把字串 str 中索引 i 的那一個字元刪掉，產生一個新的字串。
                // 因為字元會左移，所以下一層遞迴仍從索引 i 開始搜尋。
                // 以及 rremove - 1，表示已刪除一個右括號。
                Helper(str.Substring(0, i) + str.Substring(i + 1), i, lremove, rremove - 1);
            }
        }
    }

    /// <summary>
    /// 以括號餘額檢查字串是否有效：任何前綴的餘額不可為負，且整個字串結尾時必須歸零。
    /// </summary>
    /// <param name="str">只包含小寫英文字母、左括號與右括號的候選字串。</param>
    /// <returns>若候選字串中的括號配對完整且順序正確則為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private bool IsValid(string str)
    {
        int cnt = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '(')
            {
                cnt++;
            }
            else if (str[i] == ')')
            {
                cnt--;

                // 前綴已出現多餘右括號，後續字元無法修復這個順序錯誤。
                if (cnt < 0)
                {
                    return false;
                }
            }
        }
        return cnt == 0;
    }
}
