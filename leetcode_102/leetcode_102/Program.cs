using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace leetcode_102
{
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
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
        /// 102. Binary Tree Level Order Traversal
        /// https://leetcode.com/problems/binary-tree-level-order-traversal/description/
        /// 
        /// 102. 二叉树的层序遍历
        /// https://leetcode.cn/problems/binary-tree-level-order-traversal/description/
        /// 
        /// Given the root of a binary tree, return the level order traversal of its nodes' values. (i.e., from left to right, level by level).
        /// 給定一棵二元樹的根節點，返回其節點值的層序遍歷結果。（也就是按照從左到右、逐層的順序進行遍歷）。
        /// 
        /// 層序遍歷, 也就是樹(tree)的每一層進行排序, 注意是"層"
        /// 本題目只是遍歷, 並沒有排序大小. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(3);
            root.left = new TreeNode(9);
            root.right = new TreeNode(20);
            root.right.left = new TreeNode(15);
            root.right.right = new TreeNode(7);

            var res = LevelOrder(root);
            foreach(var item in res)
            {
                Console.Write("[");
                foreach(var value in item)
                {
                    Console.Write(value.ToString() + ", ");
                }
                Console.Write("] ");
            }
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/binary-tree-level-order-traversal/solutions/241885/er-cha-shu-de-ceng-xu-bian-li-by-leetcode-solution/
        /// https://leetcode.cn/problems/binary-tree-level-order-traversal/solutions/1020586/102-er-cha-shu-de-ceng-xu-bian-li-by-hai-7u98/
        /// https://leetcode.cn/problems/binary-tree-level-order-traversal/solutions/1459398/by-stormsunshine-c7iw/
        /// 
        /// 使用廣度優先搜尋
        /// 层序遍历的方法为从根结点开始依次遍历每一层的结点，由于每一层与根结点的距离依次递增，因此可以使用广度优先搜索实现层序遍历。
        /// 詳細說明可以參考 ref 連結 解釋
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if(root == null)
            {
                return res;
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            // root 加入 queue, 儲存待訪問的節點(node)
            queue.Enqueue(root);
            // 逐層搜尋
            while(queue.Count > 0)
            {
                IList<int> levelvalues = new List<int>();
                int size = queue.Count;
                // 遍歷該層每個 node
                for(int i = 0; i < size; i++)
                {
                    // 將該 node 移出
                    TreeNode node = queue.Dequeue();
                    // 該層 node.value
                    levelvalues.Add(node.val);
                    // 該 node 左右非空子節點, 加入 queue
                    if(node.left != null)
                    {
                        // 下一輪待搜尋 node
                        queue.Enqueue(node.left);
                    }
                    if(node.right != null)
                    {
                        // 下一輪待搜尋 node
                        queue.Enqueue(node.right);
                    }
                }

                // 搜尋到之 node.value 加入結果
                res.Add(levelvalues);
            }

            return res;
        }

    }
}
