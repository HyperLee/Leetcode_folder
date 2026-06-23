namespace leetcode_222;

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
    /// 222. Count Complete Tree Nodes
    /// https://leetcode.com/problems/count-complete-tree-nodes/description/
    /// 222. 完全二叉树的节点个数
    /// https://leetcode.cn/problems/count-complete-tree-nodes/description/
    ///
    /// Given the root of a complete binary tree, return the number of the nodes in the tree.
    ///
    /// According to [Wikipedia](http://en.wikipedia.org/wiki/Binary_tree#Types_of_binary_trees), every level, except possibly the last, is completely filled in a complete binary tree, and all nodes in the last level are as far left as possible.
    /// It can have between 1 and 2^h nodes inclusive at the last level h.
    ///
    /// Design an algorithm that runs in less than O(n) time complexity.
    ///
    /// 給定一棵完全二元樹的根節點，請回傳樹中節點的數量。
    ///
    /// 根據維基百科，完全二元樹除了最後一層之外，每一層皆完全填滿，且最後一層的節點會盡可能靠左。
    /// 最後一層 h 的節點數可介於 1 到 2^h（含）之間。
    ///
    /// 請設計一個時間複雜度低於 O(n) 的演算法。
    /// </summary>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一:遞迴
    /// 问题可以拆分为计算自己、左子树节点数和右子节树节点数的小问题，可以使用递归实现。
    /// 递归三步曲：
    /// 1. 定义函数功能：计算完全二叉树的节点个数，参数为树的根节点 root。
    /// 2. 结束条件：当 root == null 时，返回 0。
    /// 3. 递归公式：节点个数 = 1 + CountNodes(root.left) + CountNodes(root.right)。
    /// 
    /// 注意 要加 1
    /// 每遇到一個 node 就 + 1
    /// 累加 加總意思
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int CountNodes(TreeNode root)
    {
        if(root == null)
        {
            return 0;
        }

        return CountNodes(root.left) + CountNodes(root.right) + 1;
    }

    /// <summary>
    /// 解法二: 迭代
    /// 使用层序遍历
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int CountNodes2(TreeNode root)
    {
        if(root == null)
        {
            return 0;
        }

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int count = 0;
        while(queue.Count > 0)
        {
            var c = queue.Count;
            for(int i = 0; i < c; i++)
            {
                count++;
                var node = queue.Dequeue();
                if(node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if(node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }
        }
        return count;
    }
}
