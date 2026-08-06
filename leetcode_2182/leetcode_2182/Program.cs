using System.Text;

namespace leetcode_2182
{
    internal class Program
    {
        /// <summary>
        /// 2182. Construct String With Repeat Limit
        /// https://leetcode.com/problems/construct-string-with-repeat-limit/description/?envType=daily-question&envId=2024-12-17
        /// 
        /// 2182. 构造限制重复的字符串
        /// https://leetcode.cn/problems/construct-string-with-repeat-limit/description/
        /// </summary>
        /// <remarks>
        /// 執行六組固定案例，逐一驗證雙指標與優先佇列解法；任一檢查失敗時，程式會設定非零結束碼。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            int passedChecks = 0;
            int totalChecks = 0;

            passedChecks += RunCase("官方範例一", "cczazcc", 3, "zzcccac");
            totalChecks += 2;
            passedChecks += RunCase("官方範例二", "aababab", 2, "bbabaa");
            totalChecks += 2;
            passedChecks += RunCase("無分隔字元時捨棄剩餘內容", "zzzz", 2, "zz");
            totalChecks += 2;
            passedChecks += RunCase("限制未造成截斷", "abcabc", 3, "ccbbaa");
            totalChecks += 2;
            passedChecks += RunCase("多次使用次大字元分隔", "ccbccb", 2, "ccbccb");
            totalChecks += 2;
            passedChecks += RunCase("最小合法輸入", "a", 1, "a");
            totalChecks += 2;

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        private const int AlphabetSize = 26;

        /// <summary>
        /// 使用固定長度的字元頻率陣列與雙指標，持續選取尚未用完且字典序最大的字元；
        /// 若連續次數已達 <paramref name="repeatLimit"/>，便插入一個次大字元中斷區段。
        /// 輸入須符合題目條件：<paramref name="s"/> 非空且只含小寫英文字母，
        /// <paramref name="repeatLimit"/> 介於 1 與字串長度之間；回傳結果不一定使用全部輸入字元。
        /// </summary>
        /// <remarks>
        /// 參考資料：
        /// https://leetcode.cn/problems/construct-string-with-repeat-limit/solutions/1300982/gou-zao-xian-zhi-zhong-fu-de-zi-fu-chuan-v02s/
        /// https://leetcode.cn/problems/construct-string-with-repeat-limit/solutions/1278723/cong-da-dao-xiao-tan-xin-by-endlesscheng-b7ob/
        /// https://leetcode.cn/problems/construct-string-with-repeat-limit/solutions/2781436/2182-gou-zao-xian-zhi-zhong-fu-de-zi-fu-ow48f/
        /// </remarks>
        /// <param name="s">用來建立結果、且只包含小寫英文字母的字串。</param>
        /// <param name="repeatLimit">相同字元允許連續出現的最大次數。</param>
        /// <returns>符合連續次數限制的字典序最大字串。</returns>
        public static string RepeatLimitedString(string s, int repeatLimit)
        {
            int[] remainingCount = new int[AlphabetSize];
            foreach (char c in s)
            {
                remainingCount[c - 'a']++;
            }

            StringBuilder result = new StringBuilder();
            int consecutiveCount = 0;

            // primaryIndex 指向目前最大字元；separatorIndex 只在需要打斷連續區段時往下尋找。
            for (int primaryIndex = AlphabetSize - 1, separatorIndex = AlphabetSize - 2;
                primaryIndex >= 0 && separatorIndex >= 0;)
            {
                if (remainingCount[primaryIndex] == 0)
                {
                    consecutiveCount = 0;
                    primaryIndex--;
                }
                else if (consecutiveCount < repeatLimit)
                {
                    remainingCount[primaryIndex]--;
                    result.Append((char)('a' + primaryIndex));
                    consecutiveCount++;
                }
                else if (separatorIndex >= primaryIndex || remainingCount[separatorIndex] == 0)
                {
                    separatorIndex--;
                }
                else
                {
                    // 只插入一個次大字元即可解除連續限制，下一輪便能再次使用最大字元。
                    remainingCount[separatorIndex]--;
                    result.Append((char)('a' + separatorIndex));
                    consecutiveCount = 0;
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// 使用最大優先佇列反覆取出目前字典序最大的字元，每次最多連續加入 <paramref name="repeatLimit"/> 個；
        /// 若該字元仍有剩餘，便插入一個次大字元中斷連續區段，再將尚未用完的字元放回佇列。
        /// 輸入須符合題目條件：<paramref name="s"/> 非空且只含小寫英文字母，
        /// <paramref name="repeatLimit"/> 介於 1 與字串長度之間；回傳結果不一定使用全部輸入字元。
        /// </summary>
        /// <param name="s">用來建立結果、且只包含小寫英文字母的字串。</param>
        /// <param name="repeatLimit">相同字元允許連續出現的最大次數。</param>
        /// <returns>符合連續次數限制的字典序最大字串。</returns>
        public static string RepeatLimitedStringWithPriorityQueue(string s, int repeatLimit)
        {
            int[] count = new int[AlphabetSize];
            foreach (char c in s)
            {
                count[c - 'a']++;
            }

            PriorityQueue<(int LetterIndex, int RemainingCount), int> availableLetters = new PriorityQueue<(int LetterIndex, int RemainingCount), int>();
            for (int letterIndex = 0; letterIndex < AlphabetSize; letterIndex++)
            {
                if (count[letterIndex] > 0)
                {
                    // PriorityQueue 預設取最小 priority；使用負索引即可優先取出字典序最大的字元。
                    availableLetters.Enqueue((letterIndex, count[letterIndex]), -letterIndex);
                }
            }

            StringBuilder result = new StringBuilder();

            while (availableLetters.Count > 0)
            {
                (int currentIndex, int currentCount) = availableLetters.Dequeue();
                int appendCount = Math.Min(currentCount, repeatLimit);
                result.Append((char)('a' + currentIndex), appendCount);
                currentCount -= appendCount;

                if (currentCount == 0)
                {
                    continue;
                }

                // 最大字元仍有剩餘時，必須用一個次大字元打斷；若不存在就無法再合法延長結果。
                if (availableLetters.Count == 0)
                {
                    break;
                }

                (int separatorIndex, int separatorCount) = availableLetters.Dequeue();
                result.Append((char)('a' + separatorIndex));
                separatorCount--;

                if (separatorCount > 0)
                {
                    availableLetters.Enqueue((separatorIndex, separatorCount), -separatorIndex);
                }

                availableLetters.Enqueue((currentIndex, currentCount), -currentIndex);
            }

            return result.ToString();
        }

        /// <summary>
        /// 執行一組固定測試資料，分別比對雙指標與優先佇列解法的實際輸出，並列印 Expected、Actual 與 PASS/FAIL。
        /// 輸入須符合題目限制；回傳值代表本案例兩種解法中通過的檢查數，範圍為 0 到 2。
        /// </summary>
        /// <param name="caseName">顯示於主控台的案例名稱。</param>
        /// <param name="s">僅包含小寫英文字母的輸入字串。</param>
        /// <param name="repeatLimit">相同字元允許連續出現的最大次數。</param>
        /// <param name="expected">兩種解法都應回傳的預期結果。</param>
        /// <returns>本案例通過的解法檢查數。</returns>
        private static int RunCase(string caseName, string s, int repeatLimit, string expected)
        {
            string twoPointerActual = RepeatLimitedString(s, repeatLimit);
            string priorityQueueActual = RepeatLimitedStringWithPriorityQueue(s, repeatLimit);
            bool twoPointerPassed = string.Equals(twoPointerActual, expected, StringComparison.Ordinal);
            bool priorityQueuePassed = string.Equals(priorityQueueActual, expected, StringComparison.Ordinal);

            Console.WriteLine($"Case: {caseName}");
            Console.WriteLine($"Input: s = {FormatValue(s)}, repeatLimit = {repeatLimit}");
            PrintResult(nameof(RepeatLimitedString), expected, twoPointerActual, twoPointerPassed);
            PrintResult(nameof(RepeatLimitedStringWithPriorityQueue), expected, priorityQueueActual, priorityQueuePassed);
            Console.WriteLine();

            return (twoPointerPassed ? 1 : 0) + (priorityQueuePassed ? 1 : 0);
        }

        /// <summary>
        /// 將單一解法的名稱、預期值、實際值與驗證結果輸出成一致格式，方便人工閱讀及 README 收錄。
        /// 輸入字串須為非 null；此方法沒有回傳值，只負責輸出一筆檢查結果。
        /// </summary>
        /// <param name="methodName">受測解法名稱。</param>
        /// <param name="expected">預期回傳值。</param>
        /// <param name="actual">實際回傳值。</param>
        /// <param name="passed">預期值與實際值是否相同。</param>
        private static void PrintResult(string methodName, string expected, string actual, bool passed)
        {
            Console.WriteLine($"{methodName}:");
            Console.WriteLine($"  Expected: {FormatValue(expected)}");
            Console.WriteLine($"  Actual:   {FormatValue(actual)}");
            Console.WriteLine($"  Result:   {(passed ? "PASS" : "FAIL")}");
        }

        /// <summary>
        /// 將非 null 字串包在雙引號中，讓測試輸入與空字串結果都能清楚顯示。
        /// 此方法只產生供主控台與文件展示的文字，不會修改輸入內容。
        /// </summary>
        /// <param name="value">要格式化的字串。</param>
        /// <returns>加上雙引號的字串。</returns>
        private static string FormatValue(string value)
        {
            return $"\"{value}\"";
        }

    }
}