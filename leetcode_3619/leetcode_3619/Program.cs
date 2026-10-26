namespace leetcode_3619;

/// <summary>
/// 3619. Count Islands With Total Value Divisible by K
/// https://leetcode.com/problems/count-islands-with-total-value-divisible-by-k/description/
/// <para>
/// You are given an <c>m x n</c> matrix <c>grid</c> and a positive integer <c>k</c>. An island is a group of positive integers (representing land) that are 4-directionally connected (horizontally or vertically).
///
/// The total value of an island is the sum of the values of all cells in the island.
///
/// Return the number of islands with a total value divisible by <c>k</c>.
///
/// Example 1:
/// Input: grid = [[0,2,1,0,0],[0,5,0,0,5],[0,0,1,0,0],[0,1,4,7,0],[0,2,0,0,8]], k = 5
/// Output: 2
/// Explanation: The grid contains four islands. The islands highlighted in blue have a total value that is divisible by 5, while the islands highlighted in red do not.
///
/// Example 2:
/// Input: grid = [[3,0,3,0], [0,3,0,3], [3,0,3,0]], k = 3
/// Output: 6
/// Explanation: The grid contains six islands, each with a total value divisible by 3.
///
/// Constraints:
/// - m == grid.length
/// - n == grid[i].length
/// - 1 &lt;= m, n &lt;= 1000
/// - 1 &lt;= m * n &lt;= 10^5
/// - 0 &lt;= grid[i][j] &lt;= 10^6
/// - 1 &lt;= k &lt;= 10^6
/// </para>
/// <para>
/// 3619. 總價值可以被 K 整除的島嶼數目
/// https://leetcode.cn/problems/count-islands-with-total-value-divisible-by-k/description/
///
/// 給定一個 <c>m x n</c> 矩陣 <c>grid</c> 和一個正整數 <c>k</c>。島嶼是由正整數（代表陸地）組成，且彼此以四方向連通（水平或垂直）的群組。
///
/// 島嶼的總值是島嶼中所有儲存格數值的總和。
///
/// 請回傳總值可被 <c>k</c> 整除的島嶼數量。
///
/// 範例 1：
/// 輸入：grid = [[0,2,1,0,0],[0,5,0,0,5],[0,0,1,0,0],[0,1,4,7,0],[0,2,0,0,8]], k = 5
/// 輸出：2
/// 解釋：網格中共有四座島嶼。以藍色標示的島嶼總值可被 5 整除；以紅色標示的島嶼則不可。
///
/// 範例 2：
/// 輸入：grid = [[3,0,3,0], [0,3,0,3], [3,0,3,0]], k = 3
/// 輸出：6
/// 解釋：網格中共有六座島嶼，每座島嶼的總值都可被 3 整除。
///
/// 限制條件：
/// - m == grid.length
/// - n == grid[i].length
/// - 1 &lt;= m, n &lt;= 1000
/// - 1 &lt;= m * n &lt;= 10^5
/// - 0 &lt;= grid[i][j] &lt;= 10^6
/// - 1 &lt;= k &lt;= 10^6
/// </para>
/// </summary>
class Program
{
    /// <summary>
    /// 執行題目範例與最大尺寸案例，確認島嶼數量符合預期。
    /// <para>輸入：args 為命令列參數，本程式不使用。</para>
    /// <para>輸出：逐筆印出驗證結果；答案不符時擲出例外並以失敗結束。</para>
    /// </summary>
    /// <param name="args">未使用的命令列參數。</param>
    static void Main(string[] args)
    {
        var testCases = new[]
        {
            (
                Name: "範例 1",
                Grid: new int[][]
                {
                    new[] { 0, 2, 1, 0, 0 },
                    new[] { 0, 5, 0, 0, 5 },
                    new[] { 0, 0, 1, 0, 0 },
                    new[] { 0, 1, 4, 7, 0 },
                    new[] { 0, 2, 0, 0, 8 }
                },
                K: 5,
                Expected: 2),
            (
                Name: "範例 2",
                Grid: new int[][]
                {
                    new[] { 3, 0, 3, 0 },
                    new[] { 0, 3, 0, 3 },
                    new[] { 3, 0, 3, 0 }
                },
                K: 3,
                Expected: 6),
            (
                Name: "最大尺寸與總和值",
                Grid: Enumerable.Range(0, 100)
                    .Select(_ => Enumerable.Repeat(1_000_000, 1_000).ToArray())
                    .ToArray(),
                K: 1_000_000,
                Expected: 1)
        };

        foreach (var testCase in testCases)
        {
            int actual = new Program().CountIslands(testCase.Grid, testCase.K);
            if (actual != testCase.Expected)
            {
                throw new InvalidOperationException(
                    $"{testCase.Name} 失敗：預期 {testCase.Expected}，實際 {actual}。");
            }

            Console.WriteLine($"[PASS] {testCase.Name}：預期 {testCase.Expected}，實際 {actual}");
        }
    }

    private readonly int[] dirs = { -1, 0, 1, 0, -1 };

    /// <summary>
    /// 以逐格掃描搭配迭代深度優先搜尋，計算總值可被 k 整除的島嶼數量。
    /// <para>輸入：符合題目限制的非空矩形網格，以及正整數 k；網格中的正數代表陸地。</para>
    /// <para>輸出：島嶼總值可被 k 整除的島嶼數量。走訪時會將已訪問的陸地改為 0。</para>
    /// </summary>
    /// <param name="grid">非空矩形網格；正數為陸地，0 為水，執行後已訪問陸地會被改為 0。</param>
    /// <param name="k">大於或等於 1 的整數。</param>
    /// <returns>島嶼總值可被 <paramref name="k"/> 整除的數量。</returns>
    public int CountIslands(int[][] grid, int k)
    {
        int rowCount = grid.Length;
        int columnCount = grid[0].Length;
        int divisibleIslandCount = 0;

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                if (grid[row][column] <= 0)
                {
                    continue;
                }

                long islandValue = DFS(grid, row, column, rowCount, columnCount);
                // 每個連通區塊只會在首次遇到時走訪一次，完成後再判斷整座島的總值。
                if (islandValue % k == 0)
                {
                    divisibleIslandCount++;
                }
            }
        }

        return divisibleIslandCount;
    }

    /// <summary>
    /// 從指定陸地以迭代 DFS 收集四方向相連的格子，並回傳該島總值。
    /// <para>輸入：尚未訪問的陸地起點、網格與其列數和行數。</para>
    /// <para>輸出：島嶼中所有陸地格數值的總和；走訪過的格子會在推入堆疊時改為 0。</para>
    /// </summary>
    /// <param name="grid">待走訪的網格；已訪問格以 0 標記。</param>
    /// <param name="startRow">未訪問陸地起點的列索引。</param>
    /// <param name="startColumn">未訪問陸地起點的行索引。</param>
    /// <param name="rowCount">網格列數。</param>
    /// <param name="columnCount">網格行數。</param>
    /// <returns>起點所屬島嶼的總值。</returns>
    private long DFS(int[][] grid, int startRow, int startColumn, int rowCount, int columnCount)
    {
        long islandValue = grid[startRow][startColumn];
        grid[startRow][startColumn] = 0;

        var stack = new Stack<(int Row, int Column)>();
        stack.Push((startRow, startColumn));

        while (stack.Count > 0)
        {
            var (row, column) = stack.Pop();
            for (int direction = 0; direction < 4; direction++)
            {
                int neighborRow = row + dirs[direction];
                int neighborColumn = column + dirs[direction + 1];
                if (neighborRow < 0 || neighborRow >= rowCount
                    || neighborColumn < 0 || neighborColumn >= columnCount
                    || grid[neighborRow][neighborColumn] <= 0)
                {
                    continue;
                }

                // 先累加再清除原值；用 long 可容納最多 10^5 格、每格 10^6 的總和。
                islandValue += grid[neighborRow][neighborColumn];
                // 在推入時標記，避免同一格被相鄰的多個格子重複加入堆疊。
                grid[neighborRow][neighborColumn] = 0;
                stack.Push((neighborRow, neighborColumn));
            }
        }

        return islandValue;
    }
}
