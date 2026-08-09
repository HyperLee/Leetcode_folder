namespace leetcode_1636
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1636. Sort Array by Increasing Frequency
        /// https://leetcode.com/problems/sort-array-by-increasing-frequency/description/
        ///
        /// Given an integer array nums, sort it in increasing order by value frequency. If multiple values have the same
        /// frequency, sort those values in decreasing order. Return the sorted array.
        ///
        /// Example 1:
        /// Input: nums = [1,1,2,2,2,3]
        /// Output: [3,1,1,2,2,2]
        /// Explanation: '3' has frequency 1, '1' has frequency 2, and '2' has frequency 3.
        ///
        /// Example 2:
        /// Input: nums = [2,3,1,3,2]
        /// Output: [1,3,3,2,2]
        /// Explanation: '2' and '3' both have frequency 2, so they are sorted in decreasing order.
        ///
        /// Example 3:
        /// Input: nums = [-1,1,-6,4,5,-6,1,4,1]
        /// Output: [5,-1,4,4,-6,-6,1,1,1]
        ///
        /// Constraints:
        /// - 1 &lt;= nums.length &lt;= 100
        /// - -100 &lt;= nums[i] &lt;= 100
        /// </para>
        /// <para>
        /// 1636. 按照頻率將陣列升序排序
        /// https://leetcode.cn/problems/sort-array-by-increasing-frequency/description/
        ///
        /// 給定整數陣列 nums，按照數值出現頻率遞增排序；若多個數值頻率相同，則按數值遞減排序。
        /// 回傳排序後的陣列。
        ///
        /// 範例 1：
        /// 輸入：nums = [1,1,2,2,2,3]
        /// 輸出：[3,1,1,2,2,2]
        /// 解釋：'3' 的頻率為 1，'1' 的頻率為 2，'2' 的頻率為 3。
        ///
        /// 範例 2：
        /// 輸入：nums = [2,3,1,3,2]
        /// 輸出：[1,3,3,2,2]
        /// 解釋：'2' 與 '3' 的頻率同為 2，因此按數值遞減排序。
        ///
        /// 範例 3：
        /// 輸入：nums = [-1,1,-6,4,5,-6,1,4,1]
        /// 輸出：[5,-1,4,4,-6,-6,1,1,1]
        ///
        /// 限制條件：
        /// - 1 &lt;= nums.length &lt;= 100
        /// - -100 &lt;= nums[i] &lt;= 100
        /// </para>
        /// </summary>
        /// <remarks>
        /// 建立涵蓋官方範例、邊界值與同頻排序規則的固定案例，分別執行兩種解法，並驗證
        /// 實際輸出與預期結果一致且呼叫後輸入陣列保持不變。所有檢查皆通過時輸出摘要；
        /// 任一檢查失敗時將程序結束碼設為 1。
        /// </remarks>
        private static void Main()
        {
            (string Name, int[] Input, int[] Expected)[] cases =
            [
                ("Existing sample", [1, 5, 0, 5], [1, 0, 5, 5]),
                ("Official example 1", [1, 1, 2, 2, 2, 3], [3, 1, 1, 2, 2, 2]),
                ("Official example 2", [2, 3, 1, 3, 2], [1, 3, 3, 2, 2]),
                ("Official example 3", [-1, 1, -6, 4, 5, -6, 1, 4, 1], [5, -1, 4, 4, -6, -6, 1, 1, 1]),
                ("Single lower bound", [-100], [-100]),
                ("Repeated boundaries", [-100, 100, -100, 100, 0], [0, 100, 100, -100, -100]),
                ("All distinct tie-break", [4, -2, 7, 0], [7, 4, 0, -2]),
                ("All equal", [7, 7, 7], [7, 7, 7])
            ];
            (string Name, Func<int[], int[]> Sort)[] solutions =
            [
                ("FrequencySort - dictionary and comparison sort", FrequencySort),
                ("FrequencySort2 - frequency buckets", FrequencySort2)
            ];
            List<CaseResult> results = [];

            foreach ((string caseName, int[] input, int[] expected) in cases)
            {
                foreach ((string solutionName, Func<int[], int[]> sort) in solutions)
                {
                    results.Add(RunCase(caseName, solutionName, input, expected, sort));
                }
            }

            foreach (CaseResult result in results)
            {
                Console.WriteLine($"Case: {result.CaseName}");
                Console.WriteLine($"Solution: {result.SolutionName}");
                Console.WriteLine($"Input: {result.Input}");
                Console.WriteLine($"Expected: {result.Expected}");
                Console.WriteLine($"Actual: {result.Actual}");
                Console.WriteLine($"Input preserved: {(result.InputPreserved ? "PASS" : "FAIL")}");
                Console.WriteLine($"Result: {(result.Passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            int passedCount = results.Count(result => result.Passed);
            Console.WriteLine($"Summary: {passedCount}/{results.Count} checks passed.");

            if (passedCount != results.Count)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 以獨立輸入副本執行指定排序解法，比對預期輸出，並確認解法未修改收到的陣列；
        /// 輸入案例符合題目限制，回傳值包含格式化資料與整體通過狀態，不直接輸出至主控台。
        /// </summary>
        /// <param name="caseName">測試案例名稱。</param>
        /// <param name="solutionName">受測解法名稱。</param>
        /// <param name="input">符合題目限制的原始輸入。</param>
        /// <param name="expected">依排序規則得到的預期陣列。</param>
        /// <param name="sort">接收整數陣列並回傳排序結果的解法。</param>
        /// <returns>包含輸出比對、輸入保持狀態與顯示文字的案例結果。</returns>
        private static CaseResult RunCase(
            string caseName,
            string solutionName,
            int[] input,
            int[] expected,
            Func<int[], int[]> sort)
        {
            int[] workingInput = [.. input];
            int[] originalInput = [.. workingInput];
            int[] actual = sort(workingInput);
            bool inputPreserved = originalInput.SequenceEqual(workingInput);
            bool passed = expected.SequenceEqual(actual) && inputPreserved;

            return new CaseResult(
                caseName,
                solutionName,
                FormatArray(input),
                FormatArray(expected),
                FormatArray(actual),
                inputPreserved,
                passed);
        }

        /// <summary>
        /// 將整數序列格式化為不含額外空白的中括號表示法，供固定測試輸出與 README transcript 使用。
        /// </summary>
        /// <param name="values">要格式化的整數序列。</param>
        /// <returns>例如 <c>[1,3,3,2,2]</c> 的字串。</returns>
        private static string FormatArray(IEnumerable<int> values) => $"[{string.Join(',', values)}]";

        /// <summary>
        /// 保存單一案例與單一解法的顯示資料，以及輸入保持和整體驗證結果。
        /// </summary>
        /// <param name="CaseName">測試案例名稱。</param>
        /// <param name="SolutionName">受測解法名稱。</param>
        /// <param name="Input">格式化後的輸入。</param>
        /// <param name="Expected">格式化後的預期輸出。</param>
        /// <param name="Actual">格式化後的實際輸出。</param>
        /// <param name="InputPreserved">解法是否保留輸入內容。</param>
        /// <param name="Passed">輸出正確且輸入保持不變時為 <c>true</c>。</param>
        private sealed record CaseResult(
            string CaseName,
            string SolutionName,
            string Input,
            string Expected,
            string Actual,
            bool InputPreserved,
            bool Passed);


        /// <summary>
        /// 要注意: 
        /// If multiple values have the same frequency, sort them in decreasing order.
        /// 如果有多个值的频率相同，请你按照数值本身将它们 降序 排序。
        /// 
        /// 輸入的陣列是遞增
        /// 輸出頻率也是遞增
        /// 但是相同頻率, 數字是遞減
        /// 
        /// https://leetcode.cn/problems/sort-array-by-increasing-frequency/solutions/1831531/an-zhao-pin-lu-jiang-shu-zu-sheng-xu-pai-z2db/
        /// https://leetcode.cn/problems/sort-array-by-increasing-frequency/solutions/1833402/by-ac_oier-c3xc/
        /// https://leetcode.cn/problems/sort-array-by-increasing-frequency/solutions/1522014/by-stormsunshine-stv8/
        /// 
        /// 本題重點是最後的排序
        /// 相同頻率, 數字大小要遞減才是難點
        /// 
        /// 1.頻率不同, 依據"頻率"遞增排序
        /// 2.頻率相同, 依據"數字"遞減排序
        /// 
        /// lisr sort 排序ref:
        /// https://dotblogs.com.tw/shanna/2019/09/09/213800
        /// https://www.cnblogs.com/tomin/archive/2011/09/20/2182483.html
        /// https://www.hicsharp.com/a/7620ddb5eb644e448b06e0b8bbb97f41
        /// https://hackmd.io/@jiesen/r1awIjwlF
        /// </summary>
        /// <remarks>
        /// 先以 Dictionary 統計各數值的頻率，再對輸入內容的 List 副本使用自訂比較器：
        /// 頻率不同時由小到大排序，頻率相同時依數值由大到小排序。輸入需符合題目保證的
        /// <c>1 &lt;= nums.Length &lt;= 100</c> 與 <c>-100 &lt;= nums[i] &lt;= 100</c>，方法不修改輸入。
        /// </remarks>
        /// <param name="nums">符合題目限制、不可為 null 的整數陣列。</param>
        /// <returns>依頻率升序、同頻數值降序排列的新陣列。</returns>
        public static int[] FrequencySort(int[] nums)
        {
            Dictionary<int, int> frequencies = [];
            foreach (int num in nums)
            {
                if (frequencies.TryGetValue(num, out int frequency))
                {
                    frequencies[num] = frequency + 1;
                }
                else
                {
                    frequencies.Add(num, 1);
                }
            }

            List<int> sortedNumbers = [.. nums];
            sortedNumbers.Sort((first, second) =>
            {
                int firstFrequency = frequencies[first];
                int secondFrequency = frequencies[second];

                if (firstFrequency != secondFrequency)
                {
                    return firstFrequency.CompareTo(secondFrequency);
                }

                // 同頻時必須讓較大的數值排在前面。
                return second.CompareTo(first);
            });

            return sortedNumbers.ToArray();
        }

        /// <summary>
        /// 利用題目固定值域統計每個數值的出現次數，再將不同數值放入對應的頻率桶；依頻率
        /// 由小到大展開桶內容，並以值域的反向掃描保證同頻數值由大到小。輸入需符合
        /// <c>1 &lt;= nums.Length &lt;= 100</c> 與 <c>-100 &lt;= nums[i] &lt;= 100</c>，方法不修改輸入。
        /// </summary>
        /// <param name="nums">符合題目限制、不可為 null 的整數陣列。</param>
        /// <returns>依頻率升序、同頻數值降序排列的新陣列。</returns>
        public static int[] FrequencySort2(int[] nums)
        {
            const int minimumValue = -100;
            const int maximumValue = 100;
            const int valueOffset = 100;
            const int valueRange = 201;
            int[] frequencies = new int[valueRange];

            foreach (int num in nums)
            {
                frequencies[num + valueOffset]++;
            }

            List<int>?[] valuesByFrequency = new List<int>?[nums.Length + 1];
            for (int value = maximumValue; value >= minimumValue; value--)
            {
                int frequency = frequencies[value + valueOffset];
                if (frequency == 0)
                {
                    continue;
                }

                // 反向掃描值域，使每個頻率桶中的不同數值自然保持降序。
                valuesByFrequency[frequency] ??= [];
                valuesByFrequency[frequency]!.Add(value);
            }

            int[] sortedNumbers = new int[nums.Length];
            int writeIndex = 0;
            for (int frequency = 1; frequency < valuesByFrequency.Length; frequency++)
            {
                if (valuesByFrequency[frequency] is not List<int> values)
                {
                    continue;
                }

                foreach (int value in values)
                {
                    // 頻率桶由小到大處理，每個不同數值依其頻率重複寫入結果。
                    for (int count = 0; count < frequency; count++)
                    {
                        sortedNumbers[writeIndex++] = value;
                    }
                }
            }

            return sortedNumbers;
        }
    }
}