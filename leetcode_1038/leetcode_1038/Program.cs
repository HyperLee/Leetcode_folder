namespace leetcode_1038
{
    internal class Program
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
        /// 1038. Binary Search Tree to Greater Sum Tree
        /// https://leetcode.com/problems/binary-search-tree-to-greater-sum-tree/description/?envType=daily-question&envId=2024-06-25
        /// 1038. 从二叉搜索树到更大和树
        /// https://leetcode.cn/problems/binary-search-tree-to-greater-sum-tree/description/
        /// 
        /// 樹的走訪
        /// https://zh.wikipedia.org/zh-tw/%E6%A0%91%E7%9A%84%E9%81%8D%E5%8E%86
        /// 
        /// 前序走訪: 前序走訪（Pre-Order Traversal）是依序以根節點、左節點、右節點為順序走訪的方式。
        /// 中序走訪: 中序走訪（In-Order Traversal）是依序以左節點、根節點、右節點為順序走訪的方式。 
        /// 後序走訪: 後序走訪（Post-Order Traversal）是依序以左節點、右節點、根節點為順序走訪的方式。
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(4);
            root.left = new TreeNode(1);
            root.right = new TreeNode(6);
            root.left.left = new TreeNode(0);
            root.left.right = new TreeNode(2);
            root.right.left = new TreeNode(5);
            root.right.right = new TreeNode(7);
            root.left.right.right = new TreeNode(3);
            root.right.right.right = new TreeNode(8);

            Console.WriteLine(BstToGst(root));
            Console.ReadKey();
        }

        public static int presum = 0;


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/binary-search-tree-to-greater-sum-tree/solutions/421601/cong-er-cha-sou-suo-shu-dao-geng-da-he-shu-by-leet/
        /// https://leetcode.cn/problems/binary-search-tree-to-greater-sum-tree/solutions/2552797/jian-ji-xie-fa-li-yong-er-cha-sou-suo-sh-r5zm/
        /// https://leetcode.cn/problems/binary-search-tree-to-greater-sum-tree/solutions/2552959/gong-shui-san-xie-bst-de-zhong-xu-bian-l-vtu1/
        /// https://leetcode.cn/problems/binary-search-tree-to-greater-sum-tree/solutions/1461253/1038-ba-er-cha-sou-suo-shu-zhuan-huan-we-2r8o/
        /// 
        /// 二元搜尋樹
        /// 以root來區分
        /// 左邊小
        /// 右邊大
        /// 
        /// 正常的中序 會是 遞增 也就是取道小的, 這不是我們要的
        /// 
        /// 把中序給反向
        /// 變成
        /// 右 中 左 
        /// 原本中序 數值是遞增
        /// 反向就會是 遞減
        /// 這樣就可以拿到 前一個數值 是比較大的
        /// 然後累加目前的node.val
        /// 
        /// 題目要求 節點值加上 比節點還要大的值( 比node大簡單說就是要取右子樹)
        /// 透過反向中序 即可 達成
        /// 
        /// 反向就是取大值
        /// 也就是 node + presum = new node value
        /// 
        /// 從右子樹開始累計node.val
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static TreeNode BstToGst(TreeNode root)
        {
            if(root == null)
            {
                return root;
            }

            // 遞迴右子樹
            BstToGst(root.right);
            presum += root.val;

            // 此時 presum 就是 >= node.val 的所有數之和
            root.val = presum;

            // 遞迴左子樹
            BstToGst(root.left);
             
            return root;
        }

    }
}
