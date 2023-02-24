using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_938
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
        /// leetcode 938  Range Sum of BST
        /// https://leetcode.com/problems/range-sum-of-bst/description/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// 深度優先
        /// https://leetcode.cn/problems/range-sum-of-bst/solution/er-cha-sou-suo-shu-de-fan-wei-he-by-leet-rpq7/
        /// 
        /// 按深度优先搜索的顺序计算范围和。记当前子树根节点为 rootroot，分以下四种情况讨论：
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static int RangeSumBST(TreeNode root, int low, int high)
        {
            if (root == null)
            {
                return 0;
            }
            if (root.val > high)
            {
                return RangeSumBST(root.left, low, high);
            }
            if (root.val < low)
            {
                return RangeSumBST(root.right, low, high);
            }
            return root.val + RangeSumBST(root.left, low, high) + RangeSumBST(root.right, low, high);
        }


    }
}
