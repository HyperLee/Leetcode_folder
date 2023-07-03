using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_655
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
        /// leetcode 655. Print Binary Tree
        /// https://leetcode.com/problems/print-binary-tree/
        /// 655. 输出二叉树
        /// https://leetcode.cn/problems/print-binary-tree/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);

            //Console.WriteLine(PrintTree(root));
            PrintTree(root);
            Console.ReadKey();
        }


        /// <summary>
        /// 方法一: 官方
        /// https://leetcode.cn/problems/print-binary-tree/solution/shu-chu-er-cha-shu-by-leetcode-solution-cnxu/
        /// 方法二
        /// https://leetcode.cn/problems/print-binary-tree/solution/by-ac_oier-mays/
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IList<IList<string>> PrintTree(TreeNode root)
        {
            int height = CalDepth(root);
            // 行數 高從0開始
            int m = height + 1;
            // 列數 2^(height + 1) - 1
            int n = (1 << (height + 1)) - 1;
            IList<IList<string>> res = new List<IList<string>>();

            // 先創建一顆樹, value 先塞空
            for (int i = 0; i < m; i++)
            {
                IList<string> row = new List<string>();

                for (int j = 0; j < n; j++)
                {
                    row.Add("");
                }

                res.Add(row);
            }

            DFS(res, root, 0, (n - 1) / 2, height);

            foreach (var str in res)
            {
                Console.Write("[");
                
                for (int i = 0; i < str.Count; i++)
                {
                    if (str[i].Trim() == "")
                    {
                        if (i == str.Count - 1)
                        {
                            Console.Write("\"\"");
                        }
                        else
                        {
                            Console.Write("\"\", ");
                        }
                    }
                    else
                    {
                        if(i == str.Count - 1)
                        {
                            Console.Write(str[i]);
                        }
                        else
                        {
                            Console.Write(str[i] + ", ");
                        }
                    }
                }

                Console.Write("]");
                Console.WriteLine();
            }

            return res;
        }

        /// <summary>
        /// Depth
        /// 一開始先透過DFS, 取出高度
        /// </summary>
        /// <param name="root">輸入的tree</param>
        /// <returns></returns>
        public static int CalDepth(TreeNode root)
        {
            int h = 0;

            if (root.left != null)
            {
                h = Math.Max(h, CalDepth(root.left) + 1);
            }

            if (root.right != null)
            {
                h = Math.Max(h, CalDepth(root.right) + 1);
            }

            return h;
        }


        /// <summary>
        /// DFS
        /// 中左右
        /// 透過 深度優先 來把資料塞進去
        /// 依據題目規則來塞入
        /// </summary>
        /// <param name="res">要塞入的tree</param>
        /// <param name="root">輸入的資料(tree)</param>
        /// <param name="r">題目的r</param>
        /// <param name="c">題目的c</param>
        /// <param name="height"></param>
        public static void DFS(IList<IList<string>> res, TreeNode root, int r, int c, int height)
        {
            // root 在 高度為0(最上層) 正中央位置
            res[r][c] = root.val.ToString();

            if (root.left != null)
            {
                // 題目給的左子樹公式
                DFS(res, root.left, r + 1, c - (1 << (height - r - 1)), height);
            }

            if (root.right != null)
            {
                // 題目給的右子樹公式
                DFS(res, root.right, r + 1, c + (1 << (height - r - 1)), height);
            }

        }


    }
}
