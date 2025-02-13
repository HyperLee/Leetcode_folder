namespace leetcode_994
{
    internal class Program
    {
        /// <summary>
        /// 994. Rotting Oranges
        /// https://leetcode.com/problems/rotting-oranges/description/
        /// 994. 腐烂的橘子
        /// https://leetcode.cn/problems/rotting-oranges/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 2, 1, 1 },
                 new int[]{ 1, 1, 0 },
                 new int[]{ 0, 1, 1 }
            };

            Console.WriteLine("res: " + OrangesRotting(input)); // 4
        }

        // dr 和 dc 陣列分別儲存了上、左、下、右四個方向的行和列的偏移量。它們用於遍歷相鄰的單元格。
        // 列(垂直)
        static int[] dr = new int[] { -1, 0, 1, 0};
        // 行(水平)
        static int[] dc = new int[] { 0, -1, 0, 1 };

        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/rotting-oranges/solutions/124765/fu-lan-de-ju-zi-by-leetcode-solution/
        /// https://leetcode.cn/problems/rotting-oranges/solutions/2773461/duo-yuan-bfsfu-ti-dan-pythonjavacgojsrus-yfmh/
        /// https://leetcode.cn/problems/rotting-oranges/solutions/1862399/by-stormsunshine-6lvs/
        /// GPT解釋解法步驟
        /// 
        /// 廣度優先搜索 (BFS)：BFS 是一種非常適合用於尋找最短路徑的演算法。在這題目中，我們使用 BFS 找到從初始腐爛橘子到其他橘子的最短路徑，即最短的腐爛時間。
        /// 
        /// 座標編碼： 使用 r * n + c 這種方式將二維座標轉換為一維編碼，方便儲存和查找。
        /// 時間複雜度: O(M * N)，其中 M 和 N 分別是橘子矩陣的行數和列數。
        /// 空間複雜度: O(M * N)，用於儲存佇列和深度資訊。
        /// 
        /// -----------------------------------------------------------------------------------------------------------------------------------------------------------
        /// 座標編碼：將二維座標(2D)轉換為一維編碼(1D)
        /// 為什麼要進行座標編碼？
        /// 在許多演算法和資料結構中，我們常常需要將二維空間中的點（例如，圖像中的像素、棋盤上的格子）映射到一維的索引上。這種映射可以簡化資料結構，提高查找效率。
        /// 編碼 (code) = 行索引 (row) * 網格寬度 (n) + 列索引 (column) 編碼的原理
        /// 行索引 (row)：代表單元格所在的水平位置，通常從 0 開始編號。
        /// 列索引 (column)：代表單元格所在的垂直位置，通常從 0 開始編號。
        /// 網格寬度 (n)：代表網格每一列有多少個單元格。這也等於網格的總列數。
        /// 這個公式將二維座標 (r, c) 映射到一個唯一的整數 index 上。這個整數可以作為陣列或其他資料結構的索引，從而實現對二維空間的線性存取。
        /// 為什麼使用 r * n + c 這種方式？
        /// 簡單直觀： 公式簡單易懂，實現起來也很方便。
        /// 唯一映射： 每個二維座標都對應一個唯一的編碼，不會產生衝突。
        /// 高效查找： 通過編碼，可以快速地在一個一維陣列中找到對應的元素。
        /// 
        /// 後續如何還原 2D座標
        /// 1.取得行索引 (row)
        /// 整數除法結果：  code / n (整數除法) ≈ row
        /// 實際上，更精確地說，整數除法 code / n 的結果正好等於 行索引 (row)。  
        /// 因為整數除法會捨去小數部分，所以 (row * n + column) / n 的整數部分就只剩下 row。
        /// 因此，行索引的解碼公式為：
        /// 行索引 (row) = 編碼 (code) / 網格寬度 (n)   (整數除法)
        /// 
        /// 2.取得列索引 (column)
        /// 列索引 (column) = 編碼 (code) % 網格寬度 (n)  (取餘數)
        /// -----------------------------------------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int OrangesRotting(int[][] grid)
        {
            // 行數(水平)
            int m = grid.Length;
            // 列數(垂直)
            int n = grid[0].Length;
            // 初始化一個隊列 queue 來追蹤腐爛橘子的位置信息（壓縮成單一數字）。
            Queue<int> queue = new Queue<int>();
            // key: code, value: depth; 使用字典 depth 來存儲每個橘子變爛的時間（深度）。
            IDictionary<int, int> depth = new Dictionary<int, int>();

            // **步驟 1：將所有初始的腐爛橘子加入佇列**
            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    // 腐爛的橘子為 2
                    if (grid[r][c] == 2)
                    {
                        // 對於每個腐爛橘子，計算其位置編碼（r * n + c），將其加入隊列; 壓縮座標（1D 表示法）
                        int code = r * n + c;
                        queue.Enqueue(code);
                        // ，並將其深度(初始時間)設置為 0。
                        depth.Add(code, 0);
                    }
                }
            }

            // 最終結果（所有橘子腐爛所需時間）
            int res = 0;
            // **步驟 2：開始 BFS 傳播腐爛**
            while (queue.Count > 0)
            {
                // 取出當前腐爛的橘子
                int code = queue.Dequeue();
                // 還原成 2D 座標 (r, c)
                int r = code / n;
                int c = code % n;
                // 遍歷四個方向（上、左、下、右）
                for (int i = 0; i < 4; i++)
                {
                    // 新橘子的 row
                    int newR = r + dr[i];
                    // 新橘子的 col
                    int newC = c + dc[i];
                    // **條件判斷**：確保新座標有效且為新鮮橘子
                    if (newR >= 0 && newR < m && newC >= 0 && newC < n && grid[newR][newC] == 1)
                    {
                        // 讓新鮮橘子變成腐爛
                        grid[newR][newC] = 2;
                        // 壓縮新座標
                        int newCode = newR * n + newC;
                        // 加入 BFS 佇列
                        queue.Enqueue(newCode);
                        // 更新腐爛時間
                        depth.Add(newCode, depth[code] + 1);
                        // 更新最長時間
                        res = depth[newCode];
                    }
                }
            }

            // **步驟 3：確認是否還有新鮮橘子**
            foreach (var row in grid)
            {
                foreach (var v in row)
                {
                    // 如果還有新鮮橘子
                    if (v == 1)
                    {
                        return -1;
                    }
                }
            }

            // 返回總時間
            return res;
        }
    }
}
