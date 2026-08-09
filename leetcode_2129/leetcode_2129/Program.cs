using System.Text;

namespace leetcode_2129
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 2129. Capitalize the Title
        /// https://leetcode.com/problems/capitalize-the-title/description/
        ///
        /// You are given a title containing one or more words separated by one space. Capitalize each word as follows: if its length is 1 or 2, make every letter lowercase; otherwise uppercase the first letter and lowercase the rest. Return the capitalized title.
        ///
        /// Example 1:
        /// Input: title = "capiTalIze tHe titLe"
        /// Output: "Capitalize The Title"
        /// Explanation: Every word has length at least 3, so each first letter is uppercase and the rest lowercase.
        ///
        /// Example 2:
        /// Input: title = "First leTTeR of EACH Word"
        /// Output: "First Letter of Each Word"
        /// Explanation: "of" has length 2 and is lowercase. Every other word has length at least 3.
        ///
        /// Example 3:
        /// Input: title = "i lOve leetcode"
        /// Output: "i Love Leetcode"
        /// Explanation: "i" has length 1 and is lowercase. The remaining words have length at least 3.
        ///
        /// Constraints:
        /// - 1 &lt;= title.length &lt;= 100
        /// - Words are separated by one space with no leading or trailing spaces.
        /// - Every non-empty word contains uppercase or lowercase English letters.
        /// </para>
        /// <para>
        /// 2129. 將標題首字母大寫
        /// https://leetcode.cn/problems/capitalize-the-title/description/
        ///
        /// 給定由一個或多個單字組成、單字間以一個空格分隔的標題。依下列規則調整每個單字：長度為 1 或 2 時全部轉為小寫；否則首字母轉為大寫，其餘轉為小寫。回傳調整後的標題。
        ///
        /// 範例 1：
        /// 輸入：title = "capiTalIze tHe titLe"
        /// 輸出："Capitalize The Title"
        /// 說明：每個單字長度至少為 3，因此首字母大寫，其餘小寫。
        ///
        /// 範例 2：
        /// 輸入：title = "First leTTeR of EACH Word"
        /// 輸出："First Letter of Each Word"
        /// 說明："of" 長度為 2，因此全部小寫；其他單字長度至少為 3。
        ///
        /// 範例 3：
        /// 輸入：title = "i lOve leetcode"
        /// 輸出："i Love Leetcode"
        /// 說明："i" 長度為 1，因此小寫；其他單字長度至少為 3。
        ///
        /// 限制條件：
        /// - 1 &lt;= title.length &lt;= 100
        /// - 單字以一個空格分隔，且沒有前導或尾端空格。
        /// - 每個非空單字由大小寫英文字母組成。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 主要進入點會執行五組固定案例，比較逐字組裝與字元陣列掃描兩種解法，
        /// 並以 Expected、Actual 與 PASS/FAIL 顯示驗證結果。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            bool allPassed = RunSamples();
            Environment.ExitCode = allPassed ? 0 : 1;
        }

        /// <summary>
        /// 執行五組符合題目限制的固定案例，分別驗證逐字組裝與字元陣列掃描解法。
        /// </summary>
        /// <returns>十項答案檢查全部通過時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, string Title, string Expected)[] cases =
            {
                ("1. 官方範例一", "capiTalIze tHe titLe", "Capitalize The Title"),
                ("2. 官方範例二", "First leTTeR of EACH Word", "First Letter of Each Word"),
                ("3. 官方範例三", "i lOve leetcode", "i Love Leetcode"),
                ("4. 單字長度臨界值", "a AB abc", "a ab Abc"),
                ("5. 輸入長度上限", new string('A', 100), $"A{new string('a', 99)}")
            };

            int passedChecks = 0;
            const int checksPerCase = 2;
            int totalChecks = cases.Length * checksPerCase;

            foreach ((string name, string title, string expected) in cases)
            {
                passedChecks += RunCase(name, title, expected);
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項測試通過");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 將單一案例交給兩種解法，顯示輸入、預期答案、實際答案與驗證結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="title">符合題目限制的標題字串。</param>
        /// <param name="expected">人工推導的預期正規化結果。</param>
        /// <returns>本案例通過的解法數量，範圍為零到二。</returns>
        private static int RunCase(string name, string title, string expected)
        {
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"Input：title = {FormatValue(title)}");

            string builderActual = CapitalizeTitle(title);
            string arrayActual = CapitalizeTitle2(title);
            bool builderPassed = builderActual == expected;
            bool arrayPassed = arrayActual == expected;

            Console.WriteLine("解法一：CapitalizeTitle（分割單字與逐字組裝）");
            Console.WriteLine($"Expected：{FormatValue(expected)}");
            Console.WriteLine($"Actual：{FormatValue(builderActual)}");
            Console.WriteLine($"Result：{(builderPassed ? "PASS" : "FAIL")}");
            Console.WriteLine("解法二：CapitalizeTitle2（字元陣列單次掃描）");
            Console.WriteLine($"Expected：{FormatValue(expected)}");
            Console.WriteLine($"Actual：{FormatValue(arrayActual)}");
            Console.WriteLine($"Result：{(arrayPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (builderPassed ? 1 : 0) + (arrayPassed ? 1 : 0);
        }

        /// <summary>
        /// 將字串包在雙引號中，建立穩定且能清楚顯示大小寫的測試輸出。
        /// </summary>
        /// <param name="value">要格式化的字串。</param>
        /// <returns>以雙引號包住的字串。</returns>
        private static string FormatValue(string value)
        {
            return $"\"{value}\"";
        }

        /// <summary>
        /// 將標題依空白分割成單字，再逐字判斷大小寫規則並使用 <see cref="StringBuilder"/> 組合答案。
        /// 輸入需符合題目限制：長度為 1 到 100、單字由單一空白分隔且只含英文字母。
        /// 長度一或二的單字會全部轉為小寫；其餘單字只有首字母大寫。方法不修改輸入字串，
        /// 回傳完成正規化的新標題；時間複雜度為 O(n)，額外空間為 O(n)。
        /// </summary>
        /// <param name="title">要依題目規則調整大小寫的合法標題。</param>
        /// <returns>每個單字都已依長度完成大小寫正規化的新字串。</returns>
        public static string CapitalizeTitle(string title)
        {
            StringBuilder builder = new StringBuilder(title.Length);
            string[] words = title.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                if (builder.Length > 0)
                {
                    builder.Append(' ');
                }

                // 單字長度超過二時只有首字母大寫，否則連首字母也必須是小寫。
                if (word.Length > 2)
                {
                    builder.Append(char.ToUpperInvariant(word[0]));
                }
                else
                {
                    builder.Append(char.ToLowerInvariant(word[0]));
                }

                for (int j = 1; j < word.Length; j++)
                {
                    builder.Append(char.ToLowerInvariant(word[j]));
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// 將標題轉為字元陣列，以空白或字串結尾識別每個單字區間，並直接在陣列中調整大小寫。
        /// 輸入需符合題目限制：長度為 1 到 100、單字由單一空白分隔且只含英文字母。
        /// 長度一或二的單字會全部轉為小寫；其餘單字只有首字母大寫。方法不修改輸入字串，
        /// 回傳由正規化字元陣列建立的新標題；時間複雜度為 O(n)，額外空間為 O(n)。
        /// </summary>
        /// <param name="title">要依題目規則調整大小寫的合法標題。</param>
        /// <returns>每個單字都已依長度完成大小寫正規化的新字串。</returns>
        public static string CapitalizeTitle2(string title)
        {
            char[] characters = title.ToCharArray();
            int wordStart = 0;

            for (int boundary = 0; boundary <= characters.Length; boundary++)
            {
                if (boundary < characters.Length && characters[boundary] != ' ')
                {
                    continue;
                }

                int wordLength = boundary - wordStart;

                // 先掌握完整單字長度，再一次處理該區間，避免另外建立分割後的字串陣列。
                for (int index = wordStart; index < boundary; index++)
                {
                    bool isLongWordInitial = index == wordStart && wordLength > 2;
                    characters[index] = isLongWordInitial
                        ? char.ToUpperInvariant(characters[index])
                        : char.ToLowerInvariant(characters[index]);
                }

                wordStart = boundary + 1;
            }

            return new string(characters);
        }
    }
}