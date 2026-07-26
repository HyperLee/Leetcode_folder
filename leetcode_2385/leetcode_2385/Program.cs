namespace leetcode_2385;

internal class Program
{
    /// <summary>
    /// LeetCode 2385 - Amount of Time for Binary Tree to Be Infected.
    /// LeetCode 2385 - 感染二叉樹需要的總時間。
    /// English: https://leetcode.com/problems/amount-of-time-for-binary-tree-to-be-infected/
    /// 中文：https://leetcode.cn/problems/amount-of-time-for-binary-tree-to-be-infected/
    /// English: Starting from the uniquely valued node <c>start</c>, infection reaches each adjacent
    /// parent or child node every minute; return the minutes needed to infect the whole binary tree.
    /// 中文：從值唯一且等於 <c>start</c> 的節點開始，感染每分鐘擴散到父節點或子節點；回傳感染整棵二元樹所需分鐘數。
    /// </summary>
    private static void Main()
    {
        (string Name, string Input, string Expected, Func<string> Evaluate)[] cases =
        [
            ("Official example", "[1,5,3,null,4,10,6,9,2], start=3", "4", () => AmountOfTime(CreateOfficialTree(), 3).ToString()),
            ("Single node", "[1], start=1", "0", () => AmountOfTime(new TreeNode(1), 1).ToString()),
            ("Cross-root leaf", "[1,2,3,4,5,null,6], start=4", "4", () => AmountOfTime(CreateCrossRootTree(), 4).ToString()),
            ("Same tree from root", "[1,2,3,4,5,null,6], start=1", "2", () => AmountOfTime(CreateCrossRootTree(), 1).ToString()),
            ("Five-node skew from middle", "[1,null,2,null,3,null,4,null,5], start=3", "2", () => AmountOfTime(CreateRightSkewedTree(5), 3).ToString()),
            ("Repeated official-tree calls", "same official tree, start=(3,9)", "(4, 5)", EvaluateRepeatedOfficialTreeCalls),
            ("Official-tree result and topology preservation", "snapshot official tree, start=3", "4; True", VerifyOfficialTreeResultAndTopologyPreserved),
            ("100,000-node skew", "right-skewed [1..100000], start=1", "99999", () => AmountOfTime(CreateRightSkewedTree(100000), 1).ToString()),
            ("Asymmetric internal start", "[8,3,10,1,6,null,14], start=3", "3", () => AmountOfTime(CreateAsymmetricTree(), 3).ToString())
        ];

        int passed = 0;

        foreach ((string name, string input, string expected, Func<string> evaluate) in cases)
        {
            string actual = evaluate();
            bool isPass = actual == expected;

            Console.WriteLine($"Case: {name}; Input: {input}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine(isPass ? "PASS" : "FAIL");

            if (isPass)
            {
                passed++;
            }
        }

        Console.WriteLine($"Summary: {passed}/{cases.Length} checks passed.");

        if (passed != cases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 以父節點映射把二元樹視為無向圖，再從感染起點做逐層廣度優先搜尋。輸入為題目保證非空且節點值唯一的樹，
    /// <paramref name="start"/> 必定存在；方法不修改樹也不保留跨呼叫狀態，回傳最遠節點被感染所需的分鐘數。
    /// </summary>
    public static int AmountOfTime(TreeNode root, int start)
    {
        (Dictionary<TreeNode, TreeNode?> parents, TreeNode startNode) = BuildParentMap(root, start);
        Queue<TreeNode> infectionQueue = new();
        HashSet<TreeNode> visited = [];
        infectionQueue.Enqueue(startNode);
        visited.Add(startNode);
        int minutes = -1;

        while (infectionQueue.Count > 0)
        {
            int infectedThisMinute = infectionQueue.Count;

            // 每輪恰好處理目前感染前緣；輪次結束後新增的節點只能在下一分鐘擴散。
            for (int i = 0; i < infectedThisMinute; i++)
            {
                TreeNode node = infectionQueue.Dequeue();

                VisitIfUninfected(node.left, visited, infectionQueue);
                VisitIfUninfected(node.right, visited, infectionQueue);
                VisitIfUninfected(parents[node], visited, infectionQueue);
            }

            minutes++;
        }

        return minutes;
    }

    /// <summary>
    /// 走訪完整二元樹並建立每個節點到父節點的映射，同時找出感染起點。輸入為題目保證有效的非空樹與既存值；
    /// 回傳映射與唯一的起始節點，且不改變任何節點連結。
    /// </summary>
    private static (Dictionary<TreeNode, TreeNode?> Parents, TreeNode StartNode) BuildParentMap(TreeNode root, int start)
    {
        Dictionary<TreeNode, TreeNode?> parents = new() { [root] = null };
        Queue<TreeNode> nodes = new();
        nodes.Enqueue(root);
        TreeNode startNode = root;

        while (nodes.Count > 0)
        {
            TreeNode node = nodes.Dequeue();

            if (node.val == start)
            {
                startNode = node;
            }

            if (node.left is not null)
            {
                parents.Add(node.left, node);
                nodes.Enqueue(node.left);
            }

            if (node.right is not null)
            {
                parents.Add(node.right, node);
                nodes.Enqueue(node.right);
            }
        }

        return (parents, startNode);
    }

    /// <summary>
    /// 檢查 nullable 的相鄰節點是否尚未感染；有效輸入為 parent map 或樹邊取得的節點、目前 BFS 的 visited 集合與 queue。
    /// 節點存在且首次加入 visited 時，將它 enqueue 到下一個 BFS frontier，藉此更新搜尋狀態且避免無向邊回走。
    /// </summary>
    private static void VisitIfUninfected(TreeNode? node, HashSet<TreeNode> visited, Queue<TreeNode> infectionQueue)
    {
        if (node is not null && visited.Add(node))
        {
            infectionQueue.Enqueue(node);
        }
    }

    private static TreeNode CreateOfficialTree()
    {
        return new TreeNode(1,
            new TreeNode(5, null, new TreeNode(4, new TreeNode(9), new TreeNode(2))),
            new TreeNode(3, new TreeNode(10), new TreeNode(6)));
    }

    private static TreeNode CreateCrossRootTree()
    {
        return new TreeNode(1,
            new TreeNode(2, new TreeNode(4), new TreeNode(5)),
            new TreeNode(3, null, new TreeNode(6)));
    }

    private static TreeNode CreateRightSkewedTree(int count)
    {
        TreeNode root = new(1);
        TreeNode current = root;

        for (int value = 2; value <= count; value++)
        {
            current.right = new TreeNode(value);
            current = current.right;
        }

        return root;
    }

    private static TreeNode CreateAsymmetricTree()
    {
        return new TreeNode(8,
            new TreeNode(3, new TreeNode(1), new TreeNode(6)),
            new TreeNode(10, null, new TreeNode(14)));
    }

    private static string EvaluateRepeatedOfficialTreeCalls()
    {
        TreeNode root = CreateOfficialTree();
        return $"({AmountOfTime(root, 3)}, {AmountOfTime(root, 9)})";
    }

    private static string VerifyOfficialTreeResultAndTopologyPreserved()
    {
        TreeNode root = CreateOfficialTree();
        List<(TreeNode Node, int Value, TreeNode? Left, TreeNode? Right)> snapshot = [];
        SnapshotTopology(root, snapshot);

        int minutes = AmountOfTime(root, 3);
        bool isTopologyPreserved = snapshot.All(item =>
            item.Node.val == item.Value && item.Node.left == item.Left && item.Node.right == item.Right);

        return $"{minutes}; {isTopologyPreserved}";
    }

    private static void SnapshotTopology(TreeNode? node, List<(TreeNode Node, int Value, TreeNode? Left, TreeNode? Right)> snapshot)
    {
        if (node is null)
        {
            return;
        }

        snapshot.Add((node, node.val, node.left, node.right));
        SnapshotTopology(node.left, snapshot);
        SnapshotTopology(node.right, snapshot);
    }
}