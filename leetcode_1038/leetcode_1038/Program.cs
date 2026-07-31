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
        /// 1038. Binary Search Tree to Greater Sum Tree
        /// https://leetcode.com/problems/binary-search-tree-to-greater-sum-tree/description/?envType=daily-question&envId=2024-06-25
        /// 1038. 从二叉搜索树到更大和树
        /// https://leetcode.cn/problems/binary-search-tree-to-greater-sum-tree/description/
        /// 
        /// 樹的走訪
        /// https://zh.wikipedia.org/zh-tw/%E6%A0%91%E7%9A%84%E9%81%8D%E5%8E%86
        /// 
        /// 前序走訪: 前序走訪（Pre-Order Traversal）是依序以根節點、左節點、右節點為順序走訪的方式。
        /// 中序走訪: 中序走訪（In-Order Traversal）是依序以左節點、根節點、右節點為順序走訪的方式。 
        /// 後序走訪: 後序走訪（Post-Order Traversal）是依序以左節點、右節點、根節點為順序走訪的方式。
        /// 
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