using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(3);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(0);
            l1.next.next.next = new ListNode(-4);
            l1.next.next.next.next = l1.next;

            Console.WriteLine(HasCycle(l1));
            Console.ReadKey();
        }


        /// <summary>
        /// Floyd判圈算法(Floyd Cycle Detection Algorithm)，又稱龜兔賽跑算法(Tortoise and Hare Algorithm)
        /// leetcode 141
        /// https://leetcode.com/problems/linked-list-cycle/
        /// 
        /// https://ithelp.ithome.com.tw/articles/10223417
        /// https://leetcode.cn/problems/linked-list-cycle/solution/huan-xing-lian-biao-by-leetcode-solution/
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool HasCycle(ListNode head)
        {
            try
            {
                if (head == null)
                {
                    return false;
                }

                var oneStep = head;
                var twoStep = head;
                while (oneStep.next != null && twoStep.next != null)
                {
                    oneStep = oneStep.next;
                    twoStep = twoStep.next.next;

                    if (oneStep == twoStep)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
