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
        /// https://leetcode.com/problems/count-complete-tree-nodes/
        /// leetcode_ 222
        /// 
        /// tree
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }


        /// <summary>
        /// https://leetcode.cn/problems/count-complete-tree-nodes/solution/222-wan-quan-er-cha-shu-de-jie-dian-ge-shu-di-gu-5/
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int CountNodes(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            return 1 + CountNodes(root.left) + CountNodes(root.right);
        }

    }
}
