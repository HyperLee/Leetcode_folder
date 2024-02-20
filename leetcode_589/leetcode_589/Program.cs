using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_589
{
    internal class Program
    {
        public class Node
        {
            public int val;
            public IList<Node> children;

            public Node() { }

            public Node(int _val)
            {
                val = _val;
            }

            public Node(int _val, IList<Node> _children)
            {
                val = _val;
                children = _children;
            }
        }


        /// <summary>
        /// 589. N-ary Tree Preorder Traversal
        /// https://leetcode.com/problems/n-ary-tree-preorder-traversal/description/
        /// 589. N 叉树的前序遍历
        /// https://leetcode.cn/problems/n-ary-tree-preorder-traversal/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }


        /// <summary>
        /// 本題目類似題為 leetcode_590
        /// 一個前序, 另一個為後序
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IList<int> Preorder(Node root)
        {
            IList<int> list = new List<int>();
            PreorderVisit(root, list);
            return list;
        }


        /// <summary>
        /// 前序遍歷 (Preorder Traversal)
        /// 前序遍歷：順序是根節點、左子節點、右子節點，根排在前面。
        /// 
        /// 
        /// N-way tree
        /// 顺序是先依次递归其 children 数组中的节点（子树），再访问根节点。
        /// </summary>
        /// <param name="node"></param>
        /// <param name="res"></param>
        public static void PreorderVisit(Node node, IList<int> res)
        {
            if(node == null)
            {
                return;
            }

            // 根排在前面
            res.Add(node.val);

            // 找左右子樹
            foreach(Node child in node.children) 
            {
                PreorderVisit(child, res);
            }

        }
    }
}
