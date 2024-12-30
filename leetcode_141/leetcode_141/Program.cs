namespace leetcode_141
{
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
        /// Floyd判圈算法(Floyd Cycle Detection Algorithm)，又稱龜兔賽跑算法(Tortoise and Hare Algorithm)
        /// 141. Linked List Cycle
        /// https://leetcode.com/problems/linked-list-cycle/description/
        /// 141. 环形链表
        /// https://leetcode.cn/problems/linked-list-cycle/description/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(3);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(0);
            l1.next.next.next = new ListNode(-4);
            l1.next.next.next.next = l1.next;

            Console.WriteLine("res: " + HasCycle(l1));

        }


        /// <summary>
        /// 雙指針, 
        /// 慢針: 一次走一步
        /// 快針: 一次走兩步
        /// 
        /// ref:
        /// https://leetcode.cn/problems/linked-list-cycle/solutions/440042/huan-xing-lian-biao-by-leetcode-solution/
        /// https://leetcode.cn/problems/linked-list-cycle/solutions/1458267/by-stormsunshine-46rm/
        /// https://ithelp.ithome.com.tw/articles/10223417
        /// https://leetcode.cn/problems/linked-list-cycle/solution/huan-xing-lian-biao-by-leetcode-solution/
        /// 
        /// 鏈結串列循環的基本概念
        /// 定義：鏈結串列循環是指在鏈結串列中存在一個節點，其 next 指針指向之前的某個節點，從而形成一個環。
        /// 檢測方法：常見的檢測方法是使用「龜兔賽跑」算法（Floyd's Cycle-Finding Algorithm），即使用兩個指針以不同速度遍歷鏈結串列。
        /// 
        /// 檢測循環 (HasCycle)：使用兩個指針（slow 和 fast），slow 每次移動一步，fast 每次移動兩步。如果 slow 和 fast 相遇，則表示存在循環。
        /// 這個方法的時間複雜度為 O(n)，空間複雜度為 O(1)，非常高效。
        /// 
        /// 可以詢問 GPT 說明解法與介紹
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool HasCycle(ListNode head)
        {
            // 如果鏈結串列為空或只有一個節點，則不可能存在循環，直接返回 false。
            if (head == null || head.next == null)
            {
                return false;
            }

            // slow：每次移動一步。
            ListNode slow = head;
            // fast：每次移動兩步。
            ListNode fast = head.next;

            // 如果 slow 和 fast 相遇，表示存在循環，返回 true。
            while (fast != null && fast.next != null)
            {
                if (slow == fast)
                {
                    return true;
                }

                slow = slow.next;
                fast = fast.next.next;
            }

            // 如果 fast 到達鏈結串列的末端，表示不存在循環，返回 false。
            return false;
        }

    }
}
