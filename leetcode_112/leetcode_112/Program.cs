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
        /// 初始化 Path Sum 範例與遞迴運算使用的二元樹節點。
        /// 輸入條件：<paramref name="left"/> 與 <paramref name="right"/> 可以為 null。
        /// 輸出結果：建立一個包含 <paramref name="val"/> 與指定子節點的節點。
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
    /// 輸出一筆可執行的 Path Sum 範例，包含預期結果與實際結果。
    /// 解題概念：對準備好的樹呼叫 <see cref="HasPathSum"/>，並比較布林結果。
    /// 輸入條件：<paramref name="root"/> 可以為 null，且 <paramref name="expected"/> 為已知答案。
    /// 輸出結果：寫出一行固定格式的結果，可作為人工驗證範例。
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
    /// 遞迴解法
    /// 判斷二元樹中是否存在一條從根節點到葉節點的路徑，使節點值總和等於 targetSum。
    /// 解題概念：使用深度優先遞迴，並在走訪每個節點時從剩餘總和中扣除該節點值。
    /// 輸入條件：<paramref name="root"/> 可以為 null；<paramref name="targetSum"/> 是要求的路徑總和。
    /// 輸出結果：只有當某條路徑走到葉節點且剛好用完目標總和時，才回傳 true。
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
