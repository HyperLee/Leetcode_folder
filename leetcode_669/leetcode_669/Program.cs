using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_669
{
    class Program
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
        /// leetcode 669
        /// https://leetcode.com/problems/trim-a-binary-search-tree/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            string a = null;
            string b;
            string c = "123";
            b = a ?? c;
            // 如果a = null ,  b=c
            // 如果a 不為null 那就給a的值
            // null != "" 空
            Console.WriteLine(b);
            Console.ReadKey();

        }


        /// <summary>
        /// https://dotblogs.com.tw/abbee/2018/03/08/093549
        /// Given a binary search tree and the lowest and highest boundaries as L and R, 
        /// trim the tree so that all its elements lies in [L, R] (R >= L). 
        /// You might need to change the root of the tree, 
        /// so the result should return the new root of the trimmed binary search tree.
        /// 
        /// L = low
        /// R = high
        /// 
        /// ?? 用法
        /// 用於定義可空類型和引用類型的默認值。如果此運算符的左操作數不為null，則此運算符將返回左操作數，否則返回右操作數。
        /// 例如：a??b 當a為null時則返回b，a不為null時則返回a本身。
        /// 空合並運算符為右結合運算符，即操作時從右向左進行組合的。如，“a??b??c”的形式按“a??(b??c)”計算。
        /// https://www.itread01.com/content/1535523646.html
        /// 
        /// MSDN 官方解說 ?? 運算子 用法
        /// https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/null-coalescing-operator
        /// </summary>
        /// <param name="root"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static TreeNode TrimBST(TreeNode root, int low, int high)
        {
            if (root == null) return null;
            if (root != null && (root.val < low || root.val > high))
                root = TrimBST(root.left, low, high) ?? TrimBST(root.right, low, high);
            if (root == null) return null;
            root.left = TrimBST(root.left, low, high);
            root.right = TrimBST(root.right, low, high);
            return root;


            /*  method2 官方解法
            if (root == null) return root;
            if (root.val > R) return trimBST(root.left, L, R);
            if (root.val < L) return trimBST(root.right, L, R);

            root.left = trimBST(root.left, L, R);
            root.right = trimBST(root.right, L, R);
            return root;
            */
        }


    }
}
