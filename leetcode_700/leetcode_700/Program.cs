namespace leetcode_700;

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
    /// 700. Search in a Binary Search Tree
    /// Given the root of a binary search tree (BST) and an integer val.
    /// Find the node in the BST that the node's value equals val and return the subtree rooted with that node.
    /// If such a node does not exist, return null.
    /// 
    /// 700. 二叉搜尋樹中的搜尋
    /// 給定一個二叉搜尋樹 (BST) 的根節點與一個整數 val。
    /// 找到值等於 val 的節點，並回傳以該節點為根的子樹；若不存在，回傳 null。
    /// 
    /// Links:
    /// https://leetcode.com/problems/search-in-a-binary-search-tree/description/
    /// https://leetcode.cn/problems/search-in-a-binary-search-tree/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 建立測試用的 BST：[4,2,7,1,3]
        //         4
        //        / \
        //       2   7
        //      / \
        //     1   3
        var root = new TreeNode(4,
            new TreeNode(2,
                new TreeNode(1),
                new TreeNode(3)),
            new TreeNode(7));

        // 測試案例 1：搜尋存在的值 2
        var result1 = program.SearchBST(root, 2);
        Console.WriteLine($"搜尋值 2：{(result1 is not null ? $"找到節點，值為 {result1.val}" : "未找到")}");
        // 預期輸出：找到節點，值為 2

        // 測試案例 2：搜尋存在的值 7
        var result2 = program.SearchBST(root, 7);
        Console.WriteLine($"搜尋值 7：{(result2 is not null ? $"找到節點，值為 {result2.val}" : "未找到")}");
        // 預期輸出：找到節點，值為 7

        // 測試案例 3：搜尋不存在的值 5
        var result3 = program.SearchBST(root, 5);
        Console.WriteLine($"搜尋值 5：{(result3 is not null ? $"找到節點，值為 {result3.val}" : "未找到")}");
        // 預期輸出：未找到

        // 測試案例 4：搜尋根節點值 4
        var result4 = program.SearchBST(root, 4);
        Console.WriteLine($"搜尋值 4：{(result4 is not null ? $"找到節點，值為 {result4.val}" : "未找到")}");
        // 預期輸出：找到節點，值為 4

        // 測試案例 5：空樹搜尋
        var result5 = program.SearchBST(null, 1);
        Console.WriteLine($"空樹搜尋值 1：{(result5 is not null ? $"找到節點，值為 {result5.val}" : "未找到")}");
        // 預期輸出：未找到
    }

    /// <summary>
    /// 在二元搜尋樹中搜尋指定值的節點。
    /// 
    /// <para><b>解題思路：遞迴法</b></para>
    /// <para>
    /// 二元搜尋樹 (BST) 滿足以下性質：
    /// <list type="bullet">
    ///   <item>左子樹所有節點的值均小於根節點的值</item>
    ///   <item>右子樹所有節點的值均大於根節點的值</item>
    /// </list>
    /// </para>
    /// 
    /// <para><b>演算法步驟：</b></para>
    /// <list type="number">
    ///   <item>若 root 為空，則回傳空節點</item>
    ///   <item>若 val == root.val，則回傳 root</item>
    ///   <item>若 val &lt; root.val，遞迴搜尋左子樹</item>
    ///   <item>若 val &gt; root.val，遞迴搜尋右子樹</item>
    /// </list>
    /// 
    /// <para><b>時間複雜度：</b>O(H)，H 為樹的高度，最差情況為 O(N)</para>
    /// <para><b>空間複雜度：</b>O(H)，遞迴呼叫堆疊的深度</para>
    /// </summary>
    /// <param name="root">二元搜尋樹的根節點</param>
    /// <param name="val">要搜尋的目標值</param>
    /// <returns>若找到目標值，回傳以該節點為根的子樹；否則回傳 null</returns>
    /// <example>
    /// <code>
    ///  範例：在 BST [4,2,7,1,3] 中搜尋值 2
    ///  輸入：root = [4,2,7,1,3], val = 2
    ///  輸出：[2,1,3]
    /// </code>
    /// </example>
    public TreeNode? SearchBST(TreeNode? root, int val)
    {
        // 基本情況：若節點為空，表示未找到目標值
        if (root is null)
        {
            return null;
        }

        // 找到目標值，回傳當前節點（及其子樹）
        if (root.val == val)
        {
            return root;
        }

        // 利用 BST 特性：目標值較大時往右子樹搜尋，較小時往左子樹搜尋
        if (root.val < val)
        {
            return SearchBST(root.right, val);
        }
        else
        {
            return SearchBST(root.left, val);
        }
    }
}
