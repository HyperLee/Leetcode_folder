using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_662
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

        Dictionary<int, int> levelMin = new Dictionary<int, int>();

        /// <summary>
        /// leetcode 662
        /// https://leetcode.com/problems/maximum-width-of-binary-tree/
        /// 662. 二叉树最大宽度
        /// https://leetcode.cn/problems/maximum-width-of-binary-tree/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            
            root.left = new TreeNode(3);
            root.left.left = new TreeNode(5);
            root.left.right = new TreeNode(3);
            
            root.right = new TreeNode(2);
            root.right.right = new TreeNode(9);

            //Console.WriteLine(WidthOfBinaryTree(root));
            //Console.ReadKey();

        }


        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int WidthOfBinaryTree(TreeNode root)
        {
            return DFS(root, 1, 1);
        }


        public int DFS(TreeNode node, int depth, int index)
        {
            if (node == null)
            {
                return 0;
            }
            levelMin.TryAdd(depth, index); // 每一层最先访问到的节点会是最左边的节点，即每一层编号的最小值
            return Math.Max(index - levelMin[depth] + 1, Math.Max(DFS(node.left, depth + 1, index * 2), DFS(node.right, depth + 1, index * 2 + 1)));
        }


    }
}
