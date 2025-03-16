using System.ComponentModel.Design.Serialization;

namespace leetcode_230;

class Program
{
    public class TreeNode 
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) 
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    private int res;
    private int k;

    /// <summary>
    /// 230. Kth Smallest Element in a BST
    /// https://leetcode.com/problems/kth-smallest-element-in-a-bst/description/
    /// 230. 二元搜尋樹中第K 小的元素
    /// https://leetcode.cn/problems/kth-smallest-element-in-a-bst/description/
    /// </summary>
    /// <param name="args"></param> 
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試案例 1: [3,1,4,null,2], k = 1
        TreeNode test1 = new TreeNode(3);
        test1.left = new TreeNode(1);
        test1.right = new TreeNode(4);
        test1.left.right = new TreeNode(2);
        Console.WriteLine($"測試案例 1 結果: {program.KthSmallest(test1, 1)}"); // 預期輸出: 1
        Console.WriteLine("測試案例 1 中序遍歷結果: ");
        program.InorderTraversal(test1);
        Console.WriteLine();
        
        // 測試案例 2: [5,3,6,2,4,null,null,1], k = 3
        TreeNode test2 = new TreeNode(5);
        test2.left = new TreeNode(3);
        test2.right = new TreeNode(6);
        test2.left.left = new TreeNode(2);
        test2.left.right = new TreeNode(4);
        test2.left.left.left = new TreeNode(1);
        Console.WriteLine($"測試案例 2 結果: {program.KthSmallest(test2, 3)}"); // 預期輸出: 3
        Console.WriteLine("測試案例 2 中序遍歷結果: ");
        program.InorderTraversal(test2);
        Console.WriteLine();
    }
    
    /// <summary>
    /// 尋找二元搜尋樹中第k小的元素
    /// 解題思路：
    /// 1. 利用二元搜尋樹的特性，中序遍歷(Inorder Traversal)會產生由小到大的排序結果
    /// 2. 使用計數器記錄當前遍歷到第幾個節點
    /// 3. 當計數器等於k時，即為所求的第k小元素
    /// </summary>
    /// <param name="root">二元搜尋樹的根節點</param>
    /// <param name="k">要尋找第k小的元素</param>
    /// <returns>第k小的元素值</returns>
    public int KthSmallest(TreeNode root, int k) 
    {
        this.k = k;
        dfs(root);

        return res;
    }

    /// <summary>
    /// 執行中序遍歷的深度優先搜尋(DFS)
    /// 遍歷順序：左子樹 -> 根節點 -> 右子樹
    /// 每訪問一個節點，計數器k就減1，當k為0時表示找到目標節點
    /// </summary>
    /// <param name="node">當前遍歷的節點</param>
    private void dfs(TreeNode node)
    {
        if(node ==null)
        {
            return;
        }

        dfs(node.left);
        if(--k == 0)
        {
            res = node.val;
            return;
        }
        dfs(node.right);
    }

    /// <summary>
    /// 執行中序遍歷並輸出結果
    /// 遍歷順序：左子樹 -> 根節點 -> 右子樹
    /// </summary>
    /// <param name="root">二元搜尋樹的根節點</param>
    public void InorderTraversal(TreeNode root)
    {
        if (root == null)
        {
            return;
        }
        
        InorderTraversal(root.left);
        Console.Write($"{root.val} ");
        InorderTraversal(root.right);
    }
}
