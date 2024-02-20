using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_590
{
    internal class Program
    {
        /// <summary>
        /// 宣告這邊要注意
        /// 這是N-way 不是二元樹
        /// 所以不是給左右子樹
        /// 是給 childern
        /// 跟常見的做法略有差異
        /// 
        /// 詳情比較 leetcode_145
        /// </summary>
        public class Node
        {
            public int val;
            public IList<Node> children;

            public Node() { }

            public Node(int _val)
            {
                val = _val;
            }


            /// <summary>
            /// 注意這邊差異
            /// </summary>
            /// <param name="_val"></param>
            /// <param name="_children"></param>
            public Node(int _val, IList<Node> _children)
            {
                val = _val;
                children = _children;
            }
        }


        /// <summary>
        /// 590. N-ary Tree Postorder Traversal
        /// https://leetcode.com/problems/n-ary-tree-postorder-traversal/
        /// 590. N 叉树的后序遍历
        /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/description/?envType=daily-question&envId=Invalid%20Date
        /// 
        /// 二元樹 binary tree
        /// 後序遍歷 (Postorder Traversal)
        /// 後序遍歷：順序是左子節點、右子節點、根節點，根排在後面。
        /// 
        /// N-way tree
        /// 顺序是先依次递归其 children 数组中的节点（子树），再访问根节点。
        /// 
        /// 類似題目 leetcode_145
        /// 145. Binary Tree Postorder Traversal
        /// https://leetcode.com/problems/binary-tree-postorder-traversal/description/
        /// 可以參考
        /// 或許會比較熟悉
        /// 145是二元樹
        /// 這邊不是.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

        }


        /// <summary>
        /// 官方解法:
        /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/solutions/1327327/n-cha-shu-de-hou-xu-bian-li-by-leetcode-txesi/?envType=daily-question&envId=Invalid%20Date
        /// 
        /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/solutions/1459136/by-stormsunshine-tnzr/?envType=daily-question&envId=Invalid%20Date
        /// 
        /// 採用遞迴解法
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IList<int> Postorder(Node root)
        {
            IList<int> res = new List<int>();
            PostorderVisit(root, res);
            return res;

        }


        /// <summary>
        /// 二元樹 binary tree
        /// 後序遍歷 (Postorder Traversal)
        /// 後序遍歷：順序是左子節點、右子節點、根節點，根排在後面。
        /// 
        /// N-way tree
        /// 顺序是先依次递归其 children 数组中的节点（子树），再访问根节点。
        /// 
        /// 每次递归时，先递归访问每个孩子节点，然后再访问根节点即可。
        /// 1. 按照从左到右的顺序，依次对当前结点的每个子树调用递归。
        /// 2. 将当前结点的结点值加入后序遍历序列。
        /// 
        /// 
        /// 這是N-way 不是二元樹
        /// 所以不是給左右子樹
        /// 是給 childern
        /// 跟常見的做法略有差異
        /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/solutions/2645191/jian-dan-dfspythonjavacgojs-by-endlessch-ytdk/?envType=daily-question&envId=2024-02-19
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="res"></param>
        public static void PostorderVisit(Node root, IList<int> res)
        {
            if(root == null)
            {
                return ;
            }

            // 順序是左子節點、右子節點
            foreach (Node node in root.children) 
            {
                PostorderVisit(node, res);
            }

            // 根排在後面。
            res.Add(root.val);

        }
    }
}
