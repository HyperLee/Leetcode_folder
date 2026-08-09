namespace leetcode_133;

class Program
{
    /// <summary>
    // IList<T> 的方法：Add(T item): 添加元素到列表的末尾
    // Remove(T item): 移除元素
    // Count: 獲取元素數量
    // this[int index]: 索引器存取
    // clear(): 清空列表
    // Contains(T item): 判斷列表中是否包含某個元素
    /// </summary>
    public class Node 
    {
        public int val;
        public IList<Node> neighbors;
        // 建構函式 1: 無參數
        public Node() {
            val = 0;
            neighbors = new List<Node>();
        }
        // 建構函式 2: 只有值參數 (程式中使用的是這個)
        public Node(int _val) {
            val = _val;
            neighbors = new List<Node>();
        }
        // 建構函式 3: 值和鄰居列表參數 (在這個程式中沒有被使用)
        public Node(int _val, List<Node> _neighbors) {
            val = _val;
            neighbors = _neighbors;
        }
    }

    /// <summary>
    /// <para>
    /// 133. Clone Graph
    /// https://leetcode.com/problems/clone-graph/description/
    ///
    /// Given a reference of a node in a connected undirected graph.
    ///
    /// Return a deep copy (clone) of the graph.
    ///
    /// Each node in the graph contains a value (int) and a list (List&lt;Node&gt;) of its neighbors.
    ///
    /// class Node {
    /// public int val;
    /// public List&lt;Node&gt; neighbors;
    /// }
    ///
    /// Test case format:
    ///
    /// For simplicity, each node's value is the same as the node's index (1-indexed). For example, the first node with val == 1,
    /// the second node with val == 2, and so on. The graph is represented in the test case using an adjacency list.
    ///
    /// An adjacency list is a collection of unordered lists used to represent a finite graph. Each list describes the set of
    /// neighbors of a node in the graph.
    ///
    /// The given node will always be the first node with val = 1. You must return the copy of the given node as a reference
    /// to the cloned graph.
    ///
    /// Example 1:
    /// Official illustration: https://assets.leetcode.com/uploads/2019/11/04/133_clone_graph_question.png
    /// Input: adjList = [[2,4],[1,3],[2,4],[1,3]]
    /// Output: [[2,4],[1,3],[2,4],[1,3]]
    /// Explanation: There are 4 nodes in the graph.
    /// 1st node (val = 1)'s neighbors are 2nd node (val = 2) and 4th node (val = 4).
    /// 2nd node (val = 2)'s neighbors are 1st node (val = 1) and 3rd node (val = 3).
    /// 3rd node (val = 3)'s neighbors are 2nd node (val = 2) and 4th node (val = 4).
    /// 4th node (val = 4)'s neighbors are 1st node (val = 1) and 3rd node (val = 3).
    ///
    /// Example 2:
    /// Official illustration: https://assets.leetcode.com/uploads/2020/01/07/graph.png
    /// Input: adjList = [[]]
    /// Output: [[]]
    /// Explanation: Note that the input contains one empty list. The graph consists of only one node with val = 1 and it does
    /// not have any neighbors.
    ///
    /// Example 3:
    /// Input: adjList = []
    /// Output: []
    /// Explanation: This is an empty graph; it does not have any nodes.
    ///
    /// Constraints:
    /// - The number of nodes in the graph is in the range [0, 100].
    /// - 1 &lt;= Node.val &lt;= 100
    /// - Node.val is unique for each node.
    /// - There are no repeated edges and no self-loops in the graph.
    /// - The Graph is connected and all nodes can be visited starting from the given node.
    /// </para>
    /// <para>
    /// 133. 複製圖
    /// https://leetcode.cn/problems/clone-graph/description/
    ///
    /// 給定一個連通無向圖中某個節點的參考。
    ///
    /// 回傳該圖的深層複本（複製品）。
    ///
    /// 圖中的每個節點都包含一個值（int）以及其鄰居清單（List&lt;Node&gt;）。
    ///
    /// class Node {
    /// public int val;
    /// public List&lt;Node&gt; neighbors;
    /// }
    ///
    /// 測試案例格式：
    ///
    /// 為了簡化起見，每個節點的值都與該節點的索引相同（索引從 1 開始）。例如，第一個節點的 val == 1，
    /// 第二個節點的 val == 2，依此類推。測試案例使用鄰接串列表示圖。
    ///
    /// 鄰接串列是一組用來表示有限圖的無序清單。每個清單描述圖中某個節點的鄰居集合。
    ///
    /// 給定節點一律是 val = 1 的第一個節點。你必須回傳給定節點的複本，作為已複製圖的參考。
    ///
    /// 範例 1：
    /// 官方示意圖：https://assets.leetcode.com/uploads/2019/11/04/133_clone_graph_question.png
    /// 輸入：adjList = [[2,4],[1,3],[2,4],[1,3]]
    /// 輸出：[[2,4],[1,3],[2,4],[1,3]]
    /// 解釋：圖中有 4 個節點。
    /// 第 1 個節點（val = 1）的鄰居是第 2 個節點（val = 2）與第 4 個節點（val = 4）。
    /// 第 2 個節點（val = 2）的鄰居是第 1 個節點（val = 1）與第 3 個節點（val = 3）。
    /// 第 3 個節點（val = 3）的鄰居是第 2 個節點（val = 2）與第 4 個節點（val = 4）。
    /// 第 4 個節點（val = 4）的鄰居是第 1 個節點（val = 1）與第 3 個節點（val = 3）。
    ///
    /// 範例 2：
    /// 官方示意圖：https://assets.leetcode.com/uploads/2020/01/07/graph.png
    /// 輸入：adjList = [[]]
    /// 輸出：[[]]
    /// 解釋：請注意，輸入包含一個空清單。圖中只有一個 val = 1 的節點，而且它沒有任何鄰居。
    ///
    /// 範例 3：
    /// 輸入：adjList = []
    /// 輸出：[]
    /// 解釋：這是一個空圖，沒有任何節點。
    ///
    /// 限制條件：
    /// - 圖中的節點數量在 [0, 100] 範圍內。
    /// - 1 &lt;= Node.val &lt;= 100
    /// - 每個節點的 Node.val 均不相同。
    /// - 圖中沒有重複邊，也沒有自迴圈。
    /// - 圖是連通的，而且從給定節點出發可以造訪所有節點。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 創建測試用的圖
        // 使用建構函式 2: 只有值參數
        Node node1 = new Node(1); // 創建值為 1 的節點，neighbors 被初始化為空列表
        Node node2 = new Node(2);
        Node node3 = new Node(3);
        Node node4 = new Node(4);

        
        // 輸入範例：adjList = [[2,4],[1,3],[2,4],[1,3]]
        // 這就建立了一個如下的無向圖：
        // 1 ---- 2
        // |      |
        // 4 ---- 3

        // 建立節點之間的連接
        // 索引 1 的節點連接到節點 2 和節點 4
        node1.neighbors.Add(node2);
        node1.neighbors.Add(node4);
        // 索引 2 的節點連接到節點 1 和節點 3
        node2.neighbors.Add(node1);
        node2.neighbors.Add(node3);
        // 索引 3 的節點連接到節點 2 和節點 4
        node3.neighbors.Add(node2);
        node3.neighbors.Add(node4);
        // 索引 4 的節點連接到節點 1 和節點 3
        node4.neighbors.Add(node1);
        node4.neighbors.Add(node3);

        // 執行圖的複製
        Program program = new Program();
        Node clonedNode = program.CloneGraph(node1);

        // 驗證複製後的圖
        // 1. 節點值相同：
        Console.WriteLine("原始圖的第一個節點值: " + node1.val);
        Console.WriteLine("複製圖的第一個節點值: " + clonedNode.val);
        // 2. 鄰居節點數量相同：
        Console.WriteLine("複製圖的鄰居節點數量: " + clonedNode.neighbors.Count);
        Console.WriteLine("複製圖的第一個鄰居節點值: " + clonedNode.neighbors[0].val);
        Console.WriteLine("複製圖的第二個鄰居節點值: " + clonedNode.neighbors[1].val);
        // 3. 驗證是否為淺拷貝（相同記憶體位址）
        // 這裡的 == 比較的是物件的參考（記憶體位址），而不是值
        Console.WriteLine("是否為淺拷貝: " + (node1 == clonedNode));
        // 記憶體位址不同：
        // 4. 驗證是否為深度複製（新的記憶體位址）
        Console.WriteLine("是否為深度複製: " + (node1 != clonedNode));
        // 實際上你也可以驗證其他節點
        // 因為這是一個連通圖，從 node1 可以到達所有其他節點
        // 如果 node1 是深度拷貝，那麼與它相連的所有節點也必定是深度拷貝
        // Console.WriteLine("node1 是否深度拷貝: " + (node1 != clonedNode));
        // Console.WriteLine("node2 是否深度拷貝: " + (node2 != clonedNode.neighbors[0]));
        // Console.WriteLine("node4 是否深度拷貝: " + (node4 != clonedNode.neighbors[1]));
    }

    /// <summary>
    /// 克隆圖的主要入口函數
    /// 1. 使用 Dictionary 作為緩存，避免重複創建節點
    /// 2. 通過深度優先搜索(DFS)遍歷整個圖
    /// 3. 處理特殊情況：如果輸入節點為空，則返回 null
    /// 
    /// 時間複雜度：O(N + E)，其中 N 是節點數，E 是邊數
    /// 空間複雜度：O(N)，用於存儲訪問過的節點
    /// 
	/// 當遇到圖的問題時，如果符合以下條件，可以考慮使用 DFS：
	/// 1.需要完整遍歷
	/// 2.有循環處理需求
	/// 3.需要追蹤訪問狀態
	/// 4.需要遞迴處理子結構
	/// 5.深度優先的特性較符合問題需求
    /// </summary>
    /// <param name="node">輸入圖的起始節點</param>
    /// <returns>複製後的新圖起始節點</returns>
    public Node CloneGraph(Node node)
    {
        // 處理空節點的情況
        if (node == null) 
        {
            return null;
        }
        
        // 建立字典來儲存已複製的節點，避免重複建立
        // key: 原圖的節點物件，value: 複製後的新節點物件
        // 這樣可以避免在環形圖中陷入無限遞迴
        Dictionary<Node, Node> visited = new Dictionary<Node, Node>();
        
        // 使用 DFS 遞迴複製圖
        return DFS(node, visited);
    }
    
    /// <summary>
    /// 使用深度優先搜索(DFS)複製圖的具體實現
    /// 運作流程：
    /// 1. 檢查節點是否已被訪問，若是則直接返回對應的新節點
    /// 2. 創建當前節點的副本
    /// 3. 將原節點和新節點的對應關係存入 Dictionary
    /// 4. 遞迴處理所有相鄰節點
    /// 5. 將處理好的相鄰節點添加到新節點的 neighbors 列表中
    /// 
    /// 為什麼要用 Dictionary？
    /// - 避免在環形圖中陷入無限遞迴
    /// - 確保相同的節點只被複製一次
    /// - 維護原圖的連接關係
    /// 
    /// if (visited.ContainsKey(node))
    /// 為什麼用 node 物件？
    /// 唯一性保證
    ///     每個 Node 物件都是唯一的，這意味著即使它們的值相同，它們的記憶體位址也不同。
    ///     這樣可以確保我們在字典中使用物件本身作為鍵，而不是它的值。
    /// --反之
    ///     如果使用 node.val 作為鍵，則可能會導致不同的節點被視為相同的鍵，從而導致錯誤的行為。
    ///     例如，兩個節點的值都是 1，但它們實際上是不同的節點。
    /// </summary>
    /// <param name="node">當前要處理的節點</param>
    /// <param name="visited">記錄已訪問節點的字典</param>
    /// <returns>複製後的新節點</returns>
    private Node DFS(Node node, Dictionary<Node, Node> visited)
    {
        // 如果節點已經被訪問過，直接返回對應的新節點
        // 注意:這邊是 node 不是 node.val
        // 因為 node.val 可能會重複，所以不能用 val 作為 key
        if (visited.ContainsKey(node))
        {
            return visited[node];
        }
            
        // 建立新節點
        Node clone = new Node(node.val);
        
        // 將新節點加入已訪問字典
        visited.Add(node, clone);
        
        // 遞迴處理所有鄰居節點
        foreach (var neighbor in node.neighbors)
        {
            clone.neighbors.Add(DFS(neighbor, visited));
        }
        
        return clone;
    }
}
