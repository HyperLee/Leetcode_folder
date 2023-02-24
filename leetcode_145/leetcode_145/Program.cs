using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_145
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
        /// leetcode 145
        /// Binary Tree Postorder Traversal
        /// https://leetcode.com/problems/binary-tree-postorder-traversal/
        /// 
        /// tree 基本考題
        /// 前序（根左右）、中序（左根右）、后序（左右根）
        /// 
        /// 
        /// 基本觀念
        /// https://shubo.io/iterative-binary-tree-traversal/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1); // root
            root.left = new TreeNode(9);
            root.right = new TreeNode(2);
            //root.right.left = new TreeNode(3);

            PostorderTraversal(root);

            Console.ReadKey();
        }

        public static IList<int> PostorderTraversal(TreeNode root)
        {
            List<int> res = new List<int>();
            DFS(root, res);

            // 輸出結果
            foreach (var str in res)
            {
                Console.WriteLine(str);
            }
            //
            return res;
        }


        /// <summary>
        /// Postorder traversal (後序遍歷) 需先拜訪左右子節點，最後拜訪父節點。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="list"></param>
        private static void DFS(TreeNode node, List<int> list)
        {
            if (node == null)
            {
                return;
            }

            //Postorder traversal (後序遍歷) 需先拜訪左右子節點，最後拜訪父節點。
            DFS(node.left, list);
            DFS(node.right, list);
            list.Add(node.val);
        }

    }
}
