namespace leetcode_1372;

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
    /// <para>
    /// 1372. Longest ZigZag Path in a Binary Tree
    /// https://leetcode.com/problems/longest-zigzag-path-in-a-binary-tree/description/
    ///
    /// You are given the root of a binary tree.
    ///
    /// A ZigZag path for a binary tree is defined as follows:
    /// - Choose any node in the binary tree and a direction (right or left).
    /// - If the current direction is right, move to the right child of the current node; otherwise, move to the left child.
    /// - Change the direction from right to left or from left to right.
    /// - Repeat the second and third steps until you cannot move in the tree.
    ///
    /// ZigZag length is defined as the number of nodes visited - 1. A single node has a length of 0.
    ///
    /// Return the longest ZigZag path contained in that tree.
    ///
    /// Example 1:
    /// Official illustration: https://assets.leetcode.com/uploads/2020/01/22/sample_1_1702.png
    /// Input: root = [1,null,1,1,1,null,null,1,1,null,1,null,null,null,1]
    /// Output: 3
    /// Explanation: Longest ZigZag path in blue nodes (right -&gt; left -&gt; right).
    ///
    /// Example 2:
    /// Official illustration: https://assets.leetcode.com/uploads/2020/01/22/sample_2_1702.png
    /// Input: root = [1,1,1,null,1,null,null,1,1,null,1]
    /// Output: 4
    /// Explanation: Longest ZigZag path in blue nodes (left -&gt; right -&gt; left -&gt; right).
    ///
    /// Example 3:
    /// Input: root = [1]
    /// Output: 0
    ///
    /// Constraints:
    /// - The number of nodes in the tree is in the range [1, 5 * 10^4].
    /// - 1 &lt;= Node.val &lt;= 100
    /// </para>
    /// <para>
    /// 1372. 二元樹中的最長交錯路徑
    /// https://leetcode.cn/problems/longest-zigzag-path-in-a-binary-tree/description/
    ///
    /// 給定一棵二元樹的根節點。
    ///
    /// 二元樹的交錯路徑定義如下：
    /// - 選擇二元樹中的任意節點以及一個方向（向右或向左）。
    /// - 如果目前方向是向右，移動到目前節點的右子節點；否則，移動到左子節點。
    /// - 將方向從向右改為向左，或從向左改為向右。
    /// - 重複第二與第三個步驟，直到無法在樹中繼續移動。
    ///
    /// 交錯長度定義為造訪的節點數量 - 1。單一節點的長度為 0。
    ///
    /// 回傳該樹中包含的最長交錯路徑。
    ///
    /// 範例 1：
    /// 官方示意圖：https://assets.leetcode.com/uploads/2020/01/22/sample_1_1702.png
    /// 輸入：root = [1,null,1,1,1,null,null,1,1,null,1,null,null,null,1]
    /// 輸出：3
    /// 解釋：藍色節點構成最長交錯路徑（向右 -&gt; 向左 -&gt; 向右）。
    ///
    /// 範例 2：
    /// 官方示意圖：https://assets.leetcode.com/uploads/2020/01/22/sample_2_1702.png
    /// 輸入：root = [1,1,1,null,1,null,null,1,1,null,1]
    /// 輸出：4
    /// 解釋：藍色節點構成最長交錯路徑（向左 -&gt; 向右 -&gt; 向左 -&gt; 向右）。
    ///
    /// 範例 3：
    /// 輸入：root = [1]
    /// 輸出：0
    ///
    /// 限制條件：
    /// - 樹中的節點數量在 [1, 5 * 10^4] 範圍內。
    /// - 1 &lt;= Node.val &lt;= 100
    /// </para>
    /// </summary>
    private static void Main()
    {
        const int totalChecks = 9;
        Program solution = new();
        List<CaseResult> results =
        [
            EvaluateCase(solution, "Case 1: official example 1", 3, CreateFirstOfficialExample()),
            EvaluateCase(solution, "Case 2: official example 2", 4, CreateSecondOfficialExample()),
            EvaluateCase(solution, "Case 3: single node [1]", 0, new TreeNode(1)),
            EvaluateCase(solution, "Case 4: one left edge [1,2]", 1, new TreeNode(1, new TreeNode(2))),
            EvaluateCase(solution, "Case 5: one right edge [1,null,2]", 1, new TreeNode(1, null, new TreeNode(2))),
            EvaluateCase(solution, "Case 6: best path starts below root", 3, CreateBelowRootBestPath()),
            EvaluateCase(solution, "Case 7: 50000-node left chain", 1, BuildChain(50_000, false)),
            EvaluateCase(solution, "Case 8: 50000-node alternating chain", 49_999, BuildChain(50_000, true)),
            EvaluateCase(solution, "Case 9: same instance after large cases", 0, new TreeNode(42)),
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
    /// 以迭代深度優先走訪找出二元樹中的最長交錯路徑；每個狀態記錄上一條邊方向，
    /// 方向交替時延續長度，方向相同時從該邊重新起算。
    /// </summary>
    /// <param name="root">二元樹根節點；題目有效輸入至少有一個節點，為空時相容地回傳 0。</param>
    /// <returns>最長交錯路徑包含的邊數；單節點或空樹回傳 0。</returns>
    public int LongestZigZag(TreeNode? root)
    {
        if (root is null)
        {
            return 0;
        }

        Stack<(TreeNode Node, bool LastMoveWasLeft, int Length)> nodesToVisit = [];

        if (root.left is not null)
        {
            nodesToVisit.Push((root.left, true, 1));
        }

        if (root.right is not null)
        {
            nodesToVisit.Push((root.right, false, 1));
        }

        int maximumLength = 0;

        while (nodesToVisit.Count > 0)
        {
            (TreeNode node, bool lastMoveWasLeft, int length) = nodesToVisit.Pop();
            maximumLength = Math.Max(maximumLength, length);

            if (lastMoveWasLeft)
            {
                if (node.right is not null)
                {
                    nodesToVisit.Push((node.right, false, length + 1));
                }

                // 再走左邊會中斷交替，因此以這條邊作為新路徑的第一步。
                if (node.left is not null)
                {
                    nodesToVisit.Push((node.left, true, 1));
                }
            }
            else
            {
                if (node.left is not null)
                {
                    nodesToVisit.Push((node.left, true, length + 1));
                }

                // 再走右邊會中斷交替，因此以這條邊作為新路徑的第一步。
                if (node.right is not null)
                {
                    nodesToVisit.Push((node.right, false, 1));
                }
            }
        }

        return maximumLength;
    }

    private static CaseResult EvaluateCase(Program solution, string label, int expected, TreeNode root)
    {
        return new CaseResult(label, expected, solution.LongestZigZag(root));
    }

    private static TreeNode CreateFirstOfficialExample()
    {
        return new TreeNode(
            1,
            null,
            new TreeNode(
                1,
                new TreeNode(1),
                new TreeNode(
                    1,
                    new TreeNode(1, null, new TreeNode(1, null, new TreeNode(1))),
                    new TreeNode(1))));
    }

    private static TreeNode CreateSecondOfficialExample()
    {
        return new TreeNode(
            1,
            new TreeNode(
                1,
                null,
                new TreeNode(
                    1,
                    new TreeNode(1, null, new TreeNode(1)),
                    new TreeNode(1))),
            new TreeNode(1));
    }

    private static TreeNode CreateBelowRootBestPath()
    {
        return new TreeNode(
            1,
            new TreeNode(
                2,
                new TreeNode(
                    3,
                    null,
                    new TreeNode(4, new TreeNode(5), null))),
            null);
    }

    /// <summary>
    /// 建立指定節點數的單鏈二元樹；可選擇固定向左或逐邊交替方向，用於驗證深度上限與方向狀態。
    /// </summary>
    /// <param name="nodeCount">節點數；此驗證工具只接收至少一個節點。</param>
    /// <param name="alternateDirections">為 <see langword="true"/> 時交替左右，否則每個節點都接左子節點。</param>
    /// <returns>具有指定節點數與方向模式的根節點。</returns>
    private static TreeNode BuildChain(int nodeCount, bool alternateDirections)
    {
        TreeNode root = new(1);
        TreeNode current = root;

        for (int edgeIndex = 1; edgeIndex < nodeCount; edgeIndex++)
        {
            TreeNode next = new(edgeIndex + 1);

            if (!alternateDirections || edgeIndex % 2 == 1)
            {
                current.left = next;
            }
            else
            {
                current.right = next;
            }

            current = next;
        }

        return root;
    }
}