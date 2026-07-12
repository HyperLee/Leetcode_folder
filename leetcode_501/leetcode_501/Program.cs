namespace leetcode_501;

internal static class Program
{
    /// <summary>
    /// 501. Find Mode in Binary Search Tree
    /// 501. 二元搜尋樹中的眾數
    /// https://leetcode.com/problems/find-mode-in-binary-search-tree/
    /// https://leetcode.cn/problems/find-mode-in-binary-search-tree/
    /// Given a binary search tree that can contain duplicates, return every value with the greatest frequency.
    /// 給定可含重複值的二元搜尋樹，回傳所有出現次數最高的數值。
    /// </summary>
    /// <remarks>
    /// 501. Find Mode in Binary Search Tree
    /// https://leetcode.com/problems/find-mode-in-binary-search-tree/?envType=daily-question&amp;envId=2023-11-01
    /// 501. 二叉搜索树中的众数
    /// https://leetcode.cn/problems/find-mode-in-binary-search-tree/
    ///
    /// 找出BST中出現頻率最多的 那個數值
    /// 假定 BST 满足如下定义：
    /// 结点左子树中所含节点的值 小于等于 当前节点的值
    /// 结点右子树中所含节点的值 大于等于 当前节点的值
    /// 左子树和右子树都是二叉搜索树
    /// </remarks>
    private static void Main()
    {
        List<CaseResult> cases =
        [
            RunCase(
                "Official example 1",
                "[1,null,2,2]",
                [[2]],
                () => [FindMode(BuildTree([1, null, 2, 2]))]),
            RunCase(
                "Official example 2 / minimal tree",
                "[0]",
                [[0]],
                () => [FindMode(BuildTree([0]))]),
            RunCase(
                "Distinct constraint bounds",
                "level-order [0,-100000,100000]; in-order [-100000,0,100000]",
                [[-100000, 0, 100000]],
                () => [FindMode(BuildTree([0, -100000, 100000]))]),
            RunCase(
                "Tied modes",
                "[2,1,2,1]",
                [[1, 2]],
                () => [FindMode(BuildTree([2, 1, 2, 1]))]),
            RunCase(
                "Right subtree supplies mode",
                "[2,1,2]",
                [[2]],
                () => [FindMode(BuildTree([2, 1, 2]))]),
            RunCase(
                "Repeated negative value",
                "[-1,-1,0]",
                [[-1]],
                () => [FindMode(BuildTree([-1, -1, 0]))]),
            RunCase(
                "Repeated invocations",
                "first [1,1,2], then [2,1,2]",
                [[1], [2]],
                () =>
                [
                    FindMode(BuildTree([1, 1, 2])),
                    FindMode(BuildTree([2, 1, 2]))
                ]),
            RunCase(
                "Maximum-height spot check",
                "10000-node right chain of 7",
                [[7]],
                () => [FindMode(BuildRightChain(7, 10_000))])
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

    public sealed class TreeNode
    {
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

    private sealed record CaseResult(string Name, string Input, string Expected, string Actual, bool Passed);

    /// <summary>
    /// 以迭代中序走訪處理可重複值的二元搜尋樹；有效輸入必須符合左子樹值不大於節點、右子樹值不小於節點的題目契約，回傳所有最高出現次數的值。
    /// </summary>
    public static int[] FindMode(TreeNode? root)
    {
        List<int> modes = [];
        Stack<TreeNode> pending = new();
        TreeNode? current = root;
        int? previousValue = null;
        int currentFrequency = 0;
        int maxFrequency = 0;

        while (current is not null || pending.Count > 0)
        {
            while (current is not null)
            {
                pending.Push(current);
                current = current.left;
            }

            current = pending.Pop();

            // BST 的中序結果非遞減，因此相同值必定形成一段連續區間。
            RecordValue(current.val, ref previousValue, ref currentFrequency, ref maxFrequency, modes);
            current = current.right;
        }

        return modes.ToArray();
    }

    /// <summary>
    /// 依目前中序值更新連續次數與眾數清單；呼叫端必須按非遞減順序提供值，方法會在新最高頻率時替換結果、在平手時保留所有眾數。
    /// </summary>
    private static void RecordValue(int value, ref int? previousValue, ref int currentFrequency, ref int maxFrequency, List<int> modes)
    {
        if (previousValue == value)
        {
            currentFrequency++;
        }
        else
        {
            previousValue = value;
            currentFrequency = 1;
        }

        // 新最高頻率才淘汰舊結果；平手值也必須保留。
        if (currentFrequency > maxFrequency)
        {
            maxFrequency = currentFrequency;
            modes.Clear();
            modes.Add(value);
        }
        else if (currentFrequency == maxFrequency)
        {
            modes.Add(value);
        }
    }

    /// <summary>
    /// 執行一個或多個同名案例的查詢，並以每組結果的序列比較建立不含主控台輸出的驗證資料。
    /// </summary>
    private static CaseResult RunCase(string name, string input, IReadOnlyList<int[]> expected, Func<IReadOnlyList<int[]>> execute)
    {
        IReadOnlyList<int[]> actual = execute();
        bool passed = expected.Count == actual.Count
            && expected.Zip(actual, (expectedValues, actualValues) => expectedValues.SequenceEqual(actualValues)).All(isMatch => isMatch);

        return new CaseResult(name, input, FormatMany(expected), FormatMany(actual), passed);
    }

    /// <summary>
    /// 依層序陣列建立二元樹；空值代表該位置沒有節點，僅用於建立驗證輸入。
    /// </summary>
    private static TreeNode? BuildTree(IReadOnlyList<int?> levelOrder)
    {
        if (levelOrder.Count == 0 || levelOrder[0] is not int rootValue)
        {
            return null;
        }

        TreeNode root = new(rootValue);
        Queue<TreeNode> parents = new();
        parents.Enqueue(root);
        int index = 1;

        while (parents.Count > 0 && index < levelOrder.Count)
        {
            TreeNode parent = parents.Dequeue();

            if (levelOrder[index] is int leftValue)
            {
                parent.left = new TreeNode(leftValue);
                parents.Enqueue(parent.left);
            }

            index++;

            if (index < levelOrder.Count && levelOrder[index] is int rightValue)
            {
                parent.right = new TreeNode(rightValue);
                parents.Enqueue(parent.right);
            }

            index++;
        }

        return root;
    }

    /// <summary>
    /// 建立指定長度且全為相同值的右傾樹，供驗證走訪不依賴遞迴呼叫堆疊。
    /// </summary>
    private static TreeNode? BuildRightChain(int value, int count)
    {
        if (count <= 0)
        {
            return null;
        }

        TreeNode root = new(value);
        TreeNode current = root;

        for (int index = 1; index < count; index++)
        {
            current.right = new TreeNode(value);
            current = current.right;
        }

        return root;
    }

    /// <summary>
    /// 將整數序列格式化為方括號與逗號分隔的輸出文字。
    /// </summary>
    private static string Format(IEnumerable<int> values) => $"[{string.Join(',', values)}]";

    /// <summary>
    /// 將多次查詢的格式化序列以 then 串接，讓連續呼叫案例能保留每次呼叫的結果。
    /// </summary>
    private static string FormatMany(IEnumerable<IEnumerable<int>> values) => string.Join(", then ", values.Select(Format));
}
