using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_226
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
        /// leetcode 226  Invert Binary Tree
        /// 翻轉二元數  root不變 左右樹翻轉
        /// https://leetcode.com/problems/invert-binary-tree/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(9);
            root.val = 4;
            
            root.left = new TreeNode(9);
            root.right = new TreeNode(20);
            root.left.val = 2;
            root.right.val = 7;
            
            root.left.left = new TreeNode(9);
            root.left.right= new TreeNode(20);
            root.left.left.val = 1;
            root.left.right.val = 3;

            root.right.left = new TreeNode(9);
            root.right.right = new TreeNode(9);
            root.right.left.val = 6;
            root.right.right.val = 9;

            var res = InvertTree(root);
            Console.WriteLine(res);
            Console.ReadKey();


        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10227341
        /// https://leetcode.cn/problems/invert-binary-tree/solution/fan-zhuan-er-cha-shu-by-leetcode-solution/
        /// 
        /// 採用 遞迴 作法
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
            {
                return root;
            }

            TreeNode tmpleft = root.left;
            TreeNode tmpright = root.right;

            root.left = InvertTree(tmpright);
            root.right = InvertTree(tmpleft);

            return root;
        }
    }



}
