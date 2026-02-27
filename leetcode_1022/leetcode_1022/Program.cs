namespace leetcode_1022;

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
    /// 1022. Sum of Root To Leaf Binary Numbers
    /// https://leetcode.com/problems/sum-of-root-to-leaf-binary-numbers/description/?envType=daily-question&envId=2026-02-24
    /// 1022. 從根到葉的二進位數之和
    /// https://leetcode.cn/problems/sum-of-root-to-leaf-binary-numbers/description/?envType=daily-question&envId=2026-02-24
    ///
    /// 題目描述：
    /// 給定一個二元樹的根節點，每個節點的值為 0 或 1。
    /// 每條從根到葉的路徑都代表一個以該路徑的節點值構成的二進位數，
    /// 其中根節點對應最高位元。例如，路徑 0 -> 1 -> 1 -> 0 -> 1 對應的二進位數為 01101，
    /// 即 13。
    /// 對於所有葉節點，計算從根節點到該葉節點形成的二進位數之和，並將該總和作為回傳值。
    /// 測試案例保證答案適合儲存在 32 位元整數中。
    ///
    /// Problem statement:
    /// You are given the root of a binary tree where each node has a value 0 or 1.
    /// Each root-to-leaf path represents a binary number starting with the most significant bit.
    /// For example, if the path is 0 -> 1 -> 1 -> 0 -> 1, then this could represent 01101 in binary, which is 13.
    /// For all leaves in the tree, consider the numbers represented by the path from the root to that leaf.
    /// Return the sum of these numbers.
    ///
    /// The test cases are generated so that the answer fits in a 32-bits integer.
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 範例 1：
        //       1
        //      / \
        //     0   1
        //    / \ / \
        //   0  1 0  1
        // 路徑：100(4) + 101(5) + 110(6) + 111(7) = 22
        var root1 = new TreeNode(1,
            new TreeNode(0,
                new TreeNode(0),
                new TreeNode(1)),
            new TreeNode(1,
                new TreeNode(0),
                new TreeNode(1)));
        Console.WriteLine($"範例 1 結果: {solution.SumRootToLeaf(root1)}"); // 預期輸出: 22

        // 範例 2：
        //   0
        // 路徑：0(0) = 0
        solution = new Program(); // 重置 sum
        var root2 = new TreeNode(0);
        Console.WriteLine($"範例 2 結果: {solution.SumRootToLeaf(root2)}"); // 預期輸出: 0

        // 範例 3：（路徑 1→0→1→1 = 1011 = 11）
        //   1
        //  /
        // 0
        //  \
        //   1
        //    \
        //     1
        solution = new Program(); // 重置 sum
        var root3 = new TreeNode(1,
            new TreeNode(0,
                null,
                new TreeNode(1,
                    null,
                    new TreeNode(1))),
            null);
        Console.WriteLine($"範例 3 結果: {solution.SumRootToLeaf(root3)}"); // 預期輸出: 11
    }

    private int sum = 0;

    /// <summary>
    /// 計算二元樹中所有根到葉路徑所代表的二進位數之總和。
    ///
    /// 解題概念：
    /// 將每條根到葉的路徑視為一個二進位數，其中根節點對應最高位元。
    /// 利用 DFS 深度優先搜尋，在遞迴過程中同步累積當前路徑形成的數值。
    /// 每往下一層，就執行 num = num << 1 | node.val（等同於十進制的 num * 2 + digit），
    /// 抵達葉節點時，將累積的數值加入總和。
    ///
    /// 時間複雜度：O(N)，N 為節點數，每個節點恰好訪問一次。
    /// 空間複雜度：O(H)，H 為樹的高度，遞迴呼叫堆疊深度。
    /// </summary>
    /// <param name="root">二元樹的根節點，每個節點的值為 0 或 1。</param>
    /// <returns>所有根到葉路徑所代表的二進位數之總和。</returns>
    /// <example>
    /// <code>
    ///       1
    ///      / \
    ///     0   1
    ///    / \ / \
    ///   0  1 0  1
    /// 路徑：100(4) + 101(5) + 110(6) + 111(7) = 22
    /// var solution = new Program();
    /// int result = solution.SumRootToLeaf(root); // 回傳 22
    /// </code>
    /// </example>
    public int SumRootToLeaf(TreeNode root)
    {
        DFS(root, 0);
        return sum;
    }

    /// <summary>
    /// DFS 深度優先搜尋：自根節點向下遞迴，累積當前路徑所對應的二進位數值。
    ///
    /// 解題說明：
    /// 以十進制類比理解：把路徑 1→2→3 轉成十進制 123，過程為
    ///   0 ×10+1→ 1 ×10+2→ 12 ×10+3→ 123
    /// 二進制做法相同，把路徑 1→0→1→1 轉成二進制 1011，過程為
    ///   0 ×2+1→ 1 ×2+0→ 10 ×2+1→ 101 ×2+1→ 1011
    /// 其中 ×2 等價於左移一位（<< 1），+ node.val 可寫成 OR 運算（| node.val）。
    ///
    /// 每訪問一個新節點時，更新 num = num << 1 | node.val；
    /// 若抵達葉節點（左右子節點皆為 null），將 num 累加至 sum。
    /// </summary>
    /// <param name="node">當前訪問的樹節點。</param>
    /// <param name="num">從根到當前節點的路徑所累積的二進位數值。</param>
    private void DFS(TreeNode? node, int num)
    {
        // 遇到空節點直接返回，不做任何處理
        if(node is null)
        {
            return;
        }

        // 將當前節點的值追加到路徑數值的最低位元
        // num << 1 等同於 num * 2（左移一位，為新的位元騰出空間）
        // | node.val 等同於 + node.val（填入當前位元的值 0 或 1）
        num = num << 1 | node.val;

        // 抵達葉節點：左右子節點都為 null，表示路徑結束
        // 將此條路徑形成的完整二進位數累加到總和
        if(node.left is null && node.right is null)
        {
            sum += num;
            return;
        }

        // 遞迴搜尋左子樹與右子樹
        DFS(node.left, num);
        DFS(node.right, num);
    }
}
