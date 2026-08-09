using System.Text;

namespace leetcode_402
{
    internal class Program
    {
        /// <summary>
        /// 402. Remove K Digits
        /// https://leetcode.com/problems/remove-k-digits/description/
        /// <para>
        /// Given string num representing a non-negative integer num, and an integer k, return the smallest possible integer after removing k digits from num.
        ///
        /// Example 1:
        /// Input: num = "1432219", k = 3
        /// Output: "1219"
        /// Explanation: Remove the three digits 4, 3, and 2 to form the new number 1219, which is the smallest.
        ///
        /// Example 2:
        /// Input: num = "10200", k = 1
        /// Output: "200"
        /// Explanation: Remove the leading 1 and the number is 200. The output must not contain leading zeroes.
        ///
        /// Example 3:
        /// Input: num = "10", k = 2
        /// Output: "0"
        /// Explanation: Remove all the digits from the number; it is left with nothing, which is 0.
        ///
        /// Constraints:
        /// - 1 &lt;= k &lt;= num.length &lt;= 10^5
        /// - num consists only of digits.
        /// - num does not have any leading zeros except for the zero itself.
        /// </para>
        /// <para>
        /// 402. 移掉 K 位數字
        /// https://leetcode.cn/problems/remove-k-digits/description/
        ///
        /// 給定表示非負整數 num 的字串 num，以及整數 k，回傳從 num 移除 k 個數字後所能得到的最小整數。
        ///
        /// 範例 1：
        /// 輸入：num = "1432219", k = 3
        /// 輸出："1219"
        /// 解釋：移除三個數字 4、3 與 2，形成新數字 1219，這是能得到的最小值。
        ///
        /// 範例 2：
        /// 輸入：num = "10200", k = 1
        /// 輸出："200"
        /// 解釋：移除開頭的 1 後得到 200。輸出不得含有前導零。
        ///
        /// 範例 3：
        /// 輸入：num = "10", k = 2
        /// 輸出："0"
        /// 解釋：移除所有數字後不剩任何內容，因此結果為 0。
        ///
        /// 限制條件：
        /// - 1 &lt;= k &lt;= num.length &lt;= 10^5
        /// - num 只由數字組成。
        /// - 除了零本身之外，num 不含任何前導零。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SampleCase[] samples =
            [
                new("官方範例一", "1432219", 3, "1219"),
                new("移除高位並去除前導零", "10200", 1, "200"),
                new("刪除全部數字", "10", 2, "0"),
                new("單調遞增時從尾端刪除", "123456", 3, "123"),
                new("刪除後產生多個前導零", "10001", 1, "1"),
                new("重複數字", "1111", 2, "11"),
                new("最小合法輸入", "0", 1, "0"),
                new("連續遞減數字", "9876543210", 9, "0")
            ];

            int passedChecks = 0;
            int totalChecks = samples.Length * 2;

            for (int i = 0; i < samples.Length; i++)
            {
                SampleCase sample = samples[i];
                string stackResult = RemoveKdigits(sample.Number, sample.DigitsToRemove);
                string simulationResult = RemoveKdigits2(sample.Number, sample.DigitsToRemove);
                bool stackPassed = stackResult == sample.Expected;
                bool simulationPassed = simulationResult == sample.Expected;

                if (stackPassed)
                {
                    passedChecks++;
                }

                if (simulationPassed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"案例 {i + 1}：{sample.Name}");
                Console.WriteLine(
                    $"輸入：num = {FormatResult(sample.Number)}, k = {sample.DigitsToRemove}");
                Console.WriteLine($"Expected:       {FormatResult(sample.Expected)}");
                Console.WriteLine($"解法一 Actual: {FormatResult(stackResult)}");
                Console.WriteLine($"解法一結果：{(stackPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"解法二 Actual: {FormatResult(simulationResult)}");
                Console.WriteLine($"解法二結果：{(simulationPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項檢查通過");
        }

        /// <summary>
        /// 使用單調遞增堆疊移除指定數量的數字，使剩餘數字最小。
        /// 掃描輸入時優先移除高位中比目前數字大的元素，讓較小數字盡早出現在前方；
        /// 輸入必須是沒有非法前導零的數字字串，且 <paramref name="k"/> 介於 1 與字串長度之間。
        /// 回傳結果不含前導零，若所有位數都被移除或只剩零，則回傳 <c>"0"</c>。
        /// </summary>
        /// <param name="num">僅由數字組成，且除 <c>"0"</c> 外不含前導零的非負整數字串。</param>
        /// <param name="k">必須移除的數字位數，範圍為 1 到 <paramref name="num"/> 的長度。</param>
        /// <returns>移除恰好 <paramref name="k"/> 位後可得到的最小數字字串。</returns>
        public static string RemoveKdigits(string num, int k)
        {
            StringBuilder stack = new();
            int remainingRemovals = k;

            foreach (char digit in num)
            {
                // 較大的高位會使整體數值變大，因此遇到較小數字時優先彈出高位。
                while (stack.Length > 0 &&
                       remainingRemovals > 0 &&
                       stack[^1] > digit)
                {
                    stack.Length--;
                    remainingRemovals--;
                }

                stack.Append(digit);
            }

            // 若輸入一路非遞減，前方已是最小排列，只能從影響最小的尾端刪除。
            while (remainingRemovals > 0)
            {
                stack.Length--;
                remainingRemovals--;
            }

            return NormalizeResult(stack);
        }

        /// <summary>
        /// 使用直觀貪心模擬逐次移除數字，使剩餘數字最小。
        /// 每一輪刪除第一個大於右鄰數字的高位；若整體非遞減，則刪除最後一位。
        /// 輸入必須是沒有非法前導零的數字字串，且 <paramref name="k"/> 介於 1 與字串長度之間。
        /// 回傳結果不含前導零，若所有位數都被移除或只剩零，則回傳 <c>"0"</c>。
        /// </summary>
        /// <param name="num">僅由數字組成，且除 <c>"0"</c> 外不含前導零的非負整數字串。</param>
        /// <param name="k">必須移除的數字位數，範圍為 1 到 <paramref name="num"/> 的長度。</param>
        /// <returns>移除恰好 <paramref name="k"/> 位後可得到的最小數字字串。</returns>
        public static string RemoveKdigits2(string num, int k)
        {
            StringBuilder digits = new(num);

            for (int removal = 0; removal < k; removal++)
            {
                int removalIndex = digits.Length - 1;

                // 第一個下降位置代表目前能改善的最高位；找不到時才刪除尾端。
                for (int i = 0; i < digits.Length - 1; i++)
                {
                    if (digits[i] > digits[i + 1])
                    {
                        removalIndex = i;
                        break;
                    }
                }

                digits.Remove(removalIndex, 1);
            }

            return NormalizeResult(digits);
        }

        /// <summary>
        /// 將演算法產生的數字序列正規化為題目要求的輸出格式。
        /// 輸入可為空或含有前導零；方法略過所有前導零，
        /// 並回傳無前導零的字串，若沒有非零數字則回傳 <c>"0"</c>。
        /// </summary>
        /// <param name="digits">要正規化的可變數字序列。</param>
        /// <returns>不含前導零的數字字串，或代表零的 <c>"0"</c>。</returns>
        private static string NormalizeResult(StringBuilder digits)
        {
            int firstNonZeroIndex = 0;

            // 輸出不得保留前導零；全零與空序列最後都統一表示為 "0"。
            while (firstNonZeroIndex < digits.Length && digits[firstNonZeroIndex] == '0')
            {
                firstNonZeroIndex++;
            }

            return firstNonZeroIndex == digits.Length
                ? "0"
                : digits.ToString(firstNonZeroIndex, digits.Length - firstNonZeroIndex);
        }

        /// <summary>
        /// 將數字字串加上雙引號，讓空字串、零與一般數字在主控台輸出中清楚可辨。
        /// 輸入為任意字串，輸出為前後各包含一個雙引號的顯示文字。
        /// </summary>
        /// <param name="value">要顯示的數字字串。</param>
        /// <returns>以雙引號包住的顯示字串。</returns>
        private static string FormatResult(string value)
        {
            return $"\"{value}\"";
        }

        /// <summary>
        /// 表示一組移除 K 位數字的可執行案例。
        /// 資料包含案例名稱、合法數字字串、刪除位數與預期最小結果，
        /// 供進入點同時驗證兩種解法並輸出逐案結果。
        /// </summary>
        /// <param name="Name">案例的繁體中文顯示名稱。</param>
        /// <param name="Number">符合題目限制的非負整數字串。</param>
        /// <param name="DigitsToRemove">必須移除的數字位數。</param>
        /// <param name="Expected">移除後預期得到的最小數字字串。</param>
        private sealed record SampleCase(
            string Name,
            string Number,
            int DigitsToRemove,
            string Expected);
    }
}