using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_101
{
    internal class Program
    {
        /// <summary>
        /// 
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
        /// 101. Symmetric Tree
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
            tn.right.right = new TreeNode(3);

            Console.Write(IsSymmetric(tn));
            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10225675
        /// 注意 root 為空
        /// 也是一種對稱
        /// 
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


        /// <summary>
        /// 判斷左右是否相同, 鏡像
        /// 對稱就是兩邊都要有 node 且 數值相同
        /// 一邊沒有缺少 或是 數值 不同就是錯誤
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool IsSymmetricTree(TreeNode left, TreeNode right)
        {
            if(left == null && right == null)
            {
                // 左右子樹都為空
                return true;
            }

            if(left == null || right == null)
            {
                // 左右子樹其中一為空
                return false;
            }

            if (left.val != right.val)
            {
                // 判斷左右 val 是否相同
                return false;
            }

            // 繼續判斷 左右子樹
            return IsSymmetricTree(left.left, right.right) && IsSymmetricTree(left.right, right.left);
        }

    }
}
