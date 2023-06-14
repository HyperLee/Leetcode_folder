using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_530
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


    internal class Program
    {
        /// <summary>
        /// leetcode 530 Minimum Absolute Difference in BST
        /// https://leetcode.com/problems/minimum-absolute-difference-in-bst/description/
        /// 
        /// 本題 類似 leetcode 783
        /// https://leetcode.com/problems/minimum-distance-between-bst-nodes/description/
        /// 
        /// 解法共用
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(99);
            root.right = new TreeNode(44);

            Console.WriteLine(GetMinimumDifference(root));

            Console.ReadKey();
        }

        /// <summary>
        /// https://leetcode.cn/problems/minimum-distance-between-bst-nodes/solution/gong-shui-san-xie-yi-ti-san-jie-shu-de-s-7r17/
        /// 懶人字串解法（DFS）
        /// 如果不考虑利用二叉搜索树特性的话，轉成字串的做法是将所有节点的 val 存到一个数组中。
        /// 对数组进行排序，并获取答案。
        /// 将所有节点的 val 存入数组，可以使用 BFS 或者 DFS。
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int GetMinimumDifference(TreeNode root)
        {
            List<int> list = new List<int>();

            dfs(root, list);

            // sort
            Array.Sort(list.ToArray());

            int n = list.Count;
            int ans = int.MaxValue;

            // 找出最小數值
            for (int i = 1; i < n; i++)
            {
                int cur = Math.Abs(list[i] - list[i - 1]);
                ans = Math.Min(ans, cur);
            }

            return ans;

        }

        /// <summary>
        /// tree 基本考題
        /// 前序（根左右）、中序（左根右）、后序（左右根）
        /// Inorder traversal (中序遍歷) 會先拜訪左子節點，再拜訪父節點，最後拜訪右子節點。
        /// </summary>
        /// <param name="root"></param>
        /// <param name="list"></param>
        public static void dfs(TreeNode root, List<int> list)
        {

            if (root.left != null)
            {
                dfs(root.left, list);
            }

            list.Add(root.val);

            if (root.right != null)
            {
                dfs(root.right, list);
            }
        }

    }
}
