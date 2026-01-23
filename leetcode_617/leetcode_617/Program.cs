namespace leetcode_617;

class Program
{

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 617. Merge Two Binary Trees
    /// https://leetcode.com/problems/merge-two-binary-trees/description/
    /// 617. 合并二叉树
    /// https://leetcode.cn/problems/merge-two-binary-trees/description/
    ///
    /// English:
    /// You are given two binary trees root1 and root2.
    /// Imagine that when you put one of them to cover the other, some nodes of the two trees
    /// are overlapped while the others are not. You need to merge the two trees into a new
    /// binary tree. The merge rule is that if two nodes overlap, then sum node values up as
    /// the new value of the merged node. Otherwise, the NOT null node will be used as the
    /// node of the new tree.
    ///
    /// Note: The merging process must start from the root nodes of both trees.
    ///
    /// 繁體中文 (說明)：
    /// 給定兩個二元樹 root1 與 root2。
    /// 假設把其中一棵覆蓋在另一棵之上，部分節點會重疊，其他則不會。請將兩棵樹合併為一棵新的二元樹。
    /// 合併規則：當兩節點重疊時，合併節點的值為兩節點值的總和；否則使用非 null 的節點作為合併後的節點。
    ///
    /// 注意：合併過程需從兩棵樹的根節點開始。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("617. Merge Two Binary Trees");
        Console.WriteLine("==============================\n");

        var program = new Program();

        // 範例 1: LeetCode 官方範例
        // Tree 1:      1         Tree 2:      2
        //             / \                    / \
        //            3   2                  1   3
        //           /                        \   \
        //          5                          4   7
        //
        // 合併後:       3
        //             / \
        //            4   5
        //           / \   \
        //          5   4   7

        Console.WriteLine("範例 1:");
        TreeNode root1 = new TreeNode(1,
            new TreeNode(3, new TreeNode(5), null),
            new TreeNode(2));

        TreeNode root2 = new TreeNode(2,
            new TreeNode(1, null, new TreeNode(4)),
            new TreeNode(3, null, new TreeNode(7)));

        TreeNode merged1 = program.MergeTrees(root1, root2);
        Console.WriteLine($"合併後根節點值: {merged1.val}");  // 預期: 3
        Console.WriteLine($"合併後左子節點值: {merged1.left.val}");  // 預期: 4
        Console.WriteLine($"合併後右子節點值: {merged1.right.val}");  // 預期: 5
        Console.WriteLine();

        // 範例 2: 其中一棵樹為空
        Console.WriteLine("範例 2: (其中一棵樹為空)");
        TreeNode root3 = new TreeNode(1);
        TreeNode root4 = null;

        TreeNode merged2 = program.MergeTrees(root3, root4);
        Console.WriteLine($"合併後根節點值: {merged2.val}");  // 預期: 1
        Console.WriteLine();

        // 範例 3: 兩棵樹都為空
        Console.WriteLine("範例 3: (兩棵樹都為空)");
        TreeNode merged3 = program.MergeTrees(null, null);
        Console.WriteLine($"合併後結果: {(merged3 is null ? "null" : merged3.val.ToString())}");  // 預期: null
    }

    /// <summary>
    /// 合併兩棵二元樹。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 採用遞迴 (Recursion) 的方式，同時遍歷兩棵樹的對應節點：
    /// <list type="number">
    ///     <item>若兩個節點都為 null，回傳 null</item>
    ///     <item>若其中一個節點為 null，回傳另一個非 null 的節點</item>
    ///     <item>若兩個節點都存在，建立新節點，值為兩節點值的總和</item>
    ///     <item>遞迴處理左右子樹</item>
    /// </list>
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b> O(min(m, n))，其中 m 和 n 分別為兩棵樹的節點數</para>
    /// <para><b>空間複雜度：</b> O(min(h1, h2))，其中 h1 和 h2 分別為兩棵樹的高度（遞迴呼叫堆疊）</para>
    /// </summary>
    /// <param name="root1">第一棵二元樹的根節點</param>
    /// <param name="root2">第二棵二元樹的根節點</param>
    /// <returns>合併後的二元樹根節點</returns>
    /// <example>
    /// <code>
    ///  Tree1: [1,3,2,5], Tree2: [2,1,3,null,4,null,7]
    ///  合併結果: [3,4,5,5,4,null,7]
    /// var result = MergeTrees(root1, root2);
    /// </code>
    /// </example>
    public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
    {
        // 終止條件：若兩個節點都為 null，不需要合併，直接回傳 null
        if (root1 is null && root2 is null)
        {
            return null;
        }

        // 若 root1 為空，直接回傳 root2（包含其所有子樹）
        if (root1 is null && root2 is not null)
        {
            return root2;
        }

        // 若 root2 為空，直接回傳 root1（包含其所有子樹）
        if (root1 is not null && root2 is null)
        {
            return root1;
        }

        // 兩個節點都存在時，建立新節點，值為兩節點值的總和
        TreeNode mergedNode = new TreeNode(root1.val + root2.val);

        // 遞迴合併左子樹
        mergedNode.left = MergeTrees(root1.left, root2.left);

        // 遞迴合併右子樹
        mergedNode.right = MergeTrees(root1.right, root2.right);

        return mergedNode;
    }
}