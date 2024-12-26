namespace leetcode_226
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


    internal class Program
    {
        /// <summary> 
        /// 226. Invert Binary Tree
        /// https://leetcode.com/problems/invert-binary-tree/description/?envType=study-plan-v2&envId=top-interview-150
        /// 
        /// 226. 翻转二叉树
        /// https://leetcode.cn/problems/invert-binary-tree/description/
        /// 
        /// 翻轉二元樹 root 不變 左右子樹翻轉
        /// 
        /// InvertTree 是 翻轉後的 答案
        /// 但是 tree 輸出顯示 要依靠
        /// 前中後序遍歷才能輸出樹狀結構
        /// 本題分別使用三種遍歷 展示輸出答案
        /// 
        /// 前中後序, 所謂的前中後是指根節點位置在哪裡
        /// 前序就是 前面
        /// 依此類蓷
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(4);

            root.left = new TreeNode(2);
            root.right = new TreeNode(7);

            root.left.left = new TreeNode(1);
            root.left.right = new TreeNode(3);
            root.right.left = new TreeNode(6);
            root.right.right = new TreeNode(9);

            var res = InvertTree(root);

            Console.WriteLine("輸出採用中序遍歷, 訪問順序：左子樹 -> 根節點 -> 右子樹");
            InOrder(res);
            Console.WriteLine("");
            
            Console.WriteLine("輸出採用前序遍歷, 訪問順序：根節點 -> 左子樹 -> 右子樹");
            PreOrder(res);
            Console.WriteLine("");
            
            Console.WriteLine("輸出採用後序遍歷, 訪問順序：左子樹 -> 右子樹 -> 根節點");
            PostOrder(res);
            Console.WriteLine("");
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10227341
        /// https://leetcode.cn/problems/invert-binary-tree/solution/fan-zhuan-er-cha-shu-by-leetcode-solution/
        /// 
        /// 採用 遞迴 作法
        /// 將 tree 反轉, 
        /// 1. 判斷 root 是否為 null，若為 null 回傳 root;
        /// 2. 宣告 TreeNode tmpLeft 為 root.left 暫存;
        /// 3. 宣告 TreeNode tmpRight 為 root.right 暫存;
        /// 4. 此時使用遞迴將所有 TreeNode 對調 
        ///     root.left = InvertTree(tmpRight);
        ///     root.right = InvertTree(tmpLeft);
        ///     對調完成後回傳 root
        ///     
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
            {
                // 也可以 return null, 一樣意思
                return root;
            }

            // 左子樹暫存
            TreeNode tmpleft = root.left;
            // 右子樹暫存
            TreeNode tmpright = root.right;

            // 遞迴 將左右子樹交換存放
            root.left = InvertTree(tmpright);
            root.right = InvertTree(tmpleft);

            return root;
        }


        /// <summary>
        /// 中序遍歷 InOrder Traversal
        /// 訪問順序：左子樹 -> 根節點 -> 右子樹
        /// 特點：對於二元搜尋樹，這種遍歷方式可以用來生成二叉樹的排序序列。
        /// </summary>
        /// <param name="node"></param>
        public static void InOrder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            InOrder(node.left);
            Console.Write(node.val + " ");
            InOrder(node.right);
        }


        /// <summary>
        /// 前序遍歷 PreOrder Traversal
        /// 訪問順序：根節點 -> 左子樹 -> 右子樹
        /// 特點：通常用於複製樹結構或計算樹的高度。
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


        /// <summary>
        /// 後序遍歷 PostOrder Traversal
        /// 訪問順序：左子樹 -> 右子樹 -> 根節點
        /// 特點：通常用於刪除樹或計算樹的高度。
        /// </summary>
        /// <param name="node"></param>
        public static void PostOrder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            PostOrder(node.left);
            PostOrder(node.right);
            Console.Write(node.val + " ");
        }
    }
}
