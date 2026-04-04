namespace leetcode_024;

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
    /// Given a linked list, swap every two adjacent nodes and return its head.
    /// You must solve the problem without modifying the values in the list's nodes (i.e., only nodes themselves may be changed.)
    /// 給定一個鏈表，兩兩交換相鄰節點並返回其頭節點。
    /// 你必須在不修改節點值的情況下解題（即只能改變節點本身的連接）。
    /// 24. Swap Nodes in Pairs
    /// https://leetcode.com/problems/swap-nodes-in-pairs/description/
    /// 24. 两两交换链表中的节点
    /// https://leetcode.cn/problems/swap-nodes-in-pairs/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }


    /// <summary>
    /// 遞迴
    /// 相鄰兩個交換, 所以至少需要兩個
    /// 當為null or 只有一個node 便無法交換
    /// 奇數最後一個無法交換
    /// 偶數才能全部交換
    /// 
    /// 交換方法
    /// 原始head頭節點:  變成newhead的二節點
    /// 原始head二節點:  變成newhead的頭節點
    /// 
    /// 交換完之後 newhead.next = head
    /// 就能繼續交換之後的node
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode SwapPairs(ListNode head)
    {
        if(head == null || head.next == null)
        {
            return head;
        }

        ListNode newHead = head.next;
        head.next = SwapPairs(newHead.next);
        newHead.next = head;
        return newHead;
    }
}
