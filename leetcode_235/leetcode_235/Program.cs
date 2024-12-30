namespace leetcode_235
{
    internal class Program
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }


        /// <summary>
        /// 235. Lowest Common Ancestor of a Binary Search Tree
        /// https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-search-tree/description/
        /// 
        /// 235. 二叉搜索树的最近公共祖先
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-search-tree/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(6);
            root.left = new TreeNode(2);
            root.right = new TreeNode(8);

            root.left.left = new TreeNode(0);
            root.left.right = new TreeNode(4);

            root.left.right.left = new TreeNode(3);
            root.left.right.right = new TreeNode(5);

            root.right.left = new TreeNode(7);
            root.right.right = new TreeNode(9);

            TreeNode p = new TreeNode(2);
            TreeNode q = new TreeNode(8);

            // output root.val 
            Console.WriteLine("res: " + LowestCommonAncestor(root, p, q).val);
        }


        /// <summary>
        /// ref: 
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-search-tree/solutions/428633/er-cha-sou-suo-shu-de-zui-jin-gong-gong-zu-xian-26/
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-search-tree/solutions/2023873/zui-jin-gong-gong-zu-xian-yi-ge-shi-pin-8h2zc/
        /// https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-search-tree/solutions/1456138/235-er-cha-sou-suo-shu-de-zui-jin-gong-g-lccn/
        /// 
        /// 二元搜尋樹 (BST) 的基本概念
        /// 二元搜尋樹是一種二元樹，其中每個節點最多有兩個子節點，且左子節點的值小於父節點的值，右子節點的值大於父節點的值。
        /// 左子樹中所有節點的值都小於根節點的值。
        /// 右子樹中所有節點的值都大於根節點的值。
        /// 每個子樹也是一個二元搜尋樹。
        /// 
        ///  -- 簡單說 --
        /// 數值小的 node.val 都在 root 左邊子樹
        /// 數值大的 node.val 都在 root 右邊子樹
        /// 
        /// 本方法會一次性遍歷 p, q 兩節點
        /// 分開遍歷比較耗時
        /// 簡單說明流程
        /// 1.從根結點開始遍歷
        /// 2.如果當前節點值大於 p 和 q 的值, 說明 p 和 q 應該在當前節點的左子樹, 因此將當前節點移動到他的左子樹子節點
        /// 3.如果當前節點值小於 p 和 q 的值, 說明 p 和 q 應該在當前節點的右子樹, 因此將當前節點移動到他的右子樹子節點
        /// 4.如果當前節點的值不滿足上述兩條件要求, 那麼說明當前節點就是"分岔點", 此時 p 和 q 要麼在當前節點的不同子樹中, 要麼其中一個節點就是當前節點
        /// 4-1: 
        /// p 和 q 分別在左右子樹 ||
        /// 當前節點是 p          || => 返回當前節點
        /// 當前節點是 q          || => 返回當前節點
        /// 
        /// 題目說明:
        /// 1. 所有節點的值都是唯一
        /// 2. p, q 為不同節點且均從在於給定的 BST 中
        /// ==> 保證存在以及唯一性且不為空
        /// 
        /// 時間複雜度: O(n), n 為 BST 節點數量
        /// 空間複雜度: O(1)
        /// </summary>
        /// <param name="root"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            int x = root.val;

            if(p.val < x && q.val < x)
            {
                // p 和 q 都在左子樹
                return LowestCommonAncestor(root.left, p, q);
            }

            if(p.val > x && q.val > x)
            {
                // p 和 q 都在右子樹
                return LowestCommonAncestor(root.right, p, q);
            }

            // 其他
            return root;
        }
    }
}
