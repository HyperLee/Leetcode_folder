namespace leetcode_203;

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
    /// 203. Remove Linked List Elements
    /// https://leetcode.com/problems/remove-linked-list-elements/description/
    /// 203. 移除鏈結串列元素
    /// https://leetcode.cn/problems/remove-linked-list-elements/description/
    ///
    /// Given the head of a linked list and an integer val, remove all the nodes of the linked list that has Node.val == val, and return the new head.
    /// 給定一個鏈結串列的頭節點 head 與一個整數 val，請移除鏈結串列中所有 Node.val == val 的節點，並回傳新的頭節點。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一:遞迴
    /// 链表的定义具有递归的性质，因此链表题目常可以用递归的方法求解。
    /// 这道题要求删除链表中所有节点值等于特定值的节点，可以用递归实现。
    /// 
    /// 对于给定的链表，首先对除了头节点 head 以外的节点进行删除操作，然后判断 head 的节点值是否等于给定的 val。
    /// 如果 head 的节点值等于 val，则 head 需要被删除，因此删除操作后的头节点为 head.next；
    /// 如果 head 的节点值不等于 val，则 head 保留，因此删除操作后的头节点还是 head。上述过程是一个递归的过程。
    /// 
    /// 递归的终止条件是 head 为空，此时直接返回 head。当 head 不为空时，递归地进行删除操作，然后判断 head 的节点值是否等于
    /// val 并决定是否要删除 head。
    /// 
    /// </summary>
    /// <param name="head"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public ListNode RemoveElements(ListNode head, int val)
    {
        if(head == null)
        {
            return head;
        }

        head.next = RemoveElements(head.next, val);

        if(head.val == val)
        {
            return head.next;
        }
        else
        {
            return head;
        }
    }

    /// <summary>
    /// 解法二: 迭代
    /// 也可以用迭代的方法删除链表中所有节点值等于特定值的节点。
    /// 用 temp 表示当前节点。如果 temp 的下一个节点不为空且下一个节点的节点值等于给定的 val，则需要删除下一个节点。
    /// 删除下一个节点可以通过以下做法实现：
    /// temp.next=temp.next.next
    /// 如果 temp 的下一个节点的节点值不等于给定的 val，则保留下一个节点，将 temp 移动到下一个节点即可。
    /// 当 temp 的下一个节点为空时，链表遍历结束，此时所有节点值等于 val 的节点都被删除。
    /// 具体实现方面，由于链表的头节点 head 有可能需要被删除，因此创建哑节点 dummyHead，令 dummyHead.next=head，初始化
    /// temp=dummyHead，然后遍历链表进行删除操作。最终返回 dummyHead.next 即为删除操作后的头节点。
    /// </summary>
    /// <param name="head"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public ListNode RemoveElements2(ListNode head, int val)
    {
        ListNode dummyHead = new ListNode(0);
        dummyHead = head;
        ListNode temp = dummyHead;

        while(temp.next != null)
        {
            if(temp.next.val == val)
            {
                // 刪除下一個節點
                temp.next = temp.next.next;
            }
            else
            {
                // 繼續向後遍歷listnode
                temp = temp.next;
            }
        }
        return dummyHead.next;
    }
}
