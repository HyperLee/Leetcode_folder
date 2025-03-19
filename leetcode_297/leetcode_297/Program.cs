namespace leetcode_297;

class Program
{
    /// <summary>
    /// 297. Serialize and Deserialize Binary Tree
    /// https://leetcode.com/problems/serialize-and-deserialize-binary-tree/description/
    /// 297. 二叉树的序列化与反序列化
    /// https://leetcode.cn/problems/serialize-and-deserialize-binary-tree/description/  
    ///
    /// wiki介紹序列化與反序列化
    /// https://zh.wikipedia.org/zh-tw/%E5%BA%8F%E5%88%97%E5%8C%96
    /// 
    /// wiki 樹的走訪
    /// https://zh.wikipedia.org/zh-tw/%E6%A0%91%E7%9A%84%E9%81%8D%E5%8E%86 
    /// 前序走訪（Pre-Order Traversal）是依序以根節點、左節點、右節點為順序走訪的方式。
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Codec codec = new Codec();

        // 測試用例 1: 一般二叉樹
        TreeNode root1 = new TreeNode(1) {
            left = new TreeNode(2),
            right = new TreeNode(3) {
                left = new TreeNode(4),
                right = new TreeNode(5)
            }
        };
        Console.WriteLine("測試用例 1:");
        string serialized1 = codec.serialize(root1);
        Console.WriteLine($"序列化結果: {serialized1}");
        TreeNode deserialized1 = codec.deserialize(serialized1);
        //Console.WriteLine($"反序列化後根節點值: {deserialized1.val}");
        //Console.WriteLine("反序列化後的樹結構-視覺化結構:");
        //PrintTree(deserialized1, "", true);
        Console.Write("反序列化後的樹結構-字串: ");
        PrintTree2(deserialized1, "", true);
        Console.WriteLine();  // 換行

        // 測試用例 2: 空樹
        TreeNode root2 = null;
        Console.WriteLine("\n測試用例 2:");
        string serialized2 = codec.serialize(root2);
        Console.WriteLine($"序列化結果: {serialized2}");
        TreeNode deserialized2 = codec.deserialize(serialized2);
        //Console.WriteLine($"反序列化後結果: {(deserialized2 == null ? "null" : deserialized2.val.ToString())}");
        //Console.WriteLine("反序列化後的樹結構-視覺化結構:");
        //PrintTree(deserialized2, "", true);        
        Console.Write("反序列化後的樹結構-字串: ");
        PrintTree2(deserialized2, "", true);
        Console.WriteLine();  // 換行

        // 測試用例 3: 只有一個節點的樹
        TreeNode root3 = new TreeNode(1);
        Console.WriteLine("\n測試用例 3:");
        string serialized3 = codec.serialize(root3);
        Console.WriteLine($"序列化結果: {serialized3}");
        TreeNode deserialized3 = codec.deserialize(serialized3);
        //Console.WriteLine($"反序列化後根節點值: {deserialized3.val}");
        //Console.WriteLine("反序列化後的樹結構-視覺化結構:");
        //PrintTree(deserialized3, "", true);   
        Console.Write("反序列化後的樹結構-字串: ");
        PrintTree2(deserialized3, "", true);
        Console.WriteLine();  // 換行             
    }

    /// <summary>
    /// 新增輔助方法來印出樹的結構
    /// 視覺化結構輸出
    /// </summary>
    /// <param name="node"></param>
    /// <param name="prefix"></param>
    /// <param name="isLeft"></param>
    private static void PrintTree(TreeNode node, string prefix, bool isLeft)
    {
        if (node == null)
        {
            Console.WriteLine($"{prefix}{(isLeft ? "└── " : "├── ")}null");
            return;
        }

        Console.WriteLine($"{prefix}{(isLeft ? "└── " : "├── ")}{node.val}");

        if (node.left == null && node.right == null) return;

        // 處理左子樹
        PrintTree(node.left, prefix + (isLeft ? "    " : "│   "), false);
        // 處理右子樹
        PrintTree(node.right, prefix + (isLeft ? "    " : "│   "), true);
    }

    /// <summary>
    /// 字串輸出
    /// </summary>
    /// <param name="node"></param>
    /// <param name="prefix"></param>
    /// <param name="isLeft"></param>
    private static void PrintTree2(TreeNode node, string prefix, bool isLeft)
    {
        if (node == null)
        {
            Console.Write("null,");
            return;
        }

        // 前序遍歷輸出：根->左->右
        Console.Write($"{node.val},");
        
        // 遞迴處理左子樹
        PrintTree2(node.left, "", false);
        
        // 遞迴處理右子樹
        PrintTree2(node.right, "", true);
    }

}

/// <summary>
/// 我們可以使用**遞歸（Recursion）**來解決這個問題，並透過 DFS（深度優先搜尋） 來遍歷二叉樹。
/// 解題思路：
/// 1. 序列化(Serialize)：
///    - 使用前序遍歷(Pre-order Traversal)，按照 根->左->右 的順序訪問節點
///    - 使用深度優先搜索(DFS)遍歷整棵樹
///    - 將 null 節點也記錄下來，確保能完整重建樹的結構
///
/// 2. 反序列化(Deserialize)：
///    - 將序列化的字串分割成節點值陣列
///    - 使用佇列(Queue)儲存節點值，方便按順序處理
///    - 通過遞迴方式重建樹結構，遵循與序列化相同的前序遍歷順序
/// </summary>
public class Codec 
{
    // Encodes a tree to a single string.
    /// <summary>
    /// 序列化: 使用前序遍歷 (DFS)
    /// null 節點用 "null" 表示，節點值用 "," 分隔。 
    /// 
    /// 序列化過程：將二叉樹轉換為字串表示
    /// 時間複雜度：O(n)，其中 n 是樹中的節點數
    /// 空間複雜度：O(n)，用於儲存結果陣列
    /// </summary>
    public string serialize(TreeNode root) 
    {
        List<string> res = new List<string>();
        SerializeHelper(root, res);
        return string.Join(",", res);
    }

    /// <summary>
    /// 序列化輔助函式：使用前序遍歷處理每個節點
    /// </summary>
    /// <param name="node">當前處理的節點</param>
    /// <param name="res">用於儲存序列化結果的列表</param>
    private void SerializeHelper(TreeNode node, List<string> res)
    {
        // 步驟1: 處理空節點情況
        if(node == null)
        {
            res.Add("null"); // 將null節點記錄為字串"null"
            return;
        }

        // 步驟2: 處理當前節點 - 加入節點值
        res.Add(node.val.ToString());
        
        // 步驟3: 遞迴處理左子樹
        SerializeHelper(node.left, res);
        
        // 步驟4: 遞迴處理右子樹
        SerializeHelper(node.right, res);
    }

    // Decodes your encoded data to tree.
    /// <summary>
    /// 反序列化: 使用前序遍歷還原 (DFS)
    /// 使用 Queue 來解反序列化
    /// 讀取字串並轉換為陣列（Queue）。
    /// 透過遞歸構建二叉樹：
    /// 取出當前節點值，若為 "null" 則回傳 null。
    /// 否則建立節點，並對左子樹、右子樹進行遞歸還原。
    /// 
    /// 反序列化過程：將字串還原為二叉樹
    /// 時間複雜度：O(n)，其中 n 是節點數
    /// 空間複雜度：O(n)，用於佇列和遞迴調用棧
    /// </summary>
    public TreeNode deserialize(string data) 
    {
        // 用","來區分每個node.val
        Queue<string> nodes = new Queue<string>(data.Split(','));
        return DeserializeHelper(nodes);
    }

    /// <summary>
    /// 反序列化輔助函式：從佇列中重建二叉樹
    /// </summary>
    /// <param name="nodes">包含所有節點值的佇列</param>
    /// <returns>重建的二叉樹節點</returns>
    private TreeNode DeserializeHelper(Queue<string> nodes)
    {
        // 步驟1: 檢查佇列是否為空
        if(nodes.Count == 0)
        {
            return null;
        }

        // 步驟2: 取出當前節點值
        string val = nodes.Dequeue();
        
        // 步驟3: 處理空節點情況
        if(val == "null")
        {
            return null;
        }

        // 步驟4: 建立新節點
        TreeNode node = new TreeNode(int.Parse(val));
        
        // 步驟5: 遞迴建立左子樹
        node.left = DeserializeHelper(nodes);
        
        // 步驟6: 遞迴建立右子樹
        node.right = DeserializeHelper(nodes);

        return node;
    }
}

public class TreeNode 
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int x) { val = x; }
}
