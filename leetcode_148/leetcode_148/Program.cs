namespace leetcode_148;

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
    /// 148. Sort List
    /// https://leetcode.com/problems/sort-list/description/
    /// Given the head of a linked list, return the list after sorting it in ascending order.
    ///
    /// 148. 排序鏈結串列
    /// https://leetcode.cn/problems/sort-list/description/
    /// 給定一個 linked list 的 head，請將該 linked list 依照遞增順序排序後回傳。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 归并排序（迭代）
    /// 自底向上归并排序
    /// 
    /// 自底向上的意思是：
    /// - 首先，归并长度为 1 的子链表。例如 [4,2,1,3]，把第一个节点和第二个节点归并，第三个节点和第四个节点归并，得到 
    /// [2,4,1,3]。
    /// - 然后，归并长度为 2 的子链表。例如 [2,4,1,3]，把前两个节点和后两个节点归并，得到 [1,2,3,4]。
    /// - 然后，归并长度为 4 的子链表。
    /// - 依此类推，直到归并的长度大于等于链表长度为止，此时链表已经是有序的了。
    /// 
    /// 具体算法：
    /// 1. 遍历链表，获取链表长度 length。
    /// 2. 初始化步长 step=1。
    /// 3. 循环直到 step≥length。
    /// 4. 每轮循环，从链表头节点开始。
    /// 5. 分割出两段长为 step 的链表，合并，把合并后的链表插到新链表的末尾。重复该步骤，直到链表遍历完毕。
    /// 6. 把 step 扩大一倍。回到第 4 步。
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode SortList(ListNode head)
    {
        // 獲取鏈表長度
        int length = getListLength(head);
        // 用哨兵節點簡化代碼邏輯
        ListNode dummy = new ListNode(0, head);
        // step 為步長, 即參與合併的鏈表長度
        for(int step = 1; step < length; step *=2)
        {
            // 新鏈表的末尾
            ListNode newListTail = dummy;
            // 每輪循環的起始節點
            ListNode cur = dummy.next;
            while(cur != null)
            {
                // 從 cur 開始, 分割出兩段長為 step 的鏈表, 頭節點分別為 head1 和 head 2
                ListNode head1 = cur;
                ListNode head2 = splitList(head1, step);
                // 下一輪循環的起始節點
                cur = splitList(head2, step);
                // 合併兩段長為 step 的鏈表
                ListNode[] merged = mergeTwoLists(head1, head2);
                // 合併後的頭節點 merged[0], 插到 newListTail 的後面
                newListTail.next = merged[0];
                // merged[1] 現在是新鏈表的末尾
                newListTail = merged[1];
            }
        }
        return dummy.next;
    }

    /// <summary>
    /// 獲取鏈表長度
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    private int getListLength(ListNode head)
    {
        int length = 0;
        while(head != null)
        {
            length++;
            head = head.next;
        }
        return  length;
    }

    /// <summary>
    /// 分割鏈表
    /// 如果链表长度 <= size，不做任何操作，返回空节点
    /// 如果链表长度 > size，把链表的前 size 个节点分割出来（断开连接），并返回剩余链表的头节点
    /// </summary>
    /// <param name="head"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private ListNode splitList(ListNode head, int size)
    {
        // 先找到 nextHead 的前一個節點
        ListNode cur = head;
        for(int i = 0; i < size - 1 && cur != null; i++)
        {
            cur = cur.next;
        }

        // 如果鏈表長度 <= size
        if(cur == null || cur.next == null)
        {
            // 不做任何操作, 返回空節點
            return null;
        }

        ListNode nextHead = cur.next;
        // 斷開 nextHead 的前一個節點和 nextHead 的連接
        cur.next = null;
        return nextHead;
    }

    /// <summary>
    /// leetcode 21. 合并两个有序链表（双指针）
    /// 返回合并后的链表的头节点和尾节点
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    private ListNode[] mergeTwoLists(ListNode list1, ListNode list2)
    {
        // 用哨兵節點簡化代碼邏輯
        ListNode dummy = new ListNode();
        // cur 指向新鏈表的末尾
        ListNode cur = dummy;

        while(list1 != null && list2 != null)
        {
            if(list1.val < list2.val)
            {
                // 把 list1 加到新鏈表中
                cur.next = list1;
                list1 = list1.next;
            }
            else
            {
                // 注: 相等的情況加哪個節點都是可以的
                // 把 list2 加到新鏈表中
                cur.next = list2;
                list2 = list2.next;
            }
        }

        cur.next = list1 != null ? list1 : list2; // 拼接剩余链表

        while(cur.next != null)
        {
            cur = cur.next;
        }

        return new ListNode[]{dummy.next, cur};
    }
}
