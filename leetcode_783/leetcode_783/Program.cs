using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_783
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
        /// leetcode 783 Minimum Distance Between BST Nodes
        /// 找出 二元樹中 節點之間 最小差異值
        /// https://leetcode.com/problems/minimum-distance-between-bst-nodes/
        /// 
        /// 同類型題目 leetcode 530
        /// https://leetcode.com/problems/minimum-absolute-difference-in-bst/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(9);
            root.right = new TreeNode(4);

            Console.WriteLine(MinDiffInBST(root));

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/minimum-distance-between-bst-nodes/solution/gong-shui-san-xie-yi-ti-san-jie-shu-de-s-7r17/
        /// 朴素解法（DFS）
        /// 如果不考虑利用二叉搜索树特性的话，一个朴素的做法是将所有节点的 val 存到一个数组中。
        /// 对数组进行排序，并获取答案。
        /// 将所有节点的 val 存入数组，可以使用 BFS 或者 DFS。
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MinDiffInBST(TreeNode root)
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
