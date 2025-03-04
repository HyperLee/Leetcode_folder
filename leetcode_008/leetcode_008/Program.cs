namespace leetcode_008
{
    internal class Program
    {
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
            string s = " -42";

            Console.WriteLine("res: " + MyAtoi(s));
        }


        /// <summary>
        /// 注意事項:
        /// 1. 忽略前導空格
        /// 2. 首字只能是正號/負號/數字，否則不合法（回傳0）
        /// 3. 繼續往後匹配字符，直到結尾或不為數字為止（匹配過程中如果出現溢出，根據正負直接返回Integer.MAX_VALUE或Integer.MIN_VALUE）。
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The converted integer.</returns>
        public static int MyAtoi(string s)
        {
            // 忽略前導空格
            // 找到第一個非空格字符
            // i 用來標示當前字串中的 index
            int i = 0;
            while (i < s.Length && s[i] == ' ')
            {
                i++;
            }

            // 如果字符串全是空格，返回0
            if (i == s.Length)
            {
                return 0;
            }

            // 檢查符號
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

            // 轉換數字並處理溢出
            int result = 0;
            while (i < s.Length && s[i] >= '0' && s[i] <= '9')
            {
                // 轉換字符為數字
                int digit = s[i] - '0';
                // 檢查是否溢出
                if (result > (int.MaxValue - digit) / 10)
                {
                    // 根據正負返回對應的最大或最小值
                    return isNegative ? int.MinValue : int.MaxValue;
                }
                // 添加新數字; 沒有溢出的情況下，繼續累加數字
                result = result * 10 + digit;
                // 繼續往後匹配字符
                i++;
            }

            // 返回最終結果
            return isNegative ? -result : result;
        }
    }
}
