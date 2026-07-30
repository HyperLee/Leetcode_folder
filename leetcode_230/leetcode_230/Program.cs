namespace leetcode_230;

class Program
{
    /// <summary>
    /// 表示二元搜尋樹中的單一節點。
    /// 輸入為節點值及可省略的左右子節點，建立後提供解法讀取樹結構；
    /// 左右子節點可為 null，表示該方向沒有子樹。
    /// </summary>
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        /// <summary>
        /// 建立二元樹節點，並設定節點值及可為 null 的左右子節點。
        /// </summary>
        /// <param name="val">目前節點儲存的整數值。</param>
        /// <param name="left">左子節點；沒有左子樹時為 null。</param>
        /// <param name="right">右子節點；沒有右子樹時為 null。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// LeetCode 230. Kth Smallest Element in a BST
    /// 題目描述：
    /// 給定一個二元搜尋樹（BST）的根節點 root 和一個整數 k，請你設計一個演算法，找出 BST 中第 k 小的元素。
    /// k 從 1 開始計算。
    /// 你必須在 O(h) 的時間複雜度內完成，h 是樹的高度。
    /// 題目連結：
    /// https://leetcode.com/problems/kth-smallest-element-in-a-bst/description/
    /// https://leetcode.cn/problems/kth-smallest-element-in-a-bst/description/
    /// </summary>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行七筆固定案例，分別驗證遞迴與疊代解法的實際結果。
    /// 輸入涵蓋官方範例、單節點、偏斜樹，以及空樹與無效 k 的防禦性行為；
    /// 輸出會列出每一種解法的預期值、實際值、通過狀態與總通過數。
    /// </summary>
    private static void RunSamples()
    {
        SampleCase[] samples =
        {
            new SampleCase(
                "LeetCode 範例 1：查找最小值",
                new int?[] { 3, 1, 4, null, 2 },
                1,
                1),
            new SampleCase(
                "LeetCode 範例 2：查找第三小值",
                new int?[] { 5, 3, 6, 2, 4, null, null, 1 },
                3,
                3),
            new SampleCase(
                "單一節點：節點值為限制下界",
                new int?[] { 0 },
                1,
                0),
            new SampleCase(
                "右偏斜樹：查找最後順位",
                new int?[] { 1, null, 2, null, 3, null, 4 },
                4,
                4),
            new SampleCase(
                "空樹：沒有可回傳的節點",
                Array.Empty<int?>(),
                1,
                -1),
            new SampleCase(
                "無效順位：k 為 0",
                new int?[] { 2, 1, 3 },
                0,
                -1),
            new SampleCase(
                "無效順位：k 大於節點數",
                new int?[] { 2, 1, 3 },
                4,
                -1),
        };

        Program solution = new Program();
        int passedChecks = 0;
        int totalChecks = samples.Length * 2;

        Console.WriteLine("Kth Smallest Element in a BST sample verification");
        Console.WriteLine();

        for (int index = 0; index < samples.Length; index++)
        {
            SampleCase sample = samples[index];
            int recursiveActual = solution.KthSmallest(BuildTree(sample.Values), sample.K);
            int iterativeActual = solution.KthSmallestIterative(BuildTree(sample.Values), sample.K);
            bool recursivePassed = recursiveActual == sample.Expected;
            bool iterativePassed = iterativeActual == sample.Expected;

            if (recursivePassed)
            {
                passedChecks++;
            }

            if (iterativePassed)
            {
                passedChecks++;
            }

            Console.WriteLine($"Case {index + 1}: {sample.Name}");
            Console.WriteLine($"Input: root = {FormatTree(sample.Values)}, k = {sample.K}");
            Console.WriteLine($"Expected: {sample.Expected}");
            Console.WriteLine($"Recursive Actual: {recursiveActual} ({(recursivePassed ? "PASS" : "FAIL")})");
            Console.WriteLine($"Iterative Actual: {iterativeActual} ({(iterativePassed ? "PASS" : "FAIL")})");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
    }

    /// <summary>
    /// 將題目使用的層序陣列轉成二元樹；陣列中的 null 表示該位置沒有節點。
    /// 輸入為空或根節點為 null 時回傳 null，否則回傳新建立的樹根節點。
    /// </summary>
    /// <param name="values">以層序排列的可為 null 節點值。</param>
    /// <returns>依輸入建立的樹根節點；空樹則為 null。</returns>
    private static TreeNode? BuildTree(int?[] values)
    {
        if (values.Length == 0 || values[0] is not int rootValue)
        {
            return null;
        }

        TreeNode root = new TreeNode(rootValue);
        Queue<TreeNode> parents = new Queue<TreeNode>();
        parents.Enqueue(root);
        int valueIndex = 1;

        while (parents.Count > 0 && valueIndex < values.Length)
        {
            TreeNode parent = parents.Dequeue();

            if (values[valueIndex] is int leftValue)
            {
                parent.left = new TreeNode(leftValue);
                parents.Enqueue(parent.left);
            }

            valueIndex++;

            if (valueIndex < values.Length && values[valueIndex] is int rightValue)
            {
                parent.right = new TreeNode(rightValue);
                parents.Enqueue(parent.right);
            }

            valueIndex++;
        }

        return root;
    }

    /// <summary>
    /// 將層序節點值格式化成 README 與執行結果共用的顯示形式。
    /// 輸入為可為 null 的節點值陣列，輸出例如 [3, 1, 4, null, 2] 的字串。
    /// </summary>
    /// <param name="values">要顯示的層序節點值。</param>
    /// <returns>以方括號包住並以逗號分隔的樹表示字串。</returns>
    private static string FormatTree(IEnumerable<int?> values)
    {
        return $"[{string.Join(", ", values.Select(value => value?.ToString() ?? "null"))}]";
    }

    /// <summary>
    /// 以遞迴中序遍歷尋找二元搜尋樹中第 k 小的元素。
    /// 解題概念是利用 BST 的中序遍歷會依遞增順序造訪節點，並以單次呼叫的剩餘順位計數；
    /// 輸入應為二元搜尋樹且 k 從 1 開始，找到時回傳節點值，空樹或順位無效時回傳 -1。
    /// </summary>
    /// <param name="root">二元搜尋樹的根節點；空樹可為 null。</param>
    /// <param name="k">從 1 開始計算的目標順位。</param>
    /// <returns>第 k 小的節點值；無法取得該順位時回傳 -1。</returns>
    public int KthSmallest(TreeNode? root, int k)
    {
        if (root is null || k <= 0)
        {
            return -1;
        }

        int remaining = k;
        return TryFindKthSmallest(root, ref remaining, out int result) ? result : -1;
    }

    /// <summary>
    /// 遞迴執行左、根、右的中序遍歷，並嘗試找出剩餘順位所指向的節點。
    /// 輸入包含目前節點與共用的剩餘順位；找到答案時透過 result 輸出節點值並回傳 true，
    /// 子樹內沒有目標順位時將 result 設為 -1 並回傳 false。
    /// </summary>
    /// <param name="node">目前要遍歷的節點；空子樹可為 null。</param>
    /// <param name="remaining">尚未跳過的節點順位，依中序造訪順序遞減。</param>
    /// <param name="result">找到時的節點值；未找到時為 -1。</param>
    /// <returns>目前子樹是否包含目標順位。</returns>
    private static bool TryFindKthSmallest(TreeNode? node, ref int remaining, out int result)
    {
        if (node is null)
        {
            result = -1;
            return false;
        }

        if (TryFindKthSmallest(node.left, ref remaining, out result))
        {
            return true;
        }

        // 左子樹全部處理完後才計算目前節點，順位因此與遞增順序一致。
        if (--remaining == 0)
        {
            result = node.val;
            return true;
        }

        // 右子樹找到答案時會一路回傳 true，避免繼續造訪不必要的節點。
        return TryFindKthSmallest(node.right, ref remaining, out result);
    }

    /// <summary>
    /// 以顯式堆疊模擬中序遍歷，尋找二元搜尋樹中第 k 小的元素。
    /// 解題概念是先把左側路徑壓入堆疊，再依序彈出節點並轉往右子樹；
    /// 輸入應為二元搜尋樹且 k 從 1 開始，找到時回傳節點值，空樹或順位無效時回傳 -1。
    /// </summary>
    /// <param name="root">二元搜尋樹的根節點；空樹可為 null。</param>
    /// <param name="k">從 1 開始計算的目標順位。</param>
    /// <returns>第 k 小的節點值；無法取得該順位時回傳 -1。</returns>
    public int KthSmallestIterative(TreeNode? root, int k)
    {
        if (root is null || k <= 0)
        {
            return -1;
        }

        Stack<TreeNode> nodes = new Stack<TreeNode>();
        TreeNode? current = root;

        while (current is not null || nodes.Count > 0)
        {
            // 壓入整條左側路徑，讓堆疊頂端永遠是下一個應造訪的較小節點。
            while (current is not null)
            {
                nodes.Push(current);
                current = current.left;
            }

            // 彈出節點等同於遞迴從左子樹回溯，再依中序順位計數。
            current = nodes.Pop();

            if (--k == 0)
            {
                return current.val;
            }

            current = current.right;
        }

        return -1;
    }

    /// <summary>
    /// 表示一筆可執行範例，包含案例名稱、層序樹資料、欲查找順位與預期結果。
    /// 輸入樹可為空，輸出預期值可使用 -1 表示專案定義的防禦性失敗結果。
    /// </summary>
    /// <param name="Name">案例名稱與測試重點。</param>
    /// <param name="Values">以層序排列的可為 null 節點值。</param>
    /// <param name="K">從 1 開始計算的順位；防禦性案例可傳入無效值。</param>
    /// <param name="Expected">兩種解法都應回傳的預期結果。</param>
    private sealed record SampleCase(string Name, int?[] Values, int K, int Expected);
}