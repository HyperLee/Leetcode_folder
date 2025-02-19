namespace leetcode_236
{
    internal class Program
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }


        /// <summary>
        /// 236. Lowest Common Ancestor of a Binary Tree
        /// https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/description/
        /// 236. 二叉树的最近公共祖先
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-tree/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(3);
            root.left = new TreeNode(5);
            root.right = new TreeNode(1);

            root.left.left = new TreeNode(6);
            root.left.right = new TreeNode(2);
            root.left.right.left = new TreeNode(7);
            root.left.right.right = new TreeNode(4);

            root.right.left = new TreeNode(0);
            root.right.right = new TreeNode(8);

            Console.WriteLine("res: " + LowestCommonAncestor(root, root.left, root.left.right.right).val); // 3
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-tree/solutions/238552/er-cha-shu-de-zui-jin-gong-gong-zu-xian-by-leetc-2/
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-tree/solutions/2023872/fen-lei-tao-lun-luan-ru-ma-yi-ge-shi-pin-2r95/
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-tree/solutions/1456136/by-stormsunshine-sj0k/
        /// 
        /// 遞迴解法
        /// </summary>
        /// <param name="root"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            //如果 root 為 null，或 root 剛好是 p 或 q，直接返回 root。
            if (root == null || root == p || root == q)
            {
                return root;
            }

            // 递归查找左右子树
            TreeNode left = LowestCommonAncestor(root.left, p, q);
            // 递归查找右子树
            TreeNode right = LowestCommonAncestor(root.right, p, q);

            // 如果 left 和 right 都不為 null
            if (left != null && right != null)
            {
                // 這代表 root 就是 p 和 q 的最近公共祖先。
                return root;
            }

            return left != null ? left : right;
        }

    }
}
