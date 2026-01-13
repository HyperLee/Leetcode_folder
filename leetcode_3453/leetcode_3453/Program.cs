namespace leetcode_3453;

class Program
{
    /// <summary>
    /// 3453. Separate Squares I
    /// https://leetcode.com/problems/separate-squares-i/description/?envType=daily-question&envId=2026-01-13
    /// 3453. 分割正方形 I
    /// https://leetcode.cn/problems/separate-squares-i/description/?envType=daily-question&envId=2026-01-13
    /// 
    /// 題目（繁體中文）：
    /// 給定一個 2D 整數陣列 `squares`，每個 `squares[i] = [xi, yi, li]` 表示一個與 x 軸平行的正方形，
    /// 其左下角座標為 (xi, yi)，邊長為 li。
    /// 找出一條水平線的最小 y 座標值，使得該線上方的所有正方形面積總和等於該線下方的所有正方形面積總和。
    /// 答案在 1e-5 的誤差內視為正確。
    /// 注意：正方形之間可能會互相重疊，重疊面積應該被多次計算（對每個正方形分別計算面積）。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1
        int[][] squares1 = new int[][] {
            new int[] { 0, 0, 2 },
            new int[] { 2, 0, 2 }
        };
        double result1 = program.SeparateSquares(squares1);
        Console.WriteLine($"測試案例 1: {result1}");
        Console.WriteLine($"預期結果: 2.00000\n");

        // 測試案例 2
        int[][] squares2 = new int[][] {
            new int[] { 0, 0, 3 },
            new int[] { 0, 3, 3 }
        };
        double result2 = program.SeparateSquares(squares2);
        Console.WriteLine($"測試案例 2: {result2}");
        Console.WriteLine($"預期結果: 3.00000\n");

        // 測試案例 3
        int[][] squares3 = new int[][] {
            new int[] { 0, 0, 4 },
            new int[] { 4, 0, 2 }
        };
        double result3 = program.SeparateSquares(squares3);
        Console.WriteLine($"測試案例 3: {result3}");
        Console.WriteLine($"預期結果: 2.82843\n");
    }

    /// <summary>
    /// 方法一：二分查找
    /// 
    /// 思路與演算法：
    /// 設所有正方形的面積之和為 totalArea = Σ(li²)。
    /// 我們需要找到一條水平線 y，使得 y 以下的面積之和等於 y 以上的面積之和。
    /// 即：area_y * 2 = totalArea
    /// 
    /// 隨著分割線 y 的增大，y 以下的面積 area_y 單調不減，因此可以使用二分查找。
    /// 具體地，我們二分查找找到最小的 y 值，使得在 y 以下的正方形面積滿足：
    /// area_y * 2 >= totalArea
    /// 
    /// 時間複雜度：O(n * log(L * 10^5))，其中 n 為正方形數量，L 為最大 y 座標。
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="squares">2D 整數陣列，每個元素 [xi, yi, li] 表示一個正方形</param>
    /// <returns>水平分割線的 y 座標值</returns>
    public double SeparateSquares(int[][] squares)
    {
        // 計算所有正方形的總面積和最大 y 座標
        double max_y = 0;
        double total_area = 0;
        foreach(int[] sq in squares)
        {
            int y = sq[1];  // 正方形左下角 y 座標
            int l = sq[2];  // 正方形邊長
            total_area += (double)l * l;  // 累加面積
            max_y = Math.Max(max_y, (double)(y + l));  // 更新最大 y 座標（正方形頂部）
        }

        // 二分查找：搜尋範圍為 [0, max_y]
        double left = 0;
        double right = max_y;
        double eps = 1e-5;  // 精度要求
        
        // 當搜尋區間小於精度要求時停止
        while(Math.Abs(right - left) > eps)
        {
            double mid = (right + left) / 2.0;  // 取中點
            
            // 檢查 mid 以下的面積是否 >= 總面積的一半
            if(Check(mid, squares, total_area))
            {
                right = mid;  // 面積足夠，嘗試更小的 y 值
            }
            else
            {
                left = mid;   // 面積不足，需要更大的 y 值
            }
        }
        return right;
    }

    /// <summary>
    /// 檢查給定的 y 座標線以下的面積是否 >= 總面積的一半
    /// 
    /// 對於正方形 (xi, yi, li)：
    /// - 如果 yi >= limit_y，該正方形完全在線上方，貢獻面積為 0
    /// - 如果 yi < limit_y，該正方形在線下方的面積為：li * min(limit_y - yi, li)
    ///   * 當 limit_y - yi >= li 時，整個正方形都在線下方，面積為 li²
    ///   * 當 limit_y - yi < li 時，只有部分在線下方，面積為 li * (limit_y - yi)
    /// 
    /// 時間複雜度：O(n)，其中 n 為正方形數量
    /// </summary>
    /// <param name="limit_y">水平分割線的 y 座標</param>
    /// <param name="squares">所有正方形的陣列</param>
    /// <param name="total_area">所有正方形的總面積</param>
    /// <returns>如果 limit_y 以下的面積 >= 總面積的一半，返回 true</returns>
    private bool Check(double limit_y, int[][] squares, double total_area)
    {
        double area = 0;
        foreach(int[] sq in squares)
        {
            int y = sq[1];  // 正方形左下角 y 座標
            int l = sq[2];  // 正方形邊長
            
            // 只計算左下角在 limit_y 以下的正方形
            if(y < limit_y)
            {
                // 計算該正方形在 limit_y 以下的面積
                // Math.Min(limit_y - y, l) 確保不會超過正方形本身的高度
                area += (double)l * Math.Min(limit_y - y, (double)l);
            }
        }
        
        // 檢查累計面積是否達到總面積的一半
        return area >= total_area / 2.0;
    }
}
