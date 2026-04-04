namespace leetcode_024;

class Program
{
    public class ListNode
    {
        public int val;
        public ListNode? next;
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    /// <summary>
    /// Given a linked list, swap every two adjacent nodes and return its head.
    /// You must solve the problem without modifying the values in the list's nodes (i.e., only nodes themselves may be changed.)
    /// 給定一個鏈表，兩兩交換相鄰節點並返回其頭節點。
    /// 你必須在不修改節點值的情況下解題（即只能改變節點本身的連接）。
    /// 24. Swap Nodes in Pairs
    /// https://leetcode.com/problems/swap-nodes-in-pairs/description/
    /// 24. 两两交换链表中的节点
    /// https://leetcode.cn/problems/swap-nodes-in-pairs/description/
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：偶數節點 [1,2,3,4] → [2,1,4,3]
        ListNode? head1 = BuildList([1, 2, 3, 4]);
        Console.WriteLine("Test 1 Input:  " + PrintList(head1));
        Console.WriteLine("Test 1 Output: " + PrintList(solution.SwapPairs(head1)));
        Console.WriteLine();

        // 測試案例 2：空鏈表 [] → []
        ListNode? head2 = BuildList([]);
        Console.WriteLine("Test 2 Input:  " + PrintList(head2));
        Console.WriteLine("Test 2 Output: " + PrintList(solution.SwapPairs(head2)));
        Console.WriteLine();

        // 測試案例 3：只有一個節點 [1] → [1]
        ListNode? head3 = BuildList([1]);
        Console.WriteLine("Test 3 Input:  " + PrintList(head3));
        Console.WriteLine("Test 3 Output: " + PrintList(solution.SwapPairs(head3)));
        Console.WriteLine();

        // 測試案例 4：奇數節點 [1,2,3] → [2,1,3]，最後一個節點無法配對故維持原位
        ListNode? head4 = BuildList([1, 2, 3]);
        Console.WriteLine("Test 4 Input:  " + PrintList(head4));
        Console.WriteLine("Test 4 Output: " + PrintList(solution.SwapPairs(head4)));
    }

    /// <summary>
    /// 將整數陣列轉換為鏈表。
    /// </summary>
    /// <param name="values">節點值的整數陣列</param>
    /// <returns>鏈表的頭節點；若陣列為空則回傳 null</returns>
    static ListNode? BuildList(int[] values)
    {
        if (values.Length == 0)
        {
            return null;
        }

        // 虛擬頭節點，方便統一串接邏輯
        var dummy = new ListNode(0);
        var current = dummy;

        foreach (var v in values)
        {
            current.next = new ListNode(v);
            current = current.next!;
        }

        return dummy.next;
    }

    /// <summary>
    /// 將鏈表輸出為易讀字串，格式為 [v1 -> v2 -> ...]。
    /// </summary>
    /// <param name="head">鏈表頭節點</param>
    /// <returns>可供顯示的字串</returns>
    static string PrintList(ListNode? head)
    {
        if (head is null)
        {
            return "[]";
        }

        var sb = new System.Text.StringBuilder("[");

        while (head is not null)
        {
            sb.Append(head.val);

            if (head.next is not null)
            {
                sb.Append(" -> ");
            }

            head = head.next;
        }

        sb.Append(']');

        return sb.ToString();
    }


    /// <summary>
    /// 遞迴法：兩兩交換鏈表中相鄰的節點。
    ///
    /// ─── 解題思路 ───────────────────────────────────────────────────
    /// 核心觀察：每次只處理「目前的兩個節點」的交換，
    /// 然後把「剩餘子鏈表」的交換工作交給遞迴來處理。
    ///
    /// 終止條件（Base Case）：
    ///   - head 為 null           → 鏈表為空，無節點可交換，直接回傳。
    ///   - head.next 為 null      → 只剩一個節點（奇數尾端），無法配對，直接回傳。
    ///
    /// 單層遞迴步驟（以 [1 → 2 → 3 → 4] 為例）：
    ///   1. 令 newHead = head.next（即節點 2），它將成為這一對的新頭節點。
    ///   2. 遞迴處理 newHead.next 之後的子鏈表（[3 → 4]），
    ///      取得交換後的結果，並接到原 head（節點 1）的 next。
    ///   3. 將 newHead.next 指向 head，完成這對節點的交換。
    ///   4. 回傳 newHead，作為本層已交換完的子鏈表頭節點。
    ///
    /// 節點指標關係：
    ///   交換前：head(1) → newHead(2) → [其餘子鏈表]
    ///   交換後：newHead(2) → head(1) → SwapPairs([其餘子鏈表])
    ///
    /// 時間複雜度：O(n)，每個節點恰好被走訪一次。
    /// 空間複雜度：O(n)，遞迴呼叫堆疊深度為 n/2。
    /// ────────────────────────────────────────────────────────────────
    /// </summary>
    /// <param name="head">鏈表的頭節點</param>
    /// <returns>兩兩交換後的鏈表頭節點</returns>
    public ListNode? SwapPairs(ListNode? head)
    {
        // 終止條件：空鏈表或只剩一個節點時，無法進行交換，直接回傳
        if (head is null || head.next is null)
        {
            return head;
        }

        // 步驟 1：記錄第二個節點，它將成為本對交換後的新頭節點
        ListNode newHead = head.next;

        // 步驟 2：遞迴處理第三個節點之後的子鏈表，將結果接到原頭節點的 next
        //         這樣 head（原第一個節點）就已指向後續已交換完的鏈表
        head.next = SwapPairs(newHead.next);

        // 步驟 3：將 newHead 的 next 指向 head，完成這對節點的位置互換
        newHead.next = head;

        // 步驟 4：回傳新的頭節點（原第二個節點）
        return newHead;
    }
}
