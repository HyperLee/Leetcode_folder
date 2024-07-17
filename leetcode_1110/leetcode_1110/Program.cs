using static System.Runtime.InteropServices.JavaScript.JSType;

namespace leetcode_1110
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
        /// 1110. Delete Nodes And Return Forest
        /// https://leetcode.com/problems/delete-nodes-and-return-forest/description/?envType=daily-question&envId=2024-07-17
        /// 1110. 删点成林
        /// https://leetcode.cn/problems/delete-nodes-and-return-forest/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.left.left = new TreeNode(4);
            root.left.right = new TreeNode(5);
            root.right = new TreeNode(3);
            root.right.left = new TreeNode(6);
            root.right.right = new TreeNode(7);

            int[] to_delete = { 3, 5 };

            Console.WriteLine(DelNodes(root, to_delete));

            Console.ReadKey();
        }


        /// <summary>
        /// ref: DFS
        /// https://leetcode.cn/problems/delete-nodes-and-return-forest/solutions/2286145/shan-dian-cheng-lin-by-leetcode-solution-gy95/
        /// https://leetcode.cn/problems/delete-nodes-and-return-forest/solutions/2289131/he-shi-ji-lu-da-an-pythonjavacgo-by-endl-lpcd/
        /// https://leetcode.cn/problems/delete-nodes-and-return-forest/solutions/1461555/by-stormsunshine-1b6o/
        /// </summary>
        /// <param name="root"></param>
        /// <param name="to_delete"></param>
        /// <returns></returns>
        public static IList<TreeNode> DelNodes(TreeNode root, int[] to_delete)
        {
            ISet<int> toDeleteSet = new HashSet<int>();
            foreach (var item in to_delete)
            {
                // 預計刪除名單列表
                toDeleteSet.Add(item);
            }

            // 輸出結果用
            IList<TreeNode> roots = new List<TreeNode>();
            DFS(root, true, toDeleteSet, roots);

            //foreach (var str in roots)
            //{
            //    Console.WriteLine(str);
            //}

            return roots;

        }


        /// <summary>
        /// DFS深度優先
        /// 搜尋節點與刪除 預計刪除名單列表
        /// 
        /// 
        /// isRoot預設為true, 一開始輸入一棵樹
        /// 之後就靠判斷(bool deleted)來決定
        /// </summary>
        /// <param name="node"></param>
        /// <param name="isRoot"></param>
        /// <param name="toDeleteSet"></param>
        /// <param name="roots"></param>
        /// <returns></returns>
        public static TreeNode DFS(TreeNode node, bool isRoot, ISet<int> toDeleteSet, IList<TreeNode> roots)
        {
            if (node == null)
            {
                return null;
            }

            // 判斷該node是不是在預計刪除名單中, 如果是他的子節點(左/右子樹)有可能成為潛在新的根節點
            bool deleted = toDeleteSet.Contains(node.val);
            // 左子樹
            node.left = DFS(node.left, deleted, toDeleteSet, roots);
            // 右子樹
            node.right = DFS(node.right, deleted, toDeleteSet, roots);

            if(deleted == true)
            {
                return null;
            }
            else
            {
                if(isRoot == true)
                {
                    // 原先根結點被刪除, 剩餘的左右子樹不再預計刪除名單中
                    // 就把他們加入成為新的根結點, 加入roots中
                    roots.Add(node);
                }

                return node;

            }

        }

    }
}
