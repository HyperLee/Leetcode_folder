namespace leetcode_111;

/// <summary>
/// 提供 LeetCode 111「Minimum Depth of Binary Tree」的可執行範例與解法實作。
/// </summary>
public class Program
{
    /// <summary>
    /// 表示二元樹節點，包含節點值與左右子節點；輸入可用 null 表示缺少子樹。
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 節點儲存的整數值。
        /// </summary>
        public int val;

        /// <summary>
        /// 左子節點；為 null 時代表沒有左子樹。
        /// </summary>
        public TreeNode? left;

        /// <summary>
        /// 右子節點；為 null 時代表沒有右子樹。
        /// </summary>
        public TreeNode? right;

        /// <summary>
        /// 建立二元樹節點，並可同時指定左右子節點；輸入的左右子節點可為 null，輸出為初始化完成的節點。
        /// </summary>
        /// <param name="val">節點值。</param>
        /// <param name="left">左子節點；可為 null。</param>
        /// <param name="right">右子節點；可為 null。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 111. Minimum Depth of Binary Tree
    /// https://leetcode.com/problems/minimum-depth-of-binary-tree/description/
    ///
    /// English:
    /// Given a binary tree, find its minimum depth.
    ///
    /// The minimum depth is the number of nodes along the shortest path from the root node down to the nearest leaf node.
    ///
    /// Note: A leaf is a node with no children.
    ///
    /// 繁體中文:
    /// 給定一棵二元樹，找出它的最小深度。
    ///
    /// 最小深度是從根節點往下到最近的葉節點，最短路徑上所經過的節點數。
    ///
    /// 注意：葉節點是指沒有子節點的節點。
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("LeetCode 111 - Minimum Depth of Binary Tree");
        Console.WriteLine();

        RunExample("空樹", Array.Empty<int?>(), 0);
        RunExample("單一節點", new int?[] { 1 }, 1);
        RunExample("範例一", new int?[] { 3, 9, 20, null, null, 15, 7 }, 2);
        RunExample("範例二", new int?[] { 2, null, 3, null, 4, null, 5, null, 6 }, 5);
    }

    /// <summary>
    /// 使用遞迴深度優先搜尋計算二元樹最小深度；輸入可為 null，輸出為根節點到最近葉節點的節點數，空樹回傳 0。
    /// </summary>
    /// <param name="root">二元樹根節點；可為 null，代表空樹。</param>
    /// <returns>最小深度；空樹為 0，單一節點為 1。</returns>
    public int MinDepth(TreeNode? root)
    {
        if (root == null)
        {
            return 0;
        }

        if (root.left == null && root.right == null)
        {
            return 1;
        }

        int minDepth = int.MaxValue;

        // null 子節點不是葉節點，只有存在的子樹能參與最小深度比較。
        if (root.left != null)
        {
            minDepth = Math.Min(MinDepth(root.left), minDepth);
        }

        if (root.right != null)
        {
            minDepth = Math.Min(MinDepth(root.right), minDepth);
        }

        return minDepth + 1;
    }

    private int _bestDepth = int.MaxValue;

    /// <summary>
    /// 使用深度優先搜尋搭配最佳深度剪枝計算二元樹最小深度；輸入可為 null，輸出為根節點到最近葉節點的節點數。
    /// </summary>
    /// <param name="root">二元樹根節點；可為 null，代表空樹。</param>
    /// <returns>最小深度；空樹為 0，找到更短葉節點路徑後會剪掉不可能更短的搜尋分支。</returns>
    public int MinDepth2(TreeNode? root)
    {
        if (root == null)
        {
            return 0;
        }

        _bestDepth = int.MaxValue;
        Dfs(root, 0);
        return _bestDepth;
    }

    /// <summary>
    /// 遞迴走訪二元樹並更新目前找到的最佳最小深度；輸入節點可為 null，輸出透過欄位記錄最佳答案。
    /// </summary>
    /// <param name="node">目前走訪的節點；可為 null。</param>
    /// <param name="depth">到父節點為止的深度。</param>
    private void Dfs(TreeNode? node, int depth)
    {
        if (node == null)
        {
            return;
        }

        int currentDepth = depth + 1;

        // 若目前深度已不小於最佳答案，繼續往下只會更深，無法產生更短路徑。
        if (currentDepth >= _bestDepth)
        {
            return;
        }

        if (node.left == null && node.right == null)
        {
            _bestDepth = currentDepth;
            return;
        }

        Dfs(node.left, currentDepth);
        Dfs(node.right, currentDepth);
    }

    /// <summary>
    /// 執行一組層序輸入範例，輸出兩種解法的結果、預期值與是否通過。
    /// </summary>
    /// <param name="name">範例名稱。</param>
    /// <param name="levelOrderValues">以層序陣列表示的二元樹；null 代表缺少節點。</param>
    /// <param name="expected">預期最小深度。</param>
    private static void RunExample(string name, int?[] levelOrderValues, int expected)
    {
        var solution = new Program();
        TreeNode? root = BuildTreeFromLevelOrder(levelOrderValues);
        int minDepth = solution.MinDepth(root);
        int minDepth2 = solution.MinDepth2(root);

        Console.WriteLine($"{name} root = {FormatLevelOrder(levelOrderValues)}");
        Console.WriteLine($"  MinDepth  => {minDepth} (expected {expected}) {FormatStatus(minDepth == expected)}");
        Console.WriteLine($"  MinDepth2 => {minDepth2} (expected {expected}) {FormatStatus(minDepth2 == expected)}");
    }

    /// <summary>
    /// 將 LeetCode 常見的層序陣列輸入轉為二元樹；輸入空陣列或首節點為 null 時輸出 null。
    /// </summary>
    /// <param name="values">層序節點值；null 代表該位置沒有節點。</param>
    /// <returns>建構完成的二元樹根節點；空樹回傳 null。</returns>
    private static TreeNode? BuildTreeFromLevelOrder(int?[] values)
    {
        int? rootValue = values.Length == 0 ? null : values[0];

        if (!rootValue.HasValue)
        {
            return null;
        }

        var root = new TreeNode(rootValue.Value);
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        int index = 1;

        while (queue.Count > 0 && index < values.Length)
        {
            TreeNode current = queue.Dequeue();

            int? leftValue = index < values.Length ? values[index] : null;

            if (leftValue.HasValue)
            {
                var leftNode = new TreeNode(leftValue.Value);
                current.left = leftNode;
                queue.Enqueue(leftNode);
            }

            index++;

            int? rightValue = index < values.Length ? values[index] : null;

            if (rightValue.HasValue)
            {
                var rightNode = new TreeNode(rightValue.Value);
                current.right = rightNode;
                queue.Enqueue(rightNode);
            }

            index++;
        }

        return root;
    }

    /// <summary>
    /// 將層序陣列格式化為 README 與 console 範例使用的文字；輸入空陣列時輸出 []。
    /// </summary>
    /// <param name="values">層序節點值。</param>
    /// <returns>格式化後的層序陣列文字。</returns>
    private static string FormatLevelOrder(int?[] values)
    {
        return $"[{string.Join(", ", values.Select(value => value?.ToString() ?? "null"))}]";
    }

    /// <summary>
    /// 將布林驗證結果轉為 console 顯示狀態；輸入 true 輸出 PASS，false 輸出 FAIL。
    /// </summary>
    /// <param name="passed">實際結果是否符合預期。</param>
    /// <returns>PASS 或 FAIL。</returns>
    private static string FormatStatus(bool passed)
    {
        return passed ? "PASS" : "FAIL";
    }
}
