namespace leetcode_669;

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
    /// 669. Trim a Binary Search Tree
    /// https://leetcode.com/problems/trim-a-binary-search-tree/description/
    /// https://leetcode.cn/problems/trim-a-binary-search-tree/description/
    ///
    /// English:
    /// Given the root of a binary search tree and the lowest and highest boundaries as
    /// low and high, trim the tree so that all its elements lie in [low, high]. Trimming
    /// the tree should not change the relative structure of the elements that will
    /// remain in the tree (i.e., any node's descendant should remain a descendant).
    /// It can be proven that there is a unique answer. Return the root of the trimmed
    /// binary search tree. Note that the root may change depending on the given boundaries.
    ///
    /// 繁體中文：
    /// 給定一個二叉搜尋樹（BST）的根節點，以及下界 low 與上界 high，將樹進行修剪，使得所有節點值都位於 [low, high] 範圍內。
    /// 修剪後不應改變剩餘節點之間的相對結構（即某節點的任一子孫仍須保持為該節點的子孫）。已證明答案是唯一的。
    /// 回傳修剪後的 BST 的根節點，注意根節點可能會因給定範圍而改變。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 範例 1: root = [1,0,2], low = 1, high = 2
        // 預期輸出: [1,null,2]
        var root1 = new TreeNode(1,
            new TreeNode(0),
            new TreeNode(2));
        var result1 = program.TrimBST(root1, 1, 2);
        Console.WriteLine("範例 1:");
        PrintTree(result1);

        // 範例 2: root = [3,0,4,null,2,null,null,1], low = 1, high = 3
        // 預期輸出: [3,2,null,1]
        var root2 = new TreeNode(3,
            new TreeNode(0,
                null,
                new TreeNode(2,
                    new TreeNode(1),
                    null)),
            new TreeNode(4));
        var result2 = program.TrimBST(root2, 1, 3);
        Console.WriteLine("\n範例 2:");
        PrintTree(result2);

        // 範例 3: root = [5,3,6,2,4,null,7,1], low = 2, high = 5
        // 預期輸出: [5,3,null,2,4,1]
        var root3 = new TreeNode(5,
            new TreeNode(3,
                new TreeNode(2,
                    new TreeNode(1),
                    null),
                new TreeNode(4)),
            new TreeNode(6,
                null,
                new TreeNode(7)));
        var result3 = program.TrimBST(root3, 2, 5);
        Console.WriteLine("\n範例 3:");
        PrintTree(result3);
    }

    /// <summary>
    /// 以層序遍歷方式印出二元樹結構
    /// </summary>
    /// <param name="root">樹的根節點</param>
    static void PrintTree(TreeNode? root)
    {
        if (root is null)
        {
            Console.WriteLine("[]");
            return;
        }

        var result = new List<string>();
        var queue = new Queue<TreeNode?>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            if (node is null)
            {
                result.Add("null");
            }
            else
            {
                result.Add(node.val.ToString());
                queue.Enqueue(node.left);
                queue.Enqueue(node.right);
            }
        }

        // 移除尾端的 null
        while (result.Count > 0 && result[^1] == "null")
        {
            result.RemoveAt(result.Count - 1);
        }

        Console.WriteLine($"[{string.Join(", ", result)}]");
    }

    /// <summary>
    /// 修剪二叉搜尋樹（Trim BST）
    /// 
    /// 解題思路：
    /// 利用 BST（二叉搜尋樹）的特性：左子樹所有節點值 < 根節點值 < 右子樹所有節點值
    /// 
    /// 遞迴策略：
    /// 1. 若當前節點值 < low：表示當前節點及其左子樹都不在範圍內，應往右子樹尋找有效節點
    /// 2. 若當前節點值 > high：表示當前節點及其右子樹都不在範圍內，應往左子樹尋找有效節點
    /// 3. 若當前節點值在 [low, high] 範圍內：保留此節點，並遞迴修剪其左右子樹
    /// 
    /// 時間複雜度：O(n)，其中 n 為樹中節點數量，最壞情況需遍歷所有節點
    /// 空間複雜度：O(h)，其中 h 為樹的高度，遞迴呼叫堆疊深度
    /// </summary>
    /// <param name="root">二叉搜尋樹的根節點</param>
    /// <param name="low">範圍下界（含）</param>
    /// <param name="high">範圍上界（含）</param>
    /// <returns>修剪後的二叉搜尋樹根節點</returns>
    public TreeNode? TrimBST(TreeNode? root, int low, int high)
    {
        // 基本情況：若節點為空，直接返回 null
        if (root is null)
        {
            return null;
        }

        // 情況 1：當前節點值小於下界
        // 根據 BST 特性，當前節點及其左子樹全部小於 low，都不符合條件
        // 因此捨棄當前節點和左子樹，往右子樹尋找符合條件的節點
        if (root.val < low)
        {
            return TrimBST(root.right, low, high);
        }

        // 情況 2：當前節點值大於上界
        // 根據 BST 特性，當前節點及其右子樹全部大於 high，都不符合條件
        // 因此捨棄當前節點和右子樹，往左子樹尋找符合條件的節點
        if (root.val > high)
        {
            return TrimBST(root.left, low, high);
        }

        // 情況 3：當前節點值在 [low, high] 範圍內，保留此節點
        // 遞迴修剪左子樹，確保左子樹所有節點都在範圍內
        root.left = TrimBST(root.left, low, high);
        // 遞迴修剪右子樹，確保右子樹所有節點都在範圍內
        root.right = TrimBST(root.right, low, high);

        return root;
    }

    /// <summary>
    /// 迭代法修剪二叉搜尋樹（Trim BST）
    /// 
    /// 解題思路：
    /// 迭代法分為三個階段：
    /// 1. 找到符合條件的根節點：不斷調整根節點，直到其值在 [low, high] 範圍內
    /// 2. 修剪左子樹：對於每個節點，如果其左子節點值 < low，則用左子節點的右子樹替換
    /// 3. 修剪右子樹：對於每個節點，如果其右子節點值 > high，則用右子節點的左子樹替換
    /// 
    /// 核心概念：
    /// - 當左子節點值 < low 時，該左子節點及其左子樹都不符合條件，但其右子樹可能符合
    /// - 當右子節點值 > high 時，該右子節點及其右子樹都不符合條件，但其左子樹可能符合
    /// 
    /// 時間複雜度：O(n)，其中 n 為樹中節點數量
    /// 空間複雜度：O(1)，只使用常數額外空間，無遞迴呼叫堆疊
    /// </summary>
    /// <param name="root">二叉搜尋樹的根節點</param>
    /// <param name="low">範圍下界（含）</param>
    /// <param name="high">範圍上界（含）</param>
    /// <returns>修剪後的二叉搜尋樹根節點</returns>
    public TreeNode? TrimBST2(TreeNode? root, int low, int high)
    {
        // 階段 1：找到符合條件的根節點
        // 不斷調整根節點，直到其值在 [low, high] 範圍內
        while (root is not null && (root.val < low || root.val > high))
        {
            if (root.val < low)
            {
                // 當前根節點值太小，往右子樹尋找
                root = root.right;
            }
            else
            {
                // 當前根節點值太大，往左子樹尋找
                root = root.left;
            }
        }

        // 若找不到符合條件的根節點，返回 null
        if (root is null)
        {
            return null;
        }

        // 階段 2：修剪左子樹
        // 從根節點開始，向左遍歷並修剪不符合條件的節點
        for (TreeNode node = root; node.left is not null;)
        {
            if (node.left.val < low)
            {
                // 左子節點值太小，用其右子樹替換
                // 因為左子節點的左子樹都小於 low，而右子樹可能有符合條件的節點
                node.left = node.left.right;
            }
            else
            {
                // 左子節點符合條件，繼續向左遍歷
                node = node.left;
            }
        }

        // 階段 3：修剪右子樹
        // 從根節點開始，向右遍歷並修剪不符合條件的節點
        for (TreeNode node = root; node.right is not null;)
        {
            if (node.right.val > high)
            {
                // 右子節點值太大，用其左子樹替換
                // 因為右子節點的右子樹都大於 high，而左子樹可能有符合條件的節點
                node.right = node.right.left;
            }
            else
            {
                // 右子節點符合條件，繼續向右遍歷
                node = node.right;
            }
        }

        return root;
    }
}
