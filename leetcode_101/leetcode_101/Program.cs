namespace leetcode_101;

class Program
{
    /// <summary>
    /// 二元樹節點資料結構，保存節點值與左右子節點。
    /// 輸入條件是節點值符合題目限制，左右子節點可以為 null；輸出結果由呼叫端透過節點連結組成樹。
    /// </summary>
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        /// <summary>
        /// 建立二元樹節點，並可同時指定左右子節點。
        /// 輸入為節點值與可為 null 的左右子節點；輸出為可接入二元樹的 TreeNode 實例。
        /// </summary>
        /// <param name="val">節點數值。</param>
        /// <param name="left">左子節點，沒有左子節點時為 null。</param>
        /// <param name="right">右子節點，沒有右子節點時為 null。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 101. Symmetric Tree
    /// https://leetcode.com/problems/symmetric-tree/
    /// 101. 對稱二元樹
    /// https://leetcode.cn/problems/symmetric-tree/
    ///
    /// Problem Description:
    /// Given the root of a binary tree, check whether it is a mirror of itself (i.e., symmetric around its center).
    ///
    /// 題目描述：
    /// 給定一棵二元樹的根節點 root，檢查它是否為自身的鏡像（也就是是否以中心對稱）。
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        RunSolutionCases("Recursive DFS", solution.IsSymmetric);
        RunSolutionCases("Iterative Queue", solution.IsSymmetricIterative);

        Console.WriteLine("All examples passed.");
    }

    /// <summary>
    /// 使用同一批範例資料驗證指定解法，涵蓋空樹、單節點、對稱樹、結構不對稱與數值不對稱。
    /// 解題概念是讓主要進入點可以用一致輸入條件比較不同策略；輸入為解法名稱與判斷委派，
    /// 輸出為各案例的 PASS/FAIL 主控台訊息，若結果不符合預期則擲出例外。
    /// </summary>
    /// <param name="solutionName">要顯示在輸出中的解法名稱。</param>
    /// <param name="isSymmetric">要驗證的對稱樹判斷方法。</param>
    private static void RunSolutionCases(string solutionName, Func<TreeNode?, bool> isSymmetric)
    {
        RunCase($"{solutionName} - Empty tree", isSymmetric, null, true);
        RunCase($"{solutionName} - Single node", isSymmetric, new TreeNode(1), true);
        RunCase(
            $"{solutionName} - Example 1",
            isSymmetric,
            new TreeNode(
                1,
                new TreeNode(2, new TreeNode(3), new TreeNode(4)),
                new TreeNode(2, new TreeNode(4), new TreeNode(3))),
            true);
        RunCase(
            $"{solutionName} - Example 2",
            isSymmetric,
            new TreeNode(
                1,
                new TreeNode(2, null, new TreeNode(3)),
                new TreeNode(2, null, new TreeNode(3))),
            false);
        RunCase(
            $"{solutionName} - Value mismatch",
            isSymmetric,
            new TreeNode(
                1,
                new TreeNode(2, new TreeNode(3), new TreeNode(4)),
                new TreeNode(2, new TreeNode(3), new TreeNode(4))),
            false);
    }

    /// <summary>
    /// 執行單一範例案例，呼叫指定解法後比對實際結果與預期結果。
    /// 解題概念是把範例資料視為可重複執行的檢查；輸入為案例名稱、解法、樹根與預期布林值，
    /// 輸出為 PASS/FAIL 主控台訊息，失敗時會擲出例外避免錯誤結果被忽略。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="isSymmetric">要驗證的對稱樹判斷方法。</param>
    /// <param name="root">測試案例的二元樹根節點，可以為 null。</param>
    /// <param name="expected">預期是否為對稱二元樹。</param>
    private static void RunCase(string name, Func<TreeNode?, bool> isSymmetric, TreeNode? root, bool expected)
    {
        bool actual = isSymmetric(root);
        bool passed = actual == expected;

        Console.WriteLine($"{name}: {(passed ? "PASS" : "FAIL")} -> {actual}");

        if (!passed)
        {
            throw new InvalidOperationException($"{name} expected {expected}, got {actual}.");
        }
    }

    /// <summary>
    /// 方法一：遞迴 DFS 鏡像比較。
    /// 用途是判斷二元樹是否以根節點為中心左右鏡像；解題概念是每次比較一組應互為鏡像的節點，
    /// 左子樹的外側要對上右子樹的外側，左子樹的內側要對上右子樹的內側。
    /// 輸入可以為 null，空樹視為對稱；輸出為 true 表示對稱，false 表示任一鏡像位置結構或數值不同。
    /// </summary>
    /// <param name="root">二元樹根節點，可以為 null。</param>
    /// <returns>若二元樹為自身鏡像則回傳 true，否則回傳 false。</returns>
    public bool IsSymmetric(TreeNode? root)
    {
        if (root == null)
        {
            return true;
        }

        return IsSymmetricTree(root.left, root.right);
    }

    /// <summary>
    /// 遞迴判斷兩棵子樹是否互為鏡像。
    /// 用途是比較左右子樹的對稱位置；解題概念是先處理空節點與數值不一致的終止條件，
    /// 再遞迴比較外側 pair 與內側 pair。輸入左右節點都可以為 null；輸出為兩側是否完全鏡像。
    /// </summary>
    /// <param name="left">目前左側待比較節點，可以為 null。</param>
    /// <param name="right">目前右側待比較節點，可以為 null。</param>
    /// <returns>若兩個節點所在子樹互為鏡像則回傳 true，否則回傳 false。</returns>
    public bool IsSymmetricTree(TreeNode? left, TreeNode? right)
    {
        if (left == null && right == null)
        {
            return true;
        }

        if (left == null || right == null)
        {
            return false;
        }

        if (left.val != right.val)
        {
            return false;
        }

        // 鏡像位置要交叉比較：left.left 對 right.right，left.right 對 right.left。
        return IsSymmetricTree(left.left, right.right) && IsSymmetricTree(left.right, right.left);
    }

    /// <summary>
    /// 方法二：迭代 Queue 鏡像比較。
    /// 用途是用非遞迴方式判斷二元樹是否對稱；解題概念是把每一組應互為鏡像的節點成對放入佇列，
    /// 每次取出一組後檢查結構與數值，再加入下一層外側與內側鏡像 pair。
    /// 輸入可以為 null，空樹視為對稱；輸出為 true 表示所有鏡像 pair 都相符，false 表示任一 pair 不相符。
    /// </summary>
    /// <param name="root">二元樹根節點，可以為 null。</param>
    /// <returns>若二元樹為自身鏡像則回傳 true，否則回傳 false。</returns>
    public bool IsSymmetricIterative(TreeNode? root)
    {
        if (root == null)
        {
            return true;
        }

        Queue<(TreeNode? Left, TreeNode? Right)> queue = new();
        queue.Enqueue((root.left, root.right));

        while (queue.Count > 0)
        {
            (TreeNode? left, TreeNode? right) = queue.Dequeue();

            if (left == null && right == null)
            {
                continue;
            }

            if (left == null || right == null)
            {
                return false;
            }

            if (left.val != right.val)
            {
                return false;
            }

            // 佇列中永遠保存「應該互為鏡像」的節點 pair，下一層同樣採外側與內側交叉加入。
            queue.Enqueue((left.left, right.right));
            queue.Enqueue((left.right, right.left));
        }

        return true;
    }
}