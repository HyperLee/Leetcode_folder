using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1372
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

        public int maxAns;

        /// <summary>
        /// leetcode 1372  Longest ZigZag Path in a Binary Tree
        /// https://leetcode.com/problems/longest-zigzag-path-in-a-binary-tree/
        /// 1372. 二叉树中的最长交错路径
        /// https://leetcode.cn/problems/longest-zigzag-path-in-a-binary-tree/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode();

        }


        /// <summary>
        /// https://leetcode.cn/problems/longest-zigzag-path-in-a-binary-tree/solution/er-cha-shu-zhong-de-zui-chang-jiao-cuo-lu-jing-b-2/
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int LongestZigZag(TreeNode root)
        {
            maxAns = 0;

            if (root == null)
            {
                return 0;
            }

            // 應該是不太需要兩邊都各走一次
            // 走一次即可 反正function都有if else了
            dfs(root, false, 0);
            //dfs(root, true, 0);

            return maxAns;
        }


        /// <summary>
        /// dir: 方向
        /// 0: 左邊
        /// 1: 右邊
        /// 
        /// len: 長度
        /// 初始帶入時候 
        /// 因還在root, 故 len = 0
        /// 
        /// 走到node時候 因已經有往下走
        /// 當走到沒辦法往下 需要重置 時候
        /// len = 1
        /// 
        /// 可以繼續往下走的話就是 len + 1
        /// 
        /// 之字走法
        /// 左右左 or 右左右
        /// 每走一次方向要相反
        /// 不能連續走同方向
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dir"></param>
        /// <param name="len"></param>
        public void dfs(TreeNode o, bool dir, int len)
        {
            // 找出最長路徑
            maxAns = Math.Max(maxAns, len);

            if (!dir)
            {
                // dir left tree

                if (o.left != null)
                {
                    // 左子樹的右節點
                    dfs(o.left, true, len + 1);
                }

                if (o.right != null)
                {
                    dfs(o.right, false, 1);
                }
            }
            else
            {
                // dir right tree

                if (o.right != null)
                {
                    // 右子樹的左節點
                    dfs(o.right, false, len + 1);
                }

                if (o.left != null)
                {
                    dfs(o.left, true, 1);
                }
            }

        }


    }
}
