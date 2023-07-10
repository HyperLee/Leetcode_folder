using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_111
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
        /// 111. Minimum Depth of Binary Tree
        /// https://leetcode.com/problems/minimum-depth-of-binary-tree/
        /// 111. 二叉树的最小深度
        /// https://leetcode.cn/problems/minimum-depth-of-binary-tree/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(3);

            root.left = new TreeNode(9);

            root.right = new TreeNode(20);
            root.right.left = new TreeNode(15);
            root.right.right = new TreeNode(7);

            Console.WriteLine(MinDepth(root));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/minimum-depth-of-binary-tree/solution/er-cha-shu-de-zui-xiao-shen-du-by-leetcode-solutio/
        /// 深度優先
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MinDepth(TreeNode root)
        {
            if(root == null)
            {
                return 0;
            }
            // 只有root 沒有左右子樹
            if (root.left == null && root.right == null)
            {
                return 1;
            }

            int mindepth = int.MaxValue;
            // 找出左邊最大
            if (root.left != null)
            {
                mindepth = Math.Min(MinDepth(root.left), mindepth);
            }
            // 找出右邊最大
            if (root.right != null)
            {
                mindepth = Math.Min(MinDepth(root.right), mindepth);
            }

            // +1 上一層
            return mindepth + 1;

        }

    }
}
