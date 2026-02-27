namespace leetcode_783;

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
    /// 783. Minimum Distance Between BST Nodes
    /// https://leetcode.com/problems/minimum-distance-between-bst-nodes/description/
    ///
    /// Problem description (English):
    /// Given the root of a Binary Search Tree (BST), return the minimum difference
    /// between the values of any two different nodes in the tree.
    ///
    /// 題目描述（繁體中文）：
    /// 給定一個二叉搜尋樹（BST）的根節點，返回樹中任意兩個不同節點
    /// 值之間的最小差距。
    ///
    /// 783. 二叉搜索树节点最小距离
    /// https://leetcode.cn/problems/minimum-distance-between-bst-nodes/description/
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int MinDiffInBST(TreeNode root)
    {
        List<int> list = new List<int>();
        dfs(root, list);

        Array.Sort(list.ToArray());

        int n = list.Count;
        int res = int.MaxValue;

        for(int i = 1; i < n; i++)
        {
            int cur = Math.Abs(list[i] - list[i - 1]);
            res = Math.Min(res, cur);
        }
        return res;
    }

    /// <summary>
    /// tree 基本考題
    /// 前序（根左右）、中序（左根右）、后序（左右根）
    /// Inorder traversal (中序遍歷) 會先拜訪左子節點，再拜訪父節點，最後拜訪右子節點。
    /// </summary>
    /// <param name="root"></param>
    /// <param name="list"></param>
    public void dfs(TreeNode root, List<int> list)
    {
        if(root is null)
        {
            return;
        }

        if(root.left is not null)
        {
            dfs(root.left, list);
        }

        list.Add(root.val);

        if(root.right is not null)
        {
            dfs(root.right, list);
        }
        
    }
}
