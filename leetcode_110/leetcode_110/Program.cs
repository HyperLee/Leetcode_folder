namespace leetcode_110;

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
    /// 110. Balanced Binary Tree
    /// https://leetcode.com/problems/balanced-binary-tree/description/?envType=daily-question&envId=2026-02-08
    /// 110. 平衡二元樹
    /// https://leetcode.cn/problems/balanced-binary-tree/description/?envType=daily-question&envId=2026-02-08
    /// 
    /// Given a binary tree, determine if it is height-balanced.
    /// 
    /// 平衡二元樹的基本概念
    /// 定義：平衡二元樹是一種二元樹，旨在保持樹的高度盡可能低，以確保操作（如插入、刪除和搜尋）的時間複雜度接近 O(log n)。
    /// 特性：每個節點的左子樹和右子樹的高度差不超過 1。常見的平衡二元樹包括 AVL 樹和紅黑樹。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試案例 1: 平衡二元樹 [3,9,20,null,null,15,7]
        //       3
        //      / \
        //     9  20
        //       /  \
        //      15   7
        TreeNode test1 = new TreeNode(3,
            new TreeNode(9),
            new TreeNode(20,
                new TreeNode(15),
                new TreeNode(7)));
        
        Console.WriteLine("測試案例 1: [3,9,20,null,null,15,7]");
        Console.WriteLine($"解法一結果: {program.IsBalanced(test1)}");  // 預期: True
        Console.WriteLine($"解法二結果: {program.IsBalanced2(test1)}");  // 預期: True
        Console.WriteLine();
        
        // 測試案例 2: 不平衡二元樹 [1,2,2,3,3,null,null,4,4]
        //          1
        //         / \
        //        2   2
        //       / \
        //      3   3
        //     / \
        //    4   4
        TreeNode test2 = new TreeNode(1,
            new TreeNode(2,
                new TreeNode(3,
                    new TreeNode(4),
                    new TreeNode(4)),
                new TreeNode(3)),
            new TreeNode(2));
        
        Console.WriteLine("測試案例 2: [1,2,2,3,3,null,null,4,4]");
        Console.WriteLine($"解法一結果: {program.IsBalanced(test2)}");  // 預期: False
        Console.WriteLine($"解法二結果: {program.IsBalanced2(test2)}");  // 預期: False
        Console.WriteLine();
        
        // 測試案例 3: 空樹
        TreeNode test3 = null;
        Console.WriteLine("測試案例 3: 空樹");
        Console.WriteLine($"解法一結果: {program.IsBalanced(test3)}");  // 預期: True
        Console.WriteLine($"解法二結果: {program.IsBalanced2(test3)}");  // 預期: True
        Console.WriteLine();
        
        // 測試案例 4: 單一節點
        TreeNode test4 = new TreeNode(1);
        Console.WriteLine("測試案例 4: 單一節點 [1]");
        Console.WriteLine($"解法一結果: {program.IsBalanced(test4)}");  // 預期: True
        Console.WriteLine($"解法二結果: {program.IsBalanced2(test4)}");  // 預期: True
    }

    /// <summary>
    /// 解法一:自頂向下的遞迴 (Top-Down Approach)
    /// 
    /// 解題思路:
    /// 1. 對每個節點,計算其左右子樹的高度差
    /// 2. 檢查高度差是否 <= 1
    /// 3. 遞迴檢查左右子樹是否也平衡
    /// 
    /// 時間複雜度: O(n²)
    /// - 每個節點都要計算高度,而計算高度需要遍歷子樹
    /// - 最壞情況下(完全二元樹),每層計算高度的時間複雜度為 O(n)
    /// 
    /// 空間複雜度: O(n)
    /// - 遞迴呼叫堆疊的深度,最壞情況為樹的高度 n(鏈狀樹)
    /// 
    /// 缺點:
    /// - 重複計算:同一個節點的高度可能被計算多次
    /// - 即使已經發現某個子樹不平衡,仍會繼續檢查其他節點
    /// </summary>
    /// <param name="root">二元樹根節點</param>
    /// <returns>如果是平衡二元樹返回 true,否則返回 false</returns>
    public bool IsBalanced(TreeNode root)
    {
        // 基本情況:空樹視為平衡
        if(root is null)
        {
            return true;
        }
        else
        {
            // 三個條件必須同時滿足:
            // 1. 當前節點的左右子樹高度差 <= 1
            // 2. 左子樹本身是平衡的
            // 3. 右子樹本身是平衡的
            return Math.Abs(height(root.left) - height(root.right)) <= 1 
                && IsBalanced(root.left) 
                && IsBalanced(root.right);
        }
    }

    /// <summary>
    /// 計算二元樹的高度
    /// 輔助函式,用於解法一
    /// </summary>
    /// <param name="root">樹的根節點</param>
    /// <returns>樹的高度(空樹高度為 0)</returns>
    public int height(TreeNode root)
    {
        // 空樹高度為 0
        if(root is null)
        {
            return 0;
        }
        else
        {
            // 遞迴計算左右子樹高度,取最大值後 +1(加上當前節點這一層)
            return Math.Max(height(root.left), height(root.right)) + 1;
        }
    }

    /// <summary>
    /// 解法二:自底向上的遞迴 (Bottom-Up Approach)
    /// 
    /// 解題思路:
    /// 1. 採用後序遍歷(左->右->根)的方式
    /// 2. 先遞迴處理左右子樹,如果發現不平衡立即返回 -1
    /// 3. 如果子樹平衡,計算高度時同時檢查當前節點是否平衡
    /// 4. 使用 -1 作為特殊標記表示樹不平衡
    /// 
    /// 時間複雜度: O(n)
    /// - 每個節點只訪問一次,計算高度的同時判斷是否平衡
    /// 
    /// 空間複雜度: O(n)
    /// - 遞迴呼叫堆疊的深度,最壞情況為樹的高度 n(鏈狀樹)
    /// 
    /// 優點:
    /// - 避免重複計算:每個節點的高度只計算一次
    /// - 提前終止:一旦發現不平衡,立即返回,不再繼續檢查
    /// - 時間複雜度優於解法一
    /// </summary>
    /// <param name="root">二元樹根節點</param>
    /// <returns>如果是平衡二元樹返回 true,否則返回 false</returns>
    public bool IsBalanced2(TreeNode root)
    {
        // 如果 height2 返回 -1,表示樹不平衡
        // 否則返回的是樹的高度,表示樹平衡
        return height2(root) != -1;
    }

    /// <summary>
    /// 計算二元樹的高度,如果不平衡則返回 -1
    /// 輔助函式,用於解法二
    /// 
    /// 返回值說明:
    /// - 返回 -1: 表示以 root 為根的子樹不平衡
    /// - 返回 >= 0: 表示以 root 為根的子樹平衡,且值為該子樹的高度
    /// 
    /// 關鍵特性:
    /// 1. 空子樹高度為 0
    /// 2. 非空子樹高度 = Max(左子樹高度, 右子樹高度) + 1
    /// 3. 一旦發現不平衡,立即返回 -1,避免不必要的計算
    /// </summary>
    /// <param name="root">樹的根節點</param>
    /// <returns>樹的高度(不平衡時返回 -1)</returns>
    public int height2(TreeNode root)
    {
        // 基本情況:空子樹高度為 0(且空樹是平衡的)
        if(root is null)
        {
            return 0;
        }

        // 遞迴計算左子樹高度
        int leftH = height2(root.left);
        
        // 如果左子樹不平衡,提前終止,直接返回 -1
        // 這樣可以避免繼續檢查右子樹,提高效率
        if(leftH == -1)
        {
            return -1;
        }

        // 遞迴計算右子樹高度
        int rightH = height2(root.right);
        
        // 檢查兩個條件:
        // 1. 右子樹本身是否平衡 (rightH == -1)
        // 2. 當前節點的左右子樹高度差是否 > 1
        // 只要有一個條件滿足,就表示不平衡
        if(rightH == -1 || Math.Abs(leftH - rightH) > 1)
        {
            return -1;
        }
        
        // 如果程式執行到這裡,表示:
        // 1. 左子樹平衡 (leftH >= 0)
        // 2. 右子樹平衡 (rightH >= 0)
        // 3. 當前節點的左右子樹高度差 <= 1
        // 因此當前子樹平衡,返回其高度
        return Math.Max(leftH, rightH) + 1;
    }
}
