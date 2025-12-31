namespace leetcode_1971;

class Program
{
    /// <summary>
    /// 1971. Find if Path Exists in Graph
    /// https://leetcode.com/problems/find-if-path-exists-in-graph/description/
    /// 1971. 尋找圖中是否存在路徑
    /// https://leetcode.cn/problems/find-if-path-exists-in-graph/description/
    /// 
    /// 題目（繁體中文）：
    /// 給定一個有 n 個頂點的無向圖，頂點標號從 0 到 n - 1。邊集合以二維陣列 edges 表示，
    /// 每個 edges[i] = [u_i, v_i] 表示頂點 u_i 與 v_i 之間存在一條無向邊。
    /// 每對頂點至多一條邊，且不存在自環。
    /// 給定起點 source 與終點 destination，請判斷是否存在一條從 source 到 destination 的有效路徑，
    /// 若存在則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試範例 1：存在路徑
        // 圖結構: 0 -- 1 -- 2
        //              |    |
        //              3 -- 4 -- 5
        int n1 = 6;
        int[][] edges1 = [[0, 1], [1, 2], [1, 3], [3, 4], [2, 4], [4, 5]];
        int source1 = 0;
        int destination1 = 5;
        bool result1 = program.ValidPath(n1, edges1, source1, destination1);
        Console.WriteLine($"測試 1: n={n1}, source={source1}, destination={destination1}");
        Console.WriteLine($"結果: {result1} (預期: True)");
        Console.WriteLine();

        // 測試範例 2：不存在路徑（兩個不連通的子圖）
        // 圖結構: 0 -- 1    2 -- 3
        int n2 = 4;
        int[][] edges2 = [[0, 1], [2, 3]];
        int source2 = 0;
        int destination2 = 3;
        bool result2 = program.ValidPath(n2, edges2, source2, destination2);
        Console.WriteLine($"測試 2: n={n2}, source={source2}, destination={destination2}");
        Console.WriteLine($"結果: {result2} (預期: False)");
        Console.WriteLine();

        // 測試範例 3：起點等於終點
        int n3 = 3;
        int[][] edges3 = [[0, 1], [1, 2]];
        int source3 = 1;
        int destination3 = 1;
        bool result3 = program.ValidPath(n3, edges3, source3, destination3);
        Console.WriteLine($"測試 3: n={n3}, source={source3}, destination={destination3}");
        Console.WriteLine($"結果: {result3} (預期: True)");
        Console.WriteLine();

        // 測試範例 4：單一節點
        int n4 = 1;
        int[][] edges4 = [];
        int source4 = 0;
        int destination4 = 0;
        bool result4 = program.ValidPath(n4, edges4, source4, destination4);
        Console.WriteLine($"測試 4: n={n4}, source={source4}, destination={destination4}");
        Console.WriteLine($"結果: {result4} (預期: True)");
    }

    /// <summary>
    /// 使用深度優先搜索 (DFS) 判斷無向圖中是否存在從 source 到 destination 的路徑。
    /// <para>
    /// <b>解題思路：</b>
    /// </para>
    /// <para>
    /// 1. 首先將邊集合轉換為鄰接表 (Adjacency List) 表示法，方便遍歷每個頂點的相鄰節點。
    /// </para>
    /// <para>
    /// 2. 使用 DFS 從 source 開始遞迴搜索，嘗試找到通往 destination 的路徑。
    /// </para>
    /// <para>
    /// 3. 維護一個 visited 陣列避免重複訪問同一節點（防止無限迴圈）。
    /// </para>
    /// <para>
    /// <b>時間複雜度：</b> O(V + E)，其中 V 為頂點數，E 為邊數。
    /// </para>
    /// <para>
    /// <b>空間複雜度：</b> O(V + E)，用於儲存鄰接表和訪問陣列。
    /// </para>
    /// </summary>
    /// <param name="n">圖中頂點的數量，頂點編號從 0 到 n-1</param>
    /// <param name="edges">邊的集合，每個 edges[i] = [u, v] 表示頂點 u 和 v 之間有一條無向邊</param>
    /// <param name="source">起始頂點</param>
    /// <param name="destination">目標頂點</param>
    /// <returns>若存在從 source 到 destination 的路徑則回傳 true，否則回傳 false</returns>
    /// <example>
    /// <code>
    ///  範例：0 -- 1 -- 2
    /// int n = 3;
    /// int[][] edges = [[0, 1], [1, 2]];
    /// bool result = ValidPath(n, edges, 0, 2); // 回傳 true
    /// </code>
    /// </example>
    public bool ValidPath(int n, int[][] edges, int source, int destination)
    {
        // 建立鄰接表：adj[i] 儲存與頂點 i 相鄰的所有頂點
        IList<int>[] adj = new List<int>[n];

        // 初始化每個頂點的鄰接列表
        for (int i = 0; i < n; i++)
        {
            adj[i] = new List<int>();
        }

        // 將邊轉換為鄰接表表示
        // 由於是無向圖，每條邊需要雙向加入
        foreach (var edge in edges)
        {
            int x = edge[0];
            int y = edge[1];
            adj[x].Add(y); // x -> y
            adj[y].Add(x); // y -> x（無向邊）
        }

        // 建立訪問標記陣列，防止重複訪問造成無限迴圈
        bool[] visited = new bool[n];

        // 從 source 開始進行 DFS 搜索
        return DFS(source, destination, adj, visited);
    }

    /// <summary>
    /// 深度優先搜索 (DFS) 遞迴函式，從當前頂點探索所有可能路徑尋找目標頂點。
    /// <para>
    /// <b>演算法流程：</b>
    /// </para>
    /// <para>
    /// 1. 檢查當前頂點是否為目標頂點，若是則直接回傳 true。
    /// </para>
    /// <para>
    /// 2. 將當前頂點標記為已訪問。
    /// </para>
    /// <para>
    /// 3. 遍歷當前頂點的所有相鄰且未訪問的頂點，遞迴呼叫 DFS。
    /// </para>
    /// <para>
    /// 4. 若任一路徑能到達目標，立即回傳 true；否則回傳 false。
    /// </para>
    /// </summary>
    /// <param name="source">當前正在訪問的頂點</param>
    /// <param name="destination">目標頂點</param>
    /// <param name="adj">圖的鄰接表表示</param>
    /// <param name="visited">記錄每個頂點是否已被訪問的布林陣列</param>
    /// <returns>若從 source 能到達 destination 則回傳 true，否則回傳 false</returns>
    public bool DFS(int source, int destination, IList<int>[] adj, bool[] visited)
    {
        // 終止條件：當前頂點即為目標頂點，找到路徑
        if (source == destination)
        {
            return true;
        }

        // 標記當前頂點為已訪問，避免重複訪問
        visited[source] = true;

        // 遍歷當前頂點的所有相鄰頂點
        foreach (int next in adj[source])
        {
            // 只訪問尚未被訪問過的頂點
            // 若透過此相鄰頂點能到達目標，則回傳 true
            if (!visited[next] && DFS(next, destination, adj, visited))
            {
                return true;
            }
        }

        // 所有相鄰頂點都無法到達目標，回傳 false
        return false;
    }
}
