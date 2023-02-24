using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_094
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
        /// leetcode 94
        /// Binary Tree Inorder Traversal
        /// https://leetcode.com/problems/binary-tree-inorder-traversal/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);

            //Console.WriteLine(InorderTraversal(root));
            InorderTraversal(root);
            Console.ReadKey();

        }

        /// <summary>
        /// https://leetcode.cn/problems/binary-tree-inorder-traversal/solution/er-cha-shu-de-zhong-xu-bian-li-by-leetcode-solutio/
        /// 遞迴
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IList<int> InorderTraversal(TreeNode root)
        {
            List<int> res = new List<int>();
            inorder(root, res);

            foreach(var a in res)
            {
                Console.WriteLine(a.ToString());
            }

            return res;
        }


        /// <summary>
        /// tree 基本考題
        /// 前序（根左右）、中序（左根右）、后序（左右根）
        /// Inorder traversal (中序遍歷) 會先拜訪左子節點，再拜訪父節點，最後拜訪右子節點。
        /// </summary>
        /// <param name="root"></param>
        /// <param name="res"></param>
        public static void inorder(TreeNode root, List<int> res)
        {
            if (root == null)
            {
                return;
            }
            inorder(root.left, res);
            res.Add(root.val);
            inorder(root.right, res);
        }

    }
}
