namespace leetcode_3650;

class Program
{
    /// <summary>
    /// 3650. Minimum Cost Path with Edge Reversals
    /// https://leetcode.com/problems/minimum-cost-path-with-edge-reversals/description/?envType=daily-question&envId=2026-01-27
    /// 3650. 邊反轉的最小路徑總成本
    /// https://leetcode.cn/problems/minimum-cost-path-with-edge-reversals/description/?envType=daily-question&envId=2026-01-27
    ///
    /// English:
    /// You are given a directed, weighted graph with n nodes labeled from 0 to n - 1, and an array edges where edges[i] = [ui, vi, wi] represents a directed edge from node ui to node vi with cost wi.
    /// Each node ui has a switch that can be used at most once: when you arrive at ui and have not yet used its switch, you may activate it on one of its incoming edges vi → ui, reverse that edge to ui → vi and immediately traverse it.
    /// The reversal is only valid for that single move, and using a reversed edge costs 2 * wi.
    /// Return the minimum total cost to travel from node 0 to node n - 1. If it is not possible, return -1.
    ///
    /// 繁體中文（中文版）:
    /// 給你一個有向加權圖，節點標號為 0 到 n - 1，edges 陣列中 edges[i] = [ui, vi, wi] 表示從節點 ui 到節點 vi 的有向邊，權重（成本）為 wi。
    /// 每個節點 ui 都有一個開關且最多可使用一次：當你抵達 ui 且尚未使用過其開關時，你可以在其中一條指向 ui 的入邊 vi → ui 上啟動開關，將該邊反向為 ui → vi 並立即沿此邊移動。
    /// 該反轉僅對該次移動有效；使用反轉邊的成本為 2 * wi。
    /// 返回從節點 0 到節點 n - 1 的最小總成本；若無法到達則返回 -1。
    ///
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：基本圖形，從 0 到 2
        // 圖形：0 -> 1 (cost: 1), 1 -> 2 (cost: 2)
        // 最短路徑：0 -> 1 -> 2，成本為 1 + 2 = 3
        int[][] edges1 = [[0, 1, 1], [1, 2, 2]];
        int result1 = solution.MinCost(3, edges1);
        Console.WriteLine($"測試案例 1：n=3, edges=[[0,1,1],[1,2,2]]");
        Console.WriteLine($"預期結果：3，實際結果：{result1}");
        Console.WriteLine();

        // 測試案例 2：需要反轉邊的情況
        // 圖形：1 -> 0 (cost: 5), 1 -> 2 (cost: 3)
        // 從 0 出發無法直接到達 1，需反轉邊 1->0 為 0->1，成本為 2*5=10
        // 最短路徑：0 -> 1 (反轉，成本 10) -> 2 (成本 3) = 13
        int[][] edges2 = [[1, 0, 5], [1, 2, 3]];
        int result2 = solution.MinCost(3, edges2);
        Console.WriteLine($"測試案例 2：n=3, edges=[[1,0,5],[1,2,3]]");
        Console.WriteLine($"預期結果：13，實際結果：{result2}");
        Console.WriteLine();

        // 測試案例 3：無法到達終點
        // 圖形：0 -> 1 (cost: 1)，但沒有通往節點 2 的邊
        int[][] edges3 = [[0, 1, 1]];
        int result3 = solution.MinCost(3, edges3);
        Console.WriteLine($"測試案例 3：n=3, edges=[[0,1,1]]");
        Console.WriteLine($"預期結果：-1，實際結果：{result3}");
        Console.WriteLine();

        // 測試案例 4：多條路徑選擇最短
        // 圖形：0 -> 1 (cost: 10), 0 -> 2 (cost: 1), 2 -> 1 (cost: 1)
        int[][] edges4 = [[0, 1, 10], [0, 2, 1], [2, 1, 1]];
        int result4 = solution.MinCost(2, edges4);
        Console.WriteLine($"測試案例 4：n=2, edges=[[0,1,10],[0,2,1],[2,1,1]]");
        Console.WriteLine($"預期結果：10，實際結果：{result4}");
    }

    /// <summary>
    /// 使用 Dijkstra 演算法計算從節點 0 到節點 n-1 的最小成本路徑。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 本題的關鍵在於理解「邊反轉」機制：每個節點的開關最多使用一次，且反轉僅對該次移動有效。
    /// 由於 Dijkstra 演算法中每個點最多只會被遍歷一次（第一次出堆即為最短距離），
    /// 因此我們不需要額外追蹤開關的使用狀態。
    /// </para>
    /// 
    /// <para><b>核心轉換：</b></para>
    /// <para>
    /// 將原始邊 [x, y, w] 視為兩條邊：
    /// <list type="bullet">
    ///   <item>正向邊：x → y，成本為 w（正常走）</item>
    ///   <item>反向邊：y → x，成本為 2w（使用開關反轉後走）</item>
    /// </list>
    /// 這樣問題就轉化為標準的單源最短路徑問題。
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(m log m)，其中 m 為邊的數量。</para>
    /// <para><b>空間複雜度：</b>O(n + m)，用於儲存圖和距離陣列。</para>
    /// </summary>
    /// <param name="n">圖中節點的數量（節點編號為 0 到 n-1）</param>
    /// <param name="edges">邊的陣列，每條邊格式為 [起點, 終點, 權重]</param>
    /// <returns>從節點 0 到節點 n-1 的最小成本；若無法到達則返回 -1</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int[][] edges = [[0, 1, 1], [1, 2, 2]];
    /// int result = solution.MinCost(3, edges); // 返回 3
    /// </code>
    /// </example>
    public int MinCost(int n, int[][] edges)
    {
        // 建立鄰接表表示圖，每個節點儲存其相鄰節點和對應的邊權重
        var g = new List<(int node, int weight)>[n];
        for (int i = 0; i < n; i++)
        {
            g[i] = new List<(int, int)>();
        }

        // 建圖：對於每條邊 [x, y, w]，同時加入正向邊和反向邊
        foreach (var e in edges)
        {
            int x = e[0];  // 起點
            int y = e[1];  // 終點
            int w = e[2];  // 權重（成本）

            // 正向邊：從 x 到 y，成本為 w
            g[x].Add((y, w));

            // 反向邊：從 y 到 x（相當於在 y 點使用開關反轉邊），成本為 2w
            g[y].Add((x, 2 * w));
        }

        // dist[i] 記錄從起點 0 到節點 i 的最短距離
        int[] dist = new int[n];

        // visited[i] 標記節點 i 是否已經確定最短路徑
        bool[] visited = new bool[n];

        // 初始化所有距離為無限大
        Array.Fill(dist, int.MaxValue);

        // 起點到自身的距離為 0
        dist[0] = 0;

        // 使用最小堆優化 Dijkstra，優先處理距離最小的節點
        // 堆中元素為 (當前距離, 節點編號)，優先級為當前距離
        var pq = new PriorityQueue<(int dist, int node), int>();
        pq.Enqueue((0, 0), 0);

        // Dijkstra 主迴圈
        while (pq.Count > 0)
        {
            // 取出當前距離最小的節點
            var current = pq.Dequeue();
            int currentDist = current.dist;
            int x = current.node;

            // 若已到達終點，直接返回結果（因為 Dijkstra 保證第一次到達即為最短）
            if (x == n - 1)
            {
                return currentDist;
            }

            // 若該節點已處理過，跳過（同一節點可能因更新而多次入堆）
            if (visited[x])
            {
                continue;
            }

            // 標記該節點已確定最短路徑
            visited[x] = true;

            // 鬆弛操作：嘗試透過當前節點更新其鄰居的最短距離
            foreach (var neighbor in g[x])
            {
                int y = neighbor.node;    // 鄰居節點
                int w = neighbor.weight;  // 邊的權重

                // 若經由當前節點到達鄰居的路徑更短，則更新距離
                if (currentDist + w < dist[y])
                {
                    dist[y] = currentDist + w;
                    pq.Enqueue((dist[y], y), dist[y]);
                }
            }
        }

        // 若無法到達終點，返回 -1
        return -1;
    }
}
