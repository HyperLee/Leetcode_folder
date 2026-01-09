namespace leetcode_865;

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
    /// 865. Smallest Subtree with all the Deepest Nodes
    /// https://leetcode.com/problems/smallest-subtree-with-all-the-deepest-nodes/description/?envType=daily-question&envId=2026-01-09
    /// 865. 具有所有最深節點的最小子樹
    /// https://leetcode.cn/problems/smallest-subtree-with-all-the-deepest-nodes/description/?envType=daily-question&envId=2026-01-09
    /// 
    /// 題目描述（繁體中文）：
    /// 給定一個二叉樹的根節點，節點的深度定義為從根節點到該節點的最短距離。
    /// 請返回一個最小的子樹，使該子樹包含原樹中所有的最深節點。
    /// 若某節點在整棵樹中的深度為最大值，則稱該節點為最深節點。
    /// 節點的子樹指由該節點及其所有子孫節點所構成的樹。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試案例 1: [3,5,1,6,2,0,8,null,null,7,4]
        TreeNode test1 = new TreeNode(3);
        test1.left = new TreeNode(5);
        test1.right = new TreeNode(1);
        test1.left.left = new TreeNode(6);
        test1.left.right = new TreeNode(2);
        test1.right.left = new TreeNode(0);
        test1.right.right = new TreeNode(8);
        test1.left.right.left = new TreeNode(7);
        test1.left.right.right = new TreeNode(4);
        
        TreeNode result1 = program.SubtreeWithAllDeepest(test1);
        Console.WriteLine($"測試案例 1 結果: {result1.val}"); // 預期輸出: 2
        
        // 測試案例 2: [1]
        TreeNode test2 = new TreeNode(1);
        TreeNode result2 = program.SubtreeWithAllDeepest(test2);
        Console.WriteLine($"測試案例 2 結果: {result2.val}"); // 預期輸出: 1
        
        // 測試案例 3: [0,1,3,null,2]
        TreeNode test3 = new TreeNode(0);
        test3.left = new TreeNode(1);
        test3.right = new TreeNode(3);
        test3.left.right = new TreeNode(2);
        
        TreeNode result3 = program.SubtreeWithAllDeepest(test3);
        Console.WriteLine($"測試案例 3 結果: {result3.val}"); // 預期輸出: 2
    }

    /// <summary>
    /// 找出包含所有最深節點的最小子樹
    /// 
    /// 解題思路：
    /// 使用遞迴的方式進行深度優先搜尋（DFS），對每個節點返回兩個資訊：
    /// 1. 該子樹的最深葉節點的最近公共祖先（LCA）
    /// 2. 該子樹的深度
    /// 
    /// 時間複雜度：O(n)，其中 n 是樹中的節點數，每個節點訪問一次
    /// 空間複雜度：O(h)，其中 h 是樹的高度，遞迴呼叫堆疊的深度
    /// </summary>
    /// <param name="root">二叉樹的根節點</param>
    /// <returns>包含所有最深節點的最小子樹的根節點</returns>
    public TreeNode SubtreeWithAllDeepest(TreeNode root)
    {
        // 呼叫輔助函式 dfs，返回的 Tuple 的第一個元素即為所求的 LCA 節點
        return dfs(root).Item1;
    }

    /// <summary>
    /// 遞迴輔助函式：計算子樹的深度和包含所有最深節點的 LCA
    /// 
    /// 此函式返回一個 Tuple，包含：
    /// - Item1: 包含所有最深節點的最近公共祖先（LCA）節點
    /// - Item2: 當前子樹的深度
    /// 
    /// 演算法邏輯：
    /// 1. 若節點為空，返回 (null, 0)
    /// 2. 遞迴計算左右子樹的深度和 LCA
    /// 3. 比較左右子樹深度：
    ///    - 左子樹更深：最深節點都在左子樹，返回左子樹的 LCA
    ///    - 右子樹更深：最深節點都在右子樹，返回右子樹的 LCA
    ///    - 深度相同：左右子樹都有最深節點，當前節點就是 LCA
    /// </summary>
    /// <param name="root">當前處理的節點</param>
    /// <returns>Tuple，包含 LCA 節點和深度</returns>
    private Tuple<TreeNode, int> dfs(TreeNode root)
    {
        // 基礎情況：空節點的深度為 0
        if(root == null)
        {
            return new Tuple<TreeNode, int>(root, 0);
        }

        // 遞迴處理左子樹，獲取左子樹的 LCA 和深度
        Tuple<TreeNode, int> left = dfs(root.left);
        // 遞迴處理右子樹，獲取右子樹的 LCA 和深度
        Tuple<TreeNode, int> right = dfs(root.right);

        // 情況 1：左子樹更深，最深葉節點都在左子樹中
        if(left.Item2 > right.Item2)
        {
            return new Tuple<TreeNode, int>(left.Item1, left.Item2 + 1);
        }

        // 情況 2：右子樹更深，最深葉節點都在右子樹中
        if(left.Item2 < right.Item2)
        {
            return new Tuple<TreeNode, int>(right.Item1, right.Item2 + 1);
        }

        // 情況 3：左右子樹深度相同，當前節點就是包含所有最深節點的 LCA
        return new Tuple<TreeNode, int>(root, left.Item2 + 1);
    }
    
}
