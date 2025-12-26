namespace leetcode_083;

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
    /// 83. Remove Duplicates from Sorted List
    /// https://leetcode.com/problems/remove-duplicates-from-sorted-list/description/
    /// 83. 删除排序链表中的重复元素
    /// https://leetcode.cn/problems/remove-duplicates-from-sorted-list/description/
    /// 
    /// Given the head of a sorted linked list, delete all duplicates such that each element appears only once. 
    /// Return the linked list sorted as well.
    /// 
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        // 測試範例 1: [1,1,2] -> [1,2]
        ListNode head1 = new ListNode(1, new ListNode(1, new ListNode(2)));
        ListNode? result1 = DeleteDuplicates(head1);
        Console.Write("Example 1: ");
        PrintList(result1);

        // 測試範例 2: [1,1,2,3,3] -> [1,2,3]
        ListNode head2 = new ListNode(1, new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(3)))));
        ListNode? result2 = DeleteDuplicates(head2);
        Console.Write("Example 2: ");
        PrintList(result2);

        // 測試範例 3: 空串列 -> 空串列
        ListNode? head3 = null;
        ListNode? result3 = DeleteDuplicates(head3);
        Console.Write("Example 3 (empty): ");
        PrintList(result3);

        // 測試範例 4: [1] -> [1] (單一節點)
        ListNode head4 = new ListNode(1);
        ListNode? result4 = DeleteDuplicates(head4);
        Console.Write("Example 4 (single): ");
        PrintList(result4);
    }

    /// <summary>
    /// 輔助方法：印出鏈結串列內容
    /// </summary>
    /// <param name="head">鏈結串列的頭節點</param>
    private static void PrintList(ListNode? head)
    {
        List<int> values = [];
        ListNode? current = head;
        while (current is not null)
        {
            values.Add(current.val);
            current = current.next;
        }
        Console.WriteLine($"[{string.Join(", ", values)}]");
    }

    /// <summary>
    /// 刪除已排序鏈結串列中的重複元素
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>由於鏈結串列已經排序，所有重複的元素必定相鄰。</para>
    /// <para>因此只需要使用單指標遍歷鏈結串列，比較當前節點與下一個節點的值：</para>
    /// <list type="bullet">
    ///   <item>若相同：跳過下一個節點（將 next 指向 next.next）</item>
    ///   <item>若不同：移動指標到下一個節點繼續檢查</item>
    /// </list>
    /// 
    /// <para><b>時間複雜度：</b> O(n)，其中 n 為鏈結串列長度</para>
    /// <para><b>空間複雜度：</b> O(1)，僅使用常數額外空間</para>
    /// </summary>
    /// <param name="head">已排序鏈結串列的頭節點</param>
    /// <returns>刪除重複元素後的鏈結串列頭節點</returns>
    /// <example>
    /// <code>
    ///  輸入: [1,1,2] -> 輸出: [1,2]
    ///  輸入: [1,1,2,3,3] -> 輸出: [1,2,3]
    /// </code>
    /// </example>
    public static ListNode? DeleteDuplicates(ListNode? head)
    {
        // 邊界條件：若鏈結串列為空，直接回傳
        if (head is null)
        {
            return head;
        }

        // 使用 cur 指標從頭節點開始遍歷
        ListNode cur = head;

        // 持續遍歷直到沒有下一個節點
        while (cur.next is not null)
        {
            // 檢查當前節點與下一個節點的值是否相同
            if (cur.val == cur.next.val)
            {
                // 若相同，跳過下一個節點（刪除重複）
                // 注意：此處不移動 cur，因為新的 next 可能仍是重複值
                cur.next = cur.next.next;
            }
            else
            {
                // 若不同，移動到下一個節點繼續檢查
                cur = cur.next;
            }
        }

        // 回傳處理後的鏈結串列頭節點
        return head;
    }
}
