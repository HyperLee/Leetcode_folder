namespace leetcode_102
{
    internal class Program
    {
        /// <summary>
        /// 表示二元樹中的單一節點，保存節點值與可為空的左右子節點參考。
        /// 節點可組合成題目使用的二元樹，並作為層序遍歷方法的輸入。
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
            /// <param name="val">節點值，題目限制為 -1000 到 1000。</param>
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
        /// 102. Binary Tree Level Order Traversal
        /// https://leetcode.com/problems/binary-tree-level-order-traversal/description/
        /// 
        /// 102. 二叉树的层序遍历
        /// https://leetcode.cn/problems/binary-tree-level-order-traversal/description/
        /// 
        /// Given the root of a binary tree, return the level order traversal of its nodes' values. (i.e., from left to right, level by level).
        /// 給定一棵二元樹的根節點，返回其節點值的層序遍歷結果。（也就是按照從左到右、逐層的順序進行遍歷）。
        /// 
        /// 層序遍歷, 也就是樹(tree)的每一層進行排序, 注意是"層"
        /// 本題目只是遍歷, 並沒有排序大小. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var testCases = new (string Name, string Input, TreeNode? Root, int[][] Expected)[]
            {
                ("空樹", "[]", null, []),
                ("單一節點", "[1]", new TreeNode(1), [[1]]),
                (
                    "官方範例",
                    "[3,9,20,null,null,15,7]",
                    new TreeNode(
                        3,
                        new TreeNode(9),
                        new TreeNode(20, new TreeNode(15), new TreeNode(7))),
                    [[3], [9, 20], [15, 7]]),
                (
                    "僅有左子節點的鏈狀樹",
                    "[1,2,null,3,null,4]",
                    new TreeNode(1, new TreeNode(2, new TreeNode(3, new TreeNode(4)))),
                    [[1], [2], [3], [4]]),
                (
                    "僅有右子節點的鏈狀樹",
                    "[1,null,2,null,3,null,4]",
                    new TreeNode(1, null, new TreeNode(2, null, new TreeNode(3, null, new TreeNode(4)))),
                    [[1], [2], [3], [4]]),
                (
                    "包含負值、重複值與邊界值",
                    "[0,-1,-1,-1000,1000,-1,-1]",
                    new TreeNode(
                        0,
                        new TreeNode(-1, new TreeNode(-1000), new TreeNode(1000)),
                        new TreeNode(-1, new TreeNode(-1), new TreeNode(-1))),
                    [[0], [-1, -1], [-1000, 1000, -1, -1]])
            };

            int passedCount = 0;
            for (int i = 0; i < testCases.Length; i++)
            {
                (string name, string input, TreeNode? root, int[][] expected) = testCases[i];
                passedCount += RunTestCase(i + 1, name, input, root, expected);
            }

            int totalCount = testCases.Length * 2;
            Console.WriteLine($"總結：{passedCount}/{totalCount} 項驗證通過");
            Console.WriteLine(passedCount == totalCount ? "Overall: PASS" : "Overall: FAIL");
        }

        /// <summary>
        /// 執行單一固定案例，將同一棵輸入樹交給 BFS 與 DFS 解法，
        /// 分別比較預期層級與實際結果並輸出 PASS/FAIL。
        /// 輸入可為空樹；輸出為本案例通過的解法數，範圍為 0 到 2。
        /// </summary>
        /// <param name="caseNumber">顯示於主控台的案例編號。</param>
        /// <param name="caseName">描述案例特性的名稱。</param>
        /// <param name="input">以 LeetCode 層序格式顯示的輸入樹。</param>
        /// <param name="root">待遍歷的二元樹根節點；空樹時為 <see langword="null"/>。</param>
        /// <param name="expected">手動推導的預期分層結果。</param>
        /// <returns>BFS 與 DFS 中通過預期比對的解法數。</returns>
        private static int RunTestCase(
            int caseNumber,
            string caseName,
            string input,
            TreeNode? root,
            int[][] expected)
        {
            Console.WriteLine($"案例 {caseNumber}：{caseName}");
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Expected: {FormatLevels(expected)}");

            int passedCount = 0;
            passedCount += RunSolution("解法一 BFS", LevelOrder, root, expected) ? 1 : 0;
            passedCount += RunSolution("解法二 DFS", LevelOrder2, root, expected) ? 1 : 0;
            Console.WriteLine();

            return passedCount;
        }

        /// <summary>
        /// 執行指定的層序遍歷解法，將輸入樹的實際結果與預期值逐層比較，
        /// 並輸出格式化結果與 PASS/FAIL。輸入樹可以為空；
        /// 輸出為實際結果是否完全符合預期。
        /// </summary>
        /// <param name="solutionName">顯示於主控台的解法名稱。</param>
        /// <param name="solution">接受根節點並回傳分層結果的解法。</param>
        /// <param name="root">待遍歷的根節點；空樹時為 <see langword="null"/>。</param>
        /// <param name="expected">手動推導的預期分層結果。</param>
        /// <returns>實際結果與預期結果完全相同時為 <see langword="true"/>。</returns>
        private static bool RunSolution(
            string solutionName,
            Func<TreeNode?, IList<IList<int>>> solution,
            TreeNode? root,
            int[][] expected)
        {
            IList<IList<int>> actual = solution(root);
            bool passed = AreLevelsEqual(actual, expected);
            Console.WriteLine(
                $"  {solutionName} Actual: {FormatLevels(actual)} => {(passed ? "PASS" : "FAIL")}");

            return passed;
        }

        /// <summary>
        /// 比較實際與預期的層序遍歷結果。兩者必須具有相同層數，
        /// 且每一層的節點數、數值與左右順序都完全相同。
        /// 輸入為兩組分層資料；輸出為是否完全相等。
        /// </summary>
        /// <param name="actual">演算法產生的實際分層結果。</param>
        /// <param name="expected">手動推導的預期分層結果。</param>
        /// <returns>所有層級依序相同時為 <see langword="true"/>。</returns>
        private static bool AreLevelsEqual(IList<IList<int>> actual, int[][] expected)
        {
            if (actual.Count != expected.Length)
            {
                return false;
            }

            for (int i = 0; i < actual.Count; i++)
            {
                if (!actual[i].SequenceEqual(expected[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 將分層整數序列轉成容易閱讀的巢狀陣列文字，
        /// 供案例的預期值與實際值共用。輸入可為空集合；
        /// 輸出格式例如 <c>[[3], [9, 20], [15, 7]]</c>。
        /// </summary>
        /// <param name="levels">要格式化的分層整數序列。</param>
        /// <returns>使用方括號呈現的層序遍歷結果。</returns>
        private static string FormatLevels(IEnumerable<IEnumerable<int>> levels)
        {
            return $"[{string.Join(", ", levels.Select(level => $"[{string.Join(", ", level)}]"))}]";
        }


        /// <summary>
        /// 使用廣度優先搜尋取得二元樹的層序遍歷結果。
        /// 解法以佇列保存尚未處理的節點，並在每輪開始時固定目前層的節點數，
        /// 因此能將下一層節點留到下一輪。輸入可為空樹且不會被修改；
        /// 輸出依深度分組，每一層皆維持由左至右的節點順序。
        /// </summary>
        /// <param name="root">待遍歷的二元樹根節點；空樹時為 <see langword="null"/>。</param>
        /// <returns>由根節點開始、逐層由左至右排列的節點值；空樹回傳空集合。</returns>
        public static IList<IList<int>> LevelOrder(TreeNode? root)
        {
            IList<IList<int>> result = new List<IList<int>>();
            if (root == null)
            {
                return result;
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                IList<int> levelValues = new List<int>();

                // 先固定目前層的節點數，迴圈中新加入的子節點才會留給下一層。
                int levelSize = queue.Count;
                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode node = queue.Dequeue();
                    levelValues.Add(node.val);

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }

                result.Add(levelValues);
            }

            return result;
        }

        /// <summary>
        /// 使用深度優先搜尋取得二元樹的層序分組結果。
        /// 解法在遞迴時攜帶節點深度，首次到達某個深度便建立對應集合，
        /// 再依左子樹、右子樹的順序加入節點。輸入可為空樹且不會被修改；
        /// 輸出依深度分組，每一層皆維持由左至右的節點順序。
        /// </summary>
        /// <param name="root">待遍歷的二元樹根節點；空樹時為 <see langword="null"/>。</param>
        /// <returns>由根節點開始、逐層由左至右排列的節點值；空樹回傳空集合。</returns>
        public static IList<IList<int>> LevelOrder2(TreeNode? root)
        {
            IList<IList<int>> result = new List<IList<int>>();
            AddNodeByDepth(root, 0, result);
            return result;
        }

        /// <summary>
        /// 將目前節點值加入指定深度的集合，再以相同規則走訪左右子樹。
        /// 輸入節點可為空，深度從 0 開始，結果集合由呼叫端累積；
        /// 方法結束後，各深度的節點會依由左至右的順序保存在結果中。
        /// </summary>
        /// <param name="node">目前走訪的節點；空節點會直接結束目前分支。</param>
        /// <param name="depth">目前節點距離根節點的深度，根節點為 0。</param>
        /// <param name="result">依深度累積節點值的結果集合。</param>
        private static void AddNodeByDepth(TreeNode? node, int depth, IList<IList<int>> result)
        {
            if (node == null)
            {
                return;
            }

            // 第一次到達某個深度時，先建立該層的容器。
            if (result.Count == depth)
            {
                result.Add(new List<int>());
            }

            result[depth].Add(node.val);

            // 固定先左後右，確保同一層的節點仍維持由左至右排列。
            AddNodeByDepth(node.left, depth + 1, result);
            AddNodeByDepth(node.right, depth + 1, result);
        }
    }
}
