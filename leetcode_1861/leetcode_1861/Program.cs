namespace leetcode_1861;

class Program
{
    /// <summary>
    /// 1861. Rotating the Box
    /// https://leetcode.com/problems/rotating-the-box/description/?envType=daily-question&envId=2026-05-06
    /// 1861. 旋轉盒子
    /// https://leetcode.cn/problems/rotating-the-box/description/?envType=daily-question&envId=2026-05-06
    ///
    /// You are given an m x n matrix of characters boxGrid representing a side-view of a box.
    /// Each cell of the box is one of the following:
    ///   A stone '#'
    ///   A stationary obstacle '*'
    ///   Empty '.'
    ///
    /// The box is rotated 90 degrees clockwise, causing some of the stones to fall due to gravity.
    /// Each stone falls down until it lands on an obstacle, another stone, or the bottom of the box.
    /// Gravity does not affect the obstacles' positions, and the inertia from the box's rotation
    /// does not affect the stones' horizontal positions.
    ///
    /// It is guaranteed that each stone in boxGrid rests on an obstacle, another stone, or the bottom of the box.
    ///
    /// Return an n x m matrix representing the box after the rotation described above.
    ///
    /// 給定一個 m x n 的字元矩陣 boxGrid，代表一個盒子的側視圖。
    /// 盒子中的每個格子是以下之一：
    ///   石頭 '#'
    ///   固定障礙物 '*'
    ///   空格 '.'
    ///
    /// 盒子順時針旋轉 90 度，由於重力作用，部分石頭會向下掉落。
    /// 每顆石頭會持續往下掉，直到碰到障礙物、另一顆石頭，或到達盒子底部為止。
    /// 重力不影響障礙物的位置，且旋轉的慣性不影響石頭的水平位置。
    ///
    /// 保證 boxGrid 中的每顆石頭都靠在障礙物、另一顆石頭或盒子底部上。
    ///
    /// 回傳旋轉後盒子的 n x m 矩陣。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：單列，無障礙物
        // 輸入:  [['#','.','#']]
        // 預期:  [['.'],['#'],['#']]
        char[][] box1 = [['#', '.', '#']];
        Console.WriteLine("=== Test Case 1 ===");
        Console.WriteLine("Input:");
        PrintGrid(box1);
        Console.WriteLine("Output:");
        PrintGrid(solution.RotateTheBox(box1));

        // 測試案例 2：兩列，含障礙物 '*'
        // 輸入:  [['#','.','*','.'],['#','#','*','.']]
        // 預期:  [['#','.'],['#','#'],['*','*'],['.','.']]
        char[][] box2 =
        [
            ['#', '.', '*', '.'],
            ['#', '#', '*', '.']
        ];
        Console.WriteLine("=== Test Case 2 ===");
        Console.WriteLine("Input:");
        PrintGrid(box2);
        Console.WriteLine("Output:");
        PrintGrid(solution.RotateTheBox(box2));

        // 測試案例 3：三列，多個障礙物
        // 輸入:  [['#','#','*','.','*','.'],['#','#','*','#','*','.'],['#','#','*','#','*','#']]
        // 預期:  [['#','#','#'],['#','#','#'],['*','*','*'],['#','#','.'],['*','*','*'],['#','.','.']]
        char[][] box3 =
        [
            ['#', '#', '*', '.', '*', '.'],
            ['#', '#', '*', '#', '*', '.'],
            ['#', '#', '*', '#', '*', '#']
        ];
        Console.WriteLine("=== Test Case 3 ===");
        Console.WriteLine("Input:");
        PrintGrid(box3);
        Console.WriteLine("Output:");
        PrintGrid(solution.RotateTheBox(box3));
    }

    /// <summary>
    /// 輔助函式：以可讀格式列印二維字元矩陣。
    /// </summary>
    /// <param name="grid">要列印的二維字元矩陣</param>
    static void PrintGrid(char[][] grid)
    {
        foreach (char[] row in grid)
        {
            Console.WriteLine("[" + string.Join(", ", row.Select(c => $"'{c}'")) + "]");
        }

        Console.WriteLine();
    }

    /// <summary>
    /// 解法：正序遍歷（模擬重力 + 旋轉）
    ///
    /// 核心思路：
    ///   boxGrid 的每一列互相獨立，可分別計算。
    ///   在原始盒子中，重力使石頭向右滑動；旋轉後，此方向恰好對應新的向下方向。
    ///
    /// 演算法步驟：
    ///   1. 以障礙物 '*' 為分隔，將每列劃分成多個獨立區段。
    ///   2. 遍歷每個區段，以 cnt 統計其中石頭('#')的數量。
    ///      遍歷時先將石頭視為空格寫入目標位置；
    ///      當區段結束（遇到障礙物或到達列末），
    ///      再從尾端往前填入 cnt 個石頭，模擬石頭向右集中掉落。
    ///   3. 同時套用旋轉座標轉換：
    ///      原座標 (i, j) ── 順時針旋轉 90° ──▶ (j, m - 1 - i)
    ///      其中 m 為原始矩陣的列數。
    ///
    /// 時間複雜度：O(m × n)
    /// 空間複雜度：O(m × n)（輸出矩陣，不計輸入）
    /// </summary>
    /// <param name="boxGrid">m × n 的字元矩陣，代表盒子側視圖</param>
    /// <returns>旋轉並套用重力後的 n × m 字元矩陣</returns>
    public char[][] RotateTheBox(char[][] boxGrid)
    {
        // m = 原始 box 的列數
        int m = boxGrid.Length;

        // n = 原始 box 的行數
        int n = boxGrid[0].Length;

        // 旋轉後矩陣大小會變成 n x m
        char[][] ans = new char[n][];

        // 初始化答案矩陣
        for (int i = 0; i < n; i++)
        {
            ans[i] = new char[m];
        }

        // 遍歷原始 box 每一列
        for (int i = 0; i < m; i++)
        {
            char[] row = boxGrid[i];

            // cnt 用來記錄目前區間內石頭(#)數量
            int cnt = 0;

            // 遍歷當前列的每個位置
            for (int j = 0; j < n; j++)
            {
                char ch = row[j];

                // 如果遇到石頭，先計數
                if (ch == '#')
                {
                    cnt++;

                    // 先把石頭清空，後面再統一掉落
                    ch = '.';
                }

                /*
                 * 先做旋轉
                 * 原座標 (i, j)
                 * 旋轉 90 度順時針後 -> (j, m - 1 - i)
                 */
                ans[j][m - 1 - i] = ch;

                /*
                 * 如果：
                 * 1. 已經到這一列最後一格
                 * 2. 下一格是障礙物(*)
                 *
                 * 表示一個區段結束，需要讓石頭往右(旋轉後往下)掉落
                 */
                if (j == n - 1 || row[j + 1] == '*')
                {
                    /*
                     * 從 j 開始往前填入 cnt 個石頭
                     * 代表石頭因重力集中到最右側
                     */
                    for (int k = j; k > j - cnt; k--)
                    {
                        ans[k][m - 1 - i] = '#';
                    }

                    // 重置石頭計數
                    cnt = 0;
                }
            }
        }

        return ans;
    }
}
