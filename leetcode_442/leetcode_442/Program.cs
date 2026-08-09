namespace leetcode_442
{
    internal class Program
    {
        /// <summary>
        /// 442. Find All Duplicates in an Array
        /// https://leetcode.com/problems/find-all-duplicates-in-an-array/description/
        /// <para>
        /// Given an integer array nums of length n where every integer is in [1, n] and each integer appears at most twice, return an array of all integers that appear twice.
        ///
        /// You must write an algorithm that runs in O(n) time and uses only constant auxiliary space, excluding the space needed to store the output.
        ///
        /// Example 1:
        /// Input: nums = [4,3,2,7,8,2,3,1]
        /// Output: [2,3]
        ///
        /// Example 2:
        /// Input: nums = [1,1,2]
        /// Output: [1]
        ///
        /// Example 3:
        /// Input: nums = [1]
        /// Output: []
        ///
        /// Constraints:
        /// - n == nums.length
        /// - 1 &lt;= n &lt;= 10^5
        /// - 1 &lt;= nums[i] &lt;= n
        /// - Each element in nums appears once or twice.
        /// </para>
        /// <para>
        /// 442. 陣列中重複的資料
        /// https://leetcode.cn/problems/find-all-duplicates-in-an-array/description/
        ///
        /// 給定長度為 n 的整數陣列 nums，其中每個整數都在 [1, n] 範圍內，且每個整數最多出現兩次；回傳所有出現兩次之整數所組成的陣列。
        ///
        /// 你必須撰寫時間複雜度為 O(n)、且只使用常數輔助空間的演算法；儲存輸出所需的空間不計。
        ///
        /// 範例 1：
        /// 輸入：nums = [4,3,2,7,8,2,3,1]
        /// 輸出：[2,3]
        ///
        /// 範例 2：
        /// 輸入：nums = [1,1,2]
        /// 輸出：[1]
        ///
        /// 範例 3：
        /// 輸入：nums = [1]
        /// 輸出：[]
        ///
        /// 限制條件：
        /// - n == nums.length
        /// - 1 &lt;= n &lt;= 10^5
        /// - 1 &lt;= nums[i] &lt;= n
        /// - nums 中的每個元素出現一次或兩次。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] maximumLengthInput = Enumerable.Range(1, 99_999).Append(99_999).ToArray();
            TestCase[] testCases =
            [
                new("Official example 1", "[4, 3, 2, 7, 8, 2, 3, 1]", [4, 3, 2, 7, 8, 2, 3, 1], [2, 3]),
                new("Official example 2", "[1, 1, 2]", [1, 1, 2], [1]),
                new("Minimum length", "[1]", [1], []),
                new("No duplicates in reverse order", "[5, 4, 3, 2, 1]", [5, 4, 3, 2, 1], []),
                new("Every present value is paired", "[1, 1, 2, 2]", [1, 1, 2, 2], [1, 2]),
                new("Duplicate values at both bounds", "[1, 6, 3, 4, 6, 1]", [1, 6, 3, 4, 6, 1], [1, 6]),
                new("Separated duplicate pairs", "[2, 4, 1, 2, 3, 4]", [2, 4, 1, 2, 3, 4], [2, 4]),
                new(
                    "Maximum length",
                    "[length 100000; values 1..99999 followed by 99999]",
                    maximumLengthInput,
                    [99_999])
            ];

            int passed = 0;
            foreach (TestCase testCase in testCases)
            {
                int[] sortingInput = [.. testCase.Input];
                int[] markingInput = [.. testCase.Input];

                IList<int> sortingActual = FindDuplicates(sortingInput);
                IList<int> markingActual = FindDuplicates2(markingInput);
                bool isPassed = HasSameElements(sortingActual, testCase.Expected)
                    && HasSameElements(markingActual, testCase.Expected);

                if (isPassed)
                {
                    passed++;
                }

                Console.WriteLine($"Case: {testCase.Name}");
                Console.WriteLine($"Input: {testCase.InputDisplay}");
                Console.WriteLine($"Expected: {FormatNumbers(testCase.Expected)}");
                Console.WriteLine($"FindDuplicates: {FormatNumbers(sortingActual)}");
                Console.WriteLine($"FindDuplicates2: {FormatNumbers(markingActual)}");
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
        /// 比較實際結果與預期結果是否包含相同的整數及相同的出現次數。
        /// 比較前只排序各自建立的序列，不要求演算法以固定順序回傳重複值。
        /// </summary>
        /// <param name="actual">演算法實際回傳的非 <see langword="null"/> 整數集合。</param>
        /// <param name="expected">測試案例預期的非 <see langword="null"/> 整數陣列。</param>
        /// <returns>兩者代表相同多重集合時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool HasSameElements(IList<int> actual, int[] expected)
        {
            return actual.Count == expected.Length
                && actual.OrderBy(number => number).SequenceEqual(expected.OrderBy(number => number));
        }

        /// <summary>
        /// 將整數序列格式化為穩定且易讀的文字。
        /// 顯示前會由小到大排序，讓不保證回傳順序的解法仍能產生可重複比對的輸出。
        /// </summary>
        /// <param name="numbers">要顯示的非 <see langword="null"/> 整數序列。</param>
        /// <returns>以中括號包住的逗號分隔字串；空序列回傳 <c>[]</c>。</returns>
        private static string FormatNumbers(IEnumerable<int> numbers)
        {
            return $"[{string.Join(", ", numbers.OrderBy(number => number))}]";
        }

        /// <summary>
        /// 使用排序與相鄰比較找出陣列中所有出現兩次的整數。
        /// 方法先原地排序，再逐一比較相鄰元素；相同時即可確定該值出現兩次。
        /// 輸入必須是符合題目限制的非 <see langword="null"/> 陣列：
        /// 長度介於 1 至 100000、每個值介於 1 至陣列長度，且每個值最多出現兩次。
        /// 方法會永久改變輸入順序；時間複雜度為 <c>O(n log n)</c>，
        /// 排序所需輔助空間為 <c>O(log n)</c>，結果空間為 <c>O(k)</c>。
        /// </summary>
        /// <param name="nums">要原地排序並尋找重複值的整數陣列。</param>
        /// <returns>包含所有出現兩次之整數的集合；本解法依數值遞增順序回傳。</returns>
        public static IList<int> FindDuplicates(int[] nums)
        {
            IList<int> duplicates = new List<int>();
            Array.Sort(nums);

            for (int i = 1; i < nums.Length; i++)
            {
                // 排序後相同值必定相鄰，因此只需和前一個位置比較。
                if (nums[i - 1] == nums[i])
                {
                    duplicates.Add(nums[i]);
                }
            }

            return duplicates;
        }

        /// <summary>
        /// 使用正負號標記法找出陣列中所有出現兩次的整數。
        /// 每個值都可映射到索引 <c>value - 1</c>；第一次遇到時將該位置改為負數，
        /// 再次映射到已為負數的位置時，即可確認該值出現兩次。
        /// 輸入必須是符合題目限制的非 <see langword="null"/> 陣列：
        /// 長度介於 1 至 100000、每個值介於 1 至陣列長度，且每個值最多出現兩次。
        /// 方法會永久改變部分輸入元素的正負號；時間複雜度為 <c>O(n)</c>，
        /// 結果之外的輔助空間為 <c>O(1)</c>，結果空間為 <c>O(k)</c>。
        /// </summary>
        /// <param name="nums">要原地標記並尋找重複值的整數陣列。</param>
        /// <returns>包含所有出現兩次之整數的集合；回傳順序依第二次遇到各值的順序而定。</returns>
        public static IList<int> FindDuplicates2(int[] nums)
        {
            IList<int> duplicates = new List<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                // 即使目前元素先前已被改成負數，絕對值仍能還原原始數值並映射到索引。
                int mappedIndex = Math.Abs(nums[i]) - 1;

                if (nums[mappedIndex] < 0)
                {
                    // 映射位置已為負數，表示這是題目限制下的第二次出現。
                    duplicates.Add(mappedIndex + 1);
                }
                else
                {
                    // 第一次看到此數值時，以負號在原陣列中留下已出現標記。
                    nums[mappedIndex] = -nums[mappedIndex];
                }
            }

            return duplicates;
        }

        /// <summary>
        /// 定義一組可重複執行的重複值驗證資料。
        /// </summary>
        /// <param name="Name">案例名稱。</param>
        /// <param name="InputDisplay">輸入陣列的穩定顯示文字。</param>
        /// <param name="Input">符合題目限制的輸入陣列。</param>
        /// <param name="Expected">預期出現兩次的整數集合。</param>
        private sealed record TestCase(
            string Name,
            string InputDisplay,
            int[] Input,
            int[] Expected);
    }
}