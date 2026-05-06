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
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="boxGrid"></param>
    /// <returns></returns>
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
