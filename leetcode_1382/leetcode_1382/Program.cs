namespace leetcode_1382
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
        /// 1382. Balance a Binary Search Tree
        /// https://leetcode.com/problems/balance-a-binary-search-tree/description/?envType=daily-question&envId=2024-06-26
        /// 1382. 将二叉搜索树变平衡
        /// https://leetcode.cn/problems/balance-a-binary-search-tree/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.right = new TreeNode(2);
            root.right.right = new TreeNode(3);
            root.right.right.right = new TreeNode(4);

            BalanceBST(root);

            //var res = BalanceBST(root);
            //Console.WriteLine("Ans:" + res.val);
            //while (res != null)
            //{
            //    Console.WriteLine("Ans:" + res.val);
            //    //res = res.next;
            //}

            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// 
        /// https://leetcode.cn/problems/balance-a-binary-search-tree/solutions/241897/jiang-er-cha-sou-suo-shu-bian-ping-heng-by-leetcod/
        /// https://leetcode.cn/problems/balance-a-binary-search-tree/solutions/150820/shou-si-avlshu-wo-bu-guan-wo-jiu-shi-yao-xuan-zhua/
        /// https://leetcode.cn/problems/balance-a-binary-search-tree/solutions/342529/xian-yong-zhong-xu-bian-li-na-dao-sheng-xu-de-list/
        /// 
        /// AVL Tree — 高度平衡二元搜尋樹
        /// https://tedwu1215.medium.com/avl-tree-%E9%AB%98%E5%BA%A6%E5%B9%B3%E8%A1%A1%E4%BA%8C%E5%85%83%E6%90%9C%E5%B0%8B%E6%A8%B9%E4%BB%8B%E7%B4%B9%E8%88%87%E7%AF%84%E4%BE%8B-15a82c5b778f
        /// 二元搜尋樹 Binary Search Tree
        /// https://zh.wikipedia.org/wiki/%E4%BA%8C%E5%85%83%E6%90%9C%E5%B0%8B%E6%A8%B9
        /// 
        /// 題目會給予一棵樹
        /// 然後我們要改造成 平衡樹, 也就是左右樹(包含子樹)高度差距不能超過1
        /// 
        /// 常規作法:
        /// 所以我們先利用 中序
        /// 找出 升序 排序
        /// 放入 list 裡面暫存
        /// 然後再從 list 把資料取出, 利用CreateNodeFromList 重新建立一棵樹
        /// 中間節點為root
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static TreeNode BalanceBST(TreeNode root)
        {
            // 排序後, 放置這邊暫存
            List<int> sortedValues = new List<int>();

            // 利用中序 把資料排序放到list 暫存
            InOrderTraversal(root, sortedValues);
            // 從list取出資料建立 平衡樹
            return BuildBalancedBST(0, sortedValues.Count - 1, sortedValues);
        }


        /// <summary>
        /// 中序走訪（In-Order Traversal）是依序以左節點、根節點、右節點為順序走訪的方式。 
        /// 
        /// 1.利用中序 來做排序, 排序後將資料放入 list 裡面暫存
        /// Convert the tree to a sorted array using an in-order traversal.
        /// </summary>
        /// <param name="root"></param>
        public static void InOrderTraversal(TreeNode node, List<int> sortedValues) 
        {
            if(node == null)
            {
                return;
            }

            InOrderTraversal(node.left, sortedValues);
            sortedValues.Add(node.val);
            InOrderTraversal(node.right, sortedValues);

        }


        /// <summary>
        /// 2.從list取出資料建立平衡樹, list列表中間值為root
        /// Construct a new balanced tree from the sorted array recursively.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static TreeNode BuildBalancedBST(int start, int end, List<int> sortedValues)
        {
            if(start == end)
            {
                // 不增加這判斷也可以
                // 加了算是優化
                return new TreeNode(sortedValues[start]);
            }

            if(start > end)
            {
                return null;
            }

            // 中間節點為root
            int mid = (end - start) / 2 + start;
            TreeNode node = new TreeNode(sortedValues[mid]);

            // 遞迴建立 左右子樹
            node.left = BuildBalancedBST(start, mid - 1, sortedValues);
            node.right = BuildBalancedBST(mid + 1, end, sortedValues);
            
            //Console.WriteLine("Ans:" + node.val);
            
            return node;
        }


    }
}
