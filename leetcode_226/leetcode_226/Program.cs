namespace leetcode_226
{
    /// <summary>
    /// 表示二元樹節點，保存節點值以及可為空的左右子節點。
    /// 節點值需符合題目限制；空子樹以 <see langword="null"/> 表示。
    /// </summary>
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        /// <summary>
        /// 建立一個二元樹節點，並可選擇指定左右子樹。
        /// </summary>
        /// <param name="val">目前節點的整數值。</param>
        /// <param name="left">左子樹根節點；沒有左子樹時為 <see langword="null"/>。</param>
        /// <param name="right">右子樹根節點；沒有右子樹時為 <see langword="null"/>。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }


    internal class Program
    {
        /// <summary>
        /// <para>
        /// 226. Invert Binary Tree
        /// https://leetcode.com/problems/invert-binary-tree/description/
        ///
        /// Given the root of a binary tree, invert the tree and return its root.
        ///
        /// Images: https://assets.leetcode.com/uploads/2021/03/14/invert1-tree.jpg and https://assets.leetcode.com/uploads/2021/03/14/invert2-tree.jpg
        ///
        /// Example 1:
        /// Input: root = [4,2,7,1,3,6,9]
        /// Output: [4,7,2,9,6,3,1]
        ///
        /// Example 2:
        /// Input: root = [2,1,3]
        /// Output: [2,3,1]
        ///
        /// Example 3:
        /// Input: root = []
        /// Output: []
        ///
        /// Constraints:
        /// - The number of nodes is in [0,100].
        /// - -100 &lt;= Node.val &lt;= 100
        /// </para>
        /// <para>
        /// 226. 反轉二元樹
        /// https://leetcode.cn/problems/invert-binary-tree/description/
        ///
        /// 給定二元樹的根節點，反轉整棵樹並回傳其根節點。
        ///
        /// 圖片：https://assets.leetcode.com/uploads/2021/03/14/invert1-tree.jpg 與 https://assets.leetcode.com/uploads/2021/03/14/invert2-tree.jpg
        ///
        /// 範例 1：
        /// 輸入：root = [4,2,7,1,3,6,9]
        /// 輸出：[4,7,2,9,6,3,1]
        ///
        /// 範例 2：
        /// 輸入：root = [2,1,3]
        /// 輸出：[2,3,1]
        ///
        /// 範例 3：
        /// 輸入：root = []
        /// 輸出：[]
        ///
        /// 限制條件：
        /// - 節點數量在 [0,100] 範圍內。
        /// - -100 &lt;= Node.val &lt;= 100
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行六組二元樹案例，分別驗證遞迴、迭代 DFS 與迭代 BFS 三種反轉解法。
        /// 每次呼叫解法前都由層序陣列建立新樹，避免原地修改影響其他解法，最後輸出 18 項檢查摘要。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] sampleCases =
            {
                new("官方範例 1：完整二元樹", [4, 2, 7, 1, 3, 6, 9], [4, 7, 2, 9, 6, 3, 1]),
                new("官方範例 2：三個節點", [2, 1, 3], [2, 3, 1]),
                new("官方範例 3：空樹", [], []),
                new("單一節點", [1], [1]),
                new("節點值上下界", [0, -100, 100], [0, 100, -100]),
                new(
                    "非對稱且含重複值",
                    [1, 2, 2, 3, null, 4, 3],
                    [1, 2, 2, 3, 4, null, 3])
            };

            int passedChecks = 0;

            for (int index = 0; index < sampleCases.Length; index++)
            {
                passedChecks += RunSample(index + 1, sampleCases[index]);
            }

            RunTraversalDemo();

            int totalChecks = sampleCases.Length * 3;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項演算法驗證通過");
        }

        /// <summary>
        /// 對單一案例建立三棵獨立輸入樹，執行所有反轉方法並將層序結果與預期值逐一比較。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="sampleCase">包含案例名稱、層序輸入與預期層序輸出的測試資料。</param>
        /// <returns>三種解法中通過預期結果比對的項目數。</returns>
        private static int RunSample(int caseNumber, SampleCase sampleCase)
        {
            (string Name, Func<TreeNode?, TreeNode?> Solution)[] solutions =
            {
                ("遞迴 DFS", InvertTree),
                ("迭代 DFS", InvertTree2),
                ("迭代 BFS", InvertTree3)
            };

            int passedChecks = 0;

            Console.WriteLine($"案例 {caseNumber}：{sampleCase.Name}");
            Console.WriteLine($"  輸入：{FormatTree(sampleCase.Input)}");
            Console.WriteLine($"  預期：{FormatTree(sampleCase.Expected)}");

            foreach ((string name, Func<TreeNode?, TreeNode?> solution) in solutions)
            {
                TreeNode? root = BuildTree(sampleCase.Input);
                int?[] actual = SerializeTree(solution(root));
                bool passed = actual.SequenceEqual(sampleCase.Expected);
                passedChecks += Convert.ToInt32(passed);

                Console.WriteLine(
                    $"  {name}：{FormatTree(actual)} => {(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
            return passedChecks;
        }

        /// <summary>
        /// 以官方完整樹案例展示反轉結果的中序、前序與後序走訪順序。
        /// 輸入樹會先使用遞迴解法反轉，再由三種遍歷方法直接輸出節點值。
        /// </summary>
        private static void RunTraversalDemo()
        {
            TreeNode? root = BuildTree([4, 2, 7, 1, 3, 6, 9]);
            TreeNode? invertedRoot = InvertTree(root);

            Console.WriteLine("遍歷展示：反轉 [4,2,7,1,3,6,9] 後的樹");
            Console.Write("  中序（左 -> 根 -> 右）：");
            InOrder(invertedRoot);
            Console.WriteLine();

            Console.Write("  前序（根 -> 左 -> 右）：");
            PreOrder(invertedRoot);
            Console.WriteLine();

            Console.Write("  後序（左 -> 右 -> 根）：");
            PostOrder(invertedRoot);
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// 由 LeetCode 層序陣列建立二元樹，陣列中的 <see langword="null"/> 代表該位置沒有節點。
        /// 空陣列或根元素為 <see langword="null"/> 時回傳空樹。
        /// </summary>
        /// <param name="values">依層序排列的節點值，內容必須表示一棵合法二元樹。</param>
        /// <returns>建立完成的根節點；輸入表示空樹時回傳 <see langword="null"/>。</returns>
        private static TreeNode? BuildTree(int?[] values)
        {
            if (values.Length == 0 || values[0] is not int rootValue)
            {
                return null;
            }

            TreeNode root = new(rootValue);
            Queue<TreeNode> nodes = new();
            nodes.Enqueue(root);
            int index = 1;

            while (nodes.Count > 0 && index < values.Length)
            {
                TreeNode node = nodes.Dequeue();

                if (values[index] is int leftValue)
                {
                    node.left = new TreeNode(leftValue);
                    nodes.Enqueue(node.left);
                }

                index++;

                if (index < values.Length && values[index] is int rightValue)
                {
                    node.right = new TreeNode(rightValue);
                    nodes.Enqueue(node.right);
                }

                index++;
            }

            return root;
        }

        /// <summary>
        /// 將二元樹轉為 LeetCode 層序陣列，保留樹內部缺少子節點的位置。
        /// 空樹回傳空陣列，並移除結果尾端不影響樹形的連續 <see langword="null"/>。
        /// </summary>
        /// <param name="root">要序列化的二元樹根節點；可以是空樹。</param>
        /// <returns>可穩定比較與顯示的層序節點值陣列。</returns>
        private static int?[] SerializeTree(TreeNode? root)
        {
            if (root is null)
            {
                return [];
            }

            List<int?> values = [];
            Queue<TreeNode?> nodes = new();
            nodes.Enqueue(root);

            while (nodes.Count > 0)
            {
                TreeNode? node = nodes.Dequeue();

                if (node is null)
                {
                    values.Add(null);
                    continue;
                }

                values.Add(node.val);
                nodes.Enqueue(node.left);
                nodes.Enqueue(node.right);
            }

            // 尾端的 null 不會提供額外樹形資訊，移除後才能得到標準層序表示。
            while (values.Count > 0 && values[^1] is null)
            {
                values.RemoveAt(values.Count - 1);
            }

            return values.ToArray();
        }

        /// <summary>
        /// 將層序節點陣列格式化為 README 與主控台共用的穩定文字。
        /// </summary>
        /// <param name="values">要格式化的可空節點值陣列。</param>
        /// <returns>例如 <c>[1,2,null,3]</c>；空陣列則回傳 <c>[]</c>。</returns>
        private static string FormatTree(int?[] values)
        {
            return $"[{string.Join(",", values.Select(
                static value => value?.ToString() ?? "null"))}]";
        }

        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10227341
        /// https://leetcode.cn/problems/invert-binary-tree/solution/fan-zhuan-er-cha-shu-by-leetcode-solution/
        /// 
        /// 採用 遞迴 作法
        /// 將 tree 反轉, 
        /// 1. 判斷 root 是否為 null，若為 null 回傳 root;
        /// 2. 宣告 TreeNode tmpLeft 為 root.left 暫存;
        /// 3. 宣告 TreeNode tmpRight 為 root.right 暫存;
        /// 4. 此時使用遞迴將所有 TreeNode 對調 
        ///     root.left = InvertTree(tmpRight);
        ///     root.right = InvertTree(tmpLeft);
        ///     對調完成後回傳 root
        ///     
        /// </summary>
        /// <param name="root">要原地反轉的二元樹根節點；可以是空樹。</param>
        /// <returns>反轉後的同一個根節點；輸入為空樹時回傳 <see langword="null"/>。</returns>
        public static TreeNode? InvertTree(TreeNode? root)
        {
            if (root is null)
            {
                return null;
            }

            TreeNode? originalLeft = root.left;

            // 先遞迴反轉兩棵子樹，再把它們放到相反方向。
            root.left = InvertTree(root.right);
            root.right = InvertTree(originalLeft);

            return root;
        }

        /// <summary>
        /// 使用堆疊進行迭代深度優先搜尋，取出每個節點後原地交換左右子樹。
        /// 輸入可以是空樹；非空輸入會回傳相同根節點，且每個節點恰好處理一次。
        /// </summary>
        /// <param name="root">要原地反轉的二元樹根節點；可以是空樹。</param>
        /// <returns>反轉後的同一個根節點；輸入為空樹時回傳 <see langword="null"/>。</returns>
        public static TreeNode? InvertTree2(TreeNode? root)
        {
            if (root is null)
            {
                return null;
            }

            Stack<TreeNode> nodes = new();
            nodes.Push(root);

            while (nodes.Count > 0)
            {
                TreeNode node = nodes.Pop();

                // 每個節點在彈出時交換左右子樹，堆疊則保存尚未處理的節點。
                (node.left, node.right) = (node.right, node.left);

                if (node.right is not null)
                {
                    nodes.Push(node.right);
                }

                if (node.left is not null)
                {
                    nodes.Push(node.left);
                }
            }

            return root;
        }

        /// <summary>
        /// 使用佇列進行迭代廣度優先搜尋，逐層取出節點並原地交換左右子樹。
        /// 輸入可以是空樹；非空輸入會回傳相同根節點，且每個節點恰好處理一次。
        /// </summary>
        /// <param name="root">要原地反轉的二元樹根節點；可以是空樹。</param>
        /// <returns>反轉後的同一個根節點；輸入為空樹時回傳 <see langword="null"/>。</returns>
        public static TreeNode? InvertTree3(TreeNode? root)
        {
            if (root is null)
            {
                return null;
            }

            Queue<TreeNode> nodes = new();
            nodes.Enqueue(root);

            while (nodes.Count > 0)
            {
                TreeNode node = nodes.Dequeue();

                // 交換後再加入子節點，確保下一層會沿著反轉後的左右方向處理。
                (node.left, node.right) = (node.right, node.left);

                if (node.left is not null)
                {
                    nodes.Enqueue(node.left);
                }

                if (node.right is not null)
                {
                    nodes.Enqueue(node.right);
                }
            }

            return root;
        }

        /// <summary>
        /// 以中序方式走訪二元樹，依序將左子樹、根節點、右子樹的值寫入主控台。
        /// 空樹不輸出任何內容；輸出結果沒有額外換行。
        /// </summary>
        /// <param name="node">目前要走訪的節點；可以是 <see langword="null"/>。</param>
        public static void InOrder(TreeNode? node)
        {
            List<int> values = [];
            CollectInOrder(node, values);
            Console.Write(string.Join(" ", values));
        }

        /// <summary>
        /// 以前序方式走訪二元樹，依序將根節點、左子樹、右子樹的值寫入主控台。
        /// 空樹不輸出任何內容；輸出結果沒有額外換行。
        /// </summary>
        /// <param name="node">目前要走訪的節點；可以是 <see langword="null"/>。</param>
        public static void PreOrder(TreeNode? node)
        {
            List<int> values = [];
            CollectPreOrder(node, values);
            Console.Write(string.Join(" ", values));
        }

        /// <summary>
        /// 以後序方式走訪二元樹，依序將左子樹、右子樹、根節點的值寫入主控台。
        /// 空樹不輸出任何內容；輸出結果沒有額外換行。
        /// </summary>
        /// <param name="node">目前要走訪的節點；可以是 <see langword="null"/>。</param>
        public static void PostOrder(TreeNode? node)
        {
            List<int> values = [];
            CollectPostOrder(node, values);
            Console.Write(string.Join(" ", values));
        }

        /// <summary>
        /// 遞迴收集中序遍歷值，供 <see cref="InOrder"/> 以無行尾空白的格式一次輸出。
        /// </summary>
        /// <param name="node">目前走訪的節點；可以是 <see langword="null"/>。</param>
        /// <param name="values">依中序順序累積節點值的清單。</param>
        private static void CollectInOrder(TreeNode? node, List<int> values)
        {
            if (node is null)
            {
                return;
            }

            CollectInOrder(node.left, values);
            values.Add(node.val);
            CollectInOrder(node.right, values);
        }

        /// <summary>
        /// 遞迴收集前序遍歷值，供 <see cref="PreOrder"/> 以無行尾空白的格式一次輸出。
        /// </summary>
        /// <param name="node">目前走訪的節點；可以是 <see langword="null"/>。</param>
        /// <param name="values">依前序順序累積節點值的清單。</param>
        private static void CollectPreOrder(TreeNode? node, List<int> values)
        {
            if (node is null)
            {
                return;
            }

            values.Add(node.val);
            CollectPreOrder(node.left, values);
            CollectPreOrder(node.right, values);
        }

        /// <summary>
        /// 遞迴收集後序遍歷值，供 <see cref="PostOrder"/> 以無行尾空白的格式一次輸出。
        /// </summary>
        /// <param name="node">目前走訪的節點；可以是 <see langword="null"/>。</param>
        /// <param name="values">依後序順序累積節點值的清單。</param>
        private static void CollectPostOrder(TreeNode? node, List<int> values)
        {
            if (node is null)
            {
                return;
            }

            CollectPostOrder(node.left, values);
            CollectPostOrder(node.right, values);
            values.Add(node.val);
        }

        /// <summary>
        /// 表示一筆可重複執行的二元樹案例，包含名稱、層序輸入與反轉後的預期結果。
        /// </summary>
        /// <param name="Name">案例用途或覆蓋情境。</param>
        /// <param name="Input">反轉前的 LeetCode 層序陣列。</param>
        /// <param name="Expected">反轉後預期得到的層序陣列。</param>
        private sealed record SampleCase(string Name, int?[] Input, int?[] Expected);
    }
}
