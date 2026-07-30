namespace leetcode_199
{
    internal class Program
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }


        /// <summary>
        /// 199. Binary Tree Right Side View
        /// https://leetcode.com/problems/binary-tree-right-side-view/description/
        /// 
        /// 199. 二叉树的右视图
        /// https://leetcode.cn/problems/binary-tree-right-side-view/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行固定的二元樹右視圖案例，將每棵樹交給 DFS 與 BFS 解法，
        /// 分別比較預期值與實際結果，最後輸出所有解法的通過項數。
        /// 輸入資料涵蓋空樹、單一節點、一般分支、偏斜樹及重複邊界值；
        /// 輸出為每筆案例的 Expected、Actual、PASS/FAIL 與總結。
        /// </summary>
        private static void RunSamples()
        {
            (string Name, string Input, TreeNode? Root, int[] Expected)[] testCases =
            {
                ("空樹", "[]", null, []),
                ("單一節點", "[1]", new TreeNode(1), [1]),
                (
                    "官方範例",
                    "[1,2,3,null,5,null,4]",
                    new TreeNode(
                        1,
                        new TreeNode(2, null, new TreeNode(5)),
                        new TreeNode(3, null, new TreeNode(4))),
                    [1, 3, 4]),
                (
                    "左側節點在更深層可見",
                    "[1,2,3,4,null,null,null,5]",
                    new TreeNode(
                        1,
                        new TreeNode(2, new TreeNode(4, new TreeNode(5))),
                        new TreeNode(3)),
                    [1, 3, 4, 5]),
                (
                    "僅左子樹鏈",
                    "[1,2,null,3,null,4]",
                    new TreeNode(1, new TreeNode(2, new TreeNode(3, new TreeNode(4)))),
                    [1, 2, 3, 4]),
                (
                    "包含邊界值與重複值",
                    "[0,-100,-100,null,100,100,-100]",
                    new TreeNode(
                        0,
                        new TreeNode(-100, null, new TreeNode(100)),
                        new TreeNode(-100, new TreeNode(100), new TreeNode(-100))),
                    [0, -100, -100])
            };

            int passedCount = 0;
            for (int i = 0; i < testCases.Length; i++)
            {
                (string name, string input, TreeNode? root, int[] expected) = testCases[i];
                passedCount += RunTestCase(i + 1, name, input, root, expected);
            }

            int totalCount = testCases.Length * 2;
            Console.WriteLine($"總結：{passedCount}/{totalCount} 項驗證通過");
            Console.WriteLine(passedCount == totalCount ? "Overall: PASS" : "Overall: FAIL");
        }

        /// <summary>
        /// 執行單一固定案例，將同一棵輸入樹交給 DFS 與 BFS 解法，
        /// 分別比較右視圖結果並輸出 PASS/FAIL。
        /// 輸入根節點可為空；輸出為本案例通過的解法數，範圍為 0 到 2。
        /// </summary>
        /// <param name="caseNumber">顯示於主控台的案例編號。</param>
        /// <param name="caseName">描述案例特性的名稱。</param>
        /// <param name="input">以 LeetCode 層序格式顯示的輸入樹。</param>
        /// <param name="root">待觀察的二元樹根節點；空樹時為 <see langword="null"/>。</param>
        /// <param name="expected">手動推導的預期右視圖節點值。</param>
        /// <returns>DFS 與 BFS 中通過預期比對的解法數。</returns>
        private static int RunTestCase(
            int caseNumber,
            string caseName,
            string input,
            TreeNode? root,
            int[] expected)
        {
            Console.WriteLine($"案例 {caseNumber}：{caseName}");
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Expected: {FormatValues(expected)}");

            int passedCount = 0;
            passedCount += RunSolution("解法一 DFS", RightSideView, root, expected) ? 1 : 0;
            passedCount += RunSolution("解法二 BFS", RightSideView2, root, expected) ? 1 : 0;
            Console.WriteLine();

            return passedCount;
        }

        /// <summary>
        /// 執行指定的右視圖解法，將實際結果與手動指定的預期序列比較，
        /// 並輸出格式化結果與 PASS/FAIL。輸入樹可為空且不會被修改；
        /// 輸出為兩個序列的節點數、數值及順序是否完全相同。
        /// </summary>
        /// <param name="solutionName">顯示於主控台的解法名稱。</param>
        /// <param name="solution">接受根節點並回傳右視圖的解法。</param>
        /// <param name="root">待觀察的根節點；空樹時為 <see langword="null"/>。</param>
        /// <param name="expected">手動推導的預期右視圖節點值。</param>
        /// <returns>實際結果與預期結果完全相同時為 <see langword="true"/>。</returns>
        private static bool RunSolution(
            string solutionName,
            Func<TreeNode?, IList<int>> solution,
            TreeNode? root,
            int[] expected)
        {
            IList<int> actual = solution(root);
            bool passed = actual.SequenceEqual(expected);
            Console.WriteLine(
                $"  {solutionName} Actual: {FormatValues(actual)} => {(passed ? "PASS" : "FAIL")}");

            return passed;
        }

        /// <summary>
        /// 將整數序列轉為易讀的方括號格式，供案例的預期值與實際值共用。
        /// 輸入可為空集合；輸出格式例如 <c>[1, 3, 4]</c>。
        /// </summary>
        /// <param name="values">要格式化的右視圖節點值序列。</param>
        /// <returns>使用方括號與逗號分隔的節點值文字。</returns>
        private static string FormatValues(IEnumerable<int> values)
        {
            return $"[{string.Join(", ", values)}]";
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/binary-tree-right-side-view/solutions/213494/er-cha-shu-de-you-shi-tu-by-leetcode-solution/
        /// https://leetcode.cn/problems/binary-tree-right-side-view/solutions/2015061/ru-he-ling-huo-yun-yong-di-gui-lai-kan-s-r1nc/
        /// https://leetcode.cn/problems/binary-tree-right-side-view/solutions/1459266/199-er-cha-shu-de-you-shi-tu-by-stormsun-dj0b/
        /// 
        /// 取得二叉樹的右視圖
        /// 使用深度優先搜索(DFS)，優先遍歷右子樹
        /// 當遍歷到新的深度時，第一個看到的節點即為該層右視圖可見的節點
        /// </summary>
        /// <param name="root">二叉樹根節點</param>
        /// <returns>右視圖節點值的列表</returns>
        public static IList<int> RightSideView(TreeNode root)
        {
            List<int> res = new List<int>();
            dfs(root, 0, res);
            return res;
        }

        /// <summary>
        /// 深度優先搜索遍歷二叉樹
        /// 先遍歷右子樹，再遍歷左子樹，確保同一深度先訪問到右邊的節點
        /// 
        /// 為什麼第一個看到的節點會是該層右視圖的節點：
        /// 因為我們首先訪問右子樹，當我們到達新的深度時，右子樹的節點會最先被訪問到。
        /// 如果右子樹是空的，那麼左子樹的節點會被訪問到。
        /// 因此，第一個訪問到的節點一定是該層最右邊的節點。
        /// 
        /// </summary>
        /// <param name="root">當前節點</param>
        /// <param name="depth">當前深度</param>
        /// <param name="ans">結果列表</param>
        private static void dfs(TreeNode root, int depth, IList<int> ans)
        {
            // 如果節點為空，則返回
            if (root == null)
            {
                return;
            }

            // 如果當前深度等於結果列表的長度，表示是該層第一個訪問的節點
            // 將該節點的值加入結果列表
            if (depth == ans.Count)
            {
                ans.Add(root.val);
            }

            depth++; // 深度加1
            dfs(root.right, depth, ans); // 優先遍歷右子樹
            dfs(root.left, depth, ans);  // 再遍歷左子樹
        }
    }
}
