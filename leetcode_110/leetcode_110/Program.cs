using System.ComponentModel.Design.Serialization;

namespace leetcode_110
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
        /// 110. Balanced Binary Tree
        /// https://leetcode.com/problems/balanced-binary-tree/description/
        /// 
        /// 110. 平衡二叉树
        /// https://leetcode.cn/problems/balanced-binary-tree/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(3);
            root.left = new TreeNode(9);
            root.right = new TreeNode(20);
            root.right.left = new TreeNode(15);
            root.right.right = new TreeNode(7);

            Console.WriteLine("res: " + IsBalanced(root));
            Console.WriteLine("res2: " + IsBalanced2(root));
        }


        /// <summary>
        /// 平衡二元樹的基本概念
        /// 定義：平衡二元樹是一種二元樹，旨在保持樹的高度盡可能低，以確保操作（如插入、刪除和搜尋）的時間複雜度接近 O(log n)。
        /// 特性：每個節點的左子樹和右子樹的高度差不超過 1。常見的平衡二元樹包括 AVL 樹和紅黑樹。
        /// 
        /// ref:
        /// https://leetcode.cn/problems/balanced-binary-tree/solutions/377216/ping-heng-er-cha-shu-by-leetcode-solution/
        /// https://leetcode.cn/problems/balanced-binary-tree/solutions/2015068/ru-he-ling-huo-yun-yong-di-gui-lai-kan-s-c3wj/
        /// 
        /// 方法從上至下搜尋
        /// 遞迴持續搜尋
        /// 再加上 判斷高度 必須小於等於 1.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool IsBalanced(TreeNode root)
        {
            if(root == null)
            {
                return true;
            }
            else
            {
                // 左右子樹成立且高度差不能超過 1
                return Math.Abs(height(root.left) - height(root.right)) <= 1 && IsBalanced(root.left) && IsBalanced(root.right);
            }
        }


        /// <summary>
        /// 空子樹 高度 0
        /// 非空子樹計算出來之後要 + 1, 從第一層開始
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int height(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                // 取最大子樹 + 1
                return Math.Max(height(root.left), height(root.right)) + 1;
            }
        }


        /// <summary>
        /// 方法2
        /// 
        /// -1: False
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool IsBalanced2(TreeNode root)
        {
            return height2(root) != -1;
        }


        /// <summary>
        /// 空子樹 高度 0
        /// 非空子樹計算出來之後要 + 1, 從第一層開始
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int height2(TreeNode root)
        {
            if(root == null)
            {
                // 空子樹高度 0
                return 0;
            }

            int leftH = height2(root.left);
            if(leftH == -1)
            {
                // 提前退出, 不再遞迴
                return -1;
            }

            int rightH = height2(root.right);
            if(rightH == -1 || Math.Abs(leftH - rightH) > 1)
            {
                // 兩邊子樹差距太大, 給 -1
                return -1;
            }

            // 取最大子樹 + 1
            return Math.Max(leftH, rightH) + 1;
        }

    }
}
