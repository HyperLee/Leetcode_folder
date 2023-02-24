using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_101
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
        /// leetcode 101 Symmetric Tree
        /// https://leetcode.com/problems/symmetric-tree/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10225675
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public bool IsSymmetric(TreeNode root)
        {
            if (root == null)
                return true;
            else
                return IsSymmetricTree(root.left, root.right);
        }

        public bool IsSymmetricTree(TreeNode left, TreeNode right)
        {
            if (left == null || right == null)
                return left == right;
            if (left.val != right.val)
                return false;
            return IsSymmetricTree(left.left, right.right) && IsSymmetricTree(left.right, right.left);
        }

    }
}
