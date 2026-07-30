namespace leetcode_199
{
    internal class Program
    {
        /// <summary>
        /// 表示二元樹中的單一節點，保存節點值與可為空的左右子節點參考。
        /// 節點可組合成題目使用的二元樹，並作為右視圖方法的輸入。
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立一個二元樹節點。輸入節點值與可省略的左右子節點，
            /// 輸出為保存指定值及子樹參考的新節點。
            /// </summary>
            /// <param name="val">節點值，題目限制為 -100 到 100。</param>
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
        /// 使用深度優先搜尋取得二元樹的右視圖。
        /// 解法攜帶目前深度並固定先走右子樹，因此首次抵達新深度的節點
        /// 就是該層從右側可見的節點。輸入可為空樹且不會被修改；
        /// 輸出依照由上到下的順序保存每層最右側節點值。
        /// </summary>
        /// <param name="root">待觀察的二元樹根節點；空樹時為 <see langword="null"/>。</param>
        /// <returns>由上到下排列的右視圖節點值；空樹回傳空集合。</returns>
        public static IList<int> RightSideView(TreeNode? root)
        {
            IList<int> result = new List<int>();
            Dfs(root, 0, result);
            return result;
        }

        /// <summary>
        /// 以右子樹優先的 DFS 走訪目前分支，並在首次抵達新深度時記錄節點值。
        /// 輸入節點可為空，深度從 0 開始，結果集合由呼叫端累積；
        /// 方法完成後，結果會包含已走訪各層的最右側節點。
        /// </summary>
        /// <param name="node">目前走訪的節點；空節點會直接結束目前分支。</param>
        /// <param name="depth">目前節點距離根節點的深度，根節點為 0。</param>
        /// <param name="result">依深度累積右視圖節點值的結果集合。</param>
        private static void Dfs(TreeNode? node, int depth, IList<int> result)
        {
            if (node == null)
            {
                return;
            }

            // 先右後左會讓每個新深度首次遇到的節點正好位於該層最右側。
            if (depth == result.Count)
            {
                result.Add(node.val);
            }

            Dfs(node.right, depth + 1, result);
            Dfs(node.left, depth + 1, result);
        }

        /// <summary>
        /// 使用廣度優先搜尋取得二元樹的右視圖。
        /// 解法以佇列逐層走訪，先固定目前層的節點數，再將該層最後取出的
        /// 節點加入結果。輸入可為空樹且不會被修改；
        /// 輸出依照由上到下的順序保存每層最右側節點值。
        /// </summary>
        /// <param name="root">待觀察的二元樹根節點；空樹時為 <see langword="null"/>。</param>
        /// <returns>由上到下排列的右視圖節點值；空樹回傳空集合。</returns>
        public static IList<int> RightSideView2(TreeNode? root)
        {
            IList<int> result = new List<int>();
            if (root == null)
            {
                return result;
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                // 固定本層節點數，處理時加入的子節點會完整留給下一層。
                int levelSize = queue.Count;
                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode node = queue.Dequeue();

                    // 由左至右出列時，本層最後一個節點就是右視圖可見節點。
                    if (i == levelSize - 1)
                    {
                        result.Add(node.val);
                    }

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }
            }

            return result;
        }
    }
}
