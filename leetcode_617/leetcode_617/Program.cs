namespace leetcode_617;

class Program
{
    /// <summary>
    /// 表示二元樹節點；保存整數值以及可為空的左右子節點，作為合併演算法的輸入與輸出資料結構。
    /// </summary>
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        /// <summary>
        /// 建立二元樹節點；節點值可為任意整數，左右子節點可省略，回傳完成初始化的節點。
        /// </summary>
        /// <param name="val">目前節點保存的整數值。</param>
        /// <param name="left">左子節點；沒有左子節點時為 <see langword="null"/>。</param>
        /// <param name="right">右子節點；沒有右子節點時為 <see langword="null"/>。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 保存一組可執行驗證資料；輸入與預期結果皆使用 level-order 陣列表示。
    /// </summary>
    /// <param name="Name">案例名稱。</param>
    /// <param name="Root1Values">第一棵樹的 level-order 輸入。</param>
    /// <param name="Root2Values">第二棵樹的 level-order 輸入。</param>
    /// <param name="ExpectedValues">合併後樹的預期 level-order 結果。</param>
    private sealed record TestCase(
        string Name,
        int?[] Root1Values,
        int?[] Root2Values,
        int?[] ExpectedValues);

    /// <summary>
    /// 保存單一案例的實際 level-order 結果與是否符合預期，供主要進入點統一輸出。
    /// </summary>
    /// <param name="ActualValues">合併後樹的實際 level-order 結果。</param>
    /// <param name="Passed">實際結果是否與預期完全一致。</param>
    private sealed record CaseResult(int?[] ActualValues, bool Passed);

    /// <summary>
    /// 617. Merge Two Binary Trees
    /// https://leetcode.com/problems/merge-two-binary-trees/description/
    /// 617. 合并二叉树
    /// https://leetcode.cn/problems/merge-two-binary-trees/description/
    ///
    /// English:
    /// You are given two binary trees root1 and root2.
    /// Imagine that when you put one of them to cover the other, some nodes of the two trees
    /// are overlapped while the others are not. You need to merge the two trees into a new
    /// binary tree. The merge rule is that if two nodes overlap, then sum node values up as
    /// the new value of the merged node. Otherwise, the NOT null node will be used as the
    /// node of the new tree.
    ///
    /// Note: The merging process must start from the root nodes of both trees.
    ///
    /// 繁體中文 (說明)：
    /// 給定兩個二元樹 root1 與 root2。
    /// 假設把其中一棵覆蓋在另一棵之上，部分節點會重疊，其他則不會。請將兩棵樹合併為一棵新的二元樹。
    /// 合併規則：當兩節點重疊時，合併節點的值為兩節點值的總和；否則使用非 null 的節點作為合併後的節點。
    ///
    /// 注意：合併過程需從兩棵樹的根節點開始。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("617. Merge Two Binary Trees");
        Console.WriteLine("===========================");
        Console.WriteLine();

        TestCase[] testCases =
        [
            new("官方完整範例", [1, 3, 2, 5], [2, 1, 3, null, 4, null, 7], [3, 4, 5, 5, 4, null, 7]),
            new("官方第二範例", [1], [1, 2], [2, 2]),
            new("兩棵空樹", [], [], []),
            new("僅第一棵存在", [1, null, 2], [], [1, null, 2]),
            new("僅第二棵存在", [], [0, -1, 1], [0, -1, 1]),
            new("含負值且完全重疊", [-10, -5, 3], [10, 5, -3], [0, 0, 0])
        ];

        int passedCount = 0;
        for (int index = 0; index < testCases.Length; index++)
        {
            TestCase testCase = testCases[index];
            CaseResult result = RunCase(testCase);
            passedCount += result.Passed ? 1 : 0;

            Console.WriteLine($"Case {index + 1}: {testCase.Name}");
            Console.WriteLine($"  root1:    {FormatValues(testCase.Root1Values)}");
            Console.WriteLine($"  root2:    {FormatValues(testCase.Root2Values)}");
            Console.WriteLine($"  expected: {FormatValues(testCase.ExpectedValues)}");
            Console.WriteLine($"  actual:   {FormatValues(result.ActualValues)}");
            Console.WriteLine($"  result:   {(result.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passedCount}/{testCases.Length} checks passed.");
        if (passedCount != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 執行單一測試案例；由 level-order 輸入建樹、呼叫合併解法並序列化結果，最後回傳比對結果。
    /// </summary>
    /// <param name="testCase">包含兩棵輸入樹與預期輸出的有效測試案例。</param>
    /// <returns>實際 level-order 結果，以及是否與預期結果完全一致。</returns>
    private static CaseResult RunCase(TestCase testCase)
    {
        TreeNode? root1 = BuildTree(testCase.Root1Values);
        TreeNode? root2 = BuildTree(testCase.Root2Values);
        TreeNode? mergedRoot = new Program().MergeTrees(root1, root2);
        int?[] actualValues = SerializeTree(mergedRoot);

        return new CaseResult(
            actualValues,
            actualValues.SequenceEqual(testCase.ExpectedValues));
    }

    /// <summary>
    /// 將有效的 level-order 陣列轉換為二元樹；空陣列或根值為 null 時建立空樹。
    /// </summary>
    /// <param name="values">以 null 表示缺少節點的 level-order 陣列。</param>
    /// <returns>建立完成的根節點；輸入表示空樹時回傳 <see langword="null"/>。</returns>
    private static TreeNode? BuildTree(int?[] values)
    {
        if (values.Length == 0 || values[0] is not int rootValue)
        {
            return null;
        }

        TreeNode root = new(rootValue);
        Queue<TreeNode> parents = new();
        parents.Enqueue(root);
        int valueIndex = 1;

        // 依 level-order 逐一取出父節點，將後續值配對為左、右子節點。
        while (parents.Count > 0 && valueIndex < values.Length)
        {
            TreeNode parent = parents.Dequeue();

            if (values[valueIndex] is int leftValue)
            {
                parent.left = new TreeNode(leftValue);
                parents.Enqueue(parent.left);
            }

            valueIndex++;
            if (valueIndex >= values.Length)
            {
                break;
            }

            if (values[valueIndex] is int rightValue)
            {
                parent.right = new TreeNode(rightValue);
                parents.Enqueue(parent.right);
            }

            valueIndex++;
        }

        return root;
    }

    /// <summary>
    /// 以廣度優先走訪將二元樹轉為 level-order 陣列，並移除尾端不影響樹形的 null。
    /// </summary>
    /// <param name="root">要序列化的根節點，可為 <see langword="null"/>。</param>
    /// <returns>可完整表示樹形的 level-order 陣列；空樹回傳空陣列。</returns>
    private static int?[] SerializeTree(TreeNode? root)
    {
        if (root is null)
        {
            return [];
        }

        List<int?> values = [];
        Queue<TreeNode?> nodes = new();
        nodes.Enqueue(root);

        while (nodes.Count > 0)
        {
            TreeNode? node = nodes.Dequeue();
            if (node is null)
            {
                values.Add(null);
                continue;
            }

            values.Add(node.val);
            nodes.Enqueue(node.left);
            nodes.Enqueue(node.right);
        }

        // 尾端 null 不會改變 level-order 所代表的樹形，移除後可穩定比對與顯示。
        int lastValueIndex = values.Count - 1;
        while (lastValueIndex >= 0 && values[lastValueIndex] is null)
        {
            lastValueIndex--;
        }

        return values.Take(lastValueIndex + 1).ToArray();
    }

    /// <summary>
    /// 將 level-order 陣列格式化為 README 與主程式共用的穩定文字表示。
    /// </summary>
    /// <param name="values">要顯示的整數與 null 序列。</param>
    /// <returns>以方括號包覆、逗號分隔的字串；空陣列顯示為 []。</returns>
    private static string FormatValues(int?[] values)
    {
        return $"[{string.Join(",", values.Select(value => value?.ToString() ?? "null"))}]";
    }

    /// <summary>
    /// 以遞迴同時走訪兩棵二元樹的對應位置，合併重疊節點並沿用未重疊子樹。
    ///
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 採用遞迴方式，同時遍歷兩棵樹的對應節點：
    /// <list type="number">
    ///     <item>若其中一個節點為 null，回傳另一個節點及其完整子樹。</item>
    ///     <item>若兩個節點都存在，建立值為兩節點總和的新節點。</item>
    ///     <item>遞迴合併左右子樹，再回傳目前的合併節點。</item>
    /// </list>
    /// </para>
    ///
    /// <para><b>輸入條件：</b>兩個根節點皆可為 null；節點值須符合題目限制。</para>
    /// <para><b>輸出結果：</b>回傳合併後的根節點；兩棵空樹會回傳 null。</para>
    /// <para><b>時間複雜度：</b>O(min(m, n))，其中 m、n 分別為兩棵樹的節點數。</para>
    /// <para><b>輔助空間複雜度：</b>O(min(h1, h2))，來自遞迴呼叫堆疊。</para>
    /// </summary>
    /// <param name="root1">第一棵二元樹的根節點，可為 <see langword="null"/>。</param>
    /// <param name="root2">第二棵二元樹的根節點，可為 <see langword="null"/>。</param>
    /// <returns>合併後的二元樹根節點；兩棵樹皆空時為 <see langword="null"/>。</returns>
    /// <example>
    /// <code>
    /// Tree1: [1,3,2,5], Tree2: [2,1,3,null,4,null,7]
    /// 合併結果: [3,4,5,5,4,null,7]
    /// var result = MergeTrees(root1, root2);
    /// </code>
    /// </example>
    public TreeNode? MergeTrees(TreeNode? root1, TreeNode? root2)
    {
        // 任一側為空時直接沿用另一側的完整子樹；兩側皆空也會在此回傳 null。
        if (root1 is null)
        {
            return root2;
        }

        if (root2 is null)
        {
            return root1;
        }

        // 只有重疊位置需要建立新節點，其左右子樹再交由相同規則處理。
        TreeNode mergedNode = new(root1.val + root2.val)
        {
            left = MergeTrees(root1.left, root2.left),
            right = MergeTrees(root1.right, root2.right)
        };

        return mergedNode;
    }
}