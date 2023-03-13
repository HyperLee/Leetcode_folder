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
        /// 101. 对称二叉树
        /// https://leetcode.cn/problems/symmetric-tree/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode tn = new TreeNode(1);
            tn.left = new TreeNode(2);
            tn.right = new TreeNode(2);

            tn.left.left = new TreeNode(3);
            tn.left.right = new TreeNode(4);

            tn.right.left = new TreeNode(4);
            tn.right.right = new TreeNode(3333);


            Console.Write(IsSymmetric(tn));
            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10225675
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool IsSymmetric(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }
            else
            {
                return IsSymmetricTree(root.left, root.right);
            }
        }

        public static bool IsSymmetricTree(TreeNode left, TreeNode right)
        {
            /* // 可替換成下面寫法
            if (left == null || right == null)
            {
                return left == right;
            }
            */
            ///////////////////////////////////////////////////////////
            if(left == null && right == null)
            {
                return true;
            }

            if(left == null || right == null)
            {
                return false;
            }
            ////////////////////////////////////////////////////////////

            if (left.val != right.val)
            {
                return false;
            }

            return IsSymmetricTree(left.left, right.right) && IsSymmetricTree(left.right, right.left);
        }

    }
}
