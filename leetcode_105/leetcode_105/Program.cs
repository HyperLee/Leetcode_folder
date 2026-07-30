namespace leetcode_105;

class Program
{
    /// <summary>
    /// 表示二元樹節點，保存節點值以及可為空的左、右子節點。
    /// 建樹方法以此型別串接重建結果，根節點即為最終輸出。
    /// </summary>
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        /// <summary>
        /// 建立指定值的二元樹節點，並可選擇性指定左右子節點。
        /// 輸入節點值與可為空的子樹；輸出為完成初始化的新節點。
        /// </summary>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 105. Construct Binary Tree from Preorder and Inorder Traversal
    /// https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/description/
    /// 105. 从前序与中序遍历序列构造二叉树
    /// https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/description/
    /// 
    /// 題目描述：
    /// 根據前序和中序遍歷序列構造二叉樹。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行五組固定案例，分別驗證兩種建樹解法產生的前序與中序遍歷。
    /// 輸入案例皆符合題目對相同長度、元素唯一且遍歷有效的條件；
    /// 執行完成後會輸出每項 Expected、Actual、PASS/FAIL 與通過數總結。
    /// </summary>
    private static void RunSamples()
    {
        (string Name, int[] Preorder, int[] Inorder)[] samples =
        [
            ("官方一般案例", [3, 9, 20, 15, 7], [9, 3, 15, 20, 7]),
            ("單節點", [-1], [-1]),
            ("全左偏樹", [3, 2, 1], [1, 2, 3]),
            ("全右偏樹", [1, 2, 3], [1, 2, 3]),
            ("含負數且左右不對稱", [0, -3, -4, -1, 9, 12], [-4, -3, -1, 0, 9, 12])
        ];

        int passedChecks = 0;
        int totalChecks = samples.Length * 2 * 2;

        for (int index = 0; index < samples.Length; index++)
        {
            (string name, int[] preorder, int[] inorder) = samples[index];
            Console.WriteLine($"案例 {index + 1}：{name}");
            Console.WriteLine($"輸入 preorder = {FormatValues(preorder)}");
            Console.WriteLine($"輸入 inorder  = {FormatValues(inorder)}");

            passedChecks += RunSolution(
                "解法一：區間遞迴 + 線性搜尋",
                preorder,
                inorder,
                BuildTree);
            passedChecks += RunSolution(
                "解法二：區間遞迴 + 雜湊表索引",
                preorder,
                inorder,
                BuildTree2);

            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 執行指定建樹函式，將重建樹的前序與中序結果和輸入遍歷逐一比對。
    /// 輸入為解法名稱、兩組有效遍歷與建樹委派；輸出為本解法通過的檢查數，範圍為 0 到 2。
    /// </summary>
    private static int RunSolution(
        string solutionName,
        int[] preorder,
        int[] inorder,
        Func<int[]?, int[]?, TreeNode?> buildTree)
    {
        TreeNode? root = buildTree([.. preorder], [.. inorder]);
        List<int> actualPreorder = PreorderTraversal(root);
        List<int> actualInorder = InorderTraversal(root);
        bool preorderPassed = preorder.SequenceEqual(actualPreorder);
        bool inorderPassed = inorder.SequenceEqual(actualInorder);

        Console.WriteLine($"  {solutionName}");
        PrintCheck("前序", preorder, actualPreorder, preorderPassed);
        PrintCheck("中序", inorder, actualInorder, inorderPassed);

        return (preorderPassed ? 1 : 0) + (inorderPassed ? 1 : 0);
    }

    /// <summary>
    /// 輸出單一遍歷檢查的預期值、實際值與 PASS/FAIL。
    /// 輸入包含標籤、預期序列、實際序列與比對結果；此方法沒有回傳值。
    /// </summary>
    private static void PrintCheck(
        string traversalName,
        IEnumerable<int> expected,
        IEnumerable<int> actual,
        bool passed)
    {
        Console.WriteLine($"    {traversalName} Expected: {FormatValues(expected)}");
        Console.WriteLine($"    {traversalName} Actual:   {FormatValues(actual)} => {(passed ? "PASS" : "FAIL")}");
    }

    /// <summary>
    /// 將整數序列格式化為便於閱讀的方括號表示法。
    /// 輸入為可列舉的整數序列；輸出格式如 <c>[3, 9, 20]</c>。
    /// </summary>
    private static string FormatValues(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// 使用區間遞迴與線性搜尋，從前序及中序遍歷重建二元樹。
    /// 前序區間首值是根節點，再以 <c>Array.IndexOf</c>
    /// 在中序區間找出根節點，據此切分左右子樹；最壞時間為 O(n²)，遞迴空間為 O(n)。
    /// 輸入須為題目保證的相同長度、元素唯一且彼此一致的遍歷；null 或空陣列會回傳 null。
    /// </summary>
    /// <param name="preorder">二元樹的前序遍歷。</param>
    /// <param name="inorder">同一棵二元樹的中序遍歷。</param>
    /// <returns>重建後的根節點；輸入為 null 或空陣列時回傳 null。</returns>
    public static TreeNode? BuildTree(int[]? preorder, int[]? inorder)
    {
        if (preorder is null || inorder is null || preorder.Length == 0 || inorder.Length == 0)
        {
            return null;
        }

        return BuildTreeHelper(preorder, 0, preorder.Length - 1, inorder, 0, inorder.Length - 1);
    }

    /// <summary>
    /// 在指定的前序與中序閉區間內，以線性搜尋定位根節點並遞迴建立左右子樹。
    /// 輸入區間必須描述同一棵有效子樹；區間為空時回傳 null，否則回傳該子樹根節點。
    /// </summary>
    private static TreeNode? BuildTreeHelper(
        int[] preorder,
        int preStart,
        int preEnd,
        int[] inorder,
        int inStart,
        int inEnd)
    {
        if (preStart > preEnd || inStart > inEnd)
        {
            return null;
        }

        TreeNode root = new(preorder[preStart]);
        int rootIndex = Array.IndexOf(inorder, root.val, inStart, inEnd - inStart + 1);
        int leftSubtreeSize = rootIndex - inStart;

        // 中序根節點左側的元素數量，正好決定前序中左子樹的結束位置。
        root.left = BuildTreeHelper(
            preorder,
            preStart + 1,
            preStart + leftSubtreeSize,
            inorder,
            inStart,
            rootIndex - 1);
        root.right = BuildTreeHelper(
            preorder,
            preStart + leftSubtreeSize + 1,
            preEnd,
            inorder,
            rootIndex + 1,
            inEnd);

        return root;
    }

    /// <summary>
    /// 使用中序索引雜湊表與區間遞迴，從前序及中序遍歷重建二元樹。
    /// 預先記錄每個中序值的位置，使每層都能以 O(1) 定位根節點；
    /// 建表與建樹總時間為 O(n)，雜湊表及遞迴所需空間為 O(n)。
    /// 輸入須為題目保證的相同長度、元素唯一且彼此一致的遍歷；null 或空陣列會回傳 null。
    /// </summary>
    /// <param name="preorder">二元樹的前序遍歷。</param>
    /// <param name="inorder">同一棵二元樹的中序遍歷。</param>
    /// <returns>重建後的根節點；輸入為 null 或空陣列時回傳 null。</returns>
    public static TreeNode? BuildTree2(int[]? preorder, int[]? inorder)
    {
        if (preorder is null || inorder is null || preorder.Length == 0 || inorder.Length == 0)
        {
            return null;
        }

        Dictionary<int, int> inorderIndexMap = [];
        for (int index = 0; index < inorder.Length; index++)
        {
            inorderIndexMap[inorder[index]] = index;
        }

        return BuildTreeHelper2(
            preorder,
            0,
            preorder.Length - 1,
            0,
            inorder.Length - 1,
            inorderIndexMap);
    }

    /// <summary>
    /// 在指定的前序與中序閉區間內，透過雜湊表定位根節點並遞迴建立左右子樹。
    /// 輸入區間必須描述同一棵有效子樹，索引表須包含所有中序值；
    /// 區間為空時回傳 null，否則回傳該子樹根節點。
    /// </summary>
    private static TreeNode? BuildTreeHelper2(
        int[] preorder,
        int preStart,
        int preEnd,
        int inStart,
        int inEnd,
        IReadOnlyDictionary<int, int> inorderIndexMap)
    {
        if (preStart > preEnd || inStart > inEnd)
        {
            return null;
        }

        TreeNode root = new(preorder[preStart]);
        int rootIndex = inorderIndexMap[root.val];
        int leftSubtreeSize = rootIndex - inStart;

        // 索引表只取代線性搜尋；左右子樹的區間切分規則與解法一完全相同。
        root.left = BuildTreeHelper2(
            preorder,
            preStart + 1,
            preStart + leftSubtreeSize,
            inStart,
            rootIndex - 1,
            inorderIndexMap);
        root.right = BuildTreeHelper2(
            preorder,
            preStart + leftSubtreeSize + 1,
            preEnd,
            rootIndex + 1,
            inEnd,
            inorderIndexMap);

        return root;
    }

    /// <summary>
    /// 以根、左、右的順序遞迴走訪二元樹。
    /// 輸入可為空根節點；輸出為前序節點值清單，空樹輸出空清單。
    /// </summary>
    private static List<int> PreorderTraversal(TreeNode? root)
    {
        List<int> result = [];
        if (root is null)
        {
            return result;
        }

        result.Add(root.val);
        result.AddRange(PreorderTraversal(root.left));
        result.AddRange(PreorderTraversal(root.right));
        return result;
    }

    /// <summary>
    /// 以左、根、右的順序遞迴走訪二元樹。
    /// 輸入可為空根節點；輸出為中序節點值清單，空樹輸出空清單。
    /// </summary>
    private static List<int> InorderTraversal(TreeNode? root)
    {
        List<int> result = [];
        if (root is null)
        {
            return result;
        }

        result.AddRange(InorderTraversal(root.left));
        result.Add(root.val);
        result.AddRange(InorderTraversal(root.right));
        return result;
    }
}