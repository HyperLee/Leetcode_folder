namespace leetcode_417;

class Program
{
    /// <summary>
    /// 417. Pacific Atlantic Water Flow
    /// https://leetcode.com/problems/pacific-atlantic-water-flow/description/?envType=problem-list-v2&envId=oizxjoit
    /// 417. 太平洋大西洋水流问题
    /// https://leetcode.cn/problems/pacific-atlantic-water-flow/description/
    /// 
    /// 給定一個 m x n 的矩陣 heights 表示地形的高度，水流可以從某個單元格流向上下左右相鄰的單元格，前提是相鄰單元格的高度不高於當前單元格的高度。
    /// 水流可以從矩陣的邊界流出，並且水流可以同時流向太平洋和大西洋。請找出所有能同時流向太平洋和大西洋的單元格的坐標。
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
    /// 1. 從太平洋邊界(左邊界和上邊界)開始DFS，記錄所有可以流向太平洋的位置
    /// 2. 從大西洋邊界(右邊界和下邊界)開始DFS，記錄所有可以流向大西洋的位置
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

        // 從太平洋邊界（左邊和上邊）開始DFS
        for (int i = 0; i < m; i++) 
        {
            DFS(heights, i, 0, pacificVisited, heights[i][0]);  // 從左邊（第0欄）出發
            DFS(heights, i, n - 1, atlanticVisited, heights[i][n - 1]);  // 從右邊（最後一欄）出發
        }

        // 從大西洋邊界（右邊和下邊）開始DFS
        for (int j = 0; j < n; j++) 
        {
            DFS(heights, 0, j, pacificVisited, heights[0][j]);  // 上邊界（第0列）
            DFS(heights, m - 1, j, atlanticVisited, heights[m - 1][j]);  // 下邊界（最後一列）
        }

        // 找出同時可以流向兩個海洋的位置
        for (int i = 0; i < m; i++) 
        {
            for (int j = 0; j < n; j++) 
            {
                if (pacificVisited[i, j] && atlanticVisited[i, j]) 
                {
                    result.Add(new List<int> { i, j });
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 深度優先搜索 (DFS) 實現：
    /// 1. 從邊界開始，向四個方向探索
    /// 2. 只有當下一個位置的高度大於或等於當前高度時才能流動
    /// 3. 使用visited數組標記已訪問的位置，避免重複訪問
    /// 
    /// 判斷條件：
    /// - 確保不超出邊界
    /// - 確保未被訪問
    /// - 確保水能流動（新位置高度 >= 當前高度）
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
        if (r < 0 || r >= m || c < 0 || c >= n || visited[r, c] || heights[r][c] < prevHeight)
        {
            return;
        }

        // 標記當前位置為已訪問
        visited[r, c] = true;

        // 向四個方向繼續搜索
        DFS(heights, r + 1, c, visited, heights[r][c]);
        DFS(heights, r - 1, c, visited, heights[r][c]);
        DFS(heights, r, c + 1, visited, heights[r][c]);
        DFS(heights, r, c - 1, visited, heights[r][c]);
    }
}
