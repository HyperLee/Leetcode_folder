namespace leetcode_589;

internal static class Program
{
    /// <summary>
    /// 589. N-ary Tree Preorder Traversal；589. N 元樹的前序遍歷。
    /// English: Given the root of an n-ary tree, return the preorder traversal of its nodes' values.
    /// 繁中：給定 N 元樹的根節點，回傳依前序規則排列的所有節點值。
    /// English URL: https://leetcode.com/problems/n-ary-tree-preorder-traversal/
    /// 中文 URL: https://leetcode.cn/problems/n-ary-tree-preorder-traversal/
    /// </summary>
    /// <remarks>
    /// 589. N 叉树的前序遍历
    /// https://leetcode.com/problems/n-ary-tree-preorder-traversal/description/
    /// 589. N 叉樹的前序遍歷
    /// https://leetcode.cn/problems/n-ary-tree-preorder-traversal/
    /// </remarks>
    private static void Main()
    {
        List<CaseResult> cases =
        [
            RunSequenceCase(
                "Official example 1",
                "[1,null,3,2,4,null,5,6]",
                [1, 3, 5, 6, 2, 4],
                () => Preorder(BuildOfficialExample1())),
            RunSequenceCase(
                "Official example 2",
                "[1,null,2,3,4,5,null,null,6,7,null,8,null,9,10,null,null,11,null,12,null,13,null,null,14]",
                [1, 2, 3, 6, 7, 11, 14, 4, 8, 12, 5, 9, 13, 10],
                () => Preorder(BuildOfficialExample2())),
            RunSequenceCase(
                "Minimum: null root",
                "root = null",
                [],
                () => Preorder(null)),
            RunSequenceCase(
                "Minimum: single leaf",
                "root = [42]",
                [42],
                () => Preorder(new Node(42))),
            RunSequenceCase(
                "Children keep input order",
                "root 10 -> children 30, 20, 40; 30 -> child 5",
                [10, 30, 5, 20, 40],
                () => Preorder(new Node(10, [new Node(30, [new Node(5)]), new Node(20), new Node(40)]))),
            RunRepeatedInvocationCase(),
            RunDeepChainCase(),
            RunInvariantCase()
        ];

        foreach (CaseResult caseResult in cases)
        {
            Console.WriteLine($"Case: {caseResult.Name}");
            Console.WriteLine($"Input: {caseResult.Input}");
            Console.WriteLine($"Expected: {caseResult.Expected}");
            Console.WriteLine($"Actual: {caseResult.Actual}");
            Console.WriteLine(caseResult.Passed ? "PASS" : "FAIL");
        }

        int passedCount = cases.Count(caseResult => caseResult.Passed);
        Console.WriteLine($"Summary: {passedCount}/{cases.Count} checks passed.");
        Environment.ExitCode = passedCount == cases.Count ? 0 : 1;
    }

    public sealed class Node
    {
        public int val;
        public IList<Node> children;

        public Node() : this(0, [])
        {
        }

        public Node(int value) : this(value, [])
        {
        }

        public Node(int value, IList<Node> childNodes)
        {
            val = value;
            children = childNodes;
        }
    }

    /// <summary>
    /// 依 N 元樹的前序規則遞迴走訪；有效輸入可以是空樹，非空樹則會先處理根節點，再依 children 原順序處理所有子樹，回傳節點值序列。
    /// </summary>
    /// <remarks>
    /// 本題目類似題為 leetcode_590
    /// 一個前序, 另一個為後序
    /// </remarks>
    public static IList<int> Preorder(Node? root)
    {
        List<int> values = [];
        PreorderVisit(root, values);
        return values;
    }

    /// <summary>
    /// 將目前節點及其子樹依根節點優先順序追加到結果集合；node 可以是 null，result 必須是由呼叫端提供的可寫入集合，方法不會產生主控台輸出。
    /// </summary>
    private static void PreorderVisit(Node? node, IList<int> result)
    {
        if (node is null)
        {
            return;
        }

        // 前序不變量是先記錄根值，再依 children 的原始順序完整走訪每一棵子樹。
        result.Add(node.val);

        foreach (Node child in node.children)
        {
            PreorderVisit(child, result);
        }
    }

    private static CaseResult RunSequenceCase(
        string name,
        string input,
        IReadOnlyList<int> expected,
        Func<IList<int>> execute)
    {
        try
        {
            IList<int> actual = execute();
            bool passed = expected.SequenceEqual(actual);
            return new CaseResult(name, input, FormatValues(expected), FormatValues(actual), passed);
        }
        catch (Exception exception)
        {
            return new CaseResult(name, input, FormatValues(expected), exception.GetType().Name, false);
        }
    }

    private static CaseResult RunRepeatedInvocationCase()
    {
        const string name = "Repeated invocations";
        const string input = "first root [1,2], then root [9,8]";
        const string expected = "[1, 2], then [9, 8]";

        try
        {
            IList<int> first = Preorder(new Node(1, [new Node(2)]));
            IList<int> second = Preorder(new Node(9, [new Node(8)]));
            string actual = $"{FormatValues(first)}, then {FormatValues(second)}";
            bool passed = first.SequenceEqual([1, 2]) && second.SequenceEqual([9, 8]);
            return new CaseResult(name, input, expected, actual, passed);
        }
        catch (Exception exception)
        {
            return new CaseResult(name, input, expected, exception.GetType().Name, false);
        }
    }

    private static CaseResult RunDeepChainCase()
    {
        const int nodeCount = 1000;
        const string name = "Height upper-bound spot check";
        const string input = "1000-node chain with values 0..999";
        const string expected = "count=1000, first=0, last=999";

        try
        {
            IList<int> actual = Preorder(BuildChain(nodeCount));
            bool passed = actual.Count == nodeCount
                && actual.SequenceEqual(Enumerable.Range(0, nodeCount));
            string actualSummary = actual.Count == 0
                ? "count=0"
                : $"count={actual.Count}, first={actual[0]}, last={actual[actual.Count - 1]}";
            return new CaseResult(name, input, expected, actualSummary, passed);
        }
        catch (Exception exception)
        {
            return new CaseResult(name, input, expected, exception.GetType().Name, false);
        }
    }

    private static CaseResult RunInvariantCase()
    {
        const string name = "Traversal invariants";
        const string input = "root 1 -> children 2 -> 4 and 3 -> 5";
        const string expected = "count=5, first=1, sequence=[1, 2, 4, 3, 5]";
        Node root = new(1, [new Node(2, [new Node(4)]), new Node(3, [new Node(5)])]);

        try
        {
            IList<int> actual = Preorder(root);
            int expectedNodeCount = CountNodes(root);
            bool passed = actual.Count == expectedNodeCount
                && actual.Count > 0
                && actual[0] == root.val
                && actual.SequenceEqual([1, 2, 4, 3, 5]);
            string actualSummary = actual.Count == 0
                ? "count=0"
                : $"count={actual.Count}, first={actual[0]}, sequence={FormatValues(actual)}";
            return new CaseResult(name, input, expected, actualSummary, passed);
        }
        catch (Exception exception)
        {
            return new CaseResult(name, input, expected, exception.GetType().Name, false);
        }
    }

    private static Node BuildOfficialExample1()
    {
        return new Node(1, [new Node(3, [new Node(5), new Node(6)]), new Node(2), new Node(4)]);
    }

    private static Node BuildOfficialExample2()
    {
        Node node14 = new(14);
        Node node11 = new(11, [node14]);
        Node node7 = new(7, [node11]);
        Node node3 = new(3, [new Node(6), node7]);
        Node node12 = new(12);
        Node node8 = new(8, [node12]);
        Node node4 = new(4, [node8]);
        Node node13 = new(13);
        Node node9 = new(9, [node13]);
        Node node5 = new(5, [node9, new Node(10)]);

        return new Node(1, [new Node(2), node3, node4, node5]);
    }

    private static Node BuildChain(int count)
    {
        Node root = new(0);
        Node current = root;

        for (int value = 1; value < count; value++)
        {
            Node child = new(value);
            current.children.Add(child);
            current = child;
        }

        return root;
    }

    private static int CountNodes(Node? node)
    {
        if (node is null)
        {
            return 0;
        }

        return 1 + node.children.Sum(CountNodes);
    }

    private static string FormatValues(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    private sealed record CaseResult(
        string Name,
        string Input,
        string Expected,
        string Actual,
        bool Passed);
}
