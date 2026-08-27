namespace leetcode_020
{
    internal class Program
    {
        /// <summary>
        /// 20. Valid Parentheses
        /// https://leetcode.com/problems/valid-parentheses/description/
        /// <para>
        /// Given a string s containing only the characters '(', ')', '{', '}', '[', and ']', determine whether the input string is valid.
        ///
        /// An input string is valid if:
        /// - Open brackets are closed by the same type of brackets.
        /// - Open brackets are closed in the correct order.
        /// - Every closing bracket has a corresponding opening bracket of the same type.
        ///
        /// Example 1:
        /// Input: s = "()"
        /// Output: true
        ///
        /// Example 2:
        /// Input: s = "()[]{}"
        /// Output: true
        ///
        /// Example 3:
        /// Input: s = "(]"
        /// Output: false
        ///
        /// Example 4:
        /// Input: s = "([])"
        /// Output: true
        ///
        /// Example 5:
        /// Input: s = "([)]"
        /// Output: false
        ///
        /// Constraints:
        /// - 1 &lt;= s.length &lt;= 10^4
        /// - s consists only of parentheses '()[]{}'.
        /// </para>
        /// <para>
        /// 20. 有效括號
        /// https://leetcode.cn/problems/valid-parentheses/description/
        ///
        /// 給定一個只包含字元 '(', ')', '{', '}', '[', 和 ']' 的字串 s，請判斷輸入字串是否有效。
        ///
        /// 有效字串必須符合：
        /// - 左括號必須由相同類型的右括號閉合。
        /// - 左括號必須以正確順序閉合。
        /// - 每個右括號都有一個對應且類型相同的左括號。
        ///
        /// 範例 1：
        /// 輸入：s = "()"
        /// 輸出：true
        ///
        /// 範例 2：
        /// 輸入：s = "()[]{}"
        /// 輸出：true
        ///
        /// 範例 3：
        /// 輸入：s = "(]"
        /// 輸出：false
        ///
        /// 範例 4：
        /// 輸入：s = "([])"
        /// 輸出：true
        ///
        /// 範例 5：
        /// 輸入：s = "([)]"
        /// 輸出：false
        ///
        /// 限制條件：
        /// - 1 &lt;= s.length &lt;= 10^4
        /// - s 只由括號字元 '()[]{}' 組成。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Name, string Input, bool Expected)[] testCases =
            {
                ("空字串", "", true),
                ("最小有效配對", "()", true),
                ("多種類連續配對", "()[]{}", true),
                ("多層巢狀配對", "{[()]}", true),
                ("左右括號種類不同", "(]", false),
                ("關閉順序錯誤", "([)]", false),
                ("仍有未配對左括號", "((", false),
                ("右括號先出現", "][", false)
            };

            int passedChecks = 0;
            foreach ((string name, string input, bool expected) in testCases)
            {
                (
                    bool firstActual,
                    bool firstPassed,
                    bool secondActual,
                    bool secondPassed
                ) = RunCase(input, expected);

                Console.WriteLine($"測試案例：{name}");
                Console.WriteLine($"輸入：\"{input}\"");
                Console.WriteLine($"預期：{expected}");
                Console.WriteLine($"解法一實際：{firstActual}，結果：{(firstPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"解法二實際：{secondActual}，結果：{(secondPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();

                passedChecks += firstPassed ? 1 : 0;
                passedChecks += secondPassed ? 1 : 0;
            }

            Console.WriteLine($"總結：{passedChecks}/{testCases.Length * 2} 項檢查通過");
        }

        /// <summary>
        /// 使用兩種 Stack 解法執行同一筆有效括號案例，並分別比較實際結果與預期值。
        /// 輸入必須只包含三種左右括號或為空字串；回傳兩種解法的結果與是否通過檢查。
        /// </summary>
        /// <param name="input">要驗證的括號字串。</param>
        /// <param name="expected">此案例預期是否為有效括號字串。</param>
        /// <returns>兩種解法的實際結果，以及各自是否符合預期。</returns>
        private static (
            bool FirstActual,
            bool FirstPassed,
            bool SecondActual,
            bool SecondPassed
        ) RunCase(string input, bool expected)
        {
            bool firstActual = IsValid(input);
            bool secondActual = IsValid2(input);

            return (
                firstActual,
                firstActual == expected,
                secondActual,
                secondActual == expected
            );
        }

        /// <summary>
        /// 判斷只包含三種括號的字串，是否每個括號都依正確種類與順序完成配對。
        /// 解題時先排除無法完全配對的奇數長度；接著使用 <c>Stack</c> 保存每個左括號所期待的右括號，遇到右括號時核對堆疊頂端。
        /// 掃描完成後堆疊為空，代表所有括號都已正確閉合。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValid(string s)
        {
            Stack<char> expectedClosings = new Stack<char>();

            // 每一組合法括號都包含兩個字元，奇數長度不可能完全配對。
            if (s.Length % 2 != 0)
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    expectedClosings.Push(')');
                }
                else if (s[i] == '[')
                {
                    expectedClosings.Push(']');
                }
                else if (s[i] == '{')
                {
                    expectedClosings.Push('}');
                }
                else if (expectedClosings.Count == 0)
                {
                    // 尚未遇到左括號便出現右括號，沒有可完成的配對。
                    return false;
                }
                else if (s[i] == expectedClosings.Peek())
                {
                    // Stack 頂端是最近一個左括號所期待的右括號，符合後才能完成配對。
                    expectedClosings.Pop();
                }
                else
                {
                    return false;
                }
            }

            // Stack 為空代表每一個左括號都已依正確種類與順序閉合。
            return expectedClosings.Count == 0;
        }

        /// <summary>
        /// 以 Stack 保存尚未配對的左括號，遇到右括號時核對最近的左括號種類。
        /// 輸入必須只包含三種左右括號或為空字串；全部括號種類與順序都正確時回傳 <see langword="true"/>。
        /// </summary>
        /// <param name="s">要驗證的括號字串。</param>
        /// <returns>字串中的所有括號是否皆以正確種類與順序完成配對。</returns>
        public static bool IsValid2(string s)
        {
            Stack<char> openings = new Stack<char>();

            // 每一組合法括號都包含兩個字元，奇數長度不可能完全配對。
            if (s.Length % 2 != 0)
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == '[' || s[i] == '{')
                {
                    openings.Push(s[i]);
                    continue;
                }

                // 右括號必須和最近尚未配對的左括號形成同種類的一組。
                if (openings.Count == 0 || !IsMatchingPair(openings.Pop(), s[i]))
                {
                    return false;
                }
            }

            // Stack 為空代表沒有遺留任何尚未配對的左括號。
            return openings.Count == 0;
        }

        /// <summary>
        /// 判斷指定的左括號與右括號是否屬於同一組，供保存左括號的解法集中核對種類。
        /// 輸入應為題目允許的括號字元；只有 <c>()</c>、<c>[]</c> 或 <c>{}</c> 會回傳 <see langword="true"/>。
        /// </summary>
        /// <param name="opening">較早出現且等待配對的左括號。</param>
        /// <param name="closing">目前要核對的右括號。</param>
        /// <returns>兩個括號是否為相同種類的一組。</returns>
        private static bool IsMatchingPair(char opening, char closing)
        {
            return (opening == '(' && closing == ')')
                || (opening == '[' && closing == ']')
                || (opening == '{' && closing == '}');
        }
    }
}