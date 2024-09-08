using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_021
{
    class Program
    {


        /// <summary>
        /// 
        /// </summary>
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
        /// 21. Merge Two Sorted Lists
        /// https://leetcode.com/problems/merge-two-sorted-lists/description/
        /// 
        /// 21. 合并两个有序链表
        /// https://leetcode.cn/problems/merge-two-sorted-lists/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(4);
            //l1.next.next.next = new ListNode(9);
            //l1.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next.next = new ListNode(9);

            ListNode l2 = new ListNode(1);
            l2.next = new ListNode(3);
            l2.next.next = new ListNode(4);
            //l2.next.next.next = new ListNode(9);

            //Console.WriteLine(MergeTwoLists(l1, l2));

            var res = MergeTwoLists(l1, l2);

            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }

            Console.ReadKey();

        }

        /// <summary>
        /// ref:
        /// https://ithelp.ithome.com.tw/articles/10222827
        /// 
        /// 主要是採取遞迴方式實作
        /// 如過不用遞迴 也可以採用 while 去跑
        /// 但是遞迴 比較多人使用
        /// 
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null)
            {
                return null;
            }

            if (l1 == null)
            {
                return l2;
            }

            if (l2 == null)
            {
                return l1;
            }

            if (l1.val <= l2.val)
            {
                // 去比較 l1.next 及 l2 的大小並串接
                l1.next = MergeTwoLists(l1.next, l2);
                // 因為 l1 比較小要先串接
                return l1; 
            }
            else
            {
                // 去比較 l2.next 及 l1 的大小並串接
                l2.next = MergeTwoLists(l1, l2.next);
                // 因為 l2 比較小要先串接
                return l2; 
            }

        }


    }
}
