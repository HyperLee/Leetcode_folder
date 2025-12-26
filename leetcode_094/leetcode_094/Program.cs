namespace leetcode_094;

class Program
{
    /// <summary>
    /// 二元樹節點的定義。
    /// </summary>
    public class TreeNode
    {
        /// <summary>節點儲存的值</summary>
        public int val;

        /// <summary>左子節點</summary>
        public TreeNode? left;

        /// <summary>右子節點</summary>
        public TreeNode? right;

        /// <summary>
        /// 建立一個二元樹節點。
        /// </summary>
        /// <param name="val">節點值，預設為 0</param>
        /// <param name="left">左子節點，預設為 null</param>
        /// <param name="right">右子節點，預設為 null</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 94. Binary Tree Inorder Traversal
    /// https://leetcode.com/problems/binary-tree-inorder-traversal/description/
    /// 94. 二叉树的中序遍历
    /// https://leetcode.cn/problems/binary-tree-inorder-traversal/description/
    /// 
    /// Given the root of a binary tree, return the inorder traversal of its nodes' values.
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試範例 1: [1,null,2,3]
        // 建立二元樹結構:
        //     1
        //      \
        //       2
        //      /
        //     3
        TreeNode root1 = new TreeNode(1);
        root1.right = new TreeNode(2);
        root1.right.left = new TreeNode(3);

        IList<int> result1 = program.InorderTraversal(root1);
        Console.WriteLine($"範例 1: [{string.Join(", ", result1)}]"); // 預期輸出: [1, 3, 2]

        // 測試範例 2: [1,2,3,4,5,null,8,null,null,6,7,9]
        // 建立二元樹結構:
        //         1
        //        / \
        //       2   3
        //      / \   \
        //     4   5   8
        //        / \ /
        //       6  7 9
        TreeNode root2 = new TreeNode(1);
        root2.left = new TreeNode(2);
        root2.right = new TreeNode(3);
        root2.left.left = new TreeNode(4);
        root2.left.right = new TreeNode(5);
        root2.right.right = new TreeNode(8);
        root2.left.right.left = new TreeNode(6);
        root2.left.right.right = new TreeNode(7);
        root2.right.right.left = new TreeNode(9);

        IList<int> result2 = program.InorderTraversal(root2);
        Console.WriteLine($"範例 2: [{string.Join(", ", result2)}]"); // 預期輸出: [4, 2, 6, 5, 7, 1, 3, 9, 8]

        // 測試範例 3: 空樹
        TreeNode? root3 = null;
        IList<int> result3 = program.InorderTraversal(root3);
        Console.WriteLine($"範例 3 (空樹): [{string.Join(", ", result3)}]"); // 預期輸出: []

        // 測試範例 4: 單節點 [1]
        TreeNode root4 = new TreeNode(1);
        IList<int> result4 = program.InorderTraversal(root4);
        Console.WriteLine($"範例 4 (單節點): [{string.Join(", ", result4)}]"); // 預期輸出: [1]
    }

    /// <summary>
    /// 執行二元樹的中序走訪 (Inorder Traversal)。
    /// 
    /// <para>
    /// <b>解題思路：</b>
    /// 中序走訪的順序為：左子樹 → 根節點 → 右子樹。
    /// 對於二元搜尋樹 (BST)，中序走訪會按照節點值的升序排列。
    /// </para>
    /// 
    /// <para>
    /// <b>演算法：</b>
    /// 使用遞迴方式實作，對每個節點依序拜訪左子樹、記錄當前節點值、再拜訪右子樹。
    /// </para>
    /// 
    /// <para>
    /// <b>時間複雜度：</b> O(n)，其中 n 為節點數量，每個節點恰好被拜訪一次。
    /// </para>
    /// 
    /// <para>
    /// <b>空間複雜度：</b> O(n)，最壞情況下遞迴堆疊深度為 n（樹為鏈狀）。
    /// </para>
    /// </summary>
    /// <param name="root">二元樹的根節點，可為 null 表示空樹</param>
    /// <returns>中序走訪後的節點值列表</returns>
    /// <example>
    /// <code>
    /// // 範例：對樹 [1,null,2,3] 執行中序走訪
    /// //     1
    /// //      \
    /// //       2
    /// //      /
    /// //     3
    /// // 輸出: [1, 3, 2]
    /// var result = InorderTraversal(root);
    /// </code>
    /// </example>
    public IList<int> InorderTraversal(TreeNode? root)
    {
        // 建立結果列表用於儲存走訪順序
        List<int> res = new List<int>();

        // 呼叫遞迴輔助函式執行中序走訪
        Inorder(root, res);

        // 回傳走訪結果
        return res;
    }

    /// <summary>
    /// 遞迴輔助函式：執行中序走訪並將結果存入列表。
    /// 
    /// <para>
    /// <b>中序走訪步驟：</b>
    /// <list type="number">
    ///   <item><description>遞迴走訪左子樹</description></item>
    ///   <item><description>拜訪當前節點（將值加入結果列表）</description></item>
    ///   <item><description>遞迴走訪右子樹</description></item>
    /// </list>
    /// </para>
    /// 
    /// <para>
    /// <b>遞迴終止條件：</b>
    /// 當節點為 null 時，直接返回不做任何處理。
    /// </para>
    /// </summary>
    /// <param name="root">當前要處理的節點</param>
    /// <param name="res">儲存走訪結果的列表（引用傳遞）</param>
    public static void Inorder(TreeNode? root, List<int> res)
    {
        // 遞迴終止條件：若節點為空，直接返回
        if (root is null)
        {
            return;
        }

        // Step 1: 遞迴走訪左子樹（先處理所有左側節點）
        Inorder(root.left, res);

        // Step 2: 拜訪當前節點（將當前節點值加入結果列表）
        res.Add(root.val);

        // Step 3: 遞迴走訪右子樹（最後處理右側節點）
        Inorder(root.right, res);
    }
}
