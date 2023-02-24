using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_002
{
    class Program
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        /// <summary>
        /// https://leetcode.com/problems/add-two-numbers/
        /// https://www.itread01.com/content/1545328944.html  --> function1
        /// https://blog.csdn.net/weixin_41969800/article/details/121118863  --> function2
        /// 
        /// 002. Add Two Numbers
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(2);
            l1.next = new ListNode(4);
            l1.next.next = new ListNode(3);
            //l1.next.next.next = new ListNode(9);
            //l1.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next.next = new ListNode(9);

            ListNode l2 = new ListNode(5);
            l2.next = new ListNode(6);
            l2.next.next = new ListNode(4);
            //l2.next.next.next = new ListNode(9);

            var res = addTwoNumbers(l1, l2);
            while (res != null)
            {
                Console.WriteLine("function1:" + res.val);
                //Console.WriteLine(res.val);
                res = res.next;
            }
            //Console.WriteLine("-----------------");
            //var res3 = addTwoNumbers2(l1, l2);
            //while (res3 != null)
            //{
            //    Console.WriteLine("function2:" + res3.val);
            //    //Console.WriteLine(res.val);
            //    res3 = res3.next;
            //}

            Console.ReadKey();
        }

        public static ListNode addTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode l3 = new ListNode(0);
            ListNode head = l3;
            int sum = 0;
            while (l1 != null || l2 != null)
            {
                sum = sum > 9 ? 1 : 0;
                if (l1 != null)
                {
                    sum += l1.val;
                    l1 = l1.next;
                }
                if (l2 != null)
                {
                    sum += l2.val;
                    l2 = l2.next;
                }
                //存儲在l3中
                l3.next = new ListNode(sum % 10);
                l3 = l3.next;
            }
            //判斷最後一項是否和大於9，大於則需要再添加一個1.
            if (sum > 9)
            {
                l3.next = new ListNode(1);
            }
            return head.next;
        }

        public static ListNode addTwoNumbers2(ListNode l1, ListNode l2)
        {
            ListNode resultnode = new ListNode(0);
            ListNode curr = resultnode;

            int carry = 0;
            while (l1 != null || l2 != null)
            {
                int d1 = l1 == null ? 0 : l1.val;
                int d2 = l2 == null ? 0 : l2.val;

                int sum = d1 + d2 + carry;
                carry = sum > 9 ? 1 : 0;

                curr.next = new ListNode(sum % 10);
                curr = curr.next;
                if (l1 != null) l1 = l1.next;
                if (l2 != null) l2 = l2.next;


            }

            if (carry == 1) curr.next = new ListNode(1);
            return resultnode.next;


        }


    }
}
