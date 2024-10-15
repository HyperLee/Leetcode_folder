using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_114
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
        /// 114. Flatten Binary Tree to Linked List
        /// https://leetcode.com/problems/flatten-binary-tree-to-linked-list/
        /// 
        /// 114. 二叉树展开为链表
        /// https://leetcode.cn/problems/flatten-binary-tree-to-linked-list/description/
        /// 
        /// 給定二元樹的根節點，將該樹「展平成一個鏈結串列」：
        /// 「鏈結串列」應使用相同的 TreeNode 類別，其中右子節點指標指向串列中的下一個節點，而左子節點指標則永遠為 null。
        /// 「鏈結串列」的節點順序應與二元樹的先序遍歷（pre-order traversal）順序相同。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1); // root
            root.left = new TreeNode(9);
            root.right = new TreeNode(2);

            Flatten(root);

            Console.ReadKey();
        }


        /// <summary>
        ///  Preorder traversal (前序遍歷) 需先拜訪父節點再拜訪左右子節點。
        ///  
        /// 对于每个结点，将其左子结点指针设为 null，
        /// 将其右子结点指针设为前序遍历顺序的后一个结点
        /// ，其中前序遍历顺序的最后一个结点的右子结点指针也设为 null。
        /// => prev -> curr 類似這樣顯示 prev 後面接上 curr 
        /// 
        /// 簡單說就是將 tree 用前序遍歷轉成 linklist
        /// 且 linklist 只存在右子樹有資料, 左子樹保持 null
        /// 
        /// List 要宣告為 TreeNode 型別
        /// </summary>
        /// <param name="root"></param>
        public static void Flatten(TreeNode root)
        {
            List<TreeNode> list = new List<TreeNode>();
            preorder(root, list);

            int size = list.Count;
            for(int i = 1; i < size; i++)
            {
                TreeNode prev = list[i - 1];
                TreeNode curr = list[i];

                // 左子樹保持 null
                prev.left = null;
                // 右子樹才需要放置資料
                prev.right = curr;

                Console.WriteLine("prev: " + prev.val.ToString());
                Console.WriteLine("curr: " + curr.val.ToString());
                
            }

        }


        /// <summary>
        /// tree 基本考題
        ///  Preorder traversal (前序遍歷) 需先拜訪父節點再拜訪左右子節點。
        /// </summary>
        /// <param name="root"></param>
        /// <param name="list"></param>
        public static void preorder(TreeNode root, List<TreeNode> list)
        {
            if(root != null)
            {
                list.Add(root);

                if (root.left != null)
                {
                    preorder(root.left, list);
                }

                if (root.right != null)
                {
                    preorder(root.right, list);
                }

            }
        }


        /// <summary>
        /// 前序遍歷
        /// 訪問順序：根節點 -> 左子樹 -> 右子樹
        /// 特點：通常用於複製樹的結構。
        /// 
        /// 輸出 tree 資料顯示使用
        /// tree 要經過遍歷才能輸出資料, 無法直接顯示
        /// </summary>
        /// <param name="node"></param>
        public static void PreOrder_show(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            Console.WriteLine(node.val + " ");
            PreOrder_show(node.left);
            PreOrder_show(node.right);
        }

    }
}
