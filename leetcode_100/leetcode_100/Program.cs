namespace leetcode_100;

class Program
{
    /// <summary>
    /// 表示二元樹中的單一節點，保存節點值以及可為空的左右子節點參考。
    /// 輸入節點值與左右子樹即可組成題目使用的二元樹；建構後的節點可作為
    /// <see cref="IsSameTree"/> 的輸入。
    /// </summary>
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        /// <summary>
        /// 建立一個二元樹節點。節點值預設為 0，左右子節點可省略；
        /// 省略或傳入 <see langword="null"/> 代表該方向沒有子樹。
        /// 建構結果是包含指定值與子樹參考的新節點。
        /// </summary>
        /// <param name="val">節點儲存的整數值，題目限制為 -10^4 到 10^4。</param>
        /// <param name="left">左子節點；沒有左子樹時為 <see langword="null"/>。</param>
        /// <param name="right">右子節點；沒有右子樹時為 <see langword="null"/>。</param>
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }


    /// <summary>
    /// <para>
    /// 100. Same Tree
    /// https://leetcode.com/problems/same-tree/description/
    ///
    /// Given the roots of two binary trees p and q, write a function to check if they are the same or not.
    /// Two binary trees are considered the same if they are structurally identical, and the nodes have the
    /// same value.
    ///
    /// Example 1:
    /// Input: p = [1,2,3], q = [1,2,3]
    /// Output: true
    ///
    /// Example 2:
    /// Input: p = [1,2], q = [1,null,2]
    /// Output: false
    ///
    /// Example 3:
    /// Input: p = [1,2,1], q = [1,1,2]
    /// Output: false
    ///
    /// Constraints:
    /// The number of nodes in both trees is in the range [0, 100].
    /// -10^4 &lt;= Node.val &lt;= 10^4
    /// </para>
    /// <para>
    /// 100. 相同的樹
    /// https://leetcode.cn/problems/same-tree/description/
    ///
    /// 給定兩棵二元樹的根節點 p 與 q，請撰寫一個函式檢查它們是否相同。
    /// 如果兩棵二元樹的結構完全相同，且所有對應節點的值也相同，則視為相同的樹。
    ///
    /// 範例 1：
    /// 輸入：p = [1,2,3], q = [1,2,3]
    /// 輸出：true
    ///
    /// 範例 2：
    /// 輸入：p = [1,2], q = [1,null,2]
    /// 輸出：false
    ///
    /// 範例 3：
    /// 輸入：p = [1,2,1], q = [1,1,2]
    /// 輸出：false
    ///
    /// 限制條件：
    /// 兩棵樹中的節點數量都介於 [0, 100]。
    /// -10^4 &lt;= Node.val &lt;= 10^4
    /// </para>
    /// </summary>
    /// <remarks>
    /// 以五組固定案例執行目前的遞迴解法，逐一輸出 PASS/FAIL，最後彙整整體結果。
    /// </remarks>
    /// <param name="args">命令列參數；本範例不需要額外輸入。</param>
    private static void Main(string[] args)
    {
        int passedCount = 0;
        const int totalCount = 5;

        passedCount += RunTestCase(
            "兩棵空樹",
            null,
            null,
            true) ? 1 : 0;
        passedCount += RunTestCase(
            "相同的三節點樹",
            new TreeNode(1, new TreeNode(2), new TreeNode(3)),
            new TreeNode(1, new TreeNode(2), new TreeNode(3)),
            true) ? 1 : 0;
        passedCount += RunTestCase(
            "左右結構不同",
            new TreeNode(1, new TreeNode(2)),
            new TreeNode(1, null, new TreeNode(2)),
            false) ? 1 : 0;
        passedCount += RunTestCase(
            "相同結構但節點值不同",
            new TreeNode(1, new TreeNode(2), new TreeNode(1)),
            new TreeNode(1, new TreeNode(1), new TreeNode(2)),
            false) ? 1 : 0;
        passedCount += RunTestCase(
            "單邊為空樹",
            new TreeNode(1),
            null,
            false) ? 1 : 0;

        Console.WriteLine();
        Console.WriteLine($"{passedCount}/{totalCount} test cases passed.");
        Console.WriteLine(passedCount == totalCount ? "Overall: PASS" : "Overall: FAIL");
    }


    /// <summary>
    /// 執行單一固定案例，呼叫 <see cref="IsSameTree"/>
    /// 比較兩棵輸入樹，再將實際結果與預期布林值比對。
    /// 輸入樹可以為空；方法會輸出案例名稱、PASS/FAIL、預期值與實際值，
    /// 並回傳案例是否通過，供主要進入點統計結果。
    /// </summary>
    /// <param name="caseName">顯示於主控台的案例名稱。</param>
    /// <param name="p">第一棵待比較的二元樹根節點；空樹時為 <see langword="null"/>。</param>
    /// <param name="q">第二棵待比較的二元樹根節點；空樹時為 <see langword="null"/>。</param>
    /// <param name="expected">此案例預期的樹比較結果。</param>
    /// <returns>實際結果與預期結果相同時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private static bool RunTestCase(string caseName, TreeNode? p, TreeNode? q, bool expected)
    {
        bool actual = IsSameTree(p, q);
        bool passed = actual == expected;

        Console.WriteLine(
            $"[{(passed ? "PASS" : "FAIL")}] {caseName} | Expected: {expected} | Actual: {actual}");

        return passed;
    }


    /// <summary>
    /// 判斷兩棵二元樹是否具有完全相同的結構與節點值。
    /// 解法從兩個根節點同步遞迴：先處理空節點與值不同的終止條件，
    /// 再分別比較左右子樹；只有左右兩側都相同才回傳
    /// <see langword="true"/>。輸入可為空樹，且方法不會修改任何節點。
    /// </summary>
    /// <param name="p">第一棵待比較的二元樹根節點；空樹時為 <see langword="null"/>。</param>
    /// <param name="q">第二棵待比較的二元樹根節點；空樹時為 <see langword="null"/>。</param>
    /// <returns>兩棵樹的結構及對應節點值完全相同時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    public static bool IsSameTree(TreeNode? p, TreeNode? q)
    {
        if (p == null && q == null)
        {
            // 兩邊同時走到空節點，表示目前分支的結構與內容完全一致。
            return true;
        }
        else if (p == null || q == null)
        {
            // 僅一邊為空代表樹形不同，不需要再向下比較。
            return false;
        }
        else if (p.val != q.val)
        {
            // 目前位置的節點值不同，兩棵樹不可能相同。
            return false;
        }
        else
        {
            // 左右子樹必須同時相同；任一側失敗即可提前結束。
            return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }
    }
}
