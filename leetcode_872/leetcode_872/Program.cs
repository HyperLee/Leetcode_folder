using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_872
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
        /// leetcode 872
        /// https://leetcode.com/problems/leaf-similar-trees/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode t1 = new TreeNode(1);
            t1.left = new TreeNode(2);
            //t1.left.left = new TreeNode(9);
            t1.right = new TreeNode(3);

            TreeNode t2 = new TreeNode(1);
            t2.left = new TreeNode(2);
            t2.right = new TreeNode(3);

            Console.WriteLine(LeafSimilar(t1, t2));
            Console.ReadKey();

        }

        public static bool LeafSimilar(TreeNode root1, TreeNode root2)
        {
            IList<int> seq1 = new List<int>();
            if (root1 != null)
            {
                DFS(root1, seq1);
            }

            IList<int> seq2 = new List<int>();
            if (root2 != null)
            {
                DFS(root2, seq2);
            }

            return seq1.SequenceEqual(seq2);
        }


        /// <summary>
        /// https://leetcode.cn/problems/leaf-similar-trees/solution/xie-zi-xiang-si-de-shu-by-leetcode-solut-z0w6/
        /// </summary>
        /// <param name="node"></param>
        /// <param name="seq"></param>
        public static void DFS(TreeNode node, IList<int> seq)
        {
            if (node.left == null && node.right == null)
            {
                seq.Add(node.val);
            }
            else
            {
                if (node.left != null)
                {
                    DFS(node.left, seq);
                }
                if (node.right != null)
                {
                    DFS(node.right, seq);
                }
            }
        }

    }
}
