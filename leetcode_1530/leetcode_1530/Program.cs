namespace leetcode_1530
{
    internal class Program
    {
        /// <summary>
        /// 表示二元樹中的一個節點，保存節點值與可選的左右子樹。
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立二元樹節點。
            /// 輸入節點值與可選的左右子節點，輸出可連接成二元樹的節點物件。
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
        /// 1530. Number of Good Leaf Nodes Pairs
        /// https://leetcode.com/problems/number-of-good-leaf-nodes-pairs/description/?envType=daily-question&envId=2024-07-18
        /// 1530. 好叶子节点对的数量
        /// https://leetcode.cn/problems/number-of-good-leaf-nodes-pairs/description/
        /// <para>English:</para>
        /// You are given the root of a binary tree and an integer distance. A pair of two different leaf nodes of a binary tree is said to be good if the length of the shortest path between them is less than or equal to distance.
        /// Return the number of good leaf node pairs in the tree.
        /// <para>繁體中文：</para>
        /// 給定一棵二元樹的根節點 root，以及一個整數 distance。若一對由兩個不同葉節點組成的節點，其間最短路徑的長度小於或等於 distance，則稱這一對為「好葉節點對」。
        /// 回傳樹中好葉節點對的數量。
        /// </summary>
        /// <remarks>
        /// 主程式會用固定案例分別執行兩種解法，列出預期值、實際值與 PASS/FAIL 結果。
        /// </remarks>
        /// <param name="args">未使用的命令列參數。</param>
        static void Main(string[] args)
        {
            SampleCase[] testCases =
            [
                new(
                    "官方範例一：葉節點距離剛好 3",
                    () => new TreeNode(
                        1,
                        new TreeNode(2, right: new TreeNode(4)),
                        new TreeNode(3)),
                    3,
                    1),
                new(
                    "官方範例二：兩組好葉節點對",
                    () => new TreeNode(
                        1,
                        new TreeNode(2, new TreeNode(4), new TreeNode(5)),
                        new TreeNode(3, new TreeNode(6), new TreeNode(7))),
                    3,
                    2),
                new(
                    "官方範例三：只有一組好葉節點對",
                    () => new TreeNode(
                        7,
                        new TreeNode(1, new TreeNode(6)),
                        new TreeNode(
                            4,
                            new TreeNode(5),
                            new TreeNode(3, right: new TreeNode(2)))),
                    3,
                    1),
                new(
                    "距離剛好符合",
                    () => new TreeNode(1, new TreeNode(2), new TreeNode(3)),
                    2,
                    1),
                new(
                    "距離不足無法配對",
                    () => new TreeNode(1, new TreeNode(2), new TreeNode(3)),
                    1,
                    0),
                new(
                    "重複值節點",
                    () => new TreeNode(
                        1,
                        new TreeNode(1),
                        new TreeNode(1, new TreeNode(1), new TreeNode(1))),
                    3,
                    3),
                new(
                    "單一節點",
                    () => new TreeNode(1),
                    10,
                    0),
                new(
                    "空樹防禦案例",
                    () => null,
                    3,
                    0)
            ];

            int passedChecks = 0;

            Console.WriteLine("LeetCode 1530：好葉子節點對的數量");
            Console.WriteLine();

            for (int i = 0; i < testCases.Length; i++)
            {
                passedChecks += RunTestCase(testCases[i], i + 1);
            }

            int totalChecks = testCases.Length * 2;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// 執行一組固定案例，分別比對兩種解法的回傳結果與手動推導的預期值。
        /// 輸入包含樹的建立函式、距離上限與預期配對數；回傳本案例通過的檢查數量。
        /// </summary>
        /// <param name="testCase">要執行的固定案例。</param>
        /// <param name="caseNumber">顯示用的案例編號。</param>
        /// <returns>兩種解法各自通過時回傳 2，否則回傳通過的檢查數。</returns>
        private static int RunTestCase(SampleCase testCase, int caseNumber)
        {
            string treeDescription = SerializeLevelOrder(testCase.CreateTree());
            int actualByDistances = CountPairs(testCase.CreateTree(), testCase.Distance);
            int actualByPaths = CountPairsByLeafPaths(testCase.CreateTree(), testCase.Distance);
            bool distancesPassed = actualByDistances == testCase.Expected;
            bool pathsPassed = actualByPaths == testCase.Expected;

            Console.WriteLine($"案例 {caseNumber}：{testCase.Name}");
            Console.WriteLine($"輸入：root = {treeDescription}, distance = {testCase.Distance}");
            Console.WriteLine($"預期：{testCase.Expected}");
            Console.WriteLine($"CountPairs：實際 = {actualByDistances}，結果 = {(distancesPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"CountPairsByLeafPaths：實際 = {actualByPaths}，結果 = {(pathsPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (distancesPassed ? 1 : 0) + (pathsPassed ? 1 : 0);
        }

        /// <summary>
        /// 將二元樹序列化為 LeetCode 常用的層序陣列格式，供 console 與 README 顯示。
        /// 輸入可為空樹；輸出會保留必要的中間 <c>null</c>，並移除尾端無關的 <c>null</c>。
        /// </summary>
        /// <param name="root">要序列化的二元樹根節點。</param>
        /// <returns>層序陣列字串；空樹輸出 <c>[]</c>。</returns>
        private static string SerializeLevelOrder(TreeNode? root)
        {
            if (root is null)
            {
                return "[]";
            }

            List<string> values = new List<string>();
            Queue<TreeNode?> nodes = new Queue<TreeNode?>();
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
        /// 使用後序遞迴收集每個子樹到葉節點的距離，並在同一個節點合併左右子樹的距離。
        /// 當兩個葉節點分別位於左右子樹，且經過目前節點的總距離不超過 distance，
        /// 就將它們計入答案。輸入為二元樹根節點與距離上限，輸出為好葉節點對數量。
        ///
        /// https://leetcode.cn/problems/number-of-good-leaf-nodes-pairs/solutions/357905/hao-xie-zi-jie-dian-dui-de-shu-liang-by-leetcode-s/
        /// https://leetcode.cn/problems/number-of-good-leaf-nodes-pairs/solutions/347315/good-leaf-nodes-pairs-by-ikaruga/
        /// https://leetcode.cn/problems/number-of-good-leaf-nodes-pairs/solutions/1461559/1530-hao-xie-zi-jie-dian-dui-de-shu-lian-wltu/
        ///
        /// </summary>
        /// <param name="root">待分析的二元樹根節點；空樹時回傳 0。</param>
        /// <param name="distance">好葉節點對允許的最大路徑長度。</param>
        /// <returns>符合距離條件的葉節點對數量。</returns>
        public static int CountPairs(TreeNode? root, int distance)
        {
            int pairCount = 0;
            CollectDistances(root, distance, ref pairCount);

            return pairCount;
        }

        /// <summary>
        /// 以後序方式收集子樹的葉節點距離，並把跨左右子樹的合法配對累加到呼叫端計數器。
        /// 簡單說步驟如下
        /// 1. node 為空 無距離
        /// 2. node 無左右子樹, 距離為 0 (node 到葉節點距離為 0)
        /// 3. 分別計算左子樹距離與右子樹距離
        ///    再來把左 + 右距離加總 (葉節點要求必須包含左右子樹節點, 不能只有單邊節點)
        /// 4. 上述三種 case 分別計算出 題目要求的 好叶子节点对的数量
        /// 
        /// 我們只需要紀錄 <= distance 的距離即可
        /// 超過的不用加入 list 裡面
        /// </summary>
        /// <param name="node">目前處理的子樹根節點。</param>
        /// <param name="distance">距離保留上限。</param>
        /// <param name="pairCount">由本次 CountPairs 呼叫持有的配對計數器。</param>
        /// <returns>目前節點到其子樹葉節點且不超過 distance 的距離清單。</returns>
        private static IList<int> CollectDistances(TreeNode? node, int distance, ref int pairCount)
        {
            IList<int> distances = new List<int>();

            // 1. node 為空無距離
            if (node is null)
            {
                return distances;
            }

            // 2. node 無左右子樹, 距離為 0 ( node 到葉節點距離為 0 )
            if (node.left is null && node.right is null)
            {
                distances.Add(0);
                return distances;
            }

            // 3. 分別計算左子樹距離與右子樹距離
            IList<int> leftDistances = CollectDistances(node.left, distance, ref pairCount);
            IList<int> rightDistances = CollectDistances(node.right, distance, ref pairCount);

            // 從子樹往目前節點移動一層，因此所有葉節點距離都要加一；超過上限者不必保留。
            foreach (int leftDistance in leftDistances)
            {
                if (leftDistance + 1 <= distance)
                {
                    distances.Add(leftDistance + 1);
                }
            }

            foreach (int rightDistance in rightDistances)
            {
                if (rightDistance + 1 <= distance)
                {
                    distances.Add(rightDistance + 1);
                }
            }

            // 4. 只有分屬左右子樹的葉節點對會以目前節點為路徑中繼點。
            foreach (int leftDistance in leftDistances)
            {
                foreach (int rightDistance in rightDistances)
                {
                    if (leftDistance + rightDistance + 2 <= distance)
                    {
                        pairCount++;
                    }
                }
            }

            return distances;
        }

        /// <summary>
        /// 先收集所有 root-to-leaf 路徑，再逐對計算共同前綴所代表的最低共同祖先距離。
        /// 輸入為二元樹根節點與距離上限，輸出為距離不超過上限的好葉節點對數量。
        /// </summary>
        /// <remarks>
        /// 此解法保留完整路徑，概念直觀但需要逐一檢查葉節點對；空樹回傳 0。
        /// </remarks>
        /// <param name="root">待分析的二元樹根節點；空樹時回傳 0。</param>
        /// <param name="distance">好葉節點對允許的最大路徑長度。</param>
        /// <returns>符合距離條件的葉節點對數量。</returns>
        public static int CountPairsByLeafPaths(TreeNode? root, int distance)
        {
            List<List<TreeNode>> leafPaths = new List<List<TreeNode>>();
            List<TreeNode> currentPath = new List<TreeNode>();

            // DFS 沿著目前路徑走到葉節點，保存一份快照後再回溯。
            void CollectLeafPaths(TreeNode? node)
            {
                if (node == null)
                {
                    return;
                }

                currentPath.Add(node);

                if (node.left == null && node.right == null)
                {
                    // 必須複製路徑，否則回溯時會改掉已收集的葉節點路徑。
                    leafPaths.Add(new List<TreeNode>(currentPath));
                }
                else
                {
                    CollectLeafPaths(node.left);
                    CollectLeafPaths(node.right);
                }

                currentPath.RemoveAt(currentPath.Count - 1);
            }

            CollectLeafPaths(root);

            int pairCount = 0;

            for (int leftIndex = 0; leftIndex < leafPaths.Count; leftIndex++)
            {
                for (int rightIndex = leftIndex + 1; rightIndex < leafPaths.Count; rightIndex++)
                {
                    int commonNodeCount = 0;
                    List<TreeNode> leftPath = leafPaths[leftIndex];
                    List<TreeNode> rightPath = leafPaths[rightIndex];

                    // 重複值節點仍可能是不同物件，因此比較節點參考而不是 val。
                    while (commonNodeCount < leftPath.Count
                        && commonNodeCount < rightPath.Count
                        && ReferenceEquals(leftPath[commonNodeCount], rightPath[commonNodeCount]))
                    {
                        commonNodeCount++;
                    }

                    int pathDistance = leftPath.Count + rightPath.Count - (commonNodeCount * 2);
                    if (pathDistance <= distance)
                    {
                        pairCount++;
                    }
                }
            }

            return pairCount;
        }

        /// <summary>
        /// 表示一組固定 console 驗證案例，使用建立函式確保兩種解法不共用同一棵可變樹。
        /// </summary>
        /// <param name="Name">案例顯示名稱。</param>
        /// <param name="CreateTree">每次呼叫時建立獨立樹根的函式。</param>
        /// <param name="Distance">判定好葉節點對的最大距離。</param>
        /// <param name="Expected">手動推導的預期好葉節點對數量。</param>
        private sealed record SampleCase(
            string Name,
            Func<TreeNode?> CreateTree,
            int Distance,
            int Expected);

    }
}