namespace leetcode_199
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
        /// 199. Binary Tree Right Side View
        /// https://leetcode.com/problems/binary-tree-right-side-view/description/
        /// 
        /// 199. 二叉树的右视图
        /// https://leetcode.cn/problems/binary-tree-right-side-view/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            root.left.right = new TreeNode(5);
            root.right.right = new TreeNode(4);

            IList<int> result = RightSideView(root);
            Console.WriteLine("res: " + string.Join(", ", result));
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/binary-tree-right-side-view/solutions/213494/er-cha-shu-de-you-shi-tu-by-leetcode-solution/
        /// https://leetcode.cn/problems/binary-tree-right-side-view/solutions/2015061/ru-he-ling-huo-yun-yong-di-gui-lai-kan-s-r1nc/
        /// https://leetcode.cn/problems/binary-tree-right-side-view/solutions/1459266/199-er-cha-shu-de-you-shi-tu-by-stormsun-dj0b/
        /// 
        /// 取得二叉樹的右視圖
        /// 使用深度優先搜索(DFS)，優先遍歷右子樹
        /// 當遍歷到新的深度時，第一個看到的節點即為該層右視圖可見的節點
        /// </summary>
        /// <param name="root">二叉樹根節點</param>
        /// <returns>右視圖節點值的列表</returns>
        public static IList<int> RightSideView(TreeNode root)
        {
            List<int> res = new List<int>();
            dfs(root, 0, res);
            return res;
        }

        /// <summary>
        /// 深度優先搜索遍歷二叉樹
        /// 先遍歷右子樹，再遍歷左子樹，確保同一深度先訪問到右邊的節點
        /// 
        /// 為什麼第一個看到的節點會是該層右視圖的節點：
        /// 因為我們首先訪問右子樹，當我們到達新的深度時，右子樹的節點會最先被訪問到。
        /// 如果右子樹是空的，那麼左子樹的節點會被訪問到。
        /// 因此，第一個訪問到的節點一定是該層最右邊的節點。
        /// 
        /// </summary>
        /// <param name="root">當前節點</param>
        /// <param name="depth">當前深度</param>
        /// <param name="ans">結果列表</param>
        private static void dfs(TreeNode root, int depth, IList<int> ans)
        {
            // 如果節點為空，則返回
            if (root == null)
            {
                return;
            }

            // 如果當前深度等於結果列表的長度，表示是該層第一個訪問的節點
            // 將該節點的值加入結果列表
            if (depth == ans.Count)
            {
                ans.Add(root.val);
            }

            depth++; // 深度加1
            dfs(root.right, depth, ans); // 優先遍歷右子樹
            dfs(root.left, depth, ans);  // 再遍歷左子樹
        }
    }
}
