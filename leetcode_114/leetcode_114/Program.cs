using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_114
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
        /// leetcode 114 Flatten Binary Tree to Linked List
        /// 二叉树展开为链表
        /// https://leetcode.com/problems/flatten-binary-tree-to-linked-list/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1); // root
            root.left = new TreeNode(9);
            root.right = new TreeNode(2);

            Flatten(root);

            Console.ReadKey();
        }

        /// <summary>
        ///  Preorder traversal (前序遍歷) 需先拜訪父節點再拜訪左右子節點。
        /// </summary>
        /// <param name="root"></param>
        public static void Flatten(TreeNode root)
        {
            List<TreeNode> list = new List<TreeNode>();

            preorder(root, list);

            int size = list.Count;

            for(int i = 1; i < size; i++)
            {
                TreeNode prev = list[i - 1];
                TreeNode curr = list[i];

                prev.left = null;
                prev.right = curr;

                //Console.WriteLine(prev.val.ToString());
            }

            //foreach (var a in list)
            //{
            //    Console.WriteLine(a.ToString());
            //}

        }

        /// <summary>
        /// tree 基本考題
        ///  Preorder traversal (前序遍歷) 需先拜訪父節點再拜訪左右子節點。
        /// </summary>
        /// <param name="root"></param>
        /// <param name="list"></param>
        public static void preorder(TreeNode root, List<TreeNode> list)
        {
            if(root != null)
            {
                list.Add(root);

                if (root.left != null)
                {
                    preorder(root.left, list);
                }

                if (root.right != null)
                {
                    preorder(root.right, list);
                }
            }

        }

    }
}
