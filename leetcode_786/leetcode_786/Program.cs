namespace leetcode_786
{
    internal class Program
    {
        /// <summary>
        /// 786. K-th Smallest Prime Fraction
        /// https://leetcode.com/problems/k-th-smallest-prime-fraction/description/
        /// <para>
        /// You are given a sorted integer array arr containing 1 and prime numbers, where all integers in arr are unique. You are also given an integer k.
        ///
        /// For every i and j where 0 &lt;= i &lt; j &lt; arr.length, consider the fraction arr[i] / arr[j].
        ///
        /// Return the k-th smallest fraction considered. Return the answer as an integer array of size 2, where answer[0] == arr[i] and answer[1] == arr[j].
        ///
        /// Example 1:
        /// Input: arr = [1,2,3,5], k = 3
        /// Output: [2,5]
        /// Explanation: The fractions in sorted order are 1/5, 1/3, 2/5, 1/2, 3/5, and 2/3. The third fraction is 2/5.
        ///
        /// Example 2:
        /// Input: arr = [1,7], k = 1
        /// Output: [1,7]
        ///
        /// Constraints:
        /// - 2 &lt;= arr.length &lt;= 1000
        /// - 1 &lt;= arr[i] &lt;= 3 * 10^4
        /// - arr[0] == 1
        /// - arr[i] is a prime number for i &gt; 0.
        /// - All numbers in arr are unique and sorted in strictly increasing order.
        /// - 1 &lt;= k &lt;= arr.length * (arr.length - 1) / 2
        ///
        /// Follow-up: Can you solve the problem with better than O(n^2) complexity?
        /// </para>
        /// <para>
        /// 786. 第 K 小的質數分數
        /// https://leetcode.cn/problems/k-th-smallest-prime-fraction/description/
        ///
        /// 給定已排序的整數陣列 arr，其中包含 1 與質數，且 arr 中所有整數都不相同；另給定整數 k。
        ///
        /// 對每一組滿足 0 &lt;= i &lt; j &lt; arr.length 的 i 與 j，考慮分數 arr[i] / arr[j]。
        ///
        /// 回傳所考慮分數中第 k 小的分數。答案以大小為 2 的整數陣列回傳，其中 answer[0] == arr[i]，answer[1] == arr[j]。
        ///
        /// 範例 1：
        /// 輸入：arr = [1,2,3,5], k = 3
        /// 輸出：[2,5]
        /// 解釋：依序排列的分數為 1/5、1/3、2/5、1/2、3/5 與 2/3。第三個分數是 2/5。
        ///
        /// 範例 2：
        /// 輸入：arr = [1,7], k = 1
        /// 輸出：[1,7]
        ///
        /// 限制條件：
        /// - 2 &lt;= arr.length &lt;= 1000
        /// - 1 &lt;= arr[i] &lt;= 3 * 10^4
        /// - arr[0] == 1
        /// - 當 i &gt; 0 時，arr[i] 是質數。
        /// - arr 中所有數字都不相同，並按嚴格遞增順序排列。
        /// - 1 &lt;= k &lt;= arr.length * (arr.length - 1) / 2
        ///
        /// 延伸問題：能否以優於 O(n^2) 的複雜度解決此問題？
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>
        /// 主要進入點會執行兩種解法的固定案例，不使用命令列參數；
        /// 輸出每項驗證結果，若有任一失敗則設定非零結束碼。
        /// </remarks>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行固定範例並比較兩種解法的結果。測試資料皆符合題目限制，
        /// 每一筆都會輸出輸入、預期結果、實際結果及是否通過。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] samples =
            {
                new("官方範例一", new[] { 1, 2, 3, 5 }, 3, new[] { 2, 5 }),
                new("官方範例二", new[] { 1, 7 }, 1, new[] { 1, 7 }),
                new("最小順位", new[] { 1, 2, 3, 5 }, 1, new[] { 1, 5 }),
                new("最大順位", new[] { 1, 2, 3, 5 }, 6, new[] { 2, 3 }),
                new("較長陣列的中間順位", new[] { 1, 2, 3, 5, 7 }, 7, new[] { 1, 2 })
            };

            int passedChecks = 0;
            int totalChecks = samples.Length * 2;

            for (int index = 0; index < samples.Length; index++)
            {
                SampleCase sample = samples[index];
                int[] sortingResult = KthSmallestPrimeFraction(sample.Input, sample.K);
                int[] heapResult = KthSmallestPrimeFraction2(sample.Input, sample.K);

                Console.WriteLine($"案例 {index + 1}：{sample.Name}");
                Console.WriteLine($"輸入：arr = {FormatArray(sample.Input)}, k = {sample.K}");
                Console.WriteLine($"預期：{FormatArray(sample.Expected)}");
                passedChecks += PrintResult("解法一（列舉排序）", sortingResult, sample.Expected);
                passedChecks += PrintResult("解法二（最小堆）", heapResult, sample.Expected);
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 顯示單一解法的實際結果並與預期陣列比較。
        /// 輸入為解法名稱、兩元素結果與預期值，輸出為通過檢查的數量（0 或 1）。
        /// </summary>
        private static int PrintResult(string solutionName, int[] actual, int[] expected)
        {
            bool passed = actual.SequenceEqual(expected);
            Console.WriteLine(
                $"{solutionName}：{FormatArray(actual)} => {(passed ? "PASS" : "FAIL")}");
            return passed ? 1 : 0;
        }

        /// <summary>
        /// 將整數陣列格式化為 README 與主控台共用的方括號表示法。
        /// 輸入可為任意整數陣列，輸出格式例如 <c>[1, 2, 3]</c>。
        /// </summary>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 表示一筆可執行範例，保存案例名稱、合法輸入、順位及預期的分子分母。
        /// </summary>
        private sealed record SampleCase(string Name, int[] Input, int K, int[] Expected);

        /// <summary>
        /// 列舉所有分子索引小於分母索引的分數，再以交叉相乘排序。
        /// 輸入必須是由 1 與不重複質數組成的嚴格遞增陣列，且 k 位於合法順位；
        /// 輸出為第 k 小分數的兩元素陣列 <c>[分子, 分母]</c>。
        /// </summary>
        /// <param name="arr">包含 1 與不重複質數的嚴格遞增陣列。</param>
        /// <param name="k">要尋找的分數順位，從 1 開始計算。</param>
        /// <returns>第 k 小分數的分子與分母。</returns>
        public static int[] KthSmallestPrimeFraction(int[] arr, int k)
        {
            int n = arr.Length;
            List<int[]> fractions = new List<int[]>();

            // 只建立 i < j 的組合，確保每個分數皆小於 1 且不重複列舉。
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    fractions.Add(new[] { arr[i], arr[j] });
                }
            }

            fractions.Sort(static (left, right) =>
                CompareFractions(
                    (left[0], left[1]),
                    (right[0], right[1])));

            return fractions[k - 1];
        }

        /// <summary>
        /// 使用最小堆合併多條已排序的分數序列。每個分母先放入最小分子，
        /// 取出目前最小值後再推進同一分母的下一個分子；輸入條件與解法一相同，
        /// 輸出為第 k 小分數的兩元素陣列 <c>[分子, 分母]</c>。
        /// </summary>
        /// <param name="arr">包含 1 與不重複質數的嚴格遞增陣列。</param>
        /// <param name="k">要尋找的分數順位，從 1 開始計算。</param>
        /// <returns>第 k 小分數的分子與分母。</returns>
        public static int[] KthSmallestPrimeFraction2(int[] arr, int k)
        {
            IComparer<(int Numerator, int Denominator)> fractionComparer =
                Comparer<(int Numerator, int Denominator)>.Create(CompareFractions);
            PriorityQueue<
                (int NumeratorIndex, int DenominatorIndex),
                (int Numerator, int Denominator)> minHeap = new PriorityQueue<
                    (int NumeratorIndex, int DenominatorIndex),
                    (int Numerator, int Denominator)>(fractionComparer);

            // 每個分母各自形成遞增序列，初始只放入該序列最小的 arr[0] / arr[j]。
            for (int denominatorIndex = 1; denominatorIndex < arr.Length; denominatorIndex++)
            {
                minHeap.Enqueue(
                    (0, denominatorIndex),
                    (arr[0], arr[denominatorIndex]));
            }

            (int NumeratorIndex, int DenominatorIndex) current = default;

            for (int rank = 1; rank <= k; rank++)
            {
                current = minHeap.Dequeue();

                if (rank == k)
                {
                    break;
                }

                int nextNumeratorIndex = current.NumeratorIndex + 1;

                // 同一分母只推進一格，且分子索引必須維持小於分母索引。
                if (nextNumeratorIndex < current.DenominatorIndex)
                {
                    minHeap.Enqueue(
                        (nextNumeratorIndex, current.DenominatorIndex),
                        (arr[nextNumeratorIndex], arr[current.DenominatorIndex]));
                }
            }

            return new[]
            {
                arr[current.NumeratorIndex],
                arr[current.DenominatorIndex]
            };
        }

        /// <summary>
        /// 以交叉相乘精確比較兩個正分數，不轉換成浮點數。
        /// 輸入為兩組分子與分母，輸出負數、零或正數供排序與最小堆判定順序。
        /// </summary>
        private static int CompareFractions(
            (int Numerator, int Denominator) left,
            (int Numerator, int Denominator) right)
        {
            // a / b 與 c / d 的大小可由 a * d 與 c * b 決定，long 可避免乘法溢位。
            long leftProduct = (long)left.Numerator * right.Denominator;
            long rightProduct = (long)right.Numerator * left.Denominator;
            return leftProduct.CompareTo(rightProduct);
        }
    }
}
