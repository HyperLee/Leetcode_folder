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

            TreeNode t2 = new TreeNode(5);
            t2.left = new TreeNode(2);
            t2.right = new TreeNode(3);

            Console.WriteLine(LeafSimilar(t1, t2));
            Console.ReadKey();

        }


        /// <summary>
        /// SequenceEqual: 如果根據其型別的預設相等比較子判斷，兩個來源序列的長度相等，而且其對應
        /// 項目也相等，則為 true，否則為 false。
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.linq.enumerable.sequenceequal?view=net-8.0
        /// 
        /// </summary>
        /// <param name="root1"></param>
        /// <param name="root2"></param>
        /// <returns></returns>
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
        /// 
        /// 找出樹的葉節點 加入 List
        /// </summary>
        /// <param name="node"></param>
        /// <param name="seq"></param>
        public static void DFS(TreeNode node, IList<int> seq)
        {
            // 葉節點位置,左右都為空. 最下層
            if (node.left == null && node.right == null)
            {
                seq.Add(node.val);
            }
            else
            {
                if (node.left != null)
                {
                    //往左找
                    DFS(node.left, seq);
                }
                if (node.right != null)
                {
                    // 往右找
                    DFS(node.right, seq);
                }
            }
        }

    }
}
