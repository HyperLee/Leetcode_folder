using System.Text;

namespace leetcode_1021;

class Program
{
    /// <summary>
    /// 1021. Remove Outermost Parentheses
    /// https://leetcode.com/problems/remove-outermost-parentheses/description/
    /// 1021. 删除最外层的括号
    /// https://leetcode.cn/problems/remove-outermost-parentheses/description/
    ///
    /// English:
    /// A valid parentheses string is either empty "", "(" + A + ")", or A + B,
    /// where A and B are valid parentheses strings, and + represents string concatenation.
    /// For example, "", "()", "(())()", and "(()(()))" are all valid parentheses strings.
    /// A valid parentheses string s is primitive if it is nonempty and cannot be split into
    /// s = A + B, where A and B are both nonempty valid parentheses strings.
    /// Given a valid parentheses string s, its primitive decomposition is
    /// s = P1 + P2 + ... + Pk, where every Pi is primitive.
    /// Return s after removing the outermost parentheses of every primitive string in its
    /// primitive decomposition.
    ///
    /// 繁體中文：
    /// 有效的括號字串符合以下任一條件：空字串 ""、"(" + A + ")"，或 A + B；
    /// 其中 A 與 B 都是有效的括號字串，而 + 表示字串串接。
    /// 例如，""、"()"、"(())()" 與 "(()(()))" 都是有效的括號字串。
    /// 有效括號字串 s 若非空，且無法拆分為 s = A + B（其中 A 與 B 都是非空的
    /// 有效括號字串），則稱為原始字串（primitive）。
    /// 給定有效括號字串 s，其原始分解為 s = P1 + P2 + ... + Pk，其中每個 Pi 都是
    /// 原始字串。回傳移除原始分解中每個原始字串最外層括號後的 s。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        (string Input, string Expected)[] testCases =
        {
            ("(()())(())", "()()()"),
            ("(()())(())(()(()))", "()()()()(())"),
            ("()()", "")
        };

        Program solution = new Program();
        (string Name, Func<string, string> Solve)[] solutions =
        {
            (nameof(Program.RemoveOuterParentheses), solution.RemoveOuterParentheses),
            (nameof(Program.RemoveOuterParentheses2), solution.RemoveOuterParentheses2),
            (nameof(Program.RemoveOuterParentheses3), solution.RemoveOuterParentheses3)
        };

        int passedCount = 0;
        int failedCount = 0;

        for (int i = 0; i < testCases.Length; i++)
        {
            (string input, string expected) = testCases[i];
            Console.WriteLine($"Case {i + 1}: s = \"{input}\"");
            Console.WriteLine($"Expected: \"{expected}\"");

            foreach ((string name, Func<string, string> solve) in solutions)
            {
                string actual = solve(input);
                bool passed = actual == expected;

                if (passed)
                {
                    passedCount++;
                }
                else
                {
                    failedCount++;
                }

                Console.WriteLine($"{name}: \"{actual}\" [{(passed ? "PASS" : "FAIL")}]");
            }

            Console.WriteLine();
        }

        int totalCount = testCases.Length * solutions.Length;
        Console.WriteLine($"Result: {passedCount}/{totalCount} PASS");

        if (failedCount > 0)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 移除有效括號字串每個原始片段的最外層括號。
    /// 解題概念：以 Stack<char> 的深度表示巢狀層數，只保留原語內部的括號。
    /// 輸入條件：非 null、長度為 1 至 100000，只含 '(' 與 ')'，且為有效括號字串。
    /// 輸出結果：回傳移除每個原始片段最外層括號後的新字串。
    /// </summary>
    /// <param name="s">符合題目條件的有效括號字串。</param>
    /// <returns>移除每個原始片段最外層括號後的字串。</returns>
    public string RemoveOuterParentheses(string s)
    {
        StringBuilder res = new StringBuilder();
        Stack<char> stack = new Stack<char>();

        foreach (char c in s)
        {
            if (c == ')')
            {
                stack.Pop();
            }

            // 右括號先退出一層；堆疊仍有內容時，才是原語內部的右括號。
            if (stack.Count > 0)
            {
                res.Append(c);
            }

            // 左括號在入堆疊前判斷；只有原語內部的左括號會被保留。
            if (c == '(')
            {
                stack.Push(c);
            }
        }

        return res.ToString();
    }

    /// <summary>
    /// 移除有效括號字串每個原始片段的最外層括號。
    /// 解題概念：以整數深度計數巢狀層數，深度大於零時保留目前括號。
    /// 輸入條件：非 null、長度為 1 至 100000，只含 '(' 與 ')'，且為有效括號字串。
    /// 輸出結果：回傳移除每個原始片段最外層括號後的新字串。
    /// </summary>
    /// <param name="s">符合題目條件的有效括號字串。</param>
    /// <returns>移除每個原始片段最外層括號後的字串。</returns>
    public string RemoveOuterParentheses2(string s)
    {
        int level = 0;
        StringBuilder sb = new StringBuilder();
        foreach (char c in s)
        {
            // 右括號先減深度，讓原語最外層右括號在深度歸零時被排除。
            if (c == ')')
            {
                level--;
            }

            if (level > 0)
            {
                sb.Append(c);
            }

            // 左括號在增加深度前判斷，因此深度為零的原語起始括號不會被加入。
            if (c == '(')
            {
                level++;
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 移除有效括號字串每個原始片段的最外層括號。
    /// 解題概念：以深度判斷是否保留括號，並用 size 將保留字元壓縮到工作陣列前綴。
    /// 輸入條件：非 null、長度為 1 至 100000，只含 '(' 與 ')'，且為有效括號字串。
    /// 輸出結果：回傳工作陣列前 size 個字元組成的新字串。
    /// </summary>
    /// <param name="s">符合題目條件的有效括號字串。</param>
    /// <returns>移除每個原始片段最外層括號後的字串。</returns>
    public string RemoveOuterParentheses3(string s)
    {
        // string 不可變，先複製成工作陣列，再將保留的字元壓縮至前綴。
        char[] chars = s.ToCharArray();
        int size = 0;
        int depth = 0;

        foreach (char c in chars)
        {
            if (c == '(')
            {
                // 深度大於零代表這不是原語最外層的起始括號。
                if (depth > 0)
                {
                    chars[size++] = c;
                }
                depth++;
            }
            else
            {
                depth--;

                // 先退出一層後深度仍大於零，代表這不是原語最外層的結尾括號。
                if (depth > 0)
                {
                    chars[size++] = c;
                }
            }
        }
        return new string(chars, 0, size);
    }
}