namespace leetcode_979
{
    internal class Program
    {
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立二元樹節點。輸入節點持有的硬幣數量與可省略的左右子節點；
            /// 建立結果會保留這些值，供分配硬幣演算法沿樹結構進行後序走訪。
            /// </summary>
            /// <param name="val">目前節點持有的硬幣數量。</param>
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
        /// 979. Distribute Coins in Binary Tree
        /// https://leetcode.com/problems/distribute-coins-in-binary-tree/description/?envType=daily-question&envId=2024-05-18
        /// 979. 在二叉树中分配硬币
        /// https://leetcode.cn/problems/distribute-coins-in-binary-tree/description/
        /// </summary>
        /// <remarks>
        /// 建立固定測試樹，逐一比對預期與實際的最少移動次數，最後輸出整體測試結果。
        /// </remarks>
        /// <param name="args">命令列參數；此固定案例 runner 不使用任何參數。</param>
        static void Main(string[] args)
        {
            int passed = 0;
            int total = 4;

            TreeNode firstRoot = new TreeNode(
                3,
                new TreeNode(0),
                new TreeNode(0));
            passed += RunTestCase("Case 1: root = [3,0,0]", firstRoot, 2) ? 1 : 0;

            TreeNode secondRoot = new TreeNode(
                0,
                new TreeNode(3),
                new TreeNode(0));
            passed += RunTestCase("Case 2: root = [0,3,0]", secondRoot, 3) ? 1 : 0;

            TreeNode thirdRoot = new TreeNode(1);
            passed += RunTestCase("Case 3: root = [1]", thirdRoot, 0) ? 1 : 0;

            TreeNode fourthRoot = new TreeNode(
                1,
                new TreeNode(0, null, new TreeNode(3)),
                new TreeNode(0));
            passed += RunTestCase("Case 4: root = [1,0,0,null,3]", fourthRoot, 4) ? 1 : 0;

            Console.WriteLine($"Summary: {passed}/{total} passed.");
            Console.WriteLine($"Overall: {(passed == total ? "PASS" : "FAIL")}");

            Environment.ExitCode = passed == total ? 0 : 1;
        }

        /// <summary>
        /// 執行單一固定案例並比對答案。輸入須包含案例名稱、符合題目限制的二元樹，
        /// 以及已知的最少移動次數；輸出案例明細至主控台，並回傳實際結果是否符合預期。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱與層序樹表示法。</param>
        /// <param name="root">待分配硬幣的非空二元樹根節點。</param>
        /// <param name="expected">此案例預期的最少移動次數。</param>
        /// <returns>實際結果等於預期結果時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunTestCase(string name, TreeNode root, int expected)
        {
            int actual = DistributeCoins(root);
            bool passed = actual == expected;

            Console.WriteLine(name);
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed;
        }

        /// <summary>
        /// 計算讓二元樹每個節點都恰好持有一枚硬幣所需的最少移動次數。
        /// 解法以後序深度優先搜尋先取得左右子樹的硬幣盈虧，再用盈虧絕對值
        /// 計算硬幣跨越父子邊的次數。輸入須為符合題目限制的非空二元樹，
        /// 且全樹硬幣總數等於節點總數；輸出為完成平均分配的最少移動次數。
        /// </summary>
        /// <param name="root">待分配硬幣的非空二元樹根節點。</param>
        /// <returns>使每個節點都恰好有一枚硬幣的最少移動次數。</returns>
        public static int DistributeCoins(TreeNode root)
        {
            int moves = 0;
            CalculateBalance(root, ref moves);
            return moves;
        }

        /// <summary>
        /// 以後序 DFS 計算指定子樹在每個節點保留一枚硬幣後的淨盈虧，
        /// 並把左右子樹為了平衡而跨越父子邊的次數累加至 <paramref name="moves"/>。
        /// 輸入可為空節點；空節點回傳零，非空節點則回傳正數盈餘或負數短缺。
        /// </summary>
        /// <param name="node">目前處理的子樹根節點；空子樹可為 <see langword="null"/>。</param>
        /// <param name="moves">目前已累計的硬幣跨邊移動次數。</param>
        /// <returns>此子樹扣除每個節點所需一枚硬幣後的淨硬幣數量。</returns>
        private static int CalculateBalance(TreeNode? node, ref int moves)
        {
            if (node == null)
            {
                return 0;
            }

            int leftBalance = CalculateBalance(node.left, ref moves);
            int rightBalance = CalculateBalance(node.right, ref moves);

            // 子樹的每一枚盈餘或短缺硬幣，都必須恰好跨越與父節點相連的邊一次。
            moves += Math.Abs(leftBalance) + Math.Abs(rightBalance);

            // 正數代表可交給父節點的盈餘，負數代表必須由父節點補入的短缺。
            return node.val + leftBalance + rightBalance - 1;
        }
    }
}
