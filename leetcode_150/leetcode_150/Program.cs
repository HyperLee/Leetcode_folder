namespace leetcode_150
{
    internal class Program
    {
        /// <summary>
        /// 150. Evaluate Reverse Polish Notation
        /// https://leetcode.com/problems/evaluate-reverse-polish-notation/description/?envType=daily-question&envId=2024-01-30
        /// 150. 逆波兰表达式求值
        /// https://leetcode.cn/problems/evaluate-reverse-polish-notation/description/
        /// 
        /// 本題目重點 要先看懂 表示法
        /// 才知道如何計算
        /// 
        /// 把題目給的字串按順序輸入
        /// 數字直接 push, 符號也是 push 然後把 最上方兩個數字抓出來
        /// 做運算
        /// 大致上是這樣
        /// 詳細看wiki說明
        /// 
        /// 主要是 stack 用法
        /// 
        /// 逆波蘭表示法
        /// https://zh.wikipedia.org/zh-tw/%E9%80%86%E6%B3%A2%E5%85%B0%E8%A1%A8%E7%A4%BA%E6%B3%95
        /// https://en.wikipedia.org/wiki/Reverse_Polish_notation
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
