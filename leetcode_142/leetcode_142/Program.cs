using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_142
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
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(3);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(0);
            l1.next.next.next = new ListNode(-4);
            l1.next.next.next.next = l1.next;
            //l1.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next.next = new ListNode(9);

            //Console.WriteLine(DetectCycle(l1));
            DetectCycle(l1);
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.com/problems/linked-list-cycle-ii/
        /// leetcode 142
        /// 
        /// https://ithelp.ithome.com.tw/articles/10223721
        /// 
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
        public static ListNode DetectCycle(ListNode head)
        {
            try
            {
                if (head == null)
                    return head;

                var oneStep = head;
                var twoStep = head;
                while (oneStep.next != null && twoStep.next != null)
                {
                    oneStep = oneStep.next;
                    twoStep = twoStep.next.next;
                    if (oneStep == twoStep)
                    {
                        // oneStep2 從頭走為a路徑, 
                        // 原先的oneStep繼續走為c路徑
                        // 兩者交會 就是答案
                        var oneStep2 = head;
                        while (oneStep.next != null && oneStep2.next != null)
                        {
                            if (oneStep == oneStep2)
                            {
                                Console.WriteLine("result: " +  oneStep.val);
                                return oneStep;
                            }

                            oneStep = oneStep.next;
                            oneStep2 = oneStep2.next;
                        }
                    }

                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
