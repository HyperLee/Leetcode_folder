namespace leetcode_653;

public class Program
{
    /// <summary>
    /// 可安全表示空子節點的二元樹節點。
    /// </summary>
    public sealed class TreeNode
    {
        /// <summary>
        /// 初始化節點及其可選的左右子節點。
        /// </summary>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            Val = val;
            Left = left;
            Right = right;
        }

        public int Val { get; }

        public TreeNode? Left { get; set; }

        public TreeNode? Right { get; set; }
    }

    /// <summary>
    /// leetcode 653 Two Sum IV - Input is a BST
    /// https://leetcode.com/problems/two-sum-iv-input-is-a-bst/
    /// 两数之和 IV - 输入二叉搜索树
    /// https://leetcode.cn/problems/two-sum-iv-input-is-a-bst/
    /// Given a BST root and an integer k, return true if and only if two distinct tree nodes sum to k; otherwise, return false.
    /// 給定 BST 根節點與整數 k，當且僅當兩個不同樹節點的值總和為 k 時回傳 true；否則回傳 false。
    /// </summary>
    private static void Main()
    {
        const int totalChecks = 9;
        int passedChecks = 0;

        RecordCheck("Case 1: [5,3,6,2,4,null,7], k=9", true, FindTarget(CreateExampleTree(), 9), ref passedChecks);
        RecordCheck("Case 2: [5,3,6,2,4,null,7], k=28", false, FindTarget(CreateExampleTree(), 28), ref passedChecks);
        RecordCheck("Case 3: [5], k=10", false, FindTarget(new TreeNode(5), 10), ref passedChecks);
        RecordCheck("Case 4: [-2,-3,-1], k=-5", true, FindTarget(CreateNegativeTree(), -5), ref passedChecks);
        RecordCheck("Case 5: [0,-2,2], k=0", true, FindTarget(CreateZeroTree(), 0), ref passedChecks);
        RecordCheck("Case 6: first isolated [5], k=8", false, FindTarget(new TreeNode(5), 8), ref passedChecks);
        RecordCheck("Case 7: following new tree [3], k=8", false, FindTarget(new TreeNode(3), 8), ref passedChecks);

        TreeNode upperBoundTree = BuildRightSkewedTree(10_000);
        RecordCheck("Case 8: right-skewed BST [1..10000], k=19999", true, FindTarget(upperBoundTree, 19_999), ref passedChecks);
        RecordCheck("Case 9: same upper-bound BST [1..10000], k=20001", false, FindTarget(upperBoundTree, 20_001), ref passedChecks);

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 以每次呼叫獨立的雜湊集合與堆疊，迭代尋找兩個不同節點是否可加總為目標值。
    /// </summary>
    /// <param name="root">要搜尋的二元搜尋樹根節點，可為空。</param>
    /// <param name="k">欲配對的目標和。</param>
    /// <returns>存在兩個不同節點的和為 <paramref name="k"/> 時回傳 <see langword="true"/>。</returns>
    public static bool FindTarget(TreeNode? root, int k)
    {
        HashSet<int> seenValues = [];
        Stack<TreeNode> nodesToVisit = [];

        if (root is not null)
        {
            nodesToVisit.Push(root);
        }

        while (nodesToVisit.Count > 0)
        {
            TreeNode current = nodesToVisit.Pop();

            if (seenValues.Contains(k - current.Val))
            {
                return true;
            }

            seenValues.Add(current.Val);

            if (current.Left is not null)
            {
                nodesToVisit.Push(current.Left);
            }

            if (current.Right is not null)
            {
                nodesToVisit.Push(current.Right);
            }
        }

        return false;
    }

    /// <summary>
    /// 建立題目範例的 BST，讓每個驗收案例使用彼此獨立的輸入樹。
    /// </summary>
    private static TreeNode CreateExampleTree()
    {
        return new TreeNode(
            5,
            new TreeNode(3, new TreeNode(2), new TreeNode(4)),
            new TreeNode(6, null, new TreeNode(7)));
    }

    /// <summary>
    /// 建立包含負數的三節點 BST。
    /// </summary>
    private static TreeNode CreateNegativeTree()
    {
        return new TreeNode(-2, new TreeNode(-3), new TreeNode(-1));
    }

    /// <summary>
    /// 建立以零為根的三節點 BST。
    /// </summary>
    private static TreeNode CreateZeroTree()
    {
        return new TreeNode(0, new TreeNode(-2), new TreeNode(2));
    }

    /// <summary>
    /// 迭代建立從 1 到指定節點數的右斜 BST，避免建樹階段使用遞迴。
    /// </summary>
    /// <param name="nodeCount">樹中的節點數，必須至少為 1。</param>
    /// <returns>值為 1 的根節點。</returns>
    private static TreeNode BuildRightSkewedTree(int nodeCount)
    {
        TreeNode root = new(1);
        TreeNode current = root;

        for (int value = 2; value <= nodeCount; value++)
        {
            current.Right = new TreeNode(value);
            current = current.Right;
        }

        return root;
    }

    /// <summary>
    /// 記錄一個驗收案例的預期與實際結果，並累計通過數。
    /// </summary>
    private static void RecordCheck(string caseLabel, bool expected, bool actual, ref int passedChecks)
    {
        bool passed = expected == actual;

        if (passed)
        {
            passedChecks++;
        }

        Console.WriteLine($"{caseLabel} | Expected: {expected} | Actual: {actual} | {(passed ? "PASS" : "FAIL")}");
    }
}
