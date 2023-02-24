using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_617
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
        /// leetcode 617  Merge Two Binary Trees
        /// https://leetcode.com/problems/merge-two-binary-trees/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10227617
        /// 
        /// https://leetcode.cn/problems/merge-two-binary-trees/solution/he-bing-er-cha-shu-by-leetcode-solution/
        /// 
        /// 遞迴觀點
        /// root merge
        /// root.left merge
        /// root.right merge
        /// 都merge之後給到新的子樹左右即可
        /// </summary>
        /// <param name="root1"></param>
        /// <param name="root2"></param>
        /// <returns></returns>
        public static TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            if(root1 == null && root2 == null)
            {
                return null;
            }

            if(root1 == null && root2 != null)
            {
                return root2;
            }

            if(root1 != null && root2 == null)
            {
                return root1;
            }

            TreeNode result = new TreeNode(root1.val + root2.val); // root 加總

            result.left = MergeTrees(root1.left, root2.left); // left merge
            result.right = MergeTrees(root1.right, root2.right); // right merge

            return result; // 全部加總之後 丟給 新的tree  result 就好

        }

    }
}
