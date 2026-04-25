namespace leetcode_1026;

class Program
{
    /// <summary>
    /// LeetCode 題目提供的二元樹節點模型。
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 節點數值。
        /// </summary>
        public int val;

        /// <summary>
        /// 左子節點；沒有左子樹時為 <see langword="null"/>。
        /// </summary>
        public TreeNode? left;

        /// <summary>
        /// 右子節點；沒有右子樹時為 <see langword="null"/>。
        /// </summary>
        public TreeNode? right;

        /// <summary>
        /// 建立一個二元樹節點。
        /// </summary>
        /// <param name="val">節點數值。</param>
        /// <param name="left">左子節點。</param>
        /// <param name="right">右子節點。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 建立 LeetCode 1026 的範例二元樹，執行題解，並輸出實際結果與預期答案。
    /// </summary>
    /// <param name="args">命令列參數，本範例未使用。</param>
    static void Main(string[] args)
    {
        Program solution = new();

        TreeNode exampleOne = new(
            8,
            new TreeNode(
                3,
                new TreeNode(1),
                new TreeNode(6, new TreeNode(4), new TreeNode(7))),
            new TreeNode(
                10,
                null,
                new TreeNode(14, new TreeNode(13))));

        TreeNode exampleTwo = new(
            1,
            null,
            new TreeNode(
                2,
                null,
                new TreeNode(0, new TreeNode(3))));

        PrintCase("Example 1", solution.MaxAncestorDiff(exampleOne), 7);
        PrintCase("Example 2", solution.MaxAncestorDiff(exampleTwo), 3);
    }

    /// <summary>
    /// 輸出單一測試案例的實際答案與預期答案，方便從 console 快速驗證。
    /// </summary>
    /// <param name="name">測試案例名稱。</param>
    /// <param name="actual">演算法計算出的答案。</param>
    /// <param name="expected">題目範例的預期答案。</param>
    private static void PrintCase(string name, int actual, int expected)
    {
        Console.WriteLine($"{name}: actual = {actual}, expected = {expected}");
    }

    /// <summary>
    /// 1026. Maximum Difference Between Node and Ancestor。
    /// 給定一棵二元樹，找出任一祖先節點與其子孫節點之間的最大絕對差值。
    /// 解題重點是：對目前節點來說，不必比較路徑上的所有祖先，只需要比較祖先中的最小值與最大值。
    /// </summary>
    /// <param name="root">二元樹根節點；題目保證至少存在一個節點。</param>
    /// <returns>所有祖先與子孫節點值的最大絕對差。</returns>
    /// <exception cref="ArgumentNullException">當 <paramref name="root"/> 為 <see langword="null"/> 時擲出。</exception>
    public int MaxAncestorDiff(TreeNode root)
    {
        ArgumentNullException.ThrowIfNull(root);

        return DFS(root, root.val, root.val);
    }

    /// <summary>
    /// 深度優先搜尋目前子樹，沿路保存祖先節點中的最小值與最大值。
    /// 若目前節點值為 x，最大差值只可能來自 |x - ancestorMin| 或 |x - ancestorMax|，
    /// 因為任一其他祖先 y 都必定落在 ancestorMin 與 ancestorMax 之間。
    /// </summary>
    /// <param name="root">目前搜尋到的節點；走到空子樹時為 <see langword="null"/>。</param>
    /// <param name="ancestorMin">從根節點到目前節點路徑上的最小祖先值。</param>
    /// <param name="ancestorMax">從根節點到目前節點路徑上的最大祖先值。</param>
    /// <returns>目前子樹中能取得的最大祖先差值。</returns>
    public int DFS(TreeNode? root, int ancestorMin, int ancestorMax)
    {
        if (root is null)
        {
            return 0;
        }

        // 目前節點與路徑上最小/最大祖先相比，即可涵蓋所有祖先差值的最大可能。
        int currentDiff = Math.Max(Math.Abs(root.val - ancestorMin), Math.Abs(root.val - ancestorMax));

        // 將目前節點納入下一層子樹的祖先範圍。
        int nextMin = Math.Min(ancestorMin, root.val);
        int nextMax = Math.Max(ancestorMax, root.val);

        int leftDiff = DFS(root.left, nextMin, nextMax);
        int rightDiff = DFS(root.right, nextMin, nextMax);

        return Math.Max(currentDiff, Math.Max(leftDiff, rightDiff));
    }
}
