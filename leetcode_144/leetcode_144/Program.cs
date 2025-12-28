namespace leetcode_144;

class Program
{
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }    

    /// <summary>
    /// 144. Binary Tree Preorder Traversal
    /// https://leetcode.com/problems/binary-tree-preorder-traversal/description/
    /// 144. 二叉树的前序遍历
    /// https://leetcode.cn/problems/binary-tree-preorder-traversal/description/
    /// 
    /// Given the root of a binary tree, return the preorder traversal of its nodes' values.
    /// 
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== LeetCode 144. Binary Tree Preorder Traversal ===\n");

        // 建立測試用的二元樹
        //       1
        //        \
        //         2
        //        /
        //       3
        // 預期輸出: [1, 2, 3]
        var example1 = new TreeNode(1)
        {
            right = new TreeNode(2)
            {
                left = new TreeNode(3)
            }
        };

        // 建立測試用的二元樹
        //         1
        //        / \
        //       2   3
        //      / \
        //     4   5
        // 預期輸出: [1, 2, 4, 5, 3]
        var example2 = new TreeNode(1)
        {
            left = new TreeNode(2)
            {
                left = new TreeNode(4),
                right = new TreeNode(5)
            },
            right = new TreeNode(3)
        };

        // 空樹測試
        TreeNode? example3 = null;

        // 單一節點測試
        var example4 = new TreeNode(1);

        var program = new Program();

        // 執行測試
        Console.WriteLine("Example 1: [1, null, 2, 3]");
        var result1 = program.PreorderTraversal(example1);
        Console.WriteLine($"Output: [{string.Join(", ", result1)}]");
        Console.WriteLine($"Expected: [1, 2, 3]\n");

        Console.WriteLine("Example 2: [1, 2, 3, 4, 5]");
        var result2 = program.PreorderTraversal(example2);
        Console.WriteLine($"Output: [{string.Join(", ", result2)}]");
        Console.WriteLine($"Expected: [1, 2, 4, 5, 3]\n");

        Console.WriteLine("Example 3: [] (empty tree)");
        var result3 = program.PreorderTraversal(example3);
        Console.WriteLine($"Output: [{string.Join(", ", result3)}]");
        Console.WriteLine($"Expected: []\n");

        Console.WriteLine("Example 4: [1] (single node)");
        var result4 = program.PreorderTraversal(example4);
        Console.WriteLine($"Output: [{string.Join(", ", result4)}]");
        Console.WriteLine($"Expected: [1]");
    }

    /// <summary>
    /// 解題說明：
    /// 前序遍歷 (Preorder Traversal) 的順序為：根節點 → 左子樹 → 右子樹
    /// 
    /// 解題思路：
    /// 1. 使用深度優先搜尋 (DFS) 遞迴方式實作
    /// 2. 先訪問當前節點，將其值加入結果清單
    /// 3. 遞迴處理左子樹
    /// 4. 遞迴處理右子樹
    /// 
    /// 時間複雜度：O(n)，其中 n 為節點數量，每個節點恰好被訪問一次
    /// 空間複雜度：O(h)，其中 h 為樹的高度，為遞迴呼叫堆疊的空間消耗
    ///           最壞情況下（傾斜樹）為 O(n)，最佳情況下（平衡樹）為 O(log n)
    /// </summary>
    /// <param name="root">二元樹的根節點，可為 null 表示空樹</param>
    /// <returns>前序遍歷的節點值清單</returns>
    /// <example>
    /// <code>
    ///  範例：建立二元樹 [1, null, 2, 3]
    ///        1
    ///         \
    ///          2
    ///         /
    ///        3
    /// var root = new TreeNode(1) { right = new TreeNode(2) { left = new TreeNode(3) } };
    /// var result = PreorderTraversal(root);
    ///  result = [1, 2, 3]
    /// </code>
    /// </example>
    public IList<int> PreorderTraversal(TreeNode? root)
    {
        // 初始化結果清單，用於存放遍歷順序
        List<int> list = new List<int>();
        
        // 呼叫 DFS 輔助方法進行遞迴遍歷
        DFS(root, list);
        
        return list;
    }

    /// <summary>
    /// 深度優先搜尋 (DFS) 輔助方法，使用遞迴方式進行前序遍歷
    /// 
    /// 前序遍歷的核心邏輯：
    /// 1. 終止條件：若節點為 null，直接返回
    /// 2. 處理當前節點：將節點值加入清單（根）
    /// 3. 遞迴左子樹（左）
    /// 4. 遞迴右子樹（右）
    /// 
    /// 遍歷順序：根 → 左 → 右 (Root → Left → Right)
    /// </summary>
    /// <param name="node">當前要處理的節點，可為 null</param>
    /// <param name="list">儲存遍歷結果的清單</param>
    private static void DFS(TreeNode? node, List<int> list)
    {
        // 終止條件：若節點為空，結束此次遞迴
        if (node is null)
        {
            return;
        }

        // 步驟 1：訪問根節點 - 將當前節點的值加入結果清單
        list.Add(node.val);
        
        // 步驟 2：遞迴遍歷左子樹
        DFS(node.left, list);
        
        // 步驟 3：遞迴遍歷右子樹
        DFS(node.right, list);
    }
}
