﻿namespace leetcode_572;

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

    /// <summary>
    /// 572. Subtree of Another Tree
    /// https://leetcode.com/problems/subtree-of-another-tree/description/?envType=problem-list-v2&envId=oizxjoit
    /// 572. 另一棵樹的子樹
    /// https://leetcode.cn/problems/subtree-of-another-tree/description/
    /// 題目描述：
    /// 給定兩個二元樹 root 和 subRoot，判斷 subRoot 是否為 root 的子樹。
    /// 子樹必須包含其所有後代節點，結構完全相同。
    /// 
    /// 限制條件：
    /// - root 樹的節點數範圍為 [1, 2000]
    /// - subRoot 樹的節點數範圍為 [1, 1000]
    /// - -10^4 <= root.val <= 10^4
    /// - -10^4 <= subRoot.val <= 10^4
    /// 
    /// 建議先去寫 100. Same Tree, 再過來解這題.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試案例 1: [3,4,5,1,2] 與 [4,1,2]
        TreeNode root1 = new TreeNode(3);
        root1.left = new TreeNode(4);
        root1.right = new TreeNode(5);
        root1.left.left = new TreeNode(1);
        root1.left.right = new TreeNode(2);

        TreeNode subRoot1 = new TreeNode(4);
        subRoot1.left = new TreeNode(1);
        subRoot1.right = new TreeNode(2);

        Console.WriteLine("Test Case 1 - Expected: True, Result: " + IsSubtree(root1, subRoot1));

        // 測試案例 2: [3,4,5,1,2,null,null,null,null,0] 與 [4,1,2]
        TreeNode root2 = new TreeNode(3);
        root2.left = new TreeNode(4);
        root2.right = new TreeNode(5);
        root2.left.left = new TreeNode(1);
        root2.left.right = new TreeNode(2);
        root2.left.right.left = new TreeNode(0);

        TreeNode subRoot2 = new TreeNode(4);
        subRoot2.left = new TreeNode(1);
        subRoot2.right = new TreeNode(2);

        Console.WriteLine("Test Case 2 - Expected: False, Result: " + IsSubtree(root2, subRoot2));
    }

    /// <summary>
    /// 解題思路：
    /// 1. 遞迴檢查root樹中是否包含與subRoot相同的子樹
    /// 2. 遞迴過程分為三個部分：
    ///    - 檢查當前root樹是否與subRoot相同
    ///    - 檢查root的左子樹是否包含subRoot
    ///    - 檢查root的右子樹是否包含subRoot
    /// 3. 時間複雜度：O(m*n)，其中m是root樹的節點數，n是subRoot樹的節點數
    /// </summary>
    /// <param name="root">主樹的根節點</param>
    /// <param name="subRoot">需要搜尋的子樹的根節點</param>
    /// <returns>如果subRoot是root的子樹則返回true，否則返回false</returns>
    public static bool IsSubtree(TreeNode root, TreeNode subRoot)
    {
        // 如果子樹為空，根據定義，空樹是任何樹的子樹
        if (subRoot == null) 
        {
            return true;
        }
        // 如果主樹為空但子樹不為空，返回false
        if (root == null) 
        {
            return false;
        }
        // 檢查當前樹是否與子樹相同
        if (isSameTree(root, subRoot)) 
        {
            return true;
        }
        // 遞迴檢查左右子樹; 注意這邊是 "||"，因為只要有一個子樹相同就可以了
        // 這裡的root.left和root.right是當前樹的左子樹和右子樹
        return IsSubtree(root.left, subRoot) || IsSubtree(root.right, subRoot);
    }

    /// <summary>
    /// 這邊function 從 100. Same Tree 複製過來.
    /// 
    /// 判斷兩棵二元樹是否完全相同的輔助函數
    /// 解題思路：
    /// 1. 使用遞迴方式，同時遍歷兩棵樹的對應節點
    /// 2. 遞迴終止條件：
    ///    - 當兩個節點都是 null 時，表示到達葉子節點之後，返回 true
    ///    - 當其中一個節點是 null 時，表示結構不同，返回 false
    ///    - 當兩個節點值不相等時，返回 false
    /// 3. 遞迴處理：
    ///    - 同時檢查左子樹是否相同 (p.left 和 q.left)
    ///    - 同時檢查右子樹是否相同 (p.right 和 q.right)
    ///    - 只有當左右子樹都相同時，才返回 true
    /// 4. 時間複雜度：O(min(m,n))，空間複雜度：O(min(h1,h2))，其中 h1,h2 為樹高
    /// </summary>
    /// <param name="p">第一棵樹的當前節點</param>
    /// <param name="q">第二棵樹的當前節點</param>
    /// <returns>若兩棵樹結構和節點值完全相同則返回 true，否則返回 false</returns>
    private static bool isSameTree(TreeNode p, TreeNode q)
    {
        // 基礎案例 1：兩節點都為 null，表示已到達葉子節點之後
        if (p == null && q == null)
        {
            return true;
        }
        // 基礎案例 2：其中一個節點為 null，表示樹的結構不同
        else if (p == null || q == null)
        {
            return false;
        }
        // 基礎案例 3：節點值不相等，表示樹不同
        else if (p.val != q.val)
        {
            return false;
        }
        // 遞迴案例：同時比較左右子樹
        else
        {
            // 只有當左右子樹都相同時，才返回 true
            return isSameTree(p.left, q.left) && isSameTree(p.right, q.right);
        }
    }
}
