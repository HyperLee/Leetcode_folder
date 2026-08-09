namespace leetcode_2053
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 2053. Kth Distinct String in an Array
        /// https://leetcode.com/problems/kth-distinct-string-in-an-array/description/
        ///
        /// A distinct string appears exactly once in an array. Given string array arr and integer k, return the k-th distinct string in its original order. If fewer than k distinct strings exist, return "".
        ///
        /// Example 1:
        /// Input: arr = ["d","b","c","b","c","a"], k = 2
        /// Output: "a"
        /// Explanation: The distinct strings are "d" and "a". "d" is 1st and "a" is 2nd, so k == 2 returns "a".
        ///
        /// Example 2:
        /// Input: arr = ["aaa","aa","a"], k = 1
        /// Output: "aaa"
        /// Explanation: Every string is distinct, so the 1st string is returned.
        ///
        /// Example 3:
        /// Input: arr = ["a","b","a"], k = 3
        /// Output: ""
        /// Explanation: Only "b" is distinct. Fewer than 3 distinct strings exist, so return "".
        ///
        /// Constraints:
        /// - 1 &lt;= k &lt;= arr.length &lt;= 1000
        /// - 1 &lt;= arr[i].length &lt;= 5
        /// - arr[i] consists of lowercase English letters.
        /// </para>
        /// <para>
        /// 2053. 陣列中第 K 個獨一無二的字串
        /// https://leetcode.cn/problems/kth-distinct-string-in-an-array/description/
        ///
        /// 獨一無二的字串是在陣列中恰好出現一次的字串。給定字串陣列 arr 與整數 k，依原始出現順序回傳第 k 個獨一無二的字串。若不足 k 個，回傳 ""。
        ///
        /// 範例 1：
        /// 輸入：arr = ["d","b","c","b","c","a"], k = 2
        /// 輸出："a"
        /// 說明：獨一無二的字串是 "d" 與 "a"；"d" 是第 1 個、"a" 是第 2 個，因此 k == 2 時回傳 "a"。
        ///
        /// 範例 2：
        /// 輸入：arr = ["aaa","aa","a"], k = 1
        /// 輸出："aaa"
        /// 說明：所有字串都獨一無二，因此回傳第 1 個字串。
        ///
        /// 範例 3：
        /// 輸入：arr = ["a","b","a"], k = 3
        /// 輸出：""
        /// 說明：只有 "b" 獨一無二；不足 3 個，因此回傳 ""。
        ///
        /// 限制條件：
        /// - 1 &lt;= k &lt;= arr.length &lt;= 1000
        /// - 1 &lt;= arr[i].length &lt;= 5
        /// - arr[i] 僅由小寫英文字母組成。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int passedChecks = 0;
            int totalChecks = 0;

            passedChecks += RunCase("官方範例一", ["d", "b", "c", "b", "c", "a"], 2, "a");
            totalChecks += 2;
            passedChecks += RunCase("官方範例二：全部字串皆不重複", ["aaa", "aa", "a"], 1, "aaa");
            totalChecks += 2;
            passedChecks += RunCase("官方範例三：不重複字串不足 k 個", ["a", "b", "a"], 3, string.Empty);
            totalChecks += 2;
            passedChecks += RunCase("邊界案例：陣列只有一個字串", ["only"], 1, "only");
            totalChecks += 2;
            passedChecks += RunCase("全部重複", ["x", "x", "y", "y"], 1, string.Empty);
            totalChecks += 2;
            passedChecks += RunCase("維持原始順序", ["x", "y", "x", "z", "w", "z"], 2, "w");
            totalChecks += 2;

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 使用字典統計每個字串的出現次數，再依原陣列順序尋找第 <paramref name="k"/> 個只出現一次的字串。
        /// 輸入須符合題目條件：陣列非空、字串由小寫英文字母組成，且 <paramref name="k"/> 介於 1 與陣列長度之間。
        /// 此方法不會修改輸入陣列；若不重複字串不足 <paramref name="k"/> 個，則回傳空字串。
        /// </summary>
        /// <param name="arr">依題目原始順序排列的字串陣列。</param>
        /// <param name="k">要取得的不重複字串順位，採一維計數。</param>
        /// <returns>第 <paramref name="k"/> 個只出現一次的字串；若不存在則為空字串。</returns>
        public static string KthDistinct(string[] arr, int k)
        {
            Dictionary<string, int> frequency = new Dictionary<string, int>(StringComparer.Ordinal);

            // 第一輪只負責建立完整頻率，避免尚未看完陣列就誤判字串為不重複。
            foreach (string value in arr)
            {
                if (frequency.TryGetValue(value, out int count))
                {
                    frequency[value] = count + 1;
                }
                else
                {
                    frequency[value] = 1;
                }
            }

            int distinctCount = 0;

            // 第二輪掃描原陣列，直接以輸入順序決定第 k 個不重複字串。
            foreach (string value in arr)
            {
                if (frequency[value] == 1)
                {
                    distinctCount++;

                    if (distinctCount == k)
                    {
                        return value;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 以暴力逐項比對計算每個候選字串的出現次數，並依原陣列順序尋找第 <paramref name="k"/> 個不重複字串。
        /// 輸入須符合題目條件：陣列非空、字串由小寫英文字母組成，且 <paramref name="k"/> 介於 1 與陣列長度之間。
        /// 此方法不使用額外集合且不修改輸入陣列；若不重複字串不足 <paramref name="k"/> 個，則回傳空字串。
        /// </summary>
        /// <param name="arr">依題目原始順序排列的字串陣列。</param>
        /// <param name="k">要取得的不重複字串順位，採一維計數。</param>
        /// <returns>第 <paramref name="k"/> 個只出現一次的字串；若不存在則為空字串。</returns>
        public static string KthDistinctBruteForce(string[] arr, int k)
        {
            int distinctCount = 0;

            for (int candidateIndex = 0; candidateIndex < arr.Length; candidateIndex++)
            {
                int occurrenceCount = 0;

                for (int comparisonIndex = 0; comparisonIndex < arr.Length; comparisonIndex++)
                {
                    if (string.Equals(arr[candidateIndex], arr[comparisonIndex], StringComparison.Ordinal))
                    {
                        occurrenceCount++;

                        // 一旦確認至少重複一次，就不必繼續掃描剩餘元素。
                        if (occurrenceCount > 1)
                        {
                            break;
                        }
                    }
                }

                if (occurrenceCount == 1)
                {
                    distinctCount++;

                    if (distinctCount == k)
                    {
                        return arr[candidateIndex];
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 執行一組固定測試資料，分別比對字典解法與暴力解法的實際輸出，並列印 Expected、Actual 與 PASS/FAIL。
        /// 輸入案例須符合題目限制；回傳值代表本案例兩種解法中通過的檢查數，範圍為 0 到 2。
        /// </summary>
        /// <param name="caseName">顯示於主控台的案例名稱。</param>
        /// <param name="arr">本案例使用的字串陣列。</param>
        /// <param name="k">本案例要查找的不重複字串順位。</param>
        /// <param name="expected">兩種解法都應回傳的預期結果。</param>
        /// <returns>本案例通過的解法檢查數。</returns>
        private static int RunCase(string caseName, string[] arr, int k, string expected)
        {
            string dictionaryActual = KthDistinct(arr, k);
            string bruteForceActual = KthDistinctBruteForce(arr, k);
            bool dictionaryPassed = string.Equals(dictionaryActual, expected, StringComparison.Ordinal);
            bool bruteForcePassed = string.Equals(bruteForceActual, expected, StringComparison.Ordinal);

            Console.WriteLine($"Case: {caseName}");
            Console.WriteLine($"Input: arr = {FormatArray(arr)}, k = {k}");
            PrintResult(nameof(KthDistinct), expected, dictionaryActual, dictionaryPassed);
            PrintResult(nameof(KthDistinctBruteForce), expected, bruteForceActual, bruteForcePassed);
            Console.WriteLine();

            return (dictionaryPassed ? 1 : 0) + (bruteForcePassed ? 1 : 0);
        }

        /// <summary>
        /// 將單一解法的名稱、預期值、實際值與驗證結果輸出成一致格式，方便人工閱讀及 README 收錄。
        /// 輸入字串可為空字串；此方法沒有回傳值，只負責輸出一筆檢查結果。
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
        /// 將字串陣列格式化為包含雙引號的可讀表示法，供測試輸入輸出使用。
        /// 輸入須為非 null 陣列；回傳結果不會修改原陣列。
        /// </summary>
        /// <param name="arr">要格式化的字串陣列。</param>
        /// <returns>例如 <c>["a", "b"]</c> 的陣列文字。</returns>
        private static string FormatArray(string[] arr)
        {
            return $"[{string.Join(", ", arr.Select(FormatValue))}]";
        }

        /// <summary>
        /// 將字串包在雙引號中，讓空字串也能明確顯示為 <c>""</c>。
        /// 輸入須為非 null 字串；輸出僅供測試與文件展示。
        /// </summary>
        /// <param name="value">要格式化的字串。</param>
        /// <returns>加上雙引號的字串。</returns>
        private static string FormatValue(string value)
        {
            return $"\"{value}\"";
        }
    }
}