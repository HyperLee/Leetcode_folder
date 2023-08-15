using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_086
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
        /// 86. Partition List
        /// https://leetcode.com/problems/partition-list/
        /// 86. 分隔链表
        /// https://leetcode.cn/problems/partition-list/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode input = new ListNode(1);
            input.next = new ListNode(4);
            input.next.next = new ListNode(3);
            input.next.next.next = new ListNode(2);
            input.next.next.next.next = new ListNode(5);
            input.next.next.next.next.next = new ListNode(2);

            int x = 3;

            //Console.WriteLine(Partition(input, x));
            // ListNode 輸出方式 範例
            var res = Partition(input, x);
            
            Console.WriteLine("ANS: " );
            while (res != null)
            {
                Console.Write(res.val + ", ");
                res = res.next;
            }

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/partition-list/solutions/543768/fen-ge-lian-biao-by-leetcode-solution-7ade/
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static ListNode Partition(ListNode head, int x)
        {
            ListNode small = new ListNode(0);
            ListNode smallHead = small;
            ListNode large = new ListNode(0);
            ListNode largeHead = large;

            while(head != null)
            {
                if(head.val < x)
                {
                    small.next = head;
                    small = small.next;
                }
                else
                {
                    large.next = head;
                    large = large.next;
                }
                head = head.next;
            }

            large.next = null;
            small.next = largeHead.next;

            return smallHead.next;
        }


    }
}
