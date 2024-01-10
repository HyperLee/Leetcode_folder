using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2385
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
        /// 2385. Amount of Time for Binary Tree to Be Infected
        /// https://leetcode.com/problems/amount-of-time-for-binary-tree-to-be-infected/?envType=daily-question&envId=2024-01-10
        /// 
        /// 2385. 感染二叉树需要的总时间
        /// https://leetcode.cn/problems/amount-of-time-for-binary-tree-to-be-infected/description/
        /// 
        /// 放到leetcode跑要小心 static 會導致錯誤
        /// 要移除.  VS跑放進去沒問題
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(5);
            root.left.right = new TreeNode(4);
            root.left.right.left = new TreeNode(9);
            root.left.right.right = new TreeNode(2);

            root.right = new TreeNode(3);
            root.right.left = new TreeNode(10);
            root.right.right = new TreeNode(6);

            int start = 3;
            Console.WriteLine(AmountOfTime(root, start));
            Console.ReadKey();
        }

        // 感染所需時間
        public static int ans = 0;

        // 起始節點高度
        public static int depth = -1;

        /// <summary>
        /// 
        /// https://leetcode.cn/problems/amount-of-time-for-binary-tree-to-be-infected/solutions/1764544/java-dfs-by-backtraxe-5ov9/
        /// </summary>
        /// <param name="root"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int AmountOfTime(TreeNode root, int start)
        {
            DFS(root, 0, start);
            return ans;
        }


        /// <summary>
        /// 解法要多學習
        /// 這解法優雅 好理解
        /// 
        /// 1.從最上層root 出發 就計算左右子樹哪邊高度最大, 即是所需時間
        /// 2.如果感染起始點分別位於左子樹 與 右子樹
        ///   就要分別加上另一邊的高度, 這樣才能計算達到完全感染整棵樹的時間
        ///   
        /// 放到leetcode跑要小心 static 會導致錯誤
        /// 要移除.  VS跑放進去沒問題
        /// </summary>
        /// <param name="root"></param>
        /// <param name="level"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int DFS(TreeNode root, int level, int start)
        {
            if(root == null)
            {
                return 0;
            }

            if(root.val == start)
            {
                depth = level;
            }

            // 左子樹高度
            int L = DFS(root.left, level + 1, start);

            // 起始節點是否在左子樹上
            bool inLeft = depth != -1;

            // 右子樹高度
            int R = DFS(root.right, level + 1, start);

            //  情况1：感染以 start 為根節點的樹所需時間
            if (root.val == start)
            {
                // 從樹的最上層root出發就找出左右子樹哪一邊最大時間即可
                ans = Math.Max(ans, Math.Max(L, R));
            }

            // 情况2：感染以 root 為根節點的樹所需時間
            if (inLeft == true)
            {
                // 起始點在左子樹就要加上右子樹的高度, 由左至右感染整棵樹
                // 根结点与起始节点的高度差 + 根结点另一颗子树的高度。
                ans = Math.Max(ans, depth - level + R);
            }
            else
            {
                // 起始點在右子樹就要加上左子樹的高度, 由右至左感染整棵樹
                ans = Math.Max(ans, depth - level + L);
            }

            return Math.Max(L, R) + 1;
        }
    }
}
