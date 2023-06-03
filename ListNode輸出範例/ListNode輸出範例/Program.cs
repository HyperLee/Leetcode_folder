using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListNode輸出範例
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
        /// 舉例為 leetcode 203 Remove Linked List Elements
        /// https://leetcode.com/problems/remove-linked-list-elements/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode list = new ListNode(1);
            list.next = new ListNode(2);
            list.next.next = new ListNode(6);
            list.next.next.next = new ListNode(3);
            list.next.next.next.next = new ListNode(4);
            list.next.next.next.next.next = new ListNode(5);
            list.next.next.next.next.next.next = new ListNode(6);

            // ListNode 輸出方式 範例
            var res = RemoveElements(list, 6);
            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }

            Console.ReadKey();
        }


        /// <summary>
        /// 遞迴處理
        /// 遇到 head.val == val
        /// 就直接往下接 head.next
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ListNode RemoveElements(ListNode head, int val)
        {
            if (head == null)
            {
                return head;
            }

            head.next = RemoveElements(head.next, val);

            return head.val == val ? head.next : head;
        }


    }
}
