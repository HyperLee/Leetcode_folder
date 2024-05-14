namespace leetcode_1219
{
    internal class Program
    {
        /// <summary>
        /// 1219. Path with Maximum Gold
        /// https://leetcode.com/problems/path-with-maximum-gold/description/?envType=daily-question&envId=2024-05-14
        /// 1219. 黄金矿工
        /// https://leetcode.cn/problems/path-with-maximum-gold/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 0, 6, 0 },
                 new int[]{ 5, 8, 7 },
                 new int[]{ 0, 9, 0 }
            };



        }

        // 可走 上下左右 四個方向
        static int[][] dirs = { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { 0, 1 } };
        int[][] grid;
        int m, n;
        int ans = 0;


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/path-with-maximum-gold/solutions/1240253/huang-jin-kuang-gong-by-leetcode-solutio-f9gg/
        /// https://leetcode.cn/problems/path-with-maximum-gold/solutions/1245984/gong-shui-san-xie-hui-su-suan-fa-yun-yon-scxo/
        /// https://leetcode.cn/problems/path-with-maximum-gold/solutions/1932654/by-stormsunshine-blm6/
        /// 
        /// 用兩個for迴圈
        /// 遍歷全部位置
        /// gold預設初始給0
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int GetMaximumGold(int[][] grid)
        {
            this.grid = grid;
            this.m = grid.Length;
            this.n = grid[0].Length;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] != 0)
                    {
                        DFS(i, j, 0);
                    }
                }
            }

            return ans;
        }



        /// <summary>
        /// grid內cell 數值不能為0的才能走
        /// 以及 不為0的才能當作起始點
        /// 
        /// 一進入該cell, 就開採cell裡面數值
        /// 從當前位置 延伸 可以走四個方向, 上下左右
        /// 每個cell只能走一次
        /// 
        /// 從當下位置, 進行上下左右方向進行開採
        /// 採DFS 回朔 找出累計最大值
        /// 
        /// 當前位置(x, y), 延伸四個方向(nx, ny)
        /// 去找出最大化收益 ans
        /// 找完之後回去main function
        /// 再去走下一個(x, y)位置
        /// </summary>
        /// <param name="x">當前所在位置</param>
        /// <param name="y">當前所在位置</param>
        /// <param name="gold">開採路徑(x, y)</param>
        public void DFS(int x, int y, int gold)
        {
            // 一進入該cell, 就進行開採
            gold += grid[x][y];
            // 更新, 累計最大化收益
            ans = Math.Max(ans, gold);
            int rec = grid[x][y];
            // 到達(x, y)位置, 先將grid[x][y]暫時設為0
            grid[x][y] = 0;

            // 從目前(x, y)位置 走上下左右 四個方向延伸, cell 值不能為 0
            for (int d = 0; d < 4; d++)
            {
                // new x
                int nx = x + dirs[d][0];
                // new y
                int ny = y + dirs[d][1];

                if (nx >= 0 && nx < m && ny >= 0 && ny < n && grid[nx][ny] > 0)
                {
                    DFS(nx, ny, gold);
                }
            }

            // 回溯之前, 先將grid[x][y]回復
            grid[x][y] = rec;
        }
    }
}
