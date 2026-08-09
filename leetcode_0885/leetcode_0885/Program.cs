namespace leetcode_0885
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 885. Spiral Matrix III
        /// https://leetcode.com/problems/spiral-matrix-iii/
        ///
        /// You start at the cell (rStart, cStart) of an rows x cols grid facing east. The northwest corner is
        /// at the first row and column in the grid, and the southeast corner is at the last row and column.
        /// You will walk in a clockwise spiral shape to visit every position in this grid. Whenever you move
        /// outside the grid's boundary, we continue our walk outside the grid (but may return to the grid
        /// boundary later). Eventually, we reach all rows * cols spaces of the grid.
        /// Return an array of coordinates representing the positions of the grid in the order you visited them.
        ///
        /// Example 1:
        /// Input: rows = 1, cols = 4, rStart = 0, cStart = 0
        /// Output: [[0,0],[0,1],[0,2],[0,3]]
        ///
        /// Example 2:
        /// Input: rows = 5, cols = 6, rStart = 1, cStart = 4
        /// Output: [[1,4],[1,5],[2,5],[2,4],[2,3],[1,3],[0,3],[0,4],[0,5],[3,5],[3,4],[3,3],
        /// [3,2],[2,2],[1,2],[0,2],[4,5],[4,4],[4,3],[4,2],[4,1],[3,1],[2,1],[1,1],[0,1],
        /// [4,0],[3,0],[2,0],[1,0],[0,0]]
        ///
        /// Constraints:
        /// 1 &lt;= rows, cols &lt;= 100
        /// 0 &lt;= rStart &lt; rows
        /// 0 &lt;= cStart &lt; cols
        /// </para>
        /// <para>
        /// 885. 螺旋矩陣 III
        /// https://leetcode.cn/problems/spiral-matrix-iii/
        ///
        /// 你從 rows x cols 網格中的儲存格 (rStart, cStart) 出發，面向東方。
        /// 網格的西北角位於第一列第一欄，東南角位於最後一列最後一欄。
        /// 你會沿順時針螺旋形狀行走，拜訪此網格中的每個位置。每當移出網格邊界時，
        /// 仍會繼續在網格外行走（之後可能再次回到網格邊界）。最終會到達網格中所有 rows * cols 個位置。
        /// 請依照拜訪順序，回傳一個表示網格位置的座標陣列。
        ///
        /// 範例 1：
        /// 輸入：rows = 1, cols = 4, rStart = 0, cStart = 0
        /// 輸出：[[0,0],[0,1],[0,2],[0,3]]
        ///
        /// 範例 2：
        /// 輸入：rows = 5, cols = 6, rStart = 1, cStart = 4
        /// 輸出：[[1,4],[1,5],[2,5],[2,4],[2,3],[1,3],[0,3],[0,4],[0,5],[3,5],[3,4],[3,3],
        /// [3,2],[2,2],[1,2],[0,2],[4,5],[4,4],[4,3],[4,2],[4,1],[3,1],[2,1],[1,1],[0,1],
        /// [4,0],[3,0],[2,0],[1,0],[0,0]]
        ///
        /// 限制條件：
        /// 1 &lt;= rows, cols &lt;= 100
        /// 0 &lt;= rStart &lt; rows
        /// 0 &lt;= cStart &lt; cols
        /// </para>
        /// </summary>
        /// <remarks>
        /// 依序執行官方案例與邊界案例，將每組輸入交給 <see cref="SpiralMatrixIII"/>，
        /// 再逐一比對預期與實際座標。輸出包含案例輸入、完整座標、PASS/FAIL 與通過總數。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        static void Main(string[] args)
        {
            SampleCase[] samples =
            [
                new(
                    "官方範例一",
                    1,
                    4,
                    0,
                    0,
                    [[0, 0], [0, 1], [0, 2], [0, 3]]),
                new(
                    "官方範例二",
                    5,
                    6,
                    1,
                    4,
                    [
                        [1, 4], [1, 5], [2, 5], [2, 4], [2, 3], [1, 3], [0, 3], [0, 4], [0, 5],
                        [3, 5], [3, 4], [3, 3], [3, 2], [2, 2], [1, 2], [0, 2], [4, 5], [4, 4],
                        [4, 3], [4, 2], [4, 1], [3, 1], [2, 1], [1, 1], [0, 1], [4, 0], [3, 0],
                        [2, 0], [1, 0], [0, 0]
                    ]),
                new(
                    "最小網格",
                    1,
                    1,
                    0,
                    0,
                    [[0, 0]]),
                new(
                    "單欄網格",
                    4,
                    1,
                    2,
                    0,
                    [[2, 0], [3, 0], [1, 0], [0, 0]]),
                new(
                    "由內部起點出發",
                    3,
                    3,
                    1,
                    1,
                    [[1, 1], [1, 2], [2, 2], [2, 1], [2, 0], [1, 0], [0, 0], [0, 1], [0, 2]])
            ];

            int passedCount = 0;

            for (int i = 0; i < samples.Length; i++)
            {
                SampleCase sample = samples[i];
                int[][] actual = SpiralMatrixIII(
                    sample.Rows,
                    sample.Cols,
                    sample.RStart,
                    sample.CStart);
                bool passed = CoordinatesEqual(sample.Expected, actual);

                if (passed)
                {
                    passedCount++;
                }

                Console.WriteLine($"案例 {i + 1}：{sample.Name}");
                Console.WriteLine(
                    $"輸入：rows = {sample.Rows}, cols = {sample.Cols}, " +
                    $"rStart = {sample.RStart}, cStart = {sample.CStart}");
                Console.WriteLine($"預期：{FormatCoordinates(sample.Expected)}");
                Console.WriteLine($"實際：{FormatCoordinates(actual)}");
                Console.WriteLine($"結果：{(passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCount}/{samples.Length} 筆測試通過");
        }

        /// <summary>
        /// 逐列、逐欄比較兩組座標序列是否完全相同。
        /// 輸入必須是由二元素座標陣列組成的非 null 序列；
        /// 只有座標數量、順序與每個 row/column 值都一致時才回傳 <see langword="true"/>。
        /// </summary>
        /// <param name="expected">預期的拜訪座標序列。</param>
        /// <param name="actual">實際產生的拜訪座標序列。</param>
        /// <returns>兩組座標序列完全相同時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool CoordinatesEqual(int[][] expected, int[][] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i].Length != actual[i].Length)
                {
                    return false;
                }

                for (int j = 0; j < expected[i].Length; j++)
                {
                    if (expected[i][j] != actual[i][j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 將座標序列格式化為 LeetCode 常見的 <c>[[row,col],...]</c> 表示法。
        /// 輸入必須是由二元素座標陣列組成的非 null 序列，輸出為可直接顯示及記錄的字串。
        /// </summary>
        /// <param name="coordinates">要格式化的座標序列。</param>
        /// <returns>包含全部座標且不含多餘空白的字串。</returns>
        private static string FormatCoordinates(int[][] coordinates)
        {
            return $"[{string.Join(",", coordinates.Select(coordinate => $"[{coordinate[0]},{coordinate[1]}]"))}]";
        }

        /// <summary>
        /// ref:
        /// 這方法比較好理解
        /// https://leetcode.cn/problems/spiral-matrix-iii/solutions/660264/dongge-de-jie-fa-si-lu-qing-xi-by-victor-gmmz/
        ///
        /// https://leetcode.cn/problems/spiral-matrix-iii/solutions/3546/luo-xuan-ju-zhen-iii-by-leetcode/
        /// https://leetcode.cn/problems/spiral-matrix-iii/solutions/1984188/by-stormsunshine-m9yn/
        ///
        /// 題目要求
        /// 順時鐘方向 走 螺旋方式
        /// 就會是:右, 下, 左, 上 這樣走
        ///
        /// 大原則就是 遍歷整個網格(Grid)
        /// 但是有可能會超出題目輸入的網格範圍邊界
        /// 所以 res 結果, 我們只會把範圍邊界內的給加入而已
        ///
        /// 螺旋狀的走路方向,遍歷整個網格
        /// 1. 先確定 四個邊界
        /// 2. 當一個方向走到底邊界時候, 舊調整方向
        /// 3. 根據方向更新下一個節點
        /// 4. 當節點在網格範圍內, 加到結果 res 中
        ///
        /// 每次改變方向 dir++
        ///
        /// Row 是橫的<上下增減數量>，Column 是直的<左右增減加數量>
        ///
        /// rows = 1, cols = 4
        /// => 1234
        ///
        /// </summary>
        /// <remarks>
        /// 輸入需滿足 rows、cols 為正整數，且起點位於網格內。
        /// 以右、下、左、上的方向陣列模擬順時針螺旋，動態向外擴張四個轉向邊界；
        /// 行走超出網格時仍繼續前進，但只將合法座標依拜訪順序加入結果。
        /// </remarks>
        /// <param name="rows">網格的列數。</param>
        /// <param name="cols">網格的欄數。</param>
        /// <param name="rStart">起點的列索引，範圍為 0 到 rows - 1。</param>
        /// <param name="cStart">起點的欄索引，範圍為 0 到 cols - 1。</param>
        /// <returns>依順時針螺旋拜訪順序排列的 rows * cols 組座標。</returns>
        public static int[][] SpiralMatrixIII(int rows, int cols, int rStart, int cStart)
        {
            int[][] res = new int[rows * cols][];
            for (int i = 0; i < rows * cols; i++)
            {
                res[i] = new int[2];
            }

            // 四個向量依序代表右、下、左、上；dir 在抵達對應邊界時切換到下一方向。
            int[][] around = { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 } };

            int x = rStart, y = cStart, num = 1, dir = 0;

            // 四個轉向邊界會在每次轉彎後向外擴張一格，形成逐圈放大的螺旋。
            int left = cStart - 1, right = cStart + 1, upper = rStart - 1, bottom = rStart + 1;

            while (num <= rows * cols)
            {
                if (x >= 0 && x < rows && y >= 0 && y < cols)
                {
                    // 越界座標只影響行走路徑；結果陣列僅收錄網格內的座標。
                    res[num - 1] = new int[] { x, y };
                    num++;
                }

                if (dir == 0 && y == right)
                {
                    dir += 1;
                    right += 1;
                }
                else if (dir == 1 && x == bottom)
                {
                    dir += 1;
                    bottom += 1;
                }
                else if (dir == 2 && y == left)
                {
                    dir += 1;
                    left--;
                }
                else if (dir == 3 && x == upper)
                {
                    dir = 0;
                    upper--;
                }

                x += around[dir][0];
                y += around[dir][1];
            }

            return res;
        }

        /// <summary>
        /// 保存一組可重現的螺旋矩陣案例，包括名稱、網格尺寸、起點與完整預期座標。
        /// </summary>
        /// <param name="Name">案例名稱。</param>
        /// <param name="Rows">網格列數。</param>
        /// <param name="Cols">網格欄數。</param>
        /// <param name="RStart">起點列索引。</param>
        /// <param name="CStart">起點欄索引。</param>
        /// <param name="Expected">完整預期座標序列。</param>
        private sealed record SampleCase(
            string Name,
            int Rows,
            int Cols,
            int RStart,
            int CStart,
            int[][] Expected);
    }
}
