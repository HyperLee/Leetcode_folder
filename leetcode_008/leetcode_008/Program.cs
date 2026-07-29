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
        /// 8. 字串轉換整數(atoi)
        /// https://leetcode.cn/problems/string-to-integer-atoi/description/
        /// 
        /// 解題步驟:
        /// 1. 忽略前導空格。
        /// 2. 檢查第一個非空字符是否為正號或負號，並記錄符號。
        /// 3. 逐字符轉換數字，直到遇到非數字字符或到達字符串末尾。
        /// 4. 在轉換過程中檢查是否溢出，若溢出則返回對應的最大或最小值。
        /// 4.1 如果整數數超過32 位元有符號整數範圍，需要截斷這個整數，使其保持在這個範圍內。
        /// 5. 返回最終結果，根據符號決定正負。
        /// 
        /// 如何判斷是否溢出:
        /// 在每次添加新數字之前，檢查當前結果是否會因為乘以 10 並加上新數字而超過 int.MaxValue。
        /// 當我們要執行 result = result * 10 + digit 時，需要確保這個運算不會超過 int.MaxValue。
        /// 具體來說，如果 result > (int.MaxValue - digit) / 10，則表示添加新數字後會溢出。
        /// 
        /// 我們可以將這個條件寫成不等式：
        /// result * 10 + digit ≤ int.MaxValue
        /// 通過數學變換：
        /// result * 10 ≤ int.MaxValue - digit
        /// result ≤ (int.MaxValue - digit) / 10
        /// 
        /// 在以上的理解基礎上，正確處理邊界情況，確保程式在各種輸入下均能正常運行。
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
