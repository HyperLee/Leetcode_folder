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
    /// 662. Maximum Width of Binary Tree
    /// https://leetcode.com/problems/maximum-width-of-binary-tree/description/
    /// <para>
    /// Given the root of a binary tree, return the maximum width of the given tree.
    ///
    /// The maximum width of a tree is the maximum width among all levels.
    ///
    /// The width of one level is defined as the length between the end-nodes (the leftmost and rightmost non-null nodes), where the null nodes between the end-nodes that would be present in a complete binary tree extending down to that level are also counted into the length calculation.
    ///
    /// It is guaranteed that the answer will be in the range of a 32-bit signed integer.
    ///
    /// Example 1:
    /// Image: https://assets.leetcode.com/uploads/2021/05/03/width1-tree.jpg
    /// Input: root = [1,3,2,5,3,null,9]
    /// Output: 4
    /// Explanation: The maximum width exists in the third level with length 4 (5,3,null,9).
    ///
    /// Example 2:
    /// Image: https://assets.leetcode.com/uploads/2022/03/14/maximum-width-of-binary-tree-v3.jpg
    /// Input: root = [1,3,2,5,null,null,9,6,null,7]
    /// Output: 7
    /// Explanation: The maximum width exists in the fourth level with length 7 (6,null,null,null,null,null,7).
    ///
    /// Example 3:
    /// Image: https://assets.leetcode.com/uploads/2021/05/03/width3-tree.jpg
    /// Input: root = [1,3,2,5]
    /// Output: 2
    /// Explanation: The maximum width exists in the second level with length 2 (3,2).
    ///
    /// Constraints:
    /// - The number of nodes in the tree is in the range [1, 3000].
    /// - -100 &lt;= Node.val &lt;= 100
    /// </para>
    /// <para>
    /// 662. 二元樹的最大寬度
    /// https://leetcode.cn/problems/maximum-width-of-binary-tree/description/
    ///
    /// 給定二元樹的根節點 root，回傳這棵樹的最大寬度。
    ///
    /// 樹的最大寬度，是所有層級寬度中的最大值。
    ///
    /// 某一層的寬度定義為兩個端點節點（最左與最右的非空節點）之間的長度；若將完整二元樹延伸至該層，兩端點之間本應存在的 null 節點也要計入長度。
    ///
    /// 保證答案在 32 位元有號整數範圍內。
    ///
    /// 範例 1：
    /// 圖片：https://assets.leetcode.com/uploads/2021/05/03/width1-tree.jpg
    /// 輸入：root = [1,3,2,5,3,null,9]
    /// 輸出：4
    /// 解釋：最大寬度出現在第三層，長度為 4（5,3,null,9）。
    ///
    /// 範例 2：
    /// 圖片：https://assets.leetcode.com/uploads/2022/03/14/maximum-width-of-binary-tree-v3.jpg
    /// 輸入：root = [1,3,2,5,null,null,9,6,null,7]
    /// 輸出：7
    /// 解釋：最大寬度出現在第四層，長度為 7（6,null,null,null,null,null,7）。
    ///
    /// 範例 3：
    /// 圖片：https://assets.leetcode.com/uploads/2021/05/03/width3-tree.jpg
    /// 輸入：root = [1,3,2,5]
    /// 輸出：2
    /// 解釋：最大寬度出現在第二層，長度為 2（3,2）。
    ///
    /// 限制條件：
    /// - 樹中的節點數量在 [1, 3000] 範圍內。
    /// - -100 &lt;= Node.val &lt;= 100
    /// </para>
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
