using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_543
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
    /// leetcode 543
    /// https://leetcode.com/problems/diameter-of-binary-tree/
    /// </summary>
    internal class Program
    {
        public static int max = 0;
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(9);
            root.val = 1;
            root.left = new TreeNode(9);
            root.right = new TreeNode(20);
            root.left.val = 2;
            root.right.val = 3;
            //root.left.left = new TreeNode(9);
            root.right.left = new TreeNode(9);
            root.right.right = new TreeNode(9);
            root.right.left.val = 4;
            root.right.right.val = 5;

            var res = DiameterOfBinaryTree(root);
            Console.WriteLine(res);
            //Console.Write( DiameterOfBinaryTree(root));
            //Console.ReadLine();
            Console.ReadKey();
        }

        public static int DiameterOfBinaryTree(TreeNode root)
        {
            MaxDepth(root);
            return max;
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10227129
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MaxDepth(TreeNode root)
        {
            if (root == null) return 0;

            //找出 left 節點的最大深度
            int left = MaxDepth(root.left);

            // 找出 right 節點的最大深度
            int right = MaxDepth(root.right);

            // 這邊就是在紀錄除了root 外的最長路徑
            max = Math.Max(max, left + right);

            //此時的 +1 就是 往下多一階 的意思
            return Math.Max(left, right) + 1;
        }


    }
}
