namespace leetcode_783;

class Program
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        // left 與 right 宣告為可為 null（TreeNode?），允許葉節點傳入 null
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left!;
            this.right = right!;
        }
    }

    /// <summary>
    /// 783. Minimum Distance Between BST Nodes
    /// https://leetcode.com/problems/minimum-distance-between-bst-nodes/description/
    ///
    /// Problem description (English):
    /// Given the root of a Binary Search Tree (BST), return the minimum difference
    /// between the values of any two different nodes in the tree.
    ///
    /// 題目描述（繁體中文）：
    /// 給定一個二叉搜尋樹（BST）的根節點，返回樹中任意兩個不同節點
    /// 值之間的最小差距。
    ///
    /// 783. 二叉搜索树节点最小距离
    /// https://leetcode.cn/problems/minimum-distance-between-bst-nodes/description/
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // ====== 測試資料 ======
        // 範例 1：建立 BST [4, 2, 6, 1, 3]，期望輸出 1
        //        4
        //       / \
        //      2   6
        //     / \
        //    1   3
        TreeNode root1 = new TreeNode(4,
            new TreeNode(2, new TreeNode(1), new TreeNode(3)),
            new TreeNode(6));

        // 範例 2：建立 BST [1, 0, 48, null, null, 12, 49]，期望輸出 1
        //        1
        //       / \
        //      0   48
        //         / \
        //        12  49
        TreeNode root2 = new TreeNode(1,
            new TreeNode(0),
            new TreeNode(48, new TreeNode(12), new TreeNode(49)));

        Program p = new Program();

        // 解法一：DFS 朴素排序法
        Console.WriteLine($"[解法一] 範例1 MinDiffInBST  = {p.MinDiffInBST(root1)}");  // 期望: 1
        Console.WriteLine($"[解法一] 範例2 MinDiffInBST  = {p.MinDiffInBST(root2)}");  // 期望: 1

        // 解法二：BST 中序遍歷 + 前驅節點比較
        Console.WriteLine($"[解法二] 範例1 MinDiffInBST2 = {p.MinDiffInBST2(root1)}"); // 期望: 1
        Console.WriteLine($"[解法二] 範例2 MinDiffInBST2 = {p.MinDiffInBST2(root2)}"); // 期望: 1
    }

    /// <summary>
    /// 解法一：朴素 DFS + 排序
    ///
    /// 【解題概念】
    /// 不利用 BST 特性，只把所有節點的值收集進陣列後排序，
    /// 相鄰兩元素之差的最小值即為答案。
    ///
    /// 【解題步驟】
    /// 1. 用中序 DFS（dfs）將所有節點值依序放入 list（由於 BST 中序遍歷天然有序，
    ///    list 本身已是排好序的遞增序列）。
    /// 2. Array.Sort(list.ToArray()) 對「副本」排序，實際上 list 已有序，此行為冗餘
    ///    但不影響正確性，可刪除。保留以說明朴素思路。
    /// 3. 走訪排序後的相鄰元素，逐一計算差值並取最小值。
    ///
    /// 【時間複雜度】O(n log n)，空間複雜度 O(n)
    /// </summary>
    /// <param name="root">BST 根節點</param>
    /// <returns>任意兩節點值之最小差距</returns>
    public int MinDiffInBST(TreeNode root)
    {
        // 收集所有節點值（中序遍歷後為遞增有序）
        List<int> list = new List<int>();
        dfs(root, list);

        // 注意：Array.Sort 排序的是 list.ToArray() 回傳的副本，
        // 並不會更動 list 本身。由於 BST 中序遍歷已產生有序序列，
        // 此行實際上是多餘的，保留僅作示意說明。
        Array.Sort(list.ToArray());

        int n = list.Count;
        int res = int.MaxValue;

        // 比較相鄰節點的差值，取全局最小
        for(int i = 1; i < n; i++)
        {
            int cur = Math.Abs(list[i] - list[i - 1]);
            res = Math.Min(res, cur);
        }

        return res;
    }

    /// <summary>
    /// 中序遍歷（Inorder Traversal）輔助函式 — 用於解法一
    ///
    /// 【三種遍歷順序】
    ///   前序（Pre-order）：根 → 左 → 右
    ///   中序（In-order） ：左 → 根 → 右  ← 本函式採用
    ///   後序（Post-order）：左 → 右 → 根
    ///
    /// 【為什麼用中序？】
    /// BST 的中序遍歷結果天然是「遞增有序序列」，
    /// 可直接在不排序的情況下取得相鄰差值的最小值。
    ///
    /// 【遞迴流程】
    ///   1. 先遞迴拜訪左子樹（取得所有小於當前節點的值）
    ///   2. 將當前節點值加入 list
    ///   3. 再遞迴拜訪右子樹（取得所有大於當前節點的值）
    /// </summary>
    /// <param name="root">當前遞迴節點</param>
    /// <param name="list">收集節點值的陣列（輸出為遞增有序）</param>
    public void dfs(TreeNode root, List<int> list)
    {
        // 遞迴終止條件：節點為空則返回
        if(root is null)
        {
            return;
        }

        // 1. 先遞迴左子樹（左 < 根）
        if(root.left is not null)
        {
            dfs(root.left, list);
        }

        // 2. 記錄當前節點的值
        list.Add(root.val);

        // 3. 再遞迴右子樹（右 > 根）
        if(root.right is not null)
        {
            dfs(root.right, list);
        }
    }

    private int pre;
    private int res;

    /// <summary>
    /// 解法二：BST 中序遍歷 + 前驅節點即時比較（空間最佳化）
    ///
    /// 【解題概念】
    /// 利用 BST 中序遍歷產生遞增有序序列的特性：
    /// 最小差值必定出現在「相鄰兩節點」之間，因此不需要額外陣列，
    /// 只需用一個 pre 變數記錄上一個拜訪的節點值，邊遍歷邊更新答案。
    ///
    /// 【解題步驟】
    /// 1. 初始化 res = int.MaxValue（答案的上界），pre = -1（標記尚未拜訪任何節點）。
    /// 2. 呼叫 dfs2 進行中序遍歷。
    ///    - 拜訪到第一個節點時（pre == -1）：只記錄值到 pre，不計算差值。
    ///    - 拜訪到後續節點時（pre != -1）：計算 root.val - pre 並更新 res，再更新 pre。
    /// 3. 遍歷完成後回傳 res。
    ///
    /// 【時間複雜度】O(n)，空間複雜度 O(h)（h 為樹高，遞迴堆疊深度）
    /// </summary>
    /// <param name="root">BST 根節點</param>
    /// <returns>任意兩節點值之最小差距</returns>
    public int MinDiffInBST2(TreeNode root)
    {
        // 初始化答案為最大值，pre = -1 表示尚未拜訪任何節點
        res = int.MaxValue;
        pre = -1;
        dfs2(root);

        return res;
    }

    /// <summary>
    /// 解法二輔助函式：中序遍歷 + 前驅節點即時比較
    ///
    /// 【pre 變數說明】
    /// pre 初始值設為 -1 作為「尚未拜訪任何節點」的哨兵旗幟。
    /// BST 節點值均為非負整數，故 -1 不會與任何有效值衝突。
    ///
    /// 【判斷邏輯】
    ///   pre == -1：目前是中序遍歷的第一個節點，無前驅可比較，
    ///              直接將 pre 設為當前節點值。
    ///   pre != -1：已有前驅節點，計算 root.val - pre（因中序遍歷保證遞增，
    ///              差值必為正），更新全局最小 res，並推進 pre。
    ///
    /// 【Bug 修正說明】
    /// 原程式碼條件寫反（if pre != -1 執行單純賦值），
    /// 正確邏輯應為 pre == -1 時初始化、pre != -1 時才計算差值。
    /// </summary>
    /// <param name="root">當前遞迴節點</param>
    public void dfs2(TreeNode root)
    {
        // 遞迴終止條件：節點為空則返回
        if(root is null)
        {
            return;
        }

        // 1. 先遞迴左子樹（值較小的節點）
        dfs2(root.left);

        // 2. 處理當前節點
        if(pre == -1)
        {
            // 中序遍歷的第一個節點：無前驅，僅記錄值
            pre = root.val;
        }
        else
        {
            // 後續節點：計算與前驅的差值並更新答案
            // 由於 BST 中序遞增，root.val > pre，差值恆為正
            res = Math.Min(res, root.val - pre);
            pre = root.val; // 推進前驅指標到當前節點
        }

        // 3. 再遞迴右子樹（值較大的節點）
        dfs2(root.right);
    }
}
