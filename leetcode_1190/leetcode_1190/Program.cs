using System.Text;

namespace leetcode_1190
{
    internal class Program
    {
        /// <summary>
        /// 1190. Reverse Substrings Between Each Pair of Parentheses
        /// https://leetcode.com/problems/reverse-substrings-between-each-pair-of-parentheses/description/?envType=daily-question&envId=2024-07-11
        /// 1190. 反转每对括号间的子串
        /// https://leetcode.cn/problems/reverse-substrings-between-each-pair-of-parentheses/description/
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>
        /// 執行固定案例，逐一驗證兩種括號反轉解法，並以結束碼表示所有檢查是否通過。
        /// </remarks>
        static void Main(string[] args)
        {
            Environment.ExitCode = RunSamples() ? 0 : 1;
        }

        /// <summary>
        /// 建立並執行七組固定案例，同時驗證堆疊反轉法與括號跳躍法。
        /// 此方法不接受外部輸入；輸出每組案例的輸入、預期結果、實際結果與 PASS/FAIL，
        /// 並回傳全部十四項檢查是否通過。
        /// </summary>
        /// <returns>十四項檢查全部通過時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            string maximumLengthInput = new string('a', 2000);
            (string Name, string Input, string Expected)[] cases =
            [
                ("官方範例一", "(abcd)", "dcba"),
                ("官方範例二", "(u(love)i)", "iloveu"),
                ("官方範例三", "(ed(et(oc))el)", "leetcode"),
                ("官方範例四", "a(bcdefghijkl(mno)p)q", "apmnolkjihgfedcbq"),
                ("長度下界且沒有括號", "a", "a"),
                ("相鄰括號區段", "(ab)(cd)", "badc"),
                ("長度 2000 上界", maximumLengthInput, maximumLengthInput)
            ];

            int passedChecks = 0;
            int totalChecks = 0;

            foreach ((string name, string input, string expected) in cases)
            {
                (int passed, int total) = RunTestCase(name, input, expected);
                passedChecks += passed;
                totalChecks += total;
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過。");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 對同一個合法括號字串執行兩種解法，並以完整字串比較各自的實際結果與預期結果。
        /// 輸入包含案例名稱、符合題目限制的字串及預期輸出；此方法會輸出比較明細，
        /// 並回傳本案例通過的解法數與固定檢查總數二。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="input">只含小寫英文字母及平衡括號的非空字串。</param>
        /// <param name="expected">移除括號並完成所有反轉後的預期字串。</param>
        /// <returns>本案例通過的解法數與檢查總數二。</returns>
        private static (int Passed, int Total) RunTestCase(string name, string input, string expected)
        {
            string actual1 = ReverseParentheses(input);
            string actual2 = ReverseParentheses2(input);
            bool passed1 = actual1 == expected;
            bool passed2 = actual2 == expected;

            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"輸入：s = {FormatValue(input)}");
            Console.WriteLine($"預期：{FormatValue(expected)}");
            Console.WriteLine($"解法一（堆疊保存前綴）實際：{FormatValue(actual1)} => {(passed1 ? "PASS" : "FAIL")}");
            Console.WriteLine($"解法二（括號配對跳躍）實際：{FormatValue(actual2)} => {(passed2 ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return ((passed1 ? 1 : 0) + (passed2 ? 1 : 0), 2);
        }

        /// <summary>
        /// 將字串格式化為適合固定測試輸出的顯示值，過長內容只顯示前後各二十個字元。
        /// 輸入須為非 <see langword="null"/> 字串；輸出一定包含雙引號，長字串另標示完整長度。
        /// </summary>
        /// <param name="value">要顯示的測試輸入、預期值或實際值。</param>
        /// <returns>可穩定寫入主控台及 README 的字串表示。</returns>
        private static string FormatValue(string value)
        {
            const int visibleCharacterCount = 20;

            if (value.Length <= visibleCharacterCount * 2)
            {
                return $"\"{value}\"";
            }

            return $"\"{value[..visibleCharacterCount]}…{value[^visibleCharacterCount..]}\"（長度 {value.Length}）";
        }

        /// <summary>
        /// 使用堆疊保存每層左括號之前的字串前綴，遇到右括號時反轉目前片段，
        /// 再與上一層前綴合併。輸入須為只含小寫英文字母及成對括號的非空字串；
        /// 輸出為完成由內而外反轉且不含括號的字串。
        /// </summary>
        /// <param name="s">符合題目限制且所有括號平衡的字串。</param>
        /// <returns>反轉每對括號內字元並移除所有括號後的結果。</returns>
        public static string ReverseParentheses(string s)
        {
            Stack<string> outerPrefixes = new Stack<string>();
            StringBuilder current = new StringBuilder();

            foreach (char character in s)
            {
                if (character == '(')
                {
                    // 新的一層只處理括號內片段，外層前綴留待配對右括號時接回。
                    outerPrefixes.Push(current.ToString());
                    current.Clear();
                }
                else if (character == ')')
                {
                    // 右括號代表目前最內層完成；先原地反轉，再回到上一層字串。
                    for (int left = 0, right = current.Length - 1; left < right; left++, right--)
                    {
                        (current[left], current[right]) = (current[right], current[left]);
                    }

                    current.Insert(0, outerPrefixes.Pop());
                }
                else
                {
                    current.Append(character);
                }
            }

            return current.ToString();
        }

        /// <summary>
        /// 先以堆疊建立每對括號的索引映射，再沿字串走訪；每次遇到括號便跳至配對位置
        /// 並反轉走訪方向，使巢狀區段自然以相反順序輸出。輸入須為只含小寫英文字母及
        /// 成對括號的非空字串；輸出為完成所有反轉且不含括號的字串。
        /// </summary>
        /// <param name="s">符合題目限制且所有括號平衡的字串。</param>
        /// <returns>反轉每對括號內字元並移除所有括號後的結果。</returns>
        public static string ReverseParentheses2(string s)
        {
            int[] matchingParenthesis = new int[s.Length];
            Stack<int> openingParentheses = new Stack<int>();

            for (int index = 0; index < s.Length; index++)
            {
                if (s[index] == '(')
                {
                    openingParentheses.Push(index);
                }
                else if (s[index] == ')')
                {
                    int openingIndex = openingParentheses.Pop();
                    matchingParenthesis[openingIndex] = index;
                    matchingParenthesis[index] = openingIndex;
                }
            }

            StringBuilder result = new StringBuilder();

            for (int index = 0, direction = 1; index < s.Length; index += direction)
            {
                if (s[index] == '(' || s[index] == ')')
                {
                    // 跳到配對括號後切換方向，等同由內而外反轉括號中的走訪順序。
                    index = matchingParenthesis[index];
                    direction = -direction;
                }
                else
                {
                    result.Append(s[index]);
                }
            }

            return result.ToString();
        }
    }
}