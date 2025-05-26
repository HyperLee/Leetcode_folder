namespace leetcode_1857;

class Program
{
    /// <summary>
    /// 1857. Largest Color Value in a Directed Graph
    /// https://leetcode.com/problems/largest-color-value-in-a-directed-graph/description/?envType=daily-question&envId=2025-05-26
    /// 
    /// 1857. 有向圖中最大顏色值
    /// https://leetcode.cn/problems/largest-color-value-in-a-directed-graph/description/?envType=daily-question&envId=2025-05-26
    /// 
    /// 給定一個有 n 個顏色節點和 m 條邊的有向圖，節點編號為 0 到 n-1。
    /// colors 是一個字串，colors[i] 代表第 i 個節點的顏色（小寫英文字母）。
    /// edges 是一個二維陣列，edges[j] = [aj, bj] 表示存在一條從 aj 指向 bj 的有向邊。
    /// 合法路徑為 x1 -> x2 -> ... -> xk，且對於每個 1 <= i < k，xi 到 xi+1 有有向邊。
    /// 路徑的顏色值為該路徑上出現次數最多的顏色的節點數。
    /// 請回傳所有合法路徑中的最大顏色值；如果圖中存在環，則回傳 -1。
    /// </summary>
    static void Main(string[] args)
    {
        // 測試資料 1
        string colors1 = "abaca";
        int[][] edges1 = new int[][] {
            new int[] {0,1},
            new int[] {0,2},
            new int[] {2,3},
            new int[] {3,4}
        };
        // 預期答案: 3

        // 測試資料 2（有環）
        string colors2 = "a";
        int[][] edges2 = new int[][] {
            new int[] {0,0}
        };
        // 預期答案: -1

        // 測試資料 3
        string colors3 = "abcde";
        int[][] edges3 = new int[][] {
        };
        // 預期答案: 1

        var prog = new Program();
        Console.WriteLine("=== 測試資料 1 ===");
        Console.WriteLine($"解法1: {prog.LargestPathValue(colors1, edges1)}");
        Console.WriteLine($"解法2: {prog.LargestPathValue2(colors1, edges1)}");
        Console.WriteLine($"解法3: {prog.LargestPathValue3(colors1, edges1)}");

        Console.WriteLine("=== 測試資料 2 (有環) ===");
        Console.WriteLine($"解法1: {prog.LargestPathValue(colors2, edges2)}");
        Console.WriteLine($"解法2: {prog.LargestPathValue2(colors2, edges2)}");
        Console.WriteLine($"解法3: {prog.LargestPathValue3(colors2, edges2)}");

        Console.WriteLine("=== 測試資料 3 ===");
        Console.WriteLine($"解法1: {prog.LargestPathValue(colors3, edges3)}");
        Console.WriteLine($"解法2: {prog.LargestPathValue2(colors3, edges3)}");
        Console.WriteLine($"解法3: {prog.LargestPathValue3(colors3, edges3)}");
    }
    

    /// <summary>
    /// 解法1：拓撲排序 + 動態規劃
    /// 思路：使用拓撲排序遍歷有向圖，同時用動態規劃記錄到達每個節點時各種顏色的最大值
    /// 時間複雜度：O(V + E)，其中 V 是節點數，E 是邊數
    /// 空間複雜度：O(V)
    /// ref:https://leetcode.cn/problems/largest-color-value-in-a-directed-graph/solutions/766070/you-xiang-tu-zhong-zui-da-yan-se-zhi-by-dmtaa/?envType=daily-question&envId=2025-05-26
    /// </summary>
    /// <param name="colors">節點顏色字串，每個字元代表一個節點的顏色</param>
    /// <param name="edges">邊的陣列，每個邊表示為 [起點, 終點]</param>
    /// <returns>有向圖中路徑上相同顏色節點的最大數量，如果有環則返回 -1</returns>
    public int LargestPathValue(string colors, int[][] edges)
    {
        int n = colors.Length;
        // 建立鄰接表表示圖結構
        int[][] graph = new int[n][];
        // 記錄每個節點的入度
        int[] indegree = new int[n];
        // 初始化圖和入度陣列
        for (int i = 0; i < n; i++)
        {
            graph[i] = new int[0];
        }

        // 建構圖並計算入度
        foreach (var edge in edges)
        {
            graph[edge[0]] = graph[edge[0]].Append(edge[1]).ToArray();
            indegree[edge[1]]++;
        }

        // 將所有入度為 0 的節點加入佇列（拓撲排序起點）
        Queue<int> queue = new Queue<int>();
        for (int i = 0; i < n; i++)
        {
            if (indegree[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        // dp[i][j] 表示到達節點 i 時，顏色 j 的最大數量
        int[][] dp = new int[n][];
        for (int i = 0; i < n; i++)
        {
            dp[i] = new int[26];
            // 初始化當前節點的顏色值為 1
            // 節點 0: 顏色 'a' -> 索引 0; dp[0] = [1, 0, 0, 0, 0, ...] // 只有索引 0 (顏色 a) 為 1
            // 節點 1: 顏色 'b' -> 索引 1; dp[1] = [0, 1, 0, 0, 0, ...] // 只有索引 1 (顏色 b) 為 1
            dp[i][colors[i] - 'a'] = 1; 
        }

        int maxColorValue = 0;
        int processedNodes = 0; // 記錄已處理的節點數，用於環檢測
        
        // 進行拓撲排序
        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            processedNodes++; // 增加已處理節點計數
            
            // 更新最大顏色值
            maxColorValue = Math.Max(maxColorValue, dp[node].Max());

            // 遍歷當前節點的所有鄰居
            foreach (var neighbor in graph[node])
            {
                // 更新鄰居節點的 dp 值
                for (int j = 0; j < 26; j++)
                {
                    // 如果鄰居節點的顏色與當前顏色相同，則 +1，否則保持原值
                    dp[neighbor][j] = Math.Max(dp[neighbor][j], dp[node][j] + (colors[neighbor] - 'a' == j ? 1 : 0));
                }
                // 減少鄰居的入度
                indegree[neighbor]--;
                // 如果鄰居的入度變為 0，則加入佇列
                if (indegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        // 如果處理的節點數不等於總節點數，說明存在環
        if (processedNodes != n)
        {
            return -1;
        }

        return maxColorValue;
    }
    

    /// <summary>
    /// 解法2：拓撲排序 + 動態規劃（改良版）
    /// 思路：使用拓撲排序檢測環，同時用動態規劃記錄到達每個節點時各種顏色的最大值
    /// 與解法1的差異：更嚴格的環檢測，確保所有節點都被處理
    /// 時間複雜度：O(V + E)，其中 V 是節點數，E 是邊數
    /// 空間複雜度：O(V)
    /// 
    /// ref:
    /// https://leetcode.cn/problems/largest-color-value-in-a-directed-graph/solutions/766070/you-xiang-tu-zhong-zui-da-yan-se-zhi-by-dmtaa/?envType=daily-question&envId=2025-05-26
    /// </summary>
    /// <param name="colors">節點顏色字串，每個字元代表一個節點的顏色</param>
    /// <param name="edges">邊的陣列，每個邊表示為 [起點, 終點]</param>
    /// <returns>有向圖中路徑上相同顏色節點的最大數量，如果有環則返回 -1</returns>
    public int LargestPathValue2(string colors, int[][] edges)
    {
        int n = colors.Length;
        // 使用 List 建立鄰接表，更靈活的圖表示方式
        List<List<int>> graph = new List<List<int>>();
        // 初始化圖的鄰接表
        for (int i = 0; i < n; i++)
        {
            graph.Add(new List<int>());
        }

        // 記錄每個節點的入度
        int[] indegree = new int[n];
        foreach (var edge in edges)
        {
            graph[edge[0]].Add(edge[1]);
            indegree[edge[1]]++;
        }

        // 記錄已處理的節點數，用於檢測環
        int processedNodes = 0;
        // dp[i][j] 表示到達節點 i 時，顏色 j 的最大數量
        int[][] dp = new int[n][];
        for (int i = 0; i < n; i++)
        {
            dp[i] = new int[26];
        }

        // 將所有入度為 0 的節點加入佇列
        Queue<int> queue = new Queue<int>();
        for (int i = 0; i < n; i++)
        {
            if (indegree[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        // 拓撲排序處理
        while (queue.Count > 0)
        {
            // 記錄已處理的節點數
            processedNodes++; 
            int u = queue.Dequeue();
            // 當前節點的顏色值 +1
            dp[u][colors[u] - 'a']++; 

            // 遍歷當前節點的所有鄰居
            foreach (int v in graph[u])
            {
                // 減少鄰居的入度
                indegree[v]--; 
                // 更新鄰居節點的顏色值
                for (int c = 0; c < 26; c++)
                {
                    dp[v][c] = Math.Max(dp[v][c], dp[u][c]);
                }
                // 如果鄰居入度變為 0，加入佇列
                if (indegree[v] == 0)
                {
                    queue.Enqueue(v);
                }
            }
        }

        // 如果處理的節點數不等於總節點數，說明存在環
        if (processedNodes != n)
        {
            return -1;
        }

        // 找出所有節點中各顏色的最大值
        int ans = 0;
        for (int i = 0; i < n; i++)
        {
            ans = Math.Max(ans, dp[i].Max());
        }
        return ans;
    }
    

    /// <summary>
    /// 解法3：深度優先搜尋 + 記憶化（DFS + Memoization）
    /// 思路：使用 DFS 遍歷每個節點，記憶化存儲中間結果，避免重複計算
    /// 優勢：能夠有效檢測自環和其他類型的環
    /// 時間複雜度：O(V + E)，其中 V 是節點數，E 是邊數
    /// 空間複雜度：O(V)
    /// 
    /// ref:https://leetcode.cn/problems/largest-color-value-in-a-directed-graph/solutions/765871/an-zhao-tuo-bu-xu-dp-by-endlesscheng-2n4g/?envType=daily-question&envId=2025-05-26
    /// </summary>
    /// <param name="colors">節點顏色字串，每個字元代表一個節點的顏色</param>
    /// <param name="edges">邊的陣列，每個邊表示為 [起點, 終點]</param>
    /// <returns>有向圖中路徑上相同顏色節點的最大數量，如果有環則返回 -1</returns>
    public int LargestPathValue3(string colors, int[][] edges)
    {
        int n = colors.Length;
        // 建立鄰接表
        List<int>[] g = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            g[i] = new List<int>();
        }

        // 建構圖並檢測自環
        foreach (int[] e in edges)
        {
            int x = e[0];
            int y = e[1];
            if (x == y) // 自環檢測
            {
                return -1;
            }
            g[x].Add(y);
        }

        int ans = 0;
        char[] cs = colors.ToCharArray();
        // memo[i] 存儲從節點 i 開始的 DFS 結果
        // null 表示未計算，空陣列表示正在計算中（用於環檢測）
        int[][] memo = new int[n][];

        // 對每個節點執行 DFS
        for (int x = 0; x < n; x++)
        {
            int[] res = Dfs(x, g, cs, memo);
            if (res.Length == 0) // 檢測到環
            {
                return -1;
            }
            // 更新以當前節點顏色結尾的路徑最大值
            ans = Math.Max(ans, res[cs[x] - 'a']);
        }
        return ans;
    }


    /// <summary>
    /// DFS 輔助方法：計算從節點 x 開始的所有路徑中各顏色的最大數量
    /// </summary>
    /// <param name="x">當前節點</param>
    /// <param name="g">圖的鄰接表表示</param>
    /// <param name="colors">節點顏色陣列</param>
    /// <param name="memo">記憶化陣列</param>
    /// <returns>從節點 x 開始各顏色的最大數量，空陣列表示有環</returns>
    private int[] Dfs(int x, List<int>[] g, char[] colors, int[][] memo)
    {
        if (memo[x] != null) // 節點 x 已經計算過或正在計算中
        {
            return memo[x]; // 如果是空陣列，表示檢測到環
        }

        memo[x] = new int[] { }; // 標記為正在計算中，用於環檢測
        int[] res = new int[26]; // 存儲各顏色的最大數量

        // 遍歷當前節點的所有鄰居
        foreach (int y in g[x])
        {
            int[] cy = Dfs(y, g, colors, memo); // 遞歸計算鄰居節點
            if (cy.Length == 0) // 鄰居節點檢測到環
            {
                return cy; // 返回空陣列，表示有環
            }
            // 更新各顏色的最大值
            for (int i = 0; i < 26; i++)
            {
                res[i] = Math.Max(res[i], cy[i]);
            }
        }

        // 將當前節點的顏色數量 +1
        res[colors[x] - 'a']++;
        return memo[x] = res; // 記憶化存儲結果，同時表示節點 x 計算完畢
    }
}
