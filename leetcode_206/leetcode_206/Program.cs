namespace leetcode_206
{
    internal class Program
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
        /// 206. Reverse Linked List
        /// https://leetcode.com/problems/reverse-linked-list/
        /// 
        /// 206. 反转链表
        /// https://leetcode.cn/problems/reverse-linked-list/description/
        /// 
        /// 偏好方法一解法, 要多想幾次
        /// 會比較好理解
        /// 先暫存 下個交換目標
        /// next 指向新的 ListNode
        /// 將 node 轉換至新的 ListNode
        /// 輪到當初暫存來當新的 head
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(3);
            l1.next.next.next = new ListNode(4);
            l1.next.next.next.next = new ListNode(5);
            //l1.next.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next.next = new ListNode(77);

            var res = ReverseList(l1);
            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }

            Console.WriteLine("--------------");

            ListNode l2 = new ListNode(1);
            l2.next = new ListNode(2);
            l2.next.next = new ListNode(3);
            l2.next.next.next = new ListNode(4);
            l2.next.next.next.next = new ListNode(5);

            var res2 = ReverseList2(l2);
            while (res2 != null)
            {
                Console.WriteLine("Ans2:" + res2.val);
                res2 = res2.next;
            }

        }


        /// <summary>
        /// 解法來源:
        /// 本解法為 Iteration
        /// https://ithelp.ithome.com.tw/articles/10225226
        /// 主要是邊找下一個 head node，邊將 head node 串接到新的 LinkedList root 前面，這樣就會是反轉的
        /// 
        /// 另有 Recursion
        /// https://ithelp.ithome.com.tw/articles/10224711
        /// 
        /// -- 解題概念
        /// 0. 宣告  ListNode root 為回傳答案使用, 新的  ListNode
        /// 1. 先宣告一個暫存的 ListNode next, 儲存下一個要交換得目標節點
        /// 2. 將 head.next 指向 root
        /// 3. 上述步驟已經指向了, 所以這邊可以直接接收 root = head
        /// 4. 因為 head 已經給 root 了, 所以要輪到暫存的 next 來當作新的 head . 
        /// 5. 持續上述步驟
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode ReverseList(ListNode head)
        {
            if (head == null)
            {
                return head;
            }

            // 回傳答案
            ListNode root = null;

            while (head != null)
            {
                // 宣告 ListNode next 記錄當下的 next node 是誰
                ListNode next = head.next;
                // next 改 串接 root
                head.next = root;
                // 把原先輸入 list 的 head 放到 新的 ListNode root 裡面當 root; root 則改為已經串接好的 head
                root = head;
                // 第二個 node 給 head 達到交換; head 則改為原本的 next
                head = next; 
            }

            // 回傳已經反轉的 LinkedList
            return root; 
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/reverse-linked-list/solutions/551596/fan-zhuan-lian-biao-by-leetcode-solution-d1k2/
        /// https://leetcode.cn/problems/reverse-linked-list/solutions/1992225/you-xie-cuo-liao-yi-ge-shi-pin-jiang-tou-o5zy/
        /// 
        /// 假设链表为 1→2→3→∅，我们想要把它改成 ∅←1←2←3。
        /// 在遍历链表时，将当前节点的 next 指针改为指向前一个节点。由于节点没有引用其前一个节点，因此必须事先存储其前一个节点。
        /// 在更改引用之前，还需要存储后一个节点。最后返回新的头引用。
        /// 
        /// 做这种题时，关键是有一个意识，去想象”空“的存在，链表的前和后都有”空“的存在，而尾节点更是指向”空“ 然后，记得要有3个指针，prev，curr，
        /// next，它们组成一个前中后关系，从链表的一头移向另一头，起始时 prev 为空，结束时 next 为空。翻转链表时不要去想象一个新链表，而是想象对
        /// 旧链表的指针做修改，开始时，prev 指向空，curr 指向头节点，next 尚未存在。进入循环后，next 成为 curr.next，curr 指向 prev，prev 成为 curr，
        /// curr 成为 next
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode ReverseList2(ListNode head)
        {
            if(head == null)
            {
                return head;
            }

            // 回傳答案
            ListNode prev = null;
            // 現在 node
            ListNode curr = head;

            while(curr != null)
            {
                // 未来出于现在; 暫存下一個交換目標
                ListNode next = curr.next;
                // 现在指向过去; 先指向至新的 ListNode
                curr.next = prev;
                // 过去成为现在; 因為上個步驟已經指向了, 所以可以把 node 給轉過去新的 ListNode
                prev = curr;
                // 现在成为未来; 原先的 head 已經轉去給新的 ListNode, 所以原先的 head.next 變成新的 head node
                curr = next;
            }

            return prev;
        }
    }
}
