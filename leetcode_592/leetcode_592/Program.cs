namespace leetcode_592
{
    internal class Program
    {
        /// <summary>
        /// 592. Fraction Addition and Subtraction
        /// https://leetcode.com/problems/fraction-addition-and-subtraction/description/
        /// <para>
        /// Given a string expression representing an expression of fraction addition and subtraction, return the calculation result in string format.
        ///
        /// The final result must be an irreducible fraction. If it is an integer, convert it to a fraction with denominator 1. For example, 2 becomes 2/1.
        ///
        /// Example 1:
        /// Input: expression = "-1/2+1/2"
        /// Output: "0/1"
        ///
        /// Example 2:
        /// Input: expression = "-1/2+1/2+1/3"
        /// Output: "1/3"
        ///
        /// Example 3:
        /// Input: expression = "1/3-1/2"
        /// Output: "-1/6"
        ///
        /// Constraints:
        /// - The input string, and also the output, contains only '0' through '9', '/', '+', and '-'.
        /// - Each input and output fraction has format ±numerator/denominator. If the first input fraction or the output is positive, '+' is omitted.
        /// - Input contains only valid irreducible fractions whose numerator and denominator are always in [1, 10]. A denominator of 1 means the fraction is an integer in the format above.
        /// - The number of fractions is in [1, 10].
        /// - The final numerator and denominator are guaranteed to be valid 32-bit integers.
        /// </para>
        /// <para>
        /// 592. 分數加減運算
        /// https://leetcode.cn/problems/fraction-addition-and-subtraction/description/
        ///
        /// 給定字串 expression，表示分數加減運算式，請以字串格式回傳計算結果。
        ///
        /// 最終結果必須是不可約分數。若結果是整數，請轉換為分母為 1 的分數。例如，2 應轉換為 2/1。
        ///
        /// 範例 1：
        /// 輸入：expression = "-1/2+1/2"
        /// 輸出："0/1"
        ///
        /// 範例 2：
        /// 輸入：expression = "-1/2+1/2+1/3"
        /// 輸出："1/3"
        ///
        /// 範例 3：
        /// 輸入：expression = "1/3-1/2"
        /// 輸出："-1/6"
        ///
        /// 限制條件：
        /// - 輸入字串與輸出都只包含 '0' 到 '9'、'/'、'+' 與 '-'。
        /// - 每個輸入與輸出分數的格式都是 ±分子/分母。若第一個輸入分數或輸出為正數，則省略 '+'。
        /// - 輸入只包含有效的不可約分數，其分子與分母一定在 [1, 10] 範圍內。分母為 1 代表該分數是以上述格式表示的整數。
        /// - 分數數量在 [1, 10] 範圍內。
        /// - 最終結果的分子與分母保證是有效的 32 位元整數。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Expression, string Expected)[] testCases =
            [
                ("-1/2+1/2", "0/1"),
                ("-1/2+1/2+1/3", "1/3"),
                ("1/3-1/2", "-1/6"),
                ("1/1", "1/1"),
                ("5/3+1/3", "2/1"),
                ("1/2+1/3+1/4", "13/12"),
                ("-10/7+3/7", "-1/1"),
                ("1/10+1/10+1/10+1/10+1/10+1/10+1/10+1/10+1/10+1/10", "1/1")
            ];

            int passedCount = 0;

            for (int index = 0; index < testCases.Length; index++)
            {
                (string expression, string expected) = testCases[index];
                string actual = FractionAddition(expression);
                bool passed = actual == expected;

                if (passed)
                {
                    passedCount++;
                }

                Console.WriteLine($"案例 {index + 1}：expression = \"{expression}\"");
                Console.WriteLine($"預期：\"{expected}\"");
                Console.WriteLine($"實際：\"{actual}\"");
                Console.WriteLine($"結果：{(passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCount}/{testCases.Length} 筆測試通過");
        }


        /// <summary>
        /// 計算由分數加法與減法組成的合法運算式，並回傳最簡分數。
        /// 逐字解析每個分數的符號、分子與分母，再以交叉相乘的方式累加至共同分母，
        /// 最後使用最大公約數同時約分累積結果的分子與分母。
        /// </summary>
        /// <param name="expression">
        /// 至少包含一個合法最簡分數的運算式；每個分數的分母皆為正數，分數之間以
        /// <c>+</c> 或 <c>-</c> 連接。
        /// </param>
        /// <returns>
        /// 最簡分數格式的計算結果；正值不含前導正號，整數以分母 <c>1</c> 表示，
        /// 零固定回傳 <c>0/1</c>。
        /// </returns>
        public static string FractionAddition(string expression)
        {
            long numerator = 0;
            long denominator = 1;
            int index = 0;
            int length = expression.Length;

            while (index < length)
            {
                // 每一段可能有正負號；第一個正分數則直接從數字開始。
                long fractionNumerator = 0;
                long sign = 1;
                if (expression[index] == '-' || expression[index] == '+')
                {
                    sign = expression[index] == '-' ? -1 : 1;
                    index++;
                }

                while (index < length && char.IsDigit(expression[index]))
                {
                    fractionNumerator = fractionNumerator * 10 + expression[index] - '0';
                    index++;
                }

                fractionNumerator *= sign;

                // 題目保證格式合法，因此分子後的下一個字元必定是 '/'。
                index++;

                long fractionDenominator = 0;
                while (index < length && char.IsDigit(expression[index]))
                {
                    fractionDenominator = fractionDenominator * 10 + expression[index] - '0';
                    index++;
                }

                // a/b + c/d = (a*d + c*b) / (b*d)，逐段累加即可保留精確分數。
                numerator = numerator * fractionDenominator + fractionNumerator * denominator;
                denominator *= fractionDenominator;
            }

            if (numerator == 0)
            {
                // 零可寫成任意 0/n，題目要求統一正規化為 0/1。
                return "0/1";
            }

            long greatestCommonDivisor = GCD(Math.Abs(numerator), denominator);

            return (numerator / greatestCommonDivisor).ToString()
                + "/"
                + (denominator / greatestCommonDivisor).ToString();
        }


        /// <summary>
        /// 使用迭代版歐幾里得演算法計算兩個非負整數的最大公約數。
        /// 每輪以 <c>(b, a % b)</c> 取代原值，直到餘數為零；此方法由
        /// <see cref="FractionAddition(string)"/> 用來將最終分數約為最簡形式。
        /// </summary>
        /// <param name="a">第一個非負整數；與 <paramref name="b"/> 不應同時為零。</param>
        /// <param name="b">第二個非負整數；與 <paramref name="a"/> 不應同時為零。</param>
        /// <returns><paramref name="a"/> 與 <paramref name="b"/> 的最大公約數。</returns>
        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                // gcd(a, b) = gcd(b, a mod b)，替換後不會改變最大公約數。
                long temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }


        /// <summary>
        /// 使用遞迴版歐幾里得演算法計算兩個非負整數的最大公約數。
        /// 每層把問題縮小為 <c>GCD2(b, a % b)</c>，並在第二個參數為零時回傳結果；
        /// 此方法保留作為迭代版 <see cref="GCD(long, long)"/> 的教學比較。
        /// </summary>
        /// <param name="a">第一個非負整數；與 <paramref name="b"/> 不應同時為零。</param>
        /// <param name="b">第二個非負整數；與 <paramref name="a"/> 不應同時為零。</param>
        /// <returns><paramref name="a"/> 與 <paramref name="b"/> 的最大公約數。</returns>
        public static long GCD2(long a, long b)
        {
            if (b == 0)
            {
                return a;
            }

            return GCD2(b, a % b);
        }
    }
}
