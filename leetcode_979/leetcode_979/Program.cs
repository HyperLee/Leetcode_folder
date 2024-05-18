namespace leetcode_979
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
        /// 979. Distribute Coins in Binary Tree
        /// https://leetcode.com/problems/distribute-coins-in-binary-tree/description/?envType=daily-question&envId=2024-05-18
        /// 979. 在二叉树中分配硬币
        /// https://leetcode.cn/problems/distribute-coins-in-binary-tree/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(3);
            root.left = new TreeNode(0);
            root.right = new TreeNode(0);

            Console.WriteLine(DistributeCoins(root));
            Console.ReadKey();
        }

        static int ans = 0;

        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/distribute-coins-in-binary-tree/solutions/2339545/zai-er-cha-shu-zhong-fen-pei-ying-bi-by-e4poq/
        /// https://leetcode.cn/problems/distribute-coins-in-binary-tree/solutions/2343262/tu-jie-mei-you-si-lu-jin-lai-miao-dong-p-vrni/
        /// https://leetcode.cn/problems/distribute-coins-in-binary-tree/solutions/1461471/979-zai-er-cha-shu-zhong-fen-pei-ying-bi-eueu/
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int DistributeCoins(TreeNode root)
        {
            DFS(root);
            return ans;
        }


        /// <summary>
        /// node.val - 1
        /// => 根結點本身所含硬幣數量 扣除 根結點 node值為1
        /// 得到數值極為需要移動之硬幣數量
        /// 
        /// 簡單說就是 
        /// coins - node 即為移動次數
        /// 詳細解說可看上述連結
        /// 連結2有圖示說明
        /// 以及入門與進階解法兩種說明
        /// 
        /// 
        /// DFS取出每個node硬幣數量
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int DFS(TreeNode node)
        {
            if(node == null)
            {
                return 0;
            }

            // 累計左子樹 + 右子樹 + (根節點 - 1)
            //int d = DFS(node.left) + DFS(node.right) + node.val - 1;
            //ans += Math.Abs(d);
            
            // 左子樹總和
            int sumleft = DFS(node.left);
            // 右子樹總和
            int sumright = DFS(node.right);
            // 累計左子樹 + 右子樹 + (根節點 - 1)
            ans += Math.Abs(sumleft) + Math.Abs(sumright) + node.val - 1;

            return sumleft + sumright + node.val - 1;
        }

    }
}
