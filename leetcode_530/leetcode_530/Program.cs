using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_530
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


    internal class Program
    {
        /// <summary>
        /// 530. Minimum Absolute Difference in BST
        /// https://leetcode.com/problems/minimum-absolute-difference-in-bst/description/
        /// 
        /// 530. 二叉搜索树的最小绝对差
        /// https://leetcode.cn/problems/minimum-absolute-difference-in-bst/description/
        /// 
        /// 本題 類似 leetcode 783
        /// https://leetcode.com/problems/minimum-distance-between-bst-nodes/description/
        /// 
        /// 解法共用
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(44);
            root.right = new TreeNode(99);

            Console.WriteLine(GetMinimumDifference(root));

            Console.ReadKey();
        }

        public static int pre;
        public static int ans;

        /// <summary>
        /// https://leetcode.cn/problems/minimum-distance-between-bst-nodes/solution/gong-shui-san-xie-yi-ti-san-jie-shu-de-s-7r17/
        /// 懶人字串解法（DFS）
        /// 如果不考虑利用二叉搜索树特性的话，轉成字串的做法是将所有节点的 val 存到一个数组中。
        /// 对数组进行排序，并获取答案。
        /// 将所有节点的 val 存入数组，可以使用 BFS 或者 DFS。
        /// 
        /// 
        /// https://leetcode.cn/problems/minimum-absolute-difference-in-bst/solutions/443276/er-cha-sou-suo-shu-de-zui-xiao-jue-dui-chai-by-lee/
        /// 20241004
        /// 改採用中序解法
        /// 省略排序,
        /// 因輸入是二元樹且中序是由小至大排序特性
        /// 二叉搜索树的中序遍历是有序的，因此我们可以直接对「二叉搜索树」进行中序遍历，保存遍历过程中的相邻元素最小值即是答案。
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int GetMinimumDifference(TreeNode root)
        {
            pre = -1;
            ans = int.MaxValue;
            Inorder_traversal(root);

            return ans;
        }


        /// <summary>
        /// tree 基本考題
        /// 前序（根左右）、中序（左根右）、后序（左右根）
        /// 
        /// 這邊使用
        /// Inorder traversal (中序遍歷) 會先拜訪左子節點，再拜訪父節點，最後拜訪右子節點。
        /// </summary>
        /// <param name="root"></param>
        /// <param name="list"></param>
        public static void Inorder_traversal(TreeNode root)
        {
            if(root == null)
            {
                return;
            }

            Inorder_traversal(root.left);
            if(pre == -1)
            {
                pre = root.val;
            }
            else
            {
                ans = Math.Min(ans, Math.Abs(root.val - pre));
                pre = root.val;
            }

            Inorder_traversal(root.right);
        }

    }
}
