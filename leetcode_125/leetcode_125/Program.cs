namespace leetcode_125
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 125. Valid Palindrome
        /// https://leetcode.com/problems/valid-palindrome/description/
        ///
        /// A phrase is a palindrome if, after converting all uppercase letters into lowercase letters and
        /// removing all non-alphanumeric characters, it reads the same forward and backward. Alphanumeric
        /// characters include letters and numbers.
        /// Given a string s, return true if it is a palindrome, or false otherwise.
        ///
        /// Example 1:
        /// Input: s = "A man, a plan, a canal: Panama"
        /// Output: true
        /// Explanation: "amanaplanacanalpanama" is a palindrome.
        ///
        /// Example 2:
        /// Input: s = "race a car"
        /// Output: false
        /// Explanation: "raceacar" is not a palindrome.
        ///
        /// Example 3:
        /// Input: s = " "
        /// Output: true
        /// Explanation: s is an empty string "" after removing non-alphanumeric characters.
        /// Since an empty string reads the same forward and backward, it is a palindrome.
        ///
        /// Constraints:
        /// 1 &lt;= s.length &lt;= 2 * 10^5
        /// s consists only of printable ASCII characters.
        /// </para>
        /// <para>
        /// 125. 驗證回文
        /// https://leetcode.cn/problems/valid-palindrome/description/
        ///
        /// 將一個片語中的所有大寫字母轉為小寫，並移除所有非英數字元後，若正向與反向讀取結果相同，
        /// 則此片語是回文。英數字元包括字母與數字。
        /// 給定字串 s，若它是回文則回傳 true，否則回傳 false。
        ///
        /// 範例 1：
        /// 輸入：s = "A man, a plan, a canal: Panama"
        /// 輸出：true
        /// 解釋："amanaplanacanalpanama" 是回文。
        ///
        /// 範例 2：
        /// 輸入：s = "race a car"
        /// 輸出：false
        /// 解釋："raceacar" 不是回文。
        ///
        /// 範例 3：
        /// 輸入：s = " "
        /// 輸出：true
        /// 解釋：移除非英數字元後，s 會成為空字串 ""。
        /// 由於空字串正向與反向讀取都相同，因此它是回文。
        ///
        /// 限制條件：
        /// 1 &lt;= s.length &lt;= 2 * 10^5
        /// s 只包含可列印的 ASCII 字元。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Input, bool Expected)[] testCases =
            [
                ("A man, a plan, a canal: Panama", true),
                ("race a car", false),
                (" ", true),
                ("", true),
                (".,!?", true),
                ("0P", false),
                ("No 'x' in Nixon", true)
            ];

            int passedCount = 0;

            for (int index = 0; index < testCases.Length; index++)
            {
                (string input, bool expected) = testCases[index];
                bool actual = IsPalindrome(input);
                bool isPassed = actual == expected;
                string displayedInput = input.Length == 0 ? "\"\"" : $"\"{input}\"";

                if (isPassed)
                {
                    passedCount++;
                }

                Console.WriteLine($"案例 {index + 1}");
                Console.WriteLine($"輸入：{displayedInput}");
                Console.WriteLine($"預期：{expected}");
                Console.WriteLine($"實際：{actual}");
                Console.WriteLine($"結果：{(isPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCount}/{testCases.Length} 筆測試通過");
        }


        /// <summary>
        /// 判斷非 null 字串忽略非英數字元及大小寫後是否為回文。
        /// 解法使用左右雙指針直接在原始字串上向中央比對，不建立清理後的暫存字串。
        /// 輸入可包含英文字母、數字與其他可列印字元；若有效字元前後順序相同則回傳
        /// <see langword="true"/>，否則回傳 <see langword="false"/>。
        /// </summary>
        /// <param name="s">要檢查的非 null 字串。</param>
        /// <returns>忽略非英數字元與大小寫後是否為回文。</returns>
        public static bool IsPalindrome(string s)
        {
            int left = 0, right = s.Length - 1;

            while (left < right)
            {
                // 先讓左右指針各自略過不參與回文判斷的非英數字元。
                while (left < right && !char.IsLetterOrDigit(s[left]))
                {
                    left++;
                }

                while (left < right && !char.IsLetterOrDigit(s[right]))
                {
                    right--;
                }

                // 將兩端字元轉成相同大小寫後比較；一旦不同即可判定不是回文。
                if (char.ToLower(s[left]) != char.ToLower(s[right]))
                {
                    return false;
                }

                // 本輪有效字元相同，兩個指針同時向中央收斂。
                left++;
                right--;
            }

            return true;
        }
    }
}
