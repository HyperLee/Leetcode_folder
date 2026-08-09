namespace leetcode_297;

class Program
{
    /// <summary>
    /// 297. Serialize and Deserialize Binary Tree
    /// https://leetcode.com/problems/serialize-and-deserialize-binary-tree/description/
    /// <para>
    /// Serialization is the process of converting a data structure or object into a sequence of bits so it can be stored in a file or memory buffer, or transmitted across a network connection and reconstructed later in the same or another computer environment.
    ///
    /// Design an algorithm to serialize and deserialize a binary tree. There is no restriction on how the algorithm should work. You only need to ensure that a binary tree can be serialized to a string and that the string can be deserialized to the original tree structure.
    ///
    /// Clarification: The input/output format is the same as how LeetCode serializes a binary tree. You do not have to follow this format; be creative and devise your own approach.
    ///
    /// Example 1:
    /// Image: https://assets.leetcode.com/uploads/2020/09/15/serdeser.jpg
    /// Input: root = [1,2,3,null,null,4,5]
    /// Output: [1,2,3,null,null,4,5]
    ///
    /// Example 2:
    /// Input: root = []
    /// Output: []
    ///
    /// Constraints:
    /// - The number of nodes in the tree is in the range [0, 10^4].
    /// - -1000 &lt;= Node.val &lt;= 1000
    /// </para>
    /// <para>
    /// 297. 二元樹的序列化與反序列化
    /// https://leetcode.cn/problems/serialize-and-deserialize-binary-tree/description/
    ///
    /// 序列化是將資料結構或物件轉換成位元序列的過程，使其能儲存在檔案或記憶體緩衝區中，或透過網路連線傳送，之後再於相同或其他電腦環境中重建。
    ///
    /// 請設計一個演算法來序列化與反序列化二元樹。序列化與反序列化演算法的運作方式不受限制；你只需確保二元樹可以被序列化為字串，且該字串可以反序列化回原始樹結構。
    ///
    /// 說明：輸入／輸出格式與 LeetCode 序列化二元樹的方式相同。你不一定要遵循此格式，可以自行設計不同的方法。
    ///
    /// 範例 1：
    /// 圖片：https://assets.leetcode.com/uploads/2020/09/15/serdeser.jpg
    /// 輸入：root = [1,2,3,null,null,4,5]
    /// 輸出：[1,2,3,null,null,4,5]
    ///
    /// 範例 2：
    /// 輸入：root = []
    /// 輸出：[]
    ///
    /// 限制條件：
    /// - 樹中的節點數量介於 [0, 10^4]。
    /// - -1000 &lt;= Node.val &lt;= 1000
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行固定的二元樹案例，分別驗證序列化結果與反序列化後的往返結果。
    /// 案例涵蓋一般樹、空樹、單一節點、負值不平衡樹與重複值；不需要外部輸入。
    /// </summary>
    private static void RunSamples()
    {
        SampleCase[] samples =
        [
            new(
                "一般二元樹",
                new TreeNode(1)
                {
                    left = new TreeNode(2),
                    right = new TreeNode(3)
                    {
                        left = new TreeNode(4),
                        right = new TreeNode(5)
                    }
                },
                "1,2,null,null,3,4,null,null,5,null,null"),
            new("空樹", null, "null"),
            new("單一節點", new TreeNode(1), "1,null,null"),
            new(
                "含負值的不平衡樹",
                new TreeNode(-1)
                {
                    left = new TreeNode(-2)
                    {
                        right = new TreeNode(3)
                    }
                },
                "-1,-2,null,3,null,null,null"),
            new(
                "重複值樹",
                new TreeNode(7)
                {
                    left = new TreeNode(7),
                    right = new TreeNode(7)
                },
                "7,7,null,null,7,null,null")
        ];

        Codec codec = new Codec();
        int passedCount = 0;

        for (int index = 0; index < samples.Length; index++)
        {
            SampleCase sample = samples[index];
            string actual = codec.serialize(sample.Root);
            string roundTrip = codec.serialize(codec.deserialize(actual));
            bool passed = actual == sample.Expected && roundTrip == sample.Expected;

            if (index > 0)
            {
                Console.WriteLine();
            }

            Console.WriteLine($"案例 {index + 1}：{sample.Name}");
            Console.WriteLine($"預期序列化：{sample.Expected}");
            Console.WriteLine($"實際序列化：{actual}");
            Console.WriteLine($"往返序列化：{roundTrip}");
            Console.WriteLine($"結果：{(passed ? "PASS" : "FAIL")}");

            if (passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"總結：{passedCount}/{samples.Length} 筆測試通過");
    }

    /// <summary>
    /// 以帶有分支線的多行格式輸出二元樹，遞迴顯示節點與缺少的子節點。
    /// 輸入可以是空樹；此方法直接寫入主控台，不回傳資料。
    /// </summary>
    /// <param name="node">目前要輸出的節點；空值代表缺少的子節點。</param>
    /// <param name="prefix">目前深度所需的縮排與分支線前綴。</param>
    /// <param name="isLeft">決定目前節點使用結尾分支或中段分支符號。</param>
    private static void PrintTree(TreeNode? node, string prefix, bool isLeft)
    {
        if (node == null)
        {
            Console.WriteLine($"{prefix}{(isLeft ? "└── " : "├── ")}null");
            return;
        }

        Console.WriteLine($"{prefix}{(isLeft ? "└── " : "├── ")}{node.val}");

        if (node.left == null && node.right == null)
        {
            return;
        }

        PrintTree(node.left, prefix + (isLeft ? "    " : "│   "), false);
        PrintTree(node.right, prefix + (isLeft ? "    " : "│   "), true);
    }

    /// <summary>
    /// 以前序走訪順序將二元樹寫入主控台，並以 <c>null</c> 保留缺少的子節點。
    /// 輸入可以是空樹；輸出為逗號分隔的節點 token，不回傳資料。
    /// </summary>
    /// <param name="node">目前要輸出的節點；空值會輸出 <c>null</c>。</param>
    /// <param name="prefix">保留給樹狀輸出呼叫慣例的前綴，目前字串格式不使用。</param>
    /// <param name="isLeft">保留給樹狀輸出呼叫慣例的方向旗標，目前字串格式不使用。</param>
    private static void PrintTree2(TreeNode? node, string prefix, bool isLeft)
    {
        if (node == null)
        {
            Console.Write("null,");
            return;
        }

        Console.Write($"{node.val},");

        // 前序走訪固定依根、左、右遞迴，null token 才能保留原始樹形。
        PrintTree2(node.left, "", false);
        PrintTree2(node.right, "", true);
    }

    /// <summary>
    /// 表示一筆可執行範例，包含案例名稱、允許為空的樹根與手動推導的預期序列。
    /// </summary>
    /// <param name="Name">顯示於主控台的案例名稱。</param>
    /// <param name="Root">要驗證的二元樹根節點；空值表示空樹。</param>
    /// <param name="Expected">以前序走訪和 <c>null</c> 標記表示的預期序列。</param>
    private sealed record SampleCase(string Name, TreeNode? Root, string Expected);
}

/// <summary>
/// 使用 DFS 前序走訪序列化與反序列化二元樹。
/// 每個缺少的子節點都記錄為 <c>null</c>，因此節點值與樹形都能完整往返。
/// </summary>
public class Codec
{
    /// <summary>
    /// 以根、左、右的 DFS 前序順序序列化二元樹，並用 <c>null</c> 保留空分支。
    /// 輸入可以是空樹；輸出為逗號分隔且可供 <see cref="deserialize"/> 還原的字串。
    /// </summary>
    /// <param name="root">要序列化的樹根；空值表示空樹。</param>
    /// <returns>包含節點值與 <c>null</c> 標記的前序序列。</returns>
    public string serialize(TreeNode? root)
    {
        List<string> res = new List<string>();
        SerializeHelper(root, res);
        return string.Join(",", res);
    }

    /// <summary>
    /// 遞迴走訪目前節點，把節點值或空分支標記依前序順序加入結果集合。
    /// </summary>
    /// <param name="node">目前節點；空值表示此分支已結束。</param>
    /// <param name="res">依走訪順序累積 token 的集合。</param>
    private void SerializeHelper(TreeNode? node, List<string> res)
    {
        if (node == null)
        {
            res.Add("null");
            return;
        }

        res.Add(node.val.ToString());

        // 每個空分支也必須寫入，否則不同樹形可能得到相同的節點值序列。
        SerializeHelper(node.left, res);
        SerializeHelper(node.right, res);
    }

    /// <summary>
    /// 將合法的逗號分隔前序序列放入佇列，再依根、左、右順序遞迴重建二元樹。
    /// 輸入必須由 <see cref="serialize"/> 的格式產生；<c>null</c> 會還原成空樹或空分支。
    /// </summary>
    /// <param name="data">包含整數節點值與 <c>null</c> 標記的合法序列。</param>
    /// <returns>重建的樹根；若輸入表示空樹則回傳空值。</returns>
    public TreeNode? deserialize(string data)
    {
        Queue<string> nodes = new Queue<string>(data.Split(','));
        return DeserializeHelper(nodes);
    }

    /// <summary>
    /// 從前序 token 佇列消耗目前節點，並遞迴建立其左、右子樹。
    /// </summary>
    /// <param name="nodes">尚未處理的前序 token 佇列。</param>
    /// <returns>目前子樹的根節點；<c>null</c> token 或空佇列會回傳空值。</returns>
    private TreeNode? DeserializeHelper(Queue<string> nodes)
    {
        if (nodes.Count == 0)
        {
            return null;
        }

        string val = nodes.Dequeue();

        if (val == "null")
        {
            return null;
        }

        TreeNode node = new TreeNode(int.Parse(val));

        // 每個非空 token 都會依序消耗左、右子樹，與序列化順序完全對稱。
        node.left = DeserializeHelper(nodes);
        node.right = DeserializeHelper(nodes);

        return node;
    }
}

/// <summary>
/// 表示二元樹節點，包含整數值以及允許為空的左、右子節點。
/// </summary>
public class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;

    /// <summary>
    /// 建立具有指定整數值且左右子節點皆為空的新節點。
    /// </summary>
    /// <param name="x">節點要保存的整數值。</param>
    public TreeNode(int x)
    {
        val = x;
    }
}
