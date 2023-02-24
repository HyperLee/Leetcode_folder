using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// leetcode 206 Reverse Linked List
        /// https://leetcode.com/problems/reverse-linked-list/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(3);
            l1.next.next.next = new ListNode(4);
            l1.next.next.next.next = new ListNode(5);
            l1.next.next.next.next.next = new ListNode(9);
            l1.next.next.next.next.next.next = new ListNode(77);

            var res = ReverseList(l1);
            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }

            Console.ReadKey();
        }


        /// <summary>
        /// 本解法為 Iteration
        /// https://ithelp.ithome.com.tw/articles/10225226
        /// 主要是邊找下一個 head node，邊將 head node 串接到新的 LinkedList root 前面，這樣就會是反轉的
        /// 
        /// 另有 Recursion
        /// https://ithelp.ithome.com.tw/articles/10224711
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode ReverseList(ListNode head)
        {
            if (head == null)
                return head;

            ListNode root = null;
            while (head != null)
            {
                ListNode next = head.next;
                head.next = root;
                root = head; // 把原先輸入list的head 放到 新的 ListNode root 裡面當root
                head = next; 
            }
            return root; //一次一次找
        }

    }
}
