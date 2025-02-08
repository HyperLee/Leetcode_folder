using System.Text.Json.Serialization;

namespace leetcode_200
{
    internal class Program
    {
        /// <summary>
        /// 200. Number of Islands
        /// https://leetcode.com/problems/number-of-islands/description/
        /// 200. 岛屿数量
        /// https://leetcode.cn/problems/number-of-islands/description/
        /// 
        /// Grid 題目類型大部分都可以採用 DFS 深度優先搜索解題
        /// 但並不是全部, 有些題目可能需要 BFS 廣度優先搜索
        /// 
        /// 網格題目中，深度優先搜索（DFS）確實是一種常見且有效的解法，特別是在處理連通分量、島嶼數量、迷宮問題等情況下。
        /// 然而，並不是所有網格題目都適合僅用DFS來解決。有些情況下，廣度優先搜索（BFS）可能更合適，特別是當需要找到最短路徑或最小步數時。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 輸入 座標 範例, 也可輸入其他方式 參考MSDN
            char[][] input = new char[][]
            {
                 new char[]{ '1', '1', '1', '1', '0' },
                 new char[]{ '1', '1', '0', '1', '0' },
                 new char[]{ '1', '1', '0', '0', '0' },
                 new char[]{ '0', '0', '0', '0', '0' }
            };

            Console.WriteLine("res: " + NumIslands(input));
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/number-of-islands/solutions/13103/dao-yu-shu-liang-by-leetcode/
        /// https://leetcode.cn/problems/number-of-islands/solutions/2965773/ba-fang-wen-guo-de-ge-zi-cha-shang-qi-zi-9gs0/
        /// https://leetcode.cn/problems/number-of-islands/solutions/1861061/by-stormsunshine-46k4/
        /// 
        /// 採用DFS深度優先搜索下去解題
        /// 我們可以將二維網格看作一個無向圖，其中竖直或水平相鄰的 1 之間有邊相連。
        /// Grid:
        /// 1: 陸地
        /// 0: 水域
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int NumIslands(char[][] grid)
        {
            // 把dfs次數當作結果(幾個島嶼, 找幾次就是幾個島嶼)
            int res = 0;
            for(int i = 0; i < grid.Length; i++)
            {
                for(int j = 0; j < grid[i].Length; j++)
                {
                    // 如果是陸地, 則進行深度優先搜索
                    if (grid[i][j] == '1')
                    {
                        // 找到的島嶼插上旗子, 避免重覆搜尋
                        dfs(grid, i, j);
                        res++;
                    }
                }
            }

            return res;
        }


        /// <summary>
        /// 注意 Grid 是二維陣列, 且是 char 類型
        /// 左上角為原點, 向右為 x 軸正方向, 向下為 y 軸正方向
        /// 
        /// 只能訪問 上下左右 四個方向, 不能訪問對角線
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public static void dfs(char[][] grid, int i, int j)
        {
            // 超出邊界或者是水域, 則返回
            if (i < 0 || i >= grid.Length || j < 0 || j>= grid[0].Length || grid[i][j] != '1')
            {
                return;
            }

            grid[i][j] = '2'; // 插旗,註記為已經訪問過. 這樣下次就不會再訪問了
            dfs(grid, i, j - 1); // 向左
            dfs(grid, i, j + 1); // 向右
            dfs(grid, i - 1, j); // 向上
            dfs(grid, i + 1, j); // 向下
        }
    }
}
