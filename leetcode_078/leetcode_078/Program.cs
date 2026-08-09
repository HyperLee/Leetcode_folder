namespace leetcode_078
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 78. Subsets
        /// https://leetcode.com/problems/subsets/
        ///
        /// Given an integer array nums of unique elements, return all possible subsets (the power set).
        /// The solution set must not contain duplicate subsets. Return the solution in any order.
        ///
        /// Example 1:
        /// Input: nums = [1,2,3]
        /// Output: [[],[1],[2],[1,2],[3],[1,3],[2,3],[1,2,3]]
        ///
        /// Example 2:
        /// Input: nums = [0]
        /// Output: [[],[0]]
        ///
        /// Constraints:
        /// 1 &lt;= nums.length &lt;= 10
        /// -10 &lt;= nums[i] &lt;= 10
        /// All the numbers of nums are unique.
        /// </para>
        /// <para>
        /// 78. 子集
        /// https://leetcode.cn/problems/subsets/
        ///
        /// 給定一個由互不相同元素組成的整數陣列 nums，請回傳所有可能的子集（冪集）。
        /// 解答集合不可包含重複子集。你可以用任意順序回傳解答。
        ///
        /// 範例 1：
        /// 輸入：nums = [1,2,3]
        /// 輸出：[[],[1],[2],[1,2],[3],[1,3],[2,3],[1,2,3]]
        ///
        /// 範例 2：
        /// 輸入：nums = [0]
        /// 輸出：[[],[0]]
        ///
        /// 限制條件：
        /// 1 &lt;= nums.length &lt;= 10
        /// -10 &lt;= nums[i] &lt;= 10
        /// nums 中的所有數字都互不相同。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 主要進入點會執行四組固定案例，以不考慮子集排列順序的方式比較預期與實際結果，
        /// 並輸出每組案例的輸入、預期結果、實際結果及 PASS/FAIL。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用此參數。</param>
        static void Main(string[] args)
        {
            int passed = 0;

            passed += RunExample(
                "Case 1: 兩個元素",
                new[] { 1, 2 },
                new List<IList<int>>
                {
                    new List<int>(),
                    new List<int> { 1 },
                    new List<int> { 2 },
                    new List<int> { 1, 2 }
                }) ? 1 : 0;

            passed += RunExample(
                "Case 2: 三個元素",
                new[] { 1, 2, 3 },
                new List<IList<int>>
                {
                    new List<int>(),
                    new List<int> { 1 },
                    new List<int> { 2 },
                    new List<int> { 1, 2 },
                    new List<int> { 3 },
                    new List<int> { 1, 3 },
                    new List<int> { 2, 3 },
                    new List<int> { 1, 2, 3 }
                }) ? 1 : 0;

            // 題目限制 nums 至少有一個元素；此案例額外確認演算法仍能正確保留空集合。
            passed += RunExample(
                "Case 3: 空陣列（超出官方限制的健全性案例）",
                Array.Empty<int>(),
                new List<IList<int>>
                {
                    new List<int>()
                }) ? 1 : 0;

            passed += RunExample(
                "Case 4: 單一元素",
                new[] { 5 },
                new List<IList<int>>
                {
                    new List<int>(),
                    new List<int> { 5 }
                }) ? 1 : 0;

            Console.WriteLine($"{passed}/4 passed.");
        }

        /// <summary>
        /// 執行一組子集案例，呼叫迭代解法後以語意等價方式比對結果。
        /// 輸入必須包含案例名稱、不含重複值的整數陣列與完整預期子集；
        /// 輸出案例明細，並回傳實際結果是否包含完全相同的子集集合。
        /// </summary>
        /// <param name="caseName">顯示於主控台的案例名稱。</param>
        /// <param name="nums">要產生冪集的整數陣列。</param>
        /// <param name="expected">此輸入對應的完整預期子集集合。</param>
        /// <returns>預期與實際子集集合語意等價時回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        private static bool RunExample(string caseName, int[] nums, IList<IList<int>> expected)
        {
            IList<IList<int>> actual = Subsets2(nums);
            bool passed = AreEquivalent(expected, actual);

            Console.WriteLine(caseName);
            Console.WriteLine($"Input: {FormatValues(nums)}");
            Console.WriteLine($"Expected: {FormatSubsets(expected)}");
            Console.WriteLine($"Actual: {FormatSubsets(actual)}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed;
        }

        /// <summary>
        /// 比較兩個子集集合是否語意等價；先排序每個子集的元素，再排序所有子集的表示字串，
        /// 因此輸入的子集順序與子集內元素順序都不影響結果。
        /// 輸入為兩個完整的子集集合，輸出為是否包含相同數量且內容相同的子集。
        /// </summary>
        /// <param name="expected">預期的子集集合。</param>
        /// <param name="actual">演算法實際產生的子集集合。</param>
        /// <returns>兩個集合語意等價時回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        private static bool AreEquivalent(IList<IList<int>> expected, IList<IList<int>> actual)
        {
            return NormalizeSubsets(expected)
                .SequenceEqual(NormalizeSubsets(actual), StringComparer.Ordinal);
        }

        /// <summary>
        /// 將子集集合轉換為可穩定比較的鍵值序列；每個子集先依數值排序並串接，
        /// 所有鍵值再依序排序。輸入為完整子集集合，輸出保留重複項目的正規化字串序列。
        /// </summary>
        /// <param name="subsets">要正規化的子集集合。</param>
        /// <returns>依固定順序排列、可供等價比較的子集鍵值序列。</returns>
        private static IEnumerable<string> NormalizeSubsets(IList<IList<int>> subsets)
        {
            return subsets
                .Select(subset => string.Join(",", subset.OrderBy(value => value)))
                .OrderBy(key => key, StringComparer.Ordinal);
        }

        /// <summary>
        /// 將整數序列格式化為含中括號的可讀字串。
        /// 輸入可為空序列，輸出範例如 <c>[]</c> 或 <c>[1, 2]</c>。
        /// </summary>
        /// <param name="values">要格式化的整數序列。</param>
        /// <returns>以逗號與空白分隔、外層含中括號的字串。</returns>
        private static string FormatValues(IEnumerable<int> values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 將完整子集集合格式化為單行可讀字串，並使用 <c>[]</c> 明確呈現空集合。
        /// 輸入為演算法或案例定義的子集集合，輸出範例如 <c>[[], [1], [2], [1, 2]]</c>。
        /// </summary>
        /// <param name="subsets">要格式化的子集集合。</param>
        /// <returns>保留目前列舉順序的單行子集集合字串。</returns>
        private static string FormatSubsets(IList<IList<int>> subsets)
        {
            return $"[{string.Join(", ", subsets.Select(FormatValues))}]";
        }

        /// <summary>
        /// 使用迭代擴張法產生整數陣列的所有子集。先放入空集合；每處理一個數字，
        /// 就複製當輪既有的每個子集並加入該數字，使子集數量倍增。
        /// 輸入須符合題目條件：陣列元素互不重複；輸出包含空集合、完整集合及所有組合。
        /// 時間複雜度為 O(n × 2^n)，包含輸出結果的空間複雜度為 O(n × 2^n)。
        /// </summary>
        /// <param name="nums">元素互不重複、要產生冪集的整數陣列。</param>
        /// <returns>包含所有可能子集的集合，共有 2^n 個子集。</returns>
        public static IList<IList<int>> Subsets2(int[] nums)
        {
            // 空集合是任何集合的子集，也是逐輪擴張的起點。
            IList<IList<int>> result = new List<IList<int>>
            {
                new List<int>()
            };

            foreach (int num in nums)
            {
                // 固定當輪開始前的數量，避免本輪新增的子集再次被同一個 num 擴張。
                int size = result.Count;
                for (int i = 0; i < size; i++)
                {
                    IList<int> expandedSubset = new List<int>(result[i])
                    {
                        num
                    };
                    result.Add(expandedSubset);
                }
            }

            return result;
        }
    }
}