namespace leetcode_104
{
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
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
        /// 104. Maximum Depth of Binary Tree
        /// https://leetcode.com/problems/maximum-depth-of-binary-tree/
        /// 
        /// 104. 二叉树的最大深度
        /// https://leetcode.cn/problems/maximum-depth-of-binary-tree/description/
        /// 
        /// A binary tree's maximum depth is the number of nodes along the longest path from the root node down to the farthest leaf node.
        /// 計算方式從 root 為起始點, 找出子樹最大深度
        /// 
        /// build tree sample
        /// http://e-troy.blogspot.com/2015/02/c-binary-search-tree.html
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(3);

            root.left = new TreeNode(9);
            root.right = new TreeNode(20);

            root.right.right = new TreeNode(7);
            root.right.left = new TreeNode(15);

            Console.WriteLine("MaxDepth: " + MaxDepth(root));
            Console.WriteLine("MaxDepth2: " + MaxDepth2(root));
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10226788
        /// 遞迴概念
        /// 1. 判斷 root 是否為 null，若是則回傳 0
        /// 2. 回傳此節點的深度 
        ///     遞迴找出 root 的 right 右節點 最大深度
        ///     遞迴找出 root 的 left 左節點 最大深度
        ///     比較兩個節點的最大深度，使用 Math.Max 取 最大值
        ///     最後並 + 1，代表需要往上多加這一層
        ///     
        /// root 為第一層, 每往下一層都要 + 1
        /// 不要忽略 root 是第一層
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MaxDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            return Math.Max(MaxDepth(root.right), MaxDepth(root.left)) + 1;
        }


        /// <summary>
        /// 方法二,
        /// 參考 leetcode_111 Minimum Depth of Binary Tree
        /// 修改而來
        /// 
        /// root 為第一層, 每往下一層都要 + 1
        /// 不要忽略 root 是第一層
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MaxDepth2(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            // 只有 root 沒有左右子樹
            if (root.left == null && root.right == null)
            {
                return 1;
            }

            // 紀錄最大深度
            int maxDepth = int.MinValue;

            // 找出左子樹最大深度
            if (root.left != null)
            {
                maxDepth = Math.Max(MaxDepth2(root.left), maxDepth);
            }

            // 找出右子樹最大深度
            if (root.right != null)
            {
                maxDepth = Math.Max(MaxDepth2(root.right), maxDepth);
            }

            // 最後並 + 1，代表需要往上多加這一層
            return maxDepth + 1;
        }
    }
}
