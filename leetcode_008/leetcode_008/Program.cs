namespace leetcode_008
{
    internal class Program
    {
        private enum ParseState
        {
            Start,
            Signed,
            InNumber,
            End
        }

        private enum CharacterType
        {
            Space,
            Sign,
            Digit,
            Other
        }

        private static readonly ParseState[,] StateTransitions =
        {
            { ParseState.Start, ParseState.Signed, ParseState.InNumber, ParseState.End },
            { ParseState.End, ParseState.End, ParseState.InNumber, ParseState.End },
            { ParseState.End, ParseState.End, ParseState.InNumber, ParseState.End },
            { ParseState.End, ParseState.End, ParseState.End, ParseState.End }
        };

        /// <summary>
        /// 8. String to Integer (atoi)
        /// https://leetcode.com/problems/string-to-integer-atoi/description/
        /// <para>
        /// Implement the myAtoi(string s) function, which converts a string to a 32-bit signed integer.
        ///
        /// The algorithm for myAtoi(string s) is as follows:
        /// - Whitespace: Ignore any leading whitespace (" ").
        /// - Signedness: Determine the sign by checking whether the next character is '-' or '+', assuming positivity if neither is present.
        /// - Conversion: Read the integer by skipping leading zeros until a non-digit character is encountered or the end of the string is reached. If no digits were read, the result is 0.
        /// - Rounding: If the integer is outside the signed 32-bit integer range [-2^31, 2^31 - 1], round it to remain in the range. Integers less than -2^31 are rounded to -2^31, and integers greater than 2^31 - 1 are rounded to 2^31 - 1.
        ///
        /// Return the integer as the final result.
        ///
        /// Example 1:
        /// Input: s = "42"
        /// Output: 42
        /// Explanation: Brackets mark the characters read, and ^ marks the current reader position.
        /// Step 1: "42" (no characters read because there is no leading whitespace); ^ is before 4.
        /// Step 2: "42" (no characters read because there is neither a '-' nor '+'); ^ is before 4.
        /// Step 3: "[42]" ("42" is read); ^ is after 2.
        ///
        /// Example 2:
        /// Input: s = "   -042"
        /// Output: -42
        /// Explanation:
        /// Step 1: "[   ]-042" (leading whitespace is read and ignored); ^ is before '-'.
        /// Step 2: "   [-]042" ('-' is read, so the result is negative); ^ is before 0.
        /// Step 3: "   -[042]" ("042" is read, with leading zeros ignored in the result); ^ is after 2.
        ///
        /// Example 3:
        /// Input: s = "1337c0d3"
        /// Output: 1337
        /// Explanation:
        /// Step 1: "1337c0d3" (no characters read because there is no leading whitespace); ^ is before 1.
        /// Step 2: "1337c0d3" (no characters read because there is neither a '-' nor '+'); ^ is before 1.
        /// Step 3: "[1337]c0d3" ("1337" is read; reading stops because the next character is a non-digit); ^ is before c.
        ///
        /// Example 4:
        /// Input: s = "0-1"
        /// Output: 0
        /// Explanation:
        /// Step 1: "0-1" (no characters read because there is no leading whitespace); ^ is before 0.
        /// Step 2: "0-1" (no characters read because there is neither a '-' nor '+'); ^ is before 0.
        /// Step 3: "[0]-1" ("0" is read; reading stops because the next character is a non-digit); ^ is before '-'.
        ///
        /// Example 5:
        /// Input: s = "words and 987"
        /// Output: 0
        /// Explanation: Reading stops at the first non-digit character 'w'.
        ///
        /// Constraints:
        /// - 0 &lt;= s.length &lt;= 200
        /// - s consists of English letters (lower-case and upper-case), digits (0-9), ' ', '+', '-', and '.'.
        /// </para>
        /// <para>
        /// 8. 字串轉整數 (atoi)
        /// https://leetcode.cn/problems/string-to-integer-atoi/description/
        ///
        /// 實作 myAtoi(string s) 函式，將字串轉換成有符號 32 位元整數。
        ///
        /// myAtoi(string s) 的演算法如下：
        /// - 空白：忽略所有前導空白字元（" "）。
        /// - 正負號：檢查下一個字元是否為 '-' 或 '+' 來決定正負號；若兩者皆不是，則視為正數。
        /// - 轉換：略過前導零並讀取整數，直到遇到非數字字元或到達字串結尾。若沒有讀取任何數字，結果為 0。
        /// - 邊界調整：若整數超出有符號 32 位元整數範圍 [-2^31, 2^31 - 1]，請將它調整到此範圍內。小於 -2^31 的整數調整為 -2^31，大於 2^31 - 1 的整數調整為 2^31 - 1。
        ///
        /// 回傳最終的整數結果。
        ///
        /// 範例 1：
        /// 輸入：s = "42"
        /// 輸出：42
        /// 解釋：方括號標示已讀取的字元，^ 表示目前讀取位置。
        /// 步驟 1："42"（因為沒有前導空白，所以尚未讀取任何字元）；^ 位於 4 之前。
        /// 步驟 2："42"（因為既沒有 '-' 也沒有 '+'，所以尚未讀取任何字元）；^ 位於 4 之前。
        /// 步驟 3："[42]"（讀取 "42"）；^ 位於 2 之後。
        ///
        /// 範例 2：
        /// 輸入：s = "   -042"
        /// 輸出：-42
        /// 解釋：
        /// 步驟 1："[   ]-042"（讀取並忽略前導空白）；^ 位於 '-' 之前。
        /// 步驟 2："   [-]042"（讀取 '-'，因此結果應為負數）；^ 位於 0 之前。
        /// 步驟 3："   -[042]"（讀取 "042"，結果會忽略前導零）；^ 位於 2 之後。
        ///
        /// 範例 3：
        /// 輸入：s = "1337c0d3"
        /// 輸出：1337
        /// 解釋：
        /// 步驟 1："1337c0d3"（因為沒有前導空白，所以尚未讀取任何字元）；^ 位於 1 之前。
        /// 步驟 2："1337c0d3"（因為既沒有 '-' 也沒有 '+'，所以尚未讀取任何字元）；^ 位於 1 之前。
        /// 步驟 3："[1337]c0d3"（讀取 "1337"；因為下一個字元不是數字而停止讀取）；^ 位於 c 之前。
        ///
        /// 範例 4：
        /// 輸入：s = "0-1"
        /// 輸出：0
        /// 解釋：
        /// 步驟 1："0-1"（因為沒有前導空白，所以尚未讀取任何字元）；^ 位於 0 之前。
        /// 步驟 2："0-1"（因為既沒有 '-' 也沒有 '+'，所以尚未讀取任何字元）；^ 位於 0 之前。
        /// 步驟 3："[0]-1"（讀取 "0"；因為下一個字元不是數字而停止讀取）；^ 位於 '-' 之前。
        ///
        /// 範例 5：
        /// 輸入：s = "words and 987"
        /// 輸出：0
        /// 解釋：讀取在第一個非數字字元 'w' 處停止。
        ///
        /// 限制條件：
        /// - 0 &lt;= s.length &lt;= 200
        /// - s 由英文字母（小寫與大寫）、數字 (0-9)、' '、'+'、'-' 和 '.' 組成。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Input, int Expected)[] sampleCases =
            [
                ("42", 42),
                (" -042", -42),
                ("1337c0d3", 1337),
                ("0-1", 0),
                ("words and 987", 0),
                ("", 0),
                ("   ", 0),
                ("+17", 17),
                ("2147483648", int.MaxValue),
                ("-2147483649", int.MinValue)
            ];

            int passedChecks = 0;
            for (int index = 0; index < sampleCases.Length; index++)
            {
                (string input, int expected) = sampleCases[index];
                int actual1 = MyAtoi(input);
                int actual2 = MyAtoi2(input);
                bool passed1 = actual1 == expected;
                bool passed2 = actual2 == expected;

                passedChecks += passed1 ? 1 : 0;
                passedChecks += passed2 ? 1 : 0;

                Console.WriteLine($"案例 {index + 1}");
                Console.WriteLine($"輸入：{FormatInput(input)}");
                Console.WriteLine($"預期：{expected}");
                Console.WriteLine($"解法一：{actual1} => {(passed1 ? "PASS" : "FAIL")}");
                Console.WriteLine($"解法二：{actual2} => {(passed2 ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            int totalChecks = sampleCases.Length * 2;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        }


        /// <summary>
        /// 使用索引由左至右掃描字串，依序略過前導空白、判斷符號並累積連續數字。
        /// 輸入必須是題目允許的非 null 字串；遇到第一個非數字字元便停止，
        /// 並在乘以 10 前檢查溢位，確保輸出落在 32 位元有號整數範圍。
        /// </summary>
        /// <param name="s">由英文字母、數字、空白、正負號或小數點組成的非 null 字串。</param>
        /// <returns>轉換後的整數；沒有可讀取數字時回傳 0，超出範圍時回傳對應邊界值。</returns>
        public static int MyAtoi(string s)
        {
            // 前導空白不參與數值解析，第一個非空白字元才可能是符號或數字。
            int i = 0;
            while (i < s.Length && s[i] == ' ')
            {
                i++;
            }

            if (i == s.Length)
            {
                return 0;
            }

            // 正負號只允許緊接在前導空白之後，並且最多讀取一次。
            bool isNegative = false;
            if (s[i] == '-')
            {
                isNegative = true;
                i++;
            }
            else if (s[i] == '+')
            {
                i++;
            }
            else if (s[i] < '0' || s[i] > '9')
            {
                return 0;
            }

            int result = 0;
            while (i < s.Length && s[i] >= '0' && s[i] <= '9')
            {
                int digit = s[i] - '0';

                // 先反推 result 可接受的上限，避免 result * 10 + digit 本身發生溢位。
                if (result > (int.MaxValue - digit) / 10)
                {
                    return isNegative ? int.MinValue : int.MaxValue;
                }

                result = result * 10 + digit;
                i++;
            }

            return isNegative ? -result : result;
        }

        /// <summary>
        /// 使用有限狀態機將字串轉換為 32 位元有號整數。
        /// 依目前狀態與字元類型決定下一個狀態，並在累積數字時將結果限制於合法範圍。
        /// </summary>
        /// <param name="s">由題目允許字元組成的非 null 字串。</param>
        /// <returns>轉換後的整數；超出範圍時回傳 <see cref="int.MinValue"/> 或 <see cref="int.MaxValue"/>。</returns>
        public static int MyAtoi2(string s)
        {
            ParseState state = ParseState.Start;
            long magnitude = 0;
            int sign = 1;

            foreach (char character in s)
            {
                CharacterType characterType = ClassifyCharacter(character);

                // 以「目前狀態 × 字元類型」查表，統一管理合法轉移與停止條件。
                state = StateTransitions[(int)state, (int)characterType];

                if (state == ParseState.End)
                {
                    break;
                }

                if (state == ParseState.Signed)
                {
                    sign = character == '-' ? -1 : 1;
                }
                else if (state == ParseState.InNumber)
                {
                    int digit = character - '0';
                    long limit = sign == -1 ? -(long)int.MinValue : int.MaxValue;

                    // 負數可容納的絕對值比正數多 1，因此依符號選擇不同上限。
                    if (magnitude > (limit - digit) / 10)
                    {
                        magnitude = limit;
                        break;
                    }

                    magnitude = magnitude * 10 + digit;
                }
            }

            return sign == -1 ? (int)-magnitude : (int)magnitude;
        }

        /// <summary>
        /// 將輸入字元分類為空白、正負號、數字或其他字元，供狀態機查詢轉移規則。
        /// </summary>
        /// <param name="character">目前讀取的字元。</param>
        /// <returns>對應的字元類型。</returns>
        private static CharacterType ClassifyCharacter(char character)
        {
            if (character == ' ')
            {
                return CharacterType.Space;
            }

            if (character == '+' || character == '-')
            {
                return CharacterType.Sign;
            }

            if (character >= '0' && character <= '9')
            {
                return CharacterType.Digit;
            }

            return CharacterType.Other;
        }

        /// <summary>
        /// 將測試字串包在雙引號中，並以可見符號表示空白，讓空字串與前導空白容易辨識。
        /// </summary>
        /// <param name="input">要顯示的測試輸入。</param>
        /// <returns>適合輸出至主控台的字串表示法。</returns>
        private static string FormatInput(string input)
        {
            return $"\"{input.Replace(' ', '␠')}\"";
        }
    }
}
