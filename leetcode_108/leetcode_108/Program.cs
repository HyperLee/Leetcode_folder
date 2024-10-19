using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_108
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
        /// 108. Convert Sorted Array to Binary Search Tree
        /// https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree/description/
        /// 108. 将有序数组转换为二叉搜索树
        /// https://leetcode.cn/problems/convert-sorted-array-to-binary-search-tree/description/
        /// 
        /// 题意：根据升序数组，恢复一棵高度平衡的 BST🌲。
        /// 分析：
        /// BST 的中序遍历是升序的，因此本题等同于根据中序遍历的序列恢复二叉搜索树。
        /// 因此我们可以以升序序列中的任一个元素作为根节点，以该元素左边的升序序列构建左子树，以该元素右边的升序序列构建右子树
        /// ，这样得到的树就是一棵二叉搜索树啦～ 又因为本题要求高度平衡
        /// ，因此我们需要选择升序序列的中间元素作为根节点奥～
        /// 
        /// 注意本題目 輸出解答可以能不唯一
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] num = new int[] { -10, -3, 0, 5, 9 };
            //Console.WriteLine(SortedArrayToBST(num));

            var res = SortedArrayToBST(num);
            PreOrder(res);

            Console.ReadKey();
        }

        /// <summary>
        /// https://leetcode.cn/problems/convert-sorted-array-to-binary-search-tree/solution/jiang-you-xu-shu-zu-zhuan-huan-wei-er-cha-sou-s-33/
        /// 方法一：中序遍历，总是选择中间位置左边的数字作为根节点
        /// 
        /// https://leetcode.cn/problems/convert-sorted-array-to-binary-search-tree/solutions/313508/jian-dan-di-gui-bi-xu-miao-dong-by-sweetiee/
        /// https://leetcode.cn/problems/convert-sorted-array-to-binary-search-tree/solutions/2927064/ru-men-di-gui-cong-er-cha-shu-kai-shi-py-inu6/
        /// 
        /// 平衡樹: 平衡指所有葉子的深度趨於平衡，更廣義的是指在樹上所有可能尋找的均攤複雜度偏低。 
        /// https://zh.wikipedia.org/zh-tw/%E5%B9%B3%E8%A1%A1%E6%A0%91
        /// 
        /// 樹的走訪
        /// https://zh.wikipedia.org/zh-tw/%E6%A0%91%E7%9A%84%E9%81%8D%E5%8E%86
        /// 
        /// 中序走訪:中序走訪（In-Order Traversal）是依序以左節點、根節點、右節點為順序走訪的方式。 => 陣列數值會是一個升序
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static TreeNode SortedArrayToBST(int[] nums)
        {
            return Helper(nums, 0, nums.Length - 1);
        }


        /// <summary>
        /// 取出陣列中 中間的數值出來當作 root 來建立 平衡樹
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static TreeNode Helper(int[] nums, int left, int right)
        {
            if (left > right)
            {
                return null;
            }

            // 总是选择中间位置左边的数字作为根节点
            int mid = (left + right) / 2;

            TreeNode root = new TreeNode(nums[mid]);
            // 遞迴 建立左右子樹
            // 左小
            root.left = Helper(nums, left, mid - 1);
            // 右大
            root.right = Helper(nums, mid + 1, right);

            return root;
        }


        /// <summary>
        /// 前序遍歷 -- 輸出答案顯示
        /// 訪問順序：根節點 -> 左子樹 -> 右子樹
        /// 特點：通常用於複製樹的結構。
        /// </summary>
        /// <param name="node"></param>
        public static void PreOrder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            Console.Write(node.val + " ");
            PreOrder(node.left);
            PreOrder(node.right);
        }

    }
}
