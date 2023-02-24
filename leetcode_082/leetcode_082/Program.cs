using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_082
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
        /// leetcode 082
        /// Remove Duplicates from Sorted List II
        /// https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(3);
            l1.next.next.next = new ListNode(3);
            l1.next.next.next.next = new ListNode(4);
            l1.next.next.next.next.next = new ListNode(4);
            l1.next.next.next.next.next.next = new ListNode(5);

            var res = DeleteDuplicates(l1);
            while (res != null)
            {
                Console.WriteLine("Ans:" + res.val);
                res = res.next;
            }

            Console.ReadKey();

        }

        /// <summary>
        /// https://www.codetd.com/article/3706450
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode DeleteDuplicates(ListNode head) {

            ListNode last = null; // 紀錄上一個節點
            ListNode curr = head;

            bool issame = false; // 是否有相同的節點

            while(curr != null && curr.next != null)
            {
                if(curr.val == curr.next.val)
                {
                    // 當前節點與下一個節點相同時, 當前節點next指向下下個節點
                    // 再次循環直到不相同為止
                    curr.next = curr.next.next;

                    issame = true; // 記錄節點需要被刪除
                }
                else
                {
                    // 節點值不同時

                    // 當前節點已經出現過相同節點, 直接替換刪除
                    if(issame)
                    {
                        curr.val = curr.next.val;
                        curr.next = curr.next.next;
                        issame = false; // 紀錄節點刪除
                    }
                    else
                    {
                        last = curr;
                        curr = curr.next;
                    }
                }
            }
            // 由於要等到遇到不同節點時候才替換, 有可能最後節點依舊相同. 下個節點為空就無法刪除
            if(issame)// 節點未刪除
            {
                if(last == null)
                {
                    return null;
                }
                last.next = null;  // 上一個節點的next放空
            }

            return head;

        }


    }
}
