namespace leetcode_865;

class Program
{
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 865. Smallest Subtree with all the Deepest Nodes
    /// https://leetcode.com/problems/smallest-subtree-with-all-the-deepest-nodes/description/
    /// <para>
    /// Given the root of a binary tree, the depth of each node is the shortest distance to the root.
    ///
    /// Return the smallest subtree that contains all the deepest nodes in the original tree.
    ///
    /// A node is deepest if it has the greatest depth among all nodes in the tree. A node's subtree consists of that node and all its descendants.
    ///
    /// Example 1:
    /// Image: https://s3-lc-upload.s3.amazonaws.com/uploads/2018/07/01/sketch1.png
    /// Input: root = [3,5,1,6,2,0,8,null,null,7,4]
    /// Output: [2,7,4]
    /// Explanation: Return the node with value 2, shown in yellow. The blue nodes are the tree's deepest nodes. Nodes 5, 3, and 2 all have subtrees containing the deepest nodes, but node 2 gives the smallest such subtree.
    ///
    /// Example 2:
    /// Input: root = [1]
    /// Output: [1]
    /// Explanation: The root is the deepest node in the tree.
    ///
    /// Example 3:
    /// Input: root = [0,1,3,null,2]
    /// Output: [2]
    /// Explanation: The deepest node is 2. The subtrees rooted at nodes 2, 1, and 0 are valid, but the subtree rooted at 2 is the smallest.
    ///
    /// Constraints:
    /// - The number of nodes in the tree is in [1, 500].
    /// - 0 &lt;= Node.val &lt;= 500
    /// - Node values are unique.
    ///
    /// Note: This question is the same as 1123: https://leetcode.com/problems/lowest-common-ancestor-of-deepest-leaves/
    /// </para>
    /// <para>
    /// 865. 具有所有最深節點的最小子樹
    /// https://leetcode.cn/problems/smallest-subtree-with-all-the-deepest-nodes/description/
    ///
    /// 給定二元樹的根節點 root，每個節點的深度是它到根節點的最短距離。
    ///
    /// 回傳包含原樹中所有最深節點的最小子樹。
    ///
    /// 若一個節點的深度是整棵樹所有節點中的最大值，就稱為最深節點。某節點的子樹由該節點與其所有後代組成。
    ///
    /// 範例 1：
    /// 圖片：https://s3-lc-upload.s3.amazonaws.com/uploads/2018/07/01/sketch1.png
    /// 輸入：root = [3,5,1,6,2,0,8,null,null,7,4]
    /// 輸出：[2,7,4]
    /// 解釋：回傳圖中黃色、值為 2 的節點。藍色節點是樹中的最深節點。節點 5、3、2 的子樹都包含所有最深節點，但以節點 2 為根的子樹最小。
    ///
    /// 範例 2：
    /// 輸入：root = [1]
    /// 輸出：[1]
    /// 解釋：根節點就是樹中的最深節點。
    ///
    /// 範例 3：
    /// 輸入：root = [0,1,3,null,2]
    /// 輸出：[2]
    /// 解釋：最深節點是 2。以節點 2、1、0 為根的子樹都符合條件，但以節點 2 為根的子樹最小。
    ///
    /// 限制條件：
    /// - 樹中的節點數量在 [1, 500] 範圍內。
    /// - 0 &lt;= Node.val &lt;= 500
    /// - 節點值都不相同。
    ///
    /// 注意：本題與 1123 相同：https://leetcode.cn/problems/lowest-common-ancestor-of-deepest-leaves/
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試案例 1: [3,5,1,6,2,0,8,null,null,7,4]
        TreeNode test1 = new TreeNode(3);
        test1.left = new TreeNode(5);
        test1.right = new TreeNode(1);
        test1.left.left = new TreeNode(6);
        test1.left.right = new TreeNode(2);
        test1.right.left = new TreeNode(0);
        test1.right.right = new TreeNode(8);
        test1.left.right.left = new TreeNode(7);
        test1.left.right.right = new TreeNode(4);
        
        TreeNode result1 = program.SubtreeWithAllDeepest(test1);
        Console.WriteLine($"測試案例 1 結果: {result1.val}"); // 預期輸出: 2
        
        // 測試案例 2: [1]
        TreeNode test2 = new TreeNode(1);
        TreeNode result2 = program.SubtreeWithAllDeepest(test2);
        Console.WriteLine($"測試案例 2 結果: {result2.val}"); // 預期輸出: 1
        
        // 測試案例 3: [0,1,3,null,2]
        TreeNode test3 = new TreeNode(0);
        test3.left = new TreeNode(1);
        test3.right = new TreeNode(3);
        test3.left.right = new TreeNode(2);
        
        TreeNode result3 = program.SubtreeWithAllDeepest(test3);
        Console.WriteLine($"測試案例 3 結果: {result3.val}"); // 預期輸出: 2
    }

    /// <summary>
    /// 找出包含所有最深節點的最小子樹
    /// 
    /// 解題思路：
    /// 使用遞迴的方式進行深度優先搜尋（DFS），對每個節點返回兩個資訊：
    /// 1. 該子樹的最深葉節點的最近公共祖先（LCA）
    /// 2. 該子樹的深度
    /// 
    /// 時間複雜度：O(n)，其中 n 是樹中的節點數，每個節點訪問一次
    /// 空間複雜度：O(h)，其中 h 是樹的高度，遞迴呼叫堆疊的深度
    /// </summary>
    /// <param name="root">二叉樹的根節點</param>
    /// <returns>包含所有最深節點的最小子樹的根節點</returns>
    public TreeNode SubtreeWithAllDeepest(TreeNode root)
    {
        // 呼叫輔助函式 dfs，返回的 Tuple 的第一個元素即為所求的 LCA 節點
        return dfs(root).Item1;
    }

    /// <summary>
    /// 遞迴輔助函式：計算子樹的深度和包含所有最深節點的 LCA
    /// 
    /// 此函式返回一個 Tuple，包含：
    /// - Item1: 包含所有最深節點的最近公共祖先（LCA）節點
    /// - Item2: 當前子樹的深度
    /// 
    /// 演算法邏輯：
    /// 1. 若節點為空，返回 (null, 0)
    /// 2. 遞迴計算左右子樹的深度和 LCA
    /// 3. 比較左右子樹深度：
    ///    - 左子樹更深：最深節點都在左子樹，返回左子樹的 LCA
    ///    - 右子樹更深：最深節點都在右子樹，返回右子樹的 LCA
    ///    - 深度相同：左右子樹都有最深節點，當前節點就是 LCA
    /// </summary>
    /// <param name="root">當前處理的節點</param>
    /// <returns>Tuple，包含 LCA 節點和深度</returns>
    private Tuple<TreeNode, int> dfs(TreeNode root)
    {
        // 基礎情況：空節點的深度為 0
        if(root == null)
        {
            return new Tuple<TreeNode, int>(root, 0);
        }

        // 遞迴處理左子樹，獲取左子樹的 LCA 和深度
        Tuple<TreeNode, int> left = dfs(root.left);
        // 遞迴處理右子樹，獲取右子樹的 LCA 和深度
        Tuple<TreeNode, int> right = dfs(root.right);

        // 情況 1：左子樹更深，最深葉節點都在左子樹中
        if(left.Item2 > right.Item2)
        {
            return new Tuple<TreeNode, int>(left.Item1, left.Item2 + 1);
        }

        // 情況 2：右子樹更深，最深葉節點都在右子樹中
        if(left.Item2 < right.Item2)
        {
            return new Tuple<TreeNode, int>(right.Item1, right.Item2 + 1);
        }

        // 情況 3：左右子樹深度相同，當前節點就是包含所有最深節點的 LCA
        return new Tuple<TreeNode, int>(root, left.Item2 + 1);
    }
    
}
