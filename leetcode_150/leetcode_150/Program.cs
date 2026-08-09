namespace leetcode_150
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 150. Evaluate Reverse Polish Notation
        /// https://leetcode.com/problems/evaluate-reverse-polish-notation/description/
        ///
        /// You are given an array of strings tokens that represents an arithmetic expression in Reverse Polish Notation.
        /// Evaluate the expression and return an integer representing its value.
        ///
        /// Note that:
        /// - The valid operators are '+', '-', '*', and '/'.
        /// - Each operand may be an integer or another expression.
        /// - Division between two integers always truncates toward zero.
        /// - There will not be any division by zero.
        /// - The input represents a valid arithmetic expression in Reverse Polish Notation.
        /// - The answer and all intermediate calculations can be represented in a 32-bit integer.
        ///
        /// Example 1:
        /// Input: tokens = ["2","1","+","3","*"]
        /// Output: 9
        /// Explanation: ((2 + 1) * 3) = 9
        ///
        /// Example 2:
        /// Input: tokens = ["4","13","5","/","+"]
        /// Output: 6
        /// Explanation: (4 + (13 / 5)) = 6
        ///
        /// Example 3:
        /// Input: tokens = ["10","6","9","3","+","-11","*","/","*","17","+","5","+"]
        /// Output: 22
        /// Explanation:
        /// ((10 * (6 / ((9 + 3) * -11))) + 17) + 5
        /// = ((10 * (6 / (12 * -11))) + 17) + 5
        /// = ((10 * (6 / -132)) + 17) + 5
        /// = ((10 * 0) + 17) + 5
        /// = (0 + 17) + 5
        /// = 17 + 5
        /// = 22
        ///
        /// Constraints:
        /// - 1 &lt;= tokens.length &lt;= 10^4
        /// - tokens[i] is an operator "+", "-", "*", or "/", or an integer in the range [-200, 200].
        /// </para>
        /// <para>
        /// 150. 逆波蘭表示式求值
        /// https://leetcode.cn/problems/evaluate-reverse-polish-notation/description/
        ///
        /// 給定字串陣列 tokens，表示一個以逆波蘭表示法寫成的算術表示式。
        /// 計算此表示式，並回傳代表其值的整數。
        ///
        /// 請注意：
        /// - 有效運算子為 '+'、'-'、'*' 與 '/'。
        /// - 每個運算元可以是整數或另一個表示式。
        /// - 兩個整數相除一律向零截斷。
        /// - 不會出現除以零。
        /// - 輸入代表一個有效的逆波蘭表示法算術表示式。
        /// - 答案與所有中間計算都可用 32 位元整數表示。
        ///
        /// 範例 1：
        /// 輸入：tokens = ["2","1","+","3","*"]
        /// 輸出：9
        /// 解釋：((2 + 1) * 3) = 9
        ///
        /// 範例 2：
        /// 輸入：tokens = ["4","13","5","/","+"]
        /// 輸出：6
        /// 解釋：(4 + (13 / 5)) = 6
        ///
        /// 範例 3：
        /// 輸入：tokens = ["10","6","9","3","+","-11","*","/","*","17","+","5","+"]
        /// 輸出：22
        /// 解釋：
        /// ((10 * (6 / ((9 + 3) * -11))) + 17) + 5
        /// = ((10 * (6 / (12 * -11))) + 17) + 5
        /// = ((10 * (6 / -132)) + 17) + 5
        /// = ((10 * 0) + 17) + 5
        /// = (0 + 17) + 5
        /// = 17 + 5
        /// = 22
        ///
        /// 限制條件：
        /// - 1 &lt;= tokens.length &lt;= 10^4
        /// - tokens[i] 是運算子 "+"、"-"、"*"、"/" 之一，或介於 [-200, 200] 的整數。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行固定的逆波蘭表示法範例，逐筆比較預期值與
        /// <see cref="EvalRPN(string[])"/> 的實際結果，最後輸出通過筆數。
        /// 輸入資料皆為題目保證有效的非空 token 陣列；本方法不接收參數，
        /// 並將每筆案例的 Tokens、Expected、Actual 與 PASS/FAIL 寫入主控台。
        /// </summary>
        private static void RunSamples()
        {
            (string[] Tokens, int Expected)[] samples =
            {
                (new[] { "2", "1", "+", "3", "*" }, 9),
                (new[] { "4", "13", "5", "/", "+" }, 6),
                (new[] { "10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+" }, 22),
                (new[] { "5" }, 5),
                (new[] { "3", "-4", "+" }, -1),
                (new[] { "7", "-3", "/" }, -2)
            };

            int passedCount = 0;

            for (int i = 0; i < samples.Length; i++)
            {
                (string[] tokens, int expected) = samples[i];
                int actual = EvalRPN(tokens);
                bool passed = actual == expected;

                if (passed)
                {
                    passedCount++;
                }

                Console.WriteLine($"案例 {i + 1}");
                Console.WriteLine($"Tokens: {FormatTokens(tokens)}");
                Console.WriteLine($"Expected: {expected}");
                Console.WriteLine($"Actual: {actual}");
                Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCount}/{samples.Length} 筆測試通過");
        }

        /// <summary>
        /// 將有效的逆波蘭表示法 token 陣列轉為易讀字串，供範例輸出使用。
        /// 輸入必須是非 null 的字串陣列；輸出會以雙引號包住每個 token，
        /// 並以方括號及逗號呈現，例如 <c>["2", "1", "+"]</c>。
        /// </summary>
        /// <param name="tokens">要格式化的非 null token 陣列。</param>
        /// <returns>適合顯示於主控台與 README 的 token 清單。</returns>
        private static string FormatTokens(string[] tokens)
        {
            return $"[{string.Join(", ", tokens.Select(token => $"\"{token}\""))}]";
        }

        /// <summary>
        /// 計算有效逆波蘭表示法 token 陣列的整數結果。
        /// 依序將運算元推入後進先出的 Stack；遇到運算子時取出右、左運算元，
        /// 完成四則運算後再將結果推回。輸入必須是題目保證有效的非空運算式，
        /// 且所有中間結果皆可用 32 位元整數表示；輸出為整個運算式的值。
        /// </summary>
        /// <param name="tokens">由整數字串及 <c>+、-、*、/</c> 組成的有效 token 陣列。</param>
        /// <returns>逆波蘭表示法運算式的整數計算結果。</returns>
        public static int EvalRPN(string[] tokens)
        {
            Stack<int> stack = new Stack<int>();
            int length = tokens.Length;

            for (int i = 0; i < length; i++)
            {
                string token = tokens[i];

                if (IsNumber(token) == true)
                {
                    // 運算元先保留在 Stack，等待後續運算子使用。
                    stack.Push(int.Parse(token));
                }
                else
                {
                    // Stack 後進先出，因此先取得右運算元，再取得左運算元。
                    int num2 = stack.Pop();
                    int num1 = stack.Pop();

                    // 減法與除法不可交換運算元；C# 整數除法會向零截斷。
                    switch (token)
                    {
                        case "+":
                            stack.Push(num1 + num2);
                            break;
                        case "-":
                            stack.Push(num1 - num2);
                            break;
                        case "*":
                            stack.Push(num1 * num2);
                            break;
                        case "/":
                            stack.Push(num1 / num2);
                            break;
                        default:
                            break;
                    }
                }
            }

            return stack.Pop();
        }

        /// <summary>
        /// 判斷 token 是否代表整數。題目輸入只會包含有效整數或四種運算子，
        /// 因此檢查非空 token 的最後一個字元是否為數字，即可同時辨識正整數、
        /// 零與負整數；輸出為是否可當作運算元處理的布林值。
        /// </summary>
        /// <param name="token">題目保證非空的整數或運算子字串。</param>
        /// <returns>若 token 代表整數則為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        public static bool IsNumber(string token)
        {
            return char.IsDigit(token[token.Length - 1]);
        }
    }
}
