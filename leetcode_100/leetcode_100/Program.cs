namespace leetcode_100;

class Program
{
    /// <summary>
    /// 
    /// </summary>
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
    /// 100. Same Tree
    /// https://leetcode.com/problems/same-tree/description/
    /// 
    /// 100. 相同的树
    /// https://leetcode.cn/problems/same-tree/description/
    /// 
    /// 題目描述：
    /// 給定兩個二元樹的根節點 p 和 q，編寫一個函數來檢查它們是否相同。
    /// 如果兩個樹在結構上相同，並且節點具有相同的值，則認為它們是相同的。
    /// 
    /// 範例 1：
    /// 輸入：p = [1,2,3], q = [1,2,3]
    /// 輸出：true
    /// 
    /// 範例 2：
    /// 輸入：p = [1,2], q = [1,null,2]
    /// 輸出：false
    /// 
    /// 範例 3：
    /// 輸入：p = [1,2,1], q = [1,1,2]
    /// 輸出：false
    /// 
    /// 限制條件：
    /// - 樹中節點數目在範圍 [0, 100] 內
    /// - -10^4 <= Node.val <= 10^4
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        TreeNode p= new TreeNode(1);
        p.left = new TreeNode(2);
        p.right = new TreeNode(3);

        TreeNode q = new TreeNode(1);
        q.left = new TreeNode(2);
        q.right = new TreeNode(3);

        Console.WriteLine("ans: " + IsSameTree(p, q));
    }


    /// <summary>
    /// 解題思路：
    /// 1. 使用遞迴方式，同時比較兩棵樹的節點
    /// 2. 判斷順序：
    ///    - 先檢查節點是否同時為 null (代表該分支完全相同)
    ///    - 再檢查是否其中一個節點為 null (代表結構不同)
    ///    - 接著比較節點值是否相等
    ///    - 最後遞迴比較左右子樹
    /// 3. 時間複雜度 O(min(p,q)) - 只需遍歷到較小的樹的節點數
    /// 4. 空間複雜度 O(min(p,q)) - 遞迴調用的棧空間
    /// 
    /// </summary>
    /// <param name="p">trees p</param>
    /// <param name="q">trees q</param>
    /// <returns>如果兩樹相同返回 true，否則返回 false</returns>
    public static bool IsSameTree(TreeNode p, TreeNode q)
    {
        if (p == null && q == null)
        {
            // 兩樹 root 皆為空
            return true;
        }
        else if (p == null || q == null)
        {
            // 兩樹 root 其中一個為空
            return false;
        }
        else if (p.val != q.val)
        {
            // 兩樹 root 都不為空 就比較 root 的數值是否一樣
            return false;
        }
        else
        {
            // 比較兩樹 各自的左右子樹
            return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }
    }
}
