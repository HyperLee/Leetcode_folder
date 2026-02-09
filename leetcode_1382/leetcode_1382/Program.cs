namespace leetcode_1382;

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
    /// 1382. Balance a Binary Search Tree
    /// https://leetcode.com/problems/balance-a-binary-search-tree/description/?envType=daily-question&envId=2026-02-09
    /// 1382. 將二叉搜尋樹變平衡
    /// https://leetcode.cn/problems/balance-a-binary-search-tree/description/?envType=daily-question&envId=2026-02-09
    ///
    /// English:
    /// Given the root of a binary search tree, return a balanced binary search tree with the same node values.
    /// If there is more than one answer, return any of them.
    /// A binary search tree is balanced if the depth of the two subtrees of every node never differs by more than 1.
    ///
    /// 繁體中文:
    /// 給定一個二叉搜尋樹的根節點，回傳一個具有相同節點值的平衡二叉搜尋樹。
    /// 如果存在多個答案，可回傳任意一個。
    /// 當每個節點的左右子樹深度差不超過 1 時，該二叉搜尋樹視為平衡的。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試案例 1: [1,null,2,null,3,null,4,null,null]
        // 不平衡的二叉搜尋樹（完全向右傾斜）
        TreeNode root1 = new TreeNode(1);
        root1.right = new TreeNode(2);
        root1.right.right = new TreeNode(3);
        root1.right.right.right = new TreeNode(4);
        
        Console.WriteLine("測試案例 1: 不平衡樹 [1,null,2,null,3,null,4]");
        Console.WriteLine("原始樹（中序遍歷）: ");
        List<int> original1 = new List<int>();
        InOrderTraversal(root1, original1);
        Console.WriteLine(string.Join(", ", original1));
        
        TreeNode balanced1 = new Program().BalanceBST(root1);
        Console.WriteLine("平衡後（中序遍歷）: ");
        List<int> result1 = new List<int>();
        InOrderTraversal(balanced1, result1);
        Console.WriteLine(string.Join(", ", result1));
        Console.WriteLine();

        // 測試案例 2: [2,1,3]
        // 已經平衡的二叉搜尋樹
        TreeNode root2 = new TreeNode(2);
        root2.left = new TreeNode(1);
        root2.right = new TreeNode(3);
        
        Console.WriteLine("測試案例 2: 已平衡樹 [2,1,3]");
        Console.WriteLine("原始樹（中序遍歷）: ");
        List<int> original2 = new List<int>();
        InOrderTraversal(root2, original2);
        Console.WriteLine(string.Join(", ", original2));
        
        TreeNode balanced2 = new Program().BalanceBST(root2);
        Console.WriteLine("平衡後（中序遍歷）: ");
        List<int> result2 = new List<int>();
        InOrderTraversal(balanced2, result2);
        Console.WriteLine(string.Join(", ", result2));
    }

    /// <summary>
    /// 將二叉搜尋樹轉換為平衡二叉搜尋樹
    /// 
    /// 解題思路:
    /// 1. 利用中序走訪（In-Order Traversal）將 BST 轉換為有序陣列
    /// 2. 將有序陣列轉換為平衡 BST（選取中間元素作為根節點）
    /// 
    /// 時間複雜度: O(n)，其中 n 是樹中節點的數量
    ///   - 中序遍歷需要 O(n)
    ///   - 建構平衡樹需要 O(n)
    /// 空間複雜度: O(n)，需要儲存所有節點值和遞迴呼叫堆疊
    /// 
    /// 參考資料:
    /// https://leetcode.cn/problems/balance-a-binary-search-tree/solutions/241897/jiang-er-cha-sou-suo-shu-bian-ping-heng-by-leetcod/
    /// https://leetcode.cn/problems/balance-a-binary-search-tree/solutions/150820/shou-si-avlshu-wo-bu-guan-wo-jiu-shi-yao-xuan-zhua/
    /// https://leetcode.cn/problems/balance-a-binary-search-tree/solutions/342529/xian-yong-zhong-xu-bian-li-na-dao-sheng-xu-de-list/
    /// 
    /// AVL Tree — 高度平衡二元搜尋樹
    /// https://tedwu1215.medium.com/avl-tree-%E9%AB%98%E5%BA%A6%E5%B9%B3%E8%A1%A1%E4%BA%8C%E5%85%83%E6%90%9C%E5%B0%8B%E6%A8%B9%E4%BB%8B%E7%B4%B9%E8%88%87%E7%AF%84%E4%BE%8B-15a82c5b778f
    /// 二元搜尋樹 Binary Search Tree
    /// https://zh.wikipedia.org/wiki/%E4%BA%8C%E5%85%83%E6%90%9C%E5%B0%8B%E6%A8%B9
    /// </summary>
    /// <param name="root">二叉搜尋樹的根節點</param>
    /// <returns>平衡後的二叉搜尋樹的根節點</returns>
    public TreeNode BalanceBST(TreeNode root)
    {
        // 步驟 1: 建立儲存有序節點值的列表
        List<int> sortedValues = new List<int>();
        
        // 步驟 2: 透過中序遍歷將 BST 轉換為有序陣列
        // 中序遍歷 BST 會得到遞增序列
        InOrderTraversal(root, sortedValues);
        
        // 步驟 3: 從有序陣列重建平衡 BST
        // 選擇中間元素作為根節點，保證左右子樹高度差不超過 1
        return BuildBalancedBST(0, sortedValues.Count - 1, sortedValues);
    }

    /// <summary>
    /// 中序走訪（In-Order Traversal）將二叉搜尋樹轉換為有序陣列
    /// 
    /// 走訪順序: 左子樹 → 根節點 → 右子樹
    /// 對於 BST，中序遍歷會產生遞增排序的序列
    /// 
    /// 範例:
    ///     2
    ///    / \
    ///   1   3
    /// 中序遍歷結果: [1, 2, 3]
    /// 
    /// 時間複雜度: O(n)，訪問每個節點一次
    /// 空間複雜度: O(n)，用於儲存結果陣列
    /// </summary>
    /// <param name="node">當前訪問的節點</param>
    /// <param name="sortedValues">儲存有序值的列表</param>
    public static void InOrderTraversal(TreeNode node, List<int> sortedValues) 
    {
        // 基礎情況: 空節點直接返回
        if(node is null)
        {
            return;
        }

        // 步驟 1: 遞迴處理左子樹
        InOrderTraversal(node.left, sortedValues);
        
        // 步驟 2: 處理當前節點（加入結果列表）
        sortedValues.Add(node.val);
        
        // 步驟 3: 遞迴處理右子樹
        InOrderTraversal(node.right, sortedValues);
    }

    /// <summary>
    /// 從有序陣列遞迴建立平衡二叉搜尋樹
    /// 
    /// 核心思路:
    /// 選擇中間元素作為根節點，確保左右子樹節點數量相近，從而保證樹的平衡性
    /// 
    /// 範例:
    /// sortedValues = [1, 2, 3, 4, 5]
    /// 
    /// 步驟 1: mid = 2, root = 3
    /// 步驟 2: 左子樹 [1, 2], 右子樹 [4, 5]
    /// 步驟 3: 遞迴建立左右子樹
    /// 
    /// 結果樹:
    ///     3
    ///    / \
    ///   2   4
    ///  /     \
    /// 1       5
    /// 
    /// 時間複雜度: O(n)，每個節點建立一次
    /// 空間複雜度: O(log n)，遞迴呼叫堆疊深度
    /// </summary>
    /// <param name="start">有序陣列的起始索引</param>
    /// <param name="end">有序陣列的結束索引</param>
    /// <param name="sortedValues">有序的節點值陣列</param>
    /// <returns>建立的平衡 BST 的根節點</returns>
    public static TreeNode BuildBalancedBST(int start, int end, List<int> sortedValues)
    {
        // 基礎情況 1: 只有一個元素，直接建立節點並返回
        if(start == end)
        {
            return new TreeNode(sortedValues[start]);
        }
        // 基礎情況 2: 範圍無效（start > end），返回 null
        else if(start > end)
        {
            return null;
        }
        
        // 步驟 1: 計算中間索引（避免溢位的寫法）
        // 使用 start + (end - start) / 2 而非 (start + end) / 2
        int mid = start + (end - start) / 2;
        
        // 步驟 2: 以中間元素建立根節點
        TreeNode node = new TreeNode(sortedValues[mid]);
        
        // 步驟 3: 遞迴建立左子樹（使用左半部分的元素）
        node.left = BuildBalancedBST(start, mid - 1, sortedValues);
        
        // 步驟 4: 遞迴建立右子樹（使用右半部分的元素）
        node.right = BuildBalancedBST(mid + 1, end, sortedValues);

        return node;
    }
}
