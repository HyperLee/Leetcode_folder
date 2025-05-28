namespace leetcode_3372;

class Program
{
    /// <summary>
    /// 主程式進入點，包含測試資料與兩種解法的驗證。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        // 測試資料：
        // Tree1: 0-1-2
        // Tree2: 0-1
        // k = 2
        int[][] edges1 = new int[][]
        {
            new int[] { 0, 1 },
            new int[] { 1, 2 }
        };
        int[][] edges2 = new int[][]
        {
            new int[] { 0, 1 }
        };
        int k = 2;

        // 解法1
        var prog = new Program();
        int[] ans1 = prog.MaxTargetNodes(edges1, edges2, k);
        Console.WriteLine("解法1結果: [" + string.Join(", ", ans1) + "]");

        // 解法2
        int n = edges1.Length + 1;
        int m = edges2.Length + 1;
        int[] ans2 = prog.MaximumNodes2(n, m, edges1, edges2, k);
        Console.WriteLine("解法2結果: [" + string.Join(", ", ans2) + "]");

        // 驗證兩解法結果是否一致
        bool same = ans1.Length == ans2.Length && ans1.Zip(ans2, (a, b) => a == b).All(x => x);
        Console.WriteLine($"兩解法結果一致: {same}");
    }


    /// <summary>
    /// 解法1:使用 DFS 計算每個節點在自身樹內距離 k 內可達的節點數，然後找出第二棵樹中距離 k-1 可達的最大節點數。
    ///       計算每個第一棵樹節點，連接第二棵樹任一節點後，最多可達到的目標節點數量。
    /// 
    /// 注意:計算第二顆樹距離是 k - 1，因為連接(題目有說 tree1 與 tree2 連接計算)邊長為 1，所以實際上是 k - 1。
    ///      要扣除連接的那段距離 1
    /// </summary>
    /// <param name="edges1">第一棵樹的邊資訊</param>
    /// <param name="edges2">第二棵樹的邊資訊</param>
    /// <param name="k">最大距離</param>
    /// <returns>回傳長度為 n 的整數陣列，answer[i] 表示節點 i 最多可達的目標節點數</returns>
    public int[] MaxTargetNodes(int[][] edges1, int[][] edges2, int k)
    {
        // n 為第一棵樹節點數，m 為第二棵樹節點數
        // 節點數 = 邊數 + 1
        int n = edges1.Length + 1;
        int m = edges2.Length + 1;
        // count1[i]：第一棵樹每個節點在自身樹內距離 <= k 可達的節點數
        int[] count1 = Build(edges1, k);
        // count2[i]：第二棵樹每個節點在自身樹內距離 <= k-1 可達的節點數
        int[] count2 = Build(edges2, k - 1);
        // 找出第二棵樹中，距離 <= k-1 可達的最大節點數
        int maxCount2 = 0;
        foreach (int c in count2)
        {
            if (c > maxCount2)
            {
                maxCount2 = c;
            }
        }

        // 對於第一棵樹每個節點，最大目標節點數 = 自己樹內可達 + 第二棵樹最大可達
        int[] res = new int[n];
        for (int i = 0; i < n; i++)
        {
            res[i] = count1[i] + maxCount2;
        }

        return res;
    }


    /// <summary>
    /// 計算每個節點在距離 k 內可達的節點數（包含自己），用於單棵樹。
    /// </summary>
    /// <param name="edges">樹的邊資訊</param>
    /// <param name="k">最大距離</param>
    /// <returns>每個節點在距離 k 內可達的節點數陣列</returns>
    private int[] Build(int[][] edges, int k)
    {
        // 節點數 = 邊數 + 1
        int n = edges.Length + 1;
        // 建立鄰接串列
        List<List<int>> children = new List<List<int>>(n);
        // 初始化每個節點的子節點列表
        for (int i = 0; i < n; i++)
        {
            children.Add(new List<int>());
        }

        // 將每條邊加入鄰接串列
        foreach (var edge in edges)
        {
            int a = edge[0];
            int b = edge[1];
            children[a].Add(b);
            children[b].Add(a);
        }

        int[] res = new int[n];
        // 對每個節點進行 DFS，計算距離 k 內可達節點數
        for (int i = 0; i < n; i++)
        {
            res[i] = Dfs(i, -1, children, k);
        }

        return res;
    }


    /// <summary>
    /// 使用 DFS 計算從 node 出發，距離不超過 k 的可達節點數（包含自己）。
    /// </summary>
    /// <param name="node">目前節點</param>
    /// <param name="parent">父節點，避免回頭</param>
    /// <param name="children">鄰接串列</param>
    /// <param name="k">剩餘可走步數</param>
    /// <returns>可達節點數</returns>
    private int Dfs(int node, int parent, List<List<int>> children, int k)
    {
        if (k < 0)
        {
            // 超過距離限制，回傳 0
            return 0;
        }

        int count = 1; // 包含自己
        foreach (int child in children[node])
        {
            if (child != parent)
            {
                // 遞迴搜尋子節點，距離減 1
                count += Dfs(child, node, children, k - 1);
            }
        }

        return count;
    }


    /// <summary>
    /// 解法2: 全節點對全節點距離 DFS，窮舉所有連接方式，計算每個 Tree1 節點最大目標節點數。
    /// 
    /// </summary>
    /// <param name="n">Tree1 節點數</param>
    /// <param name="m">Tree2 節點數</param>
    /// <param name="edges1">Tree1 邊資訊</param>
    /// <param name="edges2">Tree2 邊資訊</param>
    /// <param name="k">最大距離</param>
    /// <returns>回傳長度為 n 的整數陣列，answer[i] 表示節點 i 最多可達的目標節點數</returns>
    public int[] MaximumNodes2(int n, int m, int[][] edges1, int[][] edges2, int k)
    {
        // 建立兩棵樹的鄰接表
        var graph1 = BuildGraph(n, edges1);
        var graph2 = BuildGraph(m, edges2);

        // 計算兩棵樹所有節點對之間的距離
        var dist1 = AllPairDistancesDFS(graph1, n);
        var dist2 = AllPairDistancesDFS(graph2, m);

        int[] res = new int[n];
        for (int i = 0; i < n; i++)
        {
            int maxCount = 0;
            // 枚舉所有連接方式 (Tree1 的 u, Tree2 的 v)
            for (int u = 0; u < n; u++)
            {
                for (int v = 0; v < m; v++)
                {
                    int count = 0;

                    // Tree1 中距離 i ≤ k 的節點
                    for (int a = 0; a < n; a++)
                    {
                        if (dist1[a][i] <= k)
                        {
                            count++;
                        }
                    }

                    // Tree2 中滿足 dist(b,v) + dist(u,i) + 1 ≤ k 的節點
                    for (int b = 0; b < m; b++)
                    {
                        int path = dist2[b][v] + dist1[u][i] + 1; // 連接邊長為1
                        if (path <= k)
                        {
                            count++;
                        }
                    }

                    // 取最大目標節點數
                    maxCount = Math.Max(maxCount, count);
                }
            }
            res[i] = maxCount;
        }
        return res;
    }


    /// <summary>
    /// 建立無向圖的鄰接表。
    /// </summary>
    /// <param name="size">節點數</param>
    /// <param name="edges">邊資訊</param>
    /// <returns>鄰接表</returns>
    private List<int>[] BuildGraph(int size, int[][] edges)
    {
        var graph = new List<int>[size];
        for (int i = 0; i < size; i++)
            graph[i] = new List<int>();

        // 加入每條邊
        foreach (var edge in edges)
        {
            int u = edge[0], v = edge[1];
            graph[u].Add(v);
            graph[v].Add(u);
        }

        return graph;
    }


    /// <summary>
    /// 計算所有節點對之間的距離（每個節點出發 DFS 一次）。
    /// </summary>
    /// <param name="graph">鄰接表</param>
    /// <param name="size">節點數</param>
    /// <returns>dist[i][j] 表示 i 到 j 的距離</returns>
    private int[][] AllPairDistancesDFS(List<int>[] graph, int size)
    {
        var dist = new int[size][];
        for (int i = 0; i < size; i++)
        {
            dist[i] = new int[size];
            // 初始化距離為最大值
            Array.Fill(dist[i], int.MaxValue);

            // 從 i 出發 DFS，記錄到每個節點的距離
            DFS(graph, i, -1, 0, dist[i]);
        }

        return dist;
    }


    /// <summary>
    /// DFS：從 current 出發，記錄到每個節點的距離。
    /// </summary>
    /// <param name="graph">鄰接表</param>
    /// <param name="current">目前節點</param>
    /// <param name="parent">父節點，避免回頭</param>
    /// <param name="depth">目前深度</param>
    /// <param name="dist">距離陣列</param>
    private void DFS(List<int>[] graph, int current, int parent, int depth, int[] dist)
    {
        dist[current] = depth; // 記錄目前節點距離

        foreach (var neighbor in graph[current])
        {
            if (neighbor != parent)
            {
                // 遞迴搜尋子節點，深度加1
                DFS(graph, neighbor, current, depth + 1, dist);
            }
        }
    }
}
