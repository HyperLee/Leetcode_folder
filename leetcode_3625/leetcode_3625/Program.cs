namespace leetcode_3625;

class Program
{
    /// <summary>
    /// 3625. Count Number of Trapezoids II
    /// https://leetcode.com/problems/count-number-of-trapezoids-ii/description/
    /// <para>
    /// You are given a 2D integer array points where points[i] = [x_i, y_i] represents the coordinates of the i-th point on the Cartesian plane.
    ///
    /// Return the number of unique trapezoids that can be formed by choosing any four distinct points from points.
    ///
    /// A trapezoid is a convex quadrilateral with at least one pair of parallel sides. Two lines are parallel if and only if they have the same slope.
    ///
    /// Example 1:
    /// Input: points = [[-3,2],[3,0],[2,3],[3,2],[2,-3]]
    /// Output: 2
    /// Explanation: Images: https://assets.leetcode.com/uploads/2025/04/29/desmos-graph-4.png and https://assets.leetcode.com/uploads/2025/04/29/desmos-graph-3.png. There are two distinct ways to pick four points that form a trapezoid: [-3,2], [2,3], [3,2], [2,-3] form one trapezoid; [2,3], [3,2], [3,0], [2,-3] form another trapezoid.
    ///
    /// Example 2:
    /// Input: points = [[0,0],[1,0],[0,1],[2,1]]
    /// Output: 1
    /// Explanation: Image: https://assets.leetcode.com/uploads/2025/04/29/desmos-graph-5.png. There is only one trapezoid which can be formed.
    ///
    /// Constraints:
    /// - 4 &lt;= points.length &lt;= 500
    /// - -1000 &lt;= x_i, y_i &lt;= 1000
    /// - All points are pairwise distinct.
    /// </para>
    /// <para>
    /// 3625. 統計梯形的數目 II
    /// https://leetcode.cn/problems/count-number-of-trapezoids-ii/description/
    ///
    /// 給定一個二維整數陣列 points，其中 points[i] = [x_i, y_i] 表示笛卡兒平面上第 i 個點的座標。
    ///
    /// 回傳從 points 中選出任意四個不同的點所能形成之不重複梯形數量。
    ///
    /// 梯形是至少有一組平行邊的凸四邊形。兩條直線若且唯若斜率相同時互相平行。
    ///
    /// 範例 1：
    /// 輸入：points = [[-3,2],[3,0],[2,3],[3,2],[2,-3]]
    /// 輸出：2
    /// 解釋：圖片：https://assets.leetcode.com/uploads/2025/04/29/desmos-graph-4.png 與 https://assets.leetcode.com/uploads/2025/04/29/desmos-graph-3.png。選出四個點形成梯形共有兩種不同方式：[-3,2]、[2,3]、[3,2]、[2,-3] 形成一個梯形；[2,3]、[3,2]、[3,0]、[2,-3] 形成另一個梯形。
    ///
    /// 範例 2：
    /// 輸入：points = [[0,0],[1,0],[0,1],[2,1]]
    /// 輸出：1
    /// 解釋：圖片：https://assets.leetcode.com/uploads/2025/04/29/desmos-graph-5.png。只能形成一個梯形。
    ///
    /// 限制條件：
    /// - 4 &lt;= points.length &lt;= 500
    /// - -1000 &lt;= x_i, y_i &lt;= 1000
    /// - 所有點兩兩不同。
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試範例 1：基本測試
        // 四個點：(0,0), (1,1), (1,0), (2,0)
        // 其中 (0,0), (1,0), (2,0) 三點共線，無法組成梯形
        // 預期結果：0 個梯形
        int[][] testCase1 = [[0, 0], [1, 1], [1, 0], [2, 0]];
        int result1 = program.CountTrapezoids(testCase1);
        Console.WriteLine($"測試範例 1: {result1} 個梯形");

        // 測試範例 2：多個平行線段
        // 六個點形成多組平行線，可組成多個梯形
        // 預期結果：3 個梯形
        int[][] testCase2 = [[0, 0], [2, 0], [0, 1], [2, 1], [1, 2], [3, 2]];
        int result2 = program.CountTrapezoids(testCase2);
        Console.WriteLine($"測試範例 2: {result2} 個梯形");

        // 測試範例 3：矩形
        // 四個點形成矩形，有兩對平行邊
        // 根據演算法定義，會計算出 1 個梯形
        // 預期結果：1 個梯形
        int[][] testCase3 = [[0, 0], [1, 0], [1, 1], [0, 1]];
        int result3 = program.CountTrapezoids(testCase3);
        Console.WriteLine($"測試範例 3: {result3} 個梯形");

        // 測試範例 4：空陣列
        int[][] testCase4 = [];
        int result4 = program.CountTrapezoids(testCase4);
        Console.WriteLine($"測試範例 4: {result4} 個梯形 (空陣列)");

        // 測試範例 5：只有三個點
        int[][] testCase5 = [[0, 0], [1, 0], [0, 1]];
        int result5 = program.CountTrapezoids(testCase5);
        Console.WriteLine($"測試範例 5: {result5} 個梯形 (三角形)");

        Console.WriteLine("\n所有測試完成！");
    }

    /// <summary>
    /// LeetCode 3625: 統計梯形的數目 II (Count Number of Trapezoids II)
    /// 
    /// <para><b>題目說明：</b></para>
    /// <para>給定一組 2D 平面上的點，計算可以形成梯形的四元組數量。</para>
    /// <para>梯形定義：恰好有一對邊平行（斜率相同但不共線），且不是平行四邊形。</para>
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>1. 使用雜湊表記錄相同斜率的線段及其截距 (slopeToIntercept)</para>
    /// <para>2. 使用雜湊表記錄相同中點的線段及其斜率 (midToSlope)</para>
    /// <para>3. 相同斜率但不同截距的線段對可組成梯形</para>
    /// <para>4. 需扣除平行四邊形的數量（相同中點但不同斜率的線段對）</para>
    /// 
    /// <para><b>數學公式：</b></para>
    /// <para>- 斜率 k = (y2 - y1) / (x2 - x1)，垂直線時 k = MOD</para>
    /// <para>- 截距 b = (y1 * dx - x1 * dy) / dx</para>
    /// <para>- 中點編碼 mid = (x1 + x2) * 10000.0 + (y1 + y2)</para>
    /// <para>- 梯形數量 = Σ(相同斜率不同截距的配對) - Σ(相同中點不同斜率的配對)</para>
    /// 
    /// <para><b>時間複雜度：</b>O(n²)，其中 n 為點的數量</para>
    /// <para><b>空間複雜度：</b>O(n²)，用於儲存線段資訊</para>
    /// </summary>
    /// <param name="points">2D 整數陣列，表示平面上各點的座標 [x, y]</param>
    /// <returns>可形成梯形的四元組數量</returns>
    /// <example>
    /// <code>
    /// int[][] points = [[0,0], [1,1], [1,0], [2,0]];
    /// int result = CountTrapezoids(points);
    /// result = 1，表示可組成 1 個梯形
    /// </code>
    /// </example>
    public int CountTrapezoids(int[][] points)
    {
        int n = points.Length;

        // 使用大數值表示垂直線的斜率，避免除以零
        double MOD = 1e9 + 7;

        // slopeToIntercept: 相同斜率 → 截距列表（用於找平行線段）
        Dictionary<double, List<double>> slopeToIntercept = new Dictionary<double, List<double>>();

        // midToSlope: 相同中點 → 斜率列表（用於識別平行四邊形）
        Dictionary<double, List<double>> midToSlope = new Dictionary<double, List<double>>();

        int res = 0;

        // 遍歷所有點對，計算每條線段的斜率、截距和中點
        for (int i = 0; i < n; i++)
        {
            int x1 = points[i][0];
            int y1 = points[i][1];

            for (int j = i + 1; j < n; j++)
            {
                int x2 = points[j][0];
                int y2 = points[j][1];
                int dx = x1 - x2;
                int dy = y1 - y2;

                double k, b;

                // 處理垂直線（斜率為無限大）的情況
                if (x2 == x1)
                {
                    k = MOD;     // 使用特殊值表示垂直線
                    b = x1;      // 垂直線的截距用 x 座標表示
                }
                else
                {
                    // 計算斜率 k = (y2 - y1) / (x2 - x1)
                    k = (double)(y2 - y1) / (x2 - x1);

                    // 計算截距 b = y1 - k * x1 = (y1 * dx - x1 * dy) / dx
                    b = (double)(y1 * dx - x1 * dy) / dx;
                }

                // 中點編碼：將 (x1+x2, y1+y2) 編碼為唯一值
                // 乘以 10000 確保不同中點產生不同的編碼
                double mid = (x1 + x2) * 10000.0 + (y1 + y2);

                // 將截距加入對應斜率的列表
                if (!slopeToIntercept.ContainsKey(k))
                {
                    slopeToIntercept[k] = new List<double>();
                }

                // 將斜率加入對應中點的列表
                if (!midToSlope.ContainsKey(mid))
                {
                    midToSlope[mid] = new List<double>();
                }

                slopeToIntercept[k].Add(b);
                midToSlope[mid].Add(k);
            }
        }

        // 統計梯形數量：相同斜率但不同截距的線段對
        foreach (var sti in slopeToIntercept.Values)
        {
            // 至少需要兩條線段才能組成梯形
            if (sti.Count == 1)
            {
                continue;
            }

            // 統計每種截距出現的次數
            Dictionary<double, int> cnt = new Dictionary<double, int>();
            foreach (double bVal in sti)
            {
                cnt[bVal] = cnt.GetValueOrDefault(bVal, 0) + 1;
            }

            // 計算不同截距線段的配對數（累積相乘法）
            int totalSum = 0;
            foreach (int count in cnt.Values)
            {
                res += totalSum * count;
                totalSum += count;
            }
        }

        // 扣除平行四邊形數量：相同中點但不同斜率的線段對
        // 平行四邊形的對角線必定交於同一中點
        foreach (var mts in midToSlope.Values)
        {
            if (mts.Count == 1)
            {
                continue;
            }

            // 統計每種斜率出現的次數
            Dictionary<double, int> cnt = new Dictionary<double, int>();
            foreach (double kVal in mts)
            {
                cnt[kVal] = cnt.GetValueOrDefault(kVal, 0) + 1;
            }

            // 計算不同斜率線段的配對數並從結果中扣除
            int totalSum = 0;
            foreach (int count in cnt.Values)
            {
                res -= totalSum * count;
                totalSum += count;
            }
        }        

        return res;
    }
}
