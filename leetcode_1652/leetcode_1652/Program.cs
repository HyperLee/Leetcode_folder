namespace leetcode_1652
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1652. Defuse the Bomb
        /// https://leetcode.com/problems/defuse-the-bomb/description/
        ///
        /// You have a bomb to defuse. Your informer provides a circular array code of length n and a key k. Replace every
        /// number simultaneously:
        /// - If k &gt; 0, replace the i-th number with the sum of the next k numbers.
        /// - If k &lt; 0, replace it with the sum of the previous -k numbers.
        /// - If k == 0, replace it with 0.
        /// Since code is circular, code[n-1] is followed by code[0], and code[0] is preceded by code[n-1]. Return the decrypted code.
        ///
        /// Example 1:
        /// Input: code = [5,7,1,4], k = 3
        /// Output: [12,10,16,13]
        /// Explanation: Replace each number by the next 3: [7+1+4, 1+4+5, 4+5+7, 5+7+1].
        ///
        /// Example 2:
        /// Input: code = [1,2,3,4], k = 0
        /// Output: [0,0,0,0]
        /// Explanation: When k is 0, every number is replaced by 0.
        ///
        /// Example 3:
        /// Input: code = [2,4,9,3], k = -2
        /// Output: [12,5,6,13]
        /// Explanation: Using previous numbers gives [3+9, 2+3, 4+2, 9+4].
        ///
        /// Constraints:
        /// - n == code.length
        /// - 1 &lt;= n &lt;= 100
        /// - 1 &lt;= code[i] &lt;= 100
        /// - -(n - 1) &lt;= k &lt;= n - 1
        /// </para>
        /// <para>
        /// 1652. 拆除炸彈
        /// https://leetcode.cn/problems/defuse-the-bomb/description/
        ///
        /// 你有一枚炸彈需要拆除。線人提供長度為 n 的循環陣列 code 與密鑰 k。所有數字同時替換：
        /// - 若 k &gt; 0，以接下來 k 個數字的總和替換第 i 個數字。
        /// - 若 k &lt; 0，以前面 -k 個數字的總和替換。
        /// - 若 k == 0，以 0 替換。
        /// code 為循環陣列，因此 code[n-1] 的下一個是 code[0]，code[0] 的前一個是 code[n-1]。回傳解密結果。
        ///
        /// 範例 1：
        /// 輸入：code = [5,7,1,4]，k = 3
        /// 輸出：[12,10,16,13]
        /// 解釋：以接下來 3 個數字替換：[7+1+4, 1+4+5, 4+5+7, 5+7+1]。
        ///
        /// 範例 2：
        /// 輸入：code = [1,2,3,4]，k = 0
        /// 輸出：[0,0,0,0]
        /// 解釋：k 為 0 時，每個數字都替換為 0。
        ///
        /// 範例 3：
        /// 輸入：code = [2,4,9,3]，k = -2
        /// 輸出：[12,5,6,13]
        /// 解釋：使用前面的數字得到 [3+9, 2+3, 4+2, 9+4]。
        ///
        /// 限制條件：
        /// - n == code.length
        /// - 1 &lt;= n &lt;= 100
        /// - 1 &lt;= code[i] &lt;= 100
        /// - -(n - 1) &lt;= k &lt;= n - 1
        /// </para>
        /// </summary>
        /// <remarks>
        /// 主要進入點會執行六組固定案例，比較取模滑動視窗、雙倍陣列滑動視窗與暴力模擬三種解法，
        /// 並以 Expected、Actual、PASS/FAIL 及結束代碼呈現驗證結果。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            bool allPassed = RunSamples();
            Environment.ExitCode = allPassed ? 0 : 1;
        }


        /// <summary>
        /// 以取模滑動視窗解密循環陣列；先依 <paramref name="k"/> 的正負建立第一個固定長度視窗，
        /// 再讓視窗向右滑動，使用取模索引處理跨越陣列尾端的元素。
        /// 輸入需符合題目限制：陣列長度為 1 到 100、元素為 1 到 100，且 <c>-(n - 1) &lt;= k &lt;= n - 1</c>。
        /// 方法不修改 <paramref name="code"/>，並回傳等長的新陣列；時間複雜度為 O(n)，輸出以外的額外空間為 O(1)。
        /// </summary>
        /// <remarks>
        /// 參考：
        /// https://leetcode.cn/problems/defuse-the-bomb/solutions/2765762/on-ding-chang-hua-dong-chuang-kou-python-y2py/
        /// https://leetcode.cn/problems/defuse-the-bomb/solutions/1845161/by-ac_oier-osbg/
        /// </remarks>
        /// <param name="code">要解密的非空循環整數陣列。</param>
        /// <param name="k">正數代表向後、負數代表向前、零代表全部輸出零。</param>
        /// <returns>依題目規則同時替換每個位置後的解密陣列。</returns>
        public static int[] Decrypt(int[] code, int k)
        {
            int n = code.Length;
            int[] result = new int[n];
            int rightExclusive = k > 0 ? k + 1 : n;
            int windowSize = Math.Abs(k);
            int windowSum = 0;

            // 正向第一個視窗是 [1, k]；反向第一個視窗是 [n - |k|, n - 1]。
            for (int i = rightExclusive - windowSize; i < rightExclusive; i++)
            {
                windowSum += code[i];
            }

            for (int i = 0; i < n; i++)
            {
                result[i] = windowSum;

                // 視窗向右移一格：加入右端新元素並移除距離 windowSize 的舊元素。
                int enteringIndex = rightExclusive % n;
                int leavingIndex = (rightExclusive - windowSize) % n;
                windowSum += code[enteringIndex] - code[leavingIndex];
                rightExclusive++;
            }

            return result;
        }


        /// <summary>
        /// 以雙倍陣列滑動視窗解密循環陣列；先把輸入複製兩次，將跨越尾端的區間轉成連續索引，
        /// 再以固定長度視窗逐格更新答案。
        /// 輸入需符合題目限制：陣列長度為 1 到 100、元素為 1 到 100，且 <c>-(n - 1) &lt;= k &lt;= n - 1</c>。
        /// 方法不修改 <paramref name="code"/>，並回傳等長的新陣列；時間複雜度為 O(n)，輸出以外的額外空間為 O(n)。
        /// </summary>
        /// <remarks>
        /// 參考：https://leetcode.cn/problems/defuse-the-bomb/solutions/1843157/chai-zha-dan-by-leetcode-solution-01x3/
        /// </remarks>
        /// <param name="code">要解密的非空循環整數陣列。</param>
        /// <param name="k">正數代表向後、負數代表向前、零代表全部輸出零。</param>
        /// <returns>依題目規則同時替換每個位置後的解密陣列。</returns>
        public static int[] Decrypt2(int[] code, int k)
        {
            int n = code.Length;
            if (k == 0)
            {
                return new int[n];
            }

            int[] result = new int[n];
            int[] extendedCode = new int[n * 2];

            // 複製成 code + code，讓所有環狀視窗都能用連續的左右邊界表示。
            Array.Copy(code, 0, extendedCode, 0, n);
            Array.Copy(code, 0, extendedCode, n, n);

            int left = k > 0 ? 1 : n + k;
            int right = k > 0 ? k : n - 1;
            int windowSum = 0;

            for (int i = left; i <= right; i++)
            {
                windowSum += extendedCode[i];
            }

            for (int i = 0; i < n; i++)
            {
                result[i] = windowSum;

                // 左右邊界同步右移，扣掉離開視窗的元素並加入右側新元素。
                windowSum -= extendedCode[left];
                windowSum += extendedCode[right + 1];
                left++;
                right++;
            }

            return result;
        }

        /// <summary>
        /// 以暴力模擬解密循環陣列；依 <paramref name="k"/> 的正負方向，為每個位置逐一加總相鄰的 <c>|k|</c> 個元素。
        /// 輸入需符合題目限制：陣列長度為 1 到 100、元素為 1 到 100，且 <c>-(n - 1) &lt;= k &lt;= n - 1</c>。
        /// 方法不修改 <paramref name="code"/>，並回傳等長的新陣列；時間複雜度為 O(n × |k|)，輸出以外的額外空間為 O(1)。
        /// </summary>
        /// <param name="code">要解密的非空循環整數陣列。</param>
        /// <param name="k">正數代表向後、負數代表向前、零代表全部輸出零。</param>
        /// <returns>依題目規則同時替換每個位置後的解密陣列。</returns>
        public static int[] DecryptBruteForce(int[] code, int k)
        {
            int n = code.Length;
            int[] result = new int[n];
            int direction = k > 0 ? 1 : -1;
            int stepCount = Math.Abs(k);

            for (int i = 0; i < n; i++)
            {
                for (int step = 1; step <= stepCount; step++)
                {
                    // 加上 n 再取模，讓向前走時的負索引映射回循環陣列尾端。
                    int index = (i + (direction * step) + n) % n;
                    result[i] += code[index];
                }
            }

            return result;
        }

        /// <summary>
        /// 執行六組固定案例，分別驗證三種解法的輸出與輸入不變性，並輸出測試總結。
        /// </summary>
        /// <returns>十八項「案例乘以解法」檢查全部通過時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            const int sampleCount = 6;
            const int solutionCount = 3;
            int passedCount = 0;

            passedCount += RunSample(
                "1. 官方範例（k > 0）",
                new[] { 5, 7, 1, 4 },
                3,
                new[] { 12, 10, 16, 13 });
            passedCount += RunSample(
                "2. 官方範例（k = 0）",
                new[] { 1, 2, 3, 4 },
                0,
                new[] { 0, 0, 0, 0 });
            passedCount += RunSample(
                "3. 官方範例（k < 0）",
                new[] { 2, 4, 9, 3 },
                -2,
                new[] { 12, 5, 6, 13 });
            passedCount += RunSample(
                "4. 最小長度",
                new[] { 8 },
                0,
                new[] { 0 });
            passedCount += RunSample(
                "5. 正向最大視窗與重複值",
                new[] { 1, 1, 1, 1 },
                3,
                new[] { 3, 3, 3, 3 });
            passedCount += RunSample(
                "6. 反向最大視窗",
                new[] { 10, 20, 30, 40, 50 },
                -4,
                new[] { 140, 130, 120, 110, 100 });

            int totalCount = sampleCount * solutionCount;
            Console.WriteLine();
            Console.WriteLine($"總結：{passedCount}/{totalCount} 項測試通過");
            return passedCount == totalCount;
        }

        /// <summary>
        /// 顯示單一案例的輸入與預期結果，並依序執行三種解法。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="code">符合題目限制的循環陣列。</param>
        /// <param name="k">決定加總方向與元素數量的金鑰。</param>
        /// <param name="expected">此案例的預期解密結果。</param>
        /// <returns>本案例通過的解法數量，範圍為零到三。</returns>
        private static int RunSample(string name, int[] code, int k, int[] expected)
        {
            Console.WriteLine();
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"Input：code = {FormatArray(code)}, k = {k}");

            int passedCount = 0;

            if (RunSolution("解法一：Decrypt（取模滑動視窗）", Decrypt, code, k, expected))
            {
                passedCount++;
            }

            if (RunSolution("解法二：Decrypt2（雙倍陣列滑動視窗）", Decrypt2, code, k, expected))
            {
                passedCount++;
            }

            if (RunSolution("解法三：DecryptBruteForce（暴力模擬）", DecryptBruteForce, code, k, expected))
            {
                passedCount++;
            }

            return passedCount;
        }

        /// <summary>
        /// 使用獨立輸入複本執行指定解法，檢查回傳陣列及呼叫後的輸入內容。
        /// </summary>
        /// <param name="solutionName">解法的顯示名稱。</param>
        /// <param name="solution">接受循環陣列與金鑰並回傳解密結果的函式。</param>
        /// <param name="code">案例的原始輸入陣列。</param>
        /// <param name="k">案例的金鑰。</param>
        /// <param name="expected">案例的預期解密結果。</param>
        /// <returns>輸出正確且解法未修改輸入時回傳 <see langword="true"/>。</returns>
        private static bool RunSolution(
            string solutionName,
            Func<int[], int, int[]> solution,
            int[] code,
            int k,
            int[] expected)
        {
            int[] workingCode = (int[])code.Clone();
            int[] actual = solution(workingCode, k);
            bool outputMatches = actual.SequenceEqual(expected);
            bool inputUnchanged = workingCode.SequenceEqual(code);
            bool passed = outputMatches && inputUnchanged;

            Console.WriteLine(solutionName);
            Console.WriteLine($"Expected：{FormatArray(expected)}");
            Console.WriteLine($"Actual：{FormatArray(actual)}");
            Console.WriteLine($"Input unchanged：{(inputUnchanged ? "PASS" : "FAIL")}");
            Console.WriteLine($"Result：{(passed ? "PASS" : "FAIL")}");

            return passed;
        }

        /// <summary>
        /// 將整數陣列格式化為穩定、易於比對的方括號字串。
        /// </summary>
        /// <param name="values">要顯示的整數陣列。</param>
        /// <returns>以逗號與空格分隔元素的字串。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }
    }
}