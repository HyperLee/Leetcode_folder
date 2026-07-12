namespace leetcode_530;

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

internal class Program
{
    /// <summary>
    /// 530. Minimum Absolute Difference in BST.
    ///
    /// 530. 二叉搜尋樹的最小絕對差。
    ///
    /// English problem: Given the root of a binary search tree, return the
    /// minimum absolute difference between the values of any two different
    /// nodes in the tree. The tree contains 2 to 10,000 nodes, and each value
    /// is between 0 and 100,000.
    ///
    /// 繁體中文題述：給定一棵二叉搜尋樹的根節點，請回傳樹中任意兩個不同
    /// 節點值之間的最小絕對差。樹中有 2 到 10,000 個節點，節點值介於
    /// 0 到 100,000 之間。
    ///
    /// English: https://leetcode.com/problems/minimum-absolute-difference-in-bst/description/
    /// 中文：https://leetcode.cn/problems/minimum-absolute-difference-in-bst/
    /// </summary>
    /// <remarks>
    /// 本題 類似 leetcode 783
    /// https://leetcode.com/problems/minimum-distance-between-bst-nodes/description/
    ///
    /// 解法共用
    ///
    /// 原始解法筆記：
    /// https://leetcode.cn/problems/minimum-distance-between-bst-nodes/solution/gong-shui-san-xie-yi-ti-san-jie-shu-de-s-7r17/
    /// 懶人字串解法（DFS）
    /// 如果不考虑利用二叉搜索树特性的话，轉成字串的做法是将所有节点的 val 存到一个数组中。
    /// 对数组进行排序，并获取答案。
    /// 将所有节点的 val 存入数组，可以使用 BFS 或者 DFS。
    ///
    /// https://leetcode.cn/problems/minimum-absolute-difference-in-bst/solutions/443276/er-cha-sou-suo-shu-de-zui-xiao-jue-dui-chai-by-lee/
    /// 20241004
    /// 改採用中序解法
    /// 省略排序,
    /// 因輸入是二元樹且中序是由小至大排序特性
    /// 二叉搜索树的中序遍历是有序的，因此我们可以直接对「二叉搜索树」进行中序遍历，保存遍历过程中的相邻元素最小值即是答案。
    /// </remarks>
    /// <param name="args">Command-line arguments; this harness does not require any.</param>
    private static void Main(string[] args)
    {
        int totalChecks = 0;
        int passedChecks = 0;

        void RunCase(string name, TreeNode root, int expected)
        {
            int actual = GetMinimumDifference(root);
            bool passed = actual == expected;
            totalChecks++;
            if (passed)
            {
                passedChecks++;
            }

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        RunCase(
            "官方範例 1",
            new TreeNode(
                4,
                new TreeNode(2, new TreeNode(1), new TreeNode(3)),
                new TreeNode(6)),
            1);

        RunCase(
            "官方範例 2",
            new TreeNode(
                1,
                new TreeNode(0),
                new TreeNode(48, new TreeNode(12), new TreeNode(49))),
            1);

        RunCase("最小有效輸入", new TreeNode(0, null, new TreeNode(1)), 1);

        RunCase(
            "最小差值來自中序相鄰節點",
            new TreeNode(13, new TreeNode(1, null, new TreeNode(10)), new TreeNode(30)),
            3);

        RunCase("值域下界與上界", new TreeNode(100000, new TreeNode(0)), 100000);

        RunCase(
            "高值相鄰",
            new TreeNode(99999, new TreeNode(99998), new TreeNode(100000)),
            1);

        RunCase(
            "多層不規則 BST",
            new TreeNode(
                20,
                new TreeNode(10, new TreeNode(5), new TreeNode(15)),
                new TreeNode(30, new TreeNode(25), new TreeNode(40))),
            5);

        TreeNode upperBoundTree = BuildBalancedBst(0, 9999)!;
        RunCase("10,000 節點上限 spot check", upperBoundTree, 1);

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用 BST 的中序遍歷，回傳任意兩個不同節點值之間的最小絕對差。
    /// 有效輸入必須是符合題目契約且至少包含兩個節點的二叉搜尋樹；回傳值是
    /// 中序遞增序列中相鄰值的最小差距。所有狀態都限制在本次呼叫內。
    /// </summary>
    /// <param name="root">符合題目契約的二叉搜尋樹根節點。</param>
    /// <returns>樹中任意兩個不同節點值之間的最小絕對差。</returns>
    public static int GetMinimumDifference(TreeNode root)
    {
        bool hasPrevious = false;
        int previousValue = 0;
        int minimumDifference = int.MaxValue;

        InorderTraversal(root, ref hasPrevious, ref previousValue, ref minimumDifference);

        return minimumDifference;
    }

    /// <summary>
    /// 遞迴走訪 BST 的左子樹、目前節點與右子樹，更新前一個值及目前最小差距。
    /// 有效輸入是任意可能為 null 的子樹；此方法不輸出主控台內容，並透過參考
    /// 參數把中序遍歷狀態傳回給公開解法。
    /// </summary>
    /// <param name="node">目前要走訪的子樹根節點。</param>
    /// <param name="hasPrevious">是否已經走訪過第一個節點。</param>
    /// <param name="previousValue">中序遍歷中前一個節點的值。</param>
    /// <param name="minimumDifference">目前已觀察到的最小相鄰差距。</param>
    private static void InorderTraversal(
        TreeNode? node,
        ref bool hasPrevious,
        ref int previousValue,
        ref int minimumDifference)
    {
        if (node is null)
        {
            return;
        }

        InorderTraversal(node.left, ref hasPrevious, ref previousValue, ref minimumDifference);

        // BST 的中序遍歷是遞增序列，因此只需比較目前節點與前一個節點。
        if (hasPrevious)
        {
            minimumDifference = Math.Min(minimumDifference, node.val - previousValue);
        }

        previousValue = node.val;
        hasPrevious = true;

        InorderTraversal(node.right, ref hasPrevious, ref previousValue, ref minimumDifference);
    }

    private static TreeNode? BuildBalancedBst(int lower, int upper)
    {
        if (lower > upper)
        {
            return null;
        }

        int middle = lower + ((upper - lower) / 2);
        return new TreeNode(
            middle,
            BuildBalancedBst(lower, middle - 1),
            BuildBalancedBst(middle + 1, upper));
    }
}
