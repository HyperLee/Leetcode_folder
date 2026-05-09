namespace leetcode_142;

class Program
{

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }

    /// <summary>
    /// 142. Linked List Cycle II
    /// https://leetcode.com/problems/linked-list-cycle-ii/description/
    /// 142. 環形鏈結串列 II
    /// https://leetcode.cn/problems/linked-list-cycle-ii/description/
    ///
    /// English:
    /// Given the head of a linked list, return the node where the cycle begins.
    /// If there is no cycle, return null.
    ///
    /// There is a cycle in a linked list if there is some node in the list that
    /// can be reached again by continuously following the next pointer. Internally,
    /// pos is used to denote the index of the node that tail's next pointer is
    /// connected to (0-indexed). It is -1 if there is no cycle. Note that pos is
    /// not passed as a parameter.
    ///
    /// Do not modify the linked list.
    ///
    /// 繁體中文:
    /// 給定一個鏈結串列的頭節點 head，請回傳環開始的節點。
    /// 如果鏈結串列中沒有環，請回傳 null。
    ///
    /// 如果鏈結串列中存在某個節點，可以透過不斷沿著 next 指標前進而再次到達該節點，
    /// 則表示這個鏈結串列中存在環。在內部，pos 用來表示尾節點的 next 指標連接到的節點索引
    /// (索引從 0 開始)。如果沒有環，pos 為 -1。請注意，pos 不會作為參數傳入。
    ///
    /// 請勿修改鏈結串列。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 快慢針解法:
    /// slow * 2 = fast;
    /// slow = a + b;
    /// fast = a + b + c + b = a + 2*b + c;
    /// (a + b)*2 = a + 2*b + c;
    /// a = c;
    /// 
    /// 快针走的是慢针的两倍。
    /// 慢针走过的路，快针走过一遍。
    /// 快针走过的剩余路程，也就是和慢针走过的全部路程相等。(a+b = c+b)
    /// 刨去快针追赶慢针的半圈(b)，剩余路程即为所求入环距离(a=c)
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode DetectCycle(ListNode head)
    {
        if(head == null)
        {
            return head;
        }

        var oneStep = head;
        var twoStep = head;

        while(oneStep.next != null && twoStep.next != null)
        {
            // 慢針
            oneStep = oneStep.next;
            // 快針
            twoStep = twoStep.next.next;

            if(oneStep == twoStep)
            {
                var oneStep2 = head;
                while(oneStep.next != null && oneStep2.next != null)
                {
                    // oneStep2 從頭走為a路徑, 
                    // 原先的oneStep繼續走為c路徑
                    // 兩者交會 就是答案
                    if(oneStep == oneStep2)
                    {
                        return oneStep;
                    }
                    oneStep = oneStep.next;
                    oneStep2 = oneStep2.next;
                }
            }
        }
        return null;
    }
}
