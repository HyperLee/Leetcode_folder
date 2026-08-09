namespace leetcode_067
{
    internal class Program
    {
        /// <summary>
        /// 67. Add Binary
        /// https://leetcode.com/problems/add-binary/description/
        /// <para>
        /// Given two binary strings a and b, return their sum as a binary string.
        ///
        /// Example 1:
        /// Input: a = "11", b = "1"
        /// Output: "100"
        ///
        /// Example 2:
        /// Input: a = "1010", b = "1011"
        /// Output: "10101"
        ///
        /// Constraints:
        /// - 1 &lt;= a.length, b.length &lt;= 10^4
        /// - a and b consist only of the characters '0' and '1'.
        /// - Neither string contains leading zeros except for zero itself.
        /// </para>
        /// <para>
        /// 67. 二進位求和
        /// https://leetcode.cn/problems/add-binary/description/
        ///
        /// 給定兩個二進位字串 a 和 b，請以二進位字串回傳它們的總和。
        ///
        /// 範例 1：
        /// 輸入：a = "11", b = "1"
        /// 輸出："100"
        ///
        /// 範例 2：
        /// 輸入：a = "1010", b = "1011"
        /// 輸出："10101"
        ///
        /// 限制條件：
        /// - 1 &lt;= a.length, b.length &lt;= 10^4
        /// - a 和 b 只由字元 '0' 與 '1' 組成。
        /// - 除了零本身之外，兩個字串都不含前導零。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 以固定案例依序驗證兩種二進位字串加法，並輸出每次執行的預期值、實際值與通過狀態。
        /// </remarks>
        /// <param name="args">命令列參數；此範例不需要傳入任何參數。</param>
        static void Main(string[] args)
        {
            (string A, string B, string Expected)[] testCases =
            [
                ("11", "1", "100"),
                ("1010", "1011", "10101"),
                ("0", "0", "0"),
                ("1111", "1", "10000")
            ];

            int passed = 0;
            int total = testCases.Length * 2;

            for (int index = 0; index < testCases.Length; index++)
            {
                (string a, string b, string expected) = testCases[index];
                passed += RunTestCase(index + 1, a, b, expected);
            }

            Console.WriteLine($"Overall: {passed}/{total} passed.");
        }

        /// <summary>
        /// 執行一組二進位字串相加案例，讓 <see cref="AddBinary"/> 與
        /// <see cref="AddBinary2"/> 使用相同輸入，並分別比對預期結果。
        /// 輸入必須符合題目限制：兩個字串皆非空、只含 <c>0</c> 或 <c>1</c>；
        /// 回傳值為本案例通過的解法數量，範圍為 0 到 2。
        /// </summary>
        /// <param name="caseNumber">顯示於主控台的案例編號。</param>
        /// <param name="a">第一個合法的二進位字串。</param>
        /// <param name="b">第二個合法的二進位字串。</param>
        /// <param name="expected">兩個輸入相加後的預期二進位字串。</param>
        /// <returns>本案例中結果符合預期值的解法數量。</returns>
        private static int RunTestCase(int caseNumber, string a, string b, string expected)
        {
            string result1 = AddBinary(a, b);
            string result2 = AddBinary2(a, b);
            bool solution1Passed = result1 == expected;
            bool solution2Passed = result2 == expected;

            Console.WriteLine(
                $"Case {caseNumber}: a = \"{a}\", b = \"{b}\", Expected = \"{expected}\"");
            Console.WriteLine(
                $"  AddBinary:  Actual = \"{result1}\", {(solution1Passed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"  AddBinary2: Actual = \"{result2}\", {(solution2Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (solution1Passed ? 1 : 0) + (solution2Passed ? 1 : 0);
        }


        /// <summary>
        /// 將兩個二進位字串相加。此解法使用兩個索引從最低位往最高位掃描，
        /// 將每一位與前一輪進位相加，再由 <see cref="GetCarryAndUpdateResult"/>
        /// 對加總值 0、1、2、3 決定結果位元與下一輪進位。
        /// 輸入字串必須非空、只包含 <c>0</c> 或 <c>1</c>，且除 <c>"0"</c>
        /// 以外不含前導零；輸出為不含前導零的二進位總和字串。
        /// </summary>
        /// <param name="a">第一個合法的二進位字串。</param>
        /// <param name="b">第二個合法的二進位字串。</param>
        /// <returns><paramref name="a"/> 與 <paramref name="b"/> 相加後的二進位字串。</returns>
        public static string AddBinary(string a, string b)
        {
            List<char> result = new List<char>();
            int carry = 0;

            // 二進位加法必須由最低位向左處理，較短字串缺少的高位視為 0。
            for (int i = a.Length - 1, j = b.Length - 1; i >= 0 || j >= 0; i--, j--)
            {
                int aElement = i >= 0 ? int.Parse(a[i].ToString()) : 0;
                int bElement = j >= 0 ? int.Parse(b[j].ToString()) : 0;
                int tempResult = carry + aElement + bElement;

                carry = GetCarryAndUpdateResult(result, tempResult);
            }

            // 最高位計算完仍有進位時，答案需要再補上一個 1。
            if (carry == 1)
            {
                result.Add('1');
            }

            // 位元依低位到高位加入，因此輸出前要反轉成正常閱讀順序。
            result.Reverse();

            return new string(result.ToArray());
        }


        /// <summary>
        /// 將兩個二進位字串相加。此解法同樣從最低位往最高位掃描，
        /// 但直接以 <c>sum % 2</c> 取得當前位元、以 <c>sum / 2</c>
        /// 取得下一輪進位，並使用 <see cref="System.Text.StringBuilder"/> 暫存反向結果。
        /// 輸入字串必須非空、只包含 <c>0</c> 或 <c>1</c>，且除 <c>"0"</c>
        /// 以外不含前導零；輸出為不含前導零的二進位總和字串。
        /// </summary>
        /// <param name="a">第一個合法的二進位字串。</param>
        /// <param name="b">第二個合法的二進位字串。</param>
        /// <returns><paramref name="a"/> 與 <paramref name="b"/> 相加後的二進位字串。</returns>
        public static string AddBinary2(string a, string b)
        {
            System.Text.StringBuilder reversedResult = new System.Text.StringBuilder();
            int carry = 0;

            // 兩個索引各自向左移動，較短字串超出範圍後不再加入位元。
            for (int i = a.Length - 1, j = b.Length - 1; i >= 0 || j >= 0; i--, j--)
            {
                int sum = carry;

                if (i >= 0)
                {
                    sum += a[i] - '0';
                }

                if (j >= 0)
                {
                    sum += b[j] - '0';
                }

                reversedResult.Append((char)('0' + (sum % 2)));
                carry = sum / 2;
            }

            if (carry == 1)
            {
                reversedResult.Append('1');
            }

            char[] result = reversedResult.ToString().ToCharArray();
            Array.Reverse(result);

            return new string(result);
        }


        /// <summary>
        /// 根據單一位元欄位的加總值更新反向結果，並回傳下一個高位要使用的進位。
        /// 加總值只能是 0、1、2 或 3：0 與 1 不進位，2 與 3 進位；
        /// 寫入的結果位元依序為 0、1、0、1。
        /// </summary>
        /// <param name="result">依低位到高位順序暫存答案位元的集合。</param>
        /// <param name="tempResult">兩個當前位元加上舊進位後的值，範圍為 0 到 3。</param>
        /// <returns>下一個高位使用的進位值，必為 0 或 1。</returns>
        private static int GetCarryAndUpdateResult(List<char> result, int tempResult)
        {
            int carry = 0;

            switch (tempResult)
            {
                case 0:
                    carry = 0;
                    result.Add('0');
                    break;
                case 1:
                    carry = 0;
                    result.Add('1');
                    break;
                case 2:
                    carry = 1;
                    result.Add('0');
                    break;
                case 3:
                    carry = 1;
                    result.Add('1');
                    break;
            }

            return carry;
        }
    }
}
