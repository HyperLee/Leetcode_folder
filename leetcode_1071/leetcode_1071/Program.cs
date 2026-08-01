using System.Text;

namespace leetcode_1071
{
    internal class Program
    {
        /// <summary>
        /// 1071. Greatest Common Divisor of Strings
        /// https://leetcode.com/problems/greatest-common-divisor-of-strings/
        /// 
        /// 1071. 字符串的最大公因子
        /// https://leetcode.cn/problems/greatest-common-divisor-of-strings/description/
        /// 
        /// 对于字符串 s 和 t，只有在 s = t + t + t + ... + t + t（t 自身连接 1 次或多次）时，我们才认定 “t 能除尽 s”。
        /// 给定两个字符串 str1 和 str2 。返回 最长字符串 x，要求满足 x 能除尽 str1 且 x 能除尽 str2 。
        /// 
        /// 最大公因數（英語：highest common factor，hcf）也稱最大公約數（英語：greatest common divisor，gcd）是數學詞彙，
        /// 指能夠整除多個非零整數的最大正整數。例如8和12的最大公因數為4。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Environment.ExitCode = RunSamples() ? 0 : 1;
        }

        /// <summary>
        /// 執行固定測試案例，逐一驗證三種字串最大公因子解法。
        /// 此方法不接受輸入；輸出每個案例的輸入、預期值、實際值與通過狀態，
        /// 並回傳所有解法檢查是否全部通過。
        /// </summary>
        /// <returns>全部檢查通過時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, string Str1, string Str2, string Expected)[] cases =
            [
                ("官方範例一", "ABCABC", "ABC", "ABC"),
                ("官方範例二", "ABABAB", "ABAB", "AB"),
                ("官方範例三", "LEET", "CODE", ""),
                ("最小合法輸入", "A", "A", "A"),
                ("兩字串完全相同", "ABCABC", "ABCABC", "ABCABC"),
                ("較短字串位於第一參數", "ABAB", "ABABAB", "AB"),
                ("長度相容但內容不相容", "AAAAAB", "AAA", "")
            ];

            int passedChecks = 0;
            foreach ((string name, string str1, string str2, string expected) in cases)
            {
                passedChecks += RunTestCase(name, str1, str2, expected);
            }

            int totalChecks = cases.Length * 3;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過。");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 執行一組字串案例並比較三種解法。
        /// 輸入包含案例名稱、兩個符合題目限制的非空大寫英文字串與預期結果；
        /// 輸出繁中比較資訊，並回傳答案符合預期的解法數量。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="str1">第一個待求最大公因子的字串。</param>
        /// <param name="str2">第二個待求最大公因子的字串。</param>
        /// <param name="expected">預期的最長共同因子字串。</param>
        /// <returns>通過的解法數量，範圍為 0 到 3。</returns>
        private static int RunTestCase(string name, string str1, string str2, string expected)
        {
            string actual1 = GcdOfStrings(str1, str2);
            string actual2 = GcdOfStrings2(str1, str2);
            string actual3 = GcdOfStrings3(str1, str2);
            bool passed1 = actual1 == expected;
            bool passed2 = actual2 == expected;
            bool passed3 = actual3 == expected;

            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"輸入：str1 = \"{str1}\", str2 = \"{str2}\"");
            Console.WriteLine($"預期：\"{expected}\"");
            Console.WriteLine($"解法一實際：\"{actual1}\" => {(passed1 ? "PASS" : "FAIL")}");
            Console.WriteLine($"解法二實際：\"{actual2}\" => {(passed2 ? "PASS" : "FAIL")}");
            Console.WriteLine($"解法三實際：\"{actual3}\" => {(passed3 ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (passed1 ? 1 : 0) + (passed2 ? 1 : 0) + (passed3 ? 1 : 0);
        }


        /// <summary>
        /// 使用「長度最大公因數加重複拼接驗證」找出最長共同因子字串。
        /// 解法先以歐幾里得算法取得兩字串長度的最大公因數，再取第一個字串的對應前綴，
        /// 並驗證該前綴能否分別重複組成兩個輸入；輸入須為符合題目限制的非空大寫英文字串。
        /// </summary>
        /// <param name="str1">第一個待求最大公因子的非空字串。</param>
        /// <param name="str2">第二個待求最大公因子的非空字串。</param>
        /// <returns>最長共同因子字串；若不存在則回傳空字串。</returns>
        /// <remarks>時間複雜度為 O(m + n)，額外空間複雜度為 O(m + n)。</remarks>
        public static string GcdOfStrings(string str1, string str2)
        {
            int gcdLength = CalculateGcd(str1.Length, str2.Length);
            string candidate = str1[..gcdLength];

            // 長度符合只是必要條件，仍須確認候選週期能完整組成兩個輸入。
            if (IsDivisor(candidate, str1) && IsDivisor(candidate, str2))
            {
                return candidate;
            }

            return "";
        }

        /// <summary>
        /// 使用「拼接交換律加長度最大公因數」找出最長共同因子字串。
        /// 若兩字串由同一個基本週期組成，交換拼接順序後內容必定相同；相容時再取長度最大公因數對應的前綴。
        /// 輸入須為符合題目限制的非空大寫英文字串。
        /// </summary>
        /// <param name="str1">第一個待求最大公因子的非空字串。</param>
        /// <param name="str2">第二個待求最大公因子的非空字串。</param>
        /// <returns>最長共同因子字串；若不存在則回傳空字串。</returns>
        /// <remarks>時間複雜度為 O(m + n)，額外空間複雜度為 O(m + n)。</remarks>
        public static string GcdOfStrings2(string str1, string str2)
        {
            // 交換拼接順序仍相等，代表兩字串共享同一個週期來源。
            if (str1 + str2 != str2 + str1)
            {
                return "";
            }

            int gcdLength = CalculateGcd(str1.Length, str2.Length);
            return str1[..gcdLength];
        }

        /// <summary>
        /// 使用由長到短枚舉候選前綴的方式找出最長共同因子字串。
        /// 解法只檢查能同時整除兩個字串長度的候選長度，再驗證候選前綴能否重複組成兩個輸入；
        /// 輸入須為符合題目限制的非空大寫英文字串。
        /// </summary>
        /// <param name="str1">第一個待求最大公因子的非空字串。</param>
        /// <param name="str2">第二個待求最大公因子的非空字串。</param>
        /// <returns>最長共同因子字串；若不存在則回傳空字串。</returns>
        /// <remarks>最壞時間複雜度為 O(min(m, n) * (m + n))，額外空間複雜度為 O(m + n)。</remarks>
        public static string GcdOfStrings3(string str1, string str2)
        {
            int maxCandidateLength = Math.Min(str1.Length, str2.Length);

            for (int candidateLength = maxCandidateLength; candidateLength >= 1; candidateLength--)
            {
                // 不能同時整除兩個長度的前綴，不可能重複組成兩個完整字串。
                if (str1.Length % candidateLength != 0 || str2.Length % candidateLength != 0)
                {
                    continue;
                }

                string candidate = str1[..candidateLength];
                if (IsDivisor(candidate, str1) && IsDivisor(candidate, str2))
                {
                    return candidate;
                }
            }

            return "";
        }

        /// <summary>
        /// 驗證候選字串是否能經由重複拼接完整組成指定字串。
        /// 輸入的候選字串與原始字串都必須非空，且原始字串長度必須可被候選長度整除；
        /// 輸出表示候選字串是否為原始字串的字串因子。
        /// </summary>
        /// <param name="candidate">要重複拼接的非空候選字串。</param>
        /// <param name="source">要驗證的非空原始字串。</param>
        /// <returns>候選字串能完整組成原始字串時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool IsDivisor(string candidate, string source)
        {
            int repetitions = source.Length / candidate.Length;
            StringBuilder builder = new StringBuilder(source.Length);

            for (int i = 0; i < repetitions; i++)
            {
                builder.Append(candidate);
            }

            return builder.ToString() == source;
        }

        /// <summary>
        /// 使用歐幾里得算法計算兩個正整數的最大公因數。
        /// 輸入必須為正整數；演算法反覆以餘數取代較大的數，直到餘數為零，
        /// 輸出可同時整除兩個輸入的最大正整數。
        /// </summary>
        /// <param name="a">第一個正整數。</param>
        /// <param name="b">第二個正整數。</param>
        /// <returns><paramref name="a"/> 與 <paramref name="b"/> 的最大公因數。</returns>
        private static int CalculateGcd(int a, int b)
        {
            while (b != 0)
            {
                // gcd(a, b) 等於 gcd(b, a mod b)，因此可持續縮小問題規模。
                int remainder = a % b;
                a = b;
                b = remainder;
            }

            return a;
        }
    }
}
