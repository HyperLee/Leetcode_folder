using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_144
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
        /// leetcode 144
        /// Binary Tree Preorder Traversal

        /// https://leetcode.com/problems/binary-tree-preorder-traversal/description/
        /// 
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

            //Console.WriteLine(PreorderTraversal(root));
            PreorderTraversal(root);

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.com/problems/binary-tree-preorder-traversal/solutions/533636/c-iterative-and-recursive-solutions/?q=c%23&orderBy=most_relevant
        /// 遞迴作法
        /// DFS 深度優先搜尋
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IList<int> PreorderTraversal(TreeNode root)
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
        /// Preorder traversal (前序遍歷) 需先拜訪父節點再拜訪左右子節點。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="list"></param>
        private static void DFS(TreeNode node, List<int> list)
        {
            if (node == null)
                return;

            // Preorder traversal (前序遍歷) 需先拜訪父節點再拜訪左右子節點。
            list.Add(node.val);
            DFS(node.left, list);
            DFS(node.right, list);
        }

    }
}
