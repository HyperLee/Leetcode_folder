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
    /// 
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int MinimumArea(int[][] grid)
    {
        int n = grid.Length;
        int m = grid[0].Length;
        int minRow = n, maxRow = -1, minCol = m, maxCol = -1;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (grid[i][j] == 1)
                {
                    minRow = Math.Min(minRow, i);
                    maxRow = Math.Max(maxRow, i);
                    minCol = Math.Min(minCol, j);
                    maxCol = Math.Max(maxCol, j);
                }
            }
        }

        if (maxRow == -1)
        {
            return 0; // No 1's found
        }

        int area = (maxRow - minRow + 1) * (maxCol - minCol + 1);
        return area;
    }
}
