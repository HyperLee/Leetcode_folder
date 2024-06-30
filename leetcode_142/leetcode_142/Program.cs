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


        /// <summary>
        /// 142. Linked List Cycle II
        /// https://leetcode.com/problems/linked-list-cycle-ii/
        /// 142. 环形链表 II
        /// https://leetcode.cn/problems/linked-list-cycle-ii/description/
        /// 
        /// 題目141延伸題目
        /// 141是考試不是一個 linklist
        /// 142考試哪一個node造成 linklist
        /// 
        /// </summary>
        /// <param name="args"></param>
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
            DetectCycle2(l1);
            Console.ReadKey();
        }


        /// <summary>
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
                                Console.WriteLine("result1: " +  oneStep.val);
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



        /// <summary>
        /// 優化上方 方法
        /// 省略 try catch
        /// 找出錯誤點在哪裡
        /// 
        /// 
        /// 快針能走, 那麼慢針也肯定能走
        /// 因為快針走比較快
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode DetectCycle2(ListNode head)
        {
            if (head == null)
            {
                return head;
            }

            var oneStep = head;
            var twoStep = head;

            // 這邊改判斷 快指針的 就可以不用寫 try catch
            // 判斷慢指針 會出現error
            while (twoStep != null && twoStep.next != null)
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
                            Console.WriteLine("result2: " + oneStep.val);
                            return oneStep;
                        }

                        oneStep = oneStep.next;
                        oneStep2 = oneStep2.next;
                    }
                }

            }

            return null;
        }



    }
}
