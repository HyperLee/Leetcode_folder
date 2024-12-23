namespace leetcode_2471
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
        /// 2471. Minimum Number of Operations to Sort a Binary Tree by Level
        /// https://leetcode.com/problems/minimum-number-of-operations-to-sort-a-binary-tree-by-level/description/?envType=daily-question&envId=2024-12-23
        /// 
        /// 2471. 逐层排序二叉树所需的最少操作数目
        /// https://leetcode.cn/problems/minimum-number-of-operations-to-sort-a-binary-tree-by-level/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);

            root.left = new TreeNode(3);
            root.right = new TreeNode(2);

            root.left. left = new TreeNode(7);
            root.left.right = new TreeNode(6);

            root.right.left = new TreeNode(5);
            root.right.right = new TreeNode(4);

            Console.WriteLine("res: " + MinimumOperations(root));
        }


        /// <summary>
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
        /// 1. 將資料遞增排序
        /// 2. 未排序原始資料
        /// 3. 將 1 與 2 比對
        /// 4. 不同就進行 swap
        /// 
        /// swap(輸入資料, 原始資料 index, 預期正確資料 index)
        /// 進行交換, 同時累計 交換次數
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MinimumOperations(TreeNode root)
        {
            int res = 0;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            // root 加入 queue
            queue.Enqueue(root);
            // 逐層搜尋
            while (queue.Count > 0)
            {
                int size = queue.Count;
                // 每層 node.value; 未排序
                int[] arr = new int[size];
                // 每層 node.value, 遞增排序後狀態
                int[] sorted = new int[size];
                for(int i = 0; i < size; i++)
                {
                    // 將該 node 移出
                    TreeNode node = queue.Dequeue();
                    // 取出該 node.val
                    arr[i] = node.val;
                    // 取出該 node.val
                    sorted[i] = node.val;
                    // 該 node 左右非空子節點, 加入 queue
                    if (node.left != null)
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
                // 排序
                Array.Sort(sorted);
                // 儲存排序後每個 node.val 以及位置 index; 預期正確位置
                IDictionary<int, int> targetIndices = new Dictionary<int, int>();
                for(int i = 0; i < size; i++)
                {
                    targetIndices.Add(sorted[i], i);
                }

                // 將該層 node.val 原始資料與排序後的進行比對
                for(int i = 0; i < size; i++)
                {
                    // 當不再預期位置 index
                    while (arr[i] != sorted[i])
                    {
                        // 將當前 "位置 index" 與 "預期正確位置 index" 交換, 並累計交換次數
                        int targetIndex = targetIndices[arr[i]];
                        Swap(arr, i, targetIndex);
                        res++;
                    }
                }
            }

            return res;
        }


        /// <summary>
        /// SWAP
        /// </summary>
        /// <param name="arr">輸入資料</param>
        /// <param name="index1">當前位置(錯誤;非預期) index</param>
        /// <param name="index2">預期正確位置 index</param>
        public static void Swap(int[] arr, int index1, int index2)
        {
            int temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }
    }
}
