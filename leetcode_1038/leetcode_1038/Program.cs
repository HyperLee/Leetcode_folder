namespace leetcode_1038
{
    internal class Program
    {
        /// <summary>
        /// 表示二元搜尋樹的節點。
        /// 每個節點保存整數值，並可連結至左、右子節點；缺少子節點時以 <see langword="null"/> 表示。
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立一個二元搜尋樹節點。
            /// 輸入節點值與可選的左右子樹，輸出可被串接成二元樹的節點物件。
            /// </summary>
            /// <param name="val">節點保存的整數值。</param>
            /// <param name="left">左子節點；沒有左子節點時為 <see langword="null"/>。</param>
            /// <param name="right">右子節點；沒有右子節點時為 <see langword="null"/>。</param>
            public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }


        /// <summary>
        /// <para>
        /// 1038. Binary Search Tree to Greater Sum Tree
        /// https://leetcode.com/problems/binary-search-tree-to-greater-sum-tree/description/
        ///
        /// Given the root of a Binary Search Tree (BST), convert it to a Greater Tree such that every key of
        /// the original BST is changed to the original key plus the sum of all keys greater than the original
        /// key in BST.
        /// As a reminder, a binary search tree is a tree that satisfies these constraints:
        /// - The left subtree of a node contains only nodes with keys less than the node's key.
        /// - The right subtree of a node contains only nodes with keys greater than the node's key.
        /// - Both the left and right subtrees must also be binary search trees.
        ///
        /// Example 1:
        /// Input: root = [4,1,6,0,2,5,7,null,null,null,3,null,null,null,8]
        /// Output: [30,36,21,36,35,26,15,null,null,null,33,null,null,null,8]
        ///
        /// Example 2:
        /// Input: root = [0,null,1]
        /// Output: [1,null,1]
        ///
        /// Constraints:
        /// The number of nodes in the tree is in the range [1, 100].
        /// 0 &lt;= Node.val &lt;= 100
        /// All the values in the tree are unique.
        ///
        /// Note: This question is the same as 538:
        /// https://leetcode.com/problems/convert-bst-to-greater-tree/
        /// </para>
        /// <para>
        /// 1038. 從二元搜尋樹到較大總和樹
        /// https://leetcode.cn/problems/binary-search-tree-to-greater-sum-tree/description/
        ///
        /// 給定二元搜尋樹（BST）的根節點 root，請將它轉換為較大總和樹，使原 BST 中的每個鍵值
        /// 都改為原鍵值加上 BST 中所有大於該原鍵值之鍵值的總和。
        /// 提醒你，二元搜尋樹必須滿足下列條件：
        /// - 節點的左子樹只包含鍵值小於該節點鍵值的節點。
        /// - 節點的右子樹只包含鍵值大於該節點鍵值的節點。
        /// - 左、右子樹也都必須是二元搜尋樹。
        ///
        /// 範例 1：
        /// 輸入：root = [4,1,6,0,2,5,7,null,null,null,3,null,null,null,8]
        /// 輸出：[30,36,21,36,35,26,15,null,null,null,33,null,null,null,8]
        ///
        /// 範例 2：
        /// 輸入：root = [0,null,1]
        /// 輸出：[1,null,1]
        ///
        /// 限制條件：
        /// 樹中的節點數量介於 [1, 100]。
        /// 0 &lt;= Node.val &lt;= 100
        /// 樹中的所有值都互不相同。
        ///
        /// 注意：本題與第 538 題相同：
        /// https://leetcode.com/problems/convert-bst-to-greater-tree/
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode officialExample = new TreeNode(
                4,
                new TreeNode(
                    1,
                    new TreeNode(0),
                    new TreeNode(2, right: new TreeNode(3))),
                new TreeNode(
                    6,
                    new TreeNode(5),
                    new TreeNode(7, right: new TreeNode(8))));

            int passedCount = 0;
            passedCount += RunTestCase(
                "官方完整 BST",
                officialExample,
                "[30,36,21,36,35,26,15,null,null,null,33,null,null,null,8]");
            passedCount += RunTestCase(
                "兩個節點",
                new TreeNode(0, right: new TreeNode(1)),
                "[1,null,1]");
            passedCount += RunTestCase(
                "單一節點",
                new TreeNode(1),
                "[1]");
            passedCount += RunTestCase(
                "空樹",
                null,
                "[]");

            const int totalCount = 4;
            Console.WriteLine($"Summary: {passedCount}/{totalCount} tests passed.");
            Environment.ExitCode = passedCount == totalCount ? 0 : 1;
        }

        /// <summary>
        /// 執行單一測試案例並顯示預期結果、實際結果與通過狀態。
        /// 輸入必須是二元搜尋樹或空樹，預期結果使用層序陣列字串；
        /// 輸出為 1（通過）或 0（失敗），供進入點累計通過案例數。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="root">待轉換的二元搜尋樹根節點；空樹可傳入 <see langword="null"/>。</param>
        /// <param name="expected">預期的層序陣列字串。</param>
        /// <returns>案例通過時為 1，否則為 0。</returns>
        private static int RunTestCase(string name, TreeNode? root, string expected)
        {
            TreeNode? convertedRoot = BstToGst(root);
            string actual = SerializeLevelOrder(convertedRoot);
            bool passed = actual == expected;

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual:   {actual}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed ? 1 : 0;
        }

        /// <summary>
        /// 將二元樹轉成 LeetCode 慣用的層序陣列字串，以便精確比較樹的結構與節點值。
        /// 輸入可為任意二元樹或空樹；輸出會保留中間必要的 <c>null</c>，
        /// 並移除尾端不影響結構的 <c>null</c>。
        /// </summary>
        /// <param name="root">要序列化的根節點；空樹可傳入 <see langword="null"/>。</param>
        /// <returns>層序陣列字串；空樹輸出 <c>[]</c>。</returns>
        private static string SerializeLevelOrder(TreeNode? root)
        {
            if (root is null)
            {
                return "[]";
            }

            List<string> values = [];
            Queue<TreeNode?> nodes = new();
            nodes.Enqueue(root);

            while (nodes.Count > 0)
            {
                TreeNode? node = nodes.Dequeue();
                if (node is null)
                {
                    values.Add("null");
                    continue;
                }

                values.Add(node.val.ToString());
                nodes.Enqueue(node.left);
                nodes.Enqueue(node.right);
            }

            while (values.Count > 0 && values[^1] == "null")
            {
                values.RemoveAt(values.Count - 1);
            }

            return $"[{string.Join(",", values)}]";
        }

        /// <summary>
        /// 將二元搜尋樹原地轉換為較大和樹。
        /// 透過「右子樹、根節點、左子樹」的反向中序走訪，由大到小累加節點值；
        /// 輸入必須是節點值互異的有效二元搜尋樹或空樹，輸出為同一棵已更新的樹。
        /// </summary>
        /// <param name="root">待轉換的二元搜尋樹根節點；空樹可傳入 <see langword="null"/>。</param>
        /// <returns>轉換後的原根節點；輸入空樹時回傳 <see langword="null"/>。</returns>
        public static TreeNode? BstToGst(TreeNode? root)
        {
            int accumulatedSum = 0;

            void Traverse(TreeNode? node)
            {
                if (node is null)
                {
                    return;
                }

                // 反向中序確保處理目前節點時，所有比它大的值都已加入累加器。
                Traverse(node.right);
                accumulatedSum += node.val;
                node.val = accumulatedSum;
                Traverse(node.left);
            }

            Traverse(root);
            return root;
        }
    }
}