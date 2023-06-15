using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1161
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
        /// 1161. Maximum Level Sum of a Binary Tree
        /// https://leetcode.com/problems/maximum-level-sum-of-a-binary-tree/
        /// 1161. 最大层内元素和
        /// https://leetcode.cn/problems/maximum-level-sum-of-a-binary-tree/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            
            root.left = new TreeNode(7);
            root.right = new TreeNode(0);

            root.left.left = new TreeNode(7);
            root.left.right = new TreeNode(-8);

            Console.WriteLine(MaxLevelSum(root));
            Console.ReadKey();

        }

        private static IList<int> sum = new List<int>();

        /// <summary>
        /// 深度優先
        /// https://leetcode.cn/problems/maximum-level-sum-of-a-binary-tree/solution/zui-da-ceng-nei-yuan-su-he-by-leetcode-s-2tm4/
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MaxLevelSum(TreeNode root)
        {
            DFS(root, 0);

            int ans = 0;

            for(int i = 0; i < sum.Count; i++)
            {
                // 找出 tree裡面哪一層加總最大
                if (sum[i] > sum[ans])
                {
                    ans = i;
                }
            }

            return ans + 1; // root 從第一層開始
        }


        /// <summary>
        /// 深度優先
        /// 中 左 右 這順序跑
        /// 不為空且層數與list計數相同 表示為同一層
        /// 跑完一邊的分支之後
        /// 接著跑另一邊 就來 更新 該層的node 加總 數值  sum[level] += node.val
        /// 
        /// ex: root 只有一個直接加入
        /// 再來假設左邊都不為空
        /// 就先跑左邊 先 list.add 進去
        /// 左邊都跑完畢之後
        /// 換跑右邊, 此時更新 該層 左右分之 node.val 之加總
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="level"> tree 裡面的第幾層</param>
        private static void DFS(TreeNode node, int level)
        {
            // 每一層其中一邊分支 先加入 list.add 裡面
            if(level == sum.Count)
            {
                sum.Add(node.val);
            }
            else
            {
                // 更新該層的 node.val 加總數值
                sum[level] += node.val;
            }

            if(node.left != null)
            {
                // 往下一層 level + 1
                DFS(node.left, level + 1);
            }

            if(node.right != null)
            {
                // 往下一層 level +１
                DFS(node.right, level + 1);
            }

        }

    }
}
