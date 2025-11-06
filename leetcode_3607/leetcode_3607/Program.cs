namespace leetcode_3607;

/// <summary>
/// 表示電網中的一個發電站節點
/// </summary>
public class Vertex
{
    /// <summary>
    /// 發電站的唯一識別碼
    /// </summary>
    public int vertexId;
    
    /// <summary>
    /// 標記發電站是否離線（非運作狀態）
    /// </summary>
    public bool offline = false;
    
    /// <summary>
    /// 該發電站所屬的電網編號，-1 表示尚未分配
    /// </summary>
    public int powerGridId = -1;

    public Vertex() { }

    public Vertex(int id)
    {
        this.vertexId = id;
    }
}
    
class Program
{
    /// <summary>
    /// 3607. Power Grid Maintenance
    /// https://leetcode.com/problems/power-grid-maintenance/description/?envType=daily-question&envId=2025-11-06
    /// 3607. 电网维护
    /// https://leetcode.cn/problems/power-grid-maintenance/description/?envType=daily-question&envId=2025-11-06
    /// 
    /// 給定一個整數 c，表示 c 個發電站，每個發電站有一個從 1 到 c 的唯一識別碼（1 基索引）。
    /// 這些發電站通過 n 條雙向電纜連接，表示為 2D 陣列 connections，其中每個元素 connections[i] = [ui, vi] 表示發電站 ui 和 vi 之間的連接。直接或間接連接的發電站形成一個電網。
    /// 最初，所有發電站都在線（運作中）。
    /// 還給定一個 2D 陣列 queries，其中每個查詢是以下兩種類型之一：
    /// [1, x]：請求對發電站 x 進行維護檢查。如果發電站 x 在線，它自己解決檢查。如果發電站 x 離線，檢查由同一電網中識別碼最小的運作發電站解決。如果該電網中沒有運作發電站，則返回 -1。
    /// [2, x]：發電站 x 離線（即變為非運作）。
    /// 返回一個整數陣列，表示每個類型 [1, x] 查詢的結果，按它們出現的順序。
    /// 注意：電網保持其結構；離線（非運作）節點仍然是其電網的一部分，將其離線不會改變連接性。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        
        Console.WriteLine("=== 解法一：使用 Vertex 類別封裝 ===\n");
        
        // 測試案例 1: 基本電網維護查詢
        Console.WriteLine("測試案例 1:");
        int c1 = 4;
        int[][] connections1 = [[1, 2], [2, 3], [3, 4]];
        int[][] queries1 = [[1, 1], [2, 2], [1, 2], [1, 1]];
        int[] result1 = solution.ProcessQueries(c1, connections1, queries1);
        Console.WriteLine($"結果: [{string.Join(", ", result1)}]");
        Console.WriteLine($"預期: [1, 1, 1]");
        Console.WriteLine();

        // 測試案例 2: 多個電網
        Console.WriteLine("測試案例 2:");
        solution = new Program(); // 重新建立實例以重置狀態
        int c2 = 5;
        int[][] connections2 = [[1, 2], [3, 4]];
        int[][] queries2 = [[1, 1], [2, 1], [1, 1], [1, 3]];
        int[] result2 = solution.ProcessQueries(c2, connections2, queries2);
        Console.WriteLine($"結果: [{string.Join(", ", result2)}]");
        Console.WriteLine($"預期: [1, 2, 3]");
        Console.WriteLine();

        // 測試案例 3: 電網中所有發電站都離線
        Console.WriteLine("測試案例 3:");
        solution = new Program();
        int c3 = 3;
        int[][] connections3 = [[1, 2], [2, 3]];
        int[][] queries3 = [[2, 1], [2, 2], [2, 3], [1, 1]];
        int[] result3 = solution.ProcessQueries(c3, connections3, queries3);
        Console.WriteLine($"結果: [{string.Join(", ", result3)}]");
        Console.WriteLine($"預期: [-1]");
        Console.WriteLine();

        // 測試案例 4: 複雜的離線和查詢操作
        Console.WriteLine("測試案例 4:");
        solution = new Program();
        int c4 = 6;
        int[][] connections4 = [[1, 2], [2, 3], [4, 5], [5, 6]];
        int[][] queries4 = [[1, 1], [2, 1], [1, 2], [2, 2], [1, 3], [1, 4]];
        int[] result4 = solution.ProcessQueries(c4, connections4, queries4);
        Console.WriteLine($"結果: [{string.Join(", ", result4)}]");
        Console.WriteLine($"預期: [1, 2, 3, 4]");
        Console.WriteLine();

        Console.WriteLine("=== 解法二：使用陣列優化（記憶體效率更高） ===\n");

        // 測試案例 1: 基本電網維護查詢
        Console.WriteLine("測試案例 1:");
        solution = new Program();
        int[] result1V2 = solution.ProcessQueriesV2(c1, connections1, queries1);
        Console.WriteLine($"結果: [{string.Join(", ", result1V2)}]");
        Console.WriteLine($"預期: [1, 1, 1]");
        Console.WriteLine();

        // 測試案例 2: 多個電網
        Console.WriteLine("測試案例 2:");
        solution = new Program();
        int[] result2V2 = solution.ProcessQueriesV2(c2, connections2, queries2);
        Console.WriteLine($"結果: [{string.Join(", ", result2V2)}]");
        Console.WriteLine($"預期: [1, 2, 3]");
        Console.WriteLine();

        // 測試案例 3: 電網中所有發電站都離線
        Console.WriteLine("測試案例 3:");
        solution = new Program();
        int[] result3V2 = solution.ProcessQueriesV2(c3, connections3, queries3);
        Console.WriteLine($"結果: [{string.Join(", ", result3V2)}]");
        Console.WriteLine($"預期: [-1]");
        Console.WriteLine();

        // 測試案例 4: 複雜的離線和查詢操作
        Console.WriteLine("測試案例 4:");
        solution = new Program();
        int[] result4V2 = solution.ProcessQueriesV2(c4, connections4, queries4);
        Console.WriteLine($"結果: [{string.Join(", ", result4V2)}]");
        Console.WriteLine($"預期: [1, 2, 3, 4]");
    }

    private readonly List<Vertex> vertices = [];

    /// <summary>
    /// 使用深度優先搜索（DFS）遍歷電網，標記所有屬於同一個連通分量的節點
    /// </summary>
    /// <param name="u">當前訪問的節點</param>
    /// <param name="powerGridId">當前電網的識別碼</param>
    /// <param name="powerGrid">優先佇列，用於維護電網內在線發電站的最小編號（小根堆）</param>
    /// <param name="graph">圖的鄰接表表示</param>
    private void Traverse(Vertex u, int powerGridId, PriorityQueue<int, int> powerGrid, List<List<int>> graph)
    {
        // 標記當前節點屬於哪個電網
        u.powerGridId = powerGridId;
        // 將當前節點加入優先佇列，鍵和優先級都是節點編號
        powerGrid.Enqueue(u.vertexId, u.vertexId);
        
        // 遍歷所有相鄰節點
        foreach (int vid in graph[u.vertexId])
        {
            Vertex v = vertices[vid];
            // 如果相鄰節點尚未被訪問（未分配電網 ID），則遞迴訪問
            if (v.powerGridId == -1)
            {
                Traverse(v, powerGridId, powerGrid, graph);
            }
        }
    }

    /// <summary>
    /// 處理電網維護查詢
    /// 
    /// 解題思路：
    /// 1. 計算連通塊：使用 DFS 遍歷圖，將所有連通的發電站分組為不同的電網
    /// 2. 維護在線電站：使用優先佇列（小根堆）為每個電網維護在線發電站的最小編號
    /// 3. 惰性刪除：當發電站離線時，不立即從優先佇列中刪除，而是在查詢時才檢查並移除離線的發電站
    /// 4. 查詢處理：
    ///    - 類型 1 查詢：如果發電站在線，直接返回其編號；否則從優先佇列中找到最小編號的在線發電站
    ///    - 類型 2 查詢：標記發電站為離線狀態
    /// 
    /// 時間複雜度：O(n + m + q log n)，其中 n 是發電站數量，m 是連接數，q 是查詢數
    /// 空間複雜度：O(n + m)，用於儲存圖和優先佇列
    /// </summary>
    /// <param name="c">發電站總數</param>
    /// <param name="connections">發電站之間的連接關係</param>
    /// <param name="queries">查詢陣列，每個查詢是 [操作類型, 發電站編號]</param>
    /// <returns>所有類型 1 查詢的結果陣列</returns>
    /// <example>
    /// <code>
    /// int c = 4;
    /// int[][] connections = [[1, 2], [2, 3], [3, 4]];
    /// int[][] queries = [[1, 1], [2, 2], [1, 2], [1, 1]];
    /// int[] result = ProcessQueries(c, connections, queries);
    /// result = [1, 1, 1]
    /// </code>
    /// </example>
    public int[] ProcessQueries(int c, int[][] connections, int[][] queries)
    {
        // 建立圖的鄰接表表示，並初始化所有節點
        List<List<int>> graph = new List<List<int>>();
        for (int i = 0; i <= c; i++)
        {
            graph.Add(new List<int>());
            vertices.Add(new Vertex(i));
        }

        // 根據連接關係建立無向圖
        foreach (var conn in connections)
        {
            int u = conn[0];
            int v = conn[1];
            graph[u].Add(v);
            graph[v].Add(u);
        }

        // 使用 DFS 計算所有連通分量（電網）
        // 為每個電網維護一個優先佇列，儲存該電網內所有發電站的編號
        List<PriorityQueue<int, int>> powerGrids = new List<PriorityQueue<int, int>>();
        for (int i = 1, powerGridId = 0; i <= c; i++)
        {
            Vertex v = vertices[i];
            // 如果節點尚未被訪問，則作為新電網的起點
            if (v.powerGridId == -1)
            {
                var powerGrid = new PriorityQueue<int, int>();
                Traverse(v, powerGridId, powerGrid, graph);
                powerGrids.Add(powerGrid);
                powerGridId++;
            }
        }

        // 處理所有查詢
        List<int> result = new List<int>();
        foreach (var query in queries)
        {
            int op = query[0];
            int x = query[1];
            
            if (op == 1)
            {
                // 查詢操作：找到負責維護檢查的發電站
                if (!vertices[x].offline)
                {
                    // 如果發電站 x 在線，直接返回其編號
                    result.Add(x);
                }
                else
                {
                    // 如果發電站 x 離線，使用惰性刪除策略
                    // 從優先佇列中移除所有離線的發電站，直到找到在線的發電站或佇列為空
                    var powerGrid = powerGrids[vertices[x].powerGridId];
                    while (powerGrid.Count > 0 && vertices[powerGrid.Peek()].offline)
                    {
                        powerGrid.Dequeue();
                    }

                    // 如果佇列不為空，返回堆頂元素（最小編號的在線發電站）；否則返回 -1
                    result.Add(powerGrid.Count > 0 ? powerGrid.Peek() : -1);
                }
            }
            else if (op == 2)
            {
                // 離線操作：標記發電站 x 為離線狀態
                // 不立即從優先佇列中刪除，而是在查詢時進行惰性刪除
                vertices[x].offline = true;
            }
        }
        
        return result.ToArray();
    }

    /// <summary>
    /// 處理電網維護查詢 - 解法二（優化版本）
    /// 
    /// 解題思路：
    /// 1. 計算連通塊：使用 DFS 遍歷圖，將所有連通的發電站分組為不同的電網
    /// 2. 維護在線電站：使用優先佇列（小根堆）為每個電網維護在線發電站的最小編號
    /// 3. 惰性刪除：當發電站離線時，不立即從優先佇列中刪除，而是在查詢時才檢查並移除離線的發電站
    /// 4. 查詢處理：
    ///    - 類型 1 查詢：如果發電站在線，直接返回其編號；否則從優先佇列中找到最小編號的在線發電站
    ///    - 類型 2 查詢：標記發電站為離線狀態
    /// 
    /// 與解法一的差異：
    /// - 使用陣列而非類別物件來管理節點狀態，減少記憶體開銷
    /// - 預先計算答案陣列大小，避免動態擴容
    /// - 使用更簡潔的資料結構，提升執行效率
    /// 
    /// 時間複雜度：O(n + m + q log n)，其中 n 是發電站數量，m 是連接數，q 是查詢數
    /// 空間複雜度：O(n + m)，用於儲存圖和優先佇列
    /// </summary>
    /// <param name="c">發電站總數</param>
    /// <param name="connections">發電站之間的連接關係</param>
    /// <param name="queries">查詢陣列，每個查詢是 [操作類型, 發電站編號]</param>
    /// <returns>所有類型 1 查詢的結果陣列</returns>
    /// <example>
    /// <code>
    /// int c = 4;
    /// int[][] connections = [[1, 2], [2, 3], [3, 4]];
    /// int[][] queries = [[1, 1], [2, 2], [1, 2], [1, 1]];
    /// int[] result = ProcessQueriesV2(c, connections, queries);
    /// result = [1, 1, 1]
    /// </code>
    /// </example>
    public int[] ProcessQueriesV2(int c, int[][] connections, int[][] queries)
    {
        // 建立圖的鄰接表表示
        List<int>[] graph = new List<int>[c + 1];
        for (int i = 0; i <= c; i++)
        {
            graph[i] = new List<int>();
        }

        // 根據連接關係建立無向圖
        foreach (var edge in connections)
        {
            int x = edge[0];
            int y = edge[1];
            graph[x].Add(y);
            graph[y].Add(x);
        }

        // belong[i] 記錄節點 i 所屬的電網（連通分量）編號
        // -1 表示尚未訪問
        int[] belong = new int[c + 1];
        Array.Fill(belong, -1);

        // 為每個連通分量（電網）建立一個優先佇列
        List<PriorityQueue<int, int>> heaps = new List<PriorityQueue<int, int>>();

        // 使用 DFS 計算所有連通分量
        for (int i = 1; i <= c; i++)
        {
            // 如果節點已經被訪問過，跳過
            if (belong[i] >= 0)
            {
                continue;
            }

            // 為新的連通分量建立優先佇列
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            // 從節點 i 開始 DFS，將所有連通的節點加入堆中
            DfsV2(i, graph, belong, heaps.Count, pq);
            // 將該連通分量的堆加入列表
            heaps.Add(pq);
        }

        // 預先計算答案陣列的大小（類型 1 查詢的數量）
        // 這樣可以避免動態擴容，提升效率
        int ansSize = 0;
        foreach (var q in queries)
        {
            if (q[0] == 1)
            {
                ansSize++;
            }
        }

        // 建立答案陣列
        int[] ans = new int[ansSize];
        int idx = 0;

        // offline[i] 標記發電站 i 是否離線
        bool[] offline = new bool[c + 1];

        // 處理所有查詢
        foreach (var q in queries)
        {
            int x = q[1];

            if (q[0] == 2)
            {
                // 類型 2：發電站 x 離線
                // 只標記狀態，不從堆中刪除（惰性刪除策略）
                offline[x] = true;
                continue;
            }

            // 類型 1：查詢發電站 x 的維護檢查由誰負責
            if (!offline[x])
            {
                // 如果發電站 x 在線，直接返回 x
                ans[idx++] = x;
                continue;
            }

            // 發電站 x 離線，需要找到同一電網中最小編號的在線發電站
            // 獲取 x 所屬電網的優先佇列
            PriorityQueue<int, int> pq = heaps[belong[x]];

            // 惰性刪除：從堆頂開始，移除所有離線的發電站
            // 直到找到在線的發電站或堆為空
            while (pq.Count > 0 && offline[pq.Peek()])
            {
                pq.Dequeue();
            }

            // 如果堆不為空，返回堆頂元素（最小編號的在線發電站）
            // 否則返回 -1（該電網中沒有在線的發電站）
            ans[idx++] = pq.Count > 0 ? pq.Peek() : -1;
        }

        return ans;
    }

    /// <summary>
    /// 深度優先搜索（DFS）遍歷連通分量
    /// </summary>
    /// <param name="x">當前訪問的節點</param>
    /// <param name="graph">圖的鄰接表表示</param>
    /// <param name="belong">記錄每個節點所屬的電網編號</param>
    /// <param name="compId">當前連通分量（電網）的識別碼</param>
    /// <param name="pq">當前連通分量的優先佇列</param>
    private void DfsV2(int x, List<int>[] graph, int[] belong, int compId, PriorityQueue<int, int> pq)
    {
        // 記錄節點 x 屬於哪個電網（連通分量）
        belong[x] = compId;
        // 將節點 x 加入優先佇列，編號同時作為元素和優先級
        pq.Enqueue(x, x);

        // 遍歷所有相鄰節點
        foreach (int y in graph[x])
        {
            // 如果相鄰節點尚未被訪問（belong[y] < 0），則遞迴訪問
            if (belong[y] < 0)
            {
                DfsV2(y, graph, belong, compId, pq);
            }
        }
    }

}
