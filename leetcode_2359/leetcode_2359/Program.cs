namespace leetcode_2359;

class Program
{
    /// <summary>
    /// 2359. Find Closest Node to Given Two Nodes
    /// https://leetcode.com/problems/find-closest-node-to-given-two-nodes/description/?envType=daily-question&envId=2025-05-30
    /// 2359. 找到離給定兩個節點最近的節點
    /// https://leetcode.cn/problems/find-closest-node-to-given-two-nodes/description/?envType=daily-question&envId=2025-05-30
    ///
    /// 題目說明（繁體中文）：
    /// 給定一個有 n 個節點（編號從 0 到 n-1）的有向圖，每個節點最多只有一條出邊。
    /// 這個圖用一個長度為 n 的陣列 edges 表示，edges[i] 代表從節點 i 指向 edges[i] 的有向邊。
    /// 如果節點 i 沒有出邊，則 edges[i] == -1。
    /// 現在給定兩個整數 node1 和 node2，請你返回一個節點的編號，使得該節點同時可以從 node1 和 node2 到達，
    /// 並且從 node1 和 node2 到這個節點的距離的最大值最小。
    /// 如果有多個答案，返回編號最小的那個；如果不存在這樣的節點，返回 -1。
    /// 注意：edges 可能包含環。
    ///
    /// 解題思路：
    /// 1. 此題是有向圖問題，每個節點最多只有一條出邊
    /// 2. 分別從 node1 和 node2 沿著唯一出邊路徑追蹤，計算到所有節點的距離（非 BFS，無需佇列，僅單一路徑遍歷）。
    /// 3. 找到兩個起始節點都能到達的節點中，距離最大值最小的節點
    /// 4. 時間複雜度：O(n)，空間複雜度：O(n)
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試案例 1: 基本測試
        int[] edges1 = {2, 2, 3, -1};
        int node1_1 = 0, node2_1 = 1;
        int result1 = program.ClosestMeetingNode(edges1, node1_1, node2_1);
        Console.WriteLine($"測試案例 1: edges=[2,2,3,-1], node1={node1_1}, node2={node2_1}");
        Console.WriteLine($"結果: {result1}");
        Console.WriteLine();

        // 測試案例 2: 線性圖測試
        int[] edges2 = {1, 2, -1};
        int node1_2 = 0, node2_2 = 2;
        int result2 = program.ClosestMeetingNode(edges2, node1_2, node2_2);
        Console.WriteLine($"測試案例 2: edges=[1,2,-1], node1={node1_2}, node2={node2_2}");
        Console.WriteLine($"結果: {result2}");
        Console.WriteLine();

        // 測試案例 3: 相同起始節點
        int[] edges3 = {1, 2, 3, 4, -1};
        int node1_3 = 0, node2_3 = 0;
        int result3 = program.ClosestMeetingNode(edges3, node1_3, node2_3);
        Console.WriteLine($"測試案例 3: edges=[1,2,3,4,-1], node1={node1_3}, node2={node2_3}");
        Console.WriteLine($"結果: {result3}");
        Console.WriteLine();

        // 測試案例 4: 無法相遇
        int[] edges4 = {1, -1, 3, -1};
        int node1_4 = 0, node2_4 = 2;
        int result4 = program.ClosestMeetingNode(edges4, node1_4, node2_4);
        Console.WriteLine($"測試案例 4: edges=[1,-1,3,-1], node1={node1_4}, node2={node2_4}");
        Console.WriteLine($"結果: {result4}");
        Console.WriteLine();

        // 測試案例 5: 環形圖測試
        int[] edges5 = {4, 4, 4, 4, 1};
        int node1_5 = 1, node2_5 = 3;
        int result5 = program.ClosestMeetingNode(edges5, node1_5, node2_5);
        Console.WriteLine($"測試案例 5: edges=[4,4,4,4,1], node1={node1_5}, node2={node2_5}");
        Console.WriteLine($"結果: {result5}");
    }


    /// <summary>
    /// 找到距離給定兩個節點最近的節點
    /// 
    /// 解題說明：
    /// 1. 先分別從 node1 與 node2 出發，利用 Build 函式計算到所有節點的距離陣列。
    /// 2. 遍歷所有節點，檢查哪些節點同時可由 node1 與 node2 到達。
    /// 3. 對於每個可同時到達的節點，計算兩個起點到該節點距離的最大值，選擇最大值最小且索引最小的節點作為答案。
    /// 4. 若沒有任何節點同時可達，則回傳 -1。
    /// 時間複雜度 O(n)，空間複雜度 O(n)。
    /// </summary>
    /// <param name="edges">邊陣列，edges[i] 表示節點 i 的下一個節點</param>
    /// <param name="node1">起始節點1</param>
    /// <param name="node2">起始節點2</param>
    /// <returns>最近的相遇節點索引，如果沒有相遇節點則返回 -1</returns>
    public int ClosestMeetingNode(int[] edges, int node1, int node2)
    {
        int n = edges.Length;

        // 分別計算從兩個起始節點到所有其他節點的距離
        int[] dist1 = Build(edges, node1);  // 從 node1 出發的距離陣列
        int[] dist2 = Build(edges, node2);  // 從 node2 出發的距離陣列

        int res = -1;  // 結果節點，初始為 -1 表示沒有找到

        // 遍歷所有節點，尋找最佳相遇點
        for (int i = 0; i < n; i++)
        {
            // 檢查節點 i 是否同時被兩個起始節點可達
            if (dist1[i] != -1 && dist2[i] != -1)
            {
                // 如果還沒有找到任何相遇點，或者當前節點更優
                // 優化條件：選擇兩個距離中較大值最小的節點
                if (res == -1 || Math.Max(dist1[i], dist2[i]) < Math.Max(dist1[res], dist2[res]))
                {
                    res = i;
                }
            }
        }
        return res;
    }


    /// <summary>
    /// 建構從指定節點出發的距離陣列
    /// 
    /// 解題說明：
    /// 1. 從起始節點 node 沿著 edges 追蹤路徑，記錄每個節點的距離。
    /// 2. 若遇到無效節點（-1）或已訪問過的節點（避免環），則停止。
    /// 3. 回傳每個節點的距離陣列，無法到達的節點為 -1。
    /// 時間複雜度 O(n)，空間複雜度 O(n)。
    /// </summary>
    /// <param name="edges">邊陣列，edges[i] 表示節點 i 指向的下一個節點</param>
    /// <param name="node">起始節點</param>
    /// <returns>距離陣列，dist[i] 表示從起始節點到節點 i 的距離，-1 表示不可達</returns>
    private int[] Build(int[] edges, int node)
    {
        int n = edges.Length;
        int[] dist = new int[n];
        Array.Fill(dist, -1);  // 初始化所有距離為 -1（表示不可達）

        int distance = 0;  // 目前的步數

        // 沿著邊一步步前進，直到遇到無效節點或已訪問過的節點
        // 修正條件檢查，避免陣列越界
        while (node != -1 && dist[node] == -1)
        {
            dist[node] = distance;  // 記錄到達當前節點的距離
            distance++;             // 步數增加
            node = edges[node];     // 移動到下一個節點
        }

        // 注意：如果遇到環（cycle），迴圈會在重新訪問已訪問節點時停止
        // 這樣可以避免無限迴圈，同時正確記錄所有可達節點的距離

        return dist;
    }
}
