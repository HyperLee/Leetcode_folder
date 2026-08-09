namespace leetcode_2265;

internal class Program
{
    /// <summary>
    /// <para>
    /// 2265. Count Nodes Equal to Average of Subtree
    /// https://leetcode.com/problems/count-nodes-equal-to-average-of-subtree/description/
    ///
    /// Given a binary tree root, return the number of nodes whose value equals the average of all values in their subtree. The average of n elements is their sum divided by n, rounded down. A node's subtree contains that node and all descendants.
    ///
    /// Images: https://assets.leetcode.com/uploads/2022/03/15/image-20220315203925-1.png and https://assets.leetcode.com/uploads/2022/03/26/image-20220326133920-1.png
    ///
    /// Example 1:
    /// Input: root = [4,8,5,0,1,null,6]
    /// Output: 5
    /// Explanation: Node 4 has average (4 + 8 + 5 + 0 + 1 + 6) / 6 = 24 / 6 = 4; node 5 has (5 + 6) / 2 = 11 / 2 = 5; nodes 0, 1, 6 have averages 0 / 1 = 0, 1 / 1 = 1, 6 / 1 = 6.
    ///
    /// Example 2:
    /// Input: root = [1]
    /// Output: 1
    /// Explanation: Node 1 has subtree average 1 / 1 = 1.
    ///
    /// Constraints:
    /// - The number of nodes is in [1,1000].
    /// - 0 &lt;= Node.val &lt;= 1000
    /// </para>
    /// <para>
    /// 2265. 統計值等於子樹平均值的節點數
    /// https://leetcode.cn/problems/count-nodes-equal-to-average-of-subtree/description/
    ///
    /// 給定二元樹 root，回傳節點值等於其子樹所有值平均數的節點數量。n 個元素的平均值是總和除以 n 並向下取整。節點的子樹包含該節點與所有後代。
    ///
    /// 圖片：https://assets.leetcode.com/uploads/2022/03/15/image-20220315203925-1.png 與 https://assets.leetcode.com/uploads/2022/03/26/image-20220326133920-1.png
    ///
    /// 範例 1：
    /// 輸入：root = [4,8,5,0,1,null,6]
    /// 輸出：5
    /// 說明：節點 4 的平均為 (4 + 8 + 5 + 0 + 1 + 6) / 6 = 24 / 6 = 4；節點 5 為 (5 + 6) / 2 = 11 / 2 = 5；節點 0、1、6 的平均分別為 0 / 1 = 0、1 / 1 = 1、6 / 1 = 6。
    ///
    /// 範例 2：
    /// 輸入：root = [1]
    /// 輸出：1
    /// 說明：節點 1 的子樹平均為 1 / 1 = 1。
    ///
    /// 限制條件：
    /// - 節點數量在 [1,1000] 範圍內。
    /// - 0 &lt;= Node.val &lt;= 1000
    /// </para>
    /// </summary>
    private static void Main()
    {
        (string Name, string Input, string Expected, Func<string> Evaluate)[] cases =
        [
            ("Official example", "[4,8,5,0,1,null,6]", "5", () => AverageOfSubtree(CreateOfficialTree()).ToString()),
            ("Single node", "[1]", "1", () => AverageOfSubtree(new TreeNode(1)).ToString()),
            ("Root equals truncated average", "[2,1,4]", "3", () => AverageOfSubtree(new TreeNode(2, new TreeNode(1), new TreeNode(4))).ToString()),
            ("Root does not equal average", "[9,1,1]", "2", () => AverageOfSubtree(new TreeNode(9, new TreeNode(1), new TreeNode(1))).ToString()),
            ("All zeroes", "[0,0,0]", "3", () => AverageOfSubtree(new TreeNode(0, new TreeNode(0), new TreeNode(0))).ToString()),
            ("Right-skewed mixed values", "[3,null,1,null,0]", "1", () => AverageOfSubtree(new TreeNode(3, null, new TreeNode(1, null, new TreeNode(0)))).ToString()),
            ("Repeated call on same official tree", "same [4,8,5,0,1,null,6] instance", "(5, 5)", EvaluateRepeatedCall),
            ("Truncating average and tree topology preservation", "snapshot [2,1]", "1; True", VerifyTruncatingAverageAndTopologyPreserved),
            ("Right-skewed limit spot check", "1000 zero-valued nodes", "1000", () => AverageOfSubtree(CreateRightSkewedZeroTree(1000)).ToString())
        ];

        int passed = 0;

        foreach ((string name, string input, string expected, Func<string> evaluate) in cases)
        {
            string actual = evaluate();
            bool isPass = actual == expected;

            Console.WriteLine($"Case: {name}; Input: {input}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine(isPass ? "PASS" : "FAIL");

            if (isPass)
            {
                passed++;
            }
        }

        Console.WriteLine($"Summary: {passed}/{cases.Length} checks passed.");

        if (passed != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 以後序走訪統計值等於子樹整數平均的節點數。輸入為題目保證非空的二元樹根節點；方法不輸出、
    /// 不修改任何節點，也不保留跨呼叫狀態，回傳符合條件的節點總數。
    /// </summary>
    public static int AverageOfSubtree(TreeNode root)
    {
        return Traverse(root).Matches;
    }

    /// <summary>
    /// 後序彙總指定子樹的節點值總和、節點數與已匹配數。有效輸入可為空子節點；空節點回傳全零，
    /// 非空節點在合併左右子樹後，以整數除法判斷自身是否等於子樹平均值。
    /// </summary>
    private static (int Sum, int Count, int Matches) Traverse(TreeNode? node)
    {
        if (node is null)
        {
            return (0, 0, 0);
        }

        (int leftSum, int leftCount, int leftMatches) = Traverse(node.left);
        (int rightSum, int rightCount, int rightMatches) = Traverse(node.right);
        int sum = node.val + leftSum + rightSum;
        int count = leftCount + rightCount + 1;

        // 後序先取得完整子樹彙總，才可用題目指定的整數平均判斷目前節點。
        int matches = leftMatches + rightMatches + (node.val == sum / count ? 1 : 0);
        return (sum, count, matches);
    }

    private static TreeNode CreateOfficialTree()
    {
        return new TreeNode(4,
            new TreeNode(8, new TreeNode(0), new TreeNode(1)),
            new TreeNode(5, null, new TreeNode(6)));
    }

    private static string EvaluateRepeatedCall()
    {
        TreeNode root = CreateOfficialTree();
        return $"({AverageOfSubtree(root)}, {AverageOfSubtree(root)})";
    }

    private static string VerifyTruncatingAverageAndTopologyPreserved()
    {
        TreeNode root = new(2, new TreeNode(1));
        List<(TreeNode Node, int Value, TreeNode? Left, TreeNode? Right)> snapshot = [];
        SnapshotTopology(root, snapshot);

        int matches = AverageOfSubtree(root);
        bool isTopologyPreserved = true;

        foreach ((TreeNode node, int value, TreeNode? left, TreeNode? right) in snapshot)
        {
            if (node.val != value || node.left != left || node.right != right)
            {
                isTopologyPreserved = false;
                break;
            }
        }

        return $"{matches}; {isTopologyPreserved}";
    }

    private static void SnapshotTopology(TreeNode? node, List<(TreeNode Node, int Value, TreeNode? Left, TreeNode? Right)> snapshot)
    {
        if (node is null)
        {
            return;
        }

        snapshot.Add((node, node.val, node.left, node.right));
        SnapshotTopology(node.left, snapshot);
        SnapshotTopology(node.right, snapshot);
    }

    private static TreeNode CreateRightSkewedZeroTree(int count)
    {
        TreeNode root = new();
        TreeNode current = root;

        for (int i = 1; i < count; i++)
        {
            current.right = new TreeNode();
            current = current.right;
        }

        return root;
    }
}