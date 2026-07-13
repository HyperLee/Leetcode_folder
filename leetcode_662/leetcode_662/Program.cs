namespace leetcode_662;

public class Program
{
    /// <summary>
    /// 表示 LeetCode 二元樹節點，保留題目使用的公開欄位名稱與可為空子節點。
    /// </summary>
    public sealed class TreeNode
    {
        /// <summary>
        /// 初始化節點及其可選的左右子節點。
        /// </summary>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }

        public int val;

        public TreeNode? left;

        public TreeNode? right;
    }

    private sealed record CaseResult(string Label, int Expected, int Actual)
    {
        public bool Passed => Expected == Actual;
    }

    /// <summary>
    /// leetcode 662 Maximum Width of Binary Tree
    /// https://leetcode.com/problems/maximum-width-of-binary-tree/
    /// 662. 二元樹的最大寬度
    /// https://leetcode.cn/problems/maximum-width-of-binary-tree/
    /// Given the root of a binary tree, return the maximum width among its levels, counting null positions between the leftmost and rightmost non-null nodes.
    /// 給定二元樹根節點，回傳所有層級中的最大寬度；最左與最右非空節點之間在完整二元樹中應存在的 null 位置也要計入。
    /// </summary>
    private static void Main()
    {
        const int totalChecks = 8;
        Program solution = new();
        List<CaseResult> results =
        [
            EvaluateCase(solution, "Case 1: [1,3,2,5,3,null,9]", 4, CreateFirstExample()),
            EvaluateCase(solution, "Case 2: [1,3,2,5,null,null,9,6,null,7]", 7, CreateSecondExample()),
            EvaluateCase(solution, "Case 3: [1,3,2,5]", 2, CreateThirdExample()),
            EvaluateCase(solution, "Case 4: single node [1]", 1, new TreeNode(1)),
            EvaluateCase(solution, "Case 5: 3000-node left chain", 1, BuildLeftChain(3_000)),
            EvaluateCase(solution, "Case 6: sparse extreme paths at depth 3", 8, CreateSparseExtremeTree()),
            EvaluateCase(solution, "Case 7: same instance wide tree", 4, CreateFirstExample()),
            EvaluateCase(solution, "Case 8: same instance then single node", 1, new TreeNode(42)),
        ];

        int passedChecks = 0;

        foreach (CaseResult result in results)
        {
            if (result.Passed)
            {
                passedChecks++;
            }

            Console.WriteLine($"{result.Label} | Expected: {result.Expected} | Actual: {result.Actual} | {(result.Passed ? "PASS" : "FAIL")}");
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 以逐層廣度優先走訪計算二元樹最大寬度，並在每層將完整二元樹位置正規化，避免深度增加時索引持續膨脹。
    /// </summary>
    /// <param name="root">二元樹根節點；有效題目輸入至少有一個節點。</param>
    /// <returns>所有層級中最左與最右非空節點（含中間空位）的最大寬度；空樹回傳 0。</returns>
    public int WidthOfBinaryTree(TreeNode? root)
    {
        if (root is null)
        {
            return 0;
        }

        Queue<(TreeNode Node, long Position)> nodesToVisit = [];
        nodesToVisit.Enqueue((root, 0));
        int maximumWidth = 0;

        while (nodesToVisit.Count > 0)
        {
            int levelNodeCount = nodesToVisit.Count;
            long levelFirstPosition = nodesToVisit.Peek().Position;
            long levelLastPosition = 0;

            for (int index = 0; index < levelNodeCount; index++)
            {
                (TreeNode node, long position) = nodesToVisit.Dequeue();
                long normalizedPosition = position - levelFirstPosition;
                levelLastPosition = normalizedPosition;

                // 從本層最左位置重新編號，仍保留缺口，同時避免完整樹索引隨深度爆增。
                if (node.left is not null)
                {
                    nodesToVisit.Enqueue((node.left, normalizedPosition * 2));
                }

                if (node.right is not null)
                {
                    nodesToVisit.Enqueue((node.right, normalizedPosition * 2 + 1));
                }
            }

            int levelWidth = checked((int)(levelLastPosition + 1));
            maximumWidth = Math.Max(maximumWidth, levelWidth);
        }

        return maximumWidth;
    }

    private static CaseResult EvaluateCase(Program solution, string label, int expected, TreeNode root)
    {
        return new CaseResult(label, expected, solution.WidthOfBinaryTree(root));
    }

    private static TreeNode CreateFirstExample()
    {
        return new TreeNode(
            1,
            new TreeNode(3, new TreeNode(5), new TreeNode(3)),
            new TreeNode(2, null, new TreeNode(9)));
    }

    private static TreeNode CreateSecondExample()
    {
        return new TreeNode(
            1,
            new TreeNode(3, new TreeNode(5, new TreeNode(6)), null),
            new TreeNode(2, null, new TreeNode(9, new TreeNode(7))));
    }

    private static TreeNode CreateThirdExample()
    {
        return new TreeNode(1, new TreeNode(3, new TreeNode(5)), new TreeNode(2));
    }

    private static TreeNode BuildLeftChain(int nodeCount)
    {
        TreeNode root = new(1);
        TreeNode current = root;

        for (int value = 2; value <= nodeCount; value++)
        {
            current.left = new TreeNode(value);
            current = current.left;
        }

        return root;
    }

    private static TreeNode CreateSparseExtremeTree()
    {
        TreeNode root = new(1);
        root.left = new TreeNode(2, new TreeNode(4, new TreeNode(8)));
        root.right = new TreeNode(3, null, new TreeNode(7, null, new TreeNode(15)));
        return root;
    }
}
