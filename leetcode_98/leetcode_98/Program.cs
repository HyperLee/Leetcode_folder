namespace leetcode_98
{
    internal class Program
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
        /// 98. Validate Binary Search Tree
        /// https://leetcode.com/problems/validate-binary-search-tree/description/
        /// 98. 验证二叉搜索树
        /// https://leetcode.cn/problems/validate-binary-search-tree/description/
        /// 
        /// 二元搜尋樹（Binary Search Tree, BST）
        /// 二元搜尋樹（BST, Binary Search Tree）是一種 二元樹（Binary Tree），並滿足以下特性：
        /// 左子樹的所有節點值 < 根節點值
        /// 右子樹的所有節點值 > 根節點值
        /// 左右子樹同樣為 BST
        /// 根據上述特性, 可以使用遞迴的方式來判斷是否為 BST
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(5);
            root.left = new TreeNode(1);
            root.right = new TreeNode(4);
            root.right.left = new TreeNode(3);
            root.right.right = new TreeNode(6);

            Console.WriteLine("res (區間遞迴法): " + IsValidBST(root));
            Console.WriteLine("res (中序遍歷法): " + IsValidBST_Inorder(root));
        }


        /// <summary>
        /// 驗證一棵二元搜尋樹（Binary Search Tree, BST）是否有效。
        /// 
        /// 解題思路：
        /// - BST 的定義：左子樹所有節點值 < 根節點值，右子樹所有節點值 > 根節點值，且左右子樹也必須是 BST。
        /// - 設計遞迴函式 helper(root, lower, upper)，判斷以 root 為根的子樹所有節點值是否都在 (lower, upper) 的開區間內。
        /// - 若 root.val 不在範圍內，直接返回 false。
        /// - 遞迴檢查左子樹時，更新上界 upper 為 root.val；檢查右子樹時，更新下界 lower 為 root.val。
        /// - 初始呼叫 helper(root, long.MinValue, long.MaxValue)，表示整棵樹的值必須在 (-∞, +∞) 之間。
        /// <example>
        /// <code>
        /// TreeNode root = new TreeNode(5);
        /// root.left = new TreeNode(1);
        /// root.right = new TreeNode(4);
        /// root.right.left = new TreeNode(3);
        /// root.right.right = new TreeNode(6);
        /// bool result = IsValidBST(root); // 返回 false
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="root">二元搜尋樹的根節點</param>
        /// <returns>若為有效 BST 返回 true，否則返回 false</returns>
        public static bool IsValidBST(TreeNode root)
        {
            return helper(root, long.MinValue, long.MaxValue);
        }


        /// <summary>
        /// 遞迴判斷以 node 為根的子樹是否為有效 BST。
        /// 簡單概念 BST 就是嚴格遞增的二元樹。
        /// 如果沒有遞增那就是錯誤的。
        /// </summary>
        /// <param name="node">目前遞迴的節點</param>
        /// <param name="lowerbound">允許的最小值（開區間左界）</param>
        /// <param name="upperbound">允許的最大值（開區間右界）</param>
        /// <returns>若子樹為有效 BST 返回 true，否則返回 false</returns>
        public static bool helper(TreeNode node, long lowerbound, long upperbound)
        {
            // 若節點為 null，表示已到葉節點，視為有效 BST
            if (node is null)
            {
                return true;
            }

            // 判斷目前節點值是否在合法範圍內（開區間），不合法則返回 false
            // BST 左子樹所有節點值 < 根節點值，右子樹所有節點值 > 根節點值
            if (node.val <= lowerbound || node.val >= upperbound)
            {
                return false;
            }

            // 遞迴檢查左子樹，更新上界（右界）為目前節點值
            // 遞迴檢查右子樹，更新下界（左界）為目前節點值
            return helper(node.left, lowerbound, node.val)
                && helper(node.right, node.val, upperbound);
        }

        /// <summary>
        /// 使用中序遍歷判斷一棵二元搜尋樹（BST）是否有效。
        /// 
        /// 解題思路：
        /// - BST 的中序遍歷結果必須是嚴格遞增的序列。
        /// - 透過遞迴中序遍歷，記錄上一次訪問的節點值，若有不遞增則返回 false。
        /// <example>
        /// <code>
        /// TreeNode root = new TreeNode(5);
        /// root.left = new TreeNode(1);
        /// root.right = new TreeNode(4);
        /// root.right.left = new TreeNode(3);
        /// root.right.right = new TreeNode(6);
        /// bool result = IsValidBST_Inorder(root); // 返回 false
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="root">二元搜尋樹的根節點</param>
        /// <returns>若為有效 BST 返回 true，否則返回 false</returns>
        public static bool IsValidBST_Inorder(TreeNode root)
        {
            long prev = long.MinValue;
            return InorderCheck(root, ref prev);
        }

        /// <summary>
        /// 遞迴中序遍歷，判斷是否嚴格遞增。
        /// </summary>
        /// <param name="node">目前遞迴的節點</param>
        /// <param name="prev">上一次訪問的節點值（用 ref 傳遞）</param>
        /// <returns>若中序遍歷嚴格遞增則返回 true，否則返回 false</returns>
        private static bool InorderCheck(TreeNode node, ref long prev)
        {
            // 若節點為 null，表示已到葉節點，視為有效 BST
            if (node is null)
            {
                return true;
            }

            // 先遞迴左子樹
            if (!InorderCheck(node.left, ref prev))
            {
                return false;
            }

            // 檢查目前節點值是否大於上一次訪問的值
            if (node.val <= prev)
            {
                return false;
            }

            prev = node.val;

            // 再遞迴右子樹
            return InorderCheck(node.right, ref prev);
        }
    }
}
