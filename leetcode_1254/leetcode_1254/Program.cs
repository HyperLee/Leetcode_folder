using System.ComponentModel.DataAnnotations.Schema;

namespace leetcode_1254;

class Program
{
    /// <summary>
    /// 1254. Number of Closed Islands
    /// https://leetcode.com/problems/number-of-closed-islands/description/
    /// 1254. 统计封闭岛屿的数目
    /// https://leetcode.cn/problems/number-of-closed-islands/description/
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// LeetCode 1254. Number of Closed Islands
    /// 使用深度優先搜尋（DFS）計算封閉島嶼數量。
    ///
    /// 0：尚未走訪的陸地
    /// 1：水域或已走訪的陸地
    ///
    /// 時間複雜度：O(m * n)
    /// 空間複雜度：O(m * n)，最壞情況為遞迴堆疊
    /// 
    /// https://chatgpt.com/s/t_6ade24d1fed88191aa2f2c274cbdc4aa
    /// </summary>
    public int ClosedIsland(int[][] grid)
    {
        int ans = 0;
        int rows = grid.Length;
        int cols = grid[0].Length;

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                // 只從尚未走訪的陸地開始 DFS。
                if(grid[i][j] == 0 && DFS(i, j, grid, rows, cols))
                {
                    ans++;
                }
            }
        }
        return ans;
    }

    /// <summary>
    /// 從指定陸地開始探索所有相連陸地。
    /// 回傳 true 代表整座島嶼沒有接觸矩陣邊界。
    /// 回傳 false 代表島嶼與邊界相連。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="grid"></param>
    /// <param name="m"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public bool DFS(int x, int y, int[][] grid, int m ,int n)
    {
        // 越界代表島嶼不是封閉的。
        if(x < 0 || y < 0 || x >= m || y >= n)
        {
            return false;
        }

        // 遇到水域或已走訪的位置，不再探索。
        if(grid[x][y] != 0)
        {
            return true;
        }
        
        // 將目前陸地標記為已走訪。
        grid[x][y] = -1;

        // 必須分別執行四個方向的 DFS，
        // 確保整座島嶼的陸地都被標記。
        bool up = DFS(x - 1, y, grid, m, n);
        bool down = DFS(x + 1, y, grid, m, n);
        bool left = DFS(x, y - 1, grid, m, n);
        bool right = DFS(x, y + 1, grid, m, n);

        // 四個方向都沒有越界，才是封閉島嶼。
        return up && down && left && right;
    }
}
