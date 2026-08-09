namespace leetcode_235
{
    internal class Program
    {
        /// <summary>
        /// 表示二元搜尋樹節點；節點值必須唯一，左右子節點可為空。
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立指定值的葉節點，左右子節點初始皆為空。
            /// </summary>
            /// <param name="x">節點儲存的整數值。</param>
            public TreeNode(int x)
            {
                val = x;
            }
        }

        /// <summary>
        /// <para>
        /// 235. Lowest Common Ancestor of a Binary Search Tree
        /// https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-search-tree/description/
        ///
        /// Given a binary search tree, find the lowest common ancestor (LCA) of nodes p and q. The LCA is the lowest node in tree T that has both p and q as descendants, allowing a node to be a descendant of itself.
        ///
        /// Image: https://assets.leetcode.com/uploads/2018/12/14/binarysearchtree_improved.png
        ///
        /// Example 1:
        /// Input: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 8
        /// Output: 6
        /// Explanation: The LCA of nodes 2 and 8 is 6.
        ///
        /// Example 2:
        /// Input: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 4
        /// Output: 2
        /// Explanation: The LCA of nodes 2 and 4 is 2 because a node may be its own descendant.
        ///
        /// Example 3:
        /// Input: root = [2,1], p = 2, q = 1
        /// Output: 2
        ///
        /// Constraints:
        /// - The number of nodes is in [2,10^5].
        /// - -10^9 &lt;= Node.val &lt;= 10^9
        /// - Every Node.val is unique.
        /// - p != q
        /// - p and q exist in the BST.
        /// </para>
        /// <para>
        /// 235. 二元搜尋樹的最近共同祖先
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-search-tree/description/
        ///
        /// 給定二元搜尋樹，找出節點 p 與 q 的最近共同祖先（LCA）。LCA 是樹 T 中同時以 p、q 為後代的最低節點，且允許節點是自己的後代。
        ///
        /// 圖片：https://assets.leetcode.com/uploads/2018/12/14/binarysearchtree_improved.png
        ///
        /// 範例 1：
        /// 輸入：root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 8
        /// 輸出：6
        /// 說明：節點 2 與 8 的 LCA 是 6。
        ///
        /// 範例 2：
        /// 輸入：root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 4
        /// 輸出：2
        /// 說明：節點 2 與 4 的 LCA 是 2，因為節點可以是自己的後代。
        ///
        /// 範例 3：
        /// 輸入：root = [2,1], p = 2, q = 1
        /// 輸出：2
        ///
        /// 限制條件：
        /// - 節點數量在 [2,10^5] 範圍內。
        /// - -10^9 &lt;= Node.val &lt;= 10^9
        /// - 所有 Node.val 皆唯一。
        /// - p != q
        /// - p 與 q 都存在於 BST 中。
        /// </para>
        /// </summary>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            (string Name, int[] Values, int P, int Q, int Expected)[] testCases =
            [
                ("Official example 1 / split at root", [6, 2, 8, 0, 4, 7, 9, 3, 5], 2, 8, 6),
                ("Official example 2 / ancestor is p", [6, 2, 8, 0, 4, 7, 9, 3, 5], 2, 4, 2),
                ("Official example 3 / minimum tree", [2, 1], 2, 1, 2),
                ("Deep nodes in left subtree", [6, 2, 8, 0, 4, 7, 9, 3, 5], 3, 5, 4),
                ("Nodes in right subtree", [6, 2, 8, 0, 4, 7, 9, 3, 5], 7, 9, 8),
                ("Reversed p and q", [6, 2, 8, 0, 4, 7, 9, 3, 5], 8, 2, 6),
                ("Negative values", [-2, -4, 1, -5, -3, 0, 2], -5, -3, -4),
                ("Right-skewed tree", [1, 2, 3, 4, 5, 6, 7, 8, 9, 10], 8, 10, 8),
            ];

            int passedChecks = 0;

            Console.WriteLine("LeetCode 235 - Lowest Common Ancestor of a Binary Search Tree");
            Console.WriteLine(new string('=', 68));
            Console.WriteLine();

            for (int i = 0; i < testCases.Length; i++)
            {
                (string name, int[] values, int p, int q, int expected) = testCases[i];
                passedChecks += RunCase(i + 1, name, values, p, q, expected);
            }

            int totalChecks = testCases.Length * 2;
            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 使用遞迴搜尋二元搜尋樹中兩個既存節點的最近公共祖先。
        /// 若兩個目標值都小於或大於目前節點，就只搜尋對應子樹；
        /// 否則目前節點即為分岔點或其中一個目標節點。
        /// ref: 
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-search-tree/solutions/428633/er-cha-sou-suo-shu-de-zui-jin-gong-gong-zu-xian-26/
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-search-tree/solutions/2023873/zui-jin-gong-gong-zu-xian-yi-ge-shi-pin-8h2zc/
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-search-tree/solutions/1456138/235-er-cha-sou-suo-shu-de-zui-jin-gong-g-lccn/
        /// 
        /// 題目說明:
        /// 1. 所有節點的值都是唯一
        /// 2. p, q 為不同節點且均從在於給定的 BST 中
        /// ==> 保證存在以及唯一性且不為空
        /// </summary>
        /// <param name="root">非空的二元搜尋樹根節點。</param>
        /// <param name="p">存在於樹中的第一個目標節點。</param>
        /// <param name="q">存在於樹中的第二個目標節點。</param>
        /// <returns>同時包含 <paramref name="p"/> 與 <paramref name="q"/> 的最低層祖先節點。</returns>
        /// <remarks>時間複雜度為 O(h)，遞迴呼叫堆疊的輔助空間為 O(h)，h 為樹高。</remarks>
        public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            int currentValue = root.val;

            if (p.val < currentValue && q.val < currentValue)
            {
                // 兩個目標都較小，最近公共祖先只可能位於左子樹。
                return LowestCommonAncestor(root.left!, p, q);
            }

            if (p.val > currentValue && q.val > currentValue)
            {
                // 兩個目標都較大，最近公共祖先只可能位於右子樹。
                return LowestCommonAncestor(root.right!, p, q);
            }

            // 目標分居兩側，或目前節點就是其中一個目標；此處即為最低分岔點。
            return root;
        }

        /// <summary>
        /// 使用迭代方式搜尋二元搜尋樹中兩個既存節點的最近公共祖先。
        /// 每輪依兩個目標值與目前節點值的關係縮小到單一子樹，
        /// 遇到分岔點或目標節點本身時回傳目前節點。
        /// </summary>
        /// <param name="root">非空的二元搜尋樹根節點。</param>
        /// <param name="p">存在於樹中的第一個目標節點。</param>
        /// <param name="q">存在於樹中的第二個目標節點。</param>
        /// <returns>同時包含 <paramref name="p"/> 與 <paramref name="q"/> 的最低層祖先節點。</returns>
        /// <remarks>時間複雜度為 O(h)，輔助空間為 O(1)，h 為樹高。</remarks>
        public static TreeNode LowestCommonAncestor2(TreeNode root, TreeNode p, TreeNode q)
        {
            TreeNode current = root;

            while (true)
            {
                if (p.val < current.val && q.val < current.val)
                {
                    // 只保留仍可能包含最近公共祖先的左子樹。
                    current = current.left!;
                }
                else if (p.val > current.val && q.val > current.val)
                {
                    // 只保留仍可能包含最近公共祖先的右子樹。
                    current = current.right!;
                }
                else
                {
                    // 目前節點是最低分岔點，或等於 p、q 其中之一。
                    return current;
                }
            }
        }

        /// <summary>
        /// 建立一棵用於範例驗證的二元搜尋樹。
        /// 依輸入順序逐一插入互不重複的值，第一個值成為根節點。
        /// </summary>
        /// <param name="values">至少包含一個元素且所有值互不重複的插入序列。</param>
        /// <returns>依序插入完成的非空二元搜尋樹根節點。</returns>
        private static TreeNode BuildBinarySearchTree(int[] values)
        {
            TreeNode root = new TreeNode(values[0]);

            for (int i = 1; i < values.Length; i++)
            {
                TreeNode current = root;

                while (true)
                {
                    if (values[i] < current.val)
                    {
                        if (current.left is null)
                        {
                            current.left = new TreeNode(values[i]);
                            break;
                        }

                        current = current.left;
                    }
                    else
                    {
                        if (current.right is null)
                        {
                            current.right = new TreeNode(values[i]);
                            break;
                        }

                        current = current.right;
                    }
                }
            }

            return root;
        }

        /// <summary>
        /// 利用二元搜尋樹的排序特性尋找指定值的既存節點。
        /// 每輪只進入可能包含目標值的一側子樹。
        /// </summary>
        /// <param name="root">要搜尋的非空二元搜尋樹根節點。</param>
        /// <param name="target">必須存在於樹中的唯一目標值。</param>
        /// <returns>樹中值等於 <paramref name="target"/> 的節點參考。</returns>
        /// <exception cref="InvalidOperationException">樹中不存在目標值時擲出。</exception>
        private static TreeNode FindNode(TreeNode root, int target)
        {
            TreeNode? current = root;

            while (current is not null)
            {
                if (current.val == target)
                {
                    return current;
                }

                current = target < current.val ? current.left : current.right;
            }

            throw new InvalidOperationException($"Node {target} was not found.");
        }

        /// <summary>
        /// 建立單一測試樹並驗證遞迴與迭代解法是否回傳預期的實際節點。
        /// 輸入必須構成合法 BST，且 p、q 與預期值都必須存在於樹中。
        /// </summary>
        /// <param name="number">顯示用的案例編號。</param>
        /// <param name="name">案例名稱。</param>
        /// <param name="values">建立 BST 的節點插入順序。</param>
        /// <param name="pValue">第一個目標節點值。</param>
        /// <param name="qValue">第二個目標節點值。</param>
        /// <param name="expectedValue">預期最近公共祖先的節點值。</param>
        /// <returns>本案例通過的解法檢查數，範圍為 0 到 2。</returns>
        private static int RunCase(
            int number,
            string name,
            int[] values,
            int pValue,
            int qValue,
            int expectedValue)
        {
            TreeNode root = BuildBinarySearchTree(values);
            TreeNode p = FindNode(root, pValue);
            TreeNode q = FindNode(root, qValue);
            TreeNode expected = FindNode(root, expectedValue);
            TreeNode recursiveResult = LowestCommonAncestor(root, p, q);
            TreeNode iterativeResult = LowestCommonAncestor2(root, p, q);
            bool recursivePassed = ReferenceEquals(recursiveResult, expected);
            bool iterativePassed = ReferenceEquals(iterativeResult, expected);

            Console.WriteLine($"[{number}] {name}");
            Console.WriteLine($"Tree insertion order: [{string.Join(", ", values)}]");
            Console.WriteLine($"p = {pValue}, q = {qValue}, expected node = {expectedValue}");
            Console.WriteLine(
                $"Recursive: {recursiveResult.val} ({(recursivePassed ? "PASS" : "FAIL")})");
            Console.WriteLine(
                $"Iterative: {iterativeResult.val} ({(iterativePassed ? "PASS" : "FAIL")})");
            Console.WriteLine();

            return Convert.ToInt32(recursivePassed) + Convert.ToInt32(iterativePassed);
        }
    }
}