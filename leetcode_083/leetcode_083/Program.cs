using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_083
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

    internal class Program
    {
        /// <summary>
        /// leetcode 083
        /// Remove Duplicates from Sorted List
        /// https://leetcode.com/problems/remove-duplicates-from-sorted-list/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(2);
            l1.next.next.next = new ListNode(4);
            //l1.next.next.next.next = new ListNode(5);
            //l1.next.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next.next = new ListNode(77);

            //Console.WriteLine(DeleteDuplicates(l1));

            var res = DeleteDuplicates(l1);
            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }

            Console.ReadKey();
        }


        public static ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null)
            {
                return head;
            }

            ListNode cur = head;
            while (cur.next != null)
            {
                if (cur.val == cur.next.val)
                {
                    cur.next = cur.next.next;
                }
                else
                {
                    cur = cur.next;
                }
            }

            return head;
        }

    }
}
