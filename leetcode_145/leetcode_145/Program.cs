namespace leetcode_145;

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
    /// 145. Binary Tree Postorder Traversal
    /// https://leetcode.com/problems/binary-tree-postorder-traversal/description/
    /// 145. 二叉树的后序遍历
    /// https://leetcode.cn/problems/binary-tree-postorder-traversal/description/
    /// 
    /// Given the root of a binary tree, return the postorder traversal of its nodes' values.
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1: [1, null, 2, 3]
        //     1
        //      \
        //       2
        //      /
        //     3
        // 預期輸出: [3, 2, 1]
        TreeNode root1 = new TreeNode(1);
        root1.right = new TreeNode(2);
        root1.right.left = new TreeNode(3);
        IList<int> result1 = program.PostorderTraversal(root1);
        Console.WriteLine($"測試案例 1: [{string.Join(", ", result1)}]"); // 預期: [3, 2, 1]

        // 測試案例 2: 空樹
        // 預期輸出: []
        TreeNode? root2 = null;
        IList<int> result2 = program.PostorderTraversal(root2!);
        Console.WriteLine($"測試案例 2: [{string.Join(", ", result2)}]"); // 預期: []

        // 測試案例 3: 只有一個節點 [1]
        // 預期輸出: [1]
        TreeNode root3 = new TreeNode(1);
        IList<int> result3 = program.PostorderTraversal(root3);
        Console.WriteLine($"測試案例 3: [{string.Join(", ", result3)}]"); // 預期: [1]

        // 測試案例 4: 完整二元樹
        //       1
        //      / \
        //     2   3
        //    / \
        //   4   5
        // 預期輸出: [4, 5, 2, 3, 1]
        TreeNode root4 = new TreeNode(1);
        root4.left = new TreeNode(2);
        root4.right = new TreeNode(3);
        root4.left.left = new TreeNode(4);
        root4.left.right = new TreeNode(5);
        IList<int> result4 = program.PostorderTraversal(root4);
        Console.WriteLine($"測試案例 4: [{string.Join(", ", result4)}]"); // 預期: [4, 5, 2, 3, 1]
    }

    /// <summary>
    /// 後序遍歷二元樹 (Postorder Traversal)
    /// 
    /// 解題思路：
    /// 後序遍歷的順序是：左子樹 -> 右子樹 -> 根節點
    /// 使用深度優先搜尋 (DFS) 遞迴方式實現
    /// 
    /// 演算法步驟：
    /// 1. 建立一個結果清單來儲存遍歷結果
    /// 2. 呼叫 DFS 輔助函式進行遞迴遍歷
    /// 3. 回傳最終結果清單
    /// 
    /// 時間複雜度：O(n)，其中 n 是節點數量，每個節點恰好被訪問一次
    /// 空間複雜度：O(h)，其中 h 是樹的高度，用於遞迴呼叫堆疊
    ///            最壞情況（傾斜樹）為 O(n)，最佳情況（平衡樹）為 O(log n)
    /// </summary>
    /// <param name="root">二元樹的根節點</param>
    /// <returns>後序遍歷的節點值清單</returns>
    /// <example>
    /// <code>
    /// 輸入: root = [1, null, 2, 3]
    /// 輸出: [3, 2, 1]
    /// </code>
    /// </example>
    public IList<int> PostorderTraversal(TreeNode root)
    {
        // 建立結果清單來儲存遍歷順序
        List<int> res = new List<int>();

        // 呼叫 DFS 輔助函式進行後序遍歷
        DFS(root, res);

        // 回傳遍歷結果
        return res;
    }

    /// <summary>
    /// 深度優先搜尋 (DFS) 輔助函式 - 實現後序遍歷的遞迴邏輯
    /// 
    /// 後序遍歷特點：
    /// - 先遞迴處理左子樹
    /// - 再遞迴處理右子樹  
    /// - 最後處理當前節點（加入結果清單）
    /// 
    /// 遞迴終止條件：當節點為 null 時返回
    /// 
    /// 與其他遍歷方式的差異：
    /// - 前序 (Preorder):  根 -> 左 -> 右
    /// - 中序 (Inorder):   左 -> 根 -> 右
    /// - 後序 (Postorder): 左 -> 右 -> 根 (本方法)
    /// </summary>
    /// <param name="node">當前處理的節點</param>
    /// <param name="list">儲存遍歷結果的清單</param>
    private void DFS(TreeNode node, List<int> list)
    {
        // 遞迴終止條件：若節點為空，則直接返回
        if (node is null)
        {
            return;
        }

        // 步驟 1：先遞迴遍歷左子樹
        DFS(node.left, list);

        // 步驟 2：再遞迴遍歷右子樹
        DFS(node.right, list);

        // 步驟 3：最後將當前節點的值加入結果清單（後序的關鍵）
        list.Add(node.val);
    }
}
