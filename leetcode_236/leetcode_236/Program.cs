namespace leetcode_236
{
    internal class Program
    {
        /// <summary>
        /// 表示二元樹中的單一節點，保存整數值與可為空的左右子節點。
        /// 輸入值需符合題目限制；建立後可供最近公共祖先演算法讀取樹結構。
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立指定整數值的二元樹節點，左右子節點預設為空。
            /// </summary>
            /// <param name="x">目前節點儲存的整數值。</param>
            public TreeNode(int x)
            {
                val = x;
            }
        }

        /// <summary>
        /// 保存一筆可執行案例的名稱、層序樹資料、兩個目標節點值與預期祖先值。
        /// 輸入樹必須符合題目節點值唯一且目標節點存在的條件。
        /// </summary>
        private sealed record SampleCase(
            string Name,
            int?[] TreeValues,
            int PValue,
            int QValue,
            int Expected);

        /// <summary>
        /// <para>
        /// 236. Lowest Common Ancestor of a Binary Tree
        /// https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/description/
        ///
        /// Given a binary tree, find the lowest common ancestor (LCA) of two specified nodes. The LCA of nodes p and q is the lowest node in tree T that has both p and q as descendants, and a node may be a descendant of itself.
        ///
        /// Image: https://assets.leetcode.com/uploads/2018/12/14/binarytree.png
        ///
        /// Example 1:
        /// Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
        /// Output: 3
        /// Explanation: The LCA of nodes 5 and 1 is 3.
        ///
        /// Example 2:
        /// Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 4
        /// Output: 5
        /// Explanation: The LCA of nodes 5 and 4 is 5 because a node may be a descendant of itself.
        ///
        /// Example 3:
        /// Input: root = [1,2], p = 1, q = 2
        /// Output: 1
        ///
        /// Constraints:
        /// - The number of nodes is in [2,10^5].
        /// - -10^9 &lt;= Node.val &lt;= 10^9
        /// - Every Node.val is unique.
        /// - p != q
        /// - p and q exist in the tree.
        /// </para>
        /// <para>
        /// 236. 二元樹的最近共同祖先
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-tree/description/
        ///
        /// 給定一棵二元樹，找出兩個指定節點的最近共同祖先（LCA）。節點 p 與 q 的 LCA，是樹 T 中同時以 p、q 為後代的最低節點，且節點可以是自己的後代。
        ///
        /// 圖片：https://assets.leetcode.com/uploads/2018/12/14/binarytree.png
        ///
        /// 範例 1：
        /// 輸入：root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
        /// 輸出：3
        /// 說明：節點 5 與 1 的 LCA 是 3。
        ///
        /// 範例 2：
        /// 輸入：root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 4
        /// 輸出：5
        /// 說明：節點 5 與 4 的 LCA 是 5，因為節點可以是自己的後代。
        ///
        /// 範例 3：
        /// 輸入：root = [1,2], p = 1, q = 2
        /// 輸出：1
        ///
        /// 限制條件：
        /// - 節點數量在 [2,10^5] 範圍內。
        /// - -10^9 &lt;= Node.val &lt;= 10^9
        /// - 所有 Node.val 皆唯一。
        /// - p != q
        /// - p 與 q 都存在於樹中。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行五筆固定案例，建立各自的二元樹並驗證最近公共祖先。
        /// 輸入涵蓋官方範例、最小合法樹與左右深層分流；輸出每筆案例的
        /// 預期值、實際值、PASS/FAIL 狀態及總通過數。
        /// </summary>
        private static void RunSamples()
        {
            int?[] officialTree = new int?[]
            {
                3, 5, 1, 6, 2, 0, 8, null, null, 7, 4
            };

            SampleCase[] samples =
            {
                new SampleCase(
                    "官方案例 1：目標分別位於根節點左右子樹",
                    officialTree,
                    5,
                    1,
                    3),
                new SampleCase(
                    "官方案例 2：其中一個目標本身就是最近公共祖先",
                    officialTree,
                    5,
                    4,
                    5),
                new SampleCase(
                    "官方案例 3：最小合法樹且根節點就是目標",
                    new int?[] { 1, 2 },
                    1,
                    2,
                    1),
                new SampleCase(
                    "左側深層分流：最近公共祖先位於左子樹",
                    officialTree,
                    6,
                    4,
                    5),
                new SampleCase(
                    "右側子樹分流：兩個目標同在右子樹",
                    officialTree,
                    0,
                    8,
                    1)
            };

            int passedCases = 0;

            Console.WriteLine("Lowest Common Ancestor of a Binary Tree sample verification");
            Console.WriteLine();

            for (int index = 0; index < samples.Length; index++)
            {
                if (RunSample(index + 1, samples[index]))
                {
                    passedCases++;
                }
            }

            Console.WriteLine($"Summary: {passedCases}/{samples.Length} checks passed.");
        }

        /// <summary>
        /// 由單筆案例建立二元樹，取得樹中的 p、q 節點並執行最近公共祖先解法。
        /// 輸入案例的根與目標節點必須存在；輸出會列印比較結果，並回傳是否符合預期值。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="sample">包含樹、目標節點及預期答案的案例。</param>
        /// <returns>實際最近公共祖先值是否等於預期值。</returns>
        private static bool RunSample(int caseNumber, SampleCase sample)
        {
            TreeNode root = BuildTree(sample.TreeValues);
            TreeNode p = FindNode(root, sample.PValue)
                ?? throw new InvalidOperationException(
                    $"案例 {caseNumber} 找不到 p 節點值 {sample.PValue}。");
            TreeNode q = FindNode(root, sample.QValue)
                ?? throw new InvalidOperationException(
                    $"案例 {caseNumber} 找不到 q 節點值 {sample.QValue}。");
            TreeNode actual = LowestCommonAncestor(root, p, q);
            bool passed = actual.val == sample.Expected;

            Console.WriteLine($"Case {caseNumber}: {sample.Name}");
            Console.WriteLine(
                $"Input: root = {FormatTree(sample.TreeValues)}, p = {sample.PValue}, q = {sample.QValue}");
            Console.WriteLine($"Expected: {sample.Expected}");
            Console.WriteLine($"Actual: {actual.val}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed;
        }

        /// <summary>
        /// 將 LeetCode 層序陣列建立成二元樹，陣列中的 null 代表該位置沒有節點。
        /// 輸入必須至少包含非 null 根節點；輸出為新建立的樹根，無效根資料會拋出例外。
        /// </summary>
        /// <param name="values">以層序排列的可為 null 節點值。</param>
        /// <returns>依輸入資料建立的二元樹根節點。</returns>
        private static TreeNode BuildTree(int?[] values)
        {
            if (values.Length == 0 || values[0] is not int rootValue)
            {
                throw new ArgumentException("層序樹資料必須包含非 null 根節點。", nameof(values));
            }

            TreeNode root = new TreeNode(rootValue);
            Queue<TreeNode> parents = new Queue<TreeNode>();
            parents.Enqueue(root);
            int valueIndex = 1;

            // 佇列只保存仍可能接收左右子節點的父節點，並依層序消耗輸入值。
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

        /// <summary>
        /// 以深度優先搜尋在二元樹中尋找指定值的節點。
        /// 輸入節點可為 null；找到時回傳樹中的原始節點參考，否則回傳 null。
        /// </summary>
        /// <param name="root">目前搜尋的子樹根節點；空子樹可為 null。</param>
        /// <param name="targetValue">要尋找的唯一節點值。</param>
        /// <returns>符合目標值的樹中節點；不存在時為 null。</returns>
        private static TreeNode? FindNode(TreeNode? root, int targetValue)
        {
            if (root is null || root.val == targetValue)
            {
                return root;
            }

            return FindNode(root.left, targetValue)
                ?? FindNode(root.right, targetValue);
        }

        /// <summary>
        /// 將層序節點值格式化為 console 與 README 共用的顯示形式。
        /// 輸入可包含 null；輸出例如 [3, 5, 1, null, 2] 的字串。
        /// </summary>
        /// <param name="values">要格式化的層序節點值。</param>
        /// <returns>以方括號包住並以逗號分隔的樹表示字串。</returns>
        private static string FormatTree(IEnumerable<int?> values)
        {
            return $"[{string.Join(", ", values.Select(value => value?.ToString() ?? "null"))}]";
        }

        /// <summary>
        /// 以遞迴深度優先搜尋找出二元樹中 p、q 的最近公共祖先。
        /// 解題概念是分別搜尋左右子樹：兩側都有結果時目前節點是分流點，
        /// 只有一側有結果時向上傳遞該節點。輸入保證 root、p、q 非空，
        /// p 與 q 不同且都存在於樹中；輸出為唯一的最近公共祖先。
        ///
        /// ref:
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-tree/solutions/238552/er-cha-shu-de-zui-jin-gong-gong-zu-xian-by-leetc-2/
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-tree/solutions/2023872/fen-lei-tao-lun-luan-ru-ma-yi-ge-shi-pin-2r95/
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-tree/solutions/1456136/by-stormsunshine-sj0k/
        /// </summary>
        /// <param name="root">二元樹根節點。</param>
        /// <param name="p">第一個目標節點，必須存在於樹中。</param>
        /// <param name="q">第二個目標節點，必須存在於樹中且不同於 p。</param>
        /// <returns>p 與 q 的最近公共祖先節點。</returns>
        public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            return FindLowestCommonAncestor(root, p, q)
                ?? throw new InvalidOperationException("找不到最近公共祖先。");
        }

        /// <summary>
        /// 遞迴搜尋目前子樹是否包含 p、q，並向呼叫端回傳找到的目標或分流點。
        /// 輸入子樹可為空；輸出為目前子樹找到的節點，完全未找到時回傳 null。
        /// </summary>
        /// <param name="root">目前搜尋的子樹根節點；空子樹可為 null。</param>
        /// <param name="p">第一個目標節點。</param>
        /// <param name="q">第二個目標節點。</param>
        /// <returns>目標節點、最近公共祖先，或未找到時的 null。</returns>
        private static TreeNode? FindLowestCommonAncestor(
            TreeNode? root,
            TreeNode p,
            TreeNode q)
        {
            // 空子樹沒有答案；遇到任一目標時，該節點本身可成為自己的祖先。
            if (root is null || ReferenceEquals(root, p) || ReferenceEquals(root, q))
            {
                return root;
            }

            TreeNode? left = FindLowestCommonAncestor(root.left, p, q);
            TreeNode? right = FindLowestCommonAncestor(root.right, p, q);

            // 左右都找到目標代表路徑首次在目前節點匯合，因此它就是最近公共祖先。
            if (left is not null && right is not null)
            {
                return root;
            }

            // 只有一側找到時，將該側候選節點向上傳遞；兩側皆空則回傳 null。
            return left ?? right;
        }
    }
}