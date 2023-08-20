using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_112
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
        /// 112. Path Sum
        /// https://leetcode.com/problems/path-sum/
        /// 112. 路径总和
        /// https://leetcode.cn/problems/path-sum/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            int targetSum = 5;
            Console.WriteLine(HasPathSum(root, 5));
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/path-sum/solutions/318487/lu-jing-zong-he-by-leetcode-solution/
        /// 方法二 遞迴
        /// 
        /// 当题目中提到了 叶子节点 时，正确的做法一定要同时判断节点的 左右子
        /// 树同时为空 才是叶子节点。
        /// </summary>
        /// <param name="root"></param>
        /// <param name="targetSum"></param>
        /// <returns></returns>
        public static bool HasPathSum(TreeNode root, int targetSum)
        {
            if(root == null)
            {
                return false;
            }

            // 叶子节点
            if (root.left == null && root.right == null)
            {
                return targetSum == root.val;
            }

            return HasPathSum(root.left, targetSum - root.val) || HasPathSum(root.right, targetSum - root.val);
        }


    }
}
