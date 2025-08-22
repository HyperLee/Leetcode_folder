using System;

namespace leetcode_3195;

class Program
{
    /// <summary>
    /// 3195. Find the Minimum Area to Cover All Ones I
    /// https://leetcode.com/problems/find-the-minimum-area-to-cover-all-ones-i/description/?envType=daily-question&envId=2025-08-22
    /// 3195. 包含所有 1 的最小矩形面積 I
    /// https://leetcode.cn/problems/find-the-minimum-area-to-cover-all-ones-i/description/?envType=daily-question&envId=2025-08-22
    ///
    /// Problem (English):
    /// Given a 2D binary array grid. Find a rectangle with horizontal and vertical sides with the smallest area,
    /// such that all the 1's in grid lie inside this rectangle.
    /// Return the minimum possible area of the rectangle.
    ///
    /// 中文翻譯：
    /// 給定一個 2D 二元陣列 grid（只包含 0 和 1），找出一個邊與座標軸平行的最小面積矩形，
    /// 使得 grid 中所有值為 1 的格子都被包含在該矩形內。回傳該矩形的最小可能面積。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Running MinimumArea tests...");

        // 測試 1: 沒有 1
        RunTest(new int[][]
        {
            new int[] {0,0},
            new int[] {0,0}
        }, 0, "NoOnes");

        // 測試 2: 單一 1
        RunTest(new int[][]
        {
            new int[] {0,0,0},
            new int[] {0,1,0},
            new int[] {0,0,0}
        }, 1, "SingleOne");

        // 測試 3: 連續 1 的矩形 (2x2)
        RunTest(new int[][]
        {
            new int[] {0,0,0,0},
            new int[] {0,1,1,0},
            new int[] {0,1,1,0},
            new int[] {0,0,0,0}
        }, 4, "Block2x2");

        // 測試 4: 分散的 1
        RunTest(new int[][]
        {
            new int[] {1,0,0},
            new int[] {0,0,1},
            new int[] {0,0,0}
        }, 6, "Scattered");

        // 測試 5: 分散的 1
        RunTest(new int[][]
        {
            new int[] {0,1,0},
            new int[] {1,0,1},
        }, 6, "Scattered - 2");        

        Console.WriteLine("Tests finished.");
    }

    /// <summary>
    /// 執行單一測試並輸出結果
    /// </summary>
    /// <param name="grid">輸入的二元陣列</param>
    /// <param name="expected">預期面積</param>
    /// <param name="name">測試名稱</param>
    static void RunTest(int[][] grid, int expected, string name)
    {
        var runner = new Program();
        int actual = runner.MinimumArea(grid);
        string status = actual == expected ? "PASS" : "FAIL";
        Console.WriteLine($"{name}: expected={expected}, actual={actual} => {status}");
    }

    /// <summary>
    /// 計算包含所有 1 的最小軸平行矩形面積。
    ///
    /// 解題說明：遍歷整個 `grid`，記錄出現 1 的最小/最大列與欄索引 (minRow, maxRow, minCol, maxCol)。
    /// 這四個邊界即可唯一決定包含所有 1 的最小矩形，面積為 (maxRow - minRow + 1) * (maxCol - minCol + 1)。
    ///
    /// 時間複雜度：O(n * m)（需掃描整個矩陣）。
    /// 空間複雜度：O(1)。
    ///
    /// 邊界情況：
    /// - 若輸入為 null 或大小為 0，回傳 0。
    /// - 若矩陣中沒有任何 1，回傳 0。
    /// </summary>
    /// <param name="grid">輸入的二元陣列（只含 0 和 1）</param>
    /// <returns>最小矩形的面積；若無 1 或輸入無效，則回傳 0</returns>
    public int MinimumArea(int[][] grid)
    {
        // 輸入驗證：處理 null 或空陣列的情況
        if (grid is null || grid.Length == 0 || grid[0] is null || grid[0].Length == 0)
        {
            return 0;
        }

        int n = grid.Length; // 列數
        int m = grid[0].Length; // 欄數
        // 初始化邊界，min 設為極大值，max 設為極小值
        int minRow = n, maxRow = -1, minCol = m, maxCol = -1;

        // 掃描整個矩陣，更新出現 1 的最小/最大索引
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (grid[i][j] == 1)
                {
                    // 找到 1，更新四個邊界
                    minRow = Math.Min(minRow, i);
                    maxRow = Math.Max(maxRow, i);
                    minCol = Math.Min(minCol, j);
                    maxCol = Math.Max(maxCol, j);
                }
            }
        }

        // 如果 maxRow 未被修改代表沒有任何 1
        if (maxRow == -1)
        {
            return 0; // 矩陣內沒有 1
        }

        // 計算面積（包含邊界的格子數）
        int area = (maxRow - minRow + 1) * (maxCol - minCol + 1);
        return area;
    }
}
