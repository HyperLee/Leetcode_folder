using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

namespace leetcode_1266;

class Program
{
    /// <summary>
    /// <para>
    /// 1266. Minimum Time Visiting All Points
    /// https://leetcode.com/problems/minimum-time-visiting-all-points/description/
    ///
    /// On a 2D plane, there are n points with integer coordinates points[i] = [x_i, y_i]. Return the minimum
    /// time in seconds to visit all the points in the order given by points.
    /// You can move according to these rules:
    /// - In 1 second, you can either:
    ///   - move vertically by one unit,
    ///   - move horizontally by one unit, or
    ///   - move diagonally sqrt(2) units (in other words, move one unit vertically then one unit horizontally
    ///     in 1 second).
    /// - You have to visit the points in the same order as they appear in the array.
    /// - You are allowed to pass through points that appear later in the order, but these do not count as visits.
    ///
    /// Example 1:
    /// Input: points = [[1,1],[3,4],[-1,0]]
    /// Output: 7
    /// Illustration: https://assets.leetcode.com/uploads/2019/11/14/1626_example_1.PNG
    /// Explanation: One optimal path is [1,1] -&gt; [2,2] -&gt; [3,3] -&gt; [3,4] -&gt; [2,3] -&gt;
    /// [1,2] -&gt; [0,1] -&gt; [-1,0].
    /// Time from [1,1] to [3,4] = 3 seconds.
    /// Time from [3,4] to [-1,0] = 4 seconds.
    /// Total time = 7 seconds.
    ///
    /// Example 2:
    /// Input: points = [[3,2],[-2,2]]
    /// Output: 5
    ///
    /// Constraints:
    /// points.length == n
    /// 1 &lt;= n &lt;= 100
    /// points[i].length == 2
    /// -1000 &lt;= points[i][0], points[i][1] &lt;= 1000
    /// </para>
    /// <para>
    /// 1266. 拜訪所有點的最短時間
    /// https://leetcode.cn/problems/minimum-time-visiting-all-points/description/
    ///
    /// 在 2D 平面上有 n 個整數座標點 points[i] = [x_i, y_i]。請回傳依 points 給定順序拜訪
    /// 所有點所需的最少秒數。
    /// 可以依照下列規則移動：
    /// - 在 1 秒內，可以選擇：
    ///   - 垂直移動一個單位，
    ///   - 水平移動一個單位，或
    ///   - 沿對角線移動 sqrt(2) 個單位（也就是在 1 秒內垂直移動一個單位，再水平移動一個單位）。
    /// - 必須依照各點在陣列中出現的順序拜訪它們。
    /// - 可以經過順序中稍後才出現的點，但這不算完成拜訪。
    ///
    /// 範例 1：
    /// 輸入：points = [[1,1],[3,4],[-1,0]]
    /// 輸出：7
    /// 示意圖：https://assets.leetcode.com/uploads/2019/11/14/1626_example_1.PNG
    /// 解釋：一條最佳路徑是 [1,1] -&gt; [2,2] -&gt; [3,3] -&gt; [3,4] -&gt; [2,3] -&gt;
    /// [1,2] -&gt; [0,1] -&gt; [-1,0]。
    /// 從 [1,1] 到 [3,4] 需要 3 秒。
    /// 從 [3,4] 到 [-1,0] 需要 4 秒。
    /// 總時間為 7 秒。
    ///
    /// 範例 2：
    /// 輸入：points = [[3,2],[-2,2]]
    /// 輸出：5
    ///
    /// 限制條件：
    /// points.length == n
    /// 1 &lt;= n &lt;= 100
    /// points[i].length == 2
    /// -1000 &lt;= points[i][0], points[i][1] &lt;= 1000
    /// </para>
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1: [[1,1],[3,4],[-1,0]]
        int[][] points1 = new int[][] 
        {
            new int[] { 1, 1 },
            new int[] { 3, 4 },
            new int[] { -1, 0 }
        };
        Console.WriteLine($"測試案例 1: {solution.MinTimeToVisitAllPoints(points1)}"); // 預期輸出: 7
        
        // 測試案例 2: [[3,2],[-2,2]]
        int[][] points2 = new int[][] 
        {
            new int[] { 3, 2 },
            new int[] { -2, 2 }
        };
        Console.WriteLine($"測試案例 2: {solution.MinTimeToVisitAllPoints(points2)}"); // 預期輸出: 5
    }

    /// <summary>
    /// 使用切比雪夫距離解法計算拜訪所有點的最少時間
    /// 
    /// 解題思路：
    /// 從點 (x1, y1) 移動到點 (x2, y2) 時：
    /// - 水平距離 dx = |x1 - x2|
    /// - 垂直距離 dy = |y1 - y2|
    /// 
    /// 移動策略：
    /// 1. 當 dx 和 dy 都大於 0 時，使用對角線移動最優（一秒同時減少 dx 和 dy）
    /// 2. 若 dx > dy：先對角線移動 dy 秒，再水平移動 (dx - dy) 秒，共 dx 秒
    /// 3. 若 dx ≤ dy：先對角線移動 dx 秒，再垂直移動 (dy - dx) 秒，共 dy 秒
    /// 
    /// 因此，兩點間的最少移動時間 = max(dx, dy) = max(|x1 - x2|, |y1 - y2|)
    /// 這正是兩點的切比雪夫距離（Chebyshev Distance）
    /// 
    /// 時間複雜度：O(n)，其中 n 是點的數量
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="points">二維平面上的點陣列，points[i] = [xi, yi]</param>
    /// <returns>按順序拜訪所有點所需的最少秒數</returns>
    public int MinTimeToVisitAllPoints(int[][] points)
    {
        // 初始化起點座標
        int x0 = points[0][0];
        int y0 = points[0][1];
        int res = 0;
        
        // 遍歷所有點，計算相鄰點對之間的切比雪夫距離
        for (int i = 0; i < points.Length; i++)
        {
            int x1 = points[i][0];
            int y1 = points[i][1];
            
            // 計算水平距離 dx 和垂直距離 dy
            // 兩點間的最少移動時間 = max(|dx|, |dy|)
            // 這是切比雪夫距離的定義
            res += Math.Max(Math.Abs(x1 - x0), Math.Abs(y1 - y0));
            
            // 更新當前位置為下一次迭代的起點
            x0 = x1;
            y0 = y1;
        }
        
        return res;
    }
}
