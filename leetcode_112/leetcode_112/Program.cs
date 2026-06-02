namespace leetcode_112;

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
    /// 112. Path Sum
    /// English:
    /// Given the root of a binary tree and an integer targetSum, return true if the tree has a root-to-leaf path such that adding up all the values along the path equals targetSum.
    /// A leaf is a node with no children.
    ///
    /// Traditional Chinese:
    /// 給定一棵二元樹的根節點 root 和一個整數 targetSum，如果樹中存在一條從根節點到葉節點的路徑，使得沿途所有節點值的總和等於 targetSum，則回傳 true。
    /// 葉節點是沒有子節點的節點。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 遞迴
    /// 
    /// 观察要求我们完成的函数，我们可以归纳出它的功能：询问是否存在从当前节点 root 到叶子节点的路径，满足其路径和为 sum。
    /// 假定从根节点到当前节点的值之和为 val，我们可以将这个大问题转化为一个小问题：是否存在从当前节点的子节点到叶子的路
    /// 径，满足其路径和为 sum - val。
    /// 不难发现这满足递归的性质，若当前节点就是叶子节点，那么我们直接判断 sum 是否等于 val 即可（因为路径和已经确定，就是
    /// 当前节点的值，我们只需要判断该路径和是否满足条件）。若当前节点不是叶子节点，我们只需要递归地询问它的子节点是否能满足
    /// 条件即可。
    /// 当题目中提到了 叶子节点 时，正确的做法一定要同时判断节点的 左右子树同时为空 才是叶子节点。
    /// 叶子节点 是指没有子节点的节点。
    /// 
    /// 簡單說就是 路徑總和 要與 targetSum 相同
    /// 所以
    /// 從 root 往下走 每走到一個 node 就扣減該 node value 
    /// => targetSum - root.val
    /// 直到 走到 leaf node 為止
    /// 此時判斷 targetSum == root.val 是否相同
    /// 相同即是 true
    /// 反之則是 false
    /// </summary>
    /// <param name="root"></param>
    /// <param name="targetSum"></param>
    /// <returns></returns>
    public bool HasPathSum(TreeNode root, int targetSum)
    {
        if(root == null)
        {
            return false;
        }

        // 沒有左右子樹的節點 => 葉子節點
        if(root.left == null && root.right == null)
        {
            return targetSum == root.val;
        }

        return HasPathSum(root.left, targetSum - root.val) || HasPathSum(root.right, targetSum - root.val);
    }
}
