namespace leetcode_349
{
    internal class Program
    {
        /// <summary>
        /// 349. Intersection of Two Arrays
        /// https://leetcode.com/problems/intersection-of-two-arrays/description/
        /// <para>
        /// Given integer arrays nums1 and nums2, return an array of their intersection. Every result element must be unique, and the result may be returned in any order.
        ///
        /// Example 1:
        /// Input: nums1 = [1,2,2,1], nums2 = [2,2]
        /// Output: [2]
        ///
        /// Example 2:
        /// Input: nums1 = [4,9,5], nums2 = [9,4,9,8,4]
        /// Output: [9,4]
        /// Explanation: [4,9] is also accepted.
        ///
        /// Constraints:
        /// - 1 &lt;= nums1.length, nums2.length &lt;= 1000
        /// - 0 &lt;= nums1[i], nums2[i] &lt;= 1000
        /// </para>
        /// <para>
        /// 349. 兩個陣列的交集
        /// https://leetcode.cn/problems/intersection-of-two-arrays/description/
        ///
        /// 給定整數陣列 nums1 與 nums2，回傳兩者的交集陣列。結果中的每個元素必須唯一，且可用任意順序回傳。
        ///
        /// 範例 1：
        /// 輸入：nums1 = [1,2,2,1], nums2 = [2,2]
        /// 輸出：[2]
        ///
        /// 範例 2：
        /// 輸入：nums1 = [4,9,5], nums2 = [9,4,9,8,4]
        /// 輸出：[9,4]
        /// 解釋：[4,9] 也會被接受。
        ///
        /// 限制條件：
        /// - 1 &lt;= nums1.length, nums2.length &lt;= 1000
        /// - 0 &lt;= nums1[i], nums2[i] &lt;= 1000
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] maximumLengthNums1 = Enumerable.Repeat(1000, 1000).ToArray();
            int[] maximumLengthNums2 = Enumerable.Repeat(0, 999).Append(1000).ToArray();
            TestCase[] testCases =
            [
                new("Official example 1", "[1, 2, 2, 1]", "[2, 2]", [1, 2, 2, 1], [2, 2], [2]),
                new("Official example 2", "[4, 9, 5]", "[9, 4, 9, 8, 4]", [4, 9, 5], [9, 4, 9, 8, 4], [4, 9]),
                new("No intersection", "[1, 2, 3]", "[4, 5, 6]", [1, 2, 3], [4, 5, 6], []),
                new("Duplicates in both arrays", "[1, 1, 2, 2]", "[2, 2, 2]", [1, 1, 2, 2], [2, 2, 2], [2]),
                new("Minimum lengths and value", "[0]", "[0]", [0], [0], [0]),
                new("Complete intersection in different order", "[0, 500, 1000]", "[1000, 500, 0]", [0, 500, 1000], [1000, 500, 0], [0, 500, 1000]),
                new("Partial intersection", "[1, 2, 3, 4]", "[2, 4, 6, 8]", [1, 2, 3, 4], [2, 4, 6, 8], [2, 4]),
                new(
                    "Maximum lengths and value",
                    "[length 1000; all values are 1000]",
                    "[length 1000; 999 zeros followed by 1000]",
                    maximumLengthNums1,
                    maximumLengthNums2,
                    [1000])
            ];

            int passed = 0;
            foreach (TestCase testCase in testCases)
            {
                int[] intersectionNums1 = [.. testCase.Nums1];
                int[] intersectionNums2 = [.. testCase.Nums2];
                int[] intersection2Nums1 = [.. testCase.Nums1];
                int[] intersection2Nums2 = [.. testCase.Nums2];

                int[] intersectionActual = Intersection(intersectionNums1, intersectionNums2);
                int[] intersection2Actual = Intersection2(intersection2Nums1, intersection2Nums2);
                bool inputsPreserved = intersectionNums1.SequenceEqual(testCase.Nums1)
                    && intersectionNums2.SequenceEqual(testCase.Nums2)
                    && intersection2Nums1.SequenceEqual(testCase.Nums1)
                    && intersection2Nums2.SequenceEqual(testCase.Nums2);
                bool isPassed = IsValidIntersection(intersectionActual, testCase.Expected)
                    && IsValidIntersection(intersection2Actual, testCase.Expected)
                    && inputsPreserved;
                if (isPassed)
                {
                    passed++;
                }

                Console.WriteLine($"Case: {testCase.Name}");
                Console.WriteLine($"Nums1: {testCase.Nums1Display}");
                Console.WriteLine($"Nums2: {testCase.Nums2Display}");
                Console.WriteLine($"Expected: {FormatArray(testCase.Expected)}");
                Console.WriteLine($"Intersection: {FormatArray(intersectionActual)}");
                Console.WriteLine($"Intersection2: {FormatArray(intersection2Actual)}");
                Console.WriteLine($"Inputs preserved: {inputsPreserved}");
                Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
            if (passed != testCases.Length)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 驗證演算法結果是否與預期集合相等，且實際輸出中的每個元素皆唯一。
        /// </summary>
        /// <param name="actual">演算法回傳的交集陣列。</param>
        /// <param name="expected">測試案例定義的預期唯一交集。</param>
        /// <returns>實際結果集合正確且沒有重複元素時回傳 <see langword="true"/>。</returns>
        private static bool IsValidIntersection(int[] actual, int[] expected)
        {
            HashSet<int> actualValues = new HashSet<int>(actual);
            return actualValues.Count == actual.Length && actualValues.SetEquals(expected);
        }

        /// <summary>
        /// 將整數陣列的排序副本格式化為穩定且易讀的文字，不修改原始陣列。
        /// </summary>
        /// <param name="numbers">要顯示的非 null 整數陣列。</param>
        /// <returns>以中括號包住、由小到大排列的逗號分隔字串。</returns>
        private static string FormatArray(int[] numbers)
        {
            int[] sortedNumbers = [.. numbers];
            Array.Sort(sortedNumbers);
            return $"[{string.Join(", ", sortedNumbers)}]";
        }

        /// <summary>
        /// 計算兩個整數陣列的唯一交集。先以 Dictionary 建立 <paramref name="nums1"/> 的
        /// 雜湊查找表，再掃描 <paramref name="nums2"/>，將共同元素加入 HashSet 以自動去重。
        /// 適用於題目定義的有效輸入，不修改任一輸入陣列；回傳元素的順序不保證。
        /// 平均時間複雜度為 O(n + m)，輔助空間為 O(u1 + k)，結果空間為 O(k)。
        /// </summary>
        /// <param name="nums1">長度介於 1 至 1000，且元素介於 0 至 1000 的整數陣列。</param>
        /// <param name="nums2">長度介於 1 至 1000，且元素介於 0 至 1000 的整數陣列。</param>
        /// <returns>兩個輸入陣列共有且不重複的元素陣列，元素順序不保證。</returns>
        public static int[] Intersection(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> valuesInNums1 = new Dictionary<int, int>();
            HashSet<int> intersection = new HashSet<int>();

            // 查找表只需記錄元素是否出現在 nums1；重複值不必重複加入。
            foreach (int number in nums1)
            {
                if (!valuesInNums1.ContainsKey(number))
                {
                    valuesInNums1.Add(number, 1);
                }
            }

            foreach (int number in nums2)
            {
                if (valuesInNums1.ContainsKey(number))
                {
                    // 結果集合保證同一個共同元素只會出現一次。
                    intersection.Add(number);
                }
            }

            return intersection.ToArray();
        }

        /// <summary>
        /// 計算兩個整數陣列的唯一交集。先由 <paramref name="nums1"/> 建立 HashSet，
        /// 再以 <see cref="HashSet{T}.IntersectWith(IEnumerable{T})"/> 原地移除未出現在
        /// <paramref name="nums2"/> 的元素。只修改方法內建立的集合，不修改任一輸入陣列；
        /// 回傳元素的順序不保證。平均時間複雜度為 O(n + m)，輔助空間為 O(u1)，
        /// 結果空間為 O(k)。
        /// </summary>
        /// <param name="nums1">長度介於 1 至 1000，且元素介於 0 至 1000 的整數陣列。</param>
        /// <param name="nums2">長度介於 1 至 1000，且元素介於 0 至 1000 的整數陣列。</param>
        /// <returns>兩個輸入陣列共有且不重複的元素陣列，元素順序不保證。</returns>
        public static int[] Intersection2(int[] nums1, int[] nums2)
        {
            HashSet<int> intersection = new HashSet<int>(nums1);

            // 集合交集會保留同時存在於 nums2 的值，集合性質也自然滿足唯一輸出要求。
            intersection.IntersectWith(nums2);

            return intersection.ToArray();
        }

        private sealed record TestCase(
            string Name,
            string Nums1Display,
            string Nums2Display,
            int[] Nums1,
            int[] Nums2,
            int[] Expected);
    }
}