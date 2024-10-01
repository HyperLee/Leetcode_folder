using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace leetcode_222
{
    internal class Program
    {
        /// <summary>
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
        /// 222. Count Complete Tree Nodes
        /// https://leetcode.com/problems/count-complete-tree-nodes/description/?envType=study-plan-v2&envId=top-interview-150
        /// 222. 完全二叉树的节点个数
        /// https://leetcode.cn/problems/count-complete-tree-nodes/description/
        /// 
        /// 計算完整二元樹總共有多少 node
        /// 完整二元樹: 在完全二叉树中，除了最底层节点可能没填满外，其余每层节点数都达到最大值，并且最下面一层的节点都集中在该层最左边的若干位置。
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);

            root.left = new TreeNode(2);
            root.right = new TreeNode(3);

            //root.left.left = new TreeNode(4);
            //root.left.right = new TreeNode(5);
            //root.right.left = new TreeNode(6);

            Console.WriteLine(CountNodes(root));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/count-complete-tree-nodes/solution/222-wan-quan-er-cha-shu-de-jie-dian-ge-shu-di-gu-5/
        /// 遞迴 作法
        /// 
        /// 注意 要加 1
        /// 每遇到一個 node 就 + 1
        /// 累加 加總意思
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int CountNodes(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            return CountNodes(root.left) + CountNodes(root.right) + 1;

            /*
            int left = CountNodes(root.left);
            int right = CountNodes(root.right);
            return left + right + 1;
            */

        }

    }
}
