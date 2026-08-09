namespace leetcode_1431
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1431. Kids With the Greatest Number of Candies
        /// https://leetcode.com/problems/kids-with-the-greatest-number-of-candies/description/
        ///
        /// There are n kids with candies. You are given an integer array candies, where each candies[i] represents the number
        /// of candies the i-th kid has, and an integer extraCandies, denoting the number of extra candies that you have.
        ///
        /// Return a boolean array result of length n, where result[i] is true if, after giving the i-th kid all the
        /// extraCandies, they will have the greatest number of candies among all the kids, or false otherwise.
        ///
        /// Note that multiple kids can have the greatest number of candies.
        ///
        /// Example 1:
        /// Input: candies = [2,3,5,1,3], extraCandies = 3
        /// Output: [true,true,true,false,true]
        /// Explanation: If you give all extraCandies to:
        /// - Kid 1, they will have 2 + 3 = 5 candies, which is the greatest among the kids.
        /// - Kid 2, they will have 3 + 3 = 6 candies, which is the greatest among the kids.
        /// - Kid 3, they will have 5 + 3 = 8 candies, which is the greatest among the kids.
        /// - Kid 4, they will have 1 + 3 = 4 candies, which is not the greatest among the kids.
        /// - Kid 5, they will have 3 + 3 = 6 candies, which is the greatest among the kids.
        ///
        /// Example 2:
        /// Input: candies = [4,2,1,1,2], extraCandies = 1
        /// Output: [true,false,false,false,false]
        /// Explanation: There is only 1 extra candy. Kid 1 will always have the greatest number of candies, even if a
        /// different kid is given the extra candy.
        ///
        /// Example 3:
        /// Input: candies = [12,1,12], extraCandies = 10
        /// Output: [true,false,true]
        ///
        /// Constraints:
        /// - n == candies.length
        /// - 2 &lt;= n &lt;= 100
        /// - 1 &lt;= candies[i] &lt;= 100
        /// - 1 &lt;= extraCandies &lt;= 50
        /// </para>
        /// <para>
        /// 1431. 擁有最多糖果的孩子
        /// https://leetcode.cn/problems/kids-with-the-greatest-number-of-candies/description/
        ///
        /// 有 n 個孩子擁有糖果。給定整數陣列 candies，其中 candies[i] 表示第 i 個孩子擁有的糖果數量；另給定
        /// 整數 extraCandies，表示你擁有的額外糖果數量。
        ///
        /// 回傳長度為 n 的布林陣列 result；如果將所有 extraCandies 給第 i 個孩子後，該孩子將擁有所有孩子中
        /// 最多的糖果，則 result[i] 為 true，否則為 false。
        ///
        /// 請注意，可以有多個孩子同時擁有最多的糖果。
        ///
        /// 範例 1：
        /// 輸入：candies = [2,3,5,1,3]，extraCandies = 3
        /// 輸出：[true,true,true,false,true]
        /// 解釋：如果將所有 extraCandies 給：
        /// - 孩子 1，將有 2 + 3 = 5 顆糖果，為所有孩子中的最多數量。
        /// - 孩子 2，將有 3 + 3 = 6 顆糖果，為所有孩子中的最多數量。
        /// - 孩子 3，將有 5 + 3 = 8 顆糖果，為所有孩子中的最多數量。
        /// - 孩子 4，將有 1 + 3 = 4 顆糖果，並非所有孩子中的最多數量。
        /// - 孩子 5，將有 3 + 3 = 6 顆糖果，為所有孩子中的最多數量。
        ///
        /// 範例 2：
        /// 輸入：candies = [4,2,1,1,2]，extraCandies = 1
        /// 輸出：[true,false,false,false,false]
        /// 解釋：只有 1 顆額外糖果。即使將額外糖果給其他孩子，孩子 1 仍一定擁有最多的糖果。
        ///
        /// 範例 3：
        /// 輸入：candies = [12,1,12]，extraCandies = 10
        /// 輸出：[true,false,true]
        ///
        /// 限制條件：
        /// - n == candies.length
        /// - 2 &lt;= n &lt;= 100
        /// - 1 &lt;= candies[i] &lt;= 100
        /// - 1 &lt;= extraCandies &lt;= 50
        /// </para>
        /// </summary>
        /// <remarks>
        /// 程式進入點會以邊界、官方範例與重複值案例驗證三種解法，
        /// 並逐項顯示預期值、實際值、輸入是否保持不變及 PASS/FAIL。
        /// 全部檢查通過時結束碼為 0，否則為 1。
        /// </remarks>
        /// <param name="args">命令列參數；此範例不使用。</param>
        static void Main(string[] args)
        {
            (string Name, int[] Candies, int ExtraCandies, bool[] Expected)[] testCases =
            [
                ("最小邊界", [1, 1], 1, [true, true]),
                ("官方範例一", [2, 3, 5, 1, 3], 3, [true, true, true, false, true]),
                ("官方範例二", [4, 2, 1, 1, 2], 1, [true, false, false, false, false]),
                ("官方範例三", [12, 1, 12], 10, [true, false, true]),
                ("剛好追平最大值", [1, 2], 1, [true, true]),
                ("最大數值邊界", [100, 1, 100], 50, [true, false, true]),
            ];

            (string Name, Func<int[], int, IList<bool>> Solve)[] solutions =
            [
                (nameof(KidsWithCandies), KidsWithCandies),
                (nameof(KidsWithCandies2), KidsWithCandies2),
                (nameof(KidsWithCandies3), KidsWithCandies3),
            ];

            int passedChecks = 0;
            int totalChecks = testCases.Length * solutions.Length;

            Console.WriteLine("LeetCode 1431 - Kids With the Greatest Number of Candies");
            Console.WriteLine();

            foreach ((string name, int[] candies, int extraCandies, bool[] expected) in testCases)
            {
                Console.WriteLine($"案例：{name}");
                Console.WriteLine(
                    $"Input: candies = {FormatNumbers(candies)}, extraCandies = {extraCandies}");

                foreach ((string solutionName, Func<int[], int, IList<bool>> solve) in solutions)
                {
                    int[] input = [.. candies];
                    IList<bool> actual = solve(input, extraCandies);
                    bool inputPreserved = input.SequenceEqual(candies);
                    bool passed = actual.SequenceEqual(expected) && inputPreserved;
                    passedChecks += passed ? 1 : 0;

                    Console.WriteLine(
                        $"{solutionName} | Expected: {FormatBooleans(expected)} | " +
                        $"Actual: {FormatBooleans(actual)} | Input preserved: {inputPreserved} | " +
                        $"{(passed ? "PASS" : "FAIL")}");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// 以兩趟線性掃描先找出原本最多的糖果數，再判斷每位孩子加上額外糖果後能否追平或超越最大值。
        /// 適用於長度 2 到 100、元素介於 1 到 100 的 <paramref name="candies" />，
        /// 以及介於 1 到 50 的 <paramref name="extraCandies" />，並回傳與孩子順序相同的布林結果。
        /// </summary>
        /// <param name="candies">每位孩子原本持有的糖果數；方法不會修改此陣列。</param>
        /// <param name="extraCandies">假設一次全部給同一位孩子的額外糖果數。</param>
        /// <returns>若第 i 位孩子加糖後可擁有最多糖果，第 i 個元素為 <see langword="true" />，否則為 <see langword="false" />。</returns>
        /// <remarks>時間複雜度為 O(n)；不計回傳結果的額外空間複雜度為 O(1)。</remarks>
        public static IList<bool> KidsWithCandies(int[] candies, int extraCandies)
        {
            int maximumCandies = candies[0];

            for (int i = 1; i < candies.Length; i++)
            {
                maximumCandies = Math.Max(maximumCandies, candies[i]);
            }

            IList<bool> result = new List<bool>(candies.Length);
            foreach (int candyCount in candies)
            {
                // 題目允許多人並列最多，因此剛好等於最大值也應為 true。
                result.Add(candyCount + extraCandies >= maximumCandies);
            }

            return result;
        }

        /// <summary>
        /// 先建立每位孩子取得額外糖果後的新陣列，再將新數量與原始最大值比較。
        /// 適用於長度 2 到 100、元素介於 1 到 100 的 <paramref name="candies" />，
        /// 以及介於 1 到 50 的 <paramref name="extraCandies" />，並回傳每位孩子能否成為最多者。
        /// </summary>
        /// <param name="candies">每位孩子原本持有的糖果數；方法不會修改此陣列。</param>
        /// <param name="extraCandies">假設一次全部給同一位孩子的額外糖果數。</param>
        /// <returns>依原順序排列的布林清單，表示每位孩子加糖後是否可擁有最多糖果。</returns>
        /// <remarks>時間複雜度為 O(n)；加糖後陣列使用 O(n) 額外空間。</remarks>
        public static IList<bool> KidsWithCandies2(int[] candies, int extraCandies)
        {
            int maximumCandies = candies.Max();
            int[] candiesWithExtra = new int[candies.Length];

            for (int i = 0; i < candies.Length; i++)
            {
                candiesWithExtra[i] = candies[i] + extraCandies;
            }

            IList<bool> result = new List<bool>(candies.Length);
            foreach (int candyCount in candiesWithExtra)
            {
                result.Add(candyCount >= maximumCandies);
            }

            return result;
        }

        /// <summary>
        /// 對每位候選孩子逐一比較所有孩子，直接確認加上額外糖果後是否不小於任何原始糖果數。
        /// 適用於長度 2 到 100、元素介於 1 到 100 的 <paramref name="candies" />，
        /// 以及介於 1 到 50 的 <paramref name="extraCandies" />，並回傳每位孩子的比較結果。
        /// </summary>
        /// <param name="candies">每位孩子原本持有的糖果數；方法不會修改此陣列。</param>
        /// <param name="extraCandies">假設一次全部給同一位孩子的額外糖果數。</param>
        /// <returns>依原順序排列的布林清單，表示每位孩子加糖後是否可擁有最多糖果。</returns>
        /// <remarks>時間複雜度為 O(n²)；不計回傳結果的額外空間複雜度為 O(1)。</remarks>
        public static IList<bool> KidsWithCandies3(int[] candies, int extraCandies)
        {
            IList<bool> result = new List<bool>(candies.Length);

            foreach (int candidateCandyCount in candies)
            {
                int candidateTotal = candidateCandyCount + extraCandies;
                bool canHaveGreatest = true;

                // 只要仍低於任一孩子的原始數量，就不可能成為最多者，可提前結束比較。
                foreach (int currentCandyCount in candies)
                {
                    if (candidateTotal < currentCandyCount)
                    {
                        canHaveGreatest = false;
                        break;
                    }
                }

                result.Add(canHaveGreatest);
            }

            return result;
        }

        /// <summary>
        /// 將整數序列格式化為 README 與主控台共用的方括號表示法。
        /// </summary>
        /// <param name="values">要依原順序呈現的整數序列。</param>
        /// <returns>以逗號分隔並包在方括號中的文字。</returns>
        private static string FormatNumbers(IEnumerable<int> values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 將布林序列格式化為使用小寫 true／false 的方括號表示法。
        /// </summary>
        /// <param name="values">要依原順序呈現的布林序列。</param>
        /// <returns>以逗號分隔並包在方括號中的文字。</returns>
        private static string FormatBooleans(IEnumerable<bool> values)
        {
            return $"[{string.Join(", ", values.Select(value => value ? "true" : "false"))}]";
        }
    }
}