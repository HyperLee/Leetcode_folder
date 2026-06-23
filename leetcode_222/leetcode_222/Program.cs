namespace leetcode_222;

class Program
{
    /// <summary>
    /// 表示二元樹中的單一節點，提供節點值以及左右子節點參考。
    /// 輸入條件是使用者會以完整二元樹的層序資料來組裝樹。
    /// 輸出結果是可供各種節點計數解法使用的節點物件。
    /// </summary>
    public sealed class TreeNode
    {
        /// <summary>
        /// 取得目前節點儲存的整數值。
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// 取得或設定左子節點；若不存在則為 <see langword="null"/>。
        /// </summary>
        public TreeNode? Left { get; set; }

        /// <summary>
        /// 取得或設定右子節點；若不存在則為 <see langword="null"/>。
        /// </summary>
        public TreeNode? Right { get; set; }

        /// <summary>
        /// 初始化一個樹節點，允許在建樹時直接指定左右子節點。
        /// 輸入條件是值為整數，左右子節點可省略為 <see langword="null"/>。
        /// 輸出結果是一個可串接成完整二元樹的節點物件。
        /// </summary>
        /// <param name="value">目前節點的整數值。</param>
        /// <param name="left">左子節點，可為 <see langword="null"/>。</param>
        /// <param name="right">右子節點，可為 <see langword="null"/>。</param>
        public TreeNode(int value = 0, TreeNode? left = null, TreeNode? right = null)
        {
            Value = value;
            Left = left;
            Right = right;
        }
    }

    /// <summary>
    /// 222. Count Complete Tree Nodes
    /// https://leetcode.com/problems/count-complete-tree-nodes/description/
    /// 222. 完全二叉树的节点个数
    /// https://leetcode.cn/problems/count-complete-tree-nodes/description/
    ///
    /// Given the root of a complete binary tree, return the number of the nodes in the tree.
    ///
    /// According to [Wikipedia](http://en.wikipedia.org/wiki/Binary_tree#Types_of_binary_trees), every level, except possibly the last, is completely filled in a complete binary tree, and all nodes in the last level are as far left as possible.
    /// It can have between 1 and 2^h nodes inclusive at the last level h.
    ///
    /// Design an algorithm that runs in less than O(n) time complexity.
    ///
    /// 給定一棵完全二元樹的根節點，請回傳樹中節點的數量。
    ///
    /// 根據維基百科，完全二元樹除了最後一層之外，每一層皆完全填滿，且最後一層的節點會盡可能靠左。
    /// 最後一層 h 的節點數可介於 1 到 2^h（含）之間。
    ///
    /// 請設計一個時間複雜度低於 O(n) 的演算法。
    /// </summary>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行固定的完整二元樹測資，逐一比較三種解法是否都得到預期節點數。
    /// 輸入條件是內建的 level-order 整數陣列必須代表完整二元樹。
    /// 輸出結果是具備 deterministic 格式的主控台報表，方便 README 直接引用。
    /// </summary>
    private static void RunSamples()
    {
        (int[] Values, int Expected)[] sampleCases =
        {
            (Array.Empty<int>(), 0),
            (new[] { 1 }, 1),
            (new[] { 1, 2, 3, 4, 5, 6 }, 6),
            (new[] { 1, 2, 3, 4, 5, 6, 7 }, 7),
            (new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10),
        };

        int passedCount = 0;

        Console.WriteLine("LeetCode 222 - Count Complete Tree Nodes");
        Console.WriteLine("========================================");
        Console.WriteLine();

        for (int i = 0; i < sampleCases.Length; i++)
        {
            bool isPassed = RunSample(i + 1, sampleCases[i].Values, sampleCases[i].Expected);
            if (isPassed)
            {
                passedCount++;
            }
        }

        Console.WriteLine($"Summary: {passedCount}/{sampleCases.Length} sample(s) passed.");
    }

    /// <summary>
    /// 執行單筆 sample，將輸入、預期值與三種解法的輸出整理成對照格式。
    /// 輸入條件是 values 必須用 level-order 表示一棵完整二元樹，expected 為預期節點數。
    /// 輸出結果是該筆 sample 是否通過，並同步印出 README 可直接沿用的驗證文字。
    /// </summary>
    /// <param name="caseNumber">顯示在主控台上的案例編號。</param>
    /// <param name="values">以 level-order 排列的完整二元樹節點值。</param>
    /// <param name="expected">該棵樹預期應有的節點總數。</param>
    /// <returns>若三種解法都與預期值一致則回傳 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private static bool RunSample(int caseNumber, int[] values, int expected)
    {
        TreeNode? root = BuildCompleteTree(values);
        int optimizedResult = CountNodes(root);
        int recursiveResult = CountNodesRecursive(root);
        int breadthFirstResult = CountNodesBreadthFirst(root);
        bool isPassed =
            optimizedResult == expected &&
            recursiveResult == expected &&
            breadthFirstResult == expected;

        Console.WriteLine($"Case {caseNumber}");
        Console.WriteLine($"Input (level-order): {FormatArray(values)}");
        Console.WriteLine($"Expected nodes: {expected}");
        Console.WriteLine($"Optimized CountNodes: {optimizedResult}");
        Console.WriteLine($"Recursive CountNodesRecursive: {recursiveResult}");
        Console.WriteLine($"Breadth-first CountNodesBreadthFirst: {breadthFirstResult}");
        Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
        Console.WriteLine();

        return isPassed;
    }

    /// <summary>
    /// 使用完整二元樹的高度特性，以遞迴方式在低於 O(n) 的時間內計算節點數。
    /// 輸入條件是 root 應為完整二元樹的根節點，才能保證利用左右子樹高度做剪枝。
    /// 輸出結果是整棵完整二元樹的節點總數；空樹則回傳 0。
    /// </summary>
    /// <param name="root">完整二元樹的根節點，可為 <see langword="null"/>。</param>
    /// <returns>樹中的節點總數。</returns>
    public static int CountNodes(TreeNode? root)
    {
        if (root == null)
        {
            return 0;
        }

        int leftDepth = GetLeftDepth(root.Left);
        int rightDepth = GetLeftDepth(root.Right);

        // 左右最左深度相同時，左子樹一定是滿二元樹，可以直接用公式算出節點數，
        // 只需要遞迴處理右側尚未填滿的那一邊。
        if (leftDepth == rightDepth)
        {
            int leftPerfectNodeCount = (1 << leftDepth) - 1;
            return 1 + leftPerfectNodeCount + CountNodes(root.Right);
        }

        // 若左深度較大，表示右子樹是較矮的滿二元樹，遞迴處理左側不完整的區域即可。
        int rightPerfectNodeCount = (1 << rightDepth) - 1;
        return 1 + rightPerfectNodeCount + CountNodes(root.Left);
    }

    /// <summary>
    /// 使用最直觀的 DFS 遞迴逐節點計數，作為完整二元樹最佳解法的線性時間對照組。
    /// 輸入條件是 root 可以是任意二元樹根節點，空樹也可接受。
    /// 輸出結果是整棵樹的節點總數，時間複雜度為 O(n)。
    /// </summary>
    /// <param name="root">任意二元樹的根節點，可為 <see langword="null"/>。</param>
    /// <returns>樹中的節點總數。</returns>
    public static int CountNodesRecursive(TreeNode? root)
    {
        if (root == null)
        {
            return 0;
        }

        return 1 + CountNodesRecursive(root.Left) + CountNodesRecursive(root.Right);
    }

    /// <summary>
    /// 使用 BFS 層序走訪逐節點累加，展示不依賴完整二元樹特性的迭代寫法。
    /// 輸入條件是 root 可以是任意二元樹根節點，空樹也可接受。
    /// 輸出結果是樹中的節點總數，時間複雜度同樣為 O(n)。
    /// </summary>
    /// <param name="root">任意二元樹的根節點，可為 <see langword="null"/>。</param>
    /// <returns>樹中的節點總數。</returns>
    public static int CountNodesBreadthFirst(TreeNode? root)
    {
        if (root == null)
        {
            return 0;
        }

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int count = 0;

        while (queue.Count > 0)
        {
            TreeNode node = queue.Dequeue();
            count++;

            if (node.Left != null)
            {
                queue.Enqueue(node.Left);
            }

            if (node.Right != null)
            {
                queue.Enqueue(node.Right);
            }
        }

        return count;
    }

    /// <summary>
    /// 取得某棵子樹沿著最左分支向下的深度，用來判斷該子樹是否為滿二元樹。
    /// 輸入條件是 node 可為 <see langword="null"/>；若為空，深度定義為 0。
    /// 輸出結果是從目前節點開始向左走到底時經過的層數。
    /// </summary>
    /// <param name="node">要量測深度的子樹根節點，可為 <see langword="null"/>。</param>
    /// <returns>沿著最左分支向下的層數。</returns>
    private static int GetLeftDepth(TreeNode? node)
    {
        int depth = 0;

        while (node != null)
        {
            depth++;
            node = node.Left;
        }

        return depth;
    }

    /// <summary>
    /// 依照 level-order 陣列建立完整二元樹，讓 sample 能用簡潔的陣列資料描述測資。
    /// 輸入條件是 values 必須代表沒有空洞的完整二元樹層序資料；空陣列代表空樹。
    /// 輸出結果是對應的根節點；若沒有節點則回傳 <see langword="null"/>。
    /// </summary>
    /// <param name="values">以 level-order 排列的完整二元樹節點值。</param>
    /// <returns>建立完成的完整二元樹根節點。</returns>
    private static TreeNode? BuildCompleteTree(int[] values)
    {
        if (values.Length == 0)
        {
            return null;
        }

        TreeNode[] nodes = new TreeNode[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            nodes[i] = new TreeNode(values[i]);
        }

        // Sample 使用完整二元樹的層序陣列，因此父子索引可直接套用 2*i+1 / 2*i+2 規則。
        for (int i = 0; i < values.Length; i++)
        {
            int leftChildIndex = (2 * i) + 1;
            int rightChildIndex = leftChildIndex + 1;

            if (leftChildIndex < values.Length)
            {
                nodes[i].Left = nodes[leftChildIndex];
            }

            if (rightChildIndex < values.Length)
            {
                nodes[i].Right = nodes[rightChildIndex];
            }
        }

        return nodes[0];
    }

    /// <summary>
    /// 將整數陣列格式化成 README 與主控台共用的 level-order 顯示文字。
    /// 輸入條件是 values 可為空陣列，但不應為 <see langword="null"/>。
    /// 輸出結果是形如 <c>[1, 2, 3]</c> 的固定格式字串。
    /// </summary>
    /// <param name="values">要顯示的整數陣列。</param>
    /// <returns>固定格式的陣列字串。</returns>
    private static string FormatArray(int[] values)
    {
        if (values.Length == 0)
        {
            return "[]";
        }

        return $"[{string.Join(", ", values)}]";
    }
}
