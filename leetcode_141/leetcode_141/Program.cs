using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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


        /// <summary>
        /// Floyd判圈算法(Floyd Cycle Detection Algorithm)，又稱龜兔賽跑算法(Tortoise and Hare Algorithm)
        /// 141. Linked List Cycle
        /// https://leetcode.com/problems/linked-list-cycle/description/
        /// 141. 环形链表
        /// https://leetcode.cn/problems/linked-list-cycle/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(3);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(0);
            l1.next.next.next = new ListNode(-4);
            l1.next.next.next.next = l1.next;

            Console.WriteLine("method1: " + HasCycle(l1));
            Console.WriteLine("method2: " + HasCycle2(l1));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
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

                // 這邊是判斷 慢指針 + 快指針的next
                // 才需要加上 try catch
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



        /// <summary>
        /// 參考解法
        /// 一樣是 雙指針
        /// Floyd判圈算法(Floyd Cycle Detection Algorithm)，又稱龜兔賽跑算法(Tortoise and Hare Algorithm)
        /// a
        /// 優化寫法, 忽略 try catch
        /// 找出錯誤點在哪裡
        /// 
        /// https://leetcode.cn/problems/linked-list-cycle/solutions/440042/huan-xing-lian-biao-by-leetcode-solution/
        /// https://leetcode.cn/problems/linked-list-cycle/solutions/1458267/by-stormsunshine-46rm/
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool HasCycle2(ListNode head)
        {
            if(head == null || head.next == null)
            {
                return false;
            }

            ListNode onestep = head;
            ListNode twostep = head;

            // 這邊改判斷 快指針 就可以不用寫 try catch
            // 快針比慢針還要快, 換句話說 快針沒有出現 null 那麼 慢針也不會
            // 判斷慢指針 會出現error
            while (twostep != null && twostep.next != null)
            {
                onestep = onestep.next;
                twostep = twostep.next.next;

                if(onestep == twostep)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
