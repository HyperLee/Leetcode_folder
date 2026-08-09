namespace leetcode_543
{
    internal class Program
    {
        /// <summary>
        /// 表示二元樹中的一個節點，包含整數值以及可為空的左右子節點。
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立二元樹節點；未提供左右子節點時，該方向視為空子樹。
            /// </summary>
            /// <param name="val">節點儲存的整數值，題目限制為 -100 到 100。</param>
            /// <param name="left">左子節點；沒有左子樹時為 <see langword="null"/>。</param>
            /// <param name="right">右子節點；沒有右子樹時為 <see langword="null"/>。</param>
            public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }


        /// <summary>
        /// 543. Diameter of Binary Tree
        /// https://leetcode.com/problems/diameter-of-binary-tree/description/
        /// <para>
        /// Given the root of a binary tree, return the length of the tree's diameter.
        ///
        /// The diameter is the length of the longest path between any two nodes in a tree. This path may or may not pass through root.
        ///
        /// The length of a path between two nodes is represented by the number of edges between them.
        ///
        /// Example 1:
        /// Image: https://assets.leetcode.com/uploads/2021/03/06/diamtree.jpg
        /// Input: root = [1,2,3,4,5]
        /// Output: 3
        /// Explanation: 3 is the length of the path [4,2,1,3] or [5,2,1,3].
        ///
        /// Example 2:
        /// Input: root = [1,2]
        /// Output: 1
        ///
        /// Constraints:
        /// - The number of nodes in the tree is in [1, 10^4].
        /// - -100 &lt;= Node.val &lt;= 100
        /// </para>
        /// <para>
        /// 543. 二元樹的直徑
        /// https://leetcode.cn/problems/diameter-of-binary-tree/description/
        ///
        /// 給定二元樹的根節點 root，回傳這棵樹的直徑長度。
        ///
        /// 二元樹的直徑，是樹中任意兩個節點之間最長路徑的長度。此路徑可能經過 root，也可能不經過。
        ///
        /// 兩個節點之間的路徑長度，以它們之間的邊數表示。
        ///
        /// 範例 1：
        /// 圖片：https://assets.leetcode.com/uploads/2021/03/06/diamtree.jpg
        /// 輸入：root = [1,2,3,4,5]
        /// 輸出：3
        /// 解釋：路徑 [4,2,1,3] 或 [5,2,1,3] 的長度都是 3。
        ///
        /// 範例 2：
        /// 輸入：root = [1,2]
        /// 輸出：1
        ///
        /// 限制條件：
        /// - 樹中節點數量在 [1, 10^4] 範圍內。
        /// - -100 &lt;= Node.val &lt;= 100
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 建立五棵具代表性的二元樹並逐一執行直徑計算，
        /// 將預期結果與實際結果並列輸出，最後彙整通過案例數。
        /// 此方法不接受輸入，所有案例皆為固定且可重複執行的教學資料。
        /// </summary>
        private static void RunSamples()
        {
            (string Name, string Input, TreeNode Root, int Expected)[] sampleCases =
            [
                (
                    "官方範例一",
                    "[1,2,3,4,5]",
                    new TreeNode(
                        1,
                        new TreeNode(2, new TreeNode(4), new TreeNode(5)),
                        new TreeNode(3)),
                    3
                ),
                (
                    "官方範例二",
                    "[1,2]",
                    new TreeNode(1, new TreeNode(2)),
                    1
                ),
                (
                    "單一節點",
                    "[1]",
                    new TreeNode(1),
                    0
                ),
                (
                    "最長路徑不經過根節點",
                    "[1,2,null,3,4,5,null,null,6]",
                    new TreeNode(
                        1,
                        new TreeNode(
                            2,
                            new TreeNode(3, new TreeNode(5)),
                            new TreeNode(4, null, new TreeNode(6)))),
                    4
                ),
                (
                    "向右偏斜樹",
                    "[1,null,2,null,3,null,4]",
                    new TreeNode(
                        1,
                        null,
                        new TreeNode(
                            2,
                            null,
                            new TreeNode(3, null, new TreeNode(4)))),
                    3
                )
            ];

            int passed = 0;

            for (int index = 0; index < sampleCases.Length; index++)
            {
                (string name, string input, TreeNode root, int expected) = sampleCases[index];
                int actual = DiameterOfBinaryTree(root);
                bool isPassed = actual == expected;

                if (isPassed)
                {
                    passed++;
                }

                Console.WriteLine($"案例 {index + 1}：{name}");
                Console.WriteLine($"輸入：root = {input}");
                Console.WriteLine($"預期：{expected}");
                Console.WriteLine($"實際：{actual} => {(isPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passed}/{sampleCases.Length} 筆測試通過");
        }

        public static int max = 0;

        /// <summary>
        /// 計算二元樹中任意兩個節點之間的最長路徑邊數。
        /// 透過後序深度優先搜尋取得每個節點的左右子樹高度，
        /// 並以兩側高度總和更新全樹直徑，因此最長路徑不必經過根節點。
        /// </summary>
        /// <param name="root">二元樹根節點；空樹可傳入 <see langword="null"/>。</param>
        /// <returns>樹中最長路徑包含的邊數；空樹或單一節點回傳 0。</returns>
        public static int DiameterOfBinaryTree(TreeNode? root)
        {
            // 每次公開入口呼叫都從 0 開始，避免前一棵樹的答案污染本次結果。
            max = 0;
            MaxDepth(root);
            return max;
        }


        /// <summary>
        /// 以後序深度優先搜尋計算目前子樹的最大高度。
        /// 左右子樹高度相加代表通過目前節點的直徑候選值，
        /// 更新全域最大值後，再向父節點回傳較高分支加上目前節點的一層。
        /// </summary>
        /// <param name="root">目前子樹的根節點；空子樹可傳入 <see langword="null"/>。</param>
        /// <returns>從目前節點向下到最深葉節點的節點數；空子樹回傳 0。</returns>
        public static int MaxDepth(TreeNode? root)
        {
            if (root == null)
            {
                return 0;
            }

            // 後序遍歷先取得左右子樹高度，才能計算通過目前節點的直徑。
            int left = MaxDepth(root.left);
            int right = MaxDepth(root.right);

            // 左右高度相加是以目前節點為轉折點時，路徑所包含的邊數。
            max = Math.Max(max, left + right);

            // 父節點只能選擇其中一側延伸，因此回傳較高分支再加上目前節點。
            return Math.Max(left, right) + 1;
        }
    }
}
