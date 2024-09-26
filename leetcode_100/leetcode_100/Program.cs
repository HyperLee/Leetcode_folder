using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_100
{
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
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
        /// 100. Same Tree
        /// https://leetcode.com/problems/same-tree/description/
        /// 
        /// 100. 相同的树
        /// https://leetcode.cn/problems/same-tree/description/
        /// 
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

            Console.WriteLine("ans: " + IsSameTree(p, q));

            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/same-tree/solution/xiang-tong-de-shu-by-leetcode-solution/
        /// DFS
        /// 
        /// 先判斷 root 狀態
        /// 再來判斷 val 數值是否相同
        /// 最後左右子樹用 遞迴去跑
        /// 
        /// </summary>
        /// <param name="p">trees p</param>
        /// <param name="q">trees q</param>
        /// <returns></returns>
        public static bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)
            {
                // 兩樹 root 皆為空
                return true;
            }
            else if (p == null || q == null)
            {
                // 兩樹 root 其中一個為空
                return false;
            }
            else if (p.val != q.val)
            {
                // 兩樹 root 都不為空 就比較 root 的數值是否一樣
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
