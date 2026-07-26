namespace leetcode_2265;

internal class Program
{
    /// <summary>
    /// LeetCode 2265 - Count Nodes Equal to Average of Subtree.
    /// LeetCode 2265 - 統計值等於子樹平均值的節點數。
    /// English: https://leetcode.com/problems/count-nodes-equal-to-average-of-subtree/
    /// 中文：https://leetcode.cn/problems/count-nodes-equal-to-average-of-subtree/
    /// English: For every node in a binary tree, count it when its value equals the integer average
    /// of all values in its own subtree.
    /// 中文：對二元樹的每個節點，若其值等於自身子樹所有節點值的整數平均，便將它計入答案。
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