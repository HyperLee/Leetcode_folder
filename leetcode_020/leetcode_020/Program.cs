namespace leetcode_020
{
    internal class Program
    {
        /// <summary>
        /// 20. Valid Parentheses
        /// https://leetcode.com/problems/valid-parentheses/
        /// 20. 有效的括号
        /// https://leetcode.cn/problems/valid-parentheses/
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
        /// https://leetcode.com/problems/valid-parentheses/
        /// Given a string containing just the characters '(', ')', '{', '}', '[' and ']', 
        /// determine if the input string is valid.
        /// An input string is valid if:
        /// 1. Open brackets must be closed by the same type of brackets.
        /// 2. Open brackets must be closed in the correct order.
        /// Note that an empty string is also considered valid.
        /// 
        /// ref: 
        /// 1. Stack.Peek 方法
        ///    https://docs.microsoft.com/zh-tw/dotnet/api/system.collections.stack.peek?view=net-6.0
        ///    
        /// 每當遇到一個 左括號 就會期待 一個右括號 組合成一組
        /// 所以遇到一左括號就 push 一右括號 為一組
        /// 等後續有右括號進來就 pop 出去
        /// 因為括號為偶數
        /// 故最後 stack.count 為0
        /// 就代表true 皆為兩兩一組
        /// 反之false
        /// 
        /// 需要注意 左括號 對 右括號 兩兩一組
        /// 順序大小都需要相同層級才可以
        /// 
        /// 其他方法可以參考
        /// https://ithelp.ithome.com.tw/articles/10217603
        /// https://leetcode.cn/problems/valid-parentheses/solution/you-xiao-de-gua-hao-by-leetcode-solution/
        /// https://leetcode.cn/problems/valid-parentheses/solution/you-xiao-de-gua-hao-by-leetcode-learning-p2qg/
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
