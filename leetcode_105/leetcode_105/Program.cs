namespace leetcode_105;

class Program
{
 public class TreeNode {
     public int val;
     public TreeNode left;
     public TreeNode right;
     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
         this.val = val;
         this.left = left;
         this.right = right;
     }
 }

    /// <summary>
    /// 105. Construct Binary Tree from Preorder and Inorder Traversal
    /// https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/description/
    /// 105. 从前序与中序遍历序列构造二叉树
    /// https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試案例 1
        int[] preorder1 = {3,9,20,15,7};
        int[] inorder1 = {9,3,15,20,7};
        Console.WriteLine("測試案例 1:");
        TreeNode root1 = BuildTree(preorder1, inorder1);
        Console.WriteLine($"前序遍歷: {string.Join(",", PreorderTraversal(root1))}");
        Console.WriteLine($"中序遍歷: {string.Join(",", InorderTraversal(root1))}");

        // 測試案例 2
        int[] preorder2 = {1,2,3};
        int[] inorder2 = {2,1,3};
        Console.WriteLine("\n測試案例 2:");
        TreeNode root2 = BuildTree(preorder2, inorder2);
        Console.WriteLine($"前序遍歷: {string.Join(",", PreorderTraversal(root2))}");
        Console.WriteLine($"中序遍歷: {string.Join(",", InorderTraversal(root2))}");
    }

    /// <summary>
    /// ref:
    /// https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/solutions/2646359/tu-jie-cong-on2-dao-onpythonjavacgojsrus-aob8/
    /// https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/solutions/255811/cong-qian-xu-yu-zhong-xu-bian-li-xu-lie-gou-zao-9/
    /// https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/solutions/1456907/105-cong-qian-xu-yu-zhong-xu-bian-li-xu-po3w6/
    /// 建議看ref第一個連結 有圖示說明
    /// 比較好理解左右子樹取範圍index計算
    /// 
    /// 前序遍歷特點：
    ///     順序為：根節點 → 左子樹 → 右子樹
    ///     第一個元素必定是根節點
    /// 中序遍歷特點：
    ///     順序為：左子樹 → 根節點 → 右子樹
    ///     根節點左邊是左子樹所有節點
    ///     根節點右邊是右子樹所有節點
    /// 
    /// 解題步驟
    /// 1.從前序遍歷中取第一個元素作為根節點
    /// 2.在中序遍歷中找到該根節點位置
    /// 3.根據中序遍歷中根節點的位置，可以確定：
    ///     左子樹的節點數量
    ///     左右子樹在兩個數組中的範圍
    /// 4.遞迴處理左右子樹
    /// 
    /// 主要建樹方法，處理初始參數檢查
    /// 解題概念：
    /// 1. 前序遍歷的特點：第一個節點必定是根節點，接著是左子樹所有節點，然後是右子樹所有節點
    /// 2. 中序遍歷的特點：左子樹所有節點在根節點左邊，右子樹所有節點在根節點右邊
    /// 3. 解題步驟：
    ///    - 利用前序遍歷找到根節點
    ///    - 在中序遍歷中找到根節點位置，可分割出左右子樹
    ///    - 遞迴處理左右子樹
    /// 4. 時間複雜度：O(n)，其中 n 為節點數量
    /// 5. 空間複雜度：O(n)，主要是遞迴調用棧的空間
    /// 
    /// </summary>
    public static TreeNode BuildTree(int[] preorder, int[] inorder)
    {
        // 檢查輸入參數是否有效
        if (preorder == null || inorder == null || preorder.Length == 0 || inorder.Length == 0)
        {
            return null;
        }

        return BuildTreeHelper(preorder, 0, preorder.Length - 1, inorder, 0, inorder.Length - 1);
    }

    /// <summary>
    /// 遞迴輔助方法，實際執行建樹邏輯
    /// </summary>
    /// <param name="preStart">前序遍歷起始位置</param>
    /// <param name="preEnd">前序遍歷結束位置</param>
    /// <param name="inStart">中序遍歷起始位置</param>
    /// <param name="inEnd">中序遍歷結束位置</param>
    private static TreeNode BuildTreeHelper(int[] preorder, int preStart, int preEnd, int[] inorder, int inStart, int inEnd)
    {
        // 基本情況：如果起始位置大於結束位置，返回null
        if (preStart > preEnd || inStart > inEnd)
        {
            return null;
        }    

        // 1. 從前序遍歷的第一個元素創建根節點
        TreeNode root = new TreeNode(preorder[preStart]);

        // 2. 在中序遍歷中找到根節點的位置 
        // (被搜尋的目標數組, 我們需要在中序遍歷中找到這個根節點的位置, 指定搜尋的起始位置, 指定要搜尋的元素數量)
        int rootIndex = Array.IndexOf(inorder, preorder[preStart], inStart, inEnd - inStart + 1);
        // 3. 計算左子樹的節點數量
        int leftSubtreeSize = rootIndex - inStart;

        // 4. 遞迴構建左子樹
        // 左子樹在前序遍歷中的範圍：[preStart + 1, preStart + leftSubtreeSize]
        // 左子樹在中序遍歷中的範圍：[inStart, rootIndex - 1]
        root.left = BuildTreeHelper(preorder, preStart + 1, preStart + leftSubtreeSize,
                                  inorder, inStart, rootIndex - 1);

        // 5. 遞迴構建右子樹
        // 右子樹在前序遍歷中的範圍：[preStart + leftSubtreeSize + 1, preEnd]
        // 右子樹在中序遍歷中的範圍：[rootIndex + 1, inEnd]
        root.right = BuildTreeHelper(preorder, preStart + leftSubtreeSize + 1, preEnd,
                                   inorder, rootIndex + 1, inEnd);

        return root;
    }

    /// <summary>
    /// 輔助方法：前序遍歷
    /// </summary>
    private static List<int> PreorderTraversal(TreeNode root)
    {
        var result = new List<int>();
        if (root == null) return result;
        
        result.Add(root.val);
        result.AddRange(PreorderTraversal(root.left));
        result.AddRange(PreorderTraversal(root.right));
        return result;
    }

    /// <summary>
    /// 輔助方法：中序遍歷
    /// </summary>
    private static List<int> InorderTraversal(TreeNode root)
    {
        var result = new List<int>();
        if (root == null) return result;
        
        result.AddRange(InorderTraversal(root.left));
        result.Add(root.val);
        result.AddRange(InorderTraversal(root.right));
        return result;
    }
}
