﻿namespace leetcode_133;

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
    /// 133. Clone Graph
    /// https://leetcode.com/problems/clone-graph/description/?envType=problem-list-v2&envId=oizxjoit
    /// 133. 克隆图
    /// https://leetcode.cn/problems/clone-graph/description/ 
    /// 
    /// wiki 說明 Deep copy
    /// https://zh.wikipedia.org/zh-tw/%E5%85%8B%E9%9A%86_(%E7%BC%96%E7%A8%8B)
    /// https://en.wikipedia.org/wiki/Object_copying
    /// 克隆图是指在計算機科學中，將一個圖的結構和數據複製到另一個圖中。
    /// 克隆圖的目的是為了在不改變原始圖的情況下，對其進行操作或分析。
    /// 
	/// 題目說明
	/// 這是一道關於圖(Graph)的深度拷貝題目。要求我們對一個無向連通圖進行
	/// 深度拷貝（Deep Copy），即創建一個與原圖結構和值都相同，但是記憶體
	/// 位址不同的新圖。
    ///    
    /// 解題思路:
    /// 1. 使用 DFS (深度優先搜尋) 遍歷整個圖
    /// 2. 使用 Dictionary 儲存已建立的新節點，避免重複建立和無限循環
    /// 3. 遞迴處理每個節點的鄰居節點
    /// 4. 時間複雜度 O(N+E)，N為節點數，E為邊數
    /// 5. 空間複雜度 O(N)，用於儲存已訪問節點的 Dictionary
	/// 什麼是深度拷貝？
	/// 深度拷貝意味著我們完全複製了一個物件及其所有子物件，創建了全新的記憶體位址，而不是單純複製參考。
	/// 為什麼要驗證記憶體位址？
	/// 1.淺拷貝（Shallow Copy）：
	/// 	只複製參考（reference）
	/// 	原始物件和複製物件指向相同的記憶體位址
	/// 修改其中一個會影響另一個
	/// 2.深度拷貝（Deep Copy）：
	/// 	複製整個物件結構
	/// 	創建全新的記憶體位址
	/// 	原始物件和複製物件完全獨立
	/// 驗證方式解析
	/// (node1 != clonedNode)
	/// 使用 != 運算符比較兩個物件的參考
	/// 如果結果為 true：表示是深度拷貝（不同記憶體位址）
	/// 如果結果為 false：表示是淺拷貝（相同記憶體位址）
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
