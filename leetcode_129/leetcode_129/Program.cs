using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_129
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
        /// 129. Sum Root to Leaf Numbers
        /// https://leetcode.com/problems/sum-root-to-leaf-numbers/
        /// 129. 求根节点到叶节点数字之和
        /// https://leetcode.cn/problems/sum-root-to-leaf-numbers/
        /// 
        /// 簡單說就是從 root 出發 走到 lead node
        /// 每一條路徑數值都累計起來計算總合
        /// 每一條路徑也就是每一層的每個 node  都要走過
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode tree = new TreeNode(4);

            tree.left = new TreeNode(9);
            tree.left.left = new TreeNode(5);
            tree.left.right = new TreeNode(1);

            tree.right = new TreeNode(0);

            Console.WriteLine(SumNumbers(tree));
            Console.ReadKey();

        }

        /// <summary>
        /// DFS 深度優先
        /// https://leetcode.cn/problems/sum-root-to-leaf-numbers/solution/qiu-gen-dao-xie-zi-jie-dian-shu-zi-zhi-he-by-leetc/
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int SumNumbers(TreeNode root)
        {
            return dfs(root, 0);
        }


        /// <summary>
        /// 根據題目說明
        /// For example, the root-to-leaf path 1 -> 2 -> 3 represents the number 123.
        /// 簡單說就是 每增加一層 或是 一個node 就是 乘上 十倍
        /// 
        /// 拿取 node 方式 透過 深度優先方法
        /// 從 root 開始取值 每一層(節點) 都要拿 直到 葉節點(leaf node)
        /// 注意 每一層 每個 node (簡單說就是從上至下每個路徑都要走過) 都要拿出來做加總
        /// </summary>
        /// <param name="root"></param>
        /// <param name="prevSum"></param>
        /// <returns></returns>
        public static int dfs(TreeNode root, int prevSum)
        {
            if (root == null)
            {
                return 0;
            }
            
            // 每多增加一層 就是 10 倍
            int sum = prevSum * 10 + root.val;
            
            if (root.left == null && root.right == null)
            {
                return sum;
            }
            else
            {
                // 每個路徑都要累加
                return dfs(root.left, sum) + dfs(root.right, sum);
            }
        }


    }
}
