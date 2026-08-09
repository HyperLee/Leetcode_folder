namespace leetcode_769
{
    internal class Program
    {
        /// <summary>
        /// 769. Max Chunks To Make Sorted
        /// https://leetcode.com/problems/max-chunks-to-make-sorted/description/
        /// <para>
        /// You are given an integer array arr of length n that represents a permutation of the integers in the range [0, n - 1].
        ///
        /// Split arr into some number of chunks (partitions), and sort each chunk individually. After concatenating the chunks, the result should equal the sorted array.
        ///
        /// Return the largest number of chunks that can be made to sort the array.
        ///
        /// Example 1:
        /// Input: arr = [4,3,2,1,0]
        /// Output: 1
        /// Explanation: Splitting into two or more chunks will not produce the required result. For example, splitting into [4,3] and [2,1,0] results in [3,4,0,1,2], which is not sorted.
        ///
        /// Example 2:
        /// Input: arr = [1,0,2,3,4]
        /// Output: 4
        /// Explanation: We can split into two chunks, such as [1,0] and [2,3,4]. However, splitting into [1,0], [2], [3], and [4] gives the largest possible number of chunks.
        ///
        /// Constraints:
        /// - n == arr.length
        /// - 1 &lt;= n &lt;= 10
        /// - 0 &lt;= arr[i] &lt; n
        /// - All elements of arr are unique.
        /// </para>
        /// <para>
        /// 769. 最多能完成排序的區塊
        /// https://leetcode.cn/problems/max-chunks-to-make-sorted/description/
        ///
        /// 給定長度為 n 的整數陣列 arr，它是範圍 [0, n - 1] 中所有整數的一個排列。
        ///
        /// 將 arr 分成若干區塊（分割區），並分別排序每個區塊。串接所有區塊後，結果應等於排序後的陣列。
        ///
        /// 回傳能使陣列完成排序的最大區塊數量。
        ///
        /// 範例 1：
        /// 輸入：arr = [4,3,2,1,0]
        /// 輸出：1
        /// 解釋：分成兩個或更多區塊無法得到所需結果。例如，分成 [4,3] 與 [2,1,0] 後會得到 [3,4,0,1,2]，並未排序完成。
        ///
        /// 範例 2：
        /// 輸入：arr = [1,0,2,3,4]
        /// 輸出：4
        /// 解釋：可以分成 [1,0] 與 [2,3,4] 兩個區塊；然而，分成 [1,0]、[2]、[3]、[4] 才能得到最多的區塊數。
        ///
        /// 限制條件：
        /// - n == arr.length
        /// - 1 &lt;= n &lt;= 10
        /// - 0 &lt;= arr[i] &lt; n
        /// - arr 中所有元素都不相同。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var testCases = new (string Name, int[] Input, int Expected)[]
            {
                ("Official example 1", [4, 3, 2, 1, 0], 1),
                ("Official example 2", [1, 0, 2, 3, 4], 4),
                ("Minimum input", [0], 1),
                ("Already sorted", [0, 1, 2, 3, 4], 5),
                ("Multi-element prefix", [2, 0, 1, 3, 4], 3),
                ("Delayed prefix boundary", [1, 2, 0, 3], 2),
                ("Maximum-length mixed chunks", [0, 2, 1, 4, 3, 5, 7, 6, 9, 8], 6)
            };

            int passedChecks = 0;
            int totalChecks = 0;

            Console.WriteLine("LeetCode 769 acceptance harness");
            Console.WriteLine();

            foreach ((string name, int[] input, int expected) in testCases)
            {
                (int casePassed, int caseTotal) = RunCase(name, input, expected);
                passedChecks += casePassed;
                totalChecks += caseTotal;
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
        }


        /// <summary>
        /// 計算陣列最多能切成幾個獨立排序後仍可組成完整升冪排列的區塊。
        /// 解法由左至右維護前綴最大值；此前綴最大值等於目前索引時，代表前綴恰好包含
        /// <c>0</c> 到目前索引的所有值，因此可以在此結束一個區塊。
        /// 輸入必須是長度 <c>n</c> 且由 <c>0</c> 到 <c>n - 1</c> 組成的有效排列，
        /// 方法不會修改輸入，並回傳可形成的最大區塊數。
        /// </summary>
        /// <param name="arr">由 <c>0</c> 到 <c>arr.Length - 1</c> 組成的排列。</param>
        /// <returns>個別排序後仍能使整體有序的最大區塊數。</returns>
        /// <remarks>時間複雜度為 <c>O(n)</c>，輔助空間與結果空間皆為 <c>O(1)</c>。</remarks>
        public static int MaxChunksToSorted(int[] arr)
        {
            int m = 0;
            int res = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                m = Math.Max(m, arr[i]);
                if (m == i)
                {
                    // 前綴最大值等於右端索引，表示此前綴排序後正好會落在相同索引範圍。
                    res++;
                }
            }

            return res;
        }


        /// <summary>
        /// 計算陣列最多能切成幾個獨立排序後仍可組成完整升冪排列的區塊。
        /// 解法追蹤目前候選區塊的左右邊界與最小、最大值；當區塊的最小值等於左邊界，
        /// 且最大值等於右邊界時，該區塊恰好包含邊界範圍內的所有整數，可以安全切分。
        /// 輸入必須是長度 <c>n</c> 且由 <c>0</c> 到 <c>n - 1</c> 組成的有效排列，
        /// 方法不會修改輸入，並回傳可形成的最大區塊數。
        /// </summary>
        /// <param name="arr">由 <c>0</c> 到 <c>arr.Length - 1</c> 組成的排列。</param>
        /// <returns>個別排序後仍能使整體有序的最大區塊數。</returns>
        /// <remarks>時間複雜度為 <c>O(n)</c>，輔助空間與結果空間皆為 <c>O(1)</c>。</remarks>
        public static int MaxChunksToSorted2(int[] arr)
        {
            int n = arr.Length;
            int res = 0;

            for (int i = 0, j = 0, min = n, max = -1; i < n; i++)
            {
                min = Math.Min(min, arr[i]);
                max = Math.Max(max, arr[i]);

                if (j == min && i == max)
                {
                    // [j, i] 的值域與索引範圍完全一致，因此排序後可獨立歸位。
                    res++;

                    // 從下一個索引開始追蹤新區塊，並還原最小值與最大值的哨兵。
                    j = i + 1;
                    min = n;
                    max = -1;
                }
            }

            return res;
        }

        /// <summary>
        /// 以同一組有效排列分別驗證兩種主要解法，檢查回傳值是否符合預期，
        /// 並確認兩次呼叫都不會修改各自的輸入副本。方法會輸出案例、四項檢查結果，
        /// 並回傳通過項目數與總項目數供主要進入點彙總。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="input">符合題目排列限制的測試資料。</param>
        /// <param name="expected">兩種解法都應回傳的最大區塊數。</param>
        /// <returns>此案例的通過檢查數與總檢查數。</returns>
        private static (int Passed, int Total) RunCase(string name, int[] input, int expected)
        {
            int[] method1Input = [.. input];
            int[] method2Input = [.. input];

            int method1Actual = MaxChunksToSorted(method1Input);
            int method2Actual = MaxChunksToSorted2(method2Input);
            bool method1InputPreserved = method1Input.SequenceEqual(input);
            bool method2InputPreserved = method2Input.SequenceEqual(input);

            var checks = new (string Label, string Expected, string Actual, bool Passed)[]
            {
                ("MaxChunksToSorted result", expected.ToString(), method1Actual.ToString(), method1Actual == expected),
                ("MaxChunksToSorted input preserved", bool.TrueString, method1InputPreserved.ToString(), method1InputPreserved),
                ("MaxChunksToSorted2 result", expected.ToString(), method2Actual.ToString(), method2Actual == expected),
                ("MaxChunksToSorted2 input preserved", bool.TrueString, method2InputPreserved.ToString(), method2InputPreserved)
            };

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Input: [{string.Join(", ", input)}]");

            foreach ((string label, string expectedValue, string actualValue, bool passed) in checks)
            {
                Console.WriteLine(
                    $"{(passed ? "PASS" : "FAIL")} | {label} | Expected: {expectedValue} | Actual: {actualValue}");
            }

            Console.WriteLine();

            return (checks.Count(check => check.Passed), checks.Length);
        }
    }
}