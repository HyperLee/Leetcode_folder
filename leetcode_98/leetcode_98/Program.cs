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

            Console.WriteLine("res: " + IsValidBST(root));
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/validate-binary-search-tree/solutions/230256/yan-zheng-er-cha-sou-suo-shu-by-leetcode-solution/
        /// https://leetcode.cn/problems/validate-binary-search-tree/solutions/2020306/qian-xu-zhong-xu-hou-xu-san-chong-fang-f-yxvh/
        /// https://leetcode.cn/problems/validate-binary-search-tree/solutions/1459478/98-yan-zheng-er-cha-sou-suo-shu-by-storm-4awq/
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool IsValidBST(TreeNode root)
        {
            return helper(root, long.MinValue, long.MaxValue);
        }


        /// <summary>
        /// 遞迴解法
        /// 
        /// 根據二叉搜索樹的性質，在遞迴調用左子樹時，我們需要將上界（upper）改為 root.val，即調用 helper(root.left, lower, root.val)，因為左子樹裡所有節點的值均小於它的根節點的值。
        /// 同理，遞迴調用右子樹時，我們需要將下界（lower）改為 root.val，即調用 helper(root.right, root.val, upper)。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="lowerbound">上界(最大值)</param>
        /// <param name="upperbound">下界(最小值)</param>
        /// <returns></returns>
        public static bool helper(TreeNode node, long lowerbound, long upperbound)
        {
            if(node == null)
            {
                return true;
            }

            // 判斷節點值是否在合法範圍內, 不合法則返回 false
            // BST 左子樹的所有節點值 < 根節點值
            // BST 右子樹的所有節點值 > 根節點值
            if (node.val <= lowerbound || node.val >= upperbound)
            {
                return false;
            }

            // 遞迴調用左子樹, 更新上界
            // 遞迴調用右子樹, 更新下界
            return helper(node.left, lowerbound, node.val) && helper(node.right, node.val, upperbound);
        }
    }
}
