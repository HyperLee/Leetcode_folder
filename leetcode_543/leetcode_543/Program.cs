namespace leetcode_543
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
        /// 543. Diameter of Binary Tree
        /// https://leetcode.com/problems/diameter-of-binary-tree/description/
        /// 543. 二叉树的直径
        /// https://leetcode.cn/problems/diameter-of-binary-tree/description/
        /// 
        /// Q:
        /// 給定一棵二元樹的根節點，返回該樹的直徑長度。
        /// 二元樹的直徑是樹中"任意兩個節點"之間最長路徑的長度。這條路徑可以經過根節點，也可以不經過根節點。
        /// 兩個節點之間路徑的長度表示為它們之間的邊數。
        /// 
        /// 注意題目有說是任意兩節點, 所以最上層的 root, 不是必須要經過的節點之一
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(9);
            root.val = 1;
            root.left = new TreeNode(9);
            root.right = new TreeNode(20);
            root.left.val = 2;
            root.right.val = 3;
            //root.left.left = new TreeNode(9);
            root.right.left = new TreeNode(9);
            root.right.right = new TreeNode(9);
            root.right.left.val = 4;
            root.right.right.val = 5;

            var res = DiameterOfBinaryTree(root);
            Console.WriteLine("res: " + res);

        }

        public static int max = 0;

        /// <summary>
        /// ref:
        /// https://ithelp.ithome.com.tw/articles/10227129
        /// 
        /// 注意例外 case
        /// 只要找出 root.left 的 MaxDepth 及 root.right 的 MaxDepth 相加就好
        /// 概念是對的，但應該要改成找出 所有節點的 left MaxDepth 及 所有節點的 right MaxDepth 相加最大值 才對
        /// 因為會有這種奇怪情況，最長路徑不經過 root，所以要全部都掃過
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int DiameterOfBinaryTree(TreeNode root)
        {
            MaxDepth(root);
            return max;
        }


        /// <summary>
        /// 遞回
        /// 找出 左子樹 最大深度
        /// 找出 右子樹 最大深度
        /// 持續更新紀錄 最大值( left + right)
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MaxDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            //找出 left 節點的最大深度
            int left = MaxDepth(root.left);

            // 找出 right 節點的最大深度
            int right = MaxDepth(root.right);

            // 持續更新答案,
            // 比較 max 及 left 的最大深度 + right 的最大深度，並將較大的紀錄在 max
            // 這邊就是在紀錄除了 root 外的最長路徑
            max = Math.Max(max, left + right);

            // 此時的 + 1 就是 往下多一階 的意思
            return Math.Max(left, right) + 1;
        }
    }
}
