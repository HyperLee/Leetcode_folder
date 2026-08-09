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
    /// <para>
    /// 112. Path Sum
    /// https://leetcode.com/problems/path-sum/description/
    ///
    /// Given the root of a binary tree and an integer targetSum, return true if the tree has a root-to-leaf
    /// path such that adding up all the values along the path equals targetSum.
    /// A leaf is a node with no children.
    ///
    /// Example 1:
    /// Input: root = [5,4,8,11,null,13,4,7,2,null,null,null,1], targetSum = 22
    /// Output: true
    /// Illustration: https://assets.leetcode.com/uploads/2021/01/18/pathsum1.jpg
    /// Explanation: The root-to-leaf path with the target sum is shown.
    ///
    /// Example 2:
    /// Input: root = [1,2,3], targetSum = 5
    /// Output: false
    /// Illustration: https://assets.leetcode.com/uploads/2021/01/18/pathsum2.jpg
    /// Explanation: There are two root-to-leaf paths in the tree:
    /// (1 --&gt; 2): The sum is 3.
    /// (1 --&gt; 3): The sum is 4.
    /// There is no root-to-leaf path with sum = 5.
    ///
    /// Example 3:
    /// Input: root = [], targetSum = 0
    /// Output: false
    /// Explanation: Since the tree is empty, there are no root-to-leaf paths.
    ///
    /// Constraints:
    /// The number of nodes in the tree is in the range [0, 5000].
    /// -1000 &lt;= Node.val &lt;= 1000
    /// -1000 &lt;= targetSum &lt;= 1000
    /// </para>
    /// <para>
    /// 112. 路徑總和
    /// https://leetcode.cn/problems/path-sum/description/
    ///
    /// 給定二元樹的根節點 root 與整數 targetSum，若樹中存在一條從根節點到葉節點的路徑，
    /// 且沿途所有節點值的總和等於 targetSum，則回傳 true。
    /// 葉節點是沒有子節點的節點。
    ///
    /// 範例 1：
    /// 輸入：root = [5,4,8,11,null,13,4,7,2,null,null,null,1], targetSum = 22
    /// 輸出：true
    /// 示意圖：https://assets.leetcode.com/uploads/2021/01/18/pathsum1.jpg
    /// 解釋：圖中顯示總和等於目標值的根至葉路徑。
    ///
    /// 範例 2：
    /// 輸入：root = [1,2,3], targetSum = 5
    /// 輸出：false
    /// 示意圖：https://assets.leetcode.com/uploads/2021/01/18/pathsum2.jpg
    /// 解釋：樹中有兩條從根節點到葉節點的路徑：
    /// (1 --&gt; 2)：總和為 3。
    /// (1 --&gt; 3)：總和為 4。
    /// 不存在總和為 5 的根至葉路徑。
    ///
    /// 範例 3：
    /// 輸入：root = [], targetSum = 0
    /// 輸出：false
    /// 解釋：由於樹為空，因此沒有任何根至葉路徑。
    ///
    /// 限制條件：
    /// 樹中的節點數量介於 [0, 5000]。
    /// -1000 &lt;= Node.val &lt;= 1000
    /// -1000 &lt;= targetSum &lt;= 1000
    /// </para>
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
