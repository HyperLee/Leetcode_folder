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
        /// leetcode 108
        /// https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree/description/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] num = new int[] { -10, -3, 0, 5, 9 };
            Console.WriteLine(SortedArrayToBST(num));
            Console.ReadKey();
        }

        /// <summary>
        /// https://leetcode.cn/problems/convert-sorted-array-to-binary-search-tree/solution/jiang-you-xu-shu-zu-zhuan-huan-wei-er-cha-sou-s-33/
        /// 方法一：中序遍历，总是选择中间位置左边的数字作为根节点
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static TreeNode SortedArrayToBST(int[] nums)
        {
            return Helper(nums, 0, nums.Length - 1);
        }


        public static TreeNode Helper(int[] nums, int left, int right)
        {
            if (left > right)
            {
                return null;
            }

            // 总是选择中间位置左边的数字作为根节点
            int mid = (left + right) / 2;

            TreeNode root = new TreeNode(nums[mid]);
            root.left = Helper(nums, left, mid - 1);
            root.right = Helper(nums, mid + 1, right);
            return root;
        }

    }
}
