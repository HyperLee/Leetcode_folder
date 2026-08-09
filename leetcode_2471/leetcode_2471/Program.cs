namespace leetcode_2471
{
    internal class Program
    {
        /// <summary>
        /// 表示二元樹中的單一節點，保存節點值與左右子樹參考。
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立指定值與子節點的二元樹節點。
            /// </summary>
            /// <param name="val">節點保存的整數值。</param>
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
        /// <para>
        /// 2471. Minimum Number of Operations to Sort a Binary Tree by Level
        /// https://leetcode.com/problems/minimum-number-of-operations-to-sort-a-binary-tree-by-level/description/
        ///
        /// You are given a binary tree whose values are unique. In one operation, choose any two nodes on the same level and swap their values. Return the minimum operations required to make each level's values strictly increasing. A node's level is the number of edges on the path from that node to the root.
        ///
        /// Images: https://assets.leetcode.com/uploads/2022/09/18/image-20220918174006-2.png, https://assets.leetcode.com/uploads/2022/09/18/image-20220918174026-3.png, and https://assets.leetcode.com/uploads/2022/09/18/image-20220918174052-4.png
        ///
        /// Example 1:
        /// Input: root = [1,4,3,7,6,8,5,null,null,null,null,9,null,10]
        /// Output: 3
        /// Explanation: Swap 4 and 3 so level 2 becomes [3,4]. Swap 7 and 5 so level 3 becomes [5,6,8,7]. Swap 8 and 7 so level 3 becomes [5,6,7,8]. This uses 3 operations, which is minimal.
        ///
        /// Example 2:
        /// Input: root = [1,3,2,7,6,5,4]
        /// Output: 3
        /// Explanation: Swap 3 and 2 so level 2 becomes [2,3]. Swap 7 and 4 so level 3 becomes [4,6,5,7]. Swap 6 and 5 so level 3 becomes [4,5,6,7]. This uses 3 operations, which is minimal.
        ///
        /// Example 3:
        /// Input: root = [1,2,3,4,5,6]
        /// Output: 0
        /// Explanation: Every level is already sorted in increasing order.
        ///
        /// Constraints:
        /// - The number of nodes is in [1,10^5].
        /// - 1 &lt;= Node.val &lt;= 10^5
        /// - Every tree value is unique.
        /// </para>
        /// <para>
        /// 2471. 逐層排序二元樹所需的最少操作次數
        /// https://leetcode.cn/problems/minimum-number-of-operations-to-sort-a-binary-tree-by-level/description/
        ///
        /// 給定一棵節點值皆唯一的二元樹。一次操作可選擇同一層的任兩個節點並交換其值。回傳使每一層的值嚴格遞增所需的最少操作次數。節點的層數，是該節點到根節點路徑上的邊數。
        ///
        /// 圖片：https://assets.leetcode.com/uploads/2022/09/18/image-20220918174006-2.png、https://assets.leetcode.com/uploads/2022/09/18/image-20220918174026-3.png 與 https://assets.leetcode.com/uploads/2022/09/18/image-20220918174052-4.png
        ///
        /// 範例 1：
        /// 輸入：root = [1,4,3,7,6,8,5,null,null,null,null,9,null,10]
        /// 輸出：3
        /// 說明：交換 4、3，使第 2 層成為 [3,4]；交換 7、5，使第 3 層成為 [5,6,8,7]；交換 8、7，使第 3 層成為 [5,6,7,8]。共使用 3 次操作，且已是最少次數。
        ///
        /// 範例 2：
        /// 輸入：root = [1,3,2,7,6,5,4]
        /// 輸出：3
        /// 說明：交換 3、2，使第 2 層成為 [2,3]；交換 7、4，使第 3 層成為 [4,6,5,7]；交換 6、5，使第 3 層成為 [4,5,6,7]。共使用 3 次操作，且已是最少次數。
        ///
        /// 範例 3：
        /// 輸入：root = [1,2,3,4,5,6]
        /// 輸出：0
        /// 說明：每一層都已按遞增順序排列。
        ///
        /// 限制條件：
        /// - 節點數量在 [1,10^5] 範圍內。
        /// - 1 &lt;= Node.val &lt;= 10^5
        /// - 樹中的每個值皆唯一。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 以固定的 level-order 資料建立測試樹，逐一執行兩種解法，並輸出預期值、實際值與 PASS/FAIL。
        /// </remarks>
        /// <param name="args">保留給主控台入口使用的命令列參數；本範例不需要額外參數。</param>
        static int Main(string[] args)
        {
            (string Name, int?[] Values, int Expected)[] testCases =
            {
                ("LeetCode Example 1", new int?[] { 1, 4, 3, 7, 6, 8, 5, null, null, null, null, 9, null, 10 }, 3),
                ("LeetCode Example 2", new int?[] { 1, 3, 2, 7, 6, 5, 4 }, 3),
                ("LeetCode Example 3", new int?[] { 1, 2, 3, 4, 5, 6 }, 0),
                ("Single node", new int?[] { 1 }, 0),
                ("One swap within a level", new int?[] { 1, 3, 2 }, 1),
                ("Right-skewed sparse tree", new int?[] { 1, null, 2, null, 3 }, 0)
            };

            Console.WriteLine("=== Test Cases ===");
            int passedCases = 0;
            foreach ((string name, int?[] values, int expected) in testCases)
            {
                if (RunCase(name, values, expected))
                {
                    passedCases++;
                }
            }

            Console.WriteLine($"Summary: {passedCases}/{testCases.Length} cases passed.");
            return passedCases == testCases.Length ? 0 : 1;
        }


        /// <summary>
        /// 使用逐層 BFS、排序與目標索引交換，計算將每一層整理為嚴格遞增所需的最少交換次數。
        /// </summary>
        /// <remarks>
        /// ref:
        /// https://leetcode.cn/problems/minimum-number-of-operations-to-sort-a-binary-tree-by-level/solutions/3015805/2471-zhu-ceng-pai-xu-er-cha-shu-suo-xu-d-dr3c/
        /// https://leetcode.cn/problems/minimum-number-of-operations-to-sort-a-binary-tree-by-level/solutions/1965422/by-endlesscheng-97i9/
        /// https://leetcode.cn/problems/minimum-number-of-operations-to-sort-a-binary-tree-by-level/solutions/1965867/by-liu-wan-qing-zjlj/
        /// 
        /// 實作之前先參考 題目: 102. Binary Tree Level Order Traversal (二叉树的层序遍历)
        /// 如何取出每一層資料, 基於這項知識再去進行排序
        /// 
        /// 一開始會先將 root 加入 queue
        /// 搜尋過後, 就會移出 Dequeue
        /// 將左右非空子樹加入 Enqueue
        /// 這樣可以避免重覆搜尋
        /// 
        /// 將每層的 node.val 找出來後
        /// 1. 將原始資料做遞增排序
        /// 2. 未排序原始資料
        /// 3. 將 1 與 2 比對
        /// 4. 不同就進行 swap
        /// 
        /// swap(輸入資料, 原始資料 index, 預期正確資料 index)
        /// 進行交換, 同時累計 交換次數
        ///
        /// 題目保證 root 非空且所有節點值唯一，因此可以用值直接對應到排序後的目標索引。
        /// </summary>
        /// <param name="root">要處理的非空二元樹根節點。</param>
        /// <returns>所有樹層完成排序所需的最少交換次數。</returns>
        public static int MinimumOperations(TreeNode root)
        {
            int result = 0;
            foreach (int[] values in GetLevelValues(root))
            {
                int[] sortedValues = (int[])values.Clone();
                Array.Sort(sortedValues);

                Dictionary<int, int> targetIndices = new Dictionary<int, int>();
                for (int i = 0; i < sortedValues.Length; i++)
                {
                    targetIndices.Add(sortedValues[i], i);
                }

                // 每一層可獨立排序；將目前值直接放到它的目標索引，能讓一個值固定到正確位置。
                for (int i = 0; i < values.Length; i++)
                {
                    while (values[i] != sortedValues[i])
                    {
                        int targetIndex = targetIndices[values[i]];
                        Swap(values, i, targetIndex);
                        result++;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 使用置換循環分解，計算逐層排序二元樹所需的最少交換次數。
        /// </summary>
        /// <remarks>
        /// 排序後每個值都有唯一目標索引；若目前索引沿著「目前值的目標索引」移動形成長度為 k 的循環，
        /// 便至少需要 k - 1 次任意位置交換，也可以剛好用 k - 1 次交換完成。
        /// </remarks>
        /// <param name="root">要處理的非空二元樹根節點。</param>
        /// <returns>所有樹層完成排序所需的最少交換次數。</returns>
        public static int MinimumOperationsByCycles(TreeNode root)
        {
            int result = 0;
            foreach (int[] values in GetLevelValues(root))
            {
                int[] sortedValues = (int[])values.Clone();
                Array.Sort(sortedValues);

                Dictionary<int, int> targetIndices = new Dictionary<int, int>();
                for (int i = 0; i < sortedValues.Length; i++)
                {
                    targetIndices.Add(sortedValues[i], i);
                }

                bool[] visited = new bool[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    if (visited[i] || targetIndices[values[i]] == i)
                    {
                        continue;
                    }

                    int currentIndex = i;
                    int cycleLength = 0;
                    while (!visited[currentIndex])
                    {
                        visited[currentIndex] = true;
                        currentIndex = targetIndices[values[currentIndex]];
                        cycleLength++;
                    }

                    // 長度為 k 的循環可用 k - 1 次交換固定，且這是任意交換的下界。
                    result += cycleLength - 1;
                }
            }

            return result;
        }


        /// <summary>
        /// 以 BFS 由上到下、由左到右逐層擷取二元樹節點值。
        /// </summary>
        /// <param name="root">要遍歷的非空二元樹根節點。</param>
        /// <returns>依樹的層級順序逐次產生每一層的節點值陣列。</returns>
        private static IEnumerable<int[]> GetLevelValues(TreeNode root)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                int[] values = new int[levelSize];

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode node = queue.Dequeue();
                    values[i] = node.val;

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }

                yield return values;
            }
        }


        /// <summary>
        /// 將以 level-order 表示的 nullable 值陣列建立為二元樹。
        /// </summary>
        /// <param name="values">由根開始的 level-order 值；<see langword="null"/> 表示缺少子節點。</param>
        /// <returns>建立完成的非空二元樹根節點。</returns>
        private static TreeNode BuildTree(params int?[] values)
        {
            if (values.Length == 0)
            {
                throw new ArgumentException("測試樹至少需要一個非空根節點。", nameof(values));
            }

            int rootValue = values[0]
                ?? throw new ArgumentException("測試樹根節點不可為空。", nameof(values));
            TreeNode root = new TreeNode(rootValue);
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            int index = 1;

            while (queue.Count > 0 && index < values.Length)
            {
                TreeNode node = queue.Dequeue();

                if (values[index] is int leftValue)
                {
                    TreeNode left = new TreeNode(leftValue);
                    node.left = left;
                    queue.Enqueue(left);
                }

                index++;
                if (index < values.Length && values[index] is int rightValue)
                {
                    TreeNode right = new TreeNode(rightValue);
                    node.right = right;
                    queue.Enqueue(right);
                }

                index++;
            }

            return root;
        }


        /// <summary>
        /// 執行單一固定案例，並比較兩種解法與預期最少交換次數。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="values">案例的 level-order 樹資料。</param>
        /// <param name="expected">依題意預期的最少交換次數。</param>
        /// <returns>兩種解法都符合預期時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunCase(string name, int?[] values, int expected)
        {
            TreeNode root = BuildTree(values);
            int actualBySwaps = MinimumOperations(root);
            int actualByCycles = MinimumOperationsByCycles(root);
            bool passed = actualBySwaps == expected
                && actualByCycles == expected
                && actualBySwaps == actualByCycles;

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"  Expected: {expected}");
            Console.WriteLine($"  Actual (MinimumOperations): {actualBySwaps}");
            Console.WriteLine($"  Actual (MinimumOperationsByCycles): {actualByCycles}");
            Console.WriteLine($"  Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed;
        }


        /// <summary>
        /// 交換整數陣列中兩個索引位置的值。
        /// </summary>
        /// <param name="arr">要修改的陣列。</param>
        /// <param name="index1">第一個交換位置。</param>
        /// <param name="index2">第二個交換位置。</param>
        public static void Swap(int[] arr, int index1, int index2)
        {
            int temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }
    }
}