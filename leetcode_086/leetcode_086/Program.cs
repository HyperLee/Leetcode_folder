namespace leetcode_086;

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
    /// 86. Partition List
    /// https://leetcode.com/problems/partition-list/description/
    ///
    /// Given the head of a linked list and a value x, partition it such that all nodes less than x come before nodes greater than or equal to x.
    /// You should preserve the original relative order of the nodes in each of the two partitions.
    ///
    /// 86. 分隔鏈結串列
    /// https://leetcode.cn/problems/partition-list/description/
    ///
    /// 給定一個鏈結串列的頭節點 head 和一個值 x，請將鏈結串列分隔，使所有小於 x 的節點都出現在大於或等於 x 的節點之前。
    /// 你必須保留兩個分區中各節點原本的相對順序。
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// 直观来说我们只需维护两个链表 small 和 large 即可，small 链表按顺序存储所有小于 x 的节点，
    /// large 链表按顺序存储所有大于等于 x 的节点。
    /// 遍历完原链表后，我们只要将 small 链表尾节点指向 large 链表的头节点即能完成对链表的分隔。
    /// 
    /// 1. 小於 x 的 node 放前面, 大於等於放後面
    /// 2. 輸入相對順序不能變
    /// 
    /// 遍历结束后，我们将 large 的 next 指针置空，这是因为当前节点复用的是原链表的节点，而其 next 指针可能指向一个小于 x 的节点，我们需要切断这个引用。
    /// 
    /// ListNode 開頭插入 0, 避免為空衍生問題
    /// 之後取資料直接取 next 即可
    /// </summary>
    /// <param name="head"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public ListNode Partition(ListNode head, int x)
    {
        // 專門放小於 x; 開頭 node val = 0
        ListNode small = new ListNode(0);
        ListNode smallHead = small;

        // 專門放大於 x; 開頭 node val = 0;
        ListNode large = new ListNode(0);
        ListNode largeHead = large;

        while(head != null)
        {
            if(head.val < x)
            {
                // 小於 x
                small.next = head;
                small = small.next;
            }
            else
            {
                // 大於 x
                large.next = head;
                large = large.next;
            }
            head = head.next;
        }

        // large 結束指向 null
        large.next = null;

        // small 結尾接續上 large
        // largeHead 開頭是 0, 所以要取 next
        small.next = largeHead.next;

        // smallHead 開頭是 0, 所以要取 next
        return smallHead.next;
    }
}
