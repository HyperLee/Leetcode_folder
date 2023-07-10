using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_104
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
        /// leetcode_ 104 Maximum Depth of Binary Tree
        /// https://leetcode.com/problems/maximum-depth-of-binary-tree/
        /// 
        /// build tree sample
        /// http://e-troy.blogspot.com/2015/02/c-binary-search-tree.html
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode();
            root.val = 3;
            root.left = new TreeNode(9);
            root.right = new TreeNode(20);
            root.left.val = 9;
            root.right.val = 20;
            root.left.left = new TreeNode(9);
            root.right.left = new TreeNode(9);
            root.right.left.val = 7;


            Console.WriteLine(MaxDepth2(root));
            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10226788
        /// 遞迴概念
        /// 1. 判斷 root 是否為 null，若是則回傳 0
        /// 2. 回傳此節點的深度 
        ///     遞迴找出 root 的right 右節點 最大深度
        ///     遞迴找出 root 的left 左節點 最大深度
        ///     比較兩個節點的最大深度，使用Math.Max 取 最大值
        ///     最後並 +1，代表需要往上多加這一層
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;
            return Math.Max(MaxDepth(root.right), MaxDepth(root.left)) + 1;
        }


        /// <summary>
        /// 方法二,
        /// 參考 leetcode_111 Minimum Depth of Binary Tree
        /// 修改而來
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>

        public static int MaxDepth2(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            // 只有root 沒有左右子樹
            if (root.left == null && root.right == null)
            {
                return 1;
            }

            int mindepth = int.MinValue;
            // 找出左邊最大
            if (root.left != null)
            {
                mindepth = Math.Max(MaxDepth2(root.left), mindepth);
            }
            // 找出右邊最大
            if (root.right != null)
            {
                mindepth = Math.Max(MaxDepth2(root.right), mindepth);
            }

            // +1 上一層
            return mindepth + 1;
        }


    }
}
