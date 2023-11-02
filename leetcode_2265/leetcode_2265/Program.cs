using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2265
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

        public static int ans = 0;

        /// <summary>
        /// 2265. Count Nodes Equal to Average of Subtree
        /// https://leetcode.com/problems/count-nodes-equal-to-average-of-subtree/?envType=daily-question&envId=2023-11-02
        /// 2265. 统计值等于子树平均值的节点数
        /// https://leetcode.cn/problems/count-nodes-equal-to-average-of-subtree/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
           TreeNode root = new TreeNode(4);
            root.left = new TreeNode(8);
            root.right = new TreeNode(5);

            root.left.left = new TreeNode(0);
            root.left.right = new TreeNode(1);

            root.right.right = new TreeNode(6);

            Console.WriteLine(AverageOfSubtree(root));

            //var res = AverageOfSubtree(root);
            //foreach(var value in res)
            //{
            //    Console.WriteLine(value.ToString());
            //}
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/count-nodes-equal-to-average-of-subtree/solutions/1477346/by-hu-li-hu-wai-en3h/
        /// https://leetcode.cn/problems/count-nodes-equal-to-average-of-subtree/solutions/1477315/tong-ji-zi-shu-de-jie-dian-he-ji-jie-dia-va8t/
        /// 
        /// 利用DFS計算出 總和與節點數量
        /// 由下往上做遞迴
        /// 
        /// 更簡單易懂的方法
        /// https://leetcode.cn/problems/count-nodes-equal-to-average-of-subtree/solutions/1477376/by-shou-hu-zhe-t-r565/
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int AverageOfSubtree(TreeNode root)
        {
            DFS(root);

            return ans;
        }


        /// <summary>
        ///  int[] {sum, nodeNum}
        ///  左邊為總和, 右邊為節點數量
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private static int[] DFS(TreeNode root)
        {
            if(root == null)
            {
                // 空節點
                return new int[] { 0, 0 };
            }

            int[] leftt = DFS(root.left);
            int[] rightt = DFS(root.right);

            // 子樹節點總和, 計算加上root
            int sum = root.val + leftt[0] + rightt[0];
            // 子樹節點數量, 計算加上root
            int nodeNum = rightt[1] + leftt[1] + 1;

            // 判斷是否符合題目需求
            if(root.val == sum / nodeNum)
            {
                ans++;
            }

            // 往上
            return new int[] {sum, nodeNum};
        }

    }
}
