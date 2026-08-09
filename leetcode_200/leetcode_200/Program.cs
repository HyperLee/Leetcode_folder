namespace leetcode_200
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 200. Number of Islands
        /// https://leetcode.com/problems/number-of-islands/description/
        ///
        /// Given an m x n 2D binary grid representing a map of '1' (land) and '0' (water), return the number of islands. An island is surrounded by water and formed by horizontally or vertically adjacent land. Assume all four edges of the grid are surrounded by water.
        ///
        /// Example 1:
        /// Input: grid = [["1","1","1","1","0"],["1","1","0","1","0"],["1","1","0","0","0"],["0","0","0","0","0"]]
        /// Output: 1
        ///
        /// Example 2:
        /// Input: grid = [["1","1","0","0","0"],["1","1","0","0","0"],["0","0","1","0","0"],["0","0","0","1","1"]]
        /// Output: 3
        ///
        /// Constraints:
        /// - m == grid.length
        /// - n == grid[i].length
        /// - 1 &lt;= m, n &lt;= 300
        /// - grid[i][j] is '0' or '1'.
        /// </para>
        /// <para>
        /// 200. 島嶼數量
        /// https://leetcode.cn/problems/number-of-islands/description/
        ///
        /// 給定 m x n 的二維二元網格 grid，表示由 '1'（陸地）與 '0'（水）構成的地圖，回傳島嶼數量。島嶼被水包圍，由水平或垂直相鄰的陸地連接而成；可以假設網格四周都被水包圍。
        ///
        /// 範例 1：
        /// 輸入：grid = [["1","1","1","1","0"],["1","1","0","1","0"],["1","1","0","0","0"],["0","0","0","0","0"]]
        /// 輸出：1
        ///
        /// 範例 2：
        /// 輸入：grid = [["1","1","0","0","0"],["1","1","0","0","0"],["0","0","1","0","0"],["0","0","0","1","1"]]
        /// 輸出：3
        ///
        /// 限制條件：
        /// - m == grid.length
        /// - n == grid[i].length
        /// - 1 &lt;= m, n &lt;= 300
        /// - grid[i][j] 為 '0' 或 '1'。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行五組固定網格案例，分別呼叫 DFS、BFS 與並查集解法，
        /// 並比較每種解法的實際結果與預期島嶼數量。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] samples =
            [
                new(
                    "官方範例一：單一島嶼",
                    [
                        ['1', '1', '1', '1', '0'],
                        ['1', '1', '0', '1', '0'],
                        ['1', '1', '0', '0', '0'],
                        ['0', '0', '0', '0', '0']
                    ],
                    1),
                new(
                    "官方範例二：三個島嶼",
                    [
                        ['1', '1', '0', '0', '0'],
                        ['1', '1', '0', '0', '0'],
                        ['0', '0', '1', '0', '0'],
                        ['0', '0', '0', '1', '1']
                    ],
                    3),
                new(
                    "全部為水域",
                    [
                        ['0', '0'],
                        ['0', '0']
                    ],
                    0),
                new(
                    "單一陸地格",
                    [
                        ['1']
                    ],
                    1),
                new(
                    "對角線相鄰不算連通",
                    [
                        ['1', '0', '0'],
                        ['0', '1', '0'],
                        ['0', '0', '1']
                    ],
                    3)
            ];

            (string Name, Func<char[][], int> Solve)[] solutions =
            [
                ("DFS", NumIslands),
                ("BFS", NumIslands2),
                ("並查集", NumIslands3)
            ];

            int passedChecks = 0;
            int totalChecks = samples.Length * solutions.Length;

            for (int index = 0; index < samples.Length; index++)
            {
                SampleCase sample = samples[index];
                Console.WriteLine($"案例 {index + 1}：{sample.Name}");
                Console.WriteLine($"輸入：{FormatGrid(sample.Grid)}");
                Console.WriteLine($"預期：{sample.Expected}");

                foreach ((string name, Func<char[][], int> solve) in solutions)
                {
                    int actual = solve(CloneGrid(sample.Grid));
                    bool passed = actual == sample.Expected;
                    passedChecks += passed ? 1 : 0;
                    Console.WriteLine($"{name} Actual：{actual} => {(passed ? "PASS" : "FAIL")}");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        }

        /// <summary>
        /// 建立網格的深層複本，讓會原地標記節點的解法各自使用獨立輸入。
        /// 輸入必須是由等長字元列組成的有效網格；輸出內容與原網格相同但不共用列陣列。
        /// </summary>
        /// <param name="grid">要複製的二維字元網格。</param>
        /// <returns>可獨立修改的二維字元網格。</returns>
        private static char[][] CloneGrid(char[][] grid)
        {
            char[][] copy = new char[grid.Length][];

            for (int row = 0; row < grid.Length; row++)
            {
                copy[row] = (char[])grid[row].Clone();
            }

            return copy;
        }

        /// <summary>
        /// 將二維字元網格格式化為單行陣列文字，供案例輸出與 README 紀錄使用。
        /// 輸入為有效的二維字元網格；輸出保留每一列及每個元素的順序。
        /// </summary>
        /// <param name="grid">要格式化的二維字元網格。</param>
        /// <returns>例如 <c>[["1","0"],["0","1"]]</c> 的字串。</returns>
        private static string FormatGrid(char[][] grid)
        {
            return $"[{string.Join(",", grid.Select(
                row => $"[{string.Join(",", row.Select(value => $"\"{value}\""))}]"))}]";
        }

        /// <summary>
        /// 表示一組可重複執行的島嶼數量案例，包含名稱、輸入網格與預期答案。
        /// </summary>
        /// <param name="Name">案例顯示名稱。</param>
        /// <param name="Grid">只包含 <c>'0'</c> 與 <c>'1'</c> 的矩形網格。</param>
        /// <param name="Expected">網格中的預期島嶼數量。</param>
        private sealed record SampleCase(string Name, char[][] Grid, int Expected);

        /// <summary>
        /// 使用深度優先搜尋（DFS）計算網格中的島嶼數量。
        /// 掃描到尚未造訪的陸地時，以遞迴走訪整座島並標記為 <c>'2'</c>，
        /// 因此每次啟動 DFS 就代表發現一座新島嶼。
        /// 輸入必須是只包含 <c>'0'</c> 與 <c>'1'</c> 的非空矩形網格；
        /// 方法會原地修改網格，並回傳四方向相連的島嶼總數。
        /// </summary>
        /// <param name="grid">要搜尋的陸地與水域網格。</param>
        /// <returns>網格中的島嶼數量。</returns>
        public static int NumIslands(char[][] grid)
        {
            int islandCount = 0;

            for (int row = 0; row < grid.Length; row++)
            {
                for (int column = 0; column < grid[row].Length; column++)
                {
                    if (grid[row][column] == '1')
                    {
                        // 每個尚未造訪的陸地都是一座新島嶼的起點。
                        islandCount++;
                        dfs(grid, row, column);
                    }
                }
            }

            return islandCount;
        }

        /// <summary>
        /// 從指定座標進行 DFS，將同一座島中四方向相連的所有陸地標記為已造訪。
        /// 輸入網格必須是非空矩形；超出邊界、水域或已造訪的位置會立即停止，
        /// 方法不回傳資料，而是直接把走訪過的 <c>'1'</c> 改為 <c>'2'</c>。
        /// </summary>
        /// <param name="grid">要原地標記的陸地與水域網格。</param>
        /// <param name="i">目前位置的列索引。</param>
        /// <param name="j">目前位置的欄索引。</param>
        public static void dfs(char[][] grid, int i, int j)
        {
            if (i < 0 || i >= grid.Length || j < 0 || j >= grid[0].Length || grid[i][j] != '1')
            {
                return;
            }

            // 先標記再展開四個方向，避免相鄰陸地彼此重複遞迴。
            grid[i][j] = '2';
            dfs(grid, i, j - 1);
            dfs(grid, i, j + 1);
            dfs(grid, i - 1, j);
            dfs(grid, i + 1, j);
        }

        /// <summary>
        /// 使用廣度優先搜尋（BFS）計算網格中的島嶼數量。
        /// 掃描到新陸地時，以佇列逐層走訪整座島，並在加入佇列時標記為 <c>'2'</c>。
        /// 輸入必須是只包含 <c>'0'</c> 與 <c>'1'</c> 的非空矩形網格；
        /// 方法會原地修改網格，並回傳四方向相連的島嶼總數。
        /// </summary>
        /// <param name="grid">要搜尋的陸地與水域網格。</param>
        /// <returns>網格中的島嶼數量。</returns>
        public static int NumIslands2(char[][] grid)
        {
            int islandCount = 0;

            for (int row = 0; row < grid.Length; row++)
            {
                for (int column = 0; column < grid[row].Length; column++)
                {
                    if (grid[row][column] == '1')
                    {
                        // 每次啟動 BFS 都會完整消耗一個連通分量。
                        islandCount++;
                        Bfs(grid, row, column);
                    }
                }
            }

            return islandCount;
        }

        /// <summary>
        /// 從指定陸地開始 BFS，使用佇列走訪四方向相鄰的所有陸地。
        /// 輸入網格必須是非空矩形且起點為 <c>'1'</c>；
        /// 方法會把同一座島的陸地原地標記為 <c>'2'</c>，不回傳資料。
        /// </summary>
        /// <param name="grid">要原地標記的陸地與水域網格。</param>
        /// <param name="startRow">起點的列索引。</param>
        /// <param name="startColumn">起點的欄索引。</param>
        private static void Bfs(char[][] grid, int startRow, int startColumn)
        {
            (int Row, int Column)[] directions =
            [
                (-1, 0),
                (1, 0),
                (0, -1),
                (0, 1)
            ];
            Queue<(int Row, int Column)> queue = new();
            queue.Enqueue((startRow, startColumn));
            grid[startRow][startColumn] = '2';

            while (queue.Count > 0)
            {
                (int row, int column) = queue.Dequeue();

                foreach ((int rowOffset, int columnOffset) in directions)
                {
                    int nextRow = row + rowOffset;
                    int nextColumn = column + columnOffset;
                    bool isInside = nextRow >= 0
                        && nextRow < grid.Length
                        && nextColumn >= 0
                        && nextColumn < grid[0].Length;

                    if (isInside && grid[nextRow][nextColumn] == '1')
                    {
                        // 入列時立即標記，確保同一格最多只會加入佇列一次。
                        grid[nextRow][nextColumn] = '2';
                        queue.Enqueue((nextRow, nextColumn));
                    }
                }
            }
        }

        /// <summary>
        /// 使用並查集計算網格中的島嶼數量。
        /// 先把每個陸地建立為獨立集合，再合併右方與下方相鄰陸地；
        /// 最後剩餘的集合數就是島嶼數量。
        /// 輸入必須是只包含 <c>'0'</c> 與 <c>'1'</c> 的非空矩形網格；
        /// 方法不修改輸入，並回傳四方向相連的島嶼總數。
        /// </summary>
        /// <param name="grid">要分析的陸地與水域網格。</param>
        /// <returns>網格中的島嶼數量。</returns>
        public static int NumIslands3(char[][] grid)
        {
            int rowCount = grid.Length;
            int columnCount = grid[0].Length;
            UnionFind unionFind = new(grid);

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    if (grid[row][column] != '1')
                    {
                        continue;
                    }

                    int currentIndex = (row * columnCount) + column;

                    // 只檢查右方與下方，每一對相鄰陸地只會合併一次。
                    if (column + 1 < columnCount && grid[row][column + 1] == '1')
                    {
                        unionFind.Union(currentIndex, currentIndex + 1);
                    }

                    if (row + 1 < rowCount && grid[row + 1][column] == '1')
                    {
                        unionFind.Union(currentIndex, currentIndex + columnCount);
                    }
                }
            }

            return unionFind.Count;
        }

        /// <summary>
        /// 維護陸地格子的連通集合，使用路徑壓縮與按秩合併降低查找成本。
        /// </summary>
        private sealed class UnionFind
        {
            private readonly int[] parent;
            private readonly int[] rank;

            /// <summary>
            /// 取得目前互不連通的陸地集合數量。
            /// </summary>
            public int Count { get; private set; }

            /// <summary>
            /// 依輸入網格建立並查集；每個陸地格初始為獨立集合，水域不加入集合。
            /// 輸入必須是非空矩形網格；建構完成後的 <see cref="Count"/> 等於陸地格數量。
            /// </summary>
            /// <param name="grid">只包含陸地與水域的矩形網格。</param>
            public UnionFind(char[][] grid)
            {
                int columnCount = grid[0].Length;
                parent = new int[grid.Length * columnCount];
                rank = new int[parent.Length];
                Array.Fill(parent, -1);

                for (int row = 0; row < grid.Length; row++)
                {
                    for (int column = 0; column < columnCount; column++)
                    {
                        if (grid[row][column] == '1')
                        {
                            int index = (row * columnCount) + column;
                            parent[index] = index;
                            Count++;
                        }
                    }
                }
            }

            /// <summary>
            /// 找出指定陸地格所屬集合的根節點，並壓縮沿途路徑。
            /// 輸入索引必須對應已加入並查集的陸地；輸出為該集合的根索引。
            /// </summary>
            /// <param name="index">陸地格的一維索引。</param>
            /// <returns>該陸地集合的根索引。</returns>
            private int Find(int index)
            {
                if (parent[index] != index)
                {
                    parent[index] = Find(parent[index]);
                }

                return parent[index];
            }

            /// <summary>
            /// 合併兩個相鄰陸地格所屬的集合；若原本已連通則不重複扣除集合數。
            /// 輸入索引必須都對應陸地，方法不回傳資料，成功合併時會將集合數減一。
            /// </summary>
            /// <param name="firstIndex">第一個陸地格的一維索引。</param>
            /// <param name="secondIndex">第二個陸地格的一維索引。</param>
            public void Union(int firstIndex, int secondIndex)
            {
                int firstRoot = Find(firstIndex);
                int secondRoot = Find(secondIndex);

                if (firstRoot == secondRoot)
                {
                    return;
                }

                if (rank[firstRoot] < rank[secondRoot])
                {
                    parent[firstRoot] = secondRoot;
                }
                else if (rank[firstRoot] > rank[secondRoot])
                {
                    parent[secondRoot] = firstRoot;
                }
                else
                {
                    parent[secondRoot] = firstRoot;
                    rank[firstRoot]++;
                }

                Count--;
            }
        }
    }
}
