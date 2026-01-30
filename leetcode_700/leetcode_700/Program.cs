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
    /// https://leetcode.com/problems/search-in-a-binary-search-tree/description/
    /// 700. 二叉搜索树中的搜索
    /// https://leetcode.cn/problems/search-in-a-binary-search-tree/description/
    /// 
    /// 700. Search in a Binary Search Tree
    /// Given the root of a binary search tree (BST) and an integer val.
    /// Find the node in the BST that the node's value equals val and return the subtree rooted with that node.
    /// If such a node does not exist, return null.
    /// 
    /// 700. 二叉搜尋樹中的搜尋
    /// 給定一個二叉搜尋樹 (BST) 的根節點與一個整數 val。
    /// 找到值等於 val 的節點，並回傳以該節點為根的子樹；若不存在，回傳 null。
    ///  </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== LeetCode 700. Search in a Binary Search Tree ===");
        Console.WriteLine();

        // 建立測試用的二元搜尋樹 (BST)
        //       4
        //      / \
        //     2   7
        //    / \
        //   1   3
        var root = new TreeNode(4)
        {
            left = new TreeNode(2)
            {
                left = new TreeNode(1),
                right = new TreeNode(3)
            },
            right = new TreeNode(7)
        };

        Console.WriteLine("測試樹結構:");
        Console.WriteLine("       4");
        Console.WriteLine("      / \\");
        Console.WriteLine("     2   7");
        Console.WriteLine("    / \\");
        Console.WriteLine("   1   3");
        Console.WriteLine();

        var program = new Program();

        // 測試案例 1: 搜尋存在的值
        int target1 = 2;
        var result1 = program.SearchBST(root, target1);
        Console.WriteLine($"測試 1: 搜尋 val = {target1}");
        Console.WriteLine($"結果: {(result1 is not null ? $"找到節點，子樹根節點值 = {result1.val}" : "未找到")}");
        Console.WriteLine();

        // 測試案例 2: 搜尋不存在的值
        int target2 = 5;
        var result2 = program.SearchBST(root, target2);
        Console.WriteLine($"測試 2: 搜尋 val = {target2}");
        Console.WriteLine($"結果: {(result2 is not null ? $"找到節點，子樹根節點值 = {result2.val}" : "未找到")}");
        Console.WriteLine();

        // 測試案例 3: 搜尋根節點
        int target3 = 4;
        var result3 = program.SearchBST(root, target3);
        Console.WriteLine($"測試 3: 搜尋 val = {target3}");
        Console.WriteLine($"結果: {(result3 is not null ? $"找到節點，子樹根節點值 = {result3.val}" : "未找到")}");
        Console.WriteLine();

        // 測試案例 4: 搜尋葉子節點
        int target4 = 7;
        var result4 = program.SearchBST(root, target4);
        Console.WriteLine($"測試 4: 搜尋 val = {target4}");
        Console.WriteLine($"結果: {(result4 is not null ? $"找到節點，子樹根節點值 = {result4.val}" : "未找到")}");
    }

    /// <summary>
    /// 在二元搜尋樹 (BST) 中搜尋指定值的節點。
    /// 
    /// <para>
    /// <b>解題思路：遞迴法</b>
    /// </para>
    /// <para>
    /// 二元搜尋樹 (BST) 滿足以下性質：
    /// <list type="bullet">
    ///   <item>左子樹所有節點的元素值均小於根節點的元素值</item>
    ///   <item>右子樹所有節點的元素值均大於根節點的元素值</item>
    /// </list>
    /// </para>
    /// <para>
    /// 根據此性質，演算法如下：
    /// <list type="number">
    ///   <item>若 root 為空則回傳空節點</item>
    ///   <item>若 val == root.val，則回傳 root</item>
    ///   <item>若 val &lt; root.val，遞迴搜尋左子樹</item>
    ///   <item>若 val &gt; root.val，遞迴搜尋右子樹</item>
    /// </list>
    /// </para>
    /// <para>
    /// <b>複雜度分析：</b>
    /// <list type="bullet">
    ///   <item>時間複雜度：O(h)，其中 h 為樹的高度</item>
    ///   <item>空間複雜度：O(h)，遞迴呼叫堆疊的深度</item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="root">BST 的根節點</param>
    /// <param name="val">要搜尋的目標值</param>
    /// <returns>若找到則回傳該節點為根的子樹，否則回傳 null</returns>
    /// <example>
    /// <code>
    /// 輸入: root = [4,2,7,1,3], val = 2
    /// 輸出: [2,1,3]
    /// </code>
    /// </example>
    public TreeNode? SearchBST(TreeNode? root, int val)
    {
        // 基礎情況 1：節點為空，表示搜尋結束，未找到目標值
        if (root is null)
        {
            return null;
        }

        // 基礎情況 2：找到目標值，回傳當前節點（以此節點為根的子樹）
        if (root.val == val)
        {
            return root;
        }

        // 遞迴情況：根據 BST 性質決定往左或往右搜尋
        if (root.val < val)
        {
            // 目標值大於當前節點，往右子樹搜尋
            return SearchBST(root.right, val);
        }
        else
        {
            // 目標值小於當前節點，往左子樹搜尋
            return SearchBST(root.left, val);
        }
    }
}
