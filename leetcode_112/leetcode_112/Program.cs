namespace leetcode_112;

/// <summary>
/// Provides runnable examples and the LeetCode 112 Path Sum solution.
/// </summary>
class Program
{
    /// <summary>
    /// Represents one node in a binary tree.
    /// Input conditions: child references can be null when a node has no left or right child.
    /// Output result: each instance stores a node value and links to optional child nodes.
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// The integer value stored in the node.
        /// </summary>
        public int val;

        /// <summary>
        /// The left child node, or null when no left child exists.
        /// </summary>
        public TreeNode? left;

        /// <summary>
        /// The right child node, or null when no right child exists.
        /// </summary>
        public TreeNode? right;

        /// <summary>
        /// Initializes a binary tree node for Path Sum examples and recursion.
        /// Input conditions: <paramref name="left"/> and <paramref name="right"/> may be null.
        /// Output result: creates a node containing <paramref name="val"/> and the supplied children.
        /// </summary>
        /// <param name="val">The node value.</param>
        /// <param name="left">The optional left child.</param>
        /// <param name="right">The optional right child.</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
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
    /// <param name="args">Command-line arguments are not used.</param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        TreeNode sampleTree = new TreeNode(
            5,
            new TreeNode(
                4,
                new TreeNode(
                    11,
                    new TreeNode(7),
                    new TreeNode(2))),
            new TreeNode(
                8,
                new TreeNode(13),
                new TreeNode(
                    4,
                    null,
                    new TreeNode(1))));

        TreeNode singleNodeTree = new TreeNode(1);
        TreeNode negativeTree = new TreeNode(-2, null, new TreeNode(-3));
        TreeNode prefixOnlyTree = new TreeNode(1, null, new TreeNode(2, null, new TreeNode(3)));

        Console.WriteLine("LeetCode 112 Path Sum");
        PrintExample(solution, "Example 1 - sample target 22", sampleTree, 22, true);
        PrintExample(solution, "Example 2 - sample target 5", sampleTree, 5, false);
        PrintExample(solution, "Example 3 - empty tree target 0", null, 0, false);
        PrintExample(solution, "Example 4 - single node target 1", singleNodeTree, 1, true);
        PrintExample(solution, "Example 5 - negative path target -5", negativeTree, -5, true);
        PrintExample(solution, "Example 6 - prefix-only target 3", prefixOnlyTree, 3, false);
    }

    /// <summary>
    /// Prints one executable Path Sum example with its expected and actual result.
    /// Solution concept: call <see cref="HasPathSum"/> on a prepared tree and compare the boolean result.
    /// Input conditions: <paramref name="root"/> may be null, and <paramref name="expected"/> is the known answer.
    /// Output result: writes a deterministic line that can be used as a manual verification example.
    /// </summary>
    /// <param name="solution">The solution instance that contains the recursive method.</param>
    /// <param name="name">The example label printed to the console.</param>
    /// <param name="root">The binary tree root, or null for an empty tree.</param>
    /// <param name="targetSum">The target root-to-leaf path sum.</param>
    /// <param name="expected">The expected result for the example.</param>
    private static void PrintExample(
        Program solution,
        string name,
        TreeNode? root,
        int targetSum,
        bool expected)
    {
        bool actual = solution.HasPathSum(root, targetSum);

        Console.WriteLine($"{name}: expected={expected}, actual={actual}, pass={actual == expected}");
    }

    /// <summary>
    /// Determines whether a binary tree contains a root-to-leaf path whose node values sum to targetSum.
    /// Solution concept: use depth-first recursion and subtract each visited node value from the remaining sum.
    /// Input conditions: <paramref name="root"/> may be null; <paramref name="targetSum"/> is the required path sum.
    /// Output result: returns true only when a path reaches a leaf and exactly consumes the target sum.
    ///
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
    /// <param name="root">The current tree node being checked, or null when the subtree is empty.</param>
    /// <param name="targetSum">The remaining sum required for the current root-to-leaf path.</param>
    /// <returns>True if at least one root-to-leaf path equals <paramref name="targetSum"/>; otherwise false.</returns>
    public bool HasPathSum(TreeNode? root, int targetSum)
    {
        if (root == null)
        {
            return false;
        }

        // 只有葉節點才能決定路徑是否成立，避免把中途節點的前綴和誤判為答案。
        if (root.left == null && root.right == null)
        {
            return targetSum == root.val;
        }

        // 將目前節點值扣掉後，把剩餘目標交給左右子樹繼續尋找。
        return HasPathSum(root.left, targetSum - root.val) || HasPathSum(root.right, targetSum - root.val);
    }
}
