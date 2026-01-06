namespace leetcode_1161;

class Program
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 1161. Maximum Level Sum of a Binary Tree
    /// https://leetcode.com/problems/maximum-level-sum-of-a-binary-tree/description/?envType=daily-question&envId=2026-01-06
    /// 1161. 最大层内元素和 (簡體中文)
    /// https://leetcode.cn/problems/maximum-level-sum-of-a-binary-tree/description/?envType=daily-question&envId=2026-01-06
    /// 1161. 最大層內元素和 (繁體中文)
    /// 
    /// Given the root of a binary tree, the level of its root is 1, the level of its children is 2, and so on.
    /// Return the smallest level x such that the sum of all the values of nodes at level x is maximal.
    /// 
    /// 給定一個二元樹的根節點，根節點所在層為 1，子節點所在層為 2，以此類推。
    /// 回傳使該層節點值總和最大的最小層級 x。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試案例 1: [1,7,0,7,-8,null,null]
        // 樹狀結構:
        //       1
        //      / \
        //     7   0
        //    / \
        //   7  -8
        // 第 1 層: 1, 總和 = 1
        // 第 2 層: 7 + 0 = 7
        // 第 3 層: 7 + (-8) = -1
        // 預期輸出: 2 (第 2 層總和最大)
        TreeNode root1 = new TreeNode(1,
            new TreeNode(7,
                new TreeNode(7),
                new TreeNode(-8)),
            new TreeNode(0));
        
        Console.WriteLine($"測試案例 1 結果: {MaxLevelSum(root1)}"); // 預期輸出: 2
        
        // 清空 sum 以便下一次測試
        sum.Clear();
        
        // 測試案例 2: [989,null,10250,98693,-89388,null,null,null,-32127]
        // 樹狀結構:
        //          989
        //            \
        //           10250
        //           /   \
        //       98693   -89388
        //                  \
        //                 -32127
        // 第 1 層: 989
        // 第 2 層: 10250
        // 第 3 層: 98693 + (-89388) = 9305
        // 第 4 層: -32127
        // 預期輸出: 2 (第 2 層總和最大)
        TreeNode root2 = new TreeNode(989,
            default!,
            new TreeNode(10250,
                new TreeNode(98693),
                new TreeNode(-89388,
                    default!,
                    new TreeNode(-32127))));
        
        Console.WriteLine($"測試案例 2 結果: {MaxLevelSum(root2)}"); // 預期輸出: 2
        
        Console.WriteLine("\n=== 測試 BFS 方法 ===");
        
        // BFS 測試案例 1
        TreeNode root3 = new TreeNode(1,
            new TreeNode(7,
                new TreeNode(7),
                new TreeNode(-8)),
            new TreeNode(0));
        
        Console.WriteLine($"測試案例 1 (BFS) 結果: {MaxLevelSum_BFS(root3)}"); // 預期輸出: 2
        
        // BFS 測試案例 2
        TreeNode root4 = new TreeNode(989,
            default!,
            new TreeNode(10250,
                new TreeNode(98693),
                new TreeNode(-89388,
                    default!,
                    new TreeNode(-32127))));
        
        Console.WriteLine($"測試案例 2 (BFS) 結果: {MaxLevelSum_BFS(root4)}"); // 預期輸出: 2
    }

    private static readonly IList<int> sum = new List<int>();

    /// <summary>
    /// 計算二元樹中節點值總和最大的層級
    /// 
    /// 解題思路：
    /// 1. 使用深度優先搜索 (DFS) 遍歷整棵二元樹
    /// 2. 使用動態陣列 sum 記錄每一層的節點值總和
    /// 3. 遍歷 sum 陣列，找出總和最大的層級
    /// 4. 如果有多層總和相同，回傳層號較小的那一層
    /// 
    /// 時間複雜度：O(n)，其中 n 是節點數量，需要遍歷所有節點
    /// 空間複雜度：O(h)，其中 h 是樹的高度，遞迴呼叫堆疊的深度
    /// </summary>
    /// <param name="root">二元樹的根節點</param>
    /// <returns>節點值總和最大的最小層級（層級從 1 開始計算）</returns>
    public static int MaxLevelSum(TreeNode root)
    {
        // 使用 DFS 遍歷整棵樹，從第 0 層（根節點）開始
        DFS(root, 0);
        
        // 初始化結果為第 0 層的索引
        int res = 0;
        
        // 遍歷 sum 陣列，找出總和最大的層級
        // 因為是從左到右遍歷，所以如果有多層總和相同，會自動選擇層號較小的
        for(int i = 0; i < sum.Count; i++)
        {
            if(sum[i] > sum[res])
            {
                res = i;  // 更新最大總和所在的層級索引
            }
        }
        
        // 回傳層號（層號從 1 開始，所以要加 1）
        return res + 1;
    }

    /// <summary>
    /// 深度優先搜索 (DFS) 遞迴函式，用於計算每一層的節點值總和
    /// 
    /// 演算法流程：
    /// 1. 檢查當前層號是否等於 sum 陣列的長度
    ///    - 如果相等：表示這是第一次訪問該層，將當前節點值加入 sum 陣列末尾
    ///    - 如果不相等：表示該層已經有其他節點，將當前節點值累加到對應層級的總和
    /// 2. 遞迴處理左子樹（如果存在），層號加 1
    /// 3. 遞迴處理右子樹（如果存在），層號加 1
    /// 
    /// 遍歷順序：前序遍歷（根 -> 左 -> 右）
    /// 
    /// 範例說明：
    /// 對於樹結構:
    ///       1
    ///      / \
    ///     7   0
    ///    / \
    ///   7  -8
    /// 
    /// 遍歷順序和 sum 陣列變化：
    /// 1. 訪問節點 1 (level=0): sum=[1]
    /// 2. 訪問節點 7 (level=1): sum=[1, 7]
    /// 3. 訪問節點 7 (level=2): sum=[1, 7, 7]
    /// 4. 回溯到節點 7，訪問節點 -8 (level=2): sum=[1, 7, -1]  // 7 + (-8) = -1
    /// 5. 回溯到節點 1，訪問節點 0 (level=1): sum=[1, 7, -1]  // 7 + 0 = 7
    /// </summary>
    /// <param name="node">當前訪問的節點</param>
    /// <param name="level">當前節點所在的層級（從 0 開始）</param>
    private static void DFS(TreeNode node, int level)
    {
        // 判斷當前層號是否達到陣列長度
        if(level == sum.Count)
        {
            // 第一次訪問該層，將節點值加入陣列末尾
            sum.Add(node.val);
        }
        else
        {
            // 該層已有其他節點，累加當前節點值到對應層級
            sum[level] += node.val;
        }

        // 遞迴處理左子樹，層號加 1
        if(node.left != null)
        {
            DFS(node.left, level + 1);
        }

        // 遞迴處理右子樹，層號加 1
        if(node.right != null)
        {
            DFS(node.right, level + 1);
        }
    }

    /// <summary>
    /// 使用廣度優先搜索 (BFS) 計算二元樹中節點值總和最大的層級
    /// 
    /// 解題思路：
    /// 1. 使用佇列 (Queue) 實作廣度優先搜索
    /// 2. 使用兩個動態陣列：queue 儲存當前層的節點，nextQueue 儲存下一層的節點
    /// 3. 逐層遍歷樹，同時計算每層的節點值總和
    /// 4. 追蹤最大總和及其對應的層級
    /// 5. 遍歷完當前層後，將 queue 更新為 nextQueue
    /// 
    /// 演算法流程：
    /// 1. 初始化 res = 1（結果層級），maxSum = int.MinValue（最大總和）
    /// 2. 初始化 queue 包含根節點，從第 1 層開始
    /// 3. 對於每一層：
    ///    - 建立 nextQueue 儲存下一層節點
    ///    - 遍歷當前層所有節點，累加節點值到 sum
    ///    - 將子節點（left 和 right）加入 nextQueue
    ///    - 如果 sum > maxSum，更新 maxSum 和 res
    ///    - 將 queue 設為 nextQueue，進入下一層
    /// 4. 回傳 res（總和最大的層級）
    /// 
    /// 優點：
    /// - 自然的層級遍歷方式，符合題目要求
    /// - 不需要額外記錄層號，直接在迴圈中追蹤
    /// - 程式碼結構清晰，易於理解
    /// 
    /// 時間複雜度：O(n)，其中 n 是節點數量，每個節點訪問一次
    /// 空間複雜度：O(w)，其中 w 是樹的最大寬度（某一層的最大節點數）
    /// </summary>
    /// <param name="root">二元樹的根節點</param>
    /// <returns>節點值總和最大的最小層級（層級從 1 開始計算）</returns>
    public static int MaxLevelSum_BFS(TreeNode root)
    {
        // 初始化結果層級為 1
        int res = 1;
        
        // 初始化最大總和為 int 的最小值
        int maxSum = int.MinValue;
        
        // 初始化佇列，包含根節點
        IList<TreeNode> queue = new List<TreeNode> { root };
        
        // 從第 1 層開始遍歷，當佇列不為空時繼續
        for(int level = 1; queue.Count > 0; level++)
        {
            // 建立下一層的節點佇列
            IList<TreeNode> nextQueue = new List<TreeNode>();
            
            // 初始化當前層的總和
            int sum = 0;
            
            // 遍歷當前層的所有節點
            foreach(var node in queue)
            {
                // 累加當前節點的值到總和
                sum += node.val;
                
                // 將左子節點加入下一層佇列
                if(node.left != null)
                {
                    nextQueue.Add(node.left);
                }
                
                // 將右子節點加入下一層佇列
                if(node.right != null)
                {
                    nextQueue.Add(node.right);
                }
            }
            
            // 如果當前層的總和大於最大總和，更新結果
            if(sum > maxSum)
            {
                maxSum = sum;  // 更新最大總和
                res = level;   // 更新結果層級
            }
            
            // 將當前佇列更新為下一層佇列
            queue = nextQueue;
        }
        
        // 回傳總和最大的層級
        return res;
    }
}
