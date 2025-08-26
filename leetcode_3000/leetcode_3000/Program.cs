namespace leetcode_3000;

class Program
{
    /// <summary>
    /// 3000. Maximum Area of Longest Diagonal Rectangle
    /// https://leetcode.com/problems/maximum-area-of-longest-diagonal-rectangle/description/?envType=daily-question&envId=2025-08-26
    /// 3000. 對角線最長的長方形的面積
    /// https://leetcode.cn/problems/maximum-area-of-longest-diagonal-rectangle/description/?envType=daily-question&envId=2025-08-26
    ///
    /// Problem (English):
    /// You are given a 2D 0-indexed integer array dimensions.
    /// For all indices i, 0 <= i < dimensions.length, dimensions[i][0] represents the length
    /// and dimensions[i][1] represents the width of the rectangle i.
    /// Return the area of the rectangle having the longest diagonal. If there are multiple
    /// rectangles with the longest diagonal, return the area of the rectangle having the maximum area.
    ///
    /// 題目（中文翻譯）：
    /// 給定一個二維（0 起始索引）整數陣列 dimensions。
    /// 對於所有索引 i（0 <= i < dimensions.length），dimensions[i][0] 代表第 i 個長方形的長度，
    /// dimensions[i][1] 代表第 i 個長方形的寬度。
    /// 回傳具有最長對角線的長方形的面積；若有多個長方形的對角線長度相同且為最長，
    /// 則回傳這些長方形中面積最大的那一個的面積。
    ///
    /// Notes:
    /// - 對角線長度可由勾股定理計算：sqrt(length^2 + width^2)。
    /// - 比較對角線長度時不需要實際開根號，可比較 length^2 + width^2 的值以避免浮點數誤差。
    /// </summary>
    /// <param name="args">執行參數（未使用）</param>
    static void Main(string[] args)
    {
        // 建立 Program 實例以呼叫非靜態的 AreaOfMaxDiagonal
        var solver = new Program();

        // 測資 1：示範題中範例
        int[][] dims1 = new int[][] {
            new[] { 9, 3 },
            new[] { 8, 6 }
        };
        Console.WriteLine("Test 1: [[9,3],[8,6]] -> Expected: 48, Actual: {0}", solver.AreaOfMaxDiagonal(dims1));

        // 測資 2：兩個長方形對角線相同，但面積不同，應取較大面積
        int[][] dims2 = new int[][] {
            new[] { 5, 5 }, // diagonal^2 = 50, area = 25
            new[] { 7, 1 }  // diagonal^2 = 50, area = 7
        };
        Console.WriteLine("Test 2: [[5,5],[7,1]] -> Expected: 25, Actual: {0}", solver.AreaOfMaxDiagonal(dims2));

        // 測資 3：單一長方形
        int[][] dims3 = new int[][] {
            new[] { 4, 7 }
        };
        Console.WriteLine("Test 3: [[4,7]] -> Expected: 28, Actual: {0}", solver.AreaOfMaxDiagonal(dims3));

        // 測資 4：多組，包含重複與不同對角線
        int[][] dims4 = new int[][] {
            new[] { 3, 4 }, // diag^2 = 25, area = 12
            new[] { 6, 8 }, // diag^2 = 100, area = 48
            new[] { 10, 0 } // diag^2 = 100, area = 0
        };
        Console.WriteLine("Test 4: [[3,4],[6,8],[10,0]] -> Expected: 48, Actual: {0}", solver.AreaOfMaxDiagonal(dims4));
    }

    /// <summary>
    /// 解題說明（AreaOfMaxDiagonal）：
    /// - 目標是挑出「對角線最長」的長方形；若對角線同長，回傳其中「面積最大」者的面積。
    /// - 使用勾股定理，對角線長度為 sqrt(L^2 + W^2)。為避免浮點運算與誤差，比較時僅比較 L^2 + W^2 的整數值。
    /// - 逐一遍歷每個長方形，維護兩個狀態：目前最大的對角線平方值 maxDiagonalSquared，以及對應的最大面積 maxArea。
    /// - 若遇到更大的對角線平方值，更新兩者；若遇到同樣的對角線平方值，取面積較大的值。
    ///
    /// 複雜度：
    /// - 時間複雜度 O(n)，n 為長方形數量（每個元素掃描一次）。
    /// - 空間複雜度 O(1)。
    ///
    /// <example>
    /// <code>
    /// 範例：
    /// 輸入：[[9,3],[8,6]]
    /// 對角線平方分別為：9^2+3^2=90，8^2+6^2=100 → 第二個較大
    /// 回傳面積：8*6=48
    /// var ans = AreaOfMaxDiagonal(new int[][] {
    ///     new [] { 9, 3 },
    ///     new [] { 8, 6 }
    /// });
    /// ans == 48
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="dimensions">尺寸陣列，dimensions[i] = [length, width]</param>
    /// <returns>最長對角線對應之長方形的面積；若有多個同最長對角線，取最大面積</returns>
    public int AreaOfMaxDiagonal(int[][] dimensions)
    {
        // 紀錄當前觀察到的最大對角線平方值（比較用，避免使用開根號）
        int maxDiagonalSquared = 0;
        // 與 maxDiagonalSquared 對應之最佳面積（同對角線長度時取較大面積）
        int maxArea = 0;

        foreach (var dimension in dimensions)
        {
            // 長與寬
            int length = dimension[0];
            int width = dimension[1];
            // 對角線平方（length^2 + width^2），僅用於比較大小
            int diagonalSquared = length * length + width * width;
            // 面積
            int area = length * width;

            if (diagonalSquared > maxDiagonalSquared)
            {
                maxDiagonalSquared = diagonalSquared;
                maxArea = area;
            }
            else if (diagonalSquared == maxDiagonalSquared)
            {
                // 對角線同長時，取面積較大者
                maxArea = Math.Max(maxArea, area);
            }
        }

        return maxArea;
    }
}
