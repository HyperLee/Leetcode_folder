namespace leetcode_938;

class Program
{
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 938. Range Sum of BST
    /// https://leetcode.com/problems/range-sum-of-bst/
    /// 938. 二叉搜索树的范围和
    /// https://leetcode.cn/problems/range-sum-of-bst/description/
    /// 
    /// Given the root node of a binary search tree and two integers low and high, 
    /// return the sum of values of all nodes with a value in the inclusive range [low, high].
    /// 給定一個二元搜尋樹的根節點，以及兩個整數 low 和 high，
    /// 回傳所有節點值位於包含區間 [low, high] 內的總和。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program p = new Program();

        // 測試範例 1
        // 樹結構：[10, 5, 15, 3, 7, null, 18]，low = 7，high = 15
        // 符合範圍的節點：7、10、15，總和 = 32
        TreeNode root1 = new TreeNode(10,
            new TreeNode(5,
                new TreeNode(3),
                new TreeNode(7)),
            new TreeNode(15,
                null,
                new TreeNode(18)));
        Console.WriteLine($"範例 1 結果：{p.RangeSumBST(root1, 7, 15)}"); // 預期：32

        // 測試範例 2
        // 樹結構：[10, 5, 15, 3, 7, 13, 18, 1, null, 6]，low = 6，high = 10
        // 符合範圍的節點：6、7、10，總和 = 23
        TreeNode root2 = new TreeNode(10,
            new TreeNode(5,
                new TreeNode(3,
                    new TreeNode(1),
                    null),
                new TreeNode(7,
                    new TreeNode(6),
                    null)),
            new TreeNode(15,
                new TreeNode(13),
                new TreeNode(18)));
        Console.WriteLine($"範例 2 結果：{p.RangeSumBST(root2, 6, 10)}"); // 預期：23
    }

    /// <summary>
    /// 方法：深度優先搜尋（DFS）
    /// <para>
    /// 利用二元搜尋樹（BST）的性質進行剪枝，避免不必要的子樹遍歷：
    /// <list type="bullet">
    ///   <item>若 root 為 null，回傳 0。</item>
    ///   <item>若 root.val &gt; high，右子樹所有值均大於 high，只需遞迴左子樹。</item>
    ///   <item>若 root.val &lt; low，左子樹所有值均小於 low，只需遞迴右子樹。</item>
    ///   <item>否則 root.val 在 [low, high] 之間，回傳 root.val 加上左右子樹的範圍和。</item>
    /// </list>
    /// </para>
    /// <example>
    /// <code>
    /// // 樹：[10,5,15,3,7,null,18]，low=7，high=15 → 32
    /// int result = RangeSumBST(root, 7, 15);
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="root">二元搜尋樹的根節點</param>
    /// <param name="low">範圍下界（含）</param>
    /// <param name="high">範圍上界（含）</param>
    /// <returns>所有節點值位於 [low, high] 的總和</returns>
    public int RangeSumBST(TreeNode? root, int low, int high)
    {
        if(root == null)
        {
            return 0;
        }

        // 二元樹右子樹節點值大於左子樹, 所以走左邊就不會有大於 high 的值了
        if(root.val > high)
        {
            return RangeSumBST(root.left, low, high);
        }

        // 二元樹右子樹節點值大於左子樹, 所以走右邊就不會有小於 low 的值了
        if(root.val < low)
        {
            return RangeSumBST(root.right, low, high);
        }

        // root.val 在 [low, high] 之間, 就把 root.val 加上左右子樹的結果
        return root.val + RangeSumBST(root.left, low, high) + RangeSumBST(root.right, low, high);
    }
}
