using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_061
{
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
        /// https://leetcode.com/problems/rotate-list/
        /// leetcode 016 Rotate List
        /// https://leetcode-cn.com/problems/rotate-list/solution/c-bi-huan-jie-fa-by-determined-moserpj7-ab5c/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(3);
            l1.next.next.next = new ListNode(4);
            l1.next.next.next.next = new ListNode(5);

            var res = RotateRight(l1, 2);
            while (res != null)
            {
                Console.WriteLine(res.val);
                //Console.WriteLine(res.val);
                res = res.next;
            }
            Console.ReadKey();
        }

        public static ListNode RotateRight(ListNode head, int k)
        {
            if (head == null)
                return head;

            ListNode p = head;
            int length = 1;

            while(p.next !=null)
            {
                p = p.next;
                length++;
            }
            p = p.next;

            int a = length - k % length - 1;

            p = head;
            while (a > 0)
            {
                p = p.next;
                a--;
            }
            head = p.next;
            p.next = null;

            return head;
        }
    }
}
