namespace leetcode_082;

class Program
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }    


    /// <summary>
    /// 82. Remove Duplicates from Sorted List II
    /// https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/description/
    /// 82. 删除排序链表中的重复元素 II
    /// https://leetcode.cn/problems/remove-duplicates-from-sorted-list-ii/description/
    ///
    /// English:
    /// Given the head of a sorted linked list, delete all nodes that have duplicate numbers,
    /// leaving only distinct numbers from the original list. Return the linked list sorted as well.
    ///
    /// 繁體中文:
    /// 給定一個已排序鏈結串列的頭節點，刪除所有具有重複數值的節點，
    /// 只保留原始串列中數值不重複的節點。回傳處理後仍為已排序的鏈結串列。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 題目敘述, ListNode 已經經過排序了.
    /// 所以相同的 node val 必定會相鄰
    /// 所以判斷相鄰是不是 相同 val 即可
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode DeleteDuplicates(ListNode head)
    {
        // 記錄上一個節點
        ListNode prev = null;
        // 記錄目前節點
        ListNode current = head;
        // 是否有相同的節點
        bool hasDuplicate = false;

        while (current != null && current.next != null)
        {
            if(current.val == current.next.val)
            {
                // 當前節點與下一個節點相同時, 當前節點next指向下下個節點
                // 再次循環直到不相同為止
                current.next = current.next.next;
                // 記錄節點需要被刪除
                hasDuplicate = true;
            }
            else
            {
                // 節點值不同時

                // 當前節點已經出現過相同節點, 直接替換刪除
                if(hasDuplicate)
                {
                    current.val = current.next.val;
                    current.next = current.next.next;
                    // 記錄節點刪除完成
                    hasDuplicate = false;
                }
                else
                {
                    // 更新上一個節點 val
                    prev = current;
                    // 繼續往下走
                    current = current.next;
                }
            }
        }

        // 由於要等到遇到不同節點時候才替換, 有可能最後節點依舊相同. 下個節點為空就無法刪除
        if(hasDuplicate) // 節點為刪除
        {
            if(prev == null)
            {
                return null;
            }

            // 上一個節點的 next 放空
            prev.next = null;
        }

        return head;
    }

    /// <summary>
    /// 題目敘述, ListNode 已經經過排序了.
    /// 所以相同的 node val 必定會相鄰
    /// 所以判斷相鄰是不是 相同 val 即可
    /// 
    /// 如果当前 cur.next 与 cur.next.next 对应的元素相同，那么我们就需要将 cur.next 以及所有后面拥有相同元素值的链表节点全部删除。
    /// 我们记下这个元素值 x，随后不断将 cur.next 从链表中移除，直到 cur.next 为空节点或者其元素值不等于 x 为止。
    /// 此时，我们将链表中所有元素值为 x 的节点全部删除。
    /// 
    /// 如果当前 cur.next 与 cur.next.next 对应的元素不相同，那么说明链表中只有一个元素值为 cur.next 的节点，那么我们就可以将
    /// cur 指向 cur.next。
    /// 
    /// 当遍历完整个链表之后，我们返回链表的的哑节点的下一个节点 dummy.next 即可。
    /// 
    /// 需要注意 cur.next 以及 cur.next.next 可能为空节点，如果不加以判断，可能会产生运行错误。
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode DeleteDuplicates2(ListNode head)
    {
        if(head == null)
        {
            return head;
        }

        // 開頭插入 0, 後續接上 head
        ListNode dummy = new ListNode(0, head);
        ListNode cur = dummy;

        // 下一個與下下一個不為空為停止條件
        while(cur.next != null && cur.next.next != null)
        {
            // 現在位置 node val
            int now = cur.val;

            // 下一個與下下一個是否相同
            if(cur.next.val == cur.next.next.val)
            {
                int x = cur.next.val;
                // 持續往下找, 直到不同 node val 為止
                while(cur.next != null && cur.next.val == x)
                {
                    // 有相同,就要找到新的不同 val 來替換
                    // 也可以說是刪除相同 val
                    cur.next = cur.next.next;
                }
            }
            else
            {
                // 往下走
                cur = cur.next;
            }
        }

        // 開頭有插入 dummy node, 最後回傳 dummy.next
        return dummy.next;
    }
}
