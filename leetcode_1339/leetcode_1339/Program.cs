namespace leetcode_1339;

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
    /// 1339. Maximum Product of Splitted Binary Tree
    /// https://leetcode.com/problems/maximum-product-of-splitted-binary-tree/description/?envType=daily-question&envId=2026-01-07
    /// 1339. 分裂二叉树的最大乘积
    /// https://leetcode.cn/problems/maximum-product-of-splitted-binary-tree/description/?envType=daily-question&envId=2026-01-07
    /// 
    /// 描述 (繁體中文翻譯):
    /// 給定一個二叉樹的根節點，移除一條邊把二叉樹分成兩個子樹，使得兩個子樹節點值之和的乘積最大化。
    /// 返回兩個子樹節點和的最大乘積。由於答案可能很大，請將結果對 10^9 + 7 取餘。
    /// 注意：應先最大化乘積再對 10^9 + 7 取餘，而不是在取餘後再最大化。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試範例 1: [1,2,3,4,5,6]
        // 樹結構:
        //       1
        //      / \
        //     2   3
        //    / \   \
        //   4   5   6
        TreeNode root1 = new TreeNode(1,
            new TreeNode(2,
                new TreeNode(4),
                new TreeNode(5)),
            new TreeNode(3,
                null!,
                new TreeNode(6)));
        
        Program solution1 = new Program();
        int result1 = solution1.MaxProduct(root1);
        Console.WriteLine($"測試範例 1 結果: {result1}"); // 預期輸出: 110
        
        // 測試範例 2: [1,null,2,3,4,null,null,5,6]
        // 樹結構:
        //     1
        //      \
        //       2
        //      / \
        //     3   4
        //    / \
        //   5   6
        TreeNode root2 = new TreeNode(1,
            null!,
            new TreeNode(2,
                new TreeNode(3,
                    new TreeNode(5),
                    new TreeNode(6)),
                new TreeNode(4)));
        
        Program solution2 = new Program();
        int result2 = solution2.MaxProduct(root2);
        Console.WriteLine($"測試範例 2 結果: {result2}"); // 預期輸出: 90
    }

    private int sum = 0;
    private int best = 0;

    /// <summary>
    /// 計算分裂二叉樹後兩個子樹節點和的最大乘積
    /// 
    /// 解題思路：
    /// 1. 首先計算整棵樹的總和 sum
    /// 2. 然後遍歷每個節點，計算以該節點為根的子樹和 cur
    /// 3. 當移除某條邊後，一個子樹的和為 cur，另一個子樹的和為 sum - cur
    /// 4. 乘積為 cur * (sum - cur)，我們要找到使這個乘積最大的 cur
    /// 5. 根據均值不等式，cur 越接近 sum/2，乘積越大
    /// 6. 因此我們只需找到最接近 sum/2 的子樹和
    /// 
    /// 時間複雜度：O(n)，n 為節點數量，需要遍歷兩次樹
    /// 空間複雜度：O(h)，h 為樹的高度，遞迴呼叫堆疊空間
    /// </summary>
    /// <param name="root">二叉樹的根節點</param>
    /// <returns>兩個子樹節點和的最大乘積，對 10^9+7 取餘</returns>
    public int MaxProduct(TreeNode root)
    {
        // 第一次 DFS：計算整棵樹的總和
        DFS(root);
        
        // 第二次 DFS：找到最接近 sum/2 的子樹和
        DFS2(root);
        
        // 計算最大乘積並對 10^9+7 取餘
        // 使用 long 避免溢位，因為 best 和 (sum - best) 的乘積可能超過 int 範圍
        return (int)((long)best * (sum - best) % 1000000007);
    }

    /// <summary>
    /// 第一次深度優先搜尋：計算整棵二叉樹所有節點值的總和
    /// 
    /// 這個函式會遍歷整棵樹，將每個節點的值累加到成員變數 sum 中。
    /// sum 的值將用於後續計算分裂後的子樹乘積。
    /// </summary>
    /// <param name="node">當前遍歷的節點</param>
    private void DFS(TreeNode node)
    {
        // 遞迴終止條件：節點為空
        if(node == null)
        {
            return;
        }
        
        // 將當前節點的值加到總和中
        sum += node.val;
        
        // 遞迴遍歷左子樹
        DFS(node.left);
        
        // 遞迴遍歷右子樹
        DFS(node.right);
    }

    /// <summary>
    /// 第二次深度優先搜尋：計算每個子樹的節點和，並找出最接近 sum/2 的子樹和
    /// 
    /// 這個函式通過後序遍歷（先左後右再根）計算每個子樹的和。
    /// 對於每個子樹和 cur，我們檢查它是否比當前的 best 更接近 sum/2。
    /// 
    /// 判斷邏輯：
    /// - cur 越接近 sum/2，|cur * 2 - sum| 的值越小
    /// - 如果 |cur * 2 - sum| < |best * 2 - sum|，說明 cur 更接近 sum/2
    /// - 此時更新 best = cur
    /// </summary>
    /// <param name="node">當前遍歷的節點</param>
    /// <returns>以當前節點為根的子樹的節點值總和</returns>
    private int DFS2(TreeNode node)
    {
        // 遞迴終止條件：節點為空，返回 0
        if(node == null)
        {
            return 0;
        }

        // 計算當前子樹的總和 = 左子樹和 + 右子樹和 + 當前節點值
        int cur = DFS2(node.left) + DFS2(node.right) + node.val;
        
        // 檢查當前子樹和是否比 best 更接近 sum/2
        // |cur * 2 - sum| 表示 cur 與 sum/2 的距離（放大2倍避免浮點運算）
        if(Math.Abs(cur * 2 - sum) < Math.Abs(best * 2 - sum))
        {
            // 更新最接近 sum/2 的子樹和
            best = cur;
        }
        
        // 返回當前子樹的總和，供父節點使用
        return cur;
    }
}
