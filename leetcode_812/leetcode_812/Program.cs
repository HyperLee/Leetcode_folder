namespace leetcode_812;

class Program
{
    /// <summary>
    /// 812. Largest Triangle Area
    /// https://leetcode.com/problems/largest-triangle-area/description/?envType=daily-question&envId=2025-09-27
    /// 812. 最大三角形面積
    /// https://leetcode.cn/problems/largest-triangle-area/description/?envType=daily-question&envId=2025-09-27
    ///
    /// 題目描述（中文翻譯）:
    /// 給定一組位於 X-Y 平面上的點陣列 points，其中 points[i] = [xi, yi]。
    /// 請回傳由任意三個不同點所能形成的最大三角形的面積。
    /// 答案在 10^-5 的誤差範圍內視為正確。
    ///
    /// </summary>
    /// <param name="args">命令列參數（未使用）</param>
    static void Main(string[] args)
    {
        var program = new Program();
        
        // 測試案例 1: LeetCode 範例
        int[][] points1 = [[0, 0], [0, 1], [1, 0], [0, 2], [2, 0]];
        double result1 = program.LargestTriangleArea(points1);
        Console.WriteLine($"測試案例 1: points = [[0,0],[0,1],[1,0],[0,2],[2,0]]");
        Console.WriteLine($"結果: {result1:F5}");
        Console.WriteLine($"預期: 2.00000");
        Console.WriteLine();
        
        // 測試案例 2: 簡單三角形
        int[][] points2 = [[1, 0], [0, 0], [0, 1]];
        double result2 = program.LargestTriangleArea(points2);
        Console.WriteLine($"測試案例 2: points = [[1,0],[0,0],[0,1]]");
        Console.WriteLine($"結果: {result2:F5}");
        Console.WriteLine($"預期: 0.50000");
        Console.WriteLine();
        
        // 測試案例 3: 更複雜的點集
        int[][] points3 = [[0, 0], [1, 1], [2, 0], [1, 2], [3, 3]];
        double result3 = program.LargestTriangleArea(points3);
        Console.WriteLine($"測試案例 3: points = [[0,0],[1,1],[2,0],[1,2],[3,3]]");
        Console.WriteLine($"結果: {result3:F5}");
        Console.WriteLine();
    }

    /// <summary>
    /// 鞋带公式（行列式形式）
    /// 
    /// 原理: 鞋带公式是利用三角形顶点坐标来计算面积的一种方法，它将顶点坐标排列在一个矩阵中，并通过交叉相乘的方式计算面积。 
    /// 公式: 设三角形的三个顶点坐标分别为 A(x₁, y₁)，B(x₂, y₂)，C(x₃, y₃)，
    /// 则面积 S 可以表示为： S = 0.5 * |(x₁y₂ + x₂y₃ + x₃y₁) - (y₁x₂ + y₂x₃ + y₃x₁)|
    /// 
    /// 具体计算步骤（鞋带公式）
    /// 1. 将三个顶点的坐标按照顺序写下来，并在后面重复第一个顶点。
    /// (x₁, y₁)
    /// (x₂, y₂)
    /// (x₃, y₃)
    /// (x₁, y₁)
    /// 
    /// 2. 从左上角到右下角进行斜向乘积相加。
    ///  -1. x₁ * y₂
    ///  -2. x₂ * y₃
    ///  -3. x₃ * y₁
    /// 
    /// 3. 从左下角到右上角进行斜向乘积相加。
    ///  -1. y₁ * x₂
    ///  -2. y₂ * x₃
    ///  -3. y₃ * x₁
    /// 
    /// 4. 将两组相加的结果相减，然后取绝对值，最后乘以 0.5。
    ///  - 面积 S = 0.5 * |(x₁y₂ + x₂y₃ + x₃y₁) - (y₁x₂ + y₂x₃ + y₃x₁)|
    /// </summary>
    /// <param name="points">二維陣列，表示平面上的點座標，points[i] = [xi, yi]</param>
    /// <returns>任意三個不同點所能形成的最大三角形面積</returns>
    public double LargestTriangleArea(int[][] points)
    {
        double maxArea = 0.0;
        int n = points.Length;
        
        // 遍歷所有可能的三點組合 (i, j, k)
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                for (int k = j + 1; k < n; k++)
                {
                    // 取得三個點的座標
                    int x1 = points[i][0], y1 = points[i][1];
                    int x2 = points[j][0], y2 = points[j][1];
                    int x3 = points[k][0], y3 = points[k][1];
                    
                    // 使用鞋帶公式計算三角形面積
                    // 面積 S = 0.5 * |(x₁y₂ + x₂y₃ + x₃y₁) - (y₁x₂ + y₂x₃ + y₃x₁)|
                    double area = 0.5 * Math.Abs(
                        (x1 * y2 + x2 * y3 + x3 * y1) - 
                        (y1 * x2 + y2 * x3 + y3 * x1)
                    );
                    
                    // 更新最大面積
                    maxArea = Math.Max(maxArea, area);
                }
            }
        }
        
        return maxArea;
    }
}
