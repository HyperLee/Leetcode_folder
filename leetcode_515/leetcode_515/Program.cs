using System.Collections.Generic;
using System.Linq;

namespace leetcode_515;

internal static class Program
{
    /// <summary>
    /// 表示二叉樹中的節點，保留 LeetCode 題目使用的 val、left 與 right 欄位。
    /// </summary>
    public sealed class TreeNode
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

    private sealed record AcceptanceCase(
        string Name,
        string Input,
        TreeNode? Root,
        int[] Expected);

    private sealed record CheckResult(
        string Name,
        string Input,
        string Expected,
        string Actual,
        bool Passed);

    /// <summary>
    /// 515. Find Largest Value in Each Tree Row
    /// https://leetcode.com/problems/find-largest-value-in-each-tree-row/
    /// 515. 在每个树行中找最大值
    /// https://leetcode.cn/problems/find-largest-value-in-each-tree-row/
    /// Given the root of a binary tree, return an array of the largest value in each row of the tree (0-indexed).
    /// 給定一棵二叉樹的根節點 root，請找出該二叉樹中每一層的最大值。
    /// 節點數量介於 0 到 10^4，節點值介於 -2^31 到 2^31 - 1。
    /// </summary>
    private static void Main()
    {
        AcceptanceCase[] regularCases =
        [
            new(
                "Official example 1",
                "root = [1,3,2,5,3,null,9]",
                new TreeNode(
                    1,
                    new TreeNode(3, new TreeNode(5), new TreeNode(3)),
                    new TreeNode(2, null, new TreeNode(9))),
                [1, 3, 9]),
            new(
                "Official example 2",
                "root = [1,2,3]",
                new TreeNode(1, new TreeNode(2), new TreeNode(3)),
                [1, 3]),
            new(
                "Empty tree",
                "root = null",
                null,
                []),
            new(
                "Single int.MinValue node",
                "root = [-2147483648]",
                new TreeNode(int.MinValue),
                [int.MinValue]),
            new(
                "Negative values and zero",
                "root = [-10,-20,-5,null,null,-2147483648,0]",
                new TreeNode(
                    -10,
                    new TreeNode(-20),
                    new TreeNode(-5, new TreeNode(int.MinValue), new TreeNode(0))),
                [-10, -5, 0]),
            new(
                "Left-skewed tree",
                "root = [1,2,null,3,null,4]",
                new TreeNode(1, new TreeNode(2, new TreeNode(3, new TreeNode(4)))),
                [1, 2, 3, 4]),
            new(
                "Same-level maximum update",
                "root = [1,2,3,4,10,8,7]",
                new TreeNode(
                    1,
                    new TreeNode(2, new TreeNode(4), new TreeNode(10)),
                    new TreeNode(3, new TreeNode(8), new TreeNode(7))),
                [1, 3, 10]),
            new(
                "Integer boundaries",
                "root = [-2147483648,2147483647,-1,-2147483648,2147483647,null,0]",
                new TreeNode(
                    int.MinValue,
                    new TreeNode(int.MaxValue, new TreeNode(int.MinValue), new TreeNode(int.MaxValue)),
                    new TreeNode(-1, null, new TreeNode(0))),
                [int.MinValue, int.MaxValue, int.MaxValue])
        ];

        CheckResult[] checks = new CheckResult[10];

        for (int i = 0; i < regularCases.Length; i++)
        {
            checks[i] = EvaluateCase(regularCases[i]);
        }

        TreeNode readOnlyRoot = new TreeNode(
            5,
            new TreeNode(1, null, new TreeNode(6)),
            new TreeNode(9, new TreeNode(12), null));
        checks[8] = EvaluateReadOnlyCase(
            "Read-only tree structure",
            "root = [5,1,9,null,6,12]",
            readOnlyRoot,
            [5, 9, 12]);

        checks[9] = EvaluateCase(new AcceptanceCase(
            "10,000-node complete tree",
            "nodeCount = 10000; value = heap index",
            BuildCompleteTree(10_000),
            [0, 2, 6, 14, 30, 62, 126, 254, 510, 1022, 2046, 4094, 8190, 9999]));

        Console.WriteLine("LeetCode 515 acceptance harness");
        Console.WriteLine();

        int passedCount = 0;

        for (int i = 0; i < checks.Length; i++)
        {
            CheckResult check = checks[i];

            Console.WriteLine($"Case {i + 1}: {check.Name}");
            Console.WriteLine($"Input: {check.Input}");
            Console.WriteLine($"Expected: {check.Expected}");
            Console.WriteLine($"Actual: {check.Actual}");
            Console.WriteLine($"Result: {(check.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            if (check.Passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine($"Summary: {passedCount}/{checks.Length} checks passed.");

        if (passedCount != checks.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 以深度優先搜尋走訪二叉樹，回傳每一層的最大值；root 可為 null，代表空樹並回傳空集合。
    /// </summary>
    public static IList<int> LargestValues(TreeNode? root)
    {
        if (root is null)
        {
            return new List<int>();
        }

        IList<int> result = new List<int>();
        DepthFirstSearch(result, root, 0);
        return result;
    }

    /// <summary>
    /// 依目前深度更新對應層的最大值，並在有效節點存在時遞迴處理左右子樹。
    /// </summary>
    private static void DepthFirstSearch(IList<int> result, TreeNode node, int depth)
    {
        if (depth == result.Count)
        {
            // 第一次抵達某深度時，當前節點就是該層第一個候選值。
            result.Add(node.val);
        }
        else
        {
            // 同層後續節點只需與目前最大值比較，不需要額外儲存整層資料。
            result[depth] = Math.Max(result[depth], node.val);
        }

        if (node.left is not null)
        {
            DepthFirstSearch(result, node.left, depth + 1);
        }

        if (node.right is not null)
        {
            DepthFirstSearch(result, node.right, depth + 1);
        }
    }

    /// <summary>
    /// 執行一個題目案例並回傳可由 Main 顯示的驗證資料，不直接寫入主控台。
    /// </summary>
    private static CheckResult EvaluateCase(AcceptanceCase testCase)
    {
        IList<int> actual = LargestValues(testCase.Root);
        bool passed = actual.SequenceEqual(testCase.Expected);

        return new CheckResult(
            testCase.Name,
            testCase.Input,
            FormatValues(testCase.Expected),
            FormatValues(actual),
            passed);
    }

    /// <summary>
    /// 驗證解法回傳正確結果，且不修改題目輸入樹的節點值與左右連結。
    /// </summary>
    private static CheckResult EvaluateReadOnlyCase(
        string name,
        string input,
        TreeNode root,
        int[] expected)
    {
        TreeNode originalLeft = root.left ?? throw new InvalidOperationException("Fixture must have a left child.");
        TreeNode originalRight = root.right ?? throw new InvalidOperationException("Fixture must have a right child.");
        TreeNode originalLeftRight = originalLeft.right ?? throw new InvalidOperationException("Fixture must have a left-right child.");
        TreeNode originalRightLeft = originalRight.left ?? throw new InvalidOperationException("Fixture must have a right-left child.");

        IList<int> actual = LargestValues(root);
        bool structureUnchanged =
            root.val == 5 &&
            originalLeft.val == 1 &&
            originalLeftRight.val == 6 &&
            originalRight.val == 9 &&
            originalRightLeft.val == 12 &&
            ReferenceEquals(root.left, originalLeft) &&
            ReferenceEquals(root.right, originalRight) &&
            ReferenceEquals(originalLeft.right, originalLeftRight) &&
            ReferenceEquals(originalRight.left, originalRightLeft);
        bool passed = actual.SequenceEqual(expected) && structureUnchanged;

        return new CheckResult(
            name,
            input,
            $"values = {FormatValues(expected)}; structure unchanged = True",
            $"values = {FormatValues(actual)}; structure unchanged = {structureUnchanged}",
            passed);
    }

    /// <summary>
    /// 建立以 heap index 作為節點值的完整二叉樹，供上限規模 spot check 使用。
    /// </summary>
    private static TreeNode BuildCompleteTree(int nodeCount)
    {
        TreeNode[] nodes = new TreeNode[nodeCount];

        for (int i = 0; i < nodeCount; i++)
        {
            nodes[i] = new TreeNode(i);
        }

        for (int i = 0; i < nodeCount; i++)
        {
            int leftIndex = (2 * i) + 1;
            int rightIndex = leftIndex + 1;

            if (leftIndex < nodeCount)
            {
                nodes[i].left = nodes[leftIndex];
            }

            if (rightIndex < nodeCount)
            {
                nodes[i].right = nodes[rightIndex];
            }
        }

        return nodes[0];
    }

    /// <summary>
    /// 將整數序列格式化為 acceptance harness 使用的方括號文字。
    /// </summary>
    private static string FormatValues(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }
}
