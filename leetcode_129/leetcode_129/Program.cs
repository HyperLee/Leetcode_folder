namespace leetcode_129;

class Program
{
    /// <summary>
    /// 表示題目使用的二元樹節點，節點值限制在 0 到 9，左右子節點可為空。
    /// </summary>
    public sealed class TreeNode
    {
        public int Val { get; }
        public TreeNode? Left { get; }
        public TreeNode? Right { get; }

        /// <summary>
        /// 建立一個二元樹節點，供 DFS、BFS 與主程式範例資料使用。
        /// 輸入值預期符合題目限制的單一數字，左右子節點可省略為空。
        /// 會回傳一個完成初始化的樹節點。
        /// </summary>
        /// <param name="val">目前節點代表的數字。</param>
        /// <param name="left">左子節點，沒有時為 <see langword="null"/>。</param>
        /// <param name="right">右子節點，沒有時為 <see langword="null"/>。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            Val = val;
            Left = left;
            Right = right;
        }
    }

    /// <summary>
    /// 封裝主程式展示用的測資，包含輸入描述、路徑說明與預期答案。
    /// </summary>
    /// <param name="Name">案例名稱。</param>
    /// <param name="Input">README 與主程式共用的輸入表示法。</param>
    /// <param name="Explanation">每條路徑如何組成數字的摘要說明。</param>
    /// <param name="Root">案例對應的樹根節點，可為空樹。</param>
    /// <param name="Expected">所有根到葉路徑數字總和的預期值。</param>
    private sealed record SampleCase(string Name, string Input, string Explanation, TreeNode? Root, int Expected);

    /// <summary>
    /// 129. Sum Root to Leaf Numbers
    /// https://leetcode.com/problems/sum-root-to-leaf-numbers/description/
    ///
    /// You are given the root of a binary tree containing digits from 0 to 9 only.
    /// Each root-to-leaf path in the tree represents a number.
    /// For example, the root-to-leaf path 1 -> 2 -> 3 represents the number 123.
    /// Return the total sum of all root-to-leaf numbers.
    /// Test cases are generated so that the answer will fit in a 32-bit integer.
    /// A leaf node is a node with no children.
    ///
    /// 129. 求根節點到葉節點數字之和
    /// https://leetcode.cn/problems/sum-root-to-leaf-numbers/description/
    ///
    /// 給定一個二元樹的根節點，樹中只包含 0 到 9 的數字。
    /// 每一條從根節點到葉節點的路徑都代表一個數字。
    /// 例如，根到葉路徑 1 -> 2 -> 3 代表數字 123。
    /// 請回傳所有根到葉路徑所代表數字的總和。
    /// 測試案例保證答案會符合 32 位元整數範圍。
    /// 葉節點是沒有子節點的節點。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        IReadOnlyList<SampleCase> samples = CreateSampleCases();

        Console.WriteLine("LeetCode 129 - Sum Root to Leaf Numbers");
        Console.WriteLine("======================================");

        foreach (SampleCase sample in samples)
        {
            PrintSampleResult(solver, sample);
        }
    }

    /// <summary>
    /// 保留 LeetCode 常見的預設方法名稱，並以 DFS 作為主要解法入口。
    /// 輸入可以是空樹；若節點存在，節點值預期落在 0 到 9。
    /// 回傳所有根到葉路徑所形成數字的總和。
    /// </summary>
    /// <param name="root">二元樹根節點，可為 <see langword="null"/>。</param>
    /// <returns>所有根到葉數字的總和；空樹時回傳 0。</returns>
    public int SumNumbers(TreeNode? root)
    {
        return SumNumbersDfs(root);
    }

    /// <summary>
    /// 使用深度優先搜尋遞迴計算答案。
    /// 核心概念是沿路徑向下時將先前數字乘以 10，再加上目前節點值，直到葉節點才把完整數字納入總和。
    /// 輸入可以是空樹；非空樹的節點值需符合題目限制。
    /// 回傳所有根到葉數字的加總結果。
    /// </summary>
    /// <param name="root">二元樹根節點，可為 <see langword="null"/>。</param>
    /// <returns>以 DFS 計算出的所有根到葉數字總和。</returns>
    public int SumNumbersDfs(TreeNode? root)
    {
        return CalculateDepthFirstSum(root, 0);
    }

    /// <summary>
    /// 使用廣度優先搜尋逐層計算答案。
    /// 核心概念是讓佇列同時保存節點與到達該節點時已組成的路徑數字，遇到葉節點時再把該完整數字加進總和。
    /// 輸入可以是空樹；非空樹的節點值需符合題目限制。
    /// 回傳所有根到葉數字的加總結果。
    /// </summary>
    /// <param name="root">二元樹根節點，可為 <see langword="null"/>。</param>
    /// <returns>以 BFS 計算出的所有根到葉數字總和。</returns>
    public int SumNumbersBfs(TreeNode? root)
    {
        if (root is null)
        {
            return 0;
        }

        Queue<(TreeNode Node, int CurrentValue)> queue = new Queue<(TreeNode Node, int CurrentValue)>();
        int total = 0;

        queue.Enqueue((root, root.Val));

        while (queue.Count > 0)
        {
            (TreeNode node, int currentValue) = queue.Dequeue();

            if (node.Left is null && node.Right is null)
            {
                total += currentValue;
                continue;
            }

            // 往下一層前先把既有數字左移一位，再接上子節點的個位數。
            if (node.Left is not null)
            {
                queue.Enqueue((node.Left, currentValue * 10 + node.Left.Val));
            }

            if (node.Right is not null)
            {
                queue.Enqueue((node.Right, currentValue * 10 + node.Right.Val));
            }
        }

        return total;
    }

    /// <summary>
    /// 以 DFS 遞迴展開每一條根到葉路徑，並在葉節點回傳該路徑代表的完整數字。
    /// 遞迴過程假設 currentValue 已是父節點對應的數字，空節點直接貢獻 0。
    /// 回傳目前子樹所有根到葉數字的總和。
    /// </summary>
    /// <param name="node">目前遞迴處理的節點，可為 <see langword="null"/>。</param>
    /// <param name="currentValue">到達目前節點前，父路徑已累積出的數字。</param>
    /// <returns>目前子樹所有根到葉數字的總和。</returns>
    private int CalculateDepthFirstSum(TreeNode? node, int currentValue)
    {
        if (node is null)
        {
            return 0;
        }

        // 題目中的路徑數字是十進位串接，因此下一層必須先乘 10 再加上目前節點值。
        int nextValue = currentValue * 10 + node.Val;

        if (node.Left is null && node.Right is null)
        {
            return nextValue;
        }

        return CalculateDepthFirstSum(node.Left, nextValue) + CalculateDepthFirstSum(node.Right, nextValue);
    }

    /// <summary>
    /// 建立主程式要實際執行的固定測資，涵蓋題目範例、含零值的路徑，以及空樹輸入。
    /// 每筆資料都提供預期結果，讓主程式能直接比對 DFS 與 BFS 的答案是否一致。
    /// 回傳可供列舉輸出的測資集合。
    /// </summary>
    /// <returns>主程式展示與驗證用的固定測資清單。</returns>
    private static IReadOnlyList<SampleCase> CreateSampleCases()
    {
        List<SampleCase> samples = new List<SampleCase>
        {
            new SampleCase(
                "Example 1",
                "root = [1,2,3]",
                "Paths: 12 + 13 = 25",
                new TreeNode(1, new TreeNode(2), new TreeNode(3)),
                25),
            new SampleCase(
                "Example 2",
                "root = [4,9,0,5,1]",
                "Paths: 495 + 491 + 40 = 1026",
                new TreeNode(4, new TreeNode(9, new TreeNode(5), new TreeNode(1)), new TreeNode(0)),
                1026),
            new SampleCase(
                "Zero In Path",
                "root = [1,0,4,5]",
                "Paths: 105 + 14 = 119",
                new TreeNode(1, new TreeNode(0, new TreeNode(5), null), new TreeNode(4)),
                119),
            new SampleCase(
                "Empty Tree",
                "root = []",
                "Paths: none, total = 0",
                null,
                0)
        };

        return samples;
    }

    /// <summary>
    /// 執行單一範例案例，分別列出 DFS 與 BFS 的計算結果，並與預期值做比對。
    /// 輸入需包含完整的案例資料與可用的求解器實例。
    /// 會把該案例的執行結果輸出到主控台，方便 README 與實際執行互相對照。
    /// </summary>
    /// <param name="solver">負責執行 DFS 與 BFS 解法的程式實例。</param>
    /// <param name="sample">要輸出的固定測資案例。</param>
    private static void PrintSampleResult(Program solver, SampleCase sample)
    {
        int dfsResult = solver.SumNumbersDfs(sample.Root);
        int bfsResult = solver.SumNumbersBfs(sample.Root);
        bool passed = dfsResult == sample.Expected && bfsResult == sample.Expected;

        Console.WriteLine($"[{sample.Name}]");
        Console.WriteLine($"Input: {sample.Input}");
        Console.WriteLine(sample.Explanation);
        Console.WriteLine($"Expected: {sample.Expected}");
        Console.WriteLine($"DFS Result: {dfsResult}");
        Console.WriteLine($"BFS Result: {bfsResult}");
        Console.WriteLine($"Pass: {(passed ? "PASS" : "FAIL")}");
        Console.WriteLine();
    }
}
