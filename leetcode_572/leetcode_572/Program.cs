using System.Globalization;
using System.Text;

namespace leetcode_572;

class Program
{
    /// <summary>
    /// 表示二元樹中的一個節點，保存整數值以及可為空的左右子節點。
    /// 輸入由建構子提供節點值與選用的子樹，輸出為兩種子樹判斷解法共用的樹結構。
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 目前節點保存的整數值。
        /// </summary>
        public int val;

        /// <summary>
        /// 左子節點；null 表示沒有左子樹。
        /// </summary>
        public TreeNode? left;

        /// <summary>
        /// 右子節點；null 表示沒有右子樹。
        /// </summary>
        public TreeNode? right;

        /// <summary>
        /// 建立一個二元樹節點，並可同時指定左右子樹。
        /// 輸入為節點值及可為 null 的子節點，輸出為初始化完成的節點。
        /// </summary>
        /// <param name="val">目前節點保存的整數值。</param>
        /// <param name="left">左子節點；預設為 null。</param>
        /// <param name="right">右子節點；預設為 null。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// 572. Subtree of Another Tree
    /// https://leetcode.com/problems/subtree-of-another-tree/description/
    /// <para>
    /// Given the roots of two binary trees root and subRoot, return true if there is a subtree of root with the same structure and node values as subRoot, and false otherwise.
    ///
    /// A subtree of a binary tree tree consists of a node in tree and all of that node's descendants. The tree tree can also be considered a subtree of itself.
    ///
    /// Example 1:
    /// Image: https://assets.leetcode.com/uploads/2021/04/28/subtree1-tree.jpg
    /// Input: root = [3,4,5,1,2], subRoot = [4,1,2]
    /// Output: true
    ///
    /// Example 2:
    /// Image: https://assets.leetcode.com/uploads/2021/04/28/subtree2-tree.jpg
    /// Input: root = [3,4,5,1,2,null,null,null,null,0], subRoot = [4,1,2]
    /// Output: false
    ///
    /// Constraints:
    /// - The number of nodes in root is in [1, 2000].
    /// - The number of nodes in subRoot is in [1, 1000].
    /// - -10^4 &lt;= root.val &lt;= 10^4
    /// - -10^4 &lt;= subRoot.val &lt;= 10^4
    /// </para>
    /// <para>
    /// 572. 另一棵樹的子樹
    /// https://leetcode.cn/problems/subtree-of-another-tree/description/
    ///
    /// 給定兩棵二元樹的根節點 root 與 subRoot，若 root 中存在一棵與 subRoot 結構和節點值都相同的子樹則回傳 true，否則回傳 false。
    ///
    /// 二元樹 tree 的子樹，由 tree 中某個節點與該節點的所有後代組成。tree 本身也可視為自己的子樹。
    ///
    /// 範例 1：
    /// 圖片：https://assets.leetcode.com/uploads/2021/04/28/subtree1-tree.jpg
    /// 輸入：root = [3,4,5,1,2], subRoot = [4,1,2]
    /// 輸出：true
    ///
    /// 範例 2：
    /// 圖片：https://assets.leetcode.com/uploads/2021/04/28/subtree2-tree.jpg
    /// 輸入：root = [3,4,5,1,2,null,null,null,null,0], subRoot = [4,1,2]
    /// 輸出：false
    ///
    /// 限制條件：
    /// - root 中的節點數量在 [1, 2000] 範圍內。
    /// - subRoot 中的節點數量在 [1, 1000] 範圍內。
    /// - -10^4 &lt;= root.val &lt;= 10^4
    /// - -10^4 &lt;= subRoot.val &lt;= 10^4
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行六組固定範例，同時驗證遞迴比對與序列化加 KMP 兩種解法。
    /// 輸入來自方法內定義的層序陣列，輸出為每案的預期值、實際值、PASS/FAIL 與通過總數。
    /// </summary>
    private static void RunSamples()
    {
        SampleCase[] samples =
        [
            new("官方正例", [3, 4, 5, 1, 2], [4, 1, 2], true),
            new("官方反例：候選節點多出後代", [3, 4, 5, 1, 2, null, null, null, null, 0], [4, 1, 2], false),
            new("整棵樹完全相同", [1, 2, 3], [1, 2, 3], true),
            new("單一葉節點子樹", [1, 2, 3], [3], true),
            new("重複值但左右結構不同", [1, 1], [1, null, 1], false),
            new("多位數 token 邊界", [12], [2], false)
        ];

        int passedChecks = 0;
        int totalChecks = samples.Length * 2;

        for (int index = 0; index < samples.Length; index++)
        {
            SampleCase sample = samples[index];
            TreeNode? root = BuildTree(sample.RootValues);
            TreeNode? subRoot = BuildTree(sample.SubRootValues);
            bool recursiveResult = IsSubtree(root, subRoot);
            bool serializedResult = IsSubtree2(root, subRoot);
            bool recursivePassed = recursiveResult == sample.Expected;
            bool serializedPassed = serializedResult == sample.Expected;

            passedChecks += recursivePassed ? 1 : 0;
            passedChecks += serializedPassed ? 1 : 0;

            Console.WriteLine($"案例 {index + 1}：{sample.Name}");
            Console.WriteLine($"root = {FormatValues(sample.RootValues)}");
            Console.WriteLine($"subRoot = {FormatValues(sample.SubRootValues)}");
            Console.WriteLine($"預期：{sample.Expected}");
            Console.WriteLine($"解法一（遞迴比對）：{recursiveResult} => {(recursivePassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"解法二（序列化 + KMP）：{serializedResult} => {(serializedPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 將 LeetCode 層序陣列建立成二元樹，依序把後續值配置為每個非空節點的左右子節點。
    /// 輸入可為空陣列或以 null 開頭；輸出為建立完成的樹根，無有效根節點時回傳 null。
    /// </summary>
    /// <param name="values">以 null 表示缺少節點的層序資料。</param>
    /// <returns>依層序資料建立的樹根；資料為空樹時回傳 null。</returns>
    private static TreeNode? BuildTree(int?[] values)
    {
        if (values.Length == 0 || values[0] is not int rootValue)
        {
            return null;
        }

        TreeNode root = new(rootValue);
        Queue<TreeNode> pendingParents = new();
        pendingParents.Enqueue(root);
        int valueIndex = 1;

        while (pendingParents.Count > 0 && valueIndex < values.Length)
        {
            TreeNode parent = pendingParents.Dequeue();

            if (values[valueIndex] is int leftValue)
            {
                TreeNode left = new(leftValue);
                parent.left = left;
                pendingParents.Enqueue(left);
            }

            valueIndex++;

            if (valueIndex < values.Length && values[valueIndex] is int rightValue)
            {
                TreeNode right = new(rightValue);
                parent.right = right;
                pendingParents.Enqueue(right);
            }

            valueIndex++;
        }

        return root;
    }

    /// <summary>
    /// 將層序測試資料格式化為易讀的方括號字串，保留 null 以呈現缺少的子節點。
    /// 輸入為可含 null 的整數陣列，輸出為可直接顯示於主控台與 README 的文字。
    /// </summary>
    /// <param name="values">要格式化的層序測試資料。</param>
    /// <returns>例如 <c>[3,4,5,1,2]</c> 的顯示字串。</returns>
    private static string FormatValues(int?[] values)
    {
        return $"[{string.Join(",", values.Select(value => value?.ToString() ?? "null"))}]";
    }

    /// <summary>
    /// 使用深度優先搜尋判斷 subRoot 是否為 root 的子樹。
    /// 解題概念是把主樹每個節點視為候選根，先比較整棵樹，再遞迴搜尋左右子樹；
    /// 輸入可為空樹，空的 subRoot 視為任何樹的子樹，輸出為是否存在結構和值皆相同的子樹。
    /// </summary>
    /// <param name="root">要搜尋的主樹根節點；null 表示空樹。</param>
    /// <param name="subRoot">候選子樹根節點；null 表示空樹。</param>
    /// <returns>若 subRoot 是 root 的子樹則回傳 true，否則回傳 false。</returns>
    public static bool IsSubtree(TreeNode? root, TreeNode? subRoot)
    {
        if (subRoot is null)
        {
            return true;
        }

        if (root is null)
        {
            return false;
        }

        // 目前節點先作完整樹比對；失敗後才把左右子樹當成下一批候選根。
        if (IsSameTree(root, subRoot))
        {
            return true;
        }

        return IsSubtree(root.left, subRoot) || IsSubtree(root.right, subRoot);
    }

    /// <summary>
    /// 遞迴判斷兩棵二元樹是否完全相同，對應節點必須同時存在、值相等且左右子樹都相同。
    /// 輸入可為空節點，輸出為兩棵目前子樹的結構與節點值是否完全一致。
    /// </summary>
    /// <param name="p">第一棵樹的目前節點；null 表示空分支。</param>
    /// <param name="q">第二棵樹的目前節點；null 表示空分支。</param>
    /// <returns>若兩棵樹結構和節點值完全相同則回傳 true，否則回傳 false。</returns>
    private static bool IsSameTree(TreeNode? p, TreeNode? q)
    {
        if (p is null && q is null)
        {
            return true;
        }

        if (p is null || q is null)
        {
            return false;
        }

        if (p.val != q.val)
        {
            return false;
        }

        return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
    }

    /// <summary>
    /// 使用前序序列化與 KMP 字串搜尋判斷 subRoot 是否為 root 的子樹。
    /// 解題概念是把節點值、token 邊界與空分支完整編碼，再以線性匹配尋找候選子樹序列；
    /// 輸入可為空樹，空的 subRoot 視為任何樹的子樹，輸出為是否存在結構和值皆相同的子樹。
    /// </summary>
    /// <param name="root">要搜尋的主樹根節點；null 表示空樹。</param>
    /// <param name="subRoot">候選子樹根節點；null 表示空樹。</param>
    /// <returns>若 subRoot 是 root 的子樹則回傳 true，否則回傳 false。</returns>
    public static bool IsSubtree2(TreeNode? root, TreeNode? subRoot)
    {
        if (subRoot is null)
        {
            return true;
        }

        if (root is null)
        {
            return false;
        }

        string rootSerialization = SerializeTree(root);
        string subRootSerialization = SerializeTree(subRoot);
        return ContainsWithKmp(rootSerialization, subRootSerialization);
    }

    /// <summary>
    /// 將二元樹以前序順序轉為可匹配的字串，使用數值前綴、逗號分隔與空分支 token 保留邊界及樹形。
    /// 輸入可為空樹，輸出為能唯一描述節點值與結構的序列化結果。
    /// </summary>
    /// <param name="root">要序列化的樹根；null 表示空樹。</param>
    /// <returns>由數值 token 與 <c>#</c> 空分支 token 組成的前序字串。</returns>
    private static string SerializeTree(TreeNode? root)
    {
        StringBuilder serialization = new();
        AppendSerialized(root, serialization);
        return serialization.ToString();
    }

    /// <summary>
    /// 以前序順序把目前節點附加到序列化結果，遞迴處理根、左、右並記錄所有空分支。
    /// 輸入為目前節點與累積字串，輸出透過 StringBuilder 參數寫入，不另行回傳值。
    /// </summary>
    /// <param name="node">目前要寫入的節點；null 會寫成空分支 token。</param>
    /// <param name="serialization">累積完整前序序列的字串建構器。</param>
    private static void AppendSerialized(TreeNode? node, StringBuilder serialization)
    {
        if (node is null)
        {
            // 缺少的子節點也必須入列，否則相同節點值可能掩蓋左右結構差異。
            serialization.Append("#,");
            return;
        }

        // ^ 標示數值 token 起點，避免數值 2 誤匹配到 12 的字元內部。
        serialization.Append('^');
        serialization.Append(node.val.ToString(CultureInfo.InvariantCulture));
        serialization.Append(',');
        AppendSerialized(node.left, serialization);
        AppendSerialized(node.right, serialization);
    }

    /// <summary>
    /// 使用 KMP 判斷 pattern 是否出現在 text 中，匹配失敗時利用 LPS 表重用既有前綴資訊。
    /// 輸入為主字串與非空模式字串，輸出為是否找到完整模式，時間複雜度為 O(n + m)。
    /// </summary>
    /// <param name="text">要被搜尋的完整主樹序列。</param>
    /// <param name="pattern">要尋找的候選子樹序列。</param>
    /// <returns>若 pattern 完整出現在 text 中則回傳 true，否則回傳 false。</returns>
    private static bool ContainsWithKmp(string text, string pattern)
    {
        if (pattern.Length == 0)
        {
            return true;
        }

        int[] longestPrefixSuffix = BuildLps(pattern);
        int textIndex = 0;
        int patternIndex = 0;

        while (textIndex < text.Length)
        {
            if (text[textIndex] == pattern[patternIndex])
            {
                textIndex++;
                patternIndex++;

                if (patternIndex == pattern.Length)
                {
                    return true;
                }

                continue;
            }

            if (patternIndex > 0)
            {
                // 回退到上一個可延續的前後綴，主字串索引不必重新開始。
                patternIndex = longestPrefixSuffix[patternIndex - 1];
                continue;
            }

            textIndex++;
        }

        return false;
    }

    /// <summary>
    /// 建立 KMP 的最長相同前後綴表，記錄每個模式位置發生失配時可回退的匹配長度。
    /// 輸入為模式字串，輸出為同長度的整數陣列，每格代表該位置結尾的最長共同前後綴。
    /// </summary>
    /// <param name="pattern">要預處理的非空模式字串。</param>
    /// <returns>KMP 搜尋使用的 LPS 回退表。</returns>
    private static int[] BuildLps(string pattern)
    {
        int[] longestPrefixSuffix = new int[pattern.Length];
        int prefixLength = 0;
        int patternIndex = 1;

        while (patternIndex < pattern.Length)
        {
            if (pattern[patternIndex] == pattern[prefixLength])
            {
                prefixLength++;
                longestPrefixSuffix[patternIndex] = prefixLength;
                patternIndex++;
                continue;
            }

            if (prefixLength > 0)
            {
                // 先嘗試較短的既有前綴，不立刻放棄目前 patternIndex。
                prefixLength = longestPrefixSuffix[prefixLength - 1];
                continue;
            }

            patternIndex++;
        }

        return longestPrefixSuffix;
    }

    /// <summary>
    /// 表示一筆雙解法驗證案例，保存案例名稱、兩棵樹的層序資料與預期判斷結果。
    /// </summary>
    /// <param name="Name">顯示於主控台的案例名稱。</param>
    /// <param name="RootValues">主樹的層序資料。</param>
    /// <param name="SubRootValues">候選子樹的層序資料。</param>
    /// <param name="Expected">候選樹是否應被判定為子樹。</param>
    private sealed record SampleCase(
        string Name,
        int?[] RootValues,
        int?[] SubRootValues,
        bool Expected);
}
