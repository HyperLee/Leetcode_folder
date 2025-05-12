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
    /// LeetCode 230. Kth Smallest Element in a BST
    /// 題目描述：
    /// 給定一個二元搜尋樹（BST）的根節點 root 和一個整數 k，請你設計一個演算法，找出 BST 中第 k 小的元素。
    /// k 從 1 開始計算。
    /// 你必須在 O(h) 的時間複雜度內完成，h 是樹的高度。
    /// 題目連結：
    /// https://leetcode.com/problems/kth-smallest-element-in-a-bst/description/
    /// https://leetcode.cn/problems/kth-smallest-element-in-a-bst/description/
    /// </summary>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試案例 1: [3,1,4,null,2], k = 1
        TreeNode test1 = new TreeNode(3);
        test1.left = new TreeNode(1);
        test1.right = new TreeNode(4);
        test1.left.right = new TreeNode(2);
        Console.WriteLine($"測試案例 1 結果 (遞迴版): {program.KthSmallest(test1, 1)}"); // 預期輸出: 1
        Console.WriteLine($"測試案例 1 結果 (疊代版): {program.KthSmallestIterative(test1, 1)}"); // 預期輸出: 1
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
        Console.WriteLine($"測試案例 2 結果 (遞迴版): {program.KthSmallest(test2, 3)}"); // 預期輸出: 3
        Console.WriteLine($"測試案例 2 結果 (疊代版): {program.KthSmallestIterative(test2, 3)}"); // 預期輸出: 3
        Console.WriteLine("測試案例 2 中序遍歷結果: ");
        program.InorderTraversal(test2);
        Console.WriteLine();
        
        // 測試案例 3: 空樹測試
        TreeNode emptyTree = null;
        Console.WriteLine($"測試案例 3 (空樹) 結果 (疊代版): {program.KthSmallestIterative(emptyTree, 1)}"); // 預期輸出: -1
        
        // 測試案例 4: k 大於節點數量
        Console.WriteLine($"測試案例 4 (k=10 大於節點數量) 結果 (疊代版): {program.KthSmallestIterative(test2, 10)}"); // 預期輸出: -1
    }
    
    /// <summary>
    /// 尋找二元搜尋樹中第 k 小的元素
    /// 解題思路：
    /// 1. 利用二元搜尋樹的特性，中序遍歷 (Inorder Traversal) 會產生由小到大的排序結果
    /// 2. 使用計數器記錄當前遍歷到第幾個節點
    /// 3. 當計數器等於 k 時，即為所求的第 k 小元素
    /// </summary>
    /// <param name="root"> 二元搜尋樹的根節點 </param>
    /// <param name="k"> 要尋找第 k 小的元素 </param>
    /// <returns > 第 k 小的元素值 </returns>
    public int KthSmallest(TreeNode root, int k) 
    {
        this.k = k;
        dfs(root);

        return res;
    }

    /// <summary>
    /// 執行中序遍歷的深度優先搜尋 (DFS)
    /// 遍歷順序：左子樹 -> 根節點 -> 右子樹
    /// 每訪問一個節點，計數器 k 就減 1，當 k 為 0 時表示找到目標節點
    /// </summary>
    /// <param name="node">當前遍歷的節點</param>
    private void dfs(TreeNode node)
    {
        if(node ==null)
        {
            return;
        }

        // 遞迴遍歷左子樹
        dfs(node.left);
        // 訪問當前節點
        if(--k == 0)
        {
            res = node.val;
            return;
        }
        // 遞迴遍歷右子樹
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

    
    /// <summary>
    /// 找到二元搜尋樹中第 k 小的元素（迭代版）
    /// 解題思路：
    /// 1. 使用堆疊來模擬遞迴過程，避免系統堆疊溢位風險
    /// 2. 利用中序遍歷特性，先處理左子樹，再處理根節點，最後處理右子樹
    /// 3. 使用計數器追蹤已訪問節點數量，找到第 k 個節點時返回結果
    /// 優點：
    /// 1. 可以處理較深的樹結構而不會發生堆疊溢位
    /// 2. 在某些情況下效能較遞迴實作更佳
    /// 3. 可以提前終止，無需遍歷整棵樹
    /// </summary>
    /// <param name="root">二元搜尋樹的根節點</param>
    /// <param name="k">要尋找第 k 小的元素</param>
    /// <returns>第 k 小的元素值，若找不到則返回 -1</returns>    
    public int KthSmallestIterative(TreeNode? root, int k) 
    {
        // 處理空樹的情況
        if (root == null)
        {
            return -1;
        }
        
        // 建立堆疊用於模擬遞迴呼叫時系統維護的堆疊
        Stack<TreeNode> 堆疊 = new Stack<TreeNode>();
        TreeNode? current = root;
        
        while (current != null || 堆疊.Count > 0)
        {
            // 步驟1: 遍歷到最左邊的節點，同時將路徑上的所有節點存入堆疊
            // 使用迴圈模擬遞迴下降
            while (current != null)
            {
                堆疊.Push(current);
                current = current.left;
            }
            
            // 步驟2: 從堆疊中取出節點（相當於回溯到上一層）
            current = 堆疊.Pop();
            
            // 步驟3: 檢查是否是第k個節點 (使用--k替代count++)
            if (--k == 0)
            {
                return current.val; // 找到目標節點，返回其值
            }
            
            // 步驟4: 處理右子樹
            current = current.right;
        }
        
        return -1; // 若找不到第k小的元素
    }
}
