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
        /// 
        /// 本題目簡單說就是找出一條路徑
        /// 從 root 走到 lead node
        /// 加總 node value 要與 targetSum 相同
        /// 有就是 true
        /// 反之就是 false
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            int targetSum = 4;
            Console.WriteLine(HasPathSum(root, targetSum));
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/path-sum/solutions/318487/lu-jing-zong-he-by-leetcode-solution/
        /// 方法二 遞迴
        /// 
        /// 当题目中提到了 叶子节点 时，正确的做法一定要同时判断节点的 左右子树同时为空 才是叶子节点。
        /// 叶子节点 是指没有子节点的节点。
        /// 
        /// 簡單說就是 路徑總和 要與 targetSum 相同
        /// 所以
        /// 從 root 往下走 每走到一個 node 就扣減該 node value 
        /// => targetSum - root.val
        /// 直到 走到 leaf node 為止
        /// 此時判斷 targetSum == root.val 是否相同
        /// 相同即是 true
        /// 反之則是 false
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
            
            // 沒有左右子樹的節點 => 葉子節點
            if (root.left == null && root.right == null)
            {
                return targetSum == root.val;
            }

            return HasPathSum(root.left, targetSum - root.val) || HasPathSum(root.right, targetSum - root.val);
        }


    }
}
