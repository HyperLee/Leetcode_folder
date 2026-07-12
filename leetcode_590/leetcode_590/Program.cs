namespace leetcode_590;

internal static class Program
{
    /// <summary>
    /// 590. N-ary Tree Postorder Traversal；590. N 元樹的後序遍歷。
    /// English URL: https://leetcode.com/problems/n-ary-tree-postorder-traversal/
    /// 中文 URL: https://leetcode.cn/problems/n-ary-tree-postorder-traversal/
    /// Given the root of an n-ary tree, return the postorder traversal of its nodes' values.
    /// 給定 N 元樹的根節點，回傳其節點值的後序走訪結果。
    /// </summary>
    /// <remarks>
    /// 後序遍歷會先依 children 原始順序完成所有子樹，再將目前根節點放入結果。
    /// 本題是 N-way tree，不是只有左右子樹的 binary tree；可參考 leetcode_145 的二元樹版本。
    ///
    /// 590. N-ary Tree Postorder Traversal
    /// https://leetcode.com/problems/n-ary-tree-postorder-traversal/
    /// 590. N 叉树的后序遍历
    /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/description/?envType=daily-question&amp;envId=Invalid%20Date
    ///
    /// 二元樹 binary tree 的後序順序是左子節點、右子節點、根節點；N-way tree 則是依序完成 children，再加入根節點。
    /// 類似題目為 leetcode_145 Binary Tree Postorder Traversal，但本題不是二元樹。
    /// </remarks>
    private static void Main()
    {
        List<CaseResult> cases =
        [
            RunSequenceCase(
                "Official example 1",
                "[1,null,3,2,4,null,5,6]",
                [5, 6, 3, 2, 4, 1],
                () => Postorder(BuildOfficialExample1())),
            RunSequenceCase(
                "Official example 2",
                "[1,null,2,3,4,5,null,null,6,7,null,8,null,9,10,null,null,11,null,12,null,13,null,null,14]",
                [2, 6, 14, 11, 7, 3, 12, 8, 4, 13, 9, 10, 5, 1],
                () => Postorder(BuildOfficialExample2())),
            RunSequenceCase(
                "Minimum: null root",
                "root = null",
                [],
                () => Postorder(null)),
            RunSequenceCase(
                "Minimum: single leaf",
                "root = [42]",
                [42],
                () => Postorder(new Node(42))),
            RunSequenceCase(
                "Children keep input order",
                "root 10 -> children 30, 20, 40; 30 -> child 5",
                [5, 30, 20, 40, 10],
                () => Postorder(new Node(10, [new Node(30, [new Node(5)]), new Node(20), new Node(40)]))),
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

    /// <summary>
    /// 表示 N 元樹節點；有效題目輸入會提供 children 集合，空建構子與值建構子則建立空的 children，避免 leaf 節點需要額外 null 判斷。
    /// </summary>
    public sealed class Node
    {
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

        public int val;
        public IList<Node> children;
    }

    /// <summary>
    /// 以遞迴 DFS 依 children 原始順序完成所有子樹後再加入根值；有效輸入是符合題目定義的 N 元樹，回傳完整的後序節點值序列，空樹回傳空集合。
    /// </summary>
    /// <remarks>
    /// 官方解法參考：
    /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/solutions/1327327/n-cha-shu-de-hou-xu-bian-li-by-leetcode-txesi/
    /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/solutions/1459136/by-stormsunshine-tnzr/
    /// 本實作採用遞迴解法。
    /// </remarks>
    public static IList<int> Postorder(Node? root)
    {
        List<int> values = [];
        PostorderVisit(root, values);
        return values;
    }

    /// <summary>
    /// 將目前節點的所有子樹依原始順序追加到結果後，再追加目前節點；node 可以是 null，result 必須是呼叫端提供的可寫入集合，方法不產生主控台輸出。
    /// </summary>
    /// <remarks>
    /// 舊版筆記保留 N-way tree 與 binary tree 的差異：本題不是左右子樹，而是走訪 children 集合；每個孩子完成後才加入根節點。
    /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/solutions/2645191/jian-dan-dfspythonjavacgojs-by-endlessch-ytdk/
    /// </remarks>
    private static void PostorderVisit(Node? node, IList<int> result)
    {
        if (node is null)
        {
            return;
        }

        // 後序不變量是所有 children 都完成後，根值才可以加入結果。
        foreach (Node child in node.children)
        {
            PostorderVisit(child, result);
        }

        result.Add(node.val);
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
        const string expected = "[2, 1], then [8, 9]";

        try
        {
            IList<int> first = Postorder(new Node(1, [new Node(2)]));
            IList<int> second = Postorder(new Node(9, [new Node(8)]));
            string actual = $"{FormatValues(first)}, then {FormatValues(second)}";
            bool passed = first.SequenceEqual([2, 1]) && second.SequenceEqual([8, 9]);
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
        const string expected = "count=1000, first=999, last=0";

        try
        {
            IList<int> actual = Postorder(BuildChain(nodeCount));
            bool passed = actual.Count == nodeCount
                && actual[0] == nodeCount - 1
                && actual[actual.Count - 1] == 0
                && actual.SequenceEqual(Enumerable.Range(0, nodeCount).Reverse());
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
        const string expected = "count=5, root-last=1, sequence=[4, 2, 5, 3, 1]";
        Node root = new(1, [new Node(2, [new Node(4)]), new Node(3, [new Node(5)])]);

        try
        {
            IList<int> actual = Postorder(root);
            int expectedNodeCount = CountNodes(root);
            bool passed = actual.Count == expectedNodeCount
                && actual.Count > 0
                && actual[actual.Count - 1] == root.val
                && actual.SequenceEqual([4, 2, 5, 3, 1]);
            string actualSummary = actual.Count == 0
                ? "count=0"
                : $"count={actual.Count}, root-last={actual[actual.Count - 1]}, sequence={FormatValues(actual)}";
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
