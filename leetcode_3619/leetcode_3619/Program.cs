namespace leetcode_3619;

class Program
{
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
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
