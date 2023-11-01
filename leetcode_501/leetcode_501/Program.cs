using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_501
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

        public static int prev;
        public static int freq;
        public static int maxFreq;
        public static IList<int> modesList;

        /// <summary>
        /// 501. Find Mode in Binary Search Tree
        /// https://leetcode.com/problems/find-mode-in-binary-search-tree/?envType=daily-question&envId=2023-11-01
        /// 501. 二叉搜索树中的众数
        /// https://leetcode.cn/problems/find-mode-in-binary-search-tree/
        /// 
        /// 找出BST中出現頻率最多的 那個數值
        /// 假定 BST 满足如下定义：
        /// 结点左子树中所含节点的值 小于等于 当前节点的值
        /// 结点右子树中所含节点的值 大于等于 当前节点的值
        /// 左子树和右子树都是二叉搜索树
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.right = new TreeNode(2);
            root.right.left = new TreeNode(2);

            //Console.WriteLine(FindMode(root));
            var res = FindMode(root);
            foreach(var value in res)
            {
                Console.WriteLine(value.ToString());
            }

            Console.ReadKey();
        }

        /// <summary>
        /// 官方
        /// https://leetcode.cn/problems/find-mode-in-binary-search-tree/solutions/425430/er-cha-sou-suo-shu-zhong-de-zhong-shu-by-leetcode-/
        /// 
        /// 實作參考
        /// https://leetcode.cn/problems/find-mode-in-binary-search-tree/solutions/1461272/501-er-cha-sou-suo-shu-zhong-de-zhong-sh-tull/
        /// 樹的走訪（也稱為樹的遍歷或樹的搜尋）
        /// https://zh.wikipedia.org/zh-tw/%E6%A0%91%E7%9A%84%E9%81%8D%E5%8E%86
        /// 
        /// 中序走訪（In-Order Traversal）是依序以左節點、根節點、右節點為順序走訪的方式。
        /// 中序 數值剛好是由小到大, 數值差不多的會剛好相鄰
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int[] FindMode(TreeNode root)
        {
            // 遍歷前先給予初始預設數值
            prev = int.MinValue;
            freq = 0;
            maxFreq = 0;
            modesList = new List<int>();
            Inorder(root);
            return modesList.ToArray();
        }


        /// <summary>
        /// 中序
        /// 左, 中, 右 順序
        /// </summary>
        /// <param name="node"></param>
        public static void Inorder(TreeNode node)
        {
            if(node == null)
            {
                return;
            }

            // 左
            Inorder(node.left);

            //中, or 數值一樣就頻率++
            if(node.val == prev)
            {
                freq++;
            }
            else
            {
                // 不然就是當預設, 頻率1開始
                prev = node.val;
                freq = 1;
            }

            if(freq == maxFreq)
            {
                // 相同就加入
                modesList.Add(node.val);
            }
            else if(freq > maxFreq)
            {
                // 大於 就替換, 舊的清空後再加入
                maxFreq = freq;
                modesList.Clear();
                modesList.Add(node.val);
            }

            // 右
            Inorder(node.right);

        }

    }
}
