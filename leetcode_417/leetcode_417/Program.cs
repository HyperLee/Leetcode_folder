namespace leetcode_417;

class Program
{
    /// <summary>
    /// 417. Pacific Atlantic Water Flow
    /// https://leetcode.com/problems/pacific-atlantic-water-flow/description/
    /// <para>
    /// There is an m x n rectangular island bordering both the Pacific and Atlantic oceans. The Pacific touches the island's left and top edges, while the Atlantic touches its right and bottom edges.
    ///
    /// The island is partitioned into square cells. You are given an m x n integer matrix heights where heights[r][c] is the height above sea level of cell (r, c).
    ///
    /// Rain water can flow north, south, east, or west to a neighboring cell whose height is less than or equal to the current cell's height. Water can flow from any cell adjacent to an ocean into that ocean.
    ///
    /// Return a 2D list result where result[i] = [r_i, c_i] means rain water can flow from cell (r_i, c_i) to both oceans.
    ///
    /// Example 1:
    /// Image: https://assets.leetcode.com/uploads/2021/06/08/waterflow-grid.jpg
    /// Input: heights = [[1,2,2,3,5],[3,2,3,4,4],[2,4,5,3,1],[6,7,1,4,5],[5,1,1,2,4]]
    /// Output: [[0,4],[1,3],[1,4],[2,2],[3,0],[3,1],[4,0]]
    /// Explanation: [0,4] flows directly to both oceans. [1,3] flows through [0,3] to the Pacific and [1,4] to the Atlantic. [1,4] flows through [1,3] and [0,3] to the Pacific and directly to the Atlantic. [2,2] flows through [1,2] and [0,2] to the Pacific and through [2,3] and [2,4] to the Atlantic. [3,0] flows directly to the Pacific and through [4,0] to the Atlantic. [3,1] flows through [3,0] to the Pacific and [4,1] to the Atlantic. [4,0] flows directly to both oceans.
    ///
    /// Example 2:
    /// Input: heights = [[1]]
    /// Output: [[0,0]]
    /// Explanation: Water can flow from the only cell to both oceans.
    ///
    /// Constraints:
    /// - m == heights.length
    /// - n == heights[r].length
    /// - 1 &lt;= m, n &lt;= 200
    /// - 0 &lt;= heights[r][c] &lt;= 10^5
    /// </para>
    /// <para>
    /// 417. 太平洋大西洋水流問題
    /// https://leetcode.cn/problems/pacific-atlantic-water-flow/description/
    ///
    /// 有一座 m x n 的長方形島嶼，同時與太平洋和大西洋相鄰。太平洋接觸島嶼的左邊與上邊，大西洋接觸右邊與下邊。
    ///
    /// 島嶼被劃分為方形格子。給定 m x n 整數矩陣 heights，其中 heights[r][c] 表示格子 (r, c) 的海拔高度。
    ///
    /// 雨水可以向北、南、東或西流到高度小於或等於目前格子的相鄰格子。任何與海洋相鄰格子的水都能流入該海洋。
    ///
    /// 回傳二維清單 result，其中 result[i] = [r_i, c_i] 表示雨水可以從格子 (r_i, c_i) 流向兩個海洋。
    ///
    /// 範例 1：
    /// 圖片：https://assets.leetcode.com/uploads/2021/06/08/waterflow-grid.jpg
    /// 輸入：heights = [[1,2,2,3,5],[3,2,3,4,4],[2,4,5,3,1],[6,7,1,4,5],[5,1,1,2,4]]
    /// 輸出：[[0,4],[1,3],[1,4],[2,2],[3,0],[3,1],[4,0]]
    /// 解釋：[0,4] 可直接流向兩個海洋。[1,3] 經 [0,3] 流向太平洋，經 [1,4] 流向大西洋。[1,4] 經 [1,3] 與 [0,3] 流向太平洋，並直接流向大西洋。[2,2] 經 [1,2] 與 [0,2] 流向太平洋，經 [2,3] 與 [2,4] 流向大西洋。[3,0] 直接流向太平洋，並經 [4,0] 流向大西洋。[3,1] 經 [3,0] 流向太平洋，經 [4,1] 流向大西洋。[4,0] 可直接流向兩個海洋。
    ///
    /// 範例 2：
    /// 輸入：heights = [[1]]
    /// 輸出：[[0,0]]
    /// 解釋：水可以從唯一的格子流向兩個海洋。
    ///
    /// 限制條件：
    /// - m == heights.length
    /// - n == heights[r].length
    /// - 1 &lt;= m, n &lt;= 200
    /// - 0 &lt;= heights[r][c] &lt;= 10^5
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試案例 1: 基本測試
        int[][] heights1 = new int[][]
        {
            new int[] {1, 2, 2, 3, 5},
            new int[] {3, 2, 3, 4, 4},
            new int[] {2, 4, 5, 3, 1},
            new int[] {6, 7, 1, 4, 5},
            new int[] {5, 1, 1, 2, 4}
        };
        var result1 = PacificAtlantic(heights1);
        Console.WriteLine("測試案例 1 結果:");
        PrintResult(result1);

        // 測試案例 2: 小矩陣測試
        int[][] heights2 = new int[][]
        {
            new int[] {2, 1},
            new int[] {1, 2}
        };
        var result2 = PacificAtlantic(heights2);
        Console.WriteLine("\n測試案例 2 結果:");
        PrintResult(result2);
    }

    // 輔助函數：打印結果
    private static void PrintResult(IList<IList<int>> result)
    {
        Console.WriteLine($"找到 {result.Count} 個位置可以同時流向太平洋和大西洋：");
        foreach (var coordinate in result)
        {
            Console.WriteLine($"[{coordinate[0]}, {coordinate[1]}]");
        }
    }

    /// <summary>
    /// 解題思路：
    /// 1. 從太平洋邊界(左邊界和上邊界)開始 DFS，記錄所有可以流向太平洋的位置
    /// 2. 從大西洋邊界(右邊界和下邊界)開始 DFS，記錄所有可以流向大西洋的位置
    /// 3. 最後找出同時可以流向兩個海洋的位置
    /// 
    /// 時間複雜度：O(m*n)，其中 m 和 n 分別是矩陣的行數和列數
    /// 空間複雜度：O(m*n)，需要兩個訪問數組來記錄是否可以到達兩個海洋
    /// 
    /// DFS 呼叫位置	    對應邊界	          所屬海洋
    /// DFS(i, 0, ...)	   左邊界（第0欄）	      太平洋
    /// DFS(i, n-1, ...)   右邊界（最後一欄）	  大西洋
    /// DFS(0, j, ...)	   上邊界（第0列）	      太平洋
    /// DFS(m-1, j, ...)   下邊界（最後一列）	  大西洋
    /// 
    /// </summary>
    /// <param name="heights">表示地形高度的二維數組</param>
    /// <returns>返回能同時流向太平洋和大西洋的座標列表</returns>
    public static IList<IList<int>> PacificAtlantic(int[][] heights) 
    {
        int m = heights.Length;
        int n = heights[0].Length;
        bool[,] pacificVisited = new bool[m, n];
        bool[,] atlanticVisited = new bool[m, n];
        IList<IList<int>> result = new List<IList<int>>();

        // Step 1: 太平洋 - 左邊 + 上邊
        for (int i = 0; i < m; i++)    
        {
            DFS(heights, i, 0, pacificVisited, heights[i][0]);    // 左, 這是從左邊界（第 0 欄）每一列的格子出發
        }
        for (int j = 0; j < n; j++)    
        {
            DFS(heights, 0, j, pacificVisited, heights[0][j]);    // 上, 這是從上邊界（第 0 行）每一列的格子出發
        }

        // Step 2: 大西洋 - 右邊 + 下邊
        for (int i = 0; i < m; i++)    
        {
            DFS(heights, i, n - 1, atlanticVisited, heights[i][n - 1]); // 右, 這是從右邊界（最後一欄）每一列的格子出發
        }
        for (int j = 0; j < n; j++)    
        {
            DFS(heights, m - 1, j, atlanticVisited, heights[m - 1][j]); // 下, 這是從下邊界（最後一行）每一列的格子出發
        }

        // 找出同時可以流向兩個海洋的位置
        // 走訪每一列（row）
        for (int i = 0; i < m; i++) 
        {
            // 走訪每一欄（column）
            for (int j = 0; j < n; j++) 
            {
                // 如果當前位置同時可以流向太平洋和大西洋(兩個陣列取交集)，則加入結果列表
                // pacificVisited[i, j] = true 代表可以流向太平洋
                // atlanticVisited[i, j] = true 代表可以流向大西洋
                if (pacificVisited[i, j] && atlanticVisited[i, j]) 
                {
                    // 將符合條件的座標加入結果列表
                    result.Add(new List<int> { i, j });
                }
            }
        }

        return result;
    }


    /// <summary>
    /// 遍歷方向, 四個方向順序可以變更. 不影響結果
    /// 方向陣列只是定義搜索的方向
    /// DFS 會遍歷所有可能的路徑
    /// </summary> 
    public static int[][] directions = new int[][]
    {
        new int[] {1, 0},   // 下
        new int[] {-1, 0},  // 上
        new int[] {0, 1},   // 右
        new int[] {0, -1}   // 左
    };

    /// <summary>
    /// 深度優先搜索 (DFS) 實現：
    /// 1. 從邊界開始，向四個方向探索
    /// 2. 只有當下一個位置的高度大於或等於當前高度時才能流動
    /// 3. 使用 visited 數組標記已訪問的位置，避免重複訪問
    /// 
    /// 判斷條件：
    /// - 確保不超出邊界
    /// - 確保未被訪問
    /// - 確保水能流動（新位置高度 >= 當前高度）
    /// 
    /// heights[r][c] < prevHeight
    /// 只往「高度不小於目前格子」的方向走（因為水只能往下或平的方向流）
    /// 這邊是反向看(反推,從低處往高處找), 所以要找比當下高的才可以
    /// </summary>
    /// <param name="heights">地形高度二維數組</param>
    /// <param name="r">當前行索引</param>
    /// <param name="c">當前列索引</param>
    /// <param name="visited">訪問標記數組</param>
    /// <param name="prevHeight">前一個位置的高度</param>
    private static void DFS(int[][] heights, int r, int c, bool[,] visited, int prevHeight) 
    {
        int m = heights.Length;
        int n = heights[0].Length;
        
        // 邊界檢查：超出範圍、已訪問、或不符合流動條件時返回
        // 不會每個 cell 都走過，符合下列條件才可以, 減少複雜度
        if (r < 0 || r >= m || c < 0 || c >= n || visited[r, c] || heights[r][c] < prevHeight)
        {
            return;
        }

        // 標記當前位置為已訪問
        visited[r, c] = true;

        // 使用方向數組進行四個方向的搜索
        // directions[0] = 下; directions[1] = 上; directions[2] = 右; directions[3] = 左
        foreach (var dir in directions)
        {
            int newR = r + dir[0];
            int newC = c + dir[1];
            DFS(heights, newR, newC, visited, heights[r][c]);
        }
    }
}
