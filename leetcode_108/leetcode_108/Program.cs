namespace leetcode_108;

class Program
{
    /// <summary>
    /// 表示二元搜尋樹節點。每個節點儲存一個整數值，並以 left 與 right 指向左右子樹；
    /// 建立排序陣列轉 BST 的結果時，輸入值會放在 val，輸出則由根節點串起整棵樹。
    /// </summary>
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        /// <summary>
        /// 建立一個二元樹節點。可輸入節點值與左右子節點；未提供子節點時代表目前沒有子樹。
        /// 輸出結果是可接到 BST 中的單一節點。
        /// </summary>
        /// <param name="val">節點儲存的整數值。</param>
        /// <param name="left">左子節點，必須小於目前節點值；沒有左子樹時為 null。</param>
        /// <param name="right">右子節點，必須大於目前節點值；沒有右子樹時為 null。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 108. Convert Sorted Array to Binary Search Tree
    /// https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree/description/
    /// Given an integer array nums where the elements are sorted in ascending order,
    /// convert it to a height-balanced binary search tree.
    ///
    /// 108. 將有序陣列轉換為二元搜尋樹
    /// https://leetcode.cn/problems/convert-sorted-array-to-binary-search-tree/description/
    /// 給定一個整數陣列 nums，其中元素已按升冪排列，請將其轉換為一棵高度平衡的二元搜尋樹。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        int[][] examples =
        [
            [-10, -3, 0, 5, 9],
            [1, 3],
            [0],
        ];

        for (int index = 0; index < examples.Length; index++)
        {
            int[] nums = examples[index];
            TreeNode? root = solution.SortedArrayToBST(nums);

            Console.WriteLine($"Example {index + 1}");
            Console.WriteLine($"Input: [{string.Join(", ", nums)}]");
            Console.WriteLine($"Level-order: {FormatLevelOrder(root)}");
            Console.WriteLine($"In-order: {FormatInOrder(root)}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 將嚴格遞增排序的整數陣列轉換為高度平衡二元搜尋樹。
    /// 解題概念是每次取目前區間的中間元素作為根節點，再分別用左半與右半區間遞迴建立左右子樹；
    /// 輸入 nums 應為升冪且不重複的整數陣列，若傳入空陣列則回傳 null，輸出為建立完成的根節點。
    /// </summary>
    /// <param name="nums">已依升冪排序且元素不重複的整數陣列。</param>
    /// <returns>高度平衡二元搜尋樹的根節點；輸入為空陣列時回傳 null。</returns>
    public TreeNode? SortedArrayToBST(int[] nums)
    {
        return Helper(nums, 0, nums.Length - 1);
    }

    /// <summary>
    /// 依指定索引區間建立一棵高度平衡 BST 子樹。
    /// 解題概念是以中間索引切分 nums[left..right]，中間值成為根節點，左區間都小於根節點，
    /// 右區間都大於根節點；輸入區間超出範圍時代表沒有節點，輸出 null。
    /// </summary>
    /// <param name="nums">已依升冪排序且元素不重複的整數陣列。</param>
    /// <param name="left">目前子樹可使用區間的左邊界索引。</param>
    /// <param name="right">目前子樹可使用區間的右邊界索引。</param>
    /// <returns>由指定區間建立出的子樹根節點；若 left 大於 right 則回傳 null。</returns>
    private static TreeNode? Helper(int[] nums, int left, int right)
    {
        if (left > right)
        {
            return null;
        }

        // 使用不會溢位的寫法取得中點；偶數長度時選左中位數，兩側節點數量最多只差 1。
        int mid = left + ((right - left) / 2);
        TreeNode root = new TreeNode(nums[mid]);

        // 分治處理左右半部：左半都小於 root，右半都大於 root，因此自然符合 BST 性質。
        root.left = Helper(nums, left, mid - 1);
        root.right = Helper(nums, mid + 1, right);

        return root;
    }

    /// <summary>
    /// 將 BST 轉成 LeetCode 常見的 level-order 陣列字串，方便在 Main 中展示結果。
    /// 輸入為根節點或 null；輸出會保留中間必要的 null，並移除尾端不影響結構的 null。
    /// </summary>
    /// <param name="root">要序列化的二元樹根節點。</param>
    /// <returns>代表二元樹 level-order 結構的字串。</returns>
    private static string FormatLevelOrder(TreeNode? root)
    {
        if (root == null)
        {
            return "[]";
        }

        List<string> values = [];
        Queue<TreeNode?> queue = new Queue<TreeNode?>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            TreeNode? node = queue.Dequeue();

            if (node == null)
            {
                values.Add("null");
                continue;
            }

            values.Add(node.val.ToString());
            queue.Enqueue(node.left);
            queue.Enqueue(node.right);
        }

        // LeetCode 的樹陣列表示法會省略尾端連續 null，保留中間 null 才能看出節點位置。
        int lastValueIndex = values.Count - 1;
        while (lastValueIndex >= 0 && values[lastValueIndex] == "null")
        {
            lastValueIndex--;
        }

        return $"[{string.Join(", ", values.Take(lastValueIndex + 1))}]";
    }

    /// <summary>
    /// 將 BST 轉成中序走訪陣列字串，用來驗證輸出樹仍維持二元搜尋樹排序性質。
    /// 輸入為根節點或 null；輸出為左子樹、根節點、右子樹順序排列的字串。
    /// </summary>
    /// <param name="root">要走訪的二元樹根節點。</param>
    /// <returns>代表中序走訪結果的字串。</returns>
    private static string FormatInOrder(TreeNode? root)
    {
        List<int> values = [];
        AppendInOrder(root, values);

        return $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// 遞迴收集中序走訪結果。
    /// 解題概念是先走左子樹，再加入目前節點，最後走右子樹；輸入 null 時不加入任何值，
    /// 輸出會累積在 values 中。
    /// </summary>
    /// <param name="node">目前走訪到的節點。</param>
    /// <param name="values">用來累積中序走訪結果的清單。</param>
    private static void AppendInOrder(TreeNode? node, List<int> values)
    {
        if (node == null)
        {
            return;
        }

        AppendInOrder(node.left, values);
        values.Add(node.val);
        AppendInOrder(node.right, values);
    }
}