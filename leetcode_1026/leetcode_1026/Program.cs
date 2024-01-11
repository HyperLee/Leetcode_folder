using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1026
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
        /// 1026. Maximum Difference Between Node and Ancestor
        /// https://leetcode.com/problems/maximum-difference-between-node-and-ancestor/?envType=daily-question&envId=2024-01-11
        /// 1026. 节点与其祖先之间的最大差值
        /// https://leetcode.cn/problems/maximum-difference-between-node-and-ancestor/description/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.right = new TreeNode(2);
            root.left = new TreeNode(4);
            //root.right.right = new TreeNode(0);
            //root.right.right.left = new TreeNode(3);

            Console.WriteLine(MaxAncestorDiff(root));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/maximum-difference-between-node-and-ancestor/solutions/2231286/jie-dian-yu-qi-zu-xian-zhi-jian-de-zui-d-2ykj/
        /// 
        /// 題目要求 簡單說就是
        /// 要不同階層找出最大差異值
        /// 不同階層是要一上一下
        /// 上方為下方的祖先 或是說 下方是分支之一
        /// 不能為同階層或是無關聯之節點即可
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MaxAncestorDiff(TreeNode root)
        {
            return DFS(root, root.val, root.val);
        }



        /// <summary>
        /// 找出最大 與 最小的 root.val 根結點 數值
        /// 然後用DFS時候 搜尋到的 value 與 最大最小 相減
        /// 找出最大差異值
        /// 
        /// 找出最大root value: 有可能leaf node value 很小
        /// 找出最小root value: 有可能leaf node value 很大
        /// 兩種case都要找看看
        /// 
        /// 順序: 中左右
        /// 先找出root值
        /// 再去爬左右子樹
        /// </summary>
        /// <param name="root"></param>
        /// <param name="min">最小祖先節點, 最小的root.val; 非 leaf node value</param>
        /// <param name="max">最大祖先節點, 最大的root.val; 非 leaf node value</param>
        /// <returns></returns>
        public static int DFS(TreeNode root, int min, int max)
        {
            if (root == null)
            {
                return 0;
            }

            // 找出最大差異值 node val  - 最大/最小 
            int diff = Math.Max(Math.Abs(root.val - min), Math.Abs(root.val - max));

            // 最大最小 root 節點數值
            min = Math.Min(min, root.val);
            max = Math.Max(max, root.val);

            // 找root的左右子樹
            diff = Math.Max(diff, DFS(root.left, min, max));
            diff = Math.Max(diff, DFS(root.right, min, max));

            return diff;
        }
    }
}
