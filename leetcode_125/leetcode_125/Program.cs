namespace leetcode_125
{
    internal class Program
    {
        /// <summary>
        /// 125. Valid Palindrome
        /// https://leetcode.com/problems/valid-palindrome/
        /// 125. 验证回文串
        /// https://leetcode.cn/problems/valid-palindrome/
        /// 
        /// 回文判斷, 將大寫轉小寫
        /// 以及將非文字部分忽略不比對
        /// 從左邊 或是右邊 開始 文字要相同
        /// 
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
