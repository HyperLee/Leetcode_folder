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
        Console.WriteLine($"預期結果: 1.00000\n");

        // 測試案例 2
        int[][] squares2 = new int[][] {
            new int[] { 0, 0, 1 },
            new int[] { 2, 2, 1 }
        };
        double result2 = program.SeparateSquares(squares2);
        Console.WriteLine($"測試案例 2: {result2}");
        Console.WriteLine($"預期結果: 1.00000\n");

        // 測試案例 3
        int[][] squares3 = new int[][] {
            new int[] { 0, 0, 2 },
            new int[] { 1, 1, 1 }
        };
        double result3 = program.SeparateSquares(squares3);
        Console.WriteLine($"測試案例 3: {result3}");
        Console.WriteLine($"預期結果: 1.16667\n");

        // ========== 方法二：掃描線解法測試 ==========
        Console.WriteLine("========== 掃描線解法 ==========");

        // 測試案例 1
        double result1_v2 = program.SeparateSquares2(squares1);
        Console.WriteLine($"測試案例 1: {result1_v2}");
        Console.WriteLine($"預期結果: 1.00000\n");

        // 測試案例 2
        double result2_v2 = program.SeparateSquares2(squares2);
        Console.WriteLine($"測試案例 2: {result2_v2}");
        Console.WriteLine($"預期結果: 1.00000\n");

        // 測試案例 3
        double result3_v2 = program.SeparateSquares2(squares3);
        Console.WriteLine($"測試案例 3: {result3_v2}");
        Console.WriteLine($"預期結果: 1.16667\n");
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

                if(area >= total_area / 2.0)
                {
                    // 提前返回，避免不必要的計算
                    return true;
                }
            }
        }
        
        // 檢查累計面積是否達到總面積的一半
        return area >= total_area / 2.0;
    }

    /// <summary>
    /// 方法二：掃描線
    /// 
    /// 思路與演算法：
    /// 我們可以參考「掃描線」的解法。首先計算出所有正方形的總面積 totalArea，
    /// 接著從下往上進行掃描，設掃描線 y = y' 下方的覆蓋面積和為 area，
    /// 那麼掃描線上方的面積和為 totalArea - area。
    /// 
    /// 題目要求 y = y' 下面的面積與上方的面積相等，即：
    /// area = totalArea - area
    /// 即：area = totalArea / 2
    /// 
    /// 設當前經過正方形上/下邊界的掃描線為 y = y'，此時掃描線以下的覆蓋面積為 area；
    /// 向上移動時下一個需要經過的正方形上/下邊界的掃描線為 y = y''，
    /// 此時被正方形覆蓋的底邊長之和為 width，則此時在掃描線 y = y'' 以下覆蓋的面積之和為：
    /// area + width × (y'' - y')
    /// 
    /// 當滿足：
    /// area < totalArea / 2
    /// area + width × (y'' - y') >= totalArea / 2
    /// 
    /// 時，則可以知道目標值 y 一定處於區間 [y', y'']。
    /// 由於兩個掃描線之間的被覆蓋區域中所有矩形的高度相同，
    /// 掃描線在區間 [y', y''] 移動長度為 Δ 時，被覆蓋區域的面積變化即為 Δ × width。
    /// 此時被覆蓋的面積只需增加 totalArea / 2 - area，即可滿足上下面積相等。
    /// 我們可以直接求出目標值 y 為：
    /// y = y' + (totalArea / 2 - area) / width = y' + (totalArea - 2 × area) / (2 × width)
    /// 
    /// 時間複雜度：O(n log n)，其中 n 為正方形數量（排序的時間複雜度）
    /// 空間複雜度：O(n)，用於儲存事件列表
    /// 
    /// ref: [掃描線解法] - https://oi-wiki.org/geometry/scanning/
    /// </summary>
    /// <param name="squares">2D 整數陣列，每個元素 [xi, yi, li] 表示一個正方形</param>
    /// <returns>水平分割線的 y 座標值</returns>
    /// <example>
    /// <code>
    /// int[][] squares = new int[][] { new int[] { 0, 0, 2 }, new int[] { 2, 0, 2 } };
    /// double result = SeparateSquares2(squares); // 返回 1.0
    /// </code>
    /// </example>
    public double SeparateSquares2(int[][] squares)
    {
        // 計算所有正方形的總面積
        long totalArea = 0;
        // 事件列表：每個事件為 [y座標, 邊長, delta]
        // delta = 1 表示正方形下邊界（開始事件）
        // delta = -1 表示正方形上邊界（結束事件）
        List<int[]> events = [];

        // 遍歷所有正方形，建立事件列表
        foreach (var sq in squares)
        {
            int y = sq[1];  // 正方形左下角 y 座標
            int l = sq[2];  // 正方形邊長
            totalArea += (long)l * l;  // 累加面積
            // 下邊界事件：進入正方形區域，增加覆蓋寬度
            events.Add([y, l, 1]);
            // 上邊界事件：離開正方形區域，減少覆蓋寬度
            events.Add([y + l, l, -1]);
        }

        // 按 y 座標由小到大排序事件
        events.Sort((a, b) => a[0].CompareTo(b[0]));

        double coveredWidth = 0;  // 當前掃描線下所有正方形底邊長度之和
        double currArea = 0;      // 當前累計面積（掃描線以下的面積）
        double prevHeight = 0;    // 前一個掃描線的 y 座標

        // 從下往上掃描每個事件
        foreach (var eventItem in events)
        {
            int y = eventItem[0];      // 當前事件的 y 座標
            int l = eventItem[1];      // 正方形邊長
            int delta = eventItem[2];  // 事件類型（+1 開始，-1 結束）

            // 計算從前一個掃描線到當前掃描線的高度差
            double diff = y - prevHeight;
            // 兩條掃描線之間新增的面積 = 覆蓋寬度 × 高度差
            double area = coveredWidth * diff;

            // 檢查是否達到總面積的一半
            // 如果加上這部分面積後 >= 總面積的一半，表示目標 y 在 [prevHeight, y] 區間內
            if (2.0 * (currArea + area) >= totalArea)
            {
                // 直接計算精確的 y 值
                // y = prevHeight + (totalArea / 2 - currArea) / coveredWidth
                //   = prevHeight + (totalArea - 2 * currArea) / (2 * coveredWidth)
                return prevHeight + (totalArea - 2.0 * currArea) / (2.0 * coveredWidth);
            }

            // 更新覆蓋寬度：開始事件加上邊長，結束事件減去邊長
            coveredWidth += delta * l;
            // 累加當前區間的面積
            currArea += area;
            // 更新前一個掃描線的位置
            prevHeight = y;
        }

        // 理論上不會執行到這裡，除非輸入為空
        return 0.0;
    }
}
