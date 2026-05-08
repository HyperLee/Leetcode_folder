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
}
