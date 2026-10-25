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
    /// 解法一: 深度優先
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int ClosedIsland(int[][] grid)
    {
        int ans = 0;
        int m = grid.Length;
        int n = grid[0].Length;

        for(int i = 0; i < m; i++)
        {
            for(int j = 0; j < n; j++)
            {
                if(grid[i][j] == 0 && DFS(i, j, grid, m, n))
                {
                    ans++;
                }
            }
        }
        return ans;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="grid"></param>
    /// <param name="m"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public bool DFS(int x, int y, int[][] grid, int m ,int n)
    {
        if(x < 0 || y < 0 || x >= m || y >= n)
        {
            return false;
        }

        if(grid[x][y] != 0)
        {
            return true;
        }

        grid[x][y] = -1;
        bool ret1 = DFS(x - 1, y, grid, m, n);
        bool ret2 = DFS(x + 1, y, grid, m, n);
        bool ret3 = DFS(x, y - 1, grid, m, n);
        bool ret4 = DFS(x, y + 1, grid, m, n);

        return ret1 && ret2 && ret3 && ret4;
    }
}
