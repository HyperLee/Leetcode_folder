using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_021
{
    class Program
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
        /// leetcode 21
        /// https://leetcode.com/problems/merge-two-sorted-lists/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            /*
            List<int> iList = new List<int>();
            iList.Add(1);//0
            iList.Add(2);//1
            iList.Add(4);//2
            //iList.Add(7);//3
            //iList.Add(7);//4

            List<int> iList2 = new List<int>();
            iList2.Add(1);//0
            iList2.Add(3);//1
            iList2.Add(4);//2
            */
            //Console.Write(MergeTwoLists("1,2,4", iList2));

            ListNode l1 = new ListNode(1);
            //l1.next = new ListNode(2);
            //l1.next.next = new ListNode(3);
            //l1.next.next.next = new ListNode(9);
            //l1.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next = new ListNode(9);
            //l1.next.next.next.next.next.next = new ListNode(9);

            ListNode l2 = new ListNode(3);
            //l2.next = new ListNode(3);
            //l2.next.next = new ListNode(4);
            //l2.next.next.next = new ListNode(9);

            Console.WriteLine(MergeTwoLists(l1, l2));
            Console.ReadKey();

        }

        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10222827
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null)
                return null;
            if (l1 == null)
                return l2;
            if (l2 == null)
                return l1;

            if (l1.val <= l2.val)
            {
                l1.next = MergeTwoLists(l1.next, l2);  //去比較 l1.next 及 l2 的大小並串接
                return l1; // 因為 l1 比較小要先串接
            }
            else
            {
                l2.next = MergeTwoLists(l1, l2.next); // 去比較 l2.next 及 l1 的大小並串接
                return l2; // 因為 l2 比較小要先串接
            }
        }


    }
}
