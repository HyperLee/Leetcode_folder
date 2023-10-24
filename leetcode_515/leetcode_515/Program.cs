using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_515
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
        /// 515. Find Largest Value in Each Tree Row
        /// https://leetcode.com/problems/find-largest-value-in-each-tree-row/?envType=daily-question&envId=2023-10-24
        /// 515. 在每个树行中找最大值
        /// https://leetcode.cn/problems/find-largest-value-in-each-tree-row/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            //Console.WriteLine(LargestValues(root));
            var res = LargestValues(root);
            foreach(var value in res)
            {
                Console.Write(value.ToString() + ", ");
            }
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/find-largest-value-in-each-tree-row/solutions/1619294/zai-mei-ge-shu-xing-zhong-zhao-zui-da-zh-6xbs/
        /// 使用 深度優先DFS 找出樹的資料
        /// 并用 curHeight 来标记遍历到的当前节点的高度。
        /// 当遍历到 curHeight 高度的节点就判断是否更新该层节点的最大值。
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IList<int> LargestValues(TreeNode root)
        {
            if(root == null)
            {
                return new List<int>();
            }

            IList<int> res = new List<int>();
            DFS(res, root, 0);

            return res;

        }

        /// <summary>
        /// DFS 找尋順序中左右
        /// </summary>
        /// <param name="res"></param>
        /// <param name="root"></param>
        /// <param name="curHeight">目前遍歷中DFS樹最高高度</param>
        public static void DFS(IList<int> res, TreeNode root, int curHeight)
        {
            // 
            if(curHeight == res.Count)
            {
                res.Add(root.val);
            }
            else
            {
                // 同一層(左至右)中 找出 最大值的 root.val
                res[curHeight] = Math.Max(res[curHeight], root.val);
            }

            if(root.left != null)
            {
                DFS(res, root.left, curHeight + 1);
            }

            if(root.right != null) 
            {
                DFS(res, root.right, curHeight + 1);
            }

        }

    }
}
