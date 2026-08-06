namespace leetcode_2331
{
    internal class Program
    {
        /// <summary>
        /// 表示布林完整二元樹的節點；葉節點值為 0 或 1，運算節點值為 2（OR）或 3（AND）。
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立一個布林二元樹節點，並可選擇指定左右子節點。
            /// 葉節點不提供子節點；題目保證非葉節點同時具有左右子節點。
            /// </summary>
            /// <param name="val">節點值：0 為 false、1 為 true、2 為 OR、3 為 AND。</param>
            /// <param name="left">左子節點；葉節點為 <see langword="null"/>。</param>
            /// <param name="right">右子節點；葉節點為 <see langword="null"/>。</param>
            public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        /// <summary>
        /// 2331. Evaluate Boolean Binary Tree
        /// https://leetcode.com/problems/evaluate-boolean-binary-tree/description/?envType=daily-question&envId=2024-05-16
        /// 2331. 计算布尔二叉树的值
        /// https://leetcode.cn/problems/evaluate-boolean-binary-tree/description/
        /// </summary>
        /// <param name="args">本範例未使用命令列參數。</param>
        static void Main(string[] args)
        {
            (string Name, TreeNode Root, bool Expected)[] cases =
            [
                ("單一 false 葉節點", new TreeNode(0), false),
                ("單一 true 葉節點", new TreeNode(1), true),
                ("OR：false OR false", new TreeNode(2, new TreeNode(0), new TreeNode(0)), false),
                ("OR：false OR true", new TreeNode(2, new TreeNode(0), new TreeNode(1)), true),
                ("AND：true AND false", new TreeNode(3, new TreeNode(1), new TreeNode(0)), false),
                ("AND：true AND true", new TreeNode(3, new TreeNode(1), new TreeNode(1)), true),
                (
                    "混合：true OR (false AND true)",
                    new TreeNode(
                        2,
                        new TreeNode(1),
                        new TreeNode(3, new TreeNode(0), new TreeNode(1))),
                    true)
            ];

            int passedChecks = 0;
            foreach ((string name, TreeNode root, bool expected) in cases)
            {
                passedChecks += RunCase(name, root, expected);
            }

            int totalChecks = cases.Length * 2;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項測試通過");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 執行同一棵布林二元樹的遞迴與迭代解法，並比較兩者是否符合預期結果。
        /// 輸入必須是題目定義的非空完整布林二元樹；回傳本案例通過的檢查數，範圍為 0 到 2。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="root">要計算的布林二元樹根節點。</param>
        /// <param name="expected">人工推導的預期布林結果。</param>
        /// <returns>遞迴與迭代解法中，結果符合預期值的數量。</returns>
        private static int RunCase(string name, TreeNode root, bool expected)
        {
            bool recursiveResult = EvaluateTree(root);
            bool iterativeResult = EvaluateTreeIterative(root);
            bool recursivePassed = recursiveResult == expected;
            bool iterativePassed = iterativeResult == expected;

            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"EvaluateTree: {recursiveResult} - {(recursivePassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"EvaluateTreeIterative: {iterativeResult} - {(iterativePassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (recursivePassed ? 1 : 0) + (iterativePassed ? 1 : 0);
        }

        /// <summary>
        /// 以遞迴深度優先搜尋計算布林完整二元樹；先取得左右子樹結果，再依目前節點套用 OR 或 AND。
        /// 輸入必須是題目定義的非空完整二元樹，葉節點值為 0 或 1，非葉節點值為 2 或 3。
        /// </summary>
        /// <param name="root">要計算的布林二元樹根節點。</param>
        /// <returns>整棵樹所表示的布林運算結果。</returns>
        public static bool EvaluateTree(TreeNode root)
        {
            // 完整二元樹的葉節點沒有子節點，可直接把 0、1 轉成布林值。
            if (root.left is null && root.right is null)
            {
                return root.val == 1;
            }

            TreeNode left = root.left!;
            TreeNode right = root.right!;

            // 題目保證非葉節點只會是 2（OR）或 3（AND）。
            return root.val == 2
                ? EvaluateTree(left) || EvaluateTree(right)
                : EvaluateTree(left) && EvaluateTree(right);
        }

        /// <summary>
        /// 以單一堆疊模擬後序走訪，先計算左右子樹並保存結果，再處理目前的運算節點。
        /// 輸入必須是題目定義的非空完整二元樹；輸出為根節點所代表的布林運算結果。
        /// </summary>
        /// <param name="root">要計算的布林二元樹根節點。</param>
        /// <returns>整棵樹所表示的布林運算結果。</returns>
        public static bool EvaluateTreeIterative(TreeNode root)
        {
            Stack<(TreeNode Node, bool Expanded)> stack = new Stack<(TreeNode Node, bool Expanded)>();
            Dictionary<TreeNode, bool> values = new Dictionary<TreeNode, bool>();
            stack.Push((root, false));

            while (stack.Count > 0)
            {
                (TreeNode node, bool expanded) = stack.Pop();

                if (node.left is null && node.right is null)
                {
                    values[node] = node.val == 1;
                    continue;
                }

                TreeNode left = node.left!;
                TreeNode right = node.right!;

                if (expanded)
                {
                    // 第二次取出節點時，左右子樹結果都已完成，可以套用目前的運算子。
                    values[node] = node.val == 2
                        ? values[left] || values[right]
                        : values[left] && values[right];
                    continue;
                }

                // 先把目前節點標記為待合併，再安排左右子樹，形成後序處理順序。
                stack.Push((node, true));
                stack.Push((right, false));
                stack.Push((left, false));
            }

            return values[root];
        }
    }
}