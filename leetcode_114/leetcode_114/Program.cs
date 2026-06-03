namespace leetcode_114;

class Program
{
    /// <summary>
    /// 表示二元樹節點，沿用 LeetCode 題目指定的欄位命名與結構。
    /// 輸入條件為節點值與可為 null 的左右子樹；輸出結果為可被原地重接的樹節點實例。
    /// </summary>
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        /// <summary>
        /// 建立一個二元樹節點。
        /// 輸入條件是節點值以及可選的左右子節點；輸出結果是初始化完成的 TreeNode。
        /// </summary>
        /// <param name="val">節點值。</param>
        /// <param name="left">左子節點，可為 null。</param>
        /// <param name="right">右子節點，可為 null。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 114. Flatten Binary Tree to Linked List
    /// https://leetcode.com/problems/flatten-binary-tree-to-linked-list/description/
    /// 114. 將二元樹展開為鏈結串列
    /// https://leetcode.cn/problems/flatten-binary-tree-to-linked-list/description/
    ///
    /// English:
    /// Given the root of a binary tree, flatten the tree into a "linked list":
    /// The "linked list" should use the same TreeNode class where the right child pointer points to the next node
    /// in the list and the left child pointer is always null.
    /// The "linked list" should be in the same order as a pre-order traversal of the binary tree.
    ///
    /// 繁體中文:
    /// 給定一棵二元樹的根節點，請將這棵樹展平成一個「linked list（鏈結串列）」：
    /// 這個「linked list」必須沿用相同的 TreeNode 類別，其中 right 子節點指標要指向串列中的下一個節點，
    /// 而 left 子節點指標必須永遠為 null。
    /// 展開後的「linked list」節點順序，必須與原始二元樹的前序走訪（pre-order traversal）順序相同。
    /// </summary>
    /// <param name="args">命令列參數，本題未使用。</param>
    private static void Main(string[] args)
    {
        RunCase("balanced", [1, 2, 5, 3, 4, null, 6], [1, 2, 3, 4, 5, 6]);
        RunCase("empty", [], []);
        RunCase("single", [0], [0]);
        RunCase("left-skewed", [1, 2, null, 3, null, 4], [1, 2, 3, 4]);
    }

    /// <summary>
    /// 將二元樹原地展平成符合前序走訪順序的右向鏈結串列。
    /// 解題概念是先收集前序走訪節點，再依序重接每個節點的 right 指標並清空 left 指標。
    /// 輸入條件為樹根節點，可為 null；輸出結果不回傳新樹，而是直接修改原樹結構。
    /// </summary>
    /// <param name="root">欲展平的二元樹根節點，可為 null。</param>
    public static void Flatten(TreeNode? root)
    {
        if (root is null)
        {
            return;
        }

        List<TreeNode> nodes = [];

        // 題目要求的輸出順序就是前序走訪，因此先把節點依 preorder 收集起來最直觀。
        Preorder(root, nodes);

        for (int index = 1; index < nodes.Count; index++)
        {
            TreeNode previous = nodes[index - 1];
            TreeNode current = nodes[index];

            // 重接鏈結時必須同步清空 left，才能符合題目「只保留 right 鏈」的要求。
            previous.left = null;
            previous.right = current;
        }

        TreeNode tail = nodes[^1];
        tail.left = null;
        tail.right = null;
    }

    /// <summary>
    /// 以前序走訪收集節點順序，作為展平後 right 鏈結串列的目標順序。
    /// 解題概念是依序拜訪根、左、右，讓蒐集結果直接對應題目要求的輸出順序。
    /// 輸入條件為目前節點與可寫入的節點列表；輸出結果是把走訪到的節點附加到列表尾端。
    /// </summary>
    /// <param name="root">目前走訪到的節點，可為 null。</param>
    /// <param name="nodes">用來收集前序走訪結果的節點列表。</param>
    public static void Preorder(TreeNode? root, List<TreeNode> nodes)
    {
        if (root is null)
        {
            return;
        }

        nodes.Add(root);
        Preorder(root.left, nodes);
        Preorder(root.right, nodes);
    }

    /// <summary>
    /// 執行單一範例案例，包含建樹、展平、驗證與輸出。
    /// 輸入條件是案例名稱、level-order 測資與預期 right 鏈結串列；輸出結果是將驗證結果印到主控台，失敗時丟出例外。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="levelOrder">以 level-order 表示的輸入樹。</param>
    /// <param name="expected">展平後預期的節點順序。</param>
    private static void RunCase(string name, int?[] levelOrder, int[] expected)
    {
        TreeNode? root = BuildTreeFromLevelOrder(levelOrder);
        Flatten(root);

        List<int> actual = CollectRightChainValues(root);
        ValidateFlattenedResult(name, root, actual, expected);

        Console.WriteLine($"[{name}] input={FormatSequence(levelOrder)} output={FormatSequence(actual)}");
    }

    /// <summary>
    /// 依照 level-order 陣列建立二元樹。
    /// 輸入條件是以 null 表示缺節點的層序資料；輸出結果是對應的樹根節點，空陣列時回傳 null。
    /// </summary>
    /// <param name="levelOrder">以層序表示的節點資料。</param>
    /// <returns>建立完成的樹根節點；若輸入為空則回傳 null。</returns>
    private static TreeNode? BuildTreeFromLevelOrder(int?[] levelOrder)
    {
        if (levelOrder.Length == 0)
        {
            return null;
        }

        int? rootValue = levelOrder[0];
        if (!rootValue.HasValue)
        {
            return null;
        }

        TreeNode root = new(rootValue.Value);
        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        int index = 1;

        while (queue.Count > 0 && index < levelOrder.Length)
        {
            TreeNode current = queue.Dequeue();
            int? leftValue = index < levelOrder.Length ? levelOrder[index] : null;

            if (leftValue.HasValue)
            {
                current.left = new TreeNode(leftValue.Value);
                queue.Enqueue(current.left);
            }

            index++;
            int? rightValue = index < levelOrder.Length ? levelOrder[index] : null;

            if (rightValue.HasValue)
            {
                current.right = new TreeNode(rightValue.Value);
                queue.Enqueue(current.right);
            }

            index++;
        }

        return root;
    }

    /// <summary>
    /// 收集展平後 right 鏈結串列上的節點值，作為驗證與輸出資料。
    /// 輸入條件是已展平的樹根節點，可為 null；輸出結果是依序排列的節點值清單。
    /// </summary>
    /// <param name="root">已展平樹的根節點，可為 null。</param>
    /// <returns>從 root 沿著 right 指標走訪所得的節點值。</returns>
    private static List<int> CollectRightChainValues(TreeNode? root)
    {
        List<int> values = [];
        TreeNode? current = root;

        while (current is not null)
        {
            values.Add(current.val);
            current = current.right;
        }

        return values;
    }

    /// <summary>
    /// 驗證展平結果是否符合預期順序，且所有 left 指標都已清空。
    /// 輸入條件是案例名稱、展平後樹根、實際輸出與預期輸出；輸出結果是在驗證失敗時丟出具體例外，成功時不回傳值。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="root">展平後的樹根節點，可為 null。</param>
    /// <param name="actual">實際收集到的 right 鏈結串列值。</param>
    /// <param name="expected">預期的 right 鏈結串列值。</param>
    private static void ValidateFlattenedResult(string name, TreeNode? root, List<int> actual, int[] expected)
    {
        if (!actual.SequenceEqual(expected))
        {
            throw new InvalidOperationException(
                $"Case '{name}' failed. Expected {FormatSequence(expected)}, but got {FormatSequence(actual)}.");
        }

        TreeNode? current = root;
        while (current is not null)
        {
            if (current.left is not null)
            {
                throw new InvalidOperationException(
                    $"Case '{name}' failed. Node {current.val} still has a left child after flatten.");
            }

            current = current.right;
        }
    }

    /// <summary>
    /// 將整數序列格式化成 README 與主控台可直接對照的字串。
    /// 輸入條件為整數序列；輸出結果為以中括號包住、逗號分隔的字串表示。
    /// </summary>
    /// <param name="values">欲格式化的整數序列。</param>
    /// <returns>格式化後的字串。</returns>
    private static string FormatSequence(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// 將可含 null 的整數序列格式化成 level-order 顯示字串。
    /// 輸入條件為可含 null 的整數序列；輸出結果為以中括號包住、缺節點顯示為 null 的字串。
    /// </summary>
    /// <param name="values">欲格式化的 level-order 序列。</param>
    /// <returns>格式化後的字串。</returns>
    private static string FormatSequence(IEnumerable<int?> values)
    {
        List<string> items = [];

        foreach (int? value in values)
        {
            items.Add(value.HasValue ? value.Value.ToString() : "null");
        }

        return $"[{string.Join(", ", items)}]";
    }
}
