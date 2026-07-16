namespace leetcode_1337;

internal static class Program
{
    /// <summary>
    /// 1337. The K Weakest Rows in a Matrix
    /// https://leetcode.com/problems/the-k-weakest-rows-in-a-matrix/
    /// 1337. 矩陣中戰鬥力最弱的 K 行
    /// https://leetcode.cn/problems/the-k-weakest-rows-in-a-matrix/
    /// English: Return the indices of the k rows with the fewest soldiers, breaking ties by smaller row index.
    /// 中文：回傳軍人數最少的 k 個矩陣列索引；軍人數相同時，索引較小者較弱。
    /// </summary>
    /// <param name="args">命令列參數；驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, int[][] Matrix, int K, int[] Expected, string Input)[] testCases =
        {
            (
                "Case 1: Official example 1",
                new[]
                {
                    new[] { 1, 1, 0, 0, 0 },
                    new[] { 1, 1, 1, 1, 0 },
                    new[] { 1, 0, 0, 0, 0 },
                    new[] { 1, 1, 0, 0, 0 },
                    new[] { 1, 1, 1, 1, 1 }
                },
                3,
                new[] { 2, 0, 3 },
                "mat = [[1,1,0,0,0],[1,1,1,1,0],[1,0,0,0,0],[1,1,0,0,0],[1,1,1,1,1]], k = 3"
            ),
            (
                "Case 2: Official example 2",
                new[]
                {
                    new[] { 1, 0, 0, 0 },
                    new[] { 1, 1, 1, 1 },
                    new[] { 1, 0, 0, 0 },
                    new[] { 1, 0, 0, 0 }
                },
                2,
                new[] { 0, 2 },
                "mat = [[1,0,0,0],[1,1,1,1],[1,0,0,0],[1,0,0,0]], k = 2"
            ),
            (
                "Case 3: Minimum 2x2, all civilians",
                new[] { new[] { 0, 0 }, new[] { 0, 0 } },
                1,
                new[] { 0 },
                "mat = [[0,0],[0,0]], k = 1"
            ),
            (
                "Case 4: Minimum 2x2, all soldiers",
                new[] { new[] { 1, 1 }, new[] { 1, 1 } },
                2,
                new[] { 0, 1 },
                "mat = [[1,1],[1,1]], k = 2"
            ),
            (
                "Case 5: Zero, full, and middle strengths",
                new[] { new[] { 0, 0 }, new[] { 1, 1 }, new[] { 1, 0 } },
                3,
                new[] { 0, 2, 1 },
                "mat = [[0,0],[1,1],[1,0]], k = 3"
            ),
            (
                "Case 6: Non-adjacent equal strengths with k = m",
                new[]
                {
                    new[] { 1, 0, 0 },
                    new[] { 1, 1, 0 },
                    new[] { 1, 0, 0 },
                    new[] { 0, 0, 0 }
                },
                4,
                new[] { 3, 0, 2, 1 },
                "mat = [[1,0,0],[1,1,0],[1,0,0],[0,0,0]], k = 4"
            ),
            (
                "Case 7: Rows ordered strongest to weakest",
                new[]
                {
                    new[] { 1, 1, 1, 1 },
                    new[] { 1, 1, 1, 0 },
                    new[] { 1, 1, 0, 0 },
                    new[] { 1, 0, 0, 0 }
                },
                2,
                new[] { 3, 2 },
                "mat = [[1,1,1,1],[1,1,1,0],[1,1,0,0],[1,0,0,0]], k = 2"
            ),
            (
                "Case 8: 100x100 descending soldier counts",
                CreateDescendingMatrix(),
                5,
                new[] { 99, 98, 97, 96, 95 },
                "mat = 100x100 with soldier counts descending from 100 to 1, k = 5"
            )
        };

        int passedCount = 0;
        Console.WriteLine("LeetCode 1337 acceptance harness");

        foreach ((string caseName, int[][] matrix, int k, int[] expected, string input) in testCases)
        {
            int[] actual = KWeakestRows(matrix, k);
            bool passed = expected.SequenceEqual(actual);

            Console.WriteLine();
            Console.WriteLine(caseName);
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Expected: {FormatSequence(expected)}");
            Console.WriteLine($"Actual: {FormatSequence(actual)}");
            Console.WriteLine(passed ? "PASS" : "FAIL");

            if (passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{testCases.Length} checks passed.");

        if (passedCount != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 計算題目保證有效且每列先軍人後平民的矩陣，對每列以二分搜尋取得軍人數，
    /// 再依軍人數與列索引排序，回傳戰鬥力最弱的前 <paramref name="k"/> 個列索引。
    /// </summary>
    /// <param name="mat">題目保證為 2 至 100 列、每列等長且內容先 1 後 0 的二元矩陣。</param>
    /// <param name="k">題目保證介於 1 與矩陣列數之間的回傳數量。</param>
    /// <returns>依戰鬥力由弱至強排列的前 <paramref name="k"/> 個原始列索引。</returns>
    public static int[] KWeakestRows(int[][] mat, int k)
    {
        (int SoldierCount, int RowIndex)[] rowStrengths = new (int SoldierCount, int RowIndex)[mat.Length];

        for (int rowIndex = 0; rowIndex < mat.Length; rowIndex++)
        {
            rowStrengths[rowIndex] = (CountSoldiers(mat[rowIndex]), rowIndex);
        }

        Array.Sort(rowStrengths, (left, right) =>
        {
            int soldierComparison = left.SoldierCount.CompareTo(right.SoldierCount);

            // 軍人數相同時明確比較原始索引，確保索引較小的列先被選出。
            return soldierComparison != 0
                ? soldierComparison
                : left.RowIndex.CompareTo(right.RowIndex);
        });

        return rowStrengths
            .Take(k)
            .Select(rowStrength => rowStrength.RowIndex)
            .ToArray();
    }

    /// <summary>
    /// 在題目保證先 1 後 0 的有效列中，以二分搜尋找出第一個 0 的位置作為軍人數；
    /// 若整列皆為 1，搜尋邊界會停在列長度並回傳 <c>row.Length</c>。
    /// </summary>
    /// <param name="row">題目保證長度介於 2 與 100，且所有 1 都位於 0 之前的矩陣列。</param>
    /// <returns>該列中的軍人數。</returns>
    private static int CountSoldiers(int[] row)
    {
        int left = 0;
        int right = row.Length;

        while (left < right)
        {
            int middle = left + ((right - left) / 2);

            // 候選答案保持在閉區間 [left, right]；left 前皆為 1，right 小於列長度時其後皆為 0。
            // row.Length 是整列皆為 1 時的答案哨兵。
            if (row[middle] == 1)
            {
                left = middle + 1;
            }
            else
            {
                right = middle;
            }
        }

        return left;
    }

    private static int[][] CreateDescendingMatrix()
    {
        int[][] matrix = new int[100][];

        for (int rowIndex = 0; rowIndex < matrix.Length; rowIndex++)
        {
            int soldierCount = matrix.Length - rowIndex;
            matrix[rowIndex] = Enumerable.Range(0, 100)
                .Select(columnIndex => columnIndex < soldierCount ? 1 : 0)
                .ToArray();
        }

        return matrix;
    }

    private static string FormatSequence(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }
}
