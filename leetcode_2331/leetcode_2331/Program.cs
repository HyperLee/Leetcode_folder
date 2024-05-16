namespace leetcode_2331
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
        /// 2331. Evaluate Boolean Binary Tree
        /// https://leetcode.com/problems/evaluate-boolean-binary-tree/description/?envType=daily-question&envId=2024-05-16
        /// 2331. 计算布尔二叉树的值
        /// https://leetcode.cn/problems/evaluate-boolean-binary-tree/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(0);
            Console.WriteLine(EvaluateTree(root));
            Console.ReadKey();
        }



        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/evaluate-boolean-binary-tree/solutions/2091770/ji-suan-bu-er-er-cha-shu-de-zhi-by-leetc-4g8f/
        /// https://leetcode.cn/problems/evaluate-boolean-binary-tree/solutions/1658331/by-endlesscheng-391l/
        /// https://leetcode.cn/problems/evaluate-boolean-binary-tree/solutions/2576477/2331-ji-suan-bu-er-er-cha-shu-de-zhi-by-ao71a/
        /// 
        /// 葉子節點 Leaf nodes
        /// val:
        /// 0: false
        /// 1: true
        /// 
        /// 非葉節點 Non-leaf nodes
        /// val:
        /// 2: or
        /// 3: and
        /// 
        /// 
        /// 葉節點,直接返回該node val 數值
        /// A full binary tree is a binary tree where each node has either 0 or 2 children.
        /// A leaf node is a node that has zero children.
        /// ==> 葉節點只需判斷單邊(左或右)即可, 葉節點不會有子節點
        /// ==> 非葉節點會有兩個子節點
        /// 
        /// 非葉節點就根據題目要求
        /// 拿出下屬兩個子節點數值做運算
        /// 如果該node.val為2 就是 or
        /// 如果該node.val為3 就是 and
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool EvaluateTree(TreeNode root)
        {
            // Leaf nodes 判斷單邊 || 即可用 && 效率差一點,也沒必要
            if (root.left == null || root.right == null)
            {
                // 0: false  1: true
                return root.val == 0 ? false : true;
            }

            // Non-leaf nodes
            if (root.val == 2)
            {
                // val == 2 => or
                return EvaluateTree(root.left) || EvaluateTree(root.right);
            }
            else
            {
                // val == 3 => and
                return EvaluateTree(root.left) && EvaluateTree(root.right);
            }

        }
    }
}
