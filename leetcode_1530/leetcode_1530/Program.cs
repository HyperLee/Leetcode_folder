namespace leetcode_1530
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
        /// 1530. Number of Good Leaf Nodes Pairs
        /// https://leetcode.com/problems/number-of-good-leaf-nodes-pairs/description/?envType=daily-question&envId=2024-07-18
        /// 1530. 好叶子节点对的数量
        /// https://leetcode.cn/problems/number-of-good-leaf-nodes-pairs/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);

            root.left.right = new TreeNode(4);

            int distance = 3;

            Console.WriteLine(CountPairs(root, distance));
            Console.ReadKey();
        }

        // 計算 有幾對
        static int pairs = 0;

        /// <summary>
        /// ref:
        /// 
        /// https://leetcode.cn/problems/number-of-good-leaf-nodes-pairs/solutions/357905/hao-xie-zi-jie-dian-dui-de-shu-liang-by-leetcode-s/
        /// https://leetcode.cn/problems/number-of-good-leaf-nodes-pairs/solutions/347315/good-leaf-nodes-pairs-by-ikaruga/
        /// https://leetcode.cn/problems/number-of-good-leaf-nodes-pairs/solutions/1461559/1530-hao-xie-zi-jie-dian-dui-de-shu-lian-wltu/
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static int CountPairs(TreeNode root, int distance)
        {
            GetDistances(root,distance);

            return pairs;
        }



        /// <summary>
        /// 1. node為空 無距離
        /// 2. node無左右子樹, 距離為0 ( node到葉節點距離為0 )
        /// 3. 分別計算左子樹距離與右子樹距離
        ///    再來把左+右距離加總 (葉節點要求必須包含左右子樹節點, 不能只有單邊節點)
        /// 4. 上述三種case分別計算出 題目要求的 好叶子节点对的数量
        /// 
        /// 我們只需要紀錄 <= distance 的距離即可
        /// 超過的不用加入 list裡面
        /// </summary>
        /// <param name="node"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static IList<int> GetDistances(TreeNode node, int distance)
        {
            // 紀錄不超過 distance距離的數據
            IList<int> distances = new List<int>();

            if (node == null)
            {
                // 空節點沒有葉節點, 距離0
                return distances;
            }

            // 當前node沒有左右子樹, 則當前node為葉節點.
            // 葉節點與當前節點距離為0
            if (node.left == null && node.right == null)
            {
                distances.Add(0);

                return distances;
            }

            // 分別計算出左右子樹(非空樹, 子樹根節點至葉節點距離)距離 <= distance的距離
            IList<int> leftdistances = GetDistances(node.left, distance);
            IList<int> rightdistances = GetDistances(node.right, distance);
            int leftsize = leftdistances.Count;
            int rightsize = rightdistances.Count;

            // 計算左子樹距離(左子樹根節點至葉節點距離)
            for(int i = 0; i < leftsize; i++)
            {
                // tree 每往下一層距離就要+1
                leftdistances[i]++;

                // 找出小於 distance
                if (leftdistances[i] <= distance)
                {
                    distances.Add(leftdistances[i]);
                }
            }

            // 計算右子樹距離(右子樹根節點至葉節點距離)
            for (int i = 0; i < rightsize; i++)
            {
                // tree 每往下一層距離就要+1
                rightdistances[i]++;

                // 找出小於 distance
                if (rightdistances[i] <= distance)
                {
                    distances.Add(rightdistances[i]);
                }
            }

            // 上方分別將左右子樹距離個別計算出來
            // 這邊要將 兩邊的距離加總 找出 <= distance 好叶子节点对的数量
            foreach (int leftdistance in leftdistances)
            {
                foreach (int rightdistance in rightdistances)
                {
                    // 左子樹 + 右子樹 距離總和 <= distance, 即為題目要求
                    if (leftdistance + rightdistance <= distance)
                    {
                        pairs++;
                    }
                }
            }

            return distances;
        }

    }
}
