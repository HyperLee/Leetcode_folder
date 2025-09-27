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

        Console.WriteLine("=== LeetCode 812: 最大三角形面積 - 雙解法比較 ===");
        Console.WriteLine();

        // 測試案例 1: LeetCode 範例
        int[][] points1 = [[0, 0], [0, 1], [1, 0], [0, 2], [2, 0]];
        double result1_shoelace = program.LargestTriangleArea(points1);
        double result1_cross = program.LargestTriangleAreaCrossProduct(points1);

        Console.WriteLine($"測試案例 1: points = [[0,0],[0,1],[1,0],[0,2],[2,0]]");
        Console.WriteLine($"方法一 (鞋帶公式): {result1_shoelace:F5}");
        Console.WriteLine($"方法二 (向量叉積): {result1_cross:F5}");
        Console.WriteLine($"預期結果: 2.00000");
        Console.WriteLine($"結果一致: {Math.Abs(result1_shoelace - result1_cross) < 1e-10}");
        Console.WriteLine();

        // 測試案例 2: 簡單三角形
        int[][] points2 = [[1, 0], [0, 0], [0, 1]];
        double result2_shoelace = program.LargestTriangleArea(points2);
        double result2_cross = program.LargestTriangleAreaCrossProduct(points2);

        Console.WriteLine($"測試案例 2: points = [[1,0],[0,0],[0,1]]");
        Console.WriteLine($"方法一 (鞋帶公式): {result2_shoelace:F5}");
        Console.WriteLine($"方法二 (向量叉積): {result2_cross:F5}");
        Console.WriteLine($"預期結果: 0.50000");
        Console.WriteLine($"結果一致: {Math.Abs(result2_shoelace - result2_cross) < 1e-10}");
        Console.WriteLine();

        // 測試案例 3: 更複雜的點集
        int[][] points3 = [[0, 0], [1, 1], [2, 0], [1, 2], [3, 3]];
        double result3_shoelace = program.LargestTriangleArea(points3);
        double result3_cross = program.LargestTriangleAreaCrossProduct(points3);

        Console.WriteLine($"測試案例 3: points = [[0,0],[1,1],[2,0],[1,2],[3,3]]");
        Console.WriteLine($"方法一 (鞋帶公式): {result3_shoelace:F5}");
        Console.WriteLine($"方法二 (向量叉積): {result3_cross:F5}");
        Console.WriteLine($"結果一致: {Math.Abs(result3_shoelace - result3_cross) < 1e-10}");
        Console.WriteLine();

        Console.WriteLine("=== 效能與精度測試 ===");
        Console.WriteLine("兩種方法在數學上等價，計算結果完全一致");
        Console.WriteLine("時間複雜度: O(n³) - 遍歷所有三點組合");
        Console.WriteLine("空間複雜度: O(1) - 只使用常數額外空間");

        Console.WriteLine();
        Console.WriteLine("================================");
        Console.WriteLine("向量叉積計算步驟展示:");
        program.DemonstrateCrossProductCalculation(points1);
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

    /// <summary>
    /// 向量叉積法 (Cross Product Method)
    /// 
    /// 原理: 利用向量的叉積來計算平行四邊形面積，然後除以 2 得到三角形面積。
    /// 幾何意義: 兩個向量的叉積表示它們組成的平行四邊形的面積，三角形面積是平行四邊形面積的一半。
    /// 
    /// 公式推導:
    /// 對於三個點 A(x₁,y₁), B(x₂,y₂), C(x₃,y₃)：
    /// 1. 建立兩個向量：
    ///    - 向量 AB = (x₂-x₁, y₂-y₁) 
    ///    - 向量 AC = (x₃-x₁, y₃-y₁)
    /// 2. 計算叉積：
    ///    - AB × AC = (x₂-x₁)(y₃-y₁) - (y₂-y₁)(x₃-x₁)
    /// 3. 三角形面積：
    ///    - 面積 = 0.5 × |AB × AC|
    /// 
    /// 優點:
    /// - 幾何意義直觀，容易理解
    /// - 計算效率高，只需基本運算
    /// - 數值穩定性好
    /// - 適合向量運算思維
    /// </summary>
    /// <param name="points">二維陣列，表示平面上的點座標，points[i] = [xi, yi]</param>
    /// <returns>任意三個不同點所能形成的最大三角形面積</returns>
    public double LargestTriangleAreaCrossProduct(int[][] points)
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
                    int x1 = points[i][0], y1 = points[i][1];  // 點 A
                    int x2 = points[j][0], y2 = points[j][1];  // 點 B  
                    int x3 = points[k][0], y3 = points[k][1];  // 點 C

                    // 建立向量 AB 和 AC
                    int vectorAB_x = x2 - x1;  // AB 向量的 x 分量
                    int vectorAB_y = y2 - y1;  // AB 向量的 y 分量
                    int vectorAC_x = x3 - x1;  // AC 向量的 x 分量
                    int vectorAC_y = y3 - y1;  // AC 向量的 y 分量

                    // 計算向量叉積 AB × AC
                    // 在二維平面上，叉積結果是標量：AB × AC = AB_x * AC_y - AB_y * AC_x
                    double crossProduct = vectorAB_x * vectorAC_y - vectorAB_y * vectorAC_x;

                    // 三角形面積 = 0.5 × |叉積|
                    double area = 0.5 * Math.Abs(crossProduct);

                    // 更新最大面積
                    maxArea = Math.Max(maxArea, area);
                }
            }
        }

        return maxArea;
    }
    
    /// <summary>
    /// 展示向量叉積計算的詳細步驟
    /// 
    /// 範例：計算三角形 A(0,0), B(3,0), C(0,4) 的面積
    /// 
    /// 步驟 1: 建立向量
    /// - 向量 AB = (3-0, 0-0) = (3, 0)
    /// - 向量 AC = (0-0, 4-0) = (0, 4)
    /// 
    /// 步驟 2: 計算叉積（重點在這裡）
    /// AB × AC = |3  0|  = 3×4 - 0×0 = 12 - 0 = 12
    ///           |0  4|
    /// 
    /// 步驟 3: 計算面積
    /// 面積 = 0.5 × |12| = 6
    /// 
    /// 驗證：這是一個直角三角形，底=3，高=4，面積=0.5×3×4=6 ✓
    /// </summary>
    /// <param name="points">測試點陣列</param>
    /// <returns>三角形面積</returns>
    public double DemonstrateCrossProductCalculation(int[][] points)
    {
        // 取三個點：A(0,0), B(3,0), C(0,4)
        int x1 = 0, y1 = 0;  // A
        int x2 = 3, y2 = 0;  // B
        int x3 = 0, y3 = 4;  // C
        
        // 建立向量
        int vectorAB_x = x2 - x1;  // 3 - 0 = 3
        int vectorAB_y = y2 - y1;  // 0 - 0 = 0
        int vectorAC_x = x3 - x1;  // 0 - 0 = 0
        int vectorAC_y = y3 - y1;  // 4 - 0 = 4
        
        Console.WriteLine($"向量 AB = ({vectorAB_x}, {vectorAB_y})");
        Console.WriteLine($"向量 AC = ({vectorAC_x}, {vectorAC_y})");
        
        // 叉積計算（為什麼是減法）
        double crossProduct = vectorAB_x * vectorAC_y - vectorAB_y * vectorAC_x;
        //                   = 3 × 4           - 0 × 0
        //                   = 12             - 0  
        //                   = 12
        
        Console.WriteLine($"叉積 = {vectorAB_x}×{vectorAC_y} - {vectorAB_y}×{vectorAC_x} = {crossProduct}");
        
        double area = 0.5 * Math.Abs(crossProduct);
        Console.WriteLine($"面積 = 0.5 × |{crossProduct}| = {area}");
        
        return area;
    }    
}
