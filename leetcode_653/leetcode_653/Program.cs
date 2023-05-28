using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_653
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
        /// leetcode 653 Two Sum IV - Input is a BST
        /// https://leetcode.com/problems/two-sum-iv-input-is-a-bst/
        /// 两数之和 IV - 输入二叉搜索树
        /// https://leetcode.cn/problems/two-sum-iv-input-is-a-bst/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int k = 9;

            TreeNode root = new TreeNode(5);
            root.left = new TreeNode(3);
            
            //root.left.left = new TreeNode(2);
            //root.left.right = new TreeNode(4);

            root.right = new TreeNode(6);
            //root.right.right = new TreeNode(7);

            Console.WriteLine(FindTarget(root, k));
            //FindTarget(root, k);

            Console.ReadKey();
        }

         static ISet<int> set = new HashSet<int>();
        
        /// <summary>
        /// 方法一：深度优先搜索 + 哈希表
        /// https://leetcode.cn/problems/two-sum-iv-input-is-a-bst/solution/liang-shu-zhi-he-iv-shu-ru-bst-by-leetco-b4nl/
        /// 
        /// 深度優先走整棵樹
        /// 再
        /// 利用 HashSet  來儲存走過的node
        /// 每走過一個節點 就把該節點的 值 取出來 然後
        /// 儲存到 HashSet
        /// 只要有符合的 HashSet value 就是題目所要求的
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static bool FindTarget(TreeNode root, int k)
        {
            if(root == null)
            {
                return false;
            }

            // 
            if(set.Contains(k - root.val))
            {
                return true;
            }

            set.Add(root.val);

            return FindTarget(root.left, k) || FindTarget(root.right, k);

        }

    }
}
