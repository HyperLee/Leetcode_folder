using System.Diagnostics;

namespace leetcode_1443;

class Program
{
    /// <summary>
    /// 1443. Minimum Time to Collect All Apples in a Tree
    /// https://leetcode.com/problems/minimum-time-to-collect-all-apples-in-a-tree/description/
    /// 1443. 收集樹上所有蘋果的最少時間
    /// https://leetcode.cn/problems/minimum-time-to-collect-all-apples-in-a-tree/description/
    ///
    /// 題目（繁體中文）：
    /// 給定一棵由 n 個節點（編號 0 到 n-1）組成的無向樹，部分節點上有蘋果。
    /// 每走過一條邊需花費 1 秒。從節點 0 出發並最終回到節點 0，
    /// 求收集樹上所有蘋果所需的最短時間（秒）。
    /// 樹的邊由 edges 陣列給出，edges[i] = [ai, bi] 表示 ai 與 bi 之間存在邊。
    /// hasApple 為布林陣列，hasApple[i] = true 表示節點 i 有蘋果，否則沒有。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試範例 1：
        // 樹結構：
        //        0 (無蘋果)
        //       / \
        //      1   2 (有蘋果)
        //     /|\
        //    3 4 5
        //   (無)(有)(有)
        // 預期輸出：8（需經過邊 0-1, 1-4, 1-5, 0-2）
        int n1 = 7;
        int[][] edges1 = [[0, 1], [0, 2], [1, 4], [1, 5], [2, 3], [2, 6]];
        IList<bool> hasApple1 = [false, false, true, false, true, true, false];
        int result1 = program.MinTime(n1, edges1, hasApple1);
        Console.WriteLine($"測試範例 1: {result1}"); // 預期輸出：8

        // 測試範例 2：
        // 同樣的樹結構，但只有節點 4 和 5 有蘋果
        // 預期輸出：6（需經過邊 0-1, 1-4, 1-5）
        int n2 = 7;
        int[][] edges2 = [[0, 1], [0, 2], [1, 4], [1, 5], [2, 3], [2, 6]];
        IList<bool> hasApple2 = [false, false, false, false, true, true, false];
        int result2 = program.MinTime(n2, edges2, hasApple2);
        Console.WriteLine($"測試範例 2: {result2}"); // 預期輸出：6

        // 測試範例 3：
        // 樹上沒有蘋果
        // 預期輸出：0
        int n3 = 7;
        int[][] edges3 = [[0, 1], [0, 2], [1, 4], [1, 5], [2, 3], [2, 6]];
        IList<bool> hasApple3 = [false, false, false, false, false, false, false];
        int result3 = program.MinTime(n3, edges3, hasApple3);
        Console.WriteLine($"測試範例 3: {result3}"); // 預期輸出：0
    }

    IList<int>[] adjacentNodes = [];
    int[] parents = [];
    bool[] visited = [];

    /// <summary>
    /// 計算收集樹上所有蘋果所需的最少時間。
    /// 
    /// 解題思路：
    /// 1. 首先將無向邊轉換為鄰接表，方便遍歷相鄰節點。
    /// 2. 使用 DFS 從根節點 0 開始遍歷，建立每個節點的父節點關係。
    /// 3. 對於每個有蘋果的節點，從該節點向根節點回溯，計算需要經過的邊數。
    /// 4. 使用 visited 陣列避免重複計算已經走過的路徑。
    /// 5. 每條邊需要走兩次（去和回），所以每經過一條新邊，時間加 2。
    /// 
    /// 時間複雜度：O(n)，每個節點最多被訪問兩次（DFS 和回溯）。
    /// 空間複雜度：O(n)，用於儲存鄰接表、父節點陣列和訪問標記。
    /// </summary>
    /// <param name="n">樹的節點數量</param>
    /// <param name="edges">樹的邊，edges[i] = [ai, bi] 表示 ai 和 bi 之間有一條邊</param>
    /// <param name="hasApple">hasApple[i] = true 表示節點 i 有蘋果</param>
    /// <returns>收集所有蘋果所需的最少時間（秒）</returns>
    /// <example>
    /// <code>
    /// int n = 7;
    /// int[][] edges = [[0,1], [0,2], [1,4], [1,5], [2,3], [2,6]];
    /// IList&lt;bool&gt; hasApple = [false, false, true, false, true, true, false];
    /// int result = MinTime(n, edges, hasApple); // 回傳 8
    /// </code>
    /// </example>
    public int MinTime(int n, int[][] edges, IList<bool> hasApple)
    {
        // 步驟 1：初始化鄰接表，用於儲存每個節點的相鄰節點
        adjacentNodes = new IList<int>[n];
        for(int i = 0; i < n; i++)
        {
            adjacentNodes[i] = new List<int>();
        }

        // 步驟 2：根據邊建立無向圖的鄰接表（雙向連接）
        foreach(int[] edge in edges)
        {
            int node0 = edge[0];
            int node1 = edge[1];
            adjacentNodes[node0].Add(node1);
            adjacentNodes[node1].Add(node0);
        }

        // 步驟 3：初始化父節點陣列，-1 表示沒有父節點（根節點）
        parents = new int[n];
        Array.Fill(parents, -1);

        // 步驟 4：從根節點 0 開始 DFS，建立每個節點的父節點關係
        DFS(0, -1);

        // 步驟 5：初始化訪問標記陣列，根節點預設已訪問
        visited = new bool[n];
        visited[0] = true;

        // 步驟 6：遍歷所有有蘋果的節點，計算收集時間
        int time = 0;
        for(int i = 0; i < n; i++)
        {
            if(hasApple[i])
            {
                // 計算從該蘋果節點到已訪問路徑的時間
                time += GetTime(i);
            }
        }

        return time;
    }

    /// <summary>
    /// 深度優先搜尋，用於建立樹的父子節點關係。
    /// 
    /// 從根節點開始遍歷整棵樹，對於每個節點，記錄其父節點。
    /// 由於輸入的邊是無向的，需要透過傳入父節點參數來避免回頭訪問。
    /// 
    /// 遍歷過程：
    /// 1. 取得當前節點的所有相鄰節點
    /// 2. 跳過父節點（避免往回走）
    /// 3. 對於每個子節點，記錄其父節點為當前節點
    /// 4. 遞迴處理子節點
    /// </summary>
    /// <param name="node">當前正在訪問的節點編號</param>
    /// <param name="parent">當前節點的父節點編號，根節點的父節點為 -1</param>
    public void DFS(int node, int parent)
    {
        // 取得當前節點的所有相鄰節點
        IList<int> neighbors = adjacentNodes[node];

        foreach(int neighbor in neighbors)
        {
            // 跳過父節點，避免往回走（因為是無向圖）
            if(neighbor == parent)
            {
                continue;
            }

            // 記錄子節點的父節點為當前節點
            parents[neighbor] = node;

            // 遞迴訪問子節點
            DFS(neighbor, node);
        }
    }

    /// <summary>
    /// 計算從指定蘋果節點到已訪問路徑所需的時間。
    /// 
    /// 從蘋果節點開始，沿著父節點向根節點方向回溯，
    /// 直到遇到已經訪問過的節點為止。
    /// 
    /// 核心邏輯：
    /// - 如果一個節點已經被訪問過，表示從該節點到根節點的路徑已經計算過
    /// - 每經過一條新的邊，時間加 2（去程 1 秒 + 回程 1 秒）
    /// - 將經過的節點標記為已訪問，避免後續重複計算
    /// 
    /// 範例：假設節點 4 有蘋果，父節點路徑為 4 → 1 → 0
    /// - 若節點 0 已訪問，則需要經過邊 4-1 和 1-0，時間為 4
    /// - 若節點 1 已訪問，則只需經過邊 4-1，時間為 2
    /// </summary>
    /// <param name="node">有蘋果的節點編號</param>
    /// <returns>從該節點到已訪問路徑所需的時間</returns>
    public int GetTime(int node)
    {
        int time = 0;

        // 向根節點方向回溯，直到遇到已訪問的節點
        while(!visited[node])
        {
            // 標記當前節點為已訪問
            visited[node] = true;

            // 移動到父節點
            node = parents[node];

            // 每經過一條邊，時間加 2（來回各 1 秒）
            time += 2;
        }

        return time;
    }
}
