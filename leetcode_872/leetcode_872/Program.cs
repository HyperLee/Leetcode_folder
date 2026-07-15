namespace leetcode_872;

internal static class Program
{
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

    /// <summary>
    /// 872. Leaf-Similar Trees
    /// https://leetcode.com/problems/leaf-similar-trees/
    /// 872. 葉子相似的樹
    /// https://leetcode.cn/problems/leaf-similar-trees/
    /// Determine whether two binary trees have the same leaf values in left-to-right order.
    /// 判斷兩棵二元樹由左至右的葉節點值序列是否相同。
    /// </summary>
    private static void Main()
    {
        TreeNode firstLongChain = BuildRightChain(200, 0, 200);
        TreeNode secondLongChain = BuildRightChain(200, 199, 200);

        (string Name, TreeNode Root1, TreeNode Root2, bool Expected, string InputDescription)[] cases =
        [
            (
                "Official example: same leaf sequence",
                BuildTree([3, 5, 1, 6, 2, 9, 8, null, null, 7, 4]),
                BuildTree([3, 5, 1, 6, 7, 4, 2, null, null, null, null, null, null, 9, 8]),
                true,
                "root1 = [3,5,1,6,2,9,8,null,null,7,4], root2 = [3,5,1,6,7,4,2,null,null,null,null,null,null,9,8]"),
            (
                "Official example: different leaf order",
                BuildTree([1, 2, 3]),
                BuildTree([1, 3, 2]),
                false,
                "root1 = [1,2,3], root2 = [1,3,2]"),
            (
                "Same single node",
                new TreeNode(7),
                new TreeNode(7),
                true,
                "root1 = [7], root2 = [7]"),
            (
                "Different single nodes",
                new TreeNode(0),
                new TreeNode(200),
                false,
                "root1 = [0], root2 = [200]"),
            (
                "Different structure with the same leaves",
                BuildTree([1, 2, 4, 6, 7]),
                BuildTree([9, 6, 8, null, null, 10, 4, 7]),
                true,
                "leaf sequences = [6,7,4] and [6,7,4]"),
            (
                "Same values in a different order",
                BuildTree([0, 1, 2]),
                BuildTree([0, 2, 1]),
                false,
                "leaf sequences = [1,2] and [2,1]"),
            (
                "Repeated leaf multiplicity differs",
                BuildTree([0, 1, 8, 5, 5]),
                BuildTree([0, 5, 9, null, null, 8, 8]),
                false,
                "leaf sequences = [5,5,8] and [5,8,8]"),
            (
                "Boundary leaf values",
                BuildTree([1, 0, 200]),
                BuildTree([2, 3, 200, 0]),
                true,
                "leaf sequences = [0,200] and [0,200]"),
            (
                "Maximum-depth right chains",
                firstLongChain,
                secondLongChain,
                true,
                "two 200-node right chains; internal values differ; final leaf = 200")
        ];

        int passedCount = 0;
        Console.WriteLine("LeetCode 872 acceptance harness");
        Console.WriteLine();

        for (int i = 0; i < cases.Length; i++)
        {
            (string name, TreeNode root1, TreeNode root2, bool expected, string inputDescription) = cases[i];
            bool actual = LeafSimilar(root1, root2);
            bool passed = expected == actual;

            if (passed)
            {
                passedCount++;
            }

            Console.WriteLine($"Case {i + 1}: {name}");
            Console.WriteLine($"Input: {inputDescription}");
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | Expected: {expected.ToString().ToLowerInvariant()} | Actual: {actual.ToString().ToLowerInvariant()}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passedCount}/{cases.Length} checks passed.");

        if (passedCount != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 收集兩棵樹由左至右的葉節點值，並判斷兩個序列是否完全相同。
    /// </summary>
    public static bool LeafSimilar(TreeNode root1, TreeNode root2)
    {
        List<int> firstLeaves = [];
        List<int> secondLeaves = [];

        CollectLeaves(root1, firstLeaves);
        CollectLeaves(root2, secondLeaves);

        return firstLeaves.SequenceEqual(secondLeaves);
    }

    /// <summary>
    /// 以深度優先搜尋依左至右順序收集指定子樹的葉節點值。
    /// </summary>
    private static void CollectLeaves(TreeNode node, IList<int> leaves)
    {
        if (node.left is null && node.right is null)
        {
            leaves.Add(node.val);
            return;
        }

        // 固定先走左子樹再走右子樹，維持葉序列的由左至右順序。
        if (node.left is not null)
        {
            CollectLeaves(node.left, leaves);
        }

        if (node.right is not null)
        {
            CollectLeaves(node.right, leaves);
        }
    }

    private static TreeNode BuildTree(int?[] values)
    {
        if (values.Length == 0 || values[0] is null)
        {
            throw new ArgumentException("A fixture must contain a non-null root.", nameof(values));
        }

        TreeNode root = new(values[0]!.Value);
        Queue<TreeNode> parents = new();
        parents.Enqueue(root);
        int valueIndex = 1;

        while (parents.Count > 0 && valueIndex < values.Length)
        {
            TreeNode parent = parents.Dequeue();

            if (values[valueIndex] is int leftValue)
            {
                parent.left = new TreeNode(leftValue);
                parents.Enqueue(parent.left);
            }

            valueIndex++;

            if (valueIndex < values.Length && values[valueIndex] is int rightValue)
            {
                parent.right = new TreeNode(rightValue);
                parents.Enqueue(parent.right);
            }

            valueIndex++;
        }

        return root;
    }

    private static TreeNode BuildRightChain(int nodeCount, int internalValue, int leafValue)
    {
        if (nodeCount < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(nodeCount));
        }

        if (nodeCount == 1)
        {
            return new TreeNode(leafValue);
        }

        TreeNode root = new(internalValue);
        TreeNode current = root;

        for (int i = 1; i < nodeCount - 1; i++)
        {
            current.right = new TreeNode(internalValue);
            current = current.right;
        }

        current.right = new TreeNode(leafValue);
        return root;
    }
}
