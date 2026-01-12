using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

namespace leetcode_1266;

class Program
{
    /// <summary>
    /// 1266. Minimum Time Visiting All Points
    /// https://leetcode.com/problems/minimum-time-visiting-all-points/description/?envType=daily-question&envId=2026-01-12
    /// 1266. 访问所有点的最小时间
    /// https://leetcode.cn/problems/minimum-time-visiting-all-points/description/?envType=daily-question&envId=2026-01-12
    /// 
    /// 繁體中文題目描述：
    /// 在 2D 平面上，有 n 個整數座標的點 points[i] = [xi, yi]。請回傳按給定順序拜訪所有點所需的最少秒數。
    /// 每秒可以進行以下其中一種移動：
    /// - 垂直移動一個單位（上下）
    /// - 水平移動一個單位（左右）
    /// - 對角線移動（同時水平與垂直各移動一個單位，耗時 1 秒）
    /// 必須依照陣列中的順序拜訪點；你可以經過在後方出現的點，但不算作拜訪。
    /// 
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
