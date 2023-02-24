using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_100
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
        /// leetcode 100
        /// https://leetcode.com/problems/same-tree/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode p= new TreeNode(1);
            p.left = new TreeNode(2);
            p.right = new TreeNode(3);

            TreeNode q = new TreeNode(1);
            q.left = new TreeNode(2);
            q.right = new TreeNode(3);
            Console.WriteLine(IsSameTree(p, q));

            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/same-tree/solution/xiang-tong-de-shu-by-leetcode-solution/
        /// DFS
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)
            {
                // 兩樹root皆為空
                return true;
            }
            else if (p == null || q == null)
            {
                // 兩樹root有一隻為空
                return false;
            }
            else if (p.val != q.val)
            {
                // 兩樹root都不為空 就比較 root的數值是否一樣
                return false;
            }
            else
            {
                // 比較兩樹 各自的左右子樹
                return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
            }
        }

    }
}
