namespace leetcode_104
{
    internal class Program
    {
        /// <summary>
        /// 表示二元樹中的單一節點，保存節點值以及可為空的左右子節點參考。
        /// 節點可作為 <see cref="MaxDepth"/> 與 <see cref="MaxDepth2"/> 的輸入，
        /// 兩種解法都只讀取樹的結構與節點值，不會修改節點。
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立具有指定值與左右子樹的二元樹節點。
            /// 左右子節點可省略或傳入 <see langword="null"/>，代表該方向沒有子樹；
            /// 建構結果是可連接至其他節點的新節點。
            /// </summary>
            /// <param name="val">節點儲存的整數值；題目限制為 -100 到 100。</param>
            /// <param name="left">左子節點；沒有左子樹時為 <see langword="null"/>。</param>
            /// <param name="right">右子節點；沒有右子樹時為 <see langword="null"/>。</param>
            public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }


        /// <summary>
        /// 104. Maximum Depth of Binary Tree
        /// https://leetcode.com/problems/maximum-depth-of-binary-tree/
        /// 
        /// 104. 二叉树的最大深度
        /// https://leetcode.cn/problems/maximum-depth-of-binary-tree/description/
        /// 
        /// A binary tree's maximum depth is the number of nodes along the longest path from the root node down to the farthest leaf node.
        /// 計算方式從 root 為起始點, 找出子樹最大深度
        /// 
        /// build tree sample
        /// http://e-troy.blogspot.com/2015/02/c-binary-search-tree.html
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int passedChecks = 0;
            const int totalChecks = 10;

            passedChecks += RunTestCase("空樹", null, 0);
            passedChecks += RunTestCase("單一節點（下限值）", new TreeNode(-100), 1);
            passedChecks += RunTestCase(
                "官方範例",
                new TreeNode(
                    3,
                    new TreeNode(9),
                    new TreeNode(20, new TreeNode(15), new TreeNode(7))),
                3);
            passedChecks += RunTestCase(
                "完全右偏樹",
                new TreeNode(
                    1,
                    right: new TreeNode(
                        2,
                        right: new TreeNode(
                            3,
                            right: new TreeNode(4)))),
                4);
            passedChecks += RunTestCase(
                "左右不等深且含重複值",
                new TreeNode(
                    1,
                    new TreeNode(
                        2,
                        new TreeNode(3, new TreeNode(4))),
                    new TreeNode(
                        2,
                        right: new TreeNode(3))),
                4);

            Console.WriteLine();
            Console.WriteLine($"{passedChecks}/{totalChecks} checks passed.");
            Console.WriteLine(passedChecks == totalChecks ? "Overall: PASS" : "Overall: FAIL");
        }


        /// <summary>
        /// 執行一組固定案例，分別呼叫 <see cref="MaxDepth"/> 與
        /// <see cref="MaxDepth2"/> 計算同一棵樹的最大深度。
        /// 輸入可以是空樹；方法會將兩個實際結果與預期深度比較，
        /// 輸出 Expected、Actual 與 PASS/FAIL，並回傳通過的檢查數。
        /// </summary>
        /// <param name="caseName">顯示於主控台的案例名稱。</param>
        /// <param name="root">待測二元樹的根節點；空樹時為 <see langword="null"/>。</param>
        /// <param name="expected">此案例預期的最大深度。</param>
        /// <returns>兩種解法中實際結果符合預期值的數量，範圍為 0 到 2。</returns>
        private static int RunTestCase(string caseName, TreeNode? root, int expected)
        {
            int maxDepthResult = MaxDepth(root);
            int maxDepth2Result = MaxDepth2(root);
            bool maxDepthPassed = maxDepthResult == expected;
            bool maxDepth2Passed = maxDepth2Result == expected;

            Console.WriteLine(
                $"[{(maxDepthPassed ? "PASS" : "FAIL")}] {caseName} | MaxDepth | Expected: {expected} | Actual: {maxDepthResult}");
            Console.WriteLine(
                $"[{(maxDepth2Passed ? "PASS" : "FAIL")}] {caseName} | MaxDepth2 | Expected: {expected} | Actual: {maxDepth2Result}");

            return (maxDepthPassed ? 1 : 0) + (maxDepth2Passed ? 1 : 0);
        }


        /// <summary>
        /// 以直接遞迴計算二元樹的最大深度。
        /// 解法將空節點視為深度 0，分別取得左右子樹深度後選擇較大值，
        /// 再加上目前節點所占的一層。輸入可以是空樹，且不會被修改。
        /// </summary>
        /// <param name="root">待計算的二元樹根節點；空樹時為 <see langword="null"/>。</param>
        /// <returns>從根節點到最遠葉節點路徑上的節點數；空樹回傳 0。</returns>
        public static int MaxDepth(TreeNode? root)
        {
            if (root == null)
            {
                // 空分支不包含節點，作為遞迴向上合併深度的基底。
                return 0;
            }

            // 目前層的深度等於較深子樹的深度再加上目前節點。
            return Math.Max(MaxDepth(root.right), MaxDepth(root.left)) + 1;
        }


        /// <summary>
        /// 以明確區分葉節點與存在子樹的遞迴流程計算二元樹最大深度。
        /// 解法先處理空樹與葉節點，再只遞迴走訪實際存在的左右子樹，
        /// 保存其中較大的子樹深度並加上目前層。輸入可以是空樹，且不會被修改。
        /// </summary>
        /// <param name="root">待計算的二元樹根節點；空樹時為 <see langword="null"/>。</param>
        /// <returns>從根節點到最遠葉節點路徑上的節點數；空樹回傳 0。</returns>
        public static int MaxDepth2(TreeNode? root)
        {
            if (root == null)
            {
                return 0;
            }

            // 葉節點本身就是一層，無須再對兩個空子節點遞迴。
            if (root.left == null && root.right == null)
            {
                return 1;
            }

            int maxDepth = int.MinValue;

            // 只走訪實際存在的子樹，持續保留目前找到的最大深度。
            if (root.left != null)
            {
                maxDepth = Math.Max(MaxDepth2(root.left), maxDepth);
            }

            if (root.right != null)
            {
                maxDepth = Math.Max(MaxDepth2(root.right), maxDepth);
            }

            // 子樹深度加一，將目前節點所在層納入結果。
            return maxDepth + 1;
        }
    }
}
